using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Dto
{
    public class DocumentReferencePatchGroupDto
    {
        public DocumentReference LowerDocumentReference { get; set; }
        public IList<PatchGroupDto> Groups { get; set; }
    }
}