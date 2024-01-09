using UWB_Geolocation_Library.Communication;
using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library
{
    public class GeolocationLibraryFacade
    {
        private readonly IDataReader dataReader;
        //TODO: add an builder/factory property returning LocationCalculator class and a property for the last one two, implement both with SMA filter in ./Calculation

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
            //TODO: implement
            return new PointD(0, 0);
        
            
        }
    }
}
