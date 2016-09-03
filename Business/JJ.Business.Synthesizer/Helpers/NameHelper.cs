using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class NameHelper
    {
        public static string ToCanonicalForm(string name)
        {
            name = name ?? "";

            name = name.ToLower();

            // TODO: Accent-insensitivity?

            return name;
        }
    }
}
