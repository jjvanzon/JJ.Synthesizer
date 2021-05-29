using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Dto
{
    public class PatchGroupDto
    {
        public string FriendlyGroupName { get; set; }
        public string CanonicalGroupName { get; set; }
        public IList<Patch> Patches { get; set; }
    }
}
