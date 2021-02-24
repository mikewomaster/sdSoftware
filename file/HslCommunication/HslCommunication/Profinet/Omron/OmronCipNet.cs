namespace HslCommunication.Profinet.Omron
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Profinet.AllenBradley;
    using HslCommunication.Reflection;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class OmronCipNet : AllenBradleyNet
    {
        public OmronCipNet()
        {
        }

        public OmronCipNet(string ipAddress, [Optional, DefaultParameterValue(0xaf12)] int port) : base(ipAddress, port)
        {
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            if (length > 1)
            {
                string[] textArray1 = new string[] { address };
                int[] numArray1 = new int[] { 1 };
                return base.Read(textArray1, numArray1);
            }
            string[] textArray2 = new string[] { address };
            int[] numArray2 = new int[] { length };
            return base.Read(textArray2, numArray2);
        }

        [HslMqttApi("ReadDoubleArray", "")]
        public override OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            if (length == 1)
            {
                return ByteTransformHelper.GetResultFromBytes<double[]>(this.Read(address, 1), m => this.ByteTransform.TransDouble(m, 0, length));
            }
            int startIndex = HslHelper.ExtractStartIndex(ref address);
            return ByteTransformHelper.GetResultFromBytes<double[]>(this.Read(address, 1), m => this.ByteTransform.TransDouble(m, (startIndex < 0) ? 0 : (startIndex * 8), length));
        }

        [HslMqttApi("ReadFloatArray", "")]
        public override OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            if (length == 1)
            {
                return ByteTransformHelper.GetResultFromBytes<float[]>(this.Read(address, 1), m => this.ByteTransform.TransSingle(m, 0, length));
            }
            int startIndex = HslHelper.ExtractStartIndex(ref address);
            return ByteTransformHelper.GetResultFromBytes<float[]>(this.Read(address, 1), m => this.ByteTransform.TransSingle(m, (startIndex < 0) ? 0 : (startIndex * 4), length));
        }

        [HslMqttApi("ReadInt16Array", "")]
        public override OperateResult<short[]> ReadInt16(string address, ushort length)
        {
            if (length == 1)
            {
                return ByteTransformHelper.GetResultFromBytes<short[]>(this.Read(address, 1), m => this.ByteTransform.TransInt16(m, 0, length));
            }
            int startIndex = HslHelper.ExtractStartIndex(ref address);
            return ByteTransformHelper.GetResultFromBytes<short[]>(this.Read(address, 1), m => this.ByteTransform.TransInt16(m, (startIndex < 0) ? 0 : (startIndex * 2), length));
        }

        [HslMqttApi("ReadInt32Array", "")]
        public override OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            if (length == 1)
            {
                return ByteTransformHelper.GetResultFromBytes<int[]>(this.Read(address, 1), m => this.ByteTransform.TransInt32(m, 0, length));
            }
            int startIndex = HslHelper.ExtractStartIndex(ref address);
            return ByteTransformHelper.GetResultFromBytes<int[]>(this.Read(address, 1), m => this.ByteTransform.TransInt32(m, (startIndex < 0) ? 0 : (startIndex * 4), length));
        }

        [HslMqttApi("ReadInt64Array", "")]
        public override OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            if (length == 1)
            {
                return ByteTransformHelper.GetResultFromBytes<long[]>(this.Read(address, 1), m => this.ByteTransform.TransInt64(m, 0, length));
            }
            int startIndex = HslHelper.ExtractStartIndex(ref address);
            return ByteTransformHelper.GetResultFromBytes<long[]>(this.Read(address, 1), m => this.ByteTransform.TransInt64(m, (startIndex < 0) ? 0 : (startIndex * 8), length));
        }

        public override OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            OperateResult<byte[]> result = this.Read(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            int count = base.ByteTransform.TransUInt16(result.Content, 0);
            return OperateResult.CreateSuccessResult<string>(encoding.GetString(result.Content, 2, count));
        }

        [HslMqttApi("ReadUInt16Array", "")]
        public override OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        {
            if (length == 1)
            {
                return ByteTransformHelper.GetResultFromBytes<ushort[]>(this.Read(address, 1), m => this.ByteTransform.TransUInt16(m, 0, length));
            }
            int startIndex = HslHelper.ExtractStartIndex(ref address);
            return ByteTransformHelper.GetResultFromBytes<ushort[]>(this.Read(address, 1), m => this.ByteTransform.TransUInt16(m, (startIndex < 0) ? 0 : (startIndex * 2), length));
        }

        [HslMqttApi("ReadUInt32Array", "")]
        public override OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            if (length == 1)
            {
                return ByteTransformHelper.GetResultFromBytes<uint[]>(this.Read(address, 1), m => this.ByteTransform.TransUInt32(m, 0, length));
            }
            int startIndex = HslHelper.ExtractStartIndex(ref address);
            return ByteTransformHelper.GetResultFromBytes<uint[]>(this.Read(address, 1), m => this.ByteTransform.TransUInt32(m, (startIndex < 0) ? 0 : (startIndex * 4), length));
        }

        [HslMqttApi("ReadUInt64Array", "")]
        public override OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            if (length == 1)
            {
                return ByteTransformHelper.GetResultFromBytes<ulong[]>(this.Read(address, 1), m => this.ByteTransform.TransUInt64(m, 0, length));
            }
            int startIndex = HslHelper.ExtractStartIndex(ref address);
            return ByteTransformHelper.GetResultFromBytes<ulong[]>(this.Read(address, 1), m => this.ByteTransform.TransUInt64(m, (startIndex < 0) ? 0 : (startIndex * 8), length));
        }

        public override string ToString()
        {
            return string.Format("OmronCipNet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteByte", "")]
        public override OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[2];
            buffer1[0] = value;
            return this.WriteTag(address, 0xd1, buffer1, 1);
        }

        [HslMqttApi("WriteString", "")]
        public override OperateResult Write(string address, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;
            }
            byte[][] bytes = new byte[][] { new byte[2], SoftBasic.ArrayExpandToLengthEven<byte>(Encoding.ASCII.GetBytes(value)) };
            byte[] buffer = SoftBasic.SpliceByteArray(bytes);
            buffer[0] = BitConverter.GetBytes((int) (buffer.Length - 2))[0];
            buffer[1] = BitConverter.GetBytes((int) (buffer.Length - 2))[1];
            return base.WriteTag(address, 0xd0, buffer, 1);
        }

        public override OperateResult WriteTag(string address, ushort typeCode, byte[] value, [Optional, DefaultParameterValue(1)] int length)
        {
            return base.WriteTag(address, typeCode, value, 1);
        }
    }
}

