using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    public enum ApplyButtonStatus
    {
        OK,
        FAIL,
        NOT_PRESSED
    }

    public partial class ImageInsertDialogForm : Form
    {
        private OpenFileDialog fileDialog = new OpenFileDialog();
        private ImageParser imageParser = new ImageParser();

        private bool operationSuccess = false;
        private ApplyButtonStatus applyButtonStatus = ApplyButtonStatus.NOT_PRESSED;
        private string outputHTMLInternal;

        private float dpiX;
        private float dpiY;

        const float mmPerInch = 25.4f;
        const int webDownloadTimeoutMS = 10000;

        public string outputHTML
        {
            get => outputHTMLInternal;
        }


        public ImageInsertDialogForm(float dpiX, float dpiY)
        {
            InitializeComponent();

            fileDialog.Filter = "Image Files(*.BMP;*.JPG;*.JPEG; .*PNG)|*.BMP;*.JPG;*.JPEG;*.PNG";
            fileDialog.RestoreDirectory = true;

            this.dpiX = dpiX;
            this.dpiY = dpiY;
        }

        private void OpenFilePCBtn_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
                urlTextBox.Text = fileDialog.FileName;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            operationSuccess = imageParser.FetchImageAsBase64(urlTextBox.Text, webDownloadTimeoutMS);

            if (operationSuccess == false)
            {
                applyButtonStatus = ApplyButtonStatus.FAIL;
                return;
            }

            imageWidthInput.Value = UnitConverter.PixelsToMM(imageParser.width, dpiX);
            imageHeightInput.Value = UnitConverter.PixelsToMM(imageParser.height, dpiY);
            imageWidthInput.Enabled = true;
            imageHeightInput.Enabled = true;
            outputHTMLInternal = imageParser.outputString;
            applyButtonStatus = ApplyButtonStatus.OK;
        }


        private void UrlTextBox_TextChanged(object sender, EventArgs e)
        {
            applyButtonStatus = ApplyButtonStatus.NOT_PRESSED;
            imageWidthInput.Enabled = false;
            imageHeightInput.Enabled = false;
            imageWidthInput.Text = "";
            imageHeightInput.Text = "";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (applyButtonStatus == ApplyButtonStatus.NOT_PRESSED)
                ApplyButton_Click(sender, e);

            if (operationSuccess == true)
            {
                StringBuilder sb = new StringBuilder(outputHTMLInternal);
                sb.Remove(0, 4);
                sb.Insert(0, "<img width=\"" + UnitConverter.MMToPixels(imageWidthInput.Value, dpiX).ToString() + "\" height=\"" + UnitConverter.MMToPixels(imageHeightInput.Value, dpiY) + "\" ");
                outputHTMLInternal = sb.ToString();
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.Abort;
        }
    }
}
