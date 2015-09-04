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

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputPropertiesPresenter
    {
        private AudioFileOutputRepositories _repositories;

        public AudioFileOutputPropertiesViewModel ViewModel { get; set; }

        public AudioFileOutputPropertiesPresenter(AudioFileOutputRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
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

            IValidator validator = new AudioFileOutputValidator_InDocument(entity);
            if (!validator.IsValid)
            {
                ViewModel.Successful = false;
                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
            }
            else
            {
                ViewModel.ValidationMessages = new List<Message>();
                ViewModel.Successful = true;
            }
        }

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
