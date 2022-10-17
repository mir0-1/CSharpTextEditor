using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CSharpTextEditor
{
    class ClipboardHTMLFilter
    {
        private string filter = null;

        public ClipboardHTMLFilter()
        {
        }

        public ClipboardHTMLFilter(string regexFilter)
        {
            filter = regexFilter;
        }

        public void SetFilter(string regexFilter)
        {
            filter = regexFilter;
        }

        public string GetFilteredContent()
        {
            string pasted;
            if (filter == null)
                return Clipboard.GetText(TextDataFormat.Text);

            pasted = Clipboard.GetText(TextDataFormat.Html);
            Match prohibited = Regex.Match(pasted, filter, RegexOptions.Singleline);

            if (prohibited.Success)
                return Clipboard.GetText(TextDataFormat.Text);

            Match result = Regex.Match(pasted, "<!--StartFragment-->(.*)<!--EndFragment-->", RegexOptions.Singleline);

            return result.Value;
        }
    }
}
