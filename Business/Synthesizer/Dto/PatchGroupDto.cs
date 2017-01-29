using System.Collections.Generic;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Dto
{
    public class PatchGroupDto
    {
        public string GroupName { get; set; }
        public IList<Patch> Patches { get; set; }
    }
}
