using System;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ScaleGridPresenter : GridPresenterBase<ScaleGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ScaleManager _scaleManager;

        public ScaleGridPresenter(IDocumentRepository documentRepository, ScaleManager scaleManager)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _scaleManager = scaleManager ?? throw new ArgumentNullException(nameof(scaleManager));
        }

        protected override ScaleGridViewModel CreateViewModel(ScaleGridViewModel userInput)
        {
            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            ScaleGridViewModel viewModel = document.Scales.ToGridViewModel(userInput.DocumentID);

            return viewModel;
        }

        public ScaleGridViewModel Delete(ScaleGridViewModel userInput, int id)
        {
            return ExecuteAction(
                userInput,
                viewModel =>
                {
                    // Business
                    _scaleManager.DeleteWithRelatedEntities(id);
                });
        }
    }
}
