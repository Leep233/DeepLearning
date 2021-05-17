using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Activation
{
    public class Dropout : ILayer
    {
        /// <summary>
        /// 保存被删除的神经元
        /// </summary>
        public Matrix Mask { get;private set; }
      
        public double Ratio { get; private set; }
        public bool TrainFlag { get; private set; }

        private Random _random;
        public Dropout(double ratio=0.5,bool trainFlag = true)
        {
            Ratio = ratio;
            TrainFlag = trainFlag;
            _random = new Random();
        }
      
        public Matrix Backward(Matrix dout)
        {
            return dout * Mask;
        }

        public Matrix Forward(Matrix x, Matrix t = null)
        {
            Matrix result = null;

            if (TrainFlag)
            {
                Mask = Rand(x.Row, x.Column);
                result = x * Mask;
            }
            else {
                result = x * (1 - Ratio);
            }
            return result;
        }

        private Matrix Rand(int row, int column) {

            Matrix result = new Matrix(row,column);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    //0~1
                    result[i,j] =_random.NextDouble() > Ratio?1:0;
                }
            }
            return result;

        }
    
    }
}
