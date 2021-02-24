namespace HslCommunication.Enthernet.Redis
{
    using HslCommunication;
    using HslCommunication.Algorithms.ConnectPool;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class RedisClientPool
    {
        private HslCommunication.Algorithms.ConnectPool.ConnectPool<IRedisConnector> redisConnectPool;

        public RedisClientPool(string ipAddress, int port, string password)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                throw new Exception(StringResources.Language.InsufficientPrivileges);
            }
            RedisClient redis = new RedisClient(ipAddress, port, password);
            this.redisConnectPool = new HslCommunication.Algorithms.ConnectPool.ConnectPool<IRedisConnector>(() => new IRedisConnector { Redis = redis });
            this.redisConnectPool.MaxConnector = 0x7fffffff;
        }

        public RedisClientPool(string ipAddress, int port, string password, Action<RedisClient> initialize)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                throw new Exception(StringResources.Language.InsufficientPrivileges);
            }
            RedisClient redis = new RedisClient(ipAddress, port, password);
            initialize(redis);
            this.redisConnectPool = new HslCommunication.Algorithms.ConnectPool.ConnectPool<IRedisConnector>(() => new IRedisConnector { Redis = redis });
            this.redisConnectPool.MaxConnector = 0x7fffffff;
        }

        public OperateResult<int> AppendKey(string key, string value)
        {
            return this.ConnectPoolExecute<int>(m => m.AppendKey(key, value));
        }

        public OperateResult ChangePassword(string password)
        {
            return this.ConnectPoolExecute(m => m.ChangePassword(password));
        }

        private OperateResult ConnectPoolExecute(Func<RedisClient, OperateResult> exec)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                throw new Exception(StringResources.Language.InsufficientPrivileges);
            }
            IRedisConnector availableConnector = this.redisConnectPool.GetAvailableConnector();
            OperateResult result = exec(availableConnector.Redis);
            this.redisConnectPool.ReturnConnector(availableConnector);
            return result;
        }

        private OperateResult<T> ConnectPoolExecute<T>(Func<RedisClient, OperateResult<T>> exec)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                throw new Exception(StringResources.Language.InsufficientPrivileges);
            }
            IRedisConnector availableConnector = this.redisConnectPool.GetAvailableConnector();
            OperateResult<T> result = exec(availableConnector.Redis);
            this.redisConnectPool.ReturnConnector(availableConnector);
            return result;
        }

        public OperateResult<long> DBSize()
        {
            return this.ConnectPoolExecute<long>(m => m.DBSize());
        }

        public OperateResult<long> DecrementKey(string key)
        {
            return this.ConnectPoolExecute<long>(m => m.DecrementKey(key));
        }

        public OperateResult<long> DecrementKey(string key, long value)
        {
            return this.ConnectPoolExecute<long>(m => m.DecrementKey(key, value));
        }

        public OperateResult<int> DeleteHashKey(string key, string field)
        {
            return this.ConnectPoolExecute<int>(m => m.DeleteHashKey(key, field));
        }

        public OperateResult<int> DeleteHashKey(string key, string[] fields)
        {
            return this.ConnectPoolExecute<int>(m => m.DeleteHashKey(key, fields));
        }

        public OperateResult<int> DeleteKey(string[] keys)
        {
            return this.ConnectPoolExecute<int>(m => m.DeleteKey(keys));
        }

        public OperateResult<int> DeleteKey(string key)
        {
            return this.ConnectPoolExecute<int>(m => m.DeleteKey(key));
        }

        public OperateResult<int> ExistsHashKey(string key, string field)
        {
            return this.ConnectPoolExecute<int>(m => m.ExistsHashKey(key, field));
        }

        public OperateResult<int> ExistsKey(string key)
        {
            return this.ConnectPoolExecute<int>(m => m.ExistsKey(key));
        }

        public OperateResult<int> ExpireKey(string key, int seconds)
        {
            return this.ConnectPoolExecute<int>(m => m.ExpireKey(key, seconds));
        }

        public OperateResult FlushDB()
        {
            return this.ConnectPoolExecute(m => m.FlushDB());
        }

        public OperateResult<int> GetListLength(string key)
        {
            return this.ConnectPoolExecute<int>(m => m.GetListLength(key));
        }

        public OperateResult<long> IncrementHashKey(string key, string field, long value)
        {
            return this.ConnectPoolExecute<long>(m => m.IncrementHashKey(key, field, value));
        }

        public OperateResult<string> IncrementHashKey(string key, string field, float value)
        {
            return this.ConnectPoolExecute<string>(m => m.IncrementHashKey(key, field, value));
        }

        public OperateResult<long> IncrementKey(string key)
        {
            return this.ConnectPoolExecute<long>(m => m.IncrementKey(key));
        }

        public OperateResult<long> IncrementKey(string key, long value)
        {
            return this.ConnectPoolExecute<long>(m => m.IncrementKey(key, value));
        }

        public OperateResult<string> IncrementKey(string key, float value)
        {
            return this.ConnectPoolExecute<string>(m => m.IncrementKey(key, value));
        }

        public OperateResult<int> ListInsertAfter(string key, string value, string pivot)
        {
            return this.ConnectPoolExecute<int>(m => m.ListInsertAfter(key, value, pivot));
        }

        public OperateResult<int> ListInsertBefore(string key, string value, string pivot)
        {
            return this.ConnectPoolExecute<int>(m => m.ListInsertBefore(key, value, pivot));
        }

        public OperateResult<string> ListLeftPop(string key)
        {
            return this.ConnectPoolExecute<string>(m => m.ListLeftPop(key));
        }

        public OperateResult<int> ListLeftPush(string key, string value)
        {
            return this.ConnectPoolExecute<int>(m => m.ListLeftPush(key, value));
        }

        public OperateResult<int> ListLeftPush(string key, string[] values)
        {
            return this.ConnectPoolExecute<int>(m => m.ListLeftPush(key, values));
        }

        public OperateResult<int> ListLeftPushX(string key, string value)
        {
            return this.ConnectPoolExecute<int>(m => m.ListLeftPushX(key, value));
        }

        public OperateResult<string[]> ListRange(string key, long start, long stop)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ListRange(key, start, stop));
        }

        public OperateResult<int> ListRemoveElementMatch(string key, long count, string value)
        {
            return this.ConnectPoolExecute<int>(m => m.ListRemoveElementMatch(key, count, value));
        }

        public OperateResult<string> ListRightPop(string key)
        {
            return this.ConnectPoolExecute<string>(m => m.ListRightPop(key));
        }

        public OperateResult<string> ListRightPopLeftPush(string key1, string key2)
        {
            return this.ConnectPoolExecute<string>(m => m.ListRightPopLeftPush(key1, key2));
        }

        public OperateResult<int> ListRightPush(string key, string value)
        {
            return this.ConnectPoolExecute<int>(m => m.ListRightPush(key, value));
        }

        public OperateResult<int> ListRightPush(string key, string[] values)
        {
            return this.ConnectPoolExecute<int>(m => m.ListRightPush(key, values));
        }

        public OperateResult<int> ListRightPushX(string key, string value)
        {
            return this.ConnectPoolExecute<int>(m => m.ListRightPushX(key, value));
        }

        public OperateResult ListSet(string key, long index, string value)
        {
            return this.ConnectPoolExecute(m => m.ListSet(key, index, value));
        }

        public OperateResult ListTrim(string key, long start, long end)
        {
            return this.ConnectPoolExecute(m => m.ListTrim(key, start, end));
        }

        public OperateResult MoveKey(string key, int db)
        {
            return this.ConnectPoolExecute(m => m.MoveKey(key, db));
        }

        public OperateResult<int> PersistKey(string key)
        {
            return this.ConnectPoolExecute<int>(m => m.PersistKey(key));
        }

        public OperateResult Ping()
        {
            return this.ConnectPoolExecute(m => m.Ping());
        }

        public OperateResult<int> Publish(string channel, string message)
        {
            return this.ConnectPoolExecute<int>(m => m.Publish(channel, message));
        }

        public OperateResult<T> Read<T>() where T: class, new()
        {
            return this.ConnectPoolExecute<T>(m => m.Read<T>());
        }

        public OperateResult<string[]> ReadAllKeys(string pattern)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ReadAllKeys(pattern));
        }

        public OperateResult<string> ReadAndWriteKey(string key, string value)
        {
            return this.ConnectPoolExecute<string>(m => m.ReadAndWriteKey(key, value));
        }

        public OperateResult<string> ReadHashKey(string key, string field)
        {
            return this.ConnectPoolExecute<string>(m => m.ReadHashKey(key, field));
        }

        public OperateResult<string[]> ReadHashKey(string key, string[] fields)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ReadHashKey(key, fields));
        }

        public OperateResult<string[]> ReadHashKeyAll(string key)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ReadHashKeyAll(key));
        }

        public OperateResult<int> ReadHashKeyLength(string key)
        {
            return this.ConnectPoolExecute<int>(m => m.ReadHashKeyLength(key));
        }

        public OperateResult<string[]> ReadHashKeys(string key)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ReadHashKeys(key));
        }

        public OperateResult<string[]> ReadHashValues(string key)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ReadHashValues(key));
        }

        public OperateResult<string> ReadKey(string key)
        {
            return this.ConnectPoolExecute<string>(m => m.ReadKey(key));
        }

        public OperateResult<string[]> ReadKey(string[] keys)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ReadKey(keys));
        }

        public OperateResult<int> ReadKeyLength(string key)
        {
            return this.ConnectPoolExecute<int>(m => m.ReadKeyLength(key));
        }

        public OperateResult<string> ReadKeyRange(string key, int start, int end)
        {
            return this.ConnectPoolExecute<string>(m => m.ReadKeyRange(key, start, end));
        }

        public OperateResult<int> ReadKeyTTL(string key)
        {
            return this.ConnectPoolExecute<int>(m => m.ReadKeyTTL(key));
        }

        public OperateResult<string> ReadKeyType(string key)
        {
            return this.ConnectPoolExecute<string>(m => m.ReadKeyType(key));
        }

        public OperateResult<string> ReadListByIndex(string key, long index)
        {
            return this.ConnectPoolExecute<string>(m => m.ReadListByIndex(key, index));
        }

        public OperateResult<string> ReadRandomKey()
        {
            return this.ConnectPoolExecute<string>(m => m.ReadRandomKey());
        }

        public OperateResult<DateTime> ReadServerTime()
        {
            return this.ConnectPoolExecute<DateTime>(m => m.ReadServerTime());
        }

        public OperateResult RenameKey(string key1, string key2)
        {
            return this.ConnectPoolExecute(m => m.RenameKey(key1, key2));
        }

        public OperateResult Save()
        {
            return this.ConnectPoolExecute(m => m.Save());
        }

        public OperateResult SaveAsync()
        {
            return this.ConnectPoolExecute(m => m.SaveAsync());
        }

        public OperateResult SelectDB(int db)
        {
            return this.ConnectPoolExecute(m => m.SelectDB(db));
        }

        public OperateResult<int> SetAdd(string key, string member)
        {
            return this.ConnectPoolExecute<int>(m => m.SetAdd(key, member));
        }

        public OperateResult<int> SetAdd(string key, string[] members)
        {
            return this.ConnectPoolExecute<int>(m => m.SetAdd(key, members));
        }

        public OperateResult<int> SetCard(string key)
        {
            return this.ConnectPoolExecute<int>(m => m.SetCard(key));
        }

        public OperateResult<string[]> SetDiff(string key, string diffKey)
        {
            return this.ConnectPoolExecute<string[]>(m => m.SetDiff(key, diffKey));
        }

        public OperateResult<string[]> SetDiff(string key, string[] diffKeys)
        {
            return this.ConnectPoolExecute<string[]>(m => m.SetDiff(key, diffKeys));
        }

        public OperateResult<int> SetDiffStore(string destination, string key, string diffKey)
        {
            return this.ConnectPoolExecute<int>(m => m.SetDiffStore(destination, key, diffKey));
        }

        public OperateResult<int> SetDiffStore(string destination, string key, string[] diffKeys)
        {
            return this.ConnectPoolExecute<int>(m => m.SetDiffStore(destination, key, diffKeys));
        }

        public OperateResult<string[]> SetInter(string key, string interKey)
        {
            return this.ConnectPoolExecute<string[]>(m => m.SetInter(key, interKey));
        }

        public OperateResult<string[]> SetInter(string key, string[] interKeys)
        {
            return this.ConnectPoolExecute<string[]>(m => m.SetInter(key, interKeys));
        }

        public OperateResult<int> SetInterStore(string destination, string key, string interKey)
        {
            return this.ConnectPoolExecute<int>(m => m.SetInterStore(destination, key, interKey));
        }

        public OperateResult<int> SetInterStore(string destination, string key, string[] interKeys)
        {
            return this.ConnectPoolExecute<int>(m => m.SetInterStore(destination, key, interKeys));
        }

        public OperateResult<int> SetIsMember(string key, string member)
        {
            return this.ConnectPoolExecute<int>(m => m.SetIsMember(key, member));
        }

        public OperateResult<string[]> SetMembers(string key)
        {
            return this.ConnectPoolExecute<string[]>(m => m.SetMembers(key));
        }

        public OperateResult<int> SetMove(string source, string destination, string member)
        {
            return this.ConnectPoolExecute<int>(m => m.SetMove(source, destination, member));
        }

        public OperateResult<string> SetPop(string key)
        {
            return this.ConnectPoolExecute<string>(m => m.SetPop(key));
        }

        public OperateResult<string> SetRandomMember(string key)
        {
            return this.ConnectPoolExecute<string>(m => m.SetRandomMember(key));
        }

        public OperateResult<string[]> SetRandomMember(string key, int count)
        {
            return this.ConnectPoolExecute<string[]>(m => m.SetRandomMember(key, count));
        }

        public OperateResult<int> SetRemove(string key, string member)
        {
            return this.ConnectPoolExecute<int>(m => m.SetRemove(key, member));
        }

        public OperateResult<int> SetRemove(string key, string[] members)
        {
            return this.ConnectPoolExecute<int>(m => m.SetRemove(key, members));
        }

        public OperateResult<string[]> SetUnion(string key, string unionKey)
        {
            return this.ConnectPoolExecute<string[]>(m => m.SetUnion(key, unionKey));
        }

        public OperateResult<string[]> SetUnion(string key, string[] unionKeys)
        {
            return this.ConnectPoolExecute<string[]>(m => m.SetUnion(key, unionKeys));
        }

        public OperateResult<int> SetUnionStore(string destination, string key, string unionKey)
        {
            return this.ConnectPoolExecute<int>(m => m.SetUnionStore(destination, key, unionKey));
        }

        public OperateResult<int> SetUnionStore(string destination, string key, string[] unionKeys)
        {
            return this.ConnectPoolExecute<int>(m => m.SetUnionStore(destination, key, unionKeys));
        }

        public override string ToString()
        {
            return string.Format("RedisConnectPool[{0}]", this.redisConnectPool.MaxConnector);
        }

        public OperateResult Write<T>(T data) where T: class, new()
        {
            return this.ConnectPoolExecute(m => m.Write<T>(data));
        }

        public OperateResult WriteAndPublishKey(string key, string value)
        {
            return this.ConnectPoolExecute(m => m.WriteAndPublishKey(key, value));
        }

        public OperateResult WriteExpireKey(string key, string value, long seconds)
        {
            return this.ConnectPoolExecute(m => m.WriteExpireKey(key, value, seconds));
        }

        public OperateResult WriteHashKey(string key, string[] fields, string[] values)
        {
            return this.ConnectPoolExecute(m => m.WriteHashKey(key, fields, values));
        }

        public OperateResult<int> WriteHashKey(string key, string field, string value)
        {
            return this.ConnectPoolExecute<int>(m => m.WriteHashKey(key, field, value));
        }

        public OperateResult<int> WriteHashKeyNx(string key, string field, string value)
        {
            return this.ConnectPoolExecute<int>(m => m.WriteHashKeyNx(key, field, value));
        }

        public OperateResult WriteKey(string[] keys, string[] values)
        {
            return this.ConnectPoolExecute(m => m.WriteKey(keys, values));
        }

        public OperateResult WriteKey(string key, string value)
        {
            return this.ConnectPoolExecute(m => m.WriteKey(key, value));
        }

        public OperateResult<int> WriteKeyIfNotExists(string key, string value)
        {
            return this.ConnectPoolExecute<int>(m => m.WriteKeyIfNotExists(key, value));
        }

        public OperateResult<int> WriteKeyRange(string key, string value, int offset)
        {
            return this.ConnectPoolExecute<int>(m => m.WriteKeyRange(key, value, offset));
        }

        public OperateResult<int> ZSetAdd(string key, string member, double score)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetAdd(key, member, score));
        }

        public OperateResult<int> ZSetAdd(string key, string[] members, double[] scores)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetAdd(key, members, scores));
        }

        public OperateResult<int> ZSetCard(string key)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetCard(key));
        }

        public OperateResult<int> ZSetCount(string key, double min, double max)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetCount(key, min, max));
        }

        public OperateResult<string> ZSetIncreaseBy(string key, string member, double increment)
        {
            return this.ConnectPoolExecute<string>(m => m.ZSetIncreaseBy(key, member, increment));
        }

        public OperateResult<string[]> ZSetRange(string key, int start, int stop, [Optional, DefaultParameterValue(false)] bool withScore)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ZSetRange(key, start, stop, withScore));
        }

        public OperateResult<string[]> ZSetRangeByScore(string key, string min, string max, [Optional, DefaultParameterValue(false)] bool withScore)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ZSetRangeByScore(key, min, max, withScore));
        }

        public OperateResult<int> ZSetRank(string key, string member)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetRank(key, member));
        }

        public OperateResult<int> ZSetRemove(string key, string member)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetRemove(key, member));
        }

        public OperateResult<int> ZSetRemove(string key, string[] members)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetRemove(key, members));
        }

        public OperateResult<int> ZSetRemoveRangeByRank(string key, int start, int stop)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetRemoveRangeByRank(key, start, stop));
        }

        public OperateResult<int> ZSetRemoveRangeByScore(string key, string min, string max)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetRemoveRangeByScore(key, min, max));
        }

        public OperateResult<string[]> ZSetReverseRange(string key, int start, int stop, [Optional, DefaultParameterValue(false)] bool withScore)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ZSetReverseRange(key, start, stop, withScore));
        }

        public OperateResult<string[]> ZSetReverseRangeByScore(string key, string max, string min, [Optional, DefaultParameterValue(false)] bool withScore)
        {
            return this.ConnectPoolExecute<string[]>(m => m.ZSetReverseRangeByScore(key, max, min, withScore));
        }

        public OperateResult<int> ZSetReverseRank(string key, string member)
        {
            return this.ConnectPoolExecute<int>(m => m.ZSetReverseRank(key, member));
        }

        public OperateResult<string> ZSetScore(string key, string member)
        {
            return this.ConnectPoolExecute<string>(m => m.ZSetScore(key, member));
        }

        public HslCommunication.Algorithms.ConnectPool.ConnectPool<IRedisConnector> GetRedisConnectPool
        {
            get
            {
                return this.redisConnectPool;
            }
        }

        public int MaxConnector
        {
            get
            {
                return this.redisConnectPool.MaxConnector;
            }
            set
            {
                this.redisConnectPool.MaxConnector = value;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RedisClientPool.<>c <>9 = new RedisClientPool.<>c();
            public static Func<RedisClient, OperateResult> <>9__109_0;
            public static Func<RedisClient, OperateResult> <>9__110_0;
            public static Func<RedisClient, OperateResult<DateTime>> <>9__111_0;
            public static Func<RedisClient, OperateResult> <>9__112_0;
            public static Func<RedisClient, OperateResult<long>> <>9__113_0;
            public static Func<RedisClient, OperateResult> <>9__114_0;
            public static Func<RedisClient, OperateResult<string>> <>9__16_0;

            internal OperateResult<long> <DBSize>b__113_0(RedisClient m)
            {
                return m.DBSize();
            }

            internal OperateResult <FlushDB>b__114_0(RedisClient m)
            {
                return m.FlushDB();
            }

            internal OperateResult <Ping>b__112_0(RedisClient m)
            {
                return m.Ping();
            }

            internal OperateResult<string> <ReadRandomKey>b__16_0(RedisClient m)
            {
                return m.ReadRandomKey();
            }

            internal OperateResult<DateTime> <ReadServerTime>b__111_0(RedisClient m)
            {
                return m.ReadServerTime();
            }

            internal OperateResult <Save>b__109_0(RedisClient m)
            {
                return m.Save();
            }

            internal OperateResult <SaveAsync>b__110_0(RedisClient m)
            {
                return m.SaveAsync();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__107<T> where T: class, new()
        {
            public static readonly RedisClientPool.<>c__107<T> <>9;
            public static Func<RedisClient, OperateResult<T>> <>9__107_0;

            static <>c__107()
            {
                RedisClientPool.<>c__107<T>.<>9 = new RedisClientPool.<>c__107<T>();
            }

            internal OperateResult<T> <Read>b__107_0(RedisClient m)
            {
                return m.Read<T>();
            }
        }
    }
}

