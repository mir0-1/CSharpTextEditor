using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace CSharpTextEditor
{
    partial class PageSearchDialog : Form
    {
        private PageManager pageManager;
        private IHTMLDocument2 doc;

        public PageSearchDialog(PageManager pageManager, IHTMLDocument2 doc)
        {
            InitializeComponent();

            this.pageManager = pageManager;
            this.doc = doc;
        }

        private void PageSearch_Click(object sender, EventArgs e)
        {
            HtmlElement pageContainer = pageManager.GetIthPageContainer((int)pageIndexInput.Value);

            if (pageContainer == null)
            {
                MessageBox.Show("Невалидна страница", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pageContainer.ScrollIntoView(true);
        }

        private void ButtonSearchTextGlobal_Click(object sender, EventArgs e)
        {
            IHTMLTxtRange range = doc.selection.createRange();

            range.collapse(false);
            range.findText(pageSearchTextBox.Text);
            range.select();
        }
    }
}
