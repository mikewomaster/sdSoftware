namespace HslCommunication.Controls
{
    using HslCommunication;
    using HslCommunication.Core;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class UserPieChart : UserControl
    {
        private IContainer components = null;
        private StringFormat formatCenter = null;
        private bool m_IsRenderPercent = false;
        private bool m_IsRenderSmall = true;
        private int margin = 40;
        private HslPieItem[] pieItems = new HslPieItem[0];
        private Random random = null;

        public UserPieChart()
        {
            this.InitializeComponent();
            this.random = new Random();
            this.DoubleBuffered = true;
            this.formatCenter = new StringFormat();
            this.formatCenter.Alignment = StringAlignment.Center;
            this.formatCenter.LineAlignment = StringAlignment.Center;
            this.pieItems = new HslPieItem[0];
        }

        protected override void Dispose(bool disposing)
        {
            int expressionStack_10_0;
            if (disposing)
            {
                expressionStack_10_0 = (int) (this.components > null);
            }
            else
            {
                expressionStack_10_0 = 0;
            }
            if (expressionStack_10_0 != 0)
            {
                this.components.Dispose();
                if (this.formatCenter != null)
                {
                    this.formatCenter.Dispose();
                }
                else
                {
                    StringFormat formatCenter = this.formatCenter;
                }
            }
            base.Dispose(disposing);
        }

        private Point GetCenterPoint(out int width)
        {
            if (base.Width > base.Height)
            {
                this.SetMarginPaint(base.Height);
                width = (base.Height / 2) - this.margin;
                return new Point((base.Height / 2) - 1, (base.Height / 2) - 1);
            }
            this.SetMarginPaint(base.Width);
            width = (base.Width / 2) - this.margin;
            return new Point((base.Width / 2) - 1, (base.Width / 2) - 1);
        }

        private Color GetRandomColor()
        {
            int red = this.random.Next(0x100);
            int green = this.random.Next(0x100);
            int blue = ((red + green) > 430) ? this.random.Next(100) : this.random.Next(200);
            return Color.FromArgb(red, green, blue);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Transparent;
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Margin = new Padding(3, 4, 3, 4);
            base.Name = "UserPieChart";
            base.Size = new Size(200, 200);
            base.Load += new EventHandler(this.UserPieChart_Load);
            base.Paint += new PaintEventHandler(this.UserPieChart_Paint);
            base.ResumeLayout(false);
        }

        public void SetDataSource(HslPieItem[] source)
        {
            if (source > null)
            {
                this.pieItems = source;
                base.Invalidate();
            }
        }

        public void SetDataSource(string[] names, int[] values)
        {
            if (names == null)
            {
                throw new ArgumentNullException("names");
            }
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (names.Length != values.Length)
            {
                throw new Exception("两个数组的长度不一致！");
            }
            this.pieItems = new HslPieItem[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                this.pieItems[i] = new HslPieItem { 
                    Name = names[i],
                    Value = values[i],
                    Back = this.GetRandomColor()
                };
            }
            base.Invalidate();
        }

        private void SetMarginPaint(int value)
        {
            if (value > 500)
            {
                this.margin = 80;
            }
            else if (value > 300)
            {
                this.margin = 60;
            }
            else
            {
                this.margin = 40;
            }
        }

        private void UserPieChart_Load(object sender, EventArgs e)
        {
        }

        private void UserPieChart_Paint(object sender, PaintEventArgs e)
        {
            if (Authorization.nzugaydgwadawdibbas())
            {
                int num;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                Point centerPoint = this.GetCenterPoint(out num);
                Rectangle rect = new Rectangle(centerPoint.X - num, centerPoint.Y - num, num + num, num + num);
                if ((num > 0) && (this.pieItems.Length > 0))
                {
                    e.Graphics.FillEllipse(Brushes.AliceBlue, rect);
                    e.Graphics.DrawEllipse(Pens.DodgerBlue, rect);
                    Rectangle rectangle2 = new Rectangle(rect.X - centerPoint.X, rect.Y - centerPoint.Y, rect.Width, rect.Height);
                    e.Graphics.TranslateTransform((float) centerPoint.X, (float) centerPoint.Y);
                    e.Graphics.RotateTransform(90f);
                    e.Graphics.DrawLine(Pens.DimGray, 0, 0, num, 0);
                    int num2 = Enumerable.Sum<HslPieItem>(this.pieItems, (Func<HslPieItem, int>) (item => item.Value));
                    float num3 = 0f;
                    float num4 = -90f;
                    for (int i = 0; i < this.pieItems.Length; i++)
                    {
                        float num6 = 0f;
                        if (num2 == 0)
                        {
                            num6 = 360 / this.pieItems.Length;
                        }
                        else
                        {
                            num6 = Convert.ToSingle((double) (((this.pieItems[i].Value * 1.0) / ((double) num2)) * 360.0));
                        }
                        using (Brush brush = new SolidBrush(this.pieItems[i].Back))
                        {
                            e.Graphics.FillPie(brush, rectangle2, 0f, -num6);
                        }
                        e.Graphics.RotateTransform(0f - (num6 / 2f));
                        if ((num6 < 2f) && !this.IsRenderSmall)
                        {
                            num3 += num6;
                        }
                        else
                        {
                            num3 += num6 / 2f;
                            int num7 = 8;
                            if ((num3 < 45f) || (num3 > 315f))
                            {
                                num7 = 15;
                            }
                            if ((num3 > 135f) && (num3 < 225f))
                            {
                                num7 = 15;
                            }
                            e.Graphics.DrawLine(Pens.DimGray, (num * 2) / 3, 0, num + num7, 0);
                            e.Graphics.TranslateTransform((float) (num + num7), 0f);
                            if ((num3 - num4) < 5f)
                            {
                            }
                            num4 = num3;
                            if (num3 < 90f)
                            {
                                e.Graphics.RotateTransform(num3 - 90f);
                                e.Graphics.DrawLine(Pens.DimGray, 0, 0, this.margin - num7, 0);
                                e.Graphics.DrawString(this.pieItems[i].Name, this.Font, Brushes.DimGray, (PointF) new Point(0, -this.Font.Height));
                                if (this.IsRenderPercent)
                                {
                                    e.Graphics.DrawString(Math.Round((double) ((num6 * 100f) / 360f), 2).ToString() + "%", this.Font, Brushes.DodgerBlue, (PointF) new Point(0, 1));
                                }
                                e.Graphics.RotateTransform(90f - num3);
                            }
                            else if (num3 < 180f)
                            {
                                e.Graphics.RotateTransform(num3 - 90f);
                                e.Graphics.DrawLine(Pens.DimGray, 0, 0, this.margin - num7, 0);
                                e.Graphics.DrawString(this.pieItems[i].Name, this.Font, Brushes.DimGray, (PointF) new Point(0, -this.Font.Height));
                                if (this.IsRenderPercent)
                                {
                                    e.Graphics.DrawString(Math.Round((double) ((num6 * 100f) / 360f), 2).ToString() + "%", this.Font, Brushes.DodgerBlue, (PointF) new Point(0, 1));
                                }
                                e.Graphics.RotateTransform(90f - num3);
                            }
                            else if (num3 < 270f)
                            {
                                e.Graphics.RotateTransform(num3 - 270f);
                                e.Graphics.DrawLine(Pens.DimGray, 0, 0, this.margin - num7, 0);
                                e.Graphics.TranslateTransform((float) (this.margin - 8), 0f);
                                e.Graphics.RotateTransform(180f);
                                e.Graphics.DrawString(this.pieItems[i].Name, this.Font, Brushes.DimGray, (PointF) new Point(0, -this.Font.Height));
                                if (this.IsRenderPercent)
                                {
                                    e.Graphics.DrawString(Math.Round((double) ((num6 * 100f) / 360f), 2).ToString() + "%", this.Font, Brushes.DodgerBlue, (PointF) new Point(0, 1));
                                }
                                e.Graphics.RotateTransform(-180f);
                                e.Graphics.TranslateTransform((float) (8 - this.margin), 0f);
                                e.Graphics.RotateTransform(270f - num3);
                            }
                            else
                            {
                                e.Graphics.RotateTransform(num3 - 270f);
                                e.Graphics.DrawLine(Pens.DimGray, 0, 0, this.margin - num7, 0);
                                e.Graphics.TranslateTransform((float) (this.margin - 8), 0f);
                                e.Graphics.RotateTransform(180f);
                                e.Graphics.DrawString(this.pieItems[i].Name, this.Font, Brushes.DimGray, (PointF) new Point(0, -this.Font.Height));
                                if (this.IsRenderPercent)
                                {
                                    e.Graphics.DrawString(Math.Round((double) ((num6 * 100f) / 360f), 2).ToString() + "%", this.Font, Brushes.DodgerBlue, (PointF) new Point(0, 1));
                                }
                                e.Graphics.RotateTransform(-180f);
                                e.Graphics.TranslateTransform((float) (8 - this.margin), 0f);
                                e.Graphics.RotateTransform(270f - num3);
                            }
                            e.Graphics.TranslateTransform((float) (-num - num7), 0f);
                            e.Graphics.RotateTransform(0f - (num6 / 2f));
                            num3 += num6 / 2f;
                        }
                    }
                    e.Graphics.ResetTransform();
                }
                else
                {
                    e.Graphics.FillEllipse(Brushes.AliceBlue, rect);
                    e.Graphics.DrawEllipse(Pens.DodgerBlue, rect);
                    e.Graphics.DrawString("空", this.Font, Brushes.DimGray, rect, this.formatCenter);
                }
            }
        }

        [Browsable(true), Category("外观"), DefaultValue(false), Description("获取或设置是否显示百分比占用")]
        public bool IsRenderPercent
        {
            get
            {
                return this.m_IsRenderPercent;
            }
            set
            {
                this.m_IsRenderPercent = value;
                base.Invalidate();
            }
        }

        [Browsable(true), Category("外观"), Description("获取或设置是否显示占比很小的文本信息"), DefaultValue(true)]
        public bool IsRenderSmall
        {
            get
            {
                return this.m_IsRenderSmall;
            }
            set
            {
                this.m_IsRenderSmall = value;
                base.Invalidate();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UserPieChart.<>c <>9 = new UserPieChart.<>c();
            public static Func<HslPieItem, int> <>9__17_0;

            internal int <UserPieChart_Paint>b__17_0(HslPieItem item)
            {
                return item.Value;
            }
        }
    }
}

