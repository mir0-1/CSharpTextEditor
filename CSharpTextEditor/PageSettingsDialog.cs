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
    partial class PageSettingsDialog : Form
    {
        private GeneralPageManager pageManager;

        public int pageWidth;
        public int headerHeight;
        public int footerHeight;
        public int bodyHeight;

        public bool headerEnabled;
        public bool footerEnabled;
        public bool bordersEnabled;

        public PageSettingsDialog(GeneralPageManager pageManager)
        {
            InitializeComponent();
            this.pageManager = pageManager;
        }

        private void PageSettingsDialog_Load(object sender, EventArgs e)
        {
            headerHeightInput.ValueChanged += HeightControls_ValueChanged;
            footerHeightInput.ValueChanged += HeightControls_ValueChanged;
            bodyHeightInput.ValueChanged += HeightControls_ValueChanged;

            headerHeightInput.Value = pageManager.headerHeightMM;
            footerHeightInput.Value = pageManager.footerHeightMM;
            bodyHeightInput.Value = pageManager.bodyHeightMM;
            pageWidthInput.Value = pageManager.pageWidthMM;

            headerCheckbox.Checked = pageManager.headerEnabledBool;
            footerCheckbox.Checked = pageManager.footerEnabledBool;
        }

        private void HeightControls_ValueChanged(Object sender, EventArgs e)
        {
            decimal headerHeightValue = headerCheckbox.Checked ? headerHeightInput.Value : 0;
            decimal footerHeightValue = footerCheckbox.Checked ? footerHeightInput.Value : 0;

            totalHeightLabel.Text = (headerHeightValue + footerHeightValue + bodyHeightInput.Value).ToString();
        }

        private void HeaderCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            headerHeightInput.Enabled = headerCheckbox.Checked;
        }

        private void FooterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            footerHeightInput.Enabled = footerCheckbox.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            pageWidth = (int)pageWidthInput.Value;
            headerHeight = (int)headerHeightInput.Value;
            footerHeight = (int)footerHeightInput.Value;
            bodyHeight = (int)bodyHeightInput.Value;

            headerEnabled = headerCheckbox.Checked;
            footerEnabled = footerCheckbox.Checked;
            bordersEnabled = showBordersCheckbox.Checked;

            DialogResult = DialogResult.OK;
        }
    }
}
