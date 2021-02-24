namespace HslCommunication.Controls
{
    using HslCommunication;
    using HslCommunication.Core;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class UserGaugeChart : UserControl
    {
        private bool alarm_check = false;
        private Brush brush_gauge_pointer = null;
        private StringFormat centerFormat = null;
        private Color color_gauge_border = Color.DimGray;
        private Color color_gauge_pointer = Color.Tomato;
        private IContainer components = null;
        private SimpleHybirdLock hybirdLock;
        private bool isBigSemiCircle = false;
        private Action m_UpdateAction;
        private int m_version = 0;
        private Pen pen_gauge_alarm = null;
        private Pen pen_gauge_border = null;
        private int segment_count = 10;
        private bool text_under_pointer = true;
        private System.Windows.Forms.Timer timer_alarm_check;
        private double value_alarm_max = 80.0;
        private double value_alarm_min = 20.0;
        private double value_current = 0.0;
        private double value_max = 100.0;
        private double value_paint = 0.0;
        private double value_start = 0.0;
        private string value_unit_text = string.Empty;

        public UserGaugeChart()
        {
            this.InitializeComponent();
            this.pen_gauge_border = new Pen(this.color_gauge_border);
            this.brush_gauge_pointer = new SolidBrush(this.color_gauge_pointer);
            this.centerFormat = new StringFormat();
            this.centerFormat.Alignment = StringAlignment.Center;
            this.centerFormat.LineAlignment = StringAlignment.Center;
            this.pen_gauge_alarm = new Pen(Color.OrangeRed, 3f);
            this.pen_gauge_alarm.DashStyle = DashStyle.Custom;
            this.pen_gauge_alarm.DashPattern = new float[] { 5f, 1f };
            this.hybirdLock = new SimpleHybirdLock();
            this.m_UpdateAction = new Action(this.Invalidate);
            this.timer_alarm_check = new System.Windows.Forms.Timer();
            this.timer_alarm_check.Tick += new EventHandler(this.Timer_alarm_check_Tick);
            this.timer_alarm_check.Interval = 0x3e8;
            this.DoubleBuffered = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components > null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private OperateResult<Point, int, double> GetCenterPoint()
        {
            OperateResult<Point, int, double> result = new OperateResult<Point, int, double>();
            if (base.Height > 0x23)
            {
                if (base.Width <= 20)
                {
                    return result;
                }
                result.IsSuccess = true;
                if (!this.IsBigSemiCircle)
                {
                    result.Content2 = base.Height - 30;
                    if ((((double) (base.Width - 40)) / 2.0) > ((double) result.Content2))
                    {
                        result.Content3 = (Math.Acos(1.0) * 180.0) / 3.1415926535897931;
                    }
                    else
                    {
                        result.Content3 = (Math.Acos((((double) (base.Width - 40)) / 2.0) / ((double) (base.Height - 30))) * 180.0) / 3.1415926535897931;
                    }
                    result.Content1 = new Point(base.Width / 2, base.Height - 10);
                    return result;
                }
                result.Content2 = (base.Width - 40) / 2;
                if ((base.Height - 30) < result.Content2)
                {
                    result.Content2 = base.Height - 30;
                    result.Content3 = (Math.Acos(1.0) * 180.0) / 3.1415926535897931;
                    result.Content1 = new Point(base.Width / 2, base.Height - 10);
                    return result;
                }
                int num = (base.Height - 30) - result.Content2;
                if (num > result.Content2)
                {
                    num = result.Content2;
                }
                result.Content3 = (-Math.Asin((num * 1.0) / ((double) result.Content2)) * 180.0) / 3.1415926535897931;
                result.Content1 = new Point(base.Width / 2, result.Content2 + 20);
            }
            return result;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Transparent;
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Margin = new Padding(3, 4, 3, 4);
            base.Name = "UserGaugeChart";
            base.Size = new Size(0x101, 0xe2);
            base.Load += new EventHandler(this.UserGaugeChart_Load);
            base.Paint += new PaintEventHandler(this.UserGaugeChart_Paint);
            base.ResumeLayout(false);
        }

        private void ThreadPoolUpdateProgress(object obj)
        {
            try
            {
                int num = (int) obj;
                if (this.value_paint != this.value_current)
                {
                    double num2 = Math.Abs((double) (this.value_paint - this.value_current)) / 10.0;
                    if (num2 == 0.0)
                    {
                        num2 = 1.0;
                    }
                    while (!(this.value_paint == this.value_current))
                    {
                        Thread.Sleep(0x11);
                        if (num != this.m_version)
                        {
                            return;
                        }
                        this.hybirdLock.Enter();
                        double num3 = 0.0;
                        if (this.value_paint > this.value_current)
                        {
                            double num4 = this.value_paint - this.value_current;
                            if (num4 > num2)
                            {
                                num4 = num2;
                            }
                            num3 = this.value_paint - num4;
                        }
                        else
                        {
                            double num5 = this.value_current - this.value_paint;
                            if (num5 > num2)
                            {
                                num5 = num2;
                            }
                            num3 = this.value_paint + num5;
                        }
                        this.value_paint = num3;
                        this.hybirdLock.Leave();
                        if (num != this.m_version)
                        {
                            return;
                        }
                        if (base.IsHandleCreated)
                        {
                            base.Invoke(this.m_UpdateAction);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Timer_alarm_check_Tick(object sender, EventArgs e)
        {
            if ((this.value_current > this.value_alarm_max) || (this.value_current < this.value_alarm_min))
            {
                this.alarm_check = !this.alarm_check;
            }
            else
            {
                this.alarm_check = false;
            }
            base.Invalidate();
        }

        private void UserGaugeChart_Load(object sender, EventArgs e)
        {
            this.timer_alarm_check.Start();
        }

        private void UserGaugeChart_Paint(object sender, PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas())
            {
                Graphics graphics = e.Graphics;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                OperateResult<Point, int, double> centerPoint = this.GetCenterPoint();
                if (centerPoint.IsSuccess)
                {
                    Point point = centerPoint.Content1;
                    int num = centerPoint.Content2;
                    float num2 = Convert.ToSingle(centerPoint.Content3);
                    Rectangle rect = new Rectangle(-num, -num, 2 * num, 2 * num);
                    Rectangle rectangle2 = new Rectangle(-num - 5, -num - 5, (2 * num) + 10, (2 * num) + 10);
                    Rectangle rectangle3 = new Rectangle(-num / 3, -num / 3, (2 * num) / 3, (2 * num) / 3);
                    graphics.TranslateTransform((float) point.X, (float) point.Y);
                    graphics.DrawArc(this.pen_gauge_border, rectangle3, -num2, (num2 * 2f) - 180f);
                    graphics.DrawArc(this.pen_gauge_border, rect, num2 - 180f, 180f - (num2 * 2f));
                    graphics.DrawLine(this.pen_gauge_border, (int) (-(num / 3) * Math.Cos((num2 / 180f) * 3.1415926535897931)), -((int) ((num / 3) * Math.Sin((num2 / 180f) * 3.1415926535897931))), -((int) ((num - 30) * Math.Cos((num2 / 180f) * 3.1415926535897931))), -((int) ((num - 30) * Math.Sin((num2 / 180f) * 3.1415926535897931))));
                    graphics.DrawLine(this.pen_gauge_border, (int) ((num - 30) * Math.Cos((num2 / 180f) * 3.1415926535897931)), -((int) ((num - 30) * Math.Sin((num2 / 180f) * 3.1415926535897931))), (int) ((num / 3) * Math.Cos((num2 / 180f) * 3.1415926535897931)), -((int) ((num / 3) * Math.Sin((num2 / 180f) * 3.1415926535897931))));
                    graphics.RotateTransform(num2 - 90f);
                    int num3 = this.segment_count;
                    for (int i = 0; i <= num3; i++)
                    {
                        Rectangle rectangle6 = new Rectangle(-2, -num, 3, 7);
                        graphics.FillRectangle(Brushes.DimGray, rectangle6);
                        rectangle6 = new Rectangle(-50, -num + 7, 100, 20);
                        graphics.DrawString((this.ValueStart + (((this.ValueMax - this.ValueStart) * i) / ((double) num3))).ToString(), this.Font, Brushes.DodgerBlue, rectangle6, this.centerFormat);
                        graphics.RotateTransform(((180f - (2f * num2)) / ((float) num3)) / 2f);
                        if (i != num3)
                        {
                            graphics.DrawLine(Pens.DimGray, 0, -num, 0, -num + 3);
                        }
                        graphics.RotateTransform(((180f - (2f * num2)) / ((float) num3)) / 2f);
                    }
                    graphics.RotateTransform(-(180f - (2f * num2)) / ((float) num3));
                    graphics.RotateTransform(num2 - 90f);
                    Rectangle rectangle4 = new Rectangle(-36, -(((num * 2) / 3) - 3), 0x48, this.Font.Height + 3);
                    if (((this.value_current > this.value_alarm_max) || (this.value_current < this.value_alarm_min)) && this.alarm_check)
                    {
                        graphics.FillRectangle(Brushes.OrangeRed, rectangle4);
                    }
                    if (this.IsTextUnderPointer)
                    {
                        graphics.DrawString(this.Value.ToString(), this.Font, Brushes.DimGray, rectangle4, this.centerFormat);
                        rectangle4.Offset(0, this.Font.Height);
                        if (!string.IsNullOrEmpty(this.UnitText))
                        {
                            graphics.DrawString(this.UnitText, this.Font, Brushes.Gray, rectangle4, this.centerFormat);
                        }
                    }
                    graphics.RotateTransform(num2 - 90f);
                    graphics.RotateTransform((float) (((this.value_paint - this.ValueStart) / (this.ValueMax - this.ValueStart)) * (180f - (2f * num2))));
                    Rectangle rectangle5 = new Rectangle(-5, -5, 10, 10);
                    graphics.FillEllipse(this.brush_gauge_pointer, rectangle5);
                    Point[] points = new Point[] { new Point(5, 0), new Point(2, -num + 40), new Point(0, -num + 20), new Point(-2, -num + 40), new Point(-5, 0) };
                    graphics.FillPolygon(this.brush_gauge_pointer, points);
                    graphics.RotateTransform((float) ((-(this.value_paint - this.ValueStart) / (this.ValueMax - this.ValueStart)) * (180f - (2f * num2))));
                    graphics.RotateTransform(90f - num2);
                    if ((this.value_alarm_min > this.ValueStart) && (this.value_alarm_min <= this.ValueMax))
                    {
                        graphics.DrawArc(this.pen_gauge_alarm, rectangle2, num2 - 180f, (float) (((this.ValueAlarmMin - this.ValueStart) / (this.ValueMax - this.ValueStart)) * (180f - (2f * num2))));
                    }
                    if ((this.value_alarm_max >= this.ValueStart) && (this.value_alarm_max < this.ValueMax))
                    {
                        float num7 = (float) (((this.value_alarm_max - this.ValueStart) / (this.ValueMax - this.ValueStart)) * (180f - (2f * num2)));
                        graphics.DrawArc(this.pen_gauge_alarm, rectangle2, (-180f + num2) + num7, (180f - (2f * num2)) - num7);
                    }
                    if (!this.IsTextUnderPointer)
                    {
                        graphics.DrawString(this.Value.ToString(), this.Font, Brushes.DimGray, rectangle4, this.centerFormat);
                        rectangle4.Offset(0, this.Font.Height);
                        if (!string.IsNullOrEmpty(this.UnitText))
                        {
                            graphics.DrawString(this.UnitText, this.Font, Brushes.Gray, rectangle4, this.centerFormat);
                        }
                    }
                    graphics.ResetTransform();
                }
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置仪表盘的背景色"), DefaultValue(typeof(Color), "DimGray")]
        public Color GaugeBorder
        {
            get
            {
                return this.color_gauge_border;
            }
            set
            {
                if (this.pen_gauge_border != null)
                {
                    this.pen_gauge_border.Dispose();
                }
                else
                {
                    Pen expressionStack_A_0 = this.pen_gauge_border;
                }
                this.pen_gauge_border = new Pen(value);
                this.color_gauge_border = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), Description("通常情况，仪表盘不会大于半个圆，除非本属性设置为 True"), DefaultValue(false)]
        public bool IsBigSemiCircle
        {
            get
            {
                return this.isBigSemiCircle;
            }
            set
            {
                this.isBigSemiCircle = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置文本是否是指针的下面"), DefaultValue(true)]
        public bool IsTextUnderPointer
        {
            get
            {
                return this.text_under_pointer;
            }
            set
            {
                this.text_under_pointer = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置仪表盘指针的颜色"), DefaultValue(typeof(Color), "Tomato")]
        public Color PointerColor
        {
            get
            {
                return this.color_gauge_pointer;
            }
            set
            {
                if (this.brush_gauge_pointer != null)
                {
                    this.brush_gauge_pointer.Dispose();
                }
                else
                {
                    Brush expressionStack_A_0 = this.brush_gauge_pointer;
                }
                this.brush_gauge_pointer = new SolidBrush(value);
                this.color_gauge_pointer = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置仪表盘的分割段数，最小为2，最大1000"), DefaultValue(10)]
        public int SegmentCount
        {
            get
            {
                return this.segment_count;
            }
            set
            {
                if ((value > 1) && (value < 0x3e8))
                {
                    this.segment_count = value;
                }
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置仪表盘的单位描述文本"), DefaultValue("")]
        public string UnitText
        {
            get
            {
                return this.value_unit_text;
            }
            set
            {
                this.value_unit_text = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置数值的当前值，默认为0"), DefaultValue((double) 0.0)]
        public double Value
        {
            get
            {
                return this.value_current;
            }
            set
            {
                if (((this.ValueStart <= value) && (value <= this.ValueMax)) && !(value == this.value_current))
                {
                    this.value_current = value;
                    int state = Interlocked.Increment(ref this.m_version);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadPoolUpdateProgress), state);
                }
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置数值的上限报警值，设置为超过最大值则无上限报警，默认为80"), DefaultValue((double) 80.0)]
        public double ValueAlarmMax
        {
            get
            {
                return this.value_alarm_max;
            }
            set
            {
                if (this.ValueStart <= value)
                {
                    this.value_alarm_max = value;
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置数值的下限报警值，设置为小于最小值则无下限报警，默认为20"), DefaultValue((double) 20.0)]
        public double ValueAlarmMin
        {
            get
            {
                return this.value_alarm_min;
            }
            set
            {
                if (value <= this.ValueMax)
                {
                    this.value_alarm_min = value;
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置数值的最大值，默认为100"), DefaultValue((double) 100.0)]
        public double ValueMax
        {
            get
            {
                if (this.value_max <= this.value_start)
                {
                    return (this.value_start + 1.0);
                }
                return this.value_max;
            }
            set
            {
                this.value_max = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置数值的起始值，默认为0"), DefaultValue((double) 0.0)]
        public double ValueStart
        {
            get
            {
                if (this.value_max <= this.value_start)
                {
                    return (this.value_start + 1.0);
                }
                return this.value_start;
            }
            set
            {
                this.value_start = value;
                base.Invalidate();
            }
        }
    }
}

