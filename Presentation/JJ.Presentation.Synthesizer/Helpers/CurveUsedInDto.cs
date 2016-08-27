using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;

namespace JJ.Presentation.Synthesizer.Helpers
{
    public class CurveUsedInDto
    {
        public Curve Curve { get; set; }
        public IList<IDAndName> UsedIn { get; set; }
    }
}
