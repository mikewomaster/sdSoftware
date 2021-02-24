namespace HslCommunication.Algorithms.Fourier
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FFTHelper
    {
        private static void bitrp(double[] xreal, double[] ximag, int n)
        {
            int index = 1;
            int num5 = 0;
            while (index < n)
            {
                num5++;
                index *= 2;
            }
            for (index = 0; index < n; index++)
            {
                int num3 = index;
                int num4 = 0;
                for (int i = 0; i < num5; i++)
                {
                    num4 = (num4 * 2) + (num3 % 2);
                    num3 /= 2;
                }
                if (num4 > index)
                {
                    double num6 = xreal[index];
                    xreal[index] = xreal[num4];
                    xreal[num4] = num6;
                    num6 = ximag[index];
                    ximag[index] = ximag[num4];
                    ximag[num4] = num6;
                }
            }
        }

        public static double[] FFT(double[] xreal)
        {
            return FFTValue(xreal, new double[xreal.Length], false);
        }

        public static int FFT(double[] xreal, double[] ximag)
        {
            return FFTValue(xreal, ximag, false).Length;
        }

        public static int FFT(float[] xreal, float[] ximag)
        {
            return FFT((from m in xreal select (double) m).ToArray<double>(), (from m in ximag select (double) m).ToArray<double>());
        }

        public static double[] FFTValue(double[] xreal, double[] ximag, [Optional, DefaultParameterValue(false)] bool isSqrtDouble)
        {
            int n = 2;
            while (n <= xreal.Length)
            {
                n *= 2;
            }
            n /= 2;
            double[] numArray = new double[n / 2];
            double[] numArray2 = new double[n / 2];
            bitrp(xreal, ximag, n);
            double d = -6.2831853071795862 / ((double) n);
            double num2 = Math.Cos(d);
            double num3 = Math.Sin(d);
            numArray[0] = 1.0;
            numArray2[0] = 0.0;
            int index = 1;
            while (index < (n / 2))
            {
                numArray[index] = (numArray[index - 1] * num2) - (numArray2[index - 1] * num3);
                numArray2[index] = (numArray[index - 1] * num3) + (numArray2[index - 1] * num2);
                index++;
            }
            for (int i = 2; i <= n; i *= 2)
            {
                for (int k = 0; k < n; k += i)
                {
                    for (index = 0; index < (i / 2); index++)
                    {
                        int num11 = k + index;
                        int num12 = num11 + (i / 2);
                        int num10 = (n * index) / i;
                        num2 = (numArray[num10] * xreal[num12]) - (numArray2[num10] * ximag[num12]);
                        num3 = (numArray[num10] * ximag[num12]) + (numArray2[num10] * xreal[num12]);
                        double num4 = xreal[num11];
                        double num5 = ximag[num11];
                        xreal[num11] = num4 + num2;
                        ximag[num11] = num5 + num3;
                        xreal[num12] = num4 - num2;
                        ximag[num12] = num5 - num3;
                    }
                }
            }
            double[] numArray3 = new double[n];
            for (int j = 0; j < numArray3.Length; j++)
            {
                numArray3[j] = Math.Sqrt(Math.Pow(xreal[j], 2.0) + Math.Pow(ximag[j], 2.0));
                if (isSqrtDouble)
                {
                    numArray3[j] = Math.Sqrt(numArray3[j]);
                }
            }
            return numArray3;
        }

        public static Bitmap GetFFTImage(double[] xreal, int width, int heigh, Color lineColor, [Optional, DefaultParameterValue(false)] bool isSqrtDouble)
        {
            double[] ximag = new double[xreal.Length];
            double[] source = FFTValue(xreal, ximag, isSqrtDouble);
            Bitmap image = new Bitmap(width, heigh);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Color.White);
            Pen pen = new Pen(Color.DimGray, 1f);
            Pen pen2 = new Pen(Color.LightGray, 1f);
            Pen pen3 = new Pen(lineColor, 1f);
            pen2.DashPattern = new float[] { 5f, 5f };
            pen2.DashStyle = DashStyle.Custom;
            Font defaultFont = SystemFonts.DefaultFont;
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };
            StringFormat format2 = new StringFormat {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            int num = 20;
            int num2 = 0x31;
            int num3 = 0x31;
            int num4 = 30;
            int num5 = 9;
            float num6 = (heigh - num) - num4;
            float num7 = (width - num2) - num3;
            if (source.Length > 1)
            {
                double num8 = source.Max();
                double num9 = source.Min();
                num8 = ((num8 - num9) > 1.0) ? num8 : (num9 + 1.0);
                double num10 = num8 - num9;
                List<float> list = new List<float>();
                if (source.Length >= 2)
                {
                    if (source[0] > source[1])
                    {
                        list.Add(0f);
                    }
                    for (int m = 1; m < (source.Length - 2); m++)
                    {
                        if ((source[m - 1] < source[m]) && (source[m] > source[m + 1]))
                        {
                            list.Add((float) m);
                        }
                    }
                    if (source[source.Length - 1] > source[source.Length - 2])
                    {
                        list.Add((float) (source.Length - 1));
                    }
                }
                for (int i = 0; i < num5; i++)
                {
                    RectangleF layoutRectangle = new RectangleF(-10f, (((float) i) / ((float) (num5 - 1))) * num6, num2 + 8f, 20f);
                    graphics.DrawString((((((num5 - 1) - i) * num10) / ((double) (num5 - 1))) + num9).ToString("F1"), defaultFont, Brushes.Black, layoutRectangle, format);
                    graphics.DrawLine(pen2, (float) (num2 - 3), ((num6 * i) / ((float) (num5 - 1))) + num, (float) (width - num3), ((num6 * i) / ((float) (num5 - 1))) + num);
                }
                float num11 = num7 / ((float) source.Length);
                for (int j = 0; j < list.Count; j++)
                {
                    if (((source[(int) list[j]] * 200.0) / num8) > 1.0)
                    {
                        graphics.DrawLine(pen2, ((list[j] * num11) + num2) + 1f, (float) num, ((list[j] * num11) + num2) + 1f, (float) (heigh - num4));
                        RectangleF ef2 = new RectangleF((((list[j] * num11) + num2) + 1f) - 40f, (float) ((heigh - num4) + 1), 80f, 20f);
                        graphics.DrawString(list[j].ToString(), defaultFont, Brushes.DeepPink, ef2, format2);
                    }
                }
                for (int k = 0; k < source.Length; k++)
                {
                    PointF tf = new PointF {
                        X = ((k * num11) + num2) + 1f,
                        Y = (float) ((num6 - (((source[k] - num9) * num6) / num10)) + num)
                    };
                    PointF tf2 = new PointF {
                        X = ((k * num11) + num2) + 1f,
                        Y = (float) ((num6 - (((num9 - num9) * num6) / num10)) + num)
                    };
                    graphics.DrawLine(Pens.Tomato, tf, tf2);
                }
            }
            else
            {
                double num18 = 100.0;
                double num19 = 0.0;
                double num20 = num18 - num19;
                for (int n = 0; n < num5; n++)
                {
                    RectangleF ef3 = new RectangleF(-10f, (((float) n) / ((float) (num5 - 1))) * num6, num2 + 8f, 20f);
                    graphics.DrawString((((((num5 - 1) - n) * num20) / ((double) (num5 - 1))) + num19).ToString("F1"), defaultFont, Brushes.Black, ef3, format);
                    graphics.DrawLine(pen2, (float) (num2 - 3), ((num6 * n) / ((float) (num5 - 1))) + num, (float) (width - num3), ((num6 * n) / ((float) (num5 - 1))) + num);
                }
            }
            pen2.Dispose();
            pen.Dispose();
            pen3.Dispose();
            defaultFont.Dispose();
            format.Dispose();
            format2.Dispose();
            graphics.Dispose();
            return image;
        }

        public static int IFFT(double[] xreal, double[] ximag)
        {
            int n = 2;
            while (n <= xreal.Length)
            {
                n *= 2;
            }
            n /= 2;
            double[] numArray = new double[n / 2];
            double[] numArray2 = new double[n / 2];
            bitrp(xreal, ximag, n);
            double d = 6.2831853071795862 / ((double) n);
            double num2 = Math.Cos(d);
            double num3 = Math.Sin(d);
            numArray[0] = 1.0;
            numArray2[0] = 0.0;
            int index = 1;
            while (index < (n / 2))
            {
                numArray[index] = (numArray[index - 1] * num2) - (numArray2[index - 1] * num3);
                numArray2[index] = (numArray[index - 1] * num3) + (numArray2[index - 1] * num2);
                index++;
            }
            for (int i = 2; i <= n; i *= 2)
            {
                for (int j = 0; j < n; j += i)
                {
                    index = 0;
                    while (index < (i / 2))
                    {
                        int num11 = j + index;
                        int num12 = num11 + (i / 2);
                        int num10 = (n * index) / i;
                        num2 = (numArray[num10] * xreal[num12]) - (numArray2[num10] * ximag[num12]);
                        num3 = (numArray[num10] * ximag[num12]) + (numArray2[num10] * xreal[num12]);
                        double num4 = xreal[num11];
                        double num5 = ximag[num11];
                        xreal[num11] = num4 + num2;
                        ximag[num11] = num5 + num3;
                        xreal[num12] = num4 - num2;
                        ximag[num12] = num5 - num3;
                        index++;
                    }
                }
            }
            for (index = 0; index < n; index++)
            {
                xreal[index] /= (double) n;
                ximag[index] /= (double) n;
            }
            return n;
        }

        public static int IFFT(float[] xreal, float[] ximag)
        {
            return IFFT((from m in xreal select (double) m).ToArray<double>(), (from m in ximag select (double) m).ToArray<double>());
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FFTHelper.<>c <>9 = new FFTHelper.<>c();
            public static Func<float, double> <>9__5_0;
            public static Func<float, double> <>9__5_1;
            public static Func<float, double> <>9__6_0;
            public static Func<float, double> <>9__6_1;

            internal double <FFT>b__5_0(float m)
            {
                return (double) m;
            }

            internal double <FFT>b__5_1(float m)
            {
                return (double) m;
            }

            internal double <IFFT>b__6_0(float m)
            {
                return (double) m;
            }

            internal double <IFFT>b__6_1(float m)
            {
                return (double) m;
            }
        }
    }
}

