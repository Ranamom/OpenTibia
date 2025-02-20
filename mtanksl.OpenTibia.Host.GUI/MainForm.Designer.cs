﻿namespace mtanksl.OpenTibia.Host.GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            kickAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { serverToolStripMenuItem, commandToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(584, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // serverToolStripMenuItem
            // 
            serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { startToolStripMenuItem, restartToolStripMenuItem, stopToolStripMenuItem });
            serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            serverToolStripMenuItem.Text = "Server";
            // 
            // startToolStripMenuItem
            // 
            startToolStripMenuItem.Name = "startToolStripMenuItem";
            startToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            startToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            startToolStripMenuItem.Text = "Start";
            startToolStripMenuItem.Click += startToolStripMenuItem_Click;
            // 
            // restartToolStripMenuItem
            // 
            restartToolStripMenuItem.Enabled = false;
            restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            restartToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5;
            restartToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            restartToolStripMenuItem.Text = "Restart";
            restartToolStripMenuItem.Click += restartToolStripMenuItem_Click;
            // 
            // stopToolStripMenuItem
            // 
            stopToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            stopToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5;
            stopToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            stopToolStripMenuItem.Text = "Stop";
            stopToolStripMenuItem.Click += stopToolStripMenuItem_Click;
            // 
            // commandToolStripMenuItem
            // 
            commandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { kickAllToolStripMenuItem });
            commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            commandToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            commandToolStripMenuItem.Text = "Command";
            // 
            // kickAllToolStripMenuItem
            // 
            kickAllToolStripMenuItem.Enabled = false;
            kickAllToolStripMenuItem.Name = "kickAllToolStripMenuItem";
            kickAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            kickAllToolStripMenuItem.Text = "Kick all";
            kickAllToolStripMenuItem.Click += kickAllToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new System.Drawing.Point(0, 339);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(584, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            richTextBox1.Location = new System.Drawing.Point(0, 24);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new System.Drawing.Size(584, 315);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            richTextBox1.LinkClicked += richTextBox1_LinkClicked;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 361);
            Controls.Add(richTextBox1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new System.Drawing.Size(300, 300);
            Name = "MainForm";
            Text = "mtanksl's Open Tibia Server";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kickAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
    }
}