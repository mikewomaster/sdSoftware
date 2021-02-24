namespace HslCommunication
{
    using HslCommunication.Language;
    using System;
    using System.Globalization;

    public static class StringResources
    {
        public static DefaultLanguage Language = new DefaultLanguage();

        static StringResources()
        {
            if (CultureInfo.CurrentCulture.ToString().StartsWith("zh"))
            {
                SetLanguageChinese();
            }
            else
            {
                SeteLanguageEnglish();
            }
        }

        public static void SeteLanguageEnglish()
        {
            Language = new English();
        }

        public static void SetLanguageChinese()
        {
            Language = new DefaultLanguage();
        }
    }
}

