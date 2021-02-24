namespace HslCommunication.Profinet.Fuji
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Text;

    public class FujiSPBOverTcp : NetworkDeviceBase
    {
        private byte station;

        public FujiSPBOverTcp()
        {
            this.station = 1;
            base.WordLength = 1;
            base.LogMsgFormatBinary = false;
            base.ByteTransform = new RegularByteTransform();
        }

        public FujiSPBOverTcp(string ipAddress, int port)
        {
            this.station = 1;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.LogMsgFormatBinary = false;
            base.ByteTransform = new RegularByteTransform();
        }

        public static string AnalysisIntegerAddress(int address)
        {
            string str = address.ToString("D4");
            return (str.Substring(2) + str.Substring(0, 2));
        }

        public static OperateResult<byte[]> BuildReadCommand(byte station, FujiSPBAddress address, ushort length)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(':');
            builder.Append(station.ToString("X2"));
            builder.Append("09");
            builder.Append("FFFF");
            builder.Append("00");
            builder.Append("00");
            builder.Append(address.GetWordAddress());
            builder.Append(AnalysisIntegerAddress(length));
            builder.Append("\r\n");
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult<byte[]> BuildReadCommand(byte station, string address, ushort length)
        {
            station = (byte) HslHelper.ExtractParameter(ref address, "s", station);
            OperateResult<FujiSPBAddress> result = FujiSPBAddress.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return BuildReadCommand(station, result.Content, length);
        }

        public static OperateResult<byte[]> BuildReadCommand(byte station, string[] address, ushort[] length, bool isBool)
        {
            if ((address == null) || (length == null))
            {
                return new OperateResult<byte[]>("Parameter address or length can't be null");
            }
            if (address.Length != length.Length)
            {
                return new OperateResult<byte[]>(StringResources.Language.TwoParametersLengthIsNotSame);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(':');
            builder.Append(station.ToString("X2"));
            builder.Append((6 + (address.Length * 4)).ToString("X2"));
            builder.Append("FFFF");
            builder.Append("00");
            builder.Append("04");
            builder.Append("00");
            builder.Append(address.Length.ToString("X2"));
            for (int i = 0; i < address.Length; i++)
            {
                station = (byte) HslHelper.ExtractParameter(ref address[i], "s", station);
                OperateResult<FujiSPBAddress> result = FujiSPBAddress.ParseFrom(address[i]);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result);
                }
                builder.Append(result.Content.TypeCode);
                builder.Append(length[i].ToString("X2"));
                builder.Append(AnalysisIntegerAddress(result.Content.Address));
            }
            builder[1] = station.ToString("X2")[0];
            builder[2] = station.ToString("X2")[1];
            builder.Append("\r\n");
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult<byte[]> BuildWriteBoolCommand(byte station, string address, bool value)
        {
            station = (byte) HslHelper.ExtractParameter(ref address, "s", station);
            OperateResult<FujiSPBAddress> result = FujiSPBAddress.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            if (((((address.StartsWith("X") || address.StartsWith("Y")) || (address.StartsWith("M") || address.StartsWith("L"))) || address.StartsWith("TC")) || address.StartsWith("CC")) && (address.IndexOf('.') < 0))
            {
                result.Content.BitIndex = result.Content.Address % 0x10;
                result.Content.Address = (ushort) (result.Content.Address / 0x10);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(':');
            builder.Append(station.ToString("X2"));
            builder.Append("00");
            builder.Append("FFFF");
            builder.Append("01");
            builder.Append("02");
            builder.Append(result.Content.GetWriteBoolAddress());
            builder.Append(value ? "01" : "00");
            int num = (builder.Length - 5) / 2;
            builder[3] = num.ToString("X2")[0];
            num = (builder.Length - 5) / 2;
            builder[4] = num.ToString("X2")[1];
            builder.Append("\r\n");
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult<byte[]> BuildWriteByteCommand(byte station, string address, byte[] value)
        {
            station = (byte) HslHelper.ExtractParameter(ref address, "s", station);
            OperateResult<FujiSPBAddress> result = FujiSPBAddress.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(':');
            builder.Append(station.ToString("X2"));
            builder.Append("00");
            builder.Append("FFFF");
            builder.Append("01");
            builder.Append("00");
            builder.Append(result.Content.GetWordAddress());
            builder.Append(AnalysisIntegerAddress(value.Length / 2));
            builder.Append(value.ToHexString());
            int num = (builder.Length - 5) / 2;
            builder[3] = num.ToString("X2")[0];
            num = (builder.Length - 5) / 2;
            builder[4] = num.ToString("X2")[1];
            builder.Append("\r\n");
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

        public static OperateResult<byte[]> CheckResponseData(byte[] content)
        {
            if (content[0] != 0x3a)
            {
                return new OperateResult<byte[]>(content[0], "Read Faild:" + SoftBasic.ByteToHexString(content, ' '));
            }
            string str = Encoding.ASCII.GetString(content, 9, 2);
            if (str != "00")
            {
                return new OperateResult<byte[]>(Convert.ToInt32(str, 0x10), GetErrorDescriptionFromCode(str));
            }
            if ((content[content.Length - 2] == 13) && (content[content.Length - 1] == 10))
            {
                content = content.RemoveLast<byte>(2);
            }
            return OperateResult.CreateSuccessResult<byte[]>(content.RemoveBegin<byte>(11));
        }

        public static string GetErrorDescriptionFromCode(string code)
        {
            string s = code;
            if (s != null)
            {
                switch (<PrivateImplementationDetails>.ComputeStringHash(s))
                {
                    case 0x1bed68db:
                        if (s == "06")
                        {
                            return StringResources.Language.FujiSpbStatus06;
                        }
                        break;

                    case 0x1ced6a6e:
                        if (s == "05")
                        {
                            return StringResources.Language.FujiSpbStatus05;
                        }
                        break;

                    case 0x18ed6422:
                        if (s == "09")
                        {
                            return StringResources.Language.FujiSpbStatus09;
                        }
                        break;

                    case 0x1aed6748:
                        if (s == "07")
                        {
                            return StringResources.Language.FujiSpbStatus07;
                        }
                        break;

                    case 0x1ded6c01:
                        if (s == "04")
                        {
                            return StringResources.Language.FujiSpbStatus04;
                        }
                        break;

                    case 0x1eed6d94:
                        if (s == "03")
                        {
                            return StringResources.Language.FujiSpbStatus03;
                        }
                        break;

                    case 0x1fed6f27:
                        if (s == "02")
                        {
                            return StringResources.Language.FujiSpbStatus02;
                        }
                        break;

                    case 0x20ed70ba:
                        if (s == "01")
                        {
                            return StringResources.Language.FujiSpbStatus01;
                        }
                        break;

                    case 0x6eedeb84:
                        if (s == "0C")
                        {
                            return StringResources.Language.FujiSpbStatus0C;
                        }
                        break;
                }
            }
            return StringResources.Language.UnknownError;
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new FujiSPBMessage();
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(this.station, address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = CheckResponseData(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetString(result3.Content.RemoveBegin<byte>(4)).ToHexBytes());
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.station);
            OperateResult<FujiSPBAddress> result = FujiSPBAddress.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            if (((((address.StartsWith("X") || address.StartsWith("Y")) || (address.StartsWith("M") || address.StartsWith("L"))) || address.StartsWith("TC")) || address.StartsWith("CC")) && (address.IndexOf('.') < 0))
            {
                result.Content.BitIndex = result.Content.Address % 0x10;
                result.Content.Address = (ushort) (result.Content.Address / 0x10);
            }
            ushort num2 = (ushort) (((((result.Content.GetBitIndex() + length) - 1) / 0x10) - (result.Content.GetBitIndex() / 0x10)) + 1);
            OperateResult<byte[]> result2 = BuildReadCommand(station, result.Content, num2);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            OperateResult<byte[]> result4 = CheckResponseData(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return OperateResult.CreateSuccessResult<bool[]>(Encoding.ASCII.GetString(result4.Content.RemoveBegin<byte>(4)).ToHexBytes().ToBoolArray().SelectMiddle<bool>(result.Content.BitIndex, length));
        }

        public override string ToString()
        {
            return string.Format("FujiSPBOverTcp[{0}:{1}]", this.IpAddress, this.Port);
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
            return CheckResponseData(result2.Content);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
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
            return CheckResponseData(result2.Content);
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

