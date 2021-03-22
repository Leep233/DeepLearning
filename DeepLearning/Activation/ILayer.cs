using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Activation
{
    public interface ILayer
    {
        Matrix Forward(Matrix x,Matrix t = null);


        Matrix Backward(Matrix dout);
    }
}
