using JJ.Persistence.Synthesizer.Memory.Helpers;
using JJ.Framework.Persistence.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Mappings
{
    public class SampleMapping : MemoryMapping<Sample>
    {
        public SampleMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = PropertyNames.ID;
        }
    }
}
