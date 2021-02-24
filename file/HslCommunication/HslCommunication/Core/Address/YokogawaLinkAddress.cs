namespace HslCommunication.Core.Address
{
    using HslCommunication;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class YokogawaLinkAddress : DeviceAddressDataBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <DataCode>k__BackingField;

        public byte[] GetAddressBinaryContent()
        {
            return new byte[] { BitConverter.GetBytes(this.DataCode)[1], BitConverter.GetBytes(this.DataCode)[0], BitConverter.GetBytes(base.AddressStart)[3], BitConverter.GetBytes(base.AddressStart)[2], BitConverter.GetBytes(base.AddressStart)[1], BitConverter.GetBytes(base.AddressStart)[0] };
        }

        public override void Parse(string address, ushort length)
        {
            OperateResult<YokogawaLinkAddress> result = ParseFrom(address, length);
            if (result.IsSuccess)
            {
                base.AddressStart = result.Content.AddressStart;
                base.Length = result.Content.Length;
                this.DataCode = result.Content.DataCode;
            }
        }

        public static OperateResult<YokogawaLinkAddress> ParseFrom(string address, ushort length)
        {
            try
            {
                int num = 0;
                int num2 = 0;
                if (address.StartsWith("CN") || address.StartsWith("cn"))
                {
                    num = 0x31;
                    num2 = int.Parse(address.Substring(2));
                }
                else if (address.StartsWith("TN") || address.StartsWith("tn"))
                {
                    num = 0x21;
                    num2 = int.Parse(address.Substring(2));
                }
                else if (address.StartsWith("X") || address.StartsWith("x"))
                {
                    num = 0x18;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("Y") || address.StartsWith("y"))
                {
                    num = 0x19;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("I") || address.StartsWith("i"))
                {
                    num = 9;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("E") || address.StartsWith("e"))
                {
                    num = 5;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("M") || address.StartsWith("m"))
                {
                    num = 13;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("T") || address.StartsWith("t"))
                {
                    num = 20;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("C") || address.StartsWith("c"))
                {
                    num = 3;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("L") || address.StartsWith("l"))
                {
                    num = 12;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("D") || address.StartsWith("d"))
                {
                    num = 4;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("B") || address.StartsWith("b"))
                {
                    num = 2;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("F") || address.StartsWith("f"))
                {
                    num = 6;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("R") || address.StartsWith("r"))
                {
                    num = 0x12;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("V") || address.StartsWith("v"))
                {
                    num = 0x16;
                    num2 = int.Parse(address.Substring(1));
                }
                else if (address.StartsWith("Z") || address.StartsWith("z"))
                {
                    num = 0x1a;
                    num2 = int.Parse(address.Substring(1));
                }
                else
                {
                    if (!address.StartsWith("W") && !address.StartsWith("w"))
                    {
                        throw new Exception(StringResources.Language.NotSupportedDataType);
                    }
                    num = 0x17;
                    num2 = int.Parse(address.Substring(1));
                }
                YokogawaLinkAddress address1 = new YokogawaLinkAddress {
                    DataCode = num,
                    AddressStart = num2,
                    Length = length
                };
                return OperateResult.CreateSuccessResult<YokogawaLinkAddress>(address1);
            }
            catch (Exception exception)
            {
                return new OperateResult<YokogawaLinkAddress>(exception.Message);
            }
        }

        public int DataCode { get; set; }
    }
}

