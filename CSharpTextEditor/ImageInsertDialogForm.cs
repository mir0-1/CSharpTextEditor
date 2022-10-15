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
    public partial class ImageInsertDialogForm : Form
    {
        private OpenFileDialog fileDialog = new OpenFileDialog();
        private ImageConverter imageConverter = new ImageConverter();
        private string result;

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

        private void OKButton_Click(object sender, EventArgs e)
        {
            result = imageConverter.FetchImageAsBase64(urlTextBox.Text, 10000);

            if (result != null)
                DialogResult = DialogResult.OK;
        }
    }
}
