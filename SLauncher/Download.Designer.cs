namespace SLauncher
{
    partial class Download
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
            this.Console1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Console1
            // 
            this.Console1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Console1.Location = new System.Drawing.Point(41, 37);
            this.Console1.Name = "Console1";
            this.Console1.Size = new System.Drawing.Size(623, 42);
            this.Console1.TabIndex = 13;
            // 
            // progressBar1
            // 
            this.progressBar1.AccessibleName = "pb1";
            this.progressBar1.Location = new System.Drawing.Point(44, 136);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(620, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 12;
            this.progressBar1.Visible = false;
            // 
            // Download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = global::SLauncher.Properties.Resources.hq720;
            this.ClientSize = new System.Drawing.Size(730, 256);
            this.Controls.Add(this.Console1);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Download";
            this.Text = "Download";
            this.Load += new System.EventHandler(this.Download_Load);
            this.Enter += new System.EventHandler(this.Download_Enter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Console1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}