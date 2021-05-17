using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Math
{
    public class Matrix3DCollection
    {
        private Matrix3D[] matrix3Ds;

        public Matrix3DCollection(Matrix3D[] matrix3Ds)
        {
            this.matrix3Ds = matrix3Ds;
        }

        public Matrix3DCollection(double [][,,] data)
        {
            int length = data.GetLength(0);

            matrix3Ds = new Matrix3D[length];

            for (int i = 0; i < length; i++)
            {
                matrix3Ds[i] = new Matrix3D(data[i]);
            }
        } 
        

    }
}
