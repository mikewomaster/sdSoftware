namespace HslCommunication.Core.Address
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Profinet.Fuji;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class FujiSPBAddress : DeviceAddressBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <BitIndex>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <TypeCode>k__BackingField;

        public int GetBitIndex()
        {
            return ((base.Address * 0x10) + this.BitIndex);
        }

        public string GetWordAddress()
        {
            return (this.TypeCode + FujiSPBOverTcp.AnalysisIntegerAddress(base.Address));
        }

        public string GetWriteBoolAddress()
        {
            int address = base.Address * 2;
            int bitIndex = this.BitIndex;
            if (bitIndex >= 8)
            {
                address++;
                bitIndex -= 8;
            }
            return string.Format("{0}{1}{2:X2}", this.TypeCode, FujiSPBOverTcp.AnalysisIntegerAddress(address), bitIndex);
        }

        public static OperateResult<FujiSPBAddress> ParseFrom(string address)
        {
            return ParseFrom(address, 0);
        }

        public static OperateResult<FujiSPBAddress> ParseFrom(string address, ushort length)
        {
            FujiSPBAddress address2 = new FujiSPBAddress();
            try
            {
                address2.BitIndex = HslHelper.GetBitIndexInformation(ref address);
                switch (address[0])
                {
                    case 'C':
                    case 'c':
                        if ((address[1] != 'N') && (address[1] != 'n'))
                        {
                            if ((address[1] != 'C') && (address[1] != 'c'))
                            {
                                throw new Exception(StringResources.Language.NotSupportedDataType);
                            }
                            address2.TypeCode = "05";
                            address2.Address = Convert.ToUInt16(address.Substring(2), 10);
                        }
                        else
                        {
                            address2.TypeCode = "0B";
                            address2.Address = Convert.ToUInt16(address.Substring(2), 10);
                        }
                        goto Label_0346;

                    case 'D':
                    case 'd':
                        address2.TypeCode = "0C";
                        address2.Address = Convert.ToUInt16(address.Substring(1), 10);
                        goto Label_0346;

                    case 'L':
                    case 'l':
                        address2.TypeCode = "03";
                        address2.Address = Convert.ToUInt16(address.Substring(1), 10);
                        goto Label_0346;

                    case 'M':
                    case 'm':
                        address2.TypeCode = "02";
                        address2.Address = Convert.ToUInt16(address.Substring(1), 10);
                        goto Label_0346;

                    case 'R':
                    case 'r':
                        address2.TypeCode = "0D";
                        address2.Address = Convert.ToUInt16(address.Substring(1), 10);
                        goto Label_0346;

                    case 'T':
                    case 't':
                        if ((address[1] != 'N') && (address[1] != 'n'))
                        {
                            if ((address[1] != 'C') && (address[1] != 'c'))
                            {
                                throw new Exception(StringResources.Language.NotSupportedDataType);
                            }
                            address2.TypeCode = "04";
                            address2.Address = Convert.ToUInt16(address.Substring(2), 10);
                        }
                        else
                        {
                            address2.TypeCode = "0A";
                            address2.Address = Convert.ToUInt16(address.Substring(2), 10);
                        }
                        goto Label_0346;

                    case 'W':
                    case 'w':
                        address2.TypeCode = "0E";
                        address2.Address = Convert.ToUInt16(address.Substring(1), 10);
                        goto Label_0346;

                    case 'X':
                    case 'x':
                        address2.TypeCode = "01";
                        address2.Address = Convert.ToUInt16(address.Substring(1), 10);
                        goto Label_0346;

                    case 'Y':
                    case 'y':
                        address2.TypeCode = "00";
                        address2.Address = Convert.ToUInt16(address.Substring(1), 10);
                        goto Label_0346;
                }
                throw new Exception(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                return new OperateResult<FujiSPBAddress>(exception.Message);
            }
        Label_0346:
            return OperateResult.CreateSuccessResult<FujiSPBAddress>(address2);
        }

        public int BitIndex { get; set; }

        public string TypeCode { get; set; }
    }
}

