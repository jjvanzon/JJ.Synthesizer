using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.OneOff.Synthesizer.DataMigration
{
    internal class PersistenceHelper
    {
        public static IContext CreateContext()
        {
            return ContextFactory.CreateContextFromConfiguration();
        }

        public static TRepository CreateRepository<TRepository>(IContext context)
        {
            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepository>(context);
        }
        
        public static RepositoryWrapper CreateRepositoryWrapper(IContext context)
        {
            if (context == null) throw new NullException(() => context);

            var repositoryWrapper = new RepositoryWrapper
            (
                CreateRepository<IDocumentRepository>(context),
                CreateRepository<ICurveRepository>(context),
                CreateRepository<IPatchRepository>(context),
                CreateRepository<ISampleRepository>(context),
                CreateRepository<IAudioFileOutputRepository>(context),
                CreateRepository<IDocumentReferenceRepository>(context),
                CreateRepository<INodeRepository>(context),
                CreateRepository<IAudioFileOutputChannelRepository>(context),
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
                CreateRepository<IChildDocumentTypeRepository>(context),
                CreateRepository<IScaleTypeRepository>(context),

                CreateRepository<IIDRepository>(context)
            );

            return repositoryWrapper;
        }
    }
}