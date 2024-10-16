using System;
using System.Linq;
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
using static System.MidpointRounding;
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
        private const int    SAMPLING_RATE = 4000;
        private const double FREQUENCY     = 40;
        private const double VOLUME        = 0.50;
        private const double PANNING       = 0.25;
        private const double DURATION      = 0.25;
        private const double DURATION2     = DURATION * 1.001;
        private const int    DECIMALS      = 4;

        [UsedImplicitly]
        public AudioFormatTests()
        { }

        AudioFormatTests(IContext context)
            : base(context)
        { }

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Stereo, Int16);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Mono, Int16);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Stereo, Byte);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Mono, Byte);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Stereo, Int16);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Mono, Int16);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Stereo, Byte);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Mono, Byte);
        }

        private void Test_AudioFormat(
            AudioFileFormatEnum audioFileFormatEnum,
            SpeakerSetupEnum speakerSetupEnum,
            SampleDataTypeEnum sampleDataTypeEnum,
            [CallerMemberName] string callerMemberName = null)
        {
            // Panned sine, save to file, use sample operator, save to file again

            Outlet getSignal()
            {
                var sine      = Sine(_[FREQUENCY]);
                var amplified = Multiply(sine, _[VOLUME]);
                var panned    = Panning(amplified, _[PANNING]);
                return panned;
            }

            AudioFileOutput audioFileOutput1 =
                SaveAudio(getSignal, DURATION, volume: 1, 
                          speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum,
                          SAMPLING_RATE, callerMemberName).Data;

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

                wrapper.Sample.Amplifier = 1.0 / audioFileOutput1.SampleDataType.GetMaxAmplitude();

                return wrapper;
            }

            AudioFileOutput audioFileOutput2 =
                SaveAudio(() => getSample(), DURATION2, volume: 1,
                          speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum,
                          SAMPLING_RATE, GetFileName("_Reloaded", callerMemberName)).Data;

            Channel = Single;
            SampleOperatorWrapper monoSampleWrapper = getSample();
            
            Channel = Left;
            SampleOperatorWrapper sampleWrapperLeft = getSample();
            
            Channel = Right;
            SampleOperatorWrapper sampleWrapperRight = getSample();

            AssertEntities(
                audioFileOutput1,  audioFileOutput2, 
                monoSampleWrapper, sampleWrapperLeft, sampleWrapperRight,
                audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum,
                callerMemberName);

            // Get Values

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
                    Calculate(monoSampleWrapper, time: 0.0 / 8.0 / FREQUENCY),
                    Calculate(monoSampleWrapper, time: 1.0 / 8.0 / FREQUENCY),
                    Calculate(monoSampleWrapper, time: 2.0 / 8.0 / FREQUENCY),
                    Calculate(monoSampleWrapper, time: 3.0 / 8.0 / FREQUENCY),
                    Calculate(monoSampleWrapper, time: 4.0 / 8.0 / FREQUENCY),
                    Calculate(monoSampleWrapper, time: 5.0 / 8.0 / FREQUENCY),
                    Calculate(monoSampleWrapper, time: 6.0 / 8.0 / FREQUENCY),
                    Calculate(monoSampleWrapper, time: 7.0 / 8.0 / FREQUENCY),
                    Calculate(monoSampleWrapper, time: 8.0 / 8.0 / FREQUENCY)
                };


                Console.WriteLine($"{nameof(expectedValues)} = {FormatValues(expectedValues)}");
                Console.WriteLine($"{nameof(actualValues  )} = {FormatValues(actualValues  )}");

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

                double[] actualL =
                {
                    Calculate(sampleWrapperLeft, time: 0.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperLeft, time: 1.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperLeft, time: 2.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperLeft, time: 3.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperLeft, time: 4.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperLeft, time: 5.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperLeft, time: 6.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperLeft, time: 7.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperLeft, time: 8.0 / 8.0 / FREQUENCY)
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

                double[] actualR =
                {
                    Calculate(sampleWrapperRight, time: 0.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperRight, time: 1.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperRight, time: 2.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperRight, time: 3.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperRight, time: 4.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperRight, time: 5.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperRight, time: 6.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperRight, time: 7.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperRight, time: 8.0 / 8.0 / FREQUENCY)
                };

                Console.WriteLine($"{nameof(expectedL)} = {FormatValues(expectedL)}");
                Console.WriteLine($"{nameof(actualL  )} = {FormatValues(actualL  )}");

                Console.WriteLine();
                Console.WriteLine($"{nameof(expectedR)} = {FormatValues(expectedR)}");
                Console.WriteLine($"{nameof(actualR  )} = {FormatValues(actualR  )}");

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

        private void AssertEntities(
            AudioFileOutput audioFileOutput1,
            AudioFileOutput audioFileOutput2,
            SampleOperatorWrapper sampleWrapperMono,
            SampleOperatorWrapper sampleWrapperLeft,
            SampleOperatorWrapper sampleWrapperRight,
            AudioFileFormatEnum audioFileFormatEnum,
            SpeakerSetupEnum speakerSetupEnum,
            SampleDataTypeEnum sampleDataTypeEnum,
            string callerMemberName)
        {
            int channelCount = speakerSetupEnum.GetChannelCount();

            // AudioFileOutput
            var audioFileOutputs = new[] { audioFileOutput1, audioFileOutput2 };
            foreach (var audioFileOutput in audioFileOutputs)
            {
                // AudioFileOutput Values
                AreEqual(audioFileFormatEnum, () => audioFileOutput.GetAudioFileFormatEnum());
                AreEqual(speakerSetupEnum,    () => audioFileOutput.GetSpeakerSetupEnum());
                AreEqual(sampleDataTypeEnum,  () => audioFileOutput.GetSampleDataTypeEnum());
                AreEqual(SAMPLING_RATE,       () => audioFileOutput.SamplingRate);

                AreEqual(sampleDataTypeEnum.GetMaxAmplitude(), () => audioFileOutput.Amplifier);
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

                AreEqual(DURATION2, () => audioFileOutput2.Duration);
            }

            var sampleWrappers = new[] { sampleWrapperMono, sampleWrapperLeft, sampleWrapperRight };
            foreach (var sampleWrapper in sampleWrappers)
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
            }
        }

        // Helpers

        private string GetFileName(string suffix, [CallerMemberName] string callerMemberName = null)
            => $"{callerMemberName}{suffix}";

        static double GetTolerance(SampleDataTypeEnum sampleDataTypeEnum)
        {
            switch (sampleDataTypeEnum)
            {
                case SampleDataTypeEnum.Int16: return 0.001;
                case SampleDataTypeEnum.Byte: return 0.01;

                default:
                    throw new ValueNotSupportedException(sampleDataTypeEnum);
            }
        }
        
        static double RoundValue(double x) => Round(x, DECIMALS, AwayFromZero);

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