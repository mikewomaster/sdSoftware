namespace HslCommunication.BasicFramework
{
    using HslCommunication.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormSupport : Form
    {
        private IContainer components = null;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;

        public FormSupport()
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
            this.label1 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.pictureBox1 = new PictureBox();
            this.pictureBox2 = new PictureBox();
            this.label6 = new Label();
            this.label2 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.label9 = new Label();
            this.label10 = new Label();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            ((ISupportInitialize) this.pictureBox2).BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x20, 0x164);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x260, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "如果这个组件真的帮到了你或你们公司，那么非常感谢您的支持，个人打赏，请视个人能力选择金额，感谢支持。";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x20, 0x1f5);
            this.label3.Name = "label3";
            this.label3.Size = new Size(560, 0x11);
            this.label3.TabIndex = 3;
            this.label3.Text = "如果您的公司使用了本产品，那么非常感谢对本产品的信任，企业赞助或是合作请加群后专门联系作者。";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x138, 0x207);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x183, 0x11);
            this.label4.TabIndex = 4;
            this.label4.Text = "作者：Richard.Hu 上图为支付宝和微信账户的收钱码，请认准官方账户";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x20, 0x175);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1e8, 0x11);
            this.label5.TabIndex = 5;
            this.label5.Text = "如果不小心点错了，需要退款，请通过邮箱联系作者，提供付款的截图或是其他证明即可。";
            this.pictureBox1.Image = Resources.alipay;
            this.pictureBox1.Location = new Point(0x23, 13);
            this.pictureBox1.Margin = new Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x10b, 0x153);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox2.Image = Resources.mm_facetoface_collect_qrcode_1525331158525;
            this.pictureBox2.Location = new Point(0x19b, 10);
            this.pictureBox2.Margin = new Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(0xf7, 0x156);
            this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            this.label6.AutoSize = true;
            this.label6.ForeColor = Color.Blue;
            this.label6.Location = new Point(0x20, 0x18c);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x28d, 0x22);
            this.label6.TabIndex = 7;
            this.label6.Text = "技术支持及探讨学习群，需要赞助240元以上才能加入，申请入群时请提供微信或是支付宝的付款时间，方便管理员核对，\r\n谢谢支持：群号：838185568  群功能如下：";
            this.label2.AutoSize = true;
            this.label2.ForeColor = Color.FromArgb(0xc0, 0x40, 0);
            this.label2.Location = new Point(0x20, 0x1b7);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xe2, 0x11);
            this.label2.TabIndex = 8;
            this.label2.Text = "1. 作者给与一定的经验分享，疑问解答。";
            this.label7.AutoSize = true;
            this.label7.ForeColor = Color.FromArgb(0xc0, 0x40, 0);
            this.label7.Location = new Point(0x160, 0x1b7);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x106, 0x11);
            this.label7.TabIndex = 9;
            this.label7.Text = "2. 群里聚集了各个行业的朋友，行业经验交流。";
            this.label8.AutoSize = true;
            this.label8.ForeColor = Color.FromArgb(0xc0, 0x40, 0);
            this.label8.Location = new Point(0x20, 0x1c8);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0xe4, 0x11);
            this.label8.TabIndex = 10;
            this.label8.Text = "3. 探讨交流上位机开发，MES系统开发。";
            this.label9.AutoSize = true;
            this.label9.ForeColor = Color.FromArgb(0xc0, 0x40, 0);
            this.label9.Location = new Point(0x160, 0x1c8);
            this.label9.Name = "label9";
            this.label9.Size = new Size(310, 0x11);
            this.label9.TabIndex = 11;
            this.label9.Text = "4. 时不时会有项目需求发布，面向个人，外包或是公司。";
            this.label10.AutoSize = true;
            this.label10.ForeColor = Color.FromArgb(0xc0, 0x40, 0);
            this.label10.Location = new Point(0x20, 0x1d9);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x1f9, 0x11);
            this.label10.TabIndex = 12;
            this.label10.Text = "5. 免费使用HslControls控件库，demo参照 https://github.com/dathlin/HslControlsDemo";
            base.AutoScaleDimensions = new SizeF(7f, 17f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.AliceBlue;
            base.ClientSize = new Size(710, 0x21e);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.pictureBox2);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.pictureBox1);
            this.Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            base.Margin = new Padding(3, 4, 3, 4);
            base.Name = "FormSupport";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "开发不易，如果您觉得类库好用，并应用到了实际项目，感谢赞助";
            ((ISupportInitialize) this.pictureBox1).EndInit();
            ((ISupportInitialize) this.pictureBox2).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

