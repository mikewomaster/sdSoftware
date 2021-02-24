namespace HslCommunication.Controls
{
    using HslCommunication;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Windows.Forms;

    [DefaultBindingProperty("Text"), DefaultProperty("Text")]
    public class UserDrum : UserControl
    {
        private Brush backBrush = new SolidBrush(Color.Silver);
        private Color backColor = Color.Silver;
        private Color borderColor = Color.DimGray;
        private Pen borderPen = new Pen(Color.DimGray);
        private IContainer components = null;
        private StringFormat stringFormat = new StringFormat();
        private string text = string.Empty;
        private Brush textBackBrush = new SolidBrush(Color.DarkGreen);
        private Color textBackColor = Color.DarkGreen;
        private Brush textBrush = new SolidBrush(Color.White);
        private Color textColor = Color.White;

        public UserDrum()
        {
            this.DoubleBuffered = true;
            this.stringFormat.Alignment = StringAlignment.Center;
            this.stringFormat.LineAlignment = StringAlignment.Center;
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components > null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Transparent;
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Margin = new Padding(3, 4, 3, 4);
            base.Name = "UserDrum";
            base.Size = new Size(0xa6, 0xc4);
            base.Paint += new PaintEventHandler(this.UserDrum_Paint);
            base.ResumeLayout(false);
        }

        private void UserDrum_Paint(object sender, PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas() && ((base.Width >= 40) && (base.Height >= 50)))
            {
                Graphics graphics = e.Graphics;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Point[] points = new Point[] { new Point(base.Width / 2, 20), new Point(base.Width - 10, (base.Height * 3) / 10), new Point(base.Width - 10, (base.Height * 7) / 10), new Point(base.Width / 2, base.Height - 20), new Point(10, (base.Height * 7) / 10), new Point(10, (base.Height * 3) / 10), new Point(base.Width / 2, 20) };
                graphics.FillPolygon(this.backBrush, points);
                graphics.DrawLines(this.borderPen, points);
                Point[] pointArray2 = new Point[] { new Point(10, (base.Height * 3) / 10), new Point(base.Width / 2, ((base.Height * 3) / 10) + (base.Height / 0x19)), new Point(base.Width - 10, (base.Height * 3) / 10) };
                graphics.DrawCurve(this.borderPen, pointArray2);
                Point[] pointArray3 = new Point[] { new Point(10, (base.Height * 7) / 10), new Point(base.Width / 2, ((base.Height * 7) / 10) + (base.Height / 0x19)), new Point(base.Width - 10, (base.Height * 7) / 10) };
                graphics.DrawCurve(this.borderPen, pointArray3);
                if (!string.IsNullOrEmpty(this.text))
                {
                    SizeF ef = graphics.MeasureString(this.text, this.Font, (int) (((base.Width - 20) * 3) / 5));
                    if (ef.Width < (((base.Width - 20) * 4) / 5))
                    {
                        ef.Width = ((base.Width - 20) * 3) / 5;
                    }
                    ef.Width += 10f;
                    ef.Height += 5f;
                    Rectangle rect = new Rectangle((base.Width / 2) - ((int) (ef.Width / 2f)), (base.Height / 2) - ((int) (ef.Height / 2f)), (int) ef.Width, (int) ef.Height);
                    graphics.FillRectangle(Brushes.DarkGreen, rect);
                    graphics.DrawString(this.text, this.Font, this.textBrush, rect, this.stringFormat);
                }
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "DimGray"), Category("外观"), Description("获取或设置容器罐的边框色。")]
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                this.borderPen.Dispose();
                this.borderPen = new Pen(value);
                base.Invalidate();
            }
        }

        [Browsable(true), DefaultValue(typeof(Color), "Silver"), Category("外观"), Description("获取或设置容器罐的背景色。")]
        public Color DrumBackColor
        {
            get
            {
                return this.backColor;
            }
            set
            {
                this.backColor = value;
                this.backBrush.Dispose();
                this.backBrush = new SolidBrush(value);
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), DefaultValue(typeof(Color), "White"), Description("获取或设置文本的颜色")]
        public override Color ForeColor
        {
            get
            {
                return this.textColor;
            }
            set
            {
                this.textColor = value;
                this.textBrush.Dispose();
                this.textBrush = new SolidBrush(value);
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), EditorBrowsable(EditorBrowsableState.Always), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("获取或设置在容器上显示的文本")]
        public override string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), DefaultValue(typeof(Color), "DarkGreen"), Description("获取或设置文本的背景色")]
        public Color TextBackColor
        {
            get
            {
                return this.textBackColor;
            }
            set
            {
                this.textBackColor = value;
                this.textBackBrush.Dispose();
                this.textBackBrush = new SolidBrush(value);
                base.Invalidate();
            }
        }
    }
}

