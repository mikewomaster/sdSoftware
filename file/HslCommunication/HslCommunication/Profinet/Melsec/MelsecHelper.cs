namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core.Address;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class MelsecHelper
    {
        public static byte[] BuildAsciiReadMcCoreCommand(McAddressData addressData, bool isBit)
        {
            return new byte[] { 
                0x30, 0x34, 0x30, 0x31, 0x30, 0x30, 0x30, (isBit ? ((byte) 0x31) : ((byte) 0x30)), Encoding.ASCII.GetBytes(addressData.McDataType.AsciiCode)[0], Encoding.ASCII.GetBytes(addressData.McDataType.AsciiCode)[1], BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[0], BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[1], BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[2], BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[3], BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[4], BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[5],
                SoftBasic.BuildAsciiBytesFrom(addressData.Length)[0], SoftBasic.BuildAsciiBytesFrom(addressData.Length)[1], SoftBasic.BuildAsciiBytesFrom(addressData.Length)[2], SoftBasic.BuildAsciiBytesFrom(addressData.Length)[3]
            };
        }

        public static byte[] BuildAsciiReadRandomCommand(McAddressData[] address)
        {
            byte[] buffer = new byte[12 + (address.Length * 12)];
            buffer[0] = 0x30;
            buffer[1] = 0x34;
            buffer[2] = 0x30;
            buffer[3] = 0x36;
            buffer[4] = 0x30;
            buffer[5] = 0x30;
            buffer[6] = 0x30;
            buffer[7] = 0x30;
            buffer[8] = SoftBasic.BuildAsciiBytesFrom((byte) address.Length)[0];
            buffer[9] = SoftBasic.BuildAsciiBytesFrom((byte) address.Length)[1];
            buffer[10] = 0x30;
            buffer[11] = 0x30;
            for (int i = 0; i < address.Length; i++)
            {
                buffer[(i * 12) + 12] = Encoding.ASCII.GetBytes(address[i].McDataType.AsciiCode)[0];
                buffer[(i * 12) + 13] = Encoding.ASCII.GetBytes(address[i].McDataType.AsciiCode)[1];
                buffer[(i * 12) + 14] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[0];
                buffer[(i * 12) + 15] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[1];
                buffer[(i * 12) + 0x10] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[2];
                buffer[(i * 12) + 0x11] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[3];
                buffer[(i * 12) + 0x12] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[4];
                buffer[(i * 12) + 0x13] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[5];
                buffer[(i * 12) + 20] = SoftBasic.BuildAsciiBytesFrom(address[i].Length)[0];
                buffer[(i * 12) + 0x15] = SoftBasic.BuildAsciiBytesFrom(address[i].Length)[1];
                buffer[(i * 12) + 0x16] = SoftBasic.BuildAsciiBytesFrom(address[i].Length)[2];
                buffer[(i * 12) + 0x17] = SoftBasic.BuildAsciiBytesFrom(address[i].Length)[3];
            }
            return buffer;
        }

        public static byte[] BuildAsciiReadRandomWordCommand(McAddressData[] address)
        {
            byte[] buffer = new byte[12 + (address.Length * 8)];
            buffer[0] = 0x30;
            buffer[1] = 0x34;
            buffer[2] = 0x30;
            buffer[3] = 0x33;
            buffer[4] = 0x30;
            buffer[5] = 0x30;
            buffer[6] = 0x30;
            buffer[7] = 0x30;
            buffer[8] = SoftBasic.BuildAsciiBytesFrom((byte) address.Length)[0];
            buffer[9] = SoftBasic.BuildAsciiBytesFrom((byte) address.Length)[1];
            buffer[10] = 0x30;
            buffer[11] = 0x30;
            for (int i = 0; i < address.Length; i++)
            {
                buffer[(i * 8) + 12] = Encoding.ASCII.GetBytes(address[i].McDataType.AsciiCode)[0];
                buffer[(i * 8) + 13] = Encoding.ASCII.GetBytes(address[i].McDataType.AsciiCode)[1];
                buffer[(i * 8) + 14] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[0];
                buffer[(i * 8) + 15] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[1];
                buffer[(i * 8) + 0x10] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[2];
                buffer[(i * 8) + 0x11] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[3];
                buffer[(i * 8) + 0x12] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[4];
                buffer[(i * 8) + 0x13] = BuildBytesFromAddress(address[i].AddressStart, address[i].McDataType)[5];
            }
            return buffer;
        }

        public static byte[] BuildAsciiWriteBitCoreCommand(McAddressData addressData, bool[] value)
        {
            if (value == null)
            {
                value = new bool[0];
            }
            byte[] buffer = (from m in value select m ? ((IEnumerable<byte>) ((byte) 0x31)) : ((IEnumerable<byte>) ((byte) 0x30))).ToArray<byte>();
            byte[] array = new byte[20 + buffer.Length];
            array[0] = 0x31;
            array[1] = 0x34;
            array[2] = 0x30;
            array[3] = 0x31;
            array[4] = 0x30;
            array[5] = 0x30;
            array[6] = 0x30;
            array[7] = 0x31;
            array[8] = Encoding.ASCII.GetBytes(addressData.McDataType.AsciiCode)[0];
            array[9] = Encoding.ASCII.GetBytes(addressData.McDataType.AsciiCode)[1];
            array[10] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[0];
            array[11] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[1];
            array[12] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[2];
            array[13] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[3];
            array[14] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[4];
            array[15] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[5];
            array[0x10] = SoftBasic.BuildAsciiBytesFrom((ushort) value.Length)[0];
            array[0x11] = SoftBasic.BuildAsciiBytesFrom((ushort) value.Length)[1];
            array[0x12] = SoftBasic.BuildAsciiBytesFrom((ushort) value.Length)[2];
            array[0x13] = SoftBasic.BuildAsciiBytesFrom((ushort) value.Length)[3];
            buffer.CopyTo(array, 20);
            return array;
        }

        public static byte[] BuildAsciiWriteWordCoreCommand(McAddressData addressData, byte[] value)
        {
            value = TransByteArrayToAsciiByteArray(value);
            byte[] array = new byte[20 + value.Length];
            array[0] = 0x31;
            array[1] = 0x34;
            array[2] = 0x30;
            array[3] = 0x31;
            array[4] = 0x30;
            array[5] = 0x30;
            array[6] = 0x30;
            array[7] = 0x30;
            array[8] = Encoding.ASCII.GetBytes(addressData.McDataType.AsciiCode)[0];
            array[9] = Encoding.ASCII.GetBytes(addressData.McDataType.AsciiCode)[1];
            array[10] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[0];
            array[11] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[1];
            array[12] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[2];
            array[13] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[3];
            array[14] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[4];
            array[15] = BuildBytesFromAddress(addressData.AddressStart, addressData.McDataType)[5];
            array[0x10] = SoftBasic.BuildAsciiBytesFrom((ushort) (value.Length / 4))[0];
            array[0x11] = SoftBasic.BuildAsciiBytesFrom((ushort) (value.Length / 4))[1];
            array[0x12] = SoftBasic.BuildAsciiBytesFrom((ushort) (value.Length / 4))[2];
            array[0x13] = SoftBasic.BuildAsciiBytesFrom((ushort) (value.Length / 4))[3];
            value.CopyTo(array, 20);
            return array;
        }

        internal static byte[] BuildBytesFromAddress(int address, MelsecMcDataType type)
        {
            return Encoding.ASCII.GetBytes(address.ToString((type.FromBase == 10) ? "D6" : "X6"));
        }

        public static byte[] BuildReadMcCoreCommand(McAddressData addressData, bool isBit)
        {
            return new byte[] { 1, 4, (isBit ? ((byte) 1) : ((byte) 0)), 0, BitConverter.GetBytes(addressData.AddressStart)[0], BitConverter.GetBytes(addressData.AddressStart)[1], BitConverter.GetBytes(addressData.AddressStart)[2], addressData.McDataType.DataCode, ((byte) (addressData.Length % 0x100)), ((byte) (addressData.Length / 0x100)) };
        }

        public static byte[] BuildReadMcCoreExtendCommand(McAddressData addressData, ushort extend, bool isBit)
        {
            return new byte[] { 
                1, 4, (isBit ? ((byte) 0x81) : ((byte) 0x80)), 0, 0, 0, BitConverter.GetBytes(addressData.AddressStart)[0], BitConverter.GetBytes(addressData.AddressStart)[1], BitConverter.GetBytes(addressData.AddressStart)[2], addressData.McDataType.DataCode, 0, 0, BitConverter.GetBytes(extend)[0], BitConverter.GetBytes(extend)[1], 0xf9, ((byte) (addressData.Length % 0x100)),
                ((byte) (addressData.Length / 0x100))
            };
        }

        public static OperateResult<byte[]> BuildReadMemoryCommand(string address, ushort length)
        {
            try
            {
                uint num = uint.Parse(address);
                return OperateResult.CreateSuccessResult<byte[]>(new byte[] { 0x13, 6, BitConverter.GetBytes(num)[0], BitConverter.GetBytes(num)[1], BitConverter.GetBytes(num)[2], BitConverter.GetBytes(num)[3], (byte) (length % 0x100), (byte) (length / 0x100) });
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static byte[] BuildReadRandomCommand(McAddressData[] address)
        {
            byte[] buffer = new byte[6 + (address.Length * 6)];
            buffer[0] = 6;
            buffer[1] = 4;
            buffer[2] = 0;
            buffer[3] = 0;
            buffer[4] = (byte) address.Length;
            buffer[5] = 0;
            for (int i = 0; i < address.Length; i++)
            {
                buffer[(i * 6) + 6] = BitConverter.GetBytes(address[i].AddressStart)[0];
                buffer[(i * 6) + 7] = BitConverter.GetBytes(address[i].AddressStart)[1];
                buffer[(i * 6) + 8] = BitConverter.GetBytes(address[i].AddressStart)[2];
                buffer[(i * 6) + 9] = address[i].McDataType.DataCode;
                buffer[(i * 6) + 10] = (byte) (address[i].Length % 0x100);
                buffer[(i * 6) + 11] = (byte) (address[i].Length / 0x100);
            }
            return buffer;
        }

        public static byte[] BuildReadRandomWordCommand(McAddressData[] address)
        {
            byte[] buffer = new byte[6 + (address.Length * 4)];
            buffer[0] = 3;
            buffer[1] = 4;
            buffer[2] = 0;
            buffer[3] = 0;
            buffer[4] = (byte) address.Length;
            buffer[5] = 0;
            for (int i = 0; i < address.Length; i++)
            {
                buffer[(i * 4) + 6] = BitConverter.GetBytes(address[i].AddressStart)[0];
                buffer[(i * 4) + 7] = BitConverter.GetBytes(address[i].AddressStart)[1];
                buffer[(i * 4) + 8] = BitConverter.GetBytes(address[i].AddressStart)[2];
                buffer[(i * 4) + 9] = address[i].McDataType.DataCode;
            }
            return buffer;
        }

        public static byte[] BuildReadTag(string[] tags, ushort[] lengths)
        {
            if (tags.Length != lengths.Length)
            {
                throw new Exception(StringResources.Language.TwoParametersLengthIsNotSame);
            }
            MemoryStream stream = new MemoryStream();
            stream.WriteByte(0x1a);
            stream.WriteByte(4);
            stream.WriteByte(0);
            stream.WriteByte(0);
            stream.WriteByte(BitConverter.GetBytes(tags.Length)[0]);
            stream.WriteByte(BitConverter.GetBytes(tags.Length)[1]);
            stream.WriteByte(0);
            stream.WriteByte(0);
            for (int i = 0; i < tags.Length; i++)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(tags[i]);
                stream.WriteByte(BitConverter.GetBytes((int) (bytes.Length / 2))[0]);
                stream.WriteByte(BitConverter.GetBytes((int) (bytes.Length / 2))[1]);
                stream.Write(bytes, 0, bytes.Length);
                stream.WriteByte(1);
                stream.WriteByte(0);
                stream.WriteByte(BitConverter.GetBytes((int) (lengths[i] * 2))[0]);
                stream.WriteByte(BitConverter.GetBytes((int) (lengths[i] * 2))[1]);
            }
            byte[] buffer = stream.ToArray();
            stream.Dispose();
            return buffer;
        }

        public static byte[] BuildWriteBitCoreCommand(McAddressData addressData, bool[] value)
        {
            if (value == null)
            {
                value = new bool[0];
            }
            byte[] buffer = TransBoolArrayToByteData(value);
            byte[] array = new byte[10 + buffer.Length];
            array[0] = 1;
            array[1] = 20;
            array[2] = 1;
            array[3] = 0;
            array[4] = BitConverter.GetBytes(addressData.AddressStart)[0];
            array[5] = BitConverter.GetBytes(addressData.AddressStart)[1];
            array[6] = BitConverter.GetBytes(addressData.AddressStart)[2];
            array[7] = addressData.McDataType.DataCode;
            array[8] = (byte) (value.Length % 0x100);
            array[9] = (byte) (value.Length / 0x100);
            buffer.CopyTo(array, 10);
            return array;
        }

        public static byte[] BuildWriteWordCoreCommand(McAddressData addressData, byte[] value)
        {
            if (value == null)
            {
                value = new byte[0];
            }
            byte[] array = new byte[10 + value.Length];
            array[0] = 1;
            array[1] = 20;
            array[2] = 0;
            array[3] = 0;
            array[4] = BitConverter.GetBytes(addressData.AddressStart)[0];
            array[5] = BitConverter.GetBytes(addressData.AddressStart)[1];
            array[6] = BitConverter.GetBytes(addressData.AddressStart)[2];
            array[7] = addressData.McDataType.DataCode;
            array[8] = (byte) ((value.Length / 2) % 0x100);
            array[9] = (byte) ((value.Length / 2) / 0x100);
            value.CopyTo(array, 10);
            return array;
        }

        internal static bool CheckCRC(byte[] data)
        {
            byte[] buffer = FxCalculateCRC(data);
            if (buffer[0] != data[data.Length - 2])
            {
                return false;
            }
            if (buffer[1] != data[data.Length - 1])
            {
                return false;
            }
            return true;
        }

        public static OperateResult<byte[]> ExtraTagData(byte[] content)
        {
            try
            {
                int num = BitConverter.ToUInt16(content, 0);
                int num2 = 2;
                List<byte> list = new List<byte>(20);
                for (int i = 0; i < num; i++)
                {
                    int length = BitConverter.ToUInt16(content, num2 + 2);
                    list.AddRange(SoftBasic.ArraySelectMiddle<byte>(content, num2 + 4, length));
                    num2 += 4 + length;
                }
                return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message + " Source:" + SoftBasic.ByteToHexString(content, ' '));
            }
        }

        internal static byte[] FxCalculateCRC(byte[] data)
        {
            int num = 0;
            for (int i = 1; i < (data.Length - 2); i++)
            {
                num += data[i];
            }
            return SoftBasic.BuildAsciiBytesFrom((byte) num);
        }

        public static string GetErrorDescription(int code)
        {
            switch (code)
            {
                case 0x51:
                    return StringResources.Language.MelsecError51;

                case 0x52:
                    return StringResources.Language.MelsecError52;

                case 0x54:
                    return StringResources.Language.MelsecError54;

                case 0x55:
                    return StringResources.Language.MelsecError55;

                case 0x56:
                    return StringResources.Language.MelsecError56;

                case 0x58:
                    return StringResources.Language.MelsecError58;

                case 0x59:
                    return StringResources.Language.MelsecError59;

                case 2:
                    return StringResources.Language.MelsecError02;

                case 0xc04d:
                    return StringResources.Language.MelsecErrorC04D;

                case 0xc050:
                    return StringResources.Language.MelsecErrorC050;

                case 0xc051:
                case 0xc052:
                case 0xc053:
                case 0xc054:
                    return StringResources.Language.MelsecErrorC051_54;

                case 0xc055:
                    return StringResources.Language.MelsecErrorC055;

                case 0xc056:
                    return StringResources.Language.MelsecErrorC056;

                case 0xc057:
                    return StringResources.Language.MelsecErrorC057;

                case 0xc058:
                    return StringResources.Language.MelsecErrorC058;

                case 0xc059:
                    return StringResources.Language.MelsecErrorC059;

                case 0xc05a:
                case 0xc05b:
                    return StringResources.Language.MelsecErrorC05A_B;

                case 0xc05c:
                    return StringResources.Language.MelsecErrorC05C;

                case 0xc05d:
                    return StringResources.Language.MelsecErrorC05D;

                case 0xc05e:
                    return StringResources.Language.MelsecErrorC05E;

                case 0xc05f:
                    return StringResources.Language.MelsecErrorC05F;

                case 0xc060:
                    return StringResources.Language.MelsecErrorC060;

                case 0xc061:
                    return StringResources.Language.MelsecErrorC061;

                case 0xc062:
                    return StringResources.Language.MelsecErrorC062;

                case 0xc070:
                    return StringResources.Language.MelsecErrorC070;

                case 0xc072:
                    return StringResources.Language.MelsecErrorC072;

                case 0xc074:
                    return StringResources.Language.MelsecErrorC074;
            }
            return StringResources.Language.MelsecPleaseReferToManualDocument;
        }

        public static OperateResult<MelsecA1EDataType, int> McA1EAnalysisAddress(string address)
        {
            OperateResult<MelsecA1EDataType, int> result = new OperateResult<MelsecA1EDataType, int>();
            try
            {
                switch (address[0])
                {
                    case 'B':
                    case 'b':
                        result.Content1 = MelsecA1EDataType.B;
                        result.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EDataType.B.FromBase);
                        goto Label_04E2;

                    case 'C':
                    case 'c':
                        if ((address[1] != 'S') && (address[1] != 's'))
                        {
                            if ((address[1] != 'C') && (address[1] != 'c'))
                            {
                                if ((address[1] != 'N') && (address[1] != 'n'))
                                {
                                    throw new Exception(StringResources.Language.NotSupportedDataType);
                                }
                                result.Content1 = MelsecA1EDataType.CN;
                                result.Content2 = Convert.ToInt32(address.Substring(2), MelsecA1EDataType.CN.FromBase);
                            }
                            else
                            {
                                result.Content1 = MelsecA1EDataType.CC;
                                result.Content2 = Convert.ToInt32(address.Substring(2), MelsecA1EDataType.CC.FromBase);
                            }
                        }
                        else
                        {
                            result.Content1 = MelsecA1EDataType.CS;
                            result.Content2 = Convert.ToInt32(address.Substring(2), MelsecA1EDataType.CS.FromBase);
                        }
                        goto Label_04E2;

                    case 'D':
                    case 'd':
                        result.Content1 = MelsecA1EDataType.D;
                        result.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EDataType.D.FromBase);
                        goto Label_04E2;

                    case 'F':
                    case 'f':
                        result.Content1 = MelsecA1EDataType.F;
                        result.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EDataType.F.FromBase);
                        goto Label_04E2;

                    case 'M':
                    case 'm':
                        result.Content1 = MelsecA1EDataType.M;
                        result.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EDataType.M.FromBase);
                        goto Label_04E2;

                    case 'R':
                    case 'r':
                        result.Content1 = MelsecA1EDataType.R;
                        result.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EDataType.R.FromBase);
                        goto Label_04E2;

                    case 'S':
                    case 's':
                        result.Content1 = MelsecA1EDataType.S;
                        result.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EDataType.S.FromBase);
                        goto Label_04E2;

                    case 'T':
                    case 't':
                        if ((address[1] != 'S') && (address[1] != 's'))
                        {
                            if ((address[1] != 'C') && (address[1] != 'c'))
                            {
                                if ((address[1] != 'N') && (address[1] != 'n'))
                                {
                                    throw new Exception(StringResources.Language.NotSupportedDataType);
                                }
                                result.Content1 = MelsecA1EDataType.TN;
                                result.Content2 = Convert.ToInt32(address.Substring(2), MelsecA1EDataType.TN.FromBase);
                            }
                            else
                            {
                                result.Content1 = MelsecA1EDataType.TC;
                                result.Content2 = Convert.ToInt32(address.Substring(2), MelsecA1EDataType.TC.FromBase);
                            }
                        }
                        else
                        {
                            result.Content1 = MelsecA1EDataType.TS;
                            result.Content2 = Convert.ToInt32(address.Substring(2), MelsecA1EDataType.TS.FromBase);
                        }
                        goto Label_04E2;

                    case 'W':
                    case 'w':
                        result.Content1 = MelsecA1EDataType.W;
                        result.Content2 = Convert.ToInt32(address.Substring(1), MelsecA1EDataType.W.FromBase);
                        goto Label_04E2;

                    case 'X':
                    case 'x':
                        result.Content1 = MelsecA1EDataType.X;
                        address = address.Substring(1);
                        if (address.StartsWith("0"))
                        {
                            result.Content2 = Convert.ToInt32(address, 8);
                        }
                        else
                        {
                            result.Content2 = Convert.ToInt32(address, MelsecA1EDataType.X.FromBase);
                        }
                        goto Label_04E2;

                    case 'Y':
                    case 'y':
                        result.Content1 = MelsecA1EDataType.Y;
                        address = address.Substring(1);
                        if (address.StartsWith("0"))
                        {
                            result.Content2 = Convert.ToInt32(address, 8);
                        }
                        else
                        {
                            result.Content2 = Convert.ToInt32(address, MelsecA1EDataType.Y.FromBase);
                        }
                        goto Label_04E2;
                }
                throw new Exception(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                result.Message = exception.Message;
                return result;
            }
        Label_04E2:
            result.IsSuccess = true;
            return result;
        }

        internal static byte[] TransAsciiByteArrayToByteArray(byte[] value)
        {
            byte[] array = new byte[value.Length / 2];
            for (int i = 0; i < (array.Length / 2); i++)
            {
                BitConverter.GetBytes(Convert.ToUInt16(Encoding.ASCII.GetString(value, i * 4, 4), 0x10)).CopyTo(array, (int) (i * 2));
            }
            return array;
        }

        internal static byte[] TransBoolArrayToByteData(bool[] value)
        {
            int num = (value.Length + 1) / 2;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                if (value[i * 2])
                {
                    buffer[i] = (byte) (buffer[i] + 0x10);
                }
                if ((((i * 2) + 1) < value.Length) && value[(i * 2) + 1])
                {
                    buffer[i] = (byte) (buffer[i] + 1);
                }
            }
            return buffer;
        }

        internal static byte[] TransBoolArrayToByteData(byte[] value)
        {
            return TransBoolArrayToByteData((from m in value select m > 0).ToArray<bool>());
        }

        internal static byte[] TransByteArrayToAsciiByteArray(byte[] value)
        {
            if (value == null)
            {
                return new byte[0];
            }
            byte[] array = new byte[value.Length * 2];
            for (int i = 0; i < (value.Length / 2); i++)
            {
                SoftBasic.BuildAsciiBytesFrom(BitConverter.ToUInt16(value, i * 2)).CopyTo(array, (int) (4 * i));
            }
            return array;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MelsecHelper.<>c <>9 = new MelsecHelper.<>c();
            public static Func<byte, bool> <>9__17_0;
            public static Func<bool, byte> <>9__6_0;

            internal byte <BuildAsciiWriteBitCoreCommand>b__6_0(bool m)
            {
                return (m ? ((byte) 0x31) : ((byte) 0x30));
            }

            internal bool <TransBoolArrayToByteData>b__17_0(byte m)
            {
                return (m > 0);
            }
        }
    }
}

