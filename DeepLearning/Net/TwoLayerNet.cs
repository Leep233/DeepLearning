using DeepLearning.Activation;
using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLearning.Net
{
    public class TwoLayerNet
    {

        public Matrix[] Params;

        public Dictionary<string, ILayer> layers { get; set; }

        public ILayer softmaxWithLoss;

        Affine affineLayer01;
        Affine affineLayer02;

        public TwoLayerNet(int inputSize, int hiddenSize, int outputSize, double weightInit = 0.01)
        {
    
            Params =new Matrix[4];

            layers = new Dictionary<string, ILayer>();
            // 1x3  3 x ?
            Params[0] =( weightInit * CommonFunctions.StandardNormalDistributionRondam(inputSize, hiddenSize));

            Params[1] = ( new Matrix(1, hiddenSize));

            Params[2] = ( weightInit * CommonFunctions.StandardNormalDistributionRondam(hiddenSize, outputSize));

            Params[3] = ( new Matrix(1, outputSize));

            affineLayer01 = new Affine(Params[0], Params[1]);
            layers.Add("A1", affineLayer01);

          //  layers.Add("A1", new Affine(Params[0], Params[1]));

            layers.Add("ReLU1", new ReLU());

            affineLayer02 = new Affine(Params[2], Params[3]);
            layers.Add("A2", affineLayer02);

            //  layers.Add("A2", new Affine(Params[2], Params[3]));

            softmaxWithLoss = new SoftmaxWithLoss();
//
          //  layers.Add("Softmax",);

        }

        public void Update() {

           // Affine   affineLayer01 =  (layers["A1"] as Affine)

           affineLayer01.w = Params[0];

            affineLayer01.b = Params[1];

            affineLayer02.w = Params[2];

            affineLayer02.b = Params[3];
        }

        /// <summary>
        /// 精准值
        /// </summary>
        /// <returns></returns>
        public double Accuracy(Matrix x,Matrix t) {

            Matrix y = Predict(x);
            double[] yMaxIndex = y.Argmax(1);
            double[] tMaxIndex = t.Argmax(1);

            int sum = 0;

            for (int i = 0; i < yMaxIndex.Length; i++)
            {
                if (yMaxIndex[i].Equals(tMaxIndex[i]))
                    sum++;
            }

            // # BUG  修复
           // return sum / (float)t.Column;

            return sum / (float)t.Row;
        }


        public Matrix Predict(Matrix x) {
            /*
            Matrix w1 = Params["w1"];
            Matrix w2 = Params["w2"];

            Matrix b1 = Params["b1"];
            Matrix b2 = Params["b2"];

            Matrix y = Matrix.Dot(x, w1) + b1;
            y = ActivationFunctions.Sigmoid(y);

            y = Matrix.Dot(y, w2) + b2;
            y = ActivationFunctions.Softmax(y);
            */
     


            Matrix y=(layers["A1"] as Affine).Forward(x);
             y = (layers["ReLU1"] as ReLU).Forward(y);
             y = (layers["A2"] as Affine).Forward(y);
            

            return y;
        }

        public double Loss(Matrix x,Matrix t) {
            Matrix y = Predict(x);

            return softmaxWithLoss.Forward(y,t)[0,0];
           // return LossFunctions.CrossEntropyError(y,t);

        }
        public delegate void Print(Matrix x);
        public event Print print;
        Matrix[] grads = new Matrix[4];
        public Matrix[] Gradient(Matrix x, Matrix t)
        {

            Loss(x,t);

            Matrix dout = softmaxWithLoss.Backward(null);


            dout =affineLayer02.Backward(dout);

            print?.Invoke(affineLayer02.b);

            dout =(layers["ReLU1"] as ReLU).Backward(dout);

            dout =(affineLayer01).Backward(dout);


            grads[0] = affineLayer01.dw;//(CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[0]));

            grads[1] = affineLayer01.db;// (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[1]));

            grads[2] = affineLayer02.dw;// (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[2]));

            grads[3] = affineLayer02.db;// (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[3]));

            return grads;

        }

     //   Matrix[] grads = new Matrix[4];
        public Matrix[] NumericalGradient(Matrix x,Matrix t) {

            grads[0]=( CommonFunctions.Gradient((Matrix)=>Loss(x,t), Params[0]));

            grads[1]= ( CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[1]));

            grads[2]= ( CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[2]));

            grads[3]= ( CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[3]));

            return grads;
        }


    }
}
