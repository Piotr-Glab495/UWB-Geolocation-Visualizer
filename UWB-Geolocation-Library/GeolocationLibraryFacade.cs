﻿using UWB_Geolocation_Library.Calculation;
using UWB_Geolocation_Library.Communication;
using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library
{
    public class GeolocationLibraryFacade
    {
        private readonly IDataReader dataReader;

        public GeolocationLibraryFacade(DataReadingModeEnum mode)
        {
            if(mode == DataReadingModeEnum.USBMode)
            {
                dataReader = new USBDataReader();
            } 
            else
            {
                dataReader = new InMemoryDataReader();
            }
        }

        public PointD Locate(PointD[] anchorsLocations)
        {
            //TODO: get data with dataReader and add some loop
            dataReader.OpenPort();
            //double[] distancesData = await dataReader.ReadDataAsync();
            double[] distancesData = new double[]
            {
                3d,
                2.5d,
                1.5d,
                0.5d
            };
            LocationCalculator locationCalculator = LocationCalculator.CreateBuilder().Build();
            return locationCalculator.CalculateLocation(anchorsLocations, distancesData);
        }
    }
}
