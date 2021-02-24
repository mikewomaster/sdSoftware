namespace HslCommunication.LogNet
{
    using HslCommunication;
    using HslCommunication.BasicFramework;
    using HslCommunication.Controls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class LogNetAnalysisControl : UserControl
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private PaintItem <ClickSelected>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <IsMouseEnter>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Point <pointMove>k__BackingField;
        private CheckBox checkBox1;
        private IContainer components;
        private Label label1;
        private Label label2;
        private List<DateTime> listPaint = new List<DateTime>();
        private List<PaintItem> listRender = new List<PaintItem>();
        private string m_LogSource = string.Empty;
        private PictureBox pictureBox1;
        private UserButton selectButton = null;
        private StringFormat stringFormat;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private UserButton userButton_All;
        private UserButton userButton_Debug;
        private UserButton userButton_Error;
        private UserButton userButton_Fatal;
        private UserButton userButton_Info;
        private UserButton userButton_source;
        private UserButton userButton_Warn;

        public LogNetAnalysisControl()
        {
            StringFormat format1 = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            this.stringFormat = format1;
            this.components = null;
            this.InitializeComponent();
        }

        private void AnalysisLogSource(DateTime start, DateTime end, string degree)
        {
            if (!string.IsNullOrEmpty(this.m_LogSource))
            {
                StringBuilder builder = new StringBuilder();
                List<Match> list = new List<Match>(Regex.Matches(this.m_LogSource, "\x0002\\[[^\x0002]+").OfType<Match>());
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                List<DateTime> list2 = new List<DateTime>();
                for (int i = 0; i < list.Count; i++)
                {
                    Match match = list[i];
                    string str = match.Value.Substring(2, 5);
                    DateTime item = Convert.ToDateTime(match.Value.Substring(match.Value.IndexOf('2'), 0x13));
                    if (start == DateTime.MinValue)
                    {
                        if (i == 0)
                        {
                            this.textBox2.Text = match.Value.Substring(match.Value.IndexOf('2'), 0x13);
                        }
                        if (i == (list.Count - 1))
                        {
                            this.textBox3.Text = match.Value.Substring(match.Value.IndexOf('2'), 0x13);
                        }
                    }
                    if (((start <= item) && (item <= end)) && (!this.checkBox1.Checked || Regex.IsMatch(match.Value, this.textBox4.Text)))
                    {
                        if (str.StartsWith(StringResources.Language.LogNetDebug))
                        {
                            num++;
                        }
                        else if (str.StartsWith(StringResources.Language.LogNetInfo))
                        {
                            num2++;
                        }
                        else if (str.StartsWith(StringResources.Language.LogNetWarn))
                        {
                            num3++;
                        }
                        else if (str.StartsWith(StringResources.Language.LogNetError))
                        {
                            num4++;
                        }
                        else if (str.StartsWith(StringResources.Language.LogNetFatal))
                        {
                            num5++;
                        }
                        num6++;
                        if ((degree == StringResources.Language.LogNetAll) || str.StartsWith(degree))
                        {
                            builder.Append(match.Value.Substring(1));
                            list2.Add(item);
                        }
                    }
                }
                this.userButton_Debug.UIText = string.Format("{0} ({1})", StringResources.Language.LogNetDebug, num);
                this.userButton_Info.UIText = string.Format("{0} ({1})", StringResources.Language.LogNetInfo, num2);
                this.userButton_Warn.UIText = string.Format("{0} ({1})", StringResources.Language.LogNetWarn, num3);
                this.userButton_Error.UIText = string.Format("{0} ({1})", StringResources.Language.LogNetError, num4);
                this.userButton_Fatal.UIText = string.Format("{0} ({1})", StringResources.Language.LogNetFatal, num5);
                this.userButton_All.UIText = string.Format("{0} ({1})", StringResources.Language.LogNetAll, num6);
                this.textBox1.Text = builder.ToString();
                this.listPaint = list2;
                if (this.pictureBox1.Width > 10)
                {
                    this.pictureBox1.Image = this.PaintData(this.pictureBox1.Width, this.pictureBox1.Height);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components > null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FilterLogSource(string degree)
        {
            if (!string.IsNullOrEmpty(this.m_LogSource))
            {
                DateTime time;
                if (!DateTime.TryParse(this.textBox2.Text, out time))
                {
                    MessageBox.Show("起始时间的格式不正确，请重新输入");
                }
                else
                {
                    DateTime time2;
                    if (!DateTime.TryParse(this.textBox3.Text, out time2))
                    {
                        MessageBox.Show("结束时间的格式不正确，请重新输入");
                    }
                    else
                    {
                        this.AnalysisLogSource(time, time2, degree);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.label1 = new Label();
            this.textBox3 = new TextBox();
            this.label2 = new Label();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.pictureBox1 = new PictureBox();
            this.checkBox1 = new CheckBox();
            this.textBox4 = new TextBox();
            this.userButton_source = new UserButton();
            this.userButton_All = new UserButton();
            this.userButton_Fatal = new UserButton();
            this.userButton_Error = new UserButton();
            this.userButton_Warn = new UserButton();
            this.userButton_Info = new UserButton();
            this.userButton_Debug = new UserButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.textBox1.BorderStyle = BorderStyle.FixedSingle;
            this.textBox1.Dock = DockStyle.Fill;
            this.textBox1.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox1.Location = new Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = ScrollBars.Vertical;
            this.textBox1.Size = new Size(0x2d8, 0x1b2);
            this.textBox1.TabIndex = 0;
            this.textBox2.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox2.Location = new Point(0x5c, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x9c, 0x17);
            this.textBox2.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0x108, 6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1c, 0x11);
            this.label1.TabIndex = 3;
            this.label1.Text = "----";
            this.textBox3.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox3.Location = new Point(0x130, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0x9c, 0x17);
            this.textBox3.TabIndex = 4;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x44, 0x11);
            this.label2.TabIndex = 12;
            this.label2.Text = "时间选择：";
            this.tabControl1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.tabControl1.Location = new Point(6, 0x22);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x2e6, 470);
            this.tabControl1.TabIndex = 15;
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new Point(4, 0x1a);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x2de, 440);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据视图";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new Point(4, 0x1a);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x2de, 440);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "分布视图";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.pictureBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.pictureBox1.Location = new Point(6, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x2c4, 0x192);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.SizeChanged += new EventHandler(this.pictureBox1_SizeChanged);
            this.pictureBox1.Paint += new PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.DoubleClick += new EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseEnter += new EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new MouseEventHandler(this.pictureBox1_MouseMove);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.checkBox1.Location = new Point(0x202, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x6f, 0x15);
            this.checkBox1.TabIndex = 0x10;
            this.checkBox1.Text = "使用正则表达式";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.textBox4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.textBox4.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox4.Location = new Point(0x202, 0x1b);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(0xe3, 0x17);
            this.textBox4.TabIndex = 0x11;
            this.userButton_source.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.userButton_source.BackColor = Color.Transparent;
            this.userButton_source.CustomerInformation = "";
            this.userButton_source.EnableColor = Color.FromArgb(190, 190, 190);
            this.userButton_source.Font = new Font("微软雅黑", 9f);
            this.userButton_source.Location = new Point(650, 0x201);
            this.userButton_source.Margin = new Padding(3, 4, 3, 4);
            this.userButton_source.Name = "userButton_source";
            this.userButton_source.Size = new Size(0x62, 0x19);
            this.userButton_source.TabIndex = 13;
            this.userButton_source.UIText = "源日志";
            this.userButton_source.Click += new EventHandler(this.userButton_source_Click);
            this.userButton_All.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.userButton_All.BackColor = Color.Transparent;
            this.userButton_All.CustomerInformation = "";
            this.userButton_All.EnableColor = Color.FromArgb(190, 190, 190);
            this.userButton_All.Font = new Font("微软雅黑", 9f);
            this.userButton_All.Location = new Point(0x20d, 0x201);
            this.userButton_All.Margin = new Padding(3, 4, 3, 4);
            this.userButton_All.Name = "userButton_All";
            this.userButton_All.Size = new Size(0x62, 0x19);
            this.userButton_All.TabIndex = 11;
            this.userButton_All.UIText = "全部";
            this.userButton_All.Click += new EventHandler(this.userButton_All_Click);
            this.userButton_Fatal.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.userButton_Fatal.BackColor = Color.Transparent;
            this.userButton_Fatal.CustomerInformation = "";
            this.userButton_Fatal.EnableColor = Color.FromArgb(190, 190, 190);
            this.userButton_Fatal.Font = new Font("微软雅黑", 9f);
            this.userButton_Fatal.Location = new Point(0x1a6, 0x201);
            this.userButton_Fatal.Margin = new Padding(3, 4, 3, 4);
            this.userButton_Fatal.Name = "userButton_Fatal";
            this.userButton_Fatal.Size = new Size(0x62, 0x19);
            this.userButton_Fatal.TabIndex = 10;
            this.userButton_Fatal.UIText = "致命";
            this.userButton_Fatal.Click += new EventHandler(this.userButton_Fatal_Click);
            this.userButton_Error.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.userButton_Error.BackColor = Color.Transparent;
            this.userButton_Error.CustomerInformation = "";
            this.userButton_Error.EnableColor = Color.FromArgb(190, 190, 190);
            this.userButton_Error.Font = new Font("微软雅黑", 9f);
            this.userButton_Error.Location = new Point(0x13e, 0x201);
            this.userButton_Error.Margin = new Padding(3, 4, 3, 4);
            this.userButton_Error.Name = "userButton_Error";
            this.userButton_Error.Size = new Size(0x62, 0x19);
            this.userButton_Error.TabIndex = 9;
            this.userButton_Error.UIText = "错误";
            this.userButton_Error.Click += new EventHandler(this.userButton_Error_Click);
            this.userButton_Warn.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.userButton_Warn.BackColor = Color.Transparent;
            this.userButton_Warn.CustomerInformation = "";
            this.userButton_Warn.EnableColor = Color.FromArgb(190, 190, 190);
            this.userButton_Warn.Font = new Font("微软雅黑", 9f);
            this.userButton_Warn.Location = new Point(0xd6, 0x201);
            this.userButton_Warn.Margin = new Padding(3, 4, 3, 4);
            this.userButton_Warn.Name = "userButton_Warn";
            this.userButton_Warn.Size = new Size(0x62, 0x19);
            this.userButton_Warn.TabIndex = 8;
            this.userButton_Warn.UIText = "警告";
            this.userButton_Warn.Click += new EventHandler(this.userButton_Warn_Click);
            this.userButton_Info.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.userButton_Info.BackColor = Color.Transparent;
            this.userButton_Info.CustomerInformation = "";
            this.userButton_Info.EnableColor = Color.FromArgb(190, 190, 190);
            this.userButton_Info.Font = new Font("微软雅黑", 9f);
            this.userButton_Info.Location = new Point(110, 0x201);
            this.userButton_Info.Margin = new Padding(3, 4, 3, 4);
            this.userButton_Info.Name = "userButton_Info";
            this.userButton_Info.Size = new Size(0x62, 0x19);
            this.userButton_Info.TabIndex = 7;
            this.userButton_Info.UIText = "信息";
            this.userButton_Info.Click += new EventHandler(this.userButton_Info_Click);
            this.userButton_Debug.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.userButton_Debug.BackColor = Color.Transparent;
            this.userButton_Debug.CustomerInformation = "";
            this.userButton_Debug.EnableColor = Color.FromArgb(190, 190, 190);
            this.userButton_Debug.Font = new Font("微软雅黑", 9f);
            this.userButton_Debug.Location = new Point(6, 0x201);
            this.userButton_Debug.Margin = new Padding(3, 4, 3, 4);
            this.userButton_Debug.Name = "userButton_Debug";
            this.userButton_Debug.Size = new Size(0x62, 0x19);
            this.userButton_Debug.TabIndex = 6;
            this.userButton_Debug.UIText = "调试";
            this.userButton_Debug.Click += new EventHandler(this.userButton_Debug_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.textBox4);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.userButton_source);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.userButton_All);
            base.Controls.Add(this.userButton_Fatal);
            base.Controls.Add(this.userButton_Error);
            base.Controls.Add(this.userButton_Warn);
            base.Controls.Add(this.userButton_Info);
            base.Controls.Add(this.userButton_Debug);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textBox2);
            base.Name = "LogNetAnalysisControl";
            base.Size = new Size(0x2f0, 0x21e);
            base.Load += new EventHandler(this.LogNetAnalysisControl_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LogNetAnalysisControl_Load(object sender, EventArgs e)
        {
        }

        private Bitmap PaintData(int width, int height)
        {
            if (width < 200)
            {
                width = 200;
            }
            if (height < 100)
            {
                height = 100;
            }
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            Font font = new Font("宋体", 12f);
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };
            Pen penDash = new Pen(Color.LightGray, 1f) {
                DashStyle = DashStyle.Custom
            };
            penDash.DashPattern = new float[] { 5f, 5f };
            g.Clear(Color.White);
            if (this.listPaint.Count <= 5)
            {
                g.DrawString("数据太少了", font, Brushes.DeepSkyBlue, new Rectangle(0, 0, width, height), format);
            }
            else
            {
                int num = (width - 60) / 6;
                TimeSpan span = this.listPaint.Max<DateTime>() - this.listPaint.Min<DateTime>();
                DateTime time = this.listPaint.Min<DateTime>();
                double num2 = span.TotalSeconds / ((double) num);
                int[] source = new int[num];
                for (int i = 0; i < this.listPaint.Count; i++)
                {
                    TimeSpan span2 = this.listPaint[i] - time;
                    int index = (int) (span2.TotalSeconds / num2);
                    if (index < 0)
                    {
                        index = 0;
                    }
                    if (index == num)
                    {
                        index--;
                    }
                    source[index]++;
                }
                int max = source.Max();
                int min = 0;
                PaintItem[] collection = new PaintItem[num];
                for (int j = 0; j < source.Length; j++)
                {
                    PaintItem item = new PaintItem {
                        Count = source[j]
                    };
                    DateTime time2 = this.listPaint[0];
                    item.Start = time2.AddSeconds(j * num2);
                    if (j == (source.Length - 1))
                    {
                        item.End = this.listPaint[this.listPaint.Count - 1];
                    }
                    else
                    {
                        item.End = this.listPaint[0].AddSeconds((j + 1) * num2);
                    }
                    collection[j] = item;
                }
                this.listRender = new List<PaintItem>(collection);
                int num5 = 50;
                int right = 10;
                int up = 20;
                int down = 30;
                g.DrawLine(Pens.DimGray, num5, up - 10, num5, height - down);
                g.DrawLine(Pens.DimGray, num5, (height - down) + 1, width - right, (height - down) + 1);
                g.SmoothingMode = SmoothingMode.HighQuality;
                SoftPainting.PaintTriangle(g, Brushes.DimGray, new Point(num5, up - 10), 5, GraphDirection.Upward);
                g.SmoothingMode = SmoothingMode.None;
                int degree = 8;
                if (height < 500)
                {
                    if ((max < 15) && (max > 1))
                    {
                        degree = max;
                    }
                }
                else if (height < 700)
                {
                    if ((max < 0x19) && (max > 1))
                    {
                        degree = max;
                    }
                    else
                    {
                        degree = 0x10;
                    }
                }
                else if ((max < 40) && (max > 1))
                {
                    degree = max;
                }
                else
                {
                    degree = 0x18;
                }
                SoftPainting.PaintCoordinateDivide(g, Pens.DimGray, penDash, font, Brushes.DimGray, format, degree, max, min, width, height, num5, right, up, down);
                format.Alignment = StringAlignment.Center;
                g.DrawString("Totle: " + this.listPaint.Count.ToString(), font, Brushes.DodgerBlue, new RectangleF((float) num5, 0f, (float) ((width - num5) - right), (float) up), format);
                int num10 = num5 + 2;
                for (int k = 0; k < collection.Length; k++)
                {
                    float y = SoftPainting.ComputePaintLocationY(max, min, (height - up) - down, collection[k].Count) + up;
                    RectangleF rect = new RectangleF((float) num10, y, 5f, (height - down) - y);
                    if ((rect.Height <= 0f) && (collection[k].Count > 0))
                    {
                        rect = new RectangleF((float) num10, (float) ((height - down) - 1), 5f, 1f);
                    }
                    g.FillRectangle(Brushes.Tomato, rect);
                    num10 += 6;
                }
                g.DrawLine(Pens.DimGray, num10, up - 10, num10, height - down);
                g.SmoothingMode = SmoothingMode.HighQuality;
                SoftPainting.PaintTriangle(g, Brushes.DimGray, new Point(num10, up - 10), 5, GraphDirection.Upward);
                g.SmoothingMode = SmoothingMode.None;
            }
            format.Dispose();
            font.Dispose();
            penDash.Dispose();
            g.Dispose();
            return image;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if ((this.IsMouseEnter && ((((this.pointMove.Y > 20) && (this.pointMove.Y < (this.pictureBox1.Height - 30))) && (this.pointMove.X > 0x33)) && (this.pointMove.X < (this.pictureBox1.Width - 10)))) && (this.selectButton > null))
            {
                TimeSpan span = (TimeSpan) (this.ClickSelected.End - this.ClickSelected.Start);
                if (span.TotalSeconds > 3.0)
                {
                    this.textBox2.Text = this.ClickSelected.Start.ToString("yyyy-MM-dd HH:mm:ss");
                    this.textBox3.Text = this.ClickSelected.End.ToString("yyyy-MM-dd HH:mm:ss");
                    this.AnalysisLogSource(this.ClickSelected.Start, this.ClickSelected.End, this.selectButton.UIText.Substring(0, 2));
                }
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.IsMouseEnter = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.IsMouseEnter = false;
            this.pictureBox1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.IsMouseEnter && ((((e.Y > 20) && (e.Y < (this.pictureBox1.Height - 30))) && (e.X > 0x33)) && (e.X < (this.pictureBox1.Width - 10)))) && (((e.X - 0x34) % 6) != 5))
            {
                int num = (e.X - 0x34) / 6;
                if (num < this.listRender.Count)
                {
                    this.pointMove = e.Location;
                    this.ClickSelected = this.listRender[num];
                    this.pictureBox1.Refresh();
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if ((this.IsMouseEnter && (this.ClickSelected > null)) && (this.pictureBox1.Width > 100))
            {
                string[] textArray1 = new string[] { this.ClickSelected.Start.ToString("yyyy-MM-dd HH:mm:ss"), "  -  ", this.ClickSelected.End.ToString("yyyy-MM-dd HH:mm:ss"), Environment.NewLine, "Count:", this.ClickSelected.Count.ToString() };
                string s = string.Concat(textArray1);
                e.Graphics.DrawString(s, this.Font, Brushes.DimGray, new Rectangle(50, this.pictureBox1.Height - 0x1b, this.pictureBox1.Width - 60, 30), this.stringFormat);
                e.Graphics.DrawLine(Pens.DeepPink, this.pointMove.X, 15, this.pointMove.X, this.pictureBox1.Height - 30);
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (this.pictureBox1.Width > 10)
            {
                this.pictureBox1.Image = this.PaintData(this.pictureBox1.Width, this.pictureBox1.Height);
            }
        }

        public void SetLogNetSource(string logSource)
        {
            this.m_LogSource = logSource;
            this.SetLogNetSourceView();
        }

        private void SetLogNetSourceView()
        {
            if (!string.IsNullOrEmpty(this.m_LogSource))
            {
                this.AnalysisLogSource(DateTime.MinValue, DateTime.MaxValue, StringResources.Language.LogNetAll);
                if (this.selectButton > null)
                {
                    this.selectButton.Selected = false;
                }
                this.selectButton = this.userButton_All;
            }
        }

        private void userButton_All_Click(object sender, EventArgs e)
        {
            this.UserButtonSetSelected(this.userButton_All);
            this.FilterLogSource(StringResources.Language.LogNetAll);
        }

        private void userButton_Debug_Click(object sender, EventArgs e)
        {
            this.UserButtonSetSelected(this.userButton_Debug);
            this.FilterLogSource(StringResources.Language.LogNetDebug);
        }

        private void userButton_Error_Click(object sender, EventArgs e)
        {
            this.UserButtonSetSelected(this.userButton_Error);
            this.FilterLogSource(StringResources.Language.LogNetError);
        }

        private void userButton_Fatal_Click(object sender, EventArgs e)
        {
            this.UserButtonSetSelected(this.userButton_Fatal);
            this.FilterLogSource(StringResources.Language.LogNetFatal);
        }

        private void userButton_Info_Click(object sender, EventArgs e)
        {
            this.UserButtonSetSelected(this.userButton_Info);
            this.FilterLogSource(StringResources.Language.LogNetInfo);
        }

        private void userButton_source_Click(object sender, EventArgs e)
        {
            this.SetLogNetSourceView();
        }

        private void userButton_Warn_Click(object sender, EventArgs e)
        {
            this.UserButtonSetSelected(this.userButton_Warn);
            this.FilterLogSource(StringResources.Language.LogNetWarn);
        }

        private void UserButtonSetSelected(UserButton userButton)
        {
            if (this.selectButton != userButton)
            {
                if (this.selectButton > null)
                {
                    this.selectButton.Selected = false;
                }
                userButton.Selected = true;
                this.selectButton = userButton;
            }
        }

        private PaintItem ClickSelected { get; set; }

        private bool IsMouseEnter { get; set; }

        private Point pointMove { get; set; }

        private class PaintItem
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int <Count>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private DateTime <End>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private DateTime <Start>k__BackingField;

            public int Count { get; set; }

            public DateTime End { get; set; }

            public DateTime Start { get; set; }
        }
    }
}

