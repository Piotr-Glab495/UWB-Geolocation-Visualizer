using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Calculation.Filters
{
    internal class SimpleMovingAverageFilter : IFilterStrategy
    {
        private readonly List<PointD> locationsBuffer;
        private int windowSize = 0;

        public SimpleMovingAverageFilter(int windowSize)
        {
            this.windowSize = windowSize;
            locationsBuffer = new List<PointD>();
        }

        public void SetWindowSize(int windowSize)
        {
            this.windowSize = windowSize;
        }

        public PointD FilterLocation(PointD location)
        {
            locationsBuffer.Add(location);

            if (locationsBuffer.Count >= windowSize)
            {
                double averageX = locationsBuffer.Skip(locationsBuffer.Count - windowSize).Average(p => p.X);
                double averageY = locationsBuffer.Skip(locationsBuffer.Count - windowSize).Average(p => p.Y);

                locationsBuffer.RemoveAt(0);

                return new PointD(averageX, averageY);
            }
            else
            {
                return locationsBuffer.LastOrDefault()!;
            }
        }
    }
}
