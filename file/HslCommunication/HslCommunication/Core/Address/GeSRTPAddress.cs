namespace HslCommunication.Core.Address
{
    using HslCommunication;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class GeSRTPAddress : DeviceAddressDataBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DataCode>k__BackingField;

        public override void Parse(string address, ushort length)
        {
            OperateResult<GeSRTPAddress> result = ParseFrom(address, length, false);
            if (result.IsSuccess)
            {
                base.AddressStart = result.Content.AddressStart;
                base.Length = result.Content.Length;
                this.DataCode = result.Content.DataCode;
            }
        }

        public static OperateResult<GeSRTPAddress> ParseFrom(string address, bool isBit)
        {
            return ParseFrom(address, 0, isBit);
        }

        public static OperateResult<GeSRTPAddress> ParseFrom(string address, ushort length, bool isBit)
        {
            GeSRTPAddress address2 = new GeSRTPAddress();
            try
            {
                address2.Length = length;
                if (address.StartsWith("AI") || address.StartsWith("ai"))
                {
                    if (isBit)
                    {
                        return new OperateResult<GeSRTPAddress>(StringResources.Language.GeSRTPNotSupportBitReadWrite);
                    }
                    address2.DataCode = 10;
                    address2.AddressStart = Convert.ToInt32(address.Substring(2));
                }
                else if (address.StartsWith("AQ") || address.StartsWith("aq"))
                {
                    if (isBit)
                    {
                        return new OperateResult<GeSRTPAddress>(StringResources.Language.GeSRTPNotSupportBitReadWrite);
                    }
                    address2.DataCode = 12;
                    address2.AddressStart = Convert.ToInt32(address.Substring(2));
                }
                else if (address.StartsWith("R") || address.StartsWith("r"))
                {
                    if (isBit)
                    {
                        return new OperateResult<GeSRTPAddress>(StringResources.Language.GeSRTPNotSupportBitReadWrite);
                    }
                    address2.DataCode = 8;
                    address2.AddressStart = Convert.ToInt32(address.Substring(1));
                }
                else if (address.StartsWith("SA") || address.StartsWith("sa"))
                {
                    address2.DataCode = isBit ? ((byte) 0x4e) : ((byte) 0x18);
                    address2.AddressStart = Convert.ToInt32(address.Substring(2));
                }
                else if (address.StartsWith("SB") || address.StartsWith("sb"))
                {
                    address2.DataCode = isBit ? ((byte) 80) : ((byte) 0x1a);
                    address2.AddressStart = Convert.ToInt32(address.Substring(2));
                }
                else if (address.StartsWith("SC") || address.StartsWith("sc"))
                {
                    address2.DataCode = isBit ? ((byte) 0x52) : ((byte) 0x1c);
                    address2.AddressStart = Convert.ToInt32(address.Substring(2));
                }
                else
                {
                    if ((address[0] == 'I') || (address[0] == 'i'))
                    {
                        address2.DataCode = isBit ? ((byte) 70) : ((byte) 0x10);
                    }
                    else if ((address[0] == 'Q') || (address[0] == 'q'))
                    {
                        address2.DataCode = isBit ? ((byte) 0x48) : ((byte) 0x12);
                    }
                    else if ((address[0] == 'M') || (address[0] == 'm'))
                    {
                        address2.DataCode = isBit ? ((byte) 0x4c) : ((byte) 0x16);
                    }
                    else if ((address[0] == 'T') || (address[0] == 't'))
                    {
                        address2.DataCode = isBit ? ((byte) 0x4a) : ((byte) 20);
                    }
                    else if ((address[0] == 'S') || (address[0] == 's'))
                    {
                        address2.DataCode = isBit ? ((byte) 0x54) : ((byte) 30);
                    }
                    else
                    {
                        if ((address[0] != 'G') && (address[0] != 'g'))
                        {
                            throw new Exception(StringResources.Language.NotSupportedDataType);
                        }
                        address2.DataCode = isBit ? ((byte) 0x56) : ((byte) 0x38);
                    }
                    address2.AddressStart = Convert.ToInt32(address.Substring(1));
                }
            }
            catch (Exception exception)
            {
                return new OperateResult<GeSRTPAddress>(exception.Message);
            }
            if (address2.AddressStart == 0)
            {
                return new OperateResult<GeSRTPAddress>(StringResources.Language.GeSRTPAddressCannotBeZero);
            }
            if (address2.AddressStart > 0)
            {
                address2.AddressStart--;
            }
            return OperateResult.CreateSuccessResult<GeSRTPAddress>(address2);
        }

        public byte DataCode { get; set; }
    }
}

