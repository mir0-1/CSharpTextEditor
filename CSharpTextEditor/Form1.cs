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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void onEnableScrollPage(object sender, EventArgs e)
        {
            MessageBox.Show("resized");
        }

        private void CheckForOverflowChange(HtmlElement htmlElement)
        {
            if (htmlElement.ScrollRectangle.Height > htmlElement.ClientRectangle.Height)
            {
                // add overflow appear handling code
                htmlElement.SetAttribute("-custom-scrollbar-visible", "true");
            }
            else if (htmlElement.GetAttribute("-custom-scrollbar-visible").Equals("true"))
            {
                // add overflow disappear handling code
                MessageBox.Show("scroll bar disappear");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Focus();
            HtmlViewer.DocumentText = 
                "<html>" +
                "<body style=\"background-color: gray;\">" +
                    "<div class=\"page-container\" style=\"background-color: white;\">" +
                            "<div id=\"page-body\" style=\"position: relative; padding: 1cm; height: 300px; overflow-y: auto; word-wrap: break-word;\">" +
                            "</div>"+
                    "</div>" +
                "</body>" +
                "</html>";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            char keyCode = (char)msg.WParam;
            bool useCaps = Control.IsKeyLocked(Keys.CapsLock) ^ Control.ModifierKeys.HasFlag(Keys.Shift);

            if (!useCaps)
                keyCode = Char.ToLower(keyCode);

            HtmlElement page = HtmlViewer.Document.GetElementById("page-body");
            page.InnerHtml += keyCode;

            CheckForOverflowChange(page);

            return true;
        }


        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            IHTMLDocument2 doc = (IHTMLDocument2)HtmlViewer.Document.DomDocument;
            IHTMLElement page = (IHTMLElement)HtmlViewer.Document.GetElementById("page-body").DomElement;

            IHTMLTxtRange selectedRange = (IHTMLTxtRange)doc.selection.createRange();
            selectedRange.moveToPoint(0, 0);
            selectedRange.collapse(true);

            IDisplayPointer display;
            ((IDisplayServices)doc).CreateDisplayPointer(out display);

            uint result;
            tagPOINT point;
            point.x = 135;
            point.y = 135;

            display.moveToPoint(point, _COORD_SYSTEM.COORD_SYSTEM_CONTENT, page, 0, out result);

            IHTMLCaret caret;
            ((IDisplayServices)doc).GetCaret(out caret);
            caret.MoveCaretToPointer(display, 1, _CARET_DIRECTION.CARET_DIRECTION_FORWARD);
            caret.Show(1);
        }

        private void HtmlViewer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }
    }
}
