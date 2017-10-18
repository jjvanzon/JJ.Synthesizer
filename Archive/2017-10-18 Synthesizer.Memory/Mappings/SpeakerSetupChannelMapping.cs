using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class SpeakerSetupChannelMapping : MemoryMapping<SpeakerSetupChannel>
    {
        public SpeakerSetupChannelMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(SpeakerSetupChannel.ID);
        }
    }
}
