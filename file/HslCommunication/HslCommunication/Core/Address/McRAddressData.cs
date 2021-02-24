namespace HslCommunication.Core.Address
{
    using HslCommunication;
    using HslCommunication.Profinet.Melsec;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class McRAddressData : DeviceAddressDataBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MelsecMcRDataType <McDataType>k__BackingField;

        public McRAddressData()
        {
            this.McDataType = MelsecMcRDataType.D;
        }

        public override void Parse(string address, ushort length)
        {
            OperateResult<McRAddressData> result = ParseMelsecRFrom(address, length);
            if (result.IsSuccess)
            {
                base.AddressStart = result.Content.AddressStart;
                base.Length = result.Content.Length;
                this.McDataType = result.Content.McDataType;
            }
        }

        public static OperateResult<McRAddressData> ParseMelsecRFrom(string address, ushort length)
        {
            OperateResult<MelsecMcRDataType, int> result = MelsecMcRNet.AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<McRAddressData>(result);
            }
            McRAddressData data1 = new McRAddressData {
                McDataType = result.Content1,
                AddressStart = result.Content2,
                Length = length
            };
            return OperateResult.CreateSuccessResult<McRAddressData>(data1);
        }

        public MelsecMcRDataType McDataType { get; set; }
    }
}

