namespace HslCommunication.Controls
{
    using HslCommunication;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Windows.Forms;

    public class UserBottle : UserControl
    {
        private string bottleTag = "";
        private IContainer components = null;
        private string headTag = "原料1";
        private bool isOpen = false;
        private StringFormat stringFormat = new StringFormat();
        private double value = 50.0;

        public UserBottle()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
            this.stringFormat.Alignment = StringAlignment.Center;
            this.stringFormat.LineAlignment = StringAlignment.Center;
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
            base.Name = "UserBottle";
            base.Size = new Size(0x42, 0xac);
            base.Paint += new PaintEventHandler(this.UserBottle_Paint);
            base.ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas())
            {
                Graphics graphics = e.Graphics;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                if ((base.Width >= 15) && (base.Height >= 15))
                {
                    int x = base.Width / 2;
                    int y = (base.Height - base.Width) - ((((base.Height - base.Width) - 20) * Convert.ToInt32(this.value)) / 100);
                    GraphicsPath path = new GraphicsPath();
                    Point[] points = new Point[] { new Point(0, 20), new Point(0, base.Height - base.Width), new Point(x + 1, base.Height - 8), new Point(x + 1, 20), new Point(0, 20) };
                    path.AddPolygon(points);
                    Brush brush = new LinearGradientBrush(new Point(0, 20), new Point(x + 1, 20), Color.FromArgb(0x8e, 0xc4, 0xd8), Color.FromArgb(240, 240, 240));
                    graphics.FillPath(brush, path);
                    path.Reset();
                    Point[] pointArray2 = new Point[] { new Point(x, 20), new Point(x, base.Height - 8), new Point(base.Width - 1, base.Height - base.Width), new Point(base.Width - 1, 20), new Point(x, 20) };
                    path.AddPolygon(pointArray2);
                    brush.Dispose();
                    brush = new LinearGradientBrush(new Point(x - 1, 20), new Point(base.Width - 1, 20), Color.FromArgb(240, 240, 240), Color.FromArgb(0x8e, 0xc4, 0xd8));
                    graphics.FillPath(brush, path);
                    brush.Dispose();
                    brush = new SolidBrush(Color.FromArgb(0x97, 0xe8, 0xf4));
                    graphics.FillEllipse(brush, 1, 0x11, base.Width - 3, 6);
                    brush.Dispose();
                    path.Reset();
                    Point[] pointArray3 = new Point[] { new Point(0, y), new Point(0, base.Height - base.Width), new Point(x + 1, base.Height - 8), new Point(x + 1, y), new Point(0, y) };
                    path.AddPolygon(pointArray3);
                    brush = new LinearGradientBrush(new Point(0, 20), new Point(x + 1, 20), Color.FromArgb(0xc2, 190, 0x4d), Color.FromArgb(0xe2, 0xdd, 0x62));
                    graphics.FillPath(brush, path);
                    brush.Dispose();
                    path.Reset();
                    Point[] pointArray4 = new Point[] { new Point(x, y), new Point(x, base.Height - 8), new Point(base.Width - 1, base.Height - base.Width), new Point(base.Width - 1, y), new Point(x, y) };
                    path.AddPolygon(pointArray4);
                    brush = new LinearGradientBrush(new Point(x - 1, 20), new Point(base.Width - 1, 20), Color.FromArgb(0xe2, 0xdd, 0x62), Color.FromArgb(0xc2, 190, 0x4d));
                    graphics.FillPath(brush, path);
                    brush.Dispose();
                    path.Dispose();
                    brush = new SolidBrush(Color.FromArgb(0xf3, 0xf5, 0x8b));
                    graphics.FillEllipse(brush, 1, y - 3, base.Width - 3, 6);
                    brush.Dispose();
                    graphics.FillEllipse(Brushes.White, 4, base.Height - base.Width, base.Width - 9, base.Width - 9);
                    Pen pen = new Pen(Color.Gray, 3f);
                    if (this.isOpen)
                    {
                        pen.Color = Color.LimeGreen;
                    }
                    graphics.DrawEllipse(pen, 4, base.Height - base.Width, base.Width - 9, base.Width - 9);
                    graphics.FillEllipse(this.isOpen ? Brushes.LimeGreen : Brushes.Gray, 8, (base.Height - base.Width) + 4, base.Width - 0x11, base.Width - 0x11);
                    pen.Dispose();
                    if (!string.IsNullOrEmpty(this.bottleTag))
                    {
                        graphics.DrawString(this.bottleTag, this.Font, Brushes.Gray, new Rectangle(-10, 0x1a, base.Width + 20, 20), this.stringFormat);
                    }
                    if (!string.IsNullOrEmpty(this.headTag))
                    {
                        graphics.DrawString(this.headTag, this.Font, Brushes.DimGray, new Rectangle(-10, 0, base.Width + 20, 20), this.stringFormat);
                    }
                }
            }
        }

        private void UserBottle_Paint(object sender, PaintEventArgs e)
        {
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 20)
            {
                base.WndProc(ref m);
            }
        }

        [Browsable(true), DefaultValue(typeof(string), ""), Category("外观")]
        public string BottleTag
        {
            get
            {
                return this.bottleTag;
            }
            set
            {
                this.bottleTag = value;
                base.Invalidate();
            }
        }

        [Browsable(true), DefaultValue(typeof(string), "原料1"), Category("外观")]
        public string HeadTag
        {
            get
            {
                return this.headTag;
            }
            set
            {
                this.headTag = value;
                base.Invalidate();
            }
        }

        [Browsable(true), DefaultValue(typeof(bool), "false"), Category("外观")]
        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }
            set
            {
                this.isOpen = value;
                base.Invalidate();
            }
        }

        [Browsable(true), DefaultValue(typeof(double), "60"), Category("外观")]
        public double Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if ((value >= 0.0) && (value <= 100.0))
                {
                    this.value = value;
                    base.Invalidate();
                }
            }
        }
    }
}

