using DeepLearning;
using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ML.Drawing
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //TestConvolution2D();

            // TestConvolution3D();

            TestImage2Column2D();

            //1,2,3,0,1,2,3,0,1,2,3,0,1,2,3,0,1,2,0,1,2,3,0,1


           // Matrix matrix =  Matrix3D.Image2Column(new Matrix3D(7,7,3),5,5,1,0);

           //C//onsole.WriteLine(matrix.X + " _ " + matrix.Y);

        }
        public void TestImage2Column2D() {

            Matrix matrix = new Matrix(new double[,] {
            { 1,2,3,0 },
            { 0,1,2,3 },
            { 3,0,1,2 },
            { 2,3,0,1 }
            });
            Matrix core = new Matrix(new double[,] {
            {2,0,1 },
            {0,1,2 },
            {1,0,2 },

            });

            StringBuilder sb = new StringBuilder();

           double [] sum = Matrix.Image2Column(matrix,3,3);

            for (int i = 0; i < sum.Length; i++)
            {
                sb.Append(sum[i].ToString("F0"));
                sb.Append(',');
            }
            Console.WriteLine(sb);

        }
        public void TestConvolution3D()
        {
            Matrix matrix1 = new Matrix(new double[,] {
            { 1,2,3,0 },
            { 0,1,2,3 },
            { 3,0,1,2 },
            { 2,3,0,1 }
            });

            Matrix matrix2 = new Matrix(new double[,] {
            { 1,2,3,0 },
            { 0,1,2,3 },
            { 3,0,1,2 },
            { 2,3,0,1 }
            });

            Matrix matrix3 = new Matrix(new double[,] {
            { 1,2,3,0 },
            { 0,1,2,3 },
            { 3,0,1,2 },
            { 2,3,0,1 }
            });


            Matrix core1 = new Matrix(new double[,] {
            {2,0,1 },
            {0,1,2 },
            {1,0,2 },

            });

            Matrix core2 = new Matrix(new double[,] {
            {2,0,1 },
            {0,1,2 },
            {1,0,2 },

            });

            Matrix core3 = new Matrix(new double[,] {
            {2,0,1 },
            {0,1,2 },
            {1,0,2 },

            });

            Matrix3D matrix3D = new Matrix3D(new Matrix[] { matrix1, matrix2, matrix3 });
            Matrix3D core3D = new Matrix3D(new Matrix[] { core1, core2, core3 });



            Matrix result = CommonFunctions.Convolution3D(matrix3D, core3D);
            Console.WriteLine(result);

        }

        public void TestConvolution2D() {
            Matrix matrix = new Matrix(new double[,] {
            { 1,2,3,0 },
            { 0,1,2,3 },
            { 3,0,1,2 },
            { 2,3,0,1 }
            });
            Matrix core = new Matrix(new double[,] {
            {2,0,1 },
            {0,1,2 },
            {1,0,2 },

            });

            Matrix result = CommonFunctions.Convolution2D(matrix, core);
            Console.WriteLine(result);

        }



    }
}
