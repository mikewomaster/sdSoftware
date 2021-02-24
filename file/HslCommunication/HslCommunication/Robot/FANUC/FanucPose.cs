namespace HslCommunication.Robot.FANUC
{
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class FanucPose
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <Config>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float[] <Joint>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <UF>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <UT>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <ValidC>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private short <ValidJ>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float[] <Xyzwpr>k__BackingField;

        public void LoadByContent(IByteTransform byteTransform, byte[] content, int index)
        {
            this.Xyzwpr = new float[9];
            for (int i = 0; i < this.Xyzwpr.Length; i++)
            {
                this.Xyzwpr[i] = BitConverter.ToSingle(content, index + (4 * i));
            }
            this.Config = TransConfigStringArray(byteTransform.TransInt16(content, index + 0x24, 7));
            this.Joint = new float[9];
            for (int j = 0; j < this.Joint.Length; j++)
            {
                this.Joint[j] = BitConverter.ToSingle(content, (index + 0x34) + (4 * j));
            }
            this.ValidC = BitConverter.ToInt16(content, index + 50);
            this.ValidJ = BitConverter.ToInt16(content, index + 0x58);
            this.UF = BitConverter.ToInt16(content, index + 90);
            this.UT = BitConverter.ToInt16(content, index + 0x5c);
        }

        public static FanucPose PraseFrom(IByteTransform byteTransform, byte[] content, int index)
        {
            FanucPose pose = new FanucPose();
            pose.LoadByContent(byteTransform, content, index);
            return pose;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(string.Format("FanucPose UF={0} UT={1}", this.UF, this.UT));
            if (this.ValidC > 0)
            {
                builder.Append("\r\nXyzwpr=" + SoftBasic.ArrayFormat<float>(this.Xyzwpr) + "\r\nConfig=" + SoftBasic.ArrayFormat<string>(this.Config));
            }
            if (this.ValidJ > 0)
            {
                builder.Append("\r\nJOINT=" + SoftBasic.ArrayFormat<float>(this.Joint));
            }
            return builder.ToString();
        }

        public static string[] TransConfigStringArray(short[] value)
        {
            return new string[] { ((value[0] != 0) ? "F" : "N"), ((value[1] != 0) ? "L" : "R"), ((value[2] != 0) ? "U" : "D"), ((value[3] != 0) ? "T" : "B"), value[4].ToString(), value[5].ToString(), value[6].ToString() };
        }

        public string[] Config { get; set; }

        public float[] Joint { get; set; }

        public short UF { get; set; }

        public short UT { get; set; }

        public short ValidC { get; set; }

        public short ValidJ { get; set; }

        public float[] Xyzwpr { get; set; }
    }
}

