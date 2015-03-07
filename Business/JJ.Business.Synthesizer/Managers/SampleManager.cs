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

namespace JJ.Business.Synthesizer.Managers
{
    public class SampleManager
    {
        private ISampleRepository _sampleRepository;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;
        private IInterpolationTypeRepository _interpolationTypeRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;
        private ISampleChannelRepository _sampleChannelRepository;

        public SampleManager(
            ISampleRepository sampleRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleChannelRepository sampleChannelRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleChannelRepository == null) throw new NullException(() => sampleChannelRepository);

            _sampleRepository = sampleRepository;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _interpolationTypeRepository = interpolationTypeRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
            _sampleChannelRepository = sampleChannelRepository;
        }

        public Sample CreateSample()
        {
            Sample sample = _sampleRepository.Create();

            ISideEffect sideEffect = new Sample_SideEffect_SetDefaults(sample, _sampleDataTypeRepository, _speakerSetupRepository, _interpolationTypeRepository, _audioFileFormatRepository);
            sideEffect.Execute();

            SetSpeakerSetup(sample, sample.GetSpeakerSetupEnum());

            return sample;
        }

        /// <summary>
        /// Sets the speaker setup and adjusts the SampleChannels accordingly.
        /// </summary>
        public void SetSpeakerSetup(Sample sample, SpeakerSetupEnum speakerSetupEnum)
        {
            if (sample == null) throw new NullException(() => sample);
            if (speakerSetupEnum == SpeakerSetupEnum.Undefined) throw new Exception("speakerSetupEnum cannot be 'Undefined'.");

            SpeakerSetup speakerSetup = _speakerSetupRepository.Get((int)speakerSetupEnum);
            SetSpeakerSetup(sample, speakerSetup);
        }

        /// <summary>
        /// Sets the speaker setup and adjusts the SampleChannels accordingly.
        /// </summary>
        public void SetSpeakerSetup(Sample sample, SpeakerSetup speakerSetup)
        {
            if (sample == null) throw new NullException(() => sample);
            if (speakerSetup == null) throw new NullException(() => speakerSetup);

            sample.LinkTo(speakerSetup);

            IList<SampleChannel> entitiesToKeep = new List<SampleChannel>(sample.SampleChannels.Count);

            for (int i = sample.SampleChannels.Count; i < speakerSetup.SpeakerSetupChannels.Count; i++)
			{
                Channel channel = speakerSetup.SpeakerSetupChannels[i].Channel;

			    SampleChannel sampleChannel = TryGetSampleChannel(sample, i);
                if (sampleChannel == null)
                {
                    sampleChannel = _sampleChannelRepository.Create();
                    sampleChannel.LinkTo(sample);
                }
                sampleChannel.LinkTo(channel);

                entitiesToKeep.Add(sampleChannel);
			}

            IList<SampleChannel> entitiesToDelete = sample.SampleChannels.Except(entitiesToKeep).ToArray();
            foreach (SampleChannel entityToDelete in entitiesToDelete)
            {
                entityToDelete.UnlinkRelatedEntities();
                _sampleChannelRepository.Delete(entityToDelete);
            }
        }

        private SampleChannel TryGetSampleChannel(Sample sample, int index)
        {
            if (index >= sample.SampleChannels.Count)
            {
                return null;
            }

            return sample.SampleChannels[index];
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
