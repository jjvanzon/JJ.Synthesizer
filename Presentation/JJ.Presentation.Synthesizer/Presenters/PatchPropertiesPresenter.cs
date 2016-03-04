using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchPropertiesPresenter : PresenterBase<PatchPropertiesViewModel>
    {
        private RepositoryWrapper _repositories;

        private DocumentManager _documentManager;
        private PatchManager _patchManager;

        public PatchPropertiesPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;

            _documentManager = new DocumentManager(_repositories);
            _patchManager = new PatchManager(new PatchRepositories(_repositories));
        }

        public PatchPropertiesViewModel Show(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Document childDocument = _repositories.DocumentRepository.Get(userInput.ChildDocumentID);

            // ToViewModel
            PatchPropertiesViewModel viewModel = childDocument.ToPatchPropertiesViewModel();

            // Non-Persisted
            viewModel.Visible = true;

            return viewModel;
        }

        public PatchPropertiesViewModel Refresh(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Document childDocument = _repositories.DocumentRepository.Get(userInput.ChildDocumentID);

            // ToViewModel
            PatchPropertiesViewModel viewModel = childDocument.ToPatchPropertiesViewModel();

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchPropertiesViewModel Close(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            PatchPropertiesViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public PatchPropertiesViewModel LoseFocus(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            PatchPropertiesViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private PatchPropertiesViewModel Update(PatchPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Document childDocument =_repositories.DocumentRepository.Get(userInput.ChildDocumentID);

            // Business
            VoidResult result = _documentManager.SaveChildDocument(childDocument);

            // ToViewModel
            PatchPropertiesViewModel viewModel = childDocument.ToPatchPropertiesViewModel();

            // Non-Persisted
            viewModel.Visible = userInput.Visible;
            viewModel.ValidationMessages = result.Messages;
            viewModel.Successful = result.Successful;
            if (!result.Successful)
            {
                return viewModel;
            }

            // Business
            _patchManager.Patch = childDocument.Patches[0];
            VoidResult result2 = _patchManager.SavePatch();

            // Non-Persisted
            viewModel.ValidationMessages.AddRange(result2.Messages);
            viewModel.Successful = result2.Successful;

            return viewModel;
        }
    }
}