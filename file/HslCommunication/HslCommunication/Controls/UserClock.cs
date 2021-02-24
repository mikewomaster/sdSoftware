namespace HslCommunication.Controls
{
    using HslCommunication;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Windows.Forms;

    public class UserClock : UserControl
    {
        private Color _HourColor = Color.Chocolate;
        private Color _MiniteColor = Color.Coral;
        private DateTime _NowTime = DateTime.Now;
        private Color _SecondColor = Color.Green;
        private string _ShowText = "Sweet";
        private string _ShowTextFont = "Courier New";
        private Timer _Time1s = new Timer();
        private IContainer components = null;

        public UserClock()
        {
            this.InitializeComponent();
            this._Time1s.Interval = 50;
            this._Time1s.Tick += new EventHandler(this._Time1s_Tick);
            this.DoubleBuffered = true;
        }

        private Bitmap _BackGround()
        {
            int width = base.Width;
            Bitmap image = new Bitmap(width - 20, width - 20);
            Graphics graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Point point = new Point(image.Width / 2, image.Height / 2);
            int num2 = (image.Width - 1) / 2;
            Rectangle rect = new Rectangle(0, 0, image.Width - 1, image.Width - 1);
            Rectangle rectangle2 = new Rectangle(2, 2, image.Width - 5, image.Width - 5);
            Rectangle rectangle3 = new Rectangle(point.X - 4, point.Y - 4, 8, 8);
            Rectangle rectangle4 = new Rectangle(5, 5, image.Width - 11, image.Width - 11);
            Rectangle rectangle5 = new Rectangle(8, 8, image.Width - 0x11, image.Width - 0x11);
            graphics.FillEllipse(Brushes.DarkGray, rect);
            graphics.FillEllipse(Brushes.White, rectangle2);
            graphics.DrawEllipse(new Pen(Brushes.Black, 1.5f), rectangle3);
            graphics.TranslateTransform((float) point.X, (float) point.Y);
            for (int i = 0; i < 60; i++)
            {
                graphics.RotateTransform(6f);
                graphics.DrawLine(Pens.DarkGray, new Point(num2 - 3, 0), new Point(num2 - 1, 0));
            }
            for (int j = 0; j < 12; j++)
            {
                graphics.RotateTransform(30f);
                graphics.DrawLine(new Pen(Color.Chocolate, 2f), new Point(num2 - 6, 0), new Point(num2 - 1, 0));
            }
            graphics.ResetTransform();
            Font font = new Font("Microsoft YaHei UI", 12f);
            int num3 = num2 / 2;
            int num4 = (int) ((num2 * Math.Sqrt(3.0)) / 2.0);
            graphics.DrawString("1", font, Brushes.Green, new PointF((float) ((num2 + num3) - 13), (float) ((num2 - num4) + 4)));
            graphics.DrawString("2", font, Brushes.Green, new PointF((float) ((num2 + num4) - 0x11), (float) ((num2 - num3) - 2)));
            graphics.DrawString("3", font, Brushes.Green, new PointF((float) ((2 * num2) - 0x12), (float) (num2 - 8)));
            graphics.DrawString("4", font, Brushes.Green, new PointF((float) ((num2 + num4) - 0x12), (float) ((num2 + num3) - 14)));
            graphics.DrawString("5", font, Brushes.Green, new PointF((float) ((num2 + num3) - 14), (float) ((num2 + num4) - 0x13)));
            graphics.DrawString("6", font, Brushes.Green, new PointF((float) (num2 - 6), (float) ((2 * num2) - 0x16)));
            graphics.DrawString("7", font, Brushes.Green, new PointF((float) ((num2 - num3) + 2), (float) ((num2 + num4) - 0x13)));
            graphics.DrawString("8", font, Brushes.Green, new PointF((float) ((num2 - num4) + 5), (float) ((num2 + num3) - 14)));
            graphics.DrawString("9", font, Brushes.Green, new PointF(8f, (float) (num2 - 9)));
            graphics.DrawString("10", font, Brushes.Green, new PointF((float) ((num2 - num4) + 4), (float) ((num2 - num3) - 2)));
            graphics.DrawString("11", font, Brushes.Green, new PointF((float) (num2 - num3), (float) ((num2 - num4) + 3)));
            graphics.DrawString("12", font, Brushes.Green, new PointF((float) (num2 - 10), 7f));
            Bitmap bitmap2 = new Bitmap(base.Width, base.Height);
            Graphics.FromImage(bitmap2).DrawImage(image, new Point(10, 10));
            image.Dispose();
            return bitmap2;
        }

        private void _Time1s_Tick(object sender, EventArgs e)
        {
            this._NowTime = DateTime.Now;
            base.Invalidate();
        }

        private void ClockMy_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = this._BackGround();
            this._Time1s.Start();
        }

        private void ClockMy_SizeChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = this._BackGround();
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
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Transparent;
            this.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Name = "ClockMy";
            base.Size = new Size(130, 150);
            base.Load += new EventHandler(this.ClockMy_Load);
            base.SizeChanged += new EventHandler(this.ClockMy_SizeChanged);
            base.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas())
            {
                base.OnPaint(e);
                int num = (base.Width - 0x15) / 2;
                int hour = this._NowTime.Hour;
                int minute = this._NowTime.Minute;
                float num4 = this._NowTime.Second + (((float) this._NowTime.Millisecond) / 1000f);
                int num5 = ((hour * 30) + 270) + (minute / 2);
                int num6 = (minute * 6) + 270;
                float angle = (num4 * 6f) + 270f;
                Graphics graphics = e.Graphics;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Font font = new Font(this._ShowTextFont, 14f);
                Size size = graphics.MeasureString(this._ShowText, font).ToSize();
                graphics.DrawString(this._ShowText, font, Brushes.Green, new PointF((float) ((num - (size.Width / 2)) + 10), (float) ((num / 2) + 12)));
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                SizeF ef = graphics.MeasureString(this._NowTime.DayOfWeek.ToString(), new Font(this._ShowTextFont, 10f));
                graphics.DrawString(this._NowTime.DayOfWeek.ToString(), new Font(this._ShowTextFont, 10f), Brushes.Chocolate, new PointF((float) ((num - (ef.ToSize().Width / 2)) + 10), (float) (((num * 3) / 2) - 2)));
                graphics.TranslateTransform((float) (base.Width / 2), (float) (base.Width / 2));
                graphics.RotateTransform((float) num5, MatrixOrder.Prepend);
                graphics.DrawLine(new Pen(this._HourColor, 2f), new Point(4, 0), new Point(9, 0));
                Point[] points = new Point[] { new Point(12, 2), new Point(10, 0), new Point(12, -2), new Point(num / 2, -2), new Point((num / 2) + 6, 0), new Point(num / 2, 2) };
                graphics.DrawClosedCurve(new Pen(this._HourColor, 1f), points);
                graphics.RotateTransform((float) -num5);
                graphics.RotateTransform((float) num6, MatrixOrder.Prepend);
                graphics.DrawLine(new Pen(this._MiniteColor, 2f), new Point(4, 0), new Point(9, 0));
                Point[] pointArray2 = new Point[] { new Point(14, 2), new Point(10, 0), new Point(14, -2), new Point(num - 0x11, -2), new Point(num - 10, 0), new Point(num - 0x11, 2) };
                graphics.DrawClosedCurve(new Pen(this._MiniteColor, 1f), pointArray2);
                graphics.RotateTransform((float) -num6);
                graphics.RotateTransform(angle, MatrixOrder.Prepend);
                graphics.DrawLine(new Pen(this._SecondColor, 1f), new Point(-13, 0), new Point(num - 6, 0));
                graphics.ResetTransform();
                string[] textArray1 = new string[] { this._NowTime.Year.ToString(), "-", this._NowTime.Month.ToString(), "-", this._NowTime.Day.ToString() };
                string text = string.Concat(textArray1);
                Size size2 = graphics.MeasureString(text, new Font(this._ShowTextFont, 12f)).ToSize();
                graphics.DrawString(text, new Font(this._ShowTextFont, 12f), Brushes.Green, new PointF((float) ((num - (size2.Width / 2)) + 10), (float) ((num * 2) + 15)));
            }
        }

        [Category("我的属性"), Description("设置边框的宽度")]
        public DateTime 当前时间
        {
            get
            {
                return this._NowTime;
            }
        }

        [Category("我的属性"), Description("设置分钟的指针颜色"), DefaultValue(typeof(Color), "Coral")]
        public Color 分钟指针颜色
        {
            get
            {
                return this._MiniteColor;
            }
            set
            {
                this._MiniteColor = value;
            }
        }

        [Category("我的属性"), Description("设置秒钟的指针颜色"), DefaultValue(typeof(Color), "Green")]
        public Color 秒钟指针颜色
        {
            get
            {
                return this._SecondColor;
            }
            set
            {
                this._SecondColor = value;
            }
        }

        [Category("我的属性"), Description("设置时钟的指针颜色"), DefaultValue(typeof(Color), "Chocolate")]
        public Color 时钟指针颜色
        {
            get
            {
                return this._HourColor;
            }
            set
            {
                this._HourColor = value;
            }
        }

        [Category("我的属性"), Description("设置时钟显示的字符串"), DefaultValue(typeof(string), "Sweet")]
        public string 显示文本
        {
            get
            {
                return this._ShowText;
            }
            set
            {
                this._ShowText = value;
            }
        }

        [Category("我的属性"), Description("设置时钟显示的字符串"), DefaultValue(typeof(string), "Courier New")]
        public string 显示文本字体
        {
            get
            {
                return this._ShowTextFont;
            }
            set
            {
                this._ShowTextFont = value;
            }
        }
    }
}

