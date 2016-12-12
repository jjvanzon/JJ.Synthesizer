using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Memory.Repositories;
using System.Reflection;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions;

namespace JJ.Data.Synthesizer.Memory.Helpers
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

        public static TEntity EnsureEnumEntity<TEntity>(RepositoryBase<TEntity, int> repository, int id, string name)
            where TEntity : class, new()
        {
            if (repository == null) throw new NullException(() => repository);

            TEntity entity = repository.TryGet(id);

            if (entity == null)
            {
                entity = new TEntity();

                PropertyInfo idProperty = typeof(TEntity).GetProperty(PropertyNames.ID);
                if (idProperty == null)
                {
                    throw new PropertyNotFoundException<TEntity>(PropertyNames.ID);
                }

                PropertyInfo nameProperty = typeof(TEntity).GetProperty(PropertyNames.Name);
                if (nameProperty == null)
                {
                    throw new PropertyNotFoundException<TEntity>(PropertyNames.Name);
                }

                idProperty.SetValue(entity, id);
                nameProperty.SetValue(entity, name);

                repository.Insert(entity);
            }

            return entity;
        }
    }
}