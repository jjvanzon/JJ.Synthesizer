﻿using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class PatchRepositories
    {
        public IPatchRepository PatchRepository { get; private set; }
        public IOperatorRepository OperatorRepository { get; private set; }
        public IOperatorTypeRepository OperatorTypeRepository { get; private set; }
        public IInletRepository InletRepository { get; private set; }
        public IOutletRepository OutletRepository { get; private set; }
        public ICurveRepository CurveRepository { get; private set; }
        public ISampleRepository SampleRepository { get; private set; }
        public IDocumentRepository DocumentRepository { get; private set; }
        public IDimensionRepository DimensionRepository { get; private set; }
        public ISpeakerSetupRepository SpeakerSetupRepository { get; private set; }
        public IInterpolationTypeRepository InterpolationTypeRepository { get; set; }

        public IEntityPositionRepository EntityPositionRepository { get; private set; }
        public IIDRepository IDRepository { get; private set; }

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
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (dimensionRepository == null) throw new NullException(() => dimensionRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            PatchRepository = patchRepository;
            OperatorRepository = operatorRepository;
            OperatorTypeRepository = operatorTypeRepository;
            InletRepository = inletRepository;
            OutletRepository = outletRepository;
            CurveRepository = curveRepository;
            SampleRepository = sampleRepository;
            DocumentRepository = documentRepository;
            DimensionRepository = dimensionRepository;
            SpeakerSetupRepository = speakerSetupRepository;
            InterpolationTypeRepository = interpolationTypeRepository;
            EntityPositionRepository = entityPositionRepository;
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