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

        private InputManager inputManager;

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
                    "padding: 1cm;" +
                    "height: 300px;" +
                "}" +
                ".page-header {" +
                    "height: 300px;" +
                    "border-bottom: 2px dotted blue;" +
                "}" +
                ".page-footer {" +
                    "height: 300px;" +
                    "border-top: 2px dotted blue;" +
                "}" +
                ".page-container {" +
                    "background-color: red;" +
                    "width: 300px;" +
                    "margin: 0 auto;" +
                    "margin-top: 100px;" +
                    "text-align: center;" +
                "}" +
                ".page-section {" +
                    "position: relative;" +
                    "padding: 1cm;" +
                    "overflow-y: auto;" +
                    "overflow-x: hidden;" +
                    "word-wrap: break-word;" +
                    "background-color: white;" +
                    "text-align: left;" +
                "}" + // best to isоlate the style string
                ".global-pages-container {" +
                    "position: relative;" +
                "}" +
                ".editguard {" +
                    "-ms-user-select: none;" +
                "}" +
                "</style>" +
                "</head>" +
                "<body style=\"position: relative; background-color: gray; -ms-user-select: none; overflow-x: hidden;\">" +
                    "<div class=\"editguard global-page-container\">" +
                            "<div class=\"page-container\">" +
                                "<div class=\"page-section page-header\">" +
                                "</div>" +
                                "<div class=\"page-section page-body\">" +
                                "</div>" +
                                "<div class=\"page-section page-footer\">" +
                                "</div>" +
                            "</div>" +
                    "</div>" +
                "</body>" +
                "</html>";

            bCompleted = true;

            float dpiX, dpiY;

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

            inputManager = new InputManager(HtmlViewer.Document, dpiX, dpiY);
        }

        private void HtmlViewer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (bCompleted && !bOnce)
            {
                HtmlViewer.Document.Click += OnDocumentGlobalClick;
                HtmlViewer.Document.Body.KeyPress += OnKeyDown;
                bOnce = true;
            }
        }

        private void InsertPageBtn_Click(object sender, EventArgs e)
        {
            inputManager.InsertPageBtn_Click(sender, e);
        }

        private void HtmlViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            inputManager.OnKeyPreview(sender, e);
        }

        public void OnKeyDown(object sender, HtmlElementEventArgs e)
        {
            inputManager.OnKeyPress(sender, e);
        }

        private void OnDocumentGlobalClick(object sender, HtmlElementEventArgs e)
        {
            inputManager.OnDocumentGlobalClick(sender, e);
        }

        private void FontDialogBtn_Click(object sender, EventArgs e)
        {
            inputManager.FontDialogBtn_Click(sender, e);
        }

        private void InsertImageBtn_Click(object sender, EventArgs e)
        {
            inputManager.InsertImageBtn_Click(sender, e);
        }

        private void MainForm_DoubleClick(object sender, EventArgs e)
        {

        }

        private void PageSearchBtn_Click(object sender, EventArgs e)
        {
            inputManager.PageSearchBtn_Click(sender, e);
        }
    }
}
