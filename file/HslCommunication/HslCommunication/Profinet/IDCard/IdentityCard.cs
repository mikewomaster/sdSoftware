namespace HslCommunication.Profinet.IDCard
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class IdentityCard
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Address>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <Birthday>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Id>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Name>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Nation>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Organ>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private byte[] <Portrait>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Sex>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <ValidityEndDate>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DateTime <ValidityStartDate>k__BackingField;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("姓名：" + this.Name);
            builder.Append(Environment.NewLine);
            builder.Append("性别：" + this.Sex);
            builder.Append(Environment.NewLine);
            builder.Append("民族：" + this.Nation);
            builder.Append(Environment.NewLine);
            builder.Append("身份证号：" + this.Id);
            builder.Append(Environment.NewLine);
            builder.Append(string.Format("出身日期：{0}年{1}月{2}日", this.Birthday.Year, this.Birthday.Month, this.Birthday.Day));
            builder.Append(Environment.NewLine);
            builder.Append("地址：" + this.Address);
            builder.Append(Environment.NewLine);
            builder.Append("发证机关：" + this.Organ);
            builder.Append(Environment.NewLine);
            builder.Append(string.Format("有效日期：{0}年{1}月{2}日 - {3}年{4}月{5}日", new object[] { this.ValidityStartDate.Year, this.ValidityStartDate.Month, this.ValidityStartDate.Day, this.ValidityEndDate.Year, this.ValidityEndDate.Month, this.ValidityEndDate.Day }));
            builder.Append(Environment.NewLine);
            return builder.ToString();
        }

        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Nation { get; set; }

        public string Organ { get; set; }

        public byte[] Portrait { get; set; }

        public string Sex { get; set; }

        public DateTime ValidityEndDate { get; set; }

        public DateTime ValidityStartDate { get; set; }
    }
}

