using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentPropertiesPresenter : EntityPresenterWithSaveBase<Document, DocumentPropertiesViewModel>
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

        protected override Document GetEntity(DocumentPropertiesViewModel userInput)
        {
            return _repositories.DocumentRepository.Get(userInput.Entity.ID);
        }

        protected override DocumentPropertiesViewModel ToViewModel(Document entity)
        {
            return entity.ToPropertiesViewModel();
        }

        protected override IResult Save(Document entity)
        {
            return _documentManager.Save(entity);
        }

        public DocumentPropertiesViewModel Play(DocumentPropertiesViewModel userInput)
        {
            Outlet outlet = null;

            return ExecuteAction(
                userInput,
                entity =>
                {
                    Result<Outlet> result = _autoPatcher.TryAutoPatchFromDocumentRandomly(entity, mustIncludeHidden: true);
                    outlet = result.Data;
                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }
                    return null;
                },
                viewModel =>
                {
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }
    }
}