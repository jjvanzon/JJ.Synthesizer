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

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    /// <summary>
    /// Use the pre-calculated fields of the base class.
    /// </summary>
    internal abstract class AudioFileOutputCalculatorBase : IAudioFileOutputCalculator
    {
        private string _filePath;
        private AudioFileOutput _audioFileOutput;

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

            IList<AudioFileOutputChannel> audioFileOutputChannels = _audioFileOutput.AudioFileOutputChannels.OrderBy(x => x.Index).ToArray();
            Outlet[] outlets = audioFileOutputChannels.Select(x => x.Outlet).ToArray();
            OperatorCalculator[] operatorCalculators = audioFileOutputChannels.Select(x => new OperatorCalculator(x.Index)).ToArray();

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
                                ChannelCount = channelCount,
                                SampleCount = (int)(endTime / dt),
                                BitsPerValue = SampleDataTypeHelper.SizeOf(_audioFileOutput.SampleDataType) * 8,
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
                            Outlet outlet = outlets[i];

                            double value = 0;
                            if (outlet != null) // TODO: I do not like this 'if'.
                            {
                                value = operatorCalculators[i].CalculateValue(outlet, t);
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
