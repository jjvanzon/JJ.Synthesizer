using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class SampleMapping : MemoryMapping<Sample>
    {
        public SampleMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(Sample.ID);
        }
    }
}
