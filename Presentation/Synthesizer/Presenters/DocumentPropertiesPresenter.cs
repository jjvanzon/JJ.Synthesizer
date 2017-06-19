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
        private readonly AutoPatcher _autoPatcher;

        public DocumentPropertiesPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _documentManager = new DocumentManager(repositories);
            _autoPatcher = new AutoPatcher(_repositories);
        }

        protected override DocumentPropertiesViewModel CreateViewModel(DocumentPropertiesViewModel userInput)
        {
            // GetEntity
            Document entity = _repositories.DocumentRepository.Get(userInput.Entity.ID);

            // ToViewModel
            DocumentPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            return viewModel;
        }

        protected override void UpdateEntity(DocumentPropertiesViewModel viewModel)
        {
            // ToEntity: was already done by the MainPresenter.

            // GetEntity
            Document document = _repositories.DocumentRepository.Get(viewModel.Entity.ID);

            // Business
            IResult result = _documentManager.Save(document);

            // Non-Persisted
            viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

            // Successful?
            viewModel.Successful = result.Successful;
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
                    Result<Outlet> result = _autoPatcher.TryAutoPatchFromDocumentRandomly(document, mustIncludeHidden: true);
                    Outlet outlet = result.Data;
                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }
    }
}