namespace HslCommunication.Controls
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class HslCurveItem
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsLeftFrame>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color <LineColor>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float <LineThickness>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <Visible>k__BackingField;
        public float[] Data = null;

        public HslCurveItem()
        {
            this.LineThickness = 1f;
            this.IsLeftFrame = true;
            this.Visible = true;
        }

        public bool IsLeftFrame { get; set; }

        public Color LineColor { get; set; }

        public float LineThickness { get; set; }

        public bool Visible { get; set; }
    }
}

