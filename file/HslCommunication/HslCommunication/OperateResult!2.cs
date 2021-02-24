namespace HslCommunication
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class OperateResult<T1, T2> : OperateResult
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T1 <Content1>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T2 <Content2>k__BackingField;

        public OperateResult()
        {
        }

        public OperateResult(string msg) : base(msg)
        {
        }

        public OperateResult(int err, string msg) : base(err, msg)
        {
        }

        public OperateResult<T1, T2> Check(Func<T1, T2, OperateResult> check)
        {
            if (base.IsSuccess)
            {
                OperateResult result = check(this.Content1, this.Content2);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<T1, T2>(result);
                }
            }
            return (OperateResult<T1, T2>) this;
        }

        public OperateResult<T1, T2> Check(Func<T1, T2, bool> check, [Optional, DefaultParameterValue("All content data check failed")] string message)
        {
            if (!base.IsSuccess)
            {
                return (OperateResult<T1, T2>) this;
            }
            if (check(this.Content1, this.Content2))
            {
                return (OperateResult<T1, T2>) this;
            }
            return new OperateResult<T1, T2>(message);
        }

        public OperateResult Then(Func<T1, T2, OperateResult> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : this);
        }

        public OperateResult<TResult> Then<TResult>(Func<T1, T2, OperateResult<TResult>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult>(this));
        }

        public OperateResult<TResult1, TResult2> Then<TResult1, TResult2>(Func<T1, T2, OperateResult<TResult1, TResult2>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult1, TResult2>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3> Then<TResult1, TResult2, TResult3>(Func<T1, T2, OperateResult<TResult1, TResult2, TResult3>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4> Then<TResult1, TResult2, TResult3, TResult4>(Func<T1, T2, OperateResult<TResult1, TResult2, TResult3, TResult4>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5> Then<TResult1, TResult2, TResult3, TResult4, TResult5>(Func<T1, T2, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>(Func<T1, T2, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>(Func<T1, T2, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(Func<T1, T2, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>(Func<T1, T2, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10>(Func<T1, T2, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10>> func)
        {
            return (base.IsSuccess ? func(this.Content1, this.Content2) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10>(this));
        }

        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }
    }
}

