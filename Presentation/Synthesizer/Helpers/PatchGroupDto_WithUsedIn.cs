using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Presentation.Synthesizer.Helpers
{
    public class PatchGroupDto_WithUsedIn
    {
        public string GroupName { get; set; }
        public IList<UsedInDto<Patch>> PatchUsedInDtos { get; set; }
    }
}
