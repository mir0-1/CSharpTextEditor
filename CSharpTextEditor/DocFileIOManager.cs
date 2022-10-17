using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using mshtml;

namespace CSharpTextEditor
{
    class DocFileIOManager
    {
        private GeneralPageManager pageManager;
        private SaveFileDialog saveFileDialog = new SaveFileDialog();
        private OpenFileDialog openFileDialog = new OpenFileDialog();

        public DocFileIOManager(GeneralPageManager pageManager)
        {
            this.pageManager = pageManager;

            saveFileDialog.Filter = "XML Document | *.xml";
            saveFileDialog.DefaultExt = "xml";

            openFileDialog.Filter = saveFileDialog.Filter;
            openFileDialog.DefaultExt = saveFileDialog.DefaultExt;
        }

        public void SaveToFile()
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

        public void OpenFromFile()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XDocument doc = XDocument.Load(openFileDialog.FileName);
                XElement docMeta = doc.Root.Element("DocMeta");

                int headerHeight = Int32.Parse(docMeta.Element("HeaderHeight").Value);
                int bodyHeight = Int32.Parse(docMeta.Element("BodyHeight").Value);
                int footerHeight = Int32.Parse(docMeta.Element("FooterHeight").Value);
                int pageContainerWidth = Int32.Parse(docMeta.Element("PageContainerWidth").Value);

                bool headerEnabled = Boolean.Parse(docMeta.Element("HeaderEnabled").Value);
                bool footerEnabled = Boolean.Parse(docMeta.Element("FooterEnabled").Value);

                pageManager.SetGlobalPageStyles(
                        headerHeight,
                        bodyHeight,
                        footerHeight,
                        pageContainerWidth,
                        headerEnabled,
                        footerEnabled
               );

                XElement headerXML = docMeta.Element("HeaderContents");
                XElement footerXML = docMeta.Element("FooterContents");
                XElement globalPageContainerXML = doc.Root.Element("GlobalPageContainer");

                HtmlElement globalPageContainer = pageManager.GetGlobalPageContainer();
                globalPageContainer.InnerHtml = "";
                StringBuilder readPageContainers = new StringBuilder();

                XNode pageContainerIterator = globalPageContainerXML.FirstNode;

                while (pageContainerIterator != null)
                {
                    readPageContainers.Append("<div class=\"page-container\">" +
                                "<div class=\"page-section page-header\" style=\"" + pageManager.headerCss + "\">" + headerXML.Value +
                                "</div>" +
                                "<div class=\"page-section page-body\" style=\"" + pageManager.bodyCss + "\">" + ((XElement)pageContainerIterator).Value +
                                "</div>" +
                                "<div class=\"page-section page-footer\" style=\"" + pageManager.footerCss + "\">" + footerXML.Value +
                                "</div>" +
                            "</div>");
                    pageContainerIterator = pageContainerIterator.NextNode;
                }

                globalPageContainer.InnerHtml = readPageContainers.ToString();

                pageManager.RefreshGlobalPageStyles();
            }
        }
    }
}
