namespace poetionbot
{
    partial class poetionbot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(poetionbot));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.attachToPathOfExileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAttach = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.mnuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tmrGreenReset = new System.Windows.Forms.Timer(this.components);
            this.tck1 = new System.Windows.Forms.TrackBar();
            this.lblTck1 = new System.Windows.Forms.Label();
            this.lblTck2 = new System.Windows.Forms.Label();
            this.tck2 = new System.Windows.Forms.TrackBar();
            this.lblTck3 = new System.Windows.Forms.Label();
            this.tck3 = new System.Windows.Forms.TrackBar();
            this.lblTck4 = new System.Windows.Forms.Label();
            this.tck4 = new System.Windows.Forms.TrackBar();
            this.lblTck5 = new System.Windows.Forms.Label();
            this.tck5 = new System.Windows.Forms.TrackBar();
            this.chkHP1 = new System.Windows.Forms.CheckBox();
            this.chkHP2 = new System.Windows.Forms.CheckBox();
            this.chkHP3 = new System.Windows.Forms.CheckBox();
            this.chkHP4 = new System.Windows.Forms.CheckBox();
            this.chkHP5 = new System.Windows.Forms.CheckBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tck1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tck2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tck3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tck4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tck5)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.menuStrip;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "poetionbot";
            this.notifyIcon1.Visible = true;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings,
            this.attachToPathOfExileToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(191, 70);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(190, 22);
            this.mnuSettings.Text = "Settings";
            this.mnuSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // attachToPathOfExileToolStripMenuItem
            // 
            this.attachToPathOfExileToolStripMenuItem.Name = "attachToPathOfExileToolStripMenuItem";
            this.attachToPathOfExileToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.attachToPathOfExileToolStripMenuItem.Text = "Attach to Path of Exile";
            this.attachToPathOfExileToolStripMenuItem.Click += new System.EventHandler(this.attachToPathOfExileToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // mnuAttach
            // 
            this.mnuAttach.Name = "mnuAttach";
            this.mnuAttach.Size = new System.Drawing.Size(32, 19);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 250;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // mnuQuit
            // 
            this.mnuQuit.Name = "mnuQuit";
            this.mnuQuit.Size = new System.Drawing.Size(32, 19);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 135);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(249, 114);
            this.txtLog.TabIndex = 1;
            // 
            // tmrGreenReset
            // 
            this.tmrGreenReset.Interval = 10000;
            this.tmrGreenReset.Tick += new System.EventHandler(this.tmrGreenReset_Tick);
            // 
            // tck1
            // 
            this.tck1.Location = new System.Drawing.Point(12, 12);
            this.tck1.Maximum = 99;
            this.tck1.Minimum = 1;
            this.tck1.Name = "tck1";
            this.tck1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tck1.Size = new System.Drawing.Size(45, 79);
            this.tck1.TabIndex = 2;
            this.tck1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tck1.Value = 1;
            this.tck1.Scroll += new System.EventHandler(this.tck1_Scroll);
            this.tck1.ValueChanged += new System.EventHandler(this.tck1_ValueChanged);
            // 
            // lblTck1
            // 
            this.lblTck1.AutoSize = true;
            this.lblTck1.Location = new System.Drawing.Point(9, 94);
            this.lblTck1.Name = "lblTck1";
            this.lblTck1.Size = new System.Drawing.Size(48, 13);
            this.lblTck1.TabIndex = 3;
            this.lblTck1.Text = "1 - 100%";
            this.lblTck1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTck2
            // 
            this.lblTck2.AutoSize = true;
            this.lblTck2.Location = new System.Drawing.Point(60, 94);
            this.lblTck2.Name = "lblTck2";
            this.lblTck2.Size = new System.Drawing.Size(48, 13);
            this.lblTck2.TabIndex = 5;
            this.lblTck2.Text = "1 - 100%";
            this.lblTck2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tck2
            // 
            this.tck2.Location = new System.Drawing.Point(63, 12);
            this.tck2.Maximum = 99;
            this.tck2.Minimum = 1;
            this.tck2.Name = "tck2";
            this.tck2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tck2.Size = new System.Drawing.Size(45, 79);
            this.tck2.TabIndex = 4;
            this.tck2.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tck2.Value = 1;
            this.tck2.ValueChanged += new System.EventHandler(this.tck2_ValueChanged);
            // 
            // lblTck3
            // 
            this.lblTck3.AutoSize = true;
            this.lblTck3.Location = new System.Drawing.Point(111, 94);
            this.lblTck3.Name = "lblTck3";
            this.lblTck3.Size = new System.Drawing.Size(48, 13);
            this.lblTck3.TabIndex = 7;
            this.lblTck3.Text = "1 - 100%";
            this.lblTck3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tck3
            // 
            this.tck3.Location = new System.Drawing.Point(114, 12);
            this.tck3.Maximum = 99;
            this.tck3.Minimum = 1;
            this.tck3.Name = "tck3";
            this.tck3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tck3.Size = new System.Drawing.Size(45, 79);
            this.tck3.TabIndex = 6;
            this.tck3.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tck3.Value = 1;
            this.tck3.ValueChanged += new System.EventHandler(this.tck3_ValueChanged);
            // 
            // lblTck4
            // 
            this.lblTck4.AutoSize = true;
            this.lblTck4.Location = new System.Drawing.Point(162, 94);
            this.lblTck4.Name = "lblTck4";
            this.lblTck4.Size = new System.Drawing.Size(48, 13);
            this.lblTck4.TabIndex = 9;
            this.lblTck4.Text = "1 - 100%";
            this.lblTck4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tck4
            // 
            this.tck4.Location = new System.Drawing.Point(165, 12);
            this.tck4.Maximum = 99;
            this.tck4.Minimum = 1;
            this.tck4.Name = "tck4";
            this.tck4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tck4.Size = new System.Drawing.Size(45, 79);
            this.tck4.TabIndex = 8;
            this.tck4.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tck4.Value = 1;
            this.tck4.Scroll += new System.EventHandler(this.tck4_Scroll);
            this.tck4.ValueChanged += new System.EventHandler(this.tck4_ValueChanged);
            // 
            // lblTck5
            // 
            this.lblTck5.AutoSize = true;
            this.lblTck5.Location = new System.Drawing.Point(213, 94);
            this.lblTck5.Name = "lblTck5";
            this.lblTck5.Size = new System.Drawing.Size(48, 13);
            this.lblTck5.TabIndex = 11;
            this.lblTck5.Text = "1 - 100%";
            this.lblTck5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tck5
            // 
            this.tck5.Location = new System.Drawing.Point(216, 12);
            this.tck5.Maximum = 99;
            this.tck5.Minimum = 1;
            this.tck5.Name = "tck5";
            this.tck5.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tck5.Size = new System.Drawing.Size(45, 79);
            this.tck5.TabIndex = 10;
            this.tck5.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tck5.Value = 1;
            this.tck5.ValueChanged += new System.EventHandler(this.tck5_ValueChanged);
            // 
            // chkHP1
            // 
            this.chkHP1.AutoSize = true;
            this.chkHP1.Location = new System.Drawing.Point(12, 110);
            this.chkHP1.Name = "chkHP1";
            this.chkHP1.Size = new System.Drawing.Size(41, 17);
            this.chkHP1.TabIndex = 12;
            this.chkHP1.Text = "HP";
            this.chkHP1.UseVisualStyleBackColor = true;
            this.chkHP1.CheckedChanged += new System.EventHandler(this.chkHP1_CheckedChanged);
            // 
            // chkHP2
            // 
            this.chkHP2.AutoSize = true;
            this.chkHP2.Location = new System.Drawing.Point(63, 110);
            this.chkHP2.Name = "chkHP2";
            this.chkHP2.Size = new System.Drawing.Size(41, 17);
            this.chkHP2.TabIndex = 13;
            this.chkHP2.Text = "HP";
            this.chkHP2.UseVisualStyleBackColor = true;
            this.chkHP2.CheckedChanged += new System.EventHandler(this.chkHP2_CheckedChanged);
            // 
            // chkHP3
            // 
            this.chkHP3.AutoSize = true;
            this.chkHP3.Location = new System.Drawing.Point(114, 110);
            this.chkHP3.Name = "chkHP3";
            this.chkHP3.Size = new System.Drawing.Size(41, 17);
            this.chkHP3.TabIndex = 14;
            this.chkHP3.Text = "HP";
            this.chkHP3.UseVisualStyleBackColor = true;
            this.chkHP3.CheckedChanged += new System.EventHandler(this.chkHP3_CheckedChanged);
            // 
            // chkHP4
            // 
            this.chkHP4.AutoSize = true;
            this.chkHP4.Location = new System.Drawing.Point(165, 110);
            this.chkHP4.Name = "chkHP4";
            this.chkHP4.Size = new System.Drawing.Size(41, 17);
            this.chkHP4.TabIndex = 15;
            this.chkHP4.Text = "HP";
            this.chkHP4.UseVisualStyleBackColor = true;
            this.chkHP4.CheckedChanged += new System.EventHandler(this.chkHP4_CheckedChanged);
            // 
            // chkHP5
            // 
            this.chkHP5.AutoSize = true;
            this.chkHP5.Location = new System.Drawing.Point(216, 110);
            this.chkHP5.Name = "chkHP5";
            this.chkHP5.Size = new System.Drawing.Size(41, 17);
            this.chkHP5.TabIndex = 16;
            this.chkHP5.Text = "HP";
            this.chkHP5.UseVisualStyleBackColor = true;
            this.chkHP5.CheckedChanged += new System.EventHandler(this.chkHP5_CheckedChanged);
            // 
            // poetionbot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 260);
            this.Controls.Add(this.chkHP5);
            this.Controls.Add(this.chkHP4);
            this.Controls.Add(this.chkHP3);
            this.Controls.Add(this.chkHP2);
            this.Controls.Add(this.chkHP1);
            this.Controls.Add(this.lblTck5);
            this.Controls.Add(this.tck5);
            this.Controls.Add(this.lblTck4);
            this.Controls.Add(this.tck4);
            this.Controls.Add(this.lblTck3);
            this.Controls.Add(this.tck3);
            this.Controls.Add(this.lblTck2);
            this.Controls.Add(this.tck2);
            this.Controls.Add(this.lblTck1);
            this.Controls.Add(this.tck1);
            this.Controls.Add(this.txtLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "poetionbot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "poetionbot";
            this.Activated += new System.EventHandler(this.poetionbot_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.poetionbot_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.poetionbot_FormClosed);
            this.Load += new System.EventHandler(this.poetionbot_Load);
            this.menuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tck1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tck2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tck3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tck4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tck5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem mnuAttach;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.ToolStripMenuItem mnuQuit;
        private System.Windows.Forms.ContextMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem attachToPathOfExileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Timer tmrGreenReset;
        private System.Windows.Forms.TrackBar tck1;
        private System.Windows.Forms.Label lblTck1;
        private System.Windows.Forms.Label lblTck2;
        private System.Windows.Forms.TrackBar tck2;
        private System.Windows.Forms.Label lblTck3;
        private System.Windows.Forms.TrackBar tck3;
        private System.Windows.Forms.Label lblTck4;
        private System.Windows.Forms.TrackBar tck4;
        private System.Windows.Forms.Label lblTck5;
        private System.Windows.Forms.TrackBar tck5;
        private System.Windows.Forms.CheckBox chkHP1;
        private System.Windows.Forms.CheckBox chkHP2;
        private System.Windows.Forms.CheckBox chkHP3;
        private System.Windows.Forms.CheckBox chkHP4;
        private System.Windows.Forms.CheckBox chkHP5;
    }
}

