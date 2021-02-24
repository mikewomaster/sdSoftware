namespace HslCommunication.Profinet.LSIS
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class XGBFastEnet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <CpuError>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <CpuType>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HslCommunication.Profinet.LSIS.LSCpuStatus <LSCpuStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <SetCpuType>k__BackingField;
        public static string AddressTypes = "PMLKFTCDSQINUZR";
        private byte baseNo;
        private string CompanyID1;
        private LSCpuInfo cpuInfo;
        private byte slotNo;

        public XGBFastEnet()
        {
            this.CompanyID1 = "LSIS-XGT";
            this.cpuInfo = LSCpuInfo.XGK;
            this.baseNo = 0;
            this.slotNo = 3;
            base.WordLength = 2;
            this.IpAddress = "127.0.0.1";
            this.Port = 0x7d4;
            base.ByteTransform = new RegularByteTransform();
        }

        public XGBFastEnet(string ipAddress, int port)
        {
            this.CompanyID1 = "LSIS-XGT";
            this.cpuInfo = LSCpuInfo.XGK;
            this.baseNo = 0;
            this.slotNo = 3;
            base.WordLength = 2;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        public XGBFastEnet(string CpuType, string ipAddress, int port, byte slotNo)
        {
            this.CompanyID1 = "LSIS-XGT";
            this.cpuInfo = LSCpuInfo.XGK;
            this.baseNo = 0;
            this.slotNo = 3;
            this.SetCpuType = CpuType;
            base.WordLength = 2;
            this.IpAddress = ipAddress;
            this.Port = port;
            this.slotNo = slotNo;
            base.ByteTransform = new RegularByteTransform();
        }

        public static OperateResult<string> AnalysisAddress(string address, bool IsReadWrite)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                builder.Append("%");
                bool flag = false;
                if (IsReadWrite)
                {
                    for (int i = 0; i < AddressTypes.Length; i++)
                    {
                        if (AddressTypes[i] == address[0])
                        {
                            builder.Append(AddressTypes[i]);
                            char ch2 = address[1];
                            if (ch2 == 'X')
                            {
                                builder.Append("X");
                                if (((address[0] == 'I') || (address[0] == 'Q')) || (address[0] == 'U'))
                                {
                                    builder.Append(CalculateAddressStarted(address.Substring(2), true));
                                }
                                else if (IsHex(address.Substring(2)))
                                {
                                    builder.Append(address.Substring(2));
                                }
                                else
                                {
                                    builder.Append(CalculateAddressStarted(address.Substring(2), false));
                                }
                            }
                            else
                            {
                                builder.Append("B");
                                int num2 = 0;
                                if (address[1] == 'B')
                                {
                                    if (((address[0] == 'I') || (address[0] == 'Q')) || (address[0] == 'U'))
                                    {
                                        num2 = CalculateAddressStarted(address.Substring(2), true);
                                    }
                                    else
                                    {
                                        num2 = CalculateAddressStarted(address.Substring(2), false);
                                    }
                                    builder.Append((num2 == 0) ? num2 : (num2 *= 2));
                                }
                                else if (address[1] == 'W')
                                {
                                    if (((address[0] == 'I') || (address[0] == 'Q')) || (address[0] == 'U'))
                                    {
                                        num2 = CalculateAddressStarted(address.Substring(2), true);
                                    }
                                    else
                                    {
                                        num2 = CalculateAddressStarted(address.Substring(2), false);
                                    }
                                    builder.Append((num2 == 0) ? num2 : (num2 *= 2));
                                }
                                else if (address[1] == 'D')
                                {
                                    num2 = CalculateAddressStarted(address.Substring(2), false);
                                    builder.Append((num2 == 0) ? num2 : (num2 *= 4));
                                }
                                else if (address[1] == 'L')
                                {
                                    num2 = CalculateAddressStarted(address.Substring(2), false);
                                    builder.Append((num2 == 0) ? num2 : (num2 *= 8));
                                }
                                else if (((address[0] == 'I') || (address[0] == 'Q')) || (address[0] == 'U'))
                                {
                                    builder.Append(CalculateAddressStarted(address.Substring(1), true));
                                }
                                else if (IsHex(address.Substring(1)))
                                {
                                    builder.Append(address.Substring(1));
                                }
                                else
                                {
                                    builder.Append(CalculateAddressStarted(address.Substring(1), false));
                                }
                            }
                            flag = true;
                            break;
                        }
                    }
                }
                else
                {
                    builder.Append(address);
                    flag = true;
                }
                if (!flag)
                {
                    throw new Exception(StringResources.Language.NotSupportedDataType);
                }
            }
            catch (Exception exception)
            {
                return new OperateResult<string>(exception.Message);
            }
            return OperateResult.CreateSuccessResult<string>(builder.ToString());
        }

        private static OperateResult<byte[]> BuildReadByteCommand(string address, ushort length)
        {
            OperateResult<string> result = AnalysisAddress(address, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<string> dataTypeToAddress = GetDataTypeToAddress(address);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(dataTypeToAddress);
            }
            byte[] array = new byte[12 + result.Content.Length];
            switch (dataTypeToAddress.Content)
            {
                case "Bit":
                    array[2] = 0;
                    break;

                case "Word":
                case "DWord":
                case "LWord":
                case "Continuous":
                    array[2] = 20;
                    break;
            }
            array[0] = 0x54;
            array[1] = 0;
            array[3] = 0;
            array[4] = 0;
            array[5] = 0;
            array[6] = 1;
            array[7] = 0;
            array[8] = (byte) result.Content.Length;
            array[9] = 0;
            Encoding.ASCII.GetBytes(result.Content).CopyTo(array, 10);
            BitConverter.GetBytes(length).CopyTo(array, (int) (array.Length - 2));
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        private OperateResult<byte[]> BuildWriteByteCommand(string address, byte[] data)
        {
            OperateResult<string> result;
            switch (this.SetCpuType)
            {
                case "XGK":
                    result = AnalysisAddress(address, true);
                    break;

                case "XGB":
                    result = AnalysisAddress(address, false);
                    break;

                default:
                    result = AnalysisAddress(address, true);
                    break;
            }
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<string> dataTypeToAddress = GetDataTypeToAddress(address);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(dataTypeToAddress);
            }
            byte[] array = new byte[(12 + result.Content.Length) + data.Length];
            switch (dataTypeToAddress.Content)
            {
                case "Bit":
                case "Byte":
                    array[2] = 1;
                    break;

                case "Word":
                    array[2] = 2;
                    break;

                case "DWord":
                    array[2] = 3;
                    break;

                case "LWord":
                    array[2] = 4;
                    break;

                case "Continuous":
                    array[2] = 20;
                    break;
            }
            array[0] = 0x58;
            array[1] = 0;
            array[3] = 0;
            array[4] = 0;
            array[5] = 0;
            array[6] = 1;
            array[7] = 0;
            array[8] = (byte) result.Content.Length;
            array[9] = 0;
            Encoding.ASCII.GetBytes(result.Content).CopyTo(array, 10);
            BitConverter.GetBytes(data.Length).CopyTo(array, (int) ((array.Length - 2) - data.Length));
            data.CopyTo(array, (int) (array.Length - data.Length));
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static int CalculateAddressStarted(string address, [Optional, DefaultParameterValue(false)] bool QI)
        {
            if (address.IndexOf('.') < 0)
            {
                return Convert.ToInt32(address);
            }
            char[] separator = new char[] { '.' };
            string[] strArray = address.Split(separator);
            if (!QI)
            {
                return Convert.ToInt32(strArray[0]);
            }
            if (strArray.Length >= 4)
            {
                return Convert.ToInt32(strArray[3]);
            }
            return Convert.ToInt32(strArray[2]);
        }

        public OperateResult<byte[]> ExtractActualData(byte[] response)
        {
            if (response.Length < 20)
            {
                return new OperateResult<byte[]>("Length is less than 20:" + SoftBasic.ByteToHexString(response));
            }
            ushort num = BitConverter.ToUInt16(response, 10);
            BitArray array = new BitArray(BitConverter.GetBytes(num));
            int num2 = num % 0x20;
            switch ((num % 0x20))
            {
                case 1:
                    this.CpuType = "XGK/R-CPUH";
                    break;

                case 2:
                    this.CpuType = "XGK-CPUS";
                    break;

                case 4:
                    this.CpuType = "XGK-CPUE";
                    break;

                case 5:
                    this.CpuType = "XGK/R-CPUH";
                    break;

                case 6:
                    this.CpuType = "XGB/XBCU";
                    break;
            }
            this.CpuError = array[7];
            if (array[8])
            {
                this.LSCpuStatus = HslCommunication.Profinet.LSIS.LSCpuStatus.RUN;
            }
            if (array[9])
            {
                this.LSCpuStatus = HslCommunication.Profinet.LSIS.LSCpuStatus.STOP;
            }
            if (array[10])
            {
                this.LSCpuStatus = HslCommunication.Profinet.LSIS.LSCpuStatus.ERROR;
            }
            if (array[11])
            {
                this.LSCpuStatus = HslCommunication.Profinet.LSIS.LSCpuStatus.DEBUG;
            }
            if (response.Length < 0x1c)
            {
                return new OperateResult<byte[]>("Length is less than 28:" + SoftBasic.ByteToHexString(response));
            }
            if (BitConverter.ToUInt16(response, 0x1a) > 0)
            {
                return new OperateResult<byte[]>(response[0x1c], "Error:" + GetErrorDesciption(response[0x1c]));
            }
            if (response[20] == 0x59)
            {
                return OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
            }
            if (response[20] == 0x55)
            {
                try
                {
                    ushort length = BitConverter.ToUInt16(response, 30);
                    byte[] destinationArray = new byte[length];
                    Array.Copy(response, 0x20, destinationArray, 0, length);
                    return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
                }
                catch (Exception exception)
                {
                    return new OperateResult<byte[]>(exception.Message);
                }
            }
            return new OperateResult<byte[]>(StringResources.Language.NotSupportedFunction);
        }

        public static OperateResult<string> GetDataTypeToAddress(string address)
        {
            string str = string.Empty;
            try
            {
                char[] chArray = new char[] { 'P', 'M', 'L', 'K', 'F', 'T', 'C', 'D', 'S', 'Q', 'I', 'R' };
                bool flag = false;
                for (int i = 0; i < chArray.Length; i++)
                {
                    if (chArray[i] != address[0])
                    {
                        continue;
                    }
                    char ch = address[1];
                    if (ch <= 'D')
                    {
                        switch (ch)
                        {
                            case 'B':
                                goto Label_0094;

                            case 'D':
                                goto Label_0084;
                        }
                        goto Label_009C;
                    }
                    if (ch == 'L')
                    {
                        goto Label_008C;
                    }
                    if (ch != 'W')
                    {
                        if (ch != 'X')
                        {
                            goto Label_009C;
                        }
                        str = "Bit";
                    }
                    else
                    {
                        str = "Word";
                    }
                    goto Label_00A4;
                Label_0084:
                    str = "DWord";
                    goto Label_00A4;
                Label_008C:
                    str = "LWord";
                    goto Label_00A4;
                Label_0094:
                    str = "Continuous";
                    goto Label_00A4;
                Label_009C:
                    str = "Continuous";
                Label_00A4:
                    flag = true;
                    break;
                }
                if (!flag)
                {
                    throw new Exception(StringResources.Language.NotSupportedDataType);
                }
            }
            catch (Exception exception)
            {
                return new OperateResult<string>(exception.Message);
            }
            return OperateResult.CreateSuccessResult<string>(str);
        }

        public static string GetErrorDesciption(byte code)
        {
            switch (code)
            {
                case 0:
                    return "Normal";

                case 1:
                    return "Physical layer error (TX, RX unavailable)";

                case 3:
                    return "There is no identifier of Function Block to receive in communication channel";

                case 4:
                    return "Mismatch of data type";

                case 5:
                    return "Reset is received from partner station";

                case 6:
                    return "Communication instruction of partner station is not ready status";

                case 7:
                    return "Device status of remote station is not desirable status";

                case 8:
                    return "Access to some target is not available";

                case 9:
                    return "Can’ t deal with communication instruction of partner station by too many reception";

                case 10:
                    return "Time Out error";

                case 11:
                    return "Structure error";

                case 12:
                    return "Abort";

                case 13:
                    return "Reject(local/remote)";

                case 14:
                    return "Communication channel establishment error (Connect/Disconnect)";

                case 15:
                    return "High speed communication and connection service error";

                case 0x21:
                    return "Can’t find variable identifier";

                case 0x22:
                    return "Address error";

                case 50:
                    return "Response error";

                case 0x71:
                    return "Object Access Unsupported";

                case 0xbb:
                    return "Unknown error code (communication code of other company) is received";
            }
            return "Unknown error";
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new LsisFastEnetMessage();
        }

        private static bool IsHex(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            bool flag = false;
            for (int i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'a':
                    case 'b':
                    case 'c':
                    case 'd':
                    case 'e':
                    case 'f':
                        flag = true;
                        break;
                }
            }
            return flag;
        }

        private byte[] PackCommand(byte[] coreCommand)
        {
            byte[] array = new byte[coreCommand.Length + 20];
            Encoding.ASCII.GetBytes(this.CompanyID).CopyTo(array, 0);
            switch (this.cpuInfo)
            {
                case LSCpuInfo.XGK:
                    array[12] = 160;
                    break;

                case LSCpuInfo.XGI:
                    array[12] = 0xa4;
                    break;

                case LSCpuInfo.XGR:
                    array[12] = 0xa8;
                    break;

                case LSCpuInfo.XGB_MK:
                    array[12] = 0xb0;
                    break;

                case LSCpuInfo.XGB_IEC:
                    array[12] = 180;
                    break;
            }
            array[13] = 0x33;
            BitConverter.GetBytes((short) coreCommand.Length).CopyTo(array, 0x10);
            array[0x12] = (byte) ((this.baseNo * 0x10) + this.slotNo);
            int num = 0;
            for (int i = 0; i < 0x13; i++)
            {
                num += array[i];
            }
            array[0x13] = (byte) num;
            coreCommand.CopyTo(array, 20);
            string str = SoftBasic.ByteToHexString(array, ' ');
            return array;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadByteCommand(address, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(this.PackCommand(result.Content));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return this.ExtractActualData(result2.Content);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> result = BuildReadByteCommand(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(this.PackCommand(result.Content));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = this.ExtractActualData(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(result3.Content, length));
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 1));
        }

        public OperateResult<bool> ReadCoil(string address)
        {
            return this.ReadBool(address);
        }

        public OperateResult<bool[]> ReadCoil(string address, ushort length)
        {
            return this.ReadBool(address, length);
        }

        public override string ToString()
        {
            return string.Format("XGBFastEnet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = this.BuildWriteByteCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(this.PackCommand(result.Content));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return this.ExtractActualData(result2.Content);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            return this.WriteCoil(address, value);
        }

        [HslMqttApi("WriteByte", "")]
        public OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[] { value };
            return this.Write(address, buffer1);
        }

        public OperateResult WriteCoil(string address, bool value)
        {
            byte[] buffer1 = new byte[2];
            buffer1[0] = value ? ((byte) 1) : ((byte) 0);
            return this.Write(address, buffer1);
        }

        public byte BaseNo
        {
            get
            {
                return this.baseNo;
            }
            set
            {
                this.baseNo = value;
            }
        }

        public string CompanyID
        {
            get
            {
                return this.CompanyID1;
            }
            set
            {
                this.CompanyID1 = value;
            }
        }

        public bool CpuError { get; private set; }

        public LSCpuInfo CpuInfo
        {
            get
            {
                return this.cpuInfo;
            }
            set
            {
                this.cpuInfo = value;
            }
        }

        public string CpuType { get; private set; }

        public HslCommunication.Profinet.LSIS.LSCpuStatus LSCpuStatus { get; private set; }

        public string SetCpuType { get; set; }

        public byte SlotNo
        {
            get
            {
                return this.slotNo;
            }
            set
            {
                this.slotNo = value;
            }
        }
    }
}

