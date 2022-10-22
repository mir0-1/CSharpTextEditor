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
