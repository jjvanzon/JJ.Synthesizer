using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using JJ.Business.Synthesizer.Helpers;
using System.IO;
using JJ.Framework.Common;
using JJ.Framework.IO;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Calculation.Samples;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;

namespace JJ.Business.Synthesizer.Managers
{
    public class SampleManager
    {
        private SampleRepositories _repositories;

        public SampleManager(SampleRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        // Validate

        public VoidResult Validate(Sample entity)
        {
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

        public ISampleCalculator CreateCalculator(Sample sample, byte[] bytes)
        {
            return SampleCalculatorFactory.CreateSampleCalculator(sample, bytes);
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
                    return CreateRawSample(stream, bytes);

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
                throw new Exception(String.Format("A WAV file must be at least {0} bytes.", WavHeaderConstants.WAV_HEADER_LENGTH));
            }

            // Read header
            stream.Position = 0;
            WavHeaderStruct wavHeaderStruct;
            var reader = new BinaryReader(stream);
            wavHeaderStruct = reader.ReadStruct<WavHeaderStruct>();

            // Validate header
            IValidator validator = new WavHeaderStructValidator(wavHeaderStruct);
            validator.Verify();

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
                    throw new Exception(String.Format("audioFile.ChannelCount value '{0}' not supported.", audioFileInfo.ChannelCount));
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
                    throw new Exception(String.Format("audioFile.BytesPerValue value '{0}' not supported.", audioFileInfo.BytesPerValue));
            }

            return sample;
        }

        private SampleInfo CreateRawSample(Stream stream, byte[] bytes)
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
