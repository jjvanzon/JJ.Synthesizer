using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IChannelSetupRepository : IRepository<ChannelSetup, int>
    {
        ChannelSetup GetWithRelatedEntities(int id);
    }
}
