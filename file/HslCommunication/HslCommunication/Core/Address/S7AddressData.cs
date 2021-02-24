namespace HslCommunication.Core.Address
{
    using HslCommunication;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class S7AddressData : DeviceAddressDataBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DataCode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <DbBlock>k__BackingField;

        public static int CalculateAddressStarted(string address, [Optional, DefaultParameterValue(false)] bool isCT)
        {
            if (address.IndexOf('.') < 0)
            {
                if (isCT)
                {
                    return Convert.ToInt32(address);
                }
                return (Convert.ToInt32(address) * 8);
            }
            char[] separator = new char[] { '.' };
            string[] strArray = address.Split(separator);
            return ((Convert.ToInt32(strArray[0]) * 8) + Convert.ToInt32(strArray[1]));
        }

        public override void Parse(string address, ushort length)
        {
            OperateResult<S7AddressData> result = ParseFrom(address, length);
            if (result.IsSuccess)
            {
                base.AddressStart = result.Content.AddressStart;
                base.Length = result.Content.Length;
                this.DataCode = result.Content.DataCode;
                this.DbBlock = result.Content.DbBlock;
            }
        }

        public static OperateResult<S7AddressData> ParseFrom(string address)
        {
            return ParseFrom(address, 0);
        }

        public static OperateResult<S7AddressData> ParseFrom(string address, ushort length)
        {
            S7AddressData data = new S7AddressData();
            try
            {
                data.Length = length;
                data.DbBlock = 0;
                if (address[0] == 'I')
                {
                    data.DataCode = 0x81;
                    data.AddressStart = CalculateAddressStarted(address.Substring(1), false);
                }
                else if (address[0] == 'Q')
                {
                    data.DataCode = 130;
                    data.AddressStart = CalculateAddressStarted(address.Substring(1), false);
                }
                else if (address[0] == 'M')
                {
                    data.DataCode = 0x83;
                    data.AddressStart = CalculateAddressStarted(address.Substring(1), false);
                }
                else if ((address[0] == 'D') || (address.Substring(0, 2) == "DB"))
                {
                    data.DataCode = 0x84;
                    char[] separator = new char[] { '.' };
                    string[] strArray = address.Split(separator);
                    if (address[1] == 'B')
                    {
                        data.DbBlock = Convert.ToUInt16(strArray[0].Substring(2));
                    }
                    else
                    {
                        data.DbBlock = Convert.ToUInt16(strArray[0].Substring(1));
                    }
                    data.AddressStart = CalculateAddressStarted(address.Substring(address.IndexOf('.') + 1), false);
                }
                else if (address[0] == 'T')
                {
                    data.DataCode = 0x1f;
                    data.AddressStart = CalculateAddressStarted(address.Substring(1), true);
                }
                else if (address[0] == 'C')
                {
                    data.DataCode = 30;
                    data.AddressStart = CalculateAddressStarted(address.Substring(1), true);
                }
                else if (address[0] == 'V')
                {
                    data.DataCode = 0x84;
                    data.DbBlock = 1;
                    data.AddressStart = CalculateAddressStarted(address.Substring(1), false);
                }
                else
                {
                    return new OperateResult<S7AddressData>(StringResources.Language.NotSupportedDataType);
                }
            }
            catch (Exception exception)
            {
                return new OperateResult<S7AddressData>(exception.Message);
            }
            return OperateResult.CreateSuccessResult<S7AddressData>(data);
        }

        public byte DataCode { get; set; }

        public ushort DbBlock { get; set; }
    }
}

