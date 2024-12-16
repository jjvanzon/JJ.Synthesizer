using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Environment;
using static System.IO.Path;
using static System.Math;
using static System.MidpointRounding;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Tests.Accessors.JJFrameworkIOWishesAccessor;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Wishes.Obsolete.RecordObsoleteExtensions;
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ConvertToConstant.Local

// ReSharper disable RedundantArgumentDefaultValue

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioFormatTests : SynthWishes
    {
        private const int    ALIGNED_SAMPLING_RATE     = 8192;
        private const double ALIGNED_FREQUENCY         = 64;
        private const int    NON_ALIGNED_SAMPLING_RATE = 8191;
        private const double NON_ALIGNED_FREQUENCY     = 61;

        private const double VOLUME    = 0.50;
        private const double PANNING   = 0.25;
        private const double DURATION  = 0.25;
        private const double DURATION2 = DURATION * 1.01;
        private const int    DECIMALS  = 4;

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Block_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Block_NonAligned()
            => new AudioFormatTests().GenericTest(Wav, 1, 32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Block_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Block_NonAligned()
            => new AudioFormatTests().GenericTest(Wav, 1, 16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 8, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 8, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Blocky_Aligned()
            => new AudioFormatTests().GenericTest(Wav, 2, 8, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 2, 8, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 8, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 8, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 8, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, 1, 8, Block, aligned: false);
        
        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, 2, 32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, 2, 32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, 2, 32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Blocky_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, 2, 32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Linear_Aligned()
            => new AudioFormatTests().GenericTest(Raw, 1, 32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Linear_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, 1, 32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, 1, 32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, 1, 32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, 2, 16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, 2, 16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, 2, 16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, 2, 16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Linear_Aligned()
            => new AudioFormatTests().GenericTest(Raw, 1, 16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Linear_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, 1, 16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, 1, 16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, 1, 16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, 2, 8, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, 2, 8, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Blocky_Aligned()
            => new AudioFormatTests().GenericTest(Raw, 2, 8, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, 2, 8, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, 1, 8, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, 1, 8, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, 1, 8, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, 1, 8, Block, aligned: false);

        void GenericTest(
            AudioFileFormatEnum audioFormat,
            int channels,
            int bits,
            InterpolationTypeEnum interpolation,
            bool aligned,
            [CallerMemberName] string callerMemberName = null)
        {
            // Arrange
            int    samplingRate = aligned ? ALIGNED_SAMPLING_RATE : NON_ALIGNED_SAMPLING_RATE;
            double frequency    = aligned ? ALIGNED_FREQUENCY : NON_ALIGNED_FREQUENCY;

            WithChannels(channels);
            WithBits(bits);
            WithInterpolation(interpolation);
            WithAudioFormat(audioFormat);
            WithSamplingRate(samplingRate);

            // Panned, amplified sine
            FlowNode getSignal()
            {
                var sound = Sine(frequency) * VOLUME;
                if (IsStereo)
                {
                    sound = sound.Panning(PANNING);
                }
                return sound;
            }

            // Save to file
            // TODO: Retry Run notation later after fixes:
            //Buff buff1 = null;
            //Run(() =>  WithAudioLength(DURATION).Intercept(getSignal(), x => buff1 = x, callerMemberName));
            Buff buff1 = WithAudioLength(DURATION).Record(getSignal, callerMemberName);
            IsNotNull(() => buff1);
 
            AudioFileOutput audioFileOutput1 = buff1.UnderlyingAudioFileOutput;
            byte[] bytes = buff1.Bytes;
            
            // Use sample operator
            FlowNode getSample()
            {
                FlowNode node   = Sample(bytes, audioFileOutput1.FilePath).SetName($"{callerMemberName}_Reloaded");
                Sample   sample = node.UnderlyingSample();

                if (audioFormat == Raw)
                {
                    // In case of RAW format, set some values explicitly.
                    sample.SamplingRate   = samplingRate;
                    sample.SpeakerSetup   = audioFileOutput1.SpeakerSetup;
                    sample.SampleDataType = audioFileOutput1.SampleDataType;
                    sample.Amplifier      = 1.0 / audioFileOutput1.GetNominalMax();
                }

                return node;
            }
            
            // Save to file again
            // TODO: Retry Run notation later after fixes:
            //Buff buff2 = null;
            //Run(() => WithAudioLength(DURATION2).Intercept(getSample(), x => buff2 = x));
            Buff buff2 = WithAudioLength(DURATION2).Record(getSample);
            IsNotNull(() => buff2);
            
            AudioFileOutput audioFileOutput2 = buff2.UnderlyingAudioFileOutput;
            
            // Assert AudioFileOutput Entities

            string expectedFilePath1 = 
                GetFullPath(PrettifyName(callerMemberName) + audioFormat.GetFileExtension());
            
            AssertAudioFileOutputEntities(
                audioFileOutput1,
                audioFormat, channels, bits, samplingRate,
                expectedFilePath1, DURATION);

            string expectedFilePath2 = 
                GetFullPath(PrettifyName($"{callerMemberName}_Reloaded") + audioFormat.GetFileExtension());
            
            AssertAudioFileOutputEntities(
                audioFileOutput2,
                audioFormat, channels, bits, samplingRate,
                expectedFilePath2, DURATION2);

            // Assert Samples Entities

            if (channels == 1)
            {
                WithCenter();
                
                var sampleMono = getSample();
                
                AssertSampleEntities(
                    sampleMono,
                    audioFormat, channels, bits, interpolation, samplingRate,
                    expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
                
                Console.WriteLine();
            }

            if (channels == 2)
            {
                WithLeft();
                
                var sampleLeft  = getSample();

                AssertSampleEntities(
                    sampleLeft,
                    audioFormat, channels, bits, interpolation, samplingRate,
                    expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
                Console.WriteLine();

                WithRight();
                
                var sampleRight = getSample();

                AssertSampleEntities(
                    sampleRight,
                    audioFormat, channels, bits, interpolation, samplingRate,
                    expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
                Console.WriteLine();
            }
            
            // Get Values

            // Mono

            if (channels == 1)
            {
                // Get Values

                WithCenter();

                var sampleMono  = getSample();

                double[] expectedValues =
                {
                    VOLUME *       0.0,
                    VOLUME *  Sqrt(.5),
                    VOLUME *       1.0,
                    VOLUME *  Sqrt(.5),
                    VOLUME *       0.0,
                    VOLUME * -Sqrt(.5),
                    VOLUME *      -1.0,
                    VOLUME * -Sqrt(.5),
                    VOLUME *       0.0
                };
                expectedValues = expectedValues.Select(RoundValue).ToArray();

                double[] actualValues =
                {
                    Calculate(sampleMono, time: 0.0 / 8.0 / frequency),
                    Calculate(sampleMono, time: 1.0 / 8.0 / frequency),
                    Calculate(sampleMono, time: 2.0 / 8.0 / frequency),
                    Calculate(sampleMono, time: 3.0 / 8.0 / frequency),
                    Calculate(sampleMono, time: 4.0 / 8.0 / frequency),
                    Calculate(sampleMono, time: 5.0 / 8.0 / frequency),
                    Calculate(sampleMono, time: 6.0 / 8.0 / frequency),
                    Calculate(sampleMono, time: 7.0 / 8.0 / frequency),
                    Calculate(sampleMono, time: 8.0 / 8.0 / frequency)
                };

                double valueTolerance = GetValueTolerance(aligned, interpolation, bits);
                double valueToleranceRequired = expectedValues.Zip(actualValues, (x,y) => Abs(x - y)).Max();
                Console.WriteLine($"{nameof(valueTolerance)}         = {valueTolerance}");
                Console.WriteLine($"{nameof(valueToleranceRequired)} = {valueToleranceRequired}");
                Console.WriteLine();
                Console.WriteLine($"{nameof(expectedValues)} = {FormatValues(expectedValues)}");
                Console.WriteLine($"{nameof(actualValues)}   = {FormatValues(actualValues  )}");
                Console.WriteLine();

                // Assert Values
                
                AreEqual(expectedValues[0], actualValues[0], valueTolerance);
                AreEqual(expectedValues[1], actualValues[1], valueTolerance);
                AreEqual(expectedValues[2], actualValues[2], valueTolerance);
                AreEqual(expectedValues[3], actualValues[3], valueTolerance);
                AreEqual(expectedValues[4], actualValues[4], valueTolerance);
                AreEqual(expectedValues[5], actualValues[5], valueTolerance);
                AreEqual(expectedValues[6], actualValues[6], valueTolerance);
                AreEqual(expectedValues[7], actualValues[7], valueTolerance);
                AreEqual(expectedValues[8], actualValues[8], valueTolerance);
            }

            // Stereo

            if (channels == 2)
            {
                // GetValues

                // Left
                
                double[] expectedL =
                {
                    VOLUME * 0.75 *       0.0,
                    VOLUME * 0.75 *  Sqrt(.5),
                    VOLUME * 0.75 *       1.0,
                    VOLUME * 0.75 *  Sqrt(.5),
                    VOLUME * 0.75 *       0.0,
                    VOLUME * 0.75 * -Sqrt(.5),
                    VOLUME * 0.75 *      -1.0,
                    VOLUME * 0.75 * -Sqrt(.5),
                    VOLUME * 0.75 *       0.0
                };
                expectedL = expectedL.Select(RoundValue).ToArray();

                WithLeft();

                var sampleLeft  = getSample();

                double[] actualL =
                {
                    sampleLeft.Calculate(time: 0.0 / 8.0 / frequency, ChannelEnum.Left),
                    sampleLeft.Calculate(time: 1.0 / 8.0 / frequency, ChannelEnum.Left),
                    sampleLeft.Calculate(time: 2.0 / 8.0 / frequency, ChannelEnum.Left),
                    sampleLeft.Calculate(time: 3.0 / 8.0 / frequency, ChannelEnum.Left),
                    sampleLeft.Calculate(time: 4.0 / 8.0 / frequency, ChannelEnum.Left),
                    sampleLeft.Calculate(time: 5.0 / 8.0 / frequency, ChannelEnum.Left),
                    sampleLeft.Calculate(time: 6.0 / 8.0 / frequency, ChannelEnum.Left),
                    sampleLeft.Calculate(time: 7.0 / 8.0 / frequency, ChannelEnum.Left),
                    sampleLeft.Calculate(time: 8.0 / 8.0 / frequency, ChannelEnum.Left)
                };

                // Right
                
                double[] expectedR =
                {
                    VOLUME * 0.25 *       0.0,
                    VOLUME * 0.25 *  Sqrt(.5),
                    VOLUME * 0.25 *       1.0,
                    VOLUME * 0.25 *  Sqrt(.5),
                    VOLUME * 0.25 *       0.0,
                    VOLUME * 0.25 * -Sqrt(.5),
                    VOLUME * 0.25 *      -1.0,
                    VOLUME * 0.25 * -Sqrt(.5),
                    VOLUME * 0.25 *       0.0
                };
                expectedR = expectedR.Select(RoundValue).ToArray();

                WithRight();

                var sampleRight = getSample();

                double[] actualR =
                {
                    sampleRight.Calculate(time: 0.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleRight.Calculate(time: 1.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleRight.Calculate(time: 2.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleRight.Calculate(time: 3.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleRight.Calculate(time: 4.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleRight.Calculate(time: 5.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleRight.Calculate(time: 6.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleRight.Calculate(time: 7.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleRight.Calculate(time: 8.0 / 8.0 / frequency, ChannelEnum.Right)
                };

                double valueTolerance = GetValueTolerance(aligned, interpolation, bits);
                double valueToleranceRequired = expectedL.Concat(expectedR).Zip(actualL.Concat(actualR), (x,y) => Abs(x - y)).Max();
                Console.WriteLine($"{nameof(valueTolerance)}         = {valueTolerance}");
                Console.WriteLine($"{nameof(valueToleranceRequired)} = {valueToleranceRequired}");
                Console.WriteLine();
                Console.WriteLine($"{nameof(expectedL)} = {FormatValues(expectedL)}");
                Console.WriteLine($"  {nameof(actualL)} = {FormatValues(actualL  )}");
                Console.WriteLine();
                Console.WriteLine($"{nameof(expectedR)} = {FormatValues(expectedR)}");
                Console.WriteLine($"  {nameof(actualR)} = {FormatValues(actualR  )}");
                Console.WriteLine();

                // Assert Values
                
                // Left

                AreEqual(expectedL[0], actualL[0], valueTolerance);
                AreEqual(expectedL[1], actualL[1], valueTolerance);
                AreEqual(expectedL[2], actualL[2], valueTolerance);
                AreEqual(expectedL[3], actualL[3], valueTolerance);
                AreEqual(expectedL[4], actualL[4], valueTolerance);
                AreEqual(expectedL[5], actualL[5], valueTolerance);
                AreEqual(expectedL[6], actualL[6], valueTolerance);
                AreEqual(expectedL[7], actualL[7], valueTolerance);
                AreEqual(expectedL[8], actualL[8], valueTolerance);

                // Right
                
                AreEqual(expectedR[0], actualR[0], valueTolerance);
                AreEqual(expectedR[1], actualR[1], valueTolerance);
                AreEqual(expectedR[2], actualR[2], valueTolerance);
                AreEqual(expectedR[3], actualR[3], valueTolerance);
                AreEqual(expectedR[4], actualR[4], valueTolerance);
                AreEqual(expectedR[5], actualR[5], valueTolerance);
                AreEqual(expectedR[6], actualR[6], valueTolerance);
                AreEqual(expectedR[7], actualR[7], valueTolerance);
                AreEqual(expectedR[8], actualR[8], valueTolerance);
            }
        }

        static void AssertAudioFileOutputEntities(
            AudioFileOutput audioFileOutput, 
            AudioFileFormatEnum audioFileFormatEnum,
            int channels, 
            int bits, 
            int samplingRate,
            string expectedFilePath, 
            double expectedDuration)
        {
            // AudioFileOutput
            IsNotNull(() => audioFileOutput.AudioFileFormat);
            IsNotNull(() => audioFileOutput.SampleDataType);
            IsNotNull(() => audioFileOutput.SpeakerSetup);
            
            AreEqual(samplingRate,        () => audioFileOutput.SamplingRate);
            AreEqual(0,                   () => audioFileOutput.StartTime);
            AreEqual(expectedDuration,    () => audioFileOutput.GetEndTime());
            AreEqual(expectedDuration,    () => audioFileOutput.Duration);
            AreEqual(audioFileFormatEnum, () => audioFileOutput.GetAudioFileFormatEnum());
            AreEqual(bits,                () => audioFileOutput.GetBits());
            AreEqual(channels,            () => audioFileOutput.GetChannelCount());
            
            {
                IsNotNull(() => audioFileOutput.FilePath);
                
                (string expectedFilePathFirstPart, int number, string expectedFilePathLastPart) =
                    GetNumberedFilePathParts(expectedFilePath, "", "");
                
                var values = new 
                { 
                    actualValue = audioFileOutput.FilePath,
                    expectedFirstPart = expectedFilePathFirstPart, 
                    expectedLastPart = expectedFilePathLastPart
                };
                
                IsTrue(audioFileOutput.FilePath.StartsWith(expectedFilePathFirstPart),
                    $"Tested Expression: audioFileOutput.FilePath.StartsWith(expectedFilePathFirstPart).{NewLine}{values}");
                IsTrue(audioFileOutput.FilePath.EndsWith(expectedFilePathLastPart),
                    $"Tested Expression: audioFileOutput.FilePath.EndsWith(expectedFilePathLastPart).{NewLine}{values}");
            }
            
            IsTrue(audioFileOutput.ID > 0, "audioFileOutput.ID > 0");
            double expectedAmplifier = bits.GetNominalMax();
            AreEqual(expectedAmplifier, () => audioFileOutput.Amplifier);
            
            // AudioFileOutputChannels
            IsNotNull(() => audioFileOutput.AudioFileOutputChannels);
            AreEqual(channels, () => audioFileOutput.AudioFileOutputChannels.Count);

            for (var i = 0; i < channels; i++)
            {
                AudioFileOutputChannel channel = audioFileOutput.AudioFileOutputChannels[i];
                IsNotNull(() => channel.Outlet);
                IsNotNull(() => channel.AudioFileOutput);
                AreEqual(i, () => channel.Index);
                IsNotNull(() => channel.AudioFileOutput);
                AreEqual(audioFileOutput, () => channel.AudioFileOutput);
            }
        }

        private void AssertSampleEntities(
            FlowNode sampleFlowNode, 
            AudioFileFormatEnum audioFileFormatEnum, 
            int channels, 
            int bits, 
            InterpolationTypeEnum interpolationTypeEnum,
            int samplingRate,
            double expectedDuration,
            string filePath,
            string callerMemberName)
        {
            // Sample Wrapper
            IsNotNull(() => sampleFlowNode);
            IsNotNull(() => sampleFlowNode.UnderlyingSample());

            // Sample Operator
            Operator sampleOperator = sampleFlowNode.UnderlyingOperator;
            IsNotNull(() => sampleOperator);
            AreEqual("SampleOperator", () => sampleOperator.OperatorTypeName);
            IsNull(() => sampleOperator.AsCurveIn);
            IsNull(() => sampleOperator.AsValueOperator);
            IsNotNull(() => sampleOperator.AsSampleOperator);
            {
                string expectedName = PrettifyName(callerMemberName);
                NotNullOrEmpty(() => sampleOperator.Name);
                IsTrue(() => sampleOperator.Name.StartsWith(expectedName)); 
            }

            // Sample Inlets
            IsNotNull(() => sampleOperator.Inlets);
            AreEqual(0, () => sampleOperator.Inlets.Count);

            // Sample Outlets
            IsNotNull(() => sampleOperator.Outlets);
            AreEqual(1, () => sampleOperator.Outlets.Count);
            IsNotNull(() => sampleOperator.Outlets[0]);

            // Sample Outlet
            Outlet sampleOutlet = sampleFlowNode;
            IsNotNull(() => sampleOutlet);
            IsNotNull(() => sampleOutlet.Operator);
            AreEqual(sampleOperator, () => sampleOutlet.Operator);
            AreEqual("Result",       () => sampleOutlet.Name);
            IsNotNull(() => sampleOutlet.ConnectedInlets);
            AreEqual(0, () => sampleOutlet.ConnectedInlets.Count);
            IsNotNull(() => sampleOutlet.AsAudioFileOutputChannels);
            AreEqual(0, () => sampleOutlet.AsAudioFileOutputChannels.Count);

            // Sample
            Sample sample = sampleOperator.AsSampleOperator.Sample;
            AreEqual(1,                     () => sample.TimeMultiplier);
            AreEqual(true,                  () => sample.IsActive);
            AreEqual(0,                     () => sample.BytesToSkip);
            AreEqual(samplingRate,          () => sample.SamplingRate);
            AreEqual(bits,                  () => sample.GetBits());
            AreEqual(channels,              () => sample.GetChannelCount());
            AreEqual(audioFileFormatEnum,   () => sample.GetAudioFileFormatEnum());
            AreEqual(interpolationTypeEnum, () => sample.GetInterpolationTypeEnum());

            AreEqual(channels, () => sample.GetChannelCount());

            IsNotNull(() => sample.Bytes);

            // ByteCount
            {
                NotEqual(0, () => sample.Bytes.Length);
                
                int byteCountExpected  = (int)(audioFileFormatEnum.GetHeaderLength() + samplingRate * sample.GetFrameSize() * DURATION);
                int byteCountTolerance = GetByteCountTolerance(bits, channels);

                Console.WriteLine($"Byte count tolerance = {byteCountTolerance}");
                Console.WriteLine($"Byte count expected  = {byteCountExpected}");
                Console.WriteLine($"Byte count actual    = {sample.Bytes.Length}");
                
                AreEqual(byteCountExpected, sample.Bytes.Length, byteCountTolerance);
            }
            
            // Paths
            {
                string expectedLocation = GetFullPath(filePath);
                NotNullOrEmpty(() => sample.Location);
                AreEqual(expectedLocation, () => sample.Location);

                string expectedName = PrettifyName(callerMemberName);
                NotNullOrEmpty(() => sample.Name);
                IsTrue(() => sampleOperator.Name.StartsWith(expectedName)); 
            }

            // Sample Duration
            double durationTolerance_RatherHigh = 0.02;
            AreEqual(expectedDuration, sample.GetDuration(), durationTolerance_RatherHigh);
            
            // Sample Outlet From Different Sources
            //Outlet sampleOutlet_ImplicitConversionFromWrapper = sampleWrapper;
            //Outlet sampleOutlet_FromWrapperResult             = sampleWrapper.Result;
            Outlet sampleOutlet_FromOperatorOutlets           = sampleOperator.Outlets[0];
            //IsNotNull(() => sampleOutlet_ImplicitConversionFromWrapper);
            //IsNotNull(() => sampleOutlet_FromWrapperResult);
            IsNotNull(() => sampleOutlet_FromOperatorOutlets);
            //AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromWrapperResult);
            //AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromOperatorOutlets);
        }

        // Helpers

        internal double GetValueTolerance(
            bool aligned,
            InterpolationTypeEnum interpolation,
            int bits)
        {
            // Worst: Blocky, Not Aligned

            if (!aligned && interpolation == Block && bits == 8)
            {
                return 0.0325;
            }
            
            if (!aligned && interpolation == Block && bits == 16)
            {
                return 0.0325;
            }
            
            if (!aligned && interpolation == Block && bits == 32)
            {
                return 0.0325;
            }

            // Byte
            
            if (aligned && interpolation == Block && bits == 8)
            {
                return 0.008;
            }

            if (!aligned && interpolation == Line && bits == 8)
            {
                return 0.008;
            }

            if (aligned && interpolation == Line && bits == 8)
            {
                return 0.008;
            }

            // Int16 Not Aligned
            
            if (!aligned && interpolation == Line && bits == 16)
            {
                return 0.0006;
            }
            
            // Float32 Non-Aligned
            
            if (!aligned && interpolation == Line && bits == 32)
            {
                return 0.00015;
            }

            // Int16 Aligned

            if (aligned && interpolation == Block && bits == 16)
            {
                return 0.00008;
            }
            
            if (aligned && interpolation == Line && bits == 16)
            {
                return 0.00008;
            }

            // Float32 Aligned

            if (aligned && interpolation == Block && bits == 32)
            {
                return 0.000047;
            }
            
            if (aligned && interpolation == Line && bits == 32)
            {
                return 0.000047;
            }

            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { interpolationTypeEnum = interpolation, bits });
        }
        
        private int GetByteCountTolerance(int bits, int channels) 
            => 4 * bits.SizeOfBits() * channels; // A tolerance of 4 audio frames.

        private double RoundValue(double x) => Round(x, DECIMALS, AwayFromZero);

        private string FormatValues(double[] values)
        {
            string result = string.Join("|", values.Select(FormatValue));
            return result;
        }

        private string FormatValue(double value)
        {
            // Length "-0.1234" = sign + 0 + . + decimals 
            int    length    = 1 + 1 + 1 + DECIMALS; 
            double rounded   = Round(value, DECIMALS, AwayFromZero);
            string formatted = rounded.ToString("F" + DECIMALS);
            string padded    = formatted.PadLeft(length) + " "; 
            return padded;
        }
    }
}