namespace Name_Localizer
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
            this.pathBox = new System.Windows.Forms.TextBox();
            this.localizeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pathBox
            // 
            this.pathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathBox.Location = new System.Drawing.Point(12, 12);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(360, 20);
            this.pathBox.TabIndex = 0;
            // 
            // localizeButton
            // 
            this.localizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.localizeButton.Location = new System.Drawing.Point(12, 38);
            this.localizeButton.Name = "localizeButton";
            this.localizeButton.Size = new System.Drawing.Size(360, 36);
            this.localizeButton.TabIndex = 1;
            this.localizeButton.Text = "Localize";
            this.localizeButton.UseVisualStyleBackColor = true;
            this.localizeButton.Click += new System.EventHandler(this.localizeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 86);
            this.Controls.Add(this.localizeButton);
            this.Controls.Add(this.pathBox);
            this.MaximumSize = new System.Drawing.Size(750, 125);
            this.MinimumSize = new System.Drawing.Size(400, 125);
            this.Name = "Form1";
            this.Text = "Name Localizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.Button localizeButton;
    }
}

