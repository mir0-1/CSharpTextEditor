using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTextEditor
{
    public class UnitConverter
    {
        const float mmPerInch = 25.4f;

        public static decimal PixelsToMM(int pixel, float dpi)
        {
            return (decimal)Math.Round(((float)pixel / dpi) * mmPerInch);
        }

        public static decimal MMToPixels(decimal mm, float dpi)
        {
            return (int)Math.Round(((float)mm / mmPerInch) * dpi);
        }
    }
}
