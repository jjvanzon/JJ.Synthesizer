using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class DocumentDetailsPresenter : PresenterBase<DocumentDetailsViewModel>
	{
		private readonly RepositoryWrapper _repositories;
		private readonly DocumentFacade _documentFacade;

		public DocumentDetailsPresenter(RepositoryWrapper repositories)
		{
			_repositories = repositories ?? throw new NullException(() => repositories);
			_documentFacade = new DocumentFacade(repositories);
		}

		public DocumentDetailsViewModel Create()
		{
			// Business
			Document document = _documentFacade.CreateWithPatch();

			// ToViewModel
			DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

			// Non-Persisted
			viewModel.Visible = true;
			
			// Successful
			viewModel.Successful = true;

			return viewModel;
		}

		public DocumentDetailsViewModel Save(DocumentDetailsViewModel userInput)
		{
			if (userInput == null) throw new NullException(() => userInput);

			// RefreshCounter
			userInput.RefreshID = RefreshIDProvider.GetRefreshID();

			// Set !Successful
			userInput.Successful = false;

			// ToEntity
			Document document = userInput.ToEntityWithAudioOutput(
				_repositories.DocumentRepository,
				_repositories.DocumentReferenceRepository,
				_repositories.AudioOutputRepository,
				_repositories.PatchRepository,
				_repositories.SpeakerSetupRepository);

			// Business
			IResult result = _documentFacade.Save(document);
			if (!result.Successful)
			{
				// ToViewModel
				DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

				// Non-Persisted
				CopyNonPersistedProperties(userInput, viewModel);
				viewModel.ValidationMessages.AddRange(result.Messages);
				viewModel.Successful = false;

				return viewModel;
			}
			else
			{
				// ToViewModel
				DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();

				// Non-Persisted
				CopyNonPersistedProperties(userInput, viewModel);
				viewModel.Visible = false;

				// Successful
				viewModel.Successful = true;

				return viewModel;
			}
		}

		public void Close(DocumentDetailsViewModel viewModel)
		{
			ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);
		}
	}
}
