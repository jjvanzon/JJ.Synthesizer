using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class DocumentCannotDeletePresenter : PresenterBase<DocumentCannotDeleteViewModel>
	{
		private readonly IDocumentRepository _documentRepository;

		public DocumentCannotDeletePresenter(IDocumentRepository documentRepository)
		{
			_documentRepository = documentRepository ?? throw new NullException(() => documentRepository);
		}

		/// <see cref="PresenterBase{}.ExecuteNonPersistedAction"/>
		public DocumentCannotDeleteViewModel Show(int id, IList<string> messages)
		{
			// GetEntity
			Document document = _documentRepository.Get(id);

			// ToViewModel
			DocumentCannotDeleteViewModel viewModel = document.ToCannotDeleteViewModel(messages);

			// Non-Persisted
			viewModel.Visible = true;

			// Successful
			viewModel.Successful = true;

			return viewModel;
		}

		public void OK(DocumentCannotDeleteViewModel viewModel)
		{
			ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);
		}
	}
}
