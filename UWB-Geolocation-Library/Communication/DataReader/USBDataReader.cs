using System.Globalization;
using System.IO.Ports;

namespace UWB_Geolocation_Library.Communication.DataReader
{
    internal class USBDataReader : IDataReader
    {
        private readonly SerialPort serialPort;

        public USBDataReader(string portName, int baudRate)
        {
            serialPort = new SerialPort(portName, baudRate);
        }

        public void ClosePort()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        public void OpenPort()
        {
            serialPort.Open();
        }

        public double[]? ReadData()
        {
            if (serialPort.IsOpen)
            {
                int bytesRead = serialPort.BaseStream.Read(new byte[40], 0, 40);
                if (bytesRead > 0)
                {
                    string line;
                    do { 
                        line = serialPort.ReadLine();
                    } while (!line.StartsWith("p_A1"));
                    double[] distances = ParseData(line);
                    return distances;
                }
                return null;
            }
            else
            {
                throw new Exception("Należy najpierw otworzyć port, aby czytać dane!");
            }
        }

        /**
         * <summary>
         *  This is a method use to transform the data from a device format (currently "A1:W1, A2:W2, A3:W3, A4:W4") to an array of doubles of distances
         * </summary>
         */
        private static double[] ParseData(string data)
        {
            string[] tokens = data.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            double[] distances = new double[tokens.Length];

            for (int i = 0; i < tokens.Length; ++i)
            {
                string[] subTokens = tokens[i].Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (double.TryParse(subTokens[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double distance))
                {
                    distances[i] = distance;
                }
                else
                {
                    throw new Exception("Błąd parsowania danych " + subTokens[1] + " na typ double ");
                }
            }

            return distances;
        }
    }
}
