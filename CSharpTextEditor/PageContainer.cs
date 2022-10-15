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
        private HtmlElement activePage;

        public PageContainer(HtmlDocument document)
        {
            this.document = document;
        }

        public HtmlElement GetActivePage()
        {
            if (document == null)
                return null;

            return activePage;
        }

        public bool IsActivePage(HtmlElement htmlElement)
        {
            return htmlElement != null && htmlElement == activePage;
        }

        public bool IsPageBody(HtmlElement element)
        {
            return ElementIsClass(element, "page-body");
        }

        public bool IsPageBody(IHTMLElement element)
        {
            return ElementIsClass(element, "page-body");
        }

        public bool ElementIsClass(IHTMLElement element, string className)
        {
            return (element != null && element.className != null && element.className.Contains(className));
        }

        public bool ElementIsClass(HtmlElement element, string className)
        {
            return ElementIsClass((IHTMLElement)element.DomElement, className);
        }

        public bool SetActivePage(HtmlElement newPage)
        {
            if (!IsPageBody(newPage))
                return false;

            activePage = newPage;
            return true;
        }

        public HtmlElement GetPageFromContent(HtmlElement element)
        {
            while (element != null)
            {
                if (IsPageBody(element))
                    return element;

                element = element.Parent;
            }

            return null;
        }

        public IHTMLElement GetPageFromContent(IHTMLElement element)
        {
            while (element != null)
            {
                if (IsPageBody(element))
                    return element;

                element = element.parentElement;
            }

            return null;
        }

        public void InsertPageAfterActive()
        {
            if (activePage == null)
                return;

            ((IHTMLElement)activePage.DomElement).insertAdjacentHTML("afterEnd", "<div class=\"page-body\"></div>");
        }
    }

}
