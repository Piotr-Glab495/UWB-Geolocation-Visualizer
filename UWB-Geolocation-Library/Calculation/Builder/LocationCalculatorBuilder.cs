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
                    calculator!.FilteringStrategy = null;
                    calculator!.IsUsingFilter = false;
                    return this;
                case FilterTypeEnum.SMA:
                    calculator!.FilteringStrategy = new SimpleMovingAverageFilter(filterWidth);
                    calculator!.IsUsingFilter = true;
                    return this;
            }
        }

        public ILocationCalculatorBuilder SetInitialData(PointD[] anchors, double[] realDistances)
        {
            double[] anchorsX = new double[anchors.Length];
            double[] anchorsY = new double[anchors.Length];

            for (int i = 0; i < anchors.Length; ++i)
            {
                anchorsX[i] = anchors[i].X;
                anchorsY[i] = anchors[i].Y;
            }

            calculator!.AnchorsX = anchorsX;
            calculator!.AnchorsY = anchorsY;
            calculator!.RealDistances = realDistances;

            return this;
        }

        public LocationCalculator Build()
        {
            return calculator!;
        }
    }
}
