namespace HslCommunication.Profinet.FATEK
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class FatekProgram : SerialDeviceBase
    {
        private byte station = 1;

        public FatekProgram()
        {
            base.ByteTransform = new RegularByteTransform();
            base.WordLength = 1;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = FatekProgramOverTcp.BuildReadCommand(this.station, address, length, false);
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
            if (result2.Content[5] != 0x30)
            {
                return new OperateResult<byte[]>(result2.Content[5], FatekProgramOverTcp.GetErrorDescriptionFromCode(result2.Content[5]));
            }
            byte[] array = new byte[length * 2];
            for (int i = 0; i < (array.Length / 2); i++)
            {
                BitConverter.GetBytes(Convert.ToUInt16(Encoding.ASCII.GetString(result2.Content, (i * 4) + 6, 4), 0x10)).CopyTo(array, (int) (i * 2));
            }
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> result = FatekProgramOverTcp.BuildReadCommand(this.station, address, length, true);
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
            if (result2.Content[5] != 0x30)
            {
                return new OperateResult<bool[]>(result2.Content[5], FatekProgramOverTcp.GetErrorDescriptionFromCode(result2.Content[5]));
            }
            byte[] destinationArray = new byte[length];
            Array.Copy(result2.Content, 6, destinationArray, 0, length);
            return OperateResult.CreateSuccessResult<bool[]>((from m in destinationArray select m == 0x31).ToArray<bool>());
        }

        public override string ToString()
        {
            return string.Format("FatekProgram[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] value)
        {
            OperateResult<byte[]> result = FatekProgramOverTcp.BuildWriteBoolCommand(this.station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result2.Content[0] != 2)
            {
                return new OperateResult(result2.Content[0], "Write Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            if (result2.Content[5] != 0x30)
            {
                return new OperateResult<bool[]>(result2.Content[5], FatekProgramOverTcp.GetErrorDescriptionFromCode(result2.Content[5]));
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = FatekProgramOverTcp.BuildWriteByteCommand(this.station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result2.Content[0] != 2)
            {
                return new OperateResult(result2.Content[0], "Write Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            if (result2.Content[5] != 0x30)
            {
                return new OperateResult<byte[]>(result2.Content[5], FatekProgramOverTcp.GetErrorDescriptionFromCode(result2.Content[5]));
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

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FatekProgram.<>c <>9 = new FatekProgram.<>c();
            public static Func<byte, bool> <>9__6_0;

            internal bool <ReadBool>b__6_0(byte m)
            {
                return (m == 0x31);
            }
        }
    }
}

