using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (x.Length != y.Length)
            {
                throw new ArgumentException("Input arrays must have the same length!");
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

            _slope = numerator / denominator;
            _intercept = meanY - (_slope * meanX);
        }

        public double Predict(double x)
        {
            return _slope * x + _intercept;
        }
    }
}
