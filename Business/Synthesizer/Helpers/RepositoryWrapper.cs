using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
	public class RepositoryWrapper
	{
		public IDocumentRepository DocumentRepository { get; }
		public ICurveRepository CurveRepository { get; }
		public IPatchRepository PatchRepository { get; }
		public ISampleRepository SampleRepository { get; }
		public IAudioFileOutputRepository AudioFileOutputRepository { get; }
		public IAudioOutputRepository AudioOutputRepository { get; }
		public IDocumentReferenceRepository DocumentReferenceRepository { get; }
		public INodeRepository NodeRepository { get; }
		public IOperatorRepository OperatorRepository { get; }
		public IInletRepository InletRepository { get; }
		public IOutletRepository OutletRepository { get; }
		public IScaleRepository ScaleRepository { get; }
		public IToneRepository ToneRepository { get; }
		public IEntityPositionRepository EntityPositionRepository { get; }

		// Enum-like entities
		public IAudioFileFormatRepository AudioFileFormatRepository { get; }
		public IInterpolationTypeRepository InterpolationTypeRepository { get; }
		public INodeTypeRepository NodeTypeRepository { get; }
		public ISampleDataTypeRepository SampleDataTypeRepository { get; }
		public ISpeakerSetupRepository SpeakerSetupRepository { get; }
		public IScaleTypeRepository ScaleTypeRepository { get; }
		public IDimensionRepository DimensionRepository { get; }

		public IIDRepository IDRepository { get; }

		public RepositoryWrapper(
			IDocumentRepository documentRepository,
			ICurveRepository curveRepository,
			IPatchRepository patchRepository,
			ISampleRepository sampleRepository,
			IAudioFileOutputRepository audioFileOutputRepository,
			IAudioOutputRepository audioOutputRepository,
			IDocumentReferenceRepository documentReferenceRepository,
			INodeRepository nodeRepository,
			IOperatorRepository operatorRepository,
			IInletRepository inletRepository,
			IOutletRepository outletRepository,
			IScaleRepository scaleRepository,
			IToneRepository toneRepository,

			IEntityPositionRepository entityPositionRepository,

			// Enum-like entities
			IAudioFileFormatRepository audioFileFormatRepository,
			IInterpolationTypeRepository interpolationTypeRepository,
			INodeTypeRepository nodeTypeRepository,
			ISampleDataTypeRepository sampleDataTypeRepository,
			ISpeakerSetupRepository speakerSetupRepository,
			IScaleTypeRepository scaleTypeRepository,
			IDimensionRepository dimensionRepository,

			IIDRepository idRepository)
		{
			DocumentRepository = documentRepository ?? throw new NullException(() => documentRepository);
			CurveRepository = curveRepository ?? throw new NullException(() => curveRepository);
			PatchRepository = patchRepository ?? throw new NullException(() => patchRepository);
			SampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
			AudioFileOutputRepository = audioFileOutputRepository ?? throw new NullException(() => audioFileOutputRepository);
			AudioOutputRepository = audioOutputRepository ?? throw new NullException(() => audioOutputRepository);
			DocumentReferenceRepository = documentReferenceRepository ?? throw new NullException(() => documentReferenceRepository);
			NodeRepository = nodeRepository ?? throw new NullException(() => nodeRepository);
			OperatorRepository = operatorRepository ?? throw new NullException(() => operatorRepository);
			InletRepository = inletRepository ?? throw new NullException(() => inletRepository);
			OutletRepository = outletRepository ?? throw new NullException(() => outletRepository);
			EntityPositionRepository = entityPositionRepository ?? throw new NullException(() => entityPositionRepository);
			ScaleRepository = scaleRepository ?? throw new NullException(() => scaleRepository);
			ToneRepository = toneRepository ?? throw new NullException(() => toneRepository);

			// Enum-like entities
			AudioFileFormatRepository = audioFileFormatRepository ?? throw new NullException(() => audioFileFormatRepository);
			InterpolationTypeRepository = interpolationTypeRepository ?? throw new NullException(() => interpolationTypeRepository);
			NodeTypeRepository = nodeTypeRepository ?? throw new NullException(() => nodeTypeRepository);
			SampleDataTypeRepository = sampleDataTypeRepository ?? throw new NullException(() => sampleDataTypeRepository);
			SpeakerSetupRepository = speakerSetupRepository ?? throw new NullException(() => speakerSetupRepository);
			ScaleTypeRepository = scaleTypeRepository ?? throw new NullException(() => scaleTypeRepository);
			DimensionRepository = dimensionRepository ?? throw new NullException(() => dimensionRepository);

			IDRepository = idRepository ?? throw new NullException(() => idRepository);
		}

		public void Commit() => DocumentRepository.Commit();
		public void Rollback() => DocumentRepository.Rollback();
		public void Flush() => DocumentRepository.Flush();
	}
}