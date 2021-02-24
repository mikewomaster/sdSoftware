namespace HslCommunication.Robot.Hyundai
{
    using HslCommunication.BasicFramework;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class HyundaiData
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <CharDummy>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private char <Command>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Count>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double[] <Data>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <IntDummy>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <State>k__BackingField;

        public HyundaiData()
        {
            this.Data = new double[6];
        }

        public HyundaiData(byte[] buffer)
        {
            this.LoadBy(buffer, 0);
        }

        public void LoadBy(byte[] buffer, [Optional, DefaultParameterValue(0)] int index)
        {
            this.Command = (char) buffer[index];
            this.CharDummy = Encoding.ASCII.GetString(buffer, index + 1, 3);
            this.State = BitConverter.ToInt32(buffer, index + 4);
            this.Count = BitConverter.ToInt32(buffer, index + 8);
            this.IntDummy = BitConverter.ToInt32(buffer, index + 12);
            this.Data = new double[6];
            for (int i = 0; i < this.Data.Length; i++)
            {
                if (i < 3)
                {
                    this.Data[i] = BitConverter.ToDouble(buffer, (index + 0x10) + (8 * i)) * 1000.0;
                }
                else
                {
                    this.Data[i] = (BitConverter.ToDouble(buffer, (index + 0x10) + (8 * i)) * 180.0) / 3.1415926535897931;
                }
            }
        }

        public byte[] ToBytes()
        {
            byte[] array = new byte[0x40];
            array[0] = (byte) this.Command;
            if (!string.IsNullOrEmpty(this.CharDummy))
            {
                Encoding.ASCII.GetBytes(this.CharDummy).CopyTo(array, 1);
            }
            BitConverter.GetBytes(this.State).CopyTo(array, 4);
            BitConverter.GetBytes(this.Count).CopyTo(array, 8);
            BitConverter.GetBytes(this.IntDummy).CopyTo(array, 12);
            for (int i = 0; i < this.Data.Length; i++)
            {
                if (i < 3)
                {
                    BitConverter.GetBytes((double) (this.Data[i] / 1000.0)).CopyTo(array, (int) (0x10 + (8 * i)));
                }
                else
                {
                    BitConverter.GetBytes((double) ((this.Data[i] * 3.1415926535897931) / 180.0)).CopyTo(array, (int) (0x10 + (8 * i)));
                }
            }
            return array;
        }

        public override string ToString()
        {
            return string.Format("HyundaiData:Cmd[{0},{1},{2},{3},{4}] Data:{5}", new object[] { this.Command, this.CharDummy, this.State, this.Count, this.IntDummy, SoftBasic.ArrayFormat<double>(this.Data) });
        }

        public string CharDummy { get; set; }

        public char Command { get; set; }

        public int Count { get; set; }

        public double[] Data { get; set; }

        public int IntDummy { get; set; }

        public int State { get; set; }
    }
}

