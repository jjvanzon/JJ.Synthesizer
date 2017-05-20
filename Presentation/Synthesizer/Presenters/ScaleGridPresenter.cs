using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ScaleGridPresenter : GridPresenterBase<ScaleGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ScaleManager _scaleManager;

        public ScaleGridPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentRepository = repositories.DocumentRepository;
            _scaleManager = new ScaleManager(new ScaleRepositories(repositories));
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
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // Business
                    _scaleManager.DeleteWithRelatedEntities(id);
                });
        }
    }
}
