using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    public class DocumentReferencePatchGroupDto
    {
        public IList<PatchGroupDto> Groups { get; set; }
        public int DocumentReferenceID { get; set; }
    }
}