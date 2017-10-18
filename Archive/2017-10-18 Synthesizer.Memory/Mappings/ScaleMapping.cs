using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class ScaleMapping : MemoryMapping<Scale>
    {
        public ScaleMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Scale.ID);
        }
    }
}
