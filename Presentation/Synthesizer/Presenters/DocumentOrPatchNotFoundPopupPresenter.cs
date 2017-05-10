using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentOrPatchNotFoundPopupPresenter : PresenterBase<DocumentOrPatchNotFoundPopupViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IPatchRepository _patchRepository;

        public DocumentOrPatchNotFoundPopupPresenter([NotNull] IDocumentRepository documentRepository, [NotNull] IPatchRepository patchRepository)
        {
            _documentRepository = documentRepository ?? throw new NullException(() => documentRepository);
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);
        }

        public DocumentOrPatchNotFoundPopupViewModel Show(DocumentOrPatchNotFoundPopupViewModel userInput, string documentName, string patchName = null)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            Document document = _documentRepository.TryGetByName(documentName);
            Patch patch = _patchRepository.TryGetByName(patchName);

            // ToViewModel
            string message;
            if (document == null)
            {
                message = CommonResourceFormatter.NotFound_WithType_AndName(ResourceFormatter.Document, documentName);
            }
            else if (patch == null)
            {
                message = CommonResourceFormatter.NotFound_WithType_AndName(ResourceFormatter.Patch, patchName);
            }
            else
            {
                throw new Exception($"Either {nameof(document)} or {nameof(patch)} should have been null.");
            }

            DocumentOrPatchNotFoundPopupViewModel viewModel = ViewModelHelper.CreateDocumentOrPatchNotFoundPopupViewModel(message);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public DocumentOrPatchNotFoundPopupViewModel OK(DocumentOrPatchNotFoundPopupViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ToViewModel
            var viewModel = ViewModelHelper.CreateDocumentOrPatchNotFoundPopupViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.MustCloseMainView = true;
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }
    }
}