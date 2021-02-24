namespace HslCommunication.Profinet.Toledo
{
    using HslCommunication.BasicFramework;
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class ToledoStandardData
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <BeyondScope>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <DynamicState>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsPrint>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsTenExtend>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <SourceData>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <Suttle>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <Symbol>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <Tare>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Unit>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <Weight>k__BackingField;

        public ToledoStandardData()
        {
        }

        public ToledoStandardData(byte[] buffer)
        {
            this.Weight = float.Parse(Encoding.ASCII.GetString(buffer, 4, 6));
            this.Tare = float.Parse(Encoding.ASCII.GetString(buffer, 10, 6));
            switch ((buffer[1] & 7))
            {
                case 0:
                    this.Weight *= 100f;
                    this.Tare *= 100f;
                    break;

                case 1:
                    this.Weight *= 10f;
                    this.Tare *= 10f;
                    break;

                case 3:
                    this.Weight /= 10f;
                    this.Tare /= 10f;
                    break;

                case 4:
                    this.Weight /= 100f;
                    this.Tare /= 100f;
                    break;

                case 5:
                    this.Weight /= 1000f;
                    this.Tare /= 1000f;
                    break;

                case 6:
                    this.Weight /= 10000f;
                    this.Tare /= 10000f;
                    break;

                case 7:
                    this.Weight /= 100000f;
                    this.Tare /= 100000f;
                    break;
            }
            this.Suttle = SoftBasic.BoolOnByteIndex(buffer[2], 0);
            this.Symbol = SoftBasic.BoolOnByteIndex(buffer[2], 1);
            this.BeyondScope = SoftBasic.BoolOnByteIndex(buffer[2], 2);
            this.DynamicState = SoftBasic.BoolOnByteIndex(buffer[2], 3);
            switch ((buffer[3] & 7))
            {
                case 0:
                    this.Unit = SoftBasic.BoolOnByteIndex(buffer[2], 4) ? "kg" : "lb";
                    break;

                case 1:
                    this.Unit = "g";
                    break;

                case 2:
                    this.Unit = "t";
                    break;

                case 3:
                    this.Unit = "oz";
                    break;

                case 4:
                    this.Unit = "ozt";
                    break;

                case 5:
                    this.Unit = "dwt";
                    break;

                case 6:
                    this.Unit = "ton";
                    break;

                case 7:
                    this.Unit = "newton";
                    break;
            }
            this.IsPrint = SoftBasic.BoolOnByteIndex(buffer[3], 3);
            this.IsTenExtend = SoftBasic.BoolOnByteIndex(buffer[3], 4);
            this.SourceData = buffer;
        }

        public override string ToString()
        {
            return string.Format("ToledoStandardData[{0}]", this.Weight);
        }

        public bool BeyondScope { get; set; }

        public bool DynamicState { get; set; }

        public bool IsPrint { get; set; }

        public bool IsTenExtend { get; set; }

        [JsonIgnore]
        public byte[] SourceData { get; set; }

        public bool Suttle { get; set; }

        public bool Symbol { get; set; }

        public float Tare { get; set; }

        public string Unit { get; set; }

        public float Weight { get; set; }
    }
}

