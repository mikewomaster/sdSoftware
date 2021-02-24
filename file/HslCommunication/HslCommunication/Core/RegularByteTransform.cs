namespace HslCommunication.Core
{
    using System;

    public class RegularByteTransform : ByteTransformBase
    {
        public RegularByteTransform()
        {
        }

        public RegularByteTransform(DataFormat dataFormat) : base(dataFormat)
        {
        }

        public override IByteTransform CreateByDateFormat(DataFormat dataFormat)
        {
            return new RegularByteTransform(dataFormat) { IsStringReverseByteWord = base.IsStringReverseByteWord };
        }

        public override string ToString()
        {
            return string.Format("RegularByteTransform[{0}]", base.DataFormat);
        }
    }
}

