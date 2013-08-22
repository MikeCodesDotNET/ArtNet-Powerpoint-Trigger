namespace arttrig
{
    partial class Main
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
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.chbxEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxNextVal = new System.Windows.Forms.TextBox();
            this.tbxHoldVal = new System.Windows.Forms.TextBox();
            this.tbxPrevVal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrev
            // 
            this.btnPrev.BackgroundImage = global::arttrig.Properties.Resources._1319647769_gtk_media_next_rtl;
            this.btnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrev.Enabled = false;
            this.btnPrev.Location = new System.Drawing.Point(16, 118);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(190, 42);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "Prev";
            this.btnPrev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = global::arttrig.Properties.Resources._1319647757_gtk_media_next_ltr;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(212, 118);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(190, 42);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // chbxEnabled
            // 
            this.chbxEnabled.AutoSize = true;
            this.chbxEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbxEnabled.Location = new System.Drawing.Point(132, 7);
            this.chbxEnabled.Name = "chbxEnabled";
            this.chbxEnabled.Size = new System.Drawing.Size(170, 22);
            this.chbxEnabled.TabIndex = 2;
            this.chbxEnabled.Text = "Enable ArtNet Control";
            this.chbxEnabled.UseVisualStyleBackColor = true;
            this.chbxEnabled.CheckedChanged += new System.EventHandler(this.chbxEnabled_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxNextVal);
            this.groupBox1.Controls.Add(this.tbxHoldVal);
            this.groupBox1.Controls.Add(this.tbxPrevVal);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(16, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 77);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ArtNet Data";
            // 
            // tbxNextVal
            // 
            this.tbxNextVal.Enabled = false;
            this.tbxNextVal.Location = new System.Drawing.Point(307, 45);
            this.tbxNextVal.Name = "tbxNextVal";
            this.tbxNextVal.Size = new System.Drawing.Size(65, 20);
            this.tbxNextVal.TabIndex = 7;
            this.tbxNextVal.Text = "255";
            this.tbxNextVal.TextChanged += new System.EventHandler(this.tbxNextVal_TextChanged);
            // 
            // tbxHoldVal
            // 
            this.tbxHoldVal.Enabled = false;
            this.tbxHoldVal.Location = new System.Drawing.Point(149, 45);
            this.tbxHoldVal.Name = "tbxHoldVal";
            this.tbxHoldVal.Size = new System.Drawing.Size(65, 20);
            this.tbxHoldVal.TabIndex = 6;
            this.tbxHoldVal.Text = "128";
            this.tbxHoldVal.TextChanged += new System.EventHandler(this.tbxHoldVal_TextChanged);
            // 
            // tbxPrevVal
            // 
            this.tbxPrevVal.Enabled = false;
            this.tbxPrevVal.Location = new System.Drawing.Point(9, 45);
            this.tbxPrevVal.Name = "tbxPrevVal";
            this.tbxPrevVal.Size = new System.Drawing.Size(65, 20);
            this.tbxPrevVal.TabIndex = 5;
            this.tbxPrevVal.Text = "0";
            this.tbxPrevVal.TextChanged += new System.EventHandler(this.tbxPrevVal_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(304, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Next Slide";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(146, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Reset Trig";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Previous Slide";
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(16, 166);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(390, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Hide Window";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 207);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(414, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel1.Text = "Status: ";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(118, 17);
            this.lblStatus.Text = "toolStripStatusLabel2";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 229);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chbxEnabled);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(430, 450);
            this.MinimumSize = new System.Drawing.Size(430, 267);
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "ArtNet Triggering ";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.CheckBox chbxEnabled;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.TextBox tbxNextVal;
        private System.Windows.Forms.TextBox tbxHoldVal;
        private System.Windows.Forms.TextBox tbxPrevVal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}