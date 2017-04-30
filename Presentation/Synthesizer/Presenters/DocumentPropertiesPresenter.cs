using JJ.Business.Canonical;
using JJ.Framework.Exceptions;
using Canonicals = JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentPropertiesPresenter : PropertiesPresenterBase<DocumentPropertiesViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;

        public DocumentPropertiesPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _documentManager = new DocumentManager(repositories);
        }

        protected override DocumentPropertiesViewModel CreateViewModel(DocumentPropertiesViewModel userInput)
        {
            // GetEntity
            Document entity = _repositories.DocumentRepository.Get(userInput.Entity.ID);

            // ToViewModel
            DocumentPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            return viewModel;
        }

        protected override DocumentPropertiesViewModel UpdateEntity(DocumentPropertiesViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // ToEntity: was already done by the MainPresenter.

                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(userInput.Entity.ID);

                    // Business
                    Canonicals.VoidResultDto result = _documentManager.Save(document);

                    // Non-Persisted
                    viewModel.ValidationMessages.AddRange(result.Messages);

                    // Successful?
                    viewModel.Successful = result.Successful;
                });
        }

        public DocumentPropertiesViewModel Play(DocumentPropertiesViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(userInput.Entity.ID);

                    // Business
                    var patchManager = new PatchManager(new PatchRepositories(_repositories));
                    Result<Outlet> result = patchManager.TryAutoPatchFromDocumentRandomly(document);
                    Outlet outlet = result.Data;

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }
    }
}