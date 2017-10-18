using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class DimensionMapping : MemoryMapping<Dimension>
    {
        public DimensionMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Dimension.ID);
        }
    }
}
