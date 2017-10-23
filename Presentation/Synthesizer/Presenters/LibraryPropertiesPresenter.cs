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
    internal class LibraryPropertiesPresenter 
        : PropertiesPresenterBase<DocumentReference, LibraryPropertiesViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentManager _documentManager;
        private readonly AutoPatcher _autoPatcher;

        public LibraryPropertiesPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _documentManager = new DocumentManager(repositories);
            _autoPatcher = new AutoPatcher(_repositories);
        }

        protected override DocumentReference GetEntity(LibraryPropertiesViewModel userInput)
        {
            return _repositories.DocumentReferenceRepository.Get(userInput.DocumentReferenceID);
        }

        protected override LibraryPropertiesViewModel ToViewModel(DocumentReference entity)
        {
            return entity.ToPropertiesViewModel();
        }

        protected override IResult Save(DocumentReference entity)
        {
            return _documentManager.SaveDocumentReference(entity);
        }

        public void OpenExternally(LibraryPropertiesViewModel viewModel)
        {
            ExecuteAction(
                viewModel,
                _ =>
                {
                    viewModel.DocumentToOpenExternally = GetEntity(viewModel).LowerDocument.ToIDAndName();
                });
        }

        public LibraryPropertiesViewModel Play(LibraryPropertiesViewModel userInput)
        {
            Outlet outlet = null;

            return ExecuteAction(
                userInput,
                entity =>
                {
                    Result<Outlet> result = _autoPatcher.TryAutoPatchFromDocumentRandomly(entity.LowerDocument, mustIncludeHidden: false);
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

        public LibraryPropertiesViewModel Remove(LibraryPropertiesViewModel userInput)
        {
            return ExecuteAction(
                userInput,
                entity => _documentManager.DeleteDocumentReference(entity));
        }
    }
}