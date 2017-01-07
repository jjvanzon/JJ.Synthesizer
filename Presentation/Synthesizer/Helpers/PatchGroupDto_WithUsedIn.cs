using System.Collections.Generic;
using JJ.Business.Synthesizer.Dtos;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Helpers
{
    public class PatchGroupDto_WithUsedIn
    {
        public string GroupName { get; set; }
        public IList<UsedInDto<Patch>> PatchUsedInDtos { get; set; }
    }
}
