using DeepLearning;
using DeepLearning.Interfaces;
using DeepLearning.Math;
using DeepLearning.Net;
using DeepLearning.Optimize;
using MNIST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearingTest
{
   public class NetTester : Tester<NetTester>
    {
        public List<double> losses { get { return net.losses; } }
        public List<double> accuracies = new List<double>();

        public event Action<double> LossUpdated;
        public event Action<double> AccuracyUpdated;

        public TwoLayerNet net { get; private set; }

        /// <summary>
        /// MNIST训练数据图片文件路径
        /// </summary>
        string train_imgs_path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Datas\\train-images.idx3-ubyte";
        /// <summary>
        /// MNIST训练数据标签文件
        /// </summary>
        string train_labels_path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Datas\\train-labels.idx1-ubyte";
        /// <summary>
        /// 测试数据图片文件
        /// </summary>
        string test_imgs_path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Datas\\t10k-images.idx3-ubyte";
        /// <summary>
        /// 测试数据标签文件
        /// </summary>
        string test_labels_path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Datas\\t10k-labels.idx1-ubyte";
        /// <summary>
        /// 训练数据个数
        /// </summary>
        int TrainDataCount = 30000;
        /// <summary>
        /// 测试数据个数
        /// </summary>
        int TestDataCount = 10000;

        private List<Matrix> LoadMNIST()
        {

            List<Matrix> matrices = new List<Matrix>();

            MNISTImages train_images = MNISTImages.Load(train_imgs_path, TrainDataCount);

            Matrix x_train = new Matrix(TrainDataCount, 784);

            int x_train_row = x_train.X;
            int y_train_col = x_train.Y;

            Parallel.For(0, x_train_row, i =>
            {
                for (int j = 0; j < y_train_col; j++)
                {
                    x_train[i, j] = train_images.Images[i][j] / 255.0;
                }
            });



            MNISTLabels train_labels = MNISTLabels.Load(train_labels_path, TrainDataCount);

            Matrix t_train = new Matrix(TrainDataCount, 10);
            //转成one-hot
            for (int i = 0; i < t_train.X; i++)
            {
                t_train[i, train_labels.Labels[i]] = 1;
            }


            MNISTImages test_images = MNISTImages.Load(test_imgs_path, TestDataCount);

            Matrix x_test = new Matrix(TestDataCount, 784);

            int x_test_row = x_test.X;
            int y_test_col = x_test.Y;


            Parallel.For(0, x_test_row, i =>
            {
                for (int j = 0; j < y_test_col; j++)
                {
                    x_test[i, j] = test_images.Images[i][j] / 255.0;
                }
            });


            MNISTLabels test_labels = MNISTLabels.Load(test_labels_path, TestDataCount);

            Matrix t_test = new Matrix(TestDataCount, 10);

            for (int i = 0; i < t_test.X; i++)
            {
                t_test[i, test_labels.Labels[i]] = 1;
            }

            matrices.Add(x_train);
            matrices.Add(t_train);
            matrices.Add(x_test);
            matrices.Add(t_test);

            Console.WriteLine("MNIST 加载完成");

            return matrices;

        }

        public Task<List<Matrix>> LoadMNISTAsync()
        {

            return Task.Run(() => LoadMNIST());

        }

        System.Random random = new Random();
       
        public async void TestTwoLayerNet()
        {

            // TrainingData data = TrainingData.Load("sampleWidget.td");

            List<Matrix> matrices = await LoadMNISTAsync();

            Matrix x_train = matrices[0];
            Matrix t_train = matrices[1];

            // Matrix x_test = matrices[2];
            // Matrix y_test = matrices[3];

            int itersNum = 3000;
            int batch_size = 100;


            IOptimize optimizer = new SGD();// SGD();//AdaGrad();// Momentum();// SGD();

            net = new TwoLayerNet(784, 50, 10);

            net.LossUpdated += loss => { this.LossUpdated?.Invoke(loss); };

            //  net.print += Net_print;
            Matrix x_batch = new Matrix(batch_size, 784);

            Matrix t_batch = new Matrix(batch_size, 10);


            int[] indexs = new int[batch_size];


            for (int i = 0; i < itersNum; i++)
            {

                for (int j = 0; j < batch_size; j++)
                {
                    indexs[j] = random.Next(TrainDataCount);
                }

                for (int row = 0; row < indexs.Length; row++)
                {
                    int index = indexs[row];

                    //  Console.WriteLine(index);

                    for (int col = 0; col < x_batch.Y; col++)
                    {
                        x_batch[row, col] = x_train[index, col];

                    }
                    for (int col = 0; col < t_batch.Y; col++)
                    {
                        t_batch[row, col] = t_train[index, col];//      
                    }

                }

                // Matrix[] grads = net.NumericalGradient(x_batch, t_batch);

                Matrix[] grads = net.Gradient(x_batch, t_batch);

                optimizer.Update(net.Params, grads);

                net.Update();

                Console.WriteLine($"损失值:{net.Loss(x_batch, t_batch)} ");

                if (i % 300 == 0)
                {
                    Console.WriteLine("识别精度计算中...");
                    double accuracy = net.Accuracy(x_train, t_train);

                    AccuracyUpdated?.Invoke(accuracy);
                    accuracies.Add(accuracy);

                    Console.WriteLine($"识别精度：{accuracy}");
                }

            }

            net.Save("sampleWidget.td");

        }

    }
}
