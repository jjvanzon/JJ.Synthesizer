using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputGridPresenter : PresenterBaseWithoutSave<Document, AudioFileOutputGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly AudioFileOutputManager _audioFileOutputManager;

        public AudioFileOutputGridPresenter(AudioFileOutputManager audioFileOutputManager, IDocumentRepository documentRepository)
        {
            _audioFileOutputManager = audioFileOutputManager ?? throw new ArgumentNullException(nameof(audioFileOutputManager));
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }

        protected override Document GetEntity(AudioFileOutputGridViewModel userInput)
        {
            return _documentRepository.Get(userInput.DocumentID);
        }

        protected override AudioFileOutputGridViewModel ToViewModel(Document entity)
        {
            return entity.ToAudioFileOutputGridViewModel();
        }

        public AudioFileOutputGridViewModel Delete(AudioFileOutputGridViewModel userInput, int id)
        {
            return ExecuteAction(
                userInput,
                viewModel =>
                {
                    _audioFileOutputManager.Delete(id);
                });
        }
    }
}