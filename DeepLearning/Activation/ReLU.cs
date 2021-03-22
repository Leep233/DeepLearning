using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Activation
{
   public class ReLU: ILayer
    {

        public Matrix mask { get; private set; }

        public Matrix Forward(Matrix x, Matrix t = null) {

            int row = x.Row;
            int col = x.Column;

            Matrix result = new Matrix(row, col);
            mask = new Matrix(row,col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (x[i, j] <= 0) {
                        result[i, j] = 0;

                        mask[i, j] = 0;
                    }
                    else {
                        result[i, j] = x[i, j];
                        mask[i, j] = 1;
                    }
                    
                }
            }
            return result;
        }

        public Matrix Backward(Matrix dout) {

            Matrix dx = mask * dout;

            return dx;
        }


    }
}
