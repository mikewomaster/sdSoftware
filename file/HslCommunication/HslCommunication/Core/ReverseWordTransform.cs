namespace HslCommunication.Core
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using System;

    public class ReverseWordTransform : ByteTransformBase
    {
        public ReverseWordTransform()
        {
            base.DataFormat = DataFormat.ABCD;
        }

        public ReverseWordTransform(DataFormat dataFormat) : base(dataFormat)
        {
        }

        public override IByteTransform CreateByDateFormat(DataFormat dataFormat)
        {
            return new ReverseWordTransform(dataFormat) { IsStringReverseByteWord = base.IsStringReverseByteWord };
        }

        private byte[] ReverseBytesByWord(byte[] buffer, int index, int length)
        {
            if (buffer == null)
            {
                return null;
            }
            return SoftBasic.BytesReverseByWord(buffer.SelectMiddle<byte>(index, length));
        }

        public override string ToString()
        {
            return string.Format("ReverseWordTransform[{0}]", base.DataFormat);
        }

        public override byte[] TransByte(short[] values)
        {
            return SoftBasic.BytesReverseByWord(base.TransByte(values));
        }

        public override byte[] TransByte(ushort[] values)
        {
            return SoftBasic.BytesReverseByWord(base.TransByte(values));
        }

        public override short TransInt16(byte[] buffer, int index)
        {
            return base.TransInt16(this.ReverseBytesByWord(buffer, index, 2), 0);
        }

        public override ushort TransUInt16(byte[] buffer, int index)
        {
            return base.TransUInt16(this.ReverseBytesByWord(buffer, index, 2), 0);
        }
    }
}

