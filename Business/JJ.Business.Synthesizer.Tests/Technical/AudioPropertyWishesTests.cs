using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.AudioPropertyWishes;
using static JJ.Framework.Testing.AssertHelper;
// ReSharper disable UnusedVariable
#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioPropertyWishesTests
    {
        // Bits

        [TestMethod]
        public void _8BitGetterTests()
        {
            // Arrange
            int bits = 8;
            var x = CreateEntities(bits);
            
            // Assert
            AreEqual(bits, () => x.SynthWishes.Bits());
            AreEqual(bits, () => x.FlowNode.Bits());
            AreEqual(bits, () => x.ConfigWishes.Bits());
            AreEqual(bits, () => x.ConfigSection.Bits());
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
            IsTrue(() => x.ConfigSection.Is8Bit());
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
            // Arrange
            
            int bits = 8;
            int differentBits = 16;
            
            var x = CreateEntities(bits);
            var with8Bit = CreateEntities(8);
            var with16Bit = CreateEntities(16);
            var with32Bit = CreateEntities(32);

            // Act
            {
                var y = CreateEntities(differentBits);

                AreEqual(y.SynthWishes,        () => y.SynthWishes       .Bits(bits));
                AreEqual(y.FlowNode,           () => y.FlowNode          .Bits(bits));
                AreEqual(y.ConfigWishes,       () => y.ConfigWishes      .Bits(bits));
                AreEqual(y.ConfigSection,      () => y.ConfigSection     .Bits(bits));
                AreEqual(y.Tape,               () => y.Tape              .Bits(bits));
                AreEqual(y.TapeConfig,         () => y.TapeConfig        .Bits(bits));
                AreEqual(y.TapeActions,        () => y.TapeActions       .Bits(bits));
                AreEqual(y.TapeAction,         () => y.TapeAction        .Bits(bits));
                AreEqual(y.Buff,               () => y.Buff              .Bits(bits, y.Context));
                AreEqual(y.Sample,             () => y.Sample            .Bits(bits, y.Context));
                AreEqual(y.AudioFileOutput,    () => y.AudioFileOutput   .Bits(bits, y.Context));
                AreEqual(y.AudioInfoWish,      () => y.AudioInfoWish     .Bits(bits));
                AreEqual(y.AudioFileInfo,      () => y.AudioFileInfo     .Bits(bits));

                NotEqual(y.WavHeader,          () => y.WavHeader         .Bits(bits));
                NotEqual(y.SampleDataTypeEnum, () => y.SampleDataTypeEnum.Bits(bits));
                NotEqual(y.SampleDataTypeEnum, () => y.SampleDataTypeEnum.Bits(bits));
                NotEqual(y.SampleDataType,     () => y.SampleDataType    .Bits(bits, y.Context));
                NotEqual(y.Type,               () => y.Type              .Bits(bits));
            }

            // Assert Conversion-Style
            {
                AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
                AreEqual(x.SampleDataType,     () => bits.BitsToEntity(x.Context));
                AreEqual(x.Type,               () => bits.BitsToType());
            }
            
            // Assert Shorthand
            AreEqual(with32Bit.SynthWishes,        () => with32Bit.SynthWishes       .With8Bit());
            AreEqual(with32Bit.FlowNode,           () => with32Bit.FlowNode          .With8Bit());
            AreEqual(with32Bit.ConfigWishes,       () => with32Bit.ConfigWishes      .With8Bit());
            AreEqual(with32Bit.ConfigSection,      () => with32Bit.ConfigSection     .With8Bit());
            AreEqual(with32Bit.Tape,               () => with32Bit.Tape              .With8Bit());
            AreEqual(with32Bit.TapeConfig,         () => with32Bit.TapeConfig        .With8Bit());
            AreEqual(with32Bit.TapeActions,        () => with32Bit.TapeActions       .With8Bit());
            AreEqual(with32Bit.TapeAction,         () => with32Bit.TapeAction        .With8Bit());
            AreEqual(with32Bit.Buff,               () => with32Bit.Buff              .With8Bit(with32Bit.Context));
            AreEqual(with32Bit.Sample,             () => with32Bit.Sample            .With8Bit(with32Bit.Context));
            AreEqual(with32Bit.AudioFileOutput,    () => with32Bit.AudioFileOutput   .With8Bit(with32Bit.Context));
            AreEqual(with32Bit.AudioInfoWish,      () => with32Bit.AudioInfoWish     .With8Bit());
            AreEqual(with32Bit.AudioFileInfo,      () => with32Bit.AudioFileInfo     .With8Bit());

            NotEqual(with32Bit.WavHeader,          () => with32Bit.WavHeader         .With8Bit());
            NotEqual(with32Bit.SampleDataTypeEnum, () => with32Bit.SampleDataTypeEnum.With8Bit());
            NotEqual(with32Bit.SampleDataType,     () => with32Bit.SampleDataType    .With8Bit(with32Bit.Context));
            NotEqual(with32Bit.Type,               () => with32Bit.Type              .With8Bit());

            AreEqual(typeof(byte), () => With8Bit<byte>());
        }
        
        // Helpers
        
        private class Entities
        {
            public SynthWishes           SynthWishes        { get; set; }
            public IContext              Context            { get; set; }
            public FlowNode              FlowNode           { get; set; }
            public ConfigWishes          ConfigWishes       { get; set; }
            public ConfigSectionAccessor ConfigSection      { get; set; }
            public Tape                  Tape               { get; set; }
            public TapeConfig            TapeConfig         { get; set; }
            public TapeActions           TapeActions        { get; set; }
            public TapeAction            TapeAction         { get; set; }
            public Buff                  Buff               { get; set; }
            public Sample                Sample             { get; set; }
            public AudioFileOutput       AudioFileOutput    { get; set; }
            public WavHeaderStruct       WavHeader          { get; set; }
            public AudioInfoWish         AudioInfoWish      { get; set; }
            public AudioFileInfo         AudioFileInfo      { get; set; }
            public Type                  Type               { get; set; }
            public SampleDataTypeEnum    SampleDataTypeEnum { get; set; }
            public SampleDataType        SampleDataType     { get; set; }
        }
        
        private Entities CreateEntities(int bits)
        {
            SynthWishes     synthWishes   = new SynthWishes().WithBits(bits);
            FlowNode        flowNode      = synthWishes.Value(123);
            Tape            tape          = CreateTape(synthWishes, flowNode);
            Sample          sample        = tape.UnderlyingSample;
            WavHeaderStruct wavHeader     = sample.ToWavHeader();
            AudioInfoWish   audioInfoWish = wavHeader.ToWish();

            ConfigSectionAccessor configSection = GetConfigSectionAccessor(synthWishes);
            configSection.Bits = bits;

            Type type;
            switch (bits)
            {
                case 8:  type = typeof(byte); break;
                case 16: type = typeof(Int16); break;
                case 32: type = typeof(float); break;
                default: throw new Exception($"{new { bits }} not supported.");
            }
            
            return new Entities
            {
                Context            = synthWishes.Context,
                SynthWishes        = synthWishes,
                FlowNode           = flowNode,
                ConfigWishes       = synthWishes.Config,
                ConfigSection      = configSection,
                Tape               = tape,
                TapeConfig         = tape.Config,
                TapeActions        = tape.Actions,
                TapeAction         = tape.Actions.AfterRecord,
                Buff               = tape.Buff,
                Sample             = sample,
                AudioFileOutput    = tape.UnderlyingAudioFileOutput,
                WavHeader          = wavHeader,
                AudioInfoWish      = audioInfoWish,
                AudioFileInfo      = audioInfoWish.FromWish(),
                Type               = type,
                SampleDataTypeEnum = sample.GetSampleDataTypeEnum(),
                SampleDataType     = sample.SampleDataType
            };
        }
        
        private Tape CreateTape(SynthWishes synthWishes, FlowNode flowNode)
        {
            Tape tape = null;
            synthWishes.RunOnThis(() => flowNode.AfterRecord(x => tape = x));
            IsNotNull(() => tape);
            return tape;
        }
                
        private ConfigSectionAccessor GetConfigSectionAccessor(SynthWishes synthWishes) => new ConfigWishesAccessor(synthWishes.Config)._section;

        // Old
        
        ///// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        //[TestMethod] 
        //public void MonoExtensions_Test()
        //{
        //    Tape tape = null;
        //    Run(() => WithMono().Sine().AfterRecord(x => tape = x));
        //    IsNotNull(() => tape);
        //    AudioFileOutput audioFileOutputMono = tape.UnderlyingAudioFileOutput;
        //    IsNotNull(() => audioFileOutputMono);
        //    IsNotNull(() => audioFileOutputMono.SpeakerSetup);
        //    AreEqual(SpeakerSetupEnum.Mono, () => audioFileOutputMono.SpeakerSetup.ToEnum());
        //}

        ///// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        //[TestMethod]
        //public void StereoExtensions_Test()
        //{
        //    Tape tape = null;
        //    Run(() => WithStereo().Sine().AfterRecord(x => tape = x).Save());
        //    IsNotNull(() => tape);
        //    AudioFileOutput audioFileOutputStereo = tape.UnderlyingAudioFileOutput;
        //    IsNotNull(() => audioFileOutputStereo);
        //    IsNotNull(() => audioFileOutputStereo.SpeakerSetup);
        //    AreEqual(SpeakerSetupEnum.Stereo, () => audioFileOutputStereo.SpeakerSetup.ToEnum());
        //}

        ///// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        //[TestMethod]
        //public void WavExtensions_Test()
        //{
        //    Tape tape = null;
        //    Run(() => AsWav().Sine().AfterRecord(x => tape = x).Save());
        //    IsNotNull(() => tape);
            
        //    AudioFileOutput audioFileOutputWav = tape.UnderlyingAudioFileOutput;
        //    IsNotNull(() => audioFileOutputWav);
        //    IsNotNull(() => audioFileOutputWav.AudioFileFormat);
        //    AreEqual(".wav", () => audioFileOutputWav.AudioFileFormat.FileExtension());
        //    AreEqual(".wav", () => audioFileOutputWav.GetAudioFileFormatEnum().FileExtension());
        //    AreEqual(44,     () => audioFileOutputWav.AudioFileFormat.HeaderLength());
        //    AreEqual(44,     () => audioFileOutputWav.GetAudioFileFormatEnum().HeaderLength());
        //}

        ///// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        //[TestMethod]
        //public void RawExtensions_Test()
        //{
        //    AudioFileOutput audioFileOutputRaw = null;
        //    Run(() => AsRaw().Sine().Save().AfterRecord(x => audioFileOutputRaw = x.UnderlyingAudioFileOutput));
        //    IsNotNull(() => audioFileOutputRaw);
        //    IsNotNull(() => audioFileOutputRaw.AudioFileFormat);
        //    AreEqual(".raw", () => audioFileOutputRaw.AudioFileFormat.FileExtension());
        //    AreEqual(".raw", () => audioFileOutputRaw.GetAudioFileFormatEnum().FileExtension());
        //    AreEqual(0,      () => audioFileOutputRaw.AudioFileFormat.HeaderLength());
        //    AreEqual(0,      () => audioFileOutputRaw.GetAudioFileFormatEnum().HeaderLength());
        //}

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