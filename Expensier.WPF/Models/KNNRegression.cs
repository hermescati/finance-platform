using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensier.WPF.Models
{
    public class KNNRegression
    {
        private int _k;
        private double[][] _trainingData;
        private double[] _trainingLabels;
        private double[] _distances;

        public KNNRegression(int k)
        {
            _k = k;
        }

        public void Fit(double[][] X, double[] y)
        {
            _trainingData = X;
            _trainingLabels = y;
        }

        public double Predict(double[] x)
        {
            _distances = new double[_trainingData.Length];
            for (int i = 0; i < _trainingData.Length; i++)
            {
                _distances[i] = EuclideanDistance(x, _trainingData[i]);
            }

            int[] nearestNeighbors = GetNeareastNeighbors(_distances, _k);

            double sum = 0;
            for (int i = 0; i < nearestNeighbors.Length; i++)
            {
                sum += _trainingLabels[nearestNeighbors[i]];
            }

            return sum / nearestNeighbors.Length;
        }

        private int[] GetNeareastNeighbors(double[] distances, int k)
        {
            int[] neighbors = Enumerable.Range(0, distances.Length).ToArray();
            Array.Sort(distances, neighbors);

            int[] nearestNeighbors = new int[k];
            Array.Copy(neighbors, nearestNeighbors, k);

            return nearestNeighbors;
        }

        private double EuclideanDistance(double[] x1, double[] x2)
        {
            double sum = 0;
            for (int i = 0; i < x1.Length; i++)
            {
                sum += Math.Pow(x1[i] - x2[i], 2);
            }

            return Math.Sqrt(sum);
        }
    }
}
