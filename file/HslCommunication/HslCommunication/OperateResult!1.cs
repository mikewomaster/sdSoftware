namespace HslCommunication
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class OperateResult<T> : OperateResult
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T <Content>k__BackingField;

        public OperateResult()
        {
        }

        public OperateResult(string msg) : base(msg)
        {
        }

        public OperateResult(int err, string msg) : base(err, msg)
        {
        }

        public OperateResult<T> Check(Func<T, OperateResult> check)
        {
            if (base.IsSuccess)
            {
                OperateResult result = check(this.Content);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<T>(result);
                }
            }
            return (OperateResult<T>) this;
        }

        public OperateResult<T> Check(Func<T, bool> check, [Optional, DefaultParameterValue("All content data check failed")] string message)
        {
            if (!base.IsSuccess)
            {
                return (OperateResult<T>) this;
            }
            if (check(this.Content))
            {
                return (OperateResult<T>) this;
            }
            return new OperateResult<T>(message);
        }

        public OperateResult Then(Func<T, OperateResult> func)
        {
            return (base.IsSuccess ? func(this.Content) : this);
        }

        public OperateResult<TResult> Then<TResult>(Func<T, OperateResult<TResult>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult>(this));
        }

        public OperateResult<TResult1, TResult2> Then<TResult1, TResult2>(Func<T, OperateResult<TResult1, TResult2>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3> Then<TResult1, TResult2, TResult3>(Func<T, OperateResult<TResult1, TResult2, TResult3>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4> Then<TResult1, TResult2, TResult3, TResult4>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5> Then<TResult1, TResult2, TResult3, TResult4, TResult5>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>(this));
        }

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10>> func)
        {
            return (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9, TResult10>(this));
        }

        public T Content { get; set; }
    }
}

