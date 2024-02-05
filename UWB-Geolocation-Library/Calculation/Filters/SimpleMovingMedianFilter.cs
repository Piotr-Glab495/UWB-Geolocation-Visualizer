using UWB_Geolocation_Library.SimpleTypes;

namespace UWB_Geolocation_Library.Calculation.Filters
{
    internal class SimpleMovingMedianFilter : IFilterStrategy
    {
        private static readonly List<PointD> locationsBuffer;
        private int windowSize = 0;

        static SimpleMovingMedianFilter()
        {
            locationsBuffer = new List<PointD>();
        }

        public SimpleMovingMedianFilter(int windowSize)
        {
            this.windowSize = windowSize;
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
                var sortedX = locationsBuffer.Skip(locationsBuffer.Count - windowSize).Select(p => p.X).OrderBy(x => x).ToList();
                var sortedY = locationsBuffer.Skip(locationsBuffer.Count - windowSize).Select(p => p.Y).OrderBy(y => y).ToList();

                double medianX;
                double medianY;

                if (windowSize % 2 == 0)
                {
                    int middleIndex = windowSize / 2;
                    medianX = (sortedX[middleIndex - 1] + sortedX[middleIndex]) / 2.0;
                    medianY = (sortedY[middleIndex - 1] + sortedY[middleIndex]) / 2.0;
                }
                else
                {
                    medianX = sortedX[windowSize / 2];
                    medianY = sortedY[windowSize / 2];
                }

                locationsBuffer.RemoveAt(0);

                return new PointD(medianX, medianY);
            }
            else
            {
                return locationsBuffer.LastOrDefault()!;
            }
        }
    }
}
