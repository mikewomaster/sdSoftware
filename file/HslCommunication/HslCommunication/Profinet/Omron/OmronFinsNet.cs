namespace HslCommunication.Profinet.Omron
{
    using HslCommunication;
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

    public class OmronFinsNet : NetworkDeviceBase
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
        private bool <IsChangeSA1AfterReadFailed>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <RSV>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SA2>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SID>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SNA>k__BackingField;
        private byte computerSA1;
        private readonly byte[] handSingle;

        public OmronFinsNet()
        {
            this.<ICF>k__BackingField = 0x80;
            this.<RSV>k__BackingField = 0;
            this.<GCT>k__BackingField = 2;
            this.<DNA>k__BackingField = 0;
            this.<DA1>k__BackingField = 0x13;
            this.<DA2>k__BackingField = 0;
            this.<SNA>k__BackingField = 0;
            this.computerSA1 = 11;
            this.<SID>k__BackingField = 0;
            this.handSingle = new byte[] { 
                70, 0x49, 0x4e, 0x53, 0, 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1
            };
            base.WordLength = 1;
            base.ByteTransform = new ReverseWordTransform();
            base.ByteTransform.DataFormat = DataFormat.CDAB;
            base.ByteTransform.IsStringReverseByteWord = true;
        }

        public OmronFinsNet(string ipAddress, int port)
        {
            this.<ICF>k__BackingField = 0x80;
            this.<RSV>k__BackingField = 0;
            this.<GCT>k__BackingField = 2;
            this.<DNA>k__BackingField = 0;
            this.<DA1>k__BackingField = 0x13;
            this.<DA2>k__BackingField = 0;
            this.<SNA>k__BackingField = 0;
            this.computerSA1 = 11;
            this.<SID>k__BackingField = 0;
            this.handSingle = new byte[] { 
                70, 0x49, 0x4e, 0x53, 0, 0, 0, 12, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1
            };
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

        protected override void ExtraAfterReadFromCoreServer(OperateResult read)
        {
            base.ExtraAfterReadFromCoreServer(read);
            if (!read.IsSuccess && this.IsChangeSA1AfterReadFailed)
            {
                byte num = this.SA1;
                this.SA1 = (byte) (num + 1);
                if (this.SA1 > 0xfd)
                {
                    this.SA1 = 1;
                }
                if (this.SA1 == this.DA1)
                {
                    num = this.SA1;
                    this.SA1 = (byte) (num + 1);
                }
                if (this.SA1 > 0xfd)
                {
                    this.SA1 = 1;
                }
            }
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new FinsMessage();
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            OperateResult<byte[]> result = this.ReadFromCoreServer(socket, this.handSingle, true, true);
            if (!result.IsSuccess)
            {
                return result;
            }
            int err = BitConverter.ToInt32(new byte[] { result.Content[15], result.Content[14], result.Content[13], result.Content[12] }, 0);
            if (err > 0)
            {
                return new OperateResult(err, OmronFinsNetHelper.GetStatusDescription(err));
            }
            if (result.Content.Length >= 0x18)
            {
                this.DA1 = result.Content[0x17];
            }
            return OperateResult.CreateSuccessResult();
        }

        private byte[] PackCommand(byte[] cmd)
        {
            byte[] destinationArray = new byte[0x1a + cmd.Length];
            Array.Copy(this.handSingle, 0, destinationArray, 0, 4);
            byte[] bytes = BitConverter.GetBytes((int) (destinationArray.Length - 8));
            Array.Reverse(bytes);
            bytes.CopyTo(destinationArray, 4);
            destinationArray[11] = 2;
            destinationArray[0x10] = this.ICF;
            destinationArray[0x11] = this.RSV;
            destinationArray[0x12] = this.GCT;
            destinationArray[0x13] = this.DNA;
            destinationArray[20] = this.DA1;
            destinationArray[0x15] = this.DA2;
            destinationArray[0x16] = this.SNA;
            destinationArray[0x17] = this.SA1;
            destinationArray[0x18] = this.SA2;
            destinationArray[0x19] = this.SID;
            cmd.CopyTo(destinationArray, 0x1a);
            return destinationArray;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = this.BuildReadCommand(address, length, false);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = OmronFinsNetHelper.ResponseValidAnalysis(result2.Content, true);
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
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult<byte[]> result3 = OmronFinsNetHelper.ResponseValidAnalysis(result2.Content, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            return OperateResult.CreateSuccessResult<bool[]>((from m in result3.Content select m > 0).ToArray<bool>());
        }

        public override string ToString()
        {
            return string.Format("OmronFinsNet[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteBoolArray", "")]
        public override OperateResult Write(string address, bool[] values)
        {
            OperateResult<byte[]> result = this.BuildWriteCommand(address, (from m in values select m ? ((IEnumerable<byte>) ((byte) 1)) : ((IEnumerable<byte>) ((byte) 0))).ToArray<byte>(), true);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = OmronFinsNetHelper.ResponseValidAnalysis(result2.Content, false);
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
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = OmronFinsNetHelper.ResponseValidAnalysis(result2.Content, false);
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

        [HslMqttApi(HttpMethod="GET", Description="Get or set the IP address of the remote server. If it is a local test, then it needs to be set to 127.0.0.1")]
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

        public bool IsChangeSA1AfterReadFailed { get; set; }

        public byte RSV { get; private set; }

        public byte SA1
        {
            get
            {
                return this.computerSA1;
            }
            set
            {
                this.computerSA1 = value;
                this.handSingle[0x13] = value;
            }
        }

        public byte SA2 { get; set; }

        public byte SID { get; set; }

        public byte SNA { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly OmronFinsNet.<>c <>9 = new OmronFinsNet.<>c();
            public static Func<byte, bool> <>9__57_0;
            public static Func<bool, byte> <>9__58_0;

            internal bool <ReadBool>b__57_0(byte m)
            {
                return (m > 0);
            }

            internal byte <Write>b__58_0(bool m)
            {
                return (m ? ((byte) 1) : ((byte) 0));
            }
        }
    }
}

