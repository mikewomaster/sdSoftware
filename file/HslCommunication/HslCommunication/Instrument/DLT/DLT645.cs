namespace HslCommunication.Instrument.DLT
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Serial;
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class DLT645 : SerialDeviceBase
    {
        private string opCode = "00000000";
        private string password = "00000000";
        private string station = "1";

        public DLT645(string station, [Optional, DefaultParameterValue("")] string password, [Optional, DefaultParameterValue("")] string opCode)
        {
            base.ByteTransform = new RegularByteTransform();
            this.station = station;
            this.password = string.IsNullOrEmpty(password) ? "00000000" : password;
            this.opCode = string.IsNullOrEmpty(opCode) ? "00000000" : opCode;
        }

        public OperateResult ActiveDeveice()
        {
            return base.ReadBase(new byte[] { 0xfe, 0xfe, 0xfe, 0xfe }, true);
        }

        public static OperateResult<string, byte[]> AnalysisBytesAddress(string address, string defaultStation, [Optional, DefaultParameterValue(1)] ushort length)
        {
            string str = defaultStation;
            byte[] array = (length == 1) ? new byte[4] : new byte[5];
            if (length != 1)
            {
                array[4] = (byte) length;
            }
            if (address.IndexOf(';') > 0)
            {
                char[] separator = new char[] { ';' };
                string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].StartsWith("s="))
                    {
                        str = strArray[i].Substring(2);
                    }
                    else
                    {
                        strArray[i].ToHexBytes().Reverse<byte>().ToArray<byte>().CopyTo(array, 0);
                    }
                }
            }
            else
            {
                address.ToHexBytes().Reverse<byte>().ToArray<byte>().CopyTo(array, 0);
            }
            return OperateResult.CreateSuccessResult<string, byte[]>(str, array);
        }

        public static OperateResult<string, int> AnalysisIntegerAddress(string address, string defaultStation)
        {
            try
            {
                string str = defaultStation;
                int num = 0;
                if (address.IndexOf(';') > 0)
                {
                    char[] separator = new char[] { ';' };
                    string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (strArray[i].StartsWith("s="))
                        {
                            str = strArray[i].Substring(2);
                        }
                        else
                        {
                            num = Convert.ToInt32(strArray[i]);
                        }
                    }
                }
                else
                {
                    num = Convert.ToInt32(address);
                }
                return OperateResult.CreateSuccessResult<string, int>(str, num);
            }
            catch (Exception exception)
            {
                return new OperateResult<string, int>(exception.Message);
            }
        }

        public OperateResult BroadcastTime(DateTime dateTime)
        {
            string str = string.Format("{0:D2}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", new object[] { dateTime.Second, dateTime.Minute, dateTime.Hour, dateTime.Day, dateTime.Month, dateTime.Year % 100 });
            OperateResult<byte[]> result = BuildEntireCommand("999999999999", 8, str.ToHexBytes());
            if (!result.IsSuccess)
            {
                return result;
            }
            return base.ReadBase(result.Content, true);
        }

        public static OperateResult<byte[]> BuildEntireCommand(string address, byte control, byte[] dataArea)
        {
            if (dataArea == null)
            {
                dataArea = new byte[0];
            }
            OperateResult<byte[]> addressByteFromString = GetAddressByteFromString(address);
            if (!addressByteFromString.IsSuccess)
            {
                return addressByteFromString;
            }
            byte[] array = new byte[12 + dataArea.Length];
            array[0] = 0x68;
            addressByteFromString.Content.CopyTo(array, 1);
            array[7] = 0x68;
            array[8] = control;
            array[9] = (byte) dataArea.Length;
            if (dataArea.Length > 0)
            {
                dataArea.CopyTo(array, 10);
                for (int j = 0; j < dataArea.Length; j++)
                {
                    array[j + 10] = (byte) (array[j + 10] + 0x33);
                }
            }
            int num = 0;
            for (int i = 0; i < (array.Length - 2); i++)
            {
                num += array[i];
            }
            array[array.Length - 2] = (byte) num;
            array[array.Length - 1] = 0x16;
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public OperateResult ChangeBaudRate(string baudRate)
        {
            OperateResult<string, int> result = AnalysisIntegerAddress(baudRate, this.station);
            if (!result.IsSuccess)
            {
                return result;
            }
            byte num = 0;
            switch (result.Content2)
            {
                case 600:
                    num = 2;
                    break;

                case 0x4b0:
                    num = 4;
                    break;

                case 0x960:
                    num = 8;
                    break;

                case 0x12c0:
                    num = 0x10;
                    break;

                case 0x2580:
                    num = 0x20;
                    break;

                case 0x4b00:
                    num = 0x40;
                    break;

                default:
                    return new OperateResult(StringResources.Language.NotSupportedFunction);
            }
            byte[] dataArea = new byte[] { num };
            OperateResult<byte[]> result2 = BuildEntireCommand(result.Content1, 0x17, dataArea);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadBase(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            OperateResult result4 = CheckResponse(result3.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            if (result3.Content[10] == num)
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult(StringResources.Language.DLTErrorWriteReadCheckFailed);
        }

        public static OperateResult CheckResponse(byte[] response)
        {
            if (response.Length < 9)
            {
                return new OperateResult(StringResources.Language.ReceiveDataLengthTooShort);
            }
            if ((response[8] & 0x40) == 0x40)
            {
                byte num = response[10];
                if (num.GetBoolOnIndex(0))
                {
                    return new OperateResult(StringResources.Language.DLTErrorInfoBit0);
                }
                if (num.GetBoolOnIndex(1))
                {
                    return new OperateResult(StringResources.Language.DLTErrorInfoBit1);
                }
                if (num.GetBoolOnIndex(2))
                {
                    return new OperateResult(StringResources.Language.DLTErrorInfoBit2);
                }
                if (num.GetBoolOnIndex(3))
                {
                    return new OperateResult(StringResources.Language.DLTErrorInfoBit3);
                }
                if (num.GetBoolOnIndex(4))
                {
                    return new OperateResult(StringResources.Language.DLTErrorInfoBit4);
                }
                if (num.GetBoolOnIndex(5))
                {
                    return new OperateResult(StringResources.Language.DLTErrorInfoBit5);
                }
                if (num.GetBoolOnIndex(6))
                {
                    return new OperateResult(StringResources.Language.DLTErrorInfoBit6);
                }
                if (num.GetBoolOnIndex(7))
                {
                    return new OperateResult(StringResources.Language.DLTErrorInfoBit7);
                }
                return OperateResult.CreateSuccessResult();
            }
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult FreezeCommand(string dataArea)
        {
            OperateResult<string, byte[]> result = AnalysisBytesAddress(dataArea, this.station, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = BuildEntireCommand(result.Content1, 0x16, result.Content2);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result.Content1 == "999999999999")
            {
                return base.ReadBase(result2.Content, true);
            }
            OperateResult<byte[]> result4 = base.ReadBase(result2.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            return CheckResponse(result4.Content);
        }

        public static OperateResult<byte[]> GetAddressByteFromString(string address)
        {
            if ((address == null) || (address.Length == 0))
            {
                return new OperateResult<byte[]>(StringResources.Language.DLTAddressCannotNull);
            }
            if (address.Length > 12)
            {
                return new OperateResult<byte[]>(StringResources.Language.DLTAddressCannotMoreThan12);
            }
            if (!Regex.IsMatch(address, "^[0-9A-A]+$"))
            {
                return new OperateResult<byte[]>(StringResources.Language.DLTAddressMatchFailed);
            }
            if (address.Length < 12)
            {
                address = address.PadLeft(12, '0');
            }
            return OperateResult.CreateSuccessResult<byte[]>(address.ToHexBytes().Reverse<byte>().ToArray<byte>());
        }

        public static string GetFormatWithDataArea(byte[] dataArea)
        {
            if (dataArea[3] != 0)
            {
                if (dataArea[3] == 1)
                {
                    return "XX.XXXX";
                }
                if ((dataArea[3] == 2) && (dataArea[2] == 1))
                {
                    return "XXX.X";
                }
                if ((dataArea[3] == 2) && (dataArea[2] == 2))
                {
                    return "XXX.XXX";
                }
                if ((dataArea[3] == 2) && (dataArea[2] < 6))
                {
                    return "XX.XXXX";
                }
                if ((dataArea[3] == 2) && (dataArea[2] == 6))
                {
                    return "X.XXX";
                }
                if ((dataArea[3] == 2) && (dataArea[2] == 7))
                {
                    return "XXX.X";
                }
                if ((dataArea[3] == 2) && (dataArea[2] < 0x80))
                {
                    return "XX.XX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 1))
                {
                    return "XXX.XXX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 2))
                {
                    return "XX.XX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 3))
                {
                    return "XX.XXXX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 4))
                {
                    return "XX.XXXX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 5))
                {
                    return "XX.XXXX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 6))
                {
                    return "XX.XXXX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 7))
                {
                    return "XX.XX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 8))
                {
                    return "XX.XX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 9))
                {
                    return "XX.XX";
                }
                if (((dataArea[3] == 2) && (dataArea[2] == 0x80)) && (dataArea[0] == 10))
                {
                    return "XXXXXXXX";
                }
                if ((((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 4)) && (dataArea[0] <= 2))
                {
                    return "XXXXXXXXXXXX";
                }
                if ((((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 4)) && (dataArea[0] == 9))
                {
                    return "XXXXXX";
                }
                if ((((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 4)) && (dataArea[0] == 10))
                {
                    return "XXXXXX";
                }
                if (((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 5))
                {
                    return "XXXX";
                }
                if (((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 6))
                {
                    return "XX";
                }
                if (((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 7))
                {
                    return "XX";
                }
                if (((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 8))
                {
                    return "XX";
                }
                if (((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 9))
                {
                    return "XX";
                }
                if (((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 13))
                {
                    return "X.XXX";
                }
                if ((((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 14)) && (dataArea[0] < 3))
                {
                    return "XX.XXXX";
                }
                if (((dataArea[3] == 4) && (dataArea[2] == 0)) && (dataArea[1] == 14))
                {
                    return "XXX.X";
                }
            }
            return "XXXXXX.XX";
        }

        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<string, byte[]> result = AnalysisBytesAddress(address, this.station, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return this.ReadWithAddress(result.Content1, result.Content2);
        }

        public OperateResult<string> ReadAddress()
        {
            OperateResult<byte[]> result = BuildEntireCommand("AAAAAAAAAAAA", 0x13, null);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            OperateResult result3 = CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result3);
            }
            this.station = result2.Content.SelectMiddle<byte>(1, 6).Reverse<byte>().ToArray<byte>().ToHexString();
            return OperateResult.CreateSuccessResult<string>(result2.Content.SelectMiddle<byte>(1, 6).Reverse<byte>().ToArray<byte>().ToHexString());
        }

        public override OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            OperateResult<string, byte[]> result = AnalysisBytesAddress(address, this.station, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<double[]>(result);
            }
            OperateResult<byte[]> result2 = this.ReadWithAddress(result.Content1, result.Content2);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<double[]>(result2);
            }
            return DLTTransform.TransDoubleFromDLt(result2.Content, length, GetFormatWithDataArea(result.Content2));
        }

        public override OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            OperateResult<byte[]> result = this.Read(address, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            return DLTTransform.TransStringFromDLt(result.Content, length);
        }

        private OperateResult<byte[]> ReadWithAddress(string address, byte[] dataArea)
        {
            OperateResult<byte[]> result = BuildEntireCommand(address, 0x11, dataArea);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            if (result2.Content.Length < 0x10)
            {
                return OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result2.Content.SelectMiddle<byte>(14, result2.Content.Length - 0x10));
        }

        public override string ToString()
        {
            return string.Format("DLT645[{0}:{1}]", base.PortName, base.BaudRate);
        }

        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<string, byte[]> result = AnalysisBytesAddress(address, this.station, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[][] bytes = new byte[][] { result.Content2, this.password.ToHexBytes(), this.opCode.ToHexBytes(), value };
            byte[] dataArea = SoftBasic.SpliceByteArray(bytes);
            OperateResult<byte[]> result2 = BuildEntireCommand(result.Content1, 0x15, dataArea);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadBase(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return CheckResponse(result3.Content);
        }

        public OperateResult WriteAddress(string address)
        {
            OperateResult<byte[]> addressByteFromString = GetAddressByteFromString(address);
            if (!addressByteFromString.IsSuccess)
            {
                return addressByteFromString;
            }
            OperateResult<byte[]> result2 = BuildEntireCommand("AAAAAAAAAAAA", 0x15, addressByteFromString.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadBase(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            OperateResult result4 = CheckResponse(result3.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            if (SoftBasic.IsTwoBytesEquel(result3.Content.SelectMiddle<byte>(1, 6), GetAddressByteFromString(address).Content))
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult(StringResources.Language.DLTErrorWriteReadCheckFailed);
        }

        public string Station
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

