namespace HslCommunication.Controls
{
    using HslCommunication;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [DefaultEvent("Click")]
    public class UserSwitch : UserControl
    {
        private Brush brush_switch_background = null;
        private Brush brush_switch_foreground = null;
        private StringFormat centerFormat = null;
        private Color color_switch_background = Color.DimGray;
        private Color color_switch_foreground = Color.FromArgb(0x24, 0x24, 0x24);
        private IContainer components = null;
        private string[] description = new string[] { "Off", "On" };
        private Pen pen_switch_background = null;
        private bool switch_status = false;

        [Category("操作"), Description("点击了按钮开发后触发")]
        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event Action<object, bool> OnSwitchChanged;

        public UserSwitch()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
            this.brush_switch_background = new SolidBrush(this.color_switch_background);
            this.pen_switch_background = new Pen(this.color_switch_background, 2f);
            this.brush_switch_foreground = new SolidBrush(this.color_switch_foreground);
            this.centerFormat = new StringFormat();
            this.centerFormat.Alignment = StringAlignment.Center;
            this.centerFormat.LineAlignment = StringAlignment.Center;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components > null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Point GetCenterPoint()
        {
            if (base.Height > base.Width)
            {
                return new Point(base.Width / 2, base.Width / 2);
            }
            return new Point(base.Height / 2, base.Height / 2);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Margin = new Padding(3, 4, 3, 4);
            base.Name = "UserSwitch";
            base.Size = new Size(0xd6, 0xcf);
            base.Load += new EventHandler(this.UserSwitch_Load);
            base.Click += new EventHandler(this.UserSwitch_Click);
            base.Paint += new PaintEventHandler(this.UserSwitch_Paint);
            base.ResumeLayout(false);
        }

        private void UserSwitch_Click(object sender, EventArgs e)
        {
            this.SwitchStatus = !this.SwitchStatus;
        }

        private void UserSwitch_Load(object sender, EventArgs e)
        {
        }

        private void UserSwitch_Paint(object sender, PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas())
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Point centerPoint = this.GetCenterPoint();
                e.Graphics.TranslateTransform((float) centerPoint.X, (float) centerPoint.Y);
                int num = (0x2d * ((centerPoint.X * 2) - 30)) / 130;
                if (num >= 5)
                {
                    Rectangle rect = new Rectangle(-num - 4, -num - 4, (2 * num) + 8, (2 * num) + 8);
                    Rectangle rectangle2 = new Rectangle(-num, -num, 2 * num, 2 * num);
                    e.Graphics.DrawEllipse(this.pen_switch_background, rect);
                    e.Graphics.FillEllipse(this.brush_switch_background, rectangle2);
                    float angle = -36f;
                    if (this.SwitchStatus)
                    {
                        angle = 36f;
                    }
                    e.Graphics.RotateTransform(angle);
                    int num3 = (20 * ((centerPoint.X * 2) - 30)) / 130;
                    Rectangle rectangle3 = new Rectangle(-centerPoint.X / 8, -num - num3, centerPoint.X / 4, (num * 2) + (num3 * 2));
                    e.Graphics.FillRectangle(this.brush_switch_foreground, rectangle3);
                    Rectangle rectangle4 = new Rectangle(-centerPoint.X / 0x10, -num - 10, centerPoint.X / 8, (centerPoint.X * 3) / 8);
                    e.Graphics.FillEllipse(this.SwitchStatus ? Brushes.LimeGreen : Brushes.Tomato, rectangle4);
                    Rectangle layoutRectangle = new Rectangle(-50, (-num - num3) - 15, 100, 15);
                    e.Graphics.DrawString(this.SwitchStatus ? this.description[1] : this.description[0], this.Font, this.SwitchStatus ? Brushes.LimeGreen : Brushes.Tomato, layoutRectangle, this.centerFormat);
                    e.Graphics.ResetTransform();
                }
            }
        }

        [Browsable(true), Description("获取或设置开关按钮的背景色"), Category("外观"), DefaultValue(typeof(Color), "DimGray")]
        public Color SwitchBackground
        {
            get
            {
                return this.color_switch_background;
            }
            set
            {
                this.color_switch_background = value;
                if (this.brush_switch_background != null)
                {
                    this.brush_switch_background.Dispose();
                }
                else
                {
                    Brush expressionStack_11_0 = this.brush_switch_background;
                }
                if (this.pen_switch_background != null)
                {
                    this.pen_switch_background.Dispose();
                }
                else
                {
                    Pen expressionStack_23_0 = this.pen_switch_background;
                }
                this.brush_switch_background = new SolidBrush(this.color_switch_background);
                this.pen_switch_background = new Pen(this.color_switch_background, 2f);
                base.Invalidate();
            }
        }

        [Browsable(true), Description("获取或设置开关按钮的前景色"), Category("外观"), DefaultValue(typeof(Color), "[36, 36, 36]")]
        public Color SwitchForeground
        {
            get
            {
                return this.color_switch_foreground;
            }
            set
            {
                this.color_switch_foreground = value;
                this.brush_switch_foreground = new SolidBrush(this.color_switch_foreground);
                base.Invalidate();
            }
        }

        [Browsable(true), Description("获取或设置开关按钮的开合状态"), Category("外观"), DefaultValue(false)]
        public bool SwitchStatus
        {
            get
            {
                return this.switch_status;
            }
            set
            {
                if (value != this.switch_status)
                {
                    this.switch_status = value;
                    base.Invalidate();
                    if (this.OnSwitchChanged != null)
                    {
                        Action<object, bool> onSwitchChanged = this.OnSwitchChanged;
                        onSwitchChanged(this, this.switch_status);
                    }
                    else
                    {
                        Action<object, bool> expressionStack_29_0 = this.OnSwitchChanged;
                    }
                }
            }
        }

        [Browsable(false)]
        public string[] SwitchStatusDescription
        {
            get
            {
                return this.description;
            }
            set
            {
                if ((value != null) ? (value.Length == 2) : false)
                {
                    this.description = value;
                }
            }
        }
    }
}

