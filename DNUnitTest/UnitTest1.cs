using DeepLearning;
using DeepLearning.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DNUnitTest
{

    [TestClass]
    public class UnitTest1 {


       
        [TestMethod]
        public void MeanSquaredError() {

            Matrix t = new Matrix(new double[,] { {                
                0,0,1,0 ,0,0 ,0,0 ,0,0
                } });
            Matrix y = new Matrix(new double[,] { {

              0.1,0.05,0.6,0.0,0.05,0.1,0.0,0.1,0.0,0.0
                } });

            double loss = LossFunctions.MeanSquaredError(y, t);

             y = new Matrix(new double[,] { {

              0.1,0.05,0.1,0.0,0.05,0.1,0.0,0.6,0.0,0.0
                } });

             loss = LossFunctions.MeanSquaredError(y, t);
        }

        [TestMethod]
        public void CrossEntropyError() {


            Matrix t = new Matrix(new double[,] { {
                0,0,1,0 ,0,0 ,0,0 ,0,0
                } });
            Matrix y = new Matrix(new double[,] { {

              0.1,0.05,0.6,0.0,0.05,0.1,0.0,0.1,0.0,0.0
                } });

            double loss = LossFunctions.CrossEntropyError(y, t);

            y = new Matrix(new double[,] { {

              0.1,0.05,0.1,0.0,0.05,0.1,0.0,0.6,0.0,0.0
                } });

            loss = LossFunctions.CrossEntropyError(y, t);
        }

        [TestMethod]
        public void TestSoftmax() {
            Matrix matrix = new Matrix(new double[,] { {0.3,2.9,4.0 } });

            matrix = ActivationFunctions.Softmax(matrix);

           
        
        }

        [TestMethod]
    public void TestThreeLayerNeuralNetwork() {
        Matrix inputMatrix = new Matrix(new double[,] { {1,0.5 } });

        //1x2  *   ?? = 1x3
        // 1x2 * 2x3 =1x3;
        Matrix w1 = new Matrix(new double[,] {
        {0.1,0.3,0.5 },
        { 0.2,0.4,0.6}
        });
        Matrix b1 = new Matrix(new double[,] {
        { 0.1,0.2,0.3},
        });
            //1x3 * 3 *2 
            Matrix w2 = new Matrix(new double[,] {
        {0.1,0.4},
        { 0.2,0.5},
        { 0.3,0.6}
        });
        Matrix b2 = new Matrix(new double[,] {
        {0.1,0.2 },
        });

            Matrix w3 = new Matrix(new double[,] {
        {0.1,0.3},
        { 0.2,0.4},
        });
        Matrix b3 = new Matrix(new double[,] {
        {0.1,0.2 },
        });


        inputMatrix =  Matrix.Dot(inputMatrix, w1) + b1;
       inputMatrix = ActivationFunctions.Sigmoid(inputMatrix);

        inputMatrix = Matrix.Dot(inputMatrix, w2) + b2;
        inputMatrix = ActivationFunctions.Sigmoid(inputMatrix);

        inputMatrix = Matrix.Dot(inputMatrix, w3) + b3;
      //  inputMatrix = ActivationFunctions.Sigmoid(inputMatrix);

    }

        [TestMethod]
        public void TestReLU()
        {

            Matrix matrix = new Matrix(new double[,] { { -1, 0, 2, 3 } });
            Matrix matrix1 = new Matrix(new double[,] { { 0, 0, 2, 3 } });

            Assert.IsTrue(ActivationFunctions.ReLU(matrix) == matrix1);
        }
        [TestMethod]
        public void TestStep() {

            Matrix matrix = new Matrix(new double[,] { {-1, 0, 2, 3 }});
            Matrix matrix1 = new Matrix(new double[,] { { 0, 0, 1, 1 } });

            Assert.IsTrue(ActivationFunctions.Step(matrix) == matrix1);
        }
        [TestMethod]
        public void TestSigmoid() {

            Matrix matrix = new Matrix(new double[,] { { -1, 1, 2 } });
            Matrix matrix1 = new Matrix(new double[,] { {0.26894142,0.73105858,0.88079708 } });
            matrix = ActivationFunctions.Sigmoid(matrix);
            Assert.IsTrue(matrix.ToString() == matrix1.ToString());
        }

        [TestMethod]
        public void TestMatrixDot()
        {
            //2x3
            Matrix matrix01 = new Matrix(new double[,] {
            { 1,2,3},
            {4,5,6 }
            });
            //3x2
            Matrix matrix02 = new Matrix(new double[,] {
            {1,2 },
            {3,4 },
            {5,6 }
            });
            Matrix matrix = new Matrix(new double[,] {
            { 22,28},
            {49,64 }
            
            });
         //   Matrix result = Matrix.Dot(matrix01, matrix02);
            Assert.IsTrue(Matrix.Dot(matrix01, matrix02) == matrix);
        }
    }
}
