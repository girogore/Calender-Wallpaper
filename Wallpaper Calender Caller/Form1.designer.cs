namespace Wallpaper_Calender_Caller
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
            this.setBT = new System.Windows.Forms.Button();
            this.centeredRB = new System.Windows.Forms.RadioButton();
            this.tiledRB = new System.Windows.Forms.RadioButton();
            this.stretchedRB = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.browseFlBT = new System.Windows.Forms.Button();
            this.browseFldBT = new System.Windows.Forms.Button();
            this.clearBT = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.copyBT = new System.Windows.Forms.Button();
            this.pasteBT = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthCalendar1 = new Pabo.Calendar.MonthCalendar();
            this.setWallpaperBT = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // setBT
            // 
            this.setBT.Location = new System.Drawing.Point(238, 146);
            this.setBT.Name = "setBT";
            this.setBT.Size = new System.Drawing.Size(75, 23);
            this.setBT.TabIndex = 0;
            this.setBT.Text = "SET";
            this.setBT.UseVisualStyleBackColor = true;
            this.setBT.Click += new System.EventHandler(this.setBT_Click);
            // 
            // centeredRB
            // 
            this.centeredRB.AutoSize = true;
            this.centeredRB.Location = new System.Drawing.Point(6, 65);
            this.centeredRB.Name = "centeredRB";
            this.centeredRB.Size = new System.Drawing.Size(68, 17);
            this.centeredRB.TabIndex = 3;
            this.centeredRB.TabStop = true;
            this.centeredRB.Text = "Centered";
            this.centeredRB.UseVisualStyleBackColor = true;
            // 
            // tiledRB
            // 
            this.tiledRB.AutoSize = true;
            this.tiledRB.Location = new System.Drawing.Point(6, 42);
            this.tiledRB.Name = "tiledRB";
            this.tiledRB.Size = new System.Drawing.Size(48, 17);
            this.tiledRB.TabIndex = 3;
            this.tiledRB.TabStop = true;
            this.tiledRB.Text = "Tiled";
            this.tiledRB.UseVisualStyleBackColor = true;
            // 
            // stretchedRB
            // 
            this.stretchedRB.AutoSize = true;
            this.stretchedRB.Location = new System.Drawing.Point(6, 19);
            this.stretchedRB.Name = "stretchedRB";
            this.stretchedRB.Size = new System.Drawing.Size(71, 17);
            this.stretchedRB.TabIndex = 3;
            this.stretchedRB.TabStop = true;
            this.stretchedRB.Text = "Stretched";
            this.stretchedRB.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(239, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(349, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // browseFlBT
            // 
            this.browseFlBT.Location = new System.Drawing.Point(550, 50);
            this.browseFlBT.Name = "browseFlBT";
            this.browseFlBT.Size = new System.Drawing.Size(39, 20);
            this.browseFlBT.TabIndex = 5;
            this.browseFlBT.Text = "File";
            this.browseFlBT.UseVisualStyleBackColor = true;
            this.browseFlBT.Click += new System.EventHandler(this.browseBT_Click);
            // 
            // browseFldBT
            // 
            this.browseFldBT.Location = new System.Drawing.Point(496, 50);
            this.browseFldBT.Name = "browseFldBT";
            this.browseFldBT.Size = new System.Drawing.Size(48, 20);
            this.browseFldBT.TabIndex = 6;
            this.browseFldBT.Text = "Folder";
            this.browseFldBT.UseVisualStyleBackColor = true;
            this.browseFldBT.Click += new System.EventHandler(this.browseFldBT_Click);
            // 
            // clearBT
            // 
            this.clearBT.Location = new System.Drawing.Point(319, 146);
            this.clearBT.Name = "clearBT";
            this.clearBT.Size = new System.Drawing.Size(75, 23);
            this.clearBT.TabIndex = 7;
            this.clearBT.Text = "Clear";
            this.clearBT.UseVisualStyleBackColor = true;
            this.clearBT.Click += new System.EventHandler(this.clearBT_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.centeredRB);
            this.groupBox1.Controls.Add(this.stretchedRB);
            this.groupBox1.Controls.Add(this.tiledRB);
            this.groupBox1.Location = new System.Drawing.Point(239, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(104, 90);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wallpaper Style";
            // 
            // copyBT
            // 
            this.copyBT.Location = new System.Drawing.Point(349, 51);
            this.copyBT.Name = "copyBT";
            this.copyBT.Size = new System.Drawing.Size(75, 23);
            this.copyBT.TabIndex = 11;
            this.copyBT.Text = "Copy";
            this.copyBT.UseVisualStyleBackColor = true;
            this.copyBT.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteBT
            // 
            this.pasteBT.Enabled = false;
            this.pasteBT.Location = new System.Drawing.Point(349, 80);
            this.pasteBT.Name = "pasteBT";
            this.pasteBT.Size = new System.Drawing.Size(75, 23);
            this.pasteBT.TabIndex = 12;
            this.pasteBT.Text = "Paste";
            this.pasteBT.UseVisualStyleBackColor = true;
            this.pasteBT.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(593, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(118, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Enabled = false;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.ActiveMonth.Month = 6;
            this.monthCalendar1.ActiveMonth.Year = 2017;
            this.monthCalendar1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(198)))), ((int)(((byte)(214)))));
            this.monthCalendar1.Culture = new System.Globalization.CultureInfo("en-US");
            this.monthCalendar1.Footer.Align = Pabo.Calendar.mcTextAlign.Center;
            this.monthCalendar1.Footer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.monthCalendar1.Footer.ShowToday = false;
            this.monthCalendar1.Header.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.monthCalendar1.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.monthCalendar1.Header.ShowMonth = false;
            this.monthCalendar1.Header.TextColor = System.Drawing.Color.White;
            this.monthCalendar1.ImageList = null;
            this.monthCalendar1.Location = new System.Drawing.Point(0, 24);
            this.monthCalendar1.MaxDate = new System.DateTime(2017, 12, 31, 0, 0, 0, 0);
            this.monthCalendar1.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.monthCalendar1.Month.BackgroundImage = null;
            this.monthCalendar1.Month.Colors.Focus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.monthCalendar1.Month.Colors.Focus.Border = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(198)))), ((int)(((byte)(214)))));
            this.monthCalendar1.Month.Colors.Selected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(198)))), ((int)(((byte)(214)))));
            this.monthCalendar1.Month.Colors.Selected.Border = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(97)))), ((int)(((byte)(135)))));
            this.monthCalendar1.Month.DateFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.monthCalendar1.Month.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.SelectionMode = Pabo.Calendar.mcSelectionMode.One;
            this.monthCalendar1.ShowFocus = false;
            this.monthCalendar1.ShowFooter = false;
            this.monthCalendar1.ShowToday = false;
            this.monthCalendar1.Size = new System.Drawing.Size(232, 184);
            this.monthCalendar1.TabIndex = 14;
            this.monthCalendar1.Weekdays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.monthCalendar1.Weekdays.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.monthCalendar1.Weeknumbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.monthCalendar1.Weeknumbers.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.monthCalendar1.MonthChanged += new Pabo.Calendar.MonthChangedEventHandler(this.monthCalendar1_MonthChanged);
            this.monthCalendar1.DayClick += new Pabo.Calendar.DayClickEventHandler(this.monthCalendar1_DateChanged);
            // 
            // setWallpaperBT
            // 
            this.setWallpaperBT.Location = new System.Drawing.Point(496, 146);
            this.setWallpaperBT.Name = "setWallpaperBT";
            this.setWallpaperBT.Size = new System.Drawing.Size(92, 23);
            this.setWallpaperBT.TabIndex = 15;
            this.setWallpaperBT.Text = "Set Wallpaper";
            this.setWallpaperBT.UseVisualStyleBackColor = true;
            this.setWallpaperBT.Click += new System.EventHandler(this.setWallpaperBT_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(593, 213);
            this.Controls.Add(this.setWallpaperBT);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.pasteBT);
            this.Controls.Add(this.copyBT);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.clearBT);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.browseFlBT);
            this.Controls.Add(this.setBT);
            this.Controls.Add(this.browseFldBT);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Wallpaper Calender";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button setBT;
        private System.Windows.Forms.RadioButton centeredRB;
        private System.Windows.Forms.RadioButton tiledRB;
        private System.Windows.Forms.RadioButton stretchedRB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button browseFlBT;
        private System.Windows.Forms.Button browseFldBT;
        private System.Windows.Forms.Button clearBT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button copyBT;
        private System.Windows.Forms.Button pasteBT;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private Pabo.Calendar.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Button setWallpaperBT;
    }
}

