using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning
{
   public class SimpleNet
    {
        public Matrix x;
        public Matrix w;
        public Matrix t;
        public SimpleNet(Matrix x,Matrix t)
        {
            this.x = x;
            this.t = t;
            w = new Matrix(new double[,] {
            { 0.47355232 , 0.9977393  , 0.84668094 },
            { 0.85557411 , 0.03563661 , 0.69422093 }
            });
        }  

        public Matrix Predict() {
            //x * w  线性组合
            return Matrix.Dot(x, w);
        }

     

        public double Loss(Matrix w) {
            //1.线性组合
            Matrix z = Predict();
            //2.非线性激活
            Matrix y = ActivationFunctions.Softmax(z);
            //3.损失值
           return LossFunctions.CrossEntropyError(y,t);
        }

    }
}
