using DeepLearning.Interfaces;
using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Optimize
{
    public class AdaGrad: IOptimize
    {
        double lr;
        Matrix[] h;
        public AdaGrad(double learningrate = 0.1)
        {
            lr = learningrate;
        }
        public void Update(Matrix[] paramMap, Matrix[] grads)
        {

            if (h is null)
            {
                h = new Matrix[paramMap.Length];
                for (int i = 0; i < h.Length; i++)
                {
                    Matrix matrix = paramMap[i];
                    h[i] = new Matrix(matrix.X, matrix.Y);
                }
            }

            for (int i = 0; i < paramMap.Length; i++)
            {
                h[i] += grads[i] * grads[i];
                paramMap[i] -= lr * grads[i] / Matrix.Sqrt(h[i]);
            }
        }
    }
}
