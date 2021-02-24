namespace HslCommunication.Profinet.Omron
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using System;

    public class OmronFinsNetHelper
    {
        public static OperateResult<OmronFinsDataType, byte[]> AnalysisAddress(string address, bool isBit)
        {
            OperateResult<OmronFinsDataType, byte[]> result = new OperateResult<OmronFinsDataType, byte[]>();
            try
            {
                switch (address[0])
                {
                    case 'a':
                    case 'A':
                        result.Content1 = OmronFinsDataType.AR;
                        break;

                    case 'c':
                    case 'C':
                        result.Content1 = OmronFinsDataType.CIO;
                        break;

                    case 'd':
                    case 'D':
                        result.Content1 = OmronFinsDataType.DM;
                        break;

                    case 'e':
                    case 'E':
                    {
                        char[] separator = new char[] { '.' };
                        int num = Convert.ToInt32(address.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0].Substring(1), 0x10);
                        if (num < 0x10)
                        {
                            result.Content1 = new OmronFinsDataType((byte) (0x20 + num), (byte) (160 + num));
                        }
                        else
                        {
                            result.Content1 = new OmronFinsDataType((byte) ((0xe0 + num) - 0x10), (byte) ((0x60 + num) - 0x10));
                        }
                        break;
                    }
                    case 'h':
                    case 'H':
                        result.Content1 = OmronFinsDataType.HR;
                        break;

                    case 'w':
                    case 'W':
                        result.Content1 = OmronFinsDataType.WR;
                        break;

                    default:
                        throw new Exception(StringResources.Language.NotSupportedDataType);
                }
                if ((address[0] == 'E') || (address[0] == 'e'))
                {
                    char[] chArray2 = new char[] { '.' };
                    string[] strArray2 = address.Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                    if (isBit)
                    {
                        ushort num2 = ushort.Parse(strArray2[1]);
                        result.Content2 = new byte[3];
                        result.Content2[0] = (byte[]) BitConverter.GetBytes(num2)[1];
                        result.Content2[1] = (byte[]) BitConverter.GetBytes(num2)[0];
                        if (strArray2.Length > 2)
                        {
                            result.Content2[2] = (byte[]) byte.Parse(strArray2[2]);
                            if (result.Content2[2] > 15)
                            {
                                throw new Exception(StringResources.Language.OmronAddressMustBeZeroToFifteen);
                            }
                        }
                    }
                    else
                    {
                        ushort num3 = ushort.Parse(strArray2[1]);
                        result.Content2 = new byte[3];
                        result.Content2[0] = (byte[]) BitConverter.GetBytes(num3)[1];
                        result.Content2[1] = (byte[]) BitConverter.GetBytes(num3)[0];
                    }
                }
                else if (isBit)
                {
                    char[] chArray3 = new char[] { '.' };
                    string[] strArray3 = address.Substring(1).Split(chArray3, StringSplitOptions.RemoveEmptyEntries);
                    ushort num4 = ushort.Parse(strArray3[0]);
                    result.Content2 = new byte[3];
                    result.Content2[0] = (byte[]) BitConverter.GetBytes(num4)[1];
                    result.Content2[1] = (byte[]) BitConverter.GetBytes(num4)[0];
                    if (strArray3.Length > 1)
                    {
                        result.Content2[2] = (byte[]) byte.Parse(strArray3[1]);
                        if (result.Content2[2] > 15)
                        {
                            throw new Exception(StringResources.Language.OmronAddressMustBeZeroToFifteen);
                        }
                    }
                }
                else
                {
                    ushort num5 = ushort.Parse(address.Substring(1));
                    result.Content2 = new byte[3];
                    result.Content2[0] = (byte[]) BitConverter.GetBytes(num5)[1];
                    result.Content2[1] = (byte[]) BitConverter.GetBytes(num5)[0];
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

        public static OperateResult<byte[]> BuildReadCommand(string address, ushort length, bool isBit)
        {
            OperateResult<OmronFinsDataType, byte[]> result = AnalysisAddress(address, isBit);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] array = new byte[8];
            array[0] = 1;
            array[1] = 1;
            if (isBit)
            {
                array[2] = result.Content1.BitCode;
            }
            else
            {
                array[2] = result.Content1.WordCode;
            }
            result.Content2.CopyTo(array, 3);
            array[6] = (byte) (length / 0x100);
            array[7] = (byte) (length % 0x100);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteWordCommand(string address, byte[] value, bool isBit)
        {
            OperateResult<OmronFinsDataType, byte[]> result = AnalysisAddress(address, isBit);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] array = new byte[8 + value.Length];
            array[0] = 1;
            array[1] = 2;
            if (isBit)
            {
                array[2] = result.Content1.BitCode;
            }
            else
            {
                array[2] = result.Content1.WordCode;
            }
            result.Content2.CopyTo(array, 3);
            if (isBit)
            {
                array[6] = (byte) (value.Length / 0x100);
                array[7] = (byte) (value.Length % 0x100);
            }
            else
            {
                array[6] = (byte) ((value.Length / 2) / 0x100);
                array[7] = (byte) ((value.Length / 2) % 0x100);
            }
            value.CopyTo(array, 8);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static string GetStatusDescription(int err)
        {
            switch (err)
            {
                case 0:
                    return StringResources.Language.OmronStatus0;

                case 1:
                    return StringResources.Language.OmronStatus1;

                case 2:
                    return StringResources.Language.OmronStatus2;

                case 3:
                    return StringResources.Language.OmronStatus3;

                case 20:
                    return StringResources.Language.OmronStatus20;

                case 0x15:
                    return StringResources.Language.OmronStatus21;

                case 0x16:
                    return StringResources.Language.OmronStatus22;

                case 0x17:
                    return StringResources.Language.OmronStatus23;

                case 0x18:
                    return StringResources.Language.OmronStatus24;

                case 0x19:
                    return StringResources.Language.OmronStatus25;
            }
            return StringResources.Language.UnknownError;
        }

        public static OperateResult<byte[]> ResponseValidAnalysis(byte[] response, bool isRead)
        {
            if (response.Length >= 0x10)
            {
                int err = BitConverter.ToInt32(new byte[] { response[15], response[14], response[13], response[12] }, 0);
                if (err > 0)
                {
                    return new OperateResult<byte[]>(err, GetStatusDescription(err));
                }
                byte[] destinationArray = new byte[response.Length - 0x10];
                Array.Copy(response, 0x10, destinationArray, 0, destinationArray.Length);
                return UdpResponseValidAnalysis(destinationArray, isRead);
            }
            return new OperateResult<byte[]>(StringResources.Language.OmronReceiveDataError);
        }

        public static OperateResult<byte[]> UdpResponseValidAnalysis(byte[] response, bool isRead)
        {
            if (response.Length >= 14)
            {
                int err = (response[12] * 0x100) + response[13];
                if (!isRead)
                {
                    OperateResult<byte[]> result = OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
                    result.ErrorCode = err;
                    result.Message = GetStatusDescription(err) + " Received:" + SoftBasic.ByteToHexString(response, ' ');
                    return result;
                }
                byte[] destinationArray = new byte[response.Length - 14];
                if (destinationArray.Length > 0)
                {
                    Array.Copy(response, 14, destinationArray, 0, destinationArray.Length);
                }
                OperateResult<byte[]> result3 = OperateResult.CreateSuccessResult<byte[]>(destinationArray);
                if (destinationArray.Length == 0)
                {
                    result3.IsSuccess = false;
                }
                result3.ErrorCode = err;
                result3.Message = GetStatusDescription(err) + " Received:" + SoftBasic.ByteToHexString(response, ' ');
                return result3;
            }
            return new OperateResult<byte[]>(StringResources.Language.OmronReceiveDataError);
        }
    }
}

