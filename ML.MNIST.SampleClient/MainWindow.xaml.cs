using DeepLearning;
using DeepLearning.Net;
using MNIST;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ML.MNIST.SampleClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// 测试数据图片文件
        /// </summary>
        string test_imgs_path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Datas\\t10k-images.idx3-ubyte";
        /// <summary>
        /// 测试数据标签文件
        /// </summary>
        string test_labels_path = $"{AppDomain.CurrentDomain.BaseDirectory}\\Datas\\t10k-labels.idx1-ubyte";
        MNISTImages images;
        MNISTLabels labels;

        TwoLayerNet net;

        public static int ImageMaxCount = 1000;

        private Random _imageRandom = new Random();

        private int _currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {


            //IOptimize optimizer = new SGD();// SGD();//AdaGrad();// Momentum();// SGD();

            net = new TwoLayerNet(784, 50, 10);

            net.LoadWidgets("sampleWidget.td");

            OnClickImage(sender,e);
        }

        private Bitmap LoadImage()
        {

            images = MNISTImages.Load(test_imgs_path, ImageMaxCount);

            labels = MNISTLabels.Load(test_labels_path, ImageMaxCount);

            _currentIndex = _imageRandom.Next(ImageMaxCount);


            Bitmap bitmap = new Bitmap(28, 28);

            byte[] imageBytes = images.Images[_currentIndex];

            for (int j = 0; j < imageBytes.Length; j++)
            {
                int y = j / 28;
                int x = j % 28;
                int color = imageBytes[j];
                bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(color, color, color));
            }

            return bitmap;
        }

        private void OnClickImage(object sender, RoutedEventArgs e)
        {
            _image.Source = BitmapToBitmapImage(LoadImage());
        }

        public void Print(string mesage) {

            string content = $"[{DateTime.Now:HH:mm:ss}] : {mesage}\n";

            this.Dispatcher.Invoke(()=> {

                _msgRTB.AppendText(content);
            });
        
        }

        /// <summary>
        /// 图片转换
        /// </summary>
        /// <param name="bitmap">bitmap格式图片</param>
        /// <returns></returns>
        private static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            // 直接设置DPI
            bitmap.SetResolution(28, 28);
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        private void OnClickPredictBtn(object sender, RoutedEventArgs e)
        {

            byte[] imageBytes = images.Images[_currentIndex];

            DeepLearning.Math.Matrix x_train = new DeepLearning.Math.Matrix(1,784);

            int x_train_row = x_train.Row;
            int y_train_col = x_train.Column;

            Parallel.For(0, x_train_row, i => {
                for (int j = 0; j < y_train_col; j++)
                {
                    x_train[i, j] = imageBytes[j] / 255.0;
                }
            });

            DeepLearning.Math.Matrix matrix = net.Predict(x_train);
 
             matrix = ActivationFunctions.Softmax(matrix);

            string msg = "预测：";

            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Column; j++)
                {
                    msg += $"[{j}]:{matrix[i,j]:P2}\n";
                }
               // msg += "\n";
            }

            Print(msg);

            Console.WriteLine(matrix);
        }
    }
}
