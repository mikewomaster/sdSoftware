namespace HslCommunication.Core
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class HslPieItem
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color <Back>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Name>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Value>k__BackingField;

        public HslPieItem()
        {
            this.Back = Color.DodgerBlue;
        }

        public Color Back { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }
    }
}

