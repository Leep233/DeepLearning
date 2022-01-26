using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Math
{
    public class Matrix3D
    {

        public int X { get => _content[0].X; }
        public int Y { get => _content[0].Y; }
        public int Z { get => _content?.Length ?? 0; }


        private Matrix[] _content;

        public double this[int x, int y,int z]
        {
            get {
                return _content[z][x, y];
            }
            set {

                _content[z][x, y] = value;
            }
        
        
        }

        public Matrix3D(Matrix[] matrices)
        {
            _content = matrices;
        }

        public Matrix3D(int weight,int height,int channel)
        {
            _content = new Matrix[channel];

            for (int i = 0; i < channel; i++)
            {
                _content[i] = new Matrix(weight, height);
            }
        }

        public Matrix3D(double[,,] data) {

            int z = data.GetLength(0);

            int x = data.GetLength(1);

            int y = data.GetLength(2);

            _content = new Matrix[z];

            for (int i = 0; i < z; i++)
            {
               Matrix matrix = _content[i];

                for (int r = 0; r < x; r++)
                {
                    for (int c = 0; c < y; c++)
                    {
                        matrix[r,c] = data[i,r,c];
                    }
                }
            }
        }

        public static double [] Image2Column(Matrix3D matrix3D) {

            int size = matrix3D.Z* matrix3D.X * matrix3D.Y;

            double[] result = new double[size];

            for (int z = 0; z < matrix3D.Z; z++)
            {
                for (int x = 0; x < matrix3D.X; x++)
                {
                    for (int y = 0; y < matrix3D.Y; y++)
                    {
                        int offset = x * y + y;

                        int index = z *offset +offset;

                        result[index] = matrix3D[x,y,z];
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ????
        /// </summary>
        /// <param name="matrix3D">输入数据</param>
        /// <param name="coreWidth">卷积核宽度</param>
        /// <param name="coreHeight">卷积核高度</param>
        /// <param name="stride">步幅</param>
        /// <param name="padding">填充</param>
        /// <returns></returns>
        public static Matrix Image2Column(Matrix3D matrix3D, int coreWidth, int coreHeight, int stride=1, int padding=0)
        {
            int _2Padding = padding * 2;
            ///列长度 等于 N 个 卷积核 展开的长度， 卷积核数量* 卷积核长 *卷积核宽；
            int height = matrix3D.Z * coreWidth * coreWidth;

            int xOffset = (matrix3D.X + _2Padding - coreWidth) / stride + 1;

            int yOffset  = (matrix3D.Y + _2Padding - coreHeight) / stride + 1;

            int width = xOffset * yOffset;

            Matrix result = new Matrix(width,height);//[1 * Y] * [ Y* X] =[1 * X]

            //int z = 0;
            //
            //for (int x = 0; x < matrix3D.X; x++)
            //{
            //    for (int y = 0; y < matrix3D.Y; y++)
            //    {
            //        ///转化的矩阵 的 X下标， 输入Image矩阵的当前宽x * Image矩阵当前的高y + Image矩阵当前的高y  + 填充值
            //        int xIndex = x * y + y + padding;
            //
            //        result[xIndex, z] = matrix3D[x,y,z];
            //    }
            //}

            return result;
        }

    }
}
