using System;
using DeepLearning;
using DeepLearning.Math;

namespace DeepLearingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix t = new Matrix(new double[3, 3] {
            {0,1,0 },
            {1,0,0 },
            { 0,0,1},
            });

            CrossEntropyError();
            Console.Read();
        }

        public static void CrossEntropyError()
        {


            Matrix t = new Matrix(new double[,] { {
                0,0,1,0 ,0,0 ,0,0 ,0,0
                } });
            Matrix y = new Matrix(new double[,] { {

              0.1,0.05,0.6,0.0,0.05,0.1,0.0,0.1,0.0,0.0
                } });

            double loss = LossFunctions.CrossEntropyError(y, t);

            Console.WriteLine(loss);

            y = new Matrix(new double[,] { {

              0.1,0.05,0.1,0.0,0.05,0.1,0.0,0.6,0.0,0.0
                } });

            loss = LossFunctions.CrossEntropyError(y, t);

            Console.WriteLine(loss);

        }
    }
}
