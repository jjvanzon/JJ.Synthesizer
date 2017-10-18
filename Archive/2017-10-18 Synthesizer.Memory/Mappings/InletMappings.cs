using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class InletMapping : MemoryMapping<Inlet>
    {
        public InletMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Inlet.ID);
        }
    }
}