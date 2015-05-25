using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class AudioFileOutputPropertiesPresenter
    {
        private IAudioFileOutputRepository _audioFileOutputRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;

        public AudioFileOutputPropertiesViewModel ViewModel { get; set; }

        public AudioFileOutputPropertiesPresenter(
            IAudioFileOutputRepository audioFileOutputRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            _audioFileOutputRepository = audioFileOutputRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
        }

        public AudioFileOutputPropertiesViewModel Edit(int id)
        {
            //// Temporarily (2015-05-12) replaced by empty view model,
            //// until this view is used in the right place in the application navigation.
            //_viewModel = ViewModelHelper.CreateEmptyAudioFileOutputPropertiesViewModel();
            //_viewModel.Visible = true;
            //return _viewModel;

            bool mustCreateViewModel = ViewModel == null ||
                                       ViewModel.AudioFileOutput.ID != id;

            if (mustCreateViewModel)
            {
                AudioFileOutput entity = _audioFileOutputRepository.Get(id);
                ViewModel = entity.ToPropertiesViewModel(_audioFileFormatRepository, _sampleDataTypeRepository, _speakerSetupRepository);
            }

            return ViewModel;
        }

        // TODO: Refresh for details / properties views is probably not necessary.
        public AudioFileOutputPropertiesViewModel Refresh(AudioFileOutputPropertiesViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            AudioFileOutput entity = _audioFileOutputRepository.Get(viewModel.AudioFileOutput.ID);
            ViewModel = entity.ToPropertiesViewModel(_audioFileFormatRepository, _sampleDataTypeRepository, _speakerSetupRepository);

            ViewModel.Visible = viewModel.Visible;

            return ViewModel;
        }

        public AudioFileOutputPropertiesViewModel Close()
        {
            if (ViewModel == null)
            {
                ViewModel = ViewModelHelper.CreateEmptyAudioFileOutputPropertiesViewModel();
            }

            ViewModel.Visible = false;
            return ViewModel;
        }
    }
}
