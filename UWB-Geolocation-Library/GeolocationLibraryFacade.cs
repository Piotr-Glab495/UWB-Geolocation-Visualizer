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
                dataReader = new USBDataReader("COM3", 9600); //TODO: think about those
            } 
            else
            {
                dataReader = new InMemoryDataReader();
            }
        }

        public PointD Locate(PointD[] anchorsLocations)
        {
            //TODO: maybe i need an adjustement here for the data due to the localizer border size ??
            dataReader.OpenPort();
            double[] distancesData = dataReader.ReadData() ?? new double[anchorsLocations.Length];
            dataReader.ClosePort();

            LocationCalculatorBuilder locationCalculatorBuilder = (LocationCalculatorBuilder)LocationCalculator
                .CreateBuilder()    //builder instance only from LocationCalculator static method, because it's impossible to get calculator without builder
                .SetInitialData(anchorsLocations, distancesData)
                .SetFilteringStrategy(FilterTypeEnum.None, 9);

            LocationCalculator locationCalculator = locationCalculatorBuilder.Build();
            return locationCalculator.CalculateLocation();
        }
    }
}
