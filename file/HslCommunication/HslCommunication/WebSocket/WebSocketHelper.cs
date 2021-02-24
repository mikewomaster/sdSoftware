namespace HslCommunication.WebSocket
{
    using HslCommunication;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public class WebSocketHelper
    {
        public static byte[] BuildWsQARequest(string ipAddress, int port)
        {
            return BuildWsRequest(ipAddress, port, string.Empty, "HslRequestAndAnswer: true");
        }

        public static byte[] BuildWsRequest(string ipAddress, int port, string url, string extra)
        {
            if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("GET ws://{0}:{1}{2} HTTP/1.1", ipAddress, port, url));
            builder.Append(Environment.NewLine);
            builder.Append(string.Format("Host: {0}:{1}", ipAddress, port));
            builder.Append(Environment.NewLine);
            builder.Append("Connection: Upgrade");
            builder.Append(Environment.NewLine);
            builder.Append("Pragma: no-cache");
            builder.Append(Environment.NewLine);
            builder.Append("Cache-Control: no-cache");
            builder.Append(Environment.NewLine);
            builder.Append("Upgrade: websocket");
            builder.Append(Environment.NewLine);
            builder.Append(string.Format("Origin: http://{0}:{1}", ipAddress, port));
            builder.Append(Environment.NewLine);
            builder.Append("Sec-WebSocket-Version: 13");
            builder.Append(Environment.NewLine);
            builder.Append("User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3314.0 Safari/537.36 SE 2.X MetaSr 1.0");
            builder.Append(Environment.NewLine);
            builder.Append("Accept-Encoding: gzip, deflate, br");
            builder.Append(Environment.NewLine);
            builder.Append("Accept-Language: zh-CN,zh;q=0.9");
            builder.Append(Environment.NewLine);
            builder.Append("Sec-WebSocket-Key: ia36apzXapB4YVxRfVyTuw==");
            builder.Append(Environment.NewLine);
            builder.Append("Sec-WebSocket-Extensions: permessage-deflate; client_max_window_bits");
            builder.Append(Environment.NewLine);
            if (!string.IsNullOrEmpty(extra))
            {
                builder.Append(extra);
                builder.Append(Environment.NewLine);
            }
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public static byte[] BuildWsSubRequest(string ipAddress, int port, string url, string[] subscribes)
        {
            StringBuilder builder = new StringBuilder();
            if (subscribes > null)
            {
                builder.Append("HslSubscribes: ");
                for (int i = 0; i < subscribes.Length; i++)
                {
                    builder.Append(subscribes[i]);
                    if (i != (subscribes.Length - 1))
                    {
                        builder.Append(",");
                    }
                }
            }
            return BuildWsRequest(ipAddress, port, url, builder.ToString());
        }

        public static string CalculateWebscoketSha1(string webSocketKey)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(webSocketKey + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11")));
        }

        public static OperateResult CheckWebSocketLegality(string httpGet)
        {
            if (Regex.IsMatch(httpGet, "Connection:[ ]*Upgrade", RegexOptions.IgnoreCase) && Regex.IsMatch(httpGet, "Upgrade:[ ]*websocket", RegexOptions.IgnoreCase))
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult("Can't find Connection: Upgrade or Upgrade: websocket");
        }

        public static OperateResult<byte[]> GetResponse(string httpGet)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("HTTP/1.1 101 Switching Protocols" + Environment.NewLine);
                builder.Append("Connection: Upgrade" + Environment.NewLine);
                builder.Append("Upgrade: websocket" + Environment.NewLine);
                builder.Append("Server:hsl websocket server" + Environment.NewLine);
                builder.Append("Access-Control-Allow-Credentials:true" + Environment.NewLine);
                builder.Append("Access-Control-Allow-Headers:content-type" + Environment.NewLine);
                builder.Append("Sec-WebSocket-Accept: " + GetSecKeyAccetp(httpGet) + Environment.NewLine + Environment.NewLine);
                return OperateResult.CreateSuccessResult<byte[]>(Encoding.UTF8.GetBytes(builder.ToString()));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static string GetSecKeyAccetp(string httpGet)
        {
            string webSocketKey = string.Empty;
            Match match = new Regex(@"Sec\-WebSocket\-Key:(.*?)\r\n").Match(httpGet);
            if (match.Success)
            {
                webSocketKey = Regex.Replace(match.Value, @"Sec\-WebSocket\-Key:(.*?)\r\n", "$1").Trim();
            }
            return CalculateWebscoketSha1(webSocketKey);
        }

        public static string[] GetWebSocketSubscribes(string httpGet)
        {
            Match match = new Regex(@"HslSubscribes:[^\r\n]+").Match(httpGet);
            if (!match.Success)
            {
                return null;
            }
            char[] separator = new char[] { ',' };
            return match.Value.Substring(14).Replace(" ", "").Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static byte[] WebScoketPackData(int opCode, bool isMask, string message)
        {
            return WebScoketPackData(opCode, isMask, string.IsNullOrEmpty(message) ? new byte[0] : Encoding.UTF8.GetBytes(message));
        }

        public static byte[] WebScoketPackData(int opCode, bool isMask, byte[] payload)
        {
            if (payload == null)
            {
                payload = new byte[0];
            }
            MemoryStream stream = new MemoryStream();
            byte[] buffer = new byte[] { 0x9b, 3, 0xa1, 0xa8 };
            if (isMask)
            {
                new Random().NextBytes(buffer);
                for (int i = 0; i < payload.Length; i++)
                {
                    payload[i] = (byte) (payload[i] ^ buffer[i % 4]);
                }
            }
            stream.WriteByte((byte) (0x80 | opCode));
            if (payload.Length < 0x7e)
            {
                stream.WriteByte((byte) (payload.Length + (isMask ? 0x80 : 0)));
            }
            else if (payload.Length <= 0xffff)
            {
                stream.WriteByte((byte) (0x7e + (isMask ? 0x80 : 0)));
                byte[] bytes = BitConverter.GetBytes((ushort) payload.Length);
                Array.Reverse(bytes);
                stream.Write(bytes, 0, bytes.Length);
            }
            else
            {
                stream.WriteByte((byte) (0x7f + (isMask ? 0x80 : 0)));
                byte[] array = BitConverter.GetBytes((ulong) payload.Length);
                Array.Reverse(array);
                stream.Write(array, 0, array.Length);
            }
            if (isMask)
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            stream.Write(payload, 0, payload.Length);
            byte[] buffer2 = stream.ToArray();
            stream.Dispose();
            return buffer2;
        }
    }
}

