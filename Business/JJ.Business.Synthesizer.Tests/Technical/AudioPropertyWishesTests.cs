using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.AudioPropertyWishes;
using static JJ.Framework.Testing.AssertHelper;
#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioPropertyWishesTests : SynthWishes
    {
        // Bits

        [TestMethod] 
        public void _8BitGetterTests()
        {
            // Arrange
            int bits = 8;
            
            WithBits(bits);
            
            SynthWishes synthWishes = this;
            FlowNode flowNode = _[3];
            ConfigWishes configWishes = Config;
            Tape tape = GetTape(flowNode);
            TapeConfig tapeConfig = tape.Config;
            TapeActions tapeActions = tape.Actions;
            TapeAction tapeAction = tape.Actions.AfterRecord;
            Buff buff = tape.Buff;
            Sample sample = tape.UnderlyingSample;
            AudioFileOutput audioFileOutput = tape.UnderlyingAudioFileOutput;
            WavHeaderStruct wavHeader = sample.ToWavHeader();
            AudioInfoWish audioInfoWish = wavHeader.ToWish();
            AudioFileInfo audioFileInfo = audioInfoWish.FromWish();
            Type type = typeof(byte);
            SampleDataTypeEnum sampleDataTypeEnum = sample.GetSampleDataTypeEnum();
            SampleDataType sampleDataType = sample.SampleDataType;

            // Assert
            AreEqual(bits, () => synthWishes.Bits());
            AreEqual(bits, () => flowNode.Bits());
            AreEqual(bits, () => configWishes.Bits());
            AreEqual(bits, () => tape.Bits());
            AreEqual(bits, () => tapeConfig.Bits());
            AreEqual(bits, () => tapeActions.Bits());
            AreEqual(bits, () => tapeAction.Bits());
            AreEqual(bits, () => buff.Bits());
            AreEqual(bits, () => sample.Bits());
            AreEqual(bits, () => audioFileOutput.Bits());
            AreEqual(bits, () => wavHeader.Bits());
            AreEqual(bits, () => audioInfoWish.Bits());
            AreEqual(bits, () => audioFileInfo.Bits());
            AreEqual(bits, () => sampleDataTypeEnum.Bits());
            AreEqual(bits, () => sampleDataType.Bits());
            AreEqual(bits, () => type.Bits());
            AreEqual(bits, () => Bits<byte>());
            
            // Assert Conversion-Style
            AreEqual(bits, () => sampleDataTypeEnum.EnumToBits());
            AreEqual(bits, () => sampleDataType.EntityToBits());
            AreEqual(bits, () => type.TypeToBits());
            AreEqual(bits, () => TypeToBits<byte>());
            
            // Assert Shorthand
            IsTrue(() => synthWishes.Is8Bit());
            IsTrue(() => flowNode.Is8Bit());
            IsTrue(() => configWishes.Is8Bit());
            IsTrue(() => tape.Is8Bit());
            IsTrue(() => tapeConfig.Is8Bit());
            IsTrue(() => tapeActions.Is8Bit());
            IsTrue(() => tapeAction.Is8Bit());
            IsTrue(() => buff.Is8Bit());
            IsTrue(() => sample.Is8Bit());
            IsTrue(() => audioFileOutput.Is8Bit());
            IsTrue(() => wavHeader.Is8Bit());
            IsTrue(() => audioInfoWish.Is8Bit());
            IsTrue(() => audioFileInfo.Is8Bit());
            IsTrue(() => sampleDataTypeEnum.Is8Bit());
            IsTrue(() => sampleDataType.Is8Bit());
            IsTrue(() => type.Is8Bit());
            IsTrue(() => Is8Bit<byte>());
            
            // TODO: ConfigSection
        }
        
        [TestMethod]
        public void _8BitSetterTests()
        {
            // TODO: Write out test.
            int bits = 8;
            Type type = typeof(byte);
            AreEqual(type, () => bits.BitsToType());
        }
        
        // Helpers
        
        private Tape GetTape(FlowNode flowNode)
        {
            Tape tape = null;
            Run(() => flowNode.AfterRecord(x => tape = x));
            IsNotNull(() => tape);
            return tape;
        }
        
        // Old
        
        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
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

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
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

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
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

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
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

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void _16BitHelpers_Test()
        {
            AreEqual(SampleDataTypeEnum.Int16, () => 16.BitsToEnum());
        }

        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void _8BitHelpers_Test()
        {
            AreEqual(SampleDataTypeEnum.Byte, () => 8.BitsToEnum());
        }
 
        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void ChannelCountToSpeakerSetup_Test()
        {
            AreEqual(SpeakerSetupEnum.Mono,   () => 1.ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Stereo, () => 2.ChannelsToEnum());
            
        }
    } 
}