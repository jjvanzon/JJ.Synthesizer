using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
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
        public IOperatorTypeRepository OperatorTypeRepository { get; }
        public IInletRepository InletRepository { get; }
        public IOutletRepository OutletRepository { get; }
        public IScaleRepository ScaleRepository { get; }
        public IToneRepository ToneRepository { get; }
        public IEntityPositionRepository EntityPositionRepository { get; }

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
            IOperatorTypeRepository operatorTypeRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IScaleRepository scaleRepository,
            IToneRepository toneRepository,

            IEntityPositionRepository entityPositionRepository,

            IAudioFileFormatRepository audioFileFormatRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            INodeTypeRepository nodeTypeRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IScaleTypeRepository scaleTypeRepository,
            IDimensionRepository dimensionRepository,

            IIDRepository idRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (audioOutputRepository == null) throw new NullException(() => audioOutputRepository);
            if (documentReferenceRepository == null) throw new NullException(() => documentReferenceRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (scaleRepository == null) throw new NullException(() => scaleRepository);
            if (toneRepository == null) throw new NullException(() => toneRepository);

            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (scaleTypeRepository == null) throw new NullException(() => scaleTypeRepository);
            if (dimensionRepository == null) throw new NullException(() => dimensionRepository);

            if (idRepository == null) throw new NullException(() => idRepository);

            DocumentRepository = documentRepository;
            CurveRepository = curveRepository;
            PatchRepository = patchRepository;
            SampleRepository = sampleRepository;
            AudioFileOutputRepository = audioFileOutputRepository;
            AudioOutputRepository = audioOutputRepository;
            DocumentReferenceRepository = documentReferenceRepository;
            NodeRepository = nodeRepository;
            OperatorRepository = operatorRepository;
            OperatorTypeRepository = operatorTypeRepository;
            InletRepository = inletRepository;
            OutletRepository = outletRepository;
            EntityPositionRepository = entityPositionRepository;
            ScaleRepository = scaleRepository;
            ToneRepository = toneRepository;

            AudioFileFormatRepository = audioFileFormatRepository;
            InterpolationTypeRepository = interpolationTypeRepository;
            NodeTypeRepository = nodeTypeRepository;
            SampleDataTypeRepository = sampleDataTypeRepository;
            SpeakerSetupRepository = speakerSetupRepository;
            ScaleTypeRepository = scaleTypeRepository;
            DimensionRepository = dimensionRepository;

            IDRepository = idRepository;
        }

        public void Commit()
        {
            DocumentRepository.Commit();
        }

        public void Rollback()
        {
            DocumentRepository.Rollback();
        }

        public void Flush()
        {
            DocumentRepository.Flush();
        }
    }
}