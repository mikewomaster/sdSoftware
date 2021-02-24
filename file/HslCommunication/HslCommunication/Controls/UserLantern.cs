namespace HslCommunication.Controls
{
    using HslCommunication;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Windows.Forms;

    public class UserLantern : UserControl
    {
        private Brush brush_lantern_background = null;
        private Color color_lantern_background = Color.LimeGreen;
        private IContainer components = null;
        private Pen pen_lantern_background = null;

        public UserLantern()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
            this.brush_lantern_background = new SolidBrush(this.color_lantern_background);
            this.pen_lantern_background = new Pen(this.color_lantern_background, 2f);
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
                return new Point((base.Width - 1) / 2, (base.Width - 1) / 2);
            }
            return new Point((base.Height - 1) / 2, (base.Height - 1) / 2);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Transparent;
            base.Name = "UserLantern";
            base.Load += new EventHandler(this.UserLantern_Load);
            base.Paint += new PaintEventHandler(this.UserLantern_Paint);
            base.ResumeLayout(false);
        }

        private void UserLantern_Load(object sender, EventArgs e)
        {
        }

        private void UserLantern_Paint(object sender, PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas())
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Point centerPoint = this.GetCenterPoint();
                e.Graphics.TranslateTransform((float) centerPoint.X, (float) centerPoint.Y);
                int num = centerPoint.X - 5;
                if (num >= 5)
                {
                    Rectangle rect = new Rectangle(-num - 4, -num - 4, (2 * num) + 8, (2 * num) + 8);
                    Rectangle rectangle2 = new Rectangle(-num, -num, 2 * num, 2 * num);
                    e.Graphics.DrawEllipse(this.pen_lantern_background, rect);
                    e.Graphics.FillEllipse(this.brush_lantern_background, rectangle2);
                }
            }
        }

        [Browsable(true), Description("获取或设置信号灯的背景色"), Category("外观"), DefaultValue(typeof(Color), "LimeGreen")]
        public Color LanternBackground
        {
            get
            {
                return this.color_lantern_background;
            }
            set
            {
                this.color_lantern_background = value;
                if (this.brush_lantern_background != null)
                {
                    this.brush_lantern_background.Dispose();
                }
                else
                {
                    Brush expressionStack_11_0 = this.brush_lantern_background;
                }
                if (this.pen_lantern_background != null)
                {
                    this.pen_lantern_background.Dispose();
                }
                else
                {
                    Pen expressionStack_23_0 = this.pen_lantern_background;
                }
                this.brush_lantern_background = new SolidBrush(this.color_lantern_background);
                this.pen_lantern_background = new Pen(this.color_lantern_background, 2f);
                base.Invalidate();
            }
        }
    }
}

