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
    public class AudioFileOutputDetailsPresenter
    {
        private IAudioFileOutputRepository _audioFileOutputRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;

        public AudioFileOutputDetailsPresenter(
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

        public AudioFileOutputDetailsViewModel Edit(int id)
        {
            AudioFileOutput entity = _audioFileOutputRepository.Get(id);
            AudioFileOutputDetailsViewModel viewModel = entity.ToDetailsViewModel(_audioFileFormatRepository, _sampleDataTypeRepository, _speakerSetupRepository);
            return viewModel;
        }
    }
}
