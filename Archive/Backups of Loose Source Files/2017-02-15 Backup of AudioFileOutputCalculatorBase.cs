//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Exceptions;
//using JJ.Framework.IO;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
//{
//    internal abstract class AudioFileOutputCalculatorBase : IAudioFileOutputCalculator
//    {
//        private const int VALUE_COUNT_PER_CHUNK = 32768;

//        private readonly IPatchCalculator[] _patchCalculators;

//        public AudioFileOutputCalculatorBase(IList<IPatchCalculator> patchCalculators)
//        {
//            if (patchCalculators == null) throw new NullException(() => patchCalculators);

//            _patchCalculators = patchCalculators.ToArray();
//        }

//        public void WriteFile(AudioFileOutput audioFileOutput)
//        {
//            // Assert
//            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
//            if (string.IsNullOrEmpty(audioFileOutput.FilePath)) throw new NullOrEmptyException(() => audioFileOutput.FilePath);
//            int channelCount = audioFileOutput.GetChannelCount();
//            if (_patchCalculators.Length != channelCount) throw new NotEqualException(() => _patchCalculators.Length, audioFileOutput.GetChannelCount());

//            IValidator validator = new AudioFileOutputValidator(audioFileOutput);
//            validator.Assert();

//            // Calculate output and write file

//            double dt = 1.0 / audioFileOutput.SamplingRate / audioFileOutput.TimeMultiplier;
//            double endTime = audioFileOutput.GetEndTime();
//            double duration = audioFileOutput.Duration;
//            int frameCount = (int)(duration / dt);

//            using (Stream stream = new FileStream(audioFileOutput.FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
//            {
//                using (var writer = new BinaryWriter(stream))
//                {
//                    // Write header
//                    AudioFileFormatEnum audioFileFormatEnum = audioFileOutput.GetAudioFileFormatEnum();
//                    switch (audioFileFormatEnum)
//                    {
//                        case AudioFileFormatEnum.Wav:
//                            var audioFileInfo = new AudioFileInfo
//                            {
//                                SamplingRate = audioFileOutput.SamplingRate,
//                                BytesPerValue = SampleDataTypeHelper.SizeOf(audioFileOutput.SampleDataType),
//                                ChannelCount = channelCount,
//                                FrameCount = frameCount
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

//                    double adjustedAmplifier = GetAmplifierAdjustedToSampleDataType(audioFileOutput);

//                    // Write Samples

//                    int valueCount = frameCount * channelCount;
//                    //int valuesLeftToWrite = totalValueCount;

//                    var buffer = new float[VALUE_COUNT_PER_CHUNK];

//                    int wholeChunkCount = frameCount / VALUE_COUNT_PER_CHUNK;
//                    int lastIncompleteChunkSize = valueCount % VALUE_COUNT_PER_CHUNK;
//                    int numberOfChunks = wholeChunkCount;
//                    if (lastIncompleteChunkSize > 0)
//                    {
//                        numberOfChunks++;
//                    }

//                    //double chunkCount = (double)frameCount / VALUE_COUNT_PER_CHUNK;

//                    double chunkDuration = dt * VALUE_COUNT_PER_CHUNK;

//                    int writtenValueCount = 0;

//                    double startTime = 0.0;
//                    for (int chunkIndex = 0; chunkIndex < numberOfChunks; chunkIndex++)
//                    {
//                        for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
//                        {
//                            _patchCalculators[channelIndex].Calculate(buffer, frameCount, startTime);
//                        }

//                        // Post-Process

//                        // TODO: If you just do a little if to see if you need MAX_BUFFER_LENGTH or a smaller length
//                        // for the last portion of the file, you do not have to repeat any code.

//                        int valueCountToWrite = VALUE_COUNT_PER_CHUNK;
//                        bool isLastChunk = chunkIndex == numberOfChunks -1;
//                        if (isLastChunk)
//                        {
//                            valueCountToWrite = lastIncompleteChunkSize;
//                        }

//                        for (int i = 0; i < valueCountToWrite; i++)
//                        {
//                            float value = buffer[i];
//                            value = value * (float)adjustedAmplifier; // TODO: Unsafe double to float conversion.

//                            // Write value
//                            WriteValue(writer, value);

//                            //valuesLeftToWrite -= 1;
//                        }

//                        startTime += chunkDuration;

//                        writtenValueCount += VALUE_COUNT_PER_CHUNK;
//                    }
//                }
//            }
//        }

//        protected abstract double GetAmplifierAdjustedToSampleDataType(AudioFileOutput audioFileOutput);
//        protected abstract void WriteValue(BinaryWriter binaryWriter, double value);
//    }
//}
