namespace HslCommunication.Controls
{
    using HslCommunication;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Text;
    using System.Windows.Forms;

    public class UserPipe : UserControl
    {
        private Color activeColor = Color.Blue;
        private IContainer components = null;
        private bool isActive = true;
        private Color lineColor = Color.FromArgb(150, 150, 150);
        private float lineWidth = 5f;
        private float moveSpeed = 1f;
        private List<Point> points = new List<Point>();
        private float startOffect = 0f;
        private Timer timer = new Timer();

        public UserPipe()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
            this.timer.Interval = 50;
            this.timer.Tick += new EventHandler(this.Timer_Tick);
            this.timer.Start();
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
            base.Name = "UserPipe";
            base.Size = new Size(0x27b, 0x173);
            base.Load += new EventHandler(this.UserPipe_Load);
            base.Paint += new PaintEventHandler(this.UserPipe_Paint);
            base.ResumeLayout(false);
        }

        public void OnPaintMainWindow(Graphics g)
        {
            g.TranslateTransform((float) base.Location.X, (float) base.Location.Y);
            this.UserPipe_Paint(null, new PaintEventArgs(g, new Rectangle()));
            g.TranslateTransform((float) -base.Location.X, (float) -base.Location.Y);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.startOffect -= this.moveSpeed;
            if ((this.startOffect <= -10f) || (this.startOffect >= 10f))
            {
                this.startOffect = 0f;
            }
            base.Invalidate();
        }

        private void UserPipe_Load(object sender, EventArgs e)
        {
        }

        private void UserPipe_Paint(object sender, PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas())
            {
                Graphics graphics = e.Graphics;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Pen pen = new Pen(this.lineColor, this.lineWidth);
                if (this.points.Count > 1)
                {
                    graphics.DrawLines(pen, this.points.ToArray());
                }
                if (this.isActive)
                {
                    pen.DashStyle = DashStyle.Dash;
                    pen.DashPattern = new float[] { 5f, 5f };
                    pen.DashOffset = this.startOffect;
                    pen.Color = this.activeColor;
                    if (this.points.Count > 1)
                    {
                        graphics.DrawLines(pen, this.points.ToArray());
                    }
                }
                pen.Dispose();
            }
        }

        [Browsable(true), Description("获取或设置管道活动状态的颜色"), Category("外观"), DefaultValue(typeof(Color), "Blue")]
        public Color ActiveColor
        {
            get
            {
                return this.activeColor;
            }
            set
            {
                this.activeColor = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Description("获取或设置管道线是否处于活动状态"), Category("外观"), DefaultValue(true)]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Description("获取或设置管道的背景色"), Category("外观"), DefaultValue(typeof(Color), "(150, 150, 150 )")]
        public Color LineColor
        {
            get
            {
                return this.lineColor;
            }
            set
            {
                this.lineColor = value;
            }
        }

        [Browsable(true), Description("获取或设置管道线的坐标，格式为0,0;1,1;2,2 分号间隔点"), DefaultValue(""), Category("外观")]
        public string LinePoints
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < this.points.Count; i++)
                {
                    builder.Append(";");
                    Point point = this.points[i];
                    builder.Append(point.X.ToString());
                    builder.Append(",");
                    point = this.points[i];
                    builder.Append(point.Y.ToString());
                }
                if (builder.Length > 0)
                {
                    return builder.ToString().Substring(1);
                }
                return string.Empty;
            }
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.points.Clear();
                        char[] separator = new char[] { ';' };
                        string[] strArray = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            char[] chArray2 = new char[] { ',' };
                            string[] strArray2 = strArray[i].Split(chArray2, StringSplitOptions.RemoveEmptyEntries);
                            Point item = new Point {
                                X = Convert.ToInt32(strArray2[0]),
                                Y = Convert.ToInt32(strArray2[1])
                            };
                            this.points.Add(item);
                        }
                        base.Invalidate();
                    }
                }
                catch
                {
                }
            }
        }

        [Browsable(true), Description("获取或设置管道线的宽度"), Category("外观"), DefaultValue((float) 5f)]
        public float LineWidth
        {
            get
            {
                return this.lineWidth;
            }
            set
            {
                if (value > 0f)
                {
                    this.lineWidth = value;
                    base.Invalidate();
                }
            }
        }

        [Browsable(true), Description("获取或设置管道线的移动速度。该速度和管道的宽度有关"), Category("外观"), DefaultValue((float) 1f)]
        public float MoveSpeed
        {
            get
            {
                return this.moveSpeed;
            }
            set
            {
                this.moveSpeed = value;
                base.Invalidate();
            }
        }
    }
}

