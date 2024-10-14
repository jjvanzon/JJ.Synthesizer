using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Math;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;

// ReSharper disable RedundantArgumentDefaultValue

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class AudioFormatTests : SynthesizerSugar
    {
        [TestCategory("Busy")]
        [TestMethod]
        public void Test_AudioFileFormat_Wav_Stereo_16Bit()
        {
            // Arrange
            Outlet getPannedSine() => Panning(Sine(A4), _[0.25]);

            // Act
            AudioFileOutput audioFileOutput  = SaveAudio(getPannedSine, 0, 1, Stereo, Int16, Wav, GetFileName<AudioFileOutput>()).Data;
            Outlet          sampleOutlet     = Sample(audioFileOutput.FilePath);
            AudioFileOutput audioFileOutput2 = SaveAudio(() => sampleOutlet, 0, 1, Stereo, Int16, Wav, GetFileName<Sample>()).Data;

            // Assert
            AssertHelper.AreEqual(32767, () => audioFileOutput.Amplifier);
            AssertHelper.AreEqual(Wav,   () => audioFileOutput.GetAudioFileFormatEnum());
            AssertHelper.AreEqual(Int16, () => audioFileOutput.GetSampleDataTypeEnum());

            return;

            // TODO: Asser AudioFileOutput entity data?
            // TODO: Assert Sample entity data
            //sample.Operator.AsSampleOperator.Sample.

            Channel = Left;
            double[] valuesLeftChannel =
            {
                sampleOutlet.Calculate(time: 0.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 1.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 2.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 3.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 4.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 5.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 6.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 7.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 8.0 / 8.0 / A4)
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
                sampleOutlet.Calculate(time: 0.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 1.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 2.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 3.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 4.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 5.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 6.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 7.0 / 8.0 / A4),
                sampleOutlet.Calculate(time: 8.0 / 8.0 / A4)
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
            SaveAudio(createOutlet, 0, 0, Mono, Int16, Wav);
        }


        [TestMethod]
        public void Test_AudioFileFormat_Wav_Stereo_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, Stereo, Byte, Wav);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Wav_Mono_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, Mono, Byte, Wav);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Stereo_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, Stereo, Int16, Raw);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Mono_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, Mono, Int16, Raw);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Stereo_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, Stereo, Byte, Raw);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Mono_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 1, Mono, Byte, Raw);
        }

        // Helpers

        private string GetFileName<T>([CallerMemberName] string callerMemberName = null) => $"{callerMemberName}_{typeof(T).Name}";

        // Want my static usings, but clashes with system type names.

        private readonly SampleDataTypeEnum Int16 = SampleDataTypeEnum.Int16;
        private readonly SampleDataTypeEnum Byte  = SampleDataTypeEnum.Byte;
    }
}