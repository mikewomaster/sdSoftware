namespace HslCommunication.Profinet.Keyence
{
    using HslCommunication;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class KeyenceSR2000Helper
    {
        public static OperateResult<bool> CheckInput(int number, Func<byte[], OperateResult<byte[]>> readCore)
        {
            OperateResult<string> result = ReadCustomer("INCHK," + number.ToString(), readCore);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool>(result);
            }
            if (result.Content == "ON")
            {
                return OperateResult.CreateSuccessResult<bool>(true);
            }
            if (result.Content == "OFF")
            {
                return OperateResult.CreateSuccessResult<bool>(false);
            }
            return new OperateResult<bool>(result.Content);
        }

        public static OperateResult CloseIndicator(Func<byte[], OperateResult<byte[]>> readCore)
        {
            return ReadCustomer("AMOFF", readCore);
        }

        public static string GetErrorDescription(string error)
        {
            string s = error;
            if (s != null)
            {
                switch (<PrivateImplementationDetails>.ComputeStringHash(s))
                {
                    case 0x1beb2a44:
                        if (s == "10")
                        {
                            return StringResources.Language.KeyenceSR2000Error10;
                        }
                        break;

                    case 0x1ceb2bd7:
                        if (s == "11")
                        {
                            return StringResources.Language.KeyenceSR2000Error11;
                        }
                        break;

                    case 0x14fea6f7:
                        if (s == "99")
                        {
                            return StringResources.Language.KeyenceSR2000Error99;
                        }
                        break;

                    case 0x17eb23f8:
                        if (s == "14")
                        {
                            return StringResources.Language.KeyenceSR2000Error14;
                        }
                        break;

                    case 0x1ced6a6e:
                        if (s == "05")
                        {
                            return StringResources.Language.KeyenceSR2000Error05;
                        }
                        break;

                    case 0x1deb2d6a:
                        if (s == "12")
                        {
                            return StringResources.Language.KeyenceSR2000Error12;
                        }
                        break;

                    case 0x1ded6c01:
                        if (s == "04")
                        {
                            return StringResources.Language.KeyenceSR2000Error04;
                        }
                        break;

                    case 0x1eeb2efd:
                        if (s == "13")
                        {
                            return StringResources.Language.KeyenceSR2000Error13;
                        }
                        break;

                    case 0x20ed70ba:
                        if (s == "01")
                        {
                            return StringResources.Language.KeyenceSR2000Error01;
                        }
                        break;

                    case 0x21ed724d:
                        if (s == "00")
                        {
                            return StringResources.Language.KeyenceSR2000Error00;
                        }
                        break;

                    case 0x1eed6d94:
                        if (s == "03")
                        {
                            return StringResources.Language.KeyenceSR2000Error03;
                        }
                        break;

                    case 0x1fed6f27:
                        if (s == "02")
                        {
                            return StringResources.Language.KeyenceSR2000Error02;
                        }
                        break;

                    case 0x8cf297ec:
                        if (s == "21")
                        {
                            return StringResources.Language.KeyenceSR2000Error21;
                        }
                        break;

                    case 0x8df2997f:
                        if (s == "20")
                        {
                            return StringResources.Language.KeyenceSR2000Error20;
                        }
                        break;

                    case 0x8ef29b12:
                        if (s == "23")
                        {
                            return StringResources.Language.KeyenceSR2000Error23;
                        }
                        break;

                    case 0x8ff29ca5:
                        if (s == "22")
                        {
                            return StringResources.Language.KeyenceSR2000Error22;
                        }
                        break;
                }
            }
            return (StringResources.Language.UnknownError + " :" + error);
        }

        public static OperateResult Lock(Func<byte[], OperateResult<byte[]>> readCore)
        {
            return ReadCustomer("LOCK", readCore);
        }

        public static OperateResult OpenIndicator(Func<byte[], OperateResult<byte[]>> readCore)
        {
            return ReadCustomer("AMON", readCore);
        }

        public static OperateResult<string> ReadBarcode(Func<byte[], OperateResult<byte[]>> readCore)
        {
            OperateResult<string> result = ReadCustomer("LON", readCore);
            if (!result.IsSuccess && (result.ErrorCode < 0))
            {
                ReadCustomer("LOFF", readCore);
            }
            return result;
        }

        public static OperateResult<string> ReadCommandState(Func<byte[], OperateResult<byte[]>> readCore)
        {
            return ReadCustomer("CMDSTAT", readCore);
        }

        public static OperateResult<string> ReadCustomer(string command, Func<byte[], OperateResult<byte[]>> readCore)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(command + "\r");
            string str = command;
            if (command.IndexOf(',') > 0)
            {
                str = command.Substring(0, command.IndexOf(','));
            }
            OperateResult<byte[]> result = readCore(bytes);
            if (!result.IsSuccess)
            {
                return result.Convert<string>(null);
            }
            char[] trimChars = new char[] { '\r' };
            string str2 = Encoding.ASCII.GetString(result.Content).Trim(trimChars);
            if (str2.StartsWith("ER," + str + ","))
            {
                return new OperateResult<string>(GetErrorDescription(str2.Substring(4 + str.Length)));
            }
            if (str2.StartsWith("OK," + str) && (str2.Length > (4 + str.Length)))
            {
                return OperateResult.CreateSuccessResult<string>(str2.Substring(4 + str.Length));
            }
            return OperateResult.CreateSuccessResult<string>(str2);
        }

        public static OperateResult<string> ReadErrorState(Func<byte[], OperateResult<byte[]>> readCore)
        {
            return ReadCustomer("ERRSTAT", readCore);
        }

        public static OperateResult<int[]> ReadRecord(Func<byte[], OperateResult<byte[]>> readCore)
        {
            OperateResult<string> result = ReadCustomer("NUM", readCore);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<int[]>(result);
            }
            char[] separator = new char[] { ',' };
            return OperateResult.CreateSuccessResult<int[]>((from n in result.Content.Split(separator) select int.Parse(n)).ToArray<int>());
        }

        public static OperateResult<string> ReadVersion(Func<byte[], OperateResult<byte[]>> readCore)
        {
            return ReadCustomer("KEYENCE", readCore);
        }

        public static OperateResult Reset(Func<byte[], OperateResult<byte[]>> readCore)
        {
            return ReadCustomer("RESET", readCore);
        }

        public static OperateResult SetOutput(int number, bool value, Func<byte[], OperateResult<byte[]>> readCore)
        {
            return ReadCustomer((value ? "OUTON," : "OUTOFF,") + number.ToString(), readCore);
        }

        public static OperateResult UnLock(Func<byte[], OperateResult<byte[]>> readCore)
        {
            return ReadCustomer("UNLOCK", readCore);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly KeyenceSR2000Helper.<>c <>9 = new KeyenceSR2000Helper.<>c();
            public static Func<string, int> <>9__9_0;

            internal int <ReadRecord>b__9_0(string n)
            {
                return int.Parse(n);
            }
        }
    }
}

