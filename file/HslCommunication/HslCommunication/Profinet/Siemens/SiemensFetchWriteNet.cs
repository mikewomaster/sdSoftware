namespace HslCommunication.Profinet.Siemens
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;

    public class SiemensFetchWriteNet : NetworkDeviceBase
    {
        public SiemensFetchWriteNet()
        {
            base.WordLength = 2;
            base.ByteTransform = new ReverseBytesTransform();
        }

        public SiemensFetchWriteNet(string ipAddress, int port)
        {
            base.WordLength = 2;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new ReverseBytesTransform();
        }

        private static OperateResult<byte, int, ushort> AnalysisAddress(string address)
        {
            OperateResult<byte, int, ushort> result = new OperateResult<byte, int, ushort>();
            try
            {
                result.Content3 = 0;
                if (address[0] == 'I')
                {
                    result.Content1 = 3;
                    result.Content2 = CalculateAddressStarted(address.Substring(1));
                }
                else if (address[0] == 'Q')
                {
                    result.Content1 = 4;
                    result.Content2 = CalculateAddressStarted(address.Substring(1));
                }
                else if (address[0] == 'M')
                {
                    result.Content1 = 2;
                    result.Content2 = CalculateAddressStarted(address.Substring(1));
                }
                else if ((address[0] == 'D') || (address.Substring(0, 2) == "DB"))
                {
                    result.Content1 = 1;
                    char[] separator = new char[] { '.' };
                    string[] strArray = address.Split(separator);
                    if (address[1] == 'B')
                    {
                        result.Content3 = Convert.ToUInt16(strArray[0].Substring(2));
                    }
                    else
                    {
                        result.Content3 = Convert.ToUInt16(strArray[0].Substring(1));
                    }
                    if (result.Content3 > 0xff)
                    {
                        result.Message = StringResources.Language.SiemensDBAddressNotAllowedLargerThan255;
                        return result;
                    }
                    result.Content2 = CalculateAddressStarted(address.Substring(address.IndexOf('.') + 1));
                }
                else if (address[0] == 'T')
                {
                    result.Content1 = 7;
                    result.Content2 = CalculateAddressStarted(address.Substring(1));
                }
                else if (address[0] == 'C')
                {
                    result.Content1 = 6;
                    result.Content2 = CalculateAddressStarted(address.Substring(1));
                }
                else
                {
                    result.Message = StringResources.Language.NotSupportedDataType;
                    result.Content1 = 0;
                    result.Content2 = 0;
                    result.Content3 = 0;
                    return result;
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

        public static OperateResult<byte[]> BuildReadCommand(string address, ushort count)
        {
            OperateResult<byte, int, ushort> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] buffer = new byte[0x10];
            buffer[0] = 0x53;
            buffer[1] = 0x35;
            buffer[2] = 0x10;
            buffer[3] = 1;
            buffer[4] = 3;
            buffer[5] = 5;
            buffer[6] = 3;
            buffer[7] = 8;
            buffer[8] = result.Content1;
            buffer[9] = (byte) result.Content3;
            buffer[10] = (byte) (result.Content2 / 0x100);
            buffer[11] = (byte) (result.Content2 % 0x100);
            if (((result.Content1 == 1) || (result.Content1 == 6)) || (result.Content1 == 7))
            {
                if ((count % 2) > 0)
                {
                    return new OperateResult<byte[]>(StringResources.Language.SiemensReadLengthMustBeEvenNumber);
                }
                buffer[12] = BitConverter.GetBytes((int) (count / 2))[1];
                buffer[13] = BitConverter.GetBytes((int) (count / 2))[0];
            }
            else
            {
                buffer[12] = BitConverter.GetBytes(count)[1];
                buffer[13] = BitConverter.GetBytes(count)[0];
            }
            buffer[14] = 0xff;
            buffer[15] = 2;
            return OperateResult.CreateSuccessResult<byte[]>(buffer);
        }

        public static OperateResult<byte[]> BuildWriteCommand(string address, byte[] data)
        {
            if (data == null)
            {
                data = new byte[0];
            }
            OperateResult<byte, int, ushort> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] destinationArray = new byte[0x10 + data.Length];
            destinationArray[0] = 0x53;
            destinationArray[1] = 0x35;
            destinationArray[2] = 0x10;
            destinationArray[3] = 1;
            destinationArray[4] = 3;
            destinationArray[5] = 3;
            destinationArray[6] = 3;
            destinationArray[7] = 8;
            destinationArray[8] = result.Content1;
            destinationArray[9] = (byte) result.Content3;
            destinationArray[10] = (byte) (result.Content2 / 0x100);
            destinationArray[11] = (byte) (result.Content2 % 0x100);
            if (((result.Content1 == 1) || (result.Content1 == 6)) || (result.Content1 == 7))
            {
                if ((data.Length % 2) > 0)
                {
                    return new OperateResult<byte[]>(StringResources.Language.SiemensReadLengthMustBeEvenNumber);
                }
                destinationArray[12] = BitConverter.GetBytes((int) (data.Length / 2))[1];
                destinationArray[13] = BitConverter.GetBytes((int) (data.Length / 2))[0];
            }
            else
            {
                destinationArray[12] = BitConverter.GetBytes(data.Length)[1];
                destinationArray[13] = BitConverter.GetBytes(data.Length)[0];
            }
            destinationArray[14] = 0xff;
            destinationArray[15] = 2;
            Array.Copy(data, 0, destinationArray, 0x10, data.Length);
            return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
        }

        private static int CalculateAddressStarted(string address)
        {
            if (address.IndexOf('.') < 0)
            {
                return Convert.ToInt32(address);
            }
            char[] separator = new char[] { '.' };
            return Convert.ToInt32(address.Split(separator)[0]);
        }

        private static OperateResult CheckResponseContent(byte[] content)
        {
            if (content[8] > 0)
            {
                return new OperateResult(content[8], StringResources.Language.SiemensWriteError + content[8].ToString());
            }
            return OperateResult.CreateSuccessResult();
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new FetchWriteMessage();
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(address, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = CheckResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.ArrayRemoveBegin<byte>(result2.Content, 0x10));
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 1));
        }

        public override string ToString()
        {
            return string.Format("SiemensFetchWriteNet[{0}:{1}]", this.IpAddress, this.Port);
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
            OperateResult result3 = CheckResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByte", "")]
        public OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[] { value };
            return this.Write(address, buffer1);
        }
    }
}

