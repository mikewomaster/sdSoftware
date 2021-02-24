namespace HslCommunication.Profinet.Inovance
{
    using HslCommunication;
    using HslCommunication.Core;
    using System;

    public class InovanceHelper
    {
        private static int CalculateH3UStartAddress(string address)
        {
            if (address.IndexOf('.') < 0)
            {
                return Convert.ToInt32(address, 8);
            }
            char[] separator = new char[] { '.' };
            string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return ((Convert.ToInt32(strArray[0], 8) * 8) + int.Parse(strArray[1]));
        }

        private static int CalculateStartAddress(string address)
        {
            if (address.IndexOf('.') < 0)
            {
                return int.Parse(address);
            }
            char[] separator = new char[] { '.' };
            string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return ((int.Parse(strArray[0]) * 8) + int.Parse(strArray[1]));
        }

        public static OperateResult<string> PraseInovanceAMAddress(string address, byte modbusCode)
        {
            try
            {
                string str = string.Empty;
                OperateResult<int> result = HslHelper.ExtractParameter(ref address, "s");
                if (result.IsSuccess)
                {
                    str = string.Format("s={0};", result.Content);
                }
                if (address.StartsWith("QX") || address.StartsWith("qx"))
                {
                    return OperateResult.CreateSuccessResult<string>(str + CalculateStartAddress(address.Substring(2)).ToString());
                }
                if (address.StartsWith("Q") || address.StartsWith("q"))
                {
                    return OperateResult.CreateSuccessResult<string>(str + CalculateStartAddress(address.Substring(1)).ToString());
                }
                if (address.StartsWith("IX") || address.StartsWith("ix"))
                {
                    return OperateResult.CreateSuccessResult<string>(str + "x=2;" + CalculateStartAddress(address.Substring(2)).ToString());
                }
                if (address.StartsWith("I") || address.StartsWith("i"))
                {
                    return OperateResult.CreateSuccessResult<string>(str + "x=2;" + CalculateStartAddress(address.Substring(1)).ToString());
                }
                if (address.StartsWith("MW") || address.StartsWith("mw"))
                {
                    return OperateResult.CreateSuccessResult<string>(str + address.Substring(2));
                }
                if (address.StartsWith("M") || address.StartsWith("m"))
                {
                    return OperateResult.CreateSuccessResult<string>(str + address.Substring(1));
                }
                if (((modbusCode == 1) || (modbusCode == 15)) || (modbusCode == 5))
                {
                    if (address.StartsWith("SMX") || address.StartsWith("smx"))
                    {
                        return OperateResult.CreateSuccessResult<string>(str + string.Format("x={0};", modbusCode + 0x30) + CalculateStartAddress(address.Substring(3)).ToString());
                    }
                    if (address.StartsWith("SM") || address.StartsWith("sm"))
                    {
                        return OperateResult.CreateSuccessResult<string>(str + string.Format("x={0};", modbusCode + 0x30) + CalculateStartAddress(address.Substring(2)).ToString());
                    }
                }
                else
                {
                    if (address.StartsWith("SDW") || address.StartsWith("sdw"))
                    {
                        return OperateResult.CreateSuccessResult<string>(str + string.Format("x={0};", modbusCode + 0x30) + address.Substring(3));
                    }
                    if (address.StartsWith("SD") || address.StartsWith("sd"))
                    {
                        return OperateResult.CreateSuccessResult<string>(str + string.Format("x={0};", modbusCode + 0x30) + address.Substring(2));
                    }
                }
                return new OperateResult<string>(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                return new OperateResult<string>(exception.Message);
            }
        }

        public static OperateResult<string> PraseInovanceH3UAddress(string address, byte modbusCode)
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
                        num = CalculateH3UStartAddress(address.Substring(1)) + 0xf800;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("Y") || address.StartsWith("y"))
                    {
                        num = CalculateH3UStartAddress(address.Substring(1)) + 0xfc00;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("SM") || address.StartsWith("sm"))
                    {
                        num = Convert.ToInt32(address.Substring(2)) + 0x2400;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("S") || address.StartsWith("s"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0xe000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("T") || address.StartsWith("t"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0xf000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("C") || address.StartsWith("c"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0xf400;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("M") || address.StartsWith("m"))
                    {
                        int num2 = Convert.ToInt32(address.Substring(1));
                        if (num2 >= 0x1f40)
                        {
                            num = (num2 - 0x1f40) + 0x1f40;
                            return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                        }
                        return OperateResult.CreateSuccessResult<string>(str + num2.ToString());
                    }
                }
                else
                {
                    if (address.StartsWith("D") || address.StartsWith("d"))
                    {
                        return OperateResult.CreateSuccessResult<string>(str + Convert.ToInt32(address.Substring(1)).ToString());
                    }
                    if (address.StartsWith("SD") || address.StartsWith("sd"))
                    {
                        num = Convert.ToInt32(address.Substring(2)) + 0x2400;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("R") || address.StartsWith("r"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x3000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("T") || address.StartsWith("t"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0xf000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("C") || address.StartsWith("c"))
                    {
                        int num3 = Convert.ToInt32(address.Substring(1));
                        if (num3 >= 200)
                        {
                            num = ((num3 - 200) * 2) + 0xf700;
                            return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                        }
                        num = num3 + 0xf400;
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

        public static OperateResult<string> PraseInovanceH5UAddress(string address, byte modbusCode)
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
                        num = CalculateH3UStartAddress(address.Substring(1)) + 0xf800;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("Y") || address.StartsWith("y"))
                    {
                        num = CalculateH3UStartAddress(address.Substring(1)) + 0xfc00;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("S") || address.StartsWith("s"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0xe000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("B") || address.StartsWith("b"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x3000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("M") || address.StartsWith("m"))
                    {
                        return OperateResult.CreateSuccessResult<string>(str + Convert.ToInt32(address.Substring(1)).ToString());
                    }
                }
                else
                {
                    if (address.StartsWith("D") || address.StartsWith("d"))
                    {
                        return OperateResult.CreateSuccessResult<string>(str + Convert.ToInt32(address.Substring(1)).ToString());
                    }
                    if (address.StartsWith("R") || address.StartsWith("r"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x3000;
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

