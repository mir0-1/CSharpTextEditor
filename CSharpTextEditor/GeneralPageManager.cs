using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using mshtml;

namespace CSharpTextEditor
{
    class GeneralPageManager
    {
        private HtmlDocument document;
        private HtmlElement activePageSection;
        private HtmlElement prevActivePageSection =  null;
        private SaveFileDialog saveFileDialog = new SaveFileDialog();

        private int headerHeightInternal = 140;
        private int footerHeightInternal = 140;
        private int bodyHeightInternal = 843;
        private int pageWidthInternal = 794;
        private int xmarginsInternal = 94;
        private int ymarginsInternal = 94;

        private bool headerEnabledInternal = true;
        private bool footerEnabledInternal = true;
        private bool bordersEnabledInternal = false;

        private float dpiX;
        private float dpiY;

        public string headerCss
        {
             get => headerEnabledInternal ? ("height:" + (headerHeightInternal) + "px;background-color:white;" + 
                                    (bordersEnabledInternal ? "border:1px dashed;" : "")
                                    ): "position:relative;overflow-y:none;display:none;background-color:gray;border:0;";
        }
        public string footerCss
        {
            get => footerEnabledInternal ? ("height:" + (footerHeightInternal) + "px;background-color:white;" +
                       (bordersEnabledInternal ? "border:1px dashed;" : "")
                       ):"position:relative;overflow-y:none;display:none;background-color:gray;border:0;";
        }
        public string bodyCss
        {
            get => "height:" + bodyHeightInternal + "px;background-color:white;";
        }
        public string pageContainerCss => "background-color:white;width: " +
                                            (pageWidthInternal) + "px;" +
                                            "padding-left:" + xmarginsInternal + "px;" +
                                            "padding-top:" + ymarginsInternal + "px;" +
                                            "padding-right:" + xmarginsInternal + "px;" +
                                            "padding-bottom:" + ymarginsInternal + "px;";

        public bool headerEnabledBool => headerEnabledInternal;
        public bool footerEnabledBool  => footerEnabledInternal;
        public bool bordersEnabledBool => bordersEnabledInternal;
        public int headerHeight => headerHeightInternal;
        public int footerHeight => footerHeightInternal;
        public int bodyHeight => bodyHeightInternal;
        public int pageWidth => pageWidthInternal;
        public int xmargins => xmarginsInternal;
        public int ymargins => ymarginsInternal;

        public int headerHeightMM => (int)UnitConverter.PixelsToMM(headerHeightInternal, dpiY);
        public int footerHeightMM => (int)UnitConverter.PixelsToMM(footerHeightInternal, dpiY);
        public int bodyHeightMM => (int)UnitConverter.PixelsToMM(bodyHeightInternal, dpiY);
        public int pageWidthMM => (int)UnitConverter.PixelsToMM(pageWidthInternal, dpiX);
        public int xmarginsMM => (int)UnitConverter.PixelsToMM(xmarginsInternal, dpiX);
        public int ymarginsMM => (int)UnitConverter.PixelsToMM(ymarginsInternal, dpiY);


        public GeneralPageManager(HtmlDocument document, float dpiX, float dpiY)
        {
            this.document = document;
            this.dpiX = dpiX;
            this.dpiY = dpiY;

            saveFileDialog.Filter = "PDF Document (*.PDF)|*.PDF";
        }

        public HtmlElement GetActivePageSection()
        {
            if (document == null)
                return null;

            return activePageSection;
        }

        public void DeleteActivePage()
        {
            HtmlElement globalPageContainer = GetGlobalPageContainer();

            if (globalPageContainer == null || globalPageContainer.Children == null || globalPageContainer.Children.Count <= 1)
                return;

            HtmlElement pageSection = GetActivePageSection();

            if (pageSection == null)
                return;

            HtmlElement pageContainer = GetPageContainerFromContent(pageSection);

            if (pageContainer == null)
                return;

            DeletePageContainer(pageContainer);
        }

        public void DeleteAllPages()
        {
            HtmlElement globalPageContainer = GetGlobalPageContainer();
            globalPageContainer.InnerHtml = "";
        }

        public string GetEmptyPageHTMLTemplate()
        {
            return CreatePageHTMLWithContent("", "", "");
        }

        public void NewPageClearAll()
        {
            HtmlElement globalPageContainer = GetGlobalPageContainer();

            if (globalPageContainer == null)
                return;

            globalPageContainer.InnerHtml = GetEmptyPageHTMLTemplate();
        }

