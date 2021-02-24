namespace HslCommunication.Core.Address
{
    using HslCommunication;
    using HslCommunication.Profinet.Melsec;
    using HslCommunication.Profinet.Panasonic;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class McAddressData : DeviceAddressDataBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MelsecMcDataType <McDataType>k__BackingField;

        public McAddressData()
        {
            this.McDataType = MelsecMcDataType.D;
        }

        public override void Parse(string address, ushort length)
        {
            OperateResult<McAddressData> result = ParseMelsecFrom(address, length);
            if (result.IsSuccess)
            {
                base.AddressStart = result.Content.AddressStart;
                base.Length = result.Content.Length;
                this.McDataType = result.Content.McDataType;
            }
        }

        public static OperateResult<McAddressData> ParseKeyenceFrom(string address, ushort length)
        {
            McAddressData data = new McAddressData {
                Length = length
            };
            try
            {
                switch (address[0])
                {
                    case 'B':
                    case 'b':
                        data.McDataType = MelsecMcDataType.Keyence_B;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.Keyence_B.FromBase);
                        goto Label_057D;

                    case 'C':
                    case 'c':
                        if ((address[1] != 'N') && (address[1] != 'n'))
                        {
                            if ((address[1] != 'S') && (address[1] != 's'))
                            {
                                if ((address[1] != 'C') && (address[1] != 'c'))
                                {
                                    throw new Exception(StringResources.Language.NotSupportedDataType);
                                }
                                data.McDataType = MelsecMcDataType.Keyence_CC;
                                data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.Keyence_CC.FromBase);
                            }
                            else
                            {
                                data.McDataType = MelsecMcDataType.Keyence_CS;
                                data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.Keyence_CS.FromBase);
                            }
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.Keyence_CN;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.Keyence_CN.FromBase);
                        }
                        goto Label_057D;

                    case 'D':
                    case 'd':
                        data.McDataType = MelsecMcDataType.Keyence_D;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.Keyence_D.FromBase);
                        goto Label_057D;

                    case 'L':
                    case 'l':
                        data.McDataType = MelsecMcDataType.Keyence_L;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.Keyence_L.FromBase);
                        goto Label_057D;

                    case 'M':
                    case 'm':
                        data.McDataType = MelsecMcDataType.Keyence_M;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.Keyence_M.FromBase);
                        goto Label_057D;

                    case 'R':
                    case 'r':
                        data.McDataType = MelsecMcDataType.Keyence_R;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.Keyence_R.FromBase);
                        goto Label_057D;

                    case 'S':
                    case 's':
                        if ((address[1] != 'M') && (address[1] != 'm'))
                        {
                            if ((address[1] != 'D') && (address[1] != 'd'))
                            {
                                throw new Exception(StringResources.Language.NotSupportedDataType);
                            }
                            data.McDataType = MelsecMcDataType.Keyence_SD;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.Keyence_SD.FromBase);
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.Keyence_SM;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.Keyence_SM.FromBase);
                        }
                        goto Label_057D;

                    case 'T':
                    case 't':
                        if ((address[1] != 'N') && (address[1] != 'n'))
                        {
                            if ((address[1] != 'S') && (address[1] != 's'))
                            {
                                if ((address[1] != 'C') && (address[1] != 'c'))
                                {
                                    throw new Exception(StringResources.Language.NotSupportedDataType);
                                }
                                data.McDataType = MelsecMcDataType.Keyence_TC;
                                data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.Keyence_TC.FromBase);
                            }
                            else
                            {
                                data.McDataType = MelsecMcDataType.Keyence_TS;
                                data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.Keyence_TS.FromBase);
                            }
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.Keyence_TN;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.Keyence_TN.FromBase);
                        }
                        goto Label_057D;

                    case 'W':
                    case 'w':
                        data.McDataType = MelsecMcDataType.Keyence_W;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.Keyence_W.FromBase);
                        goto Label_057D;

                    case 'X':
                    case 'x':
                        data.McDataType = MelsecMcDataType.Keyence_X;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.Keyence_X.FromBase);
                        goto Label_057D;

                    case 'Y':
                    case 'y':
                        data.McDataType = MelsecMcDataType.Keyence_Y;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.Keyence_Y.FromBase);
                        goto Label_057D;

                    case 'Z':
                    case 'z':
                        if ((address[1] != 'R') && (address[1] != 'r'))
                        {
                            throw new Exception(StringResources.Language.NotSupportedDataType);
                        }
                        data.McDataType = MelsecMcDataType.Keyence_ZR;
                        data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.Keyence_ZR.FromBase);
                        goto Label_057D;
                }
                throw new Exception(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                return new OperateResult<McAddressData>(exception.Message);
            }
        Label_057D:
            return OperateResult.CreateSuccessResult<McAddressData>(data);
        }

        public static OperateResult<McAddressData> ParseMelsecFrom(string address, ushort length)
        {
            McAddressData data = new McAddressData {
                Length = length
            };
            try
            {
                switch (address[0])
                {
                    case 'B':
                    case 'b':
                        data.McDataType = MelsecMcDataType.B;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.B.FromBase);
                        goto Label_08E6;

                    case 'C':
                    case 'c':
                        if ((address[1] != 'N') && (address[1] != 'n'))
                        {
                            if ((address[1] != 'S') && (address[1] != 's'))
                            {
                                if ((address[1] != 'C') && (address[1] != 'c'))
                                {
                                    throw new Exception(StringResources.Language.NotSupportedDataType);
                                }
                                data.McDataType = MelsecMcDataType.CC;
                                data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.CC.FromBase);
                            }
                            else
                            {
                                data.McDataType = MelsecMcDataType.CS;
                                data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.CS.FromBase);
                            }
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.CN;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.CN.FromBase);
                        }
                        goto Label_08E6;

                    case 'D':
                    case 'd':
                        if ((address[1] == 'X') || (address[1] == 'x'))
                        {
                            data.McDataType = MelsecMcDataType.DX;
                            address = address.Substring(2);
                            if (address.StartsWith("0"))
                            {
                                data.AddressStart = Convert.ToInt32(address, 8);
                            }
                            else
                            {
                                data.AddressStart = Convert.ToInt32(address, MelsecMcDataType.DX.FromBase);
                            }
                        }
                        else if ((address[1] == 'Y') || (address[1] == 's'))
                        {
                            data.McDataType = MelsecMcDataType.DY;
                            address = address.Substring(2);
                            if (address.StartsWith("0"))
                            {
                                data.AddressStart = Convert.ToInt32(address, 8);
                            }
                            else
                            {
                                data.AddressStart = Convert.ToInt32(address, MelsecMcDataType.DY.FromBase);
                            }
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.D;
                            data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.D.FromBase);
                        }
                        goto Label_08E6;

                    case 'F':
                    case 'f':
                        data.McDataType = MelsecMcDataType.F;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.F.FromBase);
                        goto Label_08E6;

                    case 'L':
                    case 'l':
                        data.McDataType = MelsecMcDataType.L;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.L.FromBase);
                        goto Label_08E6;

                    case 'M':
                    case 'm':
                        data.McDataType = MelsecMcDataType.M;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.M.FromBase);
                        goto Label_08E6;

                    case 'R':
                    case 'r':
                        data.McDataType = MelsecMcDataType.R;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.R.FromBase);
                        goto Label_08E6;

                    case 'S':
                    case 's':
                        if ((address[1] == 'N') || (address[1] == 'n'))
                        {
                            data.McDataType = MelsecMcDataType.SN;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.SN.FromBase);
                        }
                        else if ((address[1] == 'S') || (address[1] == 's'))
                        {
                            data.McDataType = MelsecMcDataType.SS;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.SS.FromBase);
                        }
                        else if ((address[1] == 'C') || (address[1] == 'c'))
                        {
                            data.McDataType = MelsecMcDataType.SC;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.SC.FromBase);
                        }
                        else if ((address[1] == 'M') || (address[1] == 'm'))
                        {
                            data.McDataType = MelsecMcDataType.SM;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.SM.FromBase);
                        }
                        else if ((address[1] == 'D') || (address[1] == 'd'))
                        {
                            data.McDataType = MelsecMcDataType.SD;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.SD.FromBase);
                        }
                        else if ((address[1] == 'B') || (address[1] == 'b'))
                        {
                            data.McDataType = MelsecMcDataType.SB;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.SB.FromBase);
                        }
                        else if ((address[1] == 'W') || (address[1] == 'w'))
                        {
                            data.McDataType = MelsecMcDataType.SW;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.SW.FromBase);
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.S;
                            data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.S.FromBase);
                        }
                        goto Label_08E6;

                    case 'T':
                    case 't':
                        if ((address[1] != 'N') && (address[1] != 'n'))
                        {
                            if ((address[1] != 'S') && (address[1] != 's'))
                            {
                                if ((address[1] != 'C') && (address[1] != 'c'))
                                {
                                    throw new Exception(StringResources.Language.NotSupportedDataType);
                                }
                                data.McDataType = MelsecMcDataType.TC;
                                data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.TC.FromBase);
                            }
                            else
                            {
                                data.McDataType = MelsecMcDataType.TS;
                                data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.TS.FromBase);
                            }
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.TN;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.TN.FromBase);
                        }
                        goto Label_08E6;

                    case 'V':
                    case 'v':
                        data.McDataType = MelsecMcDataType.V;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.V.FromBase);
                        goto Label_08E6;

                    case 'W':
                    case 'w':
                        data.McDataType = MelsecMcDataType.W;
                        data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.W.FromBase);
                        goto Label_08E6;

                    case 'X':
                    case 'x':
                        data.McDataType = MelsecMcDataType.X;
                        address = address.Substring(1);
                        if (address.StartsWith("0"))
                        {
                            data.AddressStart = Convert.ToInt32(address, 8);
                        }
                        else
                        {
                            data.AddressStart = Convert.ToInt32(address, MelsecMcDataType.X.FromBase);
                        }
                        goto Label_08E6;

                    case 'Y':
                    case 'y':
                        data.McDataType = MelsecMcDataType.Y;
                        address = address.Substring(1);
                        if (address.StartsWith("0"))
                        {
                            data.AddressStart = Convert.ToInt32(address, 8);
                        }
                        else
                        {
                            data.AddressStart = Convert.ToInt32(address, MelsecMcDataType.Y.FromBase);
                        }
                        goto Label_08E6;

                    case 'Z':
                    case 'z':
                        if (address.StartsWith("ZR") || address.StartsWith("zr"))
                        {
                            data.McDataType = MelsecMcDataType.ZR;
                            data.AddressStart = Convert.ToInt32(address.Substring(2), MelsecMcDataType.ZR.FromBase);
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.Z;
                            data.AddressStart = Convert.ToInt32(address.Substring(1), MelsecMcDataType.Z.FromBase);
                        }
                        goto Label_08E6;
                }
                throw new Exception(StringResources.Language.NotSupportedDataType);
            }
            catch (Exception exception)
            {
                return new OperateResult<McAddressData>(exception.Message);
            }
        Label_08E6:
            return OperateResult.CreateSuccessResult<McAddressData>(data);
        }

        public static OperateResult<McAddressData> ParsePanasonicFrom(string address, ushort length)
        {
            McAddressData data = new McAddressData {
                Length = length
            };
            try
            {
                switch (address[0])
                {
                    case 'C':
                    case 'c':
                        break;

                    case 'D':
                    case 'd':
                        if (Convert.ToInt32(address.Substring(1)) < 0x15f90)
                        {
                            data.McDataType = MelsecMcDataType.Panasonic_DT;
                            data.AddressStart = Convert.ToInt32(address.Substring(1));
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.Panasonic_SD;
                            data.AddressStart = Convert.ToInt32(address.Substring(1)) - 0x15f90;
                        }
                        goto Label_0390;

                    case 'L':
                    case 'l':
                        if ((address[1] == 'D') || (address[1] == 'd'))
                        {
                            data.McDataType = MelsecMcDataType.Panasonic_LD;
                            data.AddressStart = Convert.ToInt32(address.Substring(2));
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.Panasonic_L;
                            data.AddressStart = PanasonicHelper.CalculateComplexAddress(address.Substring(1));
                        }
                        goto Label_0390;

                    case 'X':
                    case 'x':
                        data.McDataType = MelsecMcDataType.Panasonic_X;
                        data.AddressStart = PanasonicHelper.CalculateComplexAddress(address.Substring(1));
                        goto Label_0390;

                    case 'Y':
                    case 'y':
                        data.McDataType = MelsecMcDataType.Panasonic_Y;
                        data.AddressStart = PanasonicHelper.CalculateComplexAddress(address.Substring(1));
                        goto Label_0390;

                    case 'R':
                    case 'r':
                    {
                        int num = PanasonicHelper.CalculateComplexAddress(address.Substring(1));
                        if (num < 0x3840)
                        {
                            data.McDataType = MelsecMcDataType.Panasonic_R;
                            data.AddressStart = num;
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.Panasonic_SM;
                            data.AddressStart = num - 0x3840;
                        }
                        goto Label_0390;
                    }
                    case 'T':
                    case 't':
                        if ((address[1] != 'N') && (address[1] != 'n'))
                        {
                            if ((address[1] != 'S') && (address[1] != 's'))
                            {
                                throw new Exception(StringResources.Language.NotSupportedDataType);
                            }
                            data.McDataType = MelsecMcDataType.Panasonic_TS;
                            data.AddressStart = Convert.ToInt32(address.Substring(2));
                        }
                        else
                        {
                            data.McDataType = MelsecMcDataType.Panasonic_TN;
                            data.AddressStart = Convert.ToInt32(address.Substring(2));
                        }
                        goto Label_0390;

                    default:
                        throw new Exception(StringResources.Language.NotSupportedDataType);
                }
                if ((address[1] != 'N') && (address[1] != 'n'))
                {
                    if ((address[1] != 'S') && (address[1] != 's'))
                    {
                        throw new Exception(StringResources.Language.NotSupportedDataType);
                    }
                    data.McDataType = MelsecMcDataType.Panasonic_CS;
                    data.AddressStart = Convert.ToInt32(address.Substring(2));
                }
                else
                {
                    data.McDataType = MelsecMcDataType.Panasonic_CN;
                    data.AddressStart = Convert.ToInt32(address.Substring(2));
                }
            }
            catch (Exception exception)
            {
                return new OperateResult<McAddressData>(exception.Message);
            }
        Label_0390:
            return OperateResult.CreateSuccessResult<McAddressData>(data);
        }

        public override string ToString()
        {
            return (this.McDataType.AsciiCode.Replace("*", "") + Convert.ToString(base.AddressStart, this.McDataType.FromBase));
        }

        public MelsecMcDataType McDataType { get; set; }
    }
}

