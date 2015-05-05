using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Extensions;
using JJ.Framework.Presentation;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentDetailsPresenter
    {
        private IDocumentRepository _documentRepository;

        public DocumentDetailsPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public DocumentDetailsViewModel Create()
        {
            Document document = _documentRepository.Create();
            DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

            _documentRepository.Rollback();

            return viewModel;
        }

        /// <summary>
        /// Can return DocumentDetailsViewModel or PreviousViewModel.
        /// </summary>
        public object Save(DocumentDetailsViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            Document document = viewModel.ToEntity(_documentRepository);

            IValidator validator = new DocumentValidator(document);
            if (!validator.IsValid)
            {
                // TODO: Be more stateful.
                DocumentDetailsViewModel viewModel2 = document.ToDetailsViewModel();
                viewModel2.Messages = validator.ValidationMessages.ToCanonical();

                _documentRepository.Rollback();

                return viewModel2;
            }

            _documentRepository.Commit();

            return new PreviousViewModel();
        }

        /// <summary>
        /// Can return DocumentConfirmDeleteViewModel, NotFoundViewModel or DocumentCannotDeleteViewModel.
        /// </summary>
        public object Delete(
            int id,
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
            DocumentConfirmDeletePresenter presenter2 = new DocumentConfirmDeletePresenter(
                _documentRepository,
                curveRepository,
                patchRepository,
                sampleRepository,
                audioFileOutputRepository,
                documentReferenceRepository,
                nodeRepository,
                audioFileOutputChannelRepository,
                operatorRepository,
                inletRepository,
                outletRepository,
                entityPositionRepository);

            object viewModel2 = presenter2.Show(id);
            return viewModel2;
        }
    }
}
