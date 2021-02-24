namespace HslCommunication.BasicFramework
{
    using HslCommunication.Controls;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class FormAuthorize : Form
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <AboutCode>k__BackingField = "";
        private IContainer components = null;
        private Func<string, string> Encrypt = null;
        private Label label1;
        private Label label2;
        private LinkLabel linkLabel1;
        private SoftAuthorize softAuthorize = null;
        private TextBox textBox1;
        private TextBox textBox2;
        private UserButton userButton1;

        public FormAuthorize(SoftAuthorize authorize, string aboutCode, Func<string, string> encrypt)
        {
            this.InitializeComponent();
            this.softAuthorize = authorize;
            this.AboutCode = aboutCode;
            this.Encrypt = encrypt;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components > null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormAuthorize_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = this.softAuthorize.GetMachineCodeString();
        }

        private void InitializeComponent()
        {
            this.textBox2 = new TextBox();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.linkLabel1 = new LinkLabel();
            this.userButton1 = new UserButton();
            base.SuspendLayout();
            this.textBox2.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox2.Location = new Point(0x7c, 0x4e);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x124, 30);
            this.textBox2.TabIndex = 7;
            this.textBox1.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox1.Location = new Point(0x7c, 0x23);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x124, 30);
            this.textBox1.TabIndex = 6;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("微软雅黑", 18f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(12, 0x4d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(110, 0x1f);
            this.label2.TabIndex = 5;
            this.label2.Text = "注册码：";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 18f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(12, 0x22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(110, 0x1f);
            this.label1.TabIndex = 4;
            this.label1.Text = "机器码：";
            this.linkLabel1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.linkLabel1.Location = new Point(0x127, 0xae);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new Size(0x89, 0x17);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "关于注册码";
            this.linkLabel1.TextAlign = ContentAlignment.TopRight;
            this.linkLabel1.Click += new EventHandler(this.linkLabel1_Click);
            this.userButton1.BackColor = Color.Transparent;
            this.userButton1.CustomerInformation = "";
            this.userButton1.EnableColor = Color.FromArgb(190, 190, 190);
            this.userButton1.Font = new Font("微软雅黑", 9f);
            this.userButton1.Location = new Point(0x87, 130);
            this.userButton1.Margin = new Padding(3, 4, 3, 4);
            this.userButton1.Name = "userButton1";
            this.userButton1.Size = new Size(0xa6, 0x21);
            this.userButton1.TabIndex = 8;
            this.userButton1.UIText = "注册";
            this.userButton1.Click += new EventHandler(this.userButton1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b3, 0xc1);
            base.Controls.Add(this.linkLabel1);
            base.Controls.Add(this.userButton1);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(0x1c3, 0xe8);
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x1c3, 0xe8);
            base.Name = "FormAuthorize";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "注册软件";
            base.TopMost = true;
            base.Load += new EventHandler(this.FormAuthorize_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.AboutCode);
        }

        private void userButton1_Click(object sender, EventArgs e)
        {
            if (this.softAuthorize.CheckAuthorize(this.textBox2.Text, this.Encrypt))
            {
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("注册码不正确");
            }
        }

        private string AboutCode { get; set; }
    }
}

