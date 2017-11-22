using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;
using JJ.Framework.Exceptions;

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
				CreateRepository<IDocumentRepository>(context),
				CreateRepository<ICurveRepository>(context),
				CreateRepository<IPatchRepository>(context),
				CreateRepository<ISampleRepository>(context),
				CreateRepository<IAudioFileOutputRepository>(context),
				CreateRepository<IAudioOutputRepository>(context),
				CreateRepository<IDocumentReferenceRepository>(context),
				CreateRepository<INodeRepository>(context),
				CreateRepository<IOperatorRepository>(context),
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

		public static CurveRepositories CreateCurveRepositories(IContext context)
		{
			return new CurveRepositories(CreateRepositories(context));
		}
	}
}
