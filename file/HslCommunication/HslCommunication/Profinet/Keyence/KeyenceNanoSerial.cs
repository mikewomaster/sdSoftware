namespace HslCommunication.Profinet.Keyence
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Reflection;
    using HslCommunication.Serial;
    using System;

    public class KeyenceNanoSerial : SerialDeviceBase
    {
        public KeyenceNanoSerial()
        {
            base.ByteTransform = new RegularByteTransform();
            base.WordLength = 1;
        }

        protected override OperateResult ExtraOnClose()
        {
            OperateResult<byte[]> result = base.ReadBase(KeyenceNanoSerialOverTcp.DisConnectCmd);
            if (!result.IsSuccess)
            {
                return result;
            }
            if ((result.Content.Length > 2) && ((result.Content[0] == 0x43) && (result.Content[1] == 70)))
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult("Check Failed: " + SoftBasic.ByteToHexString(result.Content, ' '));
        }

        protected override OperateResult InitializationOnOpen()
        {
            OperateResult<byte[]> result = base.ReadBase(KeyenceNanoSerialOverTcp.ConnectCmd);
            if (!result.IsSuccess)
            {
                return result;
            }
            if ((result.Content.Length > 2) && ((result.Content[0] == 0x43) && (result.Content[1] == 0x43)))
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult("Check Failed: " + SoftBasic.ByteToHexString(result.Content, ' '));
        }

        [HslMqttApi("ReadByteArray", "")]
        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<byte[]> result = KeyenceNanoSerialOverTcp.BuildReadCommand(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result2);
            }
            OperateResult result3 = KeyenceNanoSerialOverTcp.CheckPlcReadResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            OperateResult<string, int> result4 = KeyenceNanoSerialOverTcp.KvAnalysisAddress(address);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result4);
            }
            return KeyenceNanoSerialOverTcp.ExtractActualData(result4.Content1, result2.Content);
        }

        [HslMqttApi("ReadBoolArray", "")]
        public override OperateResult<bool[]> ReadBool(string address, ushort length)
        {
            OperateResult<byte[]> result = KeyenceNanoSerialOverTcp.BuildReadCommand(address, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result);
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result2);
            }
            OperateResult result3 = KeyenceNanoSerialOverTcp.CheckPlcReadResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result3);
            }
            OperateResult<string, int> result4 = KeyenceNanoSerialOverTcp.KvAnalysisAddress(address);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<bool[]>(result4);
            }
            return KeyenceNanoSerialOverTcp.ExtractActualBoolData(result4.Content1, result2.Content);
        }

        public override string ToString()
        {
            return string.Format("KeyenceNanoSerial[{0}:{1}]", base.PortName, base.BaudRate);
        }

        [HslMqttApi("WriteByteArray", "")]
        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<byte[]> result = KeyenceNanoSerialOverTcp.BuildWriteCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = KeyenceNanoSerialOverTcp.CheckPlcWriteResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }

        [HslMqttApi("WriteBool", "")]
        public override OperateResult Write(string address, bool value)
        {
            OperateResult<byte[]> result = KeyenceNanoSerialOverTcp.BuildWriteCommand(address, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadBase(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = KeyenceNanoSerialOverTcp.CheckPlcWriteResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}

