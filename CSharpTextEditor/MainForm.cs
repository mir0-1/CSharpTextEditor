using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using mshtml;
using System.Text.RegularExpressions;


namespace CSharpTextEditor
{
    public partial class MainForm : Form
    {
        private bool bCompleted = false;
        private bool bOnce = false;

        private GeneralIOManager ioManager;
        private AboutDialog aboutDialog = new AboutDialog();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Focus();
            HtmlViewer.DocumentText =
                "<!DOCTYPE html>" +
                "<html>" +
                "<head>" +
                "<meta charset=\"utf=8\">" +
                "<style>" +
                ".page-body {" +
                "}" +
                ".page-header {" +
                    "overflow-x: hidden;" + 
                "}" +
                ".page-footer {" +
                    "overflow-x: hidden;" +
                "}" +
                ".page-container {" +
                    "background-color: gray;" +
                    "margin: 0 auto;" +
                    "margin-top: 100px;" +
                "}" +
                ".page-section {" +
                    "position: relative;" +
                    "overflow-y: auto;" +
                    "word-wrap: break-word;" +
                    "background-color: white;" +
                    "width: 100%;" +
                "}" +
                ".global-pages-container {" +
                    "position: relative;" +
                    "text-align: center;" +
                "}" +
                ".editguard {" +
                    "-ms-user-select: none;" +
                "}" +
                "</style>" +
                "</head>" +
                "<body style=\"position: relative; background-color: gray; -ms-user-select: none; overflow-x: hidden;\">" +
                         "<div class=\"editguard global-page-container\">" +
                          "</div>" +
                "</body>" +
                "</html>";

            bCompleted = true;
        }

        private void HtmlViewer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            float dpiX, dpiY;
            if (bCompleted && !bOnce)
            {
                Graphics g = this.CreateGraphics();
                try
                {
                    dpiX = g.DpiX;
                    dpiY = g.DpiY;
                }
                finally
                {
                    g.Dispose();
                }

                ioManager = new GeneralIOManager(HtmlViewer.Document, dpiX, dpiY);

                HtmlViewer.Document.Click += OnDocumentGlobalClick;
                HtmlViewer.Document.Body.KeyPress += OnKeyPress;
                ioManager.NewMenuItem_Click(null, null);
                bOnce = true;
            }
        }

        private void InsertPageBtn_Click(object sender, EventArgs e)
        {
            ioManager.InsertPageBtn_Click(sender, e);
        }

        private void HtmlViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            ioManager.OnKeyPreview(sender, e);
        }

        public void OnKeyPress(object sender, HtmlElementEventArgs e)
        {
            ioManager.OnKeyPress(sender, e);
        }

        private void OnDocumentGlobalClick(object sender, HtmlElementEventArgs e)
        {
            ioManager.OnDocumentGlobalClick(sender, e);
        }

        private void FontDialogBtn_Click(object sender, EventArgs e)
        {
            ioManager.FontDialogBtn_Click(sender, e);
        }

        private void InsertImageBtn_Click(object sender, EventArgs e)
        {
            ioManager.InsertImageBtn_Click(sender, e);
        }

        private void MainForm_DoubleClick(object sender, EventArgs e)
        {
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ioManager.OnFormClosing(e);
        }

        private void PageSearchBtn_Click(object sender, EventArgs e)
        {
            ioManager.PageSearchBtn_Click(sender, e);
        }

        private void PageSettingsButton_Click(object sender, EventArgs e)
        {
            ioManager.PageSettingsButton_Click(sender, e);
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            ioManager.SaveAsMenuItem_Click(sender, e);
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            ioManager.OpenMenuItem_Click(sender, e);
        }

        private void DeletePageBtn_Click(object sender, EventArgs e)
        {
            ioManager.DeletePageBtn_Click(sender, e);
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            ioManager.NewMenuItem_Click(sender, e);
        }

        private void InsertImageMenuItem_Click(object sender, EventArgs e)
        {
            InsertImageBtn_Click(sender, e);
        }

        private void AddPageMenuItem_Click(object sender, EventArgs e)
        {
            InsertPageBtn_Click(sender, e);
        }

        private void DeletePageMenuItem_Click(object sender, EventArgs e)
        {
            DeletePageBtn_Click(sender, e);
        }

        private void PageSearchMenuItem_Click(object sender, EventArgs e)
        {
            PageSearchBtn_Click(sender, e);
        }

        private void PageSettingsMenuItem_Click(object sender, EventArgs e)
        {
            PageSettingsButton_Click(sender, e);
        }

        private void PdfExportMenuItem_Click(object sender, EventArgs e)
        {
            ioManager.PdfExportMenuItem_DoubleClick(sender, e);
        }

        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            aboutDialog.ShowDialog();
        }
    }
}
