namespace HslCommunication.Profinet.Yokogawa
{
    using HslCommunication;
    using System;

    public class YokogawaLinkHelper
    {
        public static string GetErrorMsg(byte code)
        {
            switch (code)
            {
                case 1:
                    return StringResources.Language.YokogawaLinkError01;

                case 2:
                    return StringResources.Language.YokogawaLinkError02;

                case 3:
                    return StringResources.Language.YokogawaLinkError03;

                case 4:
                    return StringResources.Language.YokogawaLinkError04;

                case 5:
                    return StringResources.Language.YokogawaLinkError05;

                case 6:
                    return StringResources.Language.YokogawaLinkError06;

                case 7:
                    return StringResources.Language.YokogawaLinkError07;

                case 8:
                    return StringResources.Language.YokogawaLinkError08;

                case 0x41:
                    return StringResources.Language.YokogawaLinkError41;

                case 0x42:
                    return StringResources.Language.YokogawaLinkError42;

                case 0x43:
                    return StringResources.Language.YokogawaLinkError43;

                case 0x44:
                    return StringResources.Language.YokogawaLinkError44;

                case 0x51:
                    return StringResources.Language.YokogawaLinkError51;

                case 0x52:
                    return StringResources.Language.YokogawaLinkError52;

                case 0xf1:
                    return StringResources.Language.YokogawaLinkErrorF1;
            }
            return StringResources.Language.UnknownError;
        }
    }
}

