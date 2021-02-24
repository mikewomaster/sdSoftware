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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class XGKFastEnet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <CpuError>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <CpuType>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HslCommunication.Profinet.LSIS.LSCpuStatus <LSCpuStatus>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <SetCpuType>k__BackingField;
        private byte baseNo;
        private string CompanyID1;
        private LSCpuInfo cpuInfo;
        private byte slotNo;

        public XGKFastEnet()
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

        public XGKFastEnet(string ipAddress, int port)
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

        public XGKFastEnet(string CpuType, string ipAddress, int port, byte slotNo)
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

        private byte[] AddByte(byte[] item, ref int idx, ref byte[] header)
        {
            Array.Copy(item, 0, header, idx, item.Length);
            idx += item.Length;
            return header;
        }

        public static OperateResult<XGT_MemoryType> AnalysisAddress(string address)
        {
            XGT_MemoryType iO = XGT_MemoryType.IO;
            try
            {
                char[] chArray = new char[] { 'P', 'M', 'L', 'K', 'F', 'T', 'C', 'D', 'S', 'Q', 'I', 'N', 'U', 'Z', 'R' };
                bool flag = false;
                for (int i = 0; i < chArray.Length; i++)
                {
                    if (chArray[i] != address[0])
                    {
                        continue;
                    }
                    switch (address[0])
                    {
                        case 'C':
                            iO = XGT_MemoryType.Counter;
                            break;

                        case 'D':
                            iO = XGT_MemoryType.DataRegister;
                            break;

                        case 'F':
                            iO = XGT_MemoryType.EtcRelay;
                            break;

                        case 'K':
                            iO = XGT_MemoryType.KeepRelay;
                            break;

                        case 'L':
                            iO = XGT_MemoryType.LinkRelay;
                            break;

                        case 'M':
                            iO = XGT_MemoryType.SubRelay;
                            break;

                        case 'N':
                            iO = XGT_MemoryType.ComDataRegister;
                            break;

                        case 'P':
                            iO = XGT_MemoryType.IO;
                            break;

                        case 'R':
                            iO = XGT_MemoryType.FileDataRegister;
                            break;

                        case 'S':
                            iO = XGT_MemoryType.StepRelay;
                            break;

                        case 'T':
                            iO = XGT_MemoryType.Timer;
                            break;

                        case 'U':
                            iO = XGT_MemoryType.SpecialRegister;
                            break;
                    }
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
                return new OperateResult<XGT_MemoryType>(exception.Message);
            }
            return OperateResult.CreateSuccessResult<XGT_MemoryType>(iO);
        }

        public byte[] CreateHeader(int pInvokeID, int pDataByteLenth)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(this.CompanyID);
            byte[] item = BitConverter.GetBytes((short) 0);
            byte[] buffer3 = BitConverter.GetBytes((short) 0);
            byte[] buffer4 = new byte[1];
            switch (this.cpuInfo)
            {
                case LSCpuInfo.XGK:
                    buffer4[0] = 160;
                    break;

                case LSCpuInfo.XGI:
                    buffer4[0] = 0xa4;
                    break;

                case LSCpuInfo.XGR:
                    buffer4[0] = 0xa8;
                    break;

                case LSCpuInfo.XGB_MK:
                    buffer4[0] = 0xb0;
                    break;

                case LSCpuInfo.XGB_IEC:
                    buffer4[0] = 180;
                    break;
            }
            byte[] buffer5 = new byte[] { 0x33 };
            byte[] buffer6 = BitConverter.GetBytes((short) pInvokeID);
            byte[] buffer7 = BitConverter.GetBytes((short) pDataByteLenth);
            byte[] buffer8 = new byte[] { (byte) ((this.baseNo * 0x10) + this.slotNo) };
            byte[] buffer9 = new byte[] { 0 };
            int num = (((((((bytes.Length + item.Length) + buffer3.Length) + buffer4.Length) + buffer5.Length) + buffer6.Length) + buffer7.Length) + buffer8.Length) + buffer9.Length;
            byte[] header = new byte[num];
            int idx = 0;
            this.AddByte(bytes, ref idx, ref header);
            this.AddByte(item, ref idx, ref header);
            this.AddByte(buffer3, ref idx, ref header);
            this.AddByte(buffer4, ref idx, ref header);
            this.AddByte(buffer5, ref idx, ref header);
            this.AddByte(buffer6, ref idx, ref header);
            this.AddByte(buffer7, ref idx, ref header);
            this.AddByte(buffer8, ref idx, ref header);
            this.AddByte(buffer9, ref idx, ref header);
            return header;
        }

        private byte[] CreateReadDataFormat(XGT_Request_Func emFunc, XGT_DataType emDatatype, List<XGTAddressData> pAddressList, XGT_MemoryType emMemtype, int pDataCount)
        {
            List<XGTAddressData> list = new List<XGTAddressData>();
            int num = 0;
            byte[] bytes = BitConverter.GetBytes((short) emFunc);
            byte[] item = BitConverter.GetBytes((short) emDatatype);
            byte[] buffer3 = BitConverter.GetBytes((short) 0);
            byte[] buffer4 = BitConverter.GetBytes((short) pAddressList.Count);
            num = ((bytes.Length + item.Length) + buffer3.Length) + buffer4.Length;
            foreach (XGTAddressData data in pAddressList)
            {
                string str = this.CreateValueName(emDatatype, emMemtype, data.Address);
                XGTAddressData data2 = new XGTAddressData {
                    AddressString = str
                };
                list.Add(data2);
                num += data2.AddressByteArray.Length + data2.LengthByteArray.Length;
            }
            if ((XGT_DataType.Continue == emDatatype) && (XGT_Request_Func.Read == emFunc))
            {
                num += 2;
            }
            byte[] header = new byte[num];
            int idx = 0;
            this.AddByte(bytes, ref idx, ref header);
            this.AddByte(item, ref idx, ref header);
            this.AddByte(buffer3, ref idx, ref header);
            this.AddByte(buffer4, ref idx, ref header);
            foreach (XGTAddressData data3 in list)
            {
                this.AddByte(data3.LengthByteArray, ref idx, ref header);
                this.AddByte(data3.AddressByteArray, ref idx, ref header);
            }
            if (XGT_DataType.Continue == emDatatype)
            {
                byte[] buffer6 = BitConverter.GetBytes((short) pDataCount);
                this.AddByte(buffer6, ref idx, ref header);
            }
            return header;
        }

        public string CreateValueName(XGT_DataType dataType, XGT_MemoryType memType, string pAddress)
        {
            string memTypeChar = this.GetMemTypeChar(memType);
            string typeChar = this.GetTypeChar(dataType);
            if (dataType == XGT_DataType.Continue)
            {
                int num = Convert.ToInt32(pAddress) * 2;
                pAddress = num.ToString();
            }
            if (dataType == XGT_DataType.Bit)
            {
                int num2 = 0;
                string str4 = pAddress.Substring(0, pAddress.Length - 1);
                num2 = Convert.ToInt32(pAddress.Substring(pAddress.Length - 1), 0x10);
                pAddress = ((Convert.ToInt32(str4) * 0x10) + num2).ToString();
            }
            return ("%" + memTypeChar + typeChar + pAddress);
        }

        private byte[] CreateWriteDataFormat(XGT_Request_Func emFunc, XGT_DataType emDatatype, List<XGTAddressData> pAddressList, XGT_MemoryType emMemtype, int pDataCount)
        {
            int num = 0;
            byte[] bytes = BitConverter.GetBytes((short) emFunc);
            byte[] item = BitConverter.GetBytes((short) emDatatype);
            byte[] buffer3 = BitConverter.GetBytes((short) 0);
            byte[] buffer4 = BitConverter.GetBytes((short) pAddressList.Count);
            num = ((bytes.Length + item.Length) + buffer3.Length) + buffer4.Length;
            List<XGTAddressData> list = new List<XGTAddressData>();
            foreach (XGTAddressData data in pAddressList)
            {
                string str = this.CreateValueName(emDatatype, emMemtype, data.Address);
                data.AddressString = str;
                int length = 0;
                length = data.DataByteArray.Length;
                num += ((data.AddressByteArray.Length + data.LengthByteArray.Length) + 2) + length;
                list.Add(data);
            }
            if ((XGT_DataType.Continue == emDatatype) && (XGT_Request_Func.Read == emFunc))
            {
                num += 2;
            }
            byte[] header = new byte[num];
            int idx = 0;
            this.AddByte(bytes, ref idx, ref header);
            this.AddByte(item, ref idx, ref header);
            this.AddByte(buffer3, ref idx, ref header);
            this.AddByte(buffer4, ref idx, ref header);
            foreach (XGTAddressData data2 in list)
            {
                this.AddByte(data2.LengthByteArray, ref idx, ref header);
                this.AddByte(data2.AddressByteArray, ref idx, ref header);
            }
            foreach (XGTAddressData data3 in list)
            {
                byte[] buffer6 = BitConverter.GetBytes((short) data3.DataByteArray.Length);
                this.AddByte(buffer6, ref idx, ref header);
                this.AddByte(data3.DataByteArray, ref idx, ref header);
            }
            return header;
        }

        public OperateResult<byte[]> ExtractActualData(byte[] response)
        {
            OperateResult<bool> cpuTypeToPLC = this.GetCpuTypeToPLC(response);
            if (!cpuTypeToPLC.IsSuccess)
            {
                return new OperateResult<byte[]>(cpuTypeToPLC.Message);
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

        public OperateResult<bool[]> ExtractActualDataBool(byte[] response)
        {
            OperateResult<bool> cpuTypeToPLC = this.GetCpuTypeToPLC(response);
            if (!cpuTypeToPLC.IsSuccess)
            {
                return new OperateResult<bool[]>(cpuTypeToPLC.Message);
            }
            if (response[20] == 0x59)
            {
                return OperateResult.CreateSuccessResult<bool[]>(new bool[0]);
            }
            if (response[20] == 0x55)
            {
                int sourceIndex = 0x1c;
                byte[] destinationArray = new byte[2];
                byte[] buffer2 = new byte[2];
                byte[] buffer3 = new byte[2];
                Array.Copy(response, sourceIndex, destinationArray, 0, 2);
                int num = BitConverter.ToInt16(destinationArray, 0);
                List<bool> list = new List<bool>();
                sourceIndex += 2;
                try
                {
                    for (int i = 0; i < num; i++)
                    {
                        Array.Copy(response, sourceIndex, buffer2, 0, 2);
                        int length = BitConverter.ToInt16(buffer2, 0);
                        sourceIndex += 2;
                        buffer3 = new byte[length];
                        Array.Copy(response, sourceIndex, buffer3, 0, length);
                        sourceIndex += length;
                        list.Add(BitConverter.ToBoolean(buffer3, 0));
                    }
                    return OperateResult.CreateSuccessResult<bool[]>(list.ToArray());
                }
                catch (Exception exception)
                {
                    return new OperateResult<bool[]>(exception.Message);
                }
            }
            return new OperateResult<bool[]>(StringResources.Language.NotSupportedFunction);
        }

        public OperateResult<byte[]> ExtractActualDatabyte(byte[] response)
        {
            OperateResult<bool> cpuTypeToPLC = this.GetCpuTypeToPLC(response);
            if (!cpuTypeToPLC.IsSuccess)
            {
                return new OperateResult<byte[]>(cpuTypeToPLC.Message);
            }
            if (response[20] == 0x59)
            {
                return OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
            }
            if (response[20] == 0x55)
            {
                int sourceIndex = 0x1c;
                byte[] destinationArray = new byte[2];
                byte[] buffer2 = new byte[2];
                byte[] buffer3 = new byte[2];
                Array.Copy(response, sourceIndex, destinationArray, 0, 2);
                int num = BitConverter.ToInt16(destinationArray, 0);
                List<byte> list = new List<byte>();
                sourceIndex += 2;
                try
                {
                    for (int i = 0; i < num; i++)
                    {
                        Array.Copy(response, sourceIndex, buffer2, 0, 2);
                        int length = BitConverter.ToInt16(buffer2, 0);
                        sourceIndex += 2;
                        Array.Copy(response, sourceIndex, buffer3, 0, length);
                        sourceIndex += length;
                        list.AddRange(buffer3);
                    }
                    return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
                }
                catch (Exception exception)
                {
                    return new OperateResult<byte[]>(exception.Message);
                }
            }
            return new OperateResult<byte[]>(StringResources.Language.NotSupportedFunction);
        }

        public OperateResult<bool> GetCpuTypeToPLC(byte[] response)
        {
            try
            {
                if (response.Length < 20)
                {
                    return new OperateResult<bool>("Length is less than 20:" + SoftBasic.ByteToHexString(response));
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
                    return new OperateResult<bool>("Length is less than 28:" + SoftBasic.ByteToHexString(response));
                }
                if (BitConverter.ToUInt16(response, 0x1a) > 0)
                {
                    return new OperateResult<bool>(response[0x1c], "Error:" + GetErrorDesciption(response[0x1c]));
                }
            }
            catch (Exception exception)
            {
                return new OperateResult<bool>(exception.Message);
            }
            return OperateResult.CreateSuccessResult<bool>(true);
        }

        public static OperateResult<XGT_DataType, bool> GetDataTypeToAddress(string address)
        {
            XGT_DataType bit = XGT_DataType.Bit;
            bool flag = false;
            try
            {
                char[] chArray = new char[] { 'P', 'M', 'L', 'K', 'F', 'T', 'C', 'D', 'S', 'Q', 'I', 'N', 'U', 'Z', 'R' };
                bool flag2 = false;
                for (int i = 0; i < chArray.Length; i++)
                {
                    if (chArray[i] != address[0])
                    {
                        continue;
                    }
                    char ch = address[1];
                    if (ch <= 'L')
                    {
                        switch (ch)
                        {
                            case 'B':
                                bit = XGT_DataType.Byte;
                                goto Label_009B;

                            case 'C':
                                bit = XGT_DataType.Continue;
                                goto Label_009B;

                            case 'D':
                                bit = XGT_DataType.DWord;
                                goto Label_009B;

                            case 'L':
                                goto Label_0087;
                        }
                        goto Label_0094;
                    }
                    if (ch != 'W')
                    {
                        if (ch != 'X')
                        {
                            goto Label_0094;
                        }
                        bit = XGT_DataType.Bit;
                    }
                    else
                    {
                        bit = XGT_DataType.Word;
                    }
                    goto Label_009B;
                Label_0087:
                    bit = XGT_DataType.LWord;
                    goto Label_009B;
                Label_0094:
                    flag = true;
                    bit = XGT_DataType.Continue;
                Label_009B:
                    flag2 = true;
                    break;
                }
                if (!flag2)
                {
                    throw new Exception(StringResources.Language.NotSupportedDataType);
                }
            }
            catch (Exception exception)
            {
                return new OperateResult<XGT_DataType, bool>(exception.Message);
            }
            return OperateResult.CreateSuccessResult<XGT_DataType, bool>(bit, flag);
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

        private string GetMemTypeChar(XGT_MemoryType type)
        {
            string str = string.Empty;
            switch (type)
            {
                case XGT_MemoryType.IO:
                    return "P";

                case XGT_MemoryType.SubRelay:
                    return "M";

                case XGT_MemoryType.LinkRelay:
                    return "L";

                case XGT_MemoryType.KeepRelay:
                    return "K";

                case XGT_MemoryType.EtcRelay:
                    return "F";

                case XGT_MemoryType.Timer:
                    return "T";

                case XGT_MemoryType.Counter:
                    return "C";

                case XGT_MemoryType.DataRegister:
                    return "D";

                case XGT_MemoryType.ComDataRegister:
                    return "N";

                case XGT_MemoryType.FileDataRegister:
                    return "R";

                case XGT_MemoryType.StepRelay:
                    return "S";

                case XGT_MemoryType.SpecialRegister:
                    return "U";
            }
            return str;
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new LsisFastEnetMessage();
        }

        private string GetTypeChar(XGT_DataType type)
        {
            switch (type)
            {
                case XGT_DataType.Bit:
                    return "X";

                case XGT_DataType.Byte:
                    return "B";

                case XGT_DataType.Word:
                    return "W";

                case XGT_DataType.DWord:
                    return "D";

                case XGT_DataType.LWord:
                    return "L";

                case XGT_DataType.Continue:
                    return "B";
            }
            return "X";
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = null;
            List<XGTAddressData> pAddress = new List<XGTAddressData>();
            string[] separator = new string[] { ";", "," };
            string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray)
            {
                XGTAddressData item = new XGTAddressData();
                if (GetDataTypeToAddress(str).Content2)
                {
                    item.Address = str.Substring(1);
                }
                else
                {
                    item.Address = str.Substring(2);
                }
                pAddress.Add(item);
            }
            OperateResult<XGT_MemoryType> result2 = AnalysisAddress(strArray[0]);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<XGT_DataType, bool> dataTypeToAddress = GetDataTypeToAddress(strArray[0]);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(dataTypeToAddress);
            }
            if (((XGT_DataType) dataTypeToAddress.Content1) == XGT_DataType.Continue)
            {
                result = this.Read(dataTypeToAddress.Content1, pAddress, result2.Content, 1, length);
            }
            else
            {
                result = this.Read(dataTypeToAddress.Content1, pAddress, result2.Content, 1, 0);
            }
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result4 = base.ReadFromCoreServer(result.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result4);
            }
            if (pAddress.Count > 1)
            {
                return this.ExtractActualDatabyte(result4.Content);
            }
            return this.ExtractActualData(result4.Content);
        }

        public OperateResult<byte[]> Read(XGT_DataType pDataType, List<XGTAddressData> pAddress, XGT_MemoryType pMemtype, int pInvokeID, [Optional, DefaultParameterValue(0)] int pDataCount)
        {
            if (pAddress.Count > 0x10)
            {
                return new OperateResult<byte[]>("You cannot read more than 16 pieces.");
            }
            try
            {
                byte[] item = this.CreateReadDataFormat(XGT_Request_Func.Read, pDataType, pAddress, pMemtype, pDataCount);
                byte[] buffer2 = this.CreateHeader(pInvokeID, item.Length);
                byte[] header = new byte[buffer2.Length + item.Length];
                int idx = 0;
                this.AddByte(buffer2, ref idx, ref header);
                this.AddByte(item, ref idx, ref header);
                return OperateResult.CreateSuccessResult<byte[]>(header);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("ERROR:" + exception.Message.ToString());
            }
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> result = null;
            List<XGTAddressData> pAddress = new List<XGTAddressData>();
            string[] separator = new string[] { ";", "," };
            string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray)
            {
                XGTAddressData item = new XGTAddressData();
                if (GetDataTypeToAddress(str).Content2)
                {
                    item.Address = str.Substring(1);
                }
                else
                {
                    item.Address = str.Substring(2);
                }
                pAddress.Add(item);
            }
            OperateResult<XGT_MemoryType> result2 = AnalysisAddress(strArray[0]);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<XGT_DataType, bool> dataTypeToAddress = GetDataTypeToAddress(strArray[0]);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(dataTypeToAddress);
            }
            if (((XGT_DataType) dataTypeToAddress.Content1) == XGT_DataType.Continue)
            {
                result = this.Read(dataTypeToAddress.Content1, pAddress, result2.Content, 1, length);
            }
            else
            {
                result = this.Read(dataTypeToAddress.Content1, pAddress, result2.Content, 1, 0);
            }
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result4 = base.ReadFromCoreServer(result.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            if (pAddress.Count > 1)
            {
                OperateResult<bool[]> result7 = this.ExtractActualDataBool(result4.Content);
                if (!result7.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result7);
                }
                return OperateResult.CreateSuccessResult<bool[]>(result7.Content);
            }
            OperateResult<byte[]> result8 = this.ExtractActualData(result4.Content);
            if (!result8.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result8);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(result8.Content));
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
            return string.Format("XGkFastEnet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = null;
            List<XGTAddressData> pAddressList = new List<XGTAddressData>();
            string[] separator = new string[] { ";", "," };
            string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray)
            {
                XGTAddressData item = new XGTAddressData();
                if (GetDataTypeToAddress(str).Content2)
                {
                    item.Address = str.Substring(1);
                }
                else
                {
                    item.Address = str.Substring(2);
                }
                item.DataByteArray = value;
                pAddressList.Add(item);
            }
            OperateResult<XGT_MemoryType> result2 = AnalysisAddress(address);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<XGT_DataType, bool> dataTypeToAddress = GetDataTypeToAddress(address);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(dataTypeToAddress);
            }
            if (((XGT_DataType) dataTypeToAddress.Content1) == XGT_DataType.Continue)
            {
                result = this.Write(dataTypeToAddress.Content1, pAddressList, result2.Content, 1, value.Length);
            }
            else
            {
                result = this.Write(dataTypeToAddress.Content1, pAddressList, result2.Content, 1, 0);
            }
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result4 = base.ReadFromCoreServer(result.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result4);
            }
            return this.ExtractActualData(result4.Content);
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

        public OperateResult<byte[]> Write(XGT_DataType pDataType, List<XGTAddressData> pAddressList, XGT_MemoryType pMemtype, int pInvokeID, [Optional, DefaultParameterValue(0)] int pDataCount)
        {
            try
            {
                byte[] item = this.CreateWriteDataFormat(XGT_Request_Func.Write, pDataType, pAddressList, pMemtype, pDataCount);
                byte[] buffer2 = this.CreateHeader(pInvokeID, item.Length);
                byte[] header = new byte[buffer2.Length + item.Length];
                int idx = 0;
                this.AddByte(buffer2, ref idx, ref header);
                this.AddByte(item, ref idx, ref header);
                return OperateResult.CreateSuccessResult<byte[]>(header);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("ERROR:" + exception.Message.ToString());
            }
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

