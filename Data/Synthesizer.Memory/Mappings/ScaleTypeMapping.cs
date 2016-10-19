using JJ.Data.Synthesizer.Memory.Helpers;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class ScaleTypeMapping : MemoryMapping<ScaleType>
    {
        public ScaleTypeMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = PropertyNames.ID;
        }
    }
}
