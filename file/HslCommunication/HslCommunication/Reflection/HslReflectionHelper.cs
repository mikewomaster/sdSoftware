namespace HslCommunication.Reflection
{
    using HslCommunication;
    using HslCommunication.Core;
    using HslCommunication.Enthernet.Redis;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class HslReflectionHelper
    {
        public static JObject GetParametersFromJson(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            JObject obj2 = new JObject();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType == typeof(byte))
                {
                    obj2.Add(parameters[i].Name, new JValue(0L));
                }
                else if (parameters[i].ParameterType == typeof(short))
                {
                    obj2.Add(parameters[i].Name, new JValue(0L));
                }
                else if (parameters[i].ParameterType == typeof(ushort))
                {
                    obj2.Add(parameters[i].Name, new JValue(0L));
                }
                else if (parameters[i].ParameterType == typeof(int))
                {
                    obj2.Add(parameters[i].Name, new JValue(0L));
                }
                else if (parameters[i].ParameterType == typeof(uint))
                {
                    obj2.Add(parameters[i].Name, new JValue(0L));
                }
                else if (parameters[i].ParameterType == typeof(long))
                {
                    obj2.Add(parameters[i].Name, new JValue(0L));
                }
                else if (parameters[i].ParameterType == typeof(ulong))
                {
                    obj2.Add(parameters[i].Name, new JValue(0L));
                }
                else if (parameters[i].ParameterType == typeof(double))
                {
                    obj2.Add(parameters[i].Name, new JValue(0.0));
                }
                else if (parameters[i].ParameterType == typeof(float))
                {
                    obj2.Add(parameters[i].Name, new JValue(0f));
                }
                else if (parameters[i].ParameterType == typeof(bool))
                {
                    obj2.Add(parameters[i].Name, new JValue(false));
                }
                else if (parameters[i].ParameterType == typeof(string))
                {
                    obj2.Add(parameters[i].Name, new JValue(""));
                }
                else if (parameters[i].ParameterType == typeof(DateTime))
                {
                    obj2.Add(parameters[i].Name, new JValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                }
                else if (parameters[i].ParameterType == typeof(byte[]))
                {
                    obj2.Add(parameters[i].Name, new JValue("00 1A 2B 3C 4D"));
                }
                else if (parameters[i].ParameterType == typeof(short[]))
                {
                    obj2.Add(parameters[i].Name, new JArray(new int[] { 1, 2, 3 }));
                }
                else if (parameters[i].ParameterType == typeof(ushort[]))
                {
                    obj2.Add(parameters[i].Name, new JArray(new int[] { 1, 2, 3 }));
                }
                else if (parameters[i].ParameterType == typeof(int[]))
                {
                    obj2.Add(parameters[i].Name, new JArray(new int[] { 1, 2, 3 }));
                }
                else if (parameters[i].ParameterType == typeof(uint[]))
                {
                    obj2.Add(parameters[i].Name, new JArray(new int[] { 1, 2, 3 }));
                }
                else if (parameters[i].ParameterType == typeof(long[]))
                {
                    obj2.Add(parameters[i].Name, new JArray(new int[] { 1, 2, 3 }));
                }
                else if (parameters[i].ParameterType == typeof(ulong[]))
                {
                    obj2.Add(parameters[i].Name, new JArray(new int[] { 1, 2, 3 }));
                }
                else if (parameters[i].ParameterType == typeof(float[]))
                {
                    obj2.Add(parameters[i].Name, new JArray(new float[] { 1f, 2f, 3f }));
                }
                else if (parameters[i].ParameterType == typeof(double[]))
                {
                    obj2.Add(parameters[i].Name, new JArray(new double[] { 1.0, 2.0, 3.0 }));
                }
                else if (parameters[i].ParameterType == typeof(bool[]))
                {
                    bool[] content = new bool[3];
                    content[0] = true;
                    obj2.Add(parameters[i].Name, new JArray(content));
                }
                else
                {
                    object[] objArray;
                    if (parameters[i].ParameterType == typeof(string[]))
                    {
                        string[] textArray1 = new string[] { "1", "2", "3" };
                        objArray = textArray1;
                        obj2.Add(parameters[i].Name, new JArray(objArray));
                    }
                    else if (parameters[i].ParameterType == typeof(DateTime[]))
                    {
                        string[] textArray2 = new string[] { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                        objArray = textArray2;
                        obj2.Add(parameters[i].Name, new JArray(objArray));
                    }
                    else
                    {
                        obj2.Add(parameters[i].Name, JToken.FromObject(Activator.CreateInstance(parameters[i].ParameterType)));
                    }
                }
            }
            return obj2;
        }

        public static object[] GetParametersFromJson(ParameterInfo[] parameters, string json)
        {
            JObject obj2 = string.IsNullOrEmpty(json) ? new JObject() : JObject.Parse(json);
            object[] objArray = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType == typeof(byte))
                {
                    objArray[i] = obj2.Value<byte>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(short))
                {
                    objArray[i] = obj2.Value<short>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(ushort))
                {
                    objArray[i] = obj2.Value<ushort>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(int))
                {
                    objArray[i] = obj2.Value<int>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(uint))
                {
                    objArray[i] = obj2.Value<uint>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(long))
                {
                    objArray[i] = obj2.Value<long>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(ulong))
                {
                    objArray[i] = obj2.Value<ulong>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(double))
                {
                    objArray[i] = obj2.Value<double>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(float))
                {
                    objArray[i] = obj2.Value<float>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(bool))
                {
                    objArray[i] = obj2.Value<bool>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(string))
                {
                    objArray[i] = obj2.Value<string>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(DateTime))
                {
                    objArray[i] = obj2.Value<DateTime>(parameters[i].Name);
                }
                else if (parameters[i].ParameterType == typeof(byte[]))
                {
                    objArray[i] = obj2.Value<string>(parameters[i].Name).ToHexBytes();
                }
                else if (parameters[i].ParameterType == typeof(short[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<short>()).ToArray<short>();
                }
                else if (parameters[i].ParameterType == typeof(ushort[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<ushort>()).ToArray<ushort>();
                }
                else if (parameters[i].ParameterType == typeof(int[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<int>()).ToArray<int>();
                }
                else if (parameters[i].ParameterType == typeof(uint[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<uint>()).ToArray<uint>();
                }
                else if (parameters[i].ParameterType == typeof(long[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<long>()).ToArray<long>();
                }
                else if (parameters[i].ParameterType == typeof(ulong[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<ulong>()).ToArray<ulong>();
                }
                else if (parameters[i].ParameterType == typeof(float[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<float>()).ToArray<float>();
                }
                else if (parameters[i].ParameterType == typeof(double[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<double>()).ToArray<double>();
                }
                else if (parameters[i].ParameterType == typeof(bool[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<bool>()).ToArray<bool>();
                }
                else if (parameters[i].ParameterType == typeof(string[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<string>()).ToArray<string>();
                }
                else if (parameters[i].ParameterType == typeof(DateTime[]))
                {
                    objArray[i] = (from m in obj2[parameters[i].Name].ToArray<JToken>() select m.Value<DateTime>()).ToArray<DateTime>();
                }
                else
                {
                    objArray[i] = obj2[parameters[i].Name].ToObject(parameters[i].ParameterType);
                }
            }
            return objArray;
        }

        public static object[] GetParametersFromUrl(ParameterInfo[] parameters, string url)
        {
            if (url.IndexOf('?') > 0)
            {
                url = url.Substring(url.IndexOf('?') + 1);
            }
            char[] separator = new char[] { '&' };
            string[] strArray = url.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> dictionary = new Dictionary<string, string>(strArray.Length);
            for (int i = 0; i < strArray.Length; i++)
            {
                if (!string.IsNullOrEmpty(strArray[i]) && (strArray[i].IndexOf('=') > 0))
                {
                    char[] trimChars = new char[] { ' ' };
                    dictionary.Add(strArray[i].Substring(0, strArray[i].IndexOf('=')).Trim(trimChars), strArray[i].Substring(strArray[i].IndexOf('=') + 1));
                }
            }
            object[] objArray = new object[parameters.Length];
            for (int j = 0; j < parameters.Length; j++)
            {
                if (parameters[j].ParameterType == typeof(byte))
                {
                    objArray[j] = byte.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(short))
                {
                    objArray[j] = short.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(ushort))
                {
                    objArray[j] = ushort.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(int))
                {
                    objArray[j] = int.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(uint))
                {
                    objArray[j] = uint.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(long))
                {
                    objArray[j] = long.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(ulong))
                {
                    objArray[j] = ulong.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(double))
                {
                    objArray[j] = double.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(float))
                {
                    objArray[j] = float.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(bool))
                {
                    objArray[j] = bool.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(string))
                {
                    objArray[j] = dictionary[parameters[j].Name];
                }
                else if (parameters[j].ParameterType == typeof(DateTime))
                {
                    objArray[j] = DateTime.Parse(dictionary[parameters[j].Name]);
                }
                else if (parameters[j].ParameterType == typeof(byte[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToHexBytes();
                }
                else if (parameters[j].ParameterType == typeof(short[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<short>();
                }
                else if (parameters[j].ParameterType == typeof(ushort[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<ushort>();
                }
                else if (parameters[j].ParameterType == typeof(int[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<int>();
                }
                else if (parameters[j].ParameterType == typeof(uint[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<uint>();
                }
                else if (parameters[j].ParameterType == typeof(long[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<long>();
                }
                else if (parameters[j].ParameterType == typeof(ulong[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<ulong>();
                }
                else if (parameters[j].ParameterType == typeof(float[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<float>();
                }
                else if (parameters[j].ParameterType == typeof(double[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<double>();
                }
                else if (parameters[j].ParameterType == typeof(bool[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<bool>();
                }
                else if (parameters[j].ParameterType == typeof(string[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<string>();
                }
                else if (parameters[j].ParameterType == typeof(DateTime[]))
                {
                    objArray[j] = dictionary[parameters[j].Name].ToStringArray<DateTime>();
                }
                else
                {
                    objArray[j] = JToken.Parse(dictionary[parameters[j].Name]).ToObject(parameters[j].ParameterType);
                }
            }
            return objArray;
        }

        public static OperateResult<T> Read<T>(IReadWriteNet readWrite) where T: class, new()
        {
            Type type = typeof(T);
            object obj2 = type.Assembly.CreateInstance(type.FullName);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo info in properties)
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(HslDeviceAddressAttribute), false);
                if (customAttributes != null)
                {
                    HslDeviceAddressAttribute attribute = null;
                    for (int i = 0; i < customAttributes.Length; i++)
                    {
                        HslDeviceAddressAttribute attribute2 = (HslDeviceAddressAttribute) customAttributes[i];
                        if ((attribute2.DeviceType != null) && (attribute2.DeviceType == readWrite.GetType()))
                        {
                            attribute = attribute2;
                            break;
                        }
                    }
                    if (attribute == null)
                    {
                        for (int j = 0; j < customAttributes.Length; j++)
                        {
                            HslDeviceAddressAttribute attribute3 = (HslDeviceAddressAttribute) customAttributes[j];
                            if (attribute3.DeviceType == null)
                            {
                                attribute = attribute3;
                                break;
                            }
                        }
                    }
                    if (attribute != null)
                    {
                        Type propertyType = info.PropertyType;
                        if (propertyType == typeof(short))
                        {
                            OperateResult<short> result = readWrite.ReadInt16(attribute.Address);
                            if (!result.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result);
                            }
                            info.SetValue(obj2, result.Content, null);
                        }
                        else if (propertyType == typeof(short[]))
                        {
                            OperateResult<short[]> result3 = readWrite.ReadInt16(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result3.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result3);
                            }
                            info.SetValue(obj2, result3.Content, null);
                        }
                        else if (propertyType == typeof(ushort))
                        {
                            OperateResult<ushort> result4 = readWrite.ReadUInt16(attribute.Address);
                            if (!result4.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result4);
                            }
                            info.SetValue(obj2, result4.Content, null);
                        }
                        else if (propertyType == typeof(ushort[]))
                        {
                            OperateResult<ushort[]> result5 = readWrite.ReadUInt16(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result5.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result5);
                            }
                            info.SetValue(obj2, result5.Content, null);
                        }
                        else if (propertyType == typeof(int))
                        {
                            OperateResult<int> result6 = readWrite.ReadInt32(attribute.Address);
                            if (!result6.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result6);
                            }
                            info.SetValue(obj2, result6.Content, null);
                        }
                        else if (propertyType == typeof(int[]))
                        {
                            OperateResult<int[]> result7 = readWrite.ReadInt32(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result7.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result7);
                            }
                            info.SetValue(obj2, result7.Content, null);
                        }
                        else if (propertyType == typeof(uint))
                        {
                            OperateResult<uint> result8 = readWrite.ReadUInt32(attribute.Address);
                            if (!result8.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result8);
                            }
                            info.SetValue(obj2, result8.Content, null);
                        }
                        else if (propertyType == typeof(uint[]))
                        {
                            OperateResult<uint[]> result9 = readWrite.ReadUInt32(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result9.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result9);
                            }
                            info.SetValue(obj2, result9.Content, null);
                        }
                        else if (propertyType == typeof(long))
                        {
                            OperateResult<long> result10 = readWrite.ReadInt64(attribute.Address);
                            if (!result10.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result10);
                            }
                            info.SetValue(obj2, result10.Content, null);
                        }
                        else if (propertyType == typeof(long[]))
                        {
                            OperateResult<long[]> result11 = readWrite.ReadInt64(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result11.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result11);
                            }
                            info.SetValue(obj2, result11.Content, null);
                        }
                        else if (propertyType == typeof(ulong))
                        {
                            OperateResult<ulong> result12 = readWrite.ReadUInt64(attribute.Address);
                            if (!result12.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result12);
                            }
                            info.SetValue(obj2, result12.Content, null);
                        }
                        else if (propertyType == typeof(ulong[]))
                        {
                            OperateResult<ulong[]> result13 = readWrite.ReadUInt64(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result13.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result13);
                            }
                            info.SetValue(obj2, result13.Content, null);
                        }
                        else if (propertyType == typeof(float))
                        {
                            OperateResult<float> result14 = readWrite.ReadFloat(attribute.Address);
                            if (!result14.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result14);
                            }
                            info.SetValue(obj2, result14.Content, null);
                        }
                        else if (propertyType == typeof(float[]))
                        {
                            OperateResult<float[]> result15 = readWrite.ReadFloat(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result15.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result15);
                            }
                            info.SetValue(obj2, result15.Content, null);
                        }
                        else if (propertyType == typeof(double))
                        {
                            OperateResult<double> result16 = readWrite.ReadDouble(attribute.Address);
                            if (!result16.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result16);
                            }
                            info.SetValue(obj2, result16.Content, null);
                        }
                        else if (propertyType == typeof(double[]))
                        {
                            OperateResult<double[]> result17 = readWrite.ReadDouble(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result17.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result17);
                            }
                            info.SetValue(obj2, result17.Content, null);
                        }
                        else if (propertyType == typeof(string))
                        {
                            OperateResult<string> result18 = readWrite.ReadString(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result18.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result18);
                            }
                            info.SetValue(obj2, result18.Content, null);
                        }
                        else if (propertyType == typeof(byte[]))
                        {
                            OperateResult<byte[]> result19 = readWrite.Read(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result19.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result19);
                            }
                            info.SetValue(obj2, result19.Content, null);
                        }
                        else if (propertyType == typeof(bool))
                        {
                            OperateResult<bool> result20 = readWrite.ReadBool(attribute.Address);
                            if (!result20.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result20);
                            }
                            info.SetValue(obj2, result20.Content, null);
                        }
                        else if (propertyType == typeof(bool[]))
                        {
                            OperateResult<bool[]> result21 = readWrite.ReadBool(attribute.Address, (attribute.Length > 0) ? ((ushort) attribute.Length) : ((ushort) 1));
                            if (!result21.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result21);
                            }
                            info.SetValue(obj2, result21.Content, null);
                        }
                    }
                }
            }
            return OperateResult.CreateSuccessResult<T>((T) obj2);
        }

        public static OperateResult<T> Read<T>(RedisClient redis) where T: class, new()
        {
            Type type = typeof(T);
            object obj2 = type.Assembly.CreateInstance(type.FullName);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<PropertyInfoKeyName> list = new List<PropertyInfoKeyName>();
            List<PropertyInfoHashKeyName> list2 = new List<PropertyInfoHashKeyName>();
            foreach (PropertyInfo info in properties)
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(HslRedisKeyAttribute), false);
                if ((customAttributes != null) ? (customAttributes.Length > 0) : false)
                {
                    HslRedisKeyAttribute attribute = (HslRedisKeyAttribute) customAttributes[0];
                    list.Add(new PropertyInfoKeyName(info, attribute.KeyName));
                }
                else
                {
                    customAttributes = info.GetCustomAttributes(typeof(HslRedisListItemAttribute), false);
                    if ((customAttributes != null) ? (customAttributes.Length > 0) : false)
                    {
                        HslRedisListItemAttribute attribute2 = (HslRedisListItemAttribute) customAttributes[0];
                        OperateResult<string> result = redis.ReadListByIndex(attribute2.ListKey, attribute2.Index);
                        if (!result.IsSuccess)
                        {
                            return OperateResult.CreateFailedResult<T>(result);
                        }
                        SetPropertyObjectValue(info, obj2, result.Content);
                    }
                    else
                    {
                        customAttributes = info.GetCustomAttributes(typeof(HslRedisListAttribute), false);
                        if ((customAttributes != null) ? (customAttributes.Length > 0) : false)
                        {
                            HslRedisListAttribute attribute3 = (HslRedisListAttribute) customAttributes[0];
                            OperateResult<string[]> result3 = redis.ListRange(attribute3.ListKey, attribute3.StartIndex, attribute3.EndIndex);
                            if (!result3.IsSuccess)
                            {
                                return OperateResult.CreateFailedResult<T>(result3);
                            }
                            SetPropertyObjectValueArray(info, obj2, result3.Content);
                        }
                        else
                        {
                            customAttributes = info.GetCustomAttributes(typeof(HslRedisHashFieldAttribute), false);
                            if ((customAttributes != null) ? (customAttributes.Length > 0) : false)
                            {
                                HslRedisHashFieldAttribute attribute4 = (HslRedisHashFieldAttribute) customAttributes[0];
                                list2.Add(new PropertyInfoHashKeyName(info, attribute4.HaskKey, attribute4.Field));
                            }
                        }
                    }
                }
            }
            if (list.Count > 0)
            {
                OperateResult<string[]> result4 = redis.ReadKey((from m in list select m.KeyName).ToArray<string>());
                if (!result4.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<T>(result4);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    SetPropertyObjectValue(list[i].PropertyInfo, obj2, result4.Content[i]);
                }
            }
            if (list2.Count > 0)
            {
                var enumerable = from m in list2
                    group m by m.KeyName into g
                    select new { 
                        Key = g.Key,
                        Values = g.ToArray<PropertyInfoHashKeyName>()
                    };
                foreach (var type2 in enumerable)
                {
                    if (type2.Values.Length == 1)
                    {
                        OperateResult<string> result5 = redis.ReadHashKey(type2.Key, type2.Values[0].Field);
                        if (!result5.IsSuccess)
                        {
                            return OperateResult.CreateFailedResult<T>(result5);
                        }
                        SetPropertyObjectValue(type2.Values[0].PropertyInfo, obj2, result5.Content);
                    }
                    else
                    {
                        OperateResult<string[]> result6 = redis.ReadHashKey(type2.Key, (from m in type2.Values select m.Field).ToArray<string>());
                        if (!result6.IsSuccess)
                        {
                            return OperateResult.CreateFailedResult<T>(result6);
                        }
                        for (int j = 0; j < type2.Values.Length; j++)
                        {
                            SetPropertyObjectValue(type2.Values[j].PropertyInfo, obj2, result6.Content[j]);
                        }
                    }
                }
            }
            return OperateResult.CreateSuccessResult<T>((T) obj2);
        }

        public static void SetPropertyExp<T, K>(PropertyInfo propertyInfo, T obj, K objValue)
        {
            ParameterExpression expression;
            ParameterExpression expression2 = Expression.Parameter(propertyInfo.PropertyType, "objValue");
            Expression[] arguments = new Expression[] { expression2 };
            ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2 };
            Expression<Action<T, K>> expression4 = Expression.Lambda<Action<T, K>>(Expression.Call(expression = Expression.Parameter(typeof(T), "obj"), propertyInfo.GetSetMethod(), arguments), parameters);
            expression4.Compile()(obj, objValue);
        }

        internal static void SetPropertyObjectValue(PropertyInfo property, object obj, string value)
        {
            Type propertyType = property.PropertyType;
            if (propertyType == typeof(short))
            {
                property.SetValue(obj, short.Parse(value), null);
            }
            else if (propertyType == typeof(ushort))
            {
                property.SetValue(obj, ushort.Parse(value), null);
            }
            else if (propertyType == typeof(int))
            {
                property.SetValue(obj, int.Parse(value), null);
            }
            else if (propertyType == typeof(uint))
            {
                property.SetValue(obj, uint.Parse(value), null);
            }
            else if (propertyType == typeof(long))
            {
                property.SetValue(obj, long.Parse(value), null);
            }
            else if (propertyType == typeof(ulong))
            {
                property.SetValue(obj, ulong.Parse(value), null);
            }
            else if (propertyType == typeof(float))
            {
                property.SetValue(obj, float.Parse(value), null);
            }
            else if (propertyType == typeof(double))
            {
                property.SetValue(obj, double.Parse(value), null);
            }
            else if (propertyType == typeof(string))
            {
                property.SetValue(obj, value, null);
            }
            else if (propertyType == typeof(byte))
            {
                property.SetValue(obj, byte.Parse(value), null);
            }
            else if (propertyType == typeof(bool))
            {
                property.SetValue(obj, bool.Parse(value), null);
            }
            else
            {
                property.SetValue(obj, value, null);
            }
        }

        internal static void SetPropertyObjectValueArray(PropertyInfo property, object obj, string[] values)
        {
            Type propertyType = property.PropertyType;
            if (propertyType == typeof(short[]))
            {
                property.SetValue(obj, (from m in values select short.Parse(m)).ToArray<short>(), null);
            }
            else if (propertyType == typeof(List<short>))
            {
                property.SetValue(obj, (from m in values select short.Parse(m)).ToList<short>(), null);
            }
            else if (propertyType == typeof(ushort[]))
            {
                property.SetValue(obj, (from m in values select ushort.Parse(m)).ToArray<ushort>(), null);
            }
            else if (propertyType == typeof(List<ushort>))
            {
                property.SetValue(obj, (from m in values select ushort.Parse(m)).ToList<ushort>(), null);
            }
            else if (propertyType == typeof(int[]))
            {
                property.SetValue(obj, (from m in values select int.Parse(m)).ToArray<int>(), null);
            }
            else if (propertyType == typeof(List<int>))
            {
                property.SetValue(obj, (from m in values select int.Parse(m)).ToList<int>(), null);
            }
            else if (propertyType == typeof(uint[]))
            {
                property.SetValue(obj, (from m in values select uint.Parse(m)).ToArray<uint>(), null);
            }
            else if (propertyType == typeof(List<uint>))
            {
                property.SetValue(obj, (from m in values select uint.Parse(m)).ToList<uint>(), null);
            }
            else if (propertyType == typeof(long[]))
            {
                property.SetValue(obj, (from m in values select long.Parse(m)).ToArray<long>(), null);
            }
            else if (propertyType == typeof(List<long>))
            {
                property.SetValue(obj, (from m in values select long.Parse(m)).ToList<long>(), null);
            }
            else if (propertyType == typeof(ulong[]))
            {
                property.SetValue(obj, (from m in values select ulong.Parse(m)).ToArray<ulong>(), null);
            }
            else if (propertyType == typeof(List<ulong>))
            {
                property.SetValue(obj, (from m in values select ulong.Parse(m)).ToList<ulong>(), null);
            }
            else if (propertyType == typeof(float[]))
            {
                property.SetValue(obj, (from m in values select float.Parse(m)).ToArray<float>(), null);
            }
            else if (propertyType == typeof(List<float>))
            {
                property.SetValue(obj, (from m in values select float.Parse(m)).ToList<float>(), null);
            }
            else if (propertyType == typeof(double[]))
            {
                property.SetValue(obj, (from m in values select double.Parse(m)).ToArray<double>(), null);
            }
            else if (propertyType == typeof(double[]))
            {
                property.SetValue(obj, (from m in values select double.Parse(m)).ToList<double>(), null);
            }
            else if (propertyType == typeof(string[]))
            {
                property.SetValue(obj, values, null);
            }
            else if (propertyType == typeof(List<string>))
            {
                property.SetValue(obj, new List<string>(values), null);
            }
            else if (propertyType == typeof(byte[]))
            {
                property.SetValue(obj, (from m in values select byte.Parse(m)).ToArray<byte>(), null);
            }
            else if (propertyType == typeof(List<byte>))
            {
                property.SetValue(obj, (from m in values select byte.Parse(m)).ToList<byte>(), null);
            }
            else if (propertyType == typeof(bool[]))
            {
                property.SetValue(obj, (from m in values select bool.Parse(m)).ToArray<bool>(), null);
            }
            else if (propertyType == typeof(List<bool>))
            {
                property.SetValue(obj, (from m in values select bool.Parse(m)).ToList<bool>(), null);
            }
            else
            {
                property.SetValue(obj, values, null);
            }
        }

        public static OperateResult Write<T>(T data, IReadWriteNet readWrite) where T: class, new()
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            Type type = typeof(T);
            T local = data;
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo info in properties)
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(HslDeviceAddressAttribute), false);
                if (customAttributes != null)
                {
                    HslDeviceAddressAttribute attribute = null;
                    for (int i = 0; i < customAttributes.Length; i++)
                    {
                        HslDeviceAddressAttribute attribute2 = (HslDeviceAddressAttribute) customAttributes[i];
                        if ((attribute2.DeviceType != null) && (attribute2.DeviceType == readWrite.GetType()))
                        {
                            attribute = attribute2;
                            break;
                        }
                    }
                    if (attribute == null)
                    {
                        for (int j = 0; j < customAttributes.Length; j++)
                        {
                            HslDeviceAddressAttribute attribute3 = (HslDeviceAddressAttribute) customAttributes[j];
                            if (attribute3.DeviceType == null)
                            {
                                attribute = attribute3;
                                break;
                            }
                        }
                    }
                    if (attribute != null)
                    {
                        Type propertyType = info.PropertyType;
                        if (propertyType == typeof(short))
                        {
                            short num4 = (short) info.GetValue(local, null);
                            OperateResult result = readWrite.Write(attribute.Address, num4);
                            if (!result.IsSuccess)
                            {
                                return result;
                            }
                        }
                        else if (propertyType == typeof(short[]))
                        {
                            short[] values = (short[]) info.GetValue(local, null);
                            OperateResult result3 = readWrite.Write(attribute.Address, values);
                            if (!result3.IsSuccess)
                            {
                                return result3;
                            }
                        }
                        else if (propertyType == typeof(ushort))
                        {
                            ushort num5 = (ushort) info.GetValue(local, null);
                            OperateResult result4 = readWrite.Write(attribute.Address, num5);
                            if (!result4.IsSuccess)
                            {
                                return result4;
                            }
                        }
                        else if (propertyType == typeof(ushort[]))
                        {
                            ushort[] numArray2 = (ushort[]) info.GetValue(local, null);
                            OperateResult result5 = readWrite.Write(attribute.Address, numArray2);
                            if (!result5.IsSuccess)
                            {
                                return result5;
                            }
                        }
                        else if (propertyType == typeof(int))
                        {
                            int num6 = (int) info.GetValue(local, null);
                            OperateResult result6 = readWrite.Write(attribute.Address, num6);
                            if (!result6.IsSuccess)
                            {
                                return result6;
                            }
                        }
                        else if (propertyType == typeof(int[]))
                        {
                            int[] numArray3 = (int[]) info.GetValue(local, null);
                            OperateResult result7 = readWrite.Write(attribute.Address, numArray3);
                            if (!result7.IsSuccess)
                            {
                                return result7;
                            }
                        }
                        else if (propertyType == typeof(uint))
                        {
                            uint num7 = (uint) info.GetValue(local, null);
                            OperateResult result8 = readWrite.Write(attribute.Address, num7);
                            if (!result8.IsSuccess)
                            {
                                return result8;
                            }
                        }
                        else if (propertyType == typeof(uint[]))
                        {
                            uint[] numArray4 = (uint[]) info.GetValue(local, null);
                            OperateResult result9 = readWrite.Write(attribute.Address, numArray4);
                            if (!result9.IsSuccess)
                            {
                                return result9;
                            }
                        }
                        else if (propertyType == typeof(long))
                        {
                            long num8 = (long) info.GetValue(local, null);
                            OperateResult result10 = readWrite.Write(attribute.Address, num8);
                            if (!result10.IsSuccess)
                            {
                                return result10;
                            }
                        }
                        else if (propertyType == typeof(long[]))
                        {
                            long[] numArray5 = (long[]) info.GetValue(local, null);
                            OperateResult result11 = readWrite.Write(attribute.Address, numArray5);
                            if (!result11.IsSuccess)
                            {
                                return result11;
                            }
                        }
                        else if (propertyType == typeof(ulong))
                        {
                            ulong num9 = (ulong) info.GetValue(local, null);
                            OperateResult result12 = readWrite.Write(attribute.Address, num9);
                            if (!result12.IsSuccess)
                            {
                                return result12;
                            }
                        }
                        else if (propertyType == typeof(ulong[]))
                        {
                            ulong[] numArray6 = (ulong[]) info.GetValue(local, null);
                            OperateResult result13 = readWrite.Write(attribute.Address, numArray6);
                            if (!result13.IsSuccess)
                            {
                                return result13;
                            }
                        }
                        else if (propertyType == typeof(float))
                        {
                            float num10 = (float) info.GetValue(local, null);
                            OperateResult result14 = readWrite.Write(attribute.Address, num10);
                            if (!result14.IsSuccess)
                            {
                                return result14;
                            }
                        }
                        else if (propertyType == typeof(float[]))
                        {
                            float[] numArray7 = (float[]) info.GetValue(local, null);
                            OperateResult result15 = readWrite.Write(attribute.Address, numArray7);
                            if (!result15.IsSuccess)
                            {
                                return result15;
                            }
                        }
                        else if (propertyType == typeof(double))
                        {
                            double num11 = (double) info.GetValue(local, null);
                            OperateResult result16 = readWrite.Write(attribute.Address, num11);
                            if (!result16.IsSuccess)
                            {
                                return result16;
                            }
                        }
                        else if (propertyType == typeof(double[]))
                        {
                            double[] numArray8 = (double[]) info.GetValue(local, null);
                            OperateResult result17 = readWrite.Write(attribute.Address, numArray8);
                            if (!result17.IsSuccess)
                            {
                                return result17;
                            }
                        }
                        else if (propertyType == typeof(string))
                        {
                            string str = (string) info.GetValue(local, null);
                            OperateResult result18 = readWrite.Write(attribute.Address, str);
                            if (!result18.IsSuccess)
                            {
                                return result18;
                            }
                        }
                        else if (propertyType == typeof(byte[]))
                        {
                            byte[] buffer = (byte[]) info.GetValue(local, null);
                            OperateResult result19 = readWrite.Write(attribute.Address, buffer);
                            if (!result19.IsSuccess)
                            {
                                return result19;
                            }
                        }
                        else if (propertyType == typeof(bool))
                        {
                            bool flag46 = (bool) info.GetValue(local, null);
                            OperateResult result20 = readWrite.Write(attribute.Address, flag46);
                            if (!result20.IsSuccess)
                            {
                                return result20;
                            }
                        }
                        else if (propertyType == typeof(bool[]))
                        {
                            bool[] flagArray = (bool[]) info.GetValue(local, null);
                            OperateResult result21 = readWrite.Write(attribute.Address, flagArray);
                            if (!result21.IsSuccess)
                            {
                                return result21;
                            }
                        }
                    }
                }
            }
            return OperateResult.CreateSuccessResult<T>(local);
        }

        public static OperateResult Write<T>(T data, RedisClient redis) where T: class, new()
        {
            Type type = typeof(T);
            T local = data;
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<PropertyInfoKeyName> list = new List<PropertyInfoKeyName>();
            List<PropertyInfoHashKeyName> list2 = new List<PropertyInfoHashKeyName>();
            foreach (PropertyInfo info in properties)
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(HslRedisKeyAttribute), false);
                if ((customAttributes != null) ? (customAttributes.Length > 0) : false)
                {
                    HslRedisKeyAttribute attribute = (HslRedisKeyAttribute) customAttributes[0];
                    list.Add(new PropertyInfoKeyName(info, attribute.KeyName, info.GetValue(local, null).ToString()));
                }
                else
                {
                    customAttributes = info.GetCustomAttributes(typeof(HslRedisHashFieldAttribute), false);
                    if ((customAttributes != null) ? (customAttributes.Length > 0) : false)
                    {
                        HslRedisHashFieldAttribute attribute2 = (HslRedisHashFieldAttribute) customAttributes[0];
                        list2.Add(new PropertyInfoHashKeyName(info, attribute2.HaskKey, attribute2.Field, info.GetValue(local, null).ToString()));
                    }
                }
            }
            if (list.Count > 0)
            {
                OperateResult result = redis.WriteKey((from m in list select m.KeyName).ToArray<string>(), (from m in list select m.Value).ToArray<string>());
                if (!result.IsSuccess)
                {
                    return result;
                }
            }
            if (list2.Count > 0)
            {
                var enumerable = from m in list2
                    group m by m.KeyName into g
                    select new { 
                        Key = g.Key,
                        Values = g.ToArray<PropertyInfoHashKeyName>()
                    };
                foreach (var type2 in enumerable)
                {
                    if (type2.Values.Length == 1)
                    {
                        OperateResult result3 = redis.WriteHashKey(type2.Key, type2.Values[0].Field, type2.Values[0].Value);
                        if (!result3.IsSuccess)
                        {
                            return result3;
                        }
                    }
                    else
                    {
                        OperateResult result4 = redis.WriteHashKey(type2.Key, (from m in type2.Values select m.Field).ToArray<string>(), (from m in type2.Values select m.Value).ToArray<string>());
                        if (!result4.IsSuccess)
                        {
                            return result4;
                        }
                    }
                }
            }
            return OperateResult.CreateSuccessResult();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HslReflectionHelper.<>c <>9 = new HslReflectionHelper.<>c();
            public static Func<string, short> <>9__4_0;
            public static Func<string, short> <>9__4_1;
            public static Func<string, ulong> <>9__4_10;
            public static Func<string, ulong> <>9__4_11;
            public static Func<string, float> <>9__4_12;
            public static Func<string, float> <>9__4_13;
            public static Func<string, double> <>9__4_14;
            public static Func<string, double> <>9__4_15;
            public static Func<string, byte> <>9__4_16;
            public static Func<string, byte> <>9__4_17;
            public static Func<string, bool> <>9__4_18;
            public static Func<string, bool> <>9__4_19;
            public static Func<string, ushort> <>9__4_2;
            public static Func<string, ushort> <>9__4_3;
            public static Func<string, int> <>9__4_4;
            public static Func<string, int> <>9__4_5;
            public static Func<string, uint> <>9__4_6;
            public static Func<string, uint> <>9__4_7;
            public static Func<string, long> <>9__4_8;
            public static Func<string, long> <>9__4_9;
            public static Func<JToken, short> <>9__7_0;
            public static Func<JToken, ushort> <>9__7_1;
            public static Func<JToken, DateTime> <>9__7_10;
            public static Func<JToken, int> <>9__7_2;
            public static Func<JToken, uint> <>9__7_3;
            public static Func<JToken, long> <>9__7_4;
            public static Func<JToken, ulong> <>9__7_5;
            public static Func<JToken, float> <>9__7_6;
            public static Func<JToken, double> <>9__7_7;
            public static Func<JToken, bool> <>9__7_8;
            public static Func<JToken, string> <>9__7_9;

            internal short <GetParametersFromJson>b__7_0(JToken m)
            {
                return m.Value<short>();
            }

            internal ushort <GetParametersFromJson>b__7_1(JToken m)
            {
                return m.Value<ushort>();
            }

            internal DateTime <GetParametersFromJson>b__7_10(JToken m)
            {
                return m.Value<DateTime>();
            }

            internal int <GetParametersFromJson>b__7_2(JToken m)
            {
                return m.Value<int>();
            }

            internal uint <GetParametersFromJson>b__7_3(JToken m)
            {
                return m.Value<uint>();
            }

            internal long <GetParametersFromJson>b__7_4(JToken m)
            {
                return m.Value<long>();
            }

            internal ulong <GetParametersFromJson>b__7_5(JToken m)
            {
                return m.Value<ulong>();
            }

            internal float <GetParametersFromJson>b__7_6(JToken m)
            {
                return m.Value<float>();
            }

            internal double <GetParametersFromJson>b__7_7(JToken m)
            {
                return m.Value<double>();
            }

            internal bool <GetParametersFromJson>b__7_8(JToken m)
            {
                return m.Value<bool>();
            }

            internal string <GetParametersFromJson>b__7_9(JToken m)
            {
                return m.Value<string>();
            }

            internal short <SetPropertyObjectValueArray>b__4_0(string m)
            {
                return short.Parse(m);
            }

            internal short <SetPropertyObjectValueArray>b__4_1(string m)
            {
                return short.Parse(m);
            }

            internal ulong <SetPropertyObjectValueArray>b__4_10(string m)
            {
                return ulong.Parse(m);
            }

            internal ulong <SetPropertyObjectValueArray>b__4_11(string m)
            {
                return ulong.Parse(m);
            }

            internal float <SetPropertyObjectValueArray>b__4_12(string m)
            {
                return float.Parse(m);
            }

            internal float <SetPropertyObjectValueArray>b__4_13(string m)
            {
                return float.Parse(m);
            }

            internal double <SetPropertyObjectValueArray>b__4_14(string m)
            {
                return double.Parse(m);
            }

            internal double <SetPropertyObjectValueArray>b__4_15(string m)
            {
                return double.Parse(m);
            }

            internal byte <SetPropertyObjectValueArray>b__4_16(string m)
            {
                return byte.Parse(m);
            }

            internal byte <SetPropertyObjectValueArray>b__4_17(string m)
            {
                return byte.Parse(m);
            }

            internal bool <SetPropertyObjectValueArray>b__4_18(string m)
            {
                return bool.Parse(m);
            }

            internal bool <SetPropertyObjectValueArray>b__4_19(string m)
            {
                return bool.Parse(m);
            }

            internal ushort <SetPropertyObjectValueArray>b__4_2(string m)
            {
                return ushort.Parse(m);
            }

            internal ushort <SetPropertyObjectValueArray>b__4_3(string m)
            {
                return ushort.Parse(m);
            }

            internal int <SetPropertyObjectValueArray>b__4_4(string m)
            {
                return int.Parse(m);
            }

            internal int <SetPropertyObjectValueArray>b__4_5(string m)
            {
                return int.Parse(m);
            }

            internal uint <SetPropertyObjectValueArray>b__4_6(string m)
            {
                return uint.Parse(m);
            }

            internal uint <SetPropertyObjectValueArray>b__4_7(string m)
            {
                return uint.Parse(m);
            }

            internal long <SetPropertyObjectValueArray>b__4_8(string m)
            {
                return long.Parse(m);
            }

            internal long <SetPropertyObjectValueArray>b__4_9(string m)
            {
                return long.Parse(m);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__5<T> where T: class, new()
        {
            public static readonly HslReflectionHelper.<>c__5<T> <>9;
            public static Func<PropertyInfoKeyName, string> <>9__5_0;
            public static Func<PropertyInfoHashKeyName, string> <>9__5_1;
            public static Func<IGrouping<string, PropertyInfoHashKeyName>, <>f__AnonymousType0<string, PropertyInfoHashKeyName[]>> <>9__5_2;
            public static Func<PropertyInfoHashKeyName, string> <>9__5_3;

            static <>c__5()
            {
                HslReflectionHelper.<>c__5<T>.<>9 = new HslReflectionHelper.<>c__5<T>();
            }

            internal string <Read>b__5_0(PropertyInfoKeyName m)
            {
                return m.KeyName;
            }

            internal string <Read>b__5_1(PropertyInfoHashKeyName m)
            {
                return m.KeyName;
            }

            internal <>f__AnonymousType0<string, PropertyInfoHashKeyName[]> <Read>b__5_2(IGrouping<string, PropertyInfoHashKeyName> g)
            {
                return new { 
                    Key = g.Key,
                    Values = g.ToArray<PropertyInfoHashKeyName>()
                };
            }

            internal string <Read>b__5_3(PropertyInfoHashKeyName m)
            {
                return m.Field;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__6<T> where T: class, new()
        {
            public static readonly HslReflectionHelper.<>c__6<T> <>9;
            public static Func<PropertyInfoKeyName, string> <>9__6_0;
            public static Func<PropertyInfoKeyName, string> <>9__6_1;
            public static Func<PropertyInfoHashKeyName, string> <>9__6_2;
            public static Func<IGrouping<string, PropertyInfoHashKeyName>, <>f__AnonymousType0<string, PropertyInfoHashKeyName[]>> <>9__6_3;
            public static Func<PropertyInfoHashKeyName, string> <>9__6_4;
            public static Func<PropertyInfoHashKeyName, string> <>9__6_5;

            static <>c__6()
            {
                HslReflectionHelper.<>c__6<T>.<>9 = new HslReflectionHelper.<>c__6<T>();
            }

            internal string <Write>b__6_0(PropertyInfoKeyName m)
            {
                return m.KeyName;
            }

            internal string <Write>b__6_1(PropertyInfoKeyName m)
            {
                return m.Value;
            }

            internal string <Write>b__6_2(PropertyInfoHashKeyName m)
            {
                return m.KeyName;
            }

            internal <>f__AnonymousType0<string, PropertyInfoHashKeyName[]> <Write>b__6_3(IGrouping<string, PropertyInfoHashKeyName> g)
            {
                return new { 
                    Key = g.Key,
                    Values = g.ToArray<PropertyInfoHashKeyName>()
                };
            }

            internal string <Write>b__6_4(PropertyInfoHashKeyName m)
            {
                return m.Field;
            }

            internal string <Write>b__6_5(PropertyInfoHashKeyName m)
            {
                return m.Value;
            }
        }
    }
}

