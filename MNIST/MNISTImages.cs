using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MNIST
{
   public class MNISTImages
    {
        public int MagicNumber { get;  set; }
        public int Count { get;  set; }

        public int Row { get;  set; }

        public int Column { get;  set; }

        public List<byte[]> Images { get;  set; }

        public MNISTImages()
        {
            Images = new List<byte[]>();
        }

        public static MNISTImages Load(string fileName, int count = 1000) {

            if (!File.Exists(fileName)) return null;


            byte[] buffer = null;
            using (FileStream fs = File.OpenRead(fileName))
            {
                buffer = new byte[fs.Length];
                int length = fs.Read(buffer, 0, buffer.Length);
                
            }

            if (buffer is null || buffer.Length < 1) return null;
            MNISTImages images = new MNISTImages();

            byte[] bytes = new byte[4];

           

            Buffer.BlockCopy(buffer, 0, bytes, 0, bytes.Length);

            Array.Reverse(bytes);

            images.MagicNumber = BitConverter.ToInt32(bytes, 0);

            Buffer.BlockCopy(buffer, 4, bytes, 0, bytes.Length);

            Array.Reverse(bytes);

            images.Count = BitConverter.ToInt32(bytes, 0);

            Buffer.BlockCopy(buffer, 8, bytes, 0, bytes.Length);

            Array.Reverse(bytes);

            images.Row = BitConverter.ToInt32(bytes, 0);

            Buffer.BlockCopy(buffer, 12, bytes, 0, bytes.Length);

            Array.Reverse(bytes);

            images.Column = BitConverter.ToInt32(bytes, 0);

            int imgSize = images.Row * images.Column;

            count = images.Count < count ? images.Count : count;

            for (int i = 0; i < count; i++)
            {
              byte [] data =  new byte[imgSize];

                Buffer.BlockCopy(buffer, 12 + i * imgSize, data, 0, imgSize);

                images.Images.Add(data);
                
            }
           

            return images;
        }

    }
}
