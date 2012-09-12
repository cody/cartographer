﻿namespace cartographer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.runMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.quitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.setAllToDifferenceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.setAllToOldMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.setAllToNewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showChangedMapsOnlyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllMapsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.regexHelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRegex = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenu,
            this.viewToolStripMenu,
            this.optionsToolStripMenu,
            this.helpToolStripMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenu
            // 
            this.fileToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runMenu,
            this.saveAllMenu,
            this.quitMenu});
            this.fileToolStripMenu.Name = "fileToolStripMenu";
            this.fileToolStripMenu.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenu.Text = "&File";
            // 
            // runMenu
            // 
            this.runMenu.Image = ((System.Drawing.Image)(resources.GetObject("runMenu.Image")));
            this.runMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runMenu.Name = "runMenu";
            this.runMenu.Size = new System.Drawing.Size(210, 22);
            this.runMenu.Text = "Run";
            // 
            // saveAllMenu
            // 
            this.saveAllMenu.Image = ((System.Drawing.Image)(resources.GetObject("saveAllMenu.Image")));
            this.saveAllMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAllMenu.Name = "saveAllMenu";
            this.saveAllMenu.Size = new System.Drawing.Size(210, 22);
            this.saveAllMenu.Text = "Save all changes to repo...";
            // 
            // quitMenu
            // 
            this.quitMenu.Name = "quitMenu";
            this.quitMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitMenu.Size = new System.Drawing.Size(210, 22);
            this.quitMenu.Text = "Quit";
            this.quitMenu.Click += new System.EventHandler(this.quitMenu_Click);
            // 
            // viewToolStripMenu
            // 
            this.viewToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAllToDifferenceMenu,
            this.setAllToOldMenu,
            this.setAllToNewMenu});
            this.viewToolStripMenu.Name = "viewToolStripMenu";
            this.viewToolStripMenu.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenu.Text = "&View";
            // 
            // setAllToDifferenceMenu
            // 
            this.setAllToDifferenceMenu.Name = "setAllToDifferenceMenu";
            this.setAllToDifferenceMenu.Size = new System.Drawing.Size(175, 22);
            this.setAllToDifferenceMenu.Text = "Set all to difference";
            // 
            // setAllToOldMenu
            // 
            this.setAllToOldMenu.Name = "setAllToOldMenu";
            this.setAllToOldMenu.Size = new System.Drawing.Size(175, 22);
            this.setAllToOldMenu.Text = "Set all to old";
            // 
            // setAllToNewMenu
            // 
            this.setAllToNewMenu.Name = "setAllToNewMenu";
            this.setAllToNewMenu.Size = new System.Drawing.Size(175, 22);
            this.setAllToNewMenu.Text = "Set all to new";
            // 
            // optionsToolStripMenu
            // 
            this.optionsToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showChangedMapsOnlyMenu,
            this.showAllMapsMenu});
            this.optionsToolStripMenu.Name = "optionsToolStripMenu";
            this.optionsToolStripMenu.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenu.Text = "&Options";
            // 
            // showChangedMapsOnlyMenu
            // 
            this.showChangedMapsOnlyMenu.Checked = true;
            this.showChangedMapsOnlyMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showChangedMapsOnlyMenu.Name = "showChangedMapsOnlyMenu";
            this.showChangedMapsOnlyMenu.Size = new System.Drawing.Size(210, 22);
            this.showChangedMapsOnlyMenu.Text = "Show changed maps only";
            this.showChangedMapsOnlyMenu.Click += new System.EventHandler(this.optionMenu_Click);
            // 
            // showAllMapsMenu
            // 
            this.showAllMapsMenu.Name = "showAllMapsMenu";
            this.showAllMapsMenu.Size = new System.Drawing.Size(210, 22);
            this.showAllMapsMenu.Text = "Show all maps";
            this.showAllMapsMenu.Click += new System.EventHandler(this.optionMenu_Click);
            // 
            // helpToolStripMenu
            // 
            this.helpToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regexHelpMenu,
            this.aboutMenu});
            this.helpToolStripMenu.Name = "helpToolStripMenu";
            this.helpToolStripMenu.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenu.Text = "&Help";
            // 
            // regexHelpMenu
            // 
            this.regexHelpMenu.Name = "regexHelpMenu";
            this.regexHelpMenu.Size = new System.Drawing.Size(333, 22);
            this.regexHelpMenu.Text = "Regular Expression Language - Quick Reference...";
            this.regexHelpMenu.Click += new System.EventHandler(this.helpRegexMenu_Click);
            // 
            // aboutMenu
            // 
            this.aboutMenu.Name = "aboutMenu";
            this.aboutMenu.Size = new System.Drawing.Size(333, 22);
            this.aboutMenu.Text = "About...";
            this.aboutMenu.Click += new System.EventHandler(this.aboutMenu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Regex for map names:";
            // 
            // textBoxRegex
            // 
            this.textBoxRegex.Location = new System.Drawing.Point(380, 8);
            this.textBoxRegex.MaxLength = 1000;
            this.textBoxRegex.Name = "textBoxRegex";
            this.textBoxRegex.Size = new System.Drawing.Size(144, 20);
            this.textBoxRegex.TabIndex = 2;
            this.textBoxRegex.Text = "001-";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(530, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(576, 726);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(568, 700);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxLog
            // 
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(3, 3);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(562, 694);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.TabStop = false;
            this.textBoxLog.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(568, 700);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Minimaps";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(562, 694);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 762);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxRegex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "Form1";
            this.Text = "Cartographer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem runMenu;
        private System.Windows.Forms.ToolStripMenuItem saveAllMenu;
        private System.Windows.Forms.ToolStripMenuItem quitMenu;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem setAllToDifferenceMenu;
        private System.Windows.Forms.ToolStripMenuItem setAllToOldMenu;
        private System.Windows.Forms.ToolStripMenuItem setAllToNewMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem showChangedMapsOnlyMenu;
        private System.Windows.Forms.ToolStripMenuItem showAllMapsMenu;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem regexHelpMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRegex;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}

