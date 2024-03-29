﻿using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
    public interface IChannelRepository : IRepository<Channel, int>
    {
        // ReSharper disable once UnusedMember.Global
        Channel GetWithRelatedEntities(int id);
    }
}
