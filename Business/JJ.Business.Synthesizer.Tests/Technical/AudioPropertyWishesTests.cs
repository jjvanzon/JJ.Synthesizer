using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.AudioPropertyWishes;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioPropertyWishesTests : SynthWishes
    {
        /// <inheritdoc cref="docs._testaudiopropertywishes"/>
        [TestMethod] 
        public void MonoExtensions_Test()
        {
            Tape tape = null;
            Run(() => WithMono().Sine().AfterRecord(x => tape = x));
            IsNotNull(() => tape);
            AudioFileOutput audioFileOutputMono = tape.UnderlyingAudioFileOutput;
            IsNotNull(() => audioFileOutputMono);
            IsNotNull(() => audioFileOutputMono.SpeakerSetup);
            AreEqual(SpeakerSetupEnum.Mono, () => audioFileOutputMono.SpeakerSetup.ToEnum());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishes"/>
        [TestMethod]
        public void StereoExtensions_Test()
        {
            Tape tape = null;
            Run(() => WithStereo().Sine().AfterRecord(x => tape = x).Save());
            IsNotNull(() => tape);
            AudioFileOutput audioFileOutputStereo = tape.UnderlyingAudioFileOutput;
            IsNotNull(() => audioFileOutputStereo);
            IsNotNull(() => audioFileOutputStereo.SpeakerSetup);
            AreEqual(SpeakerSetupEnum.Stereo, () => audioFileOutputStereo.SpeakerSetup.ToEnum());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishes"/>
        [TestMethod]
        public void WavExtensions_Test()
        {
            Tape tape = null;
            Run(() => AsWav().Sine().AfterRecord(x => tape = x).Save());
            IsNotNull(() => tape);
            
            AudioFileOutput audioFileOutputWav = tape.UnderlyingAudioFileOutput;
            IsNotNull(() => audioFileOutputWav);
            IsNotNull(() => audioFileOutputWav.AudioFileFormat);
            AreEqual(".wav", () => audioFileOutputWav.AudioFileFormat.FileExtension());
            AreEqual(".wav", () => audioFileOutputWav.GetAudioFileFormatEnum().FileExtension());
            AreEqual(44,     () => audioFileOutputWav.AudioFileFormat.HeaderLength());
            AreEqual(44,     () => audioFileOutputWav.GetAudioFileFormatEnum().HeaderLength());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishes"/>
        [TestMethod]
        public void RawExtensions_Test()
        {
            AudioFileOutput audioFileOutputRaw = null;
            Run(() => AsRaw().Sine().Save().AfterRecord(x => audioFileOutputRaw = x.UnderlyingAudioFileOutput));
            IsNotNull(() => audioFileOutputRaw);
            IsNotNull(() => audioFileOutputRaw.AudioFileFormat);
            AreEqual(".raw", () => audioFileOutputRaw.AudioFileFormat.FileExtension());
            AreEqual(".raw", () => audioFileOutputRaw.GetAudioFileFormatEnum().FileExtension());
            AreEqual(0,      () => audioFileOutputRaw.AudioFileFormat.HeaderLength());
            AreEqual(0,      () => audioFileOutputRaw.GetAudioFileFormatEnum().HeaderLength());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishes"/>
        [TestMethod]
        public void _16BitHelpers_Test()
        {
            AreEqual(SampleDataTypeEnum.Int16, () => 16.BitsToEnum());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishes"/>
        [TestMethod]
        public void _8BitHelpers_Test()
        {
            AreEqual(SampleDataTypeEnum.Byte, () => 8.BitsToEnum());
        }
 
        /// <inheritdoc cref="docs._testaudiopropertywishes"/>
        [TestMethod]
        public void ChannelCountToSpeakerSetup_Test()
        {
            AreEqual(SpeakerSetupEnum.Mono,   () => 1.ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Stereo, () => 2.ChannelsToEnum());
            
        }
    } 
}