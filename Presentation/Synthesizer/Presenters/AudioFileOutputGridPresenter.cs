using System;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
// ReSharper disable ImplicitlyCapturedClosure

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class AudioFileOutputGridPresenter : EntityPresenterWithoutSaveBase<Document, AudioFileOutputGridViewModel>
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly AudioFileOutputFacade _audioFileOutputFacade;

		public AudioFileOutputGridPresenter(AudioFileOutputFacade audioFileOutputFacade, IDocumentRepository documentRepository)
		{
			_audioFileOutputFacade = audioFileOutputFacade ?? throw new ArgumentNullException(nameof(audioFileOutputFacade));
			_documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
		}

		protected override Document GetEntity(AudioFileOutputGridViewModel userInput) => _documentRepository.Get(userInput.DocumentID);

	    protected override AudioFileOutputGridViewModel ToViewModel(Document entity) => entity.ToAudioFileOutputGridViewModel();

	    public AudioFileOutputGridViewModel Delete(AudioFileOutputGridViewModel userInput, int id) => ExecuteAction(userInput, _ => _audioFileOutputFacade.Delete(id));

	    public AudioFileOutputGridViewModel Create(AudioFileOutputGridViewModel userInput)
		{
			AudioFileOutput audioFileOutput = null;
			return ExecuteAction(
				userInput,
				document => audioFileOutput = _audioFileOutputFacade.Create(document),
				viewModel => viewModel.CreatedAudioFileOutputID = audioFileOutput.ID);
		}
	}
}