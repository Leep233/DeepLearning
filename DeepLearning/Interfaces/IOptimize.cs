using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Interfaces
{
    public interface IOptimize
    {
        void Update(Matrix[] paramMap, Matrix[] grads);
    }
}
