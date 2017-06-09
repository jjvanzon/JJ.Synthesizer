using JetBrains.Annotations;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputGridPresenter : GridPresenterBase<AudioFileOutputGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly AudioFileOutputManager _audioFileOutputManager;

        public AudioFileOutputGridPresenter([NotNull] RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentRepository = repositories.DocumentRepository ?? throw new NullException(() => repositories.DocumentRepository);
            _audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
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
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    _audioFileOutputManager.Delete(id);
                });
        }
    }
}