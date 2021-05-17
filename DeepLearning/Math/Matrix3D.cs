using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Math
{
    public class Matrix3D
    {

        public int X { get => _content[0].Row; }
        public int Y { get => _content[0].Column; }
        public int Z { get => _content?.Length ?? 0; }


        private Matrix[] _content;

        public double this[int z, int x, int y]
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
                _content[channel] = new Matrix(weight, height);
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
    
    
    }
}
