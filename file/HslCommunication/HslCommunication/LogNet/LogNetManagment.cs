namespace HslCommunication.LogNet
{
    using HslCommunication;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class LogNetManagment
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static ILogNet <LogNet>k__BackingField;
        internal const string LogFileHeadString = "Logs_";

        internal static string GetDegreeDescription(HslMessageDegree degree)
        {
            switch (degree)
            {
                case HslMessageDegree.None:
                    return StringResources.Language.LogNetAbandon;

                case HslMessageDegree.FATAL:
                    return StringResources.Language.LogNetFatal;

                case HslMessageDegree.ERROR:
                    return StringResources.Language.LogNetError;

                case HslMessageDegree.WARN:
                    return StringResources.Language.LogNetWarn;

                case HslMessageDegree.INFO:
                    return StringResources.Language.LogNetInfo;

                case HslMessageDegree.DEBUG:
                    return StringResources.Language.LogNetDebug;
            }
            return StringResources.Language.LogNetAbandon;
        }

        public static string GetSaveStringFromException(string text, Exception ex)
        {
            StringBuilder builder = new StringBuilder(text);
            if (ex > null)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    builder.Append(" : ");
                }
                try
                {
                    builder.Append(StringResources.Language.ExceptionMessage);
                    builder.Append(ex.Message);
                    builder.Append(Environment.NewLine);
                    builder.Append(StringResources.Language.ExceptionSource);
                    builder.Append(ex.Source);
                    builder.Append(Environment.NewLine);
                    builder.Append(StringResources.Language.ExceptionStackTrace);
                    builder.Append(ex.StackTrace);
                    builder.Append(Environment.NewLine);
                    builder.Append(StringResources.Language.ExceptionType);
                    builder.Append(ex.GetType().ToString());
                    builder.Append(Environment.NewLine);
                    builder.Append(StringResources.Language.ExceptionTargetSite);
                    StringBuilder builder1 = (ex.TargetSite == null) ? null : builder.Append(ex.TargetSite.ToString());
                }
                catch
                {
                }
                builder.Append(Environment.NewLine);
                builder.Append("\x0002/=================================================[    Exception    ]================================================/");
            }
            return builder.ToString();
        }

        public static ILogNet LogNet
        {
            [CompilerGenerated]
            get
            {
                return <LogNet>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                <LogNet>k__BackingField = value;
            }
        }
    }
}

