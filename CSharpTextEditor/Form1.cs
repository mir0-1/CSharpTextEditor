﻿using System;
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
    public partial class Form1 : Form
    {

        private bool bCompleted = false;
        private bool bOnce = false;

        private IHTMLCaret caret;
        private ClipboardHTMLFilter clipboardFilter = new ClipboardHTMLFilter(@"<\s*\/{0,1}(?:style|script|iframe|video|input|form|button)\s*(?:href=.*)*.*>");
        private ImageConverter imageConverter = new ImageConverter();
        private PageContainer pageContainer;

        public Form1()
        {
            InitializeComponent();
        }

        private bool CanInsertTextSafely(IHTMLTxtRange range)
        {
            if (range == null)
                return false;

            return IsPageContent(range.parentElement());
        }

        private bool IsPageContent(IHTMLElement element)
        {
            while (element != null)
            {
                if (element.className != null && element.className.Contains("page-body"))
                    return true;

                element = element.parentElement;
            }

            return false;
        }

        private void OnDocumentGlobalClick(object sender, HtmlElementEventArgs e)
        {
            HtmlElement activeElement = HtmlViewer.Document.ActiveElement;

            IHTMLDocument2 doc = (IHTMLDocument2)HtmlViewer.Document.DomDocument;
            IHTMLElement activeDomElement = (IHTMLElement)activeElement.DomElement;
            HtmlElement page = pageContainer.GetPageFromContent(activeElement);

            if (page == null)
                return;

            pageContainer.SetActivePage(page);

            // Need these two lines to keep the caret blinking
            IHTMLTxtRange txtRange = doc.selection.createRange();
            txtRange.select();

            IDisplayPointer display;
            ((IDisplayServices)doc).CreateDisplayPointer(out display);

            uint result;
            tagPOINT point;
            point.x = e.MousePosition.X;
            point.y = e.MousePosition.Y;

            display.moveToPoint(point, _COORD_SYSTEM.COORD_SYSTEM_CONTENT, activeDomElement, 1, out result);

            ((IDisplayServices)doc).GetCaret(out caret);

            caret.MoveCaretToPointer(display, 1, _CARET_DIRECTION.CARET_DIRECTION_FORWARD);
            caret.Show(1);
        }

        private void HandleOverflowChange(HtmlElement htmlElement)
        {
            if (htmlElement.ScrollRectangle.Height > htmlElement.ClientRectangle.Height)
            {
                // add overflow scroll appear handling code
                htmlElement.SetAttribute("-custom-scrollbar-visible", "true");
            }
            else if (htmlElement.GetAttribute("-custom-scrollbar-visible").Equals("true"))
            {
                // add overflow scroll disappear handling code
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Focus();
            HtmlViewer.DocumentText = 
                "<html>" +
                "<head>" +
                "<style>" +
                ".page-body {" +
                    "position: relative;" +
                    "padding: 1cm;" + // make changeable
                    "margin: 30px;" +
                    "height: 300px;" + // as well
                    "overflow-y: auto;" +
                    "word-wrap: break-word;" +
                    "background-color: white;" +
                "}" + // best to isоlate the style string
                ".page-container {" +
                    "position: relative;" +
                "}" +
                ".editguard {" +
                    "-ms-user-select: none;" +
                "}" +
                "</style>" +
                "</head>" +
                "<body style=\"background-color: gray; -ms-user-select: none;\">" +
                    "<div class=\"editguard page-container\">" +
                            "<div class=\"page-body\">" +
                            "</div>"+
                    "</div>" +
                "</body>" +
                "</html>";

            bCompleted = true;
            pageContainer = new PageContainer(HtmlViewer.Document);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            char keyCode = (char)msg.WParam;
            bool useCaps = Control.IsKeyLocked(Keys.CapsLock) ^ Control.ModifierKeys.HasFlag(Keys.Shift);
            bool isPaste = Control.ModifierKeys.HasFlag(Keys.Control) && keyCode == 'V';
            bool isEnter = (msg.WParam == (IntPtr)13);

            if (!useCaps && !isPaste)
                keyCode = Char.ToLower(keyCode);

            HtmlElement page = pageContainer.GetActivePage();

            IHTMLTxtRange range = ((IHTMLDocument2)HtmlViewer.Document.DomDocument).selection.createRange();

            if (CanInsertTextSafely(range))
            {
                if (!isPaste)
                {
                    if (!isEnter)
                        range.pasteHTML(keyCode.ToString());
                    else
                        range.pasteHTML("<br>&#8203;");
                }
                else
                {
                    string content = clipboardFilter.GetFilteredContent();
                    if (content != null)
                        range.pasteHTML(content);
                }
            }

            HandleOverflowChange(page);

            return true;
        }


        private void Form1_DoubleClick(object sender, EventArgs e)
        {
        }

        private void HtmlViewer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (bCompleted && !bOnce)
            {
                HtmlViewer.Document.Click += OnDocumentGlobalClick;
                bOnce = true;
            }
        }

        private void InsertPageBtn_Click(object sender, EventArgs e)
        {
            pageContainer.InsertPageAfterActive();
        }
    }
}
