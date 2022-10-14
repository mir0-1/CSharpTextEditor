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

        public bool IsPage(HtmlElement element)
        {
            return IsPage((IHTMLElement)element.DomElement);
        }

        public bool IsPage(IHTMLElement element)
        {
            return (element != null && element.className != null && element.className.Contains("page-body"));
        }

        public bool SetActivePage(HtmlElement newPage)
        {
            if (!IsPage(newPage))
                return false;

            activePage = newPage;
            return true;
        }

        public HtmlElement GetPageFromContent(HtmlElement element)
        {
            while (element != null)
            {
                if (IsPage(element))
                    return element;

                element = element.Parent;
            }

            return null;
        }

        public void InsertPageAfterActive()
        {
            if (activePage == null)
                return;

            ((IHTMLElement)activePage.DomElement).insertAdjacentHTML("afterEnd", "<div class=\"page-body\" style=\"background-color: white;\"></div>");
        }
    }

}
