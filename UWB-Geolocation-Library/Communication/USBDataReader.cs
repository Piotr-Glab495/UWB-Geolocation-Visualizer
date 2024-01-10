using System.IO.Ports;

namespace UWB_Geolocation_Library.Communication
{
    internal class USBDataReader : IDataReader
    {
        private readonly SerialPort serialPort;
        private TaskCompletionSource<double[]> dataReceivedTaskCompletionSource;

        public USBDataReader(string portName, int baudRate)
        {
            serialPort = new SerialPort(portName, baudRate);
            dataReceivedTaskCompletionSource = new TaskCompletionSource<double[]>();
        }

        public void ClosePort()
        {
            if(serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        public void OpenPort()
        {
            serialPort.Open();
        }

        public async Task<double[]?> ReadDataAsync()
        {
            dataReceivedTaskCompletionSource = new TaskCompletionSource<double[]>();

            if (serialPort.IsOpen)
            {
                //TODO: how to get the correct vector?
                serialPort.BaseStream.BeginRead(new byte[1024], 0, 1024, DataReadCallback, null);
                return await dataReceivedTaskCompletionSource.Task;
            }

            return null;
        }

        private void DataReadCallback(IAsyncResult result)
        {
            try
            {
                int bytesRead = serialPort.BaseStream.EndRead(result);

                if (bytesRead > 0) //TODO: think about the size
                {
                    string data = serialPort.ReadExisting();
                    double[] distances = ParseData(data);

                    dataReceivedTaskCompletionSource.SetResult(distances);
                }
            }
            catch (Exception ex)
            {
                dataReceivedTaskCompletionSource.SetException(ex);
            }
        }

        /**
         * <summary>
         *  This is a method use to transform the data from a device format (currently "A1:W1, A2:W2, A3:W3, A4:W4") to an array of doubles of distances
         * </summary>
         */
        private static double[] ParseData(string data)
        {
            string[] tokens = data.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            double[] distances = new double[tokens.Length];

            for (int i = 0; i < tokens.Length; ++i)
            {
                string[] subTokens = tokens[i].Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (double.TryParse(subTokens[1], out double distance))
                {
                    distances[i] = distance;
                }
                else
                {
                    // TODO: Handle parsing error
                    distances[i] = 0.0d;
                }
            }

            return distances;
        }
    }
}
