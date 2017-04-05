using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentPropertiesPresenter : PropertiesPresenterBase<DocumentPropertiesViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly DocumentManager _documentManager;

        public DocumentPropertiesPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentRepository = repositories.DocumentRepository;
            _documentManager = new DocumentManager(repositories);
        }

        protected override DocumentPropertiesViewModel CreateViewModel(DocumentPropertiesViewModel userInput)
        {
            // GetEntity
            Document entity = _documentRepository.Get(userInput.Entity.ID);

            // ToViewModel
            DocumentPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            return viewModel;
        }

        protected override DocumentPropertiesViewModel UpdateEntity(DocumentPropertiesViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // ToEntity: was already done by the MainPresenter.

                // GetEntity
                Document document = _documentRepository.Get(userInput.Entity.ID);

                // Business
                VoidResult result = _documentManager.Save(document);

                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result.Messages);

                // Successful?
                viewModel.Successful = result.Successful;
            });
        }
    }
}