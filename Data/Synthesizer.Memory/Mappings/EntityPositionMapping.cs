using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class EntityPositionMapping : MemoryMapping<EntityPosition>
    {
        public EntityPositionMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = nameof(EntityPosition.ID);
        }
    }
}
