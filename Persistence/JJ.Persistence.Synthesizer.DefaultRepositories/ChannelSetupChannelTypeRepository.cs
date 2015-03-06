using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories
{
    public class ChannelSetupChannelTypeRepository : RepositoryBase<ChannelSetupChannelType, int>, IChannelSetupChannelTypeRepository
    {
        public ChannelSetupChannelTypeRepository(IContext context)
            : base(context)
        { }
    }
}
