using DeepLearning.Activation;
using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeepLearning.Net
{
    public class TwoLayerNet
    {
        public List<double> losses { get; private set; } = new List<double>();

        public event Action<double> LossUpdated;

        /// <summary>
        ///参数数组 保存各层权重和偏置
        /// </summary>
        public Matrix[] Params;
     
        /// <summary>
        /// 用于保存 每次求导值
        /// </summary>
        private Matrix[] grads;
  
        /// <summary>
        /// 隐藏层 Map
        /// </summary>
        public Dictionary<string, ILayer> layers { get; set; }

        /// <summary>
        /// 最后输出层内容
        /// </summary>
        public ILayer softmaxWithLoss;

        /// <summary>
        ///  第一层 线性组合
        /// </summary>
        Affine affineLayer01;
        /// <summary>
        /// 第二次 线性组合
        /// </summary>
        Affine affineLayer02;
        private Dropout dropout01;
        private Dropout dropout02;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSize">输入数据单个的size</param>
        /// <param name="hiddenSize">隐藏层size</param>
        /// <param name="outputSize">输出层size</param>
        /// <param name="weightInit"></param>
        public TwoLayerNet(int inputSize, int hiddenSize, int outputSize, double weightInit = 0.01)
        {

            Params = new Matrix[4];
            grads = new Matrix[4];

            layers = new Dictionary<string, ILayer>();
            //初始化 权重 和 偏置
            Params[0] = (weightInit * CommonFunctions.StandardNormalDistributionRondam(inputSize, hiddenSize));

            Params[1] = (new Matrix(1, hiddenSize));

            Params[2] = (weightInit * CommonFunctions.StandardNormalDistributionRondam(hiddenSize, outputSize));

            Params[3] = (new Matrix(1, outputSize));

            ///第一层线性组合
            affineLayer01 = new Affine(Params[0], Params[1]);
            layers.Add("A1", affineLayer01);

            dropout01 = new Dropout(0.15);

            layers.Add("D1", dropout01);
            ///第一次非线性激活
            layers.Add("ReLU1", new ReLU());

            ///第二次线性组合
            affineLayer02 = new Affine(Params[2], Params[3]);
            layers.Add("A2", affineLayer02);

            dropout02 = new Dropout(0.15);
            layers.Add("D2", dropout02);
            ///输出层 非线性激活 输出
            softmaxWithLoss = new SoftmaxWithLoss();

        }

        /// <summary>
        /// 更新 各个层 的权重和偏置
        /// </summary>
        public void Update()
        {

            affineLayer01.w = Params[0];

            affineLayer01.b = Params[1];

            affineLayer02.w = Params[2];

            affineLayer02.b = Params[3];
        }

        /// <summary>
        /// 精准值
        /// </summary>
        /// <returns></returns>
        public double Accuracy(Matrix x, Matrix t)
        {

            Matrix y = Predict(x);

            double[] yMaxIndex = y.Argmax(1);

            double[] tMaxIndex = t.Argmax(1);

            int sum = 0;
            int length = yMaxIndex.Length;

            Parallel.For(0, length, i =>
            {

                if (yMaxIndex[i].Equals(tMaxIndex[i])) {

                    Interlocked.Increment(ref sum);
                }
 
            });

        
            // # BUG  修复
            // return sum / (float)t.Column;

            return sum / (float)t.X;
        }

        /// <summary>
        /// 预测结果
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Matrix Predict(Matrix x)
        {

            Matrix y = (layers["A1"] as Affine).Forward(x);

            y = (layers["D1"] as Dropout).Forward(y);

            y = (layers["ReLU1"] as ReLU).Forward(y);

            y = (layers["A2"] as Affine).Forward(y);

            y = (layers["D2"] as Dropout).Forward(y);

            return y;
        }

        /// <summary>
        /// 损失计算
        /// </summary>
        /// <param name="x"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public double Loss(Matrix x, Matrix t)
        {
            Matrix y = Predict(x);
          double loss =  softmaxWithLoss.Forward(y, t)[0, 0]; 
            LossUpdated?.Invoke(loss);

            return loss;
        }

        /// <summary>
        /// 利用反向传播（链式法则） 求导
        /// </summary>
        /// <param name="x"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public Matrix[] Gradient(Matrix x, Matrix t)
        {

            losses.Add( Loss(x, t));

            ///反向传播求导

            Matrix dout = softmaxWithLoss.Backward(null);

            dout = dropout02.Backward(dout);

            dout = affineLayer02.Backward(dout);

            dout = (layers["ReLU1"] as ReLU).Backward(dout);

            dout = dropout01.Backward(dout);

            dout = (affineLayer01).Backward(dout);


            grads[0] = affineLayer01.dw;//(CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[0]));

            grads[1] = affineLayer01.db;// (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[1]));

            grads[2] = affineLayer02.dw;// (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[2]));

            grads[3] = affineLayer02.db;// (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[3]));

            return grads;

        }

        /// <summary>
        /// 通常 求导公式 方式求梯度（导数）
        /// </summary>
        /// <param name="x"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public Matrix[] NumericalGradient(Matrix x, Matrix t)
        {

            grads[0] = (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[0]));

            grads[1] = (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[1]));

            grads[2] = (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[2]));

            grads[3] = (CommonFunctions.Gradient((Matrix) => Loss(x, t), Params[3]));

            return grads;
        }

        public void LoadWidgets(string fileName) {

            TrainingData data = TrainingData.Load(fileName);


            affineLayer01.w = data.Content[0];

            affineLayer01.b = data.Content[1];

            affineLayer02.w = data.Content[2];

            affineLayer02.b = data.Content[3];

        }

        public void Save(string fileName) {

            TrainingData data = new TrainingData();
            data.LayerNumber = 2;

            data.Rows = new int[grads.Length];
            data.Columns = new int[grads.Length];

            Matrix matrix = affineLayer01.w;
            data.Rows[0] = matrix.X;
            data.Columns[0] = matrix.Y;
            data.Content.Add(matrix);

            matrix = affineLayer01.b;
            data.Rows[1] = matrix.X;
            data.Columns[1] = matrix.Y;
            data.Content.Add(matrix);

            matrix = affineLayer02.w;
            data.Rows[2] = matrix.X;
            data.Columns[2] = matrix.Y;
            data.Content.Add(matrix);

            matrix = affineLayer02.b;
            data.Rows[3] = matrix.X;
            data.Columns[3] = matrix.Y;
            data.Content.Add(matrix);

            data.SaveAsync(fileName);

        }

    }
}
