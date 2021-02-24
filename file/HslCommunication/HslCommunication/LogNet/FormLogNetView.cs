namespace HslCommunication.LogNet
{
    using HslCommunication.BasicFramework;
    using HslCommunication.Controls;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class FormLogNetView : Form
    {
        private IContainer components = null;
        private Label label1;
        private LogNetAnalysisControl logNetAnalysisControl1;
        private StatusStrip statusStrip1;
        private TextBox textBox1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private UserButton userButton1;

        public FormLogNetView()
        {
            this.InitializeComponent();
        }

        private void DealWithFileName(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("文件不存在！");
                }
                else
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
                        {
                            try
                            {
                                this.logNetAnalysisControl1.SetLogNetSource(reader.ReadToEnd());
                            }
                            catch (Exception exception)
                            {
                                SoftBasic.ShowExceptionMessage(exception);
                            }
                        }
                    }
                    catch (Exception exception2)
                    {
                        SoftBasic.ShowExceptionMessage(exception2);
                    }
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

        private void FormLogNetView_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormLogNetView));
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.statusStrip1 = new StatusStrip();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.userButton1 = new UserButton();
            this.logNetAnalysisControl1 = new LogNetAnalysisControl();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x44, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件路径：";
            this.textBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.textBox1.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.textBox1.Location = new Point(0x4a, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x26e, 0x17);
            this.textBox1.TabIndex = 2;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.toolStripStatusLabel1 };
            this.statusStrip1.Items.AddRange(toolStripItems);
            this.statusStrip1.Location = new Point(0, 0x22b);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(0x338, 0x16);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            this.toolStripStatusLabel1.ForeColor = Color.FromArgb(0x40, 0x40, 0x40);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(0xf8, 0x11);
            this.toolStripStatusLabel1.Text = "本日志查看器由HslCommunication提供支持";
            this.userButton1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.userButton1.BackColor = Color.Transparent;
            this.userButton1.CustomerInformation = "";
            this.userButton1.EnableColor = Color.FromArgb(190, 190, 190);
            this.userButton1.Font = new Font("微软雅黑", 9f);
            this.userButton1.Location = new Point(0x2cd, 6);
            this.userButton1.Margin = new Padding(3, 4, 3, 4);
            this.userButton1.Name = "userButton1";
            this.userButton1.Size = new Size(0x5f, 0x19);
            this.userButton1.TabIndex = 3;
            this.userButton1.UIText = "文件选择";
            this.userButton1.Click += new EventHandler(this.userButton1_Click);
            this.logNetAnalysisControl1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.logNetAnalysisControl1.Location = new Point(6, 30);
            this.logNetAnalysisControl1.Name = "logNetAnalysisControl1";
            this.logNetAnalysisControl1.Size = new Size(0x332, 0x20a);
            this.logNetAnalysisControl1.TabIndex = 0;
            this.logNetAnalysisControl1.Load += new EventHandler(this.logNetAnalysisControl1_Load);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x338, 0x241);
            base.Controls.Add(this.statusStrip1);
            base.Controls.Add(this.userButton1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.logNetAnalysisControl1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "FormLogNetView";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "日志查看器";
            base.Load += new EventHandler(this.FormLogNetView_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void logNetAnalysisControl1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", "https://github.com/dathlin/C-S-");
            }
            catch
            {
            }
        }

        private void userButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "日志文件(*.txt)|*.txt";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = dialog.FileName;
                    this.DealWithFileName(dialog.FileName);
                }
            }
        }
    }
}

