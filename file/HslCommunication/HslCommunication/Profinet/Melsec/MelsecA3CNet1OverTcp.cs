namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MelsecA3CNet1OverTcp : NetworkDeviceSoloBase
    {
        private byte station;

        public MelsecA3CNet1OverTcp()
        {
            this.station = 0;
            base.WordLength = 1;
            base.ByteTransform = new RegularByteTransform();
        }

        public MelsecA3CNet1OverTcp(string ipAddress, int port)
        {
            this.station = 0;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        public static byte[] PackCommand(byte[] mcCommand, [Optional, DefaultParameterValue(0)] byte station)
        {
            byte[] array = new byte[13 + mcCommand.Length];
            array[0] = 5;
            array[1] = 70;
            array[2] = 0x39;
            array[3] = SoftBasic.BuildAsciiBytesFrom(station)[0];
            array[4] = SoftBasic.BuildAsciiBytesFrom(station)[1];
            array[5] = 0x30;
            array[6] = 0x30;
            array[7] = 70;
            array[8] = 70;
            array[9] = 0x30;
            array[10] = 0x30;
            mcCommand.CopyTo(array, 11);
            int num = 0;
            for (int i = 1; i < (array.Length - 3); i++)
            {
                num += array[i];
            }
            array[array.Length - 2] = SoftBasic.BuildAsciiBytesFrom((byte) num)[0];
            array[array.Length - 1] = SoftBasic.BuildAsciiBytesFrom((byte) num)[1];
            return array;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            return ReadHelper(address, length, station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            return ReadBoolHelper(address, length, station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        public static OperateResult<bool[]> ReadBoolHelper(string address, ushort length, byte station, Func<byte[], byte, OperateResult<byte[]>> readCore)
        {
            OperateResult<McAddressData> result = McAddressData.ParseMelsecFrom(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            byte[] buffer = MelsecHelper.BuildAsciiReadMcCoreCommand(result.Content, true);
            OperateResult<byte[]> result2 = readCore(buffer, station);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            if (result2.Content[0] != 2)
            {
                return new OperateResult<bool[]>(result2.Content[0], "Read Faild:" + Encoding.ASCII.GetString(result2.Content, 1, result2.Content.Length - 1));
            }
            byte[] destinationArray = new byte[length];
            Array.Copy(result2.Content, 11, destinationArray, 0, length);
            return OperateResult.CreateSuccessResult<bool[]>((from m in destinationArray select m == 0x31).ToArray<bool>());
        }

        public static OperateResult<byte[]> ReadHelper(string address, ushort length, byte station, Func<byte[], byte, OperateResult<byte[]>> readCore)
        {
            OperateResult<McAddressData> result = McAddressData.ParseMelsecFrom(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] buffer = MelsecHelper.BuildAsciiReadMcCoreCommand(result.Content, false);
            OperateResult<byte[]> result2 = readCore(buffer, station);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result2.Content[0] != 2)
            {
                return new OperateResult<byte[]>(result2.Content[0], "Read Faild:" + Encoding.ASCII.GetString(result2.Content, 1, result2.Content.Length - 1));
            }
            byte[] array = new byte[length * 2];
            for (int i = 0; i < (array.Length / 2); i++)
            {
                BitConverter.GetBytes(Convert.ToUInt16(Encoding.ASCII.GetString(result2.Content, (i * 4) + 11, 4), 0x10)).CopyTo(array, (int) (i * 2));
            }
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        [HslMqttApi]
        public OperateResult<string> ReadPlcType()
        {
            return ReadPlcTypeHelper(this.station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        public static OperateResult<string> ReadPlcTypeHelper(byte station, Func<byte[], byte, OperateResult<byte[]>> readCore)
        {
            OperateResult<byte[]> result = readCore(Encoding.ASCII.GetBytes("01010000"), station);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            if ((result.Content[0] != 6) && (result.Content[0] != 2))
            {
                return new OperateResult<string>(result.Content[0], "Faild:" + Encoding.ASCII.GetString(result.Content, 1, result.Content.Length - 1));
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.ASCII.GetString(result.Content, 11, 0x10).TrimEnd(new char[0]));
        }

        private OperateResult<byte[]> ReadWithPackCommand(byte[] command, byte station)
        {
            return base.ReadFromCoreServer(PackCommand(command, station));
        }

        [HslMqttApi]
        public OperateResult RemoteRun()
        {
            return RemoteRunHelper(this.station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        public static OperateResult RemoteRunHelper(byte station, Func<byte[], byte, OperateResult<byte[]>> readCore)
        {
            OperateResult<byte[]> result = readCore(Encoding.ASCII.GetBytes("1001000000010000"), station);
            if (!result.IsSuccess)
            {
                return result;
            }
            if ((result.Content[0] != 6) && (result.Content[0] != 2))
            {
                return new OperateResult(result.Content[0], "Faild:" + Encoding.ASCII.GetString(result.Content, 1, result.Content.Length - 1));
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi]
        public OperateResult RemoteStop()
        {
            return RemoteStopHelper(this.station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        public static OperateResult RemoteStopHelper(byte station, Func<byte[], byte, OperateResult<byte[]>> readCore)
        {
            OperateResult<byte[]> result = readCore(Encoding.ASCII.GetBytes("100200000001"), station);
            if (!result.IsSuccess)
            {
                return result;
            }
            if ((result.Content[0] != 6) && (result.Content[0] != 2))
            {
                return new OperateResult(result.Content[0], "Faild:" + Encoding.ASCII.GetString(result.Content, 1, result.Content.Length - 1));
            }
            return OperateResult.CreateSuccessResult();
        }

        public override string ToString()
        {
            return string.Format("MelsecA3CNet1OverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            return WriteHelper(address, value, station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            return WriteHelper(address, value, station, new Func<byte[], byte, OperateResult<byte[]>>(this.ReadWithPackCommand));
        }

        public static OperateResult WriteHelper(string address, bool[] value, byte station, Func<byte[], byte, OperateResult<byte[]>> readCore)
        {
            OperateResult<McAddressData> result = McAddressData.ParseMelsecFrom(address, 0);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            byte[] buffer = MelsecHelper.BuildAsciiWriteBitCoreCommand(result.Content, value);
            OperateResult<byte[]> result2 = readCore(buffer, station);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result2.Content[0] != 6)
            {
                return new OperateResult(result2.Content[0], "Write Faild:" + Encoding.ASCII.GetString(result2.Content, 1, result2.Content.Length - 1));
            }
            return OperateResult.CreateSuccessResult();
        }

        public static OperateResult WriteHelper(string address, byte[] value, byte station, Func<byte[], byte, OperateResult<byte[]>> readCore)
        {
            OperateResult<McAddressData> result = McAddressData.ParseMelsecFrom(address, 0);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] buffer = MelsecHelper.BuildAsciiWriteWordCoreCommand(result.Content, value);
            OperateResult<byte[]> result2 = readCore(buffer, station);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result2.Content[0] != 6)
            {
                return new OperateResult(result2.Content[0], "Write Faild:" + Encoding.ASCII.GetString(result2.Content, 1, result2.Content.Length - 1));
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
            public static readonly MelsecA3CNet1OverTcp.<>c <>9 = new MelsecA3CNet1OverTcp.<>c();
            public static Func<byte, bool> <>9__17_0;

            internal bool <ReadBoolHelper>b__17_0(byte m)
            {
                return (m == 0x31);
            }
        }
    }
}

