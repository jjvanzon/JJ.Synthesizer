using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class AudioFileOutputPropertiesPresenter
    {
        private IAudioFileOutputRepository _audioFileOutputRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;
        private IAudioFileOutputChannelRepository _audioFileOutputChannelRepository;
        private IOutletRepository _outletRepository;

        public AudioFileOutputPropertiesViewModel ViewModel { get; set; }

        public AudioFileOutputPropertiesPresenter(
            IAudioFileOutputRepository audioFileOutputRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository,
            IOutletRepository outletRepository)
        {
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (audioFileOutputChannelRepository == null) throw new NullException(() => audioFileOutputChannelRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);

            _audioFileOutputRepository = audioFileOutputRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _audioFileOutputChannelRepository = audioFileOutputChannelRepository;
            _outletRepository = outletRepository;
        }

        public AudioFileOutputPropertiesViewModel Show(AudioFileOutputPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                AudioFileOutput entity = userInput.ToEntityWithRelatedEntities(_audioFileOutputRepository, _audioFileFormatRepository, _sampleDataTypeRepository, _speakerSetupRepository, _audioFileOutputChannelRepository, _outletRepository);
                ViewModel = entity.ToPropertiesViewModel(_audioFileFormatRepository, _sampleDataTypeRepository, _speakerSetupRepository);
            }

            ViewModel.Visible = true;

            return ViewModel;
        }

        public AudioFileOutputPropertiesViewModel Close(AudioFileOutputPropertiesViewModel userInput)
        {
            Update(userInput);

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }

            return ViewModel;
        }

        public AudioFileOutputPropertiesViewModel LooseFocus(AudioFileOutputPropertiesViewModel userInput)
        {
            ViewModel = Update(userInput);

            return ViewModel;
        }

        private AudioFileOutputPropertiesViewModel Update(AudioFileOutputPropertiesViewModel userInput)
        {
            AudioFileOutput entity = userInput.ToEntityWithRelatedEntities(_audioFileOutputRepository, _audioFileFormatRepository, _sampleDataTypeRepository, _speakerSetupRepository, _audioFileOutputChannelRepository, _outletRepository);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                ViewModel = entity.ToPropertiesViewModel(_audioFileFormatRepository, _sampleDataTypeRepository, _speakerSetupRepository);
            }

            IValidator validator = new AudioFileOutputValidator(entity);
            if (!validator.IsValid)
            {
                ViewModel.Successful = false;
                ViewModel.Messages = validator.ValidationMessages.ToCanonical();
            }
            else
            {
                ViewModel.Messages = new Message[0];
                ViewModel.Successful = false;
            }

            return ViewModel;
        }

        private bool MustCreateViewModel(AudioFileOutputPropertiesViewModel existingViewModel, AudioFileOutputPropertiesViewModel userInput)
        {
            bool mustCreateViewModel = existingViewModel == null ||
                                       existingViewModel.AudioFileOutput.DocumentID != userInput.AudioFileOutput.DocumentID ||
                                       existingViewModel.AudioFileOutput.ListIndex != userInput.AudioFileOutput.ListIndex;

            return mustCreateViewModel;
        }
    }
}
