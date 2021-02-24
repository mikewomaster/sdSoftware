namespace HslCommunication.Core.Net
{
    using HslCommunication;
    using HslCommunication.Core;
    using System;
    using System.Threading;

    public class ReadWriteNetHelper
    {
        public static OperateResult<TimeSpan> Wait(IReadWriteNet readWriteNet, string address, bool waitValue, int readInterval, int waitTimeout)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                OperateResult<bool> result = readWriteNet.ReadBool(address);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(result);
                }
                if (result.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult<TimeSpan>((TimeSpan) (DateTime.Now - now));
                }
                if ((waitTimeout > 0) && ((DateTime.Now - now).TotalMilliseconds > waitTimeout))
                {
                    return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout.ToString());
                }
                Thread.Sleep(readInterval);
            }
        }

        public static OperateResult<TimeSpan> Wait(IReadWriteNet readWriteNet, string address, short waitValue, int readInterval, int waitTimeout)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                OperateResult<short> result = readWriteNet.ReadInt16(address);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(result);
                }
                if (result.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult<TimeSpan>((TimeSpan) (DateTime.Now - now));
                }
                if ((waitTimeout > 0) && ((DateTime.Now - now).TotalMilliseconds > waitTimeout))
                {
                    return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout.ToString());
                }
                Thread.Sleep(readInterval);
            }
        }

        public static OperateResult<TimeSpan> Wait(IReadWriteNet readWriteNet, string address, int waitValue, int readInterval, int waitTimeout)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                OperateResult<int> result = readWriteNet.ReadInt32(address);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(result);
                }
                if (result.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult<TimeSpan>((TimeSpan) (DateTime.Now - now));
                }
                if ((waitTimeout > 0) && ((DateTime.Now - now).TotalMilliseconds > waitTimeout))
                {
                    return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout.ToString());
                }
                Thread.Sleep(readInterval);
            }
        }

        public static OperateResult<TimeSpan> Wait(IReadWriteNet readWriteNet, string address, long waitValue, int readInterval, int waitTimeout)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                OperateResult<long> result = readWriteNet.ReadInt64(address);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(result);
                }
                if (result.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult<TimeSpan>((TimeSpan) (DateTime.Now - now));
                }
                if ((waitTimeout > 0) && ((DateTime.Now - now).TotalMilliseconds > waitTimeout))
                {
                    return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout.ToString());
                }
                Thread.Sleep(readInterval);
            }
        }

        public static OperateResult<TimeSpan> Wait(IReadWriteNet readWriteNet, string address, ushort waitValue, int readInterval, int waitTimeout)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                OperateResult<ushort> result = readWriteNet.ReadUInt16(address);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(result);
                }
                if (result.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult<TimeSpan>((TimeSpan) (DateTime.Now - now));
                }
                if ((waitTimeout > 0) && ((DateTime.Now - now).TotalMilliseconds > waitTimeout))
                {
                    return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout.ToString());
                }
                Thread.Sleep(readInterval);
            }
        }

        public static OperateResult<TimeSpan> Wait(IReadWriteNet readWriteNet, string address, uint waitValue, int readInterval, int waitTimeout)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                OperateResult<uint> result = readWriteNet.ReadUInt32(address);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(result);
                }
                if (result.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult<TimeSpan>((TimeSpan) (DateTime.Now - now));
                }
                if ((waitTimeout > 0) && ((DateTime.Now - now).TotalMilliseconds > waitTimeout))
                {
                    return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout.ToString());
                }
                Thread.Sleep(readInterval);
            }
        }

        public static OperateResult<TimeSpan> Wait(IReadWriteNet readWriteNet, string address, ulong waitValue, int readInterval, int waitTimeout)
        {
            DateTime now = DateTime.Now;
            while (true)
            {
                OperateResult<ulong> result = readWriteNet.ReadUInt64(address);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<TimeSpan>(result);
                }
                if (result.Content == waitValue)
                {
                    return OperateResult.CreateSuccessResult<TimeSpan>((TimeSpan) (DateTime.Now - now));
                }
                if ((waitTimeout > 0) && ((DateTime.Now - now).TotalMilliseconds > waitTimeout))
                {
                    return new OperateResult<TimeSpan>(StringResources.Language.CheckDataTimeout + waitTimeout.ToString());
                }
                Thread.Sleep(readInterval);
            }
        }
    }
}

