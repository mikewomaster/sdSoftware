namespace HslCommunication.Enthernet.Redis
{
    using HslCommunication;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RedisHelper
    {
        public static OperateResult<long> GetLongNumberFromCommandLine(byte[] commandLine)
        {
            try
            {
                char[] trimChars = new char[] { '\r', '\n' };
                return OperateResult.CreateSuccessResult<long>(Convert.ToInt64(Encoding.UTF8.GetString(commandLine).TrimEnd(trimChars).Substring(1)));
            }
            catch (Exception exception)
            {
                return new OperateResult<long>(exception.Message);
            }
        }

        public static OperateResult<int> GetNumberFromCommandLine(byte[] commandLine)
        {
            try
            {
                char[] trimChars = new char[] { '\r', '\n' };
                return OperateResult.CreateSuccessResult<int>(Convert.ToInt32(Encoding.UTF8.GetString(commandLine).TrimEnd(trimChars).Substring(1)));
            }
            catch (Exception exception)
            {
                return new OperateResult<int>(exception.Message);
            }
        }

        public static OperateResult<string> GetStringFromCommandLine(byte[] commandLine)
        {
            try
            {
                if (commandLine[0] != 0x24)
                {
                    return new OperateResult<string>(Encoding.UTF8.GetString(commandLine));
                }
                int num = -1;
                int num2 = -1;
                for (int i = 0; i < commandLine.Length; i++)
                {
                    if ((commandLine[i] == 13) || (commandLine[i] == 10))
                    {
                        num = i;
                    }
                    if (commandLine[i] == 10)
                    {
                        num2 = i;
                        break;
                    }
                }
                int count = Convert.ToInt32(Encoding.UTF8.GetString(commandLine, 1, num - 1));
                if (count < 0)
                {
                    return new OperateResult<string>("(nil) None Value");
                }
                return OperateResult.CreateSuccessResult<string>(Encoding.UTF8.GetString(commandLine, num2 + 1, count));
            }
            catch (Exception exception)
            {
                return new OperateResult<string>(exception.Message);
            }
        }

        public static OperateResult<string[]> GetStringsFromCommandLine(byte[] commandLine)
        {
            try
            {
                List<string> list = new List<string>();
                if (commandLine[0] != 0x2a)
                {
                    return new OperateResult<string[]>(Encoding.UTF8.GetString(commandLine));
                }
                int index = 0;
                for (int i = 0; i < commandLine.Length; i++)
                {
                    if ((commandLine[i] == 13) || (commandLine[i] == 10))
                    {
                        index = i;
                        break;
                    }
                }
                int num2 = Convert.ToInt32(Encoding.UTF8.GetString(commandLine, 1, index - 1));
                for (int j = 0; j < num2; j++)
                {
                    int num5 = -1;
                    for (int k = index; k < commandLine.Length; k++)
                    {
                        if (commandLine[k] == 10)
                        {
                            num5 = k;
                            break;
                        }
                    }
                    index = num5 + 1;
                    if (commandLine[index] == 0x24)
                    {
                        int num7 = -1;
                        for (int n = index; n < commandLine.Length; n++)
                        {
                            if ((commandLine[n] == 13) || (commandLine[n] == 10))
                            {
                                num7 = n;
                                break;
                            }
                        }
                        int count = Convert.ToInt32(Encoding.UTF8.GetString(commandLine, index + 1, (num7 - index) - 1));
                        if (count >= 0)
                        {
                            for (int num10 = index; num10 < commandLine.Length; num10++)
                            {
                                if (commandLine[num10] == 10)
                                {
                                    num5 = num10;
                                    break;
                                }
                            }
                            index = num5 + 1;
                            list.Add(Encoding.UTF8.GetString(commandLine, index, count));
                            index += count;
                            continue;
                        }
                        list.Add(null);
                        continue;
                    }
                    int num11 = -1;
                    for (int m = index; m < commandLine.Length; m++)
                    {
                        if ((commandLine[m] == 13) || (commandLine[m] == 10))
                        {
                            num11 = m;
                            break;
                        }
                    }
                    list.Add(Encoding.UTF8.GetString(commandLine, index, (num11 - index) - 1));
                }
                return OperateResult.CreateSuccessResult<string[]>(list.ToArray());
            }
            catch (Exception exception)
            {
                return new OperateResult<string[]>(exception.Message);
            }
        }

        public static byte[] PackStringCommand(string[] commands)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('*');
            int length = commands.Length;
            builder.Append(length.ToString());
            builder.Append("\r\n");
            for (int i = 0; i < commands.Length; i++)
            {
                builder.Append('$');
                builder.Append(Encoding.UTF8.GetBytes(commands[i]).Length.ToString());
                builder.Append("\r\n");
                builder.Append(commands[i]);
                builder.Append("\r\n");
            }
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public static byte[] PackSubscribeCommand(string[] topics)
        {
            List<string> list = new List<string> { "SUBSCRIBE" };
            list.AddRange(topics);
            return PackStringCommand(list.ToArray());
        }

        public static byte[] PackUnSubscribeCommand(string[] topics)
        {
            List<string> list = new List<string> { "UNSUBSCRIBE" };
            list.AddRange(topics);
            return PackStringCommand(list.ToArray());
        }
    }
}

