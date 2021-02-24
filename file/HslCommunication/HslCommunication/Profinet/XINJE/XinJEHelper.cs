namespace HslCommunication.Profinet.XINJE
{
    using HslCommunication;
    using HslCommunication.Core;
    using System;

    public class XinJEHelper
    {
        private static int CalculateXinJEStartAddress(string address)
        {
            if (address.IndexOf('.') < 0)
            {
                return Convert.ToInt32(address, 8);
            }
            char[] separator = new char[] { '.' };
            string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return ((Convert.ToInt32(strArray[0], 8) * 8) + int.Parse(strArray[1]));
        }

        public static OperateResult<string> PraseXinJEXCAddress(string address, byte modbusCode)
        {
            try
            {
                int num;
                string str = string.Empty;
                OperateResult<int> result = HslHelper.ExtractParameter(ref address, "s");
                if (result.IsSuccess)
                {
                    str = string.Format("s={0};", result.Content);
                }
                if (((modbusCode == 1) || (modbusCode == 15)) || (modbusCode == 5))
                {
                    if (address.StartsWith("X") || address.StartsWith("x"))
                    {
                        num = CalculateXinJEStartAddress(address.Substring(1)) + 0x4000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("Y") || address.StartsWith("y"))
                    {
                        num = CalculateXinJEStartAddress(address.Substring(1)) + 0x4800;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("S") || address.StartsWith("s"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x5000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("T") || address.StartsWith("t"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x6400;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("C") || address.StartsWith("c"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x6c00;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("M") || address.StartsWith("m"))
                    {
                        int num2 = Convert.ToInt32(address.Substring(1));
                        if (num2 >= 0x1f40)
                        {
                            num = (num2 - 0x1f40) + 0x6000;
                            return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                        }
                        return OperateResult.CreateSuccessResult<string>(str + num2.ToString());
                    }
                }
                else
                {
                    if (address.StartsWith("D") || address.StartsWith("d"))
                    {
                        int num3 = Convert.ToInt32(address.Substring(1));
                        if (num3 >= 0x1f40)
                        {
                            num = (num3 - 0x1f40) + 0x4000;
                            return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                        }
                        return OperateResult.CreateSuccessResult<string>(str + num3.ToString());
                    }
                    if (address.StartsWith("F") || address.StartsWith("f"))
                    {
                        int num4 = Convert.ToInt32(address.Substring(1));
                        if (num4 >= 0x1f40)
                        {
                            num = (num4 - 0x1f40) + 0x6800;
                            return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                        }
                        num = num4 + 0x4800;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("E") || address.StartsWith("e"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x7000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("T") || address.StartsWith("t"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x3000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("C") || address.StartsWith("c"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x3800;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                }
                return new OperateResult<string>(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                return new OperateResult<string>(exception.Message);
            }
        }
    }
}

