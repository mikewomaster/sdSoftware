namespace HslCommunication.Profinet.Siemens
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Serial;
    using System;

    public class SiemensMPI : SerialDeviceBase
    {
        private byte[] readConfirm = new byte[] { 0x68, 8, 8, 0x68, 130, 0x80, 0x5c, 0x16, 2, 0xb0, 7, 0, 0x2d, 0x16, 0xe5 };
        private byte station = 2;
        private byte[] writeConfirm = new byte[] { 0x68, 8, 8, 0x68, 130, 0x80, 0x7c, 0x16, 2, 0xb0, 7, 0, 0x4d, 0x16, 0xe5 };

        public SiemensMPI()
        {
            base.ByteTransform = new ReverseBytesTransform();
            base.WordLength = 2;
        }

        public static OperateResult<byte[]> BuildReadCommand(byte station, string address, ushort length, bool isBit)
        {
            OperateResult<S7AddressData> result = S7AddressData.ParseFrom(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] buffer = new byte[0x26];
            buffer[0] = 0x68;
            buffer[1] = BitConverter.GetBytes((int) (buffer.Length - 6))[0];
            buffer[2] = BitConverter.GetBytes((int) (buffer.Length - 6))[0];
            buffer[3] = 0x68;
            buffer[4] = (byte) (station + 0x80);
            buffer[5] = 0x80;
            buffer[6] = 0x7c;
            buffer[7] = 0x16;
            buffer[8] = 1;
            buffer[9] = 0xf1;
            buffer[10] = 0;
            buffer[11] = 50;
            buffer[12] = 1;
            buffer[13] = 0;
            buffer[14] = 0;
            buffer[15] = 0x33;
            buffer[0x10] = 2;
            buffer[0x11] = 0;
            buffer[0x12] = 14;
            buffer[0x13] = 0;
            buffer[20] = 0;
            buffer[0x15] = 4;
            buffer[0x16] = 1;
            buffer[0x17] = 0x12;
            buffer[0x18] = 10;
            buffer[0x19] = 0x10;
            buffer[0x1a] = isBit ? ((byte) 1) : ((byte) 2);
            buffer[0x1b] = BitConverter.GetBytes(length)[1];
            buffer[0x1c] = BitConverter.GetBytes(length)[0];
            buffer[0x1d] = BitConverter.GetBytes(result.Content.DbBlock)[1];
            buffer[30] = BitConverter.GetBytes(result.Content.DbBlock)[0];
            buffer[0x1f] = result.Content.DataCode;
            buffer[0x20] = BitConverter.GetBytes(result.Content.AddressStart)[2];
            buffer[0x21] = BitConverter.GetBytes(result.Content.AddressStart)[1];
            buffer[0x22] = BitConverter.GetBytes(result.Content.AddressStart)[0];
            int num = 0;
            for (int i = 4; i < 0x23; i++)
            {
                num += buffer[i];
            }
            buffer[0x23] = BitConverter.GetBytes(num)[0];
            buffer[0x24] = 0x16;
            buffer[0x25] = 0xe5;
            return OperateResult.CreateSuccessResult<byte[]>(buffer);
        }

        public static OperateResult<byte[]> BuildWriteCommand(byte station, string address, byte[] values)
        {
            OperateResult<S7AddressData> result = S7AddressData.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            int length = values.Length;
            byte[] array = new byte[0x2a + values.Length];
            array[0] = 0x68;
            array[1] = BitConverter.GetBytes((int) (array.Length - 6))[0];
            array[2] = BitConverter.GetBytes((int) (array.Length - 6))[0];
            array[3] = 0x68;
            array[4] = (byte) (station + 0x80);
            array[5] = 0x80;
            array[6] = 0x5c;
            array[7] = 0x16;
            array[8] = 2;
            array[9] = 0xf1;
            array[10] = 0;
            array[11] = 50;
            array[12] = 1;
            array[13] = 0;
            array[14] = 0;
            array[15] = 0x43;
            array[0x10] = 2;
            array[0x11] = 0;
            array[0x12] = 14;
            array[0x13] = 0;
            array[20] = (byte) (values.Length + 4);
            array[0x15] = 5;
            array[0x16] = 1;
            array[0x17] = 0x12;
            array[0x18] = 10;
            array[0x19] = 0x10;
            array[0x1a] = 2;
            array[0x1b] = BitConverter.GetBytes(length)[0];
            array[0x1c] = BitConverter.GetBytes(length)[1];
            array[0x1d] = BitConverter.GetBytes(result.Content.DbBlock)[0];
            array[30] = BitConverter.GetBytes(result.Content.DbBlock)[1];
            array[0x1f] = result.Content.DataCode;
            array[0x20] = BitConverter.GetBytes(result.Content.AddressStart)[2];
            array[0x21] = BitConverter.GetBytes(result.Content.AddressStart)[1];
            array[0x22] = BitConverter.GetBytes(result.Content.AddressStart)[0];
            array[0x23] = 0;
            array[0x24] = 4;
            array[0x25] = BitConverter.GetBytes((int) (length * 8))[1];
            array[0x26] = BitConverter.GetBytes((int) (length * 8))[0];
            values.CopyTo(array, 0x27);
            int num2 = 0;
            for (int i = 4; i < (array.Length - 3); i++)
            {
                num2 += array[i];
            }
            array[array.Length - 3] = BitConverter.GetBytes(num2)[0];
            array[array.Length - 2] = 0x16;
            array[array.Length - 1] = 0xe5;
            return OperateResult.CreateSuccessResult<byte[]>(array);
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

        public OperateResult Handle()
        {
            while (true)
            {
                OperateResult<byte[]> result = this.SPReceived(base.sP_ReadData, true);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result);
                }
                if (((result.Content[0] == 220) && (result.Content[1] == 2)) && (result.Content[2] == 2))
                {
                    byte[] data = new byte[3];
                    data[0] = 220;
                    OperateResult result3 = this.SPSend(base.sP_ReadData, data);
                    if (!result3.IsSuccess)
                    {
                        return OperateResult.CreateFailedResult<byte[]>(result3);
                    }
                }
                else if (((result.Content[0] == 220) && (result.Content[1] == null)) && (result.Content[2] == 2))
                {
                    byte[] buffer2 = new byte[3];
                    buffer2[0] = 220;
                    buffer2[1] = 2;
                    OperateResult result4 = this.SPSend(base.sP_ReadData, buffer2);
                    if (!result4.IsSuccess)
                    {
                        return OperateResult.CreateFailedResult<byte[]>(result4);
                    }
                    return OperateResult.CreateSuccessResult();
                }
            }
        }

        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(this.station, address, length, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            if (base.IsClearCacheBeforeRead)
            {
                base.ClearSerialCache();
            }
            OperateResult result2 = this.SPSend(base.sP_ReadData, result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = this.SPReceived(base.sP_ReadData, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            if (result3.Content[14] != 0xe5)
            {
                return new OperateResult<byte[]>("PLC Receive Check Failed:" + SoftBasic.ByteToHexString(result3.Content));
            }
            result3 = this.SPReceived(base.sP_ReadData, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            if (result3.Content[0x13] > 0)
            {
                return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result3.Content[0x13]).ToString());
            }
            result2 = this.SPSend(base.sP_ReadData, this.readConfirm);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            byte[] destinationArray = new byte[length];
            if ((result3.Content[0x19] == 0xff) && (result3.Content[0x1a] == 4))
            {
                Array.Copy(result3.Content, 0x1d, destinationArray, 0, length);
            }
            return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
        }

        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(this.station, address, length, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult result2 = this.SPSend(base.sP_ReadData, result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = this.SPReceived(base.sP_ReadData, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            if (result3.Content[14] != 0xe5)
            {
                return new OperateResult<bool[]>("PLC Receive Check Failed:" + SoftBasic.ByteToHexString(result3.Content));
            }
            result3 = this.SPReceived(base.sP_ReadData, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            if (result3.Content[0x13] > 0)
            {
                return new OperateResult<bool[]>("PLC Receive Check Failed:" + ((byte) result3.Content[0x13]).ToString());
            }
            result2 = this.SPSend(base.sP_ReadData, this.readConfirm);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            byte[] destinationArray = new byte[result3.Content.Length - 0x1f];
            if ((result3.Content[0x15] == 0xff) && (result3.Content[0x16] == 3))
            {
                Array.Copy(result3.Content, 0x1c, destinationArray, 0, destinationArray.Length);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(destinationArray, length));
        }

        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 1));
        }

        public override string ToString()
        {
            return string.Format("SiemensMPI[{0}:{1}]", base.PortName, base.BaudRate);
        }

        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = BuildWriteCommand(this.station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            if (base.IsClearCacheBeforeRead)
            {
                base.ClearSerialCache();
            }
            OperateResult result2 = this.SPSend(base.sP_ReadData, result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = this.SPReceived(base.sP_ReadData, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            if (result3.Content[14] != 0xe5)
            {
                return new OperateResult<byte[]>("PLC Receive Check Failed:" + SoftBasic.ByteToHexString(result3.Content));
            }
            result3 = this.SPReceived(base.sP_ReadData, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            if (result3.Content[0x19] != 0xff)
            {
                return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result3.Content[0x19]).ToString());
            }
            result2 = this.SPSend(base.sP_ReadData, this.writeConfirm);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return OperateResult.CreateSuccessResult();
        }

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
                this.readConfirm[4] = (byte) (value + 0x80);
                this.writeConfirm[4] = (byte) (value + 0x80);
                int num = 0;
                int num2 = 0;
                for (int i = 4; i < 12; i++)
                {
                    num += this.readConfirm[i];
                    num2 += this.writeConfirm[i];
                }
                this.readConfirm[12] = (byte) num;
                this.writeConfirm[12] = (byte) num2;
            }
        }
    }
}

