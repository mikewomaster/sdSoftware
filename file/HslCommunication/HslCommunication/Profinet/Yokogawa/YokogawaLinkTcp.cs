namespace HslCommunication.Profinet.Yokogawa
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
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

    public class YokogawaLinkTcp : NetworkDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <CpuNumber>k__BackingField;

        public YokogawaLinkTcp()
        {
            base.ByteTransform = new ReverseWordTransform();
            base.ByteTransform.DataFormat = DataFormat.CDAB;
            this.CpuNumber = 1;
        }

        public YokogawaLinkTcp(string ipAddress, int port)
        {
            base.ByteTransform = new ReverseWordTransform();
            base.ByteTransform.DataFormat = DataFormat.CDAB;
            this.IpAddress = ipAddress;
            this.Port = port;
            this.CpuNumber = 1;
        }

        public static OperateResult<List<byte[]>> BuildReadCommand(byte cpu, string address, ushort length, bool isBit)
        {
            OperateResult<int[], int[]> result2;
            cpu = (byte) HslHelper.ExtractParameter(ref address, "cpu", cpu);
            OperateResult<YokogawaLinkAddress> result = YokogawaLinkAddress.ParseFrom(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<List<byte[]>>(result);
            }
            if (Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                if (isBit)
                {
                    result2 = HslHelper.SplitReadLength(result.Content.AddressStart, length, 0x100);
                }
                else
                {
                    result2 = HslHelper.SplitReadLength(result.Content.AddressStart, length, 0x40);
                }
            }
            else
            {
                result2 = HslHelper.SplitReadLength(result.Content.AddressStart, length, 0xffff);
            }
            List<byte[]> list = new List<byte[]>();
            for (int i = 0; i < result2.Content1.Length; i++)
            {
                result.Content.AddressStart = result2.Content1[i];
                byte[] array = new byte[12];
                array[0] = isBit ? ((byte) 1) : ((byte) 0x11);
                array[1] = cpu;
                array[2] = 0;
                array[3] = 8;
                result.Content.GetAddressBinaryContent().CopyTo(array, 4);
                array[10] = BitConverter.GetBytes(result2.Content2[i])[1];
                array[11] = BitConverter.GetBytes(result2.Content2[i])[0];
                list.Add(array);
            }
            return OperateResult.CreateSuccessResult<List<byte[]>>(list);
        }

        public static OperateResult<List<byte[]>> BuildReadRandomCommand(byte cpu, string[] address, bool isBit)
        {
            List<string[]> list = SoftBasic.ArraySplitByLength<string>(address, 0x20);
            List<byte[]> list2 = new List<byte[]>();
            foreach (string[] strArray in list)
            {
                byte[] array = new byte[6 + (6 * strArray.Length)];
                array[0] = isBit ? ((byte) 4) : ((byte) 20);
                array[1] = cpu;
                array[2] = BitConverter.GetBytes((int) (array.Length - 4))[1];
                array[3] = BitConverter.GetBytes((int) (array.Length - 4))[0];
                array[4] = BitConverter.GetBytes(strArray.Length)[1];
                array[5] = BitConverter.GetBytes(strArray.Length)[0];
                for (int i = 0; i < strArray.Length; i++)
                {
                    array[1] = (byte) HslHelper.ExtractParameter(ref strArray[i], "cpu", cpu);
                    OperateResult<YokogawaLinkAddress> result = YokogawaLinkAddress.ParseFrom(strArray[i], 1);
                    if (!result.IsSuccess)
                    {
                        return OperateResult.CreateFailedResult<List<byte[]>>(result);
                    }
                    result.Content.GetAddressBinaryContent().CopyTo(array, (int) ((6 * i) + 6));
                }
                list2.Add(array);
            }
            return OperateResult.CreateSuccessResult<List<byte[]>>(list2);
        }

        public static OperateResult<List<byte[]>> BuildReadSpecialModule(byte cpu, string address, ushort length)
        {
            if (address.StartsWith("Special:") || address.StartsWith("special:"))
            {
                address = address.Substring(8);
                cpu = (byte) HslHelper.ExtractParameter(ref address, "cpu", cpu);
                OperateResult<int> result = HslHelper.ExtractParameter(ref address, "unit");
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<List<byte[]>>(result);
                }
                OperateResult<int> result2 = HslHelper.ExtractParameter(ref address, "slot");
                if (!result2.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<List<byte[]>>(result2);
                }
                try
                {
                    return OperateResult.CreateSuccessResult<List<byte[]>>(BuildReadSpecialModule(cpu, (byte) result.Content, (byte) result2.Content, ushort.Parse(address), length));
                }
                catch (Exception exception)
                {
                    return new OperateResult<List<byte[]>>("Address format wrong: " + exception.Message);
                }
            }
            return new OperateResult<List<byte[]>>("Special module address must start with Special:");
        }

        public static List<byte[]> BuildReadSpecialModule(byte cpu, byte moduleUnit, byte moduleSlot, ushort dataPosition, ushort length)
        {
            List<byte[]> list = new List<byte[]>();
            OperateResult<int[], int[]> result = HslHelper.SplitReadLength(dataPosition, length, 0x40);
            for (int i = 0; i < result.Content1.Length; i++)
            {
                byte[] buffer;
                buffer = new byte[] { 0x31, cpu, BitConverter.GetBytes((int) (buffer.Length - 4))[1], BitConverter.GetBytes((int) (buffer.Length - 4))[0], moduleUnit, moduleSlot, BitConverter.GetBytes(result.Content1[i])[1], BitConverter.GetBytes(result.Content1[i])[0], BitConverter.GetBytes(result.Content2[i])[1], BitConverter.GetBytes(result.Content2[i])[0] };
                list.Add(buffer);
            }
            return list;
        }

        public static OperateResult<byte[]> BuildStartCommand(byte cpu)
        {
            byte[] buffer1 = new byte[4];
            buffer1[0] = 0x45;
            buffer1[1] = cpu;
            return OperateResult.CreateSuccessResult<byte[]>(buffer1);
        }

        public static OperateResult<byte[]> BuildStopCommand(byte cpu)
        {
            byte[] buffer1 = new byte[4];
            buffer1[0] = 70;
            buffer1[1] = cpu;
            return OperateResult.CreateSuccessResult<byte[]>(buffer1);
        }

        public static OperateResult<byte[]> BuildWriteBoolCommand(byte cpu, string address, bool[] value)
        {
            cpu = (byte) HslHelper.ExtractParameter(ref address, "cpu", cpu);
            OperateResult<YokogawaLinkAddress> result = YokogawaLinkAddress.ParseFrom(address, 0);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] array = new byte[12 + value.Length];
            array[0] = 2;
            array[1] = cpu;
            array[2] = 0;
            array[3] = (byte) (8 + value.Length);
            result.Content.GetAddressBinaryContent().CopyTo(array, 4);
            array[10] = BitConverter.GetBytes(value.Length)[1];
            array[11] = BitConverter.GetBytes(value.Length)[0];
            for (int i = 0; i < value.Length; i++)
            {
                array[12 + i] = value[i] ? ((byte) 1) : ((byte) 0);
            }
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteRandomBoolCommand(byte cpu, string[] address, bool[] value)
        {
            if (address.Length != value.Length)
            {
                return new OperateResult<byte[]>(StringResources.Language.TwoParametersLengthIsNotSame);
            }
            byte[] array = new byte[(6 + (address.Length * 8)) - 1];
            array[0] = 5;
            array[1] = cpu;
            array[2] = BitConverter.GetBytes((int) (array.Length - 4))[1];
            array[3] = BitConverter.GetBytes((int) (array.Length - 4))[0];
            array[4] = BitConverter.GetBytes(address.Length)[1];
            array[5] = BitConverter.GetBytes(address.Length)[0];
            for (int i = 0; i < address.Length; i++)
            {
                array[1] = (byte) HslHelper.ExtractParameter(ref address[i], "cpu", cpu);
                OperateResult<YokogawaLinkAddress> result = YokogawaLinkAddress.ParseFrom(address[i], 0);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result);
                }
                result.Content.GetAddressBinaryContent().CopyTo(array, (int) (6 + (8 * i)));
                array[12 + (8 * i)] = value[i] ? ((byte) 1) : ((byte) 0);
            }
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteRandomWordCommand(byte cpu, string[] address, byte[] value)
        {
            if ((address.Length * 2) != value.Length)
            {
                return new OperateResult<byte[]>(StringResources.Language.TwoParametersLengthIsNotSame);
            }
            byte[] array = new byte[6 + (address.Length * 8)];
            array[0] = 0x15;
            array[1] = cpu;
            array[2] = BitConverter.GetBytes((int) (array.Length - 4))[1];
            array[3] = BitConverter.GetBytes((int) (array.Length - 4))[0];
            array[4] = BitConverter.GetBytes(address.Length)[1];
            array[5] = BitConverter.GetBytes(address.Length)[0];
            for (int i = 0; i < address.Length; i++)
            {
                array[1] = (byte) HslHelper.ExtractParameter(ref address[i], "cpu", cpu);
                OperateResult<YokogawaLinkAddress> result = YokogawaLinkAddress.ParseFrom(address[i], 0);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result);
                }
                result.Content.GetAddressBinaryContent().CopyTo(array, (int) (6 + (8 * i)));
                array[12 + (8 * i)] = value[i * 2];
                array[13 + (8 * i)] = value[(i * 2) + 1];
            }
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteSpecialModule(byte cpu, string address, byte[] data)
        {
            OperateResult<List<byte[]>> result = BuildReadSpecialModule(cpu, address, 0);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] array = new byte[10 + data.Length];
            array[0] = 50;
            array[1] = result.Content[0][1];
            array[2] = BitConverter.GetBytes((int) (array.Length - 4))[1];
            array[3] = BitConverter.GetBytes((int) (array.Length - 4))[0];
            array[4] = result.Content[0][4];
            array[5] = result.Content[0][5];
            array[6] = result.Content[0][6];
            array[7] = result.Content[0][7];
            array[8] = BitConverter.GetBytes((int) (data.Length / 2))[1];
            array[9] = BitConverter.GetBytes((int) (data.Length / 2))[0];
            data.CopyTo(array, 10);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static byte[] BuildWriteSpecialModule(byte cpu, byte moduleUnit, byte moduleSlot, ushort dataPosition, byte[] data)
        {
            byte[] array = new byte[10 + data.Length];
            array[0] = 50;
            array[1] = cpu;
            array[2] = BitConverter.GetBytes((int) (array.Length - 4))[1];
            array[3] = BitConverter.GetBytes((int) (array.Length - 4))[0];
            array[4] = moduleUnit;
            array[5] = moduleSlot;
            array[6] = BitConverter.GetBytes(dataPosition)[1];
            array[7] = BitConverter.GetBytes(dataPosition)[0];
            array[8] = BitConverter.GetBytes((int) (data.Length / 2))[1];
            array[9] = BitConverter.GetBytes((int) (data.Length / 2))[0];
            data.CopyTo(array, 10);
            return array;
        }

        public static OperateResult<byte[]> BuildWriteWordCommand(byte cpu, string address, byte[] value)
        {
            cpu = (byte) HslHelper.ExtractParameter(ref address, "cpu", cpu);
            OperateResult<YokogawaLinkAddress> result = YokogawaLinkAddress.ParseFrom(address, 0);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[] array = new byte[12 + value.Length];
            array[0] = 0x12;
            array[1] = cpu;
            array[2] = 0;
            array[3] = (byte) (8 + value.Length);
            result.Content.GetAddressBinaryContent().CopyTo(array, 4);
            array[10] = BitConverter.GetBytes((int) (value.Length / 2))[1];
            array[11] = BitConverter.GetBytes((int) (value.Length / 2))[0];
            value.CopyTo(array, 12);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> CheckContent(byte[] content)
        {
            if (content[1] > 0)
            {
                return new OperateResult<byte[]>(YokogawaLinkHelper.GetErrorMsg(content[1]));
            }
            if (content.Length > 4)
            {
                return OperateResult.CreateSuccessResult<byte[]>(content.RemoveBegin<byte>(4));
            }
            return OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new YokogawaLinkBinaryMessage();
        }

        [HslMqttApi(Description="Reset the connection which is currently open is forced to close")]
        public OperateResult ModuleReset()
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult(StringResources.Language.InsufficientPrivileges);
            }
            byte[] send = new byte[4];
            send[0] = 0x61;
            send[1] = this.CpuNumber;
            OperateResult<byte[]> result = base.ReadFromCoreServer(send, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("ReadByteArray", "Supports X,Y,I,E,M,T,C,L,D,B,F,R,V,Z,W,TN,CN, for example: D100; or cpu=2;D100 or Special:unit=0;slot=1;100")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<List<byte[]>> result;
            if (address.StartsWith("Special:") || address.StartsWith("special:"))
            {
                if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
                {
                    return new OperateResult<byte[]>(StringResources.Language.InsufficientPrivileges);
                }
                result = BuildReadSpecialModule(this.CpuNumber, address, length);
            }
            else
            {
                result = BuildReadCommand(this.CpuNumber, address, length, false);
            }
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte>();
            for (int i = 0; i < result.Content.Count; i++)
            {
                OperateResult<byte[]> result3 = base.ReadFromCoreServer(result.Content[i]);
                if (!result3.IsSuccess)
                {
                    return result3;
                }
                OperateResult<byte[]> result4 = CheckContent(result3.Content);
                list.AddRange(result4.Content);
            }
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        [HslMqttApi("ReadBoolArray", "Read coil address supports X, Y, I, E, M, T, C, L, for example: Y100; or cpu=2;Y100")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<List<byte[]>> result = BuildReadCommand(this.CpuNumber, address, length, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            List<byte> list = new List<byte>();
            for (int i = 0; i < result.Content.Count; i++)
            {
                OperateResult<byte[]> result3 = base.ReadFromCoreServer(result.Content[i]);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result3);
                }
                OperateResult<byte[]> result4 = CheckContent(result3.Content);
                if (!result4.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result4);
                }
                list.AddRange(result4.Content);
            }
            return OperateResult.CreateSuccessResult<bool[]>((from m in list.ToArray() select m > 0).ToArray<bool>());
        }

        [HslMqttApi(Description="Read current PLC time information, including year, month, day, hour, minute, and second")]
        public OperateResult<DateTime> ReadDateTime()
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<DateTime>(StringResources.Language.InsufficientPrivileges);
            }
            byte[] send = new byte[4];
            send[0] = 0x63;
            send[1] = this.CpuNumber;
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<DateTime>(result);
            }
            OperateResult<byte[]> result2 = CheckContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<DateTime>(result2);
            }
            return OperateResult.CreateSuccessResult<DateTime>(new DateTime(0x7d0 + base.ByteTransform.TransUInt16(result2.Content, 0), base.ByteTransform.TransUInt16(result2.Content, 2), base.ByteTransform.TransUInt16(result2.Content, 4), base.ByteTransform.TransUInt16(result2.Content, 6), base.ByteTransform.TransUInt16(result2.Content, 8), base.ByteTransform.TransUInt16(result2.Content, 10)));
        }

        [HslMqttApi(Description="Read the program status. return code 1:RUN; 2:Stop; 3:Debug; 255:ROM writer")]
        public OperateResult<int> ReadProgramStatus()
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<int>(StringResources.Language.InsufficientPrivileges);
            }
            byte[] send = new byte[] { 0x62, 0, 0, 2, 0, 1 };
            send[1] = this.CpuNumber;
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<int>(result);
            }
            OperateResult<byte[]> result2 = CheckContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<int>(result2);
            }
            return OperateResult.CreateSuccessResult<int>(result2.Content[1]);
        }

        [HslMqttApi(Description="Read random register, supports D,B,F,R,V,Z,W,TN,CN，example: D100")]
        public OperateResult<byte[]> ReadRandom(string[] address)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<byte[]>(StringResources.Language.InsufficientPrivileges);
            }
            OperateResult<List<byte[]>> result = BuildReadRandomCommand(this.CpuNumber, address, false);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            List<byte> list = new List<byte>();
            foreach (byte[] buffer in result.Content)
            {
                OperateResult<byte[]> result3 = base.ReadFromCoreServer(buffer);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result3);
                }
                OperateResult<byte[]> result4 = CheckContent(result3.Content);
                if (!result4.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result4);
                }
                list.AddRange(result4.Content);
            }
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        [HslMqttApi(Description="Read random relay, supports X, Y, I, E, M, T, C, L, for example: Y100;")]
        public OperateResult<bool[]> ReadRandomBool(string[] address)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<bool[]>(StringResources.Language.InsufficientPrivileges);
            }
            OperateResult<List<byte[]>> result = BuildReadRandomCommand(this.CpuNumber, address, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            List<bool> list = new List<bool>();
            foreach (byte[] buffer in result.Content)
            {
                OperateResult<byte[]> result3 = base.ReadFromCoreServer(buffer);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result3);
                }
                OperateResult<byte[]> result4 = CheckContent(result3.Content);
                if (!result4.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<bool[]>(result4);
                }
                list.AddRange(from m in result4.Content select m > 0);
            }
            return OperateResult.CreateSuccessResult<bool[]>(list.ToArray());
        }

        [HslMqttApi(Description="Read random register, and get short array, supports D, B, F, R, V, Z, W, TN, CN，example: D100")]
        public OperateResult<short[]> ReadRandomInt16(string[] address)
        {
            return this.ReadRandom(address).Then<short[]>(m => OperateResult.CreateSuccessResult<short[]>(this.ByteTransform.TransInt16(m, 0, address.Length)));
        }

        [HslMqttApi(Description="Read random register, and get ushort array, supports D, B, F, R, V, Z, W, TN, CN，example: D100")]
        public OperateResult<ushort[]> ReadRandomUInt16(string[] address)
        {
            return this.ReadRandom(address).Then<ushort[]>(m => OperateResult.CreateSuccessResult<ushort[]>(this.ByteTransform.TransUInt16(m, 0, address.Length)));
        }

        [HslMqttApi(Description="Read the data information of a special module, you need to specify the module unit number, module slot number, data address, and length information.")]
        public OperateResult<byte[]> ReadSpecialModule(byte moduleUnit, byte moduleSlot, ushort dataPosition, ushort length)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<byte[]>(StringResources.Language.InsufficientPrivileges);
            }
            List<byte> list = new List<byte>();
            List<byte[]> list2 = BuildReadSpecialModule(this.CpuNumber, moduleUnit, moduleSlot, dataPosition, length);
            for (int i = 0; i < list2.Count; i++)
            {
                OperateResult<byte[]> result = base.ReadFromCoreServer(list2[i]);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result);
                }
                OperateResult<byte[]> result3 = CheckContent(result.Content);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<byte[]>(result3);
                }
                list.AddRange(result3.Content);
            }
            return OperateResult.CreateSuccessResult<byte[]>(list.ToArray());
        }

        [HslMqttApi(Description="Read current PLC system status, system ID, CPU type, program size information")]
        public OperateResult<YokogawaSystemInfo> ReadSystemInfo()
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<YokogawaSystemInfo>(StringResources.Language.InsufficientPrivileges);
            }
            byte[] send = new byte[] { 0x62, 0, 0, 2, 0, 2 };
            send[1] = this.CpuNumber;
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<YokogawaSystemInfo>(result);
            }
            OperateResult<byte[]> result2 = CheckContent(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<YokogawaSystemInfo>(result2);
            }
            return YokogawaSystemInfo.Prase(result2.Content);
        }

        [HslMqttApi(Description="Starts executing a program if it is not being executed")]
        public OperateResult Start()
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult(StringResources.Language.InsufficientPrivileges);
            }
            OperateResult<byte[]> result = BuildStartCommand(this.CpuNumber);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = CheckContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi(Description="Stops the executing program.")]
        public OperateResult Stop()
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult(StringResources.Language.InsufficientPrivileges);
            }
            OperateResult<byte[]> result = BuildStopCommand(this.CpuNumber);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = CheckContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        public override string ToString()
        {
            return string.Format("YokogawaLinkTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "The write coil address supports Y, I, E, M, T, C, L, for example: Y100; or cpu=2;Y100")]
        public override OperateResult Write(string address, bool[] value)
        {
            OperateResult<byte[]> result = BuildWriteBoolCommand(this.CpuNumber, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return CheckContent(result2.Content);
        }

        [HslMqttApi("WriteByteArray", "Supports Y,I,E,M,T,C,L,D,B,F,R,V,Z,W,TN,CN, for example: D100; or cpu=2;D100 or Special:unit=0;slot=1;100")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result;
            if (address.StartsWith("Special:") || address.StartsWith("special:"))
            {
                if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
                {
                    return new OperateResult(StringResources.Language.InsufficientPrivileges);
                }
                result = BuildWriteSpecialModule(this.CpuNumber, address, value);
            }
            else
            {
                result = BuildWriteWordCommand(this.CpuNumber, address, value);
            }
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return CheckContent(result2.Content);
        }

        [HslMqttApi(ApiTopic="WriteRandom", Description="Randomly write the byte array information, the main need to pass in the byte array address information")]
        public OperateResult WriteRandom(string[] address, byte[] value)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<bool[]>(StringResources.Language.InsufficientPrivileges);
            }
            if ((address.Length * 2) != value.Length)
            {
                return new OperateResult(StringResources.Language.TwoParametersLengthIsNotSame);
            }
            OperateResult<byte[]> result = BuildWriteRandomWordCommand(this.CpuNumber, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = CheckContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi(ApiTopic="WriteRandomInt16", Description="Randomly write the short array information, the main need to pass in the short array address information")]
        public OperateResult WriteRandom(string[] address, short[] value)
        {
            return this.WriteRandom(address, base.ByteTransform.TransByte(value));
        }

        [HslMqttApi(ApiTopic="WriteRandomUInt16", Description="Randomly write the ushort array information, the main need to pass in the ushort array address information")]
        public OperateResult WriteRandom(string[] address, ushort[] value)
        {
            return this.WriteRandom(address, base.ByteTransform.TransByte(value));
        }

        [HslMqttApi(Description="Write random relay, supports Y, I, E, M, T, C, L, for example: Y100;")]
        public OperateResult WriteRandomBool(string[] address, bool[] value)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<bool[]>(StringResources.Language.InsufficientPrivileges);
            }
            if (address.Length != value.Length)
            {
                return new OperateResult(StringResources.Language.TwoParametersLengthIsNotSame);
            }
            OperateResult<byte[]> result = BuildWriteRandomBoolCommand(this.CpuNumber, address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = CheckContent(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        public byte CpuNumber { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly YokogawaLinkTcp.<>c <>9 = new YokogawaLinkTcp.<>c();
            public static Func<byte, bool> <>9__11_0;
            public static Func<byte, bool> <>9__9_0;

            internal bool <ReadBool>b__9_0(byte m)
            {
                return (m > 0);
            }

            internal bool <ReadRandomBool>b__11_0(byte m)
            {
                return (m > 0);
            }
        }
    }
}

