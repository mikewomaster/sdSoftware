namespace HslCommunication.Profinet.Panasonic
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using System;
    using System.Text;

    public class PanasonicHelper
    {
        public static OperateResult<string, int> AnalysisAddress(string address)
        {
            OperateResult<string, int> result = new OperateResult<string, int>();
            try
            {
                result.Content2 = 0;
                if (!address.StartsWith("IX") && !address.StartsWith("ix"))
                {
                    if (!address.StartsWith("IY") && !address.StartsWith("iy"))
                    {
                        if (!address.StartsWith("ID") && !address.StartsWith("id"))
                        {
                            if (!address.StartsWith("SR") && !address.StartsWith("sr"))
                            {
                                if (!address.StartsWith("LD") && !address.StartsWith("ld"))
                                {
                                    if ((address[0] != 'X') && (address[0] != 'x'))
                                    {
                                        if ((address[0] != 'Y') && (address[0] != 'y'))
                                        {
                                            if ((address[0] != 'R') && (address[0] != 'r'))
                                            {
                                                if ((address[0] != 'T') && (address[0] != 't'))
                                                {
                                                    if ((address[0] != 'C') && (address[0] != 'c'))
                                                    {
                                                        if ((address[0] != 'L') && (address[0] != 'l'))
                                                        {
                                                            if ((address[0] != 'D') && (address[0] != 'd'))
                                                            {
                                                                if ((address[0] != 'F') && (address[0] != 'f'))
                                                                {
                                                                    if ((address[0] != 'S') && (address[0] != 's'))
                                                                    {
                                                                        if ((address[0] != 'K') && (address[0] != 'k'))
                                                                        {
                                                                            throw new Exception(StringResources.Language.NotSupportedDataType);
                                                                        }
                                                                        result.Content1 = "K";
                                                                        result.Content2 = int.Parse(address.Substring(1));
                                                                    }
                                                                    else
                                                                    {
                                                                        result.Content1 = "S";
                                                                        result.Content2 = int.Parse(address.Substring(1));
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    result.Content1 = "F";
                                                                    result.Content2 = int.Parse(address.Substring(1));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                result.Content1 = "D";
                                                                result.Content2 = int.Parse(address.Substring(1));
                                                            }
                                                        }
                                                        else
                                                        {
                                                            result.Content1 = "L";
                                                            result.Content2 = CalculateComplexAddress(address.Substring(1));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        result.Content1 = "C";
                                                        result.Content2 = int.Parse(address.Substring(1));
                                                    }
                                                }
                                                else
                                                {
                                                    result.Content1 = "T";
                                                    result.Content2 = int.Parse(address.Substring(1));
                                                }
                                            }
                                            else
                                            {
                                                result.Content1 = "R";
                                                result.Content2 = CalculateComplexAddress(address.Substring(1));
                                            }
                                        }
                                        else
                                        {
                                            result.Content1 = "Y";
                                            result.Content2 = CalculateComplexAddress(address.Substring(1));
                                        }
                                    }
                                    else
                                    {
                                        result.Content1 = "X";
                                        result.Content2 = CalculateComplexAddress(address.Substring(1));
                                    }
                                }
                                else
                                {
                                    result.Content1 = "LD";
                                    result.Content2 = int.Parse(address.Substring(2));
                                }
                            }
                            else
                            {
                                result.Content1 = "SR";
                                result.Content2 = CalculateComplexAddress(address.Substring(2));
                            }
                        }
                        else
                        {
                            result.Content1 = "ID";
                            result.Content2 = int.Parse(address.Substring(2));
                        }
                    }
                    else
                    {
                        result.Content1 = "IY";
                        result.Content2 = int.Parse(address.Substring(2));
                    }
                }
                else
                {
                    result.Content1 = "IX";
                    result.Content2 = int.Parse(address.Substring(2));
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

        public static OperateResult<byte[]> BuildReadCommand(byte station, string address, ushort length)
        {
            if (address == null)
            {
                return new OperateResult<byte[]>(StringResources.Language.PanasonicAddressParameterCannotBeNull);
            }
            OperateResult<string, int> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            StringBuilder sb = new StringBuilder("%");
            sb.Append(station.ToString("X2"));
            sb.Append("#");
            if ((((result.Content1 == "X") || (result.Content1 == "Y")) || (result.Content1 == "R")) || (result.Content1 == "L"))
            {
                sb.Append("RCC");
                sb.Append(result.Content1);
                int num = result.Content2 / 0x10;
                int num2 = ((result.Content2 + length) - 1) / 0x10;
                sb.Append(num.ToString("D4"));
                sb.Append(num2.ToString("D4"));
            }
            else if (((result.Content1 == "D") || (result.Content1 == "LD")) || (result.Content1 == "F"))
            {
                sb.Append("RD");
                sb.Append(result.Content1.Substring(0, 1));
                sb.Append(result.Content2.ToString("D5"));
                int num3 = (result.Content2 + length) - 1;
                sb.Append(num3.ToString("D5"));
            }
            else if (((result.Content1 == "IX") || (result.Content1 == "IY")) || (result.Content1 == "ID"))
            {
                sb.Append("RD");
                sb.Append(result.Content1);
                sb.Append("000000000");
            }
            else if ((result.Content1 == "C") || (result.Content1 == "T"))
            {
                sb.Append("RS");
                sb.Append(result.Content2.ToString("D4"));
                sb.Append(((result.Content2 + length) - 1).ToString("D4"));
            }
            else
            {
                return new OperateResult<byte[]>(StringResources.Language.NotSupportedDataType);
            }
            sb.Append(CalculateCrc(sb));
            sb.Append('\r');
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(sb.ToString()));
        }

        public static OperateResult<byte[]> BuildReadOneCoil(byte station, string address)
        {
            if (address == null)
            {
                return new OperateResult<byte[]>("address is not allowed null");
            }
            if ((address.Length < 1) || (address.Length > 8))
            {
                return new OperateResult<byte[]>("length must be 1-8");
            }
            StringBuilder sb = new StringBuilder("%");
            sb.Append(station.ToString("X2"));
            sb.Append("#RCS");
            OperateResult<string, int> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            sb.Append(result.Content1);
            if ((((result.Content1 == "X") || (result.Content1 == "Y")) || (result.Content1 == "R")) || (result.Content1 == "L"))
            {
                int num = result.Content2 / 0x10;
                sb.Append(num.ToString("D3"));
                sb.Append((result.Content2 % 0x10).ToString("X1"));
            }
            else if ((result.Content1 == "T") || (result.Content1 == "C"))
            {
                sb.Append("0");
                sb.Append(result.Content2.ToString("D3"));
            }
            else
            {
                return new OperateResult<byte[]>(StringResources.Language.NotSupportedDataType);
            }
            sb.Append(CalculateCrc(sb));
            sb.Append('\r');
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(sb.ToString()));
        }

        public static OperateResult<byte[]> BuildWriteCommand(byte station, string address, byte[] values)
        {
            int num4;
            if (address == null)
            {
                return new OperateResult<byte[]>(StringResources.Language.PanasonicAddressParameterCannotBeNull);
            }
            OperateResult<string, int> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            values = SoftBasic.ArrayExpandToLengthEven<byte>(values);
            short num = (short) (values.Length / 2);
            StringBuilder sb = new StringBuilder("%");
            sb.Append(station.ToString("X2"));
            sb.Append("#");
            if ((((result.Content1 == "X") || (result.Content1 == "Y")) || (result.Content1 == "R")) || (result.Content1 == "L"))
            {
                sb.Append("WCC");
                sb.Append(result.Content1);
                int num2 = result.Content2 / 0x10;
                int num3 = ((result.Content2 + (values.Length * 8)) - 1) / 0x10;
                sb.Append(num2.ToString("D4"));
                num4 = (num3 - num2) + 1;
                sb.Append(num4.ToString("D4"));
            }
            else if (((result.Content1 == "D") || (result.Content1 == "LD")) || (result.Content1 == "F"))
            {
                sb.Append("WD");
                sb.Append(result.Content1.Substring(0, 1));
                sb.Append(result.Content2.ToString("D5"));
                num4 = (result.Content2 + num) - 1;
                sb.Append(num4.ToString("D5"));
            }
            else if (((result.Content1 == "IX") || (result.Content1 == "IY")) || (result.Content1 == "ID"))
            {
                sb.Append("WD");
                sb.Append(result.Content1);
                sb.Append(result.Content2.ToString("D9"));
                num4 = (result.Content2 + num) - 1;
                sb.Append(num4.ToString("D9"));
            }
            else if ((result.Content1 == "C") || (result.Content1 == "T"))
            {
                sb.Append("WS");
                sb.Append(result.Content2.ToString("D4"));
                sb.Append(((result.Content2 + num) - 1).ToString("D4"));
            }
            sb.Append(SoftBasic.ByteToHexString(values));
            sb.Append(CalculateCrc(sb));
            sb.Append('\r');
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(sb.ToString()));
        }

        public static OperateResult<byte[]> BuildWriteOneCoil(byte station, string address, bool value)
        {
            StringBuilder sb = new StringBuilder("%");
            sb.Append(station.ToString("X2"));
            sb.Append("#WCS");
            OperateResult<string, int> result = AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            sb.Append(result.Content1);
            if ((((result.Content1 == "X") || (result.Content1 == "Y")) || (result.Content1 == "R")) || (result.Content1 == "L"))
            {
                int num = result.Content2 / 0x10;
                sb.Append(num.ToString("D3"));
                sb.Append((result.Content2 % 0x10).ToString("X1"));
            }
            else if ((result.Content1 == "T") || (result.Content1 == "C"))
            {
                sb.Append("0");
                sb.Append(result.Content2.ToString("D3"));
            }
            else
            {
                return new OperateResult<byte[]>(StringResources.Language.NotSupportedDataType);
            }
            sb.Append(value ? '1' : '0');
            sb.Append(CalculateCrc(sb));
            sb.Append('\r');
            return OperateResult.CreateSuccessResult<byte[]>(Encoding.ASCII.GetBytes(sb.ToString()));
        }

        public static int CalculateComplexAddress(string address)
        {
            int num = 0;
            if (address.IndexOf(".") < 0)
            {
                if (address.Length == 1)
                {
                    return Convert.ToInt32(address, 0x10);
                }
                return ((Convert.ToInt32(address.Substring(0, address.Length - 1)) * 0x10) + Convert.ToInt32(address.Substring(address.Length - 1), 0x10));
            }
            num = Convert.ToInt32(address.Substring(0, address.IndexOf("."))) * 0x10;
            string str = address.Substring(address.IndexOf(".") + 1);
            if ((((str.Contains("A") || str.Contains("B")) || (str.Contains("C") || str.Contains("D"))) || str.Contains("E")) || str.Contains("F"))
            {
                return (num + Convert.ToInt32(str, 0x10));
            }
            return (num + Convert.ToInt32(str));
        }

        private static string CalculateCrc(StringBuilder sb)
        {
            byte num = 0;
            num = (byte) sb[0];
            for (int i = 1; i < sb.Length; i++)
            {
                num = (byte) (num ^ ((byte) sb[i]));
            }
            byte[] inBytes = new byte[] { num };
            return SoftBasic.ByteToHexString(inBytes);
        }

        public static OperateResult<bool> ExtraActualBool(byte[] response)
        {
            if (response.Length < 9)
            {
                return new OperateResult<bool>(StringResources.Language.PanasonicReceiveLengthMustLargerThan9);
            }
            if (response[3] == 0x24)
            {
                return OperateResult.CreateSuccessResult<bool>(response[6] == 0x31);
            }
            if (response[3] == 0x21)
            {
                int err = int.Parse(Encoding.ASCII.GetString(response, 4, 2));
                return new OperateResult<bool>(err, GetErrorDescription(err));
            }
            return new OperateResult<bool>(StringResources.Language.UnknownError);
        }

        public static OperateResult<byte[]> ExtraActualData(byte[] response)
        {
            if (response.Length < 9)
            {
                return new OperateResult<byte[]>(StringResources.Language.PanasonicReceiveLengthMustLargerThan9);
            }
            if (response[3] == 0x24)
            {
                byte[] destinationArray = new byte[response.Length - 9];
                if (destinationArray.Length > 0)
                {
                    Array.Copy(response, 6, destinationArray, 0, destinationArray.Length);
                    destinationArray = SoftBasic.HexStringToBytes(Encoding.ASCII.GetString(destinationArray));
                }
                return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
            }
            if (response[3] == 0x21)
            {
                int err = int.Parse(Encoding.ASCII.GetString(response, 4, 2));
                return new OperateResult<byte[]>(err, GetErrorDescription(err));
            }
            return new OperateResult<byte[]>(StringResources.Language.UnknownError);
        }

        public static string GetErrorDescription(int err)
        {
            switch (err)
            {
                case 20:
                    return StringResources.Language.PanasonicMewStatus20;

                case 0x15:
                    return StringResources.Language.PanasonicMewStatus21;

                case 0x16:
                    return StringResources.Language.PanasonicMewStatus22;

                case 0x17:
                    return StringResources.Language.PanasonicMewStatus23;

                case 0x18:
                    return StringResources.Language.PanasonicMewStatus24;

                case 0x19:
                    return StringResources.Language.PanasonicMewStatus25;

                case 0x1a:
                    return StringResources.Language.PanasonicMewStatus26;

                case 0x1b:
                    return StringResources.Language.PanasonicMewStatus27;

                case 0x1c:
                    return StringResources.Language.PanasonicMewStatus28;

                case 0x1d:
                    return StringResources.Language.PanasonicMewStatus29;

                case 30:
                    return StringResources.Language.PanasonicMewStatus30;

                case 40:
                    return StringResources.Language.PanasonicMewStatus40;

                case 0x29:
                    return StringResources.Language.PanasonicMewStatus41;

                case 0x2a:
                    return StringResources.Language.PanasonicMewStatus42;

                case 0x2b:
                    return StringResources.Language.PanasonicMewStatus43;

                case 50:
                    return StringResources.Language.PanasonicMewStatus50;

                case 0x33:
                    return StringResources.Language.PanasonicMewStatus51;

                case 0x34:
                    return StringResources.Language.PanasonicMewStatus52;

                case 0x35:
                    return StringResources.Language.PanasonicMewStatus53;

                case 60:
                    return StringResources.Language.PanasonicMewStatus60;

                case 0x3d:
                    return StringResources.Language.PanasonicMewStatus61;

                case 0x3e:
                    return StringResources.Language.PanasonicMewStatus62;

                case 0x3f:
                    return StringResources.Language.PanasonicMewStatus63;

                case 0x41:
                    return StringResources.Language.PanasonicMewStatus65;

                case 0x42:
                    return StringResources.Language.PanasonicMewStatus66;

                case 0x43:
                    return StringResources.Language.PanasonicMewStatus67;
            }
            return StringResources.Language.UnknownError;
        }
    }
}

