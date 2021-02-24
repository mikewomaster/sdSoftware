namespace HslCommunication.BasicFramework
{
    using HslCommunication;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Mail;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class SoftMail
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <MailFromAddress>k__BackingField = "";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <MailSendAddress>k__BackingField = "";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private SmtpClient <smtpClient>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static long <SoftMailSendFailedCount>k__BackingField = 0L;
        public static SoftMail MailSystem163 = new SoftMail(new Action<SmtpClient>(<>c.<>9.<.cctor>b__27_0), "softmailsendcenter@163.com", "hsl200909@163.com");
        public static SoftMail MailSystemQQ = new SoftMail(new Action<SmtpClient>(<>c.<>9.<.cctor>b__27_1), "974856779@qq.com", "hsl200909@163.com");

        public SoftMail(Action<SmtpClient> mailIni, [Optional, DefaultParameterValue("")] string addr_From, [Optional, DefaultParameterValue("")] string addr_to)
        {
            this.smtpClient = new SmtpClient();
            mailIni(this.smtpClient);
            this.MailFromAddress = addr_From;
            this.MailSendAddress = addr_to;
        }

        private string GetExceptionMail(Exception ex)
        {
            string[] textArray1 = new string[] { 
                StringResources.Language.Time, DateTime.Now.ToString(), Environment.NewLine, StringResources.Language.SoftWare, ex.Source, Environment.NewLine, StringResources.Language.ExceptionMessage, ex.Message, Environment.NewLine, StringResources.Language.ExceptionType, ex.GetType().ToString(), Environment.NewLine, StringResources.Language.ExceptionStackTrace, ex.StackTrace, Environment.NewLine, StringResources.Language.ExceptionTargetSite,
                ex.TargetSite.Name
            };
            return string.Concat(textArray1);
        }

        public bool SendMail(Exception ex)
        {
            return this.SendMail(ex, "");
        }

        public bool SendMail(Exception ex, string addtion)
        {
            if (string.IsNullOrEmpty(this.MailSendAddress))
            {
                return false;
            }
            return this.SendMail(this.MailSendAddress, StringResources.Language.BugSubmit, string.IsNullOrEmpty(addtion) ? this.GetExceptionMail(ex) : ("User：" + addtion + Environment.NewLine + this.GetExceptionMail(ex)));
        }

        public bool SendMail(string subject, string body)
        {
            return this.SendMail(this.MailSendAddress, subject, body);
        }

        public bool SendMail(string subject, string body, bool isHtml)
        {
            return this.SendMail(this.MailSendAddress, subject, body, isHtml);
        }

        public bool SendMail(string addr_to, string subject, string body)
        {
            return this.SendMail(addr_to, subject, body, false);
        }

        public bool SendMail(string addr_to, string subject, string body, bool isHtml)
        {
            string[] textArray1 = new string[] { addr_to };
            return this.SendMail(this.MailFromAddress, StringResources.Language.MailServerCenter, textArray1, subject, body, MailPriority.Normal, isHtml);
        }

        public bool SendMail(string addr_from, string name, string[] addr_to, string subject, string body, MailPriority priority, bool isHtml)
        {
            bool flag2;
            if (SoftMailSendFailedCount > 10L)
            {
                SoftMailSendFailedCount += 1L;
                return true;
            }
            MailMessage message = new MailMessage();
            try
            {
                message.From = new MailAddress(addr_from, name, Encoding.UTF8);
                foreach (string str in addr_to)
                {
                    message.To.Add(str);
                }
                message.Subject = subject;
                message.Body = body;
                MailMessage message2 = message;
                string[] textArray1 = new string[] { message2.Body, Environment.NewLine, Environment.NewLine, Environment.NewLine, StringResources.Language.MailSendTail };
                message2.Body = string.Concat(textArray1);
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                message.Priority = priority;
                message.IsBodyHtml = isHtml;
                this.smtpClient.Send(message);
                SoftMailSendFailedCount = 0L;
                flag2 = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(SoftBasic.GetExceptionMessage(exception));
                SoftMailSendFailedCount += 1L;
                flag2 = false;
            }
            finally
            {
                if (message != null)
                {
                    message.Dispose();
                }
            }
            return flag2;
        }

        private string MailFromAddress { get; set; }

        public string MailSendAddress { get; set; }

        private SmtpClient smtpClient { get; set; }

        private static long SoftMailSendFailedCount
        {
            [CompilerGenerated]
            get
            {
                return <SoftMailSendFailedCount>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                <SoftMailSendFailedCount>k__BackingField = value;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SoftMail.<>c <>9 = new SoftMail.<>c();

            internal void <.cctor>b__27_0(SmtpClient mail)
            {
                mail.Host = "smtp.163.com";
                mail.UseDefaultCredentials = true;
                mail.EnableSsl = true;
                mail.Port = 0x19;
                mail.DeliveryMethod = SmtpDeliveryMethod.Network;
                mail.Credentials = new NetworkCredential("softmailsendcenter", "zxcvbnm6789");
            }

            internal void <.cctor>b__27_1(SmtpClient mail)
            {
                mail.Host = "smtp.qq.com";
                mail.UseDefaultCredentials = true;
                mail.Port = 0x24b;
                mail.EnableSsl = true;
                mail.DeliveryMethod = SmtpDeliveryMethod.Network;
                mail.Credentials = new NetworkCredential("974856779", "tvnlczxdumutbbic");
            }
        }
    }
}