        public void SyncHeadersFootersContent()
        {
            HtmlElement globalPageContainer = GetGlobalPageContainer();

            if (globalPageContainer == null)
                return;

            bool isHeader = IsHeaderSection(activePageSection);
            bool isFooter = IsFooterSection(activePageSection);

            if (isHeader)
            {
                foreach (HtmlElement pageContainer in globalPageContainer.Children)
                {
                    HtmlElement header = GetPageContainerHeader(pageContainer);
                    if (header != null && header != activePageSection)
                    {
                        header.InnerHtml = activePageSection.InnerHtml;
                    }
                }
            }
            else if (isFooter)
            {
                foreach (HtmlElement pageContainer in globalPageContainer.Children)
                {
                    HtmlElement footer = GetPageContainerFooter(pageContainer);
                    if (footer != null && footer != activePageSection)
                    {
                        footer.InnerHtml = activePageSection.InnerHtml;
                    }
                }
            }
        }

        public HtmlElement GetGlobalPageContainer()
        {
            foreach (HtmlElement element in document.Body.Children)
            {
                if (IsGlobalPageContainer(element))
                    return element;
            }

            return null;
        }

        public HtmlElement GetPageContainerHeader(HtmlElement pageContainer)
        {
            if (pageContainer == null || pageContainer.Children == null || pageContainer.Children.Count == 0)
                return null;

            return pageContainer.Children[0];
        }

        public bool IsHeaderSection(HtmlElement pageSection)
        {
            if (pageSection == null)
                return false;

            return ElementIsClass(pageSection, "page-header");
        }

        public void GeneratePDF()
        {
            if (saveFileDialog.ShowDialog() != DialogResult.OK || activePageSection == null)
                return;

            ConverterProperties properties = new ConverterProperties();

            IList<IElement> elements = HtmlConverter.ConvertToElements(activePageSection.Document.Body.InnerHtml, properties);
            PdfDocument pdf = new PdfDocument(new PdfWriter(saveFileDialog.FileName));
            pdf.SetTagged();

            Document document = new Document(pdf, new PageSize((pageWidthMM + 2*xmarginsMM) * 0.0394f *72f, (headerHeightMM + bodyHeightMM + footerHeightMM + 2*ymarginsMM)* 0.0394f * 72f));
            document.SetMargins(0, 0, 0, 0);


            foreach (IElement element in elements)
            {
                document.Add((IBlockElement)element);
            }

            document.Close();
        }

        public bool IsFooterSection(HtmlElement pageSection)
        {
            if (pageSection == null)
                return false;

            return ElementIsClass(pageSection, "page-footer");
        }

        public HtmlElement GetPageContainerFooter(HtmlElement pageContainer)
        {
            if (pageContainer == null || pageContainer.Children == null || pageContainer.Children.Count < 2)
                return null;

            return pageContainer.Children[2];
        }

        public HtmlElement GetPageContainerBody(HtmlElement pageContainer)
        {
            if (pageContainer == null || pageContainer.Children == null || pageContainer.Children.Count < 3)
                return null;

            return pageContainer.Children[1];
        }

        public bool IsActivePageSection(HtmlElement htmlElement)
        {
            return htmlElement != null && htmlElement == activePageSection;
        }

        public bool IsPageSection(HtmlElement element)
        {
            return ElementIsClass(element, "page-section");
        }

        public bool IsPageSection(IHTMLElement element)
        {
            return ElementIsClass(element, "page-section");
        }

        public bool IsPageContainer(HtmlElement element)
        {
            return ElementIsClass(element, "page-container");
        }

        public bool IsGlobalPageContainer(HtmlElement element)
        {
            return ElementIsClass(element, "global-page-container");
        }

        public bool ElementIsClass(IHTMLElement element, string className)
        {
            return (element != null && element.className != null && element.className.Contains(className));
        }

        public bool ElementIsClass(HtmlElement element, string className)
        {
            return ElementIsClass((IHTMLElement)element.DomElement, className);
        }

        public HtmlElement GetIthPageContainer(int index)
        {
            int i = 0;
            HtmlElement globalPageContainer = GetGlobalPageContainer();

            if (globalPageContainer == null)
                return null;

            foreach (HtmlElement element in globalPageContainer.Children)
            {
                if (IsPageContainer(element))
                    i++;

                if (i == index)
                    return element;
            }

            return null;
        }

