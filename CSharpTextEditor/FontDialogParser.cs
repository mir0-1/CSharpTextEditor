using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

/*
 * This application uses iText. iText Copyright notice:
 * 
 Copyright (c) 1998-2022 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.
This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS
This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/
The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.
In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.
You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.
For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
 */

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
            return String.Format("color=#{0:x2}{1:x2}{2:x2}", color.R, color.G, color.B);
        }

        private static string TextAlignAsHTML(TextAlign textAlign)
        {
            switch (textAlign)
            {
                case TextAlign.DEFAULT: return null;
                case TextAlign.CENTER: return "text-align: center; width: 100%; display:block";
                case TextAlign.LEFT: return "text-align: left; width: 100%; display:block";
                case TextAlign.RIGHT: return "text-align: right; width: 100%; display:block";
            }

            return null;
        }

        public static string GetFormattedHTMLString(CustomFontDialog fontDialog, string target)
        {
            string textAlign = TextAlignAsHTML(fontDialog.textAlign);

            string style = "style=\"font-family:" + fontDialog.Font.Name + ";" +
                            FontStyleAsHTML(fontDialog.Font.Style) +
                            ColorAsHTML(fontDialog.Color) + ";" +
                            "font-size: " + fontDialog.Font.SizeInPoints + "pt;";

            if (textAlign != null)
                style += textAlign;

            style += ";\"";

            return "<span " + style + ">" + target + "</span>";
        }
    }
}
