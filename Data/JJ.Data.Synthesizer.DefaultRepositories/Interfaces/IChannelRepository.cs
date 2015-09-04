using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IChannelRepository : IRepository<Channel, int>
    {
        Channel GetWithRelatedEntities(int id);
    }
}
