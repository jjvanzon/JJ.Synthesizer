using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class DocumentMapping : MemoryMapping<Document>
    {
        public DocumentMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Document.ID);
        }
    }
}
