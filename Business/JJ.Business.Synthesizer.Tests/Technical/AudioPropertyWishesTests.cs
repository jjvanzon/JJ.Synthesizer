using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioPropertyWishesTests : SynthWishes
    {
        /// <inheritdoc cref="docs._testaudiofileextensionwishes"/>
        [TestMethod] public void AudioPropertyWishes_Test()
        {
            // Channel Count => Speaker Setup
            AreEqual(SpeakerSetupEnum.Mono,   () => 1.ToSpeakerSetupEnum());
            AreEqual(SpeakerSetupEnum.Stereo, () => 2.ToSpeakerSetupEnum());

            // Mono Extensions
            {
                Buff buff = null;
                Run(() => WithMono().Sine().Intercept(x => buff = x));
                IsNotNull(() => buff);
                
                AudioFileOutput audioFileOutputMono = buff.UnderlyingAudioFileOutput;
                IsNotNull(() => audioFileOutputMono);
                IsNotNull(() => audioFileOutputMono.SpeakerSetup);
                AreEqual(SpeakerSetupEnum.Mono, () => audioFileOutputMono.SpeakerSetup.ToEnum());
            }
            //Stereo Extensions
            {
                Buff buff = null;
                Run(() => WithStereo().Sine().Intercept(x => buff = x).Save());
                IsNotNull(() => buff);
                
                AudioFileOutput audioFileOutputStereo = buff.UnderlyingAudioFileOutput;
                IsNotNull(() => audioFileOutputStereo);
                IsNotNull(() => audioFileOutputStereo.SpeakerSetup);
                AreEqual(SpeakerSetupEnum.Stereo, () => audioFileOutputStereo.SpeakerSetup.ToEnum());
            }

            // Wav Extensions
            {
                Buff buff = null;
                Run(() => AsWav().Sine().Intercept(x => buff = x).Save());
                IsNotNull(() => buff);
                
                AudioFileOutput audioFileOutputWav = buff.UnderlyingAudioFileOutput;
                IsNotNull(() => audioFileOutputWav);
                IsNotNull(() => audioFileOutputWav.AudioFileFormat);
                AreEqual(".wav", () => audioFileOutputWav.AudioFileFormat.GetFileExtension());
                AreEqual(".wav", () => audioFileOutputWav.GetAudioFileFormatEnum().GetFileExtension());
                AreEqual(44, () => audioFileOutputWav.AudioFileFormat.GetHeaderLength());
                AreEqual(44, () => audioFileOutputWav.GetAudioFileFormatEnum().GetHeaderLength());
            }

            // Raw Extensions
            {
                AudioFileOutput audioFileOutputRaw = null;
                Run(() => AsRaw().Sine().Save().Intercept(x => audioFileOutputRaw = x.UnderlyingAudioFileOutput));
                IsNotNull(() => audioFileOutputRaw);
                IsNotNull(() => audioFileOutputRaw.AudioFileFormat);
                AreEqual(".raw", () => audioFileOutputRaw.AudioFileFormat.GetFileExtension());
                AreEqual(".raw", () => audioFileOutputRaw.GetAudioFileFormatEnum().GetFileExtension());
                AreEqual(0, () => audioFileOutputRaw.AudioFileFormat.GetHeaderLength());
                AreEqual(0, () => audioFileOutputRaw.GetAudioFileFormatEnum().GetHeaderLength());
            }

            // 16-Bit Helpers
            AreEqual(SampleDataTypeEnum.Int16, () => EnumSpecialWishes.GetSampleDataTypeEnum<short>());

            // 8-Bit Helpers
            AreEqual(SampleDataTypeEnum.Byte, () => EnumSpecialWishes.GetSampleDataTypeEnum<byte>());
        }
    }
}