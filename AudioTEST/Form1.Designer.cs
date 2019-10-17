namespace AudioTEST
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.PlayBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.L_valueLable = new System.Windows.Forms.Label();
            this.L_resultLabel = new System.Windows.Forms.Label();
            this.channel_L_gb = new System.Windows.Forms.GroupBox();
            this.channel_R_gb = new System.Windows.Forms.GroupBox();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.R_valueLable = new System.Windows.Forms.Label();
            this.R_resultLabel = new System.Windows.Forms.Label();
            this.MIC_gb = new System.Windows.Forms.GroupBox();
            this.MIC_resultLabel = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.channel_L_gb.SuspendLayout();
            this.channel_R_gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.MIC_gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayBtn
            // 
            this.PlayBtn.Location = new System.Drawing.Point(521, 71);
            this.PlayBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.Size = new System.Drawing.Size(112, 45);
            this.PlayBtn.TabIndex = 0;
            this.PlayBtn.Text = "Play";
            this.PlayBtn.UseVisualStyleBackColor = true;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // L_valueLable
            // 
            this.L_valueLable.AutoSize = true;
            this.L_valueLable.Location = new System.Drawing.Point(34, 50);
            this.L_valueLable.Name = "L_valueLable";
            this.L_valueLable.Size = new System.Drawing.Size(60, 20);
            this.L_valueLable.TabIndex = 2;
            this.L_valueLable.Text = "value:0";
            // 
            // L_resultLabel
            // 
            this.L_resultLabel.AutoSize = true;
            this.L_resultLabel.Location = new System.Drawing.Point(210, 52);
            this.L_resultLabel.Name = "L_resultLabel";
            this.L_resultLabel.Size = new System.Drawing.Size(92, 20);
            this.L_resultLabel.TabIndex = 3;
            this.L_resultLabel.Text = "result:PASS";
            // 
            // channel_L_gb
            // 
            this.channel_L_gb.Controls.Add(this.L_valueLable);
            this.channel_L_gb.Controls.Add(this.L_resultLabel);
            this.channel_L_gb.Location = new System.Drawing.Point(51, 31);
            this.channel_L_gb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.channel_L_gb.Name = "channel_L_gb";
            this.channel_L_gb.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.channel_L_gb.Size = new System.Drawing.Size(406, 102);
            this.channel_L_gb.TabIndex = 4;
            this.channel_L_gb.TabStop = false;
            this.channel_L_gb.Text = "Channel L";
            // 
            // channel_R_gb
            // 
            this.channel_R_gb.Controls.Add(this.axWindowsMediaPlayer1);
            this.channel_R_gb.Controls.Add(this.R_valueLable);
            this.channel_R_gb.Controls.Add(this.R_resultLabel);
            this.channel_R_gb.Location = new System.Drawing.Point(51, 159);
            this.channel_R_gb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.channel_R_gb.Name = "channel_R_gb";
            this.channel_R_gb.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.channel_R_gb.Size = new System.Drawing.Size(406, 112);
            this.channel_R_gb.TabIndex = 5;
            this.channel_R_gb.TabStop = false;
            this.channel_R_gb.Text = "Channel R";
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(444, -15);
            this.axWindowsMediaPlayer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(97, 47);
            this.axWindowsMediaPlayer1.TabIndex = 7;
            // 
            // R_valueLable
            // 
            this.R_valueLable.AutoSize = true;
            this.R_valueLable.Location = new System.Drawing.Point(34, 48);
            this.R_valueLable.Name = "R_valueLable";
            this.R_valueLable.Size = new System.Drawing.Size(60, 20);
            this.R_valueLable.TabIndex = 2;
            this.R_valueLable.Text = "value:0";
            // 
            // R_resultLabel
            // 
            this.R_resultLabel.AutoSize = true;
            this.R_resultLabel.Location = new System.Drawing.Point(210, 48);
            this.R_resultLabel.Name = "R_resultLabel";
            this.R_resultLabel.Size = new System.Drawing.Size(92, 20);
            this.R_resultLabel.TabIndex = 3;
            this.R_resultLabel.Text = "result:PASS";
            // 
            // MIC_gb
            // 
            this.MIC_gb.Controls.Add(this.MIC_resultLabel);
            this.MIC_gb.Location = new System.Drawing.Point(51, 302);
            this.MIC_gb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MIC_gb.Name = "MIC_gb";
            this.MIC_gb.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MIC_gb.Size = new System.Drawing.Size(406, 96);
            this.MIC_gb.TabIndex = 6;
            this.MIC_gb.TabStop = false;
            this.MIC_gb.Text = "MIC Test";
            // 
            // MIC_resultLabel
            // 
            this.MIC_resultLabel.AutoSize = true;
            this.MIC_resultLabel.Location = new System.Drawing.Point(136, 48);
            this.MIC_resultLabel.Name = "MIC_resultLabel";
            this.MIC_resultLabel.Size = new System.Drawing.Size(92, 20);
            this.MIC_resultLabel.TabIndex = 3;
            this.MIC_resultLabel.Text = "result:PASS";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 436);
            this.Controls.Add(this.MIC_gb);
            this.Controls.Add(this.channel_R_gb);
            this.Controls.Add(this.channel_L_gb);
            this.Controls.Add(this.PlayBtn);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.channel_L_gb.ResumeLayout(false);
            this.channel_L_gb.PerformLayout();
            this.channel_R_gb.ResumeLayout(false);
            this.channel_R_gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.MIC_gb.ResumeLayout(false);
            this.MIC_gb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PlayBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label L_valueLable;
        private System.Windows.Forms.Label L_resultLabel;
        private System.Windows.Forms.GroupBox channel_L_gb;
        private System.Windows.Forms.GroupBox channel_R_gb;
        private System.Windows.Forms.Label R_valueLable;
        private System.Windows.Forms.Label R_resultLabel;
        private System.Windows.Forms.GroupBox MIC_gb;
        private System.Windows.Forms.Label MIC_resultLabel;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Timer timer2;
    }
}

