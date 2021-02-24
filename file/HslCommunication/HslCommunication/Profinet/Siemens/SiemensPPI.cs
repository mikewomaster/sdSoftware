namespace HslCommunication.Profinet.Siemens
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Runtime.InteropServices;

    public class SiemensPPI : SerialDeviceBase
    {
        private object communicationLock;
        private byte station = 2;

        public SiemensPPI()
        {
            base.ByteTransform = new ReverseBytesTransform();
            base.WordLength = 2;
            this.communicationLock = new object();
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = SiemensPPIOverTcp.BuildReadCommand(station, address, length, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result3 = base.ReadBase(result.Content);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                if (result3.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + SoftBasic.ByteToHexString(result3.Content, ' '));
                }
                OperateResult<byte[]> result4 = base.ReadBase(SiemensPPIOverTcp.GetExecuteConfirm(station));
                if (!result4.IsSuccess)
                {
                    return result4;
                }
                OperateResult result5 = SiemensPPIOverTcp.CheckResponse(result4.Content);
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
            OperateResult<byte[]> result = SiemensPPIOverTcp.BuildReadCommand(station, address, length, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result3 = base.ReadBase(result.Content);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result3);
                }
                if (result3.Content[0] != 0xe5)
                {
                    return new OperateResult<bool[]>("PLC Receive Check Failed:" + SoftBasic.ByteToHexString(result3.Content, ' '));
                }
                OperateResult<byte[]> result4 = base.ReadBase(SiemensPPIOverTcp.GetExecuteConfirm(station));
                if (!result4.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result4);
                }
                OperateResult result5 = SiemensPPIOverTcp.CheckResponse(result4.Content);
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
            buffer1[4] = station;
            byte[] send = buffer1;
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result = base.ReadBase(send);
                if (!result.IsSuccess)
                {
                    return result;
                }
                if (result.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result.Content[0]).ToString());
                }
                OperateResult<byte[]> result2 = base.ReadBase(SiemensPPIOverTcp.GetExecuteConfirm(station));
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
            buffer1[4] = station;
            byte[] send = buffer1;
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result = base.ReadBase(send);
                if (!result.IsSuccess)
                {
                    return result;
                }
                if (result.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result.Content[0]).ToString());
                }
                OperateResult<byte[]> result2 = base.ReadBase(SiemensPPIOverTcp.GetExecuteConfirm(station));
                if (!result2.IsSuccess)
                {
                    return result2;
                }
                return OperateResult.CreateSuccessResult();
            }
        }

        public override string ToString()
        {
            return string.Format("SiemensPPI[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.Station);
            OperateResult<byte[]> result = SiemensPPIOverTcp.BuildWriteCommand(station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result3 = base.ReadBase(result.Content);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                if (result3.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result3.Content[0]).ToString());
                }
                OperateResult<byte[]> result4 = base.ReadBase(SiemensPPIOverTcp.GetExecuteConfirm(station));
                if (!result4.IsSuccess)
                {
                    return result4;
                }
                OperateResult result5 = SiemensPPIOverTcp.CheckResponse(result4.Content);
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
            OperateResult<byte[]> result = SiemensPPIOverTcp.BuildWriteCommand(station, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            object communicationLock = this.communicationLock;
            lock (communicationLock)
            {
                OperateResult<byte[]> result3 = base.ReadBase(result.Content);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                if (result3.Content[0] != 0xe5)
                {
                    return new OperateResult<byte[]>("PLC Receive Check Failed:" + ((byte) result3.Content[0]).ToString());
                }
                OperateResult<byte[]> result4 = base.ReadBase(SiemensPPIOverTcp.GetExecuteConfirm(station));
                if (!result4.IsSuccess)
                {
                    return result4;
                }
                OperateResult result5 = SiemensPPIOverTcp.CheckResponse(result4.Content);
                if (!result5.IsSuccess)
                {
                    return result5;
                }
                return OperateResult.CreateSuccessResult();
            }
        }

        [HslMqttApi("Write", "")]
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

