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
        public void Test_AudioFileFormat_Stereo_16Bit_Wav()
        {
            Outlet createOutlet() => Panning(Sine(_[1]), _[0.25]);
            SaveWav(createOutlet, 0, 0, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Wav);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Mono_16Bit_Wav()
        {
            Outlet createOutlet() => Panning(Sine(_[1]), _[0.25]);
            SaveWav(createOutlet, 0, 0, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Wav);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Stereo_8Bit_Wav()
        {
            Outlet createOutlet() => Panning(Sine(_[1]), _[0.25]);
            SaveWav(createOutlet, 0, 0, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Wav);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Mono_8Bit_Wav()
        {
            Outlet createOutlet() => Panning(Sine(_[1]), _[0.25]);
            SaveWav(createOutlet, 0, 0, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Wav);
        }

        [TestMethod]
        public void Test_AudioFileFormat_Stereo_16Bit_Raw()
        {
            Outlet createOutlet() => Panning(Sine(_[1]), _[0.25]);
            SaveWav(createOutlet, 0, 0, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Raw);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Mono_16Bit_Raw()
        {
            Outlet createOutlet() => Panning(Sine(_[1]), _[0.25]);
            SaveWav(createOutlet, 0, 0, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Int16, AudioFileFormatEnum.Raw);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Stereo_8Bit_Raw()
        {
            Outlet createOutlet() => Panning(Sine(_[1]), _[0.25]);
            SaveWav(createOutlet, 0, 0, SpeakerSetupEnum.Stereo, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Raw);
        }
        
        [TestMethod]
        public void Test_AudioFileFormat_Mono_8Bit_Raw()
        {
            Outlet createOutlet() => Panning(Sine(_[1]), _[0.25]);
            SaveWav(createOutlet, 0, 0, SpeakerSetupEnum.Mono, SampleDataTypeEnum.Byte, AudioFileFormatEnum.Raw);
        }
    }
}
