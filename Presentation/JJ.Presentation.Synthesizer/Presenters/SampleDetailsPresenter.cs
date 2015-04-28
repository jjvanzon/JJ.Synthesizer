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
    public class SampleDetailsPresenter
    {
        private ISampleRepository _sampleRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;
        private IInterpolationTypeRepository _interpolationTypeRepository;

        public SampleDetailsPresenter(
            ISampleRepository sampleRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);

            _sampleRepository = sampleRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _interpolationTypeRepository = interpolationTypeRepository;
        }

        public SampleDetailsViewModel Show(int id)
        {
            Sample entity = _sampleRepository.Get(id);
            SampleDetailsViewModel viewModel = entity.ToDetailsViewModel(_audioFileFormatRepository, _sampleDataTypeRepository, _speakerSetupRepository, _interpolationTypeRepository);
            return viewModel;
        }
    }
}
