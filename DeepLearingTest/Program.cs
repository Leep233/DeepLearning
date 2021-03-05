using System;
using DeepLearning;
using DeepLearning.Math;

namespace DeepLearingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestGradient01();
            GradientDescent();
            //CrossEntropyError();
            Console.Read();
        }
        public static void GradientDescent() {

            Matrix x = new Matrix(new double[,] {

                { -3,4 },  

            });

            CommonFunctions.GradientDescent(Function02,ref x,1e-10,100);

            for (int i = 0; i < x.Row; i++)
            {
                for (int j = 0; j < x.Column; j++)
                {
                    Console.WriteLine(x[i, j]);
                }
            }

        }
        public static void TestGradient02()
        {

            Matrix x = new Matrix(new double[,] {

                { 3,4 },
                 { 0,2 },
                  { 3,0 }

            });


            Matrix grad = CommonFunctions.Gradient(Function02, x);
            for (int i = 0; i < grad.Row; i++)
            {
                for (int j = 0; j < grad.Column; j++)
                {
                    Console.WriteLine(grad[i,j]);
                }
            }
        }

        public static void TestGradient01() {

            Matrix x = new Matrix(new double[,] {

                {5 },{ 10}
            
            });

            Matrix grad = CommonFunctions.Gradient(Function01, x);
            for (int i = 0; i < grad.Row; i++)
            {
                for (int j = 0; j < grad.Column; j++)
                {
                    Console.WriteLine(grad[i, j]);
                }
            }


        }

        public static Matrix Function02(Matrix x) {
            Matrix result = new Matrix(x.Row,x.Column);

            double temp = 9;

            for (int i = 0; i < x.Row; i++)
            {
                for (int j = 0; j < x.Column; j++)
                {
                    temp += System.Math.Pow(x[i,j],2);

                    result[i, j] = temp;

                }
            }

           // result[0, 0] = temp;

            return result;
        }

        public static Matrix Function01(Matrix x) {
            Matrix result = new Matrix(x.Row,x.Column);

            for (int i = 0; i < x.Row; i++)
            {
                for (int j = 0; j < x.Column; j++)
                {
                    double temp = x[i, j];
                    result[i, j] = 0.01 * System.Math.Pow(temp, 2) + 0.1 * temp;
                }
            }
            return result;
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
