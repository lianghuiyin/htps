namespace Printer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiServerConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDataBaseConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiContactUs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAboutSoft = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripBottom = new System.Windows.Forms.StatusStrip();
            this.lbStatusBottom = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbTipInfo = new System.Windows.Forms.RichTextBox();
            this.timerPrint = new System.Windows.Forms.Timer(this.components);
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnImportContent = new System.Windows.Forms.Button();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.statusStripBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiServerConfig,
            this.tsmiHelp});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(791, 24);
            this.mainMenuStrip.TabIndex = 6;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // tsmiServerConfig
            // 
            this.tsmiServerConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDataBaseConfig});
            this.tsmiServerConfig.Name = "tsmiServerConfig";
            this.tsmiServerConfig.Size = new System.Drawing.Size(65, 20);
            this.tsmiServerConfig.Text = "服务设置";
            // 
            // tsmiDataBaseConfig
            // 
            this.tsmiDataBaseConfig.Name = "tsmiDataBaseConfig";
            this.tsmiDataBaseConfig.Size = new System.Drawing.Size(154, 22);
            this.tsmiDataBaseConfig.Text = "数据库连接设置";
            this.tsmiDataBaseConfig.Click += new System.EventHandler(this.tsmiDataBaseConfig_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiContactUs,
            this.tsmiAboutSoft});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(41, 20);
            this.tsmiHelp.Text = "帮助";
            // 
            // tsmiContactUs
            // 
            this.tsmiContactUs.Name = "tsmiContactUs";
            this.tsmiContactUs.Size = new System.Drawing.Size(280, 22);
            this.tsmiContactUs.Text = "联系我们";
            this.tsmiContactUs.Click += new System.EventHandler(this.tsmiContactUs_Click);
            // 
            // tsmiAboutSoft
            // 
            this.tsmiAboutSoft.Name = "tsmiAboutSoft";
            this.tsmiAboutSoft.Size = new System.Drawing.Size(280, 22);
            this.tsmiAboutSoft.Text = "关于M&&T HORIZON试件信息系统打印助手";
            this.tsmiAboutSoft.Click += new System.EventHandler(this.tsmiAboutSoft_Click);
            // 
            // statusStripBottom
            // 
            this.statusStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatusBottom});
            this.statusStripBottom.Location = new System.Drawing.Point(0, 659);
            this.statusStripBottom.Name = "statusStripBottom";
            this.statusStripBottom.Size = new System.Drawing.Size(791, 22);
            this.statusStripBottom.TabIndex = 9;
            this.statusStripBottom.Text = "statusStrip1";
            // 
            // lbStatusBottom
            // 
            this.lbStatusBottom.Name = "lbStatusBottom";
            this.lbStatusBottom.Size = new System.Drawing.Size(65, 17);
            this.lbStatusBottom.Text = "服务未启动";
            // 
            // timerMain
            // 
            this.timerMain.Interval = 15000;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(522, 638);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "上海好耐电子科技有限公司 www.mthorizon.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(18)))), ((int)(((byte)(241)))));
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 22);
            this.label2.TabIndex = 11;
            this.label2.Text = "M&&T HORIZON";
            // 
            // rtbTipInfo
            // 
            this.rtbTipInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbTipInfo.BackColor = System.Drawing.SystemColors.Info;
            this.rtbTipInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbTipInfo.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbTipInfo.ForeColor = System.Drawing.Color.Black;
            this.rtbTipInfo.Location = new System.Drawing.Point(0, 542);
            this.rtbTipInfo.Name = "rtbTipInfo";
            this.rtbTipInfo.ReadOnly = true;
            this.rtbTipInfo.Size = new System.Drawing.Size(791, 93);
            this.rtbTipInfo.TabIndex = 12;
            this.rtbTipInfo.Text = "";
            // 
            // timerPrint
            // 
            this.timerPrint.Interval = 3000;
            // 
            // txtContent
            // 
            this.txtContent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtContent.Location = new System.Drawing.Point(17, 68);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ReadOnly = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(384, 408);
            this.txtContent.TabIndex = 14;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrint.Location = new System.Drawing.Point(592, 492);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "打印大尺寸";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnImportContent
            // 
            this.btnImportContent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnImportContent.Location = new System.Drawing.Point(17, 492);
            this.btnImportContent.Name = "btnImportContent";
            this.btnImportContent.Size = new System.Drawing.Size(108, 23);
            this.btnImportContent.TabIndex = 17;
            this.btnImportContent.Text = "导入试件内容";
            this.btnImportContent.UseVisualStyleBackColor = true;
            this.btnImportContent.Click += new System.EventHandler(this.btnImportContent_Click);
            // 
            // nudCount
            // 
            this.nudCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.nudCount.Location = new System.Drawing.Point(493, 495);
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(59, 21);
            this.nudCount.TabIndex = 19;
            this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 497);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "打印个数：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(424, 68);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(300, 300);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(355, 408);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(173, 492);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "生成大二维码";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Location = new System.Drawing.Point(290, 492);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "生成小二维码";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(691, 491);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "打印小尺寸";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 681);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudCount);
            this.Controls.Add(this.btnImportContent);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.rtbTipInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStripBottom);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试件信息系统打印助手";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStripBottom.ResumeLayout(false);
            this.statusStripBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiServerConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiDataBaseConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiContactUs;
        private System.Windows.Forms.ToolStripMenuItem tsmiAboutSoft;
        private System.Windows.Forms.StatusStrip statusStripBottom;
        private System.Windows.Forms.ToolStripStatusLabel lbStatusBottom;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rtbTipInfo;
        private System.Windows.Forms.Timer timerPrint;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnImportContent;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

