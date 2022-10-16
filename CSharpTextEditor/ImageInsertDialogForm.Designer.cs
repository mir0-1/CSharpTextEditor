namespace CSharpTextEditor
{
    partial class ImageInsertDialogForm
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
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.OpenFilePCBtn = new System.Windows.Forms.Button();
            this.imageWidthLabel = new System.Windows.Forms.Label();
            this.imageHeightLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.imageWidthInput = new System.Windows.Forms.NumericUpDown();
            this.imageHeightInput = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.imageWidthInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageHeightInput)).BeginInit();
            this.SuspendLayout();
            // 
            // urlTextBox
            // 
            this.urlTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.urlTextBox.Location = new System.Drawing.Point(43, 98);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(348, 29);
            this.urlTextBox.TabIndex = 0;
            this.urlTextBox.TextChanged += new System.EventHandler(this.UrlTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(94, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Въведете URL за изображение";
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(160, 225);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(81, 34);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "Приложи";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // OpenFilePCBtn
            // 
            this.OpenFilePCBtn.Location = new System.Drawing.Point(281, 225);
            this.OpenFilePCBtn.Name = "OpenFilePCBtn";
            this.OpenFilePCBtn.Size = new System.Drawing.Size(110, 34);
            this.OpenFilePCBtn.TabIndex = 3;
            this.OpenFilePCBtn.Text = "Преглед в компютъра...";
            this.OpenFilePCBtn.UseVisualStyleBackColor = true;
            this.OpenFilePCBtn.Click += new System.EventHandler(this.OpenFilePCBtn_Click);
            // 
            // imageWidthLabel
            // 
            this.imageWidthLabel.AutoSize = true;
            this.imageWidthLabel.Location = new System.Drawing.Point(40, 157);
            this.imageWidthLabel.Name = "imageWidthLabel";
            this.imageWidthLabel.Size = new System.Drawing.Size(71, 13);
            this.imageWidthLabel.TabIndex = 4;
            this.imageWidthLabel.Text = "Ширина (мм)";
            // 
            // imageHeightLabel
            // 
            this.imageHeightLabel.AutoSize = true;
            this.imageHeightLabel.Location = new System.Drawing.Point(40, 188);
            this.imageHeightLabel.Name = "imageHeightLabel";
            this.imageHeightLabel.Size = new System.Drawing.Size(80, 13);
            this.imageHeightLabel.TabIndex = 5;
            this.imageHeightLabel.Text = "Дължина (мм)";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(43, 225);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(77, 34);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // imageWidthInput
            // 
            this.imageWidthInput.Enabled = false;
            this.imageWidthInput.Location = new System.Drawing.Point(271, 150);
            this.imageWidthInput.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.imageWidthInput.Name = "imageWidthInput";
            this.imageWidthInput.Size = new System.Drawing.Size(120, 20);
            this.imageWidthInput.TabIndex = 9;
            // 
            // imageHeightInput
            // 
            this.imageHeightInput.Enabled = false;
            this.imageHeightInput.Location = new System.Drawing.Point(271, 181);
            this.imageHeightInput.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.imageHeightInput.Name = "imageHeightInput";
            this.imageHeightInput.Size = new System.Drawing.Size(120, 20);
            this.imageHeightInput.TabIndex = 10;
            // 
            // ImageInsertDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 331);
            this.Controls.Add(this.imageHeightInput);
            this.Controls.Add(this.imageWidthInput);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.imageHeightLabel);
            this.Controls.Add(this.imageWidthLabel);
            this.Controls.Add(this.OpenFilePCBtn);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.urlTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImageInsertDialogForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вмъкване на изображение";
            this.Load += new System.EventHandler(this.ImageInsertDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageWidthInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageHeightInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button OpenFilePCBtn;
        private System.Windows.Forms.Label imageWidthLabel;
        private System.Windows.Forms.Label imageHeightLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.NumericUpDown imageWidthInput;
        private System.Windows.Forms.NumericUpDown imageHeightInput;
    }
}