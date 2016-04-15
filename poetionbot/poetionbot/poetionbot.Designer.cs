﻿namespace poetionbot
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
            this.menuStrip.SuspendLayout();
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
            this.tmrRefresh.Interval = 1000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // mnuQuit
            // 
            this.mnuQuit.Name = "mnuQuit";
            this.mnuQuit.Size = new System.Drawing.Size(32, 19);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 12);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(260, 237);
            this.txtLog.TabIndex = 1;
            // 
            // poetionbot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
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
    }
}

