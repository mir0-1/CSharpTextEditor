using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;

namespace CSharpTextEditor
{
    class PageContainer
    {
        private HtmlDocument document;
        private HtmlElement activePageSection;

        public PageContainer(HtmlDocument document)
        {
            this.document = document;
        }

        public HtmlElement GetActivePageSection()
        {
            if (document == null)
                return null;

            return activePageSection;
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

        public bool ElementIsClass(IHTMLElement element, string className)
        {
            return (element != null && element.className != null && element.className.Contains(className));
        }

        public bool ElementIsClass(HtmlElement element, string className)
        {
            return ElementIsClass((IHTMLElement)element.DomElement, className);
        }

        public bool SetActivePageSection(HtmlElement newPage)
        {
            if (!IsPageSection(newPage))
                return false;

            activePageSection = newPage;
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
                if (IsPageSection(element))
                    return element;

                element = element.Parent;
            }

            return null;
        }

        public void InsertPageAfterActive()
        {
            if (activePageSection == null)
                return;

            ((IHTMLElement)activePageSection.DomElement).insertAdjacentHTML("afterEnd", "<div class=\"page-body\"></div>");
        }
    }

}
