namespace ColourClock
{
    partial class ColourClock
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColourClock));
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.pictureBoxCol1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxCol4 = new System.Windows.Forms.PictureBox();
            this.pictureBoxCol3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxCol2 = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonQuit = new System.Windows.Forms.Label();
            this.buttonSetting = new System.Windows.Forms.Label();
            this.buttonSaver = new System.Windows.Forms.Label();
            this.mPictureBox1 = new MPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCol1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCol4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCol3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCol2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonExit.Location = new System.Drawing.Point(397, 19);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(25, 28);
            this.buttonExit.TabIndex = 0;
            this.buttonExit.Text = "X";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Visible = false;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExitClick);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Font = new System.Drawing.Font("Webdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonSettings.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonSettings.Location = new System.Drawing.Point(368, 19);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(25, 28);
            this.buttonSettings.TabIndex = 2;
            this.buttonSettings.Text = "@";
            this.buttonSettings.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Visible = false;
            this.buttonSettings.Click += new System.EventHandler(this.ButtonSettingsClick);
            // 
            // pictureBoxCol1
            // 
            this.pictureBoxCol1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxCol1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCol1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBoxCol1.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxCol1.Name = "pictureBoxCol1";
            this.pictureBoxCol1.Size = new System.Drawing.Size(80, 80);
            this.pictureBoxCol1.TabIndex = 3;
            this.pictureBoxCol1.TabStop = false;
            this.pictureBoxCol1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseDown);
            this.pictureBoxCol1.MouseLeave += new System.EventHandler(this.PictureBoxMouseLeave);
            this.pictureBoxCol1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseMove);
            this.pictureBoxCol1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseUp);
            // 
            // pictureBoxCol4
            // 
            this.pictureBoxCol4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxCol4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCol4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBoxCol4.Location = new System.Drawing.Point(102, 98);
            this.pictureBoxCol4.Name = "pictureBoxCol4";
            this.pictureBoxCol4.Size = new System.Drawing.Size(80, 80);
            this.pictureBoxCol4.TabIndex = 4;
            this.pictureBoxCol4.TabStop = false;
            this.pictureBoxCol4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseDown);
            this.pictureBoxCol4.MouseLeave += new System.EventHandler(this.PictureBoxMouseLeave);
            this.pictureBoxCol4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseMove);
            this.pictureBoxCol4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseUp);
            // 
            // pictureBoxCol3
            // 
            this.pictureBoxCol3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxCol3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCol3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBoxCol3.Location = new System.Drawing.Point(102, 12);
            this.pictureBoxCol3.Name = "pictureBoxCol3";
            this.pictureBoxCol3.Size = new System.Drawing.Size(80, 80);
            this.pictureBoxCol3.TabIndex = 5;
            this.pictureBoxCol3.TabStop = false;
            this.pictureBoxCol3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseDown);
            this.pictureBoxCol3.MouseLeave += new System.EventHandler(this.PictureBoxMouseLeave);
            this.pictureBoxCol3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseMove);
            this.pictureBoxCol3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseUp);
            // 
            // pictureBoxCol2
            // 
            this.pictureBoxCol2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxCol2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxCol2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBoxCol2.Location = new System.Drawing.Point(12, 98);
            this.pictureBoxCol2.Name = "pictureBoxCol2";
            this.pictureBoxCol2.Size = new System.Drawing.Size(80, 80);
            this.pictureBoxCol2.TabIndex = 6;
            this.pictureBoxCol2.TabStop = false;
            this.pictureBoxCol2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseDown);
            this.pictureBoxCol2.MouseLeave += new System.EventHandler(this.PictureBoxMouseLeave);
            this.pictureBoxCol2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseMove);
            this.pictureBoxCol2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseUp);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Webdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonSave.Location = new System.Drawing.Point(339, 19);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(25, 28);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Í";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Visible = false;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // buttonQuit
            // 
            this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuit.AutoSize = true;
            this.buttonQuit.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonQuit.ForeColor = System.Drawing.Color.White;
            this.buttonQuit.Location = new System.Drawing.Point(407, -1);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(21, 18);
            this.buttonQuit.TabIndex = 8;
            this.buttonQuit.Text = "r";
            this.buttonQuit.Click += new System.EventHandler(this.ButtonExitClick);
            this.buttonQuit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseDown);
            this.buttonQuit.MouseEnter += new System.EventHandler(this.ButtonMouseEnter);
            this.buttonQuit.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            this.buttonQuit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // buttonSetting
            // 
            this.buttonSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetting.AutoSize = true;
            this.buttonSetting.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonSetting.ForeColor = System.Drawing.Color.White;
            this.buttonSetting.Location = new System.Drawing.Point(392, -1);
            this.buttonSetting.Name = "buttonSetting";
            this.buttonSetting.Size = new System.Drawing.Size(21, 18);
            this.buttonSetting.TabIndex = 10;
            this.buttonSetting.Text = "@";
            this.buttonSetting.Click += new System.EventHandler(this.ButtonSettingsClick);
            this.buttonSetting.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseDown);
            this.buttonSetting.MouseEnter += new System.EventHandler(this.ButtonMouseEnter);
            this.buttonSetting.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            this.buttonSetting.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // buttonSaver
            // 
            this.buttonSaver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaver.AutoSize = true;
            this.buttonSaver.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonSaver.ForeColor = System.Drawing.Color.White;
            this.buttonSaver.Location = new System.Drawing.Point(377, 0);
            this.buttonSaver.Name = "buttonSaver";
            this.buttonSaver.Size = new System.Drawing.Size(21, 18);
            this.buttonSaver.TabIndex = 9;
            this.buttonSaver.Text = "Í";
            this.buttonSaver.Click += new System.EventHandler(this.ButtonSaveClick);
            this.buttonSaver.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseDown);
            this.buttonSaver.MouseEnter += new System.EventHandler(this.ButtonMouseEnter);
            this.buttonSaver.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            this.buttonSaver.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // mPictureBox1
            // 
            this.mPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.mPictureBox1.Location = new System.Drawing.Point(12, 11);
            this.mPictureBox1.Name = "mPictureBox1";
            this.mPictureBox1.Size = new System.Drawing.Size(100, 81);
            this.mPictureBox1.TabIndex = 11;
            this.mPictureBox1.TabStop = false;
            // 
            // ColourClock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(425, 210);
            this.Controls.Add(this.mPictureBox1);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.buttonSetting);
            this.Controls.Add(this.buttonSaver);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.pictureBoxCol2);
            this.Controls.Add(this.pictureBoxCol3);
            this.Controls.Add(this.pictureBoxCol4);
            this.Controls.Add(this.pictureBoxCol1);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColourClock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Colour Clock Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColourClockFormClosing);
            this.Load += new System.EventHandler(this.ColourClockLoad);
            this.ResizeEnd += new System.EventHandler(this.ColourClockResizeEnd);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColourClockPaint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColourClockMouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCol1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCol4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCol3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCol2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.PictureBox pictureBoxCol1;
        private System.Windows.Forms.PictureBox pictureBoxCol4;
        private System.Windows.Forms.PictureBox pictureBoxCol3;
        private System.Windows.Forms.PictureBox pictureBoxCol2;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label buttonQuit;
        private System.Windows.Forms.Label buttonSetting;
        private System.Windows.Forms.Label buttonSaver;
        private MPictureBox mPictureBox1;
    }
}

