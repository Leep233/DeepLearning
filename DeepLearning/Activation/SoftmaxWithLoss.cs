using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Activation
{
    class SoftmaxWithLoss: ILayer
    {
        public Matrix y { get; private set; }
        public Matrix t { get; private set; }

        public double loss { get; private set; }

        public Matrix Forward(Matrix x, Matrix t) {
            this.t = t;
            y = ActivationFunctions.Softmax(x);
            loss= LossFunctions.CrossEntropyError(y,t);
            return new Matrix(new double[,] { { loss} });
        }

        public Matrix Backward(Matrix dout) {
            int batchSize = t.Row;
      Matrix matrix = (y - t) / batchSize;
            return matrix;
        }
    }
}
