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

        private bool bCompleted = false;
        private bool bOnce = false;

        private IHTMLCaret caret;
        private IHTMLTxtRange range;
        private IDisplayPointer display;

        private ClipboardHTMLFilter clipboardFilter = new ClipboardHTMLFilter(@"<\s*\/{0,1}(?:style|script|iframe|video|input|form|button|select|embed)\s*(?:href=.*)*.*>");
        private ImageConverter imageConverter = new ImageConverter();
        private PageContainer pageContainer;
        private DomEditGuard domEditGuard;

        public Form1()
        {
            InitializeComponent();
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

            range = doc.selection.createRange();
            range.select();

            ((IDisplayServices)doc).CreateDisplayPointer(out display);

            uint result;
            tagPOINT point;
            point.x = e.MousePosition.X;
            point.y = e.MousePosition.Y + page.ScrollTop;

            display.moveToPoint(point, _COORD_SYSTEM.COORD_SYSTEM_CONTENT, activeDomElement, 1, out result);

            ((IDisplayServices)doc).GetCaret(out caret);

            caret.MoveCaretToPointer(display, 1, _CARET_DIRECTION.CARET_DIRECTION_FORWARD);
            caret.Show(1);
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
            domEditGuard = new DomEditGuard(pageContainer);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            /*char keyCode = (char)msg.WParam;
            bool useCaps = Control.IsKeyLocked(Keys.CapsLock) ^ Control.ModifierKeys.HasFlag(Keys.Shift);
            bool isCtrlActive = Control.ModifierKeys.HasFlag(Keys.Control);
            bool isPaste = isCtrlActive && keyCode == 'V';
            bool isEnter = (msg.WParam == (IntPtr)13);
            bool isBackspace = (keyCode == 'Q');

            if (!useCaps && !isPaste)
                keyCode = Char.ToLower(keyCode);

            HtmlElement page = pageContainer.GetActivePage();

            if (domEditGuard.CanInsertTextSafely(range))
            {
                if (isBackspace)
                {
                    range.moveStart("character", -1);
                    range.select();
                    range.pasteHTML("");
                    caret.Show(1);
                }
                else if (!isPaste)
                {
                    if (!isEnter)
                    {
                        range.pasteHTML(keyCode.ToString());
                        caret.Show(1);
                    }
                    else
                        range.pasteHTML("<br>&#8203;");
                }
                else
                {
                    string content = clipboardFilter.GetFilteredContent();


                    if (content == null)
                        return false;

                    range.pasteHTML(content);
                }
            }

            ElementOverflowHandler.Execute(page);

            return true;*/

            return false;
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

        private void HtmlViewer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            char keyCode = (char)e.KeyData;
            bool useCaps = Control.IsKeyLocked(Keys.CapsLock) ^ Control.ModifierKeys.HasFlag(Keys.Shift);
            bool isCtrlActive = Control.ModifierKeys.HasFlag(Keys.Control);
            bool isPaste = isCtrlActive && keyCode == 'V';
            bool isEnter = (keyCode == (char)13);
            bool isBackspace = (keyCode == (char)8);

            if (!useCaps && !isPaste)
                keyCode = Char.ToLower(keyCode);

            HtmlElement page = pageContainer.GetActivePage();

            if (domEditGuard.CanInsertTextSafely(range))
            {
                if (isBackspace)
                {
                    range.moveStart("character", -1);
                    range.select();
                    range.pasteHTML("");
                    caret.Show(1);
                }
                else if (!isPaste)
                {
                    if (!isEnter)
                    {
                        range.pasteHTML(keyCode.ToString());
                        caret.Show(1);
                    }
                    else
                        range.pasteHTML("<br>&#8203;");
                }
                else
                {
                    string content = clipboardFilter.GetFilteredContent();


                    if (content == null)
                        return;

                    range.pasteHTML(content);
                }
            }

            ElementOverflowHandler.Execute(page);

            return;
        }
    }
}
