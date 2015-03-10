using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Persistence.Synthesizer.Memory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Repositories
{
    public class ChannelRepository : JJ.Persistence.Synthesizer.DefaultRepositories.ChannelRepository
    {
        private static readonly object _lock = new object();

        public ChannelRepository(IContext context)
            : base(context)
        {
            Channel entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            lock (_lock)
            {
                entity = TryGet(1);
                if (entity == null)
                {
                    entity = Create();
                    entity.Name = "Single";
                    entity.Index = 0;
                }
                
                entity = TryGet(2);
                if (entity == null)
                {
                    entity = Create();
                    entity.Name = "Left";
                    entity.Index = 0;
                }
                
                entity = TryGet(3);
                if (entity == null)
                {
                    entity = Create();
                    entity.Name = "Right";
                    entity.Index = 1;
                }
            }
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