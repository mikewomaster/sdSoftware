namespace HslCommunication.Core
{
    using System;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;

    public static class NetSupport
    {
        internal const int SocketBufferSize = 0x4000;

        internal static byte[] ReadBytesFromSocket(Socket socket, int receive, [Optional, DefaultParameterValue(null)] Action<long, long> reportProgress)
        {
            byte[] buffer = new byte[receive];
            int offset = 0;
            while (offset < receive)
            {
                int size = Math.Min(receive - offset, 0x4000);
                int num3 = socket.Receive(buffer, offset, size, SocketFlags.None);
                offset += num3;
                if (num3 == 0)
                {
                    throw new RemoteCloseException();
                }
                if (reportProgress != null)
                {
                    reportProgress((long) offset, (long) receive);
                }
            }
            return buffer;
        }
    }
}

