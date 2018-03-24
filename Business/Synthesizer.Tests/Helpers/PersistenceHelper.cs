using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
	internal static class PersistenceHelper
	{
		public static IContext CreateContext()
		{
			return ContextFactory.CreateContextFromConfiguration();
		}

		public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
		{
			return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context);
		}

		public static RepositoryWrapper CreateRepositories(IContext context)
		{
			if (context == null) throw new NullException(() => context);

			var repositories = new RepositoryWrapper
			(
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
				CreateRepository<IMidiMappingElementRepository>(context),
				CreateRepository<IMidiMappingRepository>(context),
				CreateRepository<INodeRepository>(context),
				CreateRepository<INodeTypeRepository>(context),
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

		public static CurveRepositories CreateCurveRepositories(IContext context)
		{
			return new CurveRepositories(CreateRepositories(context));
		}
	}
}
