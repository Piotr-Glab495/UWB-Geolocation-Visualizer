using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Calculation.Filters
{
    internal interface IFilterStrategy
    {
        PointD FilterLocation(PointD location);
        void SetWindowSize(int windowSize);
    }
}
