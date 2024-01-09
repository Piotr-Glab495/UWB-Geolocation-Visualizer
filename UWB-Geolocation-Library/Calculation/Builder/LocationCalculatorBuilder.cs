using UWB_Geolocation_Library.Calculation.Filters;
using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Calculation.Builder
{
    internal class LocationCalculatorBuilder : ILocationCalculatorBuilder
    {
        private readonly LocationCalculator? calculator;

        /**
         * <summary>
         * LocationCalculatorBuilder is private, because it needs LocationCalculator private constructor usage to start, so use a LocationCalculator.CreateBuilder() static method.
         * </summary>
         */
        private LocationCalculatorBuilder() { }

        public LocationCalculatorBuilder(LocationCalculator calculator)
        {
            this.calculator = calculator;
        }

        public ILocationCalculatorBuilder SetFilteringStrategy(FilterTypeEnum filterType, int filterWidth)
        {
            switch (filterType)
            {
                case FilterTypeEnum.None:
                default:
                    calculator!.SetFilteringStrategy(null);
                    calculator!.IsUsingFilter = false;
                    return this;
                case FilterTypeEnum.SMA:
                    calculator!.SetFilteringStrategy(new SimpleMovingAverageFilter(filterWidth));
                    calculator!.IsUsingFilter = true;
                    return this;
            }
        }

        public ILocationCalculatorBuilder SetInitialData(double[]? anchorsX, double[]? anchorsY)
        {
            calculator!.AnchorsX = anchorsX;
            calculator!.AnchorsY = anchorsY;
            return this;
        }

        //TODO: maybe develop for SetIsMultiThread, SetThreadNumber, setTestMode if it wouldn't be async for testing

        public LocationCalculator Build()
        {
            return calculator!;
        }
    }
}
