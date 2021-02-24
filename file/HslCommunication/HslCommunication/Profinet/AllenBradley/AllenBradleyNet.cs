namespace HslCommunication.Profinet.AllenBradley
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    public class AllenBradleyNet : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <CipCommand>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <PortSlot>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint <SessionHandle>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <Slot>k__BackingField;

        public AllenBradleyNet()
        {
            this.<Slot>k__BackingField = 0;
            this.<CipCommand>k__BackingField = 0x6f;
            base.WordLength = 2;
            base.ByteTransform = new RegularByteTransform();
        }

        public AllenBradleyNet(string ipAddress, [Optional, DefaultParameterValue(0xaf12)] int port)
        {
            this.<Slot>k__BackingField = 0;
            this.<CipCommand>k__BackingField = 0x6f;
            base.WordLength = 2;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
        }

        public OperateResult<byte[]> BuildReadCommand(string[] address)
        {
            if (address == null)
            {
                return new OperateResult<byte[]>("address or length is null");
            }
            int[] length = new int[address.Length];
            for (int i = 0; i < address.Length; i++)
            {
                length[i] = 1;
            }
            return this.BuildReadCommand(address, length);
        }

        public virtual OperateResult<byte[]> BuildReadCommand(string[] address, int[] length)
        {
            if ((address == null) || (length == null))
            {
                return new OperateResult<byte[]>("address or length is null");
            }
            if (address.Length != length.Length)
            {
                return new OperateResult<byte[]>("address and length is not same array");
            }
            try
            {
                byte[] buffer1;
                byte slot = this.Slot;
                List<byte[]> list = new List<byte[]>();
                for (int i = 0; i < address.Length; i++)
                {
                    slot = (byte) HslHelper.ExtractParameter(ref address[i], "slot", this.Slot);
                    list.Add(AllenBradleyHelper.PackRequsetRead(address[i], length[i], false));
                }
                byte[][] service = new byte[][] { new byte[4], this.PackCommandService(new byte[] { 1, slot } ?? buffer1, list.ToArray()) };
                return OperateResult.CreateSuccessResult<byte[]>(AllenBradleyHelper.PackCommandSpecificData(service));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("Address Wrong:" + exception.Message);
            }
        }

        public OperateResult<byte[]> BuildWriteCommand(string address, bool data)
        {
            try
            {
                byte[] buffer1;
                byte num = (byte) HslHelper.ExtractParameter(ref address, "slot", this.Slot);
                byte[] buffer = AllenBradleyHelper.PackRequestWrite(address, data);
                byte[][] service = new byte[2][];
                service[0] = new byte[4];
                byte[][] cips = new byte[][] { buffer };
                service[1] = this.PackCommandService(new byte[] { 1, num } ?? buffer1, cips);
                return OperateResult.CreateSuccessResult<byte[]>(AllenBradleyHelper.PackCommandSpecificData(service));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("Address Wrong:" + exception.Message);
            }
        }

        public OperateResult<byte[]> BuildWriteCommand(string address, ushort typeCode, byte[] data, [Optional, DefaultParameterValue(1)] int length)
        {
            try
            {
                byte[] buffer1;
                byte num = (byte) HslHelper.ExtractParameter(ref address, "slot", this.Slot);
                byte[] buffer = AllenBradleyHelper.PackRequestWrite(address, typeCode, data, length, false);
                byte[][] service = new byte[2][];
                service[0] = new byte[4];
                byte[][] cips = new byte[][] { buffer };
                service[1] = this.PackCommandService(new byte[] { 1, num } ?? buffer1, cips);
                return OperateResult.CreateSuccessResult<byte[]>(AllenBradleyHelper.PackCommandSpecificData(service));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("Address Wrong:" + exception.Message);
            }
        }

        private List<AbTagItem> EnumSysStructItemType(byte[] Struct_Item_Type_buff, AbStructHandle structHandle)
        {
            List<AbTagItem> list = new List<AbTagItem>();
            if ((((Struct_Item_Type_buff.Length > 0x29) && (Struct_Item_Type_buff[40] == 0xcc)) && (Struct_Item_Type_buff[0x29] == 0)) && (Struct_Item_Type_buff[0x2a] == 0))
            {
                int num = Struct_Item_Type_buff.Length - 40;
                byte[] destinationArray = new byte[num - 4];
                Array.Copy(Struct_Item_Type_buff, 0x2c, destinationArray, 0, num - 4);
                byte[] buffer2 = new byte[structHandle.MemberCount * 8];
                Array.Copy(destinationArray, 0, buffer2, 0, structHandle.MemberCount * 8);
                byte[] buffer3 = new byte[(destinationArray.Length - buffer2.Length) + 1];
                Array.Copy(destinationArray, buffer2.Length - 1, buffer3, 0, (destinationArray.Length - buffer2.Length) + 1);
                ushort memberCount = structHandle.MemberCount;
                for (int i = 0; i < memberCount; i++)
                {
                    AbTagItem item = new AbTagItem {
                        SymbolType = BitConverter.ToUInt16(buffer2, (8 * i) + 2)
                    };
                    list.Add(item);
                }
                List<int> list2 = new List<int>();
                for (int j = 0; j < buffer3.Length; j++)
                {
                    if (buffer3[j] == 0)
                    {
                        list2.Add(j);
                    }
                }
                list2.Add(buffer3.Length);
                for (int k = 0; k < list2.Count; k++)
                {
                    if (k != 0)
                    {
                        int count = 0;
                        if ((k + 1) < list2.Count)
                        {
                            count = (list2[k + 1] - list2[k]) - 1;
                        }
                        else
                        {
                            count = 0;
                        }
                        if (count > 0)
                        {
                            list[k - 1].Name = Encoding.ASCII.GetString(buffer3, list2[k] + 1, count);
                        }
                    }
                }
            }
            return list;
        }

        private List<AbTagItem> EnumUserStructItemType(byte[] Struct_Item_Type_buff, AbStructHandle structHandle)
        {
            List<AbTagItem> list = new List<AbTagItem>();
            bool flag = false;
            int num = 0;
            if ((((Struct_Item_Type_buff.Length > 0x29) & (Struct_Item_Type_buff[40] == 0xcc)) & (Struct_Item_Type_buff[0x29] == 0)) & (Struct_Item_Type_buff[0x2a] == 0))
            {
                int num2 = Struct_Item_Type_buff.Length - 40;
                byte[] destinationArray = new byte[num2 - 4];
                Array.ConstrainedCopy(Struct_Item_Type_buff, 0x2c, destinationArray, 0, num2 - 4);
                for (int i = 0; i < destinationArray.Length; i++)
                {
                    if ((destinationArray[i] == 0) & !flag)
                    {
                        num = i;
                    }
                    if ((destinationArray[i] == 0x3b) && (destinationArray[i + 1] == 110))
                    {
                        flag = true;
                        int length = (i - num) - 1;
                        byte[] buffer4 = new byte[length];
                        Array.Copy(destinationArray, num + 1, buffer4, 0, length);
                        byte[] buffer2 = new byte[i + 1];
                        Array.Copy(destinationArray, 0, buffer2, 0, i + 1);
                        byte[] buffer3 = new byte[(destinationArray.Length - i) - 1];
                        Array.Copy(destinationArray, i + 1, buffer3, 0, (destinationArray.Length - i) - 1);
                        if (((num + 1) % 8) == 0)
                        {
                            int num5 = ((num + 1) / 8) - 1;
                            for (int j = 0; j <= num5; j++)
                            {
                                AbTagItem item = new AbTagItem {
                                    SymbolType = BitConverter.ToUInt16(buffer2, (8 * j) + 2)
                                };
                                list.Add(item);
                            }
                            List<int> list2 = new List<int>();
                            for (int k = 0; k < buffer3.Length; k++)
                            {
                                if (buffer3[k] == 0)
                                {
                                    list2.Add(k);
                                }
                            }
                            list2.Add(buffer3.Length);
                            for (int m = 0; m < list2.Count; m++)
                            {
                                int count = 0;
                                if ((m + 1) < list2.Count)
                                {
                                    count = (list2[m + 1] - list2[m]) - 1;
                                }
                                else
                                {
                                    count = 0;
                                }
                                if (count > 0)
                                {
                                    list[m].Name = Encoding.ASCII.GetString(buffer3, list2[m] + 1, count);
                                }
                            }
                        }
                        return list;
                    }
                }
            }
            return list;
        }

        protected override OperateResult ExtraOnDisconnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, AllenBradleyHelper.UnRegisterSessionHandle(this.SessionHandle), true, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            return OperateResult.CreateSuccessResult();
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new AllenBradleyMessage();
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, AllenBradleyHelper.RegisterSessionHandle(), true, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = AllenBradleyHelper.CheckResponse(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            this.SessionHandle = base.ByteTransform.TransUInt32(result.Content, 4);
            return OperateResult.CreateSuccessResult();
        }

        protected virtual byte[] PackCommandService(byte[] portSlot, params byte[][] cips)
        {
            return AllenBradleyHelper.PackCommandService(portSlot, cips);
        }

        protected override byte[] PackCommandWithHeader(byte[] command)
        {
            return AllenBradleyHelper.PackRequestHeader(this.CipCommand, this.SessionHandle, command);
        }

        [HslMqttApi("ReadAddress", "")]
        public OperateResult<byte[]> Read(string[] address)
        {
            if (address == null)
            {
                return new OperateResult<byte[]>("address can not be null");
            }
            int[] length = new int[address.Length];
            for (int i = 0; i < length.Length; i++)
            {
                length[i] = 1;
            }
            return this.Read(address, length);
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            if (length > 1)
            {
                return this.ReadSegment(address, 0, length);
            }
            string[] textArray1 = new string[] { address };
            int[] numArray1 = new int[] { length };
            return this.Read(textArray1, numArray1);
        }

        public OperateResult<byte[]> Read(string[] address, int[] length)
        {
            if (((address != null) ? (address.Length > 1) : false) && !Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<byte[]>(StringResources.Language.InsufficientPrivileges);
            }
            OperateResult<byte[], ushort, bool> result = this.ReadWithType(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result.Content1);
        }

        [HslMqttApi("ReadBool", "")]
        public override OperateResult<bool> ReadBool(string address)
        {
            if (address.StartsWith("i="))
            {
                int num;
                address = address.Substring(2);
                address = AllenBradleyHelper.AnalysisArrayIndex(address, out num);
                OperateResult<bool[]> result = this.ReadBoolArray(address + string.Format("[{0}]", num / 0x20));
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool>(result);
                }
                return OperateResult.CreateSuccessResult<bool>(result.Content[num % 0x20]);
            }
            OperateResult<byte[]> result3 = this.Read(address, 1);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool>(result3);
            }
            return OperateResult.CreateSuccessResult<bool>(base.ByteTransform.TransBool(result3.Content, 0));
        }

        [HslMqttApi("ReadBoolArrayAddress", "")]
        public OperateResult<bool[]> ReadBoolArray(string address)
        {
            OperateResult<byte[]> result = this.Read(address, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            return OperateResult.CreateSuccessResult<bool[]>(base.ByteTransform.TransBool(result.Content, 0, result.Content.Length));
        }

        private OperateResult<byte[]> ReadByCips(params byte[][] cips)
        {
            OperateResult<byte[]> result = this.ReadCipFromServer(cips);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[], ushort, bool> result2 = AllenBradleyHelper.ExtractActualData(result.Content, true);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result2.Content1);
        }

        [HslMqttApi("ReadByte", "")]
        public OperateResult<byte> ReadByte(string address)
        {
            return ByteTransformHelper.GetResultFromArray<byte>(this.Read(address, 1));
        }

        public OperateResult<byte[]> ReadCipFromServer(params byte[][] cips)
        {
            byte[] buffer1;
            byte[][] service = new byte[][] { new byte[4], this.PackCommandService(new byte[] { 1, this.Slot } ?? buffer1, cips.ToArray<byte[]>()) };
            byte[] send = AllenBradleyHelper.PackCommandSpecificData(service);
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = AllenBradleyHelper.CheckResponse(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result.Content);
        }

        [HslMqttApi("ReadDoubleArray", "")]
        public override OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<double[]>(this.Read(address, length), m => this.ByteTransform.TransDouble(m, 0, length));
        }

        public OperateResult<byte[]> ReadEipFromServer(params byte[][] eip)
        {
            byte[] send = AllenBradleyHelper.PackCommandSpecificData(eip);
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult result2 = AllenBradleyHelper.CheckResponse(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result.Content);
        }

        [HslMqttApi("ReadFloatArray", "")]
        public override OperateResult<float[]> ReadFloat(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<float[]>(this.Read(address, length), m => this.ByteTransform.TransSingle(m, 0, length));
        }

        [HslMqttApi("ReadInt16Array", "")]
        public override OperateResult<short[]> ReadInt16(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<short[]>(this.Read(address, length), m => this.ByteTransform.TransInt16(m, 0, length));
        }

        [HslMqttApi("ReadInt32Array", "")]
        public override OperateResult<int[]> ReadInt32(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<int[]>(this.Read(address, length), m => this.ByteTransform.TransInt32(m, 0, length));
        }

        [HslMqttApi("ReadInt64Array", "")]
        public override OperateResult<long[]> ReadInt64(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<long[]>(this.Read(address, length), m => this.ByteTransform.TransInt64(m, 0, length));
        }

        [HslMqttApi("ReadSegment", "")]
        public OperateResult<byte[]> ReadSegment(string address, int startIndex, int length)
        {
            OperateResult<byte[]> result3;
            try
            {
                byte[][] bufferArray1;
                bool flag4;
                List<byte> list = new List<byte>();
                goto Label_0089;
            Label_000A:
                bufferArray1 = new byte[][] { AllenBradleyHelper.PackRequestReadSegment(address, startIndex, length) };
                OperateResult<byte[]> result = this.ReadCipFromServer(bufferArray1);
                if (!result.IsSuccess)
                {
                    return result;
                }
                OperateResult<byte[], ushort, bool> result2 = AllenBradleyHelper.ExtractActualData(result.Content, true);
                if (!result2.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result2);
                }
                startIndex += result2.Content1.Length;
                list.AddRange(result2.Content1);
                if (!result2.Content3)
                {
                    goto Label_0091;
                }
            Label_0089:
                flag4 = true;
                goto Label_000A;
            Label_0091:
                result3 = OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
            }
            catch (Exception exception)
            {
                result3 = new OperateResult<byte[]>("Address Wrong:" + exception.Message);
            }
            return result3;
        }

        public OperateResult<string> ReadString(string address)
        {
            return this.ReadString(address, 1, Encoding.ASCII);
        }

        public override OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            OperateResult<byte[]> result = this.Read(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            try
            {
                if (result.Content.Length >= 6)
                {
                    int count = base.ByteTransform.TransInt32(result.Content, 2);
                    return OperateResult.CreateSuccessResult<string>(encoding.GetString(result.Content, 6, count));
                }
                return OperateResult.CreateSuccessResult<string>(encoding.GetString(result.Content));
            }
            catch (Exception exception)
            {
                return new OperateResult<string>(exception.Message + " Source: " + result.Content.ToHexString(' '));
            }
        }

        private OperateResult<AbStructHandle> ReadTagStructHandle(AbTagItem structTag)
        {
            byte[][] cips = new byte[][] { AllenBradleyHelper.GetStructHandleCommand(structTag.SymbolType) };
            OperateResult<byte[]> result = this.ReadByCips(cips);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<AbStructHandle>(result);
            }
            if ((result.Content.Length >= 0x2b) && (BitConverter.ToInt32(result.Content, 40) == 0x83))
            {
                AbStructHandle handle = new AbStructHandle {
                    Count = BitConverter.ToUInt16(result.Content, 0x2c),
                    TemplateObjectDefinitionSize = BitConverter.ToUInt32(result.Content, 50),
                    TemplateStructureSize = BitConverter.ToUInt32(result.Content, 0x3a),
                    MemberCount = BitConverter.ToUInt16(result.Content, 0x42),
                    StructureHandle = BitConverter.ToUInt16(result.Content, 0x48)
                };
                return OperateResult.CreateSuccessResult<AbStructHandle>(handle);
            }
            return new OperateResult<AbStructHandle>(StringResources.Language.UnknownError);
        }

        [HslMqttApi("ReadUInt16Array", "")]
        public override OperateResult<ushort[]> ReadUInt16(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<ushort[]>(this.Read(address, length), m => this.ByteTransform.TransUInt16(m, 0, length));
        }

        [HslMqttApi("ReadUInt32Array", "")]
        public override OperateResult<uint[]> ReadUInt32(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<uint[]>(this.Read(address, length), m => this.ByteTransform.TransUInt32(m, 0, length));
        }

        [HslMqttApi("ReadUInt64Array", "")]
        public override OperateResult<ulong[]> ReadUInt64(string address, ushort length)
        {
            return ByteTransformHelper.GetResultFromBytes<ulong[]>(this.Read(address, length), m => this.ByteTransform.TransUInt64(m, 0, length));
        }

        private OperateResult<byte[], ushort, bool> ReadWithType(string[] address, int[] length)
        {
            OperateResult<byte[]> result = this.BuildReadCommand(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[], ushort, bool>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[], ushort, bool>(result2);
            }
            OperateResult result3 = AllenBradleyHelper.CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[], ushort, bool>(result3);
            }
            return AllenBradleyHelper.ExtractActualData(result2.Content, true);
        }

        [Obsolete("未测试通过")]
        public OperateResult<AbTagItem[]> StructTagEnumerator(AbTagItem structTag)
        {
            OperateResult<AbStructHandle> result = this.ReadTagStructHandle(structTag);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<AbTagItem[]>(result);
            }
            byte[][] cips = new byte[][] { AllenBradleyHelper.GetStructItemNameType(structTag.SymbolType, result.Content) };
            OperateResult<byte[]> result2 = this.ReadCipFromServer(cips);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<AbTagItem[]>(result2);
            }
            if (((result2.Content.Length >= 0x2b) && (result2.Content[40] == 0xcc)) && (result2.Content[0x29] == 0))
            {
                byte[] bytes = BitConverter.GetBytes(structTag.SymbolType);
                bytes[1] = (byte) (bytes[1] & 15);
                if (bytes[1] >= 15)
                {
                    return OperateResult.CreateSuccessResult<AbTagItem[]>(this.EnumSysStructItemType(result2.Content, result.Content).ToArray());
                }
                return OperateResult.CreateSuccessResult<AbTagItem[]>(this.EnumUserStructItemType(result2.Content, result.Content).ToArray());
            }
            return new OperateResult<AbTagItem[]>(StringResources.Language.UnknownError);
        }

        public OperateResult<AbTagItem[]> TagEnumerator()
        {
            List<AbTagItem> list = new List<AbTagItem>();
            ushort startInstance = 0;
            while (true)
            {
                byte[][] cips = new byte[][] { AllenBradleyHelper.GetEnumeratorCommand(startInstance) };
                OperateResult<byte[]> result = this.ReadCipFromServer(cips);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<AbTagItem[]>(result);
                }
                OperateResult<byte[], ushort, bool> result2 = AllenBradleyHelper.ExtractActualData(result.Content, true);
                if (!result2.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<AbTagItem[]>(result2);
                }
                if ((result.Content.Length >= 0x2b) && (BitConverter.ToUInt16(result.Content, 40) == 0xd5))
                {
                    int startIndex = 0x2c;
                    while (startIndex < result.Content.Length)
                    {
                        AbTagItem item = new AbTagItem {
                            InstanceID = BitConverter.ToUInt32(result.Content, startIndex)
                        };
                        startInstance = (ushort) (item.InstanceID + 1);
                        startIndex += 4;
                        ushort count = BitConverter.ToUInt16(result.Content, startIndex);
                        startIndex += 2;
                        item.Name = Encoding.ASCII.GetString(result.Content, startIndex, count);
                        startIndex += count;
                        item.SymbolType = BitConverter.ToUInt16(result.Content, startIndex);
                        startIndex += 2;
                        if (((item.SymbolType & 0x1000) != 0x1000) && !item.Name.StartsWith("__"))
                        {
                            list.Add(item);
                        }
                    }
                    if (!result2.Content3)
                    {
                        return OperateResult.CreateSuccessResult<AbTagItem[]>(list.ToArray());
                    }
                }
                else
                {
                    return new OperateResult<AbTagItem[]>(StringResources.Language.UnknownError);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("AllenBradleyNet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            return new OperateResult(StringResources.Language.NotSupportedFunction + " Please refer to use WriteTag instead ");
        }

        [HslMqttApi("WriteDoubleArray", "")]
        public override OperateResult Write(string address, double[] values)
        {
            return this.WriteTag(address, 0xcb, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteInt16Array", "")]
        public override OperateResult Write(string address, short[] values)
        {
            return this.WriteTag(address, 0xc3, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteInt32Array", "")]
        public override OperateResult Write(string address, int[] values)
        {
            return this.WriteTag(address, 0xc4, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteInt64Array", "")]
        public override OperateResult Write(string address, long[] values)
        {
            return this.WriteTag(address, 0xc5, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteFloatArray", "")]
        public override OperateResult Write(string address, float[] values)
        {
            return this.WriteTag(address, 0xca, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteUInt16Array", "")]
        public override OperateResult Write(string address, ushort[] values)
        {
            return this.WriteTag(address, 0xc7, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteUInt32Array", "")]
        public override OperateResult Write(string address, uint[] values)
        {
            return this.WriteTag(address, 200, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteUInt64Array", "")]
        public override OperateResult Write(string address, ulong[] values)
        {
            return this.WriteTag(address, 0xc9, base.ByteTransform.TransByte(values), values.Length);
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            if (Regex.IsMatch(address, @"\[[0-9]+\]$"))
            {
                OperateResult<byte[]> result = this.BuildWriteCommand(address, value);
                if (!result.IsSuccess)
                {
                    return result;
                }
                OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
                if (!result2.IsSuccess)
                {
                    return result2;
                }
                OperateResult result3 = AllenBradleyHelper.CheckResponse(result2.Content);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result3);
                }
                return AllenBradleyHelper.ExtractActualData(result2.Content, false);
            }
            return this.WriteTag(address, 0xc1, value ? new byte[] { 0xff, 0xff } : new byte[2], 1);
        }

        [HslMqttApi("WriteByte", "")]
        public virtual OperateResult Write(string address, byte value)
        {
            byte[] buffer1 = new byte[2];
            buffer1[0] = value;
            return this.WriteTag(address, 0xc2, buffer1, 1);
        }

        [HslMqttApi("WriteString", "")]
        public override OperateResult Write(string address, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;
            }
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            OperateResult result = base.Write(address + ".LEN", bytes.Length);
            if (!result.IsSuccess)
            {
                return result;
            }
            byte[] buffer2 = SoftBasic.ArrayExpandToLengthEven<byte>(bytes);
            return this.WriteTag(address + ".DATA[0]", 0xc2, buffer2, bytes.Length);
        }

        public virtual OperateResult WriteTag(string address, ushort typeCode, byte[] value, [Optional, DefaultParameterValue(1)] int length)
        {
            OperateResult<byte[]> result = this.BuildWriteCommand(address, typeCode, value, length);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = AllenBradleyHelper.CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return AllenBradleyHelper.ExtractActualData(result2.Content, false);
        }

        public ushort CipCommand { get; set; }

        public byte[] PortSlot { get; set; }

        public uint SessionHandle { get; protected set; }

        public byte Slot { get; set; }
    }
}

