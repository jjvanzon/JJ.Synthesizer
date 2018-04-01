using System;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
	public class RepositoryWrapper
	{
		public IAudioFileFormatRepository AudioFileFormatRepository { get; }
		public IAudioFileOutputRepository AudioFileOutputRepository { get; }
		public IAudioOutputRepository AudioOutputRepository { get; }
		public ICurveRepository CurveRepository { get; }
		public IDimensionRepository DimensionRepository { get; }
		public IDocumentReferenceRepository DocumentReferenceRepository { get; }
		public IDocumentRepository DocumentRepository { get; }
		public IEntityPositionRepository EntityPositionRepository { get; }
		public IIDRepository IDRepository { get; }
		public IInletRepository InletRepository { get; }
		public IInterpolationTypeRepository InterpolationTypeRepository { get; }
		public IMidiMappingRepository MidiMappingRepository { get; }
		public IMidiMappingGroupRepository MidiMappingGroupRepository { get; }
		public INodeRepository NodeRepository { get; }
		public INodeTypeRepository NodeTypeRepository { get; }
		public IOperatorRepository OperatorRepository { get; }
		public IOutletRepository OutletRepository { get; }
		public IPatchRepository PatchRepository { get; }
		public ISampleDataTypeRepository SampleDataTypeRepository { get; }
		public ISampleRepository SampleRepository { get; }
		public IScaleRepository ScaleRepository { get; }
		public IScaleTypeRepository ScaleTypeRepository { get; }
		public ISpeakerSetupRepository SpeakerSetupRepository { get; }
		public IToneRepository ToneRepository { get; }
		public IMidiMappingTypeRepository MidiMappingTypeRepository { get; }

		public RepositoryWrapper(
			IAudioFileFormatRepository audioFileFormatRepository,
			IAudioFileOutputRepository audioFileOutputRepository,
			IAudioOutputRepository audioOutputRepository,
			ICurveRepository curveRepository,
			IDimensionRepository dimensionRepository,
			IDocumentReferenceRepository documentReferenceRepository,
			IDocumentRepository documentRepository,
			IEntityPositionRepository entityPositionRepository,
			IIDRepository idRepository,
			IInletRepository inletRepository,
			IInterpolationTypeRepository interpolationTypeRepository,
			IMidiMappingRepository midiMappingRepository,
			IMidiMappingGroupRepository midiMappingGroupRepository,
			[NotNull] IMidiMappingTypeRepository midiMappingTypeRepository,
			INodeRepository nodeRepository,
			INodeTypeRepository nodeTypeRepository,
			IOperatorRepository operatorRepository,
			IOutletRepository outletRepository,
			IPatchRepository patchRepository,
			ISampleDataTypeRepository sampleDataTypeRepository,
			ISampleRepository sampleRepository,
			IScaleRepository scaleRepository,
			IScaleTypeRepository scaleTypeRepository,
			ISpeakerSetupRepository speakerSetupRepository,
			IToneRepository toneRepository)
		{
			AudioFileFormatRepository = audioFileFormatRepository ?? throw new NullException(() => audioFileFormatRepository);
			AudioFileOutputRepository = audioFileOutputRepository ?? throw new NullException(() => audioFileOutputRepository);
			AudioOutputRepository = audioOutputRepository ?? throw new NullException(() => audioOutputRepository);
			CurveRepository = curveRepository ?? throw new NullException(() => curveRepository);
			DimensionRepository = dimensionRepository ?? throw new NullException(() => dimensionRepository);
			DocumentReferenceRepository = documentReferenceRepository ?? throw new NullException(() => documentReferenceRepository);
			DocumentRepository = documentRepository ?? throw new NullException(() => documentRepository);
			EntityPositionRepository = entityPositionRepository ?? throw new NullException(() => entityPositionRepository);
			IDRepository = idRepository ?? throw new NullException(() => idRepository);
			InletRepository = inletRepository ?? throw new NullException(() => inletRepository);
			InterpolationTypeRepository = interpolationTypeRepository ?? throw new NullException(() => interpolationTypeRepository);
			MidiMappingRepository = midiMappingRepository ?? throw new NullException(() => midiMappingRepository);
			MidiMappingGroupRepository = midiMappingGroupRepository ?? throw new NullException(() => midiMappingGroupRepository);
			MidiMappingTypeRepository = midiMappingTypeRepository ?? throw new ArgumentNullException(nameof(midiMappingTypeRepository));
			NodeRepository = nodeRepository ?? throw new NullException(() => nodeRepository);
			NodeTypeRepository = nodeTypeRepository ?? throw new NullException(() => nodeTypeRepository);
			OperatorRepository = operatorRepository ?? throw new NullException(() => operatorRepository);
			OutletRepository = outletRepository ?? throw new NullException(() => outletRepository);
			PatchRepository = patchRepository ?? throw new NullException(() => patchRepository);
			SampleDataTypeRepository = sampleDataTypeRepository ?? throw new NullException(() => sampleDataTypeRepository);
			SampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
			ScaleRepository = scaleRepository ?? throw new NullException(() => scaleRepository);
			ScaleTypeRepository = scaleTypeRepository ?? throw new NullException(() => scaleTypeRepository);
			SpeakerSetupRepository = speakerSetupRepository ?? throw new NullException(() => speakerSetupRepository);
			ToneRepository = toneRepository ?? throw new NullException(() => toneRepository);
		}

		public void Commit() => DocumentRepository.Commit();
		public void Rollback() => DocumentRepository.Rollback();
		public void Flush() => DocumentRepository.Flush();
	}
}