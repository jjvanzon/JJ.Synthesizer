using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Persistence.Synthesizer.Memory.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Helpers
{
    internal static class RepositoryHelper
    {
        // TODO: Is it dangerous that you basically ignore the context here?

        private static ISpeakerSetupRepository _speakerSetupRepository;
        private static IChannelRepository _channelRepository;
        private static ISpeakerSetupChannelRepository _speakerSetupChannelRepository;

        public static ISpeakerSetupRepository GetSpeakerSetupRepository(IContext context)
        {
            if (_speakerSetupRepository == null)
            {
                _speakerSetupRepository = new SpeakerSetupRepository(context);
            }
            return _speakerSetupRepository;
        }

        public static IChannelRepository GetChannelRepository(IContext context)
        {
            if (_channelRepository == null)
            {
                _channelRepository = new ChannelRepository(context);
            }
            return _channelRepository;
        }

        public static ISpeakerSetupChannelRepository GetSpeakerSetupChannelRepository(IContext context)
        {
            if (_speakerSetupChannelRepository == null)
            {
                _speakerSetupChannelRepository = new SpeakerSetupChannelRepository(context);
            }
            return _speakerSetupChannelRepository;
        }
    }
}
