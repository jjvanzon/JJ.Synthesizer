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
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
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
            var x = new TestEntities(s => s.WithBits(bits));

            // Global-Bound
            AreEqual(DefaultBits, () => x.ConfigSection.Bits());
            
            // SynthWishes-Bound
            AreEqual(bits, () => x.SynthWishes.Bits());
            AreEqual(bits, () => x.FlowNode.Bits());
            AreEqual(bits, () => x.ConfigWishes.Bits());
            
            // Tape-Bound
            AreEqual(bits, () => x.Tape.Bits());
            AreEqual(bits, () => x.TapeConfig.Bits());
            AreEqual(bits, () => x.TapeActions.Bits());
            AreEqual(bits, () => x.TapeAction.Bits());

            // Buff-Bound
            AreEqual(bits, () => x.Buff.Bits());
            AreEqual(bits, () => x.AudioFileOutput.Bits());
            
            // Independent after Taping
            AreEqual(bits, () => x.Sample.Bits());
            AreEqual(bits, () => x.WavHeader.Bits());
            AreEqual(bits, () => x.AudioInfoWish.Bits());
            AreEqual(bits, () => x.AudioFileInfo.Bits());
            
            // Immutable
            AreEqual(bits, () => x.SampleDataTypeEnum.Bits());
            AreEqual(bits, () => x.SampleDataType.Bits());
            AreEqual(bits, () => x.Type.Bits());

            // Converter-Style Getters
            AreEqual(bits, () => x.SampleDataTypeEnum.EnumToBits());
            AreEqual(bits, () => x.SampleDataType.EntityToBits());
            AreEqual(bits, () => x.Type.TypeToBits());
        }
        
        [TestMethod] public void TestBitsGetters_8BitShorthand()
        {
            var x = new TestEntities(s => s.With8Bit());

            // Global-Bound
            IsTrue(() => x.ConfigSection.Is32Bit());

            // SynthWishes-Bound
            IsTrue(() => x.SynthWishes.Is8Bit());
            IsTrue(() => x.FlowNode.Is8Bit());
            IsTrue(() => x.ConfigWishes.Is8Bit());

            // Tape-Bound
            IsTrue(() => x.Tape.Is8Bit());
            IsTrue(() => x.TapeConfig.Is8Bit());
            IsTrue(() => x.TapeActions.Is8Bit());
            IsTrue(() => x.TapeAction.Is8Bit());
            
            // Buff-Bound
            IsTrue(() => x.Buff.Is8Bit());
            IsTrue(() => x.AudioFileOutput.Is8Bit());
            
            // Independent after Taping
            IsTrue(() => x.Sample.Is8Bit());
            IsTrue(() => x.WavHeader.Is8Bit());
            IsTrue(() => x.AudioInfoWish.Is8Bit());
            IsTrue(() => x.AudioFileInfo.Is8Bit());
            
            // Immutable
            IsTrue(() => x.SampleDataTypeEnum.Is8Bit());
            IsTrue(() => x.SampleDataType.Is8Bit());
            IsTrue(() => x.Type.Is8Bit());
        }
        
        [TestMethod] public void TestBitsGetters_16BitShorthand()
        {
            var x = new TestEntities(s => s.With16Bit());
            
            // Global-Bound
            IsTrue(() => x.ConfigSection.Is32Bit());

            // SynthWishes-Bound
            IsTrue(() => x.SynthWishes.Is16Bit());
            IsTrue(() => x.FlowNode.Is16Bit());
            IsTrue(() => x.ConfigWishes.Is16Bit());

            // Tape-Bound
            IsTrue(() => x.Tape.Is16Bit());
            IsTrue(() => x.TapeConfig.Is16Bit());
            IsTrue(() => x.TapeActions.Is16Bit());
            IsTrue(() => x.TapeAction.Is16Bit());

            // Buff-Bound
            IsTrue(() => x.Buff.Is16Bit());
            IsTrue(() => x.AudioFileOutput.Is16Bit());

            // Independent after Taping
            IsTrue(() => x.Sample.Is16Bit());
            IsTrue(() => x.WavHeader.Is16Bit());
            IsTrue(() => x.AudioInfoWish.Is16Bit());
            IsTrue(() => x.AudioFileInfo.Is16Bit());

            // Immutable
            IsTrue(() => x.SampleDataTypeEnum.Is16Bit());
            IsTrue(() => x.SampleDataType.Is16Bit());
            IsTrue(() => x.Type.Is16Bit());
        }
                
        [TestMethod] public void TestBitsGetters_32BitShorthand()
        {
            var x = new TestEntities(y => y.With32Bit());
            
            // Global-Bound
            IsTrue(() => x.ConfigSection.Is32Bit());

            // SynthWishes-Bound
            IsTrue(() => x.SynthWishes.Is32Bit());
            IsTrue(() => x.FlowNode.Is32Bit());
            IsTrue(() => x.ConfigWishes.Is32Bit());

            // Tape-Bound
            IsTrue(() => x.Tape.Is32Bit());
            IsTrue(() => x.TapeConfig.Is32Bit());
            IsTrue(() => x.TapeActions.Is32Bit());
            IsTrue(() => x.TapeAction.Is32Bit());

            // Buff-Bound
            IsTrue(() => x.Buff.Is32Bit());
            IsTrue(() => x.AudioFileOutput.Is32Bit());

            // Independent after Taping
            IsTrue(() => x.Sample.Is32Bit());
            IsTrue(() => x.WavHeader.Is32Bit());
            IsTrue(() => x.AudioInfoWish.Is32Bit());
            IsTrue(() => x.AudioFileInfo.Is32Bit());

            // Immutable
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

        [TestMethod]
        public void TestBitsSetters_8Bit_Shallow()
        {
            // Arrange
            int bits = 8;
            int differentBits = 16;
            
            var x = new TestEntities(s => s.WithBits(bits));
            var with8Bit  = new TestEntities(s => s.With8Bit());
            var with16Bit = new TestEntities(s => s.With16Bit());
            var with32Bit = new TestEntities(s => s.With32Bit());

            // Assert Setters
            {
                var y = new TestEntities(s => s.WithBits(differentBits));

                // Global level: ConfigSection is immutable

                // SynthWishes-Bound
                AreEqual(y.SynthWishes, () => y.SynthWishes.Bits(bits));
                AreEqual(y.FlowNode, () => y.FlowNode.Bits(bits));
                AreEqual(y.ConfigWishes, () => y.ConfigWishes.Bits(bits));

                // Tape-Bound
                AreEqual(y.Tape, () => y.Tape.Bits(bits));
                AreEqual(y.TapeConfig, () => y.TapeConfig.Bits(bits));
                AreEqual(y.TapeActions, () => y.TapeActions.Bits(bits));
                AreEqual(y.TapeAction, () => y.TapeAction.Bits(bits));

                // Buff-Bound
                AreEqual(y.Buff, () => y.Buff.Bits(bits, y.Context));
                AreEqual(y.AudioFileOutput, () => y.AudioFileOutput.Bits(bits, y.Context));

                // Independent after Taping
                AreEqual(y.Sample, () => y.Sample.Bits(bits, y.Context));
                AreEqual(y.AudioInfoWish, () => y.AudioInfoWish.Bits(bits));
                AreEqual(y.AudioFileInfo, () => y.AudioFileInfo.Bits(bits));

                // Immutable
                NotEqual(y.WavHeader, () => y.WavHeader.Bits(bits));
                NotEqual(y.SampleDataTypeEnum, () => y.SampleDataTypeEnum.Bits(bits));
                NotEqual(y.SampleDataType, () => y.SampleDataType.Bits(bits, y.Context));
                NotEqual(y.Type, () => y.Type.Bits(bits));
            }

            // Assert Conversion-Style Setters
            {
                AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
                AreEqual(x.SampleDataType, () => bits.BitsToEntity(x.Context));
                AreEqual(x.Type, () => bits.BitsToType());
            }

            // Assert Shorthand Setters
            {
                // Global level: ConfigSection is immutable

                // SynthWishes-Bound
                AreEqual(with32Bit.SynthWishes, () => with32Bit.SynthWishes.With8Bit());
                AreEqual(with32Bit.FlowNode, () => with32Bit.FlowNode.With8Bit());
                AreEqual(with32Bit.ConfigWishes, () => with32Bit.ConfigWishes.With8Bit());

                // Tape-Bound
                AreEqual(with32Bit.Tape, () => with32Bit.Tape.With8Bit());
                AreEqual(with32Bit.TapeConfig, () => with32Bit.TapeConfig.With8Bit());
                AreEqual(with32Bit.TapeActions, () => with32Bit.TapeActions.With8Bit());
                AreEqual(with32Bit.TapeAction, () => with32Bit.TapeAction.With8Bit());

                // Buff-Bound
                AreEqual(with32Bit.Buff, () => with32Bit.Buff.With8Bit(with32Bit.Context));
                AreEqual(with32Bit.AudioFileOutput, () => with32Bit.AudioFileOutput.With8Bit(with32Bit.Context));

                // Independent after Taping
                AreEqual(with32Bit.Sample, () => with32Bit.Sample.With8Bit(with32Bit.Context));
                AreEqual(with32Bit.AudioInfoWish, () => with32Bit.AudioInfoWish.With8Bit());
                AreEqual(with32Bit.AudioFileInfo, () => with32Bit.AudioFileInfo.With8Bit());

                // Immutable
                NotEqual(with32Bit.WavHeader, () => with32Bit.WavHeader.With8Bit());
                NotEqual(with32Bit.SampleDataTypeEnum, () => with32Bit.SampleDataTypeEnum.With8Bit());
                NotEqual(with32Bit.SampleDataType, () => with32Bit.SampleDataType.With8Bit(with32Bit.Context));
                NotEqual(with32Bit.Type, () => with32Bit.Type.With8Bit());

                AreEqual(typeof(byte), () => With8Bit<byte>());
            }
        }

        [TestMethod]
        public void TestBitsSetters_8Bit_Thorough()
        {
            // TODO: Test more thoroughly, because one call can determine setting for another, making certain assertions ineffective.

            // Arrange
            int bits = 8;
            int differentBits = 16;
            
            var x         = new TestEntities(s => s.WithBits(bits));
            var with8Bit  = new TestEntities(s => s.With8Bit());
            var with16Bit = new TestEntities(s => s.With16Bit());
            var with32Bit = new TestEntities(s => s.With32Bit());

            TestEntities y;

            // Assert Setters
            {
                // SynthWishes-Bound Mutations
                {
                    y = new TestEntities(s => s.WithBits(differentBits));
                    
                    // Global level: ConfigSection is immutable

                    // SynthWishes-Bound
                    y.SynthWishes.Bits(bits);
                    
                    AreEqual(bits, () => y.SynthWishes.Bits());
                    AreEqual(bits, () => y.FlowNode.Bits());
                    AreEqual(bits, () => y.ConfigWishes.Bits());

                    y.Record();
                    
                    // Tape-Bound
                    AreEqual(bits, () => y.Tape.Bits());
                    AreEqual(bits, () => y.TapeConfig.Bits());
                    AreEqual(bits, () => y.TapeActions.Bits());
                    AreEqual(bits, () => y.TapeAction.Bits());

                    // Buff-Bound
                    AreEqual(bits, () => y.Buff.Bits());
                    AreEqual(bits, () => y.AudioFileOutput.Bits());

                    // Independent after Taping
                    AreEqual(bits, () => y.Sample.Bits());
                    AreEqual(bits, () => y.AudioInfoWish.Bits());
                    AreEqual(bits, () => y.AudioFileInfo.Bits());

                    // Immutable
                    AreEqual(bits, () => y.WavHeader.Bits());
                    AreEqual(bits, () => y.SampleDataTypeEnum.Bits());
                    AreEqual(bits, () => y.SampleDataType.Bits());
                    AreEqual(bits, () => y.Type.Bits());
                }

                y = new TestEntities(s => s.WithBits(differentBits));

                // Global level: ConfigSection is immutable
                
                // SynthWishes-Bound
                AreEqual(y.SynthWishes, () => y.SynthWishes.Bits(bits));
                AreEqual(y.FlowNode, () => y.FlowNode.Bits(bits));
                AreEqual(y.ConfigWishes, () => y.ConfigWishes.Bits(bits));

                // Tape-Bound
                AreEqual(y.Tape, () => y.Tape.Bits(bits));
                AreEqual(y.TapeConfig, () => y.TapeConfig.Bits(bits));
                AreEqual(y.TapeActions, () => y.TapeActions.Bits(bits));
                AreEqual(y.TapeAction, () => y.TapeAction.Bits(bits));

                // Buff-Bound
                AreEqual(y.Buff, () => y.Buff.Bits(bits, y.Context));
                AreEqual(y.AudioFileOutput, () => y.AudioFileOutput.Bits(bits, y.Context));

                // Independent after Taping
                AreEqual(y.Sample, () => y.Sample.Bits(bits, y.Context));
                AreEqual(y.AudioInfoWish, () => y.AudioInfoWish.Bits(bits));
                AreEqual(y.AudioFileInfo, () => y.AudioFileInfo.Bits(bits));

                // Immutable
                NotEqual(y.WavHeader, () => y.WavHeader.Bits(bits));
                NotEqual(y.SampleDataTypeEnum, () => y.SampleDataTypeEnum.Bits(bits));
                NotEqual(y.SampleDataType, () => y.SampleDataType.Bits(bits, y.Context));
                NotEqual(y.Type, () => y.Type.Bits(bits));
            }

            // Assert Conversion-Style Setters
            {
                // Immutable
                AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
                AreEqual(x.SampleDataType, () => bits.BitsToEntity(x.Context));
                AreEqual(x.Type, () => bits.BitsToType());
            }

            // Assert Shorthand Setters
            {
                // Global level: ConfigSection is immutable

                // SynthWishes-Bound
                AreEqual(with32Bit.SynthWishes, () => with32Bit.SynthWishes.With8Bit());
                AreEqual(with32Bit.FlowNode, () => with32Bit.FlowNode.With8Bit());
                AreEqual(with32Bit.ConfigWishes, () => with32Bit.ConfigWishes.With8Bit());

                // Tape-Bound
                AreEqual(with32Bit.Tape, () => with32Bit.Tape.With8Bit());
                AreEqual(with32Bit.TapeConfig, () => with32Bit.TapeConfig.With8Bit());
                AreEqual(with32Bit.TapeActions, () => with32Bit.TapeActions.With8Bit());
                AreEqual(with32Bit.TapeAction, () => with32Bit.TapeAction.With8Bit());

                // Buff-Bound
                AreEqual(with32Bit.Buff, () => with32Bit.Buff.With8Bit(with32Bit.Context));
                AreEqual(with32Bit.AudioFileOutput, () => with32Bit.AudioFileOutput.With8Bit(with32Bit.Context));

                // Independent after Taping
                AreEqual(with32Bit.Sample, () => with32Bit.Sample.With8Bit(with32Bit.Context));
                AreEqual(with32Bit.AudioInfoWish, () => with32Bit.AudioInfoWish.With8Bit());
                AreEqual(with32Bit.AudioFileInfo, () => with32Bit.AudioFileInfo.With8Bit());

                // Immutable
                NotEqual(with32Bit.WavHeader, () => with32Bit.WavHeader.With8Bit());
                NotEqual(with32Bit.SampleDataTypeEnum, () => with32Bit.SampleDataTypeEnum.With8Bit());
                NotEqual(with32Bit.SampleDataType, () => with32Bit.SampleDataType.With8Bit(with32Bit.Context));
                NotEqual(with32Bit.Type, () => with32Bit.Type.With8Bit());

                AreEqual(typeof(byte), () => With8Bit<byte>());
            }
        }

        // Helpers

        private class TestEntities
        {
            // Global-Bound
            public ConfigSectionAccessor ConfigSection      { get; }

            // SynthWishes-Bound
            public SynthWishes           SynthWishes        { get; }
            public IContext              Context            { get; }
            public FlowNode              FlowNode           { get; }
            public ConfigWishes          ConfigWishes       { get; }

            // Tape-Bound
            public Tape                  Tape               { get; private set; }
            public TapeConfig            TapeConfig         { get; private set; }
            public TapeActions           TapeActions        { get; private set; }
            public TapeAction            TapeAction         { get; private set; }

            // Buff-Bound
            public Buff                  Buff               { get; private set; }
            public AudioFileOutput       AudioFileOutput    { get; private set; }

            // Independent after Taping
            public Sample                Sample             { get; private set; }
            public AudioInfoWish         AudioInfoWish      { get; private set; }
            public AudioFileInfo         AudioFileInfo      { get; private set; }
            
            // Immutable
            public WavHeaderStruct       WavHeader          { get; private set; }
            public SampleDataTypeEnum    SampleDataTypeEnum { get; private set; }
            public SampleDataType        SampleDataType     { get; private set; }
            public Type                  Type               { get; private set; }
            
            public TestEntities(Action<SynthWishes> initialize = null) 
            {
                // SynthWishes-Bound
                SynthWishes  = new SynthWishes();
                Context      = SynthWishes.Context;
                ConfigWishes = SynthWishes.Config;
                FlowNode     = SynthWishes.Value(123);
                
                // Global-Bound
                ConfigSection = new ConfigWishesAccessor(ConfigWishes)._section; 
                
                initialize?.Invoke(SynthWishes);
                
                Record();
            }

            public void Record()
            {
                // Record
                Tape = null;
                SynthWishes.RunOnThis(() => FlowNode.AfterRecord(x => Tape = x));
                IsNotNull(() => Tape);
                
                // Tape-Bound
                TapeConfig  = Tape.Config;
                TapeActions = Tape.Actions;
                TapeAction  = Tape.Actions.AfterRecord;
                
                // Buff-Bound
                Buff            = Tape.Buff;
                AudioFileOutput = Tape.UnderlyingAudioFileOutput;
                
                // Independent after Taping
                Sample        = Tape.UnderlyingSample;
                AudioInfoWish = Sample.ToWish();
                AudioFileInfo = AudioInfoWish.FromWish();
                
                // Immutable
                WavHeader          = Sample.ToWavHeader();
                SampleDataTypeEnum = Sample.GetSampleDataTypeEnum();
                SampleDataType     = Sample.SampleDataType;

                int bits = SynthWishes.GetBits;
                switch (bits)
                {
                    case 8:  Type = typeof(byte);  break;
                    case 16: Type = typeof(Int16); break;
                    case 32: Type = typeof(float); break;
                    default: throw new Exception($"{new { bits }} not supported.");
                }
            }
        }

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