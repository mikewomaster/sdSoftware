namespace HslCommunication.ModBus
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Core.Address;
    using HslCommunication.Serial;
    using System;

    public class ModbusInfo
    {
        public const byte FunctionCodeNotSupport = 1;
        public const byte FunctionCodeOverBound = 2;
        public const byte FunctionCodeQuantityOver = 3;
        public const byte FunctionCodeReadWriteException = 4;
        public const byte ReadCoil = 1;
        public const byte ReadDiscrete = 2;
        public const byte ReadInputRegister = 4;
        public const byte ReadRegister = 3;
        public const byte WriteCoil = 15;
        public const byte WriteMaskRegister = 0x16;
        public const byte WriteOneCoil = 5;
        public const byte WriteOneRegister = 6;
        public const byte WriteRegister = 0x10;

        public static OperateResult<ModbusAddress> AnalysisAddress(string address, byte defaultStation, bool isStartWithZero, byte defaultFunction)
        {
            try
            {
                ModbusAddress address2 = new ModbusAddress(address, defaultStation, defaultFunction);
                if (!isStartWithZero)
                {
                    if (address2.Address < 1)
                    {
                        throw new Exception(StringResources.Language.ModbusAddressMustMoreThanOne);
                    }
                    address2.Address = (ushort) (address2.Address - 1);
                }
                return OperateResult.CreateSuccessResult<ModbusAddress>(address2);
            }
            catch (Exception exception)
            {
                return new OperateResult<ModbusAddress> { Message = exception.Message };
            }
        }

        public static OperateResult<byte[]> BuildReadModbusCommand(ModbusAddress mAddress, ushort length)
        {
            return OperateResult.CreateSuccessResult<byte[]>(new byte[] { (byte) mAddress.Station, (byte) mAddress.Function, BitConverter.GetBytes(mAddress.Address)[1], BitConverter.GetBytes(mAddress.Address)[0], BitConverter.GetBytes(length)[1], BitConverter.GetBytes(length)[0] });
        }

        public static OperateResult<byte[]> BuildReadModbusCommand(string address, ushort length, byte station, bool isStartWithZero, byte defaultFunction)
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress(address, station, defaultFunction);
                CheckModbusAddressStart(mAddress, isStartWithZero);
                return BuildReadModbusCommand(mAddress, length);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static OperateResult<byte[]> BuildWriteBoolModbusCommand(ModbusAddress mAddress, bool[] values)
        {
            try
            {
                byte[] buffer = SoftBasic.BoolArrayToByte(values);
                byte[] array = new byte[7 + buffer.Length];
                array[0] = (byte) mAddress.Station;
                array[1] = (byte) mAddress.Function;
                array[2] = BitConverter.GetBytes(mAddress.Address)[1];
                array[3] = BitConverter.GetBytes(mAddress.Address)[0];
                array[4] = (byte) (values.Length / 0x100);
                array[5] = (byte) (values.Length % 0x100);
                array[6] = (byte) buffer.Length;
                buffer.CopyTo(array, 7);
                return OperateResult.CreateSuccessResult<byte[]>(array);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static OperateResult<byte[]> BuildWriteBoolModbusCommand(ModbusAddress mAddress, bool value)
        {
            byte[] buffer = new byte[6];
            buffer[0] = (byte) mAddress.Station;
            buffer[1] = (byte) mAddress.Function;
            buffer[2] = BitConverter.GetBytes(mAddress.Address)[1];
            buffer[3] = BitConverter.GetBytes(mAddress.Address)[0];
            if (value)
            {
                buffer[4] = 0xff;
                buffer[5] = 0;
            }
            else
            {
                buffer[4] = 0;
                buffer[5] = 0;
            }
            return OperateResult.CreateSuccessResult<byte[]>(buffer);
        }

        public static OperateResult<byte[]> BuildWriteBoolModbusCommand(string address, bool[] values, byte station, bool isStartWithZero, byte defaultFunction)
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress(address, station, defaultFunction);
                CheckModbusAddressStart(mAddress, isStartWithZero);
                return BuildWriteBoolModbusCommand(mAddress, values);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static OperateResult<byte[]> BuildWriteBoolModbusCommand(string address, bool value, byte station, bool isStartWithZero, byte defaultFunction)
        {
            try
            {
                if (address.IndexOf('.') <= 0)
                {
                    ModbusAddress mAddress = new ModbusAddress(address, station, defaultFunction);
                    CheckModbusAddressStart(mAddress, isStartWithZero);
                    return BuildWriteBoolModbusCommand(mAddress, value);
                }
                int num = Convert.ToInt32(address.Substring(address.IndexOf('.') + 1));
                if ((num < 0) || (num > 15))
                {
                    return new OperateResult<byte[]>(StringResources.Language.ModbusBitIndexOverstep);
                }
                int num2 = ((int) 1) << num;
                int num3 = ~num2;
                if (!value)
                {
                    num2 = 0;
                }
                return BuildWriteMaskModbusCommand(address.Substring(0, address.IndexOf('.')), (ushort) num3, (ushort) num2, station, isStartWithZero, 0x16);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static OperateResult<byte[]> BuildWriteMaskModbusCommand(ModbusAddress mAddress, ushort andMask, ushort orMask)
        {
            return OperateResult.CreateSuccessResult<byte[]>(new byte[] { (byte) mAddress.Station, (byte) mAddress.Function, BitConverter.GetBytes(mAddress.Address)[1], BitConverter.GetBytes(mAddress.Address)[0], BitConverter.GetBytes(andMask)[1], BitConverter.GetBytes(andMask)[0], BitConverter.GetBytes(orMask)[1], BitConverter.GetBytes(orMask)[0] });
        }

        public static OperateResult<byte[]> BuildWriteMaskModbusCommand(string address, ushort andMask, ushort orMask, byte station, bool isStartWithZero, byte defaultFunction)
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress(address, station, defaultFunction);
                if (mAddress.Function == 3)
                {
                    mAddress.Function = defaultFunction;
                }
                CheckModbusAddressStart(mAddress, isStartWithZero);
                return BuildWriteMaskModbusCommand(mAddress, andMask, orMask);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static OperateResult<byte[]> BuildWriteOneRegisterModbusCommand(ModbusAddress mAddress, short value)
        {
            return OperateResult.CreateSuccessResult<byte[]>(new byte[] { (byte) mAddress.Station, (byte) mAddress.Function, BitConverter.GetBytes(mAddress.Address)[1], BitConverter.GetBytes(mAddress.Address)[0], BitConverter.GetBytes(value)[1], BitConverter.GetBytes(value)[0] });
        }

        public static OperateResult<byte[]> BuildWriteOneRegisterModbusCommand(ModbusAddress mAddress, ushort value)
        {
            return OperateResult.CreateSuccessResult<byte[]>(new byte[] { (byte) mAddress.Station, (byte) mAddress.Function, BitConverter.GetBytes(mAddress.Address)[1], BitConverter.GetBytes(mAddress.Address)[0], BitConverter.GetBytes(value)[1], BitConverter.GetBytes(value)[0] });
        }

        public static OperateResult<byte[]> BuildWriteWordModbusCommand(ModbusAddress mAddress, byte[] values)
        {
            byte[] array = new byte[7 + values.Length];
            array[0] = (byte) mAddress.Station;
            array[1] = (byte) mAddress.Function;
            array[2] = BitConverter.GetBytes(mAddress.Address)[1];
            array[3] = BitConverter.GetBytes(mAddress.Address)[0];
            array[4] = (byte) ((values.Length / 2) / 0x100);
            array[5] = (byte) ((values.Length / 2) % 0x100);
            array[6] = (byte) values.Length;
            values.CopyTo(array, 7);
            return OperateResult.CreateSuccessResult<byte[]>(array);
        }

        public static OperateResult<byte[]> BuildWriteWordModbusCommand(string address, byte[] values, byte station, bool isStartWithZero, byte defaultFunction)
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress(address, station, defaultFunction);
                if (mAddress.Function == 3)
                {
                    mAddress.Function = defaultFunction;
                }
                CheckModbusAddressStart(mAddress, isStartWithZero);
                return BuildWriteWordModbusCommand(mAddress, values);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static OperateResult<byte[]> BuildWriteWordModbusCommand(string address, short value, byte station, bool isStartWithZero, byte defaultFunction)
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress(address, station, defaultFunction);
                if (mAddress.Function == 3)
                {
                    mAddress.Function = defaultFunction;
                }
                CheckModbusAddressStart(mAddress, isStartWithZero);
                return BuildWriteOneRegisterModbusCommand(mAddress, value);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static OperateResult<byte[]> BuildWriteWordModbusCommand(string address, ushort value, byte station, bool isStartWithZero, byte defaultFunction)
        {
            try
            {
                ModbusAddress mAddress = new ModbusAddress(address, station, defaultFunction);
                if (mAddress.Function == 3)
                {
                    mAddress.Function = defaultFunction;
                }
                CheckModbusAddressStart(mAddress, isStartWithZero);
                return BuildWriteOneRegisterModbusCommand(mAddress, value);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        private static void CheckModbusAddressStart(ModbusAddress mAddress, bool isStartWithZero)
        {
            if (!isStartWithZero)
            {
                if (mAddress.Address < 1)
                {
                    throw new Exception(StringResources.Language.ModbusAddressMustMoreThanOne);
                }
                mAddress.Address = (ushort) (mAddress.Address - 1);
            }
        }

        public static byte[] ExplodeRtuCommandToCore(byte[] modbusRtu)
        {
            return modbusRtu.RemoveLast<byte>(2);
        }

        public static byte[] ExplodeTcpCommandToCore(byte[] modbusTcp)
        {
            return modbusTcp.RemoveBegin<byte>(6);
        }

        public static OperateResult<byte[]> ExtractActualData(byte[] response)
        {
            try
            {
                if (response[1] >= 0x80)
                {
                    return new OperateResult<byte[]>(GetDescriptionByErrorCode(response[2]));
                }
                if (response.Length > 3)
                {
                    return OperateResult.CreateSuccessResult<byte[]>(SoftBasic.ArrayRemoveBegin<byte>(response, 3));
                }
                return OperateResult.CreateSuccessResult<byte[]>(new byte[0]);
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]>(exception.Message);
            }
        }

        public static string GetDescriptionByErrorCode(byte code)
        {
            switch (code)
            {
                case 1:
                    return StringResources.Language.ModbusTcpFunctionCodeNotSupport;

                case 2:
                    return StringResources.Language.ModbusTcpFunctionCodeOverBound;

                case 3:
                    return StringResources.Language.ModbusTcpFunctionCodeQuantityOver;

                case 4:
                    return StringResources.Language.ModbusTcpFunctionCodeReadWriteException;
            }
            return StringResources.Language.UnknownError;
        }

        public static byte[] PackCommandToRtu(byte[] modbus)
        {
            return SoftCRC16.CRC16(modbus);
        }

        public static byte[] PackCommandToTcp(byte[] modbus, ushort id)
        {
            byte[] array = new byte[modbus.Length + 6];
            array[0] = BitConverter.GetBytes(id)[1];
            array[1] = BitConverter.GetBytes(id)[0];
            array[4] = BitConverter.GetBytes(modbus.Length)[1];
            array[5] = BitConverter.GetBytes(modbus.Length)[0];
            modbus.CopyTo(array, 6);
            return array;
        }

        public static OperateResult<byte[]> TransAsciiPackCommandToCore(byte[] modbusAscii)
        {
            try
            {
                if (((modbusAscii[0] != 0x3a) || (modbusAscii[modbusAscii.Length - 2] != 13)) || (modbusAscii[modbusAscii.Length - 1] != 10))
                {
                    return new OperateResult<byte[]> { Message = StringResources.Language.ModbusAsciiFormatCheckFailed + modbusAscii.ToHexString(' ') };
                }
                byte[] buffer = SoftBasic.AsciiBytesToBytes(modbusAscii.RemoveDouble<byte>(1, 2));
                if (!SoftLRC.CheckLRC(buffer))
                {
                    return new OperateResult<byte[]> { Message = StringResources.Language.ModbusLRCCheckFailed + buffer.ToHexString(' ') };
                }
                return OperateResult.CreateSuccessResult<byte[]>(buffer.RemoveLast<byte>(1));
            }
            catch (Exception exception)
            {
                return new OperateResult<byte[]> { Message = exception.Message + modbusAscii.ToHexString(' ') };
            }
        }

        public static byte[] TransModbusCoreToAsciiPackCommand(byte[] modbus)
        {
            byte[] buffer2 = SoftBasic.BytesToAsciiBytes(SoftLRC.LRC(modbus));
            byte[][] bytes = new byte[3][];
            bytes[0] = new byte[] { 0x3a };
            bytes[1] = buffer2;
            bytes[2] = new byte[] { 13, 10 };
            return SoftBasic.SpliceByteArray(bytes);
        }
    }
}

