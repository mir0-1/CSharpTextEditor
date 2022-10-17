using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CSharpTextEditor
{
    class DocFileIOManager
    {
        private GeneralPageManager pageManager;
        private SaveFileDialog saveFileDialog = new SaveFileDialog();

        public DocFileIOManager(GeneralPageManager pageManager)
        {
            this.pageManager = pageManager;

            saveFileDialog.Filter = "XML Document | *.xml";
            saveFileDialog.DefaultExt = "xml";
        }

        public void Save()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                HtmlElement anyPageContainer = pageManager.GetIthPageContainer(1);

                if (anyPageContainer == null)
                    return;

                HtmlElement header = pageManager.GetPageContainerHeader(anyPageContainer);
                HtmlElement footer = pageManager.GetPageContainerFooter(anyPageContainer);

                if (header == null || footer == null)
                    return;

                if (header.InnerHtml == null)
                    header.InnerHtml = "";

                if (footer.InnerHtml == null)
                    footer.InnerHtml = "";

                XDocument doc = new XDocument(
                    new XElement("Root",
                        new XElement("DocMeta",
                            new XElement("HeaderHeight", pageManager.headerHeightInt),
                            new XElement("FooterHeight", pageManager.footerHeightInt),
                            new XElement("BodyHeight", pageManager.bodyHeightInt),
                            new XElement("PageContainerWidth", pageManager.pageWidthInt),
                            new XElement("HeaderEnabled", pageManager.headerEnabledBool),
                            new XElement("FooterEnabled", pageManager.footerEnabledBool),
                            new XElement("HeaderContents", header.InnerHtml),
                            new XElement("FooterContents", footer.InnerHtml)
                        )
                    )
                );
                HtmlElement globalPageContainerHTML = pageManager.GetGlobalPageContainer();

                if (globalPageContainerHTML == null)
                    return;

                XElement globalPageContainer = new XElement("GlobalPageContainer");

                foreach (HtmlElement pageContainer in globalPageContainerHTML.Children)
                {
                    HtmlElement pageBodyHTML = pageManager.GetPageContainerBody(pageContainer);
                    if (pageBodyHTML.InnerHtml == null)
                        pageBodyHTML.InnerHtml = "";
                    globalPageContainer.Add(new XElement("PageBody", pageBodyHTML.InnerHtml));
                }

                doc.Root.Add(globalPageContainer);
                doc.Save(saveFileDialog.FileName);
            }
        }
    }
}
