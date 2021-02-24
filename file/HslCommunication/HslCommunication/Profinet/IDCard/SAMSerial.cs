namespace HslCommunication.Profinet.IDCard
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO.Ports;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class SAMSerial : SerialBase
    {
        public static byte[] BuildReadCommand(byte cmd, byte para, byte[] data)
        {
            if (data == null)
            {
                data = new byte[0];
            }
            byte[] array = new byte[2 + data.Length];
            array[0] = cmd;
            array[1] = para;
            data.CopyTo(array, 2);
            return array;
        }

        public static OperateResult CheckADSCommandAndSum(byte[] input)
        {
            if ((input != null) ? (input.Length < 8) : false)
            {
                return new OperateResult(StringResources.Language.SAMReceiveLengthMustLargerThan8);
            }
            if ((((input[0] != 170) || (input[1] != 170)) || ((input[2] != 170) || (input[3] != 150))) || (input[4] != 0x69))
            {
                return new OperateResult(StringResources.Language.SAMHeadCheckFailed);
            }
            if (((input[5] * 0x100) + input[6]) != (input.Length - 7))
            {
                return new OperateResult(StringResources.Language.SAMLengthCheckFailed);
            }
            int num = 0;
            for (int i = 5; i < (input.Length - 1); i++)
            {
                num ^= input[i];
            }
            if (num != input[input.Length - 1])
            {
                return new OperateResult(StringResources.Language.SAMSumCheckFailed);
            }
            return OperateResult.CreateSuccessResult();
        }

        public static bool CheckADSCommandCompletion(List<byte> input)
        {
            if ((input != null) ? (input.Count < 8) : false)
            {
                return false;
            }
            if (((input[5] * 0x100) + input[6]) > (input.Count - 7))
            {
                return false;
            }
            return true;
        }

        [HslMqttApi]
        public OperateResult CheckSafeModuleStatus()
        {
            byte[] send = PackToSAMCommand(BuildReadCommand(0x12, 0xff, null));
            OperateResult<byte[]> result = base.ReadBase(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            if (result.Content[9] != 0x90)
            {
                return new OperateResult(GetErrorDescription(result.Content[9]));
            }
            return OperateResult.CreateSuccessResult();
        }

        public static OperateResult<IdentityCard> ExtractIdentityCard(byte[] data)
        {
            try
            {
                if (data[9] != 0x90)
                {
                    return new OperateResult<IdentityCard>(GetErrorDescription(data[9]));
                }
                string str = Encoding.Unicode.GetString(data, 14, 0x100);
                byte[] buffer = SoftBasic.ArraySelectMiddle<byte>(data, 270, 0x400);
                IdentityCard card = new IdentityCard {
                    Name = str.Substring(0, 15),
                    Sex = (str.Substring(15, 1) == "1") ? "男" : ((str.Substring(15, 1) == "2") ? "女" : "未知"),
                    Nation = GetNationText(Convert.ToInt32(str.Substring(0x10, 2))),
                    Birthday = new DateTime(int.Parse(str.Substring(0x12, 4)), int.Parse(str.Substring(0x16, 2)), int.Parse(str.Substring(0x18, 2))),
                    Address = str.Substring(0x1a, 0x23),
                    Id = str.Substring(0x3d, 0x12),
                    Organ = str.Substring(0x4f, 15),
                    ValidityStartDate = new DateTime(int.Parse(str.Substring(0x5e, 4)), int.Parse(str.Substring(0x62, 2)), int.Parse(str.Substring(100, 2))),
                    ValidityEndDate = new DateTime(int.Parse(str.Substring(0x66, 4)), int.Parse(str.Substring(0x6a, 2)), int.Parse(str.Substring(0x6c, 2))),
                    Portrait = buffer
                };
                return OperateResult.CreateSuccessResult<IdentityCard>(card);
            }
            catch (Exception exception)
            {
                return new OperateResult<IdentityCard>(exception.Message);
            }
        }

        public static OperateResult<string> ExtractSafeModuleNumber(byte[] data)
        {
            try
            {
                if (data[9] != 0x90)
                {
                    return new OperateResult<string>(GetErrorDescription(data[9]));
                }
                StringBuilder builder = new StringBuilder();
                builder.Append(data[10].ToString("D2"));
                builder.Append(".");
                builder.Append(data[12].ToString("D2"));
                builder.Append("-");
                builder.Append(BitConverter.ToInt32(data, 14).ToString());
                builder.Append("-");
                builder.Append(BitConverter.ToInt32(data, 0x12).ToString("D9"));
                builder.Append("-");
                builder.Append(BitConverter.ToInt32(data, 0x16).ToString("D9"));
                return OperateResult.CreateSuccessResult<string>(builder.ToString());
            }
            catch (Exception exception)
            {
                return new OperateResult<string>("Error:" + exception.Message + "  Source Data: " + SoftBasic.ByteToHexString(data));
            }
        }

        public static string GetErrorDescription(int err)
        {
            switch (err)
            {
                case 0x10:
                    return StringResources.Language.SAMStatus10;

                case 0x11:
                    return StringResources.Language.SAMStatus11;

                case 0x21:
                    return StringResources.Language.SAMStatus21;

                case 0x23:
                    return StringResources.Language.SAMStatus23;

                case 0x24:
                    return StringResources.Language.SAMStatus24;

                case 0x31:
                    return StringResources.Language.SAMStatus31;

                case 50:
                    return StringResources.Language.SAMStatus32;

                case 0x33:
                    return StringResources.Language.SAMStatus33;

                case 0x40:
                    return StringResources.Language.SAMStatus40;

                case 0x41:
                    return StringResources.Language.SAMStatus41;

                case 0x47:
                    return StringResources.Language.SAMStatus47;

                case 0x60:
                    return StringResources.Language.SAMStatus60;

                case 0x66:
                    return StringResources.Language.SAMStatus66;

                case 0x80:
                    return StringResources.Language.SAMStatus80;

                case 0x81:
                    return StringResources.Language.SAMStatus81;

                case 0x91:
                    return StringResources.Language.SAMStatus91;
            }
            return StringResources.Language.UnknownError;
        }

        public static IEnumerator<string> GetNationEnumerator()
        {
            this.<i>5__1 = 1;
            while (this.<i>5__1 < 0x39)
            {
                yield return GetNationText(this.<i>5__1);
                int num2 = this.<i>5__1;
                this.<i>5__1 = num2 + 1;
            }
        }

        public static string GetNationText(int nation)
        {
            switch (nation)
            {
                case 1:
                    return "汉";

                case 2:
                    return "蒙古";

                case 3:
                    return "回";

                case 4:
                    return "藏";

                case 5:
                    return "维吾尔";

                case 6:
                    return "苗";

                case 7:
                    return "彝";

                case 8:
                    return "壮";

                case 9:
                    return "布依";

                case 10:
                    return "朝鲜";

                case 11:
                    return "满";

                case 12:
                    return "侗";

                case 13:
                    return "瑶";

                case 14:
                    return "白";

                case 15:
                    return "土家";

                case 0x10:
                    return "哈尼";

                case 0x11:
                    return "哈萨克";

                case 0x12:
                    return "傣";

                case 0x13:
                    return "黎";

                case 20:
                    return "傈僳";

                case 0x15:
                    return "佤";

                case 0x16:
                    return "畲";

                case 0x17:
                    return "高山";

                case 0x18:
                    return "拉祜";

                case 0x19:
                    return "水";

                case 0x1a:
                    return "东乡";

                case 0x1b:
                    return "纳西";

                case 0x1c:
                    return "景颇";

                case 0x1d:
                    return "柯尔克孜";

                case 30:
                    return "土";

                case 0x1f:
                    return "达斡尔";

                case 0x20:
                    return "仫佬";

                case 0x21:
                    return "羌";

                case 0x22:
                    return "布朗";

                case 0x23:
                    return "撒拉";

                case 0x24:
                    return "毛南";

                case 0x25:
                    return "仡佬";

                case 0x26:
                    return "锡伯";

                case 0x27:
                    return "阿昌";

                case 40:
                    return "普米";

                case 0x29:
                    return "塔吉克";

                case 0x2a:
                    return "怒";

                case 0x2b:
                    return "乌孜别克";

                case 0x2c:
                    return "俄罗斯";

                case 0x2d:
                    return "鄂温克";

                case 0x2e:
                    return "德昂";

                case 0x2f:
                    return "保安";

                case 0x30:
                    return "裕固";

                case 0x31:
                    return "京";

                case 50:
                    return "塔塔尔";

                case 0x33:
                    return "独龙";

                case 0x34:
                    return "鄂伦春";

                case 0x35:
                    return "赫哲";

                case 0x36:
                    return "门巴";

                case 0x37:
                    return "珞巴";

                case 0x38:
                    return "基诺";

                case 0x61:
                    return "其他";

                case 0x62:
                    return "外国血统中国籍人士";
            }
            return string.Empty;
        }

        public static byte[] PackToSAMCommand(byte[] command)
        {
            byte[] array = new byte[command.Length + 8];
            array[0] = 170;
            array[1] = 170;
            array[2] = 170;
            array[3] = 150;
            array[4] = 0x69;
            array[5] = BitConverter.GetBytes((int) (array.Length - 7))[1];
            array[6] = BitConverter.GetBytes((int) (array.Length - 7))[0];
            command.CopyTo(array, 7);
            int num = 0;
            for (int i = 5; i < (array.Length - 1); i++)
            {
                num ^= array[i];
            }
            array[array.Length - 1] = (byte) num;
            return array;
        }

        [HslMqttApi]
        public OperateResult<IdentityCard> ReadCard()
        {
            byte[] send = PackToSAMCommand(BuildReadCommand(0x30, 1, null));
            OperateResult<byte[]> result = base.ReadBase(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<IdentityCard>(result);
            }
            OperateResult result2 = CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<IdentityCard>(result2);
            }
            return ExtractIdentityCard(result.Content);
        }

        [HslMqttApi]
        public OperateResult<string> ReadSafeModuleNumber()
        {
            byte[] send = PackToSAMCommand(BuildReadCommand(0x12, 0xff, null));
            OperateResult<byte[]> result = base.ReadBase(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            return ExtractSafeModuleNumber(result.Content);
        }

        [HslMqttApi]
        public OperateResult SearchCard()
        {
            byte[] send = PackToSAMCommand(BuildReadCommand(0x20, 1, null));
            OperateResult<byte[]> result = base.ReadBase(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            if (result.Content[9] != 0x9f)
            {
                return new OperateResult(GetErrorDescription(result.Content[9]));
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi]
        public OperateResult SelectCard()
        {
            byte[] send = PackToSAMCommand(BuildReadCommand(0x20, 2, null));
            OperateResult<byte[]> result = base.ReadBase(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult result2 = CheckADSCommandAndSum(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            if (result.Content[9] != 0x90)
            {
                return new OperateResult(GetErrorDescription(result.Content[9]));
            }
            return OperateResult.CreateSuccessResult();
        }

        protected override OperateResult<byte[]> SPReceived(SerialPort serialPort, bool awaitData)
        {
            List<byte> input = new List<byte>();
            while (true)
            {
                OperateResult<byte[]> result = base.SPReceived(serialPort, awaitData);
                if (!result.IsSuccess)
                {
                    return result;
                }
                input.AddRange(result.Content);
                if (CheckADSCommandCompletion(input))
                {
                    return OperateResult.CreateSuccessResult<byte[]>(input.ToArray());
                }
            }
        }

        public override string ToString()
        {
            return string.Format("SAMSerial[{0}:{1}]", base.PortName, base.BaudRate);
        }

    }
}

