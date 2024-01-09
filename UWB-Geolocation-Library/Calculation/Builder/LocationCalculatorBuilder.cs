namespace UWB_Geolocation_Library.Calculation.Builder
{
    internal class LocationCalculatorBuilder : ILocationCalculatorBuilder
    {
        private LocationCalculator calculator;

        //TODO: develop the whole builder
        public LocationCalculatorBuilder()
        {
            this.calculator = new LocationCalculator();
        }

        public LocationCalculator GetCalculator()
        {
            return calculator;
        }
    }
}
