namespace HslCommunication.Profinet.Omron
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class OmronHostLinkCModeOverTcp : NetworkDeviceSoloBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <UnitNumber>k__BackingField;

        public OmronHostLinkCModeOverTcp()
        {
            base.ByteTransform = new ReverseWordTransform();
            base.WordLength = 1;
            base.ByteTransform.DataFormat = DataFormat.CDAB;
            base.ByteTransform.IsStringReverseByteWord = true;
        }

        public OmronHostLinkCModeOverTcp(string ipAddress, int port)
        {
            base.ByteTransform = new ReverseWordTransform();
            base.WordLength = 1;
            base.ByteTransform.DataFormat = DataFormat.CDAB;
            base.ByteTransform.IsStringReverseByteWord = true;
            this.IpAddress = ipAddress;
            this.Port = port;
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            byte unitNumber = (byte) HslHelper.ExtractParameter(ref address, "s", this.UnitNumber);
            OperateResult<byte[]> result = OmronHostLinkCMode.BuildReadCommand(address, length, false);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(OmronHostLinkCMode.PackCommand(result.Content, unitNumber));
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult<byte[]> result3 = OmronHostLinkCMode.ResponseValidAnalysis(result2.Content, true);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result3.Content);
        }

        [HslMqttApi]
        public OperateResult<string> ReadPlcModel()
        {
            OperateResult<byte[]> result = base.ReadFromCoreServer(OmronHostLinkCMode.PackCommand(Encoding.ASCII.GetBytes("MM"), this.UnitNumber));
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            int err = Convert.ToInt32(Encoding.ASCII.GetString(result.Content, 5, 2), 0x10);
            if (err > 0)
            {
                return new OperateResult<string>(err, "Unknown Error");
            }
            return OmronHostLinkCMode.GetModelText(Encoding.ASCII.GetString(result.Content, 7, 2));
        }

        public override string ToString()
        {
            return string.Format("OmronHostLinkCModeOverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            byte unitNumber = (byte) HslHelper.ExtractParameter(ref address, "s", this.UnitNumber);
            OperateResult<byte[]> result = OmronHostLinkCMode.BuildWriteWordCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(OmronHostLinkCMode.PackCommand(result.Content, unitNumber));
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = OmronHostLinkCMode.ResponseValidAnalysis(result2.Content, false);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        public byte UnitNumber { get; set; }
    }
}

