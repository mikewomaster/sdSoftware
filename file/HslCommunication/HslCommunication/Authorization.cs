namespace HslCommunication
{
    using HslCommunication.BasicFramework;
    using HslCommunication.Enthernet;
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;

    public class Authorization
    {
        private static readonly string Declaration = "禁止对本组件进行反编译，篡改源代码，如果用于商业用途，将追究法律责任，如需注册码，请联系作者，QQ号：200962190，邮箱：hsl200909@163.com，欢迎企业合作。";
        internal static int hidahwdauushduasdhu = 0x5ba0;
        internal static long iahsduiwikaskfhishfdi = 0L;
        internal static long moashdawidaisaosdas = 0L;
        internal static long naihsdadaasdasdiwasdaid = 0L;
        internal static int nasidhadguawdbasd = 0x3e8;
        private static DateTime niahdiahduasdbubfas = DateTime.Now;
        internal static int niasdhasdguawdwdad = 0x3039;
        internal static int nuasgdaaydbishdgas = 0;
        internal static double nuasgdawydaishdgas = 24.0;
        internal static double nuasgdawydbishcgas = 8.0;
        internal static int nuasgdawydbishdgas = 8;
        internal static int zxnkasdhiashifshfsofh = 0;

        static Authorization()
        {
            niahdiahduasdbubfas = iashdagsdawbdawda();
            if (naihsdadaasdasdiwasdaid > 0L)
            {
                naihsdadaasdasdiwasdaid = 0L;
            }
            if (!(nuasgdawydaishdgas == 24.0))
            {
                nuasgdawydaishdgas = 24.0;
            }
            if (nuasgdaaydbishdgas > 0)
            {
                nuasgdaaydbishdgas = 0;
            }
            if (nuasgdawydbishdgas != 0x18)
            {
                nuasgdawydbishdgas = 0x18;
            }
        }

        internal static bool asdhuasdgawydaduasdgu()
        {
            return false;
        }

        internal static bool asdniasnfaksndiqwhawfskhfaiw()
        {
            if ((naihsdadaasdasdiwasdaid == niasdhasdguawdwdad) && (nuasgdaaydbishdgas > 0))
            {
                return nuasduagsdwydbasudasd();
            }
            TimeSpan span = (TimeSpan) (iashdagsdawbdawda() - niahdiahduasdbubfas);
            if (span.TotalHours < nuasgdawydbishdgas)
            {
                return nuasduagsdwydbasudasd();
            }
            return asdhuasdgawydaduasdgu();
        }

        internal static bool ashdadgawdaihdadsidas()
        {
            return (niasdhasdguawdwdad == 0x3039);
        }

        private static void asidhiahfaoaksdnasoif(object obj)
        {
            for (int i = 0; i < 0xe10; i++)
            {
                Thread.Sleep(0x3e8);
                if ((naihsdadaasdasdiwasdaid == niasdhasdguawdwdad) && (nuasgdaaydbishdgas > 0))
                {
                    return;
                }
            }
            new NetSimplifyClient("118.24.36.220", 0x4823).ReadCustomerFromServer(500, SoftBasic.FrameworkVersion.ToString());
        }

        internal static DateTime iashdagsaawadawda()
        {
            return DateTime.Now.AddDays(2.0);
        }

        internal static DateTime iashdagsaawbdawda()
        {
            return DateTime.Now.AddDays(1.0);
        }

        internal static DateTime iashdagsdawbdawda()
        {
            return DateTime.Now;
        }

        internal static string nasduabwduadawdb(string miawdiawduasdhasd)
        {
            StringBuilder builder = new StringBuilder();
            MD5 md = MD5.Create();
            byte[] buffer = md.ComputeHash(Encoding.Unicode.GetBytes(miawdiawduasdhasd));
            md.Clear();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append((0xff - buffer[i]).ToString("X2"));
            }
            return builder.ToString();
        }

        internal static bool nuasduagsdwydbasudasd()
        {
            return true;
        }

        internal static bool nzugaydgwadawdibbas()
        {
            moashdawidaisaosdas += 1L;
            if ((naihsdadaasdasdiwasdaid == niasdhasdguawdwdad) && (nuasgdaaydbishdgas > 0))
            {
                return nuasduagsdwydbasudasd();
            }
            TimeSpan span = (TimeSpan) (iashdagsdawbdawda() - niahdiahduasdbubfas);
            if (span.TotalHours < nuasgdawydaishdgas)
            {
                return nuasduagsdwydbasudasd();
            }
            return asdhuasdgawydaduasdgu();
        }

        internal static void oasjodaiwfsodopsdjpasjpf()
        {
            Interlocked.Increment(ref iahsduiwikaskfhishfdi);
        }

        public static bool SetAuthorizationCode(string code)
        {
            if (nasduabwduadawdb(code) == "5021B1292F6F5B87104CAA6E4388EA5A")
            {
                nuasgdaaydbishdgas = 1;
                nuasgdawydbishcgas = 286512937.0;
                nuasgdawydaishdgas = 87600.0;
                return nuasduagsdwydbasudasd();
            }
            if (nasduabwduadawdb(code) == "B7D40F4D8B229E02AC463A096BCD5707")
            {
                nuasgdaaydbishdgas = 1;
                nuasgdawydbishcgas = 286512937.0;
                nuasgdawydaishdgas = 2160.0;
                return nuasduagsdwydbasudasd();
            }
            if (nasduabwduadawdb(code) == "2765FFFDDE2A8465A9522442F5A15593")
            {
                nuasgdaaydbishdgas = 0x2710;
                nuasgdawydbishcgas = nuasgdawydbishdgas;
                naihsdadaasdasdiwasdaid = niasdhasdguawdwdad;
                return nuasduagsdwydbasudasd();
            }
            return asdhuasdgawydaduasdgu();
        }
    }
}

