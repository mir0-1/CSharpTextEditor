using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace CSharpTextEditor
{
    class PageManager
    {
        private HtmlDocument document;
        private HtmlElement activePageSection;
        private HtmlElement prevActivePageSection =  null;

        public PageManager(HtmlDocument document)
        {
            this.document = document;
        }

        public HtmlElement GetActivePageSection()
        {
            if (document == null)
                return null;

            return activePageSection;
        }

        public void UpdateHeadersFooters()
        {
            HtmlElement globalPageContainer = GetGlobalPageContainer();

            if (globalPageContainer == null)
                return;

            bool isHeader = IsHeaderSection(activePageSection);
            bool isFooter = IsFooterSection(activePageSection);

            if (!isHeader && !isFooter)
                return;

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

        public bool IsFooterSection(HtmlElement pageSection)
        {
            if (pageSection == null)
                return false;

            return ElementIsClass(pageSection, "page-footer");
        }

        public HtmlElement GetPageContainerFooter(HtmlElement pageContainer)
        {
            if (pageContainer == null || pageContainer.Children == null || pageContainer.Children.Count < 3)
                return null;

            return pageContainer.Children[2];
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

        public bool SetActivePageSection(HtmlElement newPage)
        {
            if (!IsPageSection(newPage))
                return false;

            if (Object.ReferenceEquals(prevActivePageSection, activePageSection) && activePageSection != null && prevActivePageSection != null)
            {
                //UpdateHeadersFooters();
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

        public void InsertPageAfterActive()
        {
            if (activePageSection == null)
                return;

            HtmlElement activePageContainer = GetPageContainerFromContent(activePageSection);

            if (activePageContainer == null)
                return;


            ((IHTMLElement)activePageContainer.DomElement).insertAdjacentHTML("afterend",
                            "<div class=\"page-container\">" +
                                "<div class=\"page-section page-header\">" +
                                "</div>" +
                                "<div class=\"page-section page-body\">" +
                                "</div>" +
                                "<div class=\"page-section page-footer\">" +
                                "</div>" +
                            "</div>");
        }
    }

}
