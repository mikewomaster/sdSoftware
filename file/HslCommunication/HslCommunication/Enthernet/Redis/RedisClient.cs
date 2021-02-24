namespace HslCommunication.Enthernet.Redis
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.Net;
    using HslCommunication.Reflection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public class RedisClient : NetworkDoubleBase
    {
        private int dbBlock;
        private string password;
        private RedisSubscribe redisSubscribe;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event RedisMessageReceiveDelegate OnRedisMessageReceived;

        public RedisClient(string password)
        {
            this.password = string.Empty;
            this.dbBlock = 0;
            base.ByteTransform = new RegularByteTransform();
            base.ReceiveTimeOut = 0x7530;
            this.password = password;
        }

        public RedisClient(string ipAddress, int port, string password)
        {
            this.password = string.Empty;
            this.dbBlock = 0;
            base.ByteTransform = new RegularByteTransform();
            this.IpAddress = ipAddress;
            this.Port = port;
            base.ReceiveTimeOut = 0x7530;
            this.password = password;
        }

        public OperateResult<int> AppendKey(string key, string value)
        {
            string[] commands = new string[] { "APPEND", key, value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult ChangePassword(string password)
        {
            string[] commands = new string[] { "CONFIG", "SET", "requirepass", password };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult<long> DBSize()
        {
            string[] commands = new string[] { "DBSIZE" };
            return this.OperateLongNumberFromServer(commands);
        }

        public OperateResult<long> DecrementKey(string key)
        {
            string[] commands = new string[] { "DECR", key };
            return this.OperateLongNumberFromServer(commands);
        }

        public OperateResult<long> DecrementKey(string key, long value)
        {
            string[] commands = new string[] { "DECRBY", key, value.ToString() };
            return this.OperateLongNumberFromServer(commands);
        }

        public OperateResult<int> DeleteHashKey(string key, string field)
        {
            string[] fields = new string[] { field };
            return this.DeleteHashKey(key, fields);
        }

        public OperateResult<int> DeleteHashKey(string key, string[] fields)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("HDEL", key, fields));
        }

        public OperateResult<int> DeleteKey(string[] keys)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("DEL", keys));
        }

        public OperateResult<int> DeleteKey(string key)
        {
            string[] keys = new string[] { key };
            return this.DeleteKey(keys);
        }

        public OperateResult<int> ExistsHashKey(string key, string field)
        {
            string[] commands = new string[] { "HEXISTS", key, field };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<int> ExistsKey(string key)
        {
            string[] commands = new string[] { "EXISTS", key };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<int> ExpireKey(string key, int seconds)
        {
            string[] commands = new string[] { "EXPIRE", key, seconds.ToString() };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult FlushDB()
        {
            string[] commands = new string[] { "FLUSHDB" };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult<int> GetListLength(string key)
        {
            string[] commands = new string[] { "LLEN", key };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<long> IncrementHashKey(string key, string field, long value)
        {
            string[] commands = new string[] { "HINCRBY", key, field, value.ToString() };
            return this.OperateLongNumberFromServer(commands);
        }

        public OperateResult<string> IncrementHashKey(string key, string field, float value)
        {
            string[] commands = new string[] { "HINCRBYFLOAT", key, field, value.ToString() };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<long> IncrementKey(string key)
        {
            string[] commands = new string[] { "INCR", key };
            return this.OperateLongNumberFromServer(commands);
        }

        public OperateResult<long> IncrementKey(string key, long value)
        {
            string[] commands = new string[] { "INCRBY", key, value.ToString() };
            return this.OperateLongNumberFromServer(commands);
        }

        public OperateResult<string> IncrementKey(string key, float value)
        {
            string[] commands = new string[] { "INCRBYFLOAT", key, value.ToString() };
            return this.OperateStringFromServer(commands);
        }

        protected override OperateResult InitializationOnConnect(Socket socket)
        {
            if (!string.IsNullOrEmpty(this.password))
            {
                string[] commands = new string[] { "AUTH", this.password };
                byte[] send = RedisHelper.PackStringCommand(commands);
                OperateResult<byte[]> result = this.ReadFromCoreServer(socket, send, true, true);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<string>(result);
                }
                string msg = Encoding.UTF8.GetString(result.Content);
                if (!msg.StartsWith("+"))
                {
                    return new OperateResult<string>(msg);
                }
            }
            if (this.dbBlock > 0)
            {
                string[] textArray2 = new string[] { "SELECT", this.dbBlock.ToString() };
                byte[] buffer2 = RedisHelper.PackStringCommand(textArray2);
                OperateResult<byte[]> result3 = this.ReadFromCoreServer(socket, buffer2, true, true);
                if (!result3.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<string>(result3);
                }
                string str2 = Encoding.UTF8.GetString(result3.Content);
                if (!str2.StartsWith("+"))
                {
                    return new OperateResult<string>(str2);
                }
            }
            return base.InitializationOnConnect(socket);
        }

        public OperateResult<int> ListInsertAfter(string key, string value, string pivot)
        {
            string[] commands = new string[] { "LINSERT", key, "AFTER", pivot, value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<int> ListInsertBefore(string key, string value, string pivot)
        {
            string[] commands = new string[] { "LINSERT", key, "BEFORE", pivot, value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string> ListLeftPop(string key)
        {
            string[] commands = new string[] { "LPOP", key };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<int> ListLeftPush(string key, string value)
        {
            string[] values = new string[] { value };
            return this.ListLeftPush(key, values);
        }

        public OperateResult<int> ListLeftPush(string key, string[] values)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("LPUSH", key, values));
        }

        public OperateResult<int> ListLeftPushX(string key, string value)
        {
            string[] commands = new string[] { "LPUSHX", key, value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string[]> ListRange(string key, long start, long stop)
        {
            string[] commands = new string[] { "LRANGE", key, start.ToString(), stop.ToString() };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<int> ListRemoveElementMatch(string key, long count, string value)
        {
            string[] commands = new string[] { "LREM", key, count.ToString(), value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string> ListRightPop(string key)
        {
            string[] commands = new string[] { "RPOP", key };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<string> ListRightPopLeftPush(string key1, string key2)
        {
            string[] commands = new string[] { "RPOPLPUSH", key1, key2 };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<int> ListRightPush(string key, string value)
        {
            string[] values = new string[] { value };
            return this.ListRightPush(key, values);
        }

        public OperateResult<int> ListRightPush(string key, string[] values)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("RPUSH", key, values));
        }

        public OperateResult<int> ListRightPushX(string key, string value)
        {
            string[] commands = new string[] { "RPUSHX", key, value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult ListSet(string key, long index, string value)
        {
            string[] commands = new string[] { "LSET", key.ToString(), index.ToString(), value };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult ListTrim(string key, long start, long end)
        {
            string[] commands = new string[] { "LTRIM", key, start.ToString(), end.ToString() };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult MoveKey(string key, int db)
        {
            string[] commands = new string[] { "MOVE", key, db.ToString() };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult<long> OperateLongNumberFromServer(string[] commands)
        {
            byte[] send = RedisHelper.PackStringCommand(commands);
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<long>(result);
            }
            string msg = Encoding.UTF8.GetString(result.Content);
            if (!msg.StartsWith(":"))
            {
                return new OperateResult<long>(msg);
            }
            return RedisHelper.GetLongNumberFromCommandLine(result.Content);
        }

        public OperateResult<int> OperateNumberFromServer(string[] commands)
        {
            byte[] send = RedisHelper.PackStringCommand(commands);
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<int>(result);
            }
            string msg = Encoding.UTF8.GetString(result.Content);
            if (!msg.StartsWith(":"))
            {
                return new OperateResult<int>(msg);
            }
            return RedisHelper.GetNumberFromCommandLine(result.Content);
        }

        public OperateResult<string> OperateStatusFromServer(string[] commands)
        {
            byte[] send = RedisHelper.PackStringCommand(commands);
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            string msg = Encoding.UTF8.GetString(result.Content);
            if (!msg.StartsWith("+"))
            {
                return new OperateResult<string>(msg);
            }
            char[] trimChars = new char[] { '\r', '\n' };
            return OperateResult.CreateSuccessResult<string>(msg.Substring(1).TrimEnd(trimChars));
        }

        public OperateResult<string> OperateStringFromServer(string[] commands)
        {
            byte[] send = RedisHelper.PackStringCommand(commands);
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            return RedisHelper.GetStringFromCommandLine(result.Content);
        }

        public OperateResult<string[]> OperateStringsFromServer(string[] commands)
        {
            byte[] send = RedisHelper.PackStringCommand(commands);
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string[]>(result);
            }
            return RedisHelper.GetStringsFromCommandLine(result.Content);
        }

        public OperateResult<int> PersistKey(string key)
        {
            string[] commands = new string[] { "PERSIST", key };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult Ping()
        {
            string[] commands = new string[] { "PING" };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult<int> Publish(string channel, string message)
        {
            string[] commands = new string[] { "PUBLISH", channel, message };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<T> Read<T>() where T: class, new()
        {
            return HslReflectionHelper.Read<T>(this);
        }

        public OperateResult<string[]> ReadAllKeys(string pattern)
        {
            string[] commands = new string[] { "KEYS", pattern };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<string> ReadAndWriteKey(string key, string value)
        {
            string[] commands = new string[] { "GETSET", key, value };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<string> ReadCustomer(string command)
        {
            char[] separator = new char[] { ' ' };
            byte[] send = RedisHelper.PackStringCommand(command.Split(separator));
            OperateResult<byte[]> result = base.ReadFromCoreServer(send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            return OperateResult.CreateSuccessResult<string>(Encoding.UTF8.GetString(result.Content));
        }

        public override OperateResult<byte[]> ReadFromCoreServer(Socket socket, byte[] send, [Optional, DefaultParameterValue(true)] bool hasResponseData, [Optional, DefaultParameterValue(true)] bool usePackHeader)
        {
            OperateResult result = base.Send(socket, send);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            if (base.ReceiveTimeOut < 0)
            {
                return OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
            }
            return base.ReceiveRedisCommand(socket);
        }

        public OperateResult<string> ReadHashKey(string key, string field)
        {
            string[] commands = new string[] { "HGET", key, field };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<string[]> ReadHashKey(string key, string[] fields)
        {
            return this.OperateStringsFromServer(SoftBasic.SpliceStringArray("HMGET", key, fields));
        }

        public OperateResult<string[]> ReadHashKeyAll(string key)
        {
            string[] commands = new string[] { "HGETALL", key };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<int> ReadHashKeyLength(string key)
        {
            string[] commands = new string[] { "HLEN", key };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string[]> ReadHashKeys(string key)
        {
            string[] commands = new string[] { "HKEYS", key };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<string[]> ReadHashValues(string key)
        {
            string[] commands = new string[] { "HVALS", key };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<string> ReadKey(string key)
        {
            string[] commands = new string[] { "GET", key };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<string[]> ReadKey(string[] keys)
        {
            return this.OperateStringsFromServer(SoftBasic.SpliceStringArray("MGET", keys));
        }

        public OperateResult<int> ReadKeyLength(string key)
        {
            string[] commands = new string[] { "STRLEN", key };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string> ReadKeyRange(string key, int start, int end)
        {
            string[] commands = new string[] { "GETRANGE", key, start.ToString(), end.ToString() };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<int> ReadKeyTTL(string key)
        {
            string[] commands = new string[] { "TTL", key };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string> ReadKeyType(string key)
        {
            string[] commands = new string[] { "TYPE", key };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult<string> ReadListByIndex(string key, long index)
        {
            string[] commands = new string[] { "LINDEX", key, index.ToString() };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<string> ReadRandomKey()
        {
            string[] commands = new string[] { "RANDOMKEY" };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<DateTime> ReadServerTime()
        {
            string[] commands = new string[] { "TIME" };
            OperateResult<string[]> result = this.OperateStringsFromServer(commands);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<DateTime>(result);
            }
            long num = long.Parse(result.Content[0]);
            DateTime time2 = new DateTime(0x7b2, 1, 1, 8, 0, 0);
            return OperateResult.CreateSuccessResult<DateTime>(time2.AddSeconds((double) num));
        }

        private RedisSubscribe RedisSubscribeInitialize()
        {
            RedisSubscribe subscribe = new RedisSubscribe(this.IpAddress, this.Port) {
                Password = this.password
            };
            subscribe.OnRedisMessageReceived += delegate (string topic, string message) {
                if (this.OnRedisMessageReceived != null)
                {
                    RedisMessageReceiveDelegate onRedisMessageReceived = this.OnRedisMessageReceived;
                    onRedisMessageReceived(topic, message);
                }
                else
                {
                    RedisMessageReceiveDelegate expressionStack_9_0 = this.OnRedisMessageReceived;
                }
            };
            return subscribe;
        }

        public OperateResult RenameKey(string key1, string key2)
        {
            string[] commands = new string[] { "RENAME", key1, key2 };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult Save()
        {
            string[] commands = new string[] { "SAVE" };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult SaveAsync()
        {
            string[] commands = new string[] { "BGSAVE" };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult SelectDB(int db)
        {
            string[] commands = new string[] { "SELECT", db.ToString() };
            OperateResult result = this.OperateStatusFromServer(commands);
            if (result.IsSuccess)
            {
                this.dbBlock = db;
            }
            return result;
        }

        public OperateResult<int> SetAdd(string key, string member)
        {
            string[] members = new string[] { member };
            return this.SetAdd(key, members);
        }

        public OperateResult<int> SetAdd(string key, string[] members)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("SADD", key, members));
        }

        public OperateResult<int> SetCard(string key)
        {
            string[] commands = new string[] { "SCARD", key };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string[]> SetDiff(string key, string diffKey)
        {
            string[] diffKeys = new string[] { diffKey };
            return this.SetDiff(key, diffKeys);
        }

        public OperateResult<string[]> SetDiff(string key, string[] diffKeys)
        {
            return this.OperateStringsFromServer(SoftBasic.SpliceStringArray("SDIFF", key, diffKeys));
        }

        public OperateResult<int> SetDiffStore(string destination, string key, string diffKey)
        {
            string[] diffKeys = new string[] { diffKey };
            return this.SetDiffStore(destination, key, diffKeys);
        }

        public OperateResult<int> SetDiffStore(string destination, string key, string[] diffKeys)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("SDIFFSTORE", destination, key, diffKeys));
        }

        public OperateResult<string[]> SetInter(string key, string interKey)
        {
            string[] interKeys = new string[] { interKey };
            return this.SetInter(key, interKeys);
        }

        public OperateResult<string[]> SetInter(string key, string[] interKeys)
        {
            return this.OperateStringsFromServer(SoftBasic.SpliceStringArray("SINTER", key, interKeys));
        }

        public OperateResult<int> SetInterStore(string destination, string key, string interKey)
        {
            string[] interKeys = new string[] { interKey };
            return this.SetInterStore(destination, key, interKeys);
        }

        public OperateResult<int> SetInterStore(string destination, string key, string[] interKeys)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("SINTERSTORE", destination, key, interKeys));
        }

        public OperateResult<int> SetIsMember(string key, string member)
        {
            string[] commands = new string[] { "SISMEMBER", key, member };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string[]> SetMembers(string key)
        {
            string[] commands = new string[] { "SMEMBERS", key };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<int> SetMove(string source, string destination, string member)
        {
            string[] commands = new string[] { "SMOVE", source, destination, member };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string> SetPop(string key)
        {
            string[] commands = new string[] { "SPOP", key };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<string> SetRandomMember(string key)
        {
            string[] commands = new string[] { "SRANDMEMBER", key };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<string[]> SetRandomMember(string key, int count)
        {
            string[] commands = new string[] { "SRANDMEMBER", key, count.ToString() };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<int> SetRemove(string key, string member)
        {
            string[] members = new string[] { member };
            return this.SetRemove(key, members);
        }

        public OperateResult<int> SetRemove(string key, string[] members)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("SREM", key, members));
        }

        public OperateResult<string[]> SetUnion(string key, string unionKey)
        {
            string[] unionKeys = new string[] { unionKey };
            return this.SetUnion(key, unionKeys);
        }

        public OperateResult<string[]> SetUnion(string key, string[] unionKeys)
        {
            return this.OperateStringsFromServer(SoftBasic.SpliceStringArray("SUNION", key, unionKeys));
        }

        public OperateResult<int> SetUnionStore(string destination, string key, string unionKey)
        {
            return this.SetUnionStore(destination, key, unionKey);
        }

        public OperateResult<int> SetUnionStore(string destination, string key, string[] unionKeys)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("SUNIONSTORE", destination, key, unionKeys));
        }

        public OperateResult SubscribeMessage(string topic)
        {
            string[] topics = new string[] { topic };
            return this.SubscribeMessage(topics);
        }

        public OperateResult SubscribeMessage(string[] topics)
        {
            if (this.redisSubscribe == null)
            {
                this.redisSubscribe = this.RedisSubscribeInitialize();
            }
            return this.redisSubscribe.SubscribeMessage(topics);
        }

        public override string ToString()
        {
            return string.Format("RedisClient[{0}:{1}]", this.IpAddress, this.Port);
        }

        public OperateResult UnSubscribeMessage(string topic)
        {
            string[] topics = new string[] { topic };
            return this.UnSubscribeMessage(topics);
        }

        public OperateResult UnSubscribeMessage(string[] topics)
        {
            if (this.redisSubscribe == null)
            {
                this.redisSubscribe = this.RedisSubscribeInitialize();
            }
            return this.redisSubscribe.UnSubscribeMessage(topics);
        }

        public OperateResult Write<T>(T data) where T: class, new()
        {
            return HslReflectionHelper.Write<T>(data, this);
        }

        public OperateResult WriteAndPublishKey(string key, string value)
        {
            OperateResult result = this.WriteKey(key, value);
            if (!result.IsSuccess)
            {
                return result;
            }
            return this.Publish(key, value);
        }

        public OperateResult WriteExpireKey(string key, string value, long seconds)
        {
            string[] commands = new string[] { "SETEX", key, seconds.ToString(), value };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult WriteHashKey(string key, string[] fields, string[] values)
        {
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (fields.Length != values.Length)
            {
                throw new ArgumentException("Two arguement not same length");
            }
            List<string> list = new List<string> {
                "HMSET",
                key
            };
            for (int i = 0; i < fields.Length; i++)
            {
                list.Add(fields[i]);
                list.Add(values[i]);
            }
            return this.OperateStatusFromServer(list.ToArray());
        }

        public OperateResult<int> WriteHashKey(string key, string field, string value)
        {
            string[] commands = new string[] { "HSET", key, field, value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<int> WriteHashKeyNx(string key, string field, string value)
        {
            string[] commands = new string[] { "HSETNX", key, field, value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult WriteKey(string[] keys, string[] values)
        {
            if (keys == null)
            {
                throw new ArgumentNullException("keys");
            }
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (keys.Length != values.Length)
            {
                throw new ArgumentException("Two arguement not same length");
            }
            List<string> list = new List<string> { "MSET" };
            for (int i = 0; i < keys.Length; i++)
            {
                list.Add(keys[i]);
                list.Add(values[i]);
            }
            return this.OperateStatusFromServer(list.ToArray());
        }

        public OperateResult WriteKey(string key, string value)
        {
            string[] commands = new string[] { "SET", key, value };
            return this.OperateStatusFromServer(commands);
        }

        public OperateResult<int> WriteKeyIfNotExists(string key, string value)
        {
            string[] commands = new string[] { "SETNX", key, value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<int> WriteKeyRange(string key, string value, int offset)
        {
            string[] commands = new string[] { "SETRANGE", key, offset.ToString(), value };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<int> ZSetAdd(string key, string member, double score)
        {
            string[] members = new string[] { member };
            double[] scores = new double[] { score };
            return this.ZSetAdd(key, members, scores);
        }

        public OperateResult<int> ZSetAdd(string key, string[] members, double[] scores)
        {
            if (members.Length != scores.Length)
            {
                throw new Exception(StringResources.Language.TwoParametersLengthIsNotSame);
            }
            List<string> list = new List<string> {
                "ZADD",
                key
            };
            for (int i = 0; i < members.Length; i++)
            {
                list.Add(scores[i].ToString());
                list.Add(members[i]);
            }
            return this.OperateNumberFromServer(list.ToArray());
        }

        public OperateResult<int> ZSetCard(string key)
        {
            string[] commands = new string[] { "ZCARD", key };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<int> ZSetCount(string key, double min, double max)
        {
            string[] commands = new string[] { "ZCOUNT", key, min.ToString(), max.ToString() };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string> ZSetIncreaseBy(string key, string member, double increment)
        {
            string[] commands = new string[] { "ZINCRBY", key, increment.ToString(), member };
            return this.OperateStringFromServer(commands);
        }

        public OperateResult<string[]> ZSetRange(string key, int start, int stop, [Optional, DefaultParameterValue(false)] bool withScore)
        {
            if (withScore)
            {
                string[] textArray1 = new string[] { "ZRANGE", key, start.ToString(), stop.ToString(), "WITHSCORES" };
                return this.OperateStringsFromServer(textArray1);
            }
            string[] commands = new string[] { "ZRANGE", key, start.ToString(), stop.ToString() };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<string[]> ZSetRangeByScore(string key, string min, string max, [Optional, DefaultParameterValue(false)] bool withScore)
        {
            if (withScore)
            {
                string[] textArray1 = new string[] { "ZRANGEBYSCORE", key, min, max, "WITHSCORES" };
                return this.OperateStringsFromServer(textArray1);
            }
            string[] commands = new string[] { "ZRANGEBYSCORE", key, min, max };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<int> ZSetRank(string key, string member)
        {
            string[] commands = new string[] { "ZRANK", key, member };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<int> ZSetRemove(string key, string member)
        {
            string[] members = new string[] { member };
            return this.ZSetRemove(key, members);
        }

        public OperateResult<int> ZSetRemove(string key, string[] members)
        {
            return this.OperateNumberFromServer(SoftBasic.SpliceStringArray("ZREM", key, members));
        }

        public OperateResult<int> ZSetRemoveRangeByRank(string key, int start, int stop)
        {
            string[] commands = new string[] { "ZREMRANGEBYRANK", key, start.ToString(), stop.ToString() };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<int> ZSetRemoveRangeByScore(string key, string min, string max)
        {
            string[] commands = new string[] { "ZREMRANGEBYSCORE", key, min, max };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string[]> ZSetReverseRange(string key, int start, int stop, [Optional, DefaultParameterValue(false)] bool withScore)
        {
            if (withScore)
            {
                string[] textArray1 = new string[] { "ZREVRANGE", key, start.ToString(), stop.ToString(), "WITHSCORES" };
                return this.OperateStringsFromServer(textArray1);
            }
            string[] commands = new string[] { "ZREVRANGE", key, start.ToString(), stop.ToString() };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<string[]> ZSetReverseRangeByScore(string key, string max, string min, [Optional, DefaultParameterValue(false)] bool withScore)
        {
            if (withScore)
            {
                string[] textArray1 = new string[] { "ZREVRANGEBYSCORE", key, max, min, "WITHSCORES" };
                return this.OperateStringsFromServer(textArray1);
            }
            string[] commands = new string[] { "ZREVRANGEBYSCORE", key, max, min };
            return this.OperateStringsFromServer(commands);
        }

        public OperateResult<int> ZSetReverseRank(string key, string member)
        {
            string[] commands = new string[] { "ZREVRANK", key, member };
            return this.OperateNumberFromServer(commands);
        }

        public OperateResult<string> ZSetScore(string key, string member)
        {
            string[] commands = new string[] { "ZSCORE", key, member };
            return this.OperateStringFromServer(commands);
        }

        public delegate void RedisMessageReceiveDelegate(string topic, string message);
    }
}

