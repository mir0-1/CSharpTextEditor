using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    public class CustomWebBrowser : WebBrowser
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int VK_SPACE = 0x20;
        private const int VK_LEFT = 0x25;
        private const int VK_UP = 0x26;
        private const int VK_RIGHT = 0x27;
        private const int VK_DOWN = 0x28;

        public override bool PreProcessMessage(ref System.Windows.Forms.Message msg)
        {
            if (msg.Msg == WM_KEYDOWN)
                if (msg.WParam == (IntPtr)VK_SPACE ||
                    msg.WParam == (IntPtr)VK_LEFT ||
                    msg.WParam == (IntPtr)VK_UP ||
                    msg.WParam == (IntPtr)VK_RIGHT ||
                    msg.WParam == (IntPtr)VK_DOWN)
                    return true;

            return base.PreProcessMessage(ref msg);
        }
    }
}
