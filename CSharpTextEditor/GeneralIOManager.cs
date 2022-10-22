using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mshtml;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Drawing;

/*
 * This application uses iText. iText Copyright notice:
 * 
 Copyright (c) 1998-2022 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.
This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS
This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/
The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.
In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.
You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.
For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
 */

namespace CSharpTextEditor
{
    class GeneralIOManager
    {
        [DllImport("user32")]
        private extern static int GetCaretPos(out Point p);

        private IHTMLCaret caret;
        private IHTMLTxtRange range;
        private IDisplayPointer display;

        private bool bOnce = false;

        private enum CaretMoveHorDirection
        {
            FORWARD = 1,
            BACKWARDS = -1
        }

        private enum CaretMoveVertDirection
        {
            UP,
            DOWN
        }

        private const char VK_BACKSPACE = (char)0x08;
        private const char VK_DELETE = (char)0x2E;
        private const char VK_UPARROW = (char)0x26;
        private const char VK_DOWNARROW = (char)0x28;
        private const char VK_LEFTARROW = (char)0x25;
        private const char VK_RIGHTARROW = (char)0x27;
        private const char VK_SPACE = (char)0x20;

        private HtmlDocument document;
        private GeneralPageManager pageManager;
        private DomEditGuard domEditGuard;
        private CustomFontDialog fontDialog = new CustomFontDialog();
        private ImageInsertDialogForm dialogForm;
        private PageSearchDialog pageSearchDialog;
        private PageSettingsDialog pageSettingsDialog;
        private DocFileIOManager docIoManager;

        public GeneralIOManager(HtmlDocument document, float dpiX, float dpiY)
        {
            this.document = document;
            pageManager = new GeneralPageManager(document, dpiX, dpiY);
            domEditGuard = new DomEditGuard(pageManager);
            dialogForm = new ImageInsertDialogForm(dpiX, dpiY);
            pageSearchDialog = new PageSearchDialog(pageManager, (IHTMLDocument2)document.DomDocument);
            pageSettingsDialog = new PageSettingsDialog(pageManager);
            docIoManager = new DocFileIOManager(pageManager);

            fontDialog.AllowVerticalFonts = false;
            fontDialog.FontMustExist = true;
            fontDialog.ShowColor = true;
        }

        public void OnDocumentGlobalClick(object sender, HtmlElementEventArgs e)
        {
            HtmlElement activeElement = document.ActiveElement;

            IHTMLDocument2 doc = (IHTMLDocument2)document.DomDocument;
            IHTMLElement activeDomElement = (IHTMLElement)activeElement.DomElement;
            HtmlElement page = pageManager.GetPageSectionFromContent(activeElement);

            if (page == null)
                return;

            pageManager.SetActivePageSection(page);

            range = doc.selection.createRange();
            range.select();

            if (bOnce == false)
            {
                ((IDisplayServices)doc).CreateDisplayPointer(out display);
                bOnce = true;
            }

            uint result;
            tagPOINT point;
            point.x = e.MousePosition.X;
            point.y = e.MousePosition.Y + page.ScrollTop;

            display.moveToPoint(point, _COORD_SYSTEM.COORD_SYSTEM_CONTENT, activeDomElement, 1, out result);

            ((IDisplayServices)doc).GetCaret(out caret);

            caret.MoveCaretToPointer(display, 1, _CARET_DIRECTION.CARET_DIRECTION_FORWARD);
            caret.Show(0);
        }

        private void VerticalMoveCaret(CaretMoveVertDirection direction)
        {
            IHTMLTextRangeMetrics metrics = (IHTMLTextRangeMetrics)range;
            int newY;

            if (direction == CaretMoveVertDirection.UP)
                newY = metrics.boundingTop - 3;
            else
                newY = metrics.boundingTop + metrics.boundingHeight + 3;

            metrics = (IHTMLTextRangeMetrics)range;
            range.moveToPoint(metrics.boundingLeft, newY);
            range.select();
            caret.Show(1);
        }

        private void HorizontalMoveCaret(CaretMoveHorDirection direction)
        {
            range.move("character", (int)direction);
            if (domEditGuard.CanEditTextSafely(range))
            {
                range.select();
                caret.Show(1);
            }
        }

