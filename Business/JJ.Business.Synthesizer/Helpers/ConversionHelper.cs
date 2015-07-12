using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class ConversionHelper
    {
        public static int? ParseNullableInt32(string input)
        {
            if (String.IsNullOrEmpty(input)) return null;

            return Int32.Parse(input);
        }
    }
}
