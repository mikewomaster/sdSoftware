namespace HslCommunication
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class OperateResult<T1, T2, T3, T4, T5, T6> : OperateResult
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T1 <Content1>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T2 <Content2>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T3 <Content3>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T4 <Content4>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T5 <Content5>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T6 <Content6>k__BackingField;

        public OperateResult()
        {
        }

        public OperateResult(string msg) : base(msg)
        {
        }

        public OperateResult(int err, string msg) : base(err, msg)
        {
        }

        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }

        public T3 Content3 { get; set; }

        public T4 Content4 { get; set; }

        public T5 Content5 { get; set; }

        public T6 Content6 { get; set; }
    }
}

