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
            _defaultFont = new Font("Verdana", 12);
        }

        private static Font _defaultFont;
        public static Font DefaultFont
        {
            get { return _defaultFont; }
        }
    }
}