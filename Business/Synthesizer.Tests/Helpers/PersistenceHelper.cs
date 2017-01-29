using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Configuration;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Framework.Data.Memory;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class PersistenceHelper
    {
        private static readonly ConfigurationSection _config;

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

        public static RepositoryWrapper CreateRepositories(IContext context)
        {
            if (context == null) throw new NullException(() => context);

            var repositories = new RepositoryWrapper
            (
                CreateRepository<IDocumentRepository>(context),
                CreateRepository<ICurveRepository>(context),
                CreateRepository<IPatchRepository>(context),
                CreateRepository<ISampleRepository>(context),
                CreateRepository<IAudioFileOutputRepository>(context),
                CreateRepository<IAudioOutputRepository>(context),
                CreateRepository<IDocumentReferenceRepository>(context),
                CreateRepository<INodeRepository>(context),
                CreateRepository<IOperatorRepository>(context),
                CreateRepository<IOperatorTypeRepository>(context),
                CreateRepository<IInletRepository>(context),
                CreateRepository<IOutletRepository>(context),
                CreateRepository<IScaleRepository>(context),
                CreateRepository<IToneRepository>(context),

                CreateRepository<IEntityPositionRepository>(context),

                CreateRepository<IAudioFileFormatRepository>(context),
                CreateRepository<IInterpolationTypeRepository>(context),
                CreateRepository<INodeTypeRepository>(context),
                CreateRepository<ISampleDataTypeRepository>(context),
                CreateRepository<ISpeakerSetupRepository>(context),
                CreateRepository<IScaleTypeRepository>(context),
                CreateRepository<IDimensionRepository>(context),

                CreateRepository<IIDRepository>(context)
            );

            return repositories;
        }
    }
}
