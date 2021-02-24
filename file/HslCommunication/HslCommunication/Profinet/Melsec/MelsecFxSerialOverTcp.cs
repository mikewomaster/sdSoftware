namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class MelsecFxSerialOverTcp : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsNewVersion>k__BackingField;

        public MelsecFxSerialOverTcp()
        {
            base.WordLength = 1;
            base.ByteTransform = new RegularByteTransform();
            this.IsNewVersion = true;
            base.ByteTransform.IsStringReverseByteWord = true;
        }

        public MelsecFxSerialOverTcp(string ipAddress, int port)
        {
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
            this.IsNewVersion = true;
            base.ByteTransform.IsStringReverseByteWord = true;
        }

        public static OperateResult<byte[], int> BuildReadBoolCommand(string address, ushort length)
        {
            OperateResult<ushort, ushort, ushort> result = FxCalculateBoolStartAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[], int>(result);
            }
            ushort num = ((((result.Content2 + length) - 1) / 8) - (result.Content2 / 8)) + 1;
            ushort num2 = result.Content1;
            byte[] array = new byte[11];
            array[0] = 2;
            array[1] = 0x30;
            array[2] = SoftBasic.BuildAsciiBytesFrom(num2)[0];
            array[3] = SoftBasic.BuildAsciiBytesFrom(num2)[1];
            array[4] = SoftBasic.BuildAsciiBytesFrom(num2)[2];
            array[5] = SoftBasic.BuildAsciiBytesFrom(num2)[3];
            array[6] = SoftBasic.BuildAsciiBytesFrom((byte) num)[0];
            array[7] = SoftBasic.BuildAsciiBytesFrom((byte) num)[1];
            array[8] = 3;
            MelsecHelper.FxCalculateCRC(array).CopyTo(array, 9);
            return OperateResult.CreateSuccessResult<byte[], int>(array, result.Content3);
        }

        public static OperateResult<byte[]> BuildReadWordCommand(string address, ushort length, bool isNewVersion)
        {
            OperateResult<ushort> result = FxCalculateWordStartAddress(address, isNewVersion);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            length = (ushort) (length * 2);
            ushort content = result.Content;
            if (isNewVersion)
            {
                byte[] buffer = new byte[13];
                buffer[0] = 2;
                buffer[1] = 0x45;
                buffer[2] = 0x30;
                buffer[3] = 0x30;
                buffer[4] = SoftBasic.BuildAsciiBytesFrom(content)[0];
                buffer[5] = SoftBasic.BuildAsciiBytesFrom(content)[1];
                buffer[6] = SoftBasic.BuildAsciiBytesFrom(content)[2];
                buffer[7] = SoftBasic.BuildAsciiBytesFrom(content)[3];
                buffer[8] = SoftBasic.BuildAsciiBytesFrom((byte) length)[0];
                buffer[9] = SoftBasic.BuildAsciiBytesFrom((byte) length)[1];
                buffer[10] = 3;
                MelsecHelper.FxCalculateCRC(buffer).CopyTo(buffer, 11);
                return OperateResult.CreateSuccessResult<byte[]>(buffer);
            }
            byte[] array = new byte[11];
            array[0] = 2;
            array[1] = 0x30;
            array[2] = SoftBasic.BuildAsciiBytesFrom(content)[0];
            array[3] = SoftBasic.BuildAsciiBytesFrom(content)[1];
            array[4] = SoftBasic.BuildAsciiBytesFrom(content)[2];
            array[5] = SoftBasic.BuildAsciiBytesFrom(content)[3];
            array[6] = SoftBasic.BuildAsciiBytesFrom((byte) length)[0];
            array[7] = SoftBasic.BuildAsciiBytesFrom((byte) length)[1];
            array[8] = 3;
            MelsecHelper.FxCalculateCRC(array).CopyTo(array, 9);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteBoolPacket(string address, bool value)
        {
            OperateResult<MelsecMcDataType, ushort> result = FxAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            ushort num = result.Content2;
            if (result.Content1 == MelsecMcDataType.M)
            {
                if (num >= 0x1f40)
                {
                    num = (ushort) ((num - 0x1f40) + 0xf00);
                }
                else
                {
                    num = (ushort) (num + 0x800);
                }
            }
            else if (result.Content1 == MelsecMcDataType.S)
            {
                num = num;
            }
            else if (result.Content1 == MelsecMcDataType.X)
            {
                num = (ushort) (num + 0x400);
            }
            else if (result.Content1 == MelsecMcDataType.Y)
            {
                num = (ushort) (num + 0x500);
            }
            else if (result.Content1 == MelsecMcDataType.CS)
            {
                num = (ushort) (num + 0x1c0);
            }
            else if (result.Content1 == MelsecMcDataType.CC)
            {
                num = (ushort) (num + 960);
            }
            else if (result.Content1 == MelsecMcDataType.CN)
            {
                num = (ushort) (num + 0xe00);
            }
            else if (result.Content1 == MelsecMcDataType.TS)
            {
                num = (ushort) (num + 0xc0);
            }
            else if (result.Content1 == MelsecMcDataType.TC)
            {
                num = (ushort) (num + 0x2c0);
            }
            else if (result.Content1 == MelsecMcDataType.TN)
            {
                num = (ushort) (num + 0x600);
            }
            else
            {
                return new OperateResult<byte[]>(StringResources.Language.MelsecCurrentTypeNotSupportedBitOperate);
            }
            byte[] array = new byte[9];
            array[0] = 2;
            array[1] = value ? ((byte) 0x37) : ((byte) 0x38);
            array[2] = SoftBasic.BuildAsciiBytesFrom(num)[2];
            array[3] = SoftBasic.BuildAsciiBytesFrom(num)[3];
            array[4] = SoftBasic.BuildAsciiBytesFrom(num)[0];
            array[5] = SoftBasic.BuildAsciiBytesFrom(num)[1];
            array[6] = 3;
            MelsecHelper.FxCalculateCRC(array).CopyTo(array, 7);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteWordCommand(string address, byte[] value, bool isNewVersion)
        {
            OperateResult<ushort> result = FxCalculateWordStartAddress(address, isNewVersion);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            if (value > null)
            {
                value = SoftBasic.BuildAsciiBytesFrom(value);
            }
            ushort content = result.Content;
            if (isNewVersion)
            {
                byte[] buffer = new byte[13 + value.Length];
                buffer[0] = 2;
                buffer[1] = 0x45;
                buffer[2] = 0x31;
                buffer[3] = 0x30;
                buffer[4] = SoftBasic.BuildAsciiBytesFrom(content)[0];
                buffer[5] = SoftBasic.BuildAsciiBytesFrom(content)[1];
                buffer[6] = SoftBasic.BuildAsciiBytesFrom(content)[2];
                buffer[7] = SoftBasic.BuildAsciiBytesFrom(content)[3];
                buffer[8] = SoftBasic.BuildAsciiBytesFrom((byte) (value.Length / 2))[0];
                buffer[9] = SoftBasic.BuildAsciiBytesFrom((byte) (value.Length / 2))[1];
                Array.Copy(value, 0, buffer, 10, value.Length);
                buffer[buffer.Length - 3] = 3;
                MelsecHelper.FxCalculateCRC(buffer).CopyTo(buffer, (int) (buffer.Length - 2));
                return OperateResult.CreateSuccessResult<byte[]>(buffer);
            }
            byte[] destinationArray = new byte[11 + value.Length];
            destinationArray[0] = 2;
            destinationArray[1] = 0x31;
            destinationArray[2] = SoftBasic.BuildAsciiBytesFrom(content)[0];
            destinationArray[3] = SoftBasic.BuildAsciiBytesFrom(content)[1];
            destinationArray[4] = SoftBasic.BuildAsciiBytesFrom(content)[2];
            destinationArray[5] = SoftBasic.BuildAsciiBytesFrom(content)[3];
            destinationArray[6] = SoftBasic.BuildAsciiBytesFrom((byte) (value.Length / 2))[0];
            destinationArray[7] = SoftBasic.BuildAsciiBytesFrom((byte) (value.Length / 2))[1];
            Array.Copy(value, 0, destinationArray, 8, value.Length);
            destinationArray[destinationArray.Length - 3] = 3;
            MelsecHelper.FxCalculateCRC(destinationArray).CopyTo(destinationArray, (int) (destinationArray.Length - 2));
            return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
        }

        public static OperateResult CheckPlcReadResponse(byte[] ack)
        {
            if (ack.Length == 0)
            {
                return new OperateResult(StringResources.Language.MelsecFxReceiveZero);
            }
            if (ack[0] == 0x15)
            {
                return new OperateResult(StringResources.Language.MelsecFxAckNagative + " Actual: " + SoftBasic.ByteToHexString(ack, ' '));
            }
            if (ack[0] != 2)
            {
                return new OperateResult(StringResources.Language.MelsecFxAckWrong + ack[0].ToString() + " Actual: " + SoftBasic.ByteToHexString(ack, ' '));
            }
            if (!MelsecHelper.CheckCRC(ack))
            {
                return new OperateResult(StringResources.Language.MelsecFxCrcCheckFailed);
            }
            return OperateResult.CreateSuccessResult();
        }

        public static OperateResult CheckPlcWriteResponse(byte[] ack)
        {
            if (ack.Length == 0)
            {
                return new OperateResult(StringResources.Language.MelsecFxReceiveZero);
            }
            if (ack[0] == 0x15)
            {
                return new OperateResult(StringResources.Language.MelsecFxAckNagative + " Actual: " + SoftBasic.ByteToHexString(ack, ' '));
            }
            if (ack[0] != 6)
            {
                return new OperateResult(StringResources.Language.MelsecFxAckWrong + ack[0].ToString() + " Actual: " + SoftBasic.ByteToHexString(ack, ' '));
            }
            return OperateResult.CreateSuccessResult();
        }

        public static OperateResult<bool[]> ExtractActualBoolData(byte[] response, int start, int length)
        {
            OperateResult<byte[]> result = ExtractActualData(response);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            try
            {
                bool[] flagArray = new bool[length];
                bool[] flagArray2 = SoftBasic.ByteToBoolArray(result.Content, result.Content.Length * 8);
                for (int i = 0; i < length; i++)
                {
                    flagArray[i] = flagArray2[i + start];
                }
                return OperateResult.CreateSuccessResult<bool[]>(flagArray);
            }
            catch (Exception exception)
            {
                OperateResult<bool[]> result3 = new OperateResult<bool[]>();
                string[] textArray1 = new string[] { "Extract Msg：", exception.Message, Environment.NewLine, "Data: ", SoftBasic.ByteToHexString(response) };
                result3.Message = string.Concat(textArray1);
                return result3;
            }
        }

        public static OperateResult<byte[]> ExtractActualData(byte[] response)
        {
            try
            {
                byte[] buffer = new byte[(response.Length - 4) / 2];
                for (int i = 0; i < buffer.Length; i++)
                {
                    byte[] bytes = new byte[] { response[(i * 2) + 1], response[(i * 2) + 2] };
                    buffer[i] = Convert.ToByte(Encoding.ASCII.GetString(bytes), 0x10);
                }
                return OperateResult.CreateSuccessResult<byte[]>(buffer);
            }
            catch (Exception exception)
            {
                OperateResult<byte[]> result2 = new OperateResult<byte[]>();
                string[] textArray1 = new string[] { "Extract Msg：", exception.Message, Environment.NewLine, "Data: ", SoftBasic.ByteToHexString(response) };
                result2.Message = string.Concat(textArray1);
                return result2;
            }
        }

        private static OperateResult<MelsecMcDataType, ushort> FxAnalysisAddress(string address)
        {
            OperateResult<MelsecMcDataType, ushort> result = new OperateResult<MelsecMcDataType, ushort>();
            try
            {
                switch (address[0])
                {
                    case 'S':
                    case 's':
                        result.Content1 = MelsecMcDataType.S;
                        result.Content2 = Convert.ToUInt16(address.Substring(1), MelsecMcDataType.S.FromBase);
                        goto Label_03A3;

                    case 'T':
                    case 't':
                        if ((address[1] != 'N') && (address[1] != 'n'))
                        {
                            if ((address[1] != 'S') && (address[1] != 's'))
                            {
                                if ((address[1] != 'C') && (address[1] != 'c'))
                                {
                                    throw new Exception(StringResources.Language.NotSupportedDataType);
                                }
                                result.Content1 = MelsecMcDataType.TC;
                                result.Content2 = Convert.ToUInt16(address.Substring(2), MelsecMcDataType.TC.FromBase);
                            }
                            else
                            {
                                result.Content1 = MelsecMcDataType.TS;
                                result.Content2 = Convert.ToUInt16(address.Substring(2), MelsecMcDataType.TS.FromBase);
                            }
                        }
                        else
                        {
                            result.Content1 = MelsecMcDataType.TN;
                            result.Content2 = Convert.ToUInt16(address.Substring(2), MelsecMcDataType.TN.FromBase);
                        }
                        goto Label_03A3;

                    case 'X':
                    case 'x':
                        result.Content1 = MelsecMcDataType.X;
                        result.Content2 = Convert.ToUInt16(address.Substring(1), 8);
                        goto Label_03A3;

                    case 'Y':
                    case 'y':
                        result.Content1 = MelsecMcDataType.Y;
                        result.Content2 = Convert.ToUInt16(address.Substring(1), 8);
                        goto Label_03A3;

                    case 'M':
                    case 'm':
                        result.Content1 = MelsecMcDataType.M;
                        result.Content2 = Convert.ToUInt16(address.Substring(1), MelsecMcDataType.M.FromBase);
                        goto Label_03A3;

                    case 'C':
                    case 'c':
                        if ((address[1] != 'N') && (address[1] != 'n'))
                        {
                            if ((address[1] != 'S') && (address[1] != 's'))
                            {
                                if ((address[1] != 'C') && (address[1] != 'c'))
                                {
                                    throw new Exception(StringResources.Language.NotSupportedDataType);
                                }
                                result.Content1 = MelsecMcDataType.CC;
                                result.Content2 = Convert.ToUInt16(address.Substring(2), MelsecMcDataType.CC.FromBase);
                            }
                            else
                            {
                                result.Content1 = MelsecMcDataType.CS;
                                result.Content2 = Convert.ToUInt16(address.Substring(2), MelsecMcDataType.CS.FromBase);
                            }
                        }
                        else
                        {
                            result.Content1 = MelsecMcDataType.CN;
                            result.Content2 = Convert.ToUInt16(address.Substring(2), MelsecMcDataType.CN.FromBase);
                        }
                        goto Label_03A3;

                    case 'D':
                    case 'd':
                        result.Content1 = MelsecMcDataType.D;
                        result.Content2 = Convert.ToUInt16(address.Substring(1), MelsecMcDataType.D.FromBase);
                        goto Label_03A3;
                }
                throw new Exception(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                result.Message = exception.Message;
                return result;
            }
        Label_03A3:
            result.IsSuccess = true;
            return result;
        }

        private static OperateResult<ushort, ushort, ushort> FxCalculateBoolStartAddress(string address)
        {
            OperateResult<MelsecMcDataType, ushort> result = FxAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<ushort, ushort, ushort>(result);
            }
            ushort num = result.Content2;
            if (result.Content1 == MelsecMcDataType.M)
            {
                if (num >= 0x1f40)
                {
                    num = (ushort) (((num - 0x1f40) / 8) + 480);
                }
                else
                {
                    num = (ushort) ((num / 8) + 0x100);
                }
            }
            else if (result.Content1 == MelsecMcDataType.X)
            {
                num = (ushort) ((num / 8) + 0x80);
            }
            else if (result.Content1 == MelsecMcDataType.Y)
            {
                num = (ushort) ((num / 8) + 160);
            }
            else if (result.Content1 == MelsecMcDataType.S)
            {
                num = (ushort) (num / 8);
            }
            else if (result.Content1 == MelsecMcDataType.CS)
            {
                num = (ushort) ((num / 8) + 0x1c0);
            }
            else if (result.Content1 == MelsecMcDataType.CC)
            {
                num = (ushort) ((num / 8) + 960);
            }
            else if (result.Content1 == MelsecMcDataType.TS)
            {
                num = (ushort) ((num / 8) + 0xc0);
            }
            else if (result.Content1 == MelsecMcDataType.TC)
            {
                num = (ushort) ((num / 8) + 0x2c0);
            }
            else
            {
                return new OperateResult<ushort, ushort, ushort>(StringResources.Language.MelsecCurrentTypeNotSupportedBitOperate);
            }
            return OperateResult.CreateSuccessResult<ushort, ushort, ushort>(num, result.Content2, result.Content2 % 8);
        }

        private static OperateResult<ushort> FxCalculateWordStartAddress(string address, bool isNewVersion)
        {
            OperateResult<MelsecMcDataType, ushort> result = FxAnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<ushort>(result);
            }
            ushort num = result.Content2;
            if (result.Content1 == MelsecMcDataType.D)
            {
                if (num >= 0x1f40)
                {
                    num = (ushort) (((num - 0x1f40) * 2) + 0xe00);
                }
                else
                {
                    num = isNewVersion ? ((ushort) ((num * 2) + 0x4000)) : ((ushort) ((num * 2) + 0x1000));
                }
            }
            else if (result.Content1 == MelsecMcDataType.CN)
            {
                if (num >= 200)
                {
                    num = (ushort) (((num - 200) * 4) + 0xc00);
                }
                else
                {
                    num = (ushort) ((num * 2) + 0xa00);
                }
            }
            else if (result.Content1 == MelsecMcDataType.TN)
            {
                num = (ushort) ((num * 2) + 0x800);
            }
            else
            {
                return new OperateResult<ushort>(StringResources.Language.MelsecCurrentTypeNotSupportedWordOperate);
            }
            return OperateResult.CreateSuccessResult<ushort>(num);
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            return ReadHelper(address, length, new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer), this.IsNewVersion);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            return ReadBoolHelper(address, length, new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        public static OperateResult<bool[]> ReadBoolHelper(string address, ushort length, Func<byte[], OperateResult<byte[]>> readCore)
        {
            OperateResult<byte[], int> result = BuildReadBoolCommand(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = readCore(result.Content1);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult result3 = CheckPlcReadResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            return ExtractActualBoolData(result2.Content, result.Content2, length);
        }

        public static OperateResult<byte[]> ReadHelper(string address, ushort length, Func<byte[], OperateResult<byte[]>> readCore, bool isNewVersion)
        {
            OperateResult<byte[]> result = BuildReadWordCommand(address, length, isNewVersion);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = readCore(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult result3 = CheckPlcReadResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return ExtractActualData(result2.Content);
        }

        public override string ToString()
        {
            return string.Format("MelsecFxSerialOverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            return WriteHelper(address, value, new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer), this.IsNewVersion);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            return WriteHelper(address, value, new Func<byte[], OperateResult<byte[]>>(this.ReadFromCoreServer));
        }

        public static OperateResult WriteHelper(string address, bool value, Func<byte[], OperateResult<byte[]>> readCore)
        {
            OperateResult<byte[]> result = BuildWriteBoolPacket(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = readCore(result.Content);
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

        public static OperateResult WriteHelper(string address, byte[] value, Func<byte[], OperateResult<byte[]>> readCore, bool isNewVersion)
        {
            OperateResult<byte[]> result = BuildWriteWordCommand(address, value, isNewVersion);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = readCore(result.Content);
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

        public bool IsNewVersion { get; set; }
    }
}

