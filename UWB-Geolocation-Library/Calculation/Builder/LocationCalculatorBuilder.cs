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

        //TODO: develop the whole builder
        public LocationCalculatorBuilder(LocationCalculator calculator)
        {
            this.calculator = calculator;
        }

        public LocationCalculator Build()
        {
            return calculator!;
        }
    }
}
