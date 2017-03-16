namespace ColourClock.GUI
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
            this.buttonQuit = new System.Windows.Forms.Label();
            this.buttonMin = new System.Windows.Forms.Label();
            this.buttonSettings = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.buttonQuit.TabIndex = 3;
            this.buttonQuit.Text = "r";
            this.buttonQuit.Click += new System.EventHandler(this.ButtonExitClick);
            this.buttonQuit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseDown);
            this.buttonQuit.MouseEnter += new System.EventHandler(this.ButtonMouseEnter);
            this.buttonQuit.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            this.buttonQuit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // buttonMin
            // 
            this.buttonMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMin.AutoSize = true;
            this.buttonMin.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonMin.ForeColor = System.Drawing.Color.White;
            this.buttonMin.Location = new System.Drawing.Point(378, -1);
            this.buttonMin.Name = "buttonMin";
            this.buttonMin.Size = new System.Drawing.Size(21, 18);
            this.buttonMin.TabIndex = 4;
            this.buttonMin.Text = "0";
            this.buttonMin.Click += new System.EventHandler(this.ButtonMinimiseClick);
            this.buttonMin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseDown);
            this.buttonMin.MouseEnter += new System.EventHandler(this.ButtonMouseEnter);
            this.buttonMin.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            this.buttonMin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSettings.AutoSize = true;
            this.buttonSettings.Font = new System.Drawing.Font("Webdings", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.buttonSettings.ForeColor = System.Drawing.Color.White;
            this.buttonSettings.Location = new System.Drawing.Point(392, -1);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(21, 18);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.Text = "@";
            this.buttonSettings.Click += new System.EventHandler(this.ButtonAboutClick);
            this.buttonSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseDown);
            this.buttonSettings.MouseEnter += new System.EventHandler(this.ButtonMouseEnter);
            this.buttonSettings.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            this.buttonSettings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonMouseUp);
            // 
            // ColourClock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(425, 210);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonMin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColourClock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Colour Clock";
            this.Load += new System.EventHandler(this.ColourClockLoad);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColourClockPaint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label buttonQuit;
        private System.Windows.Forms.Label buttonMin;
        private System.Windows.Forms.Label buttonSettings;
    }
}

