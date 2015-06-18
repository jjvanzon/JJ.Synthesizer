using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class WinFormsThemeHelper
    {
        // This class is not used yet.

        static WinFormsThemeHelper()
        {
            DefaultFont = new Font("Verdana", 12);
            DefaultSpacing = 4;
        }

        public static Font DefaultFont { get; private set; }
        public static int DefaultSpacing { get; private set; }
    }
}