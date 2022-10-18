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
        private const int WM_CHAR = 0x0102;
        private const int WM_KEYDOWN = 0x0100;
        private const int VK_SPACE = 0x20;


        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_CHAR:
                    MessageBox.Show("");
                    return;
            }


            base.WndProc(ref m);
        }

        public override bool PreProcessMessage(ref System.Windows.Forms.Message msg)
        {
            if (msg.Msg == WM_KEYDOWN)
                if (msg.WParam == (IntPtr)VK_SPACE)
                {
                    base.WndProc(ref msg);
                    return true;
                }

            return base.PreProcessMessage(ref msg);
        }
    }
}