        public string CreatePageHTMLWithContent(string headerHtml, string bodyHtml, string footerHtml)
        {
            if (headerHtml == null)
                headerHtml = "";

            if (bodyHtml == null)
                bodyHtml = "";

            if (footerHtml == null)
                footerHtml = "";

            return "<div class=\"editguard page-container\" style=\"" + pageContainerCss + "\">" +
                    "<div class=\"page-section page-header\" style=\"" + headerCss + "\">" + headerHtml +
                    "</div>" +
                    "<div class=\"page-section page-body\" style=\"" + bodyCss + "\">" + bodyHtml +
                    "</div>" +
                    "<div class=\"page-section page-footer\" style=\"" + footerCss + "\">" + footerHtml +
                    "</div>" +
                "</div>";
        }

        public bool SetActivePageSection(HtmlElement newPage)
        {
            if (!IsPageSection(newPage))
                return false;

            if (Object.ReferenceEquals(prevActivePageSection, activePageSection) && activePageSection != null && prevActivePageSection != null)
            {
                SyncHeadersFootersContent();
            }

            activePageSection = newPage;
            prevActivePageSection = activePageSection;

            return true;
        }

        public HtmlElement GetPageSectionFromContent(HtmlElement element)
        {
            while (element != null)
            {
                if (IsPageSection(element))
                    return element;

                element = element.Parent;
            }

            return null;
        }

        public IHTMLElement GetPageSectionFromContent(IHTMLElement element)
        {
            while (element != null)
            {
                if (IsPageSection(element))
                    return element;

                element = element.parentElement;
            }

            return null;
        }

        public HtmlElement GetPageContainerFromContent(HtmlElement element)
        {
            while (element != null)
            {
                if (IsPageContainer(element))
                    return element;

                element = element.Parent;
            }

            return null;
        }

        public void DeletePageContainer(HtmlElement element)
        {
            if (element == null)
                return;

            if (IsPageContainer(element))
                element.OuterHtml = "";
        }

        public void InsertPageAfterActive()
        {
            if (activePageSection == null)
                return;

            HtmlElement activePageContainer = GetPageContainerFromContent(activePageSection);

            if (activePageContainer == null)
                return;

            HtmlElement header = GetPageContainerHeader(activePageContainer);
            HtmlElement footer = GetPageContainerFooter(activePageContainer);

            ((IHTMLElement)activePageContainer.DomElement).insertAdjacentHTML("afterend", CreatePageHTMLWithContent(header.InnerHtml, "", footer.InnerHtml));
        }

        public void SetGlobalPageStyles(int headerHeight, int bodyHeight, int footerHeight, int pageWidth, bool headerEnabled, bool footerEnabled, bool bordersEnabled, int xmargins, int ymargins)
        {
            this.headerEnabledInternal = headerEnabled;
            this.footerEnabledInternal = footerEnabled;
            this.bordersEnabledInternal = bordersEnabled;
            this.xmarginsInternal = (int)UnitConverter.MMToPixels(xmargins, dpiX);
            this.ymarginsInternal = (int)UnitConverter.MMToPixels(ymargins, dpiY);

            if (headerEnabled)
                this.headerHeightInternal = (int)UnitConverter.MMToPixels(headerHeight, dpiY);

            if (footerEnabled)
                this.footerHeightInternal = (int)UnitConverter.MMToPixels(footerHeight, dpiY);

            this.bodyHeightInternal = (int)UnitConverter.MMToPixels(bodyHeight, dpiY);
            this.pageWidthInternal = (int)UnitConverter.MMToPixels(pageWidth, dpiX);
        }

        public void RefreshGlobalPageStyles()
        {
            if (document == null)
                return;

            HtmlElement globalPageContainer = GetGlobalPageContainer();

            if (globalPageContainer == null || globalPageContainer.Children == null || globalPageContainer.Children.Count == 0)
                return;

            foreach (HtmlElement pageContainer in globalPageContainer.Children)
            {
                HtmlElement header = GetPageContainerHeader(pageContainer);
                header.Style = headerCss;

                HtmlElement footer = GetPageContainerFooter(pageContainer);
                footer.Style = footerCss;

                HtmlElement body = GetPageContainerBody(pageContainer);
                body.Style = bodyCss;

                pageContainer.Style = pageContainerCss;
            }
        }
    }

}
