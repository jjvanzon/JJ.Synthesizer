using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Math;
using static System.Reflection.MethodBase;
using static JJ.Business.Synthesizer.Constants.WavHeaderConstants;
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
        [TestCategory("Wip")]
        [TestMethod]
        public void Test_AudioFileFormat_Wav_Stereo_16Bit()
        {
            // Arrange
            Outlet getPannedSine() => Panning(Sine(A4), _[0.25]);

            // Act
            AudioFileOutput       audioFileOutput1 = SaveAudio(getPannedSine, DURATION, VOLUME, Stereo, Int16, Wav, SAMPLING_RATE).Data;
            SampleOperatorWrapper sampleWrapper    = Sample(audioFileOutput1.FilePath);
            AudioFileOutput       audioFileOutput2 = SaveAudio(() => sampleWrapper, DURATION, VOLUME, Stereo, Int16, Wav, SAMPLING_RATE, GetFileName("_Reloaded")).Data;

            // Assert

            // AudioFileOutput
            foreach (AudioFileOutput audioFileOutput in new[] { audioFileOutput1, audioFileOutput2 })
            {
                // AudioFileOutput Values
                AreEqual(Wav,            () => audioFileOutput.GetAudioFileFormatEnum());
                AreEqual(Stereo,         () => audioFileOutput.GetSpeakerSetupEnum());
                AreEqual(Int16,          () => audioFileOutput.GetSampleDataTypeEnum());
                AreEqual(32767 * VOLUME, () => audioFileOutput.Amplifier);
                AreEqual(DURATION,       () => audioFileOutput.Duration);
                AreEqual(SAMPLING_RATE,  () => audioFileOutput.SamplingRate);
                NotNullOrEmpty(() => audioFileOutput.FilePath);

                // AudioFileOutputChannels Filled In
                IsNotNull(() => audioFileOutput.AudioFileOutputChannels);
                AreEqual(2, () => audioFileOutput.AudioFileOutputChannels.Count);
                IsNotNull(() => audioFileOutput.AudioFileOutputChannels[0].Outlet);
                IsNotNull(() => audioFileOutput.AudioFileOutputChannels[1].Outlet);
                IsNotNull(() => audioFileOutput.AudioFileOutputChannels[0].AudioFileOutput);
                IsNotNull(() => audioFileOutput.AudioFileOutputChannels[1].AudioFileOutput);

                // AudioFileOutputChannels Equality
                AreEqual(0,               () => audioFileOutput.AudioFileOutputChannels[0].Index);
                AreEqual(1,               () => audioFileOutput.AudioFileOutputChannels[1].Index);
                AreEqual(audioFileOutput, () => audioFileOutput.AudioFileOutputChannels[0].AudioFileOutput);
                AreEqual(audioFileOutput, () => audioFileOutput.AudioFileOutputChannels[1].AudioFileOutput);
            }

            // AudioFileOutput FilePaths
            string expectedFilePath1 = $"{GetCurrentMethod()?.Name}{Wav.GetFileExtension()}";
            AreEqual(expectedFilePath1, () => audioFileOutput1.FilePath);
            
            string expectedFilePath2 = $"{GetCurrentMethod()?.Name}_Reloaded{Wav.GetFileExtension()}";
            AreEqual(expectedFilePath2, () => audioFileOutput2.FilePath);

            // Sample Wrapper
            IsNotNull(() => sampleWrapper);
            IsNotNull(() => sampleWrapper.Sample);
            IsNotNull(() => sampleWrapper.Result);

            // Sample Operator
            Operator sampleOperator = sampleWrapper.Result.Operator;
            IsNotNull(() => sampleOperator);
            AreEqual("SampleOperator",  () => sampleOperator.OperatorTypeName);
            AreEqual("Sample Operator", () => sampleOperator.Name);
            IsNull(() => sampleOperator.AsCurveIn);
            IsNull(() => sampleOperator.AsValueOperator);
            IsNotNull(() => sampleOperator.AsSampleOperator);

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
            AreEqual(1,             () => sample.TimeMultiplier);
            AreEqual(true,          () => sample.IsActive);
            AreEqual(0,             () => sample.BytesToSkip);
            AreEqual(SAMPLING_RATE, () => sample.SamplingRate);
            AreEqual(Int16,         () => sample.GetSampleDataTypeEnum());
            AreEqual(Stereo,        () => sample.GetSpeakerSetupEnum());
            AreEqual(Wav,           () => sample.GetAudioFileFormatEnum());
            AreEqual(Line,          () => sample.GetInterpolationTypeEnum());
            IsNotNull(() => sample.SampleOperators);
            AreEqual(1, () => sample.SampleOperators.Count);
            IsNotNull(() => sample.SampleOperators[0]);
            AreEqual(asSampleOperator, () => sample.SampleOperators[0]);
            IsNotNull(() => sample.Bytes);
            NotEqual(0, () => sample.Bytes.Length);
            int expectedByteCount = (int)(WAV_HEADER_LENGTH + SAMPLING_RATE * sample.GetFrameSize() * DURATION);
            Assert.AreEqual(expectedByteCount, sample.Bytes.Length);
            Console.WriteLine($"Byte count = {sample.Bytes.Length}");

            // Sample Outlet From Different Sources
            Outlet sampleOutlet_ImplicitConversionFromWrapper = sampleWrapper;
            Outlet sampleOutlet_FromWrapperResult             = sampleWrapper.Result;
            Outlet sampleOutlet_FromOperatorOutlets           = sampleOperator.Outlets[0];
            IsNotNull(() => sampleOutlet_ImplicitConversionFromWrapper);
            IsNotNull(() => sampleOutlet_FromWrapperResult);
            IsNotNull(() => sampleOutlet_FromOperatorOutlets);
            AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromWrapperResult);
            AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromOperatorOutlets);

            // ✖️ Incorrect (other)
            // NotNullOrEmpty(() => sample.Name);
            // string expectedLocation = Path.GetFullPath(audioFileOutput.FilePath);
            // AreEqual(expectedLocation, () => sample.Location);

            return;

            Channel = Left;
            double[] valuesLeftChannel =
            {
                sampleWrapper.Calculate(time: 0.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 1.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 2.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 3.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 4.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 5.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 6.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 7.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 8.0 / 8.0 / A4)
            };

            Assert.AreEqual(VOLUME * 0.75 * 0.0,      valuesLeftChannel[0]);
            Assert.AreEqual(VOLUME * 0.75 * Sqrt(2),  valuesLeftChannel[1]);
            Assert.AreEqual(VOLUME * 0.75 * 1.0,      valuesLeftChannel[2]);
            Assert.AreEqual(VOLUME * 0.75 * Sqrt(2),  valuesLeftChannel[3]);
            Assert.AreEqual(VOLUME * 0.75 * 0.0,      valuesLeftChannel[4]);
            Assert.AreEqual(VOLUME * 0.75 * -Sqrt(2), valuesLeftChannel[5]);
            Assert.AreEqual(VOLUME * 0.75 * -1.0,     valuesLeftChannel[6]);
            Assert.AreEqual(VOLUME * 0.75 * -Sqrt(2), valuesLeftChannel[7]);
            Assert.AreEqual(VOLUME * 0.75 * 0.0,      valuesLeftChannel[8]);

            Channel = Right;
            double[] valuesRightChannel =
            {
                sampleWrapper.Calculate(time: 0.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 1.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 2.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 3.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 4.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 5.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 6.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 7.0 / 8.0 / A4),
                sampleWrapper.Calculate(time: 8.0 / 8.0 / A4)
            };

            Assert.AreEqual(VOLUME * 0.25 * 0.0,      valuesRightChannel[0]);
            Assert.AreEqual(VOLUME * 0.25 * Sqrt(2),  valuesRightChannel[1]);
            Assert.AreEqual(VOLUME * 0.25 * 1.0,      valuesRightChannel[2]);
            Assert.AreEqual(VOLUME * 0.25 * Sqrt(2),  valuesRightChannel[3]);
            Assert.AreEqual(VOLUME * 0.25 * 0.0,      valuesRightChannel[4]);
            Assert.AreEqual(VOLUME * 0.25 * -Sqrt(2), valuesRightChannel[5]);
            Assert.AreEqual(VOLUME * 0.25 * -1.0,     valuesRightChannel[6]);
            Assert.AreEqual(VOLUME * 0.25 * -Sqrt(2), valuesRightChannel[7]);
            Assert.AreEqual(VOLUME * 0.25 * 0.0,      valuesRightChannel[8]);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Wav_Mono_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, DURATION, VOLUME, Mono, Int16, Wav, SAMPLING_RATE);
        }


        [TestMethod]
        public void Test_AudioFileFormat_Wav_Stereo_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, DURATION, VOLUME, Stereo, Byte, Wav, SAMPLING_RATE);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Wav_Mono_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, default, DURATION, Mono, Byte, Wav, SAMPLING_RATE);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Stereo_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, DURATION, VOLUME, Stereo, Int16, Raw, SAMPLING_RATE);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Mono_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, DURATION, VOLUME, Mono, Int16, Raw, SAMPLING_RATE);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Stereo_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, DURATION, VOLUME, Stereo, Byte, Raw, SAMPLING_RATE);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Mono_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, DURATION, VOLUME, Mono, Byte, Raw, SAMPLING_RATE);
        }

        // Helpers

        private string GetFileName<T>([CallerMemberName] string callerMemberName = null)
            => GetFileName(suffix: $"_{typeof(T).Name}", callerMemberName);

        private string GetFileName(string suffix, [CallerMemberName] string callerMemberName = null)
            => $"{callerMemberName}{suffix}";

        private const int    SAMPLING_RATE = 100;
        private const double DURATION      = 0.25;
        private const double VOLUME        = 0.50;

        // Want my static usings, but clashes with System type names.
        private readonly SampleDataTypeEnum Int16 = SampleDataTypeEnum.Int16;
        private readonly SampleDataTypeEnum Byte  = SampleDataTypeEnum.Byte;
    }
}