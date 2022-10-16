using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

public enum TextAlign
{
    LEFT,
    RIGHT,
    CENTER,
    DEFAULT
}

namespace CSharpTextEditor
{
    class CustomFontDialog : FontDialog
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            internal int left;
            internal int top;
            internal int right;
            internal int bottom;
        }

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        private const int WM_INITDIALOG = 0x0110;
        private const int SWP_NOMOVE = 0x0002;

        private const int comboboxWidth = 120;
        private const int comboboxHeight = 30;

        private TextAlign textAlignInternal = TextAlign.DEFAULT;
        public TextAlign textAlign
        {
            get => textAlignInternal;
        }


        private ComboBox textAlignCombobox = new ComboBox();
        private Label textAlignLabel = new Label();

        public CustomFontDialog()
        {
            textAlignCombobox.Items.Add("Ляво");
            textAlignCombobox.Items.Add("Център");
            textAlignCombobox.Items.Add("Дясно");
            textAlignCombobox.Items.Add("По подразбиране");

            textAlignCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
            textAlignCombobox.SelectedIndexChanged += TextAlignComboBox_SelectedIndexChanged;
            textAlignLabel.Text = "Подравняване:";
        }


        protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {

                case WM_INITDIALOG:
                    RECT clientRect;
                    GetClientRect(hWnd, out clientRect);
                    SetWindowPos(hWnd, 0, 0, 0, clientRect.right + comboboxWidth + 5, clientRect.bottom, SWP_NOMOVE);
                    GetClientRect(hWnd, out clientRect);

                    textAlignCombobox.Location = new Point(clientRect.right - comboboxWidth, 53);
                    textAlignCombobox.Size = new Size(comboboxWidth, comboboxHeight);
                    SetParent(textAlignCombobox.Handle, hWnd);

                    textAlignLabel.Location = new Point(clientRect.right - comboboxWidth, 27);
                    textAlignLabel.Size = new Size(comboboxWidth, 12);
                    SetParent(textAlignLabel.Handle, hWnd);

                    break;
            }

            return base.HookProc(hWnd, msg, wParam, lParam);
        }

        private void TextAlignComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (textAlignCombobox.SelectedIndex)
            {
                case 0: textAlignInternal = TextAlign.LEFT; break;
                case 1: textAlignInternal = TextAlign.CENTER; break;
                case 2: textAlignInternal = TextAlign.RIGHT; break;
                case 3: textAlignInternal = TextAlign.DEFAULT; break;
            }
        }

    }
}
