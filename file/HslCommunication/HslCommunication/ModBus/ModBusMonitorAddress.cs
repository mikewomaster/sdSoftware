namespace HslCommunication.ModBus
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ModBusMonitorAddress
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort <Address>k__BackingField;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event Action<ModBusMonitorAddress, short, short> OnChange;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event Action<ModBusMonitorAddress, short> OnWrite;

        public void SetChangeValue(short before, short after)
        {
            if (before != after)
            {
                if (this.OnChange != null)
                {
                    Action<ModBusMonitorAddress, short, short> onChange = this.OnChange;
                    onChange(this, before, after);
                }
                else
                {
                    Action<ModBusMonitorAddress, short, short> expressionStack_16_0 = this.OnChange;
                }
            }
        }

        public void SetValue(short value)
        {
            if (this.OnWrite != null)
            {
                Action<ModBusMonitorAddress, short> onWrite = this.OnWrite;
                onWrite(this, value);
            }
            else
            {
                Action<ModBusMonitorAddress, short> expressionStack_9_0 = this.OnWrite;
            }
        }

        public ushort Address { get; set; }
    }
}

