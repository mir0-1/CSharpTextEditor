using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using mshtml;

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

                pageManager.SyncHeadersFootersContent();

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
                            new XElement("HeaderHeight", pageManager.headerHeightMM),
                            new XElement("FooterHeight", pageManager.footerHeightMM),
                            new XElement("BodyHeight", pageManager.bodyHeightMM),
                            new XElement("PageContainerWidth", pageManager.pageWidthMM),
                            new XElement("HeaderEnabled", pageManager.headerEnabledBool),
                            new XElement("FooterEnabled", pageManager.footerEnabledBool),
                            new XElement("BordersEnabled", pageManager.bordersEnabledBool),
                            new XElement("HeaderContents", header.InnerHtml),
                            new XElement("FooterContents", footer.InnerHtml),
                            new XElement("MarginsX", pageManager.xmarginsMM),
                            new XElement("MarginsY", pageManager.ymarginsMM)
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
                int marginsX = Int32.Parse(docMeta.Element("MarginsX").Value);
                int marginsY = Int32.Parse(docMeta.Element("MarginsY").Value);

                bool headerEnabled = Boolean.Parse(docMeta.Element("HeaderEnabled").Value);
                bool footerEnabled = Boolean.Parse(docMeta.Element("FooterEnabled").Value);
                bool bordersEnabled = Boolean.Parse(docMeta.Element("BordersEnabled").Value);

                pageManager.SetGlobalPageStyles(
                        headerHeight,
                        bodyHeight,
                        footerHeight,
                        pageContainerWidth,
                        headerEnabled,
                        footerEnabled,
                        bordersEnabled,
                        marginsX,
                        marginsY
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
                    readPageContainers.Append(pageManager.CreatePageHTMLWithContent(headerXML.Value, ((XElement)pageContainerIterator).Value, footerXML.Value));
                    pageContainerIterator = pageContainerIterator.NextNode;
                }

                globalPageContainer.InnerHtml = readPageContainers.ToString();

                pageManager.RefreshGlobalPageStyles();
            }
        }
    }
}
