namespace CSharpTextEditor
{
    partial class AboutDialog
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
            this.aboutInfo = new System.Windows.Forms.Label();
            this.iTextLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // aboutInfo
            // 
            this.aboutInfo.AutoSize = true;
            this.aboutInfo.Location = new System.Drawing.Point(32, 20);
            this.aboutInfo.Name = "aboutInfo";
            this.aboutInfo.Size = new System.Drawing.Size(290, 13);
            this.aboutInfo.TabIndex = 0;
            this.aboutInfo.Text = "Приложението използва следните външни библиотеки:";
            // 
            // iTextLink
            // 
            this.iTextLink.AutoSize = true;
            this.iTextLink.Location = new System.Drawing.Point(133, 60);
            this.iTextLink.Name = "iTextLink";
            this.iTextLink.Size = new System.Drawing.Size(57, 13);
            this.iTextLink.TabIndex = 1;
            this.iTextLink.TabStop = true;
            this.iTextLink.Text = "iText 7.2.3";
            this.iTextLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ITextLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Лицензиран под LGPL";
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 145);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iTextLink);
            this.Controls.Add(this.aboutInfo);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutDialog";
            this.Text = "Относно";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label aboutInfo;
        private System.Windows.Forms.LinkLabel iTextLink;
        private System.Windows.Forms.Label label1;
    }
}