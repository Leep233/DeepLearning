using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNIST.Sample
{
    public partial class Form1 : Form
    {
        string imgsPath = @"D:\Projects\Visual Studio Projects\DeepLearning\Datas\train-images.idx3-ubyte";
        string labelsPath = @"D:\Projects\Visual Studio Projects\DeepLearning\Datas\train-labels.idx1-ubyte";
        MNISTImages images;
        MNISTLabels labels;
        Timer _timer;

        int index;

        public Form1()
        {
            InitializeComponent();

            _timer = new Timer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = 1000;
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
           // throw new NotImplementedException();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            index++;

            if(index>images.Images.Count) _timer?.Stop();

            this.Invoke(new EventHandler((s,a)=> {

                pictureBox1.Image = img[index];
                label1.Text = labels.Labels[index].ToString();

            }));

        }
        List<Bitmap> img = new List<Bitmap>();
        private void button1_Click(object sender, EventArgs e)
        {
             images = MNISTImages.Load(imgsPath);
             labels = MNISTLabels.Load(labelsPath);

            for (int i = 0; i < images.Images.Count; i++)
            {
                Bitmap bitmap = new Bitmap(images.Row, images.Column);
                byte[] imageBytes = images.Images[i];

                for (int j = 0; j < imageBytes.Length; j++)
                {
                    int y = j / 28;
                    int x = j % 28;
                    int color = imageBytes[j];
                    bitmap.SetPixel(x,y, Color.FromArgb(color, color, color));

                   
                }

                img.Add(bitmap);

            }

            _timer.Start();

        }
    }
}
