﻿namespace HslCommunication.Profinet.Omron
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class OmronHostLink : SerialDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DA2>k__BackingField = 0;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <ICF>k__BackingField = 0;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <ResponseWaitTime>k__BackingField = 0x30;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SA2>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SID>k__BackingField = 0;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <UnitNumber>k__BackingField;

        public OmronHostLink()
        {
            base.ByteTransform = new ReverseWordTransform();
            base.WordLength = 1;
            base.ByteTransform.DataFormat = DataFormat.CDAB;
            base.ByteTransform.IsStringReverseByteWord = true;
        }

        private byte[] PackCommand(byte station, byte[] cmd)
        {
            cmd = SoftBasic.BytesToAsciiBytes(cmd);
            byte[] array = new byte[0x12 + cmd.Length];
            array[0] = 0x40;
            array[1] = SoftBasic.BuildAsciiBytesFrom(station)[0];
            array[2] = SoftBasic.BuildAsciiBytesFrom(station)[1];
            array[3] = 70;
            array[4] = 0x41;
            array[5] = this.ResponseWaitTime;
            array[6] = SoftBasic.BuildAsciiBytesFrom(this.ICF)[0];
            array[7] = SoftBasic.BuildAsciiBytesFrom(this.ICF)[1];
            array[8] = SoftBasic.BuildAsciiBytesFrom(this.DA2)[0];
            array[9] = SoftBasic.BuildAsciiBytesFrom(this.DA2)[1];
            array[10] = SoftBasic.BuildAsciiBytesFrom(this.SA2)[0];
            array[11] = SoftBasic.BuildAsciiBytesFrom(this.SA2)[1];
            array[12] = SoftBasic.BuildAsciiBytesFrom(this.SID)[0];
            array[13] = SoftBasic.BuildAsciiBytesFrom(this.SID)[1];
            array[array.Length - 2] = 0x2a;
            array[array.Length - 1] = 13;
            cmd.CopyTo(array, 14);
            int num = array[0];
            for (int i = 1; i < (array.Length - 4); i++)
            {
                num ^= array[i];
            }
            array[array.Length - 4] = SoftBasic.BuildAsciiBytesFrom((byte) num)[0];
            array[array.Length - 3] = SoftBasic.BuildAsciiBytesFrom((byte) num)[1];
            return array;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.UnitNumber);
            OperateResult<byte[]> result = OmronFinsNetHelper.BuildReadCommand(address, length, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(this.PackCommand(station, result.Content));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = OmronHostLinkOverTcp.ResponseValidAnalysis(result2.Content, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result3.Content);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.UnitNumber);
            OperateResult<byte[]> result = OmronFinsNetHelper.BuildReadCommand(address, length, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(this.PackCommand(station, result.Content));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = OmronHostLinkOverTcp.ResponseValidAnalysis(result2.Content, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            return OperateResult.CreateSuccessResult<bool[]>((from m in result3.Content select m != 0).ToArray<bool>());
        }

        public override string ToString()
        {
            return string.Format("OmronHostLink[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.UnitNumber);
            OperateResult<byte[]> result = OmronFinsNetHelper.BuildWriteWordCommand(address, (from m in values select m ? ((IEnumerable<byte>) ((byte) 1)) : ((IEnumerable<byte>) ((byte) 0))).ToArray<byte>(), true);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(this.PackCommand(station, result.Content));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = OmronHostLinkOverTcp.ResponseValidAnalysis(result2.Content, false);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte station = (byte) HslHelper.ExtractParameter(ref address, "s", this.UnitNumber);
            OperateResult<byte[]> result = OmronFinsNetHelper.BuildWriteWordCommand(address, value, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(this.PackCommand(station, result.Content));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = OmronHostLinkOverTcp.ResponseValidAnalysis(result2.Content, false);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        public byte DA2 { get; set; }

        public byte ICF { get; set; }

        public byte ResponseWaitTime { get; set; }

        public byte SA2 { get; set; }

        public byte SID { get; set; }

        public byte UnitNumber { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OmronHostLink.<>c <>9 = new OmronHostLink.<>c();
            public static Func<byte, bool> <>9__27_0;
            public static Func<bool, byte> <>9__28_0;

            internal bool <ReadBool>b__27_0(byte m)
            {
                return (m != 0);
            }

            internal byte <Write>b__28_0(bool m)
            {
                return (m ? ((byte) 1) : ((byte) 0));
            }
        }
    }
}

