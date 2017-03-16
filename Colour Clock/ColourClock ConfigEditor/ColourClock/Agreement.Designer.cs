namespace ColourClock
{
    partial class Agreement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Agreement));
            this.buttonAgree = new System.Windows.Forms.Button();
            this.buttonDisagree = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonAgree
            // 
            this.buttonAgree.Location = new System.Drawing.Point(408, 117);
            this.buttonAgree.Name = "buttonAgree";
            this.buttonAgree.Size = new System.Drawing.Size(75, 26);
            this.buttonAgree.TabIndex = 0;
            this.buttonAgree.Text = "&Agree";
            this.buttonAgree.UseVisualStyleBackColor = true;
            this.buttonAgree.Click += new System.EventHandler(this.ButtonAgreeClick);
            // 
            // buttonDisagree
            // 
            this.buttonDisagree.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDisagree.Location = new System.Drawing.Point(327, 117);
            this.buttonDisagree.Name = "buttonDisagree";
            this.buttonDisagree.Size = new System.Drawing.Size(75, 26);
            this.buttonDisagree.TabIndex = 1;
            this.buttonDisagree.Text = "&Disagree";
            this.buttonDisagree.UseVisualStyleBackColor = true;
            this.buttonDisagree.Click += new System.EventHandler(this.ButtonDisagreeClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "To continue you must agree to these terms:";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(15, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(481, 89);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // Agreement
            // 
            this.AcceptButton = this.buttonDisagree;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonDisagree;
            this.ClientSize = new System.Drawing.Size(492, 150);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDisagree);
            this.Controls.Add(this.buttonAgree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Agreement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agreement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAgree;
        private System.Windows.Forms.Button buttonDisagree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}