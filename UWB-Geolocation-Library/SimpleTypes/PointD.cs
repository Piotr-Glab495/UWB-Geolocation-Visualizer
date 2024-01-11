namespace UWB_Geolocation_Library.SimpleTypes
{
    public class PointD : IEquatable<PointD> 
    {
        private double x, y;

        public PointD(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public PointD(double v)
        {
            x = v;
            y = v;
        }

        public PointD(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public PointD(int v)
        {
            x = v;
            y = v;
        }

        public PointD()
        {
            x = 0.0d;
            y = 0.0d;
        }

        public double X { get => x; set => x = value; }

        public double Y { get => y; set => y = value; }

        public static bool operator ==(PointD left, PointD right) => left.X == right.X && left.Y == right.Y;
        public static bool operator !=(PointD left, PointD right) => !(left == right);

        public override bool Equals(object? obj)
        {
            if(obj != null) {
                return Equals(obj as PointD);
            }
            return false;
        }

        public bool Equals(PointD? other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public bool IsEmpty()
        {
            return x == 0 && y == 0;
        }
    }
}
