namespace HslCommunication.Profinet.LSIS
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class XGKCnet : SerialDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <Station>k__BackingField = 5;

        public XGKCnet()
        {
            base.ByteTransform = new RegularByteTransform();
            base.WordLength = 2;
        }

        private byte[] CreateReadDataFormat(byte station, XGT_Request_Func emFunc, XGT_DataType emDatatype, List<XGTAddressData> pAddressList, XGT_MemoryType emMemtype, int pDataCount)
        {
            List<XGTAddressData> list = new List<XGTAddressData>();
            foreach (XGTAddressData data in pAddressList)
            {
                string str = new XGKFastEnet().CreateValueName(emDatatype, emMemtype, data.Address);
                XGTAddressData item = new XGTAddressData {
                    AddressString = str
                };
                list.Add(item);
            }
            List<byte> list2 = new List<byte>();
            if ((XGT_DataType.Continue == emDatatype) && (XGT_Request_Func.Read == emFunc))
            {
                list2.Add(5);
                list2.AddRange(SoftBasic.BuildAsciiBytesFrom(station));
                list2.Add(0x72);
                list2.Add(0x53);
                list2.Add(0x42);
                foreach (XGTAddressData data3 in list)
                {
                    list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) data3.AddressString.Length));
                    list2.AddRange(Encoding.ASCII.GetBytes(data3.AddressString));
                    list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) pDataCount));
                }
                list2.Add(4);
                int num = 0;
                for (int i = 0; i < list2.Count; i++)
                {
                    num += list2[i];
                }
                list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) num));
            }
            else
            {
                list2.Add(5);
                list2.AddRange(SoftBasic.BuildAsciiBytesFrom(station));
                list2.Add(0x72);
                list2.Add(0x53);
                list2.Add(0x53);
                list2.Add(0x30);
                list2.Add(0x31);
                foreach (XGTAddressData data4 in list)
                {
                    list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) data4.AddressString.Length));
                    list2.AddRange(Encoding.ASCII.GetBytes(data4.AddressString));
                }
                list2.Add(4);
                int num3 = 0;
                for (int j = 0; j < list2.Count; j++)
                {
                    num3 += list2[j];
                }
                list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) num3));
            }
            return list2.ToArray();
        }

        private byte[] CreateWriteDataFormat(byte station, XGT_Request_Func emFunc, XGT_DataType emDatatype, List<XGTAddressData> pAddressList, XGT_MemoryType emMemtype, int pDataCount)
        {
            List<XGTAddressData> list = new List<XGTAddressData>();
            foreach (XGTAddressData data in pAddressList)
            {
                string str = new XGKFastEnet().CreateValueName(emDatatype, emMemtype, data.Address);
                data.AddressString = str;
                list.Add(data);
            }
            List<byte> list2 = new List<byte>();
            if ((XGT_DataType.Continue == emDatatype) && (XGT_Request_Func.Write == emFunc))
            {
                list2.Add(5);
                list2.AddRange(SoftBasic.BuildAsciiBytesFrom(station));
                list2.Add(0x77);
                list2.Add(0x53);
                list2.Add(0x42);
                foreach (XGTAddressData data2 in list)
                {
                    list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) data2.AddressString.Length));
                    list2.AddRange(Encoding.ASCII.GetBytes(data2.AddressString));
                    list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) data2.AddressByteArray.Length));
                    list2.AddRange(SoftBasic.BytesToAsciiBytes(data2.AddressByteArray));
                }
                list2.Add(4);
                int num = 0;
                for (int i = 0; i < list2.Count; i++)
                {
                    num += list2[i];
                }
                list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) num));
            }
            else
            {
                list2.Add(5);
                list2.AddRange(SoftBasic.BuildAsciiBytesFrom(station));
                list2.Add(0x77);
                list2.Add(0x53);
                list2.Add(0x53);
                list2.Add(0x30);
                list2.Add(0x31);
                foreach (XGTAddressData data3 in list)
                {
                    list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) data3.AddressString.Length));
                    list2.AddRange(Encoding.ASCII.GetBytes(data3.AddressString));
                    list2.AddRange(SoftBasic.BytesToAsciiBytes(data3.AddressByteArray));
                }
                list2.Add(4);
                int num3 = 0;
                for (int j = 0; j < list2.Count; j++)
                {
                    num3 += list2[j];
                }
                list2.AddRange(SoftBasic.BuildAsciiBytesFrom((byte) num3));
            }
            return list2.ToArray();
        }

        public static OperateResult<byte[]> ExtractActualData(byte[] response, bool isRead)
        {
            try
            {
                if (isRead)
                {
                    if (response[0] == 6)
                    {
                        byte[] buffer = new byte[response.Length - 13];
                        Array.Copy(response, 10, buffer, 0, buffer.Length);
                        return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.AsciiBytesToBytes(buffer));
                    }
                    byte[] buffer2 = new byte[response.Length - 9];
                    Array.Copy(response, 6, buffer2, 0, buffer2.Length);
                    return new OperateResult<byte[]>(BitConverter.ToUInt16(SoftBasic.AsciiBytesToBytes(buffer2), 0), "Data:" + SoftBasic.ByteToHexString(response));
                }
                if (response[0] == 6)
                {
                    return OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
                }
                byte[] destinationArray = new byte[response.Length - 9];
                Array.Copy(response, 6, destinationArray, 0, destinationArray.Length);
                return new OperateResult<byte[]>(BitConverter.ToUInt16(SoftBasic.AsciiBytesToBytes(destinationArray), 0), "Data:" + SoftBasic.ByteToHexString(response));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            List<XGTAddressData> pAddress = new List<XGTAddressData>();
            string[] separator = new string[] { ";", "," };
            string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray)
            {
                XGTAddressData item = new XGTAddressData();
                if (XGKFastEnet.GetDataTypeToAddress(str).Content2)
                {
                    item.Address = str.Substring(1);
                }
                else
                {
                    item.Address = str.Substring(2);
                }
                pAddress.Add(item);
            }
            OperateResult<XGT_MemoryType> result = XGKFastEnet.AnalysisAddress(strArray[0]);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<XGT_DataType, bool> dataTypeToAddress = XGKFastEnet.GetDataTypeToAddress(strArray[0]);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(dataTypeToAddress);
            }
            OperateResult<byte[]> result3 = this.Read(dataTypeToAddress.Content1, pAddress, result.Content, length);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            OperateResult<byte[]> result4 = base.ReadBase(result3.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            return ExtractActualData(result4.Content, true);
        }

        public OperateResult<byte[]> Read(XGT_DataType pDataType, List<XGTAddressData> pAddress, XGT_MemoryType pMemtype, [Optional, DefaultParameterValue(0)] int pDataCount)
        {
            if (pAddress.Count > 0x10)
            {
                return new OperateResult<byte[]>("You cannot read more than 16 pieces.");
            }
            try
            {
                return OperateResult.CreateSuccessResult<byte[]>(this.CreateReadDataFormat(this.Station, XGT_Request_Func.Read, pDataType, pAddress, pMemtype, pDataCount));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("ERROR:" + exception.Message.ToString());
            }
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            List<XGTAddressData> pAddress = new List<XGTAddressData>();
            string[] separator = new string[] { ";", "," };
            string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray)
            {
                XGTAddressData item = new XGTAddressData();
                if (XGKFastEnet.GetDataTypeToAddress(str).Content2)
                {
                    item.Address = str.Substring(1);
                }
                else
                {
                    item.Address = str.Substring(2);
                }
                pAddress.Add(item);
            }
            OperateResult<XGT_MemoryType> result = XGKFastEnet.AnalysisAddress(strArray[0]);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<XGT_DataType, bool> dataTypeToAddress = XGKFastEnet.GetDataTypeToAddress(strArray[0]);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(dataTypeToAddress);
            }
            OperateResult<byte[]> result3 = this.Read(dataTypeToAddress.Content1, pAddress, result.Content, 1);
            OperateResult<byte[]> result4 = base.ReadBase(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return OperateResult.CreateSuccessResult<bool[]>(SoftBasic.ByteToBoolArray(ExtractActualData(result4.Content, true).Content, length));
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 2));
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
            return string.Format("XGkCnet[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            byte[] buffer1 = new byte[] { value ? ((byte) 1) : ((byte) 0) };
            return this.Write(address, buffer1);
        }

        [HslMqttApi("WriteByte", "")]
        public OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[] { value };
            return this.Write(address, buffer1);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            List<XGTAddressData> pAddressList = new List<XGTAddressData>();
            string[] separator = new string[] { ";", "," };
            string[] strArray = address.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray)
            {
                XGTAddressData item = new XGTAddressData();
                if (XGKFastEnet.GetDataTypeToAddress(str).Content2)
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
            OperateResult<XGT_MemoryType> result = XGKFastEnet.AnalysisAddress(address);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<XGT_DataType, bool> dataTypeToAddress = XGKFastEnet.GetDataTypeToAddress(address);
            if (!dataTypeToAddress.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(dataTypeToAddress);
            }
            OperateResult<byte[]> result3 = this.Write(dataTypeToAddress.Content1, pAddressList, result.Content, 0);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            OperateResult<byte[]> result4 = base.ReadBase(result3.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            return ExtractActualData(result4.Content, false);
        }

        public OperateResult<byte[]> Write(XGT_DataType pDataType, List<XGTAddressData> pAddressList, XGT_MemoryType pMemtype, [Optional, DefaultParameterValue(0)] int pDataCount)
        {
            try
            {
                return OperateResult.CreateSuccessResult<byte[]>(this.CreateWriteDataFormat(this.Station, XGT_Request_Func.Write, pDataType, pAddressList, pMemtype, pDataCount));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("ERROR:" + exception.Message.ToString());
            }
        }

        public OperateResult WriteCoil(string address, bool value)
        {
            return this.Write(address, value);
        }

        public byte Station { get; set; }
    }
}

