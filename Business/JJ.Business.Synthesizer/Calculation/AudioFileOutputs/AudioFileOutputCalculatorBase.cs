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
        protected string _filePath;

        /// <summary>
        /// The array index is the channel index.
        /// </summary>
        protected OperatorCalculator[] _operatorCalculators;

        /// <summary>
        /// The array index is the channel index.
        /// </summary>
        protected Outlet[] _outlets;

        protected double _dt;
        protected double _endTime;
        protected int _channelCount;

        /// <summary>
        /// The base class will verify the data.
        /// </summary>
        protected AudioFileOutput _audioFileOutput;

        public AudioFileOutputCalculatorBase(AudioFileOutput audioFileOutput, string filePath)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            if (String.IsNullOrEmpty(filePath)) 
            {
                filePath = audioFileOutput.FilePath;
            }

            if (String.IsNullOrEmpty(filePath)) throw new Exception("Either filePath must be passed explicitly or audioFileOutput.FilePath must be filled in.");

            IValidator validator = new AudioFileOutputValidator(audioFileOutput);
            validator.Verify();

            _audioFileOutput = audioFileOutput;
            _filePath = filePath;

            // Pre-calculate some data.
            _channelCount = _audioFileOutput.GetChannelCount();

            IList<AudioFileOutputChannel> audioFileOutputChannels = _audioFileOutput.AudioFileOutputChannels.OrderBy(x => x.Index).ToArray();
            _outlets = audioFileOutputChannels.Select(x => x.Outlet).ToArray();
            _operatorCalculators = audioFileOutputChannels.Select(x => new OperatorCalculator(x.Index)).ToArray();

            _dt = 1.0 / _audioFileOutput.SamplingRate / _audioFileOutput.TimeMultiplier;
            _endTime = _audioFileOutput.GetEndTime();
        }

        public abstract void Execute();

        protected void ConditionallyWriteHeader(BinaryWriter writer)
        {
            AudioFileFormatEnum audioFileFormatEnum = _audioFileOutput.GetAudioFileFormatEnum();
            switch (audioFileFormatEnum)
            {
                case AudioFileFormatEnum.Wav:
                    var audioFileInfo = new AudioFileInfo
                    {
                        SamplingRate = _audioFileOutput.SamplingRate,
                        ChannelCount = _channelCount,
                        SampleCount = (int)(_endTime / _dt),
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
        }
    }
}
