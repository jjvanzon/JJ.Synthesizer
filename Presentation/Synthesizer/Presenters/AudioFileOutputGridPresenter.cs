using System;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputGridPresenter : GridPresenterBase<AudioFileOutputGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly AudioFileOutputManager _audioFileOutputManager;

        public AudioFileOutputGridPresenter(AudioFileOutputManager audioFileOutputManager, IDocumentRepository documentRepository)
        {
            _audioFileOutputManager = audioFileOutputManager ?? throw new ArgumentNullException(nameof(audioFileOutputManager));
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }

        protected override AudioFileOutputGridViewModel CreateViewModel(AudioFileOutputGridViewModel userInput)
        {
            // GetEntity
            Document document = _documentRepository.Get(userInput.DocumentID);

            // ToViewModel
            AudioFileOutputGridViewModel viewModel = document.ToAudioFileOutputGridViewModel();

            return viewModel;
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