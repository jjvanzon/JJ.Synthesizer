using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Math;
using static System.Reflection.MethodBase;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;

// ReSharper disable RedundantArgumentDefaultValue

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class AudioFormatTests : SynthesizerSugar
    {
        [TestMethod]
        public void Test_AudioFileFormat_Wav_Stereo_16Bit()
        {
            // Arrange
            Outlet getPannedSine() => Panning(Sine(A4), _[0.25]);

            // Act
            var    saved  = SaveAudio(getPannedSine, 0, 1, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Wav, fileName: $"{GetCurrentMethod()?.Name}_AudioFileOut");
            Outlet sample = Sample(saved.Data.FilePath);
            var    saved2 = SaveAudio(() => sample, fileName: $"{GetCurrentMethod()?.Name}_LoadedSampleResaved");

            // Assert

            return;

            // TODO: Asser AudioFileOutput entity data?
            // TODO: Assert Sample entity data
            //sample.Operator.AsSampleOperator.Sample.

            Channel = Left;
            double[] valuesLeftChannel =
            {
                sample.Calculate(time: 0.0 / 8.0 / A4),
                sample.Calculate(time: 1.0 / 8.0 / A4),
                sample.Calculate(time: 2.0 / 8.0 / A4),
                sample.Calculate(time: 3.0 / 8.0 / A4),
                sample.Calculate(time: 4.0 / 8.0 / A4),
                sample.Calculate(time: 5.0 / 8.0 / A4),
                sample.Calculate(time: 6.0 / 8.0 / A4),
                sample.Calculate(time: 7.0 / 8.0 / A4),
                sample.Calculate(time: 8.0 / 8.0 / A4)
            };

            Assert.AreEqual(0.75 * 0.0,      valuesLeftChannel[0]);
            Assert.AreEqual(0.75 * Sqrt(2),  valuesLeftChannel[1]);
            Assert.AreEqual(0.75 * 1.0,      valuesLeftChannel[2]);
            Assert.AreEqual(0.75 * Sqrt(2),  valuesLeftChannel[3]);
            Assert.AreEqual(0.75 * 0.0,      valuesLeftChannel[4]);
            Assert.AreEqual(0.75 * -Sqrt(2), valuesLeftChannel[5]);
            Assert.AreEqual(0.75 * -1.0,     valuesLeftChannel[6]);
            Assert.AreEqual(0.75 * -Sqrt(2), valuesLeftChannel[7]);
            Assert.AreEqual(0.75 * 0.0,      valuesLeftChannel[8]);

            Channel = Right;
            double[] valuesRightChannel =
            {
                sample.Calculate(time: 0.0 / 8.0 / A4),
                sample.Calculate(time: 1.0 / 8.0 / A4),
                sample.Calculate(time: 2.0 / 8.0 / A4),
                sample.Calculate(time: 3.0 / 8.0 / A4),
                sample.Calculate(time: 4.0 / 8.0 / A4),
                sample.Calculate(time: 5.0 / 8.0 / A4),
                sample.Calculate(time: 6.0 / 8.0 / A4),
                sample.Calculate(time: 7.0 / 8.0 / A4),
                sample.Calculate(time: 8.0 / 8.0 / A4)
            };

            Assert.AreEqual(0.25 * 0.0,      valuesRightChannel[0]);
            Assert.AreEqual(0.25 * Sqrt(2),  valuesRightChannel[1]);
            Assert.AreEqual(0.25 * 1.0,      valuesRightChannel[2]);
            Assert.AreEqual(0.25 * Sqrt(2),  valuesRightChannel[3]);
            Assert.AreEqual(0.25 * 0.0,      valuesRightChannel[4]);
            Assert.AreEqual(0.25 * -Sqrt(2), valuesRightChannel[5]);
            Assert.AreEqual(0.25 * -1.0,     valuesRightChannel[6]);
            Assert.AreEqual(0.25 * -Sqrt(2), valuesRightChannel[7]);
            Assert.AreEqual(0.25 * 0.0,      valuesRightChannel[8]);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Wav_Mono_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 0, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Wav);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Wav_Stereo_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Wav);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Wav_Mono_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Wav);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Stereo_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Raw);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Mono_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Raw);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Stereo_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Raw);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Mono_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Raw);
        }
    }
}
