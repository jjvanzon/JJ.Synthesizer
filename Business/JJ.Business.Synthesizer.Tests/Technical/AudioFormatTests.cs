using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
 using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.IO.Path;
using static System.Math;
using static System.MidpointRounding;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Tests.Accessors.JJFrameworkIOWishesAccessor;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Framework.Reflection.ExpressionHelper;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
// ReSharper disable ConditionIsAlwaysTrueOrFalse

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
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Float32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Float32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Float32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Float32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Float32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Float32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Block_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Float32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Block_NonAligned()
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Float32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Block_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Block_NonAligned()
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Blocky_Aligned()
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, Block, aligned: false);
        
        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Float32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Float32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Float32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Blocky_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Float32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Linear_Aligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Float32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Linear_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Float32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Float32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Float32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Linear_Aligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Linear_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Blocky_Aligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, Block, aligned: false);

        void GenericTest(
            AudioFileFormatEnum audioFileFormatEnum,
            SpeakerSetupEnum speakerSetupEnum,
            SampleDataTypeEnum sampleDataTypeEnum,
            InterpolationTypeEnum interpolationTypeEnum,
            bool aligned,
            [CallerMemberName] string callerMemberName = null)
        {
            // Arrange
            int    samplingRate = aligned ? ALIGNED_SAMPLING_RATE : NON_ALIGNED_SAMPLING_RATE;
            double frequency    = aligned ? ALIGNED_FREQUENCY : NON_ALIGNED_FREQUENCY;

            WithSpeakers(speakerSetupEnum);
            WithBits(sampleDataTypeEnum.GetBits());
            WithInterpolation(interpolationTypeEnum);
            WithAudioFormat(audioFileFormatEnum);
            WithSamplingRate(samplingRate);

            // Panned, amplified sine
            FlowNode getSignal()
            {
                var sine      = Sine(frequency);
                var amplified = Multiply(sine, VOLUME);
                var panned    = Panning(amplified, PANNING);
                return panned;
            }

            // Save to file
            Buff buff1 = WithAudioLength(DURATION).Cache(getSignal, callerMemberName);
            AudioFileOutput audioFileOutput1 = buff1.UnderlyingAudioFileOutput;
            byte[] bytes = buff1.Bytes;
            
            // Use sample operator
            FlowNode getSample()
            {
                FlowNode node   = Sample(bytes, audioFileOutput1.FilePath).SetName($"{callerMemberName}_Reloaded");
                Sample   sample = node.UnderlyingSample();

                if (audioFileFormatEnum == Raw)
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
            Buff buff2 = WithAudioLength(DURATION2).Cache(getSample);
            AudioFileOutput audioFileOutput2 = buff2.UnderlyingAudioFileOutput;
            
            // Assert AudioFileOutput Entities

            string expectedFilePath1 = 
                GetFullPath(PrettifyName(callerMemberName) + audioFileFormatEnum.GetFileExtension());
            
            AssertAudioFileOutputEntities(
                audioFileOutput1,
                audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, samplingRate,
                expectedFilePath1, DURATION);

            string expectedFilePath2 = 
                GetFullPath(PrettifyName($"{callerMemberName}_Reloaded") + audioFileFormatEnum.GetFileExtension());
            
            AssertAudioFileOutputEntities(
                audioFileOutput2,
                audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, samplingRate,
                expectedFilePath2, DURATION2);

            // Assert Samples Entities

            if (speakerSetupEnum == SpeakerSetupEnum.Mono)
            {
                WithCenter();
                
                var sampleMono = getSample();
                
                AssertSampleEntities(
                    sampleMono,
                    audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, interpolationTypeEnum, samplingRate,
                    expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
                
                Console.WriteLine();
            }

            if (speakerSetupEnum == SpeakerSetupEnum.Stereo)
            {
                WithLeft();
                
                var sampleLeft  = getSample();

                AssertSampleEntities(
                    sampleLeft,
                    audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, interpolationTypeEnum, samplingRate,
                    expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
                Console.WriteLine();

                WithRight();
                
                var sampleRight = getSample();

                AssertSampleEntities(
                    sampleRight,
                    audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, interpolationTypeEnum, samplingRate,
                    expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
                Console.WriteLine();
            }
            
            // Get Values

            // Mono

            if (speakerSetupEnum == SpeakerSetupEnum.Mono)
            {
                // Get Values

                WithCenter();

                var sampleWrapperMono  = getSample();

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
                    Calculate(sampleWrapperMono, time: 0.0 / 8.0 / frequency),
                    Calculate(sampleWrapperMono, time: 1.0 / 8.0 / frequency),
                    Calculate(sampleWrapperMono, time: 2.0 / 8.0 / frequency),
                    Calculate(sampleWrapperMono, time: 3.0 / 8.0 / frequency),
                    Calculate(sampleWrapperMono, time: 4.0 / 8.0 / frequency),
                    Calculate(sampleWrapperMono, time: 5.0 / 8.0 / frequency),
                    Calculate(sampleWrapperMono, time: 6.0 / 8.0 / frequency),
                    Calculate(sampleWrapperMono, time: 7.0 / 8.0 / frequency),
                    Calculate(sampleWrapperMono, time: 8.0 / 8.0 / frequency)
                };

                double valueTolerance = GetValueTolerance(aligned, interpolationTypeEnum, sampleDataTypeEnum);
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

            if (speakerSetupEnum == SpeakerSetupEnum.Stereo)
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

                WithCenter();

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

                var sampleWrapperRight = getSample();

                double[] actualR =
                {
                    sampleWrapperRight.Calculate(time: 0.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleWrapperRight.Calculate(time: 1.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleWrapperRight.Calculate(time: 2.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleWrapperRight.Calculate(time: 3.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleWrapperRight.Calculate(time: 4.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleWrapperRight.Calculate(time: 5.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleWrapperRight.Calculate(time: 6.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleWrapperRight.Calculate(time: 7.0 / 8.0 / frequency, ChannelEnum.Right),
                    sampleWrapperRight.Calculate(time: 8.0 / 8.0 / frequency, ChannelEnum.Right)
                };

                double valueTolerance = GetValueTolerance(aligned, interpolationTypeEnum, sampleDataTypeEnum);
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
            SpeakerSetupEnum speakerSetupEnum, 
            SampleDataTypeEnum sampleDataTypeEnum, 
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
            AreEqual(sampleDataTypeEnum,  () => audioFileOutput.GetSampleDataTypeEnum());
            AreEqual(speakerSetupEnum,    () => audioFileOutput.GetSpeakerSetupEnum());
            
            {
                IsNotNull(() => audioFileOutput.FilePath);
                
                (string expectedFilePathFirstPart, int number, string expectedFilePathLastPart) =
                    GetNumberedFilePathParts(expectedFilePath, "", "");
                
                Console.WriteLine(GetText(() => audioFileOutput.FilePath) + " = " + audioFileOutput.FilePath);
                Console.WriteLine(GetText(() => expectedFilePathFirstPart) + " = " + expectedFilePathFirstPart);
                Console.WriteLine(GetText(() => expectedFilePathLastPart) + " = " + expectedFilePathLastPart);
                Console.WriteLine("");
                
                IsTrue(() => audioFileOutput.FilePath.StartsWith(expectedFilePathFirstPart));
                IsTrue(() => audioFileOutput.FilePath.EndsWith(expectedFilePathLastPart));
            }
            
            IsTrue(audioFileOutput.ID > 0, "audioFileOutput.ID > 0");
            double expectedAmplifier = sampleDataTypeEnum.GetNominalMax();
            AreEqual(expectedAmplifier, () => audioFileOutput.Amplifier);
            
            // AudioFileOutputChannels
            IsNotNull(() => audioFileOutput.AudioFileOutputChannels);
            int channelCount = speakerSetupEnum.GetChannelCount();
            AreEqual(channelCount, () => audioFileOutput.AudioFileOutputChannels.Count);

            for (var i = 0; i < channelCount; i++)
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
            SpeakerSetupEnum speakerSetupEnum, 
            SampleDataTypeEnum sampleDataTypeEnum, 
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
            AreEqual(sampleDataTypeEnum,    () => sample.GetSampleDataTypeEnum());
            AreEqual(speakerSetupEnum,      () => sample.GetSpeakerSetupEnum());
            AreEqual(audioFileFormatEnum,   () => sample.GetAudioFileFormatEnum());
            AreEqual(interpolationTypeEnum, () => sample.GetInterpolationTypeEnum());

            int expectedChannelCount = speakerSetupEnum == SpeakerSetupEnum.Mono ? 1 : 2;
            AreEqual(expectedChannelCount, () => sample.GetChannelCount());

            IsNotNull(() => sample.Bytes);

            // ByteCount
            {
                NotEqual(0, () => sample.Bytes.Length);
                
                int byteCountExpected  = (int)(audioFileFormatEnum.GetHeaderLength() + samplingRate * sample.GetFrameSize() * DURATION);
                int byteCountTolerance = GetByteCountTolerance(sampleDataTypeEnum, expectedChannelCount);

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
            InterpolationTypeEnum interpolationTypeEnum,
            SampleDataTypeEnum sampleDataTypeEnum)
        {
            // Worst: Blocky, Not Aligned

            if (!aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == SampleDataTypeEnum.Byte)
            {
                return 0.0325;
            }
            
            if (!aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == SampleDataTypeEnum.Int16)
            {
                return 0.0325;
            }
            
            if (!aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == SampleDataTypeEnum.Float32)
            {
                return 0.0325;
            }

            // Byte
            
            if (aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == SampleDataTypeEnum.Byte)
            {
                return 0.008;
            }

            if (!aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == SampleDataTypeEnum.Byte)
            {
                return 0.008;
            }

            if (aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == SampleDataTypeEnum.Byte)
            {
                return 0.008;
            }

            // Int16 Not Aligned
            
            if (!aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == SampleDataTypeEnum.Int16)
            {
                return 0.0006;
            }
            
            // Float32 Non-Aligned
            
            if (!aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == SampleDataTypeEnum.Float32)
            {
                return 0.00015;
            }

            // Int16 Aligned

            if (aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == SampleDataTypeEnum.Int16)
            {
                return 0.00008;
            }
            
            if (aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == SampleDataTypeEnum.Int16)
            {
                return 0.00008;
            }

            // Float32 Aligned

            if (aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == SampleDataTypeEnum.Float32)
            {
                return 0.000047;
            }
            
            if (aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == SampleDataTypeEnum.Float32)
            {
                return 0.000047;
            }

            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { interpolationTypeEnum, sampleDataTypeEnum });
        }
        
        private int GetByteCountTolerance(SampleDataTypeEnum sampleDataTypeEnum, int channelCount) 
            => 4 * sampleDataTypeEnum.SizeOf() * channelCount; // A tolerance of 4 audio frames.

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