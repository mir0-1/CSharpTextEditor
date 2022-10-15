﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mshtml;
using System.Windows.Forms;

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

        public InputManager(HtmlDocument document)
        {
            this.document = document;
            pageContainer = new PageContainer(document);
            domEditGuard = new DomEditGuard(pageContainer);
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

        public void OnKeyDown(object sender, PreviewKeyDownEventArgs e)
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
                    if (range.compareEndPoints("StartToEnd", range) != -1)
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
        }
    }
}
