namespace CSharpTextEditor
{
    partial class PageSearchDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.pageIndexInput = new System.Windows.Forms.NumericUpDown();
            this.buttonSearchPage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pageSearchTextBox = new System.Windows.Forms.TextBox();
            this.buttonSearchTextGlobal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pageIndexInput)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Въведете номер на страница";
            // 
            // pageIndexInput
            // 
            this.pageIndexInput.Location = new System.Drawing.Point(30, 46);
            this.pageIndexInput.Name = "pageIndexInput";
            this.pageIndexInput.Size = new System.Drawing.Size(164, 20);
            this.pageIndexInput.TabIndex = 1;
            // 
            // buttonSearchPage
            // 
            this.buttonSearchPage.Location = new System.Drawing.Point(227, 43);
            this.buttonSearchPage.Name = "buttonSearchPage";
            this.buttonSearchPage.Size = new System.Drawing.Size(128, 23);
            this.buttonSearchPage.TabIndex = 2;
            this.buttonSearchPage.Text = "Отиди на страница";
            this.buttonSearchPage.UseVisualStyleBackColor = true;
            this.buttonSearchPage.Click += new System.EventHandler(this.PageSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Търсене на текст в страниците";
            // 
            // pageSearchTextBox
            // 
            this.pageSearchTextBox.Location = new System.Drawing.Point(30, 143);
            this.pageSearchTextBox.Multiline = true;
            this.pageSearchTextBox.Name = "pageSearchTextBox";
            this.pageSearchTextBox.Size = new System.Drawing.Size(164, 98);
            this.pageSearchTextBox.TabIndex = 4;
            // 
            // buttonSearchTextGlobal
            // 
            this.buttonSearchTextGlobal.Location = new System.Drawing.Point(227, 143);
            this.buttonSearchTextGlobal.Name = "buttonSearchTextGlobal";
            this.buttonSearchTextGlobal.Size = new System.Drawing.Size(128, 23);
            this.buttonSearchTextGlobal.TabIndex = 5;
            this.buttonSearchTextGlobal.Text = "Търсене текст";
            this.buttonSearchTextGlobal.UseVisualStyleBackColor = true;
            this.buttonSearchTextGlobal.Click += new System.EventHandler(this.ButtonSearchTextGlobal_Click);
            // 
            // PageSearchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 253);
            this.Controls.Add(this.buttonSearchTextGlobal);
            this.Controls.Add(this.pageSearchTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSearchPage);
            this.Controls.Add(this.pageIndexInput);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PageSearchDialog";
            this.Text = "Търсене";
            ((System.ComponentModel.ISupportInitialize)(this.pageIndexInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown pageIndexInput;
        private System.Windows.Forms.Button buttonSearchPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pageSearchTextBox;
        private System.Windows.Forms.Button buttonSearchTextGlobal;
    }
}