using System;


namespace Expensier.WPF.Models
{
    public class LinearRegression
    {
        private double _slope;
        private double _intercept;


        public LinearRegression()
        {
            _slope = 0.0;
            _intercept = 0.0;
        }


        public void Fit(double[] x, double[] y)
        {
            if (x.Length != y.Length || x.Length < 2)
            {
                _slope = 0.0;
                _intercept = 0.0;

                return;
            }

            int arrayLength = x.Length;
            double sumX = 0.0;
            double sumY = 0.0;
            double sumXY = 0.0;
            double sumX2 = 0.0;

            for (int i = 0; i < arrayLength; i++)
            {
                sumX += x[i];
                sumY += y[i];
                sumXY += x[i] * y[i];
                sumX2 += Math.Pow(x[i], 2);
            }

            double meanX = sumX / arrayLength;
            double meanY = sumY / arrayLength;

            double numerator = (arrayLength * sumXY) - (sumX * sumY);
            double denominator = (arrayLength * sumX2) - Math.Pow(sumX, 2);

            if (denominator == 0)
            {
                _slope = 0.0;
                _intercept = 0.0;

                return;
            }

            _slope = numerator / denominator;
            _intercept = meanY - (_slope * meanX);
        }


        public double Predict(double x)
        {
            return _slope * x + _intercept;
        }


        public double MeanAbsoluteError(double[] trainX, double[] trainY)
        {
            if (trainX.Length != trainY.Length)
            {
                throw new ArgumentException("Test input and output arrays must have the same length!");
            }

            int arrayLength = trainX.Length;
            double absoluteErrorSum = 0.0;

            for (int i = 1; i < arrayLength; i++)
            {
                double predictedY = Predict(trainX[i]);
                double absoluteError = Math.Abs(predictedY - trainY[i]);
                absoluteErrorSum += absoluteError;
            }

            return absoluteErrorSum / arrayLength;
        }

        public double MeanAbsolutePercentageError(double[] trainX, double[] trainY)
        {
            if (trainX.Length != trainY.Length)
            {
                throw new ArgumentException("Test input and output arrays must have the same length!");
            }

            int arrayLength = trainX.Length;
            double absolutePercentageErrorSum = 0.0;

            for (int i = 1; i < arrayLength; i++)
            {
                double predictedY = Predict(trainX[i]);
                double absoluteError = Math.Abs(predictedY - trainY[i]);
                double absolutePercentageError = absoluteError / trainY[i];

                absolutePercentageErrorSum += absolutePercentageError;
            }

            return (absolutePercentageErrorSum / arrayLength) * 100.0;
        }
    }
}
