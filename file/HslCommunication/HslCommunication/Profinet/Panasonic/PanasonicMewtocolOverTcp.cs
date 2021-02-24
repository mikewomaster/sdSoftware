namespace HslCommunication.Profinet.Panasonic
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

    public class PanasonicMewtocolOverTcp : NetworkDeviceSoloBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <Station>k__BackingField;

        public PanasonicMewtocolOverTcp([Optional, DefaultParameterValue(0xee)] byte station)
        {
            base.ByteTransform = new RegularByteTransform();
            this.Station = station;
            base.ByteTransform.DataFormat = DataFormat.DCBA;
        }

        public PanasonicMewtocolOverTcp(string ipAddress, int port, [Optional, DefaultParameterValue(0xee)] byte station)
        {
            base.ByteTransform = new RegularByteTransform();
            this.Station = station;
            base.ByteTransform.DataFormat = DataFormat.DCBA;
            this.IpAddress = ipAddress;
            this.Port = port;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = PanasonicHelper.BuildReadCommand(station, address, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return PanasonicHelper.ExtraActualData(result2.Content);
        }

        [HslMqttApi("ReadBool", "")]
        public override OperateResult<bool> ReadBool(string address)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = PanasonicHelper.BuildReadOneCoil(station, address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool>(result2);
            }
            return PanasonicHelper.ExtraActualBool(result2.Content);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<string, int> result = PanasonicHelper.AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = PanasonicHelper.BuildReadCommand(station, address, length);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            OperateResult<byte[]> result4 = PanasonicHelper.ExtraActualData(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(result4.Content).SelectMiddle<bool>(result.Content2 % 0x10, length));
        }

        public override string ToString()
        {
            return string.Format("PanasonicMewtocolOverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<string, int> result = PanasonicHelper.AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            if ((result.Content2 % 0x10) > 0)
            {
                return new OperateResult(StringResources.Language.PanasonicAddressBitStartMulti16);
            }
            if ((values.Length % 0x10) > 0)
            {
                return new OperateResult(StringResources.Language.PanasonicBoolLengthMulti16);
            }
            byte[] buffer = SoftBasic.BoolArrayToByte(values);
            OperateResult<byte[]> result2 = PanasonicHelper.BuildWriteCommand(station, address, buffer);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return PanasonicHelper.ExtraActualData(result3.Content);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = PanasonicHelper.BuildWriteCommand(station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return PanasonicHelper.ExtraActualData(result2.Content);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = PanasonicHelper.BuildWriteOneCoil(station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return PanasonicHelper.ExtraActualData(result2.Content);
        }

        public byte Station { get; set; }
    }
}

