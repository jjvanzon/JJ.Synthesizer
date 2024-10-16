using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
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
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// ReSharper disable RedundantArgumentDefaultValue

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class AudioFormatTests : SynthesizerSugar
    {
        // Aligned
        //private const int    SAMPLING_RATE = 4096;
        //private const double FREQUENCY     = 64;
        
        // Non aligned
        private const int    SAMPLING_RATE = 4093;
        private const double FREQUENCY     = 61;
        
        private const double VOLUME        = 0.50;
        private const double PANNING       = 0.25;
        private const double DURATION      = 0.25;
        private const double DURATION2     = DURATION * 1.01;
        private const int    DECIMALS      = 4;

        [UsedImplicitly]
        public AudioFormatTests()
        { }

        AudioFormatTests(IContext context)
            : base(context)
        { }

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Linear()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Stereo, Int16, Line);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Stereo, Int16, Block);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Linear()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Mono, Int16, Line);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Block()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Mono, Int16, Block);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Linear()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Stereo, Byte, Line);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_8Bit_Blocky()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Stereo, Byte, Block);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Linear()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Mono, Byte, Line);
        }

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_8Bit_Blocky()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Wav, Mono, Byte, Block);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Linear()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Stereo, Int16, Line);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Stereo, Int16, Block);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Linear()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Mono, Int16, Line);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Blocky()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Mono, Int16, Block);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Linear()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Stereo, Byte, Line);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_8Bit_Blocky()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Stereo, Byte, Block);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Linear()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Mono, Byte, Line);
        }

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_8Bit_Blocky()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFormatTests(context).Test_AudioFormat(Raw, Mono, Byte, Block);
        }

        private void Test_AudioFormat(
            AudioFileFormatEnum audioFileFormatEnum,
            SpeakerSetupEnum speakerSetupEnum,
            SampleDataTypeEnum sampleDataTypeEnum,
            InterpolationTypeEnum interpolationTypeEnum,
            [CallerMemberName] string callerMemberName = null)
        {
            // Arrange
            
            // Panned, amplified sine
            Outlet getSignal()
            {
                var sine      = Sine(_[FREQUENCY]);
                var amplified = Multiply(sine, _[VOLUME]);
                var panned    = Panning(amplified, _[PANNING]);
                return panned;
            }

            // Save to file
            AudioFileOutput audioFileOutput1 =
                SaveAudio(getSignal, DURATION, volume: 1, 
                          speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, SAMPLING_RATE, 
                          fileName: default, callerMemberName).Data;

            // Use sample operator
            SampleOperatorWrapper getSample()
            {
                var wrapper = Sample(audioFileOutput1.FilePath, interpolationTypeEnum);

                if (audioFileFormatEnum == Raw)
                {
                    // In case of RAW format, set some values explicitly.
                    wrapper.Sample.SamplingRate   = SAMPLING_RATE;
                    wrapper.Sample.SpeakerSetup   = audioFileOutput1.SpeakerSetup;
                    wrapper.Sample.SampleDataType = audioFileOutput1.SampleDataType;
                    wrapper.Sample.Amplifier      = 1.0 / audioFileOutput1.SampleDataType.GetMaxAmplitude();
                }

                return wrapper;
            }
            
            // Save to file again
            AudioFileOutput audioFileOutput2 =
                SaveAudio(() => getSample(), DURATION2, volume: 1,
                                speakerSetupEnum, sampleDataTypeEnum, audioFileFormatEnum, SAMPLING_RATE,
                                fileName: $"{callerMemberName}_Reloaded").Data;

            // Assert AudioFileOutput Entities

            string expectedFilePath1 = $"{callerMemberName}" + audioFileFormatEnum.GetFileExtension();
            
            AssertAudioFileOutputEntities(
                audioFileOutput1,
                audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum,
                expectedFilePath1, DURATION);

            string expectedFilePath2 = $"{callerMemberName}_Reloaded" + audioFileFormatEnum.GetFileExtension();
            
            AssertAudioFileOutputEntities(
                audioFileOutput2,
                audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum,
                expectedFilePath2, DURATION2);

            // Assert Samples Entities

            if (speakerSetupEnum == Mono)
            {
                Channel = Single;
                
                var sampleWrapperMono  = getSample();
                
                AssertSampleEntities(sampleWrapperMono,
                                     audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, interpolationTypeEnum,
                                     expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
            }

            if (speakerSetupEnum == Stereo)
            {
                Channel = Left;
                
                var sampleWrapperLeft  = getSample();

                AssertSampleEntities(sampleWrapperLeft,
                                     audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, interpolationTypeEnum,
                                     expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);

                Channel = Right;
                
                var sampleWrapperRight = getSample();

                AssertSampleEntities(sampleWrapperRight,
                                     audioFileFormatEnum, speakerSetupEnum, sampleDataTypeEnum, interpolationTypeEnum,
                                     expectedDuration: DURATION, audioFileOutput1.FilePath, callerMemberName);
            }
            
            // Get Values

            double tolerance = GetValueTolerance(sampleDataTypeEnum, interpolationTypeEnum);
            Console.WriteLine();
            Console.WriteLine($"{nameof(tolerance)} = {tolerance}");

            // Mono

            if (speakerSetupEnum == Mono)
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
                    Calculate(sampleWrapperMono, time: 0.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperMono, time: 1.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperMono, time: 2.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperMono, time: 3.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperMono, time: 4.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperMono, time: 5.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperMono, time: 6.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperMono, time: 7.0 / 8.0 / FREQUENCY),
                    Calculate(sampleWrapperMono, time: 8.0 / 8.0 / FREQUENCY)
                };


                Console.WriteLine($"{nameof(expectedValues)} = {FormatValues(expectedValues)}");
                Console.WriteLine($"{nameof(actualValues)}   = {FormatValues(actualValues  )}");

                // Assert Values
                
                AreEqual(expectedValues[0], actualValues[0], tolerance);
                AreEqual(expectedValues[1], actualValues[1], tolerance);
                AreEqual(expectedValues[2], actualValues[2], tolerance);
                AreEqual(expectedValues[3], actualValues[3], tolerance);
                AreEqual(expectedValues[4], actualValues[4], tolerance);
                AreEqual(expectedValues[5], actualValues[5], tolerance);
                AreEqual(expectedValues[6], actualValues[6], tolerance);
                AreEqual(expectedValues[7], actualValues[7], tolerance);
                AreEqual(expectedValues[8], actualValues[8], tolerance);
            }

            // Stereo

            if (speakerSetupEnum == Stereo)
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

                var sampleWrapperLeft  = getSample();

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

                var sampleWrapperRight = getSample();

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
                Console.WriteLine($"  {nameof(actualL)} = {FormatValues(actualL  )}");

                Console.WriteLine();
                Console.WriteLine($"{nameof(expectedR)} = {FormatValues(expectedR)}");
                Console.WriteLine($"  {nameof(actualR)} = {FormatValues(actualR  )}");

                // Assert Values
                
                // Left
                
                AreEqual(expectedL[0], actualL[0], tolerance);
                AreEqual(expectedL[1], actualL[1], tolerance);
                AreEqual(expectedL[2], actualL[2], tolerance);
                AreEqual(expectedL[3], actualL[3], tolerance);
                AreEqual(expectedL[4], actualL[4], tolerance);
                AreEqual(expectedL[5], actualL[5], tolerance);
                AreEqual(expectedL[6], actualL[6], tolerance);
                AreEqual(expectedL[7], actualL[7], tolerance);
                AreEqual(expectedL[8], actualL[8], tolerance);

                // Right
                
                AreEqual(expectedR[0], actualR[0], tolerance);
                AreEqual(expectedR[1], actualR[1], tolerance);
                AreEqual(expectedR[2], actualR[2], tolerance);
                AreEqual(expectedR[3], actualR[3], tolerance);
                AreEqual(expectedR[4], actualR[4], tolerance);
                AreEqual(expectedR[5], actualR[5], tolerance);
                AreEqual(expectedR[6], actualR[6], tolerance);
                AreEqual(expectedR[7], actualR[7], tolerance);
                AreEqual(expectedR[8], actualR[8], tolerance);
            }
        }

        static void AssertAudioFileOutputEntities(
            AudioFileOutput audioFileOutput, 
            AudioFileFormatEnum audioFileFormatEnum,
            SpeakerSetupEnum speakerSetupEnum, 
            SampleDataTypeEnum sampleDataTypeEnum, 
            string expectedFilePath, 
            double expectedDuration)
        {
            // AudioFileOutput
            IsNotNull(() => audioFileOutput.AudioFileFormat);
            IsNotNull(() => audioFileOutput.SampleDataType);
            IsNotNull(() => audioFileOutput.SpeakerSetup);
            
            AreEqual(SAMPLING_RATE,       () => audioFileOutput.SamplingRate);
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
                string expectedName = GetFileNameWithoutExtension($"{callerMemberName}{default(string)}");
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
            AreEqual(1,                                () => sample.TimeMultiplier);
            AreEqual(true,                             () => sample.IsActive);
            AreEqual(0,                                () => sample.BytesToSkip);
            AreEqual(SAMPLING_RATE,                    () => sample.SamplingRate);
            AreEqual(sampleDataTypeEnum,               () => sample.GetSampleDataTypeEnum());
            AreEqual(speakerSetupEnum,                 () => sample.GetSpeakerSetupEnum());
            AreEqual(audioFileFormatEnum,              () => sample.GetAudioFileFormatEnum());
            AreEqual(interpolationTypeEnum,            () => sample.GetInterpolationTypeEnum());

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
                
                int byteCountExpected  = (int)(audioFileFormatEnum.GetHeaderLength() + SAMPLING_RATE * sample.GetFrameSize() * DURATION);
                int byteCountTolerance = GetByteCountTolerance(sampleDataTypeEnum);

                Console.WriteLine();
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

                string expectedName = GetFileNameWithoutExtension($"{callerMemberName}{default(string)}");
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

        private double GetValueTolerance(
            SampleDataTypeEnum sampleDataTypeEnum,
            InterpolationTypeEnum interpolationTypeEnum)
        {
            if (sampleDataTypeEnum == Int16 && interpolationTypeEnum == Line)
            {
                return 0.001;
            }

            if (sampleDataTypeEnum == Int16 && interpolationTypeEnum == Block)
            {
                return 0.050;
            }

            if (sampleDataTypeEnum == Byte && interpolationTypeEnum == Line)
            {
                return 0.010;
            }

            if (sampleDataTypeEnum == Byte && interpolationTypeEnum == Block)
            {
                return 0.050;
            }

            throw new NotSupportedException(
                "Combination of values {" + new { sampleDataTypeEnum, interpolationTypeEnum } + "} not supported.");
        }

        private int GetByteCountTolerance(SampleDataTypeEnum sampleDataTypeEnum) 
            => 2 * sampleDataTypeEnum.SizeOf();

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