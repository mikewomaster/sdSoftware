namespace HslCommunication.Robot.EFORT
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using Newtonsoft.Json;
    using System;
    using System.Text;

    public class ER7BC10Previous : NetworkDoubleBase, IRobotNet
    {
        private SoftIncrementCount softIncrementCount;

        public ER7BC10Previous(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ByteTransform = new RegularByteTransform();
            this.softIncrementCount = new SoftIncrementCount(0xffffL, 0L, 1);
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new EFORTMessagePrevious();
        }

        public byte[] GetReadCommand()
        {
            byte[] array = new byte[0x24];
            Encoding.ASCII.GetBytes("MessageHead").CopyTo(array, 0);
            BitConverter.GetBytes((ushort) array.Length).CopyTo(array, 15);
            BitConverter.GetBytes((ushort) 0x3e9).CopyTo(array, 0x11);
            BitConverter.GetBytes((ushort) this.softIncrementCount.GetCurrentValue()).CopyTo(array, 0x13);
            Encoding.ASCII.GetBytes("MessageTail").CopyTo(array, 0x15);
            return array;
        }

        [HslMqttApi(ApiTopic="ReadRobotByte", Description="Read the robot's original byte data information according to the address")]
        public OperateResult<byte[]> Read(string address)
        {
            return base.ReadFromCoreServer(this.GetReadCommand());
        }

        [HslMqttApi(Description="Read the details of the robot")]
        public OperateResult<EfortData> ReadEfortData()
        {
            OperateResult<byte[]> result = this.Read("");
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<EfortData>(result);
            }
            return EfortData.PraseFromPrevious(result.Content);
        }

        [HslMqttApi(ApiTopic="ReadRobotString", Description="Read the string data information of the robot based on the address")]
        public OperateResult<string> ReadString(string address)
        {
            OperateResult<EfortData> result = this.ReadEfortData();
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            return OperateResult.CreateSuccessResult<string>(JsonConvert.SerializeObject(result.Content, Formatting.Indented));
        }

        public override string ToString()
        {
            return string.Format("ER7BC10 Pre Robot[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi(ApiTopic="WriteRobotByte", Description="This robot does not support this method operation, will always return failed, invalid operation")]
        public OperateResult Write(string address, byte[] value)
        {
            return new OperateResult(StringResources.Language.NotSupportedFunction);
        }

        [HslMqttApi(ApiTopic="WriteRobotString", Description="This robot does not support this method operation, will always return failed, invalid operation")]
        public OperateResult Write(string address, string value)
        {
            return new OperateResult(StringResources.Language.NotSupportedFunction);
        }
    }
}

