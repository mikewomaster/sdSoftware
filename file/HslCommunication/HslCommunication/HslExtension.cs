namespace HslCommunication
{
    using HslCommunication.BasicFramework;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class HslExtension
    {
        public static OperateResult BeginReceiveResult(this Socket socket, AsyncCallback callback)
        {
            return socket.BeginReceiveResult(callback, socket);
        }

        public static OperateResult BeginReceiveResult(this Socket socket, AsyncCallback callback, object obj)
        {
            try
            {
                socket.BeginReceive(new byte[0], 0, 0, SocketFlags.None, callback, obj);
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception exception)
            {
                if (socket != null)
                {
                    socket.Close();
                }
                return new OperateResult(exception.Message);
            }
        }

        public static T[] CopyArray<T>(this T[] value)
        {
            if (value == null)
            {
                return null;
            }
            T[] destinationArray = new T[value.Length];
            Array.Copy(value, destinationArray, value.Length);
            return destinationArray;
        }

        public static OperateResult<int> EndReceiveResult(this Socket socket, IAsyncResult ar)
        {
            try
            {
                return OperateResult.CreateSuccessResult<int>(socket.EndReceive(ar));
            }
            catch (Exception exception)
            {
                if (socket != null)
                {
                    socket.Close();
                }
                return new OperateResult<int>(exception.Message);
            }
        }

        public static bool GetBoolOnIndex(this byte value, int offset)
        {
            return SoftBasic.BoolOnByteIndex(value, offset);
        }

        public static T[] IncreaseBy<T>(this T[] array, T value)
        {
            ParameterExpression expression;
            ParameterExpression expression2;
            ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2 };
            Func<T, T, T> func = Expression.Lambda<Func<T, T, T>>(Expression.Add(expression = Expression.Parameter(typeof(T), "first"), expression2 = Expression.Parameter(typeof(T), "second")), parameters).Compile();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = func(array[i], value);
            }
            return array;
        }

        public static T[] RemoveBegin<T>(this T[] value, int length)
        {
            return SoftBasic.ArrayRemoveBegin<T>(value, length);
        }

        public static T[] RemoveDouble<T>(this T[] value, int leftLength, int rightLength)
        {
            return SoftBasic.ArrayRemoveDouble<T>(value, leftLength, rightLength);
        }

        public static T[] RemoveLast<T>(this T[] value, int length)
        {
            return SoftBasic.ArrayRemoveLast<T>(value, length);
        }

        public static T[] SelectBegin<T>(this T[] value, int length)
        {
            return SoftBasic.ArraySelectBegin<T>(value, length);
        }

        public static T[] SelectLast<T>(this T[] value, int length)
        {
            return SoftBasic.ArraySelectLast<T>(value, length);
        }

        public static T[] SelectMiddle<T>(this T[] value, int index, int length)
        {
            return SoftBasic.ArraySelectMiddle<T>(value, index, length);
        }

        public static string ToArrayString<T>(this T[] value)
        {
            return SoftBasic.ArrayFormat<T>(value);
        }

        public static string ToArrayString<T>(this T[] value, string format)
        {
            return SoftBasic.ArrayFormat<T>(value, format);
        }

        public static bool[] ToBoolArray(this byte[] InBytes)
        {
            return SoftBasic.ByteToBoolArray(InBytes);
        }

        public static bool[] ToBoolArray(this byte[] InBytes, int length)
        {
            return SoftBasic.ByteToBoolArray(InBytes, length);
        }

        public static byte[] ToByteArray(this bool[] array)
        {
            return SoftBasic.BoolArrayToByte(array);
        }

        public static byte[] ToHexBytes(this string value)
        {
            return SoftBasic.HexStringToBytes(value);
        }

        public static string ToHexString(this byte[] InBytes)
        {
            return SoftBasic.ByteToHexString(InBytes);
        }

        public static string ToHexString(this byte[] InBytes, char segment)
        {
            return SoftBasic.ByteToHexString(InBytes, segment);
        }

        public static string ToHexString(this byte[] InBytes, char segment, int newLineCount)
        {
            return SoftBasic.ByteToHexString(InBytes, segment, newLineCount);
        }

        public static string ToJsonString(this object obj, [Optional, DefaultParameterValue(1)] Formatting formatting)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        public static T[] ToStringArray<T>(this string value)
        {
            Type type = typeof(T);
            if (type == typeof(byte))
            {
                return value.ToStringArray<byte>(new Func<string, byte>(byte.Parse));
            }
            if (type == typeof(sbyte))
            {
                return value.ToStringArray<sbyte>(new Func<string, sbyte>(sbyte.Parse));
            }
            if (type == typeof(bool))
            {
                return value.ToStringArray<bool>(new Func<string, bool>(bool.Parse));
            }
            if (type == typeof(short))
            {
                return value.ToStringArray<short>(new Func<string, short>(short.Parse));
            }
            if (type == typeof(ushort))
            {
                return value.ToStringArray<ushort>(new Func<string, ushort>(ushort.Parse));
            }
            if (type == typeof(int))
            {
                return value.ToStringArray<int>(new Func<string, int>(int.Parse));
            }
            if (type == typeof(uint))
            {
                return value.ToStringArray<uint>(new Func<string, uint>(uint.Parse));
            }
            if (type == typeof(long))
            {
                return value.ToStringArray<long>(new Func<string, long>(long.Parse));
            }
            if (type == typeof(ulong))
            {
                return value.ToStringArray<ulong>(new Func<string, ulong>(ulong.Parse));
            }
            if (type == typeof(float))
            {
                return value.ToStringArray<float>(new Func<string, float>(float.Parse));
            }
            if (type == typeof(double))
            {
                return value.ToStringArray<double>(new Func<string, double>(double.Parse));
            }
            if (type == typeof(DateTime))
            {
                return value.ToStringArray<DateTime>(new Func<string, DateTime>(DateTime.Parse));
            }
            if (type != typeof(string))
            {
                throw new Exception("use ToArray<T>(Func<string,T>) method instead");
            }
            return value.ToStringArray<string>(m => m);
        }

        public static T[] ToStringArray<T>(this string value, Func<string, T> selector)
        {
            if (value.IndexOf('[') >= 0)
            {
                value = value.Replace("[", "");
            }
            if (value.IndexOf(']') >= 0)
            {
                value = value.Replace("]", "");
            }
            char[] separator = new char[] { ',', ';' };
            return Enumerable.Select<string, T>(value.Split(separator, StringSplitOptions.RemoveEmptyEntries), selector).ToArray<T>();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__19<T>
        {
            public static readonly HslExtension.<>c__19<T> <>9;
            public static Func<string, string> <>9__19_0;

            static <>c__19()
            {
                HslExtension.<>c__19<T>.<>9 = new HslExtension.<>c__19<T>();
            }

            internal string <ToStringArray>b__19_0(string m)
            {
                return m;
            }
        }
    }
}

