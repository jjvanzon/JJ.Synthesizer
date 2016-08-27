using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Helpers
{
    public class PatchGroupDto
    {
        public string GroupName { get; set; }
        public IList<Patch> Patches { get; set; }
    }
}
