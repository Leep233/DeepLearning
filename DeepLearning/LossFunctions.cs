﻿using DeepLearning.Math;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeepLearning
{
   public class LossFunctions
    {

        private const double DELTA = 1E-7;
        public  static double  MeanSquaredError(Matrix y,Matrix t) {

            Matrix matrix = y - t;

            double sum = 0;

            int row = matrix.Row;
            int col = matrix.Column;

            Mutex mutex = new Mutex();

            Parallel.For(0, row, i => {
                for (int j = 0; j < matrix.Column; j++)
                {
                    mutex.WaitOne();
                    sum += System.Math.Pow(matrix[i, j], 2);
                    mutex.ReleaseMutex();
                }
            });


        
            return sum * 0.5;

        }



        public static double CrossEntropyError(Matrix y, Matrix t) {


            if (y.Row == t.Row || y.Column == t.Column) {
                //one-hot形式 0 1 0 0 0 0 0
                int row = t.Row;

                int col = t.Column;

                Matrix indexs = new Matrix(row, 1);
         
                Parallel.For(0, row, i=> {
                    for (int j = 0; j < col; j++)
                    {
                        if (t[i, j] == 1)
                        {
                            indexs[i, 0] = j;
                            break;
                        }
                    }
                });
                 t = indexs;
            }

            double loss = 0;

            int y_row = y.Row;

            int t_col = t.Column;

            using (Mutex mutex = new Mutex()) {
                Parallel.For(0, y_row, i => {
                    for (int j = 0; j < t_col; j++)
                    {
                        double temp = (int)t[i, j];

                        temp = y[i, (int)t[i, j]];

                        temp = System.Math.Log(temp + DELTA);

                        mutex.WaitOne();
                        loss += temp;
                        mutex.ReleaseMutex();
                    }

                });

            }
       

            return -loss/t.Row;

        }


    }
}
