using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class InterpolationTypeMapping : MemoryMapping<InterpolationType>
    {
        public InterpolationTypeMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(InterpolationType.ID);
        }
    }
}
