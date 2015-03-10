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
using System.IO;
using JJ.Framework.Common;
using JJ.Framework.IO;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Constants;

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

        /// <summary>
        /// Creates a Sample and sets its defaults.
        /// </summary>
        public Sample CreateSample()
        {
            Sample sample = _sampleRepository.Create();

            ISideEffect sideEffect = new Sample_SideEffect_SetDefaults(sample, _sampleDataTypeRepository, _speakerSetupRepository, _interpolationTypeRepository, _audioFileFormatRepository);
            sideEffect.Execute();

            return sample;
        }

        public Sample CreateSample(Stream stream, AudioFileFormatEnum audioFileFormatEnum)
        {
            if (stream == null) throw new NullException(() => stream);

            switch (audioFileFormatEnum)
            {
                case AudioFileFormatEnum.Wav:
                    return CreateWavSample(stream);

                case AudioFileFormatEnum.Raw:
                    return CreateRawSample(stream);

                default:
                    throw new ValueNotSupportedException(audioFileFormatEnum);
            }
        }

        /// <summary>
        /// Creates a Sample from the stream and sets its defaults.
        /// Detects the format from the header.
        /// </summary>
        public Sample CreateSample(Stream stream)
        {
            if (stream == null) throw new NullException(() => stream);

            stream.Position = 0;
            if (stream.Length >= WavHeaderConstants.WAV_HEADER_LENGTH)
            {
                WavHeaderStruct wavHeaderStruct;
                var reader = new BinaryReader(stream);
                wavHeaderStruct = reader.ReadStruct<WavHeaderStruct>();

                IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
                if (validator.IsValid)
                {
                    Sample wavSample = CreateWavSample(wavHeaderStruct);
                    stream.Position = 0;
                    wavSample.Bytes = StreamHelper.StreamToBytes(stream);
                    return wavSample;
                }
            }

            Sample rawSample = CreateSample(stream, AudioFileFormatEnum.Raw);
            return rawSample;
        }

        private Sample CreateWavSample(Stream stream)
        {
            // Read header
            if (stream.Length < WavHeaderConstants.WAV_HEADER_LENGTH)
            {
                throw new Exception(String.Format("A WAV file must be at least {0} bytes.", WavHeaderConstants.WAV_HEADER_LENGTH));
            }
            stream.Position = 0;
            WavHeaderStruct wavHeaderStruct;
            var reader = new BinaryReader(stream);
            wavHeaderStruct = reader.ReadStruct<WavHeaderStruct>();

            // Validate header
            IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
            validator.Verify();

            // Create Sample
            Sample sample = CreateWavSample(wavHeaderStruct);
            stream.Position = 0;
            sample.Bytes = StreamHelper.StreamToBytes(stream);

            return sample;
        }

        private Sample CreateWavSample(WavHeaderStruct wavHeaderStruct)
        {
            AudioFileInfo audioFileInfo = WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct);

            Sample sample = CreateSample();

            sample.SamplingRate = audioFileInfo.SamplingRate;

            switch (audioFileInfo.ChannelCount)
            {
                case 1:
                    sample.SetSpeakerSetupEnum(SpeakerSetupEnum.Mono, _speakerSetupRepository);
                    break;

                case 2:
                    sample.SetSpeakerSetupEnum(SpeakerSetupEnum.Stereo, _speakerSetupRepository);
                    break;

                default:
                    throw new Exception(String.Format("audioFile.ChannelCount value '{0}' not supported.", audioFileInfo.ChannelCount));
            }

            switch (audioFileInfo.BytesPerValue)
            {
                case 1:
                    sample.SetSampleDataTypeEnum(SampleDataTypeEnum.Byte, _sampleDataTypeRepository);
                    break;

                case 2:
                    sample.SetSampleDataTypeEnum(SampleDataTypeEnum.Int16, _sampleDataTypeRepository);
                    break;

                default:
                    throw new Exception(String.Format("audioFile.BytesPerValue value '{0}' not supported.", audioFileInfo.BytesPerValue));
            }

            return sample;
        }

        private Sample CreateRawSample(Stream stream)
        {
            Sample sample = CreateSample();
            sample.SetAudioFileFormatEnum(AudioFileFormatEnum.Raw, _audioFileFormatRepository);
            stream.Position = 0;
            sample.Bytes = StreamHelper.StreamToBytes(stream);
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
