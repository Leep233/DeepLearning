using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;


namespace DeepLearning
{
    public class CommonFunctions
    {


        private readonly static double SND =  1/System.Math.Sqrt(2*System.Math.PI);

        readonly static Random r1 = new Random();

        readonly static Random r2 = new Random();

        readonly static Random random = new Random();
        public static void GradientDescent(Func<Matrix, Matrix> f, ref Matrix t,double lr,int stepNum) {
            for (int i = 0; i < stepNum; i++)
            {
                Matrix grad = Gradient(f, t);
                t -= lr * grad;
            }
        } 
        public static Matrix Gradient(Func<Matrix, Matrix> f,Matrix t) {

            double h = 1e-4;

            int x = t.Row;

            int y = t.Column;            

            Matrix grad = new Matrix(x,y);

            Matrix tmp = new Matrix(1, 1);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j <y; j++)
                {
                    tmp[0,0] = t[i, j];

                    grad[i,j] = ((f(tmp + h) - f(tmp - h)) / (2 * h))[0,0];
                }
            }
            return grad;
        
        }

        public static double Gradient(Func<double, double> func, double t)
        {
            double h = 1e-4;
            return (func(t + h) - func(t - h)) / (2 * h);
        }

        public static Matrix Gradient(Func<Matrix, double> func, Matrix t) {
            Matrix grad = new Matrix(t.Row,t.Column);
            double h = 1e-4;
            //double  = 0;
            double l1,l2, tmp = 0;
            

            for (int i = 0; i < t.Row; i++)
            {
                for (int j = 0; j < t.Column; j++)
                {
                    tmp = t[i, j];

                    t[i, j] = tmp + h ;

                    l1 =  func(t);

                    t[i, j] = tmp - h;

                    l2 = func(t);

                    grad[i,j] = (l1-l2)/(2*h);

                    t[i, j] = tmp;
                }
            }

            return grad;
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">期望</param>
        /// <param name="d">方差</param>
        /// <returns></returns>
        public static double NormalDistribution(double u = 0, double d=1)
        {
            double u1 = r1.NextDouble();

            double u2 = r2.NextDouble();

            double result = 0;
            try
            {
                result = u + System.Math.Sqrt(d) * System. Math.Sqrt((-2) * (System.Math.Log(u1))) * System.Math.Sin(2 * System. Math.PI * u2);
            }
            catch (Exception ex)
            {
              //  result = null;
            }
            return result;
        }
    
        /// <summary>
        /// 获取一个标准正态分布值
        /// </summary>
        /// <returns></returns>
        public static double StandardNormalDistribution() 
        {
            double u1 = r1.NextDouble();
            double u2 = r2.NextDouble();
            return  System.Math.Sqrt((-2) * (System.Math.Log(u1))) * System.Math.Sin(2 * System.Math.PI * u2); ;
          
        }

        public static int[] RandomChoice(int max, int size) {

            int[] result = new int[size];         

            for (int i = 0; i < size; i++)
            {
                result[i] =  random.Next(max);
            }

            return result;
        }

        public static Matrix StandardNormalDistributionRondam(int x,int y)
        {
            Matrix matrix = new Matrix(x, y);


            for (int i = 0; i < x; i++)         
                for (int j = 0; j < y; j++)
                    matrix[i,j] = StandardNormalDistribution();

            return matrix;

        }


        public static Matrix Image2Column(Matrix3DCollection collection) {

            int row = 0;

            int column = 0;

            Matrix result = new Matrix(row, column);





            return result;

        }

    }
}
