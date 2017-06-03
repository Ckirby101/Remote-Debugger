﻿namespace RemoteDebugger
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newRegisterViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDisassemblyViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLogViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newScreenViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proofOfConceptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.version00ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useLatestSourceCodeVersionOfZEsarUXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newRegisterViewToolStripMenuItem,
            this.newDisassemblyViewToolStripMenuItem,
            this.newLogViewToolStripMenuItem,
            this.newScreenViewToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "&Window";
            // 
            // newRegisterViewToolStripMenuItem
            // 
            this.newRegisterViewToolStripMenuItem.Name = "newRegisterViewToolStripMenuItem";
            this.newRegisterViewToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.newRegisterViewToolStripMenuItem.Text = "New &Register View";
            this.newRegisterViewToolStripMenuItem.Click += new System.EventHandler(this.newRegisterViewToolStripMenuItem_Click);
            // 
            // newDisassemblyViewToolStripMenuItem
            // 
            this.newDisassemblyViewToolStripMenuItem.Name = "newDisassemblyViewToolStripMenuItem";
            this.newDisassemblyViewToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.newDisassemblyViewToolStripMenuItem.Text = "New &Disassembly View";
            this.newDisassemblyViewToolStripMenuItem.Click += new System.EventHandler(this.newDisassemblyViewToolStripMenuItem_Click);
            // 
            // newLogViewToolStripMenuItem
            // 
            this.newLogViewToolStripMenuItem.Name = "newLogViewToolStripMenuItem";
            this.newLogViewToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.newLogViewToolStripMenuItem.Text = "New &Log View";
            this.newLogViewToolStripMenuItem.Click += new System.EventHandler(this.newLogViewToolStripMenuItem_Click);
            // 
            // newScreenViewToolStripMenuItem
            // 
            this.newScreenViewToolStripMenuItem.Name = "newScreenViewToolStripMenuItem";
            this.newScreenViewToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.newScreenViewToolStripMenuItem.Text = "New &Screen View";
            this.newScreenViewToolStripMenuItem.Click += new System.EventHandler(this.newScreenViewToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.proofOfConceptToolStripMenuItem,
            this.version00ToolStripMenuItem,
            this.useLatestSourceCodeVersionOfZEsarUXToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // proofOfConceptToolStripMenuItem
            // 
            this.proofOfConceptToolStripMenuItem.Enabled = false;
            this.proofOfConceptToolStripMenuItem.Name = "proofOfConceptToolStripMenuItem";
            this.proofOfConceptToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.proofOfConceptToolStripMenuItem.Text = "Proof Of Concept";
            // 
            // version00ToolStripMenuItem
            // 
            this.version00ToolStripMenuItem.Enabled = false;
            this.version00ToolStripMenuItem.Name = "version00ToolStripMenuItem";
            this.version00ToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.version00ToolStripMenuItem.Text = "Version 0.0";
            // 
            // useLatestSourceCodeVersionOfZEsarUXToolStripMenuItem
            // 
            this.useLatestSourceCodeVersionOfZEsarUXToolStripMenuItem.Enabled = false;
            this.useLatestSourceCodeVersionOfZEsarUXToolStripMenuItem.Name = "useLatestSourceCodeVersionOfZEsarUXToolStripMenuItem";
            this.useLatestSourceCodeVersionOfZEsarUXToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.useLatestSourceCodeVersionOfZEsarUXToolStripMenuItem.Text = "Use Latest Source Code Version Of ZEsarUX";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Remote Debugger";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newRegisterViewToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem newDisassemblyViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLogViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newScreenViewToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proofOfConceptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem version00ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useLatestSourceCodeVersionOfZEsarUXToolStripMenuItem;
    }
}
