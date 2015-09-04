using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Configuration;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Framework.Data.Memory;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class PersistenceHelper
    {
        private static ConfigurationSection _config;

        static PersistenceHelper()
        {
            _config = CustomConfigurationManager.GetSection<ConfigurationSection>();
        }

        public static IContext CreateMemoryContext()
        {
            return ContextFactory.CreateContextFromConfiguration(_config.MemoryPersistence);
        }

        public static IContext CreateDatabaseContext()
        {
            return ContextFactory.CreateContextFromConfiguration(_config.DatabasePersistence);
        }

        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
        {
            if (context is MemoryContext)
            {
                return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context, _config.MemoryPersistence);
            }

            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context, _config.DatabasePersistence);
        }

        public static RepositoryWrapper CreateRepositoryWrapper(IContext context)
        {
            if (context == null) throw new NullException(() => context);

            var repositoryWrapper = new RepositoryWrapper
            (
                PersistenceHelper.CreateRepository<IDocumentRepository>(context),
                PersistenceHelper.CreateRepository<ICurveRepository>(context),
                PersistenceHelper.CreateRepository<IPatchRepository>(context),
                PersistenceHelper.CreateRepository<ISampleRepository>(context),
                PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(context),
                PersistenceHelper.CreateRepository<IDocumentReferenceRepository>(context),
                PersistenceHelper.CreateRepository<INodeRepository>(context),
                PersistenceHelper.CreateRepository<IAudioFileOutputChannelRepository>(context),
                PersistenceHelper.CreateRepository<IOperatorRepository>(context),
                PersistenceHelper.CreateRepository<IOperatorTypeRepository>(context),
                PersistenceHelper.CreateRepository<IInletRepository>(context),
                PersistenceHelper.CreateRepository<IOutletRepository>(context),
                PersistenceHelper.CreateRepository<IEntityPositionRepository>(context),
                PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(context),
                PersistenceHelper.CreateRepository<IInterpolationTypeRepository>(context),
                PersistenceHelper.CreateRepository<INodeTypeRepository>(context),
                PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(context),
                PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(context),
                PersistenceHelper.CreateRepository<IChildDocumentTypeRepository>(context),
                PersistenceHelper.CreateRepository<IIDRepository>(context)
            );

            return repositoryWrapper;
        }
    }
}
