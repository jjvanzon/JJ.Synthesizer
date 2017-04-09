using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class PatchRepositories
    {
        public IPatchRepository PatchRepository { get; }
        public IOperatorRepository OperatorRepository { get; }
        public IOperatorTypeRepository OperatorTypeRepository { get; }
        public IInletRepository InletRepository { get; }
        public IOutletRepository OutletRepository { get; }
        public ICurveRepository CurveRepository { get; }
        public ISampleRepository SampleRepository { get; }
        public IDocumentRepository DocumentRepository { get; }
        public IDimensionRepository DimensionRepository { get; }
        public ISpeakerSetupRepository SpeakerSetupRepository { get; }
        public IInterpolationTypeRepository InterpolationTypeRepository { get; set; }

        public IEntityPositionRepository EntityPositionRepository { get; }
        public IIDRepository IDRepository { get; }

        public PatchRepositories(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            PatchRepository = repositoryWrapper.PatchRepository;
            OperatorRepository = repositoryWrapper.OperatorRepository;
            OperatorTypeRepository = repositoryWrapper.OperatorTypeRepository;
            InletRepository = repositoryWrapper.InletRepository;
            OutletRepository = repositoryWrapper.OutletRepository;
            CurveRepository = repositoryWrapper.CurveRepository;
            SampleRepository = repositoryWrapper.SampleRepository;
            DocumentRepository = repositoryWrapper.DocumentRepository;
            DimensionRepository = repositoryWrapper.DimensionRepository;
            SpeakerSetupRepository = repositoryWrapper.SpeakerSetupRepository;
            InterpolationTypeRepository = repositoryWrapper.InterpolationTypeRepository;
            EntityPositionRepository = repositoryWrapper.EntityPositionRepository;
            IDRepository = repositoryWrapper.IDRepository;
        }

        public PatchRepositories(
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IDocumentRepository documentRepository,
            IDimensionRepository dimensionRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            IEntityPositionRepository entityPositionRepository,
            IIDRepository idRepository)
        {
            PatchRepository = patchRepository ?? throw new NullException(() => patchRepository);
            OperatorRepository = operatorRepository ?? throw new NullException(() => operatorRepository);
            OperatorTypeRepository = operatorTypeRepository ?? throw new NullException(() => operatorTypeRepository);
            InletRepository = inletRepository ?? throw new NullException(() => inletRepository);
            OutletRepository = outletRepository ?? throw new NullException(() => outletRepository);
            CurveRepository = curveRepository ?? throw new NullException(() => curveRepository);
            SampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);
            DocumentRepository = documentRepository ?? throw new NullException(() => documentRepository);
            DimensionRepository = dimensionRepository ?? throw new NullException(() => dimensionRepository);
            SpeakerSetupRepository = speakerSetupRepository ?? throw new NullException(() => speakerSetupRepository);
            InterpolationTypeRepository = interpolationTypeRepository ?? throw new NullException(() => interpolationTypeRepository);
            EntityPositionRepository = entityPositionRepository ?? throw new NullException(() => entityPositionRepository);
            IDRepository = idRepository ?? throw new NullException(() => idRepository);
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
