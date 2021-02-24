namespace HslCommunication.MQTT
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MqttHelper
    {
        public static OperateResult<byte[]> BuildConnectMqttCommand(MqttConnectionOptions connectionOptions, [Optional, DefaultParameterValue("MQTT")] string protocol)
        {
            List<byte> list = new List<byte>();
            byte[] collection = new byte[2];
            collection[1] = 4;
            list.AddRange(collection);
            list.AddRange(Encoding.ASCII.GetBytes(protocol));
            list.Add(4);
            byte item = 0;
            if (connectionOptions.Credentials > null)
            {
                item = (byte) (item | 0x80);
                item = (byte) (item | 0x40);
            }
            if (connectionOptions.CleanSession)
            {
                item = (byte) (item | 2);
            }
            list.Add(item);
            if (connectionOptions.KeepAlivePeriod.TotalSeconds < 1.0)
            {
                connectionOptions.KeepAlivePeriod = TimeSpan.FromSeconds(1.0);
            }
            byte[] bytes = BitConverter.GetBytes((int) connectionOptions.KeepAlivePeriod.TotalSeconds);
            list.Add(bytes[1]);
            list.Add(bytes[0]);
            List<byte> list2 = new List<byte>();
            list2.AddRange(BuildSegCommandByString(connectionOptions.ClientId));
            if (connectionOptions.Credentials > null)
            {
                list2.AddRange(BuildSegCommandByString(connectionOptions.Credentials.UserName));
                list2.AddRange(BuildSegCommandByString(connectionOptions.Credentials.Password));
            }
            return BuildMqttCommand(1, 0, list.ToArray(), list2.ToArray());
        }

        public static byte[] BuildIntBytes(int data)
        {
            return new byte[] { BitConverter.GetBytes(data)[1], BitConverter.GetBytes(data)[0] };
        }

        public static OperateResult<byte[]> BuildMqttCommand(byte head, byte[] variableHeader, byte[] payLoad)
        {
            if (variableHeader == null)
            {
                variableHeader = new byte[0];
            }
            if (payLoad == null)
            {
                payLoad = new byte[0];
            }
            OperateResult<byte[]> result = CalculateLengthToMqttLength(variableHeader.Length + payLoad.Length);
            if (!result.IsSuccess)
            {
                return result;
            }
            MemoryStream stream = new MemoryStream();
            stream.WriteByte(head);
            stream.Write(result.Content, 0, result.Content.Length);
            if (variableHeader.Length > 0)
            {
                stream.Write(variableHeader, 0, variableHeader.Length);
            }
            if (payLoad.Length > 0)
            {
                stream.Write(payLoad, 0, payLoad.Length);
            }
            byte[] buffer = stream.ToArray();
            stream.Dispose();
            return OperateResult.CreateSuccessResult<byte[]>(buffer);
        }

        public static OperateResult<byte[]> BuildMqttCommand(byte control, byte flags, byte[] variableHeader, byte[] payLoad)
        {
            control = (byte) (control << 4);
            byte head = (byte) (control | flags);
            return BuildMqttCommand(head, variableHeader, payLoad);
        }

        public static OperateResult<byte[]> BuildPublishMqttCommand(MqttPublishMessage message)
        {
            byte flags = 0;
            if (!message.IsSendFirstTime)
            {
                flags = (byte) (flags | 8);
            }
            if (message.Message.Retain)
            {
                flags = (byte) (flags | 1);
            }
            if (message.Message.QualityOfServiceLevel == MqttQualityOfServiceLevel.AtLeastOnce)
            {
                flags = (byte) (flags | 2);
            }
            else if (message.Message.QualityOfServiceLevel == MqttQualityOfServiceLevel.ExactlyOnce)
            {
                flags = (byte) (flags | 4);
            }
            else if (message.Message.QualityOfServiceLevel == MqttQualityOfServiceLevel.OnlyTransfer)
            {
                flags = (byte) (flags | 6);
            }
            List<byte> list = new List<byte>();
            list.AddRange(BuildSegCommandByString(message.Message.Topic));
            if (message.Message.QualityOfServiceLevel > MqttQualityOfServiceLevel.AtMostOnce)
            {
                list.Add(BitConverter.GetBytes(message.Identifier)[1]);
                list.Add(BitConverter.GetBytes(message.Identifier)[0]);
            }
            return BuildMqttCommand(3, flags, list.ToArray(), message.Message.Payload);
        }

        public static OperateResult<byte[]> BuildPublishMqttCommand(string topic, byte[] payload)
        {
            return BuildMqttCommand(3, 0, BuildSegCommandByString(topic), payload);
        }

        public static byte[] BuildSegCommandByString(string message)
        {
            byte[] buffer = string.IsNullOrEmpty(message) ? new byte[0] : Encoding.UTF8.GetBytes(message);
            byte[] array = new byte[buffer.Length + 2];
            buffer.CopyTo(array, 2);
            array[0] = (byte) (buffer.Length / 0x100);
            array[1] = (byte) (buffer.Length % 0x100);
            return array;
        }

        public static OperateResult<byte[]> BuildSubscribeMqttCommand(MqttSubscribeMessage message)
        {
            List<byte> list = new List<byte>();
            List<byte> list2 = new List<byte>();
            list.Add(BitConverter.GetBytes(message.Identifier)[1]);
            list.Add(BitConverter.GetBytes(message.Identifier)[0]);
            for (int i = 0; i < message.Topics.Length; i++)
            {
                list2.AddRange(BuildSegCommandByString(message.Topics[i]));
                if (message.QualityOfServiceLevel == MqttQualityOfServiceLevel.AtMostOnce)
                {
                    list2.AddRange(new byte[1]);
                }
                else if (message.QualityOfServiceLevel == MqttQualityOfServiceLevel.AtLeastOnce)
                {
                    byte[] collection = new byte[] { 1 };
                    list2.AddRange(collection);
                }
                else
                {
                    byte[] buffer2 = new byte[] { 2 };
                    list2.AddRange(buffer2);
                }
            }
            return BuildMqttCommand(8, 2, list.ToArray(), list2.ToArray());
        }

        public static OperateResult<byte[]> BuildUnSubscribeMqttCommand(MqttSubscribeMessage message)
        {
            List<byte> list = new List<byte>();
            List<byte> list2 = new List<byte>();
            list.Add(BitConverter.GetBytes(message.Identifier)[1]);
            list.Add(BitConverter.GetBytes(message.Identifier)[0]);
            for (int i = 0; i < message.Topics.Length; i++)
            {
                list2.AddRange(BuildSegCommandByString(message.Topics[i]));
            }
            return BuildMqttCommand(10, 2, list.ToArray(), list2.ToArray());
        }

        public static OperateResult<byte[]> CalculateLengthToMqttLength(int length)
        {
            if (length > 0xfffffff)
            {
                return new OperateResult<byte[]>(StringResources.Language.MQTTDataTooLong);
            }
            if (length < 0x80)
            {
                byte[] buffer1 = new byte[] { (byte) length };
                return OperateResult.CreateSuccessResult<byte[]>(buffer1);
            }
            if (length < 0x4000)
            {
                return OperateResult.CreateSuccessResult<byte[]>(new byte[] { (byte) ((length % 0x80) + 0x80), (byte) (length / 0x80) });
            }
            if (length < 0x200000)
            {
                return OperateResult.CreateSuccessResult<byte[]>(new byte[] { (byte) ((length % 0x80) + 0x80), (byte) (((length / 0x80) % 0x80) + 0x80), (byte) ((length / 0x80) / 0x80) });
            }
            return OperateResult.CreateSuccessResult<byte[]>(new byte[] { (byte) ((length % 0x80) + 0x80), (byte) (((length / 0x80) % 0x80) + 0x80), (byte) ((((length / 0x80) / 0x80) % 0x80) + 0x80), (byte) (((length / 0x80) / 0x80) / 0x80) });
        }

        public static OperateResult CheckConnectBack(byte code, byte[] data)
        {
            if ((code >> 4) != 2)
            {
                return new OperateResult("MQTT Connection Back Is Wrong: " + code.ToString());
            }
            if (data.Length < 2)
            {
                return new OperateResult("MQTT Connection Data Is Short: " + SoftBasic.ByteToHexString(data, ' '));
            }
            int err = data[1];
            int num2 = data[0];
            if (err > 0)
            {
                return new OperateResult(err, GetMqttCodeText(err));
            }
            return OperateResult.CreateSuccessResult();
        }

        public static int ExtraIntFromBytes(byte[] buffer, ref int index)
        {
            int num = (buffer[index] * 0x100) + buffer[index + 1];
            index += 2;
            return num;
        }

        public static OperateResult<string, byte[]> ExtraMqttReceiveData(byte mqttCode, byte[] data)
        {
            if (data.Length < 2)
            {
                int length = data.Length;
                return new OperateResult<string, byte[]>(StringResources.Language.ReceiveDataLengthTooShort + length.ToString());
            }
            int count = (data[0] * 0x100) + data[1];
            if (data.Length < (2 + count))
            {
                return new OperateResult<string, byte[]>(string.Format("Code[{0:X2}] ExtraMqttReceiveData Error: {1}", mqttCode, SoftBasic.ByteToHexString(data, ' ')));
            }
            string str = (count > 0) ? Encoding.UTF8.GetString(data, 2, count) : string.Empty;
            byte[] destinationArray = new byte[(data.Length - count) - 2];
            Array.Copy(data, count + 2, destinationArray, 0, destinationArray.Length);
            return OperateResult.CreateSuccessResult<string, byte[]>(str, destinationArray);
        }

        public static string ExtraMsgFromBytes(byte[] buffer, ref int index)
        {
            int num = index;
            int count = (buffer[index] * 0x100) + buffer[index + 1];
            index = (index + 2) + count;
            return Encoding.UTF8.GetString(buffer, num + 2, count);
        }

        public static string ExtraSubscribeMsgFromBytes(byte[] buffer, ref int index)
        {
            int num = index;
            int count = (buffer[index] * 0x100) + buffer[index + 1];
            index = (index + 3) + count;
            return Encoding.UTF8.GetString(buffer, num + 2, count);
        }

        public static string GetMqttCodeText(int status)
        {
            switch (status)
            {
                case 1:
                    return StringResources.Language.MQTTStatus01;

                case 2:
                    return StringResources.Language.MQTTStatus02;

                case 3:
                    return StringResources.Language.MQTTStatus03;

                case 4:
                    return StringResources.Language.MQTTStatus04;

                case 5:
                    return StringResources.Language.MQTTStatus05;
            }
            return StringResources.Language.UnknownError;
        }

        public static OperateResult<MqttRpcApiInfo> GetMqttSyncServicesApiFromMethod(string api, MethodInfo method, object obj, [Optional, DefaultParameterValue(null)] HslMqttPermissionAttribute permissionAttribute)
        {
            object[] customAttributes = method.GetCustomAttributes(typeof(HslMqttApiAttribute), false);
            if ((customAttributes == null) || (customAttributes.Length == 0))
            {
                return new OperateResult<MqttRpcApiInfo>(string.Format("Current Api ：[{0}] not support Api attribute", method));
            }
            HslMqttApiAttribute attribute = (HslMqttApiAttribute) customAttributes[0];
            MqttRpcApiInfo info = new MqttRpcApiInfo {
                SourceObject = obj,
                Method = method,
                Description = attribute.Description,
                HttpMethod = attribute.HttpMethod.ToUpper()
            };
            if (string.IsNullOrEmpty(attribute.ApiTopic))
            {
                attribute.ApiTopic = method.Name;
            }
            if (permissionAttribute == null)
            {
                customAttributes = method.GetCustomAttributes(typeof(HslMqttPermissionAttribute), false);
                if ((customAttributes != null) ? (customAttributes.Length > 0) : false)
                {
                    info.PermissionAttribute = (HslMqttPermissionAttribute) customAttributes[0];
                }
            }
            else
            {
                info.PermissionAttribute = permissionAttribute;
            }
            if (string.IsNullOrEmpty(api))
            {
                info.ApiTopic = attribute.ApiTopic;
            }
            else
            {
                info.ApiTopic = api + "/" + attribute.ApiTopic;
            }
            info.ExamplePayload = HslReflectionHelper.GetParametersFromJson(method).ToString();
            return OperateResult.CreateSuccessResult<MqttRpcApiInfo>(info);
        }

        public static OperateResult<HslMqttApiAttribute, MqttRpcApiInfo> GetMqttSyncServicesApiFromProperty(string api, PropertyInfo property, object obj, [Optional, DefaultParameterValue(null)] HslMqttPermissionAttribute permissionAttribute)
        {
            object[] customAttributes = property.GetCustomAttributes(typeof(HslMqttApiAttribute), false);
            if ((customAttributes == null) || (customAttributes.Length == 0))
            {
                return new OperateResult<HslMqttApiAttribute, MqttRpcApiInfo>(string.Format("Current Api ：[{0}] not support Api attribute", property));
            }
            HslMqttApiAttribute attribute = (HslMqttApiAttribute) customAttributes[0];
            MqttRpcApiInfo info = new MqttRpcApiInfo {
                SourceObject = obj,
                Property = property,
                Description = attribute.Description,
                HttpMethod = attribute.HttpMethod.ToUpper()
            };
            if (string.IsNullOrEmpty(attribute.ApiTopic))
            {
                attribute.ApiTopic = property.Name;
            }
            if (permissionAttribute == null)
            {
                customAttributes = property.GetCustomAttributes(typeof(HslMqttPermissionAttribute), false);
                if ((customAttributes != null) ? (customAttributes.Length > 0) : false)
                {
                    info.PermissionAttribute = (HslMqttPermissionAttribute) customAttributes[0];
                }
            }
            else
            {
                info.PermissionAttribute = permissionAttribute;
            }
            if (string.IsNullOrEmpty(api))
            {
                info.ApiTopic = attribute.ApiTopic;
            }
            else
            {
                info.ApiTopic = api + "/" + attribute.ApiTopic;
            }
            info.ExamplePayload = string.Empty;
            return OperateResult.CreateSuccessResult<HslMqttApiAttribute, MqttRpcApiInfo>(attribute, info);
        }

        public static List<MqttRpcApiInfo> GetSyncServicesApiInformationFromObject(object obj)
        {
            Type type = obj as Type;
            if (type > null)
            {
                return GetSyncServicesApiInformationFromObject(type.Name, type, null);
            }
            return GetSyncServicesApiInformationFromObject(obj.GetType().Name, obj, null);
        }

        public static List<MqttRpcApiInfo> GetSyncServicesApiInformationFromObject(string api, object obj, [Optional, DefaultParameterValue(null)] HslMqttPermissionAttribute permissionAttribute)
        {
            Type type = null;
            Type type2 = obj as Type;
            if (type2 > null)
            {
                type = type2;
                obj = null;
            }
            else
            {
                type = obj.GetType();
            }
            MethodInfo[] methods = type.GetMethods();
            List<MqttRpcApiInfo> list = new List<MqttRpcApiInfo>();
            foreach (MethodInfo info in methods)
            {
                OperateResult<MqttRpcApiInfo> result = GetMqttSyncServicesApiFromMethod(api, info, obj, permissionAttribute);
                if (result.IsSuccess)
                {
                    list.Add(result.Content);
                }
            }
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo info2 in properties)
            {
                OperateResult<HslMqttApiAttribute, MqttRpcApiInfo> result2 = GetMqttSyncServicesApiFromProperty(api, info2, obj, permissionAttribute);
                if (result2.IsSuccess)
                {
                    if (!result2.Content1.PropertyUnfold)
                    {
                        list.Add(result2.Content2);
                    }
                    else if (info2.GetValue(obj, null) != null)
                    {
                        List<MqttRpcApiInfo> collection = GetSyncServicesApiInformationFromObject(result2.Content2.ApiTopic, info2.GetValue(obj, null), permissionAttribute);
                        list.AddRange(collection);
                    }
                }
            }
            return list;
        }

        public static OperateResult<string> HandleObjectMethod(MqttSession mqttSession, MqttClientApplicationMessage message, MqttRpcApiInfo apiInformation)
        {
            object obj3 = null;
            if (apiInformation.PermissionAttribute > null)
            {
                if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
                {
                    return new OperateResult<string>("Permission function need authorization ：" + StringResources.Language.InsufficientPrivileges);
                }
                if (!apiInformation.PermissionAttribute.CheckClientID(mqttSession.ClientId))
                {
                    string[] textArray1 = new string[] { "Mqtt RPC Api ：[", apiInformation.ApiTopic, "] Check ClientID[", mqttSession.ClientId, "] failed, access not permission" };
                    return new OperateResult<string>(string.Concat(textArray1));
                }
                if (!apiInformation.PermissionAttribute.CheckUserName(mqttSession.UserName))
                {
                    string[] textArray2 = new string[] { "Mqtt RPC Api ：[", apiInformation.ApiTopic, "] Check Username[", mqttSession.UserName, "] failed, access not permission" };
                    return new OperateResult<string>(string.Concat(textArray2));
                }
            }
            try
            {
                if (apiInformation.Method > null)
                {
                    string str = Encoding.UTF8.GetString(message.Payload);
                    JObject obj2 = string.IsNullOrEmpty(str) ? new JObject() : JObject.Parse(str);
                    object[] parametersFromJson = HslReflectionHelper.GetParametersFromJson(apiInformation.Method.GetParameters(), str);
                    obj3 = apiInformation.Method.Invoke(apiInformation.SourceObject, parametersFromJson);
                }
                else if (apiInformation.Property > null)
                {
                    obj3 = apiInformation.Property.GetValue(apiInformation.SourceObject, null);
                }
            }
            catch (Exception exception)
            {
                return new OperateResult<string>("Mqtt RPC Api ：[" + apiInformation.ApiTopic + "] Wrong，Reason：" + exception.Message);
            }
            OperateResult result = obj3 as OperateResult;
            if (result > null)
            {
                OperateResult<string> result3 = new OperateResult<string> {
                    IsSuccess = result.IsSuccess,
                    ErrorCode = result.ErrorCode,
                    Message = result.Message
                };
                if (result.IsSuccess)
                {
                    PropertyInfo property = obj3.GetType().GetProperty("Content");
                    if (property <= null)
                    {
                        return result3;
                    }
                    object obj4 = property.GetValue(obj3, null);
                    if (obj4 > null)
                    {
                        result3.Content = obj4.ToJsonString(Formatting.Indented);
                    }
                }
                return result3;
            }
            return OperateResult.CreateSuccessResult<string>((obj3 == null) ? string.Empty : obj3.ToJsonString(Formatting.Indented));
        }

        public static OperateResult<string> HandleObjectMethod(MqttSession mqttSession, MqttClientApplicationMessage message, object obj)
        {
            string topic = message.Topic;
            if (topic.LastIndexOf('/') >= 0)
            {
                topic = topic.Substring(topic.LastIndexOf('/') + 1);
            }
            MethodInfo method = obj.GetType().GetMethod(topic);
            if (method == null)
            {
                return new OperateResult<string>("Current MqttSync Api ：[" + topic + "] not exsist");
            }
            OperateResult<MqttRpcApiInfo> result = GetMqttSyncServicesApiFromMethod("", method, obj, null);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            return HandleObjectMethod(mqttSession, message, result.Content);
        }
    }
}

