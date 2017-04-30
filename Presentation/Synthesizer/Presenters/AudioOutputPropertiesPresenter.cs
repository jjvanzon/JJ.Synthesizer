using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioOutputPropertiesPresenter : PropertiesPresenterBase<AudioOutputPropertiesViewModel>
    {
        private readonly IAudioOutputRepository _audioOutputRepository;

        private readonly AudioOutputManager _audioOutputManager;

        public AudioOutputPropertiesPresenter(
            IAudioOutputRepository audioOutputRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IIDRepository idRepository)
        {
            _audioOutputRepository = audioOutputRepository ?? throw new NullException(() => audioOutputRepository);

            _audioOutputManager = new AudioOutputManager(_audioOutputRepository, speakerSetupRepository, idRepository);
        }

        protected override AudioOutputPropertiesViewModel CreateViewModel(AudioOutputPropertiesViewModel userInput)
        {
            // GetEntity
            AudioOutput entity = _audioOutputRepository.Get(userInput.Entity.ID);

            // ToViewModel
            AudioOutputPropertiesViewModel viewModel = entity.ToPropertiesViewModel();

            return viewModel;
        }

        protected override void UpdateEntity(AudioOutputPropertiesViewModel viewModel)
        {
            // ToEntity: was already done by the MainPresenter.

            // GetEntity
            AudioOutput entity = _audioOutputRepository.Get(viewModel.Entity.ID);

            // Business
            VoidResult result = _audioOutputManager.Save(entity);

            // Non-Persisted
            viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

            // Successful?
            viewModel.Successful = result.Successful;
        }
    }
}