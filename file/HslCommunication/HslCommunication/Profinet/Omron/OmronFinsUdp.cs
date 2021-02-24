namespace HslCommunication.Profinet.Omron
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class OmronFinsUdp : NetworkUdpDeviceBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DA1>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DA2>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <DNA>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <GCT>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <ICF>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <RSV>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SA1>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SA2>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SID>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SNA>k__BackingField;

        public OmronFinsUdp()
        {
            this.<ICF>k__BackingField = 0x80;
            this.<RSV>k__BackingField = 0;
            this.<GCT>k__BackingField = 2;
            this.<DNA>k__BackingField = 0;
            this.<DA1>k__BackingField = 0x13;
            this.<DA2>k__BackingField = 0;
            this.<SNA>k__BackingField = 0;
            this.<SA1>k__BackingField = 13;
            this.<SID>k__BackingField = 0;
            base.WordLength = 1;
            base.ByteTransform = new ReverseWordTransform();
            base.ByteTransform.DataFormat = DataFormat.CDAB;
            base.ByteTransform.IsStringReverseByteWord = true;
        }

        public OmronFinsUdp(string ipAddress, int port)
        {
            this.<ICF>k__BackingField = 0x80;
            this.<RSV>k__BackingField = 0;
            this.<GCT>k__BackingField = 2;
            this.<DNA>k__BackingField = 0;
            this.<DA1>k__BackingField = 0x13;
            this.<DA2>k__BackingField = 0;
            this.<SNA>k__BackingField = 0;
            this.<SA1>k__BackingField = 13;
            this.<SID>k__BackingField = 0;
            base.WordLength = 1;
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new ReverseWordTransform();
            base.ByteTransform.DataFormat = DataFormat.CDAB;
            base.ByteTransform.IsStringReverseByteWord = true;
        }

        public OperateResult<byte[]> BuildReadCommand(string address, ushort length, bool isBit)
        {
            OperateResult<byte[]> result = OmronFinsNetHelper.BuildReadCommand(address, length, isBit);
            if (!result.IsSuccess)
            {
                return result;
            }
            return OperateResult.CreateSuccessResult<byte[]>(this.PackCommand(result.Content));
        }

        public OperateResult<byte[]> BuildWriteCommand(string address, byte[] value, bool isBit)
        {
            OperateResult<byte[]> result = OmronFinsNetHelper.BuildWriteWordCommand(address, value, isBit);
            if (!result.IsSuccess)
            {
                return result;
            }
            return OperateResult.CreateSuccessResult<byte[]>(this.PackCommand(result.Content));
        }

        private byte[] PackCommand(byte[] cmd)
        {
            byte[] array = new byte[10 + cmd.Length];
            array[0] = this.ICF;
            array[1] = this.RSV;
            array[2] = this.GCT;
            array[3] = this.DNA;
            array[4] = this.DA1;
            array[5] = this.DA2;
            array[6] = this.SNA;
            array[7] = this.SA1;
            array[8] = this.SA2;
            array[9] = this.SID;
            cmd.CopyTo(array, 10);
            return array;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = this.BuildReadCommand(address, length, false);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = OmronFinsNetHelper.UdpResponseValidAnalysis(result2.Content, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result3.Content);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> result = this.BuildReadCommand(address, length, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = OmronFinsNetHelper.UdpResponseValidAnalysis(result2.Content, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            return OperateResult.CreateSuccessResult<bool[]>((from m in result3.Content select m != 0).ToArray<bool>());
        }

        public override string ToString()
        {
            return string.Format("OmronFinsUdp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<byte[]> result = this.BuildWriteCommand(address, (from m in values select m ? ((IEnumerable<byte>) ((byte) 1)) : ((IEnumerable<byte>) ((byte) 0))).ToArray<byte>(), true);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = OmronFinsNetHelper.UdpResponseValidAnalysis(result2.Content, false);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = this.BuildWriteCommand(address, value, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = this.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = OmronFinsNetHelper.UdpResponseValidAnalysis(result2.Content, false);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        public byte DA1 { get; set; }

        public byte DA2 { get; set; }

        public byte DNA { get; set; }

        public byte GCT { get; set; }

        public byte ICF { get; set; }

        public override string IpAddress
        {
            get
            {
                return base.IpAddress;
            }
            set
            {
                base.IpAddress = value;
                this.DA1 = Convert.ToByte(base.IpAddress.Substring(base.IpAddress.LastIndexOf(".") + 1));
            }
        }

        public byte RSV { get; private set; }

        public byte SA1 { get; set; }

        public byte SA2 { get; set; }

        public byte SID { get; set; }

        public byte SNA { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OmronFinsUdp.<>c <>9 = new OmronFinsUdp.<>c();
            public static Func<byte, bool> <>9__50_0;
            public static Func<bool, byte> <>9__51_0;

            internal bool <ReadBool>b__50_0(byte m)
            {
                return (m != 0);
            }

            internal byte <Write>b__51_0(bool m)
            {
                return (m ? ((byte) 1) : ((byte) 0));
            }
        }
    }
}

