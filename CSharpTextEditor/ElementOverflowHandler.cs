using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTextEditor
{
    class ElementOverflowHandler
    {
        public static void Execute(HtmlElement htmlElement)
        {
            if (htmlElement.ScrollRectangle.Height > htmlElement.ClientRectangle.Height)
            {
                // add overflow scroll appear handling code
                htmlElement.SetAttribute("-custom-scrollbar-visible", "true");
            }
            else if (htmlElement.GetAttribute("-custom-scrollbar-visible").Equals("true"))
            {
                // add overflow scroll disappear handling code
            }
        }
    }
}
