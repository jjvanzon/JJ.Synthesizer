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
    public class SpeakerSetupRepository : JJ.Data.Synthesizer.DefaultRepositories.SpeakerSetupRepository
    {
        public SpeakerSetupRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Mono");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Stereo");
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