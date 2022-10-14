namespace CSharpTextEditor
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
            this.HtmlViewer = new System.Windows.Forms.WebBrowser();
            this.insertPageBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // HtmlViewer
            // 
            this.HtmlViewer.AllowNavigation = false;
            this.HtmlViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HtmlViewer.Location = new System.Drawing.Point(12, 56);
            this.HtmlViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.HtmlViewer.Name = "HtmlViewer";
            this.HtmlViewer.Size = new System.Drawing.Size(776, 382);
            this.HtmlViewer.TabIndex = 0;
            this.HtmlViewer.WebBrowserShortcutsEnabled = false;
            this.HtmlViewer.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.HtmlViewer_DocumentCompleted);
            // 
            // insertPageBtn
            // 
            this.insertPageBtn.Location = new System.Drawing.Point(617, 12);
            this.insertPageBtn.Name = "insertPageBtn";
            this.insertPageBtn.Size = new System.Drawing.Size(171, 23);
            this.insertPageBtn.TabIndex = 1;
            this.insertPageBtn.Text = "Вмъкни страница";
            this.insertPageBtn.UseVisualStyleBackColor = true;
            this.insertPageBtn.Click += new System.EventHandler(this.InsertPageBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.insertPageBtn);
            this.Controls.Add(this.HtmlViewer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DoubleClick += new System.EventHandler(this.Form1_DoubleClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser HtmlViewer;
        private System.Windows.Forms.Button insertPageBtn;
    }
}

