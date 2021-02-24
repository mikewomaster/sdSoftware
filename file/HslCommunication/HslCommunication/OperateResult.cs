namespace HslCommunication
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class OperateResult
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <ErrorCode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsSuccess>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Message>k__BackingField;

        public OperateResult()
        {
            this.<Message>k__BackingField = StringResources.Language.UnknownError;
            this.<ErrorCode>k__BackingField = 0x2710;
        }

        public OperateResult(string msg)
        {
            this.<Message>k__BackingField = StringResources.Language.UnknownError;
            this.<ErrorCode>k__BackingField = 0x2710;
            this.Message = msg;
        }

        public OperateResult(int err, string msg)
        {
            this.<Message>k__BackingField = StringResources.Language.UnknownError;
            this.<ErrorCode>k__BackingField = 0x2710;
            this.ErrorCode = err;
            this.Message = msg;
        }

        public OperateResult<T> Convert<T>(T content)
        {
            return (this.IsSuccess ? CreateSuccessResult<T>(content) : CreateFailedResult<T>(this));
        }

        public OperateResult<T1, T2> Convert<T1, T2>(T1 content1, T2 content2)
        {
            return (this.IsSuccess ? CreateSuccessResult<T1, T2>(content1, content2) : CreateFailedResult<T1, T2>(this));
        }

        public OperateResult<T1, T2, T3> Convert<T1, T2, T3>(T1 content1, T2 content2, T3 content3)
        {
            return (this.IsSuccess ? CreateSuccessResult<T1, T2, T3>(content1, content2, content3) : CreateFailedResult<T1, T2, T3>(this));
        }

        public OperateResult<T1, T2, T3, T4> Convert<T1, T2, T3, T4>(T1 content1, T2 content2, T3 content3, T4 content4)
        {
            return (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4>(content1, content2, content3, content4) : CreateFailedResult<T1, T2, T3, T4>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5> Convert<T1, T2, T3, T4, T5>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5)
        {
            return (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5>(content1, content2, content3, content4, content5) : CreateFailedResult<T1, T2, T3, T4, T5>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6> Convert<T1, T2, T3, T4, T5, T6>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5, T6 content6)
        {
            return (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5, T6>(content1, content2, content3, content4, content5, content6) : CreateFailedResult<T1, T2, T3, T4, T5, T6>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6, T7> Convert<T1, T2, T3, T4, T5, T6, T7>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5, T6 content6, T7 content7)
        {
            return (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7>(content1, content2, content3, content4, content5, content6, content7) : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> Convert<T1, T2, T3, T4, T5, T6, T7, T8>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5, T6 content6, T7 content7, T8 content8)
        {
            return (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8>(content1, content2, content3, content4, content5, content6, content7, content8) : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Convert<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5, T6 content6, T7 content7, T8 content8, T9 content9)
        {
            return (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(content1, content2, content3, content4, content5, content6, content7, content8, content9) : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Convert<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5, T6 content6, T7 content7, T8 content8, T9 content9, T10 content10)
        {
            return (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(content1, content2, content3, content4, content5, content6, content7, content8, content9, content10) : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this));
        }

        public void CopyErrorFromOther<TResult>(TResult result) where TResult: OperateResult
        {
            if (result > null)
            {
                this.ErrorCode = result.ErrorCode;
                this.Message = result.Message;
            }
        }

        public static OperateResult<T> CreateFailedResult<T>(OperateResult result)
        {
            return new OperateResult<T> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2> CreateFailedResult<T1, T2>(OperateResult result)
        {
            return new OperateResult<T1, T2> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3> CreateFailedResult<T1, T2, T3>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3, T4> CreateFailedResult<T1, T2, T3, T4>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5> CreateFailedResult<T1, T2, T3, T4, T5>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4, T5> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6> CreateFailedResult<T1, T2, T3, T4, T5, T6>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7> CreateFailedResult<T1, T2, T3, T4, T5, T6, T7>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6, T7> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> { 
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult CreateSuccessResult()
        {
            return new OperateResult { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText
            };
        }

        public static OperateResult<T> CreateSuccessResult<T>(T value)
        {
            return new OperateResult<T> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content = value
            };
        }

        public static OperateResult<T1, T2> CreateSuccessResult<T1, T2>(T1 value1, T2 value2)
        {
            return new OperateResult<T1, T2> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content1 = value1,
                Content2 = value2
            };
        }

        public static OperateResult<T1, T2, T3> CreateSuccessResult<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            return new OperateResult<T1, T2, T3> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content1 = value1,
                Content2 = value2,
                Content3 = value3
            };
        }

        public static OperateResult<T1, T2, T3, T4> CreateSuccessResult<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            return new OperateResult<T1, T2, T3, T4> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5> CreateSuccessResult<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            return new OperateResult<T1, T2, T3, T4, T5> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4,
                Content5 = value5
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6> CreateSuccessResult<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4,
                Content5 = value5,
                Content6 = value6
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7> CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6, T7> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4,
                Content5 = value5,
                Content6 = value6,
                Content7 = value7
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4,
                Content5 = value5,
                Content6 = value6,
                Content7 = value7,
                Content8 = value8
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4,
                Content5 = value5,
                Content6 = value6,
                Content7 = value7,
                Content8 = value8,
                Content9 = value9
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9, T10 value10)
        {
            return new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> { 
                IsSuccess = true,
                ErrorCode = 0,
                Message = StringResources.Language.SuccessText,
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4,
                Content5 = value5,
                Content6 = value6,
                Content7 = value7,
                Content8 = value8,
                Content9 = value9,
                Content10 = value10
            };
        }

        public OperateResult Then(Func<OperateResult> func)
        {
            return (this.IsSuccess ? func() : this);
        }

        public OperateResult<T> Then<T>(Func<OperateResult<T>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T>(this));
        }

        public OperateResult<T1, T2> Then<T1, T2>(Func<OperateResult<T1, T2>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T1, T2>(this));
        }

        public OperateResult<T1, T2, T3> Then<T1, T2, T3>(Func<OperateResult<T1, T2, T3>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3>(this));
        }

        public OperateResult<T1, T2, T3, T4> Then<T1, T2, T3, T4>(Func<OperateResult<T1, T2, T3, T4>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5> Then<T1, T2, T3, T4, T5>(Func<OperateResult<T1, T2, T3, T4, T5>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6> Then<T1, T2, T3, T4, T5, T6>(Func<OperateResult<T1, T2, T3, T4, T5, T6>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5, T6>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6, T7> Then<T1, T2, T3, T4, T5, T6, T7>(Func<OperateResult<T1, T2, T3, T4, T5, T6, T7>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> Then<T1, T2, T3, T4, T5, T6, T7, T8>(Func<OperateResult<T1, T2, T3, T4, T5, T6, T7, T8>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Then<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this));
        }

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Then<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> func)
        {
            return (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this));
        }

        public string ToMessageShowString()
        {
            return string.Format("{0}:{1}{2}{3}:{4}", new object[] { StringResources.Language.ErrorCode, this.ErrorCode, Environment.NewLine, StringResources.Language.TextDescription, this.Message });
        }

        public int ErrorCode { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}

