using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Memory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class ChannelRepository : JJ.Data.Synthesizer.DefaultRepositories.ChannelRepository
    {
        public ChannelRepository(IContext context)
            : base(context)
        {
            Channel entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            entity = Create();
            entity.Name = "Single";

            entity = Create();
            entity.Name = "Left";

            entity = Create();
            entity.Name = "Right";
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