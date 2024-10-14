using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class AudioFormatTests : SynthesizerSugar
    {
        [TestMethod]
        public void Test_AudioFileFormat_Wav_Stereo_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 0, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Wav);

            //Outlet sample = Sample();
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
            SaveAudio(createOutlet, 0, 0, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Wav);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Wav_Mono_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 0, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Wav);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Raw_Stereo_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 0, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Raw);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Raw_Mono_16Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 0, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Raw);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Raw_Stereo_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 0, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Raw);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Raw_Mono_8Bit()
        {
            Outlet createOutlet() => Panning(Sine(A4), _[0.25]);
            SaveAudio(createOutlet, 0, 0, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Raw);
        }
    }
}
