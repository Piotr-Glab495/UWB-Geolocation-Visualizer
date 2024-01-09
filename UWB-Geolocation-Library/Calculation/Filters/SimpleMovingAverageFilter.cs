using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Calculation.Filters
{
    internal class SimpleMovingAverageFilter : IFilterStrategy
    {
        private readonly List<PointD> locationsBuffer;
        private readonly int windowSize;

        public SimpleMovingAverageFilter(int windowSize)
        {
            this.windowSize = windowSize;
            locationsBuffer = new List<PointD>();
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
