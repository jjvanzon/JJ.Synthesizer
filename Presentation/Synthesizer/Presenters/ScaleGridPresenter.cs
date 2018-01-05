using System;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class ScaleGridPresenter : EntityPresenterWithoutSaveBase<Document, ScaleGridViewModel>
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly ScaleFacade _scaleFacade;

		public ScaleGridPresenter(IDocumentRepository documentRepository, ScaleFacade scaleFacade)
		{
			_documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
			_scaleFacade = scaleFacade ?? throw new ArgumentNullException(nameof(scaleFacade));
		}

		protected override Document GetEntity(ScaleGridViewModel userInput) => _documentRepository.Get(userInput.DocumentID);

		protected override ScaleGridViewModel ToViewModel(Document entity) => entity.Scales.ToGridViewModel(entity.ID);

		public ScaleGridViewModel Delete(ScaleGridViewModel userInput, int id)
		{
			return ExecuteAction(userInput, x => _scaleFacade.DeleteWithRelatedEntities(id));
		}

		public ScaleGridViewModel Create(ScaleGridViewModel userInput)
		{
			Scale scale = null;
			return ExecuteAction(
				userInput,
				document => scale = _scaleFacade.Create(document),
				viewModel => viewModel.CreatedScaleID = scale.ID);
		}
	}
}
