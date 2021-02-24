namespace HslCommunication.Profinet.Siemens
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class SiemensS7Net : NetworkDeviceBase
    {
        private SiemensPLCS CurrentPlc;
        private int pdu_length;
        private const byte pduAlreadyStarted = 2;
        private const byte pduAlreadyStopped = 7;
        private const byte pduStart = 40;
        private const byte pduStop = 0x29;
        private byte plc_rack;
        private byte plc_slot;
        private byte[] plcHead1;
        private byte[] plcHead1_200;
        private byte[] plcHead1_200smart;
        private byte[] plcHead2;
        private byte[] plcHead2_200;
        private byte[] plcHead2_200smart;
        private byte[] plcOrderNumber;
        private byte[] S7_COLD_START;
        private byte[] S7_HOT_START;
        private byte[] S7_STOP;

        public SiemensS7Net(SiemensPLCS siemens)
        {
            this.plcHead1 = new byte[] { 
                3, 0, 0, 0x16, 0x11, 0xe0, 0, 0, 0, 1, 0, 0xc0, 1, 10, 0xc1, 2,
                1, 2, 0xc2, 2, 1, 0
            };
            this.plcHead2 = new byte[] { 
                3, 0, 0, 0x19, 2, 240, 0x80, 50, 1, 0, 0, 4, 0, 0, 8, 0,
                0, 240, 0, 0, 1, 0, 1, 1, 0xe0
            };
            this.plcOrderNumber = new byte[] { 
                3, 0, 0, 0x21, 2, 240, 0x80, 50, 7, 0, 0, 0, 1, 0, 8, 0,
                8, 0, 1, 0x12, 4, 0x11, 0x44, 1, 0, 0xff, 9, 0, 4, 0, 0x11, 0,
                0
            };
            this.CurrentPlc = SiemensPLCS.S1200;
            this.plcHead1_200smart = new byte[] { 
                3, 0, 0, 0x16, 0x11, 0xe0, 0, 0, 0, 1, 0, 0xc1, 2, 0x10, 0, 0xc2,
                2, 3, 0, 0xc0, 1, 10
            };
            this.plcHead2_200smart = new byte[] { 
                3, 0, 0, 0x19, 2, 240, 0x80, 50, 1, 0, 0, 0xcc, 0xc1, 0, 8, 0,
                0, 240, 0, 0, 1, 0, 1, 3, 0xc0
            };
            this.plcHead1_200 = new byte[] { 
                3, 0, 0, 0x16, 0x11, 0xe0, 0, 0, 0, 1, 0, 0xc1, 2, 0x4d, 0x57, 0xc2,
                2, 0x4d, 0x57, 0xc0, 1, 9
            };
            this.plcHead2_200 = new byte[] { 
                3, 0, 0, 0x19, 2, 240, 0x80, 50, 1, 0, 0, 0, 0, 0, 8, 0,
                0, 240, 0, 0, 1, 0, 1, 3, 0xc0
            };
            this.S7_STOP = new byte[] { 
                3, 0, 0, 0x21, 2, 240, 0x80, 50, 1, 0, 0, 14, 0, 0, 0x10, 0,
                0, 0x29, 0, 0, 0, 0, 0, 9, 80, 0x5f, 80, 0x52, 0x4f, 0x47, 0x52, 0x41,
                0x4d
            };
            this.S7_HOT_START = new byte[] { 
                3, 0, 0, 0x25, 2, 240, 0x80, 50, 1, 0, 0, 12, 0, 0, 20, 0,
                0, 40, 0, 0, 0, 0, 0, 0, 0xfd, 0, 0, 9, 80, 0x5f, 80, 0x52,
                0x4f, 0x47, 0x52, 0x41, 0x4d
            };
            this.S7_COLD_START = new byte[] { 
                3, 0, 0, 0x27, 2, 240, 0x80, 50, 1, 0, 0, 15, 0, 0, 0x16, 0,
                0, 40, 0, 0, 0, 0, 0, 0, 0xfd, 0, 2, 0x43, 0x20, 9, 80, 0x5f,
                80, 0x52, 0x4f, 0x47, 0x52, 0x41, 0x4d
            };
            this.plc_rack = 0;
            this.plc_slot = 0;
            this.pdu_length = 0;
            this.Initialization(siemens, string.Empty);
        }

        public SiemensS7Net(SiemensPLCS siemens, string ipAddress)
        {
            this.plcHead1 = new byte[] { 
                3, 0, 0, 0x16, 0x11, 0xe0, 0, 0, 0, 1, 0, 0xc0, 1, 10, 0xc1, 2,
                1, 2, 0xc2, 2, 1, 0
            };
            this.plcHead2 = new byte[] { 
                3, 0, 0, 0x19, 2, 240, 0x80, 50, 1, 0, 0, 4, 0, 0, 8, 0,
                0, 240, 0, 0, 1, 0, 1, 1, 0xe0
            };
            this.plcOrderNumber = new byte[] { 
                3, 0, 0, 0x21, 2, 240, 0x80, 50, 7, 0, 0, 0, 1, 0, 8, 0,
                8, 0, 1, 0x12, 4, 0x11, 0x44, 1, 0, 0xff, 9, 0, 4, 0, 0x11, 0,
                0
            };
            this.CurrentPlc = SiemensPLCS.S1200;
            this.plcHead1_200smart = new byte[] { 
                3, 0, 0, 0x16, 0x11, 0xe0, 0, 0, 0, 1, 0, 0xc1, 2, 0x10, 0, 0xc2,
                2, 3, 0, 0xc0, 1, 10
            };
            this.plcHead2_200smart = new byte[] { 
                3, 0, 0, 0x19, 2, 240, 0x80, 50, 1, 0, 0, 0xcc, 0xc1, 0, 8, 0,
                0, 240, 0, 0, 1, 0, 1, 3, 0xc0
            };
            this.plcHead1_200 = new byte[] { 
                3, 0, 0, 0x16, 0x11, 0xe0, 0, 0, 0, 1, 0, 0xc1, 2, 0x4d, 0x57, 0xc2,
                2, 0x4d, 0x57, 0xc0, 1, 9
            };
            this.plcHead2_200 = new byte[] { 
                3, 0, 0, 0x19, 2, 240, 0x80, 50, 1, 0, 0, 0, 0, 0, 8, 0,
                0, 240, 0, 0, 1, 0, 1, 3, 0xc0
            };
            this.S7_STOP = new byte[] { 
                3, 0, 0, 0x21, 2, 240, 0x80, 50, 1, 0, 0, 14, 0, 0, 0x10, 0,
                0, 0x29, 0, 0, 0, 0, 0, 9, 80, 0x5f, 80, 0x52, 0x4f, 0x47, 0x52, 0x41,
                0x4d
            };
            this.S7_HOT_START = new byte[] { 
                3, 0, 0, 0x25, 2, 240, 0x80, 50, 1, 0, 0, 12, 0, 0, 20, 0,
                0, 40, 0, 0, 0, 0, 0, 0, 0xfd, 0, 0, 9, 80, 0x5f, 80, 0x52,
                0x4f, 0x47, 0x52, 0x41, 0x4d
            };
            this.S7_COLD_START = new byte[] { 
                3, 0, 0, 0x27, 2, 240, 0x80, 50, 1, 0, 0, 15, 0, 0, 0x16, 0,
                0, 40, 0, 0, 0, 0, 0, 0, 0xfd, 0, 2, 0x43, 0x20, 9, 80, 0x5f,
                80, 0x52, 0x4f, 0x47, 0x52, 0x41, 0x4d
            };
            this.plc_rack = 0;
            this.plc_slot = 0;
            this.pdu_length = 0;
            this.Initialization(siemens, ipAddress);
        }

        private static OperateResult<byte[]> AnalysisReadBit(byte[] content)
        {
            int num = 1;
            if ((content.Length >= 0x15) && (content[20] == 1))
            {
                byte[] buffer = new byte[num];
                if ((0x16 < content.Length) && ((content[0x15] == 0xff) && (content[0x16] == 3)))
                {
                    buffer[0] = content[0x19];
                }
                return OperateResult.CreateSuccessResult<byte[]>(buffer);
            }
            return new OperateResult<byte[]>(StringResources.Language.SiemensDataLengthCheckFailed);
        }

        private static OperateResult<byte[]> AnalysisReadByte(S7AddressData[] s7Addresses, byte[] content)
        {
            int num = 0;
            for (int i = 0; i < s7Addresses.Length; i++)
            {
                if ((s7Addresses[i].DataCode == 0x1f) || (s7Addresses[i].DataCode == 30))
                {
                    num += s7Addresses[i].Length * 2;
                }
                else
                {
                    num += s7Addresses[i].Length;
                }
            }
            if ((content.Length >= 0x15) && (content[20] == s7Addresses.Length))
            {
                byte[] destinationArray = new byte[num];
                int index = 0;
                int destinationIndex = 0;
                for (int j = 0x15; j < content.Length; j++)
                {
                    if ((j + 1) < content.Length)
                    {
                        if ((content[j] == 0xff) && (content[j + 1] == 4))
                        {
                            Array.Copy(content, j + 4, destinationArray, destinationIndex, s7Addresses[index].Length);
                            j += s7Addresses[index].Length + 3;
                            destinationIndex += s7Addresses[index].Length;
                            index++;
                        }
                        else if ((content[j] == 0xff) && (content[j + 1] == 9))
                        {
                            int num6 = (content[j + 2] * 0x100) + content[j + 3];
                            if ((num6 % 3) == 0)
                            {
                                for (int k = 0; k < (num6 / 3); k++)
                                {
                                    Array.Copy(content, (j + 5) + (3 * k), destinationArray, destinationIndex, 2);
                                    destinationIndex += 2;
                                }
                            }
                            else
                            {
                                for (int m = 0; m < (num6 / 5); m++)
                                {
                                    Array.Copy(content, (j + 7) + (5 * m), destinationArray, destinationIndex, 2);
                                    destinationIndex += 2;
                                }
                            }
                            j += num6 + 4;
                            index++;
                        }
                        else if ((content[j] == 5) && (content[j + 1] == 0))
                        {
                            return new OperateResult<byte[]>(content[j], StringResources.Language.SiemensReadLengthOverPlcAssign);
                        }
                    }
                }
                return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
            }
            return new OperateResult<byte[]>(StringResources.Language.SiemensDataLengthCheckFailed + " Msg:" + SoftBasic.ByteToHexString(content, ' '));
        }

        private static OperateResult AnalysisWrite(byte[] content)
        {
            byte err = content[content.Length - 1];
            if (err != 0xff)
            {
                return new OperateResult(err, StringResources.Language.SiemensWriteError + err.ToString() + " Msg:" + SoftBasic.ByteToHexString(content, ' '));
            }
            return OperateResult.CreateSuccessResult();
        }

        public static OperateResult<byte[]> BuildBitReadCommand(string address)
        {
            byte[] buffer;
            OperateResult<S7AddressData> result = S7AddressData.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return OperateResult.CreateSuccessResult<byte[]>(new byte[] { 
                3, 0, (byte) (buffer.Length / 0x100), (byte) (buffer.Length % 0x100), 2, 240, 0x80, 50, 1, 0, 0, 0, 1, (byte) ((buffer.Length - 0x11) / 0x100), (byte) ((buffer.Length - 0x11) % 0x100), 0,
                0, 4, 1, 0x12, 10, 0x10, 1, 0, 1, (byte) (result.Content.DbBlock / 0x100), (byte) (result.Content.DbBlock % 0x100), result.Content.DataCode, (byte) (((result.Content.AddressStart / 0x100) / 0x100) % 0x100), (byte) ((result.Content.AddressStart / 0x100) % 0x100), (byte) (result.Content.AddressStart % 0x100)
            });
        }

        public static OperateResult<byte[]> BuildReadCommand(S7AddressData[] s7Addresses)
        {
            if (s7Addresses == null)
            {
                throw new NullReferenceException("s7Addresses");
            }
            if (s7Addresses.Length > 0x13)
            {
                throw new Exception(StringResources.Language.SiemensReadLengthCannotLargerThan19);
            }
            int length = s7Addresses.Length;
            byte[] buffer = new byte[0x13 + (length * 12)];
            buffer[0] = 3;
            buffer[1] = 0;
            buffer[2] = (byte) (buffer.Length / 0x100);
            buffer[3] = (byte) (buffer.Length % 0x100);
            buffer[4] = 2;
            buffer[5] = 240;
            buffer[6] = 0x80;
            buffer[7] = 50;
            buffer[8] = 1;
            buffer[9] = 0;
            buffer[10] = 0;
            buffer[11] = 0;
            buffer[12] = 1;
            buffer[13] = (byte) ((buffer.Length - 0x11) / 0x100);
            buffer[14] = (byte) ((buffer.Length - 0x11) % 0x100);
            buffer[15] = 0;
            buffer[0x10] = 0;
            buffer[0x11] = 4;
            buffer[0x12] = (byte) length;
            for (int i = 0; i < length; i++)
            {
                buffer[0x13 + (i * 12)] = 0x12;
                buffer[20 + (i * 12)] = 10;
                buffer[0x15 + (i * 12)] = 0x10;
                if ((s7Addresses[i].DataCode == 30) || (s7Addresses[i].DataCode == 0x1f))
                {
                    buffer[0x16 + (i * 12)] = s7Addresses[i].DataCode;
                    buffer[0x17 + (i * 12)] = (byte) ((s7Addresses[i].Length / 2) / 0x100);
                    buffer[0x18 + (i * 12)] = (byte) ((s7Addresses[i].Length / 2) % 0x100);
                }
                else
                {
                    buffer[0x16 + (i * 12)] = 2;
                    buffer[0x17 + (i * 12)] = (byte) (s7Addresses[i].Length / 0x100);
                    buffer[0x18 + (i * 12)] = (byte) (s7Addresses[i].Length % 0x100);
                }
                buffer[0x19 + (i * 12)] = (byte) (s7Addresses[i].DbBlock / 0x100);
                buffer[0x1a + (i * 12)] = (byte) (s7Addresses[i].DbBlock % 0x100);
                buffer[0x1b + (i * 12)] = s7Addresses[i].DataCode;
                buffer[0x1c + (i * 12)] = (byte) (((s7Addresses[i].AddressStart / 0x100) / 0x100) % 0x100);
                buffer[0x1d + (i * 12)] = (byte) ((s7Addresses[i].AddressStart / 0x100) % 0x100);
                buffer[30 + (i * 12)] = (byte) (s7Addresses[i].AddressStart % 0x100);
            }
            return OperateResult.CreateSuccessResult<byte[]>(buffer);
        }

        public static OperateResult<byte[]> BuildWriteBitCommand(string address, bool data)
        {
            OperateResult<S7AddressData> result = S7AddressData.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] buffer = new byte[] { data ? ((byte) 1) : ((byte) 0) };
            byte[] array = new byte[0x23 + buffer.Length];
            array[0] = 3;
            array[1] = 0;
            array[2] = (byte) ((0x23 + buffer.Length) / 0x100);
            array[3] = (byte) ((0x23 + buffer.Length) % 0x100);
            array[4] = 2;
            array[5] = 240;
            array[6] = 0x80;
            array[7] = 50;
            array[8] = 1;
            array[9] = 0;
            array[10] = 0;
            array[11] = 0;
            array[12] = 1;
            array[13] = 0;
            array[14] = 14;
            array[15] = (byte) ((4 + buffer.Length) / 0x100);
            array[0x10] = (byte) ((4 + buffer.Length) % 0x100);
            array[0x11] = 5;
            array[0x12] = 1;
            array[0x13] = 0x12;
            array[20] = 10;
            array[0x15] = 0x10;
            array[0x16] = 1;
            array[0x17] = (byte) (buffer.Length / 0x100);
            array[0x18] = (byte) (buffer.Length % 0x100);
            array[0x19] = (byte) (result.Content.DbBlock / 0x100);
            array[0x1a] = (byte) (result.Content.DbBlock % 0x100);
            array[0x1b] = result.Content.DataCode;
            array[0x1c] = (byte) ((result.Content.AddressStart / 0x100) / 0x100);
            array[0x1d] = (byte) (result.Content.AddressStart / 0x100);
            array[30] = (byte) (result.Content.AddressStart % 0x100);
            if (result.Content.DataCode == 0x1c)
            {
                array[0x1f] = 0;
                array[0x20] = 9;
            }
            else
            {
                array[0x1f] = 0;
                array[0x20] = 3;
            }
            array[0x21] = (byte) (buffer.Length / 0x100);
            array[0x22] = (byte) (buffer.Length % 0x100);
            buffer.CopyTo(array, 0x23);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteByteCommand(OperateResult<S7AddressData> analysis, byte[] data)
        {
            byte[] array = new byte[0x23 + data.Length];
            array[0] = 3;
            array[1] = 0;
            array[2] = (byte) ((0x23 + data.Length) / 0x100);
            array[3] = (byte) ((0x23 + data.Length) % 0x100);
            array[4] = 2;
            array[5] = 240;
            array[6] = 0x80;
            array[7] = 50;
            array[8] = 1;
            array[9] = 0;
            array[10] = 0;
            array[11] = 0;
            array[12] = 1;
            array[13] = 0;
            array[14] = 14;
            array[15] = (byte) ((4 + data.Length) / 0x100);
            array[0x10] = (byte) ((4 + data.Length) % 0x100);
            array[0x11] = 5;
            array[0x12] = 1;
            array[0x13] = 0x12;
            array[20] = 10;
            array[0x15] = 0x10;
            array[0x16] = 2;
            array[0x17] = (byte) (data.Length / 0x100);
            array[0x18] = (byte) (data.Length % 0x100);
            array[0x19] = (byte) (analysis.Content.DbBlock / 0x100);
            array[0x1a] = (byte) (analysis.Content.DbBlock % 0x100);
            array[0x1b] = analysis.Content.DataCode;
            array[0x1c] = (byte) (((analysis.Content.AddressStart / 0x100) / 0x100) % 0x100);
            array[0x1d] = (byte) ((analysis.Content.AddressStart / 0x100) % 0x100);
            array[30] = (byte) (analysis.Content.AddressStart % 0x100);
            array[0x1f] = 0;
            array[0x20] = 4;
            array[0x21] = (byte) ((data.Length * 8) / 0x100);
            array[0x22] = (byte) ((data.Length * 8) % 0x100);
            data.CopyTo(array, 0x23);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        private OperateResult CheckStartResult(byte[] content)
        {
            if (content.Length < 0x13)
            {
                return new OperateResult("Receive error");
            }
            if (content[0x13] != 40)
            {
                return new OperateResult("Can not start PLC");
            }
            if (content[20] != 2)
            {
                return new OperateResult("Can not start PLC");
            }
            return OperateResult.CreateSuccessResult();
        }

        private OperateResult CheckStopResult(byte[] content)
        {
            if (content.Length < 0x13)
            {
                return new OperateResult("Receive error");
            }
            if (content[0x13] != 0x29)
            {
                return new OperateResult("Can not stop PLC");
            }
            if (content[20] != 7)
            {
                return new OperateResult("Can not stop PLC");
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi]
        public OperateResult ColdStart()
        {
            return ByteTransformHelper.GetResultFromOther<byte[]>(base.ReadFromCoreServer(this.S7_COLD_START), new Func<byte[], OperateResult>(this.CheckStartResult));
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new S7Message();
        }

        [HslMqttApi]
        public OperateResult HotStart()
        {
            return ByteTransformHelper.GetResultFromOther<byte[]>(base.ReadFromCoreServer(this.S7_HOT_START), new Func<byte[], OperateResult>(this.CheckStartResult));
        }

        private void Initialization(SiemensPLCS siemens, string ipAddress)
        {
            base.WordLength = 2;
            this.IpAddress = ipAddress;
            this.Port = 0x66;
            this.CurrentPlc = siemens;
            base.ByteTransform = new ReverseBytesTransform();
            switch (siemens)
            {
                case SiemensPLCS.S1200:
                    this.plcHead1[0x15] = 0;
                    break;

                case SiemensPLCS.S300:
                    this.plcHead1[0x15] = 2;
                    break;

                case SiemensPLCS.S400:
                    this.plcHead1[0x15] = 3;
                    this.plcHead1[0x11] = 0;
                    break;

                case SiemensPLCS.S1500:
                    this.plcHead1[0x15] = 0;
                    break;

                case SiemensPLCS.S200Smart:
                    this.plcHead1 = this.plcHead1_200smart;
                    this.plcHead2 = this.plcHead2_200smart;
                    break;

                case SiemensPLCS.S200:
                    this.plcHead1 = this.plcHead1_200;
                    this.plcHead2 = this.plcHead2_200;
                    break;

                default:
                    this.plcHead1[0x12] = 0;
                    break;
            }
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, this.plcHead1, true, true);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(socket, this.plcHead2, true, true);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            this.pdu_length = base.ByteTransform.TransUInt16(result2.Content.SelectLast<byte>(2), 0) - 0x1c;
            if (this.pdu_length < 200)
            {
                this.pdu_length = 200;
            }
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult<byte[]> Read(S7AddressData[] s7Addresses)
        {
            if (s7Addresses.Length > 0x13)
            {
                List<byte> list = new List<byte>();
                List<S7AddressData[]> list2 = SoftBasic.ArraySplitByLength<S7AddressData>(s7Addresses, 0x13);
                for (int i = 0; i < list2.Count; i++)
                {
                    OperateResult<byte[]> result = this.Read(list2[i]);
                    if (!result.IsSuccess)
                    {
                        return result;
                    }
                    list.AddRange(result.Content);
                }
                return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
            }
            return this.ReadS7AddressData(s7Addresses);
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<S7AddressData> result = S7AddressData.ParseFrom(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte>();
            ushort num = 0;
            while (num < length)
            {
                ushort num2 = (ushort) Math.Min(length - num, this.pdu_length);
                result.Content.Length = num2;
                S7AddressData[] dataArray1 = new S7AddressData[] { result.Content };
                OperateResult<byte[]> result3 = this.Read(dataArray1);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                list.AddRange(result3.Content);
                num = (ushort) (num + num2);
                if ((result.Content.DataCode == 0x1f) || (result.Content.DataCode == 30))
                {
                    S7AddressData content = result.Content;
                    content.AddressStart += num2 / 2;
                }
                else
                {
                    S7AddressData local2 = result.Content;
                    local2.AddressStart += num2 * 8;
                }
            }
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        public OperateResult<byte[]> Read(string[] address, ushort[] length)
        {
            S7AddressData[] dataArray = new S7AddressData[address.Length];
            for (int i = 0; i < address.Length; i++)
            {
                OperateResult<S7AddressData> result = S7AddressData.ParseFrom(address[i], length[i]);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result);
                }
                dataArray[i] = result.Content;
            }
            return this.Read(dataArray);
        }

        private OperateResult<byte[]> ReadBitFromPLC(string address)
        {
            OperateResult<byte[]> result = BuildBitReadCommand(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return AnalysisReadBit(result2.Content);
        }

        [HslMqttApi("ReadBool", "")]
        public override OperateResult<bool> ReadBool(string address)
        {
            return ByteTransformHelper.GetResultFromBytes<bool>(this.ReadBitFromPLC(address), m => m[0] > 0);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            int num;
            ushort num2;
            int num3;
            OperateResult<S7AddressData> result = S7AddressData.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            HslHelper.CalculateStartBitIndexAndLength(result.Content.AddressStart, length, out num, out num2, out num3);
            result.Content.AddressStart = num;
            result.Content.Length = num2;
            S7AddressData[] dataArray1 = new S7AddressData[] { result.Content };
            OperateResult<byte[]> result2 = this.Read(dataArray1);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            return OperateResult.CreateSuccessResult<bool[]>(result2.Content.ToBoolArray().SelectMiddle<bool>(num3, length));
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 1));
        }

        [HslMqttApi("ReadDateTime", "读取PLC的时间格式的数据，这个格式是s7格式的一种")]
        public OperateResult<DateTime> ReadDateTime(string address)
        {
            return ByteTransformHelper.GetResultFromBytes<DateTime>(this.Read(address, 8), new Func<byte[], DateTime>(SiemensDateTime.FromByteArray));
        }

        [HslMqttApi("ReadOrderNumber", "获取到PLC的订货号信息")]
        public OperateResult<string> ReadOrderNumber()
        {
            return ByteTransformHelper.GetSuccessResultFromOther<string, byte[]>(base.ReadFromCoreServer(this.plcOrderNumber), m => Encoding.ASCII.GetString(m, 0x47, 20));
        }

        private OperateResult<byte[]> ReadS7AddressData(S7AddressData[] s7Addresses)
        {
            OperateResult<byte[]> result = BuildReadCommand(s7Addresses);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return AnalysisReadByte(s7Addresses, result2.Content);
        }

        [HslMqttApi("ReadS7String", "读取S7格式的字符串")]
        public OperateResult<string> ReadString(string address)
        {
            if (this.CurrentPlc != SiemensPLCS.S200Smart)
            {
                OperateResult<byte[]> result = this.Read(address, 2);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<string>(result);
                }
                if ((result.Content[0] == null) || (result.Content[0] == 0xff))
                {
                    return new OperateResult<string>("Value in plc is not string type");
                }
                OperateResult<byte[]> result2 = this.Read(address, (ushort) (2 + result.Content[1]));
                if (!result2.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<string>(result2);
                }
                return OperateResult.CreateSuccessResult<string>(Encoding.ASCII.GetString(result2.Content, 2, result2.Content.Length - 2));
            }
            OperateResult<byte[]> result4 = this.Read(address, 1);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result4);
            }
            OperateResult<byte[]> result5 = this.Read(address, (ushort) (1 + result4.Content[0]));
            if (!result5.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result5);
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.ASCII.GetString(result5.Content, 1, result5.Content.Length - 1));
        }

        public override OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            if (length == 0)
            {
                return this.ReadString(address);
            }
            return base.ReadString(address, length, encoding);
        }

        [HslMqttApi]
        public OperateResult Stop()
        {
            return ByteTransformHelper.GetResultFromOther<byte[]>(base.ReadFromCoreServer(this.S7_STOP), new Func<byte[], OperateResult>(this.CheckStopResult));
        }

        public override string ToString()
        {
            return string.Format("SiemensS7Net {0}[{1}:{2}]", this.CurrentPlc, this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<byte[]> result = BuildWriteBitCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            return this.WriteBase(result.Content);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            return this.Write(address, SoftBasic.BoolArrayToByte(values));
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<S7AddressData> result = S7AddressData.ParseFrom(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            int length = value.Length;
            ushort index = 0;
            while (index < length)
            {
                ushort num3 = (ushort) Math.Min(length - index, this.pdu_length);
                byte[] data = base.ByteTransform.TransByte(value, index, num3);
                OperateResult<byte[]> result3 = BuildWriteByteCommand(result, data);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                OperateResult result4 = this.WriteBase(result3.Content);
                if (!result4.IsSuccess)
                {
                    return result4;
                }
                index = (ushort) (index + num3);
                S7AddressData content = result.Content;
                content.AddressStart += num3 * 8;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByte", "")]
        public OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[] { value };
            return this.Write(address, buffer1);
        }

        [HslMqttApi("WriteDateTime", "写入PLC的时间格式的数据，这个格式是s7格式的一种")]
        public OperateResult Write(string address, DateTime dateTime)
        {
            return this.Write(address, SiemensDateTime.ToByteArray(dateTime));
        }

        [HslMqttApi("WriteString", "")]
        public override OperateResult Write(string address, string value)
        {
            if (value == null)
            {
                value = string.Empty;
            }
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            if (this.CurrentPlc != SiemensPLCS.S200Smart)
            {
                OperateResult<byte[]> result = this.Read(address, 2);
                if (!result.IsSuccess)
                {
                    return result;
                }
                if (result.Content[0] == 0xff)
                {
                    return new OperateResult<string>("Value in plc is not string type");
                }
                if (result.Content[0] == 0)
                {
                    result.Content[0] = 0xfe;
                }
                if (value.Length > result.Content[0])
                {
                    return new OperateResult<string>("String length is too long than plc defined");
                }
                byte[] buffer1 = new byte[] { result.Content[0], (byte) bytes.Length };
                return this.Write(address, SoftBasic.SpliceTwoByteArray(buffer1, bytes));
            }
            byte[] buffer2 = new byte[] { (byte) bytes.Length };
            return this.Write(address, SoftBasic.SpliceTwoByteArray(buffer2, bytes));
        }

        private OperateResult WriteBase(byte[] entireValue)
        {
            return ByteTransformHelper.GetResultFromOther<byte[]>(base.ReadFromCoreServer(entireValue), new Func<byte[], OperateResult>(SiemensS7Net.AnalysisWrite));
        }

        public int PDULength
        {
            get
            {
                return this.pdu_length;
            }
        }

        public byte Rack
        {
            get
            {
                return this.plc_rack;
            }
            set
            {
                this.plc_rack = value;
                this.plcHead1[0x15] = (byte) ((this.plc_rack * 0x20) + this.plc_slot);
            }
        }

        public byte Slot
        {
            get
            {
                return this.plc_slot;
            }
            set
            {
                this.plc_slot = value;
                this.plcHead1[0x15] = (byte) ((this.plc_rack * 0x20) + this.plc_slot);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SiemensS7Net.<>c <>9 = new SiemensS7Net.<>c();
            public static Func<byte[], string> <>9__13_0;
            public static Func<byte[], bool> <>9__26_0;

            internal bool <ReadBool>b__26_0(byte[] m)
            {
                return (m[0] > 0);
            }

            internal string <ReadOrderNumber>b__13_0(byte[] m)
            {
                return Encoding.ASCII.GetString(m, 0x47, 20);
            }
        }
    }
}

