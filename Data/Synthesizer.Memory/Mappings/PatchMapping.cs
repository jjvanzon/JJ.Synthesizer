using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class PatchMapping : MemoryMapping<Patch>
    {
        public PatchMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Patch.ID);
        }
    }
}
