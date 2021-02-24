namespace HslCommunication.Profinet.AllenBradley
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class AllenBradleySLCNet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint <SessionHandle>k__BackingField;

        public AllenBradleySLCNet()
        {
            base.WordLength = 2;
            base.ByteTransform = new RegularByteTransform();
        }

        public AllenBradleySLCNet(string ipAddress, [Optional, DefaultParameterValue(0xaf12)] int port)
        {
            base.WordLength = 2;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult<byte, byte, ushort> AnalysisAddress(string address)
        {
            if (!address.Contains(":"))
            {
                return new OperateResult<byte, byte, ushort>("Address can't find ':', example : A9:0");
            }
            char[] separator = new char[] { ':' };
            string[] strArray = address.Split(separator);
            try
            {
                OperateResult<byte, byte, ushort> result2 = new OperateResult<byte, byte, ushort>();
                switch (strArray[0][0])
                {
                    case 'A':
                        result2.Content1 = 0x8e;
                        break;

                    case 'B':
                        result2.Content1 = 0x85;
                        break;

                    case 'C':
                        result2.Content1 = 0x87;
                        break;

                    case 'F':
                        result2.Content1 = 0x8a;
                        break;

                    case 'I':
                        result2.Content1 = 0x83;
                        break;

                    case 'N':
                        result2.Content1 = 0x89;
                        break;

                    case 'O':
                        result2.Content1 = 130;
                        break;

                    case 'R':
                        result2.Content1 = 0x88;
                        break;

                    case 'S':
                        result2.Content1 = 0x84;
                        break;

                    case 'T':
                        result2.Content1 = 0x86;
                        break;

                    default:
                        throw new Exception("Address code wrong, must be A,B,N,F,S,C,I,O,R,T");
                }
                if (result2.Content1 == 0x84)
                {
                    result2.Content2 = 2;
                }
                else
                {
                    result2.Content2 = byte.Parse(strArray[0].Substring(1));
                }
                result2.Content3 = ushort.Parse(strArray[1]);
                result2.IsSuccess = true;
                result2.Message = StringResources.Language.SuccessText;
                return result2;
            }
            catch (Exception exception)
            {
                return new OperateResult<byte, byte, ushort>("Wrong Address formate: " + exception.Message);
            }
        }

        public static string AnalysisBitIndex(string address, out int bitIndex)
        {
            bitIndex = 0;
            int index = address.IndexOf('/');
            if (index < 0)
            {
                index = address.IndexOf('.');
            }
            if (index > 0)
            {
                bitIndex = int.Parse(address.Substring(index + 1));
                address = address.Substring(0, index);
            }
            return address;
        }

        public static OperateResult<byte[]> BuildReadCommand(string address, ushort length)
        {
            OperateResult<byte, byte, ushort> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            if (length < 2)
            {
                length = 2;
            }
            if (result.Content1 == 0x8e)
            {
                result.Content3 /= 2;
            }
            byte[] array = new byte[14];
            array[0] = 0;
            array[1] = 5;
            array[2] = 0;
            array[3] = 0;
            array[4] = 15;
            array[5] = 0;
            array[6] = 0;
            array[7] = 1;
            array[8] = 0xa2;
            array[9] = (byte) length;
            array[10] = result.Content2;
            array[11] = result.Content1;
            BitConverter.GetBytes(result.Content3).CopyTo(array, 12);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteCommand(string address, byte[] value)
        {
            OperateResult<byte, byte, ushort> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            if (result.Content1 == 0x8e)
            {
                result.Content3 /= 2;
            }
            byte[] array = new byte[0x12 + value.Length];
            array[0] = 0;
            array[1] = 5;
            array[2] = 0;
            array[3] = 0;
            array[4] = 15;
            array[5] = 0;
            array[6] = 0;
            array[7] = 1;
            array[8] = 0xab;
            array[9] = 0xff;
            array[10] = BitConverter.GetBytes(value.Length)[0];
            array[11] = BitConverter.GetBytes(value.Length)[1];
            array[12] = result.Content2;
            array[13] = result.Content1;
            BitConverter.GetBytes(result.Content3).CopyTo(array, 14);
            array[0x10] = 0xff;
            array[0x11] = 0xff;
            value.CopyTo(array, 0x12);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteCommand(string address, bool value)
        {
            int num;
            address = AnalysisBitIndex(address, out num);
            OperateResult<byte, byte, ushort> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            if (result.Content1 == 0x8e)
            {
                result.Content3 /= 2;
            }
            int num2 = ((int) 1) << num;
            byte[] array = new byte[20];
            array[0] = 0;
            array[1] = 5;
            array[2] = 0;
            array[3] = 0;
            array[4] = 15;
            array[5] = 0;
            array[6] = 0;
            array[7] = 1;
            array[8] = 0xab;
            array[9] = 0xff;
            array[10] = 2;
            array[11] = 0;
            array[12] = result.Content2;
            array[13] = result.Content1;
            BitConverter.GetBytes(result.Content3).CopyTo(array, 14);
            array[0x10] = BitConverter.GetBytes(num2)[0];
            array[0x11] = BitConverter.GetBytes(num2)[1];
            if (value)
            {
                array[0x12] = BitConverter.GetBytes(num2)[0];
                array[0x13] = BitConverter.GetBytes(num2)[1];
            }
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> ExtraActualContent(byte[] content)
        {
            if (content.Length < 0x24)
            {
                return new OperateResult<byte[]>(StringResources.Language.ReceiveDataLengthTooShort + content.ToHexString(' '));
            }
            return OperateResult.CreateSuccessResult<byte[]>(content.RemoveBegin<byte>(0x24));
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new AllenBradleySLCMessage();
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, "01 01 00 00 00 00 00 00 00 00 00 00 00 04 00 05 00 00 00 00 00 00 00 00 00 00 00 00".ToHexBytes(), true, true);
            if (!result.IsSuccess)
            {
                return result;
            }
            this.SessionHandle = base.ByteTransform.TransUInt32(result.Content, 4);
            return OperateResult.CreateSuccessResult();
        }

        private byte[] PackCommand(byte[] coreCmd)
        {
            byte[] array = new byte[0x1c + coreCmd.Length];
            array[0] = 1;
            array[1] = 7;
            array[2] = (byte) (coreCmd.Length / 0x100);
            array[3] = (byte) (coreCmd.Length % 0x100);
            BitConverter.GetBytes(this.SessionHandle).CopyTo(array, 4);
            coreCmd.CopyTo(array, 0x1c);
            return array;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadCommand(address, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(this.PackCommand(result.Content));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = ExtraActualContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult<byte[]>(result3.Content);
        }

        [HslMqttApi("ReadBool", "")]
        public override OperateResult<bool> ReadBool(string address)
        {
            int num;
            address = AnalysisBitIndex(address, out num);
            OperateResult<byte[]> result = this.Read(address, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool>(result);
            }
            return OperateResult.CreateSuccessResult<bool>(result.Content.ToBoolArray()[num]);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = BuildWriteCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(this.PackCommand(result.Content));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = ExtraActualContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult<byte[]>(result3.Content);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<byte[]> result = BuildWriteCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(this.PackCommand(result.Content));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = ExtraActualContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult<byte[]>(result3.Content);
        }

        public uint SessionHandle { get; protected set; }
    }
}

