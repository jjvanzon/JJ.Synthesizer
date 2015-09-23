using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class RepositoryWrapper
    {
        public IDocumentRepository DocumentRepository { get; set; }
        public ICurveRepository CurveRepository { get; set; }
        public IPatchRepository PatchRepository { get; set; }
        public ISampleRepository SampleRepository { get; set; }
        public IAudioFileOutputRepository AudioFileOutputRepository { get; set; }
        public IDocumentReferenceRepository DocumentReferenceRepository { get; set; }
        public INodeRepository NodeRepository { get; set; }
        public IAudioFileOutputChannelRepository AudioFileOutputChannelRepository { get; set; }
        public IOperatorRepository OperatorRepository { get; set; }
        public IOperatorTypeRepository OperatorTypeRepository { get; set; }
        public IInletRepository InletRepository { get; set; }
        public IOutletRepository OutletRepository { get; set; }
        public IScaleRepository ScaleRepository { get; internal set; }
        public IToneRepository ToneRepository { get; internal set; }
        public IEntityPositionRepository EntityPositionRepository { get; set; }

        public IAudioFileFormatRepository AudioFileFormatRepository { get; set; }
        public IInterpolationTypeRepository InterpolationTypeRepository { get; set; }
        public INodeTypeRepository NodeTypeRepository { get; set; }
        public ISampleDataTypeRepository SampleDataTypeRepository { get; set; }
        public ISpeakerSetupRepository SpeakerSetupRepository { get; set; }
        public IChildDocumentTypeRepository ChildDocumentTypeRepository { get; set; }
        public IScaleTypeRepository ScaleTypeRepository { get; internal set; }

        public IIDRepository IDRepository { get; set; }


        public RepositoryWrapper(
            IDocumentRepository documentRepository,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository,
            ISampleRepository sampleRepository,
            IAudioFileOutputRepository audioFileOutputRepository,
            IDocumentReferenceRepository documentReferenceRepository,
            INodeRepository nodeRepository,
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository,
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
            IChildDocumentTypeRepository childDocumentTypeRepository,
            IScaleTypeRepository scaleTypeRepository,

            IIDRepository idRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (documentReferenceRepository == null) throw new NullException(() => documentReferenceRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);
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
            if (childDocumentTypeRepository == null) throw new NullException(() => childDocumentTypeRepository);
            if (scaleTypeRepository == null) throw new NullException(() => scaleTypeRepository);

            if (idRepository == null) throw new NullException(() => idRepository);

            DocumentRepository = documentRepository;
            CurveRepository = curveRepository;
            PatchRepository = patchRepository;
            SampleRepository = sampleRepository;
            AudioFileOutputRepository = audioFileOutputRepository;
            DocumentReferenceRepository = documentReferenceRepository;
            NodeRepository = nodeRepository;
            AudioFileOutputChannelRepository = audioFileOutputChannelRepository;
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
            ChildDocumentTypeRepository = childDocumentTypeRepository;
            ScaleTypeRepository = scaleTypeRepository;

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