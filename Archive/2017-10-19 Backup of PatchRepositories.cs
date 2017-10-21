//using JJ.Data.Synthesizer.RepositoryInterfaces;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Helpers
//{
//    public class PatchRepositories
//    {
//        public IPatchRepository PatchRepository { get; }
//        public IOperatorRepository OperatorRepository { get; }
//        public IInletRepository InletRepository { get; }
//        public IOutletRepository OutletRepository { get; }
//        public ISampleRepository SampleRepository { get; }
//        public ICurveRepository CurveRepository { get; }
//        public INodeRepository NodeRepository { get; }
//        public IDocumentRepository DocumentRepository { get; }
//        public IDimensionRepository DimensionRepository { get; }
//        public ISpeakerSetupRepository SpeakerSetupRepository { get; }
//        public IInterpolationTypeRepository InterpolationTypeRepository { get; set; }

//        public IEntityPositionRepository EntityPositionRepository { get; }
//        public IIDRepository IDRepository { get; }

//        public PatchRepositories(RepositoryWrapper repositoryWrapper)
//        {
//            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

//            PatchRepository = repositoryWrapper.PatchRepository;
//            OperatorRepository = repositoryWrapper.OperatorRepository;
//            InletRepository = repositoryWrapper.InletRepository;
//            OutletRepository = repositoryWrapper.OutletRepository;
//            SampleRepository = repositoryWrapper.SampleRepository;
//            CurveRepository = repositoryWrapper.CurveRepository;
//            NodeRepository = repositoryWrapper.NodeRepository;
//            DocumentRepository = repositoryWrapper.DocumentRepository;
//            DimensionRepository = repositoryWrapper.DimensionRepository;
//            SpeakerSetupRepository = repositoryWrapper.SpeakerSetupRepository;
//            InterpolationTypeRepository = repositoryWrapper.InterpolationTypeRepository;
//            EntityPositionRepository = repositoryWrapper.EntityPositionRepository;
//            IDRepository = repositoryWrapper.IDRepository;
//        }

//        public void Commit() => DocumentRepository.Commit();
//        public void Rollback() => DocumentRepository.Rollback();
//        public void Flush() => DocumentRepository.Flush();
//    }
//}
