using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Activation
{
    class Affine: ILayer
    {
        public Matrix w { get;internal set; }
        public Matrix b { get; internal set; }

        public Matrix dw { get; internal set; }
        public Matrix db { get; internal set; }

        public Matrix x { get; internal set; }

        public Affine(Matrix w ,Matrix b)
        {
            this.w = w;
            this.b = b;
        }

        public Matrix Forward(Matrix x, Matrix t = null) {
            this.x = x;
            return Matrix.Dot(x, w) + b;
        }


        public Matrix Backward(Matrix dout) {

            Matrix dx = Matrix.Dot(dout,w.T);
            dw = Matrix.Dot(x.T, dout);
             db = new Matrix(1, dout.Column);
            for (int i = 0; i < dout.Column; i++)
            {
                double temp = 0;
                for (int j = 0; j < dout.Row; j++)
                {
                    temp += dout[j, i];
                }
                db[0, i] = temp;
            }

           // Console.WriteLine(net.Params[net.Params.Length - 1]);

            return dx;
        
        }
    }
}
