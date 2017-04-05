using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibraryPropertiesPresenter : PropertiesPresenterBase<LibraryPropertiesViewModel>
    {
        private readonly IDocumentReferenceRepository _documentReferenceRepository;
        private readonly DocumentManager _documentManager;

        public LibraryPropertiesPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentReferenceRepository = repositories.DocumentReferenceRepository;
            _documentManager = new DocumentManager(repositories);
        }

        protected override LibraryPropertiesViewModel CreateViewModel(LibraryPropertiesViewModel userInput)
        {
            // GetEntity
            DocumentReference entity = _documentReferenceRepository.Get(userInput.DocumentReferenceID);

            // ToViewModel
            LibraryPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            return viewModel;
        }

        protected override LibraryPropertiesViewModel UpdateEntity(LibraryPropertiesViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // ToEntity: was already done by the MainPresenter.
                                         
                // GetEntity
                DocumentReference entity = _documentReferenceRepository.Get(userInput.DocumentReferenceID);

                // Business
                VoidResult result = _documentManager.SaveDocumentReference(entity);

                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result.Messages);

                // Successful?
                viewModel.Successful = result.Successful;
            });
        }
    }
}
