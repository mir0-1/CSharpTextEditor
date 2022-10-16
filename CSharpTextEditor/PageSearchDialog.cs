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
    partial class PageSearchDialog : Form
    {
        private PageManager pageManager;

        public PageSearchDialog(PageManager pageManager)
        {
            InitializeComponent();

            this.pageManager = pageManager;
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
    }
}
