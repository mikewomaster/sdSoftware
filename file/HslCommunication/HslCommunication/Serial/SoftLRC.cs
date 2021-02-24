namespace HslCommunication.Serial
{
    using HslCommunication.BasicFramework;
    using System;

    public class SoftLRC
    {
        public static bool CheckLRC(byte[] value)
        {
            if (value == null)
            {
                return false;
            }
            int length = value.Length;
            byte[] destinationArray = new byte[length - 1];
            Array.Copy(value, 0, destinationArray, 0, destinationArray.Length);
            return (LRC(destinationArray)[length - 1] == value[length - 1]);
        }

        public static byte[] LRC(byte[] value)
        {
            if (value == null)
            {
                return null;
            }
            int num = 0;
            for (int i = 0; i < value.Length; i++)
            {
                num += value[i];
            }
            num = num % 0x100;
            num = 0x100 - num;
            byte[] buffer = new byte[] { (byte) num };
            return SoftBasic.SpliceTwoByteArray(value, buffer);
        }
    }
}

