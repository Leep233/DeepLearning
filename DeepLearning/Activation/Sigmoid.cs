using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Activation
{
    class Sigmoid: ILayer
    {
        public Matrix outMatrix { get; private set; }

        public Matrix Forward(Matrix x, Matrix t = null) 
        {
            outMatrix = ActivationFunctions.Sigmoid(x);
            return outMatrix;
        }


        public Matrix Backward(Matrix dout) {
            return dout * outMatrix * (1 - outMatrix);
        }

    }
}
