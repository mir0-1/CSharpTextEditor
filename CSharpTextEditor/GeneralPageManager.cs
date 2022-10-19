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

        private int headerHeight = 37;
        private int footerHeight = 37;
        private int bodyHeight = 223;
        private int pageWidth = 210;
        private const int padding = 5;

        private bool headerEnabled = true;
        private bool footerEnabled = true;
        private bool bordersEnabled = false;

        private float dpiX;
        private float dpiY;

        public string headerCss
        {
            get
            {
                return headerEnabled ?
                        ("height:" +
                        UnitConverter.MMToPixels(headerHeightMM, dpiY) +
                        "px;" +
                        (bordersEnabled ? "border:1px dashed;" : "") /*+ "padding:" + UnitConverter.MMToPixels(padding, dpiX) + "px;"*/)
                        : "position:relative;overflow-y:none;display:none;background-color:gray;border:0;";
            }
        }
        public string footerCss
        {
            get => footerEnabled ? ("height:" + UnitConverter.MMToPixels(footerHeightMM, dpiY) + "px;" + (bordersEnabled ? "border:1px dashed;" : "") /*+ "padding:" + UnitConverter.MMToPixels(padding, dpiX) + "px;"*/) : "position:relative;overflow-y:none;display:none;background-color:gray;border:0;";
        }
        public string bodyCss
        {
            get => "height:" + UnitConverter.MMToPixels(bodyHeightMM, dpiY) + "px;";/*padding:" + UnitConverter.MMToPixels(padding, dpiX) + "px;";*/
        }
        public string pageContainerCss
        {
            get => "width:" + UnitConverter.MMToPixels(pageWidthMM, dpiX) + "px;";/*padding:" + UnitConverter.MMToPixels(padding, dpiX) + "px;";*/
        }

        public int headerHeightMM
        {
            get => headerHeight;
        }

        public int footerHeightMM
        {
            get => footerHeight;
        }

        public int bodyHeightMM
        {
            get => bodyHeight;
        }
        public int pageWidthMM
        {
            get => pageWidth;
        }

        public bool headerEnabledBool
        {
            get => headerEnabled;
        }
        public bool footerEnabledBool
        {
            get => footerEnabled;
        }

        public bool bordersEnabledBool
        {
            get => bordersEnabled;
        }

        public GeneralPageManager(HtmlDocument document, float dpiX, float dpiY)
        {
            this.document = document;
            this.dpiX = dpiX;
            this.dpiY = dpiY;
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
            ConverterProperties properties = new ConverterProperties();

            IList<IElement> elements = HtmlConverter.ConvertToElements(activePageSection.Document.Body.InnerHtml, properties);
            PdfDocument pdf = new PdfDocument(new PdfWriter("output.pdf"));
            pdf.SetTagged();

            Document document = new Document(pdf, new PageSize(pageWidth * 0.0394f *72f, (headerHeight + bodyHeight + footerHeight)* 0.0394f * 72f));
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

        public void SetGlobalPageStyles(int headerHeight, int bodyHeight, int footerHeight, int pageWidth, bool headerEnabled, bool footerEnabled, bool bordersEnabled)
        {
            this.headerEnabled = headerEnabled;
            this.footerEnabled = footerEnabled;
            this.bordersEnabled = bordersEnabled;

            if (headerEnabled)
                this.headerHeight = headerHeight;

            if (footerEnabled)
                this.footerHeight = footerHeight;

            this.bodyHeight = bodyHeight;
            this.pageWidth = pageWidth;
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
