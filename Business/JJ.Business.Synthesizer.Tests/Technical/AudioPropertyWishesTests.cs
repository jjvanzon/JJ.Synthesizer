using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
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
            var x = CreateEntities();
            x.ConfigSectionAccessor.Bits = bits;
            
            // Assert
            AreEqual(bits, () => x.SynthWishes.Bits());
            AreEqual(bits, () => x.FlowNode.Bits());
            AreEqual(bits, () => x.ConfigWishes.Bits());
            AreEqual(bits, () => x.ConfigSectionAccessor.Bits());
            AreEqual(bits, () => x.Tape.Bits());
            AreEqual(bits, () => x.TapeConfig.Bits());
            AreEqual(bits, () => x.TapeActions.Bits());
            AreEqual(bits, () => x.TapeAction.Bits());
            AreEqual(bits, () => x.Buff.Bits());
            AreEqual(bits, () => x.Sample.Bits());
            AreEqual(bits, () => x.AudioFileOutput.Bits());
            AreEqual(bits, () => x.WavHeader.Bits());
            AreEqual(bits, () => x.AudioInfoWish.Bits());
            AreEqual(bits, () => x.AudioFileInfo.Bits());
            AreEqual(bits, () => x.SampleDataTypeEnum.Bits());
            AreEqual(bits, () => x.SampleDataType.Bits());
            AreEqual(bits, () => x.Type.Bits());
            AreEqual(bits, () => Bits<byte>());
            
            // Assert Conversion-Style
            AreEqual(bits, () => x.SampleDataTypeEnum.EnumToBits());
            AreEqual(bits, () => x.SampleDataType.EntityToBits());
            AreEqual(bits, () => x.Type.TypeToBits());
            AreEqual(bits, () => TypeToBits<byte>());
            
            // Assert Shorthand
            IsTrue(() => x.SynthWishes.Is8Bit());
            IsTrue(() => x.FlowNode.Is8Bit());
            IsTrue(() => x.ConfigWishes.Is8Bit());
            IsTrue(() => x.ConfigSectionAccessor.Is8Bit());
            IsTrue(() => x.Tape.Is8Bit());
            IsTrue(() => x.TapeConfig.Is8Bit());
            IsTrue(() => x.TapeActions.Is8Bit());
            IsTrue(() => x.TapeAction.Is8Bit());
            IsTrue(() => x.Buff.Is8Bit());
            IsTrue(() => x.Sample.Is8Bit());
            IsTrue(() => x.AudioFileOutput.Is8Bit());
            IsTrue(() => x.WavHeader.Is8Bit());
            IsTrue(() => x.AudioInfoWish.Is8Bit());
            IsTrue(() => x.AudioFileInfo.Is8Bit());
            IsTrue(() => x.SampleDataTypeEnum.Is8Bit());
            IsTrue(() => x.SampleDataType.Is8Bit());
            IsTrue(() => x.Type.Is8Bit());
            IsTrue(() => Is8Bit<byte>());
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
        
        private class Entities
        {
            public SynthWishes           SynthWishes           { get; set; }
            public FlowNode              FlowNode              { get; set; }
            public ConfigWishes          ConfigWishes          { get; set; }
            public ConfigSectionAccessor ConfigSectionAccessor { get; set; }
            public Tape                  Tape                  { get; set; }
            public TapeConfig            TapeConfig            { get; set; }
            public TapeActions           TapeActions           { get; set; }
            public TapeAction            TapeAction            { get; set; }
            public Buff                  Buff                  { get; set; }
            public Sample                Sample                { get; set; }
            public AudioFileOutput       AudioFileOutput       { get; set; }
            public WavHeaderStruct       WavHeader             { get; set; }
            public AudioInfoWish         AudioInfoWish         { get; set; }
            public AudioFileInfo         AudioFileInfo         { get; set; }
            public Type                  Type                  { get; set; }
            public SampleDataTypeEnum    SampleDataTypeEnum    { get; set; }
            public SampleDataType        SampleDataType        { get; set; }
        }
        
        private Entities CreateEntities()
        {
            SynthWishes           synthWishes           = this;
            FlowNode              flowNode              = _[3];
            ConfigWishes          configWishes          = Config;
            ConfigSectionAccessor configSectionAccessor = GetConfigSectionAccessor();
            Tape                  tape                  = CreateTape(flowNode);
            TapeConfig            tapeConfig            = tape.Config;
            TapeActions           tapeActions           = tape.Actions;
            TapeAction            tapeAction            = tape.Actions.AfterRecord;
            Buff                  buff                  = tape.Buff;
            Sample                sample                = tape.UnderlyingSample;
            AudioFileOutput       audioFileOutput       = tape.UnderlyingAudioFileOutput;
            WavHeaderStruct       wavHeader             = sample.ToWavHeader();
            AudioInfoWish         audioInfoWish         = wavHeader.ToWish();
            AudioFileInfo         audioFileInfo         = audioInfoWish.FromWish();
            Type                  type                  = typeof(byte);
            SampleDataTypeEnum    sampleDataTypeEnum    = sample.GetSampleDataTypeEnum();
            SampleDataType        sampleDataType        = sample.SampleDataType;
            
            return new Entities
            {
                SynthWishes           = synthWishes,
                FlowNode              = flowNode,
                ConfigWishes          = configWishes,
                ConfigSectionAccessor = configSectionAccessor,
                Tape                  = tape,
                TapeConfig            = tapeConfig,
                TapeActions           = tapeActions,
                TapeAction            = tapeAction,
                Buff                  = buff,
                Sample                = sample,
                AudioFileOutput       = audioFileOutput,
                WavHeader             = wavHeader,
                AudioInfoWish         = audioInfoWish,
                AudioFileInfo         = audioFileInfo,
                Type                  = type,
                SampleDataTypeEnum    = sampleDataTypeEnum,
                SampleDataType        = sampleDataType
            };
        }
        
        private Tape CreateTape(FlowNode flowNode)
        {
            Tape tape = null;
            Run(() => flowNode.AfterRecord(x => tape = x));
            IsNotNull(() => tape);
            return tape;
        }
                
        private ConfigSectionAccessor GetConfigSectionAccessor() 
            => new ConfigWishesAccessor(Config)._section;

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