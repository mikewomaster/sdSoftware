namespace HslCommunication.Serial
{
    using System;

    public class SoftCRC16
    {
        public static bool CheckCRC16(byte[] value)
        {
            return CheckCRC16(value, 160, 1);
        }

        public static bool CheckCRC16(byte[] value, byte CH, byte CL)
        {
            if (value == null)
            {
                return false;
            }
            if (value.Length < 2)
            {
                return false;
            }
            int length = value.Length;
            byte[] destinationArray = new byte[length - 2];
            Array.Copy(value, 0, destinationArray, 0, destinationArray.Length);
            byte[] buffer2 = CRC16(destinationArray, CH, CL);
            return ((buffer2[length - 2] == value[length - 2]) && (buffer2[length - 1] == value[length - 1]));
        }

        public static byte[] CRC16(byte[] value)
        {
            return CRC16(value, 160, 1);
        }

        public static byte[] CRC16(byte[] value, byte CH, byte CL)
        {
            byte[] array = new byte[value.Length + 2];
            value.CopyTo(array, 0);
            byte num = 0xff;
            byte num2 = 0xff;
            byte[] buffer2 = value;
            for (int i = 0; i < buffer2.Length; i++)
            {
                num = (byte) (num ^ buffer2[i]);
                for (int j = 0; j <= 7; j++)
                {
                    byte num3 = num2;
                    byte num4 = num;
                    num2 = (byte) (num2 >> 1);
                    num = (byte) (num >> 1);
                    if ((num3 & 1) == 1)
                    {
                        num = (byte) (num | 0x80);
                    }
                    if ((num4 & 1) == 1)
                    {
                        num2 = (byte) (num2 ^ CH);
                        num = (byte) (num ^ CL);
                    }
                }
            }
            array[array.Length - 2] = num;
            array[array.Length - 1] = num2;
            return array;
        }
    }
}

