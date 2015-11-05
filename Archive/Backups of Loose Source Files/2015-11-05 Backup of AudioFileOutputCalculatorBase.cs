//using JJ.Framework.IO;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Framework.Validation;
//using JJ.Data.Synthesizer;
//using System;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using System.IO;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Managers;
//using JJ.Framework.Common;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
//using JJ.Business.Synthesizer.Configuration;
//using System.Collections.Generic;

//namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
//{
//    /// <summary>
//    /// Use the pre-calculated fields of the base class, when deriving from this class.
//    /// </summary>
//    internal abstract class AudioFileOutputCalculatorBase : IAudioFileOutputCalculator
//    {
//        private string _filePath;
//        private AudioFileOutput _audioFileOutput;

//        private AudioFileOutputChannel[] _audioFileOutputChannels;
//        private Outlet[] _outlets;
//        private IPatchCalculator[] _patchCalculators;

//        private static PatchCalculatorTypeEnum _patchCalculatorTypeEnum;

//        static AudioFileOutputCalculatorBase()
//        {
//            var config = ConfigurationHelper.GetSection<ConfigurationSection>();

//            _patchCalculatorTypeEnum = config.PatchCalculatorType;
//        }

//        public AudioFileOutputCalculatorBase(AudioFileOutput audioFileOutput, ICurveRepository curveRepository, ISampleRepository sampleRepository, IDocumentRepository documentRepository)
//        {
//            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
//            if (curveRepository == null) throw new NullException(() => curveRepository);
//            if (sampleRepository == null) throw new NullException(() => sampleRepository);
//            if (documentRepository == null) throw new NullException(() => documentRepository);

//            IValidator validator = new AudioFileOutputValidator(audioFileOutput);
//            validator.Verify();

//            if (String.IsNullOrEmpty(audioFileOutput.FilePath)) throw new NullOrEmptyException(() => audioFileOutput.FilePath);

//            _audioFileOutput = audioFileOutput;
//            _filePath = audioFileOutput.FilePath;

//            // Prepare some objects
//            int channelCount = _audioFileOutput.AudioFileOutputChannels.Count;
//            _audioFileOutputChannels = _audioFileOutput.AudioFileOutputChannels.OrderBy(x => x.IndexNumber).ToArray();
//            _outlets = _audioFileOutputChannels.Select(x => x.Outlet).ToArray();

//            var whiteNoiseCalculator = new WhiteNoiseCalculator(_audioFileOutput.SamplingRate);

//            _patchCalculators = new IPatchCalculator[channelCount];
//            for (int i = 0; i < channelCount; i++)
//            {
//                IPatchCalculator patchCalculator;
//                switch (_patchCalculatorTypeEnum)
//                {
//                    case PatchCalculatorTypeEnum.OptimizedPatchCalculator:
//                        patchCalculator = new OptimizedPatchCalculator(_outlets, whiteNoiseCalculator, curveRepository, sampleRepository, documentRepository);
//                        break;

//                    case PatchCalculatorTypeEnum.InterpretedPatchCalculator:
//                        patchCalculator = new InterpretedPatchCalculator(_outlets, whiteNoiseCalculator, curveRepository, sampleRepository, documentRepository);
//                        break;

//                    default:
//                        throw new ValueNotSupportedException(_patchCalculatorTypeEnum);
//                }

//                _patchCalculators[i] = patchCalculator;
//            }
//        }

//        public void Execute()
//        {
//            IValidator validator = new AudioFileOutputValidator(_audioFileOutput);
//            validator.Verify();

//            string filePath = _filePath;
//            if (String.IsNullOrEmpty(filePath))
//            {
//                filePath = _audioFileOutput.FilePath;
//            }

//            int channelCount = _audioFileOutput.GetChannelCount();

//            double dt = 1.0 / _audioFileOutput.SamplingRate / _audioFileOutput.TimeMultiplier;
//            double endTime = _audioFileOutput.GetEndTime();

//            using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
//            {
//                using (var writer = new BinaryWriter(stream))
//                {
//                    // Write header
//                    AudioFileFormatEnum audioFileFormatEnum = _audioFileOutput.GetAudioFileFormatEnum();
//                    switch (audioFileFormatEnum)
//                    {
//                        case AudioFileFormatEnum.Wav:
//                            var audioFileInfo = new AudioFileInfo
//                            {
//                                SamplingRate = _audioFileOutput.SamplingRate,
//                                BytesPerValue = SampleDataTypeHelper.SizeOf(_audioFileOutput.SampleDataType),
//                                ChannelCount = channelCount,
//                                SampleCount = (int)(endTime / dt)
//                            };

//                            WavHeaderStruct wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);
//                            writer.WriteStruct(wavHeaderStruct);
//                            break;

//                        case AudioFileFormatEnum.Raw:
//                            // Do nothing
//                            break;

//                        default:
//                            throw new ValueNotSupportedException(audioFileFormatEnum);
//                    }

//                    // Write Samples

//                    int[] filledInOutletIndexes = GetFilledInChanelIndexes(_outlets);

//                    for (double t = 0; t <= endTime; t += dt)
//                    {
//                        for (int i = 0; i < filledInOutletIndexes.Length; i++)
//                        {
//                            int filledInOutletIndex = filledInOutletIndexes[i];
//                            Outlet outlet = _outlets[filledInOutletIndex];
//                            IPatchCalculator patchCalculator = _patchCalculators[filledInOutletIndex];

//                            double value = patchCalculator.Calculate(t, i);
//                            value *= _audioFileOutput.Amplifier;

//                            WriteValue(writer, value);
//                        }
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Build up an array of indexes of filled in channels,
//        /// so that a null check can be omitted from the big loop.
//        /// </summary>
//        private int[] GetFilledInChanelIndexes(Outlet[] outlets)
//        {
//            IList<int> channelIndexes = new List<int>(outlets.Length);

//            for (int i = 0; i < outlets.Length; i++)
//            {
//                Outlet outlet = outlets[i];
//                if (outlet != null)
//                {
//                    channelIndexes.Add(i);
//                }
//            }

//            return channelIndexes.ToArray();
//        }

//        protected abstract void WriteValue(BinaryWriter binaryWriter, double value);
//    }
//}
