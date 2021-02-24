namespace HslCommunication.Instrument.DLT
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core;
    using HslCommunication.Core.IMessage;
    using HslCommunication.Core.Net;
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DLT645OverTcp : NetworkDeviceBase
    {
        private string opCode = "00000000";
        private string password = "00000000";
        private string station = "1";

        public DLT645OverTcp(string ipAddress, [Optional, DefaultParameterValue(0x1f6)] int port, [Optional, DefaultParameterValue("1")] string station, [Optional, DefaultParameterValue("")] string password, [Optional, DefaultParameterValue("")] string opCode)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            base.WordLength = 1;
            base.ByteTransform = new ReverseWordTransform();
            this.station = station;
            this.password = string.IsNullOrEmpty(password) ? "00000000" : password;
            this.opCode = string.IsNullOrEmpty(opCode) ? "00000000" : opCode;
        }

        public OperateResult ActiveDeveice()
        {
            return base.ReadFromCoreServer(new byte[] { 0xfe, 0xfe, 0xfe, 0xfe }, false);
        }

        public OperateResult BroadcastTime(DateTime dateTime)
        {
            string str = string.Format("{0:D2}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", new object[] { dateTime.Second, dateTime.Minute, dateTime.Hour, dateTime.Day, dateTime.Month, dateTime.Year % 100 });
            OperateResult<byte[]> result = DLT645.BuildEntireCommand("999999999999", 8, str.ToHexBytes());
            if (!result.IsSuccess)
            {
                return result;
            }
            return base.ReadFromCoreServer(result.Content, false);
        }

        public OperateResult ChangeBaudRate(string baudRate)
        {
            OperateResult<string, int> result = DLT645.AnalysisIntegerAddress(baudRate, this.station);
            if (!result.IsSuccess)
            {
                return result;
            }
            byte num = 0;
            switch (result.Content2)
            {
                case 600:
                    num = 2;
                    break;

                case 0x4b0:
                    num = 4;
                    break;

                case 0x960:
                    num = 8;
                    break;

                case 0x12c0:
                    num = 0x10;
                    break;

                case 0x2580:
                    num = 0x20;
                    break;

                case 0x4b00:
                    num = 0x40;
                    break;

                default:
                    return new OperateResult(StringResources.Language.NotSupportedFunction);
            }
            byte[] dataArea = new byte[] { num };
            OperateResult<byte[]> result2 = DLT645.BuildEntireCommand(result.Content1, 0x17, dataArea);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            OperateResult result4 = DLT645.CheckResponse(result3.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            if (result3.Content[10] == num)
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult(StringResources.Language.DLTErrorWriteReadCheckFailed);
        }

        public OperateResult FreezeCommand(string dataArea)
        {
            OperateResult<string, byte[]> result = DLT645.AnalysisBytesAddress(dataArea, this.station, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            OperateResult<byte[]> result2 = DLT645.BuildEntireCommand(result.Content1, 0x16, result.Content2);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            if (result.Content1 == "999999999999")
            {
                return base.ReadFromCoreServer(result2.Content, false);
            }
            OperateResult<byte[]> result4 = base.ReadFromCoreServer(result2.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            return DLT645.CheckResponse(result4.Content);
        }

        protected override INetMessage GetNewNetMessage()
        {
            return new DLT645Message();
        }

        public override OperateResult<byte[]> Read(string address, ushort length)
        {
            OperateResult<string, byte[]> result = DLT645.AnalysisBytesAddress(address, this.station, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return this.ReadWithAddress(result.Content1, result.Content2);
        }

        public OperateResult<string> ReadAddress()
        {
            OperateResult<byte[]> result = DLT645.BuildEntireCommand("AAAAAAAAAAAA", 0x13, null);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result2);
            }
            OperateResult result3 = DLT645.CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result3);
            }
            this.station = result2.Content.SelectMiddle<byte>(1, 6).Reverse<byte>().ToArray<byte>().ToHexString();
            return OperateResult.CreateSuccessResult<string>(result2.Content.SelectMiddle<byte>(1, 6).Reverse<byte>().ToArray<byte>().ToHexString());
        }

        public override OperateResult<double[]> ReadDouble(string address, ushort length)
        {
            OperateResult<string, byte[]> result = DLT645.AnalysisBytesAddress(address, this.station, length);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<double[]>(result);
            }
            OperateResult<byte[]> result2 = this.ReadWithAddress(result.Content1, result.Content2);
            if (!result2.IsSuccess)
            {
                return OperateResult.CreateFailedResult<double[]>(result2);
            }
            return DLTTransform.TransDoubleFromDLt(result2.Content, length, DLT645.GetFormatWithDataArea(result.Content2));
        }

        public override OperateResult<string> ReadString(string address, ushort length, Encoding encoding)
        {
            OperateResult<byte[]> result = this.Read(address, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<string>(result);
            }
            return DLTTransform.TransStringFromDLt(result.Content, length);
        }

        private OperateResult<byte[]> ReadWithAddress(string address, byte[] dataArea)
        {
            OperateResult<byte[]> result = DLT645.BuildEntireCommand(address, 0x11, dataArea);
            if (!result.IsSuccess)
            {
                return result;
            }
            OperateResult<byte[]> result2 = base.ReadFromCoreServer(result.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult result3 = DLT645.CheckResponse(result2.Content);
            if (!result3.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result3);
            }
            if (result2.Content.Length < 0x10)
            {
                return OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
            }
            return OperateResult.CreateSuccessResult<byte[]>(result2.Content.SelectMiddle<byte>(14, result2.Content.Length - 0x10));
        }

        public override string ToString()
        {
            return string.Format("DLT645OverTcp[{0}:{1}]", this.IpAddress, this.Port);
        }

        public override OperateResult Write(string address, byte[] value)
        {
            OperateResult<string, byte[]> result = DLT645.AnalysisBytesAddress(address, this.station, 1);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            byte[][] bytes = new byte[][] { result.Content2, this.password.ToHexBytes(), this.opCode.ToHexBytes(), value };
            byte[] dataArea = SoftBasic.SpliceByteArray(bytes);
            OperateResult<byte[]> result2 = DLT645.BuildEntireCommand(result.Content1, 0x15, dataArea);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            if (!result3.IsSuccess)
            {
                return result3;
            }
            return DLT645.CheckResponse(result3.Content);
        }

        public OperateResult WriteAddress(string address)
        {
            OperateResult<byte[]> addressByteFromString = DLT645.GetAddressByteFromString(address);
            if (!addressByteFromString.IsSuccess)
            {
                return addressByteFromString;
            }
            OperateResult<byte[]> result2 = DLT645.BuildEntireCommand("AAAAAAAAAAAA", 0x15, addressByteFromString.Content);
            if (!result2.IsSuccess)
            {
                return result2;
            }
            OperateResult<byte[]> result3 = base.ReadFromCoreServer(result2.Content);
            OperateResult result4 = DLT645.CheckResponse(result3.Content);
            if (!result4.IsSuccess)
            {
                return result4;
            }
            if (SoftBasic.IsTwoBytesEquel(result3.Content.SelectMiddle<byte>(1, 6), DLT645.GetAddressByteFromString(address).Content))
            {
                return OperateResult.CreateSuccessResult();
            }
            return new OperateResult(StringResources.Language.DLTErrorWriteReadCheckFailed);
        }

        public string Station
        {
            get
            {
                return this.station;
            }
            set
            {
                this.station = value;
            }
        }
    }
}

