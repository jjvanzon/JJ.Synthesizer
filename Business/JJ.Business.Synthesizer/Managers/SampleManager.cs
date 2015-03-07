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

namespace JJ.Business.Synthesizer.Managers
{
    public class SampleManager
    {
        private ISampleRepository _sampleRepository;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;
        private IInterpolationTypeRepository _interpolationTypeRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;

        public SampleManager(
            ISampleRepository sampleRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            IAudioFileFormatRepository audioFileFormatRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);

            _sampleRepository = sampleRepository;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _interpolationTypeRepository = interpolationTypeRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
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

        public double GetDuration(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.SampleChannels.Count == 0) throw new Exception("sample.SampleChannels.Count cannot be null.");
            if (sample.SamplingRate == 0) throw new Exception("sample.SamplingRate cannot be null.");

            double duration = (double)sample.SampleChannels[0].RawBytes.Length / sample.SamplingRate * sample.TimeMultiplier;
            return duration;
        }
    }
}
