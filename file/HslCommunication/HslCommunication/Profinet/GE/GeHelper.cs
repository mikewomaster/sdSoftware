namespace HslCommunication.Profinet.GE
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core.Address;
    using System;
    using System.Text;

    public class GeHelper
    {
        public static OperateResult<byte[]> BuildReadCommand(long id, GeSRTPAddress address)
        {
            if (((address.DataCode == 10) || (address.DataCode == 12)) || (address.DataCode == 8))
            {
                address.Length = (ushort) (address.Length / 2);
            }
            byte[] data = new byte[] { address.DataCode, BitConverter.GetBytes(address.AddressStart)[0], BitConverter.GetBytes(address.AddressStart)[1], BitConverter.GetBytes(address.Length)[0], BitConverter.GetBytes(address.Length)[1] };
            return BuildReadCoreCommand(id, 4, data);
        }

        public static OperateResult<byte[]> BuildReadCommand(long id, string address, ushort length, bool isBit)
        {
            OperateResult<GeSRTPAddress> result = GeSRTPAddress.ParseFrom(address, length, isBit);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return BuildReadCommand(id, result.Content);
        }

        public static OperateResult<byte[]> BuildReadCoreCommand(long id, byte code, byte[] data)
        {
            byte[] array = new byte[0x38];
            array[0] = 2;
            array[1] = 0;
            array[2] = BitConverter.GetBytes(id)[0];
            array[3] = BitConverter.GetBytes(id)[1];
            array[4] = 0;
            array[5] = 0;
            array[9] = 1;
            array[0x11] = 1;
            array[0x12] = 0;
            array[30] = 6;
            array[0x1f] = 0xc0;
            array[0x24] = 0x10;
            array[0x25] = 14;
            array[40] = 1;
            array[0x29] = 1;
            array[0x2a] = code;
            data.CopyTo(array, 0x2b);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteCommand(long id, GeSRTPAddress address, byte[] value)
        {
            int length = address.Length;
            if (((address.DataCode == 10) || (address.DataCode == 12)) || (address.DataCode == 8))
            {
                length /= 2;
            }
            byte[] array = new byte[0x38 + value.Length];
            array[0] = 2;
            array[1] = 0;
            array[2] = BitConverter.GetBytes(id)[0];
            array[3] = BitConverter.GetBytes(id)[1];
            array[4] = BitConverter.GetBytes(value.Length)[0];
            array[5] = BitConverter.GetBytes(value.Length)[1];
            array[9] = 2;
            array[0x11] = 2;
            array[0x12] = 0;
            array[30] = 9;
            array[0x1f] = 0x80;
            array[0x24] = 0x10;
            array[0x25] = 14;
            array[40] = 1;
            array[0x29] = 1;
            array[0x2a] = 2;
            array[0x30] = 1;
            array[0x31] = 1;
            array[50] = 7;
            array[0x33] = address.DataCode;
            array[0x34] = BitConverter.GetBytes(address.AddressStart)[0];
            array[0x35] = BitConverter.GetBytes(address.AddressStart)[1];
            array[0x36] = BitConverter.GetBytes(length)[0];
            array[0x37] = BitConverter.GetBytes(length)[1];
            value.CopyTo(array, 0x38);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteCommand(long id, string address, bool[] value)
        {
            OperateResult<GeSRTPAddress> result = GeSRTPAddress.ParseFrom(address, (ushort) value.Length, true);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            bool[] array = new bool[(result.Content.AddressStart % 8) + value.Length];
            value.CopyTo(array, (int) (result.Content.AddressStart % 8));
            return BuildWriteCommand(id, result.Content, SoftBasic.BoolArrayToByte(array));
        }

        public static OperateResult<byte[]> BuildWriteCommand(long id, string address, byte[] value)
        {
            OperateResult<GeSRTPAddress> result = GeSRTPAddress.ParseFrom(address, (ushort) value.Length, false);
            if (!result.IsSuccess)
            {
                return OperateResult.CreateFailedResult<byte[]>(result);
            }
            return BuildWriteCommand(id, result.Content, value);
        }

        public static OperateResult<DateTime> ExtraDateTime(byte[] content)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<DateTime>(StringResources.Language.InsufficientPrivileges);
            }
            try
            {
                return OperateResult.CreateSuccessResult<DateTime>(new DateTime(int.Parse(content[5].ToString("X2")) + 0x7d0, int.Parse(content[4].ToString("X2")), int.Parse(content[3].ToString("X2")), int.Parse(content[2].ToString("X2")), int.Parse(content[1].ToString("X2")), int.Parse(content[0].ToString("X2"))));
            }
            catch (Exception exception)
            {
                return new OperateResult<DateTime>(exception.Message + " Source:" + content.ToHexString(' '));
            }
        }

        public static OperateResult<string> ExtraProgramName(byte[] content)
        {
            if (!Authorization.asdniasnfaksndiqwhawfskhfaiw())
            {
                return new OperateResult<string>(StringResources.Language.InsufficientPrivileges);
            }
            try
            {
                return OperateResult.CreateSuccessResult<string>(Encoding.UTF8.GetString(content, 0x12, 0x10).Trim(new char[1]));
            }
            catch (Exception exception)
            {
                return new OperateResult<string>(exception.Message + " Source:" + content.ToHexString(' '));
            }
        }

        public static OperateResult<byte[]> ExtraResponseContent(byte[] content)
        {
            try
            {
                if (content[0] != 3)
                {
                    return new OperateResult<byte[]>(content[0], StringResources.Language.UnknownError + " Source:" + content.ToHexString(' '));
                }
                if (content[0x1f] == 0xd4)
                {
                    ushort err = BitConverter.ToUInt16(content, 0x2a);
                    if (err > 0)
                    {
                        return new OperateResult<byte[]>(err, StringResources.Language.UnknownError);
                    }
                    return OperateResult.CreateSuccessResult<byte[]>(content.SelectMiddle<byte>(0x2c, 6));
                }
                if (content[0x1f] == 0x94)
                {
                    return OperateResult.CreateSuccessResult<byte[]>(content.RemoveBegin<byte>(0x38));
                }
                return new OperateResult<byte[]>("Extra Wrong:" + StringResources.Language.UnknownError + " Source:" + content.ToHexString(' '));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>("Extra Wrong:" + exception.Message + " Source:" + content.ToHexString(' '));
            }
        }
    }
}

