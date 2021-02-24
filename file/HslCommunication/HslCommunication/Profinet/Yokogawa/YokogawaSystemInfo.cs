namespace HslCommunication.Profinet.Yokogawa
{
    using HslCommunication;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class YokogawaSystemInfo
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <CpuType>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <ProgramAreaSize>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Revision>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <SystemID>k__BackingField;

        public static OperateResult<YokogawaSystemInfo> Prase(byte[] content)
        {
            try
            {
                YokogawaSystemInfo info = new YokogawaSystemInfo();
                char[] trimChars = new char[2];
                trimChars[1] = ' ';
                info.SystemID = Encoding.ASCII.GetString(content, 0, 0x10).Trim(trimChars);
                char[] chArray2 = new char[2];
                chArray2[1] = ' ';
                info.Revision = Encoding.ASCII.GetString(content, 0x10, 8).Trim(chArray2);
                if ((content[0x19] == 1) || (content[0x19] == 0x11))
                {
                    info.CpuType = "Sequence";
                }
                else if ((content[0x19] == 2) || (content[0x19] == 0x12))
                {
                    info.CpuType = "BASIC";
                }
                else
                {
                    info.CpuType = StringResources.Language.UnknownError;
                }
                info.ProgramAreaSize = (content[0x1a] * 0x100) + content[0x1b];
                return OperateResult.CreateSuccessResult<YokogawaSystemInfo>(info);
            }
            catch (Exception exception)
            {
                string[] textArray1 = new string[] { "Prase YokogawaSystemInfo failed: ", exception.Message, Environment.NewLine, "Source: ", content.ToHexString(' ') };
                return new OperateResult<YokogawaSystemInfo>(string.Concat(textArray1));
            }
        }

        public override string ToString()
        {
            return ("YokogawaSystemInfo[" + this.SystemID + "]");
        }

        public string CpuType { get; set; }

        public int ProgramAreaSize { get; set; }

        public string Revision { get; set; }

        public string SystemID { get; set; }
    }
}

