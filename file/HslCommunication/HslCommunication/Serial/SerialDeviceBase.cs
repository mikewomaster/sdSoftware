namespace HslCommunication.Serial
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SerialDeviceBase : SerialBase, IReadWriteNet
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <WordLength>k__BackingField = 1;
        private IByteTransform byteTransform;
        private string connectionId = string.Empty;

        public SerialDeviceBase()
        {
            this.connectionId = SoftBasic.GetUniqueStringByGuidAndRandom();
        }

        public virtual OperateResult<T> Read<T>() where T: class, new()
        {
            return HslReflectionHelper.Read<T>(this);
        }

        [HslMqttApi("ReadByteArray", "")]
        public virtual OperateResult<byte[]> Read(string address, ushort length)
        {
            return new OperateResult<byte[]>(StringResources.Language.NotSupportedFunction);
        }

        [HslMqttApi("ReadBool", "")]
        public virtual OperateResult<bool> ReadBool(string address)
        {
            return ByteTransformHelper.GetResultFromArray<bool>(this.ReadBool(address, 1));
        }

        [HslMqttApi("ReadBoolArray", "")]
        public virtual OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            return new OperateResult<bool[]>(StringResources.Language.NotSupportedFunction);
        }

        public OperateResult<T> ReadCustomer<T>(string address) where T: IDataTransfer, new()
        {
            OperateResult<T> result = new OperateResult<T>();
            T local = Activator.CreateInstance<T>();
            OperateResult<byte[]> result2 = this.Read(address, local.ReadCount);
            if (result2.IsSuccess)
            {
                local.ParseSource(result2.Content);
                result.Content = local;
                result.IsSuccess = true;
                return result;
            }
            result.ErrorCode = result2.ErrorCode;
            result.Message = result2.Message;
            return result;
        }

        [HslMqttApi("ReadDouble", "")]
        public OperateResult<double> ReadDouble(string address)
        {
            return ByteTransformHelper.GetResultFromArray<double>(this.ReadDouble(address, 1));
        }

        [HslMqttApi("ReadDoubleArray", "")]
        public virtual OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<double[]>(this.Read(address, (ushort) ((length * this.WordLength) * 4)), m => this.ByteTransform.TransDouble(m, 0, length));
        }

        [HslMqttApi("ReadFloat", "")]
        public OperateResult<float> ReadFloat(string address)
        {
            return ByteTransformHelper.GetResultFromArray<float>(this.ReadFloat(address, 1));
        }

        [HslMqttApi("ReadFloatArray", "")]
        public virtual OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<float[]>(this.Read(address, (ushort) ((length * this.WordLength) * 2)), m => this.ByteTransform.TransSingle(m, 0, length));
        }

        [HslMqttApi("ReadInt16", "")]
        public OperateResult<short> ReadInt16(string address)
        {
            return ByteTransformHelper.GetResultFromArray<short>(this.ReadInt16(address, 1));
        }

        [HslMqttApi("ReadInt16Array", "")]
        public virtual OperateResult<short[]> ReadInt16(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<short[]>(this.Read(address, (ushort) (length * this.WordLength)), m => this.ByteTransform.TransInt16(m, 0, length));
        }

        [HslMqttApi("ReadInt32", "")]
        public OperateResult<int> ReadInt32(string address)
        {
            return ByteTransformHelper.GetResultFromArray<int>(this.ReadInt32(address, 1));
        }

        [HslMqttApi("ReadInt32Array", "")]
        public virtual OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<int[]>(this.Read(address, (ushort) ((length * this.WordLength) * 2)), m => this.ByteTransform.TransInt32(m, 0, length));
        }

        [HslMqttApi("ReadInt64", "")]
        public OperateResult<long> ReadInt64(string address)
        {
            return ByteTransformHelper.GetResultFromArray<long>(this.ReadInt64(address, 1));
        }

        [HslMqttApi("ReadInt64Array", "")]
        public virtual OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<long[]>(this.Read(address, (ushort) ((length * this.WordLength) * 4)), m => this.ByteTransform.TransInt64(m, 0, length));
        }

        [HslMqttApi("ReadString", "")]
        public OperateResult<string> ReadString(string address, ushort length)
        {
            return this.ReadString(address, length, Encoding.ASCII);
        }

        public virtual OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            return ByteTransformHelper.GetResultFromBytes<string>(this.Read(address, length), m => this.ByteTransform.TransString(m, 0, m.Length, encoding));
        }

        [HslMqttApi("ReadUInt16", "")]
        public OperateResult<ushort> ReadUInt16(string address)
        {
            return ByteTransformHelper.GetResultFromArray<ushort>(this.ReadUInt16(address, 1));
        }

        [HslMqttApi("ReadUInt16Array", "")]
        public virtual OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<ushort[]>(this.Read(address, (ushort) (length * this.WordLength)), m => this.ByteTransform.TransUInt16(m, 0, length));
        }

        [HslMqttApi("ReadUInt32", "")]
        public OperateResult<uint> ReadUInt32(string address)
        {
            return ByteTransformHelper.GetResultFromArray<uint>(this.ReadUInt32(address, 1));
        }

        [HslMqttApi("ReadUInt32Array", "")]
        public virtual OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<uint[]>(this.Read(address, (ushort) ((length * this.WordLength) * 2)), m => this.ByteTransform.TransUInt32(m, 0, length));
        }

        [HslMqttApi("ReadUInt64", "")]
        public OperateResult<ulong> ReadUInt64(string address)
        {
            return ByteTransformHelper.GetResultFromArray<ulong>(this.ReadUInt64(address, 1));
        }

        [HslMqttApi("ReadUInt64Array", "")]
        public virtual OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<ulong[]>(this.Read(address, (ushort) ((length * this.WordLength) * 4)), m => this.ByteTransform.TransUInt64(m, 0, length));
        }

        public override string ToString()
        {
            return string.Format("SerialDeviceBase<{0}>", this.byteTransform.GetType());
        }

        [HslMqttApi("WaitBool", "")]
        public OperateResult<TimeSpan> Wait(string address, bool waitValue, [Optional, DefaultParameterValue(100)] int readInterval, [Optional, DefaultParameterValue(-1)] int waitTimeout)
        {
            return ReadWriteNetHelper.Wait(this, address, waitValue, readInterval, waitTimeout);
        }

        [HslMqttApi("WaitInt16", "")]
        public OperateResult<TimeSpan> Wait(string address, short waitValue, [Optional, DefaultParameterValue(100)] int readInterval, [Optional, DefaultParameterValue(-1)] int waitTimeout)
        {
            return ReadWriteNetHelper.Wait(this, address, waitValue, readInterval, waitTimeout);
        }

        [HslMqttApi("WaitInt32", "")]
        public OperateResult<TimeSpan> Wait(string address, int waitValue, [Optional, DefaultParameterValue(100)] int readInterval, [Optional, DefaultParameterValue(-1)] int waitTimeout)
        {
            return ReadWriteNetHelper.Wait(this, address, waitValue, readInterval, waitTimeout);
        }

        [HslMqttApi("WaitInt64", "")]
        public OperateResult<TimeSpan> Wait(string address, long waitValue, [Optional, DefaultParameterValue(100)] int readInterval, [Optional, DefaultParameterValue(-1)] int waitTimeout)
        {
            return ReadWriteNetHelper.Wait(this, address, waitValue, readInterval, waitTimeout);
        }

        [HslMqttApi("WaitUInt16", "")]
        public OperateResult<TimeSpan> Wait(string address, ushort waitValue, [Optional, DefaultParameterValue(100)] int readInterval, [Optional, DefaultParameterValue(-1)] int waitTimeout)
        {
            return ReadWriteNetHelper.Wait(this, address, waitValue, readInterval, waitTimeout);
        }

        [HslMqttApi("WaitUInt32", "")]
        public OperateResult<TimeSpan> Wait(string address, uint waitValue, [Optional, DefaultParameterValue(100)] int readInterval, [Optional, DefaultParameterValue(-1)] int waitTimeout)
        {
            return ReadWriteNetHelper.Wait(this, address, waitValue, readInterval, waitTimeout);
        }

        [HslMqttApi("WaitUInt64", "")]
        public OperateResult<TimeSpan> Wait(string address, ulong waitValue, [Optional, DefaultParameterValue(100)] int readInterval, [Optional, DefaultParameterValue(-1)] int waitTimeout)
        {
            return ReadWriteNetHelper.Wait(this, address, waitValue, readInterval, waitTimeout);
        }

        public virtual OperateResult Write<T>(T data) where T: class, new()
        {
            return HslReflectionHelper.Write<T>(data, this);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public virtual OperateResult Write(string address, bool[] value)
        {
            return new OperateResult(StringResources.Language.NotSupportedFunction);
        }

        [HslMqttApi("WriteByteArray", "")]
        public virtual OperateResult Write(string address, byte[] value)
        {
            return new OperateResult(StringResources.Language.NotSupportedFunction);
        }

        [HslMqttApi("WriteBool", "")]
        public virtual OperateResult Write(string address, bool value)
        {
            bool[] flagArray1 = new bool[] { value };
            return this.Write(address, flagArray1);
        }

        [HslMqttApi("WriteInt16", "")]
        public virtual OperateResult Write(string address, short value)
        {
            short[] values = new short[] { value };
            return this.Write(address, values);
        }

        [HslMqttApi("WriteDoubleArray", "")]
        public virtual OperateResult Write(string address, double[] values)
        {
            return this.Write(address, this.ByteTransform.TransByte(values));
        }

        [HslMqttApi("WriteInt16Array", "")]
        public virtual OperateResult Write(string address, short[] values)
        {
            return this.Write(address, this.ByteTransform.TransByte(values));
        }

        [HslMqttApi("WriteInt32Array", "")]
        public virtual OperateResult Write(string address, int[] values)
        {
            return this.Write(address, this.ByteTransform.TransByte(values));
        }

        [HslMqttApi("WriteInt64Array", "")]
        public virtual OperateResult Write(string address, long[] values)
        {
            return this.Write(address, this.ByteTransform.TransByte(values));
        }

        [HslMqttApi("WriteFloatArray", "")]
        public virtual OperateResult Write(string address, float[] values)
        {
            return this.Write(address, this.ByteTransform.TransByte(values));
        }

        [HslMqttApi("WriteUInt16Array", "")]
        public virtual OperateResult Write(string address, ushort[] values)
        {
            return this.Write(address, this.ByteTransform.TransByte(values));
        }

        [HslMqttApi("WriteDouble", "")]
        public OperateResult Write(string address, double value)
        {
            double[] values = new double[] { value };
            return this.Write(address, values);
        }

        [HslMqttApi("WriteInt32", "")]
        public OperateResult Write(string address, int value)
        {
            int[] values = new int[] { value };
            return this.Write(address, values);
        }

        [HslMqttApi("WriteInt64", "")]
        public OperateResult Write(string address, long value)
        {
            long[] values = new long[] { value };
            return this.Write(address, values);
        }

        [HslMqttApi("WriteFloat", "")]
        public OperateResult Write(string address, float value)
        {
            float[] values = new float[] { value };
            return this.Write(address, values);
        }

        [HslMqttApi("WriteString", "")]
        public virtual OperateResult Write(string address, string value)
        {
            return this.Write(address, value, Encoding.ASCII);
        }

        [HslMqttApi("WriteUInt16", "")]
        public virtual OperateResult Write(string address, ushort value)
        {
            ushort[] values = new ushort[] { value };
            return this.Write(address, values);
        }

        [HslMqttApi("WriteUInt32Array", "")]
        public virtual OperateResult Write(string address, uint[] values)
        {
            return this.Write(address, this.ByteTransform.TransByte(values));
        }

        [HslMqttApi("WriteUInt32", "")]
        public OperateResult Write(string address, uint value)
        {
            uint[] values = new uint[] { value };
            return this.Write(address, values);
        }

        [HslMqttApi("WriteUInt64Array", "")]
        public virtual OperateResult Write(string address, ulong[] values)
        {
            return this.Write(address, this.ByteTransform.TransByte(values));
        }

        [HslMqttApi("WriteUInt64", "")]
        public OperateResult Write(string address, ulong value)
        {
            ulong[] values = new ulong[] { value };
            return this.Write(address, values);
        }

        public virtual OperateResult Write(string address, string value, int length)
        {
            return this.Write(address, value, length, Encoding.ASCII);
        }

        public virtual OperateResult Write(string address, string value, Encoding encoding)
        {
            byte[] data = this.ByteTransform.TransByte(value, encoding);
            if (this.WordLength == 1)
            {
                data = SoftBasic.ArrayExpandToLengthEven<byte>(data);
            }
            return this.Write(address, data);
        }

        public virtual OperateResult Write(string address, string value, int length, Encoding encoding)
        {
            byte[] data = this.ByteTransform.TransByte(value, encoding);
            if (this.WordLength == 1)
            {
                data = SoftBasic.ArrayExpandToLengthEven<byte>(data);
            }
            data = SoftBasic.ArrayExpandToLength<byte>(data, length);
            return this.Write(address, data);
        }

        public OperateResult WriteCustomer<T>(string address, T data) where T: IDataTransfer, new()
        {
            return this.Write(address, data.ToSource());
        }

        public IByteTransform ByteTransform
        {
            get
            {
                return this.byteTransform;
            }
            set
            {
                this.byteTransform = value;
            }
        }

        public string ConnectionId
        {
            get
            {
                return this.connectionId;
            }
            set
            {
                this.connectionId = value;
            }
        }

        protected ushort WordLength { get; set; }
    }
}

