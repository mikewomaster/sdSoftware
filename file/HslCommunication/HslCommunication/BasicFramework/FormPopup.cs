namespace HslCommunication.BasicFramework
{
    using HslCommunication;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class FormPopup : Form
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Color <InfoColor>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <InfoExistTime>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <InfoText>k__BackingField;
        private const int AW_ACTIVE = 0x20000;
        private const int AW_BLEND = 0x80000;
        private const int AW_CENTER = 0x10;
        private const int AW_HIDE = 0x10000;
        private const int AW_HOR_NEGATIVE = 2;
        private const int AW_HOR_POSITIVE = 1;
        private const int AW_SLIDE = 0x40000;
        private const int AW_VER_NEGATIVE = 8;
        private const int AW_VER_POSITIVE = 4;
        private IContainer components;
        private static List<FormPopup> FormsPopup = new List<FormPopup>();
        private Label label1;
        private Label label2;
        private Timer time;

        public FormPopup()
        {
            this.<InfoText>k__BackingField = "This is a test message";
            this.<InfoColor>k__BackingField = Color.DimGray;
            this.<InfoExistTime>k__BackingField = -1;
            this.time = null;
            this.components = null;
            this.InitializeComponent();
        }

        public FormPopup(string infotext)
        {
            this.<InfoText>k__BackingField = "This is a test message";
            this.<InfoColor>k__BackingField = Color.DimGray;
            this.<InfoExistTime>k__BackingField = -1;
            this.time = null;
            this.components = null;
            this.InitializeComponent();
            this.InfoText = infotext;
        }

        public FormPopup(string infotext, Color infocolor)
        {
            this.<InfoText>k__BackingField = "This is a test message";
            this.<InfoColor>k__BackingField = Color.DimGray;
            this.<InfoExistTime>k__BackingField = -1;
            this.time = null;
            this.components = null;
            this.InitializeComponent();
            this.InfoText = infotext;
            this.InfoColor = infocolor;
        }

        public FormPopup(string infotext, Color infocolor, int existTime)
        {
            this.<InfoText>k__BackingField = "This is a test message";
            this.<InfoColor>k__BackingField = Color.DimGray;
            this.<InfoExistTime>k__BackingField = -1;
            this.time = null;
            this.components = null;
            this.InitializeComponent();
            this.InfoText = infotext;
            this.InfoColor = infocolor;
            this.InfoExistTime = existTime;
        }

        private static void AddNewForm(FormPopup form)
        {
            try
            {
                foreach (FormPopup popup in FormsPopup)
                {
                    popup.LocationUpMove();
                }
                FormsPopup.Add(form);
            }
            catch (Exception exception)
            {
                Console.WriteLine(SoftBasic.GetExceptionMessage(exception));
            }
        }

        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components > null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormPopup_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.time.Enabled = false;
                FormsPopup.Remove(this);
                ResetLocation();
            }
            catch (Exception exception)
            {
                Console.WriteLine(SoftBasic.GetExceptionMessage(exception));
            }
        }

        private void FormPopup_Load(object sender, EventArgs e)
        {
            this.label1.Text = this.InfoText;
            this.label1.ForeColor = this.InfoColor;
            this.label2.Text = StringResources.Language.Close;
            AddNewForm(this);
            int x = Screen.PrimaryScreen.WorkingArea.Right - base.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - base.Height;
            base.Location = new Point(x, y);
            AnimateWindow(base.Handle, 0x3e8, 0x40008);
            base.TopMost = true;
            if (this.InfoExistTime > 100)
            {
                this.time = new Timer();
                this.time.Interval = this.InfoExistTime;
                this.time.Tick += delegate (object <p0>, EventArgs <p1>) {
                    if (base.IsHandleCreated)
                    {
                        this.time.Dispose();
                        AnimateWindow(base.Handle, 0x3e8, 0x90000);
                        base.Close();
                    }
                };
                this.time.Start();
            }
        }

        private void FormPopup_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.FillRectangle(Brushes.SkyBlue, new Rectangle(0, 0, base.Width - 1, 30));
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
            graphics.DrawString(StringResources.Language.MessageTip, this.label2.Font, Brushes.DimGray, new Rectangle(5, 0, base.Width - 1, 30), format);
            graphics.DrawRectangle(Pens.DimGray, 0, 0, base.Width - 1, base.Height - 1);
        }

        private void InitializeComponent()
        {
            this.label2 = new Label();
            this.label1 = new Label();
            base.SuspendLayout();
            this.label2.BackColor = Color.MistyRose;
            this.label2.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(0x11f, 4);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 0x15);
            this.label2.TabIndex = 1;
            this.label2.Text = "关闭";
            this.label2.TextAlign = ContentAlignment.MiddleCenter;
            this.label2.Click += new EventHandler(this.label2_Click);
            this.label2.MouseEnter += new EventHandler(this.label2_MouseEnter);
            this.label2.MouseLeave += new EventHandler(this.label2_MouseLeave);
            this.label1.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.ForeColor = Color.DimGray;
            this.label1.Location = new Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x135, 0x77);
            this.label1.TabIndex = 2;
            this.label1.Text = "这是一条测试消息";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new SizeF(10f, 21f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x14d, 0xa3);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label2);
            this.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Margin = new Padding(4, 5, 4, 5);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(0x14d, 0xa3);
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x14d, 0xa3);
            base.Name = "FormPopup";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "消息";
            base.FormClosing += new FormClosingEventHandler(this.FormPopup_Closing);
            base.Load += new EventHandler(this.FormPopup_Load);
            base.Paint += new PaintEventHandler(this.FormPopup_Paint);
            base.ResumeLayout(false);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            SoftAnimation.BeginBackcolorAnimation(this.label2, Color.Tomato, 100);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            SoftAnimation.BeginBackcolorAnimation(this.label2, Color.MistyRose, 100);
        }

        public void LocationUpMove()
        {
            base.Location = new Point(base.Location.X, base.Location.Y - base.Height);
        }

        public void LocationUpMove(int index)
        {
            base.Location = new Point(base.Location.X, (Screen.PrimaryScreen.WorkingArea.Bottom - base.Height) - (index * base.Height));
        }

        private static void ResetLocation()
        {
            try
            {
                for (int i = 0; i < FormsPopup.Count; i++)
                {
                    FormsPopup[i].LocationUpMove((FormsPopup.Count - 1) - i);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(SoftBasic.GetExceptionMessage(exception));
            }
        }

        private Color InfoColor { get; set; }

        private int InfoExistTime { get; set; }

        private string InfoText { get; set; }
    }
}

