using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class AudioFileOutputPropertiesPresenter
    {
        private AudioFileOutputRepositories _audioFileOutputRepositories;

        public AudioFileOutputPropertiesViewModel ViewModel { get; set; }

        public AudioFileOutputPropertiesPresenter(AudioFileOutputRepositories audioFileOutputRepositories)
        {
            if (audioFileOutputRepositories == null) throw new NullException(() => audioFileOutputRepositories);

            _audioFileOutputRepositories = audioFileOutputRepositories;
        }

        public AudioFileOutputPropertiesViewModel Show(AudioFileOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                throw new Exception(String.Format(
                    "{0} cannot autonomously convert user input to entity and then create a new view model, " +
                    "because it depends on its document's patches' patch outlets. Assign {0}.ViewModel instead", 
                    GetType().Name));

                AudioFileOutput entity = userInput.ToEntityWithRelatedEntities(_audioFileOutputRepositories);

                ViewModel = entity.ToPropertiesViewModel(
                    _audioFileOutputRepositories.AudioFileFormatRepository, 
                    _audioFileOutputRepositories.SampleDataTypeRepository, 
                    _audioFileOutputRepositories.SpeakerSetupRepository);
            }

            ViewModel.Visible = true;

            return ViewModel;
        }

        public AudioFileOutputPropertiesViewModel Close(AudioFileOutputPropertiesViewModel userInput)
        {
            ViewModel = Update(userInput);

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }

            return ViewModel;
        }

        public AudioFileOutputPropertiesViewModel LoseFocus(AudioFileOutputPropertiesViewModel userInput)
        {
            ViewModel = Update(userInput);

            return ViewModel;
        }

        public void Clear()
        {
            ViewModel = null;
        }

        private AudioFileOutputPropertiesViewModel Update(AudioFileOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            AudioFileOutput entity = userInput.ToEntityWithRelatedEntities(_audioFileOutputRepositories);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                ViewModel = entity.ToPropertiesViewModel(
                    _audioFileOutputRepositories.AudioFileFormatRepository, 
                    _audioFileOutputRepositories.SampleDataTypeRepository, 
                    _audioFileOutputRepositories.SpeakerSetupRepository);
            }

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

            return ViewModel;
        }

        private bool MustCreateViewModel(AudioFileOutputPropertiesViewModel existingViewModel, AudioFileOutputPropertiesViewModel userInput)
        {
            return existingViewModel == null ||
                   existingViewModel.Entity.ID != userInput.Entity.ID;
        }
    }
}
