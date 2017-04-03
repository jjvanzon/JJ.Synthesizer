using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ScaleGridPresenter : GridPresenterBase<ScaleGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;

        public ScaleGridPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        protected override ScaleGridViewModel CreateViewModel(ScaleGridViewModel userInput)
        {
            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            ScaleGridViewModel viewModel = document.Scales.ToGridViewModel(userInput.DocumentID);

            return viewModel;
        }
    }
}
