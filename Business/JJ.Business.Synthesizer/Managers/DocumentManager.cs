using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Managers
{
    public class DocumentManager
    {
        private IDocumentRepository _documentRepository;
        private ICurveRepository _curveRepository;
        private IPatchRepository _patchRepository;
        private ISampleRepository _sampleRepository;
        private IAudioFileOutputRepository _audioFileOutputRepository;
        private IDocumentReferenceRepository _documentReferenceRepository;
        private INodeRepository _nodeRepository;
        private IAudioFileOutputChannelRepository _audioFileOutputChannelRepository;
        private IOperatorRepository _operatorRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private IEntityPositionRepository _entityPositionRepository;

        public DocumentManager(
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

            _documentRepository = documentRepository;
            _curveRepository = curveRepository;
            _patchRepository = patchRepository;
            _sampleRepository = sampleRepository;
            _audioFileOutputRepository = audioFileOutputRepository;
            _documentReferenceRepository = documentReferenceRepository;
            _nodeRepository = nodeRepository;
            _audioFileOutputChannelRepository = audioFileOutputChannelRepository;
            _operatorRepository = operatorRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _entityPositionRepository = entityPositionRepository;
        }                                                                       

        public VoidResult CanDelete(Document document)
        {
            if (document == null) throw new NullException(() => document);

            IValidator validator = new DocumentDeleteValidator(document);

            if (!validator.IsValid)
            {
                var result = new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
                return result;
            }
            else
            {
                var result = new VoidResult
                {
                    Successful = true,
                    Messages = new Message[0]
                };
                return result;
            }
        }

        public void Delete(Document document)
        {
            if (document == null) throw new NullException(() => document);

            document.DeleteRelatedEntities(
                _documentRepository,
                _curveRepository,
                _patchRepository,
                _sampleRepository,
                _audioFileOutputRepository,
                _documentReferenceRepository,
                _nodeRepository,
                _audioFileOutputChannelRepository,
                _operatorRepository,
                _inletRepository,
                _outletRepository,
                _entityPositionRepository);

            document.UnlinkRelatedEntities();

            _documentRepository.Delete(document);

            _documentRepository.Commit();
        }
    }
}
