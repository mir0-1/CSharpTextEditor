﻿namespace CSharpTextEditor
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.HtmlViewer = new System.Windows.Forms.WebBrowser();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.insertPageBtn = new System.Windows.Forms.Button();
            this.pageSettingsButton = new System.Windows.Forms.Button();
            this.deletePageBtn = new System.Windows.Forms.Button();
            this.pageSearchBtn = new System.Windows.Forms.Button();
            this.generalToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.insertImageBtn = new System.Windows.Forms.Button();
            this.fontDialogBtn = new System.Windows.Forms.Button();
            this.overflowPageBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HtmlViewer
            // 
            this.HtmlViewer.AllowNavigation = false;
            this.HtmlViewer.AllowWebBrowserDrop = false;
            this.HtmlViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HtmlViewer.Location = new System.Drawing.Point(12, 110);
            this.HtmlViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.HtmlViewer.Name = "HtmlViewer";
            this.HtmlViewer.Size = new System.Drawing.Size(776, 353);
            this.HtmlViewer.TabIndex = 0;
            this.HtmlViewer.WebBrowserShortcutsEnabled = false;
            this.HtmlViewer.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.HtmlViewer_DocumentCompleted);
            this.HtmlViewer.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.HtmlViewer_PreviewKeyDown);
            // 
            // pagePanel
            // 
            this.pagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pagePanel.Controls.Add(this.insertPageBtn);
            this.pagePanel.Controls.Add(this.pageSettingsButton);
            this.pagePanel.Controls.Add(this.deletePageBtn);
            this.pagePanel.Controls.Add(this.pageSearchBtn);
            this.pagePanel.Location = new System.Drawing.Point(359, 27);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(309, 74);
            this.pagePanel.TabIndex = 6;
            // 
            // insertPageBtn
            // 
            this.insertPageBtn.BackgroundImage = global::CSharpTextEditor.Properties.Resources.add_page;
            this.insertPageBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.insertPageBtn.Location = new System.Drawing.Point(3, 3);
            this.insertPageBtn.Name = "insertPageBtn";
            this.insertPageBtn.Size = new System.Drawing.Size(71, 71);
            this.insertPageBtn.TabIndex = 1;
            this.generalToolTip.SetToolTip(this.insertPageBtn, "Вмъкване на страница след активната");
            this.insertPageBtn.UseVisualStyleBackColor = true;
            this.insertPageBtn.Click += new System.EventHandler(this.InsertPageBtn_Click);
            // 
            // pageSettingsButton
            // 
            this.pageSettingsButton.BackgroundImage = global::CSharpTextEditor.Properties.Resources.pages_settings;
            this.pageSettingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pageSettingsButton.Location = new System.Drawing.Point(234, 3);
            this.pageSettingsButton.Name = "pageSettingsButton";
            this.pageSettingsButton.Size = new System.Drawing.Size(71, 71);
            this.pageSettingsButton.TabIndex = 5;
            this.generalToolTip.SetToolTip(this.pageSettingsButton, "Общи настройки за страниците");
            this.pageSettingsButton.UseVisualStyleBackColor = true;
            this.pageSettingsButton.Click += new System.EventHandler(this.PageSettingsButton_Click);
            // 
            // deletePageBtn
            // 
            this.deletePageBtn.BackgroundImage = global::CSharpTextEditor.Properties.Resources.delete_page;
            this.deletePageBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deletePageBtn.Location = new System.Drawing.Point(80, 3);
            this.deletePageBtn.Name = "deletePageBtn";
            this.deletePageBtn.Size = new System.Drawing.Size(71, 71);
            this.deletePageBtn.TabIndex = 3;
            this.generalToolTip.SetToolTip(this.deletePageBtn, "Изтриване на активната страница");
            this.deletePageBtn.UseVisualStyleBackColor = true;
            this.deletePageBtn.Click += new System.EventHandler(this.DeletePageBtn_Click);
            // 
            // pageSearchBtn
            // 
            this.pageSearchBtn.BackgroundImage = global::CSharpTextEditor.Properties.Resources.page_search_v2;
            this.pageSearchBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pageSearchBtn.Location = new System.Drawing.Point(157, 3);
            this.pageSearchBtn.Name = "pageSearchBtn";
            this.pageSearchBtn.Size = new System.Drawing.Size(71, 71);
            this.pageSearchBtn.TabIndex = 4;
            this.generalToolTip.SetToolTip(this.pageSearchBtn, "Превключване на видимостта");
            this.pageSearchBtn.UseVisualStyleBackColor = true;
            this.pageSearchBtn.Click += new System.EventHandler(this.PageSearchBtn_Click);
            // 
            // insertImageBtn
            // 
            this.insertImageBtn.BackgroundImage = global::CSharpTextEditor.Properties.Resources.add_image;
            this.insertImageBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.insertImageBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.insertImageBtn.Location = new System.Drawing.Point(80, 3);
            this.insertImageBtn.Name = "insertImageBtn";
            this.insertImageBtn.Size = new System.Drawing.Size(71, 71);
            this.insertImageBtn.TabIndex = 7;
            this.generalToolTip.SetToolTip(this.insertImageBtn, "Вмъкване на изображение");
            this.insertImageBtn.UseVisualStyleBackColor = true;
            this.insertImageBtn.Click += new System.EventHandler(this.InsertImageBtn_Click);
            // 
            // fontDialogBtn
            // 
            this.fontDialogBtn.BackgroundImage = global::CSharpTextEditor.Properties.Resources.font_format_v5;
            this.fontDialogBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fontDialogBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fontDialogBtn.Location = new System.Drawing.Point(3, 3);
            this.fontDialogBtn.Name = "fontDialogBtn";
            this.fontDialogBtn.Size = new System.Drawing.Size(71, 71);
            this.fontDialogBtn.TabIndex = 6;
            this.generalToolTip.SetToolTip(this.fontDialogBtn, "Основно форматиране");
            this.fontDialogBtn.UseVisualStyleBackColor = true;
            this.fontDialogBtn.Click += new System.EventHandler(this.FontDialogBtn_Click);
            // 
            // overflowPageBtn
            // 
            this.overflowPageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.overflowPageBtn.BackgroundImage = global::CSharpTextEditor.Properties.Resources.page_overflow_v2_inactive;
            this.overflowPageBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.overflowPageBtn.Enabled = false;
            this.overflowPageBtn.Location = new System.Drawing.Point(717, 27);
            this.overflowPageBtn.Name = "overflowPageBtn";
            this.overflowPageBtn.Size = new System.Drawing.Size(71, 71);
            this.overflowPageBtn.TabIndex = 6;
            this.generalToolTip.SetToolTip(this.overflowPageBtn, "Преглеждане на препълнените страници");
            this.overflowPageBtn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.insertImageBtn);
            this.panel1.Controls.Add(this.fontDialogBtn);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 77);
            this.panel1.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.openMenuItem,
            this.saveAsMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // newMenuItem
            // 
            this.newMenuItem.Name = "newMenuItem";
            this.newMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newMenuItem.Text = "Нов";
            this.newMenuItem.Click += new System.EventHandler(this.NewMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openMenuItem.Text = "Отвори...";
            this.openMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Name = "saveAsMenuItem";
            this.saveAsMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsMenuItem.Text = "Запиши като...";
            this.saveAsMenuItem.Click += new System.EventHandler(this.SaveAsMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.overflowPageBtn);
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.HtmlViewer);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Тесктообработващ редактор";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DoubleClick += new System.EventHandler(this.MainForm_DoubleClick);
            this.pagePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser HtmlViewer;
        private System.Windows.Forms.Button insertPageBtn;
        private System.Windows.Forms.Button deletePageBtn;
        private System.Windows.Forms.Button pageSearchBtn;
        private System.Windows.Forms.Button pageSettingsButton;
        private System.Windows.Forms.Panel pagePanel;
        private System.Windows.Forms.ToolTip generalToolTip;
        private System.Windows.Forms.Button overflowPageBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button fontDialogBtn;
        private System.Windows.Forms.Button insertImageBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuItem;
    }
}

