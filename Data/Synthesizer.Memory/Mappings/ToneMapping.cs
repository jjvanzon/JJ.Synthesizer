using JJ.Data.Synthesizer.Memory.Helpers;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class ToneMapping : MemoryMapping<Tone>
    {
        public ToneMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = PropertyNames.ID;
        }
    }
}
