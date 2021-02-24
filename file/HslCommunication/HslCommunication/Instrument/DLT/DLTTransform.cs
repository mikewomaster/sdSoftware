namespace HslCommunication.Instrument.DLT
{
    using HslCommunication;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DLTTransform
    {
        public static OperateResult<double[]> TransDoubleFromDLt(byte[] content, ushort length, [Optional, DefaultParameterValue("XXXXXX.XX")] string format)
        {
            try
            {
                format = format.ToUpper();
                int num = Enumerable.Count<char>(format, (Func<char, bool>) (m => (m == 'X'))) / 2;
                int num2 = (format.IndexOf('.') >= 0) ? ((format.Length - format.IndexOf('.')) - 1) : 0;
                double[] numArray = new double[length];
                for (int i = 0; i < numArray.Length; i++)
                {
                    byte[] inBytes = content.SelectMiddle<byte>((i * num), num).Reverse<byte>().ToArray<byte>();
                    for (int j = 0; j < inBytes.Length; j++)
                    {
                        inBytes[j] = (byte) (inBytes[j] - 0x33);
                    }
                    numArray[i] = Convert.ToDouble(inBytes.ToHexString()) / Math.Pow(10.0, (double) num2);
                }
                return OperateResult.CreateSuccessResult<double[]>(numArray);
            }
            catch (Exception exception)
            {
                return new OperateResult<double[]>(exception.Message);
            }
        }

        public static OperateResult<string> TransStringFromDLt(byte[] content, ushort length)
        {
            try
            {
                byte[] bytes = content.SelectBegin<byte>(length).Reverse<byte>().ToArray<byte>();
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = (byte) (bytes[i] - 0x33);
                }
                return OperateResult.CreateSuccessResult<string>(Encoding.ASCII.GetString(bytes));
            }
            catch (Exception exception)
            {
                return new OperateResult<string>(exception.Message + " Reason: " + content.ToHexString(' '));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DLTTransform.<>c <>9 = new DLTTransform.<>c();
            public static Func<char, bool> <>9__1_0;

            internal bool <TransDoubleFromDLt>b__1_0(char m)
            {
                return (m == 'X');
            }
        }
    }
}

