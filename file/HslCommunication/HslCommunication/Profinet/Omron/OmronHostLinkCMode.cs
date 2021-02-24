namespace HslCommunication.Profinet.Omron
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class OmronHostLinkCMode : SerialDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <UnitNumber>k__BackingField;

        public OmronHostLinkCMode()
        {
            base.ByteTransform = new ReverseWordTransform();
            base.WordLength = 1;
            base.ByteTransform.DataFormat = DataFormat.CDAB;
            base.ByteTransform.IsStringReverseByteWord = true;
        }

        public static OperateResult<string, string> AnalysisAddress(string address, bool isBit, bool isRead)
        {
            OperateResult<string, string> result = new OperateResult<string, string>();
            try
            {
                switch (address[0])
                {
                    case 'A':
                    case 'a':
                        result.Content1 = isRead ? "RJ" : "WJ";
                        break;

                    case 'C':
                    case 'c':
                        result.Content1 = isRead ? "RR" : "WR";
                        break;

                    case 'D':
                    case 'd':
                        result.Content1 = isRead ? "RD" : "WD";
                        break;

                    case 'E':
                    case 'e':
                    {
                        char[] separator = new char[] { '.' };
                        int num = Convert.ToInt32(address.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0].Substring(1), 0x10);
                        result.Content1 = (isRead ? "RE" : "WE") + Encoding.ASCII.GetString(SoftBasic.BuildAsciiBytesFrom((byte) num));
                        break;
                    }
                    case 'H':
                    case 'h':
                        result.Content1 = isRead ? "RH" : "WH";
                        break;

                    default:
                        throw new Exception(StringResources.Language.NotSupportedDataType);
                }
                if ((address[0] == 'E') || (address[0] == 'e'))
                {
                    char[] chArray2 = new char[] { '.' };
                    string[] strArray2 = address.Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                    if (!isBit)
                    {
                        result.Content2 = ushort.Parse(strArray2[1]).ToString("D4");
                    }
                }
                else if (!isBit)
                {
                    result.Content2 = ushort.Parse(address.Substring(1)).ToString("D4");
                }
            }
            catch (Exception exception)
            {
                result.Message = exception.Message;
                return result;
            }
            result.IsSuccess = true;
            return result;
        }

        public static OperateResult<byte[]> BuildReadCommand(string address, ushort length, bool isBit)
        {
            OperateResult<string, string> result = AnalysisAddress(address, isBit, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(result.Content1);
            builder.Append(result.Content2);
            builder.Append(length.ToString("D4"));
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult<byte[]> BuildWriteWordCommand(string address, byte[] value)
        {
            OperateResult<string, string> result = AnalysisAddress(address, false, false);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(result.Content1);
            builder.Append(result.Content2);
            for (int i = 0; i < (value.Length / 2); i++)
            {
                builder.Append(BitConverter.ToUInt16(value, i * 2).ToString("X4"));
            }
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult<string> GetModelText(string model)
        {
            string s = model;
            if (s != null)
            {
                switch (<PrivateImplementationDetails>.ComputeStringHash(s))
                {
                    case 0x1ceb2bd7:
                        if (s == "11")
                        {
                            return OperateResult.CreateSuccessResult<string>("C2000H/CQM1/CPM1");
                        }
                        break;

                    case 0x1deb2d6a:
                        if (s == "12")
                        {
                            return OperateResult.CreateSuccessResult<string>("C20H/C28H/C40H, C200H, C200HS, C200HX/HG/HE (-ZE)");
                        }
                        break;

                    case 0x18ed6422:
                        if (s == "09")
                        {
                            return OperateResult.CreateSuccessResult<string>("C250F");
                        }
                        break;

                    case 0x1beb2a44:
                        if (s == "10")
                        {
                            return OperateResult.CreateSuccessResult<string>("C1000H");
                        }
                        break;

                    case 0x1eed6d94:
                        if (s == "03")
                        {
                            return OperateResult.CreateSuccessResult<string>("C120/C50");
                        }
                        break;

                    case 0x1fed6f27:
                        if (s == "02")
                        {
                            return OperateResult.CreateSuccessResult<string>("C500");
                        }
                        break;

                    case 0x20ed70ba:
                        if (s == "01")
                        {
                            return OperateResult.CreateSuccessResult<string>("C250");
                        }
                        break;

                    case 0x6cede85e:
                        if (s == "0E")
                        {
                            return OperateResult.CreateSuccessResult<string>("C2000");
                        }
                        break;

                    case 0x87e38583:
                        if (s == "42")
                        {
                            return OperateResult.CreateSuccessResult<string>("CVM1-CPU21-E");
                        }
                        break;

                    case 0x87f05176:
                        if (s == "30")
                        {
                            return OperateResult.CreateSuccessResult<string>("CS/CJ");
                        }
                        break;

                    case 0x6feded17:
                        if (s == "0B")
                        {
                            return OperateResult.CreateSuccessResult<string>("C120F");
                        }
                        break;

                    case 0x70edeeaa:
                        if (s == "0A")
                        {
                            return OperateResult.CreateSuccessResult<string>("C500F");
                        }
                        break;

                    case 0x88e38716:
                        if (s == "41")
                        {
                            return OperateResult.CreateSuccessResult<string>("CVM1-CPU11-E");
                        }
                        break;

                    case 0x89e388a9:
                        if (s == "40")
                        {
                            return OperateResult.CreateSuccessResult<string>("CVM1-CPU01-E");
                        }
                        break;

                    case 0x8cf297ec:
                        if (s == "21")
                        {
                            return OperateResult.CreateSuccessResult<string>("CV1000");
                        }
                        break;

                    case 0x8df2997f:
                        if (s == "20")
                        {
                            return OperateResult.CreateSuccessResult<string>("CV500");
                        }
                        break;

                    case 0x8ff29ca5:
                        if (s == "22")
                        {
                            return OperateResult.CreateSuccessResult<string>("CV2000");
                        }
                        break;
                }
            }
            return new OperateResult<string>("Unknown model, model code:" + model);
        }

        public static byte[] PackCommand(byte[] cmd, byte unitNumber)
        {
            byte[] array = new byte[7 + cmd.Length];
            array[0] = 0x40;
            array[1] = SoftBasic.BuildAsciiBytesFrom(unitNumber)[0];
            array[2] = SoftBasic.BuildAsciiBytesFrom(unitNumber)[1];
            array[array.Length - 2] = 0x2a;
            array[array.Length - 1] = 13;
            cmd.CopyTo(array, 3);
            int num = array[0];
            for (int i = 1; i < (array.Length - 4); i++)
            {
                num ^= array[i];
            }
            array[array.Length - 4] = SoftBasic.BuildAsciiBytesFrom((byte) num)[0];
            array[array.Length - 3] = SoftBasic.BuildAsciiBytesFrom((byte) num)[1];
            Console.WriteLine(Encoding.ASCII.GetString(array));
            return array;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte unitNumber = (byte) HslHelper.ExtractParameter(ref address, "s", this.UnitNumber);
            OperateResult<byte[]> result = BuildReadCommand(address, length, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(PackCommand(result.Content, unitNumber));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = ResponseValidAnalysis(result2.Content, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result3.Content);
        }

        [HslMqttApi]
        public OperateResult<string> ReadPlcModel()
        {
            OperateResult<byte[]> result = base.ReadBase(PackCommand(Encoding.ASCII.GetBytes("MM"), this.UnitNumber));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            int err = Convert.ToInt32(Encoding.ASCII.GetString(result.Content, 5, 2), 0x10);
            if (err > 0)
            {
                return new OperateResult<string>(err, "Unknown Error");
            }
            return GetModelText(Encoding.ASCII.GetString(result.Content, 7, 2));
        }

        public static OperateResult<byte[]> ResponseValidAnalysis(byte[] response, bool isRead)
        {
            if (response.Length >= 11)
            {
                int num = Convert.ToInt32(Encoding.ASCII.GetString(response, 5, 2), 0x10);
                byte[] buffer = null;
                if (response.Length > 11)
                {
                    byte[] array = new byte[(response.Length - 11) / 2];
                    for (int i = 0; i < (array.Length / 2); i++)
                    {
                        BitConverter.GetBytes(Convert.ToUInt16(Encoding.ASCII.GetString(response, 7 + (4 * i), 4), 0x10)).CopyTo(array, (int) (i * 2));
                    }
                    buffer = array;
                }
                if (num > 0)
                {
                    return new OperateResult<byte[]> { 
                        ErrorCode = num,
                        Content = buffer
                    };
                }
                return OperateResult.CreateSuccessResult<byte[]>(buffer);
            }
            return new OperateResult<byte[]>(StringResources.Language.OmronReceiveDataError);
        }

        public override string ToString()
        {
            return string.Format("OmronHostLinkCMode[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte unitNumber = (byte) HslHelper.ExtractParameter(ref address, "s", this.UnitNumber);
            OperateResult<byte[]> result = BuildWriteWordCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(PackCommand(result.Content, unitNumber));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = ResponseValidAnalysis(result2.Content, false);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        public byte UnitNumber { get; set; }
    }
}

