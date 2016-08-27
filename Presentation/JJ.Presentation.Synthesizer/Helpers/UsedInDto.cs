using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.Helpers
{
    public class UsedInDto
    {
        public IDAndName EntityIDAndName { get; set; }
        public IList<IDAndName> UsedInIDAndNames { get; set; }
    }
}
