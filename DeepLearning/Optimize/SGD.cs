using DeepLearning.Interfaces;
using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Optimize
{
    public class SGD:IOptimize
    {
        double lr;
        public SGD(double learningrate = 0.1)
        {
            lr = learningrate;
        }
        public void Update(Matrix[] paramMap, Matrix[] grads)
        {
            // System.Console.WriteLine("Hello world");

            for (int i = 0; i < paramMap.Length; i++)
            {
                paramMap[i] -= lr * grads[i];
            }

        }
    }
}
