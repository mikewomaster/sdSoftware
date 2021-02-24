namespace HslCommunication.Profinet.AllenBradley
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class AllenBradleyHelper
    {
        public const int CIP_MULTIREAD_DATA = 0x1000;
        public const byte CIP_READ_DATA = 0x4c;
        public const int CIP_READ_FRAGMENT = 0x52;
        public const byte CIP_READ_LIST = 0x55;
        public const int CIP_READ_WRITE_DATA = 0x4e;
        public const ushort CIP_Type_BitArray = 0xd3;
        public const ushort CIP_Type_Bool = 0xc1;
        public const ushort CIP_Type_Byte = 0xc2;
        public const ushort CIP_Type_D1 = 0xd1;
        public const ushort CIP_Type_D2 = 210;
        public const ushort CIP_Type_D3 = 0xd3;
        public const ushort CIP_Type_Double = 0xcb;
        public const ushort CIP_Type_DWord = 0xc4;
        public const ushort CIP_Type_LInt = 0xc5;
        public const ushort CIP_Type_Real = 0xca;
        public const ushort CIP_Type_String = 0xd0;
        public const ushort CIP_Type_Struct = 0xcc;
        public const ushort CIP_Type_UDint = 200;
        public const ushort CIP_Type_UInt = 0xc7;
        public const ushort CIP_Type_ULint = 0xc9;
        public const ushort CIP_Type_USInt = 0xc6;
        public const ushort CIP_Type_Word = 0xc3;
        public const int CIP_WRITE_DATA = 0x4d;
        public const int CIP_WRITE_FRAGMENT = 0x53;

        public static string AnalysisArrayIndex(string address, out int arrayIndex)
        {
            arrayIndex = 0;
            if (address.EndsWith("]"))
            {
                int length = address.LastIndexOf('[');
                if (length < 0)
                {
                    return address;
                }
                address = address.Remove(address.Length - 1);
                arrayIndex = int.Parse(address.Substring(length + 1));
                address = address.Substring(0, length);
            }
            return address;
        }

        private static byte[] BuildRequestPathCommand(string address, [Optional, DefaultParameterValue(false)] bool isConnectedAddress)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                char[] separator = new char[] { '.' };
                string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str = string.Empty;
                    int index = strArray[i].IndexOf('[');
                    int num3 = strArray[i].IndexOf(']');
                    if (((index > 0) && (num3 > 0)) && (num3 > index))
                    {
                        str = strArray[i].Substring(index + 1, (num3 - index) - 1);
                        strArray[i] = strArray[i].Substring(0, index);
                    }
                    stream.WriteByte(0x91);
                    byte[] bytes = Encoding.UTF8.GetBytes(strArray[i]);
                    stream.WriteByte((byte) bytes.Length);
                    stream.Write(bytes, 0, bytes.Length);
                    if ((bytes.Length % 2) == 1)
                    {
                        stream.WriteByte(0);
                    }
                    if (!string.IsNullOrEmpty(str))
                    {
                        char[] chArray2 = new char[] { ',' };
                        string[] strArray2 = str.Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < strArray2.Length; j++)
                        {
                            int num5 = Convert.ToInt32(strArray2[j]);
                            if ((num5 < 0x100) && !isConnectedAddress)
                            {
                                stream.WriteByte(40);
                                stream.WriteByte((byte) num5);
                            }
                            else
                            {
                                stream.WriteByte(0x29);
                                stream.WriteByte(0);
                                stream.WriteByte(BitConverter.GetBytes(num5)[0]);
                                stream.WriteByte(BitConverter.GetBytes(num5)[1]);
                            }
                        }
                    }
                }
                return stream.ToArray();
            }
        }

        public static OperateResult CheckResponse(byte[] response)
        {
            try
            {
                int err = BitConverter.ToInt32(response, 8);
                if (err == 0)
                {
                    return OperateResult.CreateSuccessResult();
                }
                string msg = string.Empty;
                switch (err)
                {
                    case 0x65:
                        msg = StringResources.Language.AllenBradleySessionStatus65;
                        break;

                    case 0x69:
                        msg = StringResources.Language.AllenBradleySessionStatus69;
                        break;

                    case 1:
                        msg = StringResources.Language.AllenBradleySessionStatus01;
                        break;

                    case 2:
                        msg = StringResources.Language.AllenBradleySessionStatus02;
                        break;

                    case 3:
                        msg = StringResources.Language.AllenBradleySessionStatus03;
                        break;

                    case 100:
                        msg = StringResources.Language.AllenBradleySessionStatus64;
                        break;

                    default:
                        msg = StringResources.Language.UnknownError;
                        break;
                }
                return new OperateResult(err, msg);
            }
            catch (Exception exception)
            {
                return new OperateResult(exception.Message);
            }
        }

        public static OperateResult<byte[], ushort, bool> ExtractActualData(byte[] response, bool isRead)
        {
            List<byte> list = new List<byte>();
            int startIndex = 0x26;
            bool flag = false;
            ushort num2 = 0;
            ushort num3 = BitConverter.ToUInt16(response, 0x26);
            if (BitConverter.ToInt32(response, 40) == 0x8a)
            {
                startIndex = 0x2c;
                int num4 = BitConverter.ToUInt16(response, startIndex);
                for (int i = 0; i < num4; i++)
                {
                    int num6 = BitConverter.ToUInt16(response, (startIndex + 2) + (i * 2)) + startIndex;
                    int num7 = (i == (num4 - 1)) ? response.Length : (BitConverter.ToUInt16(response, (startIndex + 4) + (i * 2)) + startIndex);
                    ushort num8 = BitConverter.ToUInt16(response, num6 + 2);
                    switch (num8)
                    {
                        case 0:
                            break;

                        case 4:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley04
                            };

                        case 5:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley05
                            };

                        case 6:
                            if ((response[startIndex + 2] != 210) && (response[startIndex + 2] != 0xcc))
                            {
                                break;
                            }
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley06
                            };

                        case 10:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley0A
                            };

                        case 0x13:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley13
                            };

                        case 0x1c:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley1C
                            };

                        case 30:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley1E
                            };

                        case 0x26:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.AllenBradley26
                            };

                        default:
                            return new OperateResult<byte[], ushort, bool> { 
                                ErrorCode = num8,
                                Message = StringResources.Language.UnknownError
                            };
                    }
                    if (isRead)
                    {
                        for (int j = num6 + 6; j < num7; j++)
                        {
                            list.Add(response[j]);
                        }
                    }
                }
            }
            else
            {
                byte num12 = response[startIndex + 4];
                switch (num12)
                {
                    case 0x1c:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley1C
                        };

                    case 30:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley1E
                        };

                    case 0x20:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley20
                        };

                    case 0x26:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley26
                        };

                    case 0x13:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley13
                        };

                    case 0:
                        break;

                    case 4:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley04
                        };

                    case 5:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley05
                        };

                    case 6:
                        flag = true;
                        break;

                    case 10:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.AllenBradley0A
                        };

                    default:
                        return new OperateResult<byte[], ushort, bool> { 
                            ErrorCode = num12,
                            Message = StringResources.Language.UnknownError
                        };
                }
                if ((response[startIndex + 2] == 0xcd) || (response[startIndex + 2] == 0xd3))
                {
                    return OperateResult.CreateSuccessResult<byte[], ushort, bool>(list.ToArray(), num2, flag);
                }
                if ((response[startIndex + 2] == 0xcc) || (response[startIndex + 2] == 210))
                {
                    for (int k = startIndex + 8; k < ((startIndex + 2) + num3); k++)
                    {
                        list.Add(response[k]);
                    }
                    num2 = BitConverter.ToUInt16(response, startIndex + 6);
                }
                else if (response[startIndex + 2] == 0xd5)
                {
                    for (int m = startIndex + 6; m < ((startIndex + 2) + num3); m++)
                    {
                        list.Add(response[m]);
                    }
                }
            }
            return OperateResult.CreateSuccessResult<byte[], ushort, bool>(list.ToArray(), num2, flag);
        }

        public static byte[] GetEnumeratorCommand(ushort startInstance)
        {
            return new byte[] { 0x55, 3, 0x20, 0x6b, 0x25, 0, BitConverter.GetBytes(startInstance)[0], BitConverter.GetBytes(startInstance)[1], 2, 0, 1, 0, 2, 0 };
        }

        public static byte[] GetStructHandleCommand(ushort symbolType)
        {
            byte[] buffer = new byte[0x12];
            byte[] bytes = BitConverter.GetBytes(symbolType);
            bytes[1] = (byte) (bytes[1] & 15);
            buffer[0] = 3;
            buffer[1] = 3;
            buffer[2] = 0x20;
            buffer[3] = 0x6c;
            buffer[4] = 0x25;
            buffer[5] = 0;
            buffer[6] = bytes[0];
            buffer[7] = bytes[1];
            buffer[8] = 4;
            buffer[9] = 0;
            buffer[10] = 4;
            buffer[11] = 0;
            buffer[12] = 5;
            buffer[13] = 0;
            buffer[14] = 2;
            buffer[15] = 0;
            buffer[0x10] = 1;
            buffer[0x11] = 0;
            return buffer;
        }

        public static byte[] GetStructItemNameType(ushort symbolType, AbStructHandle structHandle)
        {
            byte[] buffer = new byte[14];
            ushort num = (ushort) ((structHandle.TemplateObjectDefinitionSize * 4) - 0x15);
            byte[] bytes = BitConverter.GetBytes(symbolType);
            bytes[1] = (byte) (bytes[1] & 15);
            byte[] buffer3 = BitConverter.GetBytes(0);
            byte[] buffer4 = BitConverter.GetBytes(num);
            buffer[0] = 0x4c;
            buffer[1] = 3;
            buffer[2] = 0x20;
            buffer[3] = 0x6c;
            buffer[4] = 0x25;
            buffer[5] = 0;
            buffer[6] = bytes[0];
            buffer[7] = bytes[1];
            buffer[8] = buffer3[0];
            buffer[9] = buffer3[1];
            buffer[10] = buffer3[2];
            buffer[11] = buffer3[3];
            buffer[12] = buffer4[0];
            buffer[13] = buffer4[1];
            return buffer;
        }

        public static byte[] PackCleanCommandService(byte[] portSlot, params byte[][] cips)
        {
            MemoryStream stream = new MemoryStream();
            stream.WriteByte(0xb2);
            stream.WriteByte(0);
            stream.WriteByte(0);
            stream.WriteByte(0);
            if (cips.Length == 1)
            {
                stream.Write(cips[0], 0, cips[0].Length);
            }
            else
            {
                stream.WriteByte(10);
                stream.WriteByte(2);
                stream.WriteByte(0x20);
                stream.WriteByte(2);
                stream.WriteByte(0x24);
                stream.WriteByte(1);
                stream.Write(BitConverter.GetBytes((ushort) cips.Length), 0, 2);
                ushort num = (ushort) (2 + (2 * cips.Length));
                for (int i = 0; i < cips.Length; i++)
                {
                    stream.Write(BitConverter.GetBytes(num), 0, 2);
                    num = (ushort) (num + cips[i].Length);
                }
                for (int j = 0; j < cips.Length; j++)
                {
                    stream.Write(cips[j], 0, cips[j].Length);
                }
            }
            stream.WriteByte((byte) ((portSlot.Length + 1) / 2));
            stream.WriteByte(0);
            stream.Write(portSlot, 0, portSlot.Length);
            if ((portSlot.Length % 2) == 1)
            {
                stream.WriteByte(0);
            }
            byte[] array = stream.ToArray();
            stream.Dispose();
            BitConverter.GetBytes((short) (array.Length - 4)).CopyTo(array, 2);
            return array;
        }

        public static byte[] PackCommandGetAttributesAll(byte[] portSlot, uint sessionHandle)
        {
            byte[][] service = new byte[2][];
            service[0] = new byte[4];
            byte[][] cips = new byte[][] { new byte[] { 1, 2, 0x20, 1, 0x24, 1 } };
            service[1] = PackCommandService(portSlot, cips);
            byte[] commandSpecificData = PackCommandSpecificData(service);
            return PackRequestHeader(0x6f, sessionHandle, commandSpecificData);
        }

        public static byte[] PackCommandResponse(byte[] data, bool isRead)
        {
            if (data == null)
            {
                byte[] buffer1 = new byte[6];
                buffer1[2] = 4;
                return buffer1;
            }
            byte[] buffer2 = new byte[6];
            buffer2[0] = isRead ? ((byte) 0xcc) : ((byte) 0xcd);
            return SoftBasic.SpliceTwoByteArray(buffer2, data);
        }

        public static byte[] PackCommandService(byte[] portSlot, params byte[][] cips)
        {
            MemoryStream stream = new MemoryStream();
            stream.WriteByte(0xb2);
            stream.WriteByte(0);
            stream.WriteByte(0);
            stream.WriteByte(0);
            stream.WriteByte(0x52);
            stream.WriteByte(2);
            stream.WriteByte(0x20);
            stream.WriteByte(6);
            stream.WriteByte(0x24);
            stream.WriteByte(1);
            stream.WriteByte(10);
            stream.WriteByte(240);
            stream.WriteByte(0);
            stream.WriteByte(0);
            int num = 0;
            if (cips.Length == 1)
            {
                stream.Write(cips[0], 0, cips[0].Length);
                num += cips[0].Length;
            }
            else
            {
                stream.WriteByte(10);
                stream.WriteByte(2);
                stream.WriteByte(0x20);
                stream.WriteByte(2);
                stream.WriteByte(0x24);
                stream.WriteByte(1);
                num += 8;
                stream.Write(BitConverter.GetBytes((ushort) cips.Length), 0, 2);
                ushort num2 = (ushort) (2 + (2 * cips.Length));
                num += 2 * cips.Length;
                for (int i = 0; i < cips.Length; i++)
                {
                    stream.Write(BitConverter.GetBytes(num2), 0, 2);
                    num2 = (ushort) (num2 + cips[i].Length);
                }
                for (int j = 0; j < cips.Length; j++)
                {
                    stream.Write(cips[j], 0, cips[j].Length);
                    num += cips[j].Length;
                }
            }
            stream.WriteByte((byte) ((portSlot.Length + 1) / 2));
            stream.WriteByte(0);
            stream.Write(portSlot, 0, portSlot.Length);
            if ((portSlot.Length % 2) == 1)
            {
                stream.WriteByte(0);
            }
            byte[] array = stream.ToArray();
            stream.Dispose();
            BitConverter.GetBytes((short) num).CopyTo(array, 12);
            BitConverter.GetBytes((short) (array.Length - 4)).CopyTo(array, 2);
            return array;
        }

        public static byte[] PackCommandSingleService(byte[] command)
        {
            if (command == null)
            {
                command = new byte[0];
            }
            byte[] array = new byte[4 + command.Length];
            array[0] = 0xb2;
            array[1] = 0;
            array[2] = BitConverter.GetBytes(command.Length)[0];
            array[3] = BitConverter.GetBytes(command.Length)[1];
            command.CopyTo(array, 4);
            return array;
        }

        public static byte[] PackCommandSpecificData(params byte[][] service)
        {
            MemoryStream stream = new MemoryStream();
            stream.WriteByte(0);
            stream.WriteByte(0);
            stream.WriteByte(0);
            stream.WriteByte(0);
            stream.WriteByte(1);
            stream.WriteByte(0);
            stream.WriteByte(BitConverter.GetBytes(service.Length)[0]);
            stream.WriteByte(BitConverter.GetBytes(service.Length)[1]);
            for (int i = 0; i < service.Length; i++)
            {
                stream.Write(service[i], 0, service[i].Length);
            }
            byte[] buffer = stream.ToArray();
            stream.Dispose();
            return buffer;
        }

        public static byte[] PackRequestHeader(ushort command, uint session, byte[] commandSpecificData)
        {
            byte[] destinationArray = new byte[commandSpecificData.Length + 0x18];
            Array.Copy(commandSpecificData, 0, destinationArray, 0x18, commandSpecificData.Length);
            BitConverter.GetBytes(command).CopyTo(destinationArray, 0);
            BitConverter.GetBytes(session).CopyTo(destinationArray, 4);
            BitConverter.GetBytes((ushort) commandSpecificData.Length).CopyTo(destinationArray, 2);
            return destinationArray;
        }

        public static byte[] PackRequestReadSegment(string address, int startIndex, int length)
        {
            byte[] array = new byte[0x400];
            int index = 0;
            array[index++] = 0x52;
            index++;
            byte[] buffer2 = BuildRequestPathCommand(address, false);
            buffer2.CopyTo(array, index);
            index += buffer2.Length;
            array[1] = (byte) ((index - 2) / 2);
            array[index++] = BitConverter.GetBytes(length)[0];
            array[index++] = BitConverter.GetBytes(length)[1];
            array[index++] = BitConverter.GetBytes(startIndex)[0];
            array[index++] = BitConverter.GetBytes(startIndex)[1];
            array[index++] = BitConverter.GetBytes(startIndex)[2];
            array[index++] = BitConverter.GetBytes(startIndex)[3];
            byte[] destinationArray = new byte[index];
            Array.Copy(array, 0, destinationArray, 0, index);
            return destinationArray;
        }

        public static byte[] PackRequestWrite(string address, bool value)
        {
            int num;
            address = AnalysisArrayIndex(address, out num);
            address = address + "[" + ((num / 0x20)).ToString() + "]";
            int num2 = 0;
            int num3 = -1;
            if (value)
            {
                num2 = ((int) 1) << num;
            }
            else
            {
                num3 = ~(((int) 1) << num);
            }
            byte[] array = new byte[0x400];
            int index = 0;
            array[index++] = 0x4e;
            index++;
            byte[] buffer2 = BuildRequestPathCommand(address, false);
            buffer2.CopyTo(array, index);
            index += buffer2.Length;
            array[1] = (byte) ((index - 2) / 2);
            array[index++] = 4;
            array[index++] = 0;
            BitConverter.GetBytes(num2).CopyTo(array, index);
            index += 4;
            BitConverter.GetBytes(num3).CopyTo(array, index);
            index += 4;
            byte[] destinationArray = new byte[index];
            Array.Copy(array, 0, destinationArray, 0, index);
            return destinationArray;
        }

        public static byte[] PackRequestWrite(string address, ushort typeCode, byte[] value, [Optional, DefaultParameterValue(1)] int length, [Optional, DefaultParameterValue(false)] bool isConnectedAddress)
        {
            byte[] array = new byte[0x400];
            int index = 0;
            array[index++] = 0x4d;
            index++;
            byte[] buffer2 = BuildRequestPathCommand(address, isConnectedAddress);
            buffer2.CopyTo(array, index);
            index += buffer2.Length;
            array[1] = (byte) ((index - 2) / 2);
            array[index++] = BitConverter.GetBytes(typeCode)[0];
            array[index++] = BitConverter.GetBytes(typeCode)[1];
            array[index++] = BitConverter.GetBytes(length)[0];
            array[index++] = BitConverter.GetBytes(length)[1];
            value.CopyTo(array, index);
            index += value.Length;
            byte[] destinationArray = new byte[index];
            Array.Copy(array, 0, destinationArray, 0, index);
            return destinationArray;
        }

        public static byte[] PackRequsetRead(string address, int length, [Optional, DefaultParameterValue(false)] bool isConnectedAddress)
        {
            byte[] array = new byte[0x400];
            int index = 0;
            array[index++] = 0x4c;
            index++;
            byte[] buffer2 = BuildRequestPathCommand(address, isConnectedAddress);
            buffer2.CopyTo(array, index);
            index += buffer2.Length;
            array[1] = (byte) ((index - 2) / 2);
            array[index++] = BitConverter.GetBytes(length)[0];
            array[index++] = BitConverter.GetBytes(length)[1];
            byte[] destinationArray = new byte[index];
            Array.Copy(array, 0, destinationArray, 0, index);
            return destinationArray;
        }

        public static string ParseRequestPathCommand(byte[] pathCommand)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < pathCommand.Length; i++)
            {
                if (pathCommand[i] == 0x91)
                {
                    string str = Encoding.UTF8.GetString(pathCommand, i + 2, pathCommand[i + 1]).TrimEnd(new char[1]);
                    builder.Append(str);
                    int num2 = 2 + str.Length;
                    if ((str.Length % 2) == 1)
                    {
                        num2++;
                    }
                    if (pathCommand.Length > (num2 + i))
                    {
                        if (pathCommand[i + num2] == 40)
                        {
                            builder.Append(string.Format("[{0}]", pathCommand[(i + num2) + 1]));
                        }
                        else if (pathCommand[i + num2] == 0x29)
                        {
                            builder.Append(string.Format("[{0}]", BitConverter.ToUInt16(pathCommand, (i + num2) + 2)));
                        }
                    }
                    builder.Append(".");
                }
            }
            if (builder[builder.Length - 1] == '.')
            {
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }

        public static byte[] RegisterSessionHandle()
        {
            byte[] buffer1 = new byte[4];
            buffer1[0] = 1;
            byte[] commandSpecificData = buffer1;
            return PackRequestHeader(0x65, 0, commandSpecificData);
        }

        public static byte[] UnRegisterSessionHandle(uint sessionHandle)
        {
            return PackRequestHeader(0x66, sessionHandle, new byte[0]);
        }
    }
}

