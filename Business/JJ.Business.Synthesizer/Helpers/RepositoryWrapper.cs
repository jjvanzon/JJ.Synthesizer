using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IInletRepository InletRepository { get; set; }
        public IOutletRepository OutletRepository { get; set; }
        public IEntityPositionRepository EntityPositionRepository { get; set; }

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
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
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
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            DocumentRepository = documentRepository;
            CurveRepository = curveRepository;
            PatchRepository = patchRepository;
            SampleRepository = sampleRepository;
            AudioFileOutputRepository = audioFileOutputRepository;
            DocumentReferenceRepository = documentReferenceRepository;
            NodeRepository = nodeRepository;
            AudioFileOutputChannelRepository = audioFileOutputChannelRepository;
            OperatorRepository = operatorRepository;
            InletRepository = inletRepository;
            OutletRepository = outletRepository;
            EntityPositionRepository = entityPositionRepository;
        }

        public void Commit()
        {
            DocumentRepository.Commit();
        }
    }
}
