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
    using System.Windows.Forms;

    [DefaultEvent("Click")]
    public class UserButton : UserControl
    {
        private string _text = "button";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <CustomerInformation>k__BackingField = "";
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <is_left_mouse_down>k__BackingField = false;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <is_mouse_on>k__BackingField = false;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color <TextColor>k__BackingField = Color.Black;
        private IContainer components = null;
        private Color m_active = Color.AliceBlue;
        private Color m_backcor = Color.Lavender;
        private bool m_BorderVisiable = true;
        private Color m_enablecolor = Color.FromArgb(190, 190, 190);
        private bool m_Selected = false;
        private int RoundCorner = 3;
        private StringFormat sf = null;

        public UserButton()
        {
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
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Transparent;
            base.Name = "UserButton";
            base.Size = new Size(0x4e, 0x19);
            base.Load += new EventHandler(this.UserButton_Load);
            base.KeyDown += new KeyEventHandler(this.UserButton_KeyDown);
            base.ResumeLayout(false);
        }

        protected override void OnClick(EventArgs e)
        {
            if (base.Enabled)
            {
                base.OnClick(e);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (base.Enabled)
            {
                base.OnMouseClick(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas())
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                GraphicsPath path = new GraphicsPath();
                path.AddLine(this.RoundCorner, 0, (base.Width - this.RoundCorner) - 1, 0);
                path.AddArc((base.Width - (this.RoundCorner * 2)) - 1, 0, this.RoundCorner * 2, this.RoundCorner * 2, 270f, 90f);
                path.AddLine(base.Width - 1, this.RoundCorner, base.Width - 1, (base.Height - this.RoundCorner) - 1);
                path.AddArc((int) ((base.Width - (this.RoundCorner * 2)) - 1), (int) ((base.Height - (this.RoundCorner * 2)) - 1), (int) (this.RoundCorner * 2), (int) (this.RoundCorner * 2), 0f, 90f);
                path.AddLine((base.Width - this.RoundCorner) - 1, base.Height - 1, this.RoundCorner, base.Height - 1);
                path.AddArc(0, (base.Height - (this.RoundCorner * 2)) - 1, this.RoundCorner * 2, this.RoundCorner * 2, 90f, 90f);
                path.AddLine(0, (base.Height - this.RoundCorner) - 1, 0, this.RoundCorner);
                path.AddArc(0, 0, this.RoundCorner * 2, this.RoundCorner * 2, 180f, 90f);
                Brush brush = null;
                Brush brush2 = null;
                Rectangle layoutRectangle = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, base.ClientRectangle.Width, base.ClientRectangle.Height);
                if (base.Enabled)
                {
                    brush = new SolidBrush(this.TextColor);
                    if (this.Selected)
                    {
                        brush2 = new SolidBrush(Color.DodgerBlue);
                    }
                    else if (this.is_mouse_on)
                    {
                        brush2 = new SolidBrush(this.ActiveColor);
                    }
                    else
                    {
                        brush2 = new SolidBrush(this.OriginalColor);
                    }
                    if (this.is_left_mouse_down)
                    {
                        layoutRectangle.Offset(1, 1);
                    }
                }
                else
                {
                    brush = new SolidBrush(Color.Gray);
                    brush2 = new SolidBrush(this.EnableColor);
                }
                e.Graphics.FillPath(brush2, path);
                Pen pen = new Pen(Color.FromArgb(170, 170, 170));
                if (this.BorderVisiable)
                {
                    e.Graphics.DrawPath(pen, path);
                }
                e.Graphics.DrawString(this.UIText, this.Font, brush, layoutRectangle, this.sf);
                brush.Dispose();
                brush2.Dispose();
                pen.Dispose();
                path.Dispose();
            }
        }

        public void PerformClick()
        {
            this.OnClick(new EventArgs());
        }

        private void UserButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnClick(new EventArgs());
            }
        }

        private void UserButton_Load(object sender, EventArgs e)
        {
            this.sf = new StringFormat();
            this.sf.Alignment = StringAlignment.Center;
            this.sf.LineAlignment = StringAlignment.Center;
            base.SizeChanged += new EventHandler(this.UserButton_SizeChanged);
            this.Font = new Font("微软雅黑", this.Font.Size, this.Font.Style);
            base.MouseEnter += new EventHandler(this.UserButton_MouseEnter);
            base.MouseLeave += new EventHandler(this.UserButton_MouseLeave);
            base.MouseDown += new MouseEventHandler(this.UserButton_MouseDown);
            base.MouseUp += new MouseEventHandler(this.UserButton_MouseUp);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void UserButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.is_left_mouse_down = true;
                base.Invalidate();
            }
        }

        private void UserButton_MouseEnter(object sender, EventArgs e)
        {
            this.is_mouse_on = true;
            base.Invalidate();
        }

        private void UserButton_MouseLeave(object sender, EventArgs e)
        {
            this.is_mouse_on = false;
            base.Invalidate();
        }

        private void UserButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.is_left_mouse_down = false;
                base.Invalidate();
            }
        }

        private void UserButton_SizeChanged(object sender, EventArgs e)
        {
            if (base.Width > 1)
            {
                base.Invalidate();
            }
        }

        [Category("外观"), DefaultValue(typeof(Color), "AliceBlue"), Description("按钮的活动色")]
        public Color ActiveColor
        {
            get
            {
                return this.m_active;
            }
            set
            {
                this.m_active = value;
                base.Invalidate();
            }
        }

        [Category("外观"), Browsable(true), DefaultValue(true), Description("指示按钮是否存在边框")]
        public bool BorderVisiable
        {
            get
            {
                return this.m_BorderVisiable;
            }
            set
            {
                this.m_BorderVisiable = value;
                base.Invalidate();
            }
        }

        [Category("外观"), DefaultValue(3), Description("按钮框的圆角属性")]
        public int CornerRadius
        {
            get
            {
                return this.RoundCorner;
            }
            set
            {
                this.RoundCorner = value;
                base.Invalidate();
            }
        }

        [Browsable(false)]
        public string CustomerInformation { get; set; }

        [Category("外观"), Description("按钮的活动色")]
        public Color EnableColor
        {
            get
            {
                return this.m_enablecolor;
            }
            set
            {
                this.m_enablecolor = value;
                base.Invalidate();
            }
        }

        [Browsable(false)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        private bool is_left_mouse_down { get; set; }

        private bool is_mouse_on { get; set; }

        [Category("外观"), DefaultValue(typeof(Color), "Lavender"), Description("按钮的背景色")]
        public Color OriginalColor
        {
            get
            {
                return this.m_backcor;
            }
            set
            {
                this.m_backcor = value;
                base.Invalidate();
            }
        }

        [Category("外观"), DefaultValue(false), Description("指示按钮的选中状态")]
        public bool Selected
        {
            get
            {
                return this.m_Selected;
            }
            set
            {
                this.m_Selected = value;
                base.Invalidate();
            }
        }

        [Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [Category("外观"), DefaultValue(typeof(Color), "Black"), Description("用来设置显示的文本的颜色")]
        public Color TextColor { get; set; }

        [Category("外观"), DefaultValue("button"), Description("用来设置显示的文本信息")]
        public string UIText
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
                base.Invalidate();
            }
        }
    }
}

