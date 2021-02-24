namespace HslCommunication.BasicFramework
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Management;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Security.Principal;
    using System.Text;
    using System.Windows.Forms;

    public class SoftAuthorize : SoftFileSaveBase
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <FinalCode>k__BackingField = "";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <HasLoadByFile>k__BackingField = false;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsReleaseVersion>k__BackingField = false;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsSoftTrial>k__BackingField = false;
        private const uint CREATE_NEW = 1;
        private const uint DFP_GET_VERSION = 0x74080;
        private const uint DFP_RECEIVE_DRIVE_DATA = 0x7c088;
        private const uint DFP_SEND_DRIVE_COMMAND = 0x7c084;
        private const uint FILE_SHARE_READ = 1;
        private const uint FILE_SHARE_WRITE = 2;
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private string machine_code = "";
        private const uint OPEN_EXISTING = 3;
        public static readonly string TextCode = "Code";

        public SoftAuthorize([Optional, DefaultParameterValue(false)] bool UseAdmin)
        {
            this.machine_code = GetInfo(UseAdmin);
            base.LogHeaderText = "SoftAuthorize";
        }

        private static void ChangeByteOrder(byte[] charArray)
        {
            for (int i = 0; i < charArray.Length; i += 2)
            {
                byte num = charArray[i];
                charArray[i] = charArray[i + 1];
                charArray[i + 1] = num;
            }
        }

        public bool CheckAuthorize(string code, Func<string, string> encrypt)
        {
            if (code != encrypt(this.GetMachineCodeString()))
            {
                return false;
            }
            this.FinalCode = code;
            this.SaveToFile();
            return true;
        }

        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern int CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
        private static extern IntPtr CreateFile(string lpFileName, [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess, [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode, IntPtr lpSecurityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition, [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);
        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern int DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, ref GetVersionOutParams lpOutBuffer, uint nOutBufferSize, ref uint lpBytesReturned, [Out] IntPtr lpOverlapped);
        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern int DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, ref SendCmdInParams lpInBuffer, uint nInBufferSize, ref SendCmdOutParams lpOutBuffer, uint nOutBufferSize, ref uint lpBytesReturned, [Out] IntPtr lpOverlapped);
        private static HardDiskInfo GetHardDiskInfo(IdSector phdinfo)
        {
            HardDiskInfo info = new HardDiskInfo();
            ChangeByteOrder(phdinfo.sModelNumber);
            info.ModuleNumber = Encoding.ASCII.GetString(phdinfo.sModelNumber).Trim();
            ChangeByteOrder(phdinfo.sFirmwareRev);
            info.Firmware = Encoding.ASCII.GetString(phdinfo.sFirmwareRev).Trim();
            ChangeByteOrder(phdinfo.sSerialNumber);
            info.SerialNumber = Encoding.ASCII.GetString(phdinfo.sSerialNumber).Trim();
            info.Capacity = (phdinfo.ulTotalAddressableSectors / 2) / 0x400;
            return info;
        }

        public static HardDiskInfo GetHddInfo([Optional, DefaultParameterValue(0)] byte driveIndex)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32S:
                    throw new NotSupportedException("Win32s is not supported.");

                case PlatformID.Win32Windows:
                    return GetHddInfoArrayx(driveIndex);

                case PlatformID.Win32NT:
                    return GetHddInfoNT(driveIndex);

                case PlatformID.WinCE:
                    throw new NotSupportedException("WinCE is not supported.");
            }
            throw new NotSupportedException("Unknown Platform.");
        }

        private static HardDiskInfo GetHddInfoArrayx(byte driveIndex)
        {
            GetVersionOutParams lpOutBuffer = new GetVersionOutParams();
            SendCmdInParams lpInBuffer = new SendCmdInParams();
            SendCmdOutParams params3 = new SendCmdOutParams();
            uint lpBytesReturned = 0;
            IntPtr hDevice = CreateFile("＼＼.＼Smartvsd", FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);
            if (hDevice == IntPtr.Zero)
            {
                throw new Exception("Open smartvsd.vxd failed.");
            }
            if (DeviceIoControl(hDevice, 0x74080, IntPtr.Zero, 0, ref lpOutBuffer, (uint) Marshal.SizeOf(lpOutBuffer), ref lpBytesReturned, IntPtr.Zero) == 0)
            {
                CloseHandle(hDevice);
                throw new Exception("DeviceIoControl failed:DFP_GET_VERSION");
            }
            if ((lpOutBuffer.fCapabilities & 1) == 0)
            {
                CloseHandle(hDevice);
                throw new Exception("Error: IDE identify command not supported.");
            }
            if ((driveIndex & 1) > 0)
            {
                lpInBuffer.irDriveRegs.bDriveHeadReg = 0xb0;
            }
            else
            {
                lpInBuffer.irDriveRegs.bDriveHeadReg = 160;
            }
            if ((lpOutBuffer.fCapabilities & (((int) 0x10) >> driveIndex)) > 0L)
            {
                CloseHandle(hDevice);
                throw new Exception(string.Format("Drive {0} is a ATAPI device, we don’t detect it", driveIndex + 1));
            }
            lpInBuffer.irDriveRegs.bCommandReg = 0xec;
            lpInBuffer.bDriveNumber = driveIndex;
            lpInBuffer.irDriveRegs.bSectorCountReg = 1;
            lpInBuffer.irDriveRegs.bSectorNumberReg = 1;
            lpInBuffer.cBufferSize = 0x200;
            if (DeviceIoControl(hDevice, 0x7c088, ref lpInBuffer, (uint) Marshal.SizeOf(lpInBuffer), ref params3, (uint) Marshal.SizeOf(params3), ref lpBytesReturned, IntPtr.Zero) == 0)
            {
                CloseHandle(hDevice);
                throw new Exception("DeviceIoControl failed: DFP_RECEIVE_DRIVE_DATA");
            }
            CloseHandle(hDevice);
            return GetHardDiskInfo(params3.bBuffer);
        }

        private static HardDiskInfo GetHddInfoNT(byte driveIndex)
        {
            GetVersionOutParams lpOutBuffer = new GetVersionOutParams();
            SendCmdInParams lpInBuffer = new SendCmdInParams();
            SendCmdOutParams params3 = new SendCmdOutParams();
            uint lpBytesReturned = 0;
            IntPtr hDevice = CreateFile(@"\\.\PhysicalDrive0", FileAccess.ReadWrite, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero);
            if (hDevice == IntPtr.Zero)
            {
                throw new Exception("CreateFile faild.");
            }
            if (DeviceIoControl(hDevice, 0x74080, IntPtr.Zero, 0, ref lpOutBuffer, (uint) Marshal.SizeOf(lpOutBuffer), ref lpBytesReturned, IntPtr.Zero) == 0)
            {
                CloseHandle(hDevice);
                throw new Exception(string.Format("Drive {0} may not exists.", driveIndex + 1));
            }
            if ((lpOutBuffer.fCapabilities & 1) == 0)
            {
                CloseHandle(hDevice);
                throw new Exception("Error: IDE identify command not supported.");
            }
            if ((driveIndex & 1) > 0)
            {
                lpInBuffer.irDriveRegs.bDriveHeadReg = 0xb0;
            }
            else
            {
                lpInBuffer.irDriveRegs.bDriveHeadReg = 160;
            }
            if ((lpOutBuffer.fCapabilities & (((int) 0x10) >> driveIndex)) > 0L)
            {
                CloseHandle(hDevice);
                throw new Exception(string.Format("Drive {0} is a ATAPI device, we don’t detect it.", driveIndex + 1));
            }
            lpInBuffer.irDriveRegs.bCommandReg = 0xec;
            lpInBuffer.bDriveNumber = driveIndex;
            lpInBuffer.irDriveRegs.bSectorCountReg = 1;
            lpInBuffer.irDriveRegs.bSectorNumberReg = 1;
            lpInBuffer.cBufferSize = 0x200;
            if (DeviceIoControl(hDevice, 0x7c088, ref lpInBuffer, (uint) Marshal.SizeOf(lpInBuffer), ref params3, (uint) Marshal.SizeOf(params3), ref lpBytesReturned, IntPtr.Zero) == 0)
            {
                CloseHandle(hDevice);
                throw new Exception("DeviceIoControl failed: DFP_RECEIVE_DRIVE_DATA");
            }
            CloseHandle(hDevice);
            return GetHardDiskInfo(params3.bBuffer);
        }

        public static string GetInfo(bool UseAdmin)
        {
            string s = "";
            s = (((s + HWID.BIOS + "|") + HWID.CPU + "|") + HWID.HDD + "|") + HWID.BaseBoard + "|";
            if (HWID.IsServer)
            {
                s = s + HWID.SCSI + "|";
            }
            string environmentVariable = Environment.GetEnvironmentVariable("systemdrive");
            string str3 = "\"" + environmentVariable + "\"";
            ManagementObject obj2 = new ManagementObject("win32_logicaldisk.deviceid=" + str3);
            obj2.Get();
            s = s + obj2["VolumeSerialNumber"].ToString() + "|";
            ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystemProduct").GetInstances();
            foreach (ManagementObject obj3 in instances)
            {
                s = s + ((string) obj3.Properties["UUID"].Value);
                break;
            }
            s = s + "|";
            if (UseAdmin)
            {
                WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    HardDiskInfo hddInfo = GetHddInfo(0);
                    s = s + hddInfo.SerialNumber;
                }
                else
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo {
                        FileName = Application.ExecutablePath,
                        Verb = "runas"
                    };
                    Process.Start(startInfo);
                    Application.Exit();
                }
            }
            SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();
            return SoftBasic.ByteToHexString(provider.ComputeHash(Encoding.Unicode.GetBytes(s))).Substring(0, 0x19);
        }

        public string GetMachineCodeString()
        {
            return this.machine_code;
        }

        private static string GetWMIIdent(string Class, string Property)
        {
            string propertyValue = "";
            ManagementObjectCollection instances = new ManagementClass(Class).GetInstances();
            foreach (ManagementBaseObject obj2 in instances)
            {
                propertyValue = obj2.GetPropertyValue(Property) as string;
                if (propertyValue != "")
                {
                    return propertyValue;
                }
            }
            return propertyValue;
        }

        private static string GetWMIIdent(string Class, params string[] Propertys)
        {
            string ident = "";
            Array.ForEach<string>(Propertys, delegate (string prop) {
                ident = ident + GetWMIIdent(Class, prop) + " ";
            });
            return ident;
        }

        public bool IsAuthorizeSuccess(Func<string, string> encrypt)
        {
            if (this.IsReleaseVersion)
            {
                return true;
            }
            if (encrypt(this.GetMachineCodeString()) == this.FinalCode)
            {
                return true;
            }
            this.FinalCode = "";
            this.SaveToFile();
            return false;
        }

        public override void LoadByFile()
        {
            base.LoadByFile(m => SoftSecurity.MD5Decrypt(m));
        }

        public override void LoadByString(string content)
        {
            JObject json = JObject.Parse(content);
            this.FinalCode = SoftBasic.GetValueFromJsonObject<string>(json, TextCode, this.FinalCode);
            this.HasLoadByFile = true;
        }

        public override void SaveToFile()
        {
            base.SaveToFile(m => SoftSecurity.MD5Encrypt(m));
        }

        public override string ToSaveString()
        {
            JObject obj2 = new JObject {
                { 
                    TextCode,
                    new JValue(this.FinalCode)
                }
            };
            return obj2.ToString();
        }

        public string FinalCode { get; private set; }

        private bool HasLoadByFile { get; set; }

        public bool IsReleaseVersion { get; set; }

        public bool IsSoftTrial { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SoftAuthorize.<>c <>9 = new SoftAuthorize.<>c();
            public static Converter<string, string> <>9__22_0;
            public static Converter<string, string> <>9__23_0;

            internal string <LoadByFile>b__23_0(string m)
            {
                return SoftSecurity.MD5Decrypt(m);
            }

            internal string <SaveToFile>b__22_0(string m)
            {
                return SoftSecurity.MD5Encrypt(m);
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct DriverStatus
        {
            public byte bDriverError;
            public byte bIDEStatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
            public byte[] bReserved;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
            public uint[] dwReserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct GetVersionOutParams
        {
            public byte bVersion;
            public byte bRevision;
            public byte bReserved;
            public byte bIDEDeviceMap;
            public uint fCapabilities;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
            public uint[] dwReserved;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct HardDiskInfo
        {
            public string ModuleNumber;
            public string Firmware;
            public string SerialNumber;
            public uint Capacity;
        }

        private class HWID
        {
            public static string BaseBoard
            {
                get
                {
                    string[] propertys = new string[] { "SerialNumber", "PartNumber" };
                    return SoftAuthorize.GetWMIIdent("Win32_BaseBoard", propertys);
                }
            }

            public static string BIOS
            {
                get
                {
                    string[] propertys = new string[] { "Manufacturer", "SerialNumber", "SMBIOSBIOSVersion", "IdentificationCode" };
                    return SoftAuthorize.GetWMIIdent("Win32_BIOS", propertys);
                }
            }

            public static string CPU
            {
                get
                {
                    string[] propertys = new string[] { "ProcessorId", "UniqueId", "Name" };
                    return SoftAuthorize.GetWMIIdent("Win32_Processor", propertys);
                }
            }

            public static string GPU
            {
                get
                {
                    string[] propertys = new string[] { "DriverVersion", "Name" };
                    return SoftAuthorize.GetWMIIdent("Win32_VideoController", propertys);
                }
            }

            public static string HDD
            {
                get
                {
                    string[] propertys = new string[] { "Model", "TotalHeads" };
                    return SoftAuthorize.GetWMIIdent("Win32_DiskDrive", propertys);
                }
            }

            public static bool IsServer
            {
                get
                {
                    return HDD.Contains("SCSI");
                }
            }

            public static string MAC
            {
                get
                {
                    return SoftAuthorize.GetWMIIdent("Win32_NetworkAdapterConfiguration", "MACAddress");
                }
            }

            public static string OS
            {
                get
                {
                    string[] propertys = new string[] { "SerialNumber", "Name" };
                    return SoftAuthorize.GetWMIIdent("Win32_OperatingSystem", propertys);
                }
            }

            public static string SCSI
            {
                get
                {
                    string[] propertys = new string[] { "DeviceID", "Name" };
                    return SoftAuthorize.GetWMIIdent("Win32_SCSIController", propertys);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct IdeRegs
        {
            public byte bFeaturesReg;
            public byte bSectorCountReg;
            public byte bSectorNumberReg;
            public byte bCylLowReg;
            public byte bCylHighReg;
            public byte bDriveHeadReg;
            public byte bCommandReg;
            public byte bReserved;
        }

        [StructLayout(LayoutKind.Sequential, Size=0x200, Pack=1)]
        internal struct IdSector
        {
            public ushort wGenConfig;
            public ushort wNumCyls;
            public ushort wReserved;
            public ushort wNumHeads;
            public ushort wBytesPerTrack;
            public ushort wBytesPerSector;
            public ushort wSectorsPerTrack;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
            public ushort[] wVendorUnique;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=20)]
            public byte[] sSerialNumber;
            public ushort wBufferType;
            public ushort wBufferSize;
            public ushort wECCSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
            public byte[] sFirmwareRev;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=40)]
            public byte[] sModelNumber;
            public ushort wMoreVendorUnique;
            public ushort wDoubleWordIO;
            public ushort wCapabilities;
            public ushort wReserved1;
            public ushort wPIOTiming;
            public ushort wDMATiming;
            public ushort wBS;
            public ushort wNumCurrentCyls;
            public ushort wNumCurrentHeads;
            public ushort wNumCurrentSectorsPerTrack;
            public uint ulCurrentSectorCapacity;
            public ushort wMultSectorStuff;
            public uint ulTotalAddressableSectors;
            public ushort wSingleWordDMA;
            public ushort wMultiWordDMA;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x80)]
            public byte[] bReserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct SendCmdInParams
        {
            public uint cBufferSize;
            public SoftAuthorize.IdeRegs irDriveRegs;
            public byte bDriveNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
            public byte[] bReserved;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
            public uint[] dwReserved;
            public byte bBuffer;
        }

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        internal struct SendCmdOutParams
        {
            public uint cBufferSize;
            public HslCommunication.BasicFramework.SoftAuthorize.DriverStatus DriverStatus;
            public SoftAuthorize.IdSector bBuffer;
        }
    }
}

