using JJ.Data.Synthesizer.Memory.Helpers;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class SampleDataTypeMapping : MemoryMapping<SampleDataType>
    {
        public SampleDataTypeMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = PropertyNames.ID;
        }
    }
}
