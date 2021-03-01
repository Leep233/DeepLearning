using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning
{
   public class ActivationFunctions
    {
        public static Matrix Softmax(Matrix matrix) {

            double c = matrix.Max;

            Matrix result = new Matrix(matrix.Row,matrix.Column);

            double sum = 0;

            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Column; j++)
                {
                   double num =  System.Math.Exp(matrix[i, j] - c);
                    sum += num;
                    result[i, j] = num;
                }
            }      

            return result/sum;
        }

        public static Matrix ReLU(Matrix matrix)
        {
            Matrix result = new Matrix(matrix.Row, matrix.Column);

            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Column; j++)
                {
                    result[i, j] = matrix[i, j] <= 0 ? 0 : matrix[i, j];
                }
            }
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
            Matrix result = new Matrix(matrix.Row,matrix.Column);

            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Column; j++)
                {
                    result[i,j] = 1 / (1 + System.Math.Exp(-matrix[i, j]));
                }
            }

            return result;        
        }

    }
}
