using JJ.Framework.Data.Memory;
using JJ.Data.Synthesizer.Memory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
