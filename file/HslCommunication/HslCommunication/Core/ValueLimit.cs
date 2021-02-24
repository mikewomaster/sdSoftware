namespace HslCommunication.Core
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ValueLimit
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <MaxValue>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <MinValue>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <Average>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <StartValue>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double <Current>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Count>k__BackingField;
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public double Average { get; set; }
        public double StartValue { get; set; }
        public double Current { get; set; }
        public int Count { get; set; }
        public ValueLimit SetNewValue(double value)
        {
            if (!double.IsNaN(value))
            {
                if (this.Count == 0)
                {
                    this.MaxValue = value;
                    this.MinValue = value;
                    this.Count = 1;
                    this.Current = value;
                    this.Average = value;
                    this.StartValue = value;
                }
                else
                {
                    if (value < this.MinValue)
                    {
                        this.MinValue = value;
                    }
                    if (value > this.MaxValue)
                    {
                        this.MaxValue = value;
                    }
                    this.Current = value;
                    this.Average = ((this.Count * this.Average) + value) / ((double) (this.Count + 1));
                    int count = this.Count;
                    this.Count = count + 1;
                }
            }
            return this;
        }

        public override string ToString()
        {
            return string.Format("Avg[{0}]", this.Current);
        }

        public static bool operator ==(ValueLimit value1, ValueLimit value2)
        {
            if (value1.Count != value2.Count)
            {
                return false;
            }
            if (!(value1.MaxValue == value2.MaxValue))
            {
                return false;
            }
            if (!(value1.MinValue == value2.MinValue))
            {
                return false;
            }
            if (!(value1.Current == value2.Current))
            {
                return false;
            }
            if (!(value1.Average == value2.Average))
            {
                return false;
            }
            if (!(value1.StartValue == value2.StartValue))
            {
                return false;
            }
            return true;
        }

        public static bool operator !=(ValueLimit value1, ValueLimit value2)
        {
            return !(value1 == value2);
        }

        public override bool Equals(object obj)
        {
            ValueLimit limit;
            return (((!(obj is ValueLimit) ? 0 : 1) != 0) && (this == limit));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

