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

        [TestMethod] public void TestBitsGetters()
        {
            TestBitsGetters(8);
            TestBitsGetters(16);
            TestBitsGetters(32);
        }
        
        void TestBitsGetters(int bits)
        {
            var x = CreateEntities(bits);

            AreEqual(bits, () => x.SynthWishes.Bits());
            AreEqual(bits, () => x.FlowNode.Bits());
            AreEqual(bits, () => x.ConfigWishes.Bits());
            //AreEqual(bits, () => x.ConfigSection.Bits());
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
                    
            AreEqual(bits, () => x.SampleDataTypeEnum.EnumToBits());
            AreEqual(bits, () => x.SampleDataType.EntityToBits());
            AreEqual(bits, () => x.Type.TypeToBits());
        }
        
        [TestMethod] public void TestBitsGetters_8BitShorthand()
        {
            var x = CreateEntities(8);
            
            IsTrue(() => x.SynthWishes.Is8Bit());
            IsTrue(() => x.FlowNode.Is8Bit());
            IsTrue(() => x.ConfigWishes.Is8Bit());
            //IsTrue(() => x.ConfigSection.Is8Bit());
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
        }
        
        [TestMethod] public void TestBitsGetters_16BitShorthand()
        {
            var x = CreateEntities(16);
            
            IsTrue(() => x.SynthWishes.Is16Bit());
            IsTrue(() => x.FlowNode.Is16Bit());
            IsTrue(() => x.ConfigWishes.Is16Bit());
            //IsTrue(() => x.ConfigSection.Is16Bit());
            IsTrue(() => x.Tape.Is16Bit());
            IsTrue(() => x.TapeConfig.Is16Bit());
            IsTrue(() => x.TapeActions.Is16Bit());
            IsTrue(() => x.TapeAction.Is16Bit());
            IsTrue(() => x.Buff.Is16Bit());
            IsTrue(() => x.Sample.Is16Bit());
            IsTrue(() => x.AudioFileOutput.Is16Bit());
            IsTrue(() => x.WavHeader.Is16Bit());
            IsTrue(() => x.AudioInfoWish.Is16Bit());
            IsTrue(() => x.AudioFileInfo.Is16Bit());
            IsTrue(() => x.SampleDataTypeEnum.Is16Bit());
            IsTrue(() => x.SampleDataType.Is16Bit());
            IsTrue(() => x.Type.Is16Bit());
        }
                
        [TestMethod] public void TestBitsGetters_32BitShorthand()
        {
            var x = CreateEntities(32);
            
            IsTrue(() => x.SynthWishes.Is32Bit());
            IsTrue(() => x.FlowNode.Is32Bit());
            IsTrue(() => x.ConfigWishes.Is32Bit());
            //IsTrue(() => x.ConfigSection.Is32Bit());
            IsTrue(() => x.Tape.Is32Bit());
            IsTrue(() => x.TapeConfig.Is32Bit());
            IsTrue(() => x.TapeActions.Is32Bit());
            IsTrue(() => x.TapeAction.Is32Bit());
            IsTrue(() => x.Buff.Is32Bit());
            IsTrue(() => x.Sample.Is32Bit());
            IsTrue(() => x.AudioFileOutput.Is32Bit());
            IsTrue(() => x.WavHeader.Is32Bit());
            IsTrue(() => x.AudioInfoWish.Is32Bit());
            IsTrue(() => x.AudioFileInfo.Is32Bit());
            IsTrue(() => x.SampleDataTypeEnum.Is32Bit());
            IsTrue(() => x.SampleDataType.Is32Bit());
            IsTrue(() => x.Type.Is32Bit());
        }
                
        [TestMethod] public void TestBitsGetters_FromTypeArguments()
        {
            // Getters
            AreEqual(8, () => Bits<byte>());
            AreEqual(16, () => Bits<Int16>());
            AreEqual(32, () => Bits<float>());
        
            // Conversion-Style Getters
            AreEqual(8, () => TypeToBits<byte>());
            AreEqual(16, () => TypeToBits<Int16>());
            AreEqual(32, () => TypeToBits<float>());

            // Shorthand Getters            
            IsTrue(() => Is8Bit<byte>());
            IsTrue(() => Is16Bit<Int16>());
            IsTrue(() => Is32Bit<float>());
        }

        //[DoNotParallelize] 
        //[TestMethod]
        //public void TestBitsSetters_8Bit_1stAttempt()
        //{
        //    // TODO: Test more thoroughly, because one call can determine setting for another, making certain assertions ineffective.
            
        //    // Arrange
        //    int bits = 8;
        //    int differentBits = 16;
        //    var x = CreateEntities(bits);
        //    var with8Bit = CreateEntities(8);
        //    var with16Bit = CreateEntities(16);
        //    var with32Bit = CreateEntities(32);

        //    // Assert Setters
        //    {
        //        var y = CreateEntities(differentBits);
                
        //        // Global effect
        //        AreEqual(y.ConfigSection,      () => y.ConfigSection     .Bits(bits));

        //        // In tandem with SynthWishes
        //        AreEqual(y.SynthWishes,        () => y.SynthWishes       .Bits(bits));
        //        AreEqual(y.FlowNode,           () => y.FlowNode          .Bits(bits));
        //        AreEqual(y.ConfigWishes,       () => y.ConfigWishes      .Bits(bits));
                
        //        // In tandem with Tape
        //        AreEqual(y.Tape,               () => y.Tape              .Bits(bits));
        //        AreEqual(y.TapeConfig,         () => y.TapeConfig        .Bits(bits));
        //        AreEqual(y.TapeActions,        () => y.TapeActions       .Bits(bits));
        //        AreEqual(y.TapeAction,         () => y.TapeAction        .Bits(bits));
                
        //        // Independent after Taping
        //        AreEqual(y.Buff,               () => y.Buff              .Bits(bits, y.Context));
        //        AreEqual(y.Sample,             () => y.Sample            .Bits(bits, y.Context));
        //        AreEqual(y.AudioFileOutput,    () => y.AudioFileOutput   .Bits(bits, y.Context));
        //        AreEqual(y.AudioInfoWish,      () => y.AudioInfoWish     .Bits(bits));
        //        AreEqual(y.AudioFileInfo,      () => y.AudioFileInfo     .Bits(bits));

        //        // Stateless
        //        NotEqual(y.WavHeader,          () => y.WavHeader         .Bits(bits));
        //        NotEqual(y.SampleDataTypeEnum, () => y.SampleDataTypeEnum.Bits(bits));
        //        NotEqual(y.SampleDataType,     () => y.SampleDataType    .Bits(bits, y.Context));
        //        NotEqual(y.Type,               () => y.Type              .Bits(bits));
        //    }

        //    // Assert Conversion-Style Setters
        //    {
        //        AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
        //        AreEqual(x.SampleDataType,     () => bits.BitsToEntity(x.Context));
        //        AreEqual(x.Type,               () => bits.BitsToType());
        //    }

        //    // Assert Shorthand Setters
        //    {
        //        // Global effect
        //        AreEqual(with32Bit.ConfigSection,      () => with32Bit.ConfigSection     .With8Bit());

        //        // In tandem wit SynthWishes
        //        AreEqual(with32Bit.SynthWishes,        () => with32Bit.SynthWishes       .With8Bit());
        //        AreEqual(with32Bit.FlowNode,           () => with32Bit.FlowNode          .With8Bit());
        //        AreEqual(with32Bit.ConfigWishes,       () => with32Bit.ConfigWishes      .With8Bit());
                
        //        // In tandem with Tape
        //        AreEqual(with32Bit.Tape,               () => with32Bit.Tape              .With8Bit());
        //        AreEqual(with32Bit.TapeConfig,         () => with32Bit.TapeConfig        .With8Bit());
        //        AreEqual(with32Bit.TapeActions,        () => with32Bit.TapeActions       .With8Bit());
        //        AreEqual(with32Bit.TapeAction,         () => with32Bit.TapeAction        .With8Bit());
                
        //        // Independent after Taping
        //        AreEqual(with32Bit.Buff,               () => with32Bit.Buff              .With8Bit(with32Bit.Context));
        //        AreEqual(with32Bit.Sample,             () => with32Bit.Sample            .With8Bit(with32Bit.Context));
        //        AreEqual(with32Bit.AudioFileOutput,    () => with32Bit.AudioFileOutput   .With8Bit(with32Bit.Context));
        //        AreEqual(with32Bit.AudioInfoWish,      () => with32Bit.AudioInfoWish     .With8Bit());
        //        AreEqual(with32Bit.AudioFileInfo,      () => with32Bit.AudioFileInfo     .With8Bit());

        //        // Stateless
        //        NotEqual(with32Bit.WavHeader,          () => with32Bit.WavHeader         .With8Bit());
        //        NotEqual(with32Bit.SampleDataTypeEnum, () => with32Bit.SampleDataTypeEnum.With8Bit());
        //        NotEqual(with32Bit.SampleDataType,     () => with32Bit.SampleDataType    .With8Bit(with32Bit.Context));
        //        NotEqual(with32Bit.Type,               () => with32Bit.Type              .With8Bit());
                
        //        AreEqual(typeof(byte), () => With8Bit<byte>());
        //    }
            
        //    // Restore config section
        //    x.ConfigSection.Bits = ConfigWishes.DefaultBits;
        //}

        //[DoNotParallelize] 
        //[TestMethod]
        //public void TestBitsSetters_8Bit_2ndAttempt()
        //{
        //    // TODO: Test more thoroughly, because one call can determine setting for another, making certain assertions ineffective.
            
        //    // Arrange
        //    int bits = 8;
        //    int differentBits = 16;
        //    var x = CreateEntities(bits);
        //    var with8Bit = CreateEntities(8);
        //    var with16Bit = CreateEntities(16);
        //    var with32Bit = CreateEntities(32);
        //    Entities y;
            
        //    // Assert Setters
        //    {
        //        // Global effect
        //        {
        //            y = CreateEntities(differentBits);
                    
        //            // Global effect
        //            var configSection = y.ConfigSection.Bits(bits);
        //            AreSame(y.ConfigSection, () => configSection);
        //            IsNotNull(() => configSection.Bits);
        //            AreEqual(bits, () => configSection.Bits);
                
        //            //// In tandem with SynthWishes
        //            //AreEqual(bits, () => y.SynthWishes       .Bits());
        //            //AreEqual(bits, () => y.FlowNode          .Bits());
        //            //AreEqual(bits, () => y.ConfigWishes      .Bits());
                    
        //            //// In tandem with Tape
        //            //AreEqual(bits, () => y.Tape              .Bits());
        //            //AreEqual(bits, () => y.TapeConfig        .Bits());
        //            //AreEqual(bits, () => y.TapeActions       .Bits());
        //            //AreEqual(bits, () => y.TapeAction        .Bits());
                    
        //            //// Independent after Taping
        //            //AreEqual(bits, () => y.Buff              .Bits());
        //            //AreEqual(bits, () => y.Sample            .Bits());
        //            //AreEqual(bits, () => y.AudioFileOutput   .Bits());
        //            //AreEqual(bits, () => y.AudioInfoWish     .Bits());
        //            //AreEqual(bits, () => y.AudioFileInfo     .Bits());

        //            //// Stateless
        //            //NotEqual(bits, () => y.WavHeader         .Bits());
        //            //NotEqual(bits, () => y.SampleDataTypeEnum.Bits());
        //            //NotEqual(bits, () => y.SampleDataType    .Bits());
        //            //NotEqual(bits, () => y.Type              .Bits());
        //        }
                
        //        y = CreateEntities(differentBits);

        //        // In tandem with SynthWishes
        //        AreEqual(y.SynthWishes,        () => y.SynthWishes       .Bits(bits));
        //        AreEqual(y.FlowNode,           () => y.FlowNode          .Bits(bits));
        //        AreEqual(y.ConfigWishes,       () => y.ConfigWishes      .Bits(bits));
                
        //        // In tandem with Tape
        //        AreEqual(y.Tape,               () => y.Tape              .Bits(bits));
        //        AreEqual(y.TapeConfig,         () => y.TapeConfig        .Bits(bits));
        //        AreEqual(y.TapeActions,        () => y.TapeActions       .Bits(bits));
        //        AreEqual(y.TapeAction,         () => y.TapeAction        .Bits(bits));
                
        //        // Independent after Taping
        //        AreEqual(y.Buff,               () => y.Buff              .Bits(bits, y.Context));
        //        AreEqual(y.Sample,             () => y.Sample            .Bits(bits, y.Context));
        //        AreEqual(y.AudioFileOutput,    () => y.AudioFileOutput   .Bits(bits, y.Context));
        //        AreEqual(y.AudioInfoWish,      () => y.AudioInfoWish     .Bits(bits));
        //        AreEqual(y.AudioFileInfo,      () => y.AudioFileInfo     .Bits(bits));

        //        // Stateless
        //        NotEqual(y.WavHeader,          () => y.WavHeader         .Bits(bits));
        //        NotEqual(y.SampleDataTypeEnum, () => y.SampleDataTypeEnum.Bits(bits));
        //        NotEqual(y.SampleDataType,     () => y.SampleDataType    .Bits(bits, y.Context));
        //        NotEqual(y.Type,               () => y.Type              .Bits(bits));
        //    }

        //    // Assert Conversion-Style Setters
        //    {
        //        AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
        //        AreEqual(x.SampleDataType,     () => bits.BitsToEntity(x.Context));
        //        AreEqual(x.Type,               () => bits.BitsToType());
        //    }

        //    // Assert Shorthand Setters
        //    {
        //        // Global effect
        //        AreEqual(with32Bit.ConfigSection,      () => with32Bit.ConfigSection     .With8Bit());

        //        // In tandem wit SynthWishes
        //        AreEqual(with32Bit.SynthWishes,        () => with32Bit.SynthWishes       .With8Bit());
        //        AreEqual(with32Bit.FlowNode,           () => with32Bit.FlowNode          .With8Bit());
        //        AreEqual(with32Bit.ConfigWishes,       () => with32Bit.ConfigWishes      .With8Bit());
                
        //        // In tandem with Tape
        //        AreEqual(with32Bit.Tape,               () => with32Bit.Tape              .With8Bit());
        //        AreEqual(with32Bit.TapeConfig,         () => with32Bit.TapeConfig        .With8Bit());
        //        AreEqual(with32Bit.TapeActions,        () => with32Bit.TapeActions       .With8Bit());
        //        AreEqual(with32Bit.TapeAction,         () => with32Bit.TapeAction        .With8Bit());
                
        //        // Independent after Taping
        //        AreEqual(with32Bit.Buff,               () => with32Bit.Buff              .With8Bit(with32Bit.Context));
        //        AreEqual(with32Bit.Sample,             () => with32Bit.Sample            .With8Bit(with32Bit.Context));
        //        AreEqual(with32Bit.AudioFileOutput,    () => with32Bit.AudioFileOutput   .With8Bit(with32Bit.Context));
        //        AreEqual(with32Bit.AudioInfoWish,      () => with32Bit.AudioInfoWish     .With8Bit());
        //        AreEqual(with32Bit.AudioFileInfo,      () => with32Bit.AudioFileInfo     .With8Bit());

        //        // Stateless
        //        NotEqual(with32Bit.WavHeader,          () => with32Bit.WavHeader         .With8Bit());
        //        NotEqual(with32Bit.SampleDataTypeEnum, () => with32Bit.SampleDataTypeEnum.With8Bit());
        //        NotEqual(with32Bit.SampleDataType,     () => with32Bit.SampleDataType    .With8Bit(with32Bit.Context));
        //        NotEqual(with32Bit.Type,               () => with32Bit.Type              .With8Bit());
                
        //        AreEqual(typeof(byte), () => With8Bit<byte>());
        //    }
            
        //    // Restore config section
        //    x.ConfigSection.Bits = ConfigWishes.DefaultBits;
        //}

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
            => CreateEntities_ModifySynthWishes_AndConfigSection(bits);
        
        //private Entities CreateEntities_WithModifiedConfigSection(int bits)
        //{
        //}

        private Entities CreateEntities_ModifySynthWishes_AndConfigSection(int bits)
        {
            // SynthWishes-Bound
            SynthWishes     synthWishes   = new SynthWishes().WithBits(bits);
            FlowNode        flowNode      = synthWishes.Value(123);
            
            // Tape-Bound
            Tape            tape          = CreateTape(synthWishes, flowNode);
            Sample          sample        = tape.UnderlyingSample;
            WavHeaderStruct wavHeader     = sample.ToWavHeader();
            AudioInfoWish   audioInfoWish = wavHeader.ToWish();
            
            // Global-Bound
            ConfigSectionAccessor configSection = GetConfigSectionAccessor(synthWishes);
            //configSection.Bits = bits;

            // Stateless
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
                // Global-Bound
                ConfigSection      = configSection,
                // SynthWishes-Bound
                Context            = synthWishes.Context,
                SynthWishes        = synthWishes,
                FlowNode           = flowNode,
                ConfigWishes       = synthWishes.Config,
                // Tape-Bound
                Tape               = tape,
                TapeConfig         = tape.Config,
                TapeActions        = tape.Actions,
                TapeAction         = tape.Actions.AfterRecord,
                // Independent after Taping
                Buff               = tape.Buff,
                Sample             = sample,
                AudioFileOutput    = tape.UnderlyingAudioFileOutput,
                WavHeader          = wavHeader,
                AudioInfoWish      = audioInfoWish,
                AudioFileInfo      = audioInfoWish.FromWish(),
                // Stateless
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
                
        private ConfigSectionAccessor GetConfigSectionAccessor(SynthWishes synthWishes) 
            => new ConfigWishesAccessor(synthWishes.Config)._section;

        // Old
 
        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void ChannelCountToSpeakerSetup_Test()
        {
            AreEqual(SpeakerSetupEnum.Mono,   () => 1.ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Stereo, () => 2.ChannelsToEnum());
        }
    } 
}