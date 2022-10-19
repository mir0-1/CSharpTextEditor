using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
