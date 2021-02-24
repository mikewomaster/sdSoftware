namespace HslCommunication.Core
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using System;
    using System.Runtime.CompilerServices;

    public static class ByteTransformHelper
    {
        public static OperateResult<TResult> GetResultFromArray<TResult>(OperateResult<TResult[]> result)
        {
            return GetSuccessResultFromOther<TResult, TResult[]>(result, m => m[0]);
        }

        public static OperateResult<TResult> GetResultFromBytes<TResult>(OperateResult<byte[]> result, Func<byte[], TResult> translator)
        {
            try
            {
                if (result.IsSuccess)
                {
                    return OperateResult.CreateSuccessResult<TResult>(translator(result.Content));
                }
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            catch (Exception exception)
            {
                return new OperateResult<TResult> { Message = string.Format("{0} {1} : Length({2}) {3}", new object[] { 
                    StringResources.Language.DataTransformError,
                    SoftBasic.ByteToHexString(result.Content),
                    result.Content.Length,
                    exception.Message
                }) };
            }
        }

        public static OperateResult GetResultFromOther<TIn>(OperateResult<TIn> result, Func<TIn, OperateResult> trans)
        {
            if (!result.IsSuccess)
            {
                return result;
            }
            return trans(result.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn>(OperateResult<TIn> result, Func<TIn, OperateResult<TResult>> trans)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            return trans(result.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn1, TIn2>(OperateResult<TIn1> result, Func<TIn1, OperateResult<TIn2>> trans1, Func<TIn2, OperateResult<TResult>> trans2)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            OperateResult<TIn2> result2 = trans1(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result2);
            }
            return trans2(result2.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn1, TIn2, TIn3>(OperateResult<TIn1> result, Func<TIn1, OperateResult<TIn2>> trans1, Func<TIn2, OperateResult<TIn3>> trans2, Func<TIn3, OperateResult<TResult>> trans3)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            OperateResult<TIn2> result2 = trans1(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result2);
            }
            OperateResult<TIn3> result3 = trans2(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result3);
            }
            return trans3(result3.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn1, TIn2, TIn3, TIn4>(OperateResult<TIn1> result, Func<TIn1, OperateResult<TIn2>> trans1, Func<TIn2, OperateResult<TIn3>> trans2, Func<TIn3, OperateResult<TIn4>> trans3, Func<TIn4, OperateResult<TResult>> trans4)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            OperateResult<TIn2> result2 = trans1(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result2);
            }
            OperateResult<TIn3> result3 = trans2(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result3);
            }
            OperateResult<TIn4> result4 = trans3(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result4);
            }
            return trans4(result4.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn1, TIn2, TIn3, TIn4, TIn5>(OperateResult<TIn1> result, Func<TIn1, OperateResult<TIn2>> trans1, Func<TIn2, OperateResult<TIn3>> trans2, Func<TIn3, OperateResult<TIn4>> trans3, Func<TIn4, OperateResult<TIn5>> trans4, Func<TIn5, OperateResult<TResult>> trans5)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            OperateResult<TIn2> result2 = trans1(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result2);
            }
            OperateResult<TIn3> result3 = trans2(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result3);
            }
            OperateResult<TIn4> result4 = trans3(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result4);
            }
            OperateResult<TIn5> result5 = trans4(result4.Content);
            if (!result5.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result5);
            }
            return trans5(result5.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6>(OperateResult<TIn1> result, Func<TIn1, OperateResult<TIn2>> trans1, Func<TIn2, OperateResult<TIn3>> trans2, Func<TIn3, OperateResult<TIn4>> trans3, Func<TIn4, OperateResult<TIn5>> trans4, Func<TIn5, OperateResult<TIn6>> trans5, Func<TIn6, OperateResult<TResult>> trans6)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            OperateResult<TIn2> result2 = trans1(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result2);
            }
            OperateResult<TIn3> result3 = trans2(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result3);
            }
            OperateResult<TIn4> result4 = trans3(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result4);
            }
            OperateResult<TIn5> result5 = trans4(result4.Content);
            if (!result5.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result5);
            }
            OperateResult<TIn6> result6 = trans5(result5.Content);
            if (!result6.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result6);
            }
            return trans6(result6.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7>(OperateResult<TIn1> result, Func<TIn1, OperateResult<TIn2>> trans1, Func<TIn2, OperateResult<TIn3>> trans2, Func<TIn3, OperateResult<TIn4>> trans3, Func<TIn4, OperateResult<TIn5>> trans4, Func<TIn5, OperateResult<TIn6>> trans5, Func<TIn6, OperateResult<TIn7>> trans6, Func<TIn7, OperateResult<TResult>> trans7)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            OperateResult<TIn2> result2 = trans1(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result2);
            }
            OperateResult<TIn3> result3 = trans2(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result3);
            }
            OperateResult<TIn4> result4 = trans3(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result4);
            }
            OperateResult<TIn5> result5 = trans4(result4.Content);
            if (!result5.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result5);
            }
            OperateResult<TIn6> result6 = trans5(result5.Content);
            if (!result6.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result6);
            }
            OperateResult<TIn7> result7 = trans6(result6.Content);
            if (!result7.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result7);
            }
            return trans7(result7.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8>(OperateResult<TIn1> result, Func<TIn1, OperateResult<TIn2>> trans1, Func<TIn2, OperateResult<TIn3>> trans2, Func<TIn3, OperateResult<TIn4>> trans3, Func<TIn4, OperateResult<TIn5>> trans4, Func<TIn5, OperateResult<TIn6>> trans5, Func<TIn6, OperateResult<TIn7>> trans6, Func<TIn7, OperateResult<TIn8>> trans7, Func<TIn8, OperateResult<TResult>> trans8)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            OperateResult<TIn2> result2 = trans1(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result2);
            }
            OperateResult<TIn3> result3 = trans2(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result3);
            }
            OperateResult<TIn4> result4 = trans3(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result4);
            }
            OperateResult<TIn5> result5 = trans4(result4.Content);
            if (!result5.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result5);
            }
            OperateResult<TIn6> result6 = trans5(result5.Content);
            if (!result6.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result6);
            }
            OperateResult<TIn7> result7 = trans6(result6.Content);
            if (!result7.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result7);
            }
            OperateResult<TIn8> result8 = trans7(result7.Content);
            if (!result8.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result8);
            }
            return trans8(result8.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9>(OperateResult<TIn1> result, Func<TIn1, OperateResult<TIn2>> trans1, Func<TIn2, OperateResult<TIn3>> trans2, Func<TIn3, OperateResult<TIn4>> trans3, Func<TIn4, OperateResult<TIn5>> trans4, Func<TIn5, OperateResult<TIn6>> trans5, Func<TIn6, OperateResult<TIn7>> trans6, Func<TIn7, OperateResult<TIn8>> trans7, Func<TIn8, OperateResult<TIn9>> trans8, Func<TIn9, OperateResult<TResult>> trans9)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            OperateResult<TIn2> result2 = trans1(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result2);
            }
            OperateResult<TIn3> result3 = trans2(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result3);
            }
            OperateResult<TIn4> result4 = trans3(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result4);
            }
            OperateResult<TIn5> result5 = trans4(result4.Content);
            if (!result5.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result5);
            }
            OperateResult<TIn6> result6 = trans5(result5.Content);
            if (!result6.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result6);
            }
            OperateResult<TIn7> result7 = trans6(result6.Content);
            if (!result7.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result7);
            }
            OperateResult<TIn8> result8 = trans7(result7.Content);
            if (!result8.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result8);
            }
            OperateResult<TIn9> result9 = trans8(result8.Content);
            if (!result9.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result9);
            }
            return trans9(result9.Content);
        }

        public static OperateResult<TResult> GetResultFromOther<TResult, TIn1, TIn2, TIn3, TIn4, TIn5, TIn6, TIn7, TIn8, TIn9, TIn10>(OperateResult<TIn1> result, Func<TIn1, OperateResult<TIn2>> trans1, Func<TIn2, OperateResult<TIn3>> trans2, Func<TIn3, OperateResult<TIn4>> trans3, Func<TIn4, OperateResult<TIn5>> trans4, Func<TIn5, OperateResult<TIn6>> trans5, Func<TIn6, OperateResult<TIn7>> trans6, Func<TIn7, OperateResult<TIn8>> trans7, Func<TIn8, OperateResult<TIn9>> trans8, Func<TIn9, OperateResult<TIn10>> trans9, Func<TIn10, OperateResult<TResult>> trans10)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            OperateResult<TIn2> result2 = trans1(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result2);
            }
            OperateResult<TIn3> result3 = trans2(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result3);
            }
            OperateResult<TIn4> result4 = trans3(result3.Content);
            if (!result4.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result4);
            }
            OperateResult<TIn5> result5 = trans4(result4.Content);
            if (!result5.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result5);
            }
            OperateResult<TIn6> result6 = trans5(result5.Content);
            if (!result6.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result6);
            }
            OperateResult<TIn7> result7 = trans6(result6.Content);
            if (!result7.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result7);
            }
            OperateResult<TIn8> result8 = trans7(result7.Content);
            if (!result8.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result8);
            }
            OperateResult<TIn9> result9 = trans8(result8.Content);
            if (!result9.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result9);
            }
            OperateResult<TIn10> result10 = trans9(result9.Content);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result10);
            }
            return trans10(result10.Content);
        }

        public static OperateResult<TResult> GetSuccessResultFromOther<TResult, TIn>(OperateResult<TIn> result, Func<TIn, TResult> trans)
        {
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<TResult>(result);
            }
            return OperateResult.CreateSuccessResult<TResult>(trans(result.Content));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__1<TResult>
        {
            public static readonly ByteTransformHelper.<>c__1<TResult> <>9;
            public static Func<TResult[], TResult> <>9__1_0;

            static <>c__1()
            {
                ByteTransformHelper.<>c__1<TResult>.<>9 = new ByteTransformHelper.<>c__1<TResult>();
            }

            internal TResult <GetResultFromArray>b__1_0(TResult[] m)
            {
                return m[0];
            }
        }
    }
}

