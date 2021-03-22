using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning
{
    public interface IOptimize
    {
       void Update( Matrix[] paramMap, Matrix[] grads);
    }
    public class Momentum : IOptimize
    {
        double lr;
        double momentum;
        Matrix[] v;
        public Momentum(double lr = 0.1,double momentum = 0.9)
        {
            this.lr = lr;
            this.momentum = momentum;
        }
        public void Update(Matrix[] paramMap, Matrix[] grads)
        {
            if (v is null) {
                v = new Matrix[paramMap.Length];
                for (int i = 0; i < v.Length; i++)
                {
                    Matrix matrix = paramMap[i];
                    v[i] = new Matrix(matrix.Row, matrix.Column);
                }
            }

            for (int i = 0; i < paramMap.Length; i++)
            {
                v[i] = momentum * v[i] - lr * grads[i];
                paramMap[i] += v[i];
            }
        }
    }

    public class AdaGrad : IOptimize
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
                    h[i] = new Matrix(matrix.Row, matrix.Column);
                }
            }

            for (int i = 0; i < paramMap.Length; i++)
            {
                h[i] += grads[i] * grads[i];
                paramMap[i] -= lr * grads[i] / Matrix.Sqrt(h[i]);
            }
        }
    }

    public class SGD : IOptimize
    {
        double lr;
        public SGD(double learningrate = 0.1)
        {
            lr = learningrate;
        }
        public void Update( Matrix[] paramMap, Matrix[] grads)
        {
           // System.Console.WriteLine("Hello world");

            for (int i = 0; i < paramMap.Length; i++)
            {
                paramMap[i] -= lr * grads[i];
            }
      

        }
    }

}
