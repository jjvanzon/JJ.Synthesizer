using JJ.Framework.IO;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using System.IO;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    /// <summary>
    /// Use the pre-calculated fields of the base class.
    /// </summary>
    internal abstract class AudioFileOutputCalculatorBase : IAudioFileOutputCalculator
    {
        private const bool IS_OPTIMIZED = true;

        private string _filePath;
        private AudioFileOutput _audioFileOutput;

        AudioFileOutputChannel[] _audioFileOutputChannels;
        Outlet[] _outlets;
        IOperatorCalculator[] _operatorCalculators;

        public AudioFileOutputCalculatorBase(AudioFileOutput audioFileOutput, string filePath)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            if (String.IsNullOrEmpty(audioFileOutput.FilePath) &&
                String.IsNullOrEmpty(filePath))
            {
                throw new Exception("Either filePath must be passed explicitly or audioFileOutput.FilePath must be filled in.");
            }

            _audioFileOutput = audioFileOutput;
            _filePath = filePath;

            // Prepare some objects
            int channelCount = _audioFileOutput.AudioFileOutputChannels.Count;
            _audioFileOutputChannels = _audioFileOutput.AudioFileOutputChannels.OrderBy(x => x.IndexNumber).ToArray();
            _outlets = _audioFileOutputChannels.Select(x => x.Outlet).ToArray();

            _operatorCalculators = new IOperatorCalculator[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                IOperatorCalculator operatorCalculator;
                if (IS_OPTIMIZED)
                {
                    operatorCalculator = new OptimizedOperatorCalculator(_outlets);
                }
                else
                {
                    operatorCalculator = new InterpretedOperatorCalculator(i, _outlets[i]);
                }
                
                _operatorCalculators[i] = operatorCalculator;
            }
        }

        public void Execute()
        {
            IValidator validator = new AudioFileOutputValidator(_audioFileOutput);
            validator.Verify();

            string filePath = _filePath;
            if (String.IsNullOrEmpty(filePath))
            {
                filePath = _audioFileOutput.FilePath;
            }

            int channelCount = _audioFileOutput.GetChannelCount();

            double dt = 1.0 / _audioFileOutput.SamplingRate / _audioFileOutput.TimeMultiplier;
            double endTime = _audioFileOutput.GetEndTime();

            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
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

                    // Write Samples
                    for (double t = 0; t <= endTime; t += dt)
                    {
                        for (int i = 0; i < channelCount; i++)
                        {
                            Outlet outlet = _outlets[i];

                            double value = 0;
                            if (outlet != null) // TODO: I do not like this 'if'.
                            {
                                value = _operatorCalculators[i].Calculate(t, i);
                                value *= _audioFileOutput.Amplifier;
                            }

                            WriteValue(writer, value);
                        }
                    }
                }
            }
        }

        protected abstract void WriteValue(BinaryWriter binaryWriter, double value);
    }
}
