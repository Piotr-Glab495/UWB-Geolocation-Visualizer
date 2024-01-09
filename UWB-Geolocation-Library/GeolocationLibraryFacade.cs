using UWB_Geolocation_Library.Calculation;
using UWB_Geolocation_Library.Calculation.Builder;
using UWB_Geolocation_Library.Communication;
using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library
{
    public class GeolocationLibraryFacade
    {
        private readonly IDataReader dataReader;

        public GeolocationLibraryFacade(DataReadingModeEnum mode = DataReadingModeEnum.TestMode)
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
            //TODO: get data with dataReader and adjust them for the localizer border size 
            //dataReader.OpenPort();
            //double[] distancesData = await dataReader.ReadDataAsync();
            //dataReader.ClosePort();
            double[] distancesData = new double[]
            {
                100d,
                100d,
                100d,
                100d
            };

            LocationCalculatorBuilder locationCalculatorBuilder = (LocationCalculatorBuilder)LocationCalculator
                .CreateBuilder()    //builder instance only from LocationCalculator static method, because it's impossible to get calculator without builder
                .SetInitialData(anchorsLocations, distancesData)
                .SetFilteringStrategy(FilterTypeEnum.None, 9);

            LocationCalculator locationCalculator = locationCalculatorBuilder.Build();
            return locationCalculator.CalculateLocation();
        }
    }
}
