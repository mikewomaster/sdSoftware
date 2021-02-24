namespace HslCommunication.Algorithms.Fourier
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FFTFilter
    {
        public static List<T> FillDataArray<T>(List<T> source, out int putLength)
        {
            int num = ((int) Math.Pow(2.0, (double) Convert.ToString(source.Count, 2).Length)) - source.Count;
            num = (num / 2) + 1;
            putLength = num;
            T item = source[0];
            T local2 = source[source.Count - 1];
            for (int i = 0; i < num; i++)
            {
                source.Insert(0, item);
            }
            for (int j = 0; j < num; j++)
            {
                source.Add(local2);
            }
            return source;
        }

        private static float[] Filter(double[] source, double filter)
        {
            if (filter > 1.0)
            {
                filter = 1.0;
            }
            if (filter < 0.0)
            {
                filter = 0.0;
            }
            double[] xreal = new double[source.Length];
            double[] ximag = new double[source.Length];
            List<double> list = new List<double>();
            for (int i = 0; i < source.Length; i++)
            {
                xreal[i] = source[i];
                ximag[i] = 0.0;
            }
            double[] numArray3 = FFTHelper.FFTValue(xreal, ximag, false);
            int length = numArray3.Length;
            double num2 = numArray3.Max();
            for (int j = 0; j < numArray3.Length; j++)
            {
                if (numArray3[j] < (num2 * filter))
                {
                    xreal[j] = 0.0;
                    ximag[j] = 0.0;
                }
            }
            length = FFTHelper.IFFT(xreal, ximag);
            for (int k = 0; k < length; k++)
            {
                list.Add(Math.Sqrt((xreal[k] * xreal[k]) + (ximag[k] * ximag[k])));
            }
            return (from m in list select (float) m).ToArray<float>();
        }

        private static float[] Filter(float[] source, double filter)
        {
            return Filter((from m in source select (double) m).ToArray<double>(), filter);
        }

        public static double[] FilterFFT(double[] source, double filter)
        {
            int num;
            double[] numArray = new double[source.Length];
            float[] numArray2 = Filter(FillDataArray<double>(new List<double>(source), out num).ToArray(), filter);
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = numArray2[i + num];
            }
            return numArray;
        }

        public static float[] FilterFFT(float[] source, double filter)
        {
            int num;
            float[] numArray = new float[source.Length];
            float[] numArray2 = Filter(FillDataArray<float>(new List<float>(source), out num).ToArray(), filter);
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = numArray2[i + num];
            }
            return numArray;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FFTFilter.<>c <>9 = new FFTFilter.<>c();
            public static Func<float, double> <>9__3_0;
            public static Func<double, float> <>9__4_0;

            internal double <Filter>b__3_0(float m)
            {
                return (double) m;
            }

            internal float <Filter>b__4_0(double m)
            {
                return (float) m;
            }
        }
    }
}

