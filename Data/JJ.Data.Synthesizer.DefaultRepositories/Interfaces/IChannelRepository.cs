using JJ.Framework.Data;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IChannelRepository : IRepository<Channel, int>
    {
        Channel GetWithRelatedEntities(int id);
    }
}
