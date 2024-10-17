using JetBrains.Annotations;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class AudioFileWishesTests : SynthesizerSugar
    {
        /// <summary> Constructor for test runner. </summary>
        [UsedImplicitly]
        public AudioFileWishesTests()
        { }

        /// <summary> Constructor allowing each test to run in its own instance. </summary>
        public AudioFileWishesTests(IContext context)
            : base(context)
        { }

        /// <summary>
        /// Testing extension methods in <see cref="AudioConversionExtensionWishes" />
        /// that didn't get any coverage elsewhere.
        /// </summary>
        [TestMethod]
        public void Test_AudioFileExtensionWishes()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new AudioFileWishesTests(context).AudioFileExtensionWishes_RunTest();
        }

        void AudioFileExtensionWishes_RunTest()
        {
            // Channel Count => Speaker Setup
            AreEqual(Mono,   () => 1.GetSpeakerSetupEnum());
            AreEqual(Stereo, () => 2.GetSpeakerSetupEnum());

            // Mono Extensions
            {
                AudioFileOutput audioFileOutputMono = SaveAudio(() => Sine(), speakerSetupEnum: Mono).Data;
                IsNotNull(() => audioFileOutputMono);
                IsNotNull(() => audioFileOutputMono.SpeakerSetup);
                AreEqual(Mono, () => audioFileOutputMono.SpeakerSetup.ToEnum());
            }

            // Stereo Extensions
            {
                AudioFileOutput audioFileOutputStereo = SaveAudio(() => Sine(), speakerSetupEnum: Stereo).Data;
                IsNotNull(() => audioFileOutputStereo);
                IsNotNull(() => audioFileOutputStereo.SpeakerSetup);
                AreEqual(Stereo, () => audioFileOutputStereo.SpeakerSetup.ToEnum());
            }

            // Wav Extensions
            {
                AudioFileOutput audioFileOutputWav = SaveAudio(() => Sine(), audioFileFormatEnum: Wav).Data;
                IsNotNull(() => audioFileOutputWav);
                IsNotNull(() => audioFileOutputWav.AudioFileFormat);
                AreEqual(".wav", () => audioFileOutputWav.AudioFileFormat.GetFileExtension());
                AreEqual(".wav", () => audioFileOutputWav.GetAudioFileFormatEnum().GetFileExtension());
                AreEqual(44,     () => audioFileOutputWav.AudioFileFormat.GetHeaderLength());
                AreEqual(44,     () => audioFileOutputWav.GetAudioFileFormatEnum().GetHeaderLength());
            }

            // Raw Extensions
            {
                AudioFileOutput audioFileOutputRaw = SaveAudio(() => Sine(), audioFileFormatEnum: Raw).Data;
                IsNotNull(() => audioFileOutputRaw);
                IsNotNull(() => audioFileOutputRaw.AudioFileFormat);
                AreEqual(".raw", () => audioFileOutputRaw.AudioFileFormat.GetFileExtension());
                AreEqual(".raw", () => audioFileOutputRaw.GetAudioFileFormatEnum().GetFileExtension());
                AreEqual(0,      () => audioFileOutputRaw.AudioFileFormat.GetHeaderLength());
                AreEqual(0,      () => audioFileOutputRaw.GetAudioFileFormatEnum().GetHeaderLength());
            }

            // 16-Bit Helpers
            AreEqual(Int16, () => AudioConversionExtensionWishes.GetSampleDataTypeEnum<short>());
            
            // 8-Bit Helpers
            AreEqual(Byte, () => AudioConversionExtensionWishes.GetSampleDataTypeEnum<byte>());
        }
    }
}