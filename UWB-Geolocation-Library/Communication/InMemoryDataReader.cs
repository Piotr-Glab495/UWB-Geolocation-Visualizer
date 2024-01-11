namespace UWB_Geolocation_Library.Communication
{
    internal class InMemoryDataReader : IDataReader
    {
        private bool isOpen = false;

        private static double[] seedData;

        static InMemoryDataReader()
        {
            seedData = new double[4]
            {
                100d,
                100d,
                100d,
                100d
            };
        }

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
                int directionFirst = new Random().Next(0, seedData.Length);
                int directionSecond = new Random().Next(0, seedData.Length);
                for(int i = 0; i < seedData.Length; ++i)
                {
                    if(i == directionFirst)
                    {
                        seedData[i] += new Random().Next(-10, 11);
                    }
                    if (i == directionSecond)
                    {
                        seedData[i] += new Random().Next(-10, 11);
                    }
                }
            }

            throw new Exception("Należy najpierw otworzyć port, aby czytać dane!");
        }
    }
}
