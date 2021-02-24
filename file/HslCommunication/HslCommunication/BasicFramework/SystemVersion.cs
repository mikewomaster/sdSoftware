namespace HslCommunication.BasicFramework
{
    using System;

    [Serializable]
    public sealed class SystemVersion
    {
        private int m_EditVersion;
        private int m_InnerVersion;
        private int m_MainVersion;
        private int m_SecondaryVersion;

        public SystemVersion(string VersionString)
        {
            this.m_MainVersion = 2;
            this.m_SecondaryVersion = 0;
            this.m_EditVersion = 0;
            this.m_InnerVersion = 0;
            char[] separator = new char[] { '.' };
            string[] strArray = VersionString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length >= 1)
            {
                this.m_MainVersion = Convert.ToInt32(strArray[0]);
            }
            if (strArray.Length >= 2)
            {
                this.m_SecondaryVersion = Convert.ToInt32(strArray[1]);
            }
            if (strArray.Length >= 3)
            {
                this.m_EditVersion = Convert.ToInt32(strArray[2]);
            }
            if (strArray.Length >= 4)
            {
                this.m_InnerVersion = Convert.ToInt32(strArray[3]);
            }
        }

        public SystemVersion(int main, int sec, int edit)
        {
            this.m_MainVersion = 2;
            this.m_SecondaryVersion = 0;
            this.m_EditVersion = 0;
            this.m_InnerVersion = 0;
            this.m_MainVersion = main;
            this.m_SecondaryVersion = sec;
            this.m_EditVersion = edit;
        }

        public SystemVersion(int main, int sec, int edit, int inner)
        {
            this.m_MainVersion = 2;
            this.m_SecondaryVersion = 0;
            this.m_EditVersion = 0;
            this.m_InnerVersion = 0;
            this.m_MainVersion = main;
            this.m_SecondaryVersion = sec;
            this.m_EditVersion = edit;
            this.m_InnerVersion = inner;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(SystemVersion SV1, SystemVersion SV2)
        {
            if (SV1.MainVersion != SV2.MainVersion)
            {
                return false;
            }
            if (SV1.SecondaryVersion != SV2.SecondaryVersion)
            {
                return false;
            }
            if (SV1.m_EditVersion != SV2.m_EditVersion)
            {
                return false;
            }
            if (SV1.InnerVersion != SV2.InnerVersion)
            {
                return false;
            }
            return true;
        }

        public static bool operator >(SystemVersion SV1, SystemVersion SV2)
        {
            if (SV1.MainVersion > SV2.MainVersion)
            {
                return true;
            }
            if (SV1.MainVersion < SV2.MainVersion)
            {
                return false;
            }
            if (SV1.SecondaryVersion > SV2.SecondaryVersion)
            {
                return true;
            }
            if (SV1.SecondaryVersion < SV2.SecondaryVersion)
            {
                return false;
            }
            if (SV1.EditVersion > SV2.EditVersion)
            {
                return true;
            }
            if (SV1.EditVersion < SV2.EditVersion)
            {
                return false;
            }
            return ((SV1.InnerVersion > SV2.InnerVersion) || ((SV1.InnerVersion < SV2.InnerVersion) && false));
        }

        public static bool operator !=(SystemVersion SV1, SystemVersion SV2)
        {
            return ((SV1.MainVersion != SV2.MainVersion) || ((SV1.SecondaryVersion != SV2.SecondaryVersion) || ((SV1.m_EditVersion != SV2.m_EditVersion) || (SV1.InnerVersion != SV2.InnerVersion))));
        }

        public static bool operator <(SystemVersion SV1, SystemVersion SV2)
        {
            if (SV1.MainVersion < SV2.MainVersion)
            {
                return true;
            }
            if (SV1.MainVersion > SV2.MainVersion)
            {
                return false;
            }
            if (SV1.SecondaryVersion < SV2.SecondaryVersion)
            {
                return true;
            }
            if (SV1.SecondaryVersion > SV2.SecondaryVersion)
            {
                return false;
            }
            if (SV1.EditVersion < SV2.EditVersion)
            {
                return true;
            }
            if (SV1.EditVersion > SV2.EditVersion)
            {
                return false;
            }
            return ((SV1.InnerVersion < SV2.InnerVersion) || ((SV1.InnerVersion > SV2.InnerVersion) && false));
        }

        public override string ToString()
        {
            if (this.InnerVersion == 0)
            {
                return this.ToString("N");
            }
            return this.ToString("C");
        }

        public string ToString(string format)
        {
            if (format == "C")
            {
                return string.Format("{0}.{1}.{2}.{3}", new object[] { this.MainVersion, this.SecondaryVersion, this.EditVersion, this.InnerVersion });
            }
            if (format == "N")
            {
                return string.Format("{0}.{1}.{2}", this.MainVersion, this.SecondaryVersion, this.EditVersion);
            }
            if (format == "S")
            {
                return string.Format("{0}.{1}", this.MainVersion, this.SecondaryVersion);
            }
            return this.ToString();
        }

        public int EditVersion
        {
            get
            {
                return this.m_EditVersion;
            }
        }

        public int InnerVersion
        {
            get
            {
                return this.m_InnerVersion;
            }
        }

        public int MainVersion
        {
            get
            {
                return this.m_MainVersion;
            }
        }

        public int SecondaryVersion
        {
            get
            {
                return this.m_SecondaryVersion;
            }
        }
    }
}

