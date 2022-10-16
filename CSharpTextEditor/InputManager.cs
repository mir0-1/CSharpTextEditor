using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mshtml;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CSharpTextEditor
{
    class InputManager
    {
        private IHTMLCaret caret;
        private IHTMLTxtRange range;
        private IDisplayPointer display;

        private HtmlDocument document;
        private PageContainer pageContainer;
        private DomEditGuard domEditGuard;
        private ClipboardHTMLFilter clipboardFilter = new ClipboardHTMLFilter(@"<\s*\/{0,1}(?:style|script|iframe|video|input|form|button|select|embed)\s*(?:href=.*)*.*>");
        private CustomFontDialog fontDialog = new CustomFontDialog();
        private ImageInsertDialogForm dialogForm;

        public InputManager(HtmlDocument document, float dpiX, float dpiY)
        {
            this.document = document;
            pageContainer = new PageContainer(document);
            domEditGuard = new DomEditGuard(pageContainer);
            dialogForm = new ImageInsertDialogForm(dpiX, dpiY);

            fontDialog.AllowVerticalFonts = false;
            fontDialog.FontMustExist = true;
            fontDialog.ShowColor = true;
        }

        public void OnDocumentGlobalClick(object sender, HtmlElementEventArgs e)
        {
            HtmlElement activeElement = document.ActiveElement;

            IHTMLDocument2 doc = (IHTMLDocument2)document.DomDocument;
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

        public void OnKeyPreview(object sender, PreviewKeyDownEventArgs e)
        {
            char keyCode = (char)e.KeyCode;
            bool isBackspace = (keyCode == (char)8);
            bool isPaste = Control.ModifierKeys.HasFlag(Keys.Control) && keyCode == 'V';

            HtmlElement page = pageContainer.GetActivePage();

            if (domEditGuard.CanEditTextSafely(range))
            {
                if (isBackspace)
                {
                    if (range.compareEndPoints("StartToEnd", range) != -1)
                        range.moveStart("character", -1);

                    range.select();

                    if (domEditGuard.CanEditTextSafely(range))
                    {
                        range.pasteHTML("");
                        caret.Show(1);

                        ElementOverflowHandler.Execute(page);
                    }
                }
                else if (isPaste)
                {
                    string content = clipboardFilter.GetFilteredContent();

                    if (content == null)
                        return;

                    range.pasteHTML(content);

                    ElementOverflowHandler.Execute(page);
                }
            }
        }

        public void OnKeyPress(object sender, HtmlElementEventArgs e)
        {
            char keyCode = (char)e.KeyPressedCode;
            bool isEnter = (keyCode == (char)13);
            bool isSpace = (keyCode == (char)32);

            HtmlElement page = pageContainer.GetActivePage();

            if (domEditGuard.CanEditTextSafely(range))
            {
                if (!isEnter)
                {
                    if (isSpace)
                        range.pasteHTML("&#160;");
                    else
                        range.pasteHTML(keyCode.ToString());

                    caret.Show(1);
                }
                else
                    range.pasteHTML("<br>&#8203;");
            }

            ElementOverflowHandler.Execute(page);
        }

        public void FontDialogBtn_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                string html = FontDialogParser.GetFormattedHTMLString(fontDialog, range.text);
                if (domEditGuard.CanEditTextSafely(range))
                    range.pasteHTML(html);
            }
        }

        public void InsertImageBtn_DoubleClick(object sender, EventArgs e)
        {
            if (dialogForm.ShowDialog() == DialogResult.OK && domEditGuard.CanEditTextSafely(range))
                range.pasteHTML(dialogForm.outputHTML);
        }
    }
}
