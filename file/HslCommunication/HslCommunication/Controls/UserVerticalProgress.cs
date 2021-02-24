namespace HslCommunication.Controls
{
    using HslCommunication;
    using HslCommunication.Core;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class UserVerticalProgress : UserControl
    {
        private IContainer components = null;
        private SimpleHybirdLock hybirdLock;
        private int m_actual = 0;
        private Brush m_backBrush;
        private Color m_borderColor;
        private Pen m_borderPen;
        private Brush m_foreBrush;
        private StringFormat m_formatCenter;
        private bool m_isTextRender = true;
        private int m_Max = 100;
        private Color m_progressColor;
        private HslCommunication.Controls.ProgressStyle m_progressStyle = HslCommunication.Controls.ProgressStyle.Vertical;
        private int m_speed = 1;
        private Action m_UpdateAction;
        private bool m_UseAnimation = false;
        private int m_value = 0;
        private int m_version = 0;

        public UserVerticalProgress()
        {
            this.InitializeComponent();
            this.m_borderPen = new Pen(Color.DimGray);
            this.m_backBrush = new SolidBrush(Color.Transparent);
            this.m_foreBrush = new SolidBrush(Color.Tomato);
            this.m_progressColor = Color.Tomato;
            this.m_borderColor = Color.DimGray;
            this.m_formatCenter = new StringFormat();
            this.m_formatCenter.Alignment = StringAlignment.Center;
            this.m_formatCenter.LineAlignment = StringAlignment.Center;
            this.m_UpdateAction = new Action(this.UpdateRender);
            this.hybirdLock = new SimpleHybirdLock();
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

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Margin = new Padding(3, 4, 3, 4);
            base.Name = "UserVerticalProgress";
            base.Size = new Size(0x2c, 0xc0);
            base.Load += new EventHandler(this.UserVerticalProgress_Load);
            base.SizeChanged += new EventHandler(this.UserVerticalProgress_SizeChanged);
            base.Paint += new PaintEventHandler(this.UserVerticalProgress_Paint);
            base.ResumeLayout(false);
        }

        private void ThreadPoolUpdateProgress(object obj)
        {
            try
            {
                int num = (int) obj;
                if (this.m_speed < 1)
                {
                    this.m_speed = 1;
                }
                while (this.m_actual != this.m_value)
                {
                    Thread.Sleep(0x11);
                    if (num != this.m_version)
                    {
                        return;
                    }
                    this.hybirdLock.Enter();
                    int num2 = 0;
                    if (this.m_actual > this.m_value)
                    {
                        int speed = this.m_actual - this.m_value;
                        if (speed > this.m_speed)
                        {
                            speed = this.m_speed;
                        }
                        num2 = this.m_actual - speed;
                    }
                    else
                    {
                        int num4 = this.m_value - this.m_actual;
                        if (num4 > this.m_speed)
                        {
                            num4 = this.m_speed;
                        }
                        num2 = this.m_actual + num4;
                    }
                    this.m_actual = num2;
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
            catch (Exception)
            {
            }
        }

        private void UpdateRender()
        {
            base.Invalidate();
        }

        private void UserVerticalProgress_Load(object sender, EventArgs e)
        {
        }

        private void UserVerticalProgress_Paint(object sender, PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas())
            {
                try
                {
                    Graphics graphics = e.Graphics;
                    Rectangle rect = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
                    graphics.FillRectangle(this.m_backBrush, rect);
                    graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    switch (this.m_progressStyle)
                    {
                        case HslCommunication.Controls.ProgressStyle.Vertical:
                        {
                            int height = (int) ((this.m_actual * (base.Height - 2)) / ((long) this.m_Max));
                            rect = new Rectangle(0, (base.Height - 1) - height, base.Width - 1, height);
                            graphics.FillRectangle(this.m_foreBrush, rect);
                            break;
                        }
                        case HslCommunication.Controls.ProgressStyle.Horizontal:
                        {
                            int num2 = (int) ((this.m_actual * (base.Width - 2)) / ((long) this.m_Max));
                            rect = new Rectangle(0, 0, num2 + 1, base.Height - 1);
                            graphics.FillRectangle(this.m_foreBrush, rect);
                            break;
                        }
                    }
                    rect = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
                    if (this.m_isTextRender)
                    {
                        string s = (((this.m_actual * 100L) / ((long) this.m_Max))).ToString() + "%";
                        using (Brush brush = new SolidBrush(this.ForeColor))
                        {
                            graphics.DrawString(s, this.Font, brush, rect, this.m_formatCenter);
                        }
                    }
                    graphics.DrawRectangle(this.m_borderPen, rect);
                }
                catch (Exception)
                {
                }
            }
        }

        private void UserVerticalProgress_SizeChanged(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        [Description("获取或设置进度条的背景色"), Category("外观"), Browsable(true)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                if (this.m_backBrush != null)
                {
                    this.m_backBrush.Dispose();
                }
                else
                {
                    Brush backBrush = this.m_backBrush;
                }
                this.m_backBrush = new SolidBrush(value);
                base.Invalidate();
            }
        }

        [Description("获取或设置进度条的边框颜色"), Category("外观"), Browsable(true), DefaultValue(typeof(Color), "DimGray")]
        public Color BorderColor
        {
            get
            {
                return this.m_borderColor;
            }
            set
            {
                this.m_borderColor = value;
                if (this.m_borderPen != null)
                {
                    this.m_borderPen.Dispose();
                }
                else
                {
                    Pen borderPen = this.m_borderPen;
                }
                this.m_borderPen = new Pen(value);
                base.Invalidate();
            }
        }

        public override System.Windows.Forms.Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }

        [Description("获取或设置是否显示进度文本"), Category("外观"), Browsable(true), DefaultValue(true)]
        public bool IsTextRender
        {
            get
            {
                return this.m_isTextRender;
            }
            set
            {
                this.m_isTextRender = value;
                base.Invalidate();
            }
        }

        [Description("获取或设置进度条的最大值，默认为100"), Category("外观"), Browsable(true), DefaultValue(100)]
        public int Max
        {
            get
            {
                return this.m_Max;
            }
            set
            {
                if (value > 1)
                {
                    this.m_Max = value;
                }
                if (this.m_value > this.m_Max)
                {
                    this.m_value = this.m_Max;
                }
                base.Invalidate();
            }
        }

        [Description("获取或设置进度条的前景色"), Category("外观"), Browsable(true), DefaultValue(typeof(Color), "Tomato")]
        public Color ProgressColor
        {
            get
            {
                return this.m_progressColor;
            }
            set
            {
                this.m_progressColor = value;
                if (this.m_foreBrush != null)
                {
                    this.m_foreBrush.Dispose();
                }
                else
                {
                    Brush foreBrush = this.m_foreBrush;
                }
                this.m_foreBrush = new SolidBrush(value);
                base.Invalidate();
            }
        }

        [Description("获取或设置进度条的样式"), Category("外观"), Browsable(true), DefaultValue(typeof(HslCommunication.Controls.ProgressStyle), "Vertical")]
        public HslCommunication.Controls.ProgressStyle ProgressStyle
        {
            get
            {
                return this.m_progressStyle;
            }
            set
            {
                this.m_progressStyle = value;
                base.Invalidate();
            }
        }

        [Description("获取或设置进度条变化的时候是否采用动画效果"), Category("外观"), Browsable(true), DefaultValue(false)]
        public bool UseAnimation
        {
            get
            {
                return this.m_UseAnimation;
            }
            set
            {
                this.m_UseAnimation = value;
            }
        }

        [Description("获取或设置当前进度条的值"), Category("外观"), Browsable(true), DefaultValue(0)]
        public int Value
        {
            get
            {
                return this.m_value;
            }
            set
            {
                if (((value >= 0) && (value <= this.m_Max)) && (value != this.m_value))
                {
                    this.m_value = value;
                    if (this.UseAnimation)
                    {
                        int state = Interlocked.Increment(ref this.m_version);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadPoolUpdateProgress), state);
                    }
                    else
                    {
                        this.m_actual = value;
                        base.Invalidate();
                    }
                }
            }
        }

        [Description("获取或设置进度条的变化进度"), Category("外观"), Browsable(true), DefaultValue(1)]
        public int ValueChangeSpeed
        {
            get
            {
                return this.m_speed;
            }
            set
            {
                if (value >= 1)
                {
                    this.m_speed = value;
                }
            }
        }
    }
}

