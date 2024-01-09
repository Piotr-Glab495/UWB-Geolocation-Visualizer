using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Calculation.Builder
{
    internal interface ILocationCalculatorBuilder
    {
        public ILocationCalculatorBuilder SetFilteringStrategy(FilterTypeEnum filterType, int filterWidth);
        public ILocationCalculatorBuilder SetInitialData(double[]? anchorsX, double[]? anchorsY);
    }
}
