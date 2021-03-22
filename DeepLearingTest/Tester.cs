using DeepLearning.Math;
using MNIST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeepLearingTest
{
  public abstract class Tester<T> where T : Tester<T>,new()
    {

        static T _instance = default(T);
        public static T Inistance { get {

                if (_instance == null)
                    _instance = new T();
                return _instance;
            } }

      


    }
}
