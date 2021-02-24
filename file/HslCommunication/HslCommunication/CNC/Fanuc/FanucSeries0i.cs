namespace HslCommunication.CNC.Fanuc
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class FanucSeries0i : NetworkDoubleBase
    {
        public FanucSeries0i(string ipAddress, [Optional, DefaultParameterValue(0x2001)] int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new ReverseBytesTransform();
        }

        public byte[] BuildReadArray(params byte[][] commands)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(new byte[] { 160, 160, 160, 160, 0, 1, 0x21, 1, 0, 30 }, 0, 10);
            stream.Write(base.ByteTransform.TransByte((ushort) commands.Length), 0, 2);
            for (int i = 0; i < commands.Length; i++)
            {
                stream.Write(commands[i], 0, commands[i].Length);
            }
            byte[] array = stream.ToArray();
            base.ByteTransform.TransByte((ushort) (array.Length - 10)).CopyTo(array, 8);
            return array;
        }

        public byte[] BuildReadProgram(int program)
        {
            return "\r\na0 a0 a0 a0 00 01 15 01 02 04 00 00 00 01 4f 36\r\n30 30 32 2d 4f 36 30 30 32 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n00 00 00 00 00 00 00 00 00 00 00 00 00 00\r\n".ToHexBytes();
        }

        public byte[] BuildReadSingle(ushort code, int a, int b, int c, int d, int e)
        {
            byte[] array = new byte[0x1c];
            array[1] = 0x1c;
            array[3] = 1;
            array[5] = 1;
            base.ByteTransform.TransByte(code).CopyTo(array, 6);
            base.ByteTransform.TransByte(a).CopyTo(array, 8);
            base.ByteTransform.TransByte(b).CopyTo(array, 12);
            base.ByteTransform.TransByte(c).CopyTo(array, 0x10);
            base.ByteTransform.TransByte(d).CopyTo(array, 20);
            base.ByteTransform.TransByte(e).CopyTo(array, 0x18);
            return array;
        }

        public byte[] BuildWriteSingle(ushort code, int a, int b, int c, int d, byte[] data)
        {
            byte[] array = new byte[0x1c + data.Length];
            base.ByteTransform.TransByte((ushort) array.Length).CopyTo(array, 0);
            array[3] = 1;
            array[5] = 1;
            base.ByteTransform.TransByte(code).CopyTo(array, 6);
            base.ByteTransform.TransByte(a).CopyTo(array, 8);
            base.ByteTransform.TransByte(b).CopyTo(array, 12);
            base.ByteTransform.TransByte(c).CopyTo(array, 0x10);
            base.ByteTransform.TransByte(d).CopyTo(array, 20);
            base.ByteTransform.TransByte(data.Length).CopyTo(array, 0x18);
            if (data.Length > 0)
            {
                data.CopyTo(array, 0x1c);
            }
            return array;
        }

        public byte[] BuildWriteSingle(ushort code, int a, int b, int c, int d, double[] data)
        {
            byte[] array = new byte[data.Length * 8];
            for (int i = 0; i < data.Length; i++)
            {
                this.CreateFromFanucDouble(data[i]).CopyTo(array, 0);
            }
            return this.BuildWriteSingle(code, a, b, c, d, array);
        }

        private byte[] CreateFromFanucDouble(double value)
        {
            byte[] array = new byte[8];
            int num = (int) (value * 1000.0);
            base.ByteTransform.TransByte(num).CopyTo(array, 0);
            array[5] = 10;
            array[7] = 3;
            return array;
        }

        public List<byte[]> ExtraContentArray(byte[] content)
        {
            List<byte[]> list = new List<byte[]>();
            int num = base.ByteTransform.TransUInt16(content, 0);
            int index = 2;
            for (int i = 0; i < num; i++)
            {
                ushort num4 = base.ByteTransform.TransUInt16(content, index);
                list.Add(content.SelectMiddle<byte>(index + 2, num4 - 2));
                index += num4;
            }
            return list;
        }

        protected override OperateResult ExtraOnDisconnect(Socket socket)
        {
            return this.ReadFromCoreServer(socket, "a0 a0 a0 a0 00 01 02 01 00 00".ToHexBytes(), true, true);
        }

        private double GetFanucDouble(byte[] content, int index)
        {
            return this.GetFanucDouble(content, index, 1)[0];
        }

        private double[] GetFanucDouble(byte[] content, int index, int length)
        {
            double[] numArray = new double[length];
            for (int i = 0; i < length; i++)
            {
                int num2 = base.ByteTransform.TransInt32(content, index + (8 * i));
                int digits = base.ByteTransform.TransInt16(content, (index + (8 * i)) + 6);
                if (num2 == 0)
                {
                    numArray[i] = 0.0;
                }
                else
                {
                    numArray[i] = Math.Round((double) (num2 * Math.Pow(0.1, (double) digits)), digits);
                }
            }
            return numArray;
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new CNCFanucSeriesMessage();
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, "a0 a0 a0 a0 00 01 01 01 00 02 00 02".ToHexBytes(), true, true);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(socket, "a0 a0 a0 a0 00 01 21 01 00 1e 00 01 00 1c 00 01 00 01 00 18 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00".ToHexBytes(), true, true);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult<int> ReadAlarmStatus()
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(0x1a, 0, 0, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<int>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            return OperateResult.CreateSuccessResult<int>(base.ByteTransform.TransUInt16(list[0], 0x10));
        }

        public OperateResult<DateTime> ReadCurrentDateTime()
        {
            OperateResult<double> result = this.ReadSystemMacroValue(0xbc3);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<DateTime>(result);
            }
            OperateResult<double> result2 = this.ReadSystemMacroValue(0xbc4);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<DateTime>(result2);
            }
            string str = Convert.ToInt32(result.Content).ToString();
            string str2 = Convert.ToInt32(result2.Content).ToString().PadLeft(6, '0');
            return OperateResult.CreateSuccessResult<DateTime>(new DateTime(int.Parse(str.Substring(0, 4)), int.Parse(str.Substring(4, 2)), int.Parse(str.Substring(6)), int.Parse(str2.Substring(0, 2)), int.Parse(str2.Substring(2, 2)), int.Parse(str2.Substring(4))));
        }

        public OperateResult<string> ReadCurrentForegroundDir()
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(0xb0, 1, 0, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            int length = 0;
            for (int i = 14; i < list[0].Length; i++)
            {
                if (list[0][i] == 0)
                {
                    length = i;
                    break;
                }
            }
            if (length == 0)
            {
                length = list[0].Length;
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.ASCII.GetString(list[0], 14, length - 14));
        }

        public OperateResult<int> ReadCurrentProduceCount()
        {
            OperateResult<double> result = this.ReadSystemMacroValue(0xf3d);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<int>(result);
            }
            return OperateResult.CreateSuccessResult<int>(Convert.ToInt32(result.Content));
        }

        public OperateResult<CutterInfo[]> ReadCutterInfos([Optional, DefaultParameterValue(0x18)] int cutterNumber)
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(8, 1, cutterNumber, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<CutterInfo[]>(result);
            }
            byte[][] bufferArray2 = new byte[][] { this.BuildReadSingle(8, 1, cutterNumber, 1, 0, 0) };
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(this.BuildReadArray(bufferArray2));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<CutterInfo[]>(result2);
            }
            byte[][] bufferArray3 = new byte[][] { this.BuildReadSingle(8, 1, cutterNumber, 2, 0, 0) };
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(this.BuildReadArray(bufferArray3));
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<CutterInfo[]>(result3);
            }
            byte[][] bufferArray4 = new byte[][] { this.BuildReadSingle(8, 1, cutterNumber, 3, 0, 0) };
            OperateResult<byte[]> result4 = base.ReadFromCoreServer(this.BuildReadArray(bufferArray4));
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<CutterInfo[]>(result4);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            List<byte[]> list2 = this.ExtraContentArray(result2.Content.RemoveBegin<byte>(10));
            List<byte[]> list3 = this.ExtraContentArray(result3.Content.RemoveBegin<byte>(10));
            List<byte[]> list4 = this.ExtraContentArray(result4.Content.RemoveBegin<byte>(10));
            CutterInfo[] infoArray = new CutterInfo[cutterNumber];
            for (int i = 0; i < infoArray.Length; i++)
            {
                infoArray[i] = new CutterInfo();
                infoArray[i].LengthSharpOffset = this.GetFanucDouble(list[0], 14 + (8 * i));
                infoArray[i].LengthWearOffset = this.GetFanucDouble(list2[0], 14 + (8 * i));
                infoArray[i].RadiusSharpOffset = this.GetFanucDouble(list3[0], 14 + (8 * i));
                infoArray[i].RadiusWearOffset = this.GetFanucDouble(list4[0], 14 + (8 * i));
            }
            return OperateResult.CreateSuccessResult<CutterInfo[]>(infoArray);
        }

        public OperateResult<double[]> ReadDeviceWorkPiecesSize()
        {
            return this.ReadSystemMacroValue(0x259, 20);
        }

        public OperateResult<int> ReadExpectProduceCount()
        {
            OperateResult<double> result = this.ReadSystemMacroValue(0xf3e);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<int>(result);
            }
            return OperateResult.CreateSuccessResult<int>(Convert.ToInt32(result.Content));
        }

        public OperateResult<double[]> ReadFanucAxisLoad()
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(0xa4, 2, 0, 0, 0, 0), this.BuildReadSingle(0x89, 0, 0, 0, 0, 0), this.BuildReadSingle(0x56, 1, 0, 0, 0, 0), this.BuildReadSingle(0xa4, 2, 0, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<double[]>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            int length = base.ByteTransform.TransUInt16(list[0], 14);
            return OperateResult.CreateSuccessResult<double[]>(this.GetFanucDouble(list[2], 14, length));
        }

        [Obsolete]
        private OperateResult<string> ReadProgram(int program)
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadProgram(program));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            Console.WriteLine("等待第二次数据接收。");
            Thread.Sleep(100);
            OperateResult<byte[]> result2 = base.ReadFromCoreServer("a0 a0 a0 a0 00 01 18 04 00 08 00 00 00 00 00 00 00 00".ToHexBytes());
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.ASCII.GetString(result2.Content, 10, result2.Content.Length - 10));
        }

        public OperateResult<int[]> ReadProgramList()
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(6, 1, 0x13, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            byte[][] bufferArray2 = new byte[][] { this.BuildReadSingle(6, 0x1a0b, 0x13, 0, 0, 0) };
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(this.BuildReadArray(bufferArray2));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<int[]>(result);
            }
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<int[]>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            int num = (list[0].Length - 14) / 0x48;
            int[] numArray = new int[num];
            for (int i = 0; i < num; i++)
            {
                numArray[i] = base.ByteTransform.TransInt32(list[0], 14 + (0x48 * i));
            }
            return OperateResult.CreateSuccessResult<int[]>(numArray);
        }

        public OperateResult<double, double> ReadSpindleSpeedAndFeedRate()
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(0xa4, 3, 0, 0, 0, 0), this.BuildReadSingle(0x8a, 1, 0, 0, 0, 0), this.BuildReadSingle(0x88, 3, 0, 0, 0, 0), this.BuildReadSingle(0x88, 4, 0, 0, 0, 0), this.BuildReadSingle(0x24, 0, 0, 0, 0, 0), this.BuildReadSingle(0x25, 0, 0, 0, 0, 0), this.BuildReadSingle(0xa4, 3, 0, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<double, double>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            return OperateResult.CreateSuccessResult<double, double>(this.GetFanucDouble(list[5], 14), this.GetFanucDouble(list[4], 14));
        }

        public OperateResult<SysAllCoors> ReadSysAllCoors()
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(0xa4, 0, 0, 0, 0, 0), this.BuildReadSingle(0x89, -1, 0, 0, 0, 0), this.BuildReadSingle(0x88, 1, 0, 0, 0, 0), this.BuildReadSingle(0x88, 2, 0, 0, 0, 0), this.BuildReadSingle(0xa3, 0, -1, 0, 0, 0), this.BuildReadSingle(0x26, 0, -1, 0, 0, 0), this.BuildReadSingle(0x26, 1, -1, 0, 0, 0), this.BuildReadSingle(0x26, 2, -1, 0, 0, 0), this.BuildReadSingle(0x26, 3, -1, 0, 0, 0), this.BuildReadSingle(0xa4, 0, 0, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<SysAllCoors>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            int length = base.ByteTransform.TransUInt16(list[0], 14);
            SysAllCoors coors = new SysAllCoors {
                Absolute = this.GetFanucDouble(list[5], 14, length),
                Machine = this.GetFanucDouble(list[6], 14, length),
                Relative = this.GetFanucDouble(list[7], 14, length)
            };
            return OperateResult.CreateSuccessResult<SysAllCoors>(coors);
        }

        public OperateResult<SysStatusInfo> ReadSysStatusInfo()
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(0x19, 0, 0, 0, 0, 0), this.BuildReadSingle(0xe1, 0, 0, 0, 0, 0), this.BuildReadSingle(0x98, 0, 0, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<SysStatusInfo>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            SysStatusInfo info = new SysStatusInfo {
                Dummy = base.ByteTransform.TransInt16(list[1], 14),
                TMMode = (list[2].Length >= 0x10) ? base.ByteTransform.TransInt16(list[2], 14) : ((short) 0),
                WorkMode = (CNCWorkMode) base.ByteTransform.TransInt16(list[0], 14),
                RunStatus = (CNCRunStatus) base.ByteTransform.TransInt16(list[0], 0x10),
                Motion = base.ByteTransform.TransInt16(list[0], 0x12),
                MSTB = base.ByteTransform.TransInt16(list[0], 20),
                Emergency = base.ByteTransform.TransInt16(list[0], 0x16),
                Alarm = base.ByteTransform.TransInt16(list[0], 0x18),
                Edit = base.ByteTransform.TransInt16(list[0], 0x1a)
            };
            return OperateResult.CreateSuccessResult<SysStatusInfo>(info);
        }

        public OperateResult<SysAlarm[]> ReadSystemAlarm()
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(0x23, -1, 10, 2, 0x40, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<SysAlarm[]>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            if (base.ByteTransform.TransUInt16(list[0], 12) > 0)
            {
                int num = base.ByteTransform.TransUInt16(list[0], 12) / 80;
                SysAlarm[] alarmArray = new SysAlarm[num];
                for (int i = 0; i < alarmArray.Length; i++)
                {
                    alarmArray[i] = new SysAlarm();
                    alarmArray[i].AlarmId = base.ByteTransform.TransInt32(list[0], 14 + (80 * i));
                    alarmArray[i].Type = base.ByteTransform.TransInt16(list[0], 20 + (80 * i));
                    alarmArray[i].Axis = base.ByteTransform.TransInt16(list[0], 0x18 + (80 * i));
                    ushort count = base.ByteTransform.TransUInt16(list[0], 0x1c + (80 * i));
                    alarmArray[i].Message = Encoding.Default.GetString(list[0], 30 + (80 * i), count);
                }
                return OperateResult.CreateSuccessResult<SysAlarm[]>(alarmArray);
            }
            return OperateResult.CreateSuccessResult<SysAlarm[]>(new SysAlarm[0]);
        }

        public OperateResult<double> ReadSystemMacroValue(int number)
        {
            return ByteTransformHelper.GetResultFromArray<double>(this.ReadSystemMacroValue(number, 1));
        }

        public OperateResult<double[]> ReadSystemMacroValue(int number, int length)
        {
            int[] numArray = SoftBasic.SplitIntegerToArray(length, 5);
            int a = number;
            List<byte> list = new List<byte>();
            for (int i = 0; i < numArray.Length; i++)
            {
                byte[][] commands = new byte[][] { this.BuildReadSingle(0x15, a, (a + numArray[i]) - 1, 0, 0, 0) };
                OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<double[]>(result);
                }
                list.AddRange(this.ExtraContentArray(result.Content.RemoveBegin<byte>(10))[0].RemoveBegin<byte>(14));
                a += numArray[i];
            }
            try
            {
                return OperateResult.CreateSuccessResult<double[]>(this.GetFanucDouble(list.ToArray(), 0, length));
            }
            catch (Exception exception)
            {
                return new OperateResult<double[]>(exception.Message + " Source:" + list.ToArray().ToHexString(' '));
            }
        }

        public OperateResult<string, int> ReadSystemProgramCurrent()
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(0xcf, 0, 0, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string, int>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            int num = base.ByteTransform.TransInt32(list[0], 14);
            return OperateResult.CreateSuccessResult<string, int>(Encoding.Default.GetString(list[0].SelectMiddle<byte>(0x12, 0x24)).TrimEnd(new char[1]), num);
        }

        public OperateResult<long> ReadTimeData(int timeType)
        {
            byte[][] commands = new byte[][] { this.BuildReadSingle(0x120, timeType, 0, 0, 0, 0) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<long>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            int num = base.ByteTransform.TransInt32(list[0], 0x12);
            long num2 = base.ByteTransform.TransInt32(list[0], 14);
            if ((num < 0) || (num > 0xea60))
            {
                num = BitConverter.ToInt32(list[0], 0x12);
                num2 = BitConverter.ToInt32(list[0], 14);
            }
            long num3 = num / 0x3e8;
            return OperateResult.CreateSuccessResult<long>((num2 * 60L) + num3);
        }

        public OperateResult SetDeviceProgsCurr(string programName)
        {
            OperateResult<string> result = this.ReadCurrentForegroundDir();
            if (!result.IsSuccess)
            {
                return result;
            }
            byte[] array = new byte[0x100];
            Encoding.ASCII.GetBytes(result.Content + programName).CopyTo(array, 0);
            byte[][] commands = new byte[][] { this.BuildWriteSingle(0xba, 0, 0, 0, 0, array) };
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            List<byte[]> list = this.ExtraContentArray(result2.Content.RemoveBegin<byte>(10));
            int err = (list[0][10] * 0x100) + list[0][11];
            if (err == 0)
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult(err, StringResources.Language.UnknownError);
        }

        public OperateResult WriteCutterLengthSharpOffset(int cutter, double offset)
        {
            double[] values = new double[] { offset };
            return this.WriteSystemMacroValue(0x2af8 + cutter, values);
        }

        public OperateResult WriteCutterLengthWearOffset(int cutter, double offset)
        {
            double[] values = new double[] { offset };
            return this.WriteSystemMacroValue(0x2710 + cutter, values);
        }

        public OperateResult WriteCutterRadiusSharpOffset(int cutter, double offset)
        {
            double[] values = new double[] { offset };
            return this.WriteSystemMacroValue(0x32c8 + cutter, values);
        }

        public OperateResult WriteCutterRadiusWearOffset(int cutter, double offset)
        {
            double[] values = new double[] { offset };
            return this.WriteSystemMacroValue(0x2ee0 + cutter, values);
        }

        public OperateResult WriteSystemMacroValue(int number, double[] values)
        {
            byte[][] commands = new byte[][] { this.BuildWriteSingle(0x16, number, (number + values.Length) - 1, 0, 0, values) };
            OperateResult<byte[]> result = base.ReadFromCoreServer(this.BuildReadArray(commands));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string, int>(result);
            }
            List<byte[]> list = this.ExtraContentArray(result.Content.RemoveBegin<byte>(10));
            if (base.ByteTransform.TransUInt16(list[0], 6) == 0)
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult(base.ByteTransform.TransUInt16(list[0], 6), "Unknown Error");
        }
    }
}

