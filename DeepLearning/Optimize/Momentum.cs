using DeepLearning.Interfaces;
using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Optimize
{
    public class Momentum : IOptimize
    {
        double lr;
        double momentum;
        Matrix[] v;
        public Momentum(double lr = 0.1, double momentum = 0.9)
        {
            this.lr = lr;
            this.momentum = momentum;
        }
        public void Update(Matrix[] paramMap, Matrix[] grads)
        {
            if (v is null)
            {
                v = new Matrix[paramMap.Length];
                for (int i = 0; i < v.Length; i++)
                {
                    Matrix matrix = paramMap[i];
                    v[i] = new Matrix(matrix.X, matrix.Y);
                }
            }

            for (int i = 0; i < paramMap.Length; i++)
            {
                v[i] = momentum * v[i] - lr * grads[i];
                paramMap[i] += v[i];
            }
        }
    }
}
