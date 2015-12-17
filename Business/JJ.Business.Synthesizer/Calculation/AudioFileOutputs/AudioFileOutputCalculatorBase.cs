using JJ.Framework.IO;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using System.IO;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Configuration;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal abstract class AudioFileOutputCalculatorBase : IAudioFileOutputCalculator
    {
        private static PatchCalculatorTypeEnum _patchCalculatorTypeEnum = GetPatchCalculatorTypeEnum();

        private AudioFileOutput _audioFileOutput;
        private IPatchCalculator _patchCalculator;
        private double _amplifier;

        public AudioFileOutputCalculatorBase(
            AudioFileOutput audioFileOutput, 
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            IValidator validator = new AudioFileOutputValidator(audioFileOutput);
            validator.Verify();

            _audioFileOutput = audioFileOutput;

            // Prepare some objects
            IList<Outlet> outlets = _audioFileOutput.AudioFileOutputChannels
                                                    .OrderBy(x => x.IndexNumber)
                                                    .Select(x => x.Outlet)
                                                    .ToArray();

            var whiteNoiseCalculator = new WhiteNoiseCalculator(_audioFileOutput.SamplingRate);

            switch (_patchCalculatorTypeEnum)
            {
                case PatchCalculatorTypeEnum.OptimizedPatchCalculator:
                    _patchCalculator = new OptimizedPatchCalculator(outlets, whiteNoiseCalculator, curveRepository, sampleRepository, patchRepository);
                    break;

                case PatchCalculatorTypeEnum.InterpretedPatchCalculator:
                    _patchCalculator = new InterpretedPatchCalculator(outlets, whiteNoiseCalculator, curveRepository, sampleRepository, patchRepository);
                    break;

                default:
                    throw new ValueNotSupportedException(_patchCalculatorTypeEnum);
            }
        }

        public void Execute()
        {
            if (String.IsNullOrEmpty(_audioFileOutput.FilePath)) throw new NullOrEmptyException(() => _audioFileOutput.FilePath);

            // Because it could in theory be changed after construction, you validate here again, but a more lightweight solution is to clone the values.
            IValidator validator = new AudioFileOutputValidator(_audioFileOutput);
            validator.Verify();

            int channelCount = _audioFileOutput.GetChannelCount();

            double dt = 1.0 / _audioFileOutput.SamplingRate / _audioFileOutput.TimeMultiplier;
            double endTime = _audioFileOutput.GetEndTime();

            using (Stream stream = new FileStream(_audioFileOutput.FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Write header
                    AudioFileFormatEnum audioFileFormatEnum = _audioFileOutput.GetAudioFileFormatEnum();
                    switch (audioFileFormatEnum)
                    {
                        case AudioFileFormatEnum.Wav:
                            var audioFileInfo = new AudioFileInfo
                            {
                                SamplingRate = _audioFileOutput.SamplingRate,
                                BytesPerValue = SampleDataTypeHelper.SizeOf(_audioFileOutput.SampleDataType),
                                ChannelCount = channelCount,
                                SampleCount = (int)(endTime / dt)
                            };

                            WavHeaderStruct wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);
                            writer.WriteStruct(wavHeaderStruct);
                            break;

                        case AudioFileFormatEnum.Raw:
                            // Do nothing
                            break;

                        default:
                            throw new ValueNotSupportedException(audioFileFormatEnum);
                    }

                    double adjustedAmplifier = GetAmplifierAdjustedToSampleDataType(_audioFileOutput);

                    // Write Samples
                    for (double t = 0; t <= endTime; t += dt)
                    {
                        for (int i = 0; i < channelCount; i++)
                        {
                            double value = _patchCalculator.Calculate(t, i);
                            value *= adjustedAmplifier;

                            WriteValue(writer, value);
                        }
                    }
                }
            }
        }

        protected abstract double GetAmplifierAdjustedToSampleDataType(AudioFileOutput audioFileOutput);
        protected abstract void WriteValue(BinaryWriter binaryWriter, double value);

        // Helpers

        private static PatchCalculatorTypeEnum GetPatchCalculatorTypeEnum()
        {
            return ConfigurationHelper.GetSection<ConfigurationSection>().PatchCalculatorType;
        }
    }
}
