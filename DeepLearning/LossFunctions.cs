using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning
{
   public class LossFunctions
    {

        private const double DELTA = 1E-7;
        public  static double  MeanSquaredError(Matrix y,Matrix t) {

            Matrix matrix = y - t;

            double sum = 0;

            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Column; j++)
                {
                    sum += System.Math.Pow(matrix[i, j], 2);
                }
            }

            return sum * 0.5;

        }



        public static double CrossEntropyError(Matrix y, Matrix t) {


            if (y.Row == t.Row || y.Column == t.Column) {
                //one-hot形式 0 1 0 0 0 0 0
                Matrix indexs = new Matrix(t.Row, 1);
                for (int i = 0; i < t.Row; i++)
                {
                    for (int j = 0; j < t.Column; j++)
                    {
                        if (t[i, j] == 1) {
                            indexs[i,0] = j;
                            break;
                        }
                    }
                }
                 t = indexs;
            }

            double loss = 0;

            for (int i = 0; i < y.Row; i++)
            {

                for (int j = 0; j < t.Column; j++)
                {
                    double temp = (int)t[i, j];

                     temp = y[i, (int)t[i, j]];

                    temp = System.Math.Log(temp + DELTA);

                    loss += temp;
                }
            }

            return -loss/t.Row;

        }


    }
}
