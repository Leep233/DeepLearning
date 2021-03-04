using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Math
{
   public class Matrix
    {
        double[,] _content;

        public int Row { get => _content?.GetLength(0) ?? 0; }
        public int Column { get => _content?.GetLength(1) ?? 0; }

        public double Max { get {

                double result = this[0,0];

                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < Column; j++)
                    {
                        if (result < this[i, j]) result = this[i, j];
                    }
                }
                return result;

            }}

        public double this[int r, int c] { 
            get => _content[r, c];
            set => _content[r, c] = value;
        }

        public Matrix(double[,] content)
        {
            _content = content;
        }
        public Matrix(int row,int col)
        {
            _content = new double[row, col];
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    result += $"{this[i,j].ToString("F8")} ";
                }
                result += "\n";
            }

            return result;
        }

        public static Matrix Dot(Matrix matrix01, Matrix matrix02) {

            if (matrix01.Column != matrix02.Row) throw new Exception("矩阵1列与矩阵2的行必须一致");
            Matrix result = new Matrix(matrix01.Row,matrix02.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix02.Column; j++)
                {
                    double temp = 0;

                    for (int k = 0; k < matrix01.Column; k++)
                    {
                        temp += matrix01[i, k] * matrix02[k, j];
                    }

                    result[i, j] = temp;
                }
            }
            return result;
        
        }

        public static bool operator ==(Matrix matrix01, Matrix matrix02) {

            if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) return false;

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    if (!matrix01[i, j].Equals(matrix02[i, j])) return false;
                }
            }
            return true;

        }

        public static bool operator !=(Matrix matrix01, Matrix matrix02) {
            return !(matrix01 == matrix02);
        }
        public static Matrix operator -(Matrix matrix01, Matrix matrix02) {

            if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row,matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] - matrix02[i, j];
                }
            }
            return result;
        }
        public static Matrix operator -(double value,Matrix matrix01)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = value- matrix01[i, j] ;//matrix02[i, j];
                }
            }
            return result;
        }
        public static Matrix operator -(Matrix matrix01, double value)
        {

           // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] - value;//matrix02[i, j];
                }
            }
            return result;
        }

        public static Matrix operator +(Matrix matrix01, Matrix matrix02)
        {

            if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] + matrix02[i, j];
                }
            }
            return result;
        }
        public static Matrix operator +(double value, Matrix matrix01)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = value + matrix01[i, j];//matrix02[i, j];
                }
            }
            return result;
        }
        public static Matrix operator +(Matrix matrix01, double value)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] + value;//matrix02[i, j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix matrix01, Matrix matrix02)
        {

            if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] * matrix02[i, j];
                }
            }
            return result;
        }
        public static Matrix operator *(double value, Matrix matrix01)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = value * matrix01[i, j];//matrix02[i, j];
                }
            }
            return result;
        }
        public static Matrix operator *(Matrix matrix01, double value)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] * value;//matrix02[i, j];
                }
            }
            return result;
        }

        public static Matrix operator /(Matrix matrix01, Matrix matrix02)
        {

            if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] / matrix02[i, j];
                }
            }
            return result;
        }
        public static Matrix operator /(double value, Matrix matrix01)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = value / matrix01[i, j];//matrix02[i, j];
                }
            }
            return result;
        }
        public static Matrix operator /(Matrix matrix01, double value)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] / value;//matrix02[i, j];
                }
            }
            return result;
        }

    }
}
