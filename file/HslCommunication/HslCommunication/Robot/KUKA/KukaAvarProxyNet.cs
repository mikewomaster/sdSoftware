namespace HslCommunication.Robot.KUKA
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class KukaAvarProxyNet : NetworkDoubleBase, IRobotNet
    {
        private SoftIncrementCount softIncrementCount;

        public KukaAvarProxyNet()
        {
            this.softIncrementCount = new SoftIncrementCount(0xffffL, 0L, 1);
            base.ByteTransform = new ReverseWordTransform();
        }

        public KukaAvarProxyNet(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            this.softIncrementCount = new SoftIncrementCount(0xffffL, 0L, 1);
            base.ByteTransform = new ReverseWordTransform();
        }

        private byte[] BuildCommands(byte function, string[] commands)
        {
            List<byte> list = new List<byte> {
                function
            };
            for (int i = 0; i < commands.Length; i++)
            {
                byte[] bytes = Encoding.Default.GetBytes(commands[i]);
                list.AddRange(base.ByteTransform.TransByte((ushort) bytes.Length));
                list.AddRange(bytes);
            }
            return list.ToArray();
        }

        private byte[] BuildReadValueCommand(string address)
        {
            string[] commands = new string[] { address };
            return this.BuildCommands(0, commands);
        }

        private byte[] BuildWriteValueCommand(string address, string value)
        {
            string[] commands = new string[] { address, value };
            return this.BuildCommands(1, commands);
        }

        private OperateResult<byte[]> ExtractActualData(byte[] response)
        {
            try
            {
                if (response[response.Length - 1] != 1)
                {
                    return new OperateResult<byte[]>(response[response.Length - 1], "Wrong: " + SoftBasic.ByteToHexString(response, ' '));
                }
                int length = (response[5] * 0x100) + response[6];
                byte[] destinationArray = new byte[length];
                Array.Copy(response, 7, destinationArray, 0, length);
                return OperateResult.CreateSuccessResult<byte[]>(destinationArray);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("Wrong:" + exception.Message + " Code:" + SoftBasic.ByteToHexString(response, ' '));
            }
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new KukaVarProxyMessage();
        }

        private byte[] PackCommand(byte[] commandCore)
        {
            byte[] array = new byte[commandCore.Length + 4];
            base.ByteTransform.TransByte((ushort) this.softIncrementCount.GetCurrentValue()).CopyTo(array, 0);
            base.ByteTransform.TransByte((ushort) commandCore.Length).CopyTo(array, 2);
            commandCore.CopyTo(array, 4);
            return array;
        }

        [HslMqttApi(ApiTopic="ReadRobotByte", Description="Read the data content of the Kuka robot according to the input variable name")]
        public OperateResult<byte[]> Read(string address)
        {
            return ByteTransformHelper.GetResultFromOther<byte[], byte[]>(base.ReadFromCoreServer(this.PackCommand(this.BuildReadValueCommand(address))), new Func<byte[], OperateResult<byte[]>>(this.ExtractActualData));
        }

        [HslMqttApi(ApiTopic="ReadRobotString", Description="Read all the data information of the Kuka robot, return the string information, decode by ANSI, need to specify the variable name")]
        public OperateResult<string> ReadString(string address)
        {
            Encoding encoding1 = Encoding.Default;
            return ByteTransformHelper.GetSuccessResultFromOther<string, byte[]>(this.Read(address), new Func<byte[], string>(encoding1.GetString));
        }

        public override string ToString()
        {
            return string.Format("KukaAvarProxyNet Robot[{0}:{1}]", this.IpAddress, this.Port);
        }

        [HslMqttApi(ApiTopic="WriteRobotByte", Description="Write the original data content according to the variable name of the Kuka robot")]
        public OperateResult Write(string address, byte[] value)
        {
            return this.Write(address, Encoding.Default.GetString(value));
        }

        [HslMqttApi(ApiTopic="WriteRobotString", Description="Writes ansi-encoded string data information based on the variable name of the Kuka robot")]
        public OperateResult Write(string address, string value)
        {
            return ByteTransformHelper.GetResultFromOther<byte[], byte[]>(base.ReadFromCoreServer(this.PackCommand(this.BuildWriteValueCommand(address, value))), new Func<byte[], OperateResult<byte[]>>(this.ExtractActualData));
        }
    }
}

