using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace CSharpTextEditor
{
    class FontDialogParser
    {
        private static string FontStyleAsHTML(FontStyle style)
        {
            StringBuilder result = new StringBuilder();
            bool underlineApplied = false;

            if (style.HasFlag(FontStyle.Bold))
                result.Append("font-weight: bold;");
            if (style.HasFlag(FontStyle.Italic))
                result.Append("font-style: italic;");
            if (style.HasFlag(FontStyle.Underline))
            {
                result.Append("text-decoration: underline");
                underlineApplied = true;
            }
            if (style.HasFlag(FontStyle.Strikeout))
            {
                if (underlineApplied)
                    result.Append(" line-through");

                else
                    result.Append("text-decoration: line-through;");
            }

            if (underlineApplied)
                result.Append(";");

            return result.ToString();
        }

        private static string ColorAsHTML(Color color)
        {
            return String.Format("color=#{0:x2}{1:x2}{2:x2};", color.R, color.G, color.B);
        }

        public static string GetFormattedHTMLString(FontDialog fontDialog, string target)
        {

            string style = "style=\"font-family:" + fontDialog.Font.Name + ";" +
                            FontStyleAsHTML(fontDialog.Font.Style) +
                            ColorAsHTML(fontDialog.Color) + "\"";

            return "<span " + style + ">" + target + "</span>";
        }
    }
}
