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
		: EntityPresenterWithSaveBase<DocumentReference, LibraryPropertiesViewModel>
	{
		private readonly RepositoryWrapper _repositories;
		private readonly DocumentFacade _documentFacade;
		private readonly AutoPatcher _autoPatcher;

		public LibraryPropertiesPresenter(RepositoryWrapper repositories)
		{
			_repositories = repositories ?? throw new NullException(() => repositories);
			_documentFacade = new DocumentFacade(repositories);
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

		protected override IResult Save(DocumentReference entity, LibraryPropertiesViewModel userInput)
		{
			return _documentFacade.SaveDocumentReference(entity);
		}

		public void OpenExternally(LibraryPropertiesViewModel userInput)
		{
			Document lowerDocument = null;

			ExecuteAction(
				userInput,
				entity => lowerDocument = entity.LowerDocument,
				viewModel => viewModel.DocumentToOpenExternally = lowerDocument.ToIDAndName());
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
			return ExecuteAction(userInput, x => _documentFacade.DeleteDocumentReference(x));
		}
	}
}