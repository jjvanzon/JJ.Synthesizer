using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class AudioFileFormatMapping : MemoryMapping<AudioFileFormat>
    {
        public AudioFileFormatMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(AudioFileFormat.ID);
        }
    }
}
