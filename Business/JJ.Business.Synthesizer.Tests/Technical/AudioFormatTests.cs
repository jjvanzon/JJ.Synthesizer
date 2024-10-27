using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.IO.Path;
using static System.Math;
using static System.MidpointRounding;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// ReSharper disable RedundantArgumentDefaultValue

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioFormatTests : SynthWishes
    {
        private const int    ALIGNED_SAMPLING_RATE     = 4096;
        private const double ALIGNED_FREQUENCY         = 64;
        private const int    NON_ALIGNED_SAMPLING_RATE = 4093;
        private const double NON_ALIGNED_FREQUENCY = 61;

        private const double VOLUME    = 0.50;
        private const double PANNING   = 0.25;
        private const double DURATION  = 0.25;
        private const double DURATION2 = DURATION * 1.01;
        private const int    DECIMALS  = 4;

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, Int16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, Int16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, Int16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, Int16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, Int16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, Int16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Block_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, Int16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Block_NonAligned()
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, Int16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, Byte, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, Byte, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Blocky_Aligned()
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, Byte, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Stereo, Byte, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, Byte, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, Byte, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, Byte, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Wav, SpeakerSetupEnum.Mono, Byte, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, Int16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, Int16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, Int16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, Int16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Linear_Aligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, Int16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Linear_NonAligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, Int16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, Int16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, Int16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, Byte, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, Byte, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Blocky_Aligned()
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, Byte, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Stereo, Byte, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Linear_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, Byte, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Linear_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, Byte, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Blocky_Aligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, Byte, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().GenericTest(Raw, SpeakerSetupEnum.Mono, Byte, Block, aligned: false);

        void GenericTest(
            AudioFileFormatEnum audioFileFormatEnum,
            SpeakerSetupEnum speakerSetupEnum,
            SampleDataTypeEnum sampleDataTypeEnum,
            InterpolationTypeEnum interpolationTypeEnum,
            bool aligned,
            [CallerMemberName] string callerMemberName = null)
        {
            WithSpeakerSetup(speakerSetupEnum);
            
            // Arrange

            int samplingRate = aligned ? ALIGNED_SAMPLING_RATE : NON_ALIGNED_SAMPLING_RATE;
            double frequency = aligned ? ALIGNED_FREQUENCY : NON_ALIGNED_FREQUENCY;

            // Panned, amplified sine
            Outlet getSignal()
            {
                var sine      = Sine(frequency);
                var amplified = Multiply(sine, VOLUME);
                var panned    = Panning(amplified, PANNING);
                return panned;
            }

            // Save to file
            AudioFileOutput audioFileOutput1 =
                SaveAudio(getSignal,         DURATION,           volume: null,
                          sampleDataTypeEnum, audioFileFormatEnum, samplingRate,
                          fileName: default, callerMemberName).Data;

            // Use sample operator
            Outlet getSample()
            {
                var outlet = Sample(audioFileOutput1.FilePath, interpolationTypeEnum);
                Sample sample = outlet.GetSample();

                if (audioFileFormatEnum == Raw)
                {
                    // In case of RAW format, set some values explicitly.
                    sample.SamplingRate   = samplingRate;
                    sample.SpeakerSetup   = audioFileOutput1.SpeakerSetup;
                    sample.SampleDataType = audioFileOutput1.SampleDataType;
                    sample.Amplifier      = 1.0 / audioFileOutput1.SampleDataType.GetMaxAmplitude();
                }

                return outlet;
            }
            
            // Save to file again
            AudioFileOutput audioFileOutput2 =
                SaveAudio(getSample,        DURATION2,          volume: null,
                          sampleDataTypeEnum, audioFileFormatEnum, samplingRate,
                          fileName: $"{callerMemberName}_Reloaded").Data;

            // Assert AudioFileOutput Entities

            string expectedFilePath1 = GetPrettyName(callerMemberName) + audioFileFormatEnum.GetFileExtension();
            
            AssertAudioFileOutputEntities(
                audioFileOutput1,
                audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, samplingRate,
                expectedFilePath1, DURATION);

            string expectedFilePath2 = GetPrettyName($"{callerMemberName}_Reloaded") + audioFileFormatEnum.GetFileExtension();
            
            AssertAudioFileOutputEntities(
                audioFileOutput2,
                audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, samplingRate,
                expectedFilePath2, DURATION2);

            // Assert Samples Entities

            if (speakerSetupEnum == SpeakerSetupEnum.Mono)
            {
                Channel = Single;
                
                var sampleMono = getSample();
                
                AssertSampleEntities(
                    sampleMono.GetSampleWrapper(),
                    audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, interpolationTypeEnum, samplingRate,
                    expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
                
                Console.WriteLine();
            }

            if (speakerSetupEnum == SpeakerSetupEnum.Stereo)
            {
                Channel = Left;
                
                var sampleLeft  = getSample();

                AssertSampleEntities(
                    sampleLeft.GetSampleWrapper(),
                    audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, interpolationTypeEnum, samplingRate,
                    expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
                Console.WriteLine();

                Channel = Right;
                
                var sampleRight = getSample();

                AssertSampleEntities(
                    sampleRight.GetSampleWrapper(),
                    audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, interpolationTypeEnum, samplingRate,
                    expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
                Console.WriteLine();
            }
            
            // Get Values

            // Mono

            if (speakerSetupEnum == SpeakerSetupEnum.Mono)
            {
                // Get Values

                Channel = Single;

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

                Channel = Left;

                var sampleLeft  = getSample();

                double[] actualL =
                {
                    sampleLeft.Calculate(time: 0.0 / 8.0 / frequency, Left),
                    sampleLeft.Calculate(time: 1.0 / 8.0 / frequency, Left),
                    sampleLeft.Calculate(time: 2.0 / 8.0 / frequency, Left),
                    sampleLeft.Calculate(time: 3.0 / 8.0 / frequency, Left),
                    sampleLeft.Calculate(time: 4.0 / 8.0 / frequency, Left),
                    sampleLeft.Calculate(time: 5.0 / 8.0 / frequency, Left),
                    sampleLeft.Calculate(time: 6.0 / 8.0 / frequency, Left),
                    sampleLeft.Calculate(time: 7.0 / 8.0 / frequency, Left),
                    sampleLeft.Calculate(time: 8.0 / 8.0 / frequency, Left)
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

                Channel = Right;

                var sampleWrapperRight = getSample();

                double[] actualR =
                {
                    sampleWrapperRight.Calculate(time: 0.0 / 8.0 / frequency, Right),
                    sampleWrapperRight.Calculate(time: 1.0 / 8.0 / frequency, Right),
                    sampleWrapperRight.Calculate(time: 2.0 / 8.0 / frequency, Right),
                    sampleWrapperRight.Calculate(time: 3.0 / 8.0 / frequency, Right),
                    sampleWrapperRight.Calculate(time: 4.0 / 8.0 / frequency, Right),
                    sampleWrapperRight.Calculate(time: 5.0 / 8.0 / frequency, Right),
                    sampleWrapperRight.Calculate(time: 6.0 / 8.0 / frequency, Right),
                    sampleWrapperRight.Calculate(time: 7.0 / 8.0 / frequency, Right),
                    sampleWrapperRight.Calculate(time: 8.0 / 8.0 / frequency, Right)
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
            
            AreEqual(samplingRate,       () => audioFileOutput.SamplingRate);
            AreEqual(expectedFilePath,    () => audioFileOutput.FilePath);
            AreEqual(0,                   () => audioFileOutput.StartTime);
            AreEqual(expectedDuration,    () => audioFileOutput.GetEndTime());
            AreEqual(expectedDuration,    () => audioFileOutput.Duration);
            AreEqual(audioFileFormatEnum, () => audioFileOutput.GetAudioFileFormatEnum());
            AreEqual(sampleDataTypeEnum,  () => audioFileOutput.GetSampleDataTypeEnum());
            AreEqual(speakerSetupEnum,    () => audioFileOutput.GetSpeakerSetupEnum());

            IsTrue(audioFileOutput.ID > 0, "audioFileOutput.ID > 0");
            double expectedAmplifier = sampleDataTypeEnum.GetMaxAmplitude();
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
            SampleOperatorWrapper sampleWrapper, 
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
            IsNotNull(() => sampleWrapper);
            IsNotNull(() => sampleWrapper.Sample);
            IsNotNull(() => sampleWrapper.Result);

            // Sample Operator
            Operator sampleOperator = sampleWrapper.Result.Operator;
            IsNotNull(() => sampleOperator);
            AreEqual("SampleOperator", () => sampleOperator.OperatorTypeName);
            IsNull(() => sampleOperator.AsCurveIn);
            IsNull(() => sampleOperator.AsValueOperator);
            IsNotNull(() => sampleOperator.AsSampleOperator);
            {
                string expectedName = GetPrettyName(callerMemberName);
                NotNullOrEmpty(() => sampleOperator.Name);
                AreEqual(expectedName, () => sampleOperator.Name);
            }

            // Sample Inlets
            IsNotNull(() => sampleOperator.Inlets);
            AreEqual(0, () => sampleOperator.Inlets.Count);

            // Sample Outlets
            IsNotNull(() => sampleOperator.Outlets);
            AreEqual(1, () => sampleOperator.Outlets.Count);
            IsNotNull(() => sampleOperator.Outlets[0]);

            // Sample Outlet
            Outlet sampleOutlet = sampleWrapper.Result;
            IsNotNull(() => sampleOutlet);
            IsNotNull(() => sampleOutlet.Operator);
            AreEqual(sampleOperator, () => sampleOutlet.Operator);
            AreEqual("Result",       () => sampleOutlet.Name);
            IsNotNull(() => sampleOutlet.ConnectedInlets);
            AreEqual(0, () => sampleOutlet.ConnectedInlets.Count);
            IsNotNull(() => sampleOutlet.AsAudioFileOutputChannels);
            AreEqual(0, () => sampleOutlet.AsAudioFileOutputChannels.Count);

            // AsSampleOperator
            SampleOperator asSampleOperator = sampleOperator.AsSampleOperator;
            IsNotNull(() => asSampleOperator);
            IsNotNull(() => asSampleOperator.Operator);
            IsNotNull(() => asSampleOperator.Sample);
            AreEqual(sampleOperator, () => asSampleOperator.Operator);

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

            int expectedChannelCount = speakerSetupEnum == Mono ? 1 : 2;
            AreEqual(expectedChannelCount, () => sample.GetChannelCount());

            IsNotNull(() => sample.SampleOperators);
            AreEqual(1, () => sample.SampleOperators.Count);
            IsNotNull(() => sample.SampleOperators[0]);
            AreEqual(asSampleOperator, () => sample.SampleOperators[0]);
            IsNotNull(() => sample.Bytes);

            // ByteCount
            {
                NotEqual(0, () => sample.Bytes.Length);
                
                int byteCountExpected  = (int)(audioFileFormatEnum.GetHeaderLength() + samplingRate * sample.GetFrameSize() * DURATION);
                int byteCountTolerance = GetByteCountTolerance(sampleDataTypeEnum);

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

                string expectedName = GetPrettyName(callerMemberName);
                NotNullOrEmpty(() => sample.Name);
                AreEqual(expectedName, () => sample.Name);
            }

            // Sample Duration
            double durationTolerance_RatherHigh = 0.02;
            AreEqual(expectedDuration, sample.GetDuration(), durationTolerance_RatherHigh);
            
            // Sample Outlet From Different Sources
            Outlet sampleOutlet_ImplicitConversionFromWrapper = sampleWrapper;
            Outlet sampleOutlet_FromWrapperResult             = sampleWrapper.Result;
            Outlet sampleOutlet_FromOperatorOutlets           = sampleOperator.Outlets[0];
            IsNotNull(() => sampleOutlet_ImplicitConversionFromWrapper);
            IsNotNull(() => sampleOutlet_FromWrapperResult);
            IsNotNull(() => sampleOutlet_FromOperatorOutlets);
            AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromWrapperResult);
            AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromOperatorOutlets);
        }

        // Helpers

        internal double GetValueTolerance(
            bool aligned,
            InterpolationTypeEnum interpolationTypeEnum,
            SampleDataTypeEnum sampleDataTypeEnum)
        {
            // Worst: Not Aligned, Block
            
            if (!aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == Int16)
            {
                return 0.0325;
            }

            if (!aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == Byte)
            {
                return 0.0325;
            }
            
            // Byte
            
            if (aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == Byte)
            {
                return 0.008;
            }

            if (!aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == Byte)
            {
                return 0.008;
            }

            if (aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == Byte)
            {
                return 0.008;
            }

            // Int16 Not Aligned
            
            if (!aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == Int16)
            {
                return 0.0006;
            }
                        
            // Int16 Aligned
            
            if (aligned && interpolationTypeEnum == Line && sampleDataTypeEnum == Int16)
            {
                return 0.00008;
            }

            if (aligned && interpolationTypeEnum == Block && sampleDataTypeEnum == Int16)
            {
                return 0.00008;
            }

            throw new NotSupportedException(
                "Unsupported combination of values: " + new { interpolationTypeEnum, sampleDataTypeEnum });
        }

        private int GetByteCountTolerance(SampleDataTypeEnum sampleDataTypeEnum) 
            => 2 * sampleDataTypeEnum.SizeOf(); // A tolerance of 2 audio frames.

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