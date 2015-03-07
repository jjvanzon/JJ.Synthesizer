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

        private static IChannelSetupRepository _channelSetupRepository;
        private static IChannelTypeRepository _channelTypeRepository;
        private static IChannelSetupChannelTypeRepository _channelSetupChannelTypeRepository;

        public static IChannelSetupRepository GetChannelSetupRepository(IContext context)
        {
            if (_channelSetupRepository == null)
            {
                _channelSetupRepository = new ChannelSetupRepository(context);
            }
            return _channelSetupRepository;
        }

        public static IChannelTypeRepository GetChannelTypeRepository(IContext context)
        {
            if (_channelTypeRepository == null)
            {
                _channelTypeRepository = new ChannelTypeRepository(context);
            }
            return _channelTypeRepository;
        }

        public static IChannelSetupChannelTypeRepository GetChannelSetupChannelTypeRepository(IContext context)
        {
            if (_channelSetupChannelTypeRepository == null)
            {
                _channelSetupChannelTypeRepository = new ChannelSetupChannelTypeRepository(context);
            }
            return _channelSetupChannelTypeRepository;
        }
    }
}
