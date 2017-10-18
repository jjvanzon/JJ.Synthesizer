using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class DocumentReferenceMapping : MemoryMapping<DocumentReference>
    {
        public DocumentReferenceMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(DocumentReference.ID);
        }
    }
}
