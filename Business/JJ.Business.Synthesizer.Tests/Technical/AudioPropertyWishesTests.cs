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
#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioPropertyWishesTests
    {
        // Bits

        [TestMethod] public void Bits_Normal()
        {
            Bits_Normal(32, 8);
            Bits_Normal(32, 16);
            Bits_Normal(16, 32);
        }
        
        void Bits_Normal(int init, int value)
        {
            // Check Before Change
            { 
                var x = new TestEntities(init);
                x.All_Bits_Equal(init);
            }

            // Synth-Bound Changes
            {
                TestSetter(x => x.SynthWishes.Bits(value));
                TestSetter(x => x.FlowNode.Bits(value));
                TestSetter(x => x.ConfigWishes.Bits(value));
                TestSetter(x =>
                {
                    if (value == 8) x.SynthWishes.With8Bit();
                    if (value == 16) x.SynthWishes.With16Bit();
                    if (value == 32) x.SynthWishes.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.FlowNode.With8Bit();
                    if (value == 16) x.FlowNode.With16Bit();
                    if (value == 32) x.FlowNode.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.ConfigWishes.With8Bit();
                    if (value == 16) x.ConfigWishes.With16Bit();
                    if (value == 32) x.ConfigWishes.With32Bit();
                });
                
                void TestSetter(Action<TestEntities> setter)
                {
                    var x = new TestEntities(init);
                    x.All_Bits_Equal(init);
                    
                    setter(x);
                    
                    x.SynthBound_Bits_Equal(value);
                    x.TapeBound_Bits_Equal(init);
                    x.BuffBound_Bits_Equal(init);
                    x.Independent_Bits_Equal(init);
                    x.Immutable_Bits_Equal(init);
                    
                    x.Record();
                    
                    x.All_Bits_Equal(value);
                }
            }

            // Tape-Bound Changes
            {
                TestSetter(x => x.Tape.Bits(value));
                TestSetter(x => x.TapeConfig.Bits(value));
                TestSetter(x => x.TapeActions.Bits(value));
                TestSetter(x => x.TapeAction.Bits(value));
                TestSetter(x =>
                {
                    if (value == 8) x.Tape.With8Bit();
                    if (value == 16) x.Tape.With16Bit();
                    if (value == 32) x.Tape.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.TapeConfig.With8Bit();
                    if (value == 16) x.TapeConfig.With16Bit();
                    if (value == 32) x.TapeConfig.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.TapeActions.With8Bit();
                    if (value == 16) x.TapeActions.With16Bit();
                    if (value == 32) x.TapeActions.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.TapeAction.With8Bit();
                    if (value == 16) x.TapeAction.With16Bit();
                    if (value == 32) x.TapeAction.With32Bit();
                });
                
                void TestSetter(Action<TestEntities> setter)
                {
                    var x = new TestEntities(init);
                    x.All_Bits_Equal(init);
                    
                    setter(x);
                    
                    x.SynthBound_Bits_Equal(init);
                    x.TapeBound_Bits_Equal(value);
                    x.BuffBound_Bits_Equal(init);
                    x.Independent_Bits_Equal(init);
                    x.Immutable_Bits_Equal(init);
                    
                    x.Record();
                    
                    x.All_Bits_Equal(init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
                }
            }

            // Buff-Bound Changes
            {
                TestSetter(x => x.Buff.Bits(value, x.Context));
                TestSetter(x => x.AudioFileOutput.Bits(value, x.Context));
                TestSetter(x =>
                {
                    if (value == 8) x.Buff.With8Bit(x.Context);
                    if (value == 16) x.Buff.With16Bit(x.Context);
                    if (value == 32) x.Buff.With32Bit(x.Context);
                });
                TestSetter(x =>
                {
                    if (value == 8) x.AudioFileOutput.With8Bit(x.Context);
                    if (value == 16) x.AudioFileOutput.With16Bit(x.Context);
                    if (value == 32) x.AudioFileOutput.With32Bit(x.Context);
                });

                void TestSetter(Action<TestEntities> setter)
                {    
                    var x = new TestEntities(init);
                    x.All_Bits_Equal(init);
                    
                    setter(x);

                    x.SynthBound_Bits_Equal(init);
                    x.TapeBound_Bits_Equal(init);
                    x.BuffBound_Bits_Equal(value);
                    x.Independent_Bits_Equal(init);
                    x.Immutable_Bits_Equal(init);
                    
                    x.Record();
                    
                    x.All_Bits_Equal(init);
                }
            }
        }

        // Bits With Type Arguments
        
        [TestMethod] public void Bits_WithTypeArguments()
        {
            // Getters
            AreEqual(8, () => Bits<byte>());
            AreEqual(16, () => Bits<short>());
            AreEqual(32, () => Bits<float>());
        
            // Conversion-Style Getters
            AreEqual(8, () => TypeToBits<byte>());
            AreEqual(16, () => TypeToBits<short>());
            AreEqual(32, () => TypeToBits<float>());

            // Shorthand Getters          
            IsTrue(() => Is8Bit<byte>());
            IsFalse(() => Is8Bit<short>());
            IsFalse(() => Is8Bit<float>());

            IsFalse(() => Is16Bit<byte>());
            IsTrue(() => Is16Bit<short>());
            IsFalse(() => Is16Bit<float>());

            IsFalse(() => Is32Bit<byte>());
            IsFalse(() => Is32Bit<short>());
            IsTrue(() => Is32Bit<float>());

            // Setters
            AreEqual(typeof(byte), () => Bits<byte>(8));
            AreEqual(typeof(byte), () => Bits<short>(8));
            AreEqual(typeof(byte), () => Bits<float>(8));
            
            AreEqual(typeof(short), () => Bits<byte>(16));
            AreEqual(typeof(short), () => Bits<short>(16));
            AreEqual(typeof(short), () => Bits<float>(16));
            
            AreEqual(typeof(float), () => Bits<byte>(32));
            AreEqual(typeof(float), () => Bits<short>(32));
            AreEqual(typeof(float), () => Bits<float>(32));

            // 'Shorthand' Setters
            AreEqual(typeof(byte), () => With8Bit<byte>());
            AreEqual(typeof(byte), () => With8Bit<short>());
            AreEqual(typeof(byte), () => With8Bit<float>());

            AreEqual(typeof(short), () => With16Bit<byte>());
            AreEqual(typeof(short), () => With16Bit<short>());
            AreEqual(typeof(short), () => With16Bit<float>());

            AreEqual(typeof(float), () => With32Bit<byte>());
            AreEqual(typeof(float), () => With32Bit<short>());
            AreEqual(typeof(float), () => With32Bit<float>());
        }
        
        // Bits Conversion-Style
                
        [TestMethod] public void Bits_ConversionStyle()
        {
            foreach (int bits in new[] { 8, 16, 32 })
            {
                var x = new TestEntities(bits);
                
                // Getters
                AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
                AreEqual(x.SampleDataType,     () => bits.BitsToEntity(x.Context));
                AreEqual(x.Type,               () => bits.BitsToType());
            
                // Setters
                AreEqual(bits, () => x.SampleDataTypeEnum.EnumToBits());
                AreEqual(bits, () => x.SampleDataType    .EntityToBits());
                AreEqual(bits, () => x.Type              .TypeToBits());
            }
            
            // For test coverage
            ThrowsException(() => default(Type).TypeToBits());
        }
        
        // Bits for Independently Changeable or Immutable
        
        [TestMethod] public void Bits_Setters_IndependentsAndImmutables()
        {
            Bits_Setters_IndependentsAndImmutables(from: 32, to: 8);
            Bits_Setters_IndependentsAndImmutables(from: 32, to: 16);
            Bits_Setters_IndependentsAndImmutables(from: 16, to: 32);
        }
        void Bits_Setters_IndependentsAndImmutables(int from, int to)
        {
            var x = new TestEntities(from);
            
            // Test Mutations

            // Independent after Taping
            AreEqual(from, () => x.Sample.Bits());
            x.Sample.Bits(to, x.Context);
            AreEqual(to, () => x.Sample.Bits());
            
            AreEqual(from, () => x.AudioInfoWish.Bits());
            x.AudioInfoWish.Bits(to);
            AreEqual(to, () => x.AudioInfoWish.Bits());
            
            AreEqual(from, () => x.AudioFileInfo.Bits());
            x.AudioFileInfo.Bits(to);
            AreEqual(to, () => x.AudioFileInfo.Bits());

            // Immutable                        
            AreEqual(from, () => x.WavHeader.Bits());
            var wavHeaderAfter = x.WavHeader.Bits(to);
            AreEqual(from, () => x.WavHeader.Bits());
            AreEqual(to,   () => wavHeaderAfter.Bits());
            
            AreEqual(from, () => x.SampleDataTypeEnum.Bits());
            var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.Bits(to);
            AreEqual(from, () => x.SampleDataTypeEnum.Bits());
            AreEqual(to,   () => sampleDataTypeEnumAfter.Bits());
            
            AreEqual(from, () => x.SampleDataType.Bits());
            var sampleDataTypeAfter = x.SampleDataType.Bits(to, x.Context);
            AreEqual(from, () => x.SampleDataType.Bits());
            AreEqual(to,   () => sampleDataTypeAfter.Bits());
            
            AreEqual(from, () => x.Type.Bits());
            var typeAfter = x.Type.Bits(to);
            AreEqual(from, () => x.Type.Bits());
            AreEqual(to,   () => typeAfter.Bits());
            
            // Test After-Record
            x.Record();

            // All is reset
            x.All_Bits_Equal(from);
        
            // Except for our variables
            AreEqual(to, () => wavHeaderAfter.Bits());
            AreEqual(to, () => sampleDataTypeEnumAfter.Bits());
            AreEqual(to, () => sampleDataTypeAfter.Bits());
            AreEqual(to, () => typeAfter.Bits());
        }

        [TestMethod] public void Bits_Setters_8Bit_Shorthand_ChangingIndependentAndImmutables()
        {
            var x = new TestEntities(bits: 32);
            
            // Test Mutations

            // Independent after Taping
            IsFalse(() => x.Sample.Is8Bit());
            x.Sample.With8Bit(x.Context);
            IsTrue(() => x.Sample.Is8Bit());
            
            IsFalse(() => x.AudioInfoWish.Is8Bit());
            x.AudioInfoWish.With8Bit();
            IsTrue(() => x.AudioInfoWish.Is8Bit());
                                    
            IsFalse(() => x.AudioFileInfo.Is8Bit());
            x.AudioFileInfo.With8Bit();
            IsTrue(() => x.AudioFileInfo.Is8Bit());

            // Immutable                        
            IsFalse(() => x.WavHeader.Is8Bit());
            var wavHeaderAfter = x.WavHeader.With8Bit();
            IsFalse(() => x.WavHeader.Is8Bit());
            IsTrue(() => wavHeaderAfter.Is8Bit());
            
            IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
            var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With8Bit();
            IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is8Bit());

            IsFalse(() => x.SampleDataType.Is8Bit());
            var sampleDataTypeAfter = x.SampleDataType.With8Bit(x.Context);
            IsFalse(() => x.SampleDataType.Is8Bit());
            IsTrue(() => sampleDataTypeAfter.Is8Bit());

            IsFalse(() => x.Type.Is8Bit());
            var typeAfter = x.Type.With8Bit();
            IsFalse(() => x.Type.Is8Bit());
            IsTrue(() => typeAfter.Is8Bit());
        
            // Test After Record
            
            x.Record();

            // Independent after Taping
            IsFalse(() => x.Sample.Is8Bit());
            IsFalse(() => x.AudioInfoWish.Is8Bit());
            IsFalse(() => x.AudioFileInfo.Is8Bit());

            // Immutable
            IsFalse(() => x.WavHeader.Is8Bit());
            IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
            IsFalse(() => x.SampleDataType.Is8Bit());
            IsFalse(() => x.Type.Is8Bit());
        
            IsTrue(() => wavHeaderAfter.Is8Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is8Bit());
            IsTrue(() => sampleDataTypeAfter.Is8Bit());
            IsTrue(() => typeAfter.Is8Bit());
}
                
        [TestMethod] public void Bits_Setters_16Bit_Shorthand_ChangingIndependentAndImmutables()
        {
            var x = new TestEntities(bits: 32);
            
            // Test Mutations

            // Independent after Taping
            IsFalse(() => x.Sample.Is16Bit());
            x.Sample.With16Bit(x.Context);
            IsTrue(() => x.Sample.Is16Bit());
            
            IsFalse(() => x.AudioInfoWish.Is16Bit());
            x.AudioInfoWish.With16Bit();
            IsTrue(() => x.AudioInfoWish.Is16Bit());
                                    
            IsFalse(() => x.AudioFileInfo.Is16Bit());
            x.AudioFileInfo.With16Bit();
            IsTrue(() => x.AudioFileInfo.Is16Bit());

            // Immutable                        
            IsFalse(() => x.WavHeader.Is16Bit());
            var wavHeaderAfter = x.WavHeader.With16Bit();
            IsFalse(() => x.WavHeader.Is16Bit());
            IsTrue(() => wavHeaderAfter.Is16Bit());
            
            IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
            var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With16Bit();
            IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is16Bit());

            IsFalse(() => x.SampleDataType.Is16Bit());
            var sampleDataTypeAfter = x.SampleDataType.With16Bit(x.Context);
            IsFalse(() => x.SampleDataType.Is16Bit());
            IsTrue(() => sampleDataTypeAfter.Is16Bit());

            IsFalse(() => x.Type.Is16Bit());
            var typeAfter = x.Type.With16Bit();
            IsFalse(() => x.Type.Is16Bit());
            IsTrue(() => typeAfter.Is16Bit());
        
            // Test After Record
            
            x.Record();

            // Independent after Taping
            IsFalse(() => x.Sample.Is16Bit());
            IsFalse(() => x.AudioInfoWish.Is16Bit());
            IsFalse(() => x.AudioFileInfo.Is16Bit());

            // Immutable
            IsFalse(() => x.WavHeader.Is16Bit());
            IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
            IsFalse(() => x.SampleDataType.Is16Bit());
            IsFalse(() => x.Type.Is16Bit());
        
            IsTrue(() => wavHeaderAfter.Is16Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is16Bit());
            IsTrue(() => sampleDataTypeAfter.Is16Bit());
            IsTrue(() => typeAfter.Is16Bit());
        }
                
        [TestMethod] public void Bits_Setters_32Bit_Shorthand_ChangingIndependentAndImmutables()
        {
            var x = new TestEntities(bits: 16);
            
            // Test Mutations

            // Independent after Taping
            IsFalse(() => x.Sample.Is32Bit());
            x.Sample.With32Bit(x.Context);
            IsTrue(() => x.Sample.Is32Bit());
            
            IsFalse(() => x.AudioInfoWish.Is32Bit());
            x.AudioInfoWish.With32Bit();
            IsTrue(() => x.AudioInfoWish.Is32Bit());
                                    
            IsFalse(() => x.AudioFileInfo.Is32Bit());
            x.AudioFileInfo.With32Bit();
            IsTrue(() => x.AudioFileInfo.Is32Bit());

            // Immutable                        
            IsFalse(() => x.WavHeader.Is32Bit());
            var wavHeaderAfter = x.WavHeader.With32Bit();
            IsFalse(() => x.WavHeader.Is32Bit());
            IsTrue(() => wavHeaderAfter.Is32Bit());
            
            IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
            var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With32Bit();
            IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is32Bit());

            IsFalse(() => x.SampleDataType.Is32Bit());
            var sampleDataTypeAfter = x.SampleDataType.With32Bit(x.Context);
            IsFalse(() => x.SampleDataType.Is32Bit());
            IsTrue(() => sampleDataTypeAfter.Is32Bit());

            IsFalse(() => x.Type.Is32Bit());
            var typeAfter = x.Type.With32Bit();
            IsFalse(() => x.Type.Is32Bit());
            IsTrue(() => typeAfter.Is32Bit());
        
            // Test After Record
            
            x.Record();

            // Independent after Taping
            IsFalse(() => x.Sample.Is32Bit());
            IsFalse(() => x.AudioInfoWish.Is32Bit());
            IsFalse(() => x.AudioFileInfo.Is32Bit());

            // Immutable
            IsFalse(() => x.WavHeader.Is32Bit());
            IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
            IsFalse(() => x.SampleDataType.Is32Bit());
            IsFalse(() => x.Type.Is32Bit());
        
            IsTrue(() => wavHeaderAfter.Is32Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is32Bit());
            IsTrue(() => sampleDataTypeAfter.Is32Bit());
            IsTrue(() => typeAfter.Is32Bit());
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

            public TestEntities(int bits) : this(x => x.WithBits(bits)) 
            { }
            
            private TestEntities(Action<SynthWishes> initialize = null) 
            {
                // SynthWishes-Bound
                SynthWishes  = new SynthWishes();
                Context      = SynthWishes.Context;
                ConfigWishes = SynthWishes.Config;
                FlowNode     = SynthWishes.Sine();
                
                // Global-Bound
                ConfigSection = new ConfigWishesAccessor(ConfigWishes)._section; 

                // Initialize
                SynthWishes.WithSamplingRate(100);
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
                    case 16: Type = typeof(short); break;
                    case 32: Type = typeof(float); break;
                    default: throw new Exception($"{new { bits }} not supported.");
                }
            }
                    
            public void GlobalBound_Bits_Equal(int bits)
            {
                AreEqual(bits, () => ConfigSection.Bits());
            }

            public void All_Bits_Equal(int bits)
            {
                //GlobalBound_Bits_Equal(bits); // Skip because stays the same.
                SynthBound_Bits_Equal(bits);
                TapeBound_Bits_Equal(bits);
                BuffBound_Bits_Equal(bits);
                Independent_Bits_Equal(bits);
                Immutable_Bits_Equal(bits);
            }

            public void SynthBound_Bits_Equal(int bits)
            {
                AreEqual(bits, () => SynthWishes.Bits());
                AreEqual(bits, () => FlowNode.Bits());
                AreEqual(bits, () => ConfigWishes.Bits());
                
                AreEqual(bits == 8, () => SynthWishes.Is8Bit());
                AreEqual(bits == 8, () => FlowNode.Is8Bit());
                AreEqual(bits == 8, () => ConfigWishes.Is8Bit());
                
                AreEqual(bits == 16, () => SynthWishes.Is16Bit());
                AreEqual(bits == 16, () => FlowNode.Is16Bit());
                AreEqual(bits == 16, () => ConfigWishes.Is16Bit());
                
                AreEqual(bits == 32, () => SynthWishes.Is32Bit());
                AreEqual(bits == 32, () => FlowNode.Is32Bit());
                AreEqual(bits == 32, () => ConfigWishes.Is32Bit());
            }
            
            public void TapeBound_Bits_Equal(int bits)
            {
                AreEqual(bits, () => Tape.Bits());
                AreEqual(bits, () => TapeConfig.Bits());
                AreEqual(bits, () => TapeActions.Bits());
                AreEqual(bits, () => TapeAction.Bits());
                
                AreEqual(bits == 8, () => Tape.Is8Bit());
                AreEqual(bits == 8, () => TapeConfig.Is8Bit());
                AreEqual(bits == 8, () => TapeActions.Is8Bit());
                AreEqual(bits == 8, () => TapeAction.Is8Bit());
            
                AreEqual(bits == 16, () => Tape.Is16Bit());
                AreEqual(bits == 16, () => TapeConfig.Is16Bit());
                AreEqual(bits == 16, () => TapeActions.Is16Bit());
                AreEqual(bits == 16, () => TapeAction.Is16Bit());
            
                AreEqual(bits == 32, () => Tape.Is32Bit());
                AreEqual(bits == 32, () => TapeConfig.Is32Bit());
                AreEqual(bits == 32, () => TapeActions.Is32Bit());
                AreEqual(bits == 32, () => TapeAction.Is32Bit());
            }
            
            public void BuffBound_Bits_Equal(int bits)
            {
                AreEqual(bits, () => Buff.Bits());
                AreEqual(bits, () => AudioFileOutput.Bits());
                
                AreEqual(bits == 8, () => Buff.Is8Bit());
                AreEqual(bits == 8, () => AudioFileOutput.Is8Bit());
                
                AreEqual(bits == 16, () => Buff.Is16Bit());
                AreEqual(bits == 16, () => AudioFileOutput.Is16Bit());
                
                AreEqual(bits == 32, () => Buff.Is32Bit());
                AreEqual(bits == 32, () => AudioFileOutput.Is32Bit());
            }

            public void Independent_Bits_Equal(int bits)
            {
                // Independent after Taping
                
                AreEqual(bits, () => Sample.Bits());
                AreEqual(bits, () => AudioInfoWish.Bits());
                AreEqual(bits, () => AudioFileInfo.Bits());
            
                AreEqual(bits == 8, () => Sample.Is8Bit());
                AreEqual(bits == 8, () => AudioInfoWish.Is8Bit());
                AreEqual(bits == 8, () => AudioFileInfo.Is8Bit());
                                
                AreEqual(bits == 16, () => Sample.Is16Bit());
                AreEqual(bits == 16, () => AudioInfoWish.Is16Bit());
                AreEqual(bits == 16, () => AudioFileInfo.Is16Bit());
                                
                AreEqual(bits == 32, () => Sample.Is32Bit());
                AreEqual(bits == 32, () => AudioInfoWish.Is32Bit());
                AreEqual(bits == 32, () => AudioFileInfo.Is32Bit());
            }

            public void Immutable_Bits_Equal(int bits)
            {
                AreEqual(bits, () => WavHeader.Bits());
                AreEqual(bits, () => SampleDataTypeEnum.Bits());
                AreEqual(bits, () => SampleDataType.Bits());
                AreEqual(bits, () => Type.Bits());

                AreEqual(bits == 8, () => WavHeader.Is8Bit());
                AreEqual(bits == 8, () => SampleDataTypeEnum.Is8Bit());
                AreEqual(bits == 8, () => SampleDataType.Is8Bit());
                AreEqual(bits == 8, () => Type.Is8Bit());

                AreEqual(bits == 16, () => WavHeader.Is16Bit());
                AreEqual(bits == 16, () => SampleDataTypeEnum.Is16Bit());
                AreEqual(bits == 16, () => SampleDataType.Is16Bit());
                AreEqual(bits == 16, () => Type.Is16Bit());
            
                AreEqual(bits == 32, () => WavHeader.Is32Bit());
                AreEqual(bits == 32, () => SampleDataTypeEnum.Is32Bit());
                AreEqual(bits == 32, () => SampleDataType.Is32Bit());
                AreEqual(bits == 32, () => Type.Is32Bit());
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