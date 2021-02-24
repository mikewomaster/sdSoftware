namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MelsecFxLinks : SerialDeviceBase
    {
        private byte station = 0;
        private bool sumCheck = true;
        private byte watiingTime = 0;

        public MelsecFxLinks()
        {
            base.ByteTransform = new RegularByteTransform();
            base.WordLength = 1;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<byte[]> result = MelsecFxLinksOverTcp.BuildReadCommand(station, address, length, false, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            if (result2.Content[0] != 2)
            {
                return new OperateResult<byte[]>(result2.Content[0], "Read Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            byte[] array = new byte[length * 2];
            for (int i = 0; i < (array.Length / 2); i++)
            {
                BitConverter.GetBytes(Convert.ToUInt16(Encoding.ASCII.GetString(result2.Content, (i * 4) + 5, 4), 0x10)).CopyTo(array, (int) (i * 2));
            }
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<byte[]> result = MelsecFxLinksOverTcp.BuildReadCommand(station, address, length, true, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            if (result2.Content[0] != 2)
            {
                return new OperateResult<bool[]>(result2.Content[0], "Read Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            byte[] destinationArray = new byte[length];
            Array.Copy(result2.Content, 5, destinationArray, 0, length);
            return OperateResult.CreateSuccessResult<bool[]>((from m in destinationArray select m == 0x31).ToArray<bool>());
        }

        [HslMqttApi(Description="Read the PLC model information, you can carry additional parameter information, and specify the station number. Example: s=2; Note: The semicolon is required.")]
        public OperateResult<string> ReadPlcType([Optional, DefaultParameterValue("")] string parameter)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref parameter, "s", this.station);
            OperateResult<byte[]> result = MelsecFxLinksOverTcp.BuildReadPlcType(station, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            if (result2.Content[0] != 6)
            {
                return new OperateResult<string>(result2.Content[0], "ReadPlcType Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            return MelsecFxLinksOverTcp.GetPlcTypeFromCode(Encoding.ASCII.GetString(result2.Content, 5, 2));
        }

        [HslMqttApi(Description="Start the PLC operation, you can carry additional parameter information and specify the station number. Example: s=2; Note: The semicolon is required.")]
        public OperateResult StartPLC([Optional, DefaultParameterValue("")] string parameter)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref parameter, "s", this.station);
            OperateResult<byte[]> result = MelsecFxLinksOverTcp.BuildStart(station, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result2.Content[0] != 6)
            {
                return new OperateResult(result2.Content[0], "Start Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi(Description="Stop PLC operation, you can carry additional parameter information and specify the station number. Example: s=2; Note: The semicolon is required.")]
        public OperateResult StopPLC([Optional, DefaultParameterValue("")] string parameter)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref parameter, "s", this.station);
            OperateResult<byte[]> result = MelsecFxLinksOverTcp.BuildStop(station, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result2.Content[0] != 6)
            {
                return new OperateResult(result2.Content[0], "Stop Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            return OperateResult.CreateSuccessResult();
        }

        public override string ToString()
        {
            return string.Format("MelsecFxLinks[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<byte[]> result = MelsecFxLinksOverTcp.BuildWriteBoolCommand(station, address, value, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result2.Content[0] != 6)
            {
                return new OperateResult(result2.Content[0], "Write Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<byte[]> result = MelsecFxLinksOverTcp.BuildWriteByteCommand(station, address, value, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result2.Content[0] != 6)
            {
                return new OperateResult(result2.Content[0], "Write Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            return OperateResult.CreateSuccessResult();
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

        public bool SumCheck
        {
            get
            {
                return this.sumCheck;
            }
            set
            {
                this.sumCheck = value;
            }
        }

        public byte WaittingTime
        {
            get
            {
                return this.watiingTime;
            }
            set
            {
                if (this.watiingTime > 15)
                {
                    this.watiingTime = 15;
                }
                else
                {
                    this.watiingTime = value;
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MelsecFxLinks.<>c <>9 = new MelsecFxLinks.<>c();
            public static Func<byte, bool> <>9__12_0;

            internal bool <ReadBool>b__12_0(byte m)
            {
                return (m == 0x31);
            }
        }
    }
}

