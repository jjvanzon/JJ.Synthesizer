using JJ.Data.Synthesizer.Memory.Helpers;
using JJ.Framework.Data.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class ChannelMapping : MemoryMapping<Channel>
    {
        public ChannelMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = PropertyNames.ID;
        }
    }
}
