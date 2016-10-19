using JJ.Framework.Data.Memory;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class EntityPositionMapping : MemoryMapping<EntityPosition>
    {
        public EntityPositionMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = PropertyNames.ID;
        }
    }
}
