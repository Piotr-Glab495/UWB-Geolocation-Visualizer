using UWB_Geolocation_Library.SimpleTypes;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace UWB_Geolocation_Library.Calculation
{
    internal class LocationCalculator
    {
        private double[]? anchorsX;
        private double[]? anchorsY;
        private double[]? RealDistances;

        /**
         * <summary>
         * This method finds the most suitable point to judge as a real location.
         * It minimizes the difference between real and estimated distances from each anchor to an estimated point got from Nelder-Mead method using least squares method implementation.
         * Simpler: it's an estimation point distances vs real distances => the difference between those least => got a real location. 
         * </summary> 
         */
        public PointD CalculateLocation(PointD[] anchors, double[] distances)
        {
            if(anchors.Length != distances.Length)
            {
                throw new Exception("Różna ilość kotwic zadeklarowanych i rzeczywistych");
            }
            anchorsX ??= new double[anchors.Length];
            anchorsY ??= new double[anchors.Length];
            RealDistances = distances;
            
            for (int i = 0; i < anchors.Length; ++i)
            {
                anchorsX[i] = anchors[i].X;
                anchorsY[i] = anchors[i].Y;
            }

            // base localised point coordinates initialization
            var initialGuess = new DenseVector(new[] { 0.0, 0.0 });

            // NelderMeadSimplex implementation using least squares method to minimize
            IObjectiveFunction objFunc = ObjectiveFunction.Value(SumOfSquaresOfErrors);
            MinimizationResult minResult = NelderMeadSimplex.Minimum(objectiveFunction: objFunc, initialGuess: initialGuess, convergenceTolerance: 1e-5, maximumIterations: 10000);
            double[] finalCoordinates = minResult.MinimizingPoint.ToArray();

            return new PointD(finalCoordinates[0], finalCoordinates[1]);
        }

        // Method used as an objective function for Nealder-Mead method algorithm -  calculates the squared difference between an estimated point distances to anchors and real distances.
        // So it actually calculates an iteration step square field to be minimized by Nelder-Mead.
        private double SumOfSquaresOfErrors(Vector<double> vector)
        {
            double err = 0;
            for (int i = 0; i < anchorsX!.Length; i++)
            {
                double distanceEstimate = CalculateDistanceEstimate(vector[0], vector[1], anchorsX[i], anchorsY![i]);
                double distanceError = RealDistances![i] - distanceEstimate;
                err += Math.Pow(distanceError, 2);
            }
            return err;
        }

        // Method calculating the estimated distance from localised point to an appropriate subdevice
        private static double CalculateDistanceEstimate(double coordinateX, double coordinateY, double anchorX, double anchorY)
        {
            double deltaX = anchorX - coordinateX;
            double deltaY = anchorY - coordinateY;
            return Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
        }
        
    }
}
