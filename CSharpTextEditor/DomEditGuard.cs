using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mshtml;

namespace CSharpTextEditor
{
    class DomEditGuard
    {
        private PageContainer pageContainer;

        public DomEditGuard(PageContainer pageContainer)
        {
            this.pageContainer = pageContainer;
        }

        public bool CanEditTextSafely(IHTMLTxtRange range)
        {
            if (range == null)
                return false;

            return pageContainer.GetPageSectionFromContent(range.parentElement()) != null;
        }
    }
}
