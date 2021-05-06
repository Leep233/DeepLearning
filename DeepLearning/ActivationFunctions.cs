using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearning
{
   public class ActivationFunctions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Matrix Softmax(Matrix matrix)
        {

            double c = matrix.Max;

            int row = matrix.Row;
            int col = matrix.Column;

            Matrix result = new Matrix(row, col);

            double[] sums = new double[row];

            Parallel.For(0, row,i=> {
                for (int j = 0; j < col; j++)
                {
                    double num = System.Math.Exp(matrix[i, j] - c);
                    sums[i] += num;
                    result[i, j] = num;
                }
            });

            Parallel.For(0, row, i => {
                for (int j = 0; j <col; j++)
                {
                    result[i, j] /= sums[i];
                }
            });

            return result ;
        }
       
        public static Matrix ReLU(Matrix matrix)
        {
            int c = matrix.Column;
            int r = matrix.Row;

            Matrix result = new Matrix(r,c );

            Parallel.For(0, r, i => {
                for (int j = 0; j < c; j++)
                {
                    result[i, j] = matrix[i, j] <= 0 ? 0 : matrix[i, j];
                }
            });
            return result;
        }
        public static Matrix Step(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);

            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Column; j++)
                {
                    result[i, j] = matrix[i,j]<=0?0:1;
                }
            }
            return result;
        }
       
        public static Matrix Sigmoid(Matrix matrix) {

            int c = matrix.Column;
            int r = matrix.Row;

            Matrix result = new Matrix(r, c);

            Parallel.For(0, r, i => {
                for (int j = 0; j <c; j++)
                {
                    result[i, j] = 1 / (1 + System.Math.Exp(-matrix[i, j]));
                }
            });
            return result;
        }

    }
}
