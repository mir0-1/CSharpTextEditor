namespace CSharpTextEditor
{
    partial class PageSettingsDialog
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
            this.headerCheckbox = new System.Windows.Forms.CheckBox();
            this.footerCheckbox = new System.Windows.Forms.CheckBox();
            this.headerHeightInput = new System.Windows.Forms.NumericUpDown();
            this.footerHeightInput = new System.Windows.Forms.NumericUpDown();
            this.bodyLabel = new System.Windows.Forms.Label();
            this.bodyHeightInput = new System.Windows.Forms.NumericUpDown();
            this.totalHeightGroupBox = new System.Windows.Forms.GroupBox();
            this.totalHeightLabel = new System.Windows.Forms.Label();
            this.pageWidthInput = new System.Windows.Forms.NumericUpDown();
            this.totalPageWidthGroupBox = new System.Windows.Forms.GroupBox();
            this.okButton = new System.Windows.Forms.Button();
            this.showBordersCheckbox = new System.Windows.Forms.CheckBox();
            this.marginsXLabel = new System.Windows.Forms.Label();
            this.marginsInputX = new System.Windows.Forms.NumericUpDown();
            this.marginsYLabel = new System.Windows.Forms.Label();
            this.marginsInputY = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.headerHeightInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.footerHeightInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bodyHeightInput)).BeginInit();
            this.totalHeightGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageWidthInput)).BeginInit();
            this.totalPageWidthGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marginsInputX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marginsInputY)).BeginInit();
            this.SuspendLayout();
            // 
            // headerCheckbox
            // 
            this.headerCheckbox.AutoSize = true;
            this.headerCheckbox.Checked = true;
            this.headerCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.headerCheckbox.Location = new System.Drawing.Point(28, 14);
            this.headerCheckbox.Name = "headerCheckbox";
            this.headerCheckbox.Size = new System.Drawing.Size(192, 17);
            this.headerCheckbox.TabIndex = 0;
            this.headerCheckbox.Text = "Горен колонтитул (дължина, mm)";
            this.headerCheckbox.UseVisualStyleBackColor = true;
            this.headerCheckbox.CheckedChanged += new System.EventHandler(this.HeaderCheckbox_CheckedChanged);
            // 
            // footerCheckbox
            // 
            this.footerCheckbox.AutoSize = true;
            this.footerCheckbox.Checked = true;
            this.footerCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.footerCheckbox.Location = new System.Drawing.Point(28, 54);
            this.footerCheckbox.Name = "footerCheckbox";
            this.footerCheckbox.Size = new System.Drawing.Size(195, 17);
            this.footerCheckbox.TabIndex = 1;
            this.footerCheckbox.Text = "Долен колонтитул (дължина, mm)";
            this.footerCheckbox.UseVisualStyleBackColor = true;
            this.footerCheckbox.CheckedChanged += new System.EventHandler(this.FooterCheckbox_CheckedChanged);
            // 
            // headerHeightInput
            // 
            this.headerHeightInput.Location = new System.Drawing.Point(228, 13);
            this.headerHeightInput.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.headerHeightInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.headerHeightInput.Name = "headerHeightInput";
            this.headerHeightInput.Size = new System.Drawing.Size(103, 20);
            this.headerHeightInput.TabIndex = 2;
            this.headerHeightInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // footerHeightInput
            // 
            this.footerHeightInput.Location = new System.Drawing.Point(228, 53);
            this.footerHeightInput.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.footerHeightInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.footerHeightInput.Name = "footerHeightInput";
            this.footerHeightInput.Size = new System.Drawing.Size(103, 20);
            this.footerHeightInput.TabIndex = 3;
            this.footerHeightInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bodyLabel
            // 
            this.bodyLabel.AutoSize = true;
            this.bodyLabel.Location = new System.Drawing.Point(25, 99);
            this.bodyLabel.Name = "bodyLabel";
            this.bodyLabel.Size = new System.Drawing.Size(108, 13);
            this.bodyLabel.TabIndex = 4;
            this.bodyLabel.Text = "Тяло (дължина, mm)";
            // 
            // bodyHeightInput
            // 
            this.bodyHeightInput.Location = new System.Drawing.Point(228, 92);
            this.bodyHeightInput.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.bodyHeightInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bodyHeightInput.Name = "bodyHeightInput";
            this.bodyHeightInput.Size = new System.Drawing.Size(103, 20);
            this.bodyHeightInput.TabIndex = 5;
            this.bodyHeightInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // totalHeightGroupBox
            // 
            this.totalHeightGroupBox.Controls.Add(this.totalHeightLabel);
            this.totalHeightGroupBox.Location = new System.Drawing.Point(356, 12);
            this.totalHeightGroupBox.Name = "totalHeightGroupBox";
            this.totalHeightGroupBox.Size = new System.Drawing.Size(155, 100);
            this.totalHeightGroupBox.TabIndex = 6;
            this.totalHeightGroupBox.TabStop = false;
            this.totalHeightGroupBox.Text = "Дължина без отстъп, mm";
            // 
            // totalHeightLabel
            // 
            this.totalHeightLabel.AutoSize = true;
            this.totalHeightLabel.Location = new System.Drawing.Point(67, 46);
            this.totalHeightLabel.Name = "totalHeightLabel";
            this.totalHeightLabel.Size = new System.Drawing.Size(0, 13);
            this.totalHeightLabel.TabIndex = 0;
            // 
            // pageWidthInput
            // 
            this.pageWidthInput.Location = new System.Drawing.Point(6, 29);
            this.pageWidthInput.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.pageWidthInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pageWidthInput.Name = "pageWidthInput";
            this.pageWidthInput.Size = new System.Drawing.Size(103, 20);
            this.pageWidthInput.TabIndex = 8;
            this.pageWidthInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // totalPageWidthGroupBox
            // 
            this.totalPageWidthGroupBox.Controls.Add(this.pageWidthInput);
            this.totalPageWidthGroupBox.Location = new System.Drawing.Point(356, 148);
            this.totalPageWidthGroupBox.Name = "totalPageWidthGroupBox";
            this.totalPageWidthGroupBox.Size = new System.Drawing.Size(155, 66);
            this.totalPageWidthGroupBox.TabIndex = 9;
            this.totalPageWidthGroupBox.TabStop = false;
            this.totalPageWidthGroupBox.Text = "Ширина без отстъп, mm";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(404, 228);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // showBordersCheckbox
            // 
            this.showBordersCheckbox.AutoSize = true;
            this.showBordersCheckbox.Location = new System.Drawing.Point(28, 232);
            this.showBordersCheckbox.Name = "showBordersCheckbox";
            this.showBordersCheckbox.Size = new System.Drawing.Size(156, 17);
            this.showBordersCheckbox.TabIndex = 11;
            this.showBordersCheckbox.Text = "Очертаване на границите";
            this.showBordersCheckbox.UseVisualStyleBackColor = true;
            // 
            // marginsXLabel
            // 
            this.marginsXLabel.AutoSize = true;
            this.marginsXLabel.Location = new System.Drawing.Point(25, 150);
            this.marginsXLabel.Name = "marginsXLabel";
            this.marginsXLabel.Size = new System.Drawing.Size(145, 13);
            this.marginsXLabel.TabIndex = 12;
            this.marginsXLabel.Text = "Хор. полета за отстъп (mm)";
            // 
            // marginsInputX
            // 
            this.marginsInputX.Location = new System.Drawing.Point(228, 148);
            this.marginsInputX.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.marginsInputX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.marginsInputX.Name = "marginsInputX";
            this.marginsInputX.Size = new System.Drawing.Size(103, 20);
            this.marginsInputX.TabIndex = 13;
            this.marginsInputX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // marginsYLabel
            // 
            this.marginsYLabel.AutoSize = true;
            this.marginsYLabel.Location = new System.Drawing.Point(25, 184);
            this.marginsYLabel.Name = "marginsYLabel";
            this.marginsYLabel.Size = new System.Drawing.Size(150, 13);
            this.marginsYLabel.TabIndex = 14;
            this.marginsYLabel.Text = "Верт. полета за отстъп (mm)";
            // 
            // marginsInputY
            // 
            this.marginsInputY.Location = new System.Drawing.Point(228, 182);
            this.marginsInputY.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.marginsInputY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.marginsInputY.Name = "marginsInputY";
            this.marginsInputY.Size = new System.Drawing.Size(103, 20);
            this.marginsInputY.TabIndex = 15;
            this.marginsInputY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PageSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 274);
            this.Controls.Add(this.marginsInputY);
            this.Controls.Add(this.marginsYLabel);
            this.Controls.Add(this.marginsInputX);
            this.Controls.Add(this.marginsXLabel);
            this.Controls.Add(this.showBordersCheckbox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.totalPageWidthGroupBox);
            this.Controls.Add(this.totalHeightGroupBox);
            this.Controls.Add(this.bodyHeightInput);
            this.Controls.Add(this.bodyLabel);
            this.Controls.Add(this.footerHeightInput);
            this.Controls.Add(this.headerHeightInput);
            this.Controls.Add(this.footerCheckbox);
            this.Controls.Add(this.headerCheckbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PageSettingsDialog";
            this.Text = "Настройки за страниците";
            this.Load += new System.EventHandler(this.PageSettingsDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.headerHeightInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.footerHeightInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bodyHeightInput)).EndInit();
            this.totalHeightGroupBox.ResumeLayout(false);
            this.totalHeightGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageWidthInput)).EndInit();
            this.totalPageWidthGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.marginsInputX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marginsInputY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox headerCheckbox;
        private System.Windows.Forms.CheckBox footerCheckbox;
        private System.Windows.Forms.NumericUpDown headerHeightInput;
        private System.Windows.Forms.NumericUpDown footerHeightInput;
        private System.Windows.Forms.Label bodyLabel;
        private System.Windows.Forms.NumericUpDown bodyHeightInput;
        private System.Windows.Forms.GroupBox totalHeightGroupBox;
        private System.Windows.Forms.Label totalHeightLabel;
        private System.Windows.Forms.NumericUpDown pageWidthInput;
        private System.Windows.Forms.GroupBox totalPageWidthGroupBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.CheckBox showBordersCheckbox;
        private System.Windows.Forms.Label marginsXLabel;
        private System.Windows.Forms.NumericUpDown marginsInputX;
        private System.Windows.Forms.Label marginsYLabel;
        private System.Windows.Forms.NumericUpDown marginsInputY;
    }
}