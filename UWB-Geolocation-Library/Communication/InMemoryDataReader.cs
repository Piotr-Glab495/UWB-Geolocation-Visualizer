namespace UWB_Geolocation_Library.Communication
{
    internal class InMemoryDataReader : IDataReader
    {
        private bool isOpen;

        public void ClosePort()
        {
            isOpen = false;
        }

        public void OpenPort()
        {
            isOpen = true;
        }

        public async Task<double[]?> ReadDataAsync()
        {
            if(isOpen)
            {
                double[] distances = new double[4]
                {
                    100d,
                    100d,
                    100d,
                    100d
                };

                return await Task.FromResult<double[]?>(distances);
            }
            else
            {
                return await Task.FromResult<double[]?>(null);
            }
        }
    }
}
