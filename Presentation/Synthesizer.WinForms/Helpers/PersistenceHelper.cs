using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class PersistenceHelper
    {
        public static IContext CreateContext() => ContextFactory.CreateContextFromConfiguration();

        private static TRepository CreateRepository<TRepository>(IContext context)
            => RepositoryFactory.CreateRepositoryFromConfiguration<TRepository>(context);

        public static RepositoryWrapper CreateRepositoryWrapper(IContext context)
        {
            if (context == null) throw new NullException(() => context);

            var repositories = new RepositoryWrapper(
                CreateRepository<IAudioFileFormatRepository>(context),
                CreateRepository<IAudioFileOutputRepository>(context),
                CreateRepository<IAudioOutputRepository>(context),
                CreateRepository<ICurveRepository>(context),
                CreateRepository<IDimensionRepository>(context),
                CreateRepository<IDocumentReferenceRepository>(context),
                CreateRepository<IDocumentRepository>(context),
                CreateRepository<IEntityPositionRepository>(context),
                CreateRepository<IIDRepository>(context),
                CreateRepository<IInletRepository>(context),
                CreateRepository<IInterpolationTypeRepository>(context),
                CreateRepository<IMidiMappingRepository>(context),
                CreateRepository<IMidiMappingGroupRepository>(context),
                CreateRepository<IMidiMappingTypeRepository>(context),
                CreateRepository<INodeRepository>(context),
                CreateRepository<IOperatorRepository>(context),
                CreateRepository<IOutletRepository>(context),
                CreateRepository<IPatchRepository>(context),
                CreateRepository<ISampleDataTypeRepository>(context),
                CreateRepository<ISampleRepository>(context),
                CreateRepository<IScaleRepository>(context),
                CreateRepository<IScaleTypeRepository>(context),
                CreateRepository<ISpeakerSetupRepository>(context),
                CreateRepository<IToneRepository>(context)
            );

            return repositories;
        }
    }
}