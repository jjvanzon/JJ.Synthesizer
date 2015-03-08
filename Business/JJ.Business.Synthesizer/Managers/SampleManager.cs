using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Business;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationMessage = JJ.Business.CanonicalModel.ValidationMessage;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Managers
{
    public class SampleManager
    {
        private ISampleRepository _sampleRepository;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;
        private IInterpolationTypeRepository _interpolationTypeRepository;

        public SampleManager(
            ISampleRepository sampleRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            IInterpolationTypeRepository interpolationTypeRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);

            _sampleRepository = sampleRepository;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
            _interpolationTypeRepository = interpolationTypeRepository;
        }

        public Sample CreateSample()
        {
            Sample sample = _sampleRepository.Create();

            ISideEffect sideEffect = new Sample_SideEffect_SetDefaults(sample, _sampleDataTypeRepository, _speakerSetupRepository, _interpolationTypeRepository, _audioFileFormatRepository);
            sideEffect.Execute();

            return sample;
        }

        public IValidator ValidateSample(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            IValidator sampleValidator = new SampleValidator(sample);
            return sampleValidator;
        }
    }
}
