using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
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
                Tape tape = null;
                Run(() => WithMono().Sine().Intercept(x => tape = x));
                IsNotNull(() => tape);
                AudioFileOutput audioFileOutputMono = tape.UnderlyingAudioFileOutput;
                IsNotNull(() => audioFileOutputMono);
                IsNotNull(() => audioFileOutputMono.SpeakerSetup);
                AreEqual(SpeakerSetupEnum.Mono, () => audioFileOutputMono.SpeakerSetup.ToEnum());
            }
            //Stereo Extensions
            {
                Tape tape = null;
                Run(() => WithStereo().Sine().Intercept(x => tape = x).Save());
                IsNotNull(() => tape);
                AudioFileOutput audioFileOutputStereo = tape.UnderlyingAudioFileOutput;
                IsNotNull(() => audioFileOutputStereo);
                IsNotNull(() => audioFileOutputStereo.SpeakerSetup);
                AreEqual(SpeakerSetupEnum.Stereo, () => audioFileOutputStereo.SpeakerSetup.ToEnum());
            }

            // Wav Extensions
            {
                Tape tape = null;
                Run(() => AsWav().Sine().Intercept(x => tape = x).Save());
                IsNotNull(() => tape);
                
                AudioFileOutput audioFileOutputWav = tape.UnderlyingAudioFileOutput;
                IsNotNull(() => audioFileOutputWav);
                IsNotNull(() => audioFileOutputWav.AudioFileFormat);
                AreEqual(".wav", () => audioFileOutputWav.AudioFileFormat.FileExtension());
                AreEqual(".wav", () => audioFileOutputWav.GetAudioFileFormatEnum().FileExtension());
                AreEqual(44, () => audioFileOutputWav.AudioFileFormat.HeaderLength());
                AreEqual(44, () => audioFileOutputWav.GetAudioFileFormatEnum().HeaderLength());
            }

            // Raw Extensions
            {
                AudioFileOutput audioFileOutputRaw = null;
                Run(() => AsRaw().Sine().Save().Intercept(x => audioFileOutputRaw = x.UnderlyingAudioFileOutput));
                IsNotNull(() => audioFileOutputRaw);
                IsNotNull(() => audioFileOutputRaw.AudioFileFormat);
                AreEqual(".raw", () => audioFileOutputRaw.AudioFileFormat.FileExtension());
                AreEqual(".raw", () => audioFileOutputRaw.GetAudioFileFormatEnum().FileExtension());
                AreEqual(0, () => audioFileOutputRaw.AudioFileFormat.HeaderLength());
                AreEqual(0, () => audioFileOutputRaw.GetAudioFileFormatEnum().HeaderLength());
            }

            // 16-Bit Helpers
            AreEqual(SampleDataTypeEnum.Int16, () => EnumSpecialWishes.GetSampleDataTypeEnum<short>());

            // 8-Bit Helpers
            AreEqual(SampleDataTypeEnum.Byte, () => EnumSpecialWishes.GetSampleDataTypeEnum<byte>());
        }
    }
}