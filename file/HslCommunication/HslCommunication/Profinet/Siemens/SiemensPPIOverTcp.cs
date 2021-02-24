namespace HslCommunication.Profinet.Siemens
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Runtime.InteropServices;

    public class SiemensPPIOverTcp : NetworkDeviceSoloBase
    {
        private object communicationLock;
        private byte station;

        public SiemensPPIOverTcp()
        {
            this.station = 2;
            base.WordLength = 2;
            base.ByteTransform = new ReverseBytesTransform();
            this.communicationLock = new object();
        }

        public SiemensPPIOverTcp(string ipAddress, int port)
        {
            this.station = 2;
            base.WordLength = 2;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new ReverseBytesTransform();
            this.communicationLock = new object();
        }

        public static OperateResult<byte, int, ushort> AnalysisAddress(string address)
        {
            OperateResult<byte, int, ushort> result = new OperateResult<byte, int, ushort>();
            try
            {
                result.Content3 = 0;
                if (address.Substring(0, 2) == "AI")
                {
                    result.Content1 = 6;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(2), false);
                }
                else if (address.Substring(0, 2) == "AQ")
                {
                    result.Content1 = 7;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(2), false);
                }
                else if (address[0] == 'T')
                {
                    result.Content1 = 0x1f;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(1), false);
                }
                else if (address[0] == 'C')
                {
                    result.Content1 = 30;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(1), false);
                }
                else if (address.Substring(0, 2) == "SM")
                {
                    result.Content1 = 5;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(2), false);
                }
                else if (address[0] == 'S')
                {
                    result.Content1 = 4;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(1), false);
                }
                else if (address[0] == 'I')
                {
                    result.Content1 = 0x81;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(1), false);
                }
                else if (address[0] == 'Q')
                {
                    result.Content1 = 130;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(1), false);
                }
                else if (address[0] == 'M')
                {
                    result.Content1 = 0x83;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(1), false);
                }
                else if ((address[0] == 'D') || (address.Substring(0, 2) == "DB"))
                {
                    result.Content1 = 0x84;
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
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(address.IndexOf('.') + 1), false);
                }
                else if (address[0] == 'V')
                {
                    result.Content1 = 0x84;
                    result.Content3 = 1;
                    result.Content2 = S7AddressData.CalculateAddressStarted(address.Substring(1), false);
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

        public static OperateResult<byte[]> BuildReadCommand(byte station, string address, ushort length, bool isBit)
        {
            OperateResult<byte, int, ushort> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] buffer = new byte[0x21];
            buffer[0] = 0x68;
            buffer[1] = BitConverter.GetBytes((int) (buffer.Length - 6))[0];
            buffer[2] = BitConverter.GetBytes((int) (buffer.Length - 6))[0];
            buffer[3] = 0x68;
            buffer[4] = station;
            buffer[5] = 0;
            buffer[6] = 0x6c;
            buffer[7] = 50;
            buffer[8] = 1;
            buffer[9] = 0;
            buffer[10] = 0;
            buffer[11] = 0;
            buffer[12] = 0;
            buffer[13] = 0;
            buffer[14] = 14;
            buffer[15] = 0;
            buffer[0x10] = 0;
            buffer[0x11] = 4;
            buffer[0x12] = 1;
            buffer[0x13] = 0x12;
            buffer[20] = 10;
            buffer[0x15] = 0x10;
            buffer[0x16] = isBit ? ((byte) 1) : ((byte) 2);
            buffer[0x17] = 0;
            buffer[0x18] = BitConverter.GetBytes(length)[0];
            buffer[0x19] = BitConverter.GetBytes(length)[1];
            buffer[0x1a] = (byte) result.Content3;
            buffer[0x1b] = result.Content1;
            buffer[0x1c] = BitConverter.GetBytes(result.Content2)[2];
            buffer[0x1d] = BitConverter.GetBytes(result.Content2)[1];
            buffer[30] = BitConverter.GetBytes(result.Content2)[0];
            int num = 0;
            for (int i = 4; i < 0x1f; i++)
            {
                num += buffer[i];
            }
            buffer[0x1f] = BitConverter.GetBytes(num)[0];
            buffer[0x20] = 0x16;
            return OperateResult.CreateSuccessResult<byte[]>(buffer);
        }

        public static OperateResult<byte[]> BuildWriteCommand(byte station, string address, bool[] values)
        {
            OperateResult<byte, int, ushort> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] buffer = SoftBasic.BoolArrayToByte(values);
            byte[] array = new byte[0x25 + buffer.Length];
            array[0] = 0x68;
            array[1] = BitConverter.GetBytes((int) (array.Length - 6))[0];
            array[2] = BitConverter.GetBytes((int) (array.Length - 6))[0];
            array[3] = 0x68;
            array[4] = station;
            array[5] = 0;
            array[6] = 0x7c;
            array[7] = 50;
            array[8] = 1;
            array[9] = 0;
            array[10] = 0;
            array[11] = 0;
            array[12] = 0;
            array[13] = 0;
            array[14] = 14;
            array[15] = 0;
            array[0x10] = 5;
            array[0x11] = 5;
            array[0x12] = 1;
            array[0x13] = 0x12;
            array[20] = 10;
            array[0x15] = 0x10;
            array[0x16] = 1;
            array[0x17] = 0;
            array[0x18] = BitConverter.GetBytes(values.Length)[0];
            array[0x19] = BitConverter.GetBytes(values.Length)[1];
            array[0x1a] = (byte) result.Content3;
            array[0x1b] = result.Content1;
            array[0x1c] = BitConverter.GetBytes(result.Content2)[2];
            array[0x1d] = BitConverter.GetBytes(result.Content2)[1];
            array[30] = BitConverter.GetBytes(result.Content2)[0];
            array[0x1f] = 0;
            array[0x20] = 3;
            array[0x21] = BitConverter.GetBytes(values.Length)[1];
            array[0x22] = BitConverter.GetBytes(values.Length)[0];
            buffer.CopyTo(array, 0x23);
            int num = 0;
            for (int i = 4; i < (array.Length - 2); i++)
            {
                num += array[i];
            }
            array[array.Length - 2] = BitConverter.GetBytes(num)[0];
            array[array.Length - 1] = 0x16;
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteCommand(byte station, string address, byte[] values)
        {
            OperateResult<byte, int, ushort> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            int length = values.Length;
            byte[] array = new byte[0x25 + values.Length];
            array[0] = 0x68;
            array[1] = BitConverter.GetBytes((int) (array.Length - 6))[0];
            array[2] = BitConverter.GetBytes((int) (array.Length - 6))[0];
            array[3] = 0x68;
            array[4] = station;
            array[5] = 0;
            array[6] = 0x7c;
            array[7] = 50;
            array[8] = 1;
            array[9] = 0;
            array[10] = 0;
            array[11] = 0;
            array[12] = 0;
            array[13] = 0;
            array[14] = 14;
            array[15] = 0;
            array[0x10] = (byte) (values.Length + 4);
            array[0x11] = 5;
            array[0x12] = 1;
            array[0x13] = 0x12;
            array[20] = 10;
            array[0x15] = 0x10;
            array[0x16] = 2;
            array[0x17] = 0;
            array[0x18] = BitConverter.GetBytes(length)[0];
            array[0x19] = BitConverter.GetBytes(length)[1];
            array[0x1a] = (byte) result.Content3;
            array[0x1b] = result.Content1;
            array[0x1c] = BitConverter.GetBytes(result.Content2)[2];
            array[0x1d] = BitConverter.GetBytes(result.Content2)[1];
            array[30] = BitConverter.GetBytes(result.Content2)[0];
            array[0x1f] = 0;
            array[0x20] = 4;
            array[0x21] = BitConverter.GetBytes((int) (length * 8))[1];
            array[0x22] = BitConverter.GetBytes((int) (length * 8))[0];
            values.CopyTo(array, 0x23);
            int num2 = 0;
            for (int i = 4; i < (array.Length - 2); i++)
            {
                num2 += array[i];
            }
            array[array.Length - 2] = BitConverter.GetBytes(num2)[0];
            array[array.Length - 1] = 0x16;
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult CheckResponse(byte[] content)
        {
            if (content.Length < 0x15)
            {
                return new OperateResult(0x2710, "Failed, data too short:" + SoftBasic.ByteToHexString(content, ' '));
            }
            if ((content[0x11] != 0) || (content[0x12] > 0))
            {
                return new OperateResult(content[0x13], GetMsgFromStatus(content[0x12], content[0x13]));
            }
            if (content[0x15] != 0xff)
            {
                return new OperateResult(content[0x15], GetMsgFromStatus(content[0x15]));
            }
            return OperateResult.CreateSuccessResult();
        }

        public static byte[] GetExecuteConfirm(byte station)
        {
            byte[] buffer = new byte[] { 0x10, 2, 0, 0x5c, 0x5e, 0x16 };
            buffer[1] = station;
            int num = 0;
            for (int i = 1; i < 4; i++)
            {
                num += buffer[i];
            }
            buffer[4] = (byte) num;
            return buffer;
        }

        public static string GetMsgFromStatus(byte code)
        {
            switch (code)
            {
                case 1:
                    return "Hardware fault";

                case 3:
                    return "Illegal object access";

                case 5:
                    return "Invalid address(incorrent variable address)";

                case 6:
                    return "Data type is not supported";

                case 10:
                    return "Object does not exist or length error";

                case 0xff:
                    return "No error";
            }
            return StringResources.Language.UnknownError;
        }

        public static string GetMsgFromStatus(byte errorClass, byte errorCode)
        {
            if ((errorClass == 0x80) && (errorCode == 1))
            {
                return "Switch in wrong position for requested operation";
            }
            if ((errorClass == 0x81) && (errorCode == 4))
            {
                return "Miscellaneous structure error in command.  Command is not supportedby CPU";
            }
            if ((errorClass == 0x84) && (errorCode == 4))
            {
                return "CPU is busy processing an upload or download CPU cannot process command because of system fault condition";
            }
            if ((errorClass == 0x85) && (errorCode == 0))
            {
                return "Length fields are not correct or do not agree with the amount of data received";
            }
            if (errorClass == 210)
            {
                return "Error in upload or download command";
            }
            if (errorClass == 0xd6)
            {
                return "Protection error(password)";
            }
            if ((errorClass == 220) && (errorCode == 1))
            {
                return "Error in time-of-day clock data";
            }
            return StringResources.Language.UnknownError;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = BuildReadCommand(station, address, length, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result3 = base.ReadFromCoreServer(result.Content);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                if (result3.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + SoftBasic.ByteToHexString(result3.Content, ' '));
                }
                OperateResult<byte[]> result4 = base.ReadFromCoreServer(GetExecuteConfirm(station));
                if (!result4.IsSuccess)
                {
                    return result4;
                }
                OperateResult result5 = CheckResponse(result4.Content);
                if (!result5.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result5);
                }
                byte[] destinationArray = new byte[length];
                if ((result4.Content[0x15] == 0xff) && (result4.Content[0x16] == 4))
                {
                    Array.Copy(result4.Content, 0x19, destinationArray, 0, length);
                }
                return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
            }
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = BuildReadCommand(station, address, length, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result3 = base.ReadFromCoreServer(result.Content);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result3);
                }
                if (result3.Content[0] != 0xe5)
                {
                    return new OperateResult<bool[]>("PLC Receive Check Failed:" + SoftBasic.ByteToHexString(result3.Content, ' '));
                }
                OperateResult<byte[]> result4 = base.ReadFromCoreServer(GetExecuteConfirm(station));
                if (!result4.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result4);
                }
                OperateResult result5 = CheckResponse(result4.Content);
                if (!result5.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result5);
                }
                byte[] destinationArray = new byte[result4.Content.Length - 0x1b];
                if ((result4.Content[0x15] == 0xff) && (result4.Content[0x16] == 3))
                {
                    Array.Copy(result4.Content, 0x19, destinationArray, 0, destinationArray.Length);
                }
                return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(destinationArray, length));
            }
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 1));
        }

        [HslMqttApi]
        public OperateResult Start([Optional, DefaultParameterValue("")] string parameter)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref parameter, "s", this.Station);
            byte[] buffer1 = new byte[] { 
                0x68, 0x21, 0x21, 0x68, 0, 0, 0x6c, 50, 1, 0, 0, 0, 0, 0, 20, 0,
                0, 40, 0, 0, 0, 0, 0, 0, 0xfd, 0, 0, 9, 80, 0x5f, 80, 0x52,
                0x4f, 0x47, 0x52, 0x41, 0x4d, 170, 0x16
            };
            buffer1[4] = this.station;
            byte[] send = buffer1;
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result = base.ReadFromCoreServer(send);
                if (!result.IsSuccess)
                {
                    return result;
                }
                if (result.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result.Content[0]).ToString());
                }
                OperateResult<byte[]> result2 = base.ReadFromCoreServer(GetExecuteConfirm(station));
                if (!result2.IsSuccess)
                {
                    return result2;
                }
                return OperateResult.CreateSuccessResult();
            }
        }

        [HslMqttApi]
        public OperateResult Stop([Optional, DefaultParameterValue("")] string parameter)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref parameter, "s", this.Station);
            byte[] buffer1 = new byte[] { 
                0x68, 0x1d, 0x1d, 0x68, 0, 0, 0x6c, 50, 1, 0, 0, 0, 0, 0, 0x10, 0,
                0, 0x29, 0, 0, 0, 0, 0, 9, 80, 0x5f, 80, 0x52, 0x4f, 0x47, 0x52, 0x41,
                0x4d, 170, 0x16
            };
            buffer1[4] = this.station;
            byte[] send = buffer1;
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result = base.ReadFromCoreServer(send);
                if (!result.IsSuccess)
                {
                    return result;
                }
                if (result.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result.Content[0]).ToString());
                }
                OperateResult<byte[]> result2 = base.ReadFromCoreServer(GetExecuteConfirm(station));
                if (!result2.IsSuccess)
                {
                    return result2;
                }
                return OperateResult.CreateSuccessResult();
            }
        }

        public override string ToString()
        {
            return string.Format("SiemensPPIOverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = BuildWriteCommand(station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result3 = base.ReadFromCoreServer(result.Content);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                if (result3.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result3.Content[0]).ToString());
                }
                OperateResult<byte[]> result4 = base.ReadFromCoreServer(GetExecuteConfirm(station));
                if (!result4.IsSuccess)
                {
                    return result4;
                }
                OperateResult result5 = CheckResponse(result4.Content);
                if (!result5.IsSuccess)
                {
                    return result5;
                }
                return OperateResult.CreateSuccessResult();
            }
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = BuildWriteCommand(station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result3 = base.ReadFromCoreServer(result.Content);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                if (result3.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result3.Content[0]).ToString());
                }
                OperateResult<byte[]> result4 = base.ReadFromCoreServer(GetExecuteConfirm(station));
                if (!result4.IsSuccess)
                {
                    return result4;
                }
                OperateResult result5 = CheckResponse(result4.Content);
                if (!result5.IsSuccess)
                {
                    return result5;
                }
                return OperateResult.CreateSuccessResult();
            }
        }

        [HslMqttApi("WriteByte", "")]
        public OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[] { value };
            return this.Write(address, buffer1);
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

