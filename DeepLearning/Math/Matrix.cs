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

        public Matrix T { get {

                Matrix matrix = new Matrix(Column,Row);

                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < Column; j++)
                    {
                        matrix[j, i] = this[i, j];
                    }
                }

                return matrix;
            
            } }

        internal static double Sum(Matrix matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Column; j++)
                {
                    sum += matrix[i, j];
                }
            }
            return sum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0 列最大值下标，1 行最大值下标</param>
        /// <returns></returns>
        public double[] Argmax(int mode = 0) 
        {         

            int x = mode == 0 ? Column:Row;

            int y = mode == 0 ? Row:Column;

            double[] indexs = new double[x];

            for (int i = 0; i < x; i++)
            {
                double temp = double.MinValue;

                int index= 0;

                for (int j=0; j < y; j++)
                {
                    double num = mode == 0 ? this[j, i]:this[i, j];
                    
                        if (temp < num)
                        {
                            index = j;
                            temp = num;
                        }                 
                }
                indexs[i] = index;
            }
            return indexs;
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

        public static Matrix Sqrt(Matrix x) {

            Matrix matrix = new Matrix(x.Row, x.Column);

            for (int i = 0; i < x.Row; i++)
            {
                for (int j = 0; j < x.Column; j++)
                {
                    matrix[i,j] = System.Math.Sqrt(x[i,j]);
                }
            }
            return matrix;
        
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

            Matrix result = null;

            if (matrix02.Row == 1 && matrix01.Column == matrix02.Column)
            {

                result = new Matrix(matrix01.Row, matrix01.Column);

                for (int i = 0; i < matrix01.Row; i++)
                {
                    for (int j = 0; j < matrix01.Column; j++)
                    {
                        result[i, j] = matrix01[i, j] - matrix02[0, j];
                    }
                }

            }
            else if (matrix01.Row == 1 && matrix01.Column == matrix02.Column)
            {
                result = new Matrix(matrix02.Row, matrix02.Column);

                for (int i = 0; i < matrix01.Row; i++)
                {
                    for (int j = 0; j < matrix01.Column; j++)
                    {
                        result[i, j] = matrix01[0, j] - matrix02[i, j];
                    }
                }
            }
            else if (matrix01.Row == matrix02.Row && matrix01.Column == matrix02.Column) {

                result = new Matrix(matrix01.Row, matrix01.Column);

                for (int i = 0; i < matrix01.Row; i++)
                {
                    for (int j = 0; j < matrix01.Column; j++)
                    {
                        result[i, j] = matrix01[i, j] - matrix02[i, j];
                    }
                }

            }
            else
            {
                throw new Exception("两个矩阵大小必须一致");
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

            Matrix result = null;

            if (matrix02.Row == 1 && matrix01.Column == matrix02.Column)
            {

                result = new Matrix(matrix01.Row, matrix01.Column);

                for (int i = 0; i < matrix01.Row; i++)
                {
                    for (int j = 0; j < matrix01.Column; j++)
                    {
                        result[i, j] = matrix01[i, j] + matrix02[0, j];
                    }
                }

            }
            else if (matrix01.Row == 1 && matrix01.Column == matrix02.Column)
            {
                result = new Matrix(matrix02.Row, matrix02.Column);

                for (int i = 0; i < matrix01.Row; i++)
                {
                    for (int j = 0; j < matrix01.Column; j++)
                    {
                        result[i, j] = matrix01[0, j] + matrix02[i, j];
                    }
                }
            }
            else if (matrix01.Row == matrix02.Row && matrix01.Column == matrix02.Column)
            {

                result = new Matrix(matrix01.Row, matrix01.Column);

                for (int i = 0; i < matrix01.Row; i++)
                {
                    for (int j = 0; j < matrix01.Column; j++)
                    {
                        result[i, j] = matrix01[i, j] + matrix02[i, j];
                    }
                }

            }
            else
            {
                throw new Exception("两个矩阵大小必须一致");
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
