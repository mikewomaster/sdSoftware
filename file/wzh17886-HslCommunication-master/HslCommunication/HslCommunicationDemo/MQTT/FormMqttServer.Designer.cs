﻿namespace HslCommunicationDemo
{
    partial class FormMqttServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
			this.panel1 = new System.Windows.Forms.Panel();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button7 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.button6 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.userControlHead1 = new HslCommunicationDemo.DemoControl.UserControlHead();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.checkBox3);
			this.panel1.Controls.Add(this.checkBox1);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Location = new System.Drawing.Point(15, 43);
			this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(978, 72);
			this.panel1.TabIndex = 7;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(11, 44);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(147, 21);
			this.checkBox3.TabIndex = 9;
			this.checkBox3.Text = "是否回发一条测试数据";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(492, 12);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(288, 21);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "启用用户名和密码(用户名 admin  密码: 123456)";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(353, 8);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(91, 28);
			this.button2.TabIndex = 5;
			this.button2.Text = "关闭服务";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(256, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(91, 28);
			this.button1.TabIndex = 4;
			this.button1.Text = "启动服务";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(62, 11);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(160, 23);
			this.textBox2.TabIndex = 3;
			this.textBox2.Text = "1883";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "端口号：";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.listBox1);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.button7);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.button6);
			this.panel2.Controls.Add(this.textBox1);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.button5);
			this.panel2.Controls.Add(this.textBox8);
			this.panel2.Controls.Add(this.label12);
			this.panel2.Controls.Add(this.button4);
			this.panel2.Controls.Add(this.button3);
			this.panel2.Controls.Add(this.textBox4);
			this.panel2.Controls.Add(this.label9);
			this.panel2.Controls.Add(this.label8);
			this.panel2.Controls.Add(this.textBox5);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Location = new System.Drawing.Point(15, 122);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(977, 518);
			this.panel2.TabIndex = 13;
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.BackColor = System.Drawing.Color.LightGray;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 17;
			this.listBox1.Location = new System.Drawing.Point(557, 36);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(397, 140);
			this.listBox1.TabIndex = 31;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(557, 10);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(93, 17);
			this.label5.TabIndex = 30;
			this.label5.Text = "Online Client：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(617, 186);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 17);
			this.label4.TabIndex = 29;
			this.label4.Text = "Receive Count:";
			// 
			// button7
			// 
			this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button7.Location = new System.Drawing.Point(753, 180);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(91, 28);
			this.button7.TabIndex = 28;
			this.button7.Text = "暂停";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(860, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 17);
			this.label2.TabIndex = 27;
			this.label2.Text = "Online Count:";
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(170, 180);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(91, 28);
			this.button6.TabIndex = 26;
			this.button6.Text = "广播订阅";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click_1);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(342, 7);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(209, 23);
			this.textBox1.TabIndex = 25;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(281, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 17);
			this.label1.TabIndex = 24;
			this.label1.Text = "Client Id：";
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(280, 180);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(164, 28);
			this.button5.TabIndex = 20;
			this.button5.Text = "广播指定id";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5_Click);
			// 
			// textBox8
			// 
			this.textBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox8.Location = new System.Drawing.Point(62, 214);
			this.textBox8.Multiline = true;
			this.textBox8.Name = "textBox8";
			this.textBox8.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox8.Size = new System.Drawing.Size(892, 292);
			this.textBox8.TabIndex = 18;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(8, 217);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(61, 17);
			this.label12.TabIndex = 19;
			this.label12.Text = "receive：";
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(863, 180);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(91, 28);
			this.button4.TabIndex = 17;
			this.button4.Text = "清空";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(62, 180);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(91, 28);
			this.button3.TabIndex = 12;
			this.button3.Text = "广播所有";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox4
			// 
			this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox4.Location = new System.Drawing.Point(62, 36);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(489, 138);
			this.textBox4.TabIndex = 8;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(8, 39);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(66, 17);
			this.label9.TabIndex = 11;
			this.label9.Text = "Payload：";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(219, 11);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(56, 17);
			this.label8.TabIndex = 10;
			this.label8.Text = "主题信息";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(62, 7);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(151, 23);
			this.textBox5.TabIndex = 9;
			this.textBox5.Text = "A";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 11);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(52, 17);
			this.label7.TabIndex = 7;
			this.label7.Text = "Topic：";
			// 
			// userControlHead1
			// 
			this.userControlHead1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.userControlHead1.Dock = System.Windows.Forms.DockStyle.Top;
			this.userControlHead1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.userControlHead1.HelpLink = "https://www.cnblogs.com/dathlin/p/12312952.html";
			this.userControlHead1.Location = new System.Drawing.Point(0, 0);
			this.userControlHead1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.userControlHead1.MinimumSize = new System.Drawing.Size(800, 32);
			this.userControlHead1.Name = "userControlHead1";
			this.userControlHead1.ProtocolInfo = "MQTT";
			this.userControlHead1.Size = new System.Drawing.Size(1004, 32);
			this.userControlHead1.TabIndex = 14;
			this.userControlHead1.SaveConnectEvent += new System.EventHandler<System.EventArgs>(this.userControlHead1_SaveConnectEvent_1);
			// 
			// FormMqttServer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(1004, 645);
			this.Controls.Add(this.userControlHead1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "FormMqttServer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MQTT 服务器";
			this.Load += new System.EventHandler(this.FormClient_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private DemoControl.UserControlHead userControlHead1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label5;
	}
}