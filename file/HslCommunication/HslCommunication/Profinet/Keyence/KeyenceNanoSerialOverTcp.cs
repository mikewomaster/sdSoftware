namespace HslCommunication.Profinet.Keyence
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Linq;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class KeyenceNanoSerialOverTcp : NetworkDeviceSoloBase
    {
        public static byte[] ConnectCmd = new byte[] { 0x43, 0x52, 13 };
        public static byte[] DisConnectCmd = new byte[] { 0x43, 0x51, 13 };

        public KeyenceNanoSerialOverTcp()
        {
            base.WordLength = 1;
            base.ByteTransform = new RegularByteTransform();
        }

        public KeyenceNanoSerialOverTcp(string ipAddress, int port)
        {
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult<byte[]> BuildReadCommand(string address, ushort length)
        {
            OperateResult<string, int> result = KvAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            if (((((result.Content1 == "CTH") || (result.Content1 == "CTC")) || (result.Content1 == "C")) || (result.Content1 == "T")) && (length > 1))
            {
                length = (ushort) (length / 2);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("RDS");
            builder.Append(" ");
            builder.Append(result.Content1);
            builder.Append(result.Content2.ToString());
            builder.Append(" ");
            builder.Append(length.ToString());
            builder.Append("\r");
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult<byte[]> BuildWriteCommand(string address, byte[] value)
        {
            OperateResult<string, int> result = KvAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("WRS");
            builder.Append(" ");
            builder.Append(result.Content1);
            builder.Append(result.Content2);
            builder.Append(" ");
            if (((((result.Content1 == "DM") || (result.Content1 == "CM")) || ((result.Content1 == "TM") || (result.Content1 == "EM"))) || (result.Content1 == "FM")) || (result.Content1 == "Z"))
            {
                int num = value.Length / 2;
                builder.Append(num.ToString());
                builder.Append(" ");
                for (int i = 0; i < num; i++)
                {
                    builder.Append(BitConverter.ToUInt16(value, i * 2));
                    if (i != (num - 1))
                    {
                        builder.Append(" ");
                    }
                }
            }
            else if (((result.Content1 == "T") || (result.Content1 == "C")) || (result.Content1 == "CTH"))
            {
                int num3 = value.Length / 4;
                builder.Append(num3.ToString());
                builder.Append(" ");
                for (int j = 0; j < num3; j++)
                {
                    builder.Append(BitConverter.ToUInt32(value, j * 4));
                    if (j != (num3 - 1))
                    {
                        builder.Append(" ");
                    }
                }
            }
            builder.Append("\r");
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult<byte[]> BuildWriteCommand(string address, bool value)
        {
            OperateResult<string, int> result = KvAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder builder = new StringBuilder();
            if (value)
            {
                builder.Append("ST");
            }
            else
            {
                builder.Append("RS");
            }
            builder.Append(" ");
            builder.Append(result.Content1);
            builder.Append(result.Content2);
            builder.Append("\r");
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        public static OperateResult CheckPlcReadResponse(byte[] ack)
        {
            if (ack.Length == 0)
            {
                return new OperateResult(StringResources.Language.MelsecFxReceiveZero);
            }
            if (ack[0] == 0x45)
            {
                return new OperateResult(StringResources.Language.MelsecFxAckWrong + " Actual: " + Encoding.ASCII.GetString(ack));
            }
            if ((ack[ack.Length - 1] != 10) && (ack[ack.Length - 2] != 13))
            {
                return new OperateResult(StringResources.Language.MelsecFxAckWrong + " Actual: " + SoftBasic.ByteToHexString(ack, ' '));
            }
            return OperateResult.CreateSuccessResult();
        }

        public static OperateResult CheckPlcWriteResponse(byte[] ack)
        {
            if (ack.Length == 0)
            {
                return new OperateResult(StringResources.Language.MelsecFxReceiveZero);
            }
            if ((ack[0] == 0x4f) && (ack[1] == 0x4b))
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult(StringResources.Language.MelsecFxAckWrong + " Actual: " + SoftBasic.ByteToHexString(ack, ' '));
        }

        public static OperateResult<bool[]> ExtractActualBoolData(string addressType, byte[] response)
        {
            try
            {
                if (string.IsNullOrEmpty(addressType))
                {
                    addressType = "R";
                }
                string str = Encoding.Default.GetString(response.RemoveLast<byte>(2));
                if ((((addressType == "R") || (addressType == "CR")) || (addressType == "MR")) || (addressType == "LR"))
                {
                    char[] separator = new char[] { ' ' };
                    return OperateResult.CreateSuccessResult<bool[]>((from m in str.Split(separator, StringSplitOptions.RemoveEmptyEntries) select m == "1").ToArray<bool>());
                }
                if ((((addressType == "T") || (addressType == "C")) || (addressType == "CTH")) || (addressType == "CTC"))
                {
                    char[] chArray2 = new char[] { ' ' };
                    return OperateResult.CreateSuccessResult<bool[]>((from m in str.Split(chArray2, StringSplitOptions.RemoveEmptyEntries) select m.StartsWith("1")).ToArray<bool>());
                }
                return new OperateResult<bool[]>(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                OperateResult<bool[]> result2 = new OperateResult<bool[]>();
                string[] textArray1 = new string[] { "Extract Msg：", exception.Message, Environment.NewLine, "Data: ", SoftBasic.ByteToHexString(response) };
                result2.Message = string.Concat(textArray1);
                return result2;
            }
        }

        public static OperateResult<byte[]> ExtractActualData(string addressType, byte[] response)
        {
            try
            {
                if (string.IsNullOrEmpty(addressType))
                {
                    addressType = "R";
                }
                char[] separator = new char[] { ' ' };
                string[] strArray = Encoding.Default.GetString(response.RemoveLast<byte>(2)).Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (((((addressType == "DM") || (addressType == "CM")) || ((addressType == "TM") || (addressType == "EM"))) || (addressType == "FM")) || (addressType == "Z"))
                {
                    byte[] array = new byte[strArray.Length * 2];
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        BitConverter.GetBytes(ushort.Parse(strArray[i])).CopyTo(array, (int) (i * 2));
                    }
                    return OperateResult.CreateSuccessResult<byte[]>(array);
                }
                if (addressType == "AT")
                {
                    byte[] buffer2 = new byte[strArray.Length * 4];
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        BitConverter.GetBytes(uint.Parse(strArray[j])).CopyTo(buffer2, (int) (j * 4));
                    }
                    return OperateResult.CreateSuccessResult<byte[]>(buffer2);
                }
                if ((((addressType == "T") || (addressType == "C")) || (addressType == "CTH")) || (addressType == "CTC"))
                {
                    byte[] buffer3 = new byte[strArray.Length * 4];
                    for (int k = 0; k < strArray.Length; k++)
                    {
                        char[] chArray2 = new char[] { ',' };
                        BitConverter.GetBytes(uint.Parse(strArray[k].Split(chArray2, StringSplitOptions.RemoveEmptyEntries)[1])).CopyTo(buffer3, (int) (k * 4));
                    }
                    return OperateResult.CreateSuccessResult<byte[]>(buffer3);
                }
                return new OperateResult<byte[]>(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                OperateResult<byte[]> result2 = new OperateResult<byte[]>();
                string[] textArray1 = new string[] { "Extract Msg：", exception.Message, Environment.NewLine, "Data: ", SoftBasic.ByteToHexString(response) };
                result2.Message = string.Concat(textArray1);
                return result2;
            }
        }

        protected override OperateResult ExtraOnDisconnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, DisConnectCmd, true, true);
            if (!result.IsSuccess)
            {
                return result;
            }
            return OperateResult.CreateSuccessResult();
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, ConnectCmd, true, true);
            if (!result.IsSuccess)
            {
                return result;
            }
            return OperateResult.CreateSuccessResult();
        }

        public static OperateResult<string, int> KvAnalysisAddress(string address)
        {
            try
            {
                if (address.StartsWith("CTH") || address.StartsWith("cth"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("CTH", int.Parse(address.Substring(3)));
                }
                if (address.StartsWith("CTC") || address.StartsWith("ctc"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("CTC", int.Parse(address.Substring(3)));
                }
                if (address.StartsWith("CR") || address.StartsWith("cr"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("CR", int.Parse(address.Substring(2)));
                }
                if (address.StartsWith("MR") || address.StartsWith("mr"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("MR", int.Parse(address.Substring(2)));
                }
                if (address.StartsWith("LR") || address.StartsWith("lr"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("LR", int.Parse(address.Substring(2)));
                }
                if (address.StartsWith("DM") || address.StartsWith("DM"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("DM", int.Parse(address.Substring(2)));
                }
                if (address.StartsWith("CM") || address.StartsWith("cm"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("CM", int.Parse(address.Substring(2)));
                }
                if (address.StartsWith("TM") || address.StartsWith("tm"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("TM", int.Parse(address.Substring(2)));
                }
                if (address.StartsWith("EM") || address.StartsWith("em"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("EM", int.Parse(address.Substring(2)));
                }
                if (address.StartsWith("FM") || address.StartsWith("fm"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("FM", int.Parse(address.Substring(2)));
                }
                if (address.StartsWith("AT") || address.StartsWith("at"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("AT", int.Parse(address.Substring(2)));
                }
                if (address.StartsWith("Z") || address.StartsWith("z"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("Z", int.Parse(address.Substring(1)));
                }
                if (address.StartsWith("R") || address.StartsWith("r"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("", int.Parse(address.Substring(1)));
                }
                if (address.StartsWith("T") || address.StartsWith("t"))
                {
                    return OperateResult.CreateSuccessResult<string, int>("T", int.Parse(address.Substring(1)));
                }
                if (!address.StartsWith("C") && !address.StartsWith("c"))
                {
                    throw new Exception(StringResources.Language.NotSupportedDataType);
                }
                return OperateResult.CreateSuccessResult<string, int>("C", int.Parse(address.Substring(1)));
            }
            catch (Exception exception)
            {
                return new OperateResult<string, int>(exception.Message);
            }
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult result3 = CheckPlcReadResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            OperateResult<string, int> result4 = KvAnalysisAddress(address);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result4);
            }
            return ExtractActualData(result4.Content1, result2.Content);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult result3 = CheckPlcReadResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            OperateResult<string, int> result4 = KvAnalysisAddress(address);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return ExtractActualBoolData(result4.Content1, result2.Content);
        }

        public override string ToString()
        {
            return string.Format("KeyenceNanoSerialOverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = BuildWriteCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = CheckPlcWriteResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<byte[]> result = BuildWriteCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = CheckPlcWriteResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly KeyenceNanoSerialOverTcp.<>c <>9 = new KeyenceNanoSerialOverTcp.<>c();
            public static Func<string, bool> <>9__16_0;
            public static Func<string, bool> <>9__16_1;

            internal bool <ExtractActualBoolData>b__16_0(string m)
            {
                return (m == "1");
            }

            internal bool <ExtractActualBoolData>b__16_1(string m)
            {
                return m.StartsWith("1");
            }
        }
    }
}

