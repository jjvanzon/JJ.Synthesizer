using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class DocumentDeletePresenter : PresenterBase<DocumentDeleteViewModel>
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly DocumentManager _documentManager;

		public DocumentDeletePresenter(RepositoryWrapper repositoryWrapper)
		{
			if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

			_documentRepository = repositoryWrapper.DocumentRepository;
			_documentManager = new DocumentManager(repositoryWrapper);
		}

		/// <summary> return DocumentDeleteViewModel or DocumentCannotDeleteViewModel. </summary>
		public ViewModelBase Show(int id)
		{
			// GetEntity
			Document document = _documentRepository.Get(id);

			// Business
			VoidResultDto result = _documentManager.CanDelete(document);

			if (!result.Successful)
			{
				// Redirect
				var presenter2 = new DocumentCannotDeletePresenter(_documentRepository);
				DocumentCannotDeleteViewModel viewModel2 = presenter2.Show(id, result.Messages);
				return viewModel2;
			}
			else
			{
				// ToViewModel
				DocumentDeleteViewModel viewModel = document.ToDeleteViewModel();

				// Non-Persisted
				viewModel.Visible = true;

				// Successful
				viewModel.Successful = true;

				return viewModel;
			}
		}

		/// <summary> Can return DocumentDeletedViewModel or DocumentCannotDeletePresenter. </summary>
		public ViewModelBase Confirm(DocumentDeleteViewModel userInput)
		{
			if (userInput == null) throw new NullException(() => userInput);

			// RefreshCounter
			userInput.RefreshID = RefreshIDProvider.GetRefreshID();

			// Set !Successful
			userInput.Successful = false;

			// GetEntity
			Document document = _documentRepository.Get(userInput.Document.ID);

			// Business
			VoidResultDto result = _documentManager.DeleteWithRelatedEntities(document);

			if (!result.Successful)
			{
				// Successful (strangely, because we redirect and cannot leave a view model in unsuccessful state)
				userInput.Successful = true;

				// Redirect
				var presenter2 = new DocumentCannotDeletePresenter(_documentRepository);
				DocumentCannotDeleteViewModel viewModel = presenter2.Show(userInput.Document.ID, result.Messages);
				return viewModel;
			}
			else
			{
				// Successful
				userInput.Successful = true;

				// Redirect
				var presenter2 = new DocumentDeletedPresenter();
				DocumentDeletedViewModel viewModel = presenter2.Show();
				return viewModel;
			}
		}

		public void Cancel(DocumentDeleteViewModel viewModel)
		{
			ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);
		}
	}
}
