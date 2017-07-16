using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class ScaleTypeMapping : MemoryMapping<ScaleType>
    {
        public ScaleTypeMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(ScaleType.ID);
        }
    }
}
