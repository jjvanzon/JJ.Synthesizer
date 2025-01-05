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
#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioPropertyWishesTests
    {
        // Bit Getters

        [TestMethod] public void Bits_Getters_Normal()
        {
            Bits_Getters_Normal(8);
            Bits_Getters_Normal(16);
            Bits_Getters_Normal(32);
        }
        void Bits_Getters_Normal(int bits)
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
                
        [TestMethod] public void Bits_Getters_ConversionStyle()
        {
            int[] values = { 8, 16, 32 };
            foreach (int bits in values)
            {
                var x = new TestEntities(s => s.WithBits(bits));
                AreEqual(bits, () => x.SampleDataTypeEnum.EnumToBits());
                AreEqual(bits, () => x.SampleDataType.EntityToBits());
                AreEqual(bits, () => x.Type.TypeToBits());
            }
        }

        [TestMethod] public void Bits_Getters_FromTypeArguments()
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
        }
        
        [TestMethod] public void Bits_Getters_8BitShorthand()
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
        
        [TestMethod] public void Bits_Getters_16BitShorthand()
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
                
        [TestMethod] public void Bits_Getters_32BitShorthand()
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

        // Bit Setters
        
        [TestMethod] public void Bits_Setters_Normal_ShallowTest()
        {
            Bits_Setters_Normal_ShallowTest(32, 8);
            Bits_Setters_Normal_ShallowTest(32, 16);
            Bits_Setters_Normal_ShallowTest(16, 32);
        }
        void Bits_Setters_Normal_ShallowTest(int from, int to)
        {
            var x = new TestEntities(s => s.WithBits(from));
            
            // SynthWishes-Bound
            AreEqual(x.SynthWishes,  () => x.SynthWishes.Bits(to));
            AreEqual(x.FlowNode,     () => x.FlowNode.Bits(to));
            AreEqual(x.ConfigWishes, () => x.ConfigWishes.Bits(to));
            
            // Tape-Bound
            AreEqual(x.Tape,        () => x.Tape.Bits(to));
            AreEqual(x.TapeConfig,  () => x.TapeConfig.Bits(to));
            AreEqual(x.TapeActions, () => x.TapeActions.Bits(to));
            AreEqual(x.TapeAction,  () => x.TapeAction.Bits(to));
            
            // Buff-Bound
            AreEqual(x.Buff,            () => x.Buff.Bits(to, x.Context));
            AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Bits(to, x.Context));
            
            // Independent after Taping
            AreEqual(x.Sample,        () => x.Sample.Bits(to, x.Context));
            AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.Bits(to));
            AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.Bits(to));
            
            // Immutable
            NotEqual(x.WavHeader,          () => x.WavHeader.Bits(to));
            NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.Bits(to));
            NotEqual(x.SampleDataType,     () => x.SampleDataType.Bits(to, x.Context));
            NotEqual(x.Type,               () => x.Type.Bits(to));
        }
        
        [TestMethod] public void Bits_Setters_Normal()
        {
            Bits_Setters_Normal(32, 8);
            Bits_Setters_Normal(32, 16);
            Bits_Setters_Normal(16, 32);
        }
        void Bits_Setters_Normal(int from, int to)
        {
            { // Check Before Change
                
                var x = new TestEntities(s => s.WithBits(from));
                
                // SynthWishes-Bound
                AreEqual(from, () => x.SynthWishes.Bits());
                AreEqual(from, () => x.FlowNode.Bits());
                AreEqual(from, () => x.ConfigWishes.Bits());
                
                // Tape-Bound
                AreEqual(from, () => x.Tape.Bits());
                AreEqual(from, () => x.TapeConfig.Bits());
                AreEqual(from, () => x.TapeActions.Bits());
                AreEqual(from, () => x.TapeAction.Bits());
                
                // Buff-Bound
                AreEqual(from, () => x.Buff.Bits());
                AreEqual(from, () => x.AudioFileOutput.Bits());
                
                // Independent after Taping
                AreEqual(from, () => x.Sample.Bits());
                AreEqual(from, () => x.AudioInfoWish.Bits());
                AreEqual(from, () => x.AudioFileInfo.Bits());
                
                // Immutable
                AreEqual(from, () => x.WavHeader.Bits());
                AreEqual(from, () => x.SampleDataTypeEnum.Bits());
                AreEqual(from, () => x.SampleDataType.Bits());
                AreEqual(from, () => x.Type.Bits());
            }
            
            { // SynthWishes-Bound Changes
                
                var x = new TestEntities(s => s.WithBits(from));
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    AreEqual(from, () => x.SynthWishes.Bits());
                    AreEqual(from, () => x.FlowNode.Bits());
                    AreEqual(from, () => x.ConfigWishes.Bits());
                    
                    // Change!
                    x.ConfigWishes.Bits(to);
                    
                    // SynthWishes-Bound
                    AreEqual(to, () => x.SynthWishes.Bits());
                    AreEqual(to, () => x.FlowNode.Bits());
                    AreEqual(to, () => x.ConfigWishes.Bits());
                    
                    // Tape-Bound
                    AreEqual(from, () => x.Tape.Bits());
                    AreEqual(from, () => x.TapeConfig.Bits());
                    AreEqual(from, () => x.TapeActions.Bits());
                    AreEqual(from, () => x.TapeAction.Bits());
                    
                    // Buff-Bound
                    AreEqual(from, () => x.Buff.Bits());
                    AreEqual(from, () => x.AudioFileOutput.Bits());
                    
                    // Independent after Taping
                    AreEqual(from, () => x.Sample.Bits());
                    AreEqual(from, () => x.AudioInfoWish.Bits());
                    AreEqual(from, () => x.AudioFileInfo.Bits());
                    
                    // Immutable
                    AreEqual(from, () => x.WavHeader.Bits());
                    AreEqual(from, () => x.SampleDataTypeEnum.Bits());
                    AreEqual(from, () => x.SampleDataType.Bits());
                    AreEqual(from, () => x.Type.Bits());
                }
                
                { // After Record
                    x.Record();
                    
                    // SynthWishes-Bound
                    AreEqual(to, () => x.SynthWishes.Bits());
                    AreEqual(to, () => x.FlowNode.Bits());
                    AreEqual(to, () => x.ConfigWishes.Bits());
                    
                    // Tape-Bound
                    AreEqual(to, () => x.Tape.Bits());
                    AreEqual(to, () => x.TapeConfig.Bits());
                    AreEqual(to, () => x.TapeActions.Bits());
                    AreEqual(to, () => x.TapeAction.Bits());
                    
                    // Buff-Bound
                    AreEqual(to, () => x.Buff.Bits());
                    AreEqual(to, () => x.AudioFileOutput.Bits());
                    
                    // Independent after Taping
                    AreEqual(to, () => x.Sample.Bits());
                    AreEqual(to, () => x.AudioInfoWish.Bits());
                    AreEqual(to, () => x.AudioFileInfo.Bits());
                    
                    // Immutable
                    AreEqual(to, () => x.WavHeader.Bits());
                    AreEqual(to, () => x.SampleDataTypeEnum.Bits());
                    AreEqual(to, () => x.SampleDataType.Bits());
                    AreEqual(to, () => x.Type.Bits());
                }
            }
            
            { // Tape-Bound Changes
                
                var x = new TestEntities(s => s.WithBits(from));
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    AreEqual(from, () => x.SynthWishes.Bits());
                    AreEqual(from, () => x.FlowNode.Bits());
                    AreEqual(from, () => x.ConfigWishes.Bits());
                    
                    // Tape-Bound
                    AreEqual(from, () => x.Tape.Bits());
                    AreEqual(from, () => x.TapeConfig.Bits());
                    AreEqual(from, () => x.TapeActions.Bits());
                    AreEqual(from, () => x.TapeAction.Bits());
                    
                    // Change!
                    x.TapeAction.Bits(to);
                    
                    // SynthWishes-Bound
                    AreEqual(from, () => x.SynthWishes.Bits());
                    AreEqual(from, () => x.FlowNode.Bits());
                    AreEqual(from, () => x.ConfigWishes.Bits());
                    
                    // Tape-Bound
                    AreEqual(to, () => x.Tape.Bits());
                    AreEqual(to, () => x.TapeConfig.Bits());
                    AreEqual(to, () => x.TapeActions.Bits());
                    AreEqual(to, () => x.TapeAction.Bits());
                    
                    // Buff-Bound
                    AreEqual(from, () => x.Buff.Bits());
                    AreEqual(from, () => x.AudioFileOutput.Bits());
                    
                    // Independent after Taping
                    AreEqual(from, () => x.Sample.Bits());
                    AreEqual(from, () => x.AudioInfoWish.Bits());
                    AreEqual(from, () => x.AudioFileInfo.Bits());
                    
                    // Immutable
                    AreEqual(from, () => x.WavHeader.Bits());
                    AreEqual(from, () => x.SampleDataTypeEnum.Bits());
                    AreEqual(from, () => x.SampleDataType.Bits());
                    AreEqual(from, () => x.Type.Bits());
                }
                
                { // After Record
                    x.Record();
                    
                    // SynthWishes-Bound
                    AreEqual(from, () => x.SynthWishes.Bits());
                    AreEqual(from, () => x.FlowNode.Bits());
                    AreEqual(from, () => x.ConfigWishes.Bits());
                    
                    // By Design: Currently can't re-record over the same tape.
                    // So you always get a new tape, overwriting the changed values upon record.
                    
                    // Tape-Bound
                    AreEqual(from, () => x.Tape.Bits());
                    AreEqual(from, () => x.TapeConfig.Bits());
                    AreEqual(from, () => x.TapeActions.Bits());
                    AreEqual(from, () => x.TapeAction.Bits());
                    
                    // Buff-Bound
                    AreEqual(from, () => x.Buff.Bits());
                    AreEqual(from, () => x.AudioFileOutput.Bits());
                    
                    // Independent after Taping
                    AreEqual(from, () => x.Sample.Bits());
                    AreEqual(from, () => x.AudioInfoWish.Bits());
                    AreEqual(from, () => x.AudioFileInfo.Bits());
                    
                    // Immutable
                    AreEqual(from, () => x.WavHeader.Bits());
                    AreEqual(from, () => x.SampleDataTypeEnum.Bits());
                    AreEqual(from, () => x.SampleDataType.Bits());
                    AreEqual(from, () => x.Type.Bits());
                }
            }
            
            { // Buff-Bound Changes
                
                var x = new TestEntities(s => s.WithBits(from));
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    AreEqual(from, () => x.SynthWishes.Bits());
                    AreEqual(from, () => x.FlowNode.Bits());
                    AreEqual(from, () => x.ConfigWishes.Bits());
                    
                    // Tape-Bound
                    AreEqual(from, () => x.Tape.Bits());
                    AreEqual(from, () => x.TapeConfig.Bits());
                    AreEqual(from, () => x.TapeActions.Bits());
                    AreEqual(from, () => x.TapeAction.Bits());
                    
                    // Buff-Bound
                    AreEqual(from, () => x.Buff.Bits());
                    AreEqual(from, () => x.AudioFileOutput.Bits());
                    
                    // Change!
                    x.AudioFileOutput.Bits(to, x.Context);
                    
                    // SynthWishes-Bound
                    AreEqual(from, () => x.SynthWishes.Bits());
                    AreEqual(from, () => x.FlowNode.Bits());
                    AreEqual(from, () => x.ConfigWishes.Bits());
                    
                    // Tape-Bound
                    AreEqual(from, () => x.Tape.Bits());
                    AreEqual(from, () => x.TapeConfig.Bits());
                    AreEqual(from, () => x.TapeActions.Bits());
                    AreEqual(from, () => x.TapeAction.Bits());
                    
                    // Buff-Bound
                    AreEqual(to, () => x.Buff.Bits());
                    AreEqual(to, () => x.AudioFileOutput.Bits());
                    
                    // Independent after Taping
                    AreEqual(from, () => x.Sample.Bits());
                    AreEqual(from, () => x.AudioInfoWish.Bits());
                    AreEqual(from, () => x.AudioFileInfo.Bits());
                    
                    // Immutable
                    AreEqual(from, () => x.WavHeader.Bits());
                    AreEqual(from, () => x.SampleDataTypeEnum.Bits());
                    AreEqual(from, () => x.SampleDataType.Bits());
                    AreEqual(from, () => x.Type.Bits());
                }
                
                { // After-Record
                    
                    x.Record();
                    
                    // SynthWishes-Bound
                    AreEqual(from, () => x.SynthWishes.Bits());
                    AreEqual(from, () => x.FlowNode.Bits());
                    AreEqual(from, () => x.ConfigWishes.Bits());
                    
                    // Tape-Bound
                    AreEqual(from, () => x.Tape.Bits());
                    AreEqual(from, () => x.TapeConfig.Bits());
                    AreEqual(from, () => x.TapeActions.Bits());
                    AreEqual(from, () => x.TapeAction.Bits());
                    
                    // Buff-Bound
                    AreEqual(from, () => x.Buff.Bits());
                    AreEqual(from, () => x.AudioFileOutput.Bits());
                    
                    // Independent after Taping
                    AreEqual(from, () => x.Sample.Bits());
                    AreEqual(from, () => x.AudioInfoWish.Bits());
                    AreEqual(from, () => x.AudioFileInfo.Bits());
                    
                    // Immutable
                    AreEqual(from, () => x.WavHeader.Bits());
                    AreEqual(from, () => x.SampleDataTypeEnum.Bits());
                    AreEqual(from, () => x.SampleDataType.Bits());
                    AreEqual(from, () => x.Type.Bits());
                }
            }
        }
        
        [TestMethod] public void Bits_Setters_ConversionStyle()
        {
            foreach (int bits in new[] { 8, 16, 32 })
            {
                var x = new TestEntities(s => s.WithBits(bits));
                AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
                AreEqual(x.SampleDataType,     () => bits.BitsToEntity(x.Context));
                AreEqual(x.Type,               () => bits.BitsToType());
            }
        }

        [TestMethod] public void Bits_Setters_FromTypeArguments()
        {
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
        
        [TestMethod] public void Bits_Setters_IndependentsAndImmutables()
        {
            Bits_Setters_IndependentsAndImmutables(from: 32, to: 8);
            Bits_Setters_IndependentsAndImmutables(from: 32, to: 16);
            Bits_Setters_IndependentsAndImmutables(from: 16, to: 32);
        }
        void Bits_Setters_IndependentsAndImmutables(int from, int to)
        {
            var x = new TestEntities(s => s.WithBits(from));
            
            // Test Changing

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

            // Independent after Taping
            AreEqual(from, () => x.Sample.Bits());
            AreEqual(from, () => x.AudioInfoWish.Bits());
            AreEqual(from, () => x.AudioFileInfo.Bits());

            // Immutable
            AreEqual(from, () => x.WavHeader.Bits());
            AreEqual(from, () => x.SampleDataTypeEnum.Bits());
            AreEqual(from, () => x.SampleDataType.Bits());
            AreEqual(from, () => x.Type.Bits());
        
            AreEqual(to, () => wavHeaderAfter.Bits());
            AreEqual(to, () => sampleDataTypeEnumAfter.Bits());
            AreEqual(to, () => sampleDataTypeAfter.Bits());
            AreEqual(to, () => typeAfter.Bits());
        }    
        
        // Bit Setters - Shorthand
                
        [TestMethod] public void Bits_Setters_8Bit_Shorthand_ShallowTest()
        {
            var x = new TestEntities(s => s.With32Bit());
            
            // SynthWishes-Bound
            AreEqual(x.SynthWishes,  () => x.SynthWishes.With8Bit());
            AreEqual(x.FlowNode,     () => x.FlowNode.With8Bit());
            AreEqual(x.ConfigWishes, () => x.ConfigWishes.With8Bit());
            
            // Tape-Bound
            AreEqual(x.Tape,        () => x.Tape.With8Bit());
            AreEqual(x.TapeConfig,  () => x.TapeConfig.With8Bit());
            AreEqual(x.TapeActions, () => x.TapeActions.With8Bit());
            AreEqual(x.TapeAction,  () => x.TapeAction.With8Bit());
            
            // Buff-Bound
            AreEqual(x.Buff,            () => x.Buff.With8Bit(x.Context));
            AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With8Bit(x.Context));
            
            // Independent after Taping
            AreEqual(x.Sample,        () => x.Sample.With8Bit(x.Context));
            AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With8Bit());
            AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With8Bit());
            
            // Immutable
            NotEqual(x.WavHeader,          () => x.WavHeader.With8Bit());
            NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.With8Bit());
            NotEqual(x.SampleDataType,     () => x.SampleDataType.With8Bit(x.Context));
            NotEqual(x.Type,               () => x.Type.With8Bit());
        }

        [TestMethod] public void Bits_Setters_16Bit_Shorthand_ShallowTest()
        {
            var x = new TestEntities(s => s.With32Bit());
            
            // SynthWishes-Bound
            AreEqual(x.SynthWishes,  () => x.SynthWishes.With16Bit());
            AreEqual(x.FlowNode,     () => x.FlowNode.With16Bit());
            AreEqual(x.ConfigWishes, () => x.ConfigWishes.With16Bit());
            
            // Tape-Bound
            AreEqual(x.Tape,        () => x.Tape.With16Bit());
            AreEqual(x.TapeConfig,  () => x.TapeConfig.With16Bit());
            AreEqual(x.TapeActions, () => x.TapeActions.With16Bit());
            AreEqual(x.TapeAction,  () => x.TapeAction.With16Bit());
            
            // Buff-Bound
            AreEqual(x.Buff,            () => x.Buff.With16Bit(x.Context));
            AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With16Bit(x.Context));
            
            // Independent after Taping
            AreEqual(x.Sample,        () => x.Sample.With16Bit(x.Context));
            AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With16Bit());
            AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With16Bit());
            
            // Immutable
            NotEqual(x.WavHeader,          () => x.WavHeader.With16Bit());
            NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.With16Bit());
            NotEqual(x.SampleDataType,     () => x.SampleDataType.With16Bit(x.Context));
            NotEqual(x.Type,               () => x.Type.With16Bit());
        }
        
        [TestMethod] public void Bits_Setters_32Bit_Shorthand_ShallowTest()
        {
            var x = new TestEntities(s => s.With16Bit());
            
            // SynthWishes-Bound
            AreEqual(x.SynthWishes,  () => x.SynthWishes.With32Bit());
            AreEqual(x.FlowNode,     () => x.FlowNode.With32Bit());
            AreEqual(x.ConfigWishes, () => x.ConfigWishes.With32Bit());
            
            // Tape-Bound
            AreEqual(x.Tape,        () => x.Tape.With32Bit());
            AreEqual(x.TapeConfig,  () => x.TapeConfig.With32Bit());
            AreEqual(x.TapeActions, () => x.TapeActions.With32Bit());
            AreEqual(x.TapeAction,  () => x.TapeAction.With32Bit());
            
            // Buff-Bound
            AreEqual(x.Buff,            () => x.Buff.With32Bit(x.Context));
            AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With32Bit(x.Context));
            
            // Independent after Taping
            AreEqual(x.Sample,        () => x.Sample.With32Bit(x.Context));
            AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With32Bit());
            AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With32Bit());
            
            // Immutable
            NotEqual(x.WavHeader,          () => x.WavHeader.With32Bit());
            NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.With32Bit());
            NotEqual(x.SampleDataType,     () => x.SampleDataType.With32Bit(x.Context));
            NotEqual(x.Type,               () => x.Type.With32Bit());
        }

        [TestMethod] public void Bits_Setters_8Bit_Shorthand()
        {
            { // Check Before Change
                
                var x = new TestEntities(s => s.With32Bit());
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is8Bit());
                IsFalse(() => x.FlowNode.Is8Bit());
                IsFalse(() => x.ConfigWishes.Is8Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is8Bit());
                IsFalse(() => x.TapeConfig.Is8Bit());
                IsFalse(() => x.TapeActions.Is8Bit());
                IsFalse(() => x.TapeAction.Is8Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is8Bit());
                IsFalse(() => x.AudioFileOutput.Is8Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is8Bit());
                IsFalse(() => x.AudioInfoWish.Is8Bit());
                IsFalse(() => x.AudioFileInfo.Is8Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is8Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                IsFalse(() => x.SampleDataType.Is8Bit());
                IsFalse(() => x.Type.Is8Bit());
            }
            
            { // SynthWishes-Bound Change
                
                var x = new TestEntities(s => s.With32Bit());
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is8Bit());
                    IsFalse(() => x.FlowNode.Is8Bit());
                    IsFalse(() => x.ConfigWishes.Is8Bit());
                    
                    // Change!
                    x.ConfigWishes.With8Bit();
                    
                    // SynthWishes-Bound
                    IsTrue(() => x.SynthWishes.Is8Bit());
                    IsTrue(() => x.FlowNode.Is8Bit());
                    IsTrue(() => x.ConfigWishes.Is8Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is8Bit());
                    IsFalse(() => x.TapeConfig.Is8Bit());
                    IsFalse(() => x.TapeActions.Is8Bit());
                    IsFalse(() => x.TapeAction.Is8Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is8Bit());
                    IsFalse(() => x.AudioFileOutput.Is8Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is8Bit());
                    IsFalse(() => x.AudioInfoWish.Is8Bit());
                    IsFalse(() => x.AudioFileInfo.Is8Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is8Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                    IsFalse(() => x.SampleDataType.Is8Bit());
                    IsFalse(() => x.Type.Is8Bit());
                }
                
                { // After-Record
                    
                    x.Record();
                    
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
                    IsTrue(() => x.AudioInfoWish.Is8Bit());
                    IsTrue(() => x.AudioFileInfo.Is8Bit());
                    
                    // Immutable
                    IsTrue(() => x.WavHeader.Is8Bit());
                    IsTrue(() => x.SampleDataTypeEnum.Is8Bit());
                    IsTrue(() => x.SampleDataType.Is8Bit());
                    IsTrue(() => x.Type.Is8Bit());
                }
            }
            
            { // Tape-Bound Change
                
                var x = new TestEntities(s => s.With32Bit());
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is8Bit());
                    IsFalse(() => x.FlowNode.Is8Bit());
                    IsFalse(() => x.ConfigWishes.Is8Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is8Bit());
                    IsFalse(() => x.TapeConfig.Is8Bit());
                    IsFalse(() => x.TapeActions.Is8Bit());
                    IsFalse(() => x.TapeAction.Is8Bit());
                    
                    // Change!
                    x.TapeAction.With8Bit();
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is8Bit());
                    IsFalse(() => x.FlowNode.Is8Bit());
                    IsFalse(() => x.ConfigWishes.Is8Bit());
                    
                    // Tape-Bound
                    IsTrue(() => x.Tape.Is8Bit());
                    IsTrue(() => x.TapeConfig.Is8Bit());
                    IsTrue(() => x.TapeActions.Is8Bit());
                    IsTrue(() => x.TapeAction.Is8Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is8Bit());
                    IsFalse(() => x.AudioFileOutput.Is8Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is8Bit());
                    IsFalse(() => x.AudioInfoWish.Is8Bit());
                    IsFalse(() => x.AudioFileInfo.Is8Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is8Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                    IsFalse(() => x.SampleDataType.Is8Bit());
                    IsFalse(() => x.Type.Is8Bit());
                }
                
                { // After-Record
                    
                    x.Record();
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is8Bit());
                    IsFalse(() => x.FlowNode.Is8Bit());
                    IsFalse(() => x.ConfigWishes.Is8Bit());
                    
                    // By Design: Currently can't re-record over the same tape.
                    // So you always get a new tape, overwriting the changed values upon record.
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is8Bit());
                    IsFalse(() => x.TapeConfig.Is8Bit());
                    IsFalse(() => x.TapeActions.Is8Bit());
                    IsFalse(() => x.TapeAction.Is8Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is8Bit());
                    IsFalse(() => x.AudioFileOutput.Is8Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is8Bit());
                    IsFalse(() => x.AudioInfoWish.Is8Bit());
                    IsFalse(() => x.AudioFileInfo.Is8Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is8Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                    IsFalse(() => x.SampleDataType.Is8Bit());
                    IsFalse(() => x.Type.Is8Bit());
                }
            }
            
            { // Buff-Bound Change
                
                var x = new TestEntities(s => s.With32Bit());
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is8Bit());
                    IsFalse(() => x.FlowNode.Is8Bit());
                    IsFalse(() => x.ConfigWishes.Is8Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is8Bit());
                    IsFalse(() => x.TapeConfig.Is8Bit());
                    IsFalse(() => x.TapeActions.Is8Bit());
                    IsFalse(() => x.TapeAction.Is8Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is8Bit());
                    IsFalse(() => x.AudioFileOutput.Is8Bit());
                    
                    // Change!
                    x.AudioFileOutput.With8Bit(x.Context);
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is8Bit());
                    IsFalse(() => x.FlowNode.Is8Bit());
                    IsFalse(() => x.ConfigWishes.Is8Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is8Bit());
                    IsFalse(() => x.TapeConfig.Is8Bit());
                    IsFalse(() => x.TapeActions.Is8Bit());
                    IsFalse(() => x.TapeAction.Is8Bit());
                    
                    // Buff-Bound
                    IsTrue(() => x.Buff.Is8Bit());
                    IsTrue(() => x.AudioFileOutput.Is8Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is8Bit());
                    IsFalse(() => x.AudioInfoWish.Is8Bit());
                    IsFalse(() => x.AudioFileInfo.Is8Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is8Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                    IsFalse(() => x.SampleDataType.Is8Bit());
                    IsFalse(() => x.Type.Is8Bit());
                }
                
                { // After Record
                    
                    x.Record();
                    
                    // Overwritten with original SynthWishes settings.
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is8Bit());
                    IsFalse(() => x.FlowNode.Is8Bit());
                    IsFalse(() => x.ConfigWishes.Is8Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is8Bit());
                    IsFalse(() => x.TapeConfig.Is8Bit());
                    IsFalse(() => x.TapeActions.Is8Bit());
                    IsFalse(() => x.TapeAction.Is8Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is8Bit());
                    IsFalse(() => x.AudioFileOutput.Is8Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is8Bit());
                    IsFalse(() => x.AudioInfoWish.Is8Bit());
                    IsFalse(() => x.AudioFileInfo.Is8Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is8Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                    IsFalse(() => x.SampleDataType.Is8Bit());
                    IsFalse(() => x.Type.Is8Bit());
                }
            }
        }
        
        [TestMethod] public void Bits_Setters_16Bit_Shorthand()
        {
            { // Check Before Change
                
                var x = new TestEntities(s => s.With32Bit());
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is16Bit());
                IsFalse(() => x.FlowNode.Is16Bit());
                IsFalse(() => x.ConfigWishes.Is16Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is16Bit());
                IsFalse(() => x.TapeConfig.Is16Bit());
                IsFalse(() => x.TapeActions.Is16Bit());
                IsFalse(() => x.TapeAction.Is16Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is16Bit());
                IsFalse(() => x.AudioFileOutput.Is16Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is16Bit());
                IsFalse(() => x.AudioInfoWish.Is16Bit());
                IsFalse(() => x.AudioFileInfo.Is16Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is16Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                IsFalse(() => x.SampleDataType.Is16Bit());
                IsFalse(() => x.Type.Is16Bit());
            }
            
            { // SynthWishes-Bound Change
                
                var x = new TestEntities(s => s.With32Bit());
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is16Bit());
                    IsFalse(() => x.FlowNode.Is16Bit());
                    IsFalse(() => x.ConfigWishes.Is16Bit());
                    
                    // Change!
                    x.ConfigWishes.With16Bit();
                    
                    // SynthWishes-Bound
                    IsTrue(() => x.SynthWishes.Is16Bit());
                    IsTrue(() => x.FlowNode.Is16Bit());
                    IsTrue(() => x.ConfigWishes.Is16Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is16Bit());
                    IsFalse(() => x.TapeConfig.Is16Bit());
                    IsFalse(() => x.TapeActions.Is16Bit());
                    IsFalse(() => x.TapeAction.Is16Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is16Bit());
                    IsFalse(() => x.AudioFileOutput.Is16Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is16Bit());
                    IsFalse(() => x.AudioInfoWish.Is16Bit());
                    IsFalse(() => x.AudioFileInfo.Is16Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is16Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                    IsFalse(() => x.SampleDataType.Is16Bit());
                    IsFalse(() => x.Type.Is16Bit());
                }
                
                { // After-Record
                    
                    x.Record();
                    
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
                    IsTrue(() => x.AudioInfoWish.Is16Bit());
                    IsTrue(() => x.AudioFileInfo.Is16Bit());
                    
                    // Immutable
                    IsTrue(() => x.WavHeader.Is16Bit());
                    IsTrue(() => x.SampleDataTypeEnum.Is16Bit());
                    IsTrue(() => x.SampleDataType.Is16Bit());
                    IsTrue(() => x.Type.Is16Bit());
                }
            }
            
            { // Tape-Bound Change
                
                var x = new TestEntities(s => s.With32Bit());
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is16Bit());
                    IsFalse(() => x.FlowNode.Is16Bit());
                    IsFalse(() => x.ConfigWishes.Is16Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is16Bit());
                    IsFalse(() => x.TapeConfig.Is16Bit());
                    IsFalse(() => x.TapeActions.Is16Bit());
                    IsFalse(() => x.TapeAction.Is16Bit());
                    
                    // Change!
                    x.TapeAction.With16Bit();
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is16Bit());
                    IsFalse(() => x.FlowNode.Is16Bit());
                    IsFalse(() => x.ConfigWishes.Is16Bit());
                    
                    // Tape-Bound
                    IsTrue(() => x.Tape.Is16Bit());
                    IsTrue(() => x.TapeConfig.Is16Bit());
                    IsTrue(() => x.TapeActions.Is16Bit());
                    IsTrue(() => x.TapeAction.Is16Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is16Bit());
                    IsFalse(() => x.AudioFileOutput.Is16Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is16Bit());
                    IsFalse(() => x.AudioInfoWish.Is16Bit());
                    IsFalse(() => x.AudioFileInfo.Is16Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is16Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                    IsFalse(() => x.SampleDataType.Is16Bit());
                    IsFalse(() => x.Type.Is16Bit());
                }
                
                { // After-Record
                    
                    x.Record();
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is16Bit());
                    IsFalse(() => x.FlowNode.Is16Bit());
                    IsFalse(() => x.ConfigWishes.Is16Bit());
                    
                    // By Design: Currently can't re-record over the same tape.
                    // So you always get a new tape, overwriting the changed values upon record.
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is16Bit());
                    IsFalse(() => x.TapeConfig.Is16Bit());
                    IsFalse(() => x.TapeActions.Is16Bit());
                    IsFalse(() => x.TapeAction.Is16Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is16Bit());
                    IsFalse(() => x.AudioFileOutput.Is16Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is16Bit());
                    IsFalse(() => x.AudioInfoWish.Is16Bit());
                    IsFalse(() => x.AudioFileInfo.Is16Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is16Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                    IsFalse(() => x.SampleDataType.Is16Bit());
                    IsFalse(() => x.Type.Is16Bit());
                }
            }
            
            { // Buff-Bound Change
                
                var x = new TestEntities(s => s.With32Bit());
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is16Bit());
                    IsFalse(() => x.FlowNode.Is16Bit());
                    IsFalse(() => x.ConfigWishes.Is16Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is16Bit());
                    IsFalse(() => x.TapeConfig.Is16Bit());
                    IsFalse(() => x.TapeActions.Is16Bit());
                    IsFalse(() => x.TapeAction.Is16Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is16Bit());
                    IsFalse(() => x.AudioFileOutput.Is16Bit());
                    
                    // Change!
                    x.AudioFileOutput.With16Bit(x.Context);
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is16Bit());
                    IsFalse(() => x.FlowNode.Is16Bit());
                    IsFalse(() => x.ConfigWishes.Is16Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is16Bit());
                    IsFalse(() => x.TapeConfig.Is16Bit());
                    IsFalse(() => x.TapeActions.Is16Bit());
                    IsFalse(() => x.TapeAction.Is16Bit());
                    
                    // Buff-Bound
                    IsTrue(() => x.Buff.Is16Bit());
                    IsTrue(() => x.AudioFileOutput.Is16Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is16Bit());
                    IsFalse(() => x.AudioInfoWish.Is16Bit());
                    IsFalse(() => x.AudioFileInfo.Is16Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is16Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                    IsFalse(() => x.SampleDataType.Is16Bit());
                    IsFalse(() => x.Type.Is16Bit());
                }
                
                { // After Record
                    
                    x.Record();
                    
                    // Overwritten with original SynthWishes settings.
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is16Bit());
                    IsFalse(() => x.FlowNode.Is16Bit());
                    IsFalse(() => x.ConfigWishes.Is16Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is16Bit());
                    IsFalse(() => x.TapeConfig.Is16Bit());
                    IsFalse(() => x.TapeActions.Is16Bit());
                    IsFalse(() => x.TapeAction.Is16Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is16Bit());
                    IsFalse(() => x.AudioFileOutput.Is16Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is16Bit());
                    IsFalse(() => x.AudioInfoWish.Is16Bit());
                    IsFalse(() => x.AudioFileInfo.Is16Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is16Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
                    IsFalse(() => x.SampleDataType.Is16Bit());
                    IsFalse(() => x.Type.Is16Bit());
                }
            }
        }

        [TestMethod] public void Bits_Setters_32Bit_Shorthand()
        {
            { // Check Before Change
                
                var x = new TestEntities(s => s.With16Bit());
                
                // SynthWishes-Bound
                IsFalse(() => x.SynthWishes.Is32Bit());
                IsFalse(() => x.FlowNode.Is32Bit());
                IsFalse(() => x.ConfigWishes.Is32Bit());
                
                // Tape-Bound
                IsFalse(() => x.Tape.Is32Bit());
                IsFalse(() => x.TapeConfig.Is32Bit());
                IsFalse(() => x.TapeActions.Is32Bit());
                IsFalse(() => x.TapeAction.Is32Bit());
                
                // Buff-Bound
                IsFalse(() => x.Buff.Is32Bit());
                IsFalse(() => x.AudioFileOutput.Is32Bit());
                
                // Independent after Taping
                IsFalse(() => x.Sample.Is32Bit());
                IsFalse(() => x.AudioInfoWish.Is32Bit());
                IsFalse(() => x.AudioFileInfo.Is32Bit());
                
                // Immutable
                IsFalse(() => x.WavHeader.Is32Bit());
                IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                IsFalse(() => x.SampleDataType.Is32Bit());
                IsFalse(() => x.Type.Is32Bit());
            }
            
            { // SynthWishes-Bound Change
                
                var x = new TestEntities(s => s.With16Bit());
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is32Bit());
                    IsFalse(() => x.FlowNode.Is32Bit());
                    IsFalse(() => x.ConfigWishes.Is32Bit());
                    
                    // Change!
                    x.ConfigWishes.With32Bit();
                    
                    // SynthWishes-Bound
                    IsTrue(() => x.SynthWishes.Is32Bit());
                    IsTrue(() => x.FlowNode.Is32Bit());
                    IsTrue(() => x.ConfigWishes.Is32Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is32Bit());
                    IsFalse(() => x.TapeConfig.Is32Bit());
                    IsFalse(() => x.TapeActions.Is32Bit());
                    IsFalse(() => x.TapeAction.Is32Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is32Bit());
                    IsFalse(() => x.AudioFileOutput.Is32Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is32Bit());
                    IsFalse(() => x.AudioInfoWish.Is32Bit());
                    IsFalse(() => x.AudioFileInfo.Is32Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is32Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                    IsFalse(() => x.SampleDataType.Is32Bit());
                    IsFalse(() => x.Type.Is32Bit());
                }
                
                { // After-Record
                    
                    x.Record();
                    
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
                    IsTrue(() => x.AudioInfoWish.Is32Bit());
                    IsTrue(() => x.AudioFileInfo.Is32Bit());
                    
                    // Immutable
                    IsTrue(() => x.WavHeader.Is32Bit());
                    IsTrue(() => x.SampleDataTypeEnum.Is32Bit());
                    IsTrue(() => x.SampleDataType.Is32Bit());
                    IsTrue(() => x.Type.Is32Bit());
                }
            }
            
            { // Tape-Bound Change
                
                var x = new TestEntities(s => s.With16Bit());
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is32Bit());
                    IsFalse(() => x.FlowNode.Is32Bit());
                    IsFalse(() => x.ConfigWishes.Is32Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is32Bit());
                    IsFalse(() => x.TapeConfig.Is32Bit());
                    IsFalse(() => x.TapeActions.Is32Bit());
                    IsFalse(() => x.TapeAction.Is32Bit());
                    
                    // Change!
                    x.TapeAction.With32Bit();
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is32Bit());
                    IsFalse(() => x.FlowNode.Is32Bit());
                    IsFalse(() => x.ConfigWishes.Is32Bit());
                    
                    // Tape-Bound
                    IsTrue(() => x.Tape.Is32Bit());
                    IsTrue(() => x.TapeConfig.Is32Bit());
                    IsTrue(() => x.TapeActions.Is32Bit());
                    IsTrue(() => x.TapeAction.Is32Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is32Bit());
                    IsFalse(() => x.AudioFileOutput.Is32Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is32Bit());
                    IsFalse(() => x.AudioInfoWish.Is32Bit());
                    IsFalse(() => x.AudioFileInfo.Is32Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is32Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                    IsFalse(() => x.SampleDataType.Is32Bit());
                    IsFalse(() => x.Type.Is32Bit());
                }
                
                { // After-Record
                    
                    x.Record();
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is32Bit());
                    IsFalse(() => x.FlowNode.Is32Bit());
                    IsFalse(() => x.ConfigWishes.Is32Bit());
                    
                    // By Design: Currently can't re-record over the same tape.
                    // So you always get a new tape, overwriting the changed values upon record.
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is32Bit());
                    IsFalse(() => x.TapeConfig.Is32Bit());
                    IsFalse(() => x.TapeActions.Is32Bit());
                    IsFalse(() => x.TapeAction.Is32Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is32Bit());
                    IsFalse(() => x.AudioFileOutput.Is32Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is32Bit());
                    IsFalse(() => x.AudioInfoWish.Is32Bit());
                    IsFalse(() => x.AudioFileInfo.Is32Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is32Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                    IsFalse(() => x.SampleDataType.Is32Bit());
                    IsFalse(() => x.Type.Is32Bit());
                }
            }
            
            { // Buff-Bound Change
                
                var x = new TestEntities(s => s.With16Bit());
                
                { // Change-Check
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is32Bit());
                    IsFalse(() => x.FlowNode.Is32Bit());
                    IsFalse(() => x.ConfigWishes.Is32Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is32Bit());
                    IsFalse(() => x.TapeConfig.Is32Bit());
                    IsFalse(() => x.TapeActions.Is32Bit());
                    IsFalse(() => x.TapeAction.Is32Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is32Bit());
                    IsFalse(() => x.AudioFileOutput.Is32Bit());
                    
                    // Change!
                    x.AudioFileOutput.With32Bit(x.Context);
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is32Bit());
                    IsFalse(() => x.FlowNode.Is32Bit());
                    IsFalse(() => x.ConfigWishes.Is32Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is32Bit());
                    IsFalse(() => x.TapeConfig.Is32Bit());
                    IsFalse(() => x.TapeActions.Is32Bit());
                    IsFalse(() => x.TapeAction.Is32Bit());
                    
                    // Buff-Bound
                    IsTrue(() => x.Buff.Is32Bit());
                    IsTrue(() => x.AudioFileOutput.Is32Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is32Bit());
                    IsFalse(() => x.AudioInfoWish.Is32Bit());
                    IsFalse(() => x.AudioFileInfo.Is32Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is32Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                    IsFalse(() => x.SampleDataType.Is32Bit());
                    IsFalse(() => x.Type.Is32Bit());
                }
                
                { // After Record
                    
                    x.Record();
                    
                    // Overwritten with original SynthWishes settings.
                    
                    // SynthWishes-Bound
                    IsFalse(() => x.SynthWishes.Is32Bit());
                    IsFalse(() => x.FlowNode.Is32Bit());
                    IsFalse(() => x.ConfigWishes.Is32Bit());
                    
                    // Tape-Bound
                    IsFalse(() => x.Tape.Is32Bit());
                    IsFalse(() => x.TapeConfig.Is32Bit());
                    IsFalse(() => x.TapeActions.Is32Bit());
                    IsFalse(() => x.TapeAction.Is32Bit());
                    
                    // Buff-Bound
                    IsFalse(() => x.Buff.Is32Bit());
                    IsFalse(() => x.AudioFileOutput.Is32Bit());
                    
                    // Independent after Taping
                    IsFalse(() => x.Sample.Is32Bit());
                    IsFalse(() => x.AudioInfoWish.Is32Bit());
                    IsFalse(() => x.AudioFileInfo.Is32Bit());
                    
                    // Immutable
                    IsFalse(() => x.WavHeader.Is32Bit());
                    IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
                    IsFalse(() => x.SampleDataType.Is32Bit());
                    IsFalse(() => x.Type.Is32Bit());
                }
            }
        }

        [TestMethod] public void Bits_Setters_8Bit_Shorthand_ChangingIndependentAndImmutables()
        {
            var x = new TestEntities(s => s.With32Bit());
            
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
            var x = new TestEntities(s => s.With32Bit());
            
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
            var x = new TestEntities(s => s.With16Bit());
            
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
            
            public TestEntities(Action<SynthWishes> initialize = null) 
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