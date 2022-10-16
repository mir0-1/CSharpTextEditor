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
                "<html>" +
                "<head>" +
                "<meta charset=\"utf=8\">" +
                "<style>" +
                ".page-body {" +
                    "position: relative;" +
                    "padding: 1cm;" + // make changeable
                    "margin: 30px;" +
                    "height: 300px;" + // as well
                    "overflow-y: auto;" +
                    "overflow-x: hidden;" +
                    "word-wrap: break-word;" +
                    "background-color: white;" +
                "}" + // best to isоlate the style string
                ".global-page-container {" +
                    "position: relative;" +
                "}" +
                ".editguard {" +
                    "-ms-user-select: none;" +
                "}" +
                "</style>" +
                "</head>" +
                "<body style=\"position: relative; background-color: gray; -ms-user-select: none; overflow-x: hidden;\">" +
                    "<div class=\"editguard global-page-container\">" +
                            "<div class=\"page-body\">" +
                            "</div>"+
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
            //pageContainer.InsertPageAfterActive();
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
            inputManager.InsertImageBtn_DoubleClick(sender, e);
        }

        private void MainForm_DoubleClick(object sender, EventArgs e)
        {
            CustomFontDialog customFontDialog = new CustomFontDialog();
            customFontDialog.ShowDialog();
        }
    }
}
