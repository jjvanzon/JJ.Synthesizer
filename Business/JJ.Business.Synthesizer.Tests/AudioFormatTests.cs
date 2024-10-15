using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.IO.Path;
using static System.Math;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Framework.Testing.AssertHelper;

// ReSharper disable RedundantArgumentDefaultValue

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class AudioFormatTests : SynthesizerSugar
    {
        private const int    SAMPLING_RATE   = 4000;
        private const double FREQUENCY       = 40;
        private const double VOLUME          = 0.50;
        private const double DURATION        = 0.25;
        private const double DURATION_LONGER = DURATION;
        //private const double DURATION_LONGER = DURATION * 1.1; // For testing array bounds checks.

        // Want my static usings, but clashes with System type names.
        private readonly SampleDataTypeEnum Int16  = SampleDataTypeEnum.Int16;
        private readonly SampleDataTypeEnum Byte   = SampleDataTypeEnum.Byte;
        private readonly ChannelEnum        Single = ChannelEnum.Single;

        [UsedImplicitly]
        public AudioFormatTests()
        { }

        AudioFormatTests(IContext context)
            : base(context)
        { }

        [TestCategory("Wip")]
        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Stereo, Int16);
        }

        [TestCategory("Wip")]
        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Mono, Int16);
        }

        //[TestCategory("Wip")]
        //[TestMethod]
        //public void Test_AudioFormat_Wav_Stereo_8Bit()
        //    => Test_AudioFormat(Wav, Stereo, Byte);

        //[TestCategory("Wip")]
        //[TestMethod]
        //public void Test_AudioFormat_Wav_Mono_8Bit()
        //    => Test_AudioFormat(Wav, Mono, Byte);

        //[TestCategory("Wip")]
        //[TestMethod]
        //public void Test_AudioFormat_Raw_Stereo_16Bit()
        //    => Test_AudioFormat(Raw, Stereo, Int16);

        //[TestCategory("Wip")]
        //[TestMethod]
        //public void Test_AudioFormat_Raw_Mono_16Bit()
        //    => Test_AudioFormat(Raw, Mono, Int16);

        //[TestCategory("Wip")]
        //[TestMethod]
        //public void Test_AudioFormat_Raw_Stereo_8Bit()
        //    => Test_AudioFormat(Raw, Stereo, Byte);

        //[TestCategory("Wip")]
        //[TestMethod]
        //public void Test_AudioFormat_Raw_Mono_8Bit()
        //    => Test_AudioFormat(Raw, Mono, Byte);

        private void Test_AudioFormat(
            AudioFileFormatEnum audioFileFormatEnum,
            SpeakerSetupEnum speakerSetupEnum,
            SampleDataTypeEnum sampleDataTypeEnum,
            [CallerMemberName] string callerMemberName = null)
        {
            int channelCount = speakerSetupEnum.GetChannelCount();

            // Panned sine, save to file, use sample operator, save to file again

            Outlet getPannedSine() => Panning(Sine(_[FREQUENCY]), _[0.25]);

            AudioFileOutput audioFileOutput1 = SaveAudio(getPannedSine,    DURATION,           VOLUME,
                                                         speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum,
                                                         SAMPLING_RATE,    default,            callerMemberName).Data;

            SampleOperatorWrapper getSample()
            {
                var wrapper = Sample(audioFileOutput1.FilePath);

                // In case of RAW format, set some values explicitly.
                if (audioFileFormatEnum == Raw)
                {
                    wrapper.Sample.SamplingRate   = SAMPLING_RATE;
                    wrapper.Sample.SpeakerSetup   = audioFileOutput1.SpeakerSetup;
                    wrapper.Sample.SampleDataType = audioFileOutput1.SampleDataType;
                }

                return wrapper;
            }

            var sampleWrapper = getSample();

            AudioFileOutput audioFileOutput2 = SaveAudio(() => getSample(), DURATION_LONGER,    VOLUME,
                                                         speakerSetupEnum,  sampleDataTypeEnum, audioFileFormatEnum,
                                                         SAMPLING_RATE,     GetFileName("_Reloaded", callerMemberName)).Data;
            // Assert

            // AudioFileOutput
            foreach (AudioFileOutput audioFileOutput in new[] { audioFileOutput1, audioFileOutput2 })
            {
                // AudioFileOutput Values
                AreEqual(audioFileFormatEnum, () => audioFileOutput.GetAudioFileFormatEnum());
                AreEqual(speakerSetupEnum,    () => audioFileOutput.GetSpeakerSetupEnum());
                AreEqual(sampleDataTypeEnum,  () => audioFileOutput.GetSampleDataTypeEnum());
                AreEqual(SAMPLING_RATE,       () => audioFileOutput.SamplingRate);

                AreEqual(sampleDataTypeEnum.GetMaxAmplitude() * VOLUME, () => audioFileOutput.Amplifier);
                NotNullOrEmpty(() => audioFileOutput.FilePath);

                // AudioFileOutputChannels
                IsNotNull(() => audioFileOutput.AudioFileOutputChannels);
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

            // Specific per AudioFileOutput
            {
                string expectedFilePath = GetFileName(default, callerMemberName) + audioFileFormatEnum.GetFileExtension();
                AreEqual(expectedFilePath, () => audioFileOutput1.FilePath);

                AreEqual(DURATION, () => audioFileOutput1.Duration);
            }
            {
                string expectedFilePath = GetFileName("_Reloaded", callerMemberName) + audioFileFormatEnum.GetFileExtension();
                AreEqual(expectedFilePath, () => audioFileOutput2.FilePath);

                AreEqual(DURATION_LONGER, () => audioFileOutput2.Duration);
            }

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
                string expectedName = GetFileNameWithoutExtension(GetFileName(default, callerMemberName));
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
            AreEqual(1,                   () => sample.TimeMultiplier);
            AreEqual(true,                () => sample.IsActive);
            AreEqual(0,                   () => sample.BytesToSkip);
            AreEqual(SAMPLING_RATE,       () => sample.SamplingRate);
            AreEqual(sampleDataTypeEnum,  () => sample.GetSampleDataTypeEnum());
            AreEqual(speakerSetupEnum,    () => sample.GetSpeakerSetupEnum());
            AreEqual(audioFileFormatEnum, () => sample.GetAudioFileFormatEnum());
            AreEqual(Line,                () => sample.GetInterpolationTypeEnum());
            IsNotNull(() => sample.SampleOperators);
            AreEqual(1, () => sample.SampleOperators.Count);
            IsNotNull(() => sample.SampleOperators[0]);
            AreEqual(asSampleOperator, () => sample.SampleOperators[0]);
            IsNotNull(() => sample.Bytes);
            NotEqual(0, () => sample.Bytes.Length);
            {
                int expectedByteCount = (int)(audioFileFormatEnum.GetHeaderLength() + SAMPLING_RATE * sample.GetFrameSize() * DURATION);
                Assert.AreEqual(expectedByteCount, sample.Bytes.Length);
                Console.WriteLine($"Byte count = {sample.Bytes.Length}");

                string expectedLocation = GetFullPath(audioFileOutput1.FilePath);
                NotNullOrEmpty(() => sample.Location);
                AreEqual(expectedLocation, () => sample.Location);

                string expectedName = GetFileNameWithoutExtension(GetFileName(default, callerMemberName));
                NotNullOrEmpty(() => sample.Name);
                AreEqual(expectedName, () => sample.Name);
            }

            // Sample Outlet From Different Sources
            Outlet sampleOutlet_ImplicitConversionFromWrapper = sampleWrapper;
            Outlet sampleOutlet_FromWrapperResult             = sampleWrapper.Result;
            Outlet sampleOutlet_FromOperatorOutlets           = sampleOperator.Outlets[0];
            IsNotNull(() => sampleOutlet_ImplicitConversionFromWrapper);
            IsNotNull(() => sampleOutlet_FromWrapperResult);
            IsNotNull(() => sampleOutlet_FromOperatorOutlets);
            AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromWrapperResult);
            AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromOperatorOutlets);

            // Get Values
            
            double amplifier = sampleDataTypeEnum.GetMaxAmplitude() * VOLUME; // TODO: Shouldn't elsewhere already be an amplifier?
            double tolerance = GetTolerance(sampleDataTypeEnum);
            Console.WriteLine();
            Console.WriteLine($"{nameof(tolerance)} = {tolerance}");

            // Mono

            if (speakerSetupEnum == Mono)
            {
                // Get Values
                
                Channel = Single;

                double[] expectedValues =
                {
                    amplifier * 0.0,
                    amplifier * Sqrt(.5),
                    amplifier * 1.0,
                    amplifier * Sqrt(.5),
                    amplifier * 0.0,
                    amplifier * -Sqrt(.5),
                    amplifier * -1.0,
                    amplifier * -Sqrt(.5),
                    amplifier * 0.0
                };

                double[] actualValues =
                {
                    sampleWrapper.Calculate(time: 0.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 1.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 2.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 3.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 4.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 5.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 6.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 7.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 8.0 / 8.0 / FREQUENCY)
                };

                Console.WriteLine($"  {nameof(actualValues)} = {{ {string.Join(", ", actualValues)} }}");
                Console.WriteLine($"{nameof(expectedValues)} = {{ {string.Join(", ", expectedValues)} }}");

                // Assert Values
                
                Assert.AreEqual(expectedValues[0], actualValues[0], tolerance);
                Assert.AreEqual(expectedValues[1], actualValues[1], tolerance);
                Assert.AreEqual(expectedValues[2], actualValues[2], tolerance);
                Assert.AreEqual(expectedValues[3], actualValues[3], tolerance);
                Assert.AreEqual(expectedValues[4], actualValues[4], tolerance);
                Assert.AreEqual(expectedValues[5], actualValues[5], tolerance);
                Assert.AreEqual(expectedValues[6], actualValues[6], tolerance);
                Assert.AreEqual(expectedValues[7], actualValues[7], tolerance);
                Assert.AreEqual(expectedValues[8], actualValues[8], tolerance);
            }

            // Stereo

            if (speakerSetupEnum == Stereo)
            {
                // Left
                
                Channel = Left;

                double[] expectedL =
                {
                    amplifier * 0.75 * 0.0,
                    amplifier * 0.75 * Sqrt(.5),
                    amplifier * 0.75 * 1.0,
                    amplifier * 0.75 * Sqrt(.5),
                    amplifier * 0.75 * 0.0,
                    amplifier * 0.75 * -Sqrt(.5),
                    amplifier * 0.75 * -1.0,
                    amplifier * 0.75 * -Sqrt(.5),
                    amplifier * 0.75 * 0.0
                };

                double[] actualL =
                {
                    sampleWrapper.Calculate(time: 0.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 1.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 2.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 3.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 4.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 5.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 6.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 7.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 8.0 / 8.0 / FREQUENCY)
                };

                // Right
                
                Channel = Right;

                double[] expectedR =
                {
                    amplifier * 0.25 * 0.0,
                    amplifier * 0.25 * Sqrt(.5),
                    amplifier * 0.25 * 1.0,
                    amplifier * 0.25 * Sqrt(.5),
                    amplifier * 0.25 * 0.0,
                    amplifier * 0.25 * -Sqrt(.5),
                    amplifier * 0.25 * -1.0,
                    amplifier * 0.25 * -Sqrt(.5),
                    amplifier * 0.25 * 0.0
                };

                double[] actualR =
                {
                    sampleWrapper.Calculate(time: 0.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 1.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 2.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 3.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 4.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 5.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 6.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 7.0 / 8.0 / FREQUENCY),
                    sampleWrapper.Calculate(time: 8.0 / 8.0 / FREQUENCY)
                };

                Console.WriteLine($"{nameof(expectedL)} = {{ {string.Join(", ", expectedL)} }}");
                Console.WriteLine($"  {nameof(actualL)} = {{ {string.Join(", ", actualL)} }}");
                Console.WriteLine();
                Console.WriteLine($"{nameof(expectedR)} = {{ {string.Join(", ", expectedR)} }}");
                Console.WriteLine($"  {nameof(actualR)} = {{ {string.Join(", ", actualR)} }}");

                // Assert Values
                
                // Left
                
                Assert.AreEqual(expectedL[0], actualL[0], tolerance);
                Assert.AreEqual(expectedL[1], actualL[1], tolerance);
                Assert.AreEqual(expectedL[2], actualL[2], tolerance);
                Assert.AreEqual(expectedL[3], actualL[3], tolerance);
                Assert.AreEqual(expectedL[4], actualL[4], tolerance);
                Assert.AreEqual(expectedL[5], actualL[5], tolerance);
                Assert.AreEqual(expectedL[6], actualL[6], tolerance);
                Assert.AreEqual(expectedL[7], actualL[7], tolerance);
                Assert.AreEqual(expectedL[8], actualL[8], tolerance);

                // Right
                
                Assert.AreEqual(expectedR[0], actualR[0], tolerance);
                Assert.AreEqual(expectedR[1], actualR[1], tolerance);
                Assert.AreEqual(expectedR[2], actualR[2], tolerance);
                Assert.AreEqual(expectedR[3], actualR[3], tolerance);
                Assert.AreEqual(expectedR[4], actualR[4], tolerance);
                Assert.AreEqual(expectedR[5], actualR[5], tolerance);
                Assert.AreEqual(expectedR[6], actualR[6], tolerance);
                Assert.AreEqual(expectedR[7], actualR[7], tolerance);
                Assert.AreEqual(expectedR[8], actualR[8], tolerance);
            }
        }

        // Helpers

        private string GetFileName(string suffix, [CallerMemberName] string callerMemberName = null)
            => $"{callerMemberName}{suffix}";

        static double GetTolerance(SampleDataTypeEnum sampleDataTypeEnum)
        {
            switch (sampleDataTypeEnum)
            {
                case SampleDataTypeEnum.Int16: return 10;
                case SampleDataTypeEnum.Byte:  return 1;

                default:
                    throw new ValueNotSupportedException(sampleDataTypeEnum);
            }
        }
    }
}