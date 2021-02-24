namespace HslCommunication.Profinet.Siemens
{
    using System;
    using System.Collections.Generic;

    public class SiemensDateTime
    {
        public static readonly DateTime SpecMaximumDateTime = new DateTime(0x829, 12, 0x1f, 0x17, 0x3b, 0x3b, 0x3e7);
        public static readonly DateTime SpecMinimumDateTime = new DateTime(0x7c6, 1, 1);

        public static DateTime FromByteArray(byte[] bytes)
        {
            return FromByteArrayImpl(bytes);
        }

        private static DateTime FromByteArrayImpl(IList<byte> bytes)
        {
            if (bytes.Count != 8)
            {
                throw new ArgumentOutOfRangeException("bytes", bytes.Count, string.Format("Parsing a DateTime requires exactly 8 bytes of input data, input data is {0} bytes long.", bytes.Count));
            }
            int year = <FromByteArrayImpl>g__ByteToYear|4_1(bytes[0]);
            int month = <FromByteArrayImpl>g__AssertRangeInclusive|4_2(<FromByteArrayImpl>g__DecodeBcd|4_0(bytes[1]), 1, 12, "month");
            int day = <FromByteArrayImpl>g__AssertRangeInclusive|4_2(<FromByteArrayImpl>g__DecodeBcd|4_0(bytes[2]), 1, 0x1f, "day of month");
            int hour = <FromByteArrayImpl>g__AssertRangeInclusive|4_2(<FromByteArrayImpl>g__DecodeBcd|4_0(bytes[3]), 0, 0x17, "hour");
            int minute = <FromByteArrayImpl>g__AssertRangeInclusive|4_2(<FromByteArrayImpl>g__DecodeBcd|4_0(bytes[4]), 0, 0x3b, "minute");
            int second = <FromByteArrayImpl>g__AssertRangeInclusive|4_2(<FromByteArrayImpl>g__DecodeBcd|4_0(bytes[5]), 0, 0x3b, "second");
            int num7 = <FromByteArrayImpl>g__AssertRangeInclusive|4_2(<FromByteArrayImpl>g__DecodeBcd|4_0(bytes[6]), 0, 0x63, "first two millisecond digits");
            int num8 = <FromByteArrayImpl>g__AssertRangeInclusive|4_2(bytes[7] >> 4, 0, 9, "third millisecond digit");
            int num9 = <FromByteArrayImpl>g__AssertRangeInclusive|4_2(bytes[7] & 15, 1, 7, "day of week");
            return new DateTime(year, month, day, hour, minute, second, (num7 * 10) + num8);
        }

        public static DateTime[] ToArray(byte[] bytes)
        {
            if ((bytes.Length % 8) > 0)
            {
                throw new ArgumentOutOfRangeException("bytes", bytes.Length, string.Format("Parsing an array of DateTime requires a multiple of 8 bytes of input data, input data is '{0}' long.", bytes.Length));
            }
            int num = bytes.Length / 8;
            DateTime[] timeArray = new DateTime[bytes.Length / 8];
            for (int i = 0; i < num; i++)
            {
                ArraySegment<byte> segment = new ArraySegment<byte>(bytes, i * 8, 8);
                timeArray[i] = FromByteArrayImpl(segment.Array);
            }
            return timeArray;
        }

        public static byte[] ToByteArray(DateTime dateTime)
        {
            if (dateTime < SpecMinimumDateTime)
            {
                throw new ArgumentOutOfRangeException("dateTime", dateTime, string.Format("Date time '{0}' is before the minimum '{1}' supported in S7 date time representation.", dateTime, SpecMinimumDateTime));
            }
            if (dateTime > SpecMaximumDateTime)
            {
                throw new ArgumentOutOfRangeException("dateTime", dateTime, string.Format("Date time '{0}' is after the maximum '{1}' supported in S7 date time representation.", dateTime, SpecMaximumDateTime));
            }
            return new byte[] { <ToByteArray>g__EncodeBcd|5_0(<ToByteArray>g__MapYear|5_1(dateTime.Year)), <ToByteArray>g__EncodeBcd|5_0(dateTime.Month), <ToByteArray>g__EncodeBcd|5_0(dateTime.Day), <ToByteArray>g__EncodeBcd|5_0(dateTime.Hour), <ToByteArray>g__EncodeBcd|5_0(dateTime.Minute), <ToByteArray>g__EncodeBcd|5_0(dateTime.Second), <ToByteArray>g__EncodeBcd|5_0(dateTime.Millisecond / 10), ((byte) (((dateTime.Millisecond % 10) << 4) | <ToByteArray>g__DayOfWeekToInt|5_2(dateTime.DayOfWeek))) };
        }

        public static byte[] ToByteArray(DateTime[] dateTimes)
        {
            List<byte> list = new List<byte>(dateTimes.Length * 8);
            foreach (DateTime time in dateTimes)
            {
                list.AddRange(ToByteArray(time));
            }
            return list.ToArray();
        }
    }
}

