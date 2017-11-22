using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class DocumentGridPresenter : EntityPresenterWithoutSaveBase<IList<Document>, DocumentGridViewModel>
	{
		private readonly RepositoryWrapper _repositories;
		private readonly AutoPatcher _autoPatcher;

		public DocumentGridPresenter(RepositoryWrapper repositories)
		{
			_repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
			_autoPatcher = new AutoPatcher(_repositories);
		}

		/// <summary>
		/// Known bug, not easily solvable and also not a large problem: 
		/// A renamed, uncommitted document will not end up in a new place in the list,
		/// because the sorting done by the data store, which is not ware of the new name.
		/// </summary>
		protected override IList<Document> GetEntity(DocumentGridViewModel userInput) => _repositories.DocumentRepository.OrderByName();

		protected override DocumentGridViewModel ToViewModel(IList<Document> entities) => entities.ToGridViewModel();

		public DocumentGridViewModel Load(DocumentGridViewModel viewModel)
		{
			return ExecuteAction(viewModel, x => { }, x => x.Visible = true);
		}

		public DocumentGridViewModel Play(DocumentGridViewModel userInput, int id)
		{
			Outlet outlet = null;

			return ExecuteAction(
				userInput,
				entity =>
				{
					// GetEntity
					Document document = _repositories.DocumentRepository.Get(id);

					// Business
					Result<Outlet> result = _autoPatcher.TryAutoPatchFromDocumentRandomly(document, mustIncludeHidden: false);
					outlet = result.Data;
					if (outlet != null)
					{
						_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
					}

					return result;
				},
				viewModel =>
				{
					// Non-Persisted
					viewModel.OutletIDToPlay = outlet?.ID;
				});
		}

		[Obsolete("Use Load instead.", true)]
		public override void Show(DocumentGridViewModel viewModel)
		{
			throw new NotSupportedException("Call Load instead.");
		}
	}
}