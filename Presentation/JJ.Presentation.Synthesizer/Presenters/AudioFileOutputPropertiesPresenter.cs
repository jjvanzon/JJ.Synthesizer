using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Helpers;
using System.Collections.Generic;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Managers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputPropertiesPresenter
    {
        private AudioFileOutputRepositories _repositories;
        private AudioFileOutputManager _audioFileOutputManager;

        public AudioFileOutputPropertiesViewModel ViewModel { get; set; }

        public AudioFileOutputPropertiesPresenter(AudioFileOutputRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _audioFileOutputManager = new AudioFileOutputManager(_repositories);
        }

        public void Show()
        {
            AssertViewModel();
            ViewModel.Visible = true;
        }

        public void Refresh()
        {
            AssertViewModel();

            AudioFileOutput entity = _repositories.AudioFileOutputRepository.Get(ViewModel.Entity.ID);

            bool visible = ViewModel.Visible;

            ViewModel = entity.ToPropertiesViewModel(
                _repositories.AudioFileFormatRepository,
                _repositories.SampleDataTypeRepository,
                _repositories.SpeakerSetupRepository);

            ViewModel.Visible = visible;
        }

        public void Close()
        {
            AssertViewModel();

            Update();

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }
        }

        public void LoseFocus()
        {
            Update();
        }

        private void Update()
        {
            AssertViewModel();

            AudioFileOutput entity = ViewModel.ToEntityWithRelatedEntities(_repositories);

            VoidResult result = _audioFileOutputManager.Validate(entity);

            ViewModel.Successful = result.Successful;
            ViewModel.ValidationMessages = result.Messages;
        }

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}