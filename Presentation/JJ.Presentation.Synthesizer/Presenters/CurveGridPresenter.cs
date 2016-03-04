using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveGridPresenter : PresenterBase<CurveGridViewModel>
    {
        private IDocumentRepository _documentRepository;

        public CurveGridPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public CurveGridViewModel Show(CurveGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            CurveGridViewModel viewModel = document.Curves.ToGridViewModel(userInput.DocumentID);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurveGridViewModel Refresh(CurveGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            CurveGridViewModel viewModel = document.Curves.ToGridViewModel(userInput.DocumentID);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurveGridViewModel Close(CurveGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            CurveGridViewModel viewModel = document.Curves.ToGridViewModel(userInput.DocumentID);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }
    }
}