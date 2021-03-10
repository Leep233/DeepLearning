using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning
{
    public class CommonFunctions
    {
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

    }
}
