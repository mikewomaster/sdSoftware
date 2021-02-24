namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MelsecFxLinksOverTcp : NetworkDeviceBase
    {
        private byte station;
        private bool sumCheck;
        private byte watiingTime;

        public MelsecFxLinksOverTcp()
        {
            this.station = 0;
            this.watiingTime = 0;
            this.sumCheck = true;
            base.WordLength = 1;
            base.ByteTransform = new RegularByteTransform();
            base.SleepTime = 20;
        }

        public MelsecFxLinksOverTcp(string ipAddress, int port)
        {
            this.station = 0;
            this.watiingTime = 0;
            this.sumCheck = true;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
            base.SleepTime = 20;
        }

        public static OperateResult<byte[]> BuildReadCommand(byte station, string address, ushort length, bool isBool, [Optional, DefaultParameterValue(true)] bool sumCheck, [Optional, DefaultParameterValue(0)] byte waitTime)
        {
            OperateResult<string> result = FxAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(station.ToString("D2"));
            builder.Append("FF");
            if (isBool)
            {
                builder.Append("BR");
            }
            else
            {
                builder.Append("WR");
            }
            builder.Append(waitTime.ToString("X"));
            builder.Append(result.Content);
            builder.Append(length.ToString("D2"));
            byte[] bytes = null;
            if (sumCheck)
            {
                bytes = Encoding.ASCII.GetBytes(CalculateAcc(builder.ToString()));
            }
            else
            {
                bytes = Encoding.ASCII.GetBytes(builder.ToString());
            }
            byte[] buffer1 = new byte[] { 5 };
            return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.SpliceTwoByteArray(buffer1, bytes));
        }

        public static OperateResult<byte[]> BuildReadPlcType(byte station, [Optional, DefaultParameterValue(true)] bool sumCheck, [Optional, DefaultParameterValue(0)] byte waitTime)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<byte[]>(StringResources.Language.InsufficientPrivileges);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(station.ToString("D2"));
            builder.Append("FF");
            builder.Append("PC");
            builder.Append(waitTime.ToString("X"));
            byte[] bytes = null;
            if (sumCheck)
            {
                bytes = Encoding.ASCII.GetBytes(CalculateAcc(builder.ToString()));
            }
            else
            {
                bytes = Encoding.ASCII.GetBytes(builder.ToString());
            }
            byte[] buffer1 = new byte[] { 5 };
            return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.SpliceTwoByteArray(buffer1, bytes));
        }

        public static OperateResult<byte[]> BuildStart(byte station, [Optional, DefaultParameterValue(true)] bool sumCheck, [Optional, DefaultParameterValue(0)] byte waitTime)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<byte[]>(StringResources.Language.InsufficientPrivileges);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(station.ToString("D2"));
            builder.Append("FF");
            builder.Append("RR");
            builder.Append(waitTime.ToString("X"));
            byte[] bytes = null;
            if (sumCheck)
            {
                bytes = Encoding.ASCII.GetBytes(CalculateAcc(builder.ToString()));
            }
            else
            {
                bytes = Encoding.ASCII.GetBytes(builder.ToString());
            }
            byte[] buffer1 = new byte[] { 5 };
            return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.SpliceTwoByteArray(buffer1, bytes));
        }

        public static OperateResult<byte[]> BuildStop(byte station, [Optional, DefaultParameterValue(true)] bool sumCheck, [Optional, DefaultParameterValue(0)] byte waitTime)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<byte[]>(StringResources.Language.InsufficientPrivileges);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(station.ToString("D2"));
            builder.Append("FF");
            builder.Append("RS");
            builder.Append(waitTime.ToString("X"));
            byte[] bytes = null;
            if (sumCheck)
            {
                bytes = Encoding.ASCII.GetBytes(CalculateAcc(builder.ToString()));
            }
            else
            {
                bytes = Encoding.ASCII.GetBytes(builder.ToString());
            }
            byte[] buffer1 = new byte[] { 5 };
            return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.SpliceTwoByteArray(buffer1, bytes));
        }

        public static OperateResult<byte[]> BuildWriteBoolCommand(byte station, string address, bool[] value, [Optional, DefaultParameterValue(true)] bool sumCheck, [Optional, DefaultParameterValue(0)] byte waitTime)
        {
            OperateResult<string> result = FxAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(station.ToString("D2"));
            builder.Append("FF");
            builder.Append("BW");
            builder.Append(waitTime.ToString("X"));
            builder.Append(result.Content);
            builder.Append(value.Length.ToString("D2"));
            for (int i = 0; i < value.Length; i++)
            {
                builder.Append(value[i] ? "1" : "0");
            }
            byte[] bytes = null;
            if (sumCheck)
            {
                bytes = Encoding.ASCII.GetBytes(CalculateAcc(builder.ToString()));
            }
            else
            {
                bytes = Encoding.ASCII.GetBytes(builder.ToString());
            }
            byte[] buffer1 = new byte[] { 5 };
            return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.SpliceTwoByteArray(buffer1, bytes));
        }

        public static OperateResult<byte[]> BuildWriteByteCommand(byte station, string address, byte[] value, [Optional, DefaultParameterValue(true)] bool sumCheck, [Optional, DefaultParameterValue(0)] byte waitTime)
        {
            OperateResult<string> result = FxAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(station.ToString("D2"));
            builder.Append("FF");
            builder.Append("WW");
            builder.Append(waitTime.ToString("X"));
            builder.Append(result.Content);
            builder.Append((value.Length / 2).ToString("D2"));
            byte[] array = new byte[value.Length * 2];
            for (int i = 0; i < (value.Length / 2); i++)
            {
                SoftBasic.BuildAsciiBytesFrom(BitConverter.ToUInt16(value, i * 2)).CopyTo(array, (int) (4 * i));
            }
            builder.Append(Encoding.ASCII.GetString(array));
            byte[] bytes = null;
            if (sumCheck)
            {
                bytes = Encoding.ASCII.GetBytes(CalculateAcc(builder.ToString()));
            }
            else
            {
                bytes = Encoding.ASCII.GetBytes(builder.ToString());
            }
            byte[] buffer1 = new byte[] { 5 };
            return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.SpliceTwoByteArray(buffer1, bytes));
        }

        public static string CalculateAcc(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            int num = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                num += bytes[i];
            }
            return (data + num.ToString("X4").Substring(2));
        }

        private static OperateResult<string> FxAnalysisAddress(string address)
        {
            OperateResult<string> result = new OperateResult<string>();
            try
            {
                switch (address[0])
                {
                    case 'R':
                    case 'r':
                        result.Content = "R" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_039D;

                    case 'S':
                    case 's':
                        result.Content = "S" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_039D;

                    case 'T':
                    case 't':
                        if ((address[1] != 'S') && (address[1] != 's'))
                        {
                            if ((address[1] != 'N') && (address[1] != 'n'))
                            {
                                throw new Exception(StringResources.Language.NotSupportedDataType);
                            }
                            result.Content = "TN" + Convert.ToUInt16(address.Substring(1), 10).ToString("D3");
                        }
                        else
                        {
                            result.Content = "TS" + Convert.ToUInt16(address.Substring(1), 10).ToString("D3");
                        }
                        goto Label_039D;

                    case 'X':
                    case 'x':
                    {
                        ushort num = Convert.ToUInt16(address.Substring(1), 8);
                        result.Content = "X" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_039D;
                    }
                    case 'Y':
                    case 'y':
                    {
                        ushort num3 = Convert.ToUInt16(address.Substring(1), 8);
                        result.Content = "Y" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_039D;
                    }
                    case 'M':
                    case 'm':
                        result.Content = "M" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_039D;

                    case 'C':
                    case 'c':
                        if ((address[1] != 'S') && (address[1] != 's'))
                        {
                            if ((address[1] != 'N') && (address[1] != 'n'))
                            {
                                throw new Exception(StringResources.Language.NotSupportedDataType);
                            }
                            result.Content = "CN" + Convert.ToUInt16(address.Substring(1), 10).ToString("D3");
                        }
                        else
                        {
                            result.Content = "CS" + Convert.ToUInt16(address.Substring(1), 10).ToString("D3");
                        }
                        goto Label_039D;

                    case 'D':
                    case 'd':
                        result.Content = "D" + Convert.ToUInt16(address.Substring(1), 10).ToString("D4");
                        goto Label_039D;
                }
                throw new Exception(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                result.Message = exception.Message;
                return result;
            }
        Label_039D:
            result.IsSuccess = true;
            return result;
        }

        public static OperateResult<string> GetPlcTypeFromCode(string code)
        {
            string s = code;
            if (s != null)
            {
                switch (<PrivateImplementationDetails>.ComputeStringHash(s))
                {
                    case 0xefe9d85:
                        if (s == "93")
                        {
                            return OperateResult.CreateSuccessResult<string>("A2ACPU-S1");
                        }
                        break;

                    case 0x13fea564:
                        if (s == "98")
                        {
                            return OperateResult.CreateSuccessResult<string>("A0J2HCPU");
                        }
                        break;

                    case 0x15d2bebe:
                        if (s == "F3")
                        {
                            return OperateResult.CreateSuccessResult<string>("FX3U/FX3UC");
                        }
                        break;

                    case 0x7fe9280:
                        if (s == "94")
                        {
                            return OperateResult.CreateSuccessResult<string>("A3ACPU");
                        }
                        break;

                    case 0xdfe9bf2:
                        if (s == "92")
                        {
                            return OperateResult.CreateSuccessResult<string>("A2ACPU");
                        }
                        break;

                    case 0x16d2c051:
                        if (s == "F2")
                        {
                            return OperateResult.CreateSuccessResult<string>("FX1S");
                        }
                        break;

                    case 0x18d2c377:
                        if (s == "F4")
                        {
                            return OperateResult.CreateSuccessResult<string>("FX3G");
                        }
                        break;

                    case 0x1d00f226:
                        if (s == "8E")
                        {
                            return OperateResult.CreateSuccessResult<string>("FX0N");
                        }
                        break;

                    case 0x1e00f3b9:
                        if (s == "8D")
                        {
                            return OperateResult.CreateSuccessResult<string>("FX2/FX2C");
                        }
                        break;

                    case 0x2000f6df:
                        if (s == "8B")
                        {
                            return OperateResult.CreateSuccessResult<string>("AJ72LP25/BR15");
                        }
                        break;

                    case 0x2cd5218a:
                        if (s == "AB")
                        {
                            return OperateResult.CreateSuccessResult<string>("AJ72P25/R25");
                        }
                        break;

                    case 0x7cff4aaf:
                        if (s == "9A")
                        {
                            return OperateResult.CreateSuccessResult<string>("A2CCPU");
                        }
                        break;

                    case 0x8d01a276:
                        if (s == "85")
                        {
                            return OperateResult.CreateSuccessResult<string>("A4UCPU");
                        }
                        break;

                    case 0x8e01a409:
                        if (s == "84")
                        {
                            return OperateResult.CreateSuccessResult<string>("A3UCPU");
                        }
                        break;

                    case 0x77ff42d0:
                        if (s == "9D")
                        {
                            return OperateResult.CreateSuccessResult<string>("FX2N/FX2NC");
                        }
                        break;

                    case 0x78ff4463:
                        if (s == "9E")
                        {
                            return OperateResult.CreateSuccessResult<string>("FX1N/FX1NC");
                        }
                        break;

                    case 0x8f01a59c:
                        if (s == "83")
                        {
                            return OperateResult.CreateSuccessResult<string>("A2CPU-S1/A2USCPU-S1");
                        }
                        break;

                    case 0x9001a72f:
                        if (s == "82")
                        {
                            return OperateResult.CreateSuccessResult<string>("A2USCPU");
                        }
                        break;

                    case 0x96d5c868:
                        if (s == "A4")
                        {
                            return OperateResult.CreateSuccessResult<string>("A3HCPU/A3MCPU");
                        }
                        break;

                    case 0x9bd5d047:
                        if (s == "A1")
                        {
                            return OperateResult.CreateSuccessResult<string>("A1CPU /A1NCPU");
                        }
                        break;

                    case 0x9cd5d1da:
                        if (s == "A2")
                        {
                            return OperateResult.CreateSuccessResult<string>("A2CPU/A2NCPU/A2SCPU");
                        }
                        break;

                    case 0x9dd5d36d:
                        if (s == "A3")
                        {
                            return OperateResult.CreateSuccessResult<string>("A3CPU/A3NCPU");
                        }
                        break;
                }
            }
            return new OperateResult<string>(StringResources.Language.NotSupportedDataType + " Code:" + code);
        }

        [HslMqttApi("ReadByteArray", "Read PLC data in batches, in units of words, supports reading X, Y, M, S, D, T, C.")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<byte[]> result = BuildReadCommand(station, address, length, false, this.sumCheck, this.watiingTime);
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
            byte[] array = new byte[length * 2];
            for (int i = 0; i < (array.Length / 2); i++)
            {
                BitConverter.GetBytes(Convert.ToUInt16(Encoding.ASCII.GetString(result2.Content, (i * 4) + 5, 4), 0x10)).CopyTo(array, (int) (i * 2));
            }
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        [HslMqttApi("ReadBoolArray", "Read bool data in batches. The supported types are X, Y, S, T, C.")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<byte[]> result = BuildReadCommand(station, address, length, true, this.sumCheck, this.watiingTime);
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
            byte[] destinationArray = new byte[length];
            Array.Copy(result2.Content, 5, destinationArray, 0, length);
            return OperateResult.CreateSuccessResult<bool[]>((from m in destinationArray select m == 0x31).ToArray<bool>());
        }

        [HslMqttApi(Description="Read the PLC model information, you can carry additional parameter information, and specify the station number. Example: s=2; Note: The semicolon is required.")]
        public OperateResult<string> ReadPlcType([Optional, DefaultParameterValue("")] string parameter)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref parameter, "s", this.station);
            OperateResult<byte[]> result = BuildReadPlcType(station, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            if (result2.Content[0] != 6)
            {
                return new OperateResult<string>(result2.Content[0], "ReadPlcType Faild:" + SoftBasic.ByteToHexString(result2.Content, ' '));
            }
            return GetPlcTypeFromCode(Encoding.ASCII.GetString(result2.Content, 5, 2));
        }

        [HslMqttApi(Description="Start the PLC operation, you can carry additional parameter information and specify the station number. Example: s=2; Note: The semicolon is required.")]
        public OperateResult StartPLC([Optional, DefaultParameterValue("")] string parameter)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref parameter, "s", this.station);
            OperateResult<byte[]> result = BuildStart(station, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
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
            OperateResult<byte[]> result = BuildStop(station, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
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
            return string.Format("MelsecFxLinksOverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "Write arrays of type bool in batches. The supported types are X, Y, S, T, C.")]
        public override OperateResult Write(string address, bool[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<byte[]> result = BuildWriteBoolCommand(station, address, value, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
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

        [HslMqttApi("WriteByteArray", "The data written to the PLC in batches is in units of words, that is, at least 2 bytes of information. It supports X, Y, M, S, D, T, and C. ")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<byte[]> result = BuildWriteByteCommand(station, address, value, this.sumCheck, this.watiingTime);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
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
            public static readonly MelsecFxLinksOverTcp.<>c <>9 = new MelsecFxLinksOverTcp.<>c();
            public static Func<byte, bool> <>9__13_0;

            internal bool <ReadBool>b__13_0(byte m)
            {
                return (m == 0x31);
            }
        }
    }
}

