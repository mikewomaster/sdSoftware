namespace HslCommunication.Profinet.Melsec
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.Address;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class MelsecMcRNet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <NetworkNumber>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <NetworkStationNumber>k__BackingField;

        public MelsecMcRNet()
        {
            this.<NetworkNumber>k__BackingField = 0;
            this.<NetworkStationNumber>k__BackingField = 0;
            base.WordLength = 1;
            base.ByteTransform = new RegularByteTransform();
        }

        public MelsecMcRNet(string ipAddress, int port)
        {
            this.<NetworkNumber>k__BackingField = 0;
            this.<NetworkStationNumber>k__BackingField = 0;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult<MelsecMcRDataType, int> AnalysisAddress(string address)
        {
            try
            {
                if (address.StartsWith("LSTS"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.LSTS, Convert.ToInt32(address.Substring(4), MelsecMcRDataType.LSTS.FromBase));
                }
                if (address.StartsWith("LSTC"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.LSTC, Convert.ToInt32(address.Substring(4), MelsecMcRDataType.LSTC.FromBase));
                }
                if (address.StartsWith("LSTN"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.LSTN, Convert.ToInt32(address.Substring(4), MelsecMcRDataType.LSTN.FromBase));
                }
                if (address.StartsWith("STS"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.STS, Convert.ToInt32(address.Substring(3), MelsecMcRDataType.STS.FromBase));
                }
                if (address.StartsWith("STC"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.STC, Convert.ToInt32(address.Substring(3), MelsecMcRDataType.STC.FromBase));
                }
                if (address.StartsWith("STN"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.STN, Convert.ToInt32(address.Substring(3), MelsecMcRDataType.STN.FromBase));
                }
                if (address.StartsWith("LTS"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.LTS, Convert.ToInt32(address.Substring(3), MelsecMcRDataType.LTS.FromBase));
                }
                if (address.StartsWith("LTC"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.LTC, Convert.ToInt32(address.Substring(3), MelsecMcRDataType.LTC.FromBase));
                }
                if (address.StartsWith("LTN"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.LTN, Convert.ToInt32(address.Substring(3), MelsecMcRDataType.LTN.FromBase));
                }
                if (address.StartsWith("LCS"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.LCS, Convert.ToInt32(address.Substring(3), MelsecMcRDataType.LCS.FromBase));
                }
                if (address.StartsWith("LCC"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.LCC, Convert.ToInt32(address.Substring(3), MelsecMcRDataType.LCC.FromBase));
                }
                if (address.StartsWith("LCN"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.LCN, Convert.ToInt32(address.Substring(3), MelsecMcRDataType.LCN.FromBase));
                }
                if (address.StartsWith("TS"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.TS, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.TS.FromBase));
                }
                if (address.StartsWith("TC"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.TC, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.TC.FromBase));
                }
                if (address.StartsWith("TN"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.TN, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.TN.FromBase));
                }
                if (address.StartsWith("CS"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.CS, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.CS.FromBase));
                }
                if (address.StartsWith("CC"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.CC, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.CC.FromBase));
                }
                if (address.StartsWith("CN"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.CN, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.CN.FromBase));
                }
                if (address.StartsWith("SM"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.SM, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.SM.FromBase));
                }
                if (address.StartsWith("SB"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.SB, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.SB.FromBase));
                }
                if (address.StartsWith("DX"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.DX, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.DX.FromBase));
                }
                if (address.StartsWith("DY"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.DY, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.DY.FromBase));
                }
                if (address.StartsWith("SD"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.SD, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.SD.FromBase));
                }
                if (address.StartsWith("SW"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.SW, Convert.ToInt32(address.Substring(2), MelsecMcRDataType.SW.FromBase));
                }
                if (address.StartsWith("X"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.X, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.X.FromBase));
                }
                if (address.StartsWith("Y"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.Y, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.Y.FromBase));
                }
                if (address.StartsWith("M"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.M, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.M.FromBase));
                }
                if (address.StartsWith("L"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.L, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.L.FromBase));
                }
                if (address.StartsWith("F"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.F, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.F.FromBase));
                }
                if (address.StartsWith("V"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.V, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.V.FromBase));
                }
                if (address.StartsWith("S"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.S, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.S.FromBase));
                }
                if (address.StartsWith("B"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.B, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.B.FromBase));
                }
                if (address.StartsWith("D"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.D, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.D.FromBase));
                }
                if (address.StartsWith("W"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.W, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.W.FromBase));
                }
                if (address.StartsWith("R"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.R, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.R.FromBase));
                }
                if (address.StartsWith("Z"))
                {
                    return OperateResult.CreateSuccessResult<MelsecMcRDataType, int>(MelsecMcRDataType.Z, Convert.ToInt32(address.Substring(1), MelsecMcRDataType.Z.FromBase));
                }
                return new OperateResult<MelsecMcRDataType, int>(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                return new OperateResult<MelsecMcRDataType, int>(exception.Message);
            }
        }

        public static byte[] BuildReadMcCoreCommand(McRAddressData address, bool isBit)
        {
            return new byte[] { 1, 4, (isBit ? ((byte) 1) : ((byte) 0)), 0, BitConverter.GetBytes(address.AddressStart)[0], BitConverter.GetBytes(address.AddressStart)[1], BitConverter.GetBytes(address.AddressStart)[2], BitConverter.GetBytes(address.AddressStart)[3], address.McDataType.DataCode[0], address.McDataType.DataCode[1], ((byte) (address.Length % 0x100)), ((byte) (address.Length / 0x100)) };
        }

        public static byte[] BuildWriteBitCoreCommand(McRAddressData address, bool[] value)
        {
            if (value == null)
            {
                value = new bool[0];
            }
            byte[] buffer = MelsecHelper.TransBoolArrayToByteData(value);
            byte[] array = new byte[12 + buffer.Length];
            array[0] = 1;
            array[1] = 20;
            array[2] = 1;
            array[3] = 0;
            array[4] = BitConverter.GetBytes(address.AddressStart)[0];
            array[5] = BitConverter.GetBytes(address.AddressStart)[1];
            array[6] = BitConverter.GetBytes(address.AddressStart)[2];
            array[7] = BitConverter.GetBytes(address.AddressStart)[3];
            array[8] = address.McDataType.DataCode[0];
            array[9] = address.McDataType.DataCode[1];
            array[10] = (byte) (value.Length % 0x100);
            array[11] = (byte) (value.Length / 0x100);
            buffer.CopyTo(array, 12);
            return array;
        }

        public static byte[] BuildWriteWordCoreCommand(McRAddressData address, byte[] value)
        {
            if (value == null)
            {
                value = new byte[0];
            }
            byte[] array = new byte[12 + value.Length];
            array[0] = 1;
            array[1] = 20;
            array[2] = 0;
            array[3] = 0;
            array[4] = BitConverter.GetBytes(address.AddressStart)[0];
            array[5] = BitConverter.GetBytes(address.AddressStart)[1];
            array[6] = BitConverter.GetBytes(address.AddressStart)[2];
            array[7] = BitConverter.GetBytes(address.AddressStart)[3];
            array[8] = address.McDataType.DataCode[0];
            array[9] = address.McDataType.DataCode[1];
            array[10] = (byte) ((value.Length / 2) % 0x100);
            array[11] = (byte) ((value.Length / 2) / 0x100);
            value.CopyTo(array, 12);
            return array;
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new MelsecQnA3EBinaryMessage();
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<McRAddressData> result = McRAddressData.ParseMelsecRFrom(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte>();
            ushort num = 0;
            while (num < length)
            {
                ushort num2 = (ushort) Math.Min(length - num, 900);
                result.Content.Length = num2;
                OperateResult<byte[]> result3 = this.ReadAddressData(result.Content, false);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                list.AddRange(result3.Content);
                num = (ushort) (num + num2);
                if (result.Content.McDataType.DataType == 0)
                {
                    McRAddressData content = result.Content;
                    content.AddressStart += num2;
                }
                else
                {
                    McRAddressData local2 = result.Content;
                    local2.AddressStart += num2 * 0x10;
                }
            }
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        private OperateResult<byte[]> ReadAddressData(McRAddressData address, bool isBit)
        {
            byte[] mcCore = BuildReadMcCoreCommand(address, isBit);
            OperateResult<byte[]> result = base.ReadFromCoreServer(MelsecMcNet.PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult result2 = MelsecMcNet.CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return MelsecMcNet.ExtractActualData(result.Content.RemoveBegin<byte>(11), isBit);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<McRAddressData> result = McRAddressData.ParseMelsecRFrom(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            byte[] mcCore = BuildReadMcCoreCommand(result.Content, true);
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(MelsecMcNet.PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult result3 = MelsecMcNet.CheckResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            OperateResult<byte[]> result4 = MelsecMcNet.ExtractActualData(result2.Content.RemoveBegin<byte>(11), true);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return OperateResult.CreateSuccessResult<bool[]>((from m in result4.Content select m == 1).Take<bool>(length).ToArray<bool>());
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<McRAddressData> result = McRAddressData.ParseMelsecRFrom(address, 0);
            if (!result.IsSuccess)
            {
                return result;
            }
            byte[] mcCore = BuildWriteBitCoreCommand(result.Content, values);
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(MelsecMcNet.PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = MelsecMcNet.CheckResponseContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<McRAddressData> result = McRAddressData.ParseMelsecRFrom(address, 0);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return this.WriteAddressData(result.Content, value);
        }

        private OperateResult WriteAddressData(McRAddressData addressData, byte[] value)
        {
            byte[] mcCore = BuildWriteWordCoreCommand(addressData, value);
            OperateResult<byte[]> result = base.ReadFromCoreServer(MelsecMcNet.PackMcCommand(mcCore, this.NetworkNumber, this.NetworkStationNumber));
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = MelsecMcNet.CheckResponseContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return OperateResult.CreateSuccessResult();
        }

        public byte NetworkNumber { get; set; }

        public byte NetworkStationNumber { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MelsecMcRNet.<>c <>9 = new MelsecMcRNet.<>c();
            public static Func<byte, bool> <>9__15_0;

            internal bool <ReadBool>b__15_0(byte m)
            {
                return (m == 1);
            }
        }
    }
}

