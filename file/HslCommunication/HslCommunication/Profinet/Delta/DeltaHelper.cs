namespace HslCommunication.Profinet.Delta
{
    using HslCommunication;
    using HslCommunication.Core;
    using System;

    public class DeltaHelper
    {
        public static OperateResult<string> PraseDeltaDvpAddress(string address, byte modbusCode)
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
                    if (address.StartsWith("S") || address.StartsWith("s"))
                    {
                        return OperateResult.CreateSuccessResult<string>(str + Convert.ToInt32(address.Substring(1)).ToString());
                    }
                    if (address.StartsWith("X") || address.StartsWith("x"))
                    {
                        num = Convert.ToInt32(address.Substring(1), 8) + 0x400;
                        return OperateResult.CreateSuccessResult<string>(str + "x=2;" + num.ToString());
                    }
                    if (address.StartsWith("Y") || address.StartsWith("y"))
                    {
                        num = Convert.ToInt32(address.Substring(1), 8) + 0x500;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("T") || address.StartsWith("t"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x600;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("C") || address.StartsWith("c"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0xe00;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("M") || address.StartsWith("m"))
                    {
                        int num2 = Convert.ToInt32(address.Substring(1));
                        if (num2 >= 0x600)
                        {
                            num = (num2 - 0x600) + 0xb000;
                            return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                        }
                        num = num2 + 0x800;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                }
                else
                {
                    if (address.StartsWith("D") || address.StartsWith("d"))
                    {
                        int num3 = Convert.ToInt32(address.Substring(1));
                        if (num3 >= 0x1000)
                        {
                            num = (num3 - 0x1000) + 0x9000;
                            return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                        }
                        num = num3 + 0x1000;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("C") || address.StartsWith("c"))
                    {
                        int num4 = Convert.ToInt32(address.Substring(1));
                        if (num4 >= 200)
                        {
                            num = (num4 - 200) + 0xec8;
                            return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                        }
                        num = num4 + 0xe00;
                        return OperateResult.CreateSuccessResult<string>(str + num.ToString());
                    }
                    if (address.StartsWith("T") || address.StartsWith("t"))
                    {
                        num = Convert.ToInt32(address.Substring(1)) + 0x600;
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

