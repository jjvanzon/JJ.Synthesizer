using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class ScaleGridPresenter : EntityPresenterWithoutSaveBase<Document, ScaleGridViewModel>
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly ScaleManager _scaleManager;

		public ScaleGridPresenter(IDocumentRepository documentRepository, ScaleManager scaleManager)
		{
			_documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
			_scaleManager = scaleManager ?? throw new ArgumentNullException(nameof(scaleManager));
		}

		protected override Document GetEntity(ScaleGridViewModel userInput) => _documentRepository.Get(userInput.DocumentID);

		protected override ScaleGridViewModel ToViewModel(Document entity) => entity.Scales.ToGridViewModel(entity.ID);

		public ScaleGridViewModel Delete(ScaleGridViewModel userInput, int id)
		{
			return ExecuteAction(userInput, x => _scaleManager.DeleteWithRelatedEntities(id));
		}

		public ScaleGridViewModel Create(ScaleGridViewModel userInput)
		{
			return ExecuteAction(userInput, x => _scaleManager.Create(x));
		}
	}
}
