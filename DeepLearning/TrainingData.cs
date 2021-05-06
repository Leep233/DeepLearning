using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearning
{
    /// <summary>
    /// 训练数据
    /// </summary>
   public class TrainingData
    {
        /// <summary>
        /// 幻数
        /// </summary>
        public int MagicNumber { get; set; } = 0;
        /// <summary>
        /// 网络层数
        /// </summary>
        public byte LayerNumber { get; set; }
        public int[] Rows { get; set; }
        public int[] Columns { get; set; }
        public List<Matrix> Content { get; set; }
        public Task<bool> SaveAsync(string fileName) => Task.Run(() => Save(fileName));

        public TrainingData()
        {
            Content = new List<Matrix>();
        }
        public bool Save(string fileName)
        {
            bool isSuc = true;

            int size = LayerNumber << 1;

            int intSize = sizeof(int);
            int doubleSize = sizeof(double);

            //算出 总共需要的字节数Size
            int length = 5 + (Rows.Length * intSize) + (Columns.Length* intSize);

            for (int i = 0; i < size; i++)
                length += Rows[i] * Columns[i];

            //保证Rows , Columns 都是有序的 比如 Content[i] 的 Size = Rows[i] * Columns[i];
            //同时 必须保证Content 是有规律add进来的  结构为 [ [w1] [b1] [w2] [b2],[...] [...] [wN][bN]]

      

            using (MemoryStream ms = new MemoryStream(length))
            {
                try
                {
                    //写入幻数
                    ms.Write(BitConverter.GetBytes(MagicNumber), 0, intSize);
                    //写入神经网络层数
                    ms.WriteByte(LayerNumber);
                    //一次写入每层的权重/偏置 宽，高
                    for (int i = 0; i < size; i++)
                    {
                        ms.Write(BitConverter.GetBytes(Rows[i]), 0, intSize);

                        ms.Write(BitConverter.GetBytes(Columns[i]), 0, intSize);
                    }
                    //以此写入每层权重/偏置数据
                    for (int i = 0; i < Content.Count; i++)
                    {
                        Matrix matrix = Content[i];

                        for (int m = 0; m < matrix.Row; m++)
                        {
                            for (int n = 0; n < matrix.Column; n++)
                            {
                                ms.Write(BitConverter.GetBytes(matrix[m, n]), 0, doubleSize);
                            }
                        }
                    }

                    ms.Flush();

                    using (FileStream fs = File.Exists(fileName) ? File.OpenWrite(fileName) : File.Create(fileName))
                    {
                        byte[] buffer = ms.GetBuffer();

                        fs.Write(buffer, 0, buffer.Length);

                        fs.Flush();
                    }
                }
                catch (Exception) { isSuc = false; }
            }

            Console.WriteLine($"保存（{isSuc}）");

            return isSuc;
        }

        public static TrainingData Load(string fileName)
        {

            TrainingData data = new TrainingData();

            byte[] buffer = new byte[10240];

            if (File.Exists(fileName))
            {

                FileStream fs = File.OpenRead(fileName);
                long length = fs.Length;
                buffer = new byte[length];
                fs.Read(buffer, 0, buffer.Length);
            }

            using (MemoryStream ms =new MemoryStream(buffer)) {

                byte[] bytes = new byte[sizeof(int)];

                ms.Read(bytes, 0, bytes.Length);
                data.MagicNumber = BitConverter.ToInt32(bytes,0);

                data.LayerNumber = (byte)ms.ReadByte();

                int size = data.LayerNumber << 1;

                data.Rows = new int[size];
                data.Columns = new int[size];

                for (int i = 0; i < size; i++)
                {
                    ms.Read(bytes, 0, bytes.Length);

                    data.Rows[i] = BitConverter.ToInt32(bytes, 0);

                    ms.Read(bytes, 0, bytes.Length);

                    data.Columns[i] = BitConverter.ToInt32(bytes, 0);
                }

                bytes = new byte[sizeof(double)];

                data.Content = new List<Matrix>();

                for (int i = 0; i < size; i++)
                {
                    int row = data.Rows[i];
                    int col = data.Columns[i];
                    Matrix matrix = new Matrix(row,col);
                    for (int r = 0; r < row; r++)
                    {
                        for (int c = 0; c < col; c++)
                        {
                            ms.Read(bytes, 0, bytes.Length);
                            matrix[r, c] = BitConverter.ToDouble(bytes,0);
                        }
                    }
                    data.Content.Add(matrix);
                }
            }
            return data;

        }

    }



}
