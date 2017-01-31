using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Framework.IO;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer
{
    public class SampleManager
    {
        private readonly SampleRepositories _repositories;

        public SampleManager(SampleRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        // Validate

        public VoidResult Save(Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);

            var validators = new List<IValidator>
            {
                new SampleValidator(entity),
                new SampleValidator_UniqueName(entity)
            };

            if (entity.Document != null)
            {
                validators.Add(new SampleValidator_InDocument(entity));
            }

            var result = new VoidResult
            {
                Successful = validators.All(x => x.IsValid),
                Messages = validators.SelectMany(x => x.ValidationMessages).ToCanonical()
            };

            return result;
        }

        // Delete

        public void Delete(int id)
        {
            Sample entity = _repositories.SampleRepository.Get(id);
            Delete(entity);
        }

        public VoidResult Delete(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            IValidator validator = new SampleValidator_Delete(sample, _repositories.SampleRepository);

            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
            }
            else
            {
                sample.UnlinkRelatedEntities();
                _repositories.SampleRepository.Delete(sample);

                return new VoidResult
                {
                    Successful = true
                };
            }
        }

        // Create 

        /// <summary> Creates a Sample and sets its defaults. </summary>
        public Sample CreateSample(Document document = null, bool mustGenerateName = false)
        {
            var sample = new Sample();
            sample.ID = _repositories.IDRepository.GetID();
            _repositories.SampleRepository.Insert(sample);

            sample.LinkTo(document);

            ISideEffect sideEffect = new Sample_SideEffect_SetDefaults(sample, _repositories);
            sideEffect.Execute();

            if (mustGenerateName)
            {
                ISideEffect sideEffect2 = new Sample_SideEffect_GenerateName(sample);
                sideEffect2.Execute();
            }

            return sample;
        }

        public SampleInfo CreateSample(byte[] bytes, AudioFileFormatEnum audioFileFormatEnum)
        {
            if (bytes == null) throw new NullException(() => bytes);
            Stream stream = StreamHelper.BytesToStream(bytes);
            return CreateSample(stream, bytes, audioFileFormatEnum);
        }

        public SampleInfo CreateSample(Stream stream, AudioFileFormatEnum audioFileFormatEnum)
        {
            if (stream == null) throw new NullException(() => stream);

            stream.Position = 0;
            byte[] bytes = StreamHelper.StreamToBytes(stream);
            return CreateSample(stream, bytes, audioFileFormatEnum);
        }

        /// <summary> Creates a Sample from the stream and sets its defaults. Detects the format from the header. </summary>
        public SampleInfo CreateSample(byte[] bytes)
        {
            if (bytes == null) throw new NullException(() => bytes);
            Stream stream = StreamHelper.BytesToStream(bytes);
            return CreateSample(stream, bytes);
        }

        /// <summary> Creates a Sample from the stream and sets its defaults. Detects the format from the header. </summary>
        public SampleInfo CreateSample(Stream stream)
        {
            if (stream == null) throw new NullException(() => stream);

            stream.Position = 0;
            byte[] bytes = StreamHelper.StreamToBytes(stream);

            return CreateSample(stream, bytes);
        }

        // Misc

        /// <summary> Returns a calculator for each channel. </summary>
        public IList<ICalculatorWithPosition> CreateCalculators(Sample sample, byte[] bytes)
        {
            return SampleCalculatorFactory.CreateSampleCalculators(sample, bytes);
        }

        // Private Methods

        private SampleInfo CreateSample(Stream stream, byte[] bytes, AudioFileFormatEnum audioFileFormatEnum)
        {
            if (stream == null) throw new NullException(() => stream);

            switch (audioFileFormatEnum)
            {
                case AudioFileFormatEnum.Wav:
                    return CreateWavSample(stream, bytes);

                case AudioFileFormatEnum.Raw:
                    return CreateRawSample(bytes);

                default:
                    throw new ValueNotSupportedException(audioFileFormatEnum);
            }
        }

        private SampleInfo CreateSample(Stream stream, byte[] bytes)
        {
            // Detect wav header
            if (bytes.Length >= WavHeaderConstants.WAV_HEADER_LENGTH)
            {
                stream.Position = 0;
                WavHeaderStruct wavHeaderStruct;
                var reader = new BinaryReader(stream);
                wavHeaderStruct = reader.ReadStruct<WavHeaderStruct>();

                IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
                if (validator.IsValid)
                {
                    // Create Wav Sample
                    Sample wavSample = CreateWavSampleFromHeader(wavHeaderStruct);
                    stream.Position = 0;
                    return new SampleInfo
                    {
                        Sample = wavSample,
                        Bytes = bytes
                    };
                }
            }

            // Create Raw Sample
            SampleInfo rawSampleInfo = CreateSample(stream, AudioFileFormatEnum.Raw);
            return rawSampleInfo;
        }

        private SampleInfo CreateWavSample(Stream stream, byte[] bytes)
        {
            if (bytes.Length < WavHeaderConstants.WAV_HEADER_LENGTH)
            {
                throw new Exception($"A WAV file must be at least {WavHeaderConstants.WAV_HEADER_LENGTH} bytes.");
            }

            // Read header
            stream.Position = 0;
            WavHeaderStruct wavHeaderStruct;
            var reader = new BinaryReader(stream);
            wavHeaderStruct = reader.ReadStruct<WavHeaderStruct>();

            // Validate header
            IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
            validator.Assert();

            // Create Sample
            Sample sample = CreateWavSampleFromHeader(wavHeaderStruct);
            stream.Position = 0;

            return new SampleInfo
            {
                Sample = sample,
                Bytes = bytes
            };
        }

        private Sample CreateWavSampleFromHeader(WavHeaderStruct wavHeaderStruct)
        {
            AudioFileInfo audioFileInfo = WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct);

            Sample sample = CreateSample();

            sample.SamplingRate = audioFileInfo.SamplingRate;

            switch (audioFileInfo.ChannelCount)
            {
                case 1:
                    sample.SetSpeakerSetupEnum(SpeakerSetupEnum.Mono, _repositories.SpeakerSetupRepository);
                    break;

                case 2:
                    sample.SetSpeakerSetupEnum(SpeakerSetupEnum.Stereo, _repositories.SpeakerSetupRepository);
                    break;

                default:
                    throw new Exception($"audioFileInfo.ChannelCount value '{audioFileInfo.ChannelCount}' not supported.");
            }

            switch (audioFileInfo.BytesPerValue)
            {
                case 1:
                    sample.SetSampleDataTypeEnum(SampleDataTypeEnum.Byte, _repositories.SampleDataTypeRepository);
                    break;

                case 2:
                    sample.SetSampleDataTypeEnum(SampleDataTypeEnum.Int16, _repositories.SampleDataTypeRepository);
                    break;

                default:
                    throw new Exception($"audioFileInfo.BytesPerValue value '{audioFileInfo.BytesPerValue}' not supported.");
            }

            return sample;
        }

        private SampleInfo CreateRawSample(byte[] bytes)
        {
            Sample sample = CreateSample();
            sample.SetAudioFileFormatEnum(AudioFileFormatEnum.Raw, _repositories.AudioFileFormatRepository);

            return new SampleInfo
            {
                Sample = sample,
                Bytes = bytes
            };
        }
    }
}
