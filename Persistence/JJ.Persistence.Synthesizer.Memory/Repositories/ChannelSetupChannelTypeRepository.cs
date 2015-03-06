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
    public class ChannelSetupChannelTypeRepository : JJ.Persistence.Synthesizer.DefaultRepositories.ChannelSetupChannelTypeRepository
    {
        public ChannelSetupChannelTypeRepository(IContext context)
            : base(context)
        {
            IChannelSetupRepository channelSetupRepository = new ChannelSetupRepository(context);
            IChannelTypeRepository channelTypeRepository = new ChannelTypeRepository(context);

            ChannelSetupChannelType entity;

            entity = Create();
            entity.ChannelSetup = channelSetupRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_SETUP_MONO);
            entity.ChannelType = channelTypeRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_TYPE_SINGLE);

            entity = Create();
            entity.ChannelSetup = channelSetupRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_SETUP_STEREO);
            entity.ChannelType = channelTypeRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_TYPE_LEFT);

            entity = Create();
            entity.ChannelSetup = channelSetupRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_SETUP_STEREO);
            entity.ChannelType = channelTypeRepository.Get(EntityIDs.ENTITY_ID_CHANNEL_TYPE_RIGHT);
        }
    }
}