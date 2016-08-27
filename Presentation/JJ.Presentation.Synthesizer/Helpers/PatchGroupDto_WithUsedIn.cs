using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Helpers
{
    public class PatchGroupDto_WithUsedIn
    {
        public string GroupName { get; set; }
        public IList<UsedInDto<Patch>> PatchUsedInDtos { get; set; }
    }
}
