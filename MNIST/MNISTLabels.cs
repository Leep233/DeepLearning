using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MNIST
{
    public class MNISTLabels
    {
        public int MagicNumber { get; set; }

        public int Count { get; set; }


        public byte[] Labels { get; set; }

        public static MNISTLabels Load(string mnistFile,int count = 1000)
        {

            if (!File.Exists(mnistFile)) return null;


            byte[] buffer = null;
            using (FileStream fs = File.OpenRead(mnistFile))
            {
                buffer = new byte[fs.Length];
                int length = fs.Read(buffer, 0, buffer.Length);


            }

            if (buffer is null || buffer.Length < 1) return null;
            MNISTLabels labels = new MNISTLabels();

            byte[] bytes = new byte[4];

            Buffer.BlockCopy(buffer, 0, bytes, 0, bytes.Length);

            Array.Reverse(bytes);

            labels.MagicNumber = BitConverter.ToInt32(bytes, 0);

            Buffer.BlockCopy(buffer, 4, bytes, 0, bytes.Length);

            Array.Reverse(bytes);

            labels.Count = BitConverter.ToInt32(bytes, 0);

            count = labels.Count < count ? labels.Count : count;

            labels.Labels = new byte[count];

            Buffer.BlockCopy(buffer, 8, labels.Labels, 0, labels.Labels.Length);

            return labels;
        }
    }
}
