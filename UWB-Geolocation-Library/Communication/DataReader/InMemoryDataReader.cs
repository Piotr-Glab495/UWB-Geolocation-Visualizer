﻿namespace UWB_Geolocation_Library.Communication.DataReader
{
    internal class InMemoryDataReader : IDataReader
    {
        private bool isOpen = false;
        private static bool isFirstIteration;

        private static readonly double[] seedData;

        static InMemoryDataReader()
        {
            seedData = new double[4]
            {
                100d,
                100d,
                100d,
                100d
            };
            isFirstIteration = true;
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
            if (isOpen)
            {
                if(isFirstIteration)
                {
                    isFirstIteration = false;
                    return seedData;
                }
                int directionFirst = new Random().Next(0, seedData.Length);
                int directionSecond = new Random().Next(0, seedData.Length);
                for (int i = 0; i < seedData.Length; ++i)
                {
                    if (i == directionFirst)
                    {
                        seedData[i] += new Random().Next(-10, 11);
                    }
                    if (i == directionSecond)
                    {
                        seedData[i] += new Random().Next(-10, 11);
                    }
                }
                return seedData;
            }
            else
            {
                throw new Exception("Należy najpierw otworzyć port, aby czytać dane!");
            }
        }
    }
}
