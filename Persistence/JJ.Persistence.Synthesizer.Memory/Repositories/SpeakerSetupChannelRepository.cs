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
    public class SpeakerSetupChannelRepository : JJ.Persistence.Synthesizer.DefaultRepositories.SpeakerSetupChannelRepository
    {
        private IList<SpeakerSetupChannel> _list = new List<SpeakerSetupChannel>();

        public SpeakerSetupChannelRepository(IContext context)
            : base(context)
        {
            ISpeakerSetupRepository speakerSetupRepository = RepositoryHelper.GetSpeakerSetupRepository(context);
            IChannelRepository channelRepository = RepositoryHelper.GetChannelRepository(context);

            SpeakerSetupChannel entity;

            entity = Create();
            entity.SpeakerSetup = speakerSetupRepository.Get(EntityIDs.ENTITY_ID_SPEAKER_SETUP_MONO);
            entity.Channel = channelRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_SINGLE);
            _list.Add(entity);

            entity = Create();
            entity.SpeakerSetup = speakerSetupRepository.Get(EntityIDs.ENTITY_ID_SPEAKER_SETUP_STEREO);
            entity.Channel = channelRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_LEFT);
            _list.Add(entity);

            entity = Create();
            entity.SpeakerSetup = speakerSetupRepository.Get(EntityIDs.ENTITY_ID_SPEAKER_SETUP_STEREO);
            entity.Channel = channelRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_RIGHT);
            _list.Add(entity);
        }
    }
}