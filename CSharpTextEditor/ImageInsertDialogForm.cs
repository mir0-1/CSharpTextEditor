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

        public string outputHTML
        {
            get => outputHTMLInternal;
        }


        public ImageInsertDialogForm()
        {
            InitializeComponent();

            fileDialog.Filter = "Image Files(*.BMP;*.JPG;*.JPEG)|*.BMP;*.JPG";
            fileDialog.RestoreDirectory = true;
        }

        private void OpenFilePCBtn_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == DialogResult.OK)
                urlTextBox.Text = fileDialog.FileName;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            operationSuccess = imageParser.FetchImageAsBase64(urlTextBox.Text, 10000);

            if (operationSuccess == false)
            {
                applyButtonStatus = ApplyButtonStatus.FAIL;
                return;
            }

            imageWidthTextbox.Text = imageParser.width.ToString();
            imageHeightTextbox.Text = imageParser.height.ToString();
            imageWidthTextbox.Enabled = true;
            imageHeightTextbox.Enabled = true;
            outputHTMLInternal = imageParser.outputString;
            applyButtonStatus = ApplyButtonStatus.OK;
        }

        private void ImageInsertDialogForm_Load(object sender, EventArgs e)
        {

        }

        private void UrlTextBox_TextChanged(object sender, EventArgs e)
        {
            applyButtonStatus = ApplyButtonStatus.NOT_PRESSED;
            imageWidthTextbox.Enabled = false;
            imageHeightTextbox.Enabled = false;
            imageWidthTextbox.Text = "";
            imageHeightTextbox.Text = "";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (applyButtonStatus == ApplyButtonStatus.NOT_PRESSED)
                ApplyButton_Click(sender, e);

            if (operationSuccess == true)
            {
                StringBuilder sb = new StringBuilder(outputHTMLInternal);
                sb.Remove(0, 4);
                sb.Insert(0, "<img width=\"" + imageWidthTextbox.Text + "\" height=\"" + imageHeightTextbox.Text + "\" ");
                outputHTMLInternal = sb.ToString();
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.Abort;
        }
    }
}
