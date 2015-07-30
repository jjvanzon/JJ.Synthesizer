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
    public class SpeakerSetupChannelRepository : JJ.Data.Synthesizer.DefaultRepositories.SpeakerSetupChannelRepository
    {
        private IList<SpeakerSetupChannel> _list = new List<SpeakerSetupChannel>();

        public SpeakerSetupChannelRepository(IContext context)
            : base(context)
        {
            ISpeakerSetupRepository speakerSetupRepository = RepositoryHelper.GetSpeakerSetupRepository(context);
            IChannelRepository channelRepository = RepositoryHelper.GetChannelRepository(context);

            int id = 1;

            SpeakerSetupChannel entity;

            entity = new SpeakerSetupChannel();
            entity.ID = id++;
            entity.SpeakerSetup = speakerSetupRepository.Get(EntityIDs.ENTITY_ID_SPEAKER_SETUP_MONO);
            entity.Channel = channelRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_SINGLE);
            Insert(entity);
            _list.Add(entity);

            entity = new SpeakerSetupChannel();
            entity.ID = id++;
            entity.SpeakerSetup = speakerSetupRepository.Get(EntityIDs.ENTITY_ID_SPEAKER_SETUP_STEREO);
            entity.Channel = channelRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_LEFT);
            Insert(entity);
            _list.Add(entity);

            entity = new SpeakerSetupChannel();
            entity.ID = id++;
            entity.SpeakerSetup = speakerSetupRepository.Get(EntityIDs.ENTITY_ID_SPEAKER_SETUP_STEREO);
            entity.Channel = channelRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_RIGHT);
            Insert(entity);
            _list.Add(entity);
        }
    }
}