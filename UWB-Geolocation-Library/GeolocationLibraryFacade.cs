﻿using UWB_Geolocation_Library.Calculation;
using UWB_Geolocation_Library.Calculation.Builder;
using UWB_Geolocation_Library.Communication.DataLogger;
using UWB_Geolocation_Library.Communication.DataReader;
using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library
{
    public class GeolocationLibraryFacade
    {
        private readonly IDataReader dataReader;
        private readonly ILogger? logger;

        public GeolocationLibraryFacade(DataReadingModeEnum mode = DataReadingModeEnum.TestMode, LogModeEnum logMode = LogModeEnum.Input | LogModeEnum.Output)
        {
            if(mode == DataReadingModeEnum.USBMode)
            {
                dataReader = new USBDataReader("COM3", 9600); //TODO: think about those
            } 
            else
            {
                dataReader = new InMemoryDataReader();
            }
            logger ??= new StreamWriterLogger(logMode);
        }

        /**
         * <summary>
         *  This is a library facade main method responsible for servicing one iteration of the used device including the following steps:
         *  * reading one portion of data about ,
         *  * logging input data eventually,
         *  * creating a LocationCalculator instance with the appropriate builder (passing the data from a client's code e.g. about the used filter),
         *  * calculating the location basing on the data about anchors from the client's code and the read data about distances,
         *  * logging output data eventually,
         *  * returning the result.
         * </summary>
         */
        public PointD Locate(PointD[] anchorsLocations)
        {
            try
            {
                dataReader.OpenPort();
                double[] distancesData = dataReader.ReadData() ?? new double[anchorsLocations.Length];
                dataReader.ClosePort();

                LogInDataIfNeeded(distancesData);

                LocationCalculatorBuilder locationCalculatorBuilder = (LocationCalculatorBuilder)LocationCalculator
                    .CreateBuilder()    //builder instance only from LocationCalculator static method, because it's impossible to get calculator without builder
                    .SetInitialData(anchorsLocations, distancesData)
                    .SetFilteringStrategy(FilterTypeEnum.None, 9);

                LocationCalculator locationCalculator = locationCalculatorBuilder.Build();
                PointD calculatedLocation = locationCalculator.CalculateLocation();
                
                LogOutDataIfNeeded(calculatedLocation);
                logger?.Dispose();
                return calculatedLocation;
            } catch (Exception)
            {
                dataReader?.ClosePort();
                logger?.Dispose();
                throw;  //providing a message about error for the view after servicing the library work interruption
            }
        }

        /**
         * <summary>
         *  Method used to log input data in a correct format. Uses a logger which is fully safe due to initialization with LogMode - thus ending "IfNeeded".
         * </summary>
         */
        private void LogInDataIfNeeded(double[] distancesData)
        {
            string line = "";
            foreach (double distance in distancesData)
            {
                line += distance + " ";
            }
            logger!.LogInData(line);
        }

        /**
         * <summary>
         *  Method used to log output data in a correct format. Uses a logger which is fully safe due to initialization with LogMode - thus ending "IfNeeded".
         * </summary>
         */
        private void LogOutDataIfNeeded(PointD calculatedLocation)
        {
            logger!.LogOutData(
                "X: " + calculatedLocation.X  + " Y: " + calculatedLocation.Y
            );
        }
    }
}
