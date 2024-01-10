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

        public double[]? ReadData()
        {
            if(isOpen)
            {
                return new double[4]
                {
                    100d,
                    100d,
                    100d,
                    100d
                };
            }

            throw new Exception("Należy najpierw otworzyć port, aby czytać dane!");
        }
    }
}
