using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
