using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories
{
    public class ChannelRepository : RepositoryBase<Channel, int>, IChannelRepository
    {
        public ChannelRepository(IContext context)
            : base(context)
        { }

        /// <summary>
        /// Does not get the related entities immediately unless you override it in a specialized repository.
        /// </summary>
        public virtual Channel GetWithRelatedEntities(int id)
        {
            return Get(id);
        }
    }
}
