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
    public class SpeakerSetupRepository : JJ.Persistence.Synthesizer.DefaultRepositories.SpeakerSetupRepository
    {
        private static readonly object _lock = new object();

        public SpeakerSetupRepository(IContext context)
            : base(context)
        {
            SpeakerSetup entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs
            
            lock (_lock)
            {
                entity = TryGet(1);
                if (entity == null)
                {
                    entity = Create();
                    entity.Name = "Mono";
                }

                entity = TryGet(2);
                if (entity == null)
                {
                    entity = Create();
                    entity.Name = "Stereo";
                }
            }
        }

        public override SpeakerSetup GetWithRelatedEntities(int id)
        {
            SpeakerSetup entity = Get(id);

            ISpeakerSetupChannelRepository childRepository = RepositoryHelper.GetSpeakerSetupChannelRepository(_context);
            entity.SpeakerSetupChannels = childRepository.GetAll().Where(x => x.SpeakerSetup.ID == id).ToList();

            return entity;
        }
    }
}