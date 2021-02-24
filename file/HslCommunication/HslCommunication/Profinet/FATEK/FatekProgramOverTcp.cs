namespace HslCommunication.Profinet.FATEK
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class FatekProgramOverTcp : NetworkDeviceBase
    {
        private byte station;

        public FatekProgramOverTcp()
        {
            this.station = 1;
            base.WordLength = 1;
            base.ByteTransform = new RegularByteTransform();
            base.SleepTime = 20;
        }

        public FatekProgramOverTcp(string ipAddress, int port)
        {
            this.station = 1;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
            base.SleepTime = 20;
        }

        public static OperateResult<byte[]> BuildReadCommand(byte station, string address, ushort length, bool isBool)
        {
            station = (byte) HslHelper.ExtractParameter(ref address, "s", station);
            OperateResult<string> result = FatekAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append('\x0002');
            builder.Append(station.ToString("X2"));
            if (isBool)
            {
                builder.Append("44");
                builder.Append(length.ToString("X2"));
            }
            else
            {
                builder.Append("46");
                builder.Append(length.ToString("X2"));
                if ((((result.Content.StartsWith("X") || result.Content.StartsWith("Y")) || (result.Content.StartsWith("M") || result.Content.StartsWith("S"))) || result.Content.StartsWith("T")) || result.Content.StartsWith("C"))
                {
                    builder.Append("W");
                }
            }
            builder.Append(result.Content);
            builder.Append(CalculateAcc(builder.ToString()));
            builder.Append('\x0003');
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult<byte[]> BuildWriteBoolCommand(byte station, string address, bool[] value)
        {
            station = (byte) HslHelper.ExtractParameter(ref address, "s", station);
            OperateResult<string> result = FatekAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append('\x0002');
            builder.Append(station.ToString("X2"));
            builder.Append("45");
            builder.Append(value.Length.ToString("X2"));
            builder.Append(result.Content);
            for (int i = 0; i < value.Length; i++)
            {
                builder.Append(value[i] ? "1" : "0");
            }
            builder.Append(CalculateAcc(builder.ToString()));
            builder.Append('\x0003');
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult<byte[]> BuildWriteByteCommand(byte station, string address, byte[] value)
        {
            station = (byte) HslHelper.ExtractParameter(ref address, "s", station);
            OperateResult<string> result = FatekAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append('\x0002');
            builder.Append(station.ToString("X2"));
            builder.Append("47");
            builder.Append((value.Length / 2).ToString("X2"));
            if ((((result.Content.StartsWith("X") || result.Content.StartsWith("Y")) || (result.Content.StartsWith("M") || result.Content.StartsWith("S"))) || result.Content.StartsWith("T")) || result.Content.StartsWith("C"))
            {
                builder.Append("W");
            }
            builder.Append(result.Content);
            byte[] array = new byte[value.Length * 2];
            for (int i = 0; i < (value.Length / 2); i++)
            {
                SoftBasic.BuildAsciiBytesFrom(BitConverter.ToUInt16(value, i * 2)).CopyTo(array, (int) (4 * i));
            }
            builder.Append(Encoding.ASCII.GetString(array));
            builder.Append(CalculateAcc(builder.ToString()));
            builder.Append('\x0003');
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static string CalculateAcc(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            int num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                num += bytes[i];
            }
            return num.ToString("X4").Substring(2);
        }

        public static OperateResult<string> FatekAnalysisAddress(string address)
        {
            OperateResult<string> result = new OperateResult<string>();
            try
            {
                switch (address[0])
                {
                    case 'R':
                    case 'r':
                        if ((address[1] == 'T') || (address[1] == 't'))
                        {
                            result.Content = "RT" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        }
                        else if ((address[1] == 'C') || (address[1] == 'c'))
                        {
                            result.Content = "RC" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        }
                        else
                        {
                            result.Content = "R" + Convert.ToUInt16(address.Substring(1), 10).ToString("D5");
                        }
                        goto Label_0315;

                    case 'S':
                    case 's':
                        result.Content = "S" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_0315;

                    case 'T':
                    case 't':
                        result.Content = "T" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_0315;

                    case 'X':
                    case 'x':
                        result.Content = "X" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_0315;

                    case 'Y':
                    case 'y':
                        result.Content = "Y" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_0315;

                    case 'M':
                    case 'm':
                        result.Content = "M" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_0315;

                    case 'C':
                    case 'c':
                        result.Content = "C" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_0315;

                    case 'D':
                    case 'd':
                        result.Content = "D" + Convert.ToUInt16(address.Substring(1), 10).ToString("D5");
                        goto Label_0315;
                }
                throw new Exception(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                result.Message = exception.Message;
                return result;
            }
        Label_0315:
            result.IsSuccess = true;
            return result;
        }

        public static string GetErrorDescriptionFromCode(char code)
        {
            switch (code)
            {
                case '2':
                    return StringResources.Language.FatekStatus02;

                case '3':
                    return StringResources.Language.FatekStatus03;

                case '4':
                    return StringResources.Language.FatekStatus04;

                case '5':
                    return StringResources.Language.FatekStatus05;

                case '6':
                    return StringResources.Language.FatekStatus06;

                case '7':
                    return StringResources.Language.FatekStatus07;

                case '9':
                    return StringResources.Language.FatekStatus09;

                case 'A':
                    return StringResources.Language.FatekStatus10;
            }
            return StringResources.Language.UnknownError;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(this.station, address, length, false);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
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
                return new OperateResult<byte[]>(result2.Content[5], GetErrorDescriptionFromCode(result2.Content[5]));
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
            OperateResult<byte[]> result = BuildReadCommand(this.station, address, length, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
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
                return new OperateResult<bool[]>(result2.Content[5], GetErrorDescriptionFromCode(result2.Content[5]));
            }
            byte[] destinationArray = new byte[length];
            Array.Copy(result2.Content, 6, destinationArray, 0, length);
            return OperateResult.CreateSuccessResult<bool[]>((from m in destinationArray select m == 0x31).ToArray<bool>());
        }

        public override string ToString()
        {
            return string.Format("FatekProgramOverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] value)
        {
            OperateResult<byte[]> result = BuildWriteBoolCommand(this.station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
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
                return new OperateResult<bool[]>(result2.Content[5], GetErrorDescriptionFromCode(result2.Content[5]));
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = BuildWriteByteCommand(this.station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
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
                return new OperateResult<byte[]>(result2.Content[5], GetErrorDescriptionFromCode(result2.Content[5]));
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
            public static readonly FatekProgramOverTcp.<>c <>9 = new FatekProgramOverTcp.<>c();
            public static Func<byte, bool> <>9__7_0;

            internal bool <ReadBool>b__7_0(byte m)
            {
                return (m == 0x31);
            }
        }
    }
}

