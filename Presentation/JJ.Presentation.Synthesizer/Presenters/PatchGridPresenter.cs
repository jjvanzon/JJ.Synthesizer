using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchGridPresenter : PresenterBase<PatchGridViewModel>
    {
        private IDocumentRepository _documentRepository;

        public PatchGridPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public PatchGridViewModel Show(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document rootDocument = _documentRepository.Get(userInput.RootDocumentID);

            // ToViewModel
            PatchGridViewModel viewModel = rootDocument.ToPatchGridViewModel(userInput.Group);

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchGridViewModel Refresh(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document rootDocument = _documentRepository.Get(userInput.RootDocumentID);

            // ToViewModel
            PatchGridViewModel viewModel = rootDocument.ToPatchGridViewModel(userInput.Group);

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchGridViewModel Close(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document rootDocument = _documentRepository.Get(userInput.RootDocumentID);

            // ToViewModel
            PatchGridViewModel viewModel = rootDocument.ToPatchGridViewModel(userInput.Group);

            // Non-Persisted
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }
    }
}
