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
        private bool bRecursion = false;


        private IHTMLCaret caret;

        public Form1()
        {
            InitializeComponent();
        }

        private bool CanInsertTextSafely(IHTMLTxtRange range)
        {
            if (range == null)
                return false;

            IHTMLElement parent = range.parentElement();
            while (parent != null)
            {
                if (parent.className != null && parent.className.Contains("page-body"))
                    return true;

                parent = parent.parentElement;
            }

            return false;
        }

        private void OnDocumentGlobalClick(object sender, HtmlElementEventArgs e)
        {
            HtmlElement activeElement = HtmlViewer.Document.ActiveElement;

            IHTMLDocument2 doc = (IHTMLDocument2)HtmlViewer.Document.DomDocument;
            IHTMLElement page = (IHTMLElement)activeElement.DomElement;

            if (page.className == null || !page.className.Contains("page-body"))
                return;

            // Need these two lines to keep the caret blinking
            IHTMLTxtRange txtRange = doc.selection.createRange();
            txtRange.select();

            IDisplayPointer display;
            ((IDisplayServices)doc).CreateDisplayPointer(out display);

            uint result;
            tagPOINT point;
            point.x = e.MousePosition.X;
            point.y = e.MousePosition.Y;

            display.moveToPoint(point, _COORD_SYSTEM.COORD_SYSTEM_CONTENT, page, 0, out result);

            ((IDisplayServices)doc).GetCaret(out caret);

            caret.MoveCaretToPointer(display, 1, _CARET_DIRECTION.CARET_DIRECTION_FORWARD);
            caret.Show(1);
        }

        private void CheckForOverflowChange(HtmlElement htmlElement)
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
                    "height: 300px;" + // as well
                    "overflow-y: auto;" +
                    "word-wrap: break-word;" +
                "}" + // best to isоlate the style string
                "</style>" +
                "</head>" +
                "<body style=\"background-color: gray; -ms-user-select: none;\">" +
                    "<div class=\"page-container\" style=\"background-color: white; -ms-user-select: none;\">" +
                            "<div class=\"page-body\" id=\"page-body\">" +
                            "</div>"+
                    "</div>" +
                "</body>" +
                "</html>";

            bCompleted = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            char keyCode = (char)msg.WParam;
            bool useCaps = Control.IsKeyLocked(Keys.CapsLock) ^ Control.ModifierKeys.HasFlag(Keys.Shift);

            if (!useCaps)
                keyCode = Char.ToLower(keyCode);

            HtmlElement page = HtmlViewer.Document.GetElementById("page-body");

            IHTMLTxtRange range = ((IHTMLDocument2)HtmlViewer.Document.DomDocument).selection.createRange();

            if (CanInsertTextSafely(range))
                range.pasteHTML(keyCode.ToString());

            CheckForOverflowChange(page);

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
            HtmlElement pageContainer = HtmlViewer.Document.GetElementById("page-container");
            HtmlElement pageToAdd = HtmlViewer.Document.CreateElement("div");
        }
    }
}
