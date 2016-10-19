using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Memory.Helpers;
using System.Linq;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class ChannelRepository : DefaultRepositories.ChannelRepository
    {
        public ChannelRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Single");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Left");
            RepositoryHelper.EnsureEnumEntity(this, 3, "Right");
        }

        public override Channel GetWithRelatedEntities(int id)
        {
            Channel entity = Get(id);

            ISpeakerSetupChannelRepository childRepository = RepositoryHelper.GetSpeakerSetupChannelRepository(_context);
            entity.SpeakerSetupChannels = childRepository.GetAll().Where(x => x.Channel.ID == id).ToList();

            return entity;
        }
    }
}