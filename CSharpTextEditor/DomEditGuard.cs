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
        private GeneralPageManager pageContainer;

        public DomEditGuard(GeneralPageManager pageContainer)
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
