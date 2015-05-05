using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModel;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentConfirmDeletePresenter
    {
        private IDocumentRepository _documentRepository;
        private DocumentManager _documentManager;

        public DocumentConfirmDeletePresenter(
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

            _documentRepository = documentRepository;
            _documentManager = new DocumentManager(
                documentRepository,
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
        }

        /// <summary>
        /// Can return DocumentConfirmDeleteViewModel, DocumentNotFoundViewModel or DocumentCannotDeleteViewModel.
        /// </summary>
        public object Show(int id)
        {
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                NotFoundPresenter presenter2 = new NotFoundPresenter();
                NotFoundViewModel viewModel2 = presenter2.Show(typeof(Document).Name);
                return viewModel2;
            }
            else
            {
                VoidResult result = _documentManager.CanDelete(document);

                if (!result.Successful)
                {
                    DocumentCannotDeletePresenter presenter2 = new DocumentCannotDeletePresenter();
                    DocumentCannotDeleteViewModel viewModel2 = presenter2.Show(document, result.Messages);
                    return viewModel2;
                }
                else
                {
                    DocumentConfirmDeleteViewModel viewModel2 = document.ToConfirmDeleteViewModel();
                    return viewModel2;
                }
            }
        }

        /// <summary>
        /// Can return DocumentDeleteConfirmedViewModel or NotFoundViewModel.
        /// </summary>
        public object Confirm(int id)
        {
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                NotFoundPresenter presenter2 = new NotFoundPresenter();
                NotFoundViewModel viewModel2 = presenter2.Show(typeof(Document).Name);
                return viewModel2;
            }
            else
            {
                _documentManager.Delete(document);

                DocumentDeleteConfirmedPresenter presenter2 = new DocumentDeleteConfirmedPresenter();
                DocumentDeleteConfirmedViewModel viewModel2 = presenter2.Show();
                return viewModel2;
            }
        }

        public PreviousViewModel Cancel()
        {
            return new PreviousViewModel();
        }
    }
}