        private void CaretDeleteSelection(CaretMoveHorDirection direction, HtmlElement activePage)
        {
            if (domEditGuard.CanEditTextSafely(range))
            {
                if (direction == CaretMoveHorDirection.FORWARD)
                    range.moveEnd("character", 1);
                else
                    range.moveStart("character", -1);

                range.select();
            }

            if (domEditGuard.CanEditTextSafely(range))
            {
                range.pasteHTML("");
                caret.Show(1);
            }
        }

        public void OnKeyPreview(object sender, PreviewKeyDownEventArgs e)
        {
            char keyCode = (char)e.KeyCode;
            bool isPaste = Control.ModifierKeys.HasFlag(Keys.Control) && keyCode == 'V';

            HtmlElement page = pageManager.GetActivePageSection();

            if (domEditGuard.CanEditTextSafely(range))
            {
                if (isPaste)
                {
                    string content = Clipboard.GetText(TextDataFormat.Text);

                    if (content == null)
                        return;

                    range.pasteHTML(content);

                    return;
                }

                switch (keyCode)
                {
                    case VK_BACKSPACE:
                        CaretDeleteSelection(CaretMoveHorDirection.BACKWARDS, page);
                        break;
                    case VK_DELETE:
                        CaretDeleteSelection(CaretMoveHorDirection.FORWARD, page);
                        break;
                    case VK_UPARROW:
                        VerticalMoveCaret(CaretMoveVertDirection.UP);
                        break;
                    case VK_DOWNARROW:
                        VerticalMoveCaret(CaretMoveVertDirection.DOWN);
                        break;
                    case VK_LEFTARROW:
                        HorizontalMoveCaret(CaretMoveHorDirection.BACKWARDS);
                        break;
                    case VK_RIGHTARROW:
                        HorizontalMoveCaret(CaretMoveHorDirection.FORWARD);
                        break;
                    case VK_SPACE:
                        if (domEditGuard.CanEditTextSafely(range))
                            range.pasteHTML("&#160;");
                        break;
                }

            }
        }

        public void OnKeyPress(object sender, HtmlElementEventArgs e)
        {
            char keyCode = (char)e.KeyPressedCode;
            bool isEnter = (keyCode == (char)0x0D);

            if (domEditGuard.CanEditTextSafely(range))
            {
                if (!isEnter)
                {
                    range.pasteHTML(keyCode.ToString());
                    caret.Show(1);
                }
                else
                    range.pasteHTML("<br>&#8203;");
            }

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

        public void InsertImageBtn_Click(object sender, EventArgs e)
        {
            if (dialogForm.ShowDialog() == DialogResult.OK && domEditGuard.CanEditTextSafely(range))
                range.pasteHTML(dialogForm.outputHTML);
        }

        public void InsertPageBtn_Click(object sender, EventArgs e)
        {
            pageManager.InsertPageAfterActive();
        }

        public void PageSearchBtn_Click(object sender, EventArgs e)
        {
            pageSearchDialog.ShowDialog();
        }

        public void PageSettingsButton_Click(object sender, EventArgs e)
        {
            if (pageSettingsDialog.ShowDialog() == DialogResult.OK)
            {
                
                pageManager.SetGlobalPageStyles(pageSettingsDialog.headerHeight, 
                                                    pageSettingsDialog.bodyHeight, 
                                                    pageSettingsDialog.footerHeight, 
                                                    pageSettingsDialog.pageWidth,
                                                    pageSettingsDialog.headerEnabled,
                                                    pageSettingsDialog.footerEnabled,
                                                    pageSettingsDialog.bordersEnabled,
                                                    pageSettingsDialog.xmargins,
                                                    pageSettingsDialog.ymargins);
                pageManager.RefreshGlobalPageStyles();
            }
        }

        public void PdfExportMenuItem_DoubleClick(object sender, EventArgs e)
        {
            pageManager.GeneratePDF();
        }

        public void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            docIoManager.SaveToFile();
        }

        public void OpenMenuItem_Click(object sender, EventArgs e)
        {
            docIoManager.OpenFromFile();
        }
        public void DeletePageBtn_Click(object sender, EventArgs e)
        {
            pageManager.DeleteActivePage();
        }

        public void NewMenuItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
                if (MessageBox.Show("Създаване на нов документ? Текущите промени няма да бъдат запазени.", "Внимание", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

            pageManager.NewPageClearAll();
        }

        public void OnFormClosing(FormClosingEventArgs e)
        {
            if (MessageBox.Show("Затваряне? Всички незапазени промени ще бъдат премахнати.", "Внимание", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
