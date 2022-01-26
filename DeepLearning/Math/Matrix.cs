using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeepLearning.Math
{

    public class Matrix
    {
        double[,] _content;

        public int X { get => _content?.GetLength(0) ?? 0; }

        public int Y { get => _content?.GetLength(1) ?? 0; }

        public double Max
        {
            get
            {

                double result = this[0, 0];

                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        if (result < this[i, j]) result = this[i, j];
                    }
                }
                return result;

            }
        }

        public double this[int r, int c]
        {
            get => _content[r, c];
            set => _content[r, c] = value;
        }

        public double[] Column(int row) {

            double[] result = new double[Y];

            for (int i = 0; i < Y; i++)
            {
                result[i] = this[row, i];
            }
            return result;
        }

        public double[] Row(int col) {

            double[] result = new double[X];

            for (int i = 0; i < X; i++)
            {
                result[i] = this[i, col];
            }
            return result;
        }
        public Matrix(double[,] content)
        {
            _content = content;
        }
        public Matrix(int row, int col)
        {
            _content = new double[row, col];
        }

        public static double[] Image2Column(Matrix content, int coreWidth, int coreHeight, int stride = 1, int padding = 0)
        {

            int _2Padding = 2 * padding;
            //填充后宽
            int paddingWidth = content.X + _2Padding;
            //填充后高
            int paddingHeight = content.Y + _2Padding;

            int hw = (paddingWidth - coreWidth) / stride + 1;

            int hh = (paddingHeight - coreHeight) / stride + 1;

            ///卷积核大小
            int coreSize = coreWidth * coreHeight;

            int size = hw * hh * coreSize;

            double [] column = new double[size];

            int width = -1;
            int height = -1;

            for (int i = 0; i < hw; i++)
            {
                for (int j = 0; j < hh; j++)
                {
                    for (int w = 0; w < coreWidth; w++)
                    {
                        for (int h = 0; h < coreHeight; h++)
                        {
                            width = i + w - padding;
                            height = j+ h - padding;
                            int xindex = (i * coreWidth) + w - padding;
                            int yindex = (j * coreHeight) + h - padding;
                            //(coreSize *(j+i))+
                            int index = (coreSize * (j + i)) + width * coreWidth + height;

                            Console.WriteLine(index);

                            if (xindex < 0 || yindex < 0 || xindex >= content.X || yindex >= content.Y) continue;

                            column[index] = content[xindex, yindex];
                        }
                    }
                }
            }

            return column;
        }

        public Matrix T
        {
            get
            {

                Matrix matrix = new Matrix(Y, X);

                for (int i = 0; i < X; i++)
                {
                    for (int j = 0; j < Y; j++)
                    {
                        matrix[j, i] = this[i, j];
                    }
                }

                return matrix;

            }
        }

        public static double Sum(Matrix matrix)
        {
            double result = 0;

            Mutex mutex = new Mutex();

            int rowSize = matrix.X;

            int colSize = matrix.Y;

            Parallel.For(0, rowSize, i =>
            {

                for (int j = 0; j < colSize; j++)
                {

                    mutex.WaitOne();

                    result += matrix[i, j];

                    mutex.ReleaseMutex();
                }

            });

            mutex.Dispose();
            return result;// BitConverter.ToDouble(BitConverter.GetBytes(result),0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0 列最大值下标，1 行最大值下标</param>
        /// <returns></returns>
        public double[] Argmax(int mode = 0)
        {

            int x = mode == 0 ? Y : X;

            int y = mode == 0 ? X : Y;

            double[] indexs = new double[x];

            for (int i = 0; i < x; i++)
            {
                double temp = double.MinValue;

                int index = 0;

                for (int j = 0; j < y; j++)
                {
                    double num = mode == 0 ? this[j, i] : this[i, j];

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

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    result += $"{this[i, j].ToString("F8")} ";
                }
                result += "\n";
            }

            return result;
        }

        public static Matrix Sqrt(Matrix x)
        {

            Matrix matrix = new Matrix(x.X, x.Y);

            int rowSize = matrix.X;

            int colSize = matrix.Y;

            Parallel.For(0, rowSize, i =>
            {

                for (int j = 0; j < colSize; j++)
                {
                    matrix[i, j] = System.Math.Sqrt(x[i, j]);

                }
            });

            return matrix;
        }

        public static Matrix Dot(Matrix matrix01, Matrix matrix02)
        {

            if (matrix01.Y != matrix02.X) throw new Exception("矩阵1列与矩阵2的行必须一致");
            Matrix result = new Matrix(matrix01.X, matrix02.Y);

            int row01 = matrix01.X;
            int col01 = matrix01.Y;
            int col02 = matrix02.Y;

            Parallel.For(0, row01, i =>
            {

                for (int j = 0; j < col02; j++)
                {
                    double temp = 0;

                    for (int k = 0; k < col01; k++)
                    {
                        temp += matrix01[i, k] * matrix02[k, j];
                    }

                    result[i, j] = temp;
                }

            });
            return result;

        }

        public static bool operator ==(Matrix matrix01, Matrix matrix02)
        {

            if (matrix01.X != matrix02.X || matrix01.Y != matrix02.Y) return false;

            for (int i = 0; i < matrix01.X; i++)
            {
                for (int j = 0; j < matrix01.Y; j++)
                {
                    if (!matrix01[i, j].Equals(matrix02[i, j])) return false;
                }
            }
            return true;

        }

        public static bool operator !=(Matrix matrix01, Matrix matrix02)
        {
            return !(matrix01 == matrix02);
        }
        public static Matrix operator -(Matrix matrix01, Matrix matrix02)
        {

            Matrix result = null;

            int r = matrix01.X;
            int c = matrix01.Y;

            if (matrix02.X == 1 && matrix01.Y == matrix02.Y)
            {

                result = new Matrix(matrix01.X, matrix01.Y);

                Parallel.For(0, r, i =>
                {
                    for (int j = 0; j < matrix01.Y; j++)
                    {
                        result[i, j] = matrix01[i, j] - matrix02[0, j];
                    }

                });

            }
            else if (matrix01.X == 1 && matrix01.Y == matrix02.Y)
            {
                result = new Matrix(matrix02.X, matrix02.Y);

                Parallel.For(0, r, i =>
                {
                    for (int j = 0; j < matrix01.Y; j++)
                    {
                        result[i, j] = matrix01[0, j] - matrix02[i, j];
                    }

                });
            }
            else if (matrix01.X == matrix02.X && matrix01.Y == matrix02.Y)
            {

                result = new Matrix(matrix01.X, matrix01.Y);

                Parallel.For(0, r, i =>
                {
                    for (int j = 0; j < matrix01.Y; j++)
                    {
                        result[i, j] = matrix01[i, j] - matrix02[i, j];
                    }

                });
            }
            else
            {
                throw new Exception("两个矩阵大小必须一致");
            }


            return result;
        }
        public static Matrix operator -(double value, Matrix matrix01)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.X, matrix01.Y);

            int r = matrix01.X;
            int c = matrix01.Y;

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = value - matrix01[i, j];//matrix02[i, j];
                }
            });
            /*
            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = value- matrix01[i, j] ;//matrix02[i, j];
                }
            }*/
            return result;
        }
        public static Matrix operator -(Matrix matrix01, double value)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(matrix01.X, matrix01.Y);

            int r = matrix01.X;
            int c = matrix01.Y;

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = matrix01[i, j] - value;//matrix02[i, j];
                }
            });
            /*
            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] - value;//matrix02[i, j];
                }
            }*/
            return result;
        }

        public static Matrix operator +(Matrix matrix01, Matrix matrix02)
        {

            Matrix result = null;

            int r = matrix01.X;
            int c = matrix01.Y;


            if (matrix02.X == 1 && matrix01.Y == matrix02.Y)
            {

                result = new Matrix(matrix01.X, matrix01.Y);

                Parallel.For(0, r, i =>
                {

                    for (int j = 0; j < matrix01.Y; j++)
                    {
                        result[i, j] = matrix01[i, j] + matrix02[0, j];
                    }
                });

            }
            else if (matrix01.X == 1 && matrix01.Y == matrix02.Y)
            {
                result = new Matrix(matrix02.X, matrix02.Y);

                Parallel.For(0, r, i =>
                {

                    for (int j = 0; j < matrix01.Y; j++)
                    {
                        result[i, j] = matrix01[0, j] + matrix02[i, j];
                    }
                });
            }
            else if (matrix01.X == matrix02.X && matrix01.Y == matrix02.Y)
            {

                result = new Matrix(matrix01.X, matrix01.Y);

                Parallel.For(0, r, i =>
                {

                    for (int j = 0; j < matrix01.Y; j++)
                    {
                        result[i, j] = matrix01[i, j] + matrix02[i, j];
                    }
                });
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


            int r = matrix01.X;
            int c = matrix01.Y;

            Matrix result = new Matrix(r, c);

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = value + matrix01[i, j];//matrix02[i, j];
                }
            });

            /*

               for (int i = 0; i < matrix01.Row; i++)
               {
                   for (int j = 0; j < matrix01.Column; j++)
                   {
                       result[i, j] = value + matrix01[i, j];//matrix02[i, j];
                   }
               }*/
            return result;
        }
        public static Matrix operator +(Matrix matrix01, double value)
        {

            // if (matrix01.Row != matrix02.Row || matrix01.Column != matrix02.Column) throw new Exception("两个矩阵大小必须一致");

            int r = matrix01.X;
            int c = matrix01.Y;

            Matrix result = new Matrix(r, c);

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = matrix01[i, j] + value;//matrix02[i, j];
                }
            });
            /*
            Matrix result = new Matrix(matrix01.Row, matrix01.Column);

            for (int i = 0; i < matrix01.Row; i++)
            {
                for (int j = 0; j < matrix01.Column; j++)
                {
                    result[i, j] = matrix01[i, j] + value;//matrix02[i, j];
                }
            }*/
            return result;
        }
        public static Matrix operator *(Matrix matrix01, Matrix matrix02)
        {

            int r = matrix01.X;
            int c = matrix01.Y;

            if (r != matrix02.X || c != matrix02.Y) throw new Exception("两个矩阵大小必须一致");

            Matrix result = new Matrix(r, c);

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = matrix01[i, j] * matrix02[i, j];
                }
            });

            return result;
        }
        public static Matrix operator *(double value, Matrix matrix01)
        {
            int r = matrix01.X;
            int c = matrix01.Y;

            Matrix result = new Matrix(r, c);

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = matrix01[i, j] * value;
                }
            });


            return result;
        }
        public static Matrix operator *(Matrix matrix01, double value)
        {

            int r = matrix01.X;
            int c = matrix01.Y;

            Matrix result = new Matrix(r, c);

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = matrix01[i, j] * value;
                }
            });
            return result;
        }

        public static Matrix operator /(Matrix matrix01, Matrix matrix02)
        {

            if (matrix01.X != matrix02.X || matrix01.Y != matrix02.Y) throw new Exception("两个矩阵大小必须一致");

            int r = matrix01.X;
            int c = matrix01.Y;

            Matrix result = new Matrix(r, c);

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = matrix01[i, j] / matrix02[i, j];
                }
            });


            return result;
        }
        public static Matrix operator /(double value, Matrix matrix01)
        {

            int r = matrix01.X;
            int c = matrix01.Y;

            Matrix result = new Matrix(r, c);

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = value / matrix01[i, j];//matrix02[i, j];
                }
            });

            return result;
        }
        public static Matrix operator /(Matrix matrix01, double value)
        {

            int r = matrix01.X;
            int c = matrix01.Y;

            Matrix result = new Matrix(r, c);

            Parallel.For(0, r, i =>
            {

                for (int j = 0; j < matrix01.Y; j++)
                {
                    result[i, j] = matrix01[i, j] / value;//matrix02[i, j];
                }
            });

            return result;
        }

    }
}
