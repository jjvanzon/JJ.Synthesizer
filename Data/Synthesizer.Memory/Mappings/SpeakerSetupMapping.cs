using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class SpeakerSetupMapping : MemoryMapping<SpeakerSetup>
    {
        public SpeakerSetupMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(SpeakerSetup.ID);
        }
    }
}
