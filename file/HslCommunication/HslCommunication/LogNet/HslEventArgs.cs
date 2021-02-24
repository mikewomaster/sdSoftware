namespace HslCommunication.LogNet
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class HslEventArgs : EventArgs
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HslMessageItem <HslMessage>k__BackingField;

        public HslMessageItem HslMessage { get; set; }
    }
}

