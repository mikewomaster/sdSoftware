namespace HslCommunication.ModBus
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class ModbusUdpNet : NetworkUdpDeviceBase
    {
        private bool isAddressStartWithZero;
        private SoftIncrementCount softIncrementCount;
        private byte station;

        public ModbusUdpNet()
        {
            this.station = 1;
            this.isAddressStartWithZero = true;
            base.ByteTransform = new ReverseWordTransform();
            this.softIncrementCount = new SoftIncrementCount(0xffffL, 0L, 1);
            base.WordLength = 1;
            this.station = 1;
        }

        public ModbusUdpNet(string ipAddress, [Optional, DefaultParameterValue(0x1f6)] int port, [Optional, DefaultParameterValue(1)] byte station)
        {
            this.station = 1;
            this.isAddressStartWithZero = true;
            base.ByteTransform = new ReverseWordTransform();
            this.softIncrementCount = new SoftIncrementCount(0xffffL, 0L, 1);
            this.IpAddress = ipAddress;
            this.Port = port;
            base.WordLength = 1;
            this.station = station;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            ushort num2;
            OperateResult<ModbusAddress> result = ModbusInfo.AnalysisAddress(address, this.Station, this.isAddressStartWithZero, 3);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte>();
            for (ushort i = 0; i < length; i = (ushort) (i + num2))
            {
                num2 = (ushort) Math.Min(length - i, 120);
                OperateResult<byte[]> result3 = this.ReadModBus(result.Content.AddressAdd(i), num2);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result3);
                }
                list.AddRange(result3.Content);
            }
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildReadModbusCommand(address, length, this.Station, this.AddressStartWithZero, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(ModbusInfo.PackCommandToTcp(result.Content, (ushort) this.softIncrementCount.GetCurrentValue()));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = ModbusInfo.ExtractActualData(ModbusInfo.ExplodeTcpCommandToCore(result2.Content));
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(result3.Content, length));
        }

        public OperateResult<bool> ReadCoil(string address)
        {
            return this.ReadBool(address);
        }

        public OperateResult<bool[]> ReadCoil(string address, ushort length)
        {
            return this.ReadBool(address, length);
        }

        public OperateResult<bool> ReadDiscrete(string address)
        {
            return ByteTransformHelper.GetResultFromArray<bool>(this.ReadDiscrete(address, 1));
        }

        public OperateResult<bool[]> ReadDiscrete(string address, ushort length)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildReadModbusCommand(address, length, this.Station, this.AddressStartWithZero, 2);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(ModbusInfo.PackCommandToTcp(result.Content, (ushort) this.softIncrementCount.GetCurrentValue()));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = ModbusInfo.ExtractActualData(ModbusInfo.ExplodeTcpCommandToCore(result2.Content));
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(result3.Content, length));
        }

        [HslMqttApi("ReadDoubleArray", "")]
        public override OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return ByteTransformHelper.GetResultFromBytes<double[]>(this.Read(address, (ushort) ((length * base.WordLength) * 4)), m => transform.TransDouble(m, 0, length));
        }

        [HslMqttApi("ReadFloatArray", "")]
        public override OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return ByteTransformHelper.GetResultFromBytes<float[]>(this.Read(address, (ushort) ((length * base.WordLength) * 2)), m => transform.TransSingle(m, 0, length));
        }

        [HslMqttApi("ReadInt32Array", "")]
        public override OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return ByteTransformHelper.GetResultFromBytes<int[]>(this.Read(address, (ushort) ((length * base.WordLength) * 2)), m => transform.TransInt32(m, 0, length));
        }

        [HslMqttApi("ReadInt64Array", "")]
        public override OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return ByteTransformHelper.GetResultFromBytes<long[]>(this.Read(address, (ushort) ((length * base.WordLength) * 4)), m => transform.TransInt64(m, 0, length));
        }

        private OperateResult<byte[]> ReadModBus(ModbusAddress address, ushort length)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildReadModbusCommand(address, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(ModbusInfo.PackCommandToTcp(result.Content, (ushort) this.softIncrementCount.GetCurrentValue()));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return ModbusInfo.ExtractActualData(ModbusInfo.ExplodeTcpCommandToCore(result2.Content));
        }

        [HslMqttApi("ReadUInt32Array", "")]
        public override OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return ByteTransformHelper.GetResultFromBytes<uint[]>(this.Read(address, (ushort) ((length * base.WordLength) * 2)), m => transform.TransUInt32(m, 0, length));
        }

        [HslMqttApi("ReadUInt64Array", "")]
        public override OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return ByteTransformHelper.GetResultFromBytes<ulong[]>(this.Read(address, (ushort) ((length * base.WordLength) * 4)), m => transform.TransUInt64(m, 0, length));
        }

        public override string ToString()
        {
            return string.Format("ModbusUdpNet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildWriteBoolModbusCommand(address, values, this.Station, this.AddressStartWithZero, 15);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(ModbusInfo.PackCommandToTcp(result.Content, (ushort) this.softIncrementCount.GetCurrentValue()));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return ModbusInfo.ExtractActualData(ModbusInfo.ExplodeTcpCommandToCore(result2.Content));
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildWriteWordModbusCommand(address, value, this.Station, this.AddressStartWithZero, 0x10);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(ModbusInfo.PackCommandToTcp(result.Content, (ushort) this.softIncrementCount.GetCurrentValue()));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return ModbusInfo.ExtractActualData(ModbusInfo.ExplodeTcpCommandToCore(result2.Content));
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildWriteBoolModbusCommand(address, value, this.Station, this.AddressStartWithZero, 5);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(ModbusInfo.PackCommandToTcp(result.Content, (ushort) this.softIncrementCount.GetCurrentValue()));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return ModbusInfo.ExtractActualData(ModbusInfo.ExplodeTcpCommandToCore(result2.Content));
        }

        [HslMqttApi("WriteInt16", "")]
        public override OperateResult Write(string address, short value)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildWriteWordModbusCommand(address, value, this.Station, this.AddressStartWithZero, 6);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(ModbusInfo.PackCommandToTcp(result.Content, (ushort) this.softIncrementCount.GetCurrentValue()));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return ModbusInfo.ExtractActualData(ModbusInfo.ExplodeTcpCommandToCore(result2.Content));
        }

        [HslMqttApi("WriteUInt16", "")]
        public override OperateResult Write(string address, ushort value)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildWriteWordModbusCommand(address, value, this.Station, this.AddressStartWithZero, 6);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(ModbusInfo.PackCommandToTcp(result.Content, (ushort) this.softIncrementCount.GetCurrentValue()));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return ModbusInfo.ExtractActualData(ModbusInfo.ExplodeTcpCommandToCore(result2.Content));
        }

        [HslMqttApi("WriteDoubleArray", "")]
        public override OperateResult Write(string address, double[] values)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return this.Write(address, transform.TransByte(values));
        }

        [HslMqttApi("WriteInt32Array", "")]
        public override OperateResult Write(string address, int[] values)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return this.Write(address, transform.TransByte(values));
        }

        [HslMqttApi("WriteInt64Array", "")]
        public override OperateResult Write(string address, long[] values)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return this.Write(address, transform.TransByte(values));
        }

        [HslMqttApi("WriteFloatArray", "")]
        public override OperateResult Write(string address, float[] values)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return this.Write(address, transform.TransByte(values));
        }

        [HslMqttApi("WriteUInt32Array", "")]
        public override OperateResult Write(string address, uint[] values)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return this.Write(address, transform.TransByte(values));
        }

        [HslMqttApi("WriteUInt64Array", "")]
        public override OperateResult Write(string address, ulong[] values)
        {
            IByteTransform transform = HslHelper.ExtractTransformParameter(ref address, base.ByteTransform);
            return this.Write(address, transform.TransByte(values));
        }

        [HslMqttApi("WriteMask", "")]
        public OperateResult WriteMask(string address, ushort andMask, ushort orMask)
        {
            OperateResult<byte[]> result = ModbusInfo.BuildWriteMaskModbusCommand(address, andMask, orMask, this.Station, this.AddressStartWithZero, 0x16);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(ModbusInfo.PackCommandToTcp(result.Content, (ushort) this.softIncrementCount.GetCurrentValue()));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return ModbusInfo.ExtractActualData(ModbusInfo.ExplodeTcpCommandToCore(result2.Content));
        }

        public OperateResult WriteOneRegister(string address, short value)
        {
            return this.Write(address, value);
        }

        public OperateResult WriteOneRegister(string address, ushort value)
        {
            return this.Write(address, value);
        }

        public bool AddressStartWithZero
        {
            get
            {
                return this.isAddressStartWithZero;
            }
            set
            {
                this.isAddressStartWithZero = value;
            }
        }

        public HslCommunication.Core.DataFormat DataFormat
        {
            get
            {
                return base.ByteTransform.DataFormat;
            }
            set
            {
                base.ByteTransform.DataFormat = value;
            }
        }

        public bool IsStringReverse
        {
            get
            {
                return base.ByteTransform.IsStringReverseByteWord;
            }
            set
            {
                base.ByteTransform.IsStringReverseByteWord = value;
            }
        }

        public SoftIncrementCount MessageId
        {
            get
            {
                return this.softIncrementCount;
            }
        }

        public byte Station
        {
            get
            {
                return this.station;
            }
            set
            {
                this.station = value;
            }
        }
    }
}

