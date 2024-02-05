namespace UWB_Geolocation_Library.SimpleTypes
{
    public class GeolocationLibraryDTO
    {
        public GeolocationLibraryDTO(
            PointD[] anchorsLocations,
            DataReadingModeEnum dataReadingMode,
            int portComNumber,
            LogModeEnum logMode,
            FilterTypeEnum filterType,
            int filterWindowSize
        )
        {
            this.anchorsLocations = anchorsLocations;
            this.dataReadingMode = dataReadingMode;
            this.portComNumber = portComNumber;
            this.logMode = logMode;
            this.filterType = filterType;
            this.filterWindowSize = filterWindowSize;
        }

        private readonly PointD[] anchorsLocations;
        private readonly DataReadingModeEnum dataReadingMode;
        private readonly int portComNumber;
        private readonly LogModeEnum logMode;
        private readonly FilterTypeEnum filterType;
        private readonly int filterWindowSize;

        public PointD[] GetAnchorsLocations()
        {
            return anchorsLocations;
        }

        public DataReadingModeEnum GetDataReadingMode()
        {
            return dataReadingMode;
        }

        public int GetPortComNumber()
        {
            return portComNumber;
        }

        public LogModeEnum GetLogMode()
        {
            return logMode;
        }

        public FilterTypeEnum GetFilterType()
        {
            return filterType;
        }

        public int GetFilterWindowSize()
        {
            return filterWindowSize;
        }
    }
}
