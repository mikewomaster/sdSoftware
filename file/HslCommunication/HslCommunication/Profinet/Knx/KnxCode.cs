namespace HslCommunication.Profinet.Knx
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class KnxCode
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <Channel>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsConnect>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte <SequenceCounter>k__BackingField;
        private bool is_fresh = false;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event GetData GetData_msg;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event ReturnData Return_data_msg;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event ReturnData Set_knx_data;

        public byte[] Disconnect_knx(byte channel, IPEndPoint IP_PROT)
        {
            byte[] addressBytes = IP_PROT.Address.GetAddressBytes();
            byte[] bytes = BitConverter.GetBytes(IP_PROT.Port);
            return new byte[] { 6, 0x10, 2, 9, 0, 0x10, channel, 0, 8, 1, addressBytes[0], addressBytes[1], addressBytes[2], addressBytes[3], bytes[1], bytes[0] };
        }

        private void Extraction_of_Channel(byte[] in_data)
        {
            this.Channel = in_data[6];
            if ((in_data[5] == 8) & (in_data[7] == 0x25))
            {
                this.IsConnect = false;
            }
            if (this.Channel > 0)
            {
                this.IsConnect = true;
            }
        }

        public short Get_knx_addr(string addr, out bool is_ok)
        {
            short num = 0;
            char[] separator = new char[] { '\\' };
            string[] strArray = addr.Split(separator);
            if (strArray.Length == 3)
            {
                int num2 = int.Parse(strArray[0]);
                int num3 = int.Parse(strArray[1]);
                int num4 = int.Parse(strArray[2]);
                if ((((num2 > 0x1f) || (num3 > 7)) || (num4 > 0xff)) || (((num2 < 0) || (num3 < 0)) || (num4 < 0)))
                {
                    Console.WriteLine("地址不合法");
                    is_ok = false;
                    return num;
                }
                num2 = num2 << 11;
                num3 = num3 << 8;
                int num6 = (num2 | num3) | num4;
                num = (short) num6;
                is_ok = true;
                return num;
            }
            Console.WriteLine("地址不合法");
            is_ok = false;
            return num;
        }

        public byte[] Handshake(IPEndPoint IP_PROT)
        {
            byte[] addressBytes = IP_PROT.Address.GetAddressBytes();
            byte[] bytes = BitConverter.GetBytes(IP_PROT.Port);
            return new byte[] { 
                6, 0x10, 2, 5, 0, 0x1a, 8, 1, addressBytes[0], addressBytes[1], addressBytes[2], addressBytes[3], bytes[1], bytes[0], 8, 1,
                addressBytes[0], addressBytes[1], addressBytes[2], addressBytes[3], bytes[1], bytes[0], 4, 4, 2, 0
            };
        }

        public void KNX_check(byte[] in_data)
        {
            switch (in_data[2])
            {
                case 2:
                    this.KNX_serverOF_2(in_data);
                    break;

                case 4:
                    this.KNX_serverOF_4(in_data);
                    break;
            }
        }

        public void Knx_Resd_step1(short addr)
        {
            byte[] bytes = BitConverter.GetBytes(addr);
            byte[] data = new byte[0x15];
            byte[] buffer3 = BitConverter.GetBytes(data.Length);
            if ((this.SequenceCounter + 1) <= 0xff)
            {
                if (this.is_fresh)
                {
                    byte num = (byte) (this.SequenceCounter + 1);
                    this.SequenceCounter = num;
                }
                else
                {
                    this.is_fresh = true;
                }
            }
            else
            {
                this.SequenceCounter = 0;
            }
            data[0] = 6;
            data[1] = 0x10;
            data[2] = 4;
            data[3] = 0x20;
            data[4] = buffer3[1];
            data[5] = buffer3[0];
            data[6] = 4;
            data[7] = this.Channel;
            data[8] = this.SequenceCounter;
            data[9] = 0;
            data[10] = 0x11;
            data[11] = 0;
            data[12] = 0xbc;
            data[13] = 0xe0;
            data[14] = 0;
            data[15] = 0;
            data[0x10] = bytes[1];
            data[0x11] = bytes[0];
            data[0x12] = 1;
            data[0x13] = 0;
            data[20] = 0;
            if (this.Set_knx_data > null)
            {
                this.Return_data_msg(data);
            }
        }

        public void knx_server_is_real(IPEndPoint IP_PROT)
        {
            byte[] data = new byte[0x10];
            byte[] addressBytes = IP_PROT.Address.GetAddressBytes();
            byte[] bytes = BitConverter.GetBytes(IP_PROT.Port);
            data[0] = 6;
            data[1] = 0x10;
            data[2] = 2;
            data[3] = 7;
            data[4] = 0;
            data[5] = 0x10;
            data[6] = this.Channel;
            data[7] = 0;
            data[8] = 8;
            data[9] = 1;
            data[10] = addressBytes[0];
            data[11] = addressBytes[1];
            data[12] = addressBytes[2];
            data[13] = addressBytes[3];
            data[14] = bytes[1];
            data[15] = bytes[0];
            if (this.Return_data_msg > null)
            {
                this.Return_data_msg(data);
            }
        }

        private void KNX_serverOF_2(byte[] in_data)
        {
            switch (in_data[3])
            {
                case 6:
                    this.Extraction_of_Channel(in_data);
                    break;

                case 7:
                    this.Return_status();
                    break;
            }
        }

        private void KNX_serverOF_4(byte[] in_data)
        {
            switch (in_data[3])
            {
                case 0x20:
                    this.Read_com_CEMI(in_data);
                    break;
            }
        }

        public void Knx_Write(short addr, byte len, byte[] data)
        {
            byte[] bytes = BitConverter.GetBytes(addr);
            byte[] buffer2 = new byte[20 + len];
            byte[] buffer3 = BitConverter.GetBytes(buffer2.Length);
            if ((this.SequenceCounter + 1) <= 0xff)
            {
                if (this.is_fresh)
                {
                    byte num = (byte) (this.SequenceCounter + 1);
                    this.SequenceCounter = num;
                }
                else
                {
                    this.is_fresh = true;
                }
            }
            else
            {
                this.SequenceCounter = 0;
            }
            buffer2[0] = 6;
            buffer2[1] = 0x10;
            buffer2[2] = 4;
            buffer2[3] = 0x20;
            buffer2[4] = buffer3[1];
            buffer2[5] = buffer3[0];
            buffer2[6] = 4;
            buffer2[7] = this.Channel;
            buffer2[8] = this.SequenceCounter;
            buffer2[9] = 0;
            buffer2[10] = 0x11;
            buffer2[11] = 0;
            buffer2[12] = 0xbc;
            buffer2[13] = 0xe0;
            buffer2[14] = 0;
            buffer2[15] = 0;
            buffer2[0x10] = bytes[1];
            buffer2[0x11] = bytes[0];
            buffer2[0x12] = len;
            buffer2[0x13] = 0;
            if (len == 1)
            {
                byte[] buffer4 = BitConverter.GetBytes((int) ((data[0] & 0x3f) | 0x80));
                buffer2[20] = buffer4[0];
            }
            else
            {
                buffer2[20] = 0x80;
                for (int i = 2; i <= len; i++)
                {
                    buffer2[(len - 1) + 20] = data[i - 2];
                }
            }
            if (this.Set_knx_data > null)
            {
                this.Set_knx_data(buffer2);
            }
        }

        private void Read_CEMI(byte[] in_data)
        {
            if (in_data.Length > 11)
            {
                switch (in_data[10])
                {
                    case 0x29:
                        this.Read_CEMI_29(in_data);
                        break;

                    case 0x2e:
                        this.Read_CEMI_2e(in_data);
                        break;
                }
            }
        }

        private void Read_CEMI_29(byte[] in_data)
        {
            byte[] bytes;
            short addr = BitConverter.ToInt16(new byte[] { in_data[0x11], in_data[0x10] }, 0);
            if (in_data[0x12] > 1)
            {
                bytes = new byte[in_data[0x11]];
                for (int i = 0; i < (in_data[0x12] - 1); i++)
                {
                    bytes[i] = in_data[0x15 + i];
                }
            }
            else
            {
                bytes = BitConverter.GetBytes((int) (in_data[20] & 0x3f));
            }
            if (this.GetData_msg > null)
            {
                this.GetData_msg(addr, in_data[0x12], bytes);
            }
            this.Read_setp6(in_data);
        }

        private void Read_CEMI_2e(byte[] in_data)
        {
            byte[] data = new byte[] { 6, 0x10, 4, 0x21, 0, 10, 4, this.Channel, in_data[8], 0 };
            if (this.Set_knx_data > null)
            {
                this.Return_data_msg(data);
            }
        }

        private void Read_com_CEMI(byte[] in_data)
        {
            this.Read_CEMI(in_data);
        }

        private void Read_setp6(byte[] in_data)
        {
            byte[] data = new byte[] { 6, 0x10, 4, 0x21, 0, 10, 4, this.Channel, in_data[8], 0 };
            if (this.Return_data_msg > null)
            {
                this.Return_data_msg(data);
            }
        }

        private void Return_status()
        {
            byte[] data = new byte[] { 6, 0x10, 2, 8, 0, 8, this.Channel, 0 };
            if (this.Return_data_msg > null)
            {
                this.Return_data_msg(data);
            }
        }

        public byte Channel { get; set; }

        public bool IsConnect { get; private set; }

        public byte SequenceCounter { get; set; }

        public delegate void GetData(short addr, byte len, byte[] data);

        public delegate void ReturnData(byte[] data);
    }
}

