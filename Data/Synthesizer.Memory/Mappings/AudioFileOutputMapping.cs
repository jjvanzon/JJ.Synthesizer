using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Memory.Helpers;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class AudioFileOutputMapping : MemoryMapping<AudioFileOutput>
    {
        public AudioFileOutputMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = PropertyNames.ID;
        }
    }
}
