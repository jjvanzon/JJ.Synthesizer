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

        [TestMethod] public void TestBitGetters()
        {
            TestBitGetters(8);
            TestBitGetters(16);
            TestBitGetters(32);
        }
        
        void TestBitGetters(int bits)
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
        
        [TestMethod] public void TestBitGetters_8BitShorthand()
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
        
        [TestMethod] public void TestBitGetters_16BitShorthand()
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
                
        [TestMethod] public void TestBitGetters_32BitShorthand()
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
                
        [TestMethod] public void TestBitGetters_FromTypeArguments()
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

        // Bit Setters
        
        [TestMethod] public void TestBitSetters_8Bit_Shallow()
        {
            // Arrange
            int bits = 8;
            int differentBits = 16;
            
            TestEntities x;
            
            // Regular Setter Methods
            {
                x = new TestEntities(s => s.WithBits(differentBits));
                
                // SynthWishes-Bound
                AreEqual(x.SynthWishes,  () => x.SynthWishes.Bits(bits));
                AreEqual(x.FlowNode,     () => x.FlowNode.Bits(bits));
                AreEqual(x.ConfigWishes, () => x.ConfigWishes.Bits(bits));
                
                // Tape-Bound
                AreEqual(x.Tape,        () => x.Tape.Bits(bits));
                AreEqual(x.TapeConfig,  () => x.TapeConfig.Bits(bits));
                AreEqual(x.TapeActions, () => x.TapeActions.Bits(bits));
                AreEqual(x.TapeAction,  () => x.TapeAction.Bits(bits));
                
                // Buff-Bound
                AreEqual(x.Buff,            () => x.Buff.Bits(bits, x.Context));
                AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Bits(bits, x.Context));
                
                // Independent after Taping
                AreEqual(x.Sample,        () => x.Sample.Bits(bits, x.Context));
                AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.Bits(bits));
                AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.Bits(bits));
                
                // Immutable
                NotEqual(x.WavHeader,          () => x.WavHeader.Bits(bits));
                NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.Bits(bits));
                NotEqual(x.SampleDataType,     () => x.SampleDataType.Bits(bits, x.Context));
                NotEqual(x.Type,               () => x.Type.Bits(bits));
            }
            
            // Conversion-Style
            {
                x = new TestEntities(s => s.WithBits(bits));
                
                AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
                AreEqual(x.SampleDataType,     () => bits.BitsToEntity(x.Context));
                AreEqual(x.Type,               () => bits.BitsToType());
            }
            
            // Shorthand
            {
                x = new TestEntities(s => s.WithBits(differentBits));
                
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
        }

        [TestMethod] public void TestBitSetters_8Bit_Thorough()
        {
            // TODO: Test more thoroughly, because one call can determine setting for another, making certain assertions ineffective.

            // Arrange
            int from = 16;
            int to = 8;
            
            { // Regular Setter Methods
                
                { // Check Before Change
                    
                    var x = new TestEntities(s => s.WithBits(from));
                        
                    // SynthWishes-Bound
                    AreEqual(from, () => x.SynthWishes.Bits());
                    AreEqual(from, () => x.FlowNode.Bits());
                    AreEqual(from, () => x.ConfigWishes.Bits());
                    
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
                
                { // SynthWishes-Bound Change
                    
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
                
                { // Tape-Bound Change
                    
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
                
                { // Buff-Bound Change
                    
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
                
                { // Independent/Immutables Change
                    
                    var x = new TestEntities(s => s.WithBits(from));

                    WavHeaderStruct wavHeaderAfter;
                    SampleDataTypeEnum sampleDataTypeEnumAfter;
                    SampleDataType sampleDataTypeAfter;
                    Type typeAfter;
                    
                    { // Change-Check

                        // SynthWishes-Bound
                        AreEqual(from, () => x.SynthWishes.Bits());
                        AreEqual(from, () => x.FlowNode.Bits());
                        AreEqual(from, () => x.ConfigWishes.Bits());
                        
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
                        wavHeaderAfter    =  x.WavHeader.Bits(to);
                        AreEqual(from, () => x.WavHeader.Bits());
                        AreEqual(to,   () => wavHeaderAfter.Bits());
                        
                        AreEqual(from, () => x.SampleDataTypeEnum.Bits());
                        sampleDataTypeEnumAfter = x.SampleDataTypeEnum.Bits(to);
                        AreEqual(from, () => x.SampleDataTypeEnum.Bits());
                        AreEqual(to,   () => sampleDataTypeEnumAfter.Bits());
                        
                        AreEqual(from, () => x.SampleDataType.Bits());
                        sampleDataTypeAfter = x.SampleDataType.Bits(to, x.Context);
                        AreEqual(from, () => x.SampleDataType.Bits());
                        AreEqual(to,   () => sampleDataTypeAfter.Bits());
                        
                        AreEqual(from, () => x.Type.Bits());
                        typeAfter = x.Type.Bits(to);
                        AreEqual(from, () => x.Type.Bits());
                        AreEqual(to,   () => typeAfter.Bits());
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
                    
                        AreEqual(to, () => wavHeaderAfter.Bits());
                        AreEqual(to, () => sampleDataTypeEnumAfter.Bits());
                        AreEqual(to, () => sampleDataTypeAfter.Bits());
                        AreEqual(to, () => typeAfter.Bits());

                    }
                }
            }

            { // Conversion-Style Setters
                
                // Immutable
                {
                    var x = new TestEntities(s => s.WithBits(from));
                    AreEqual(x.SampleDataTypeEnum, () => from.BitsToEnum());
                    AreEqual(x.SampleDataType,     () => from.BitsToEntity(x.Context));
                    AreEqual(x.Type,               () => from.BitsToType());
                }
                {
                    var x = new TestEntities(s => s.WithBits(to));
                    AreEqual(x.SampleDataTypeEnum, () => to.BitsToEnum());
                    AreEqual(x.SampleDataType,     () => to.BitsToEntity(x.Context));
                    AreEqual(x.Type,               () => to.BitsToType());
                }
            }

            { // Shorthand Setters
                
                { // Check Before Change
                
                    var x = new TestEntities(s => s.WithBits(from));
                    
                    // SynthWishes-Bound
                    AreEqual(false, () => x.SynthWishes.Is8Bit());
                    AreEqual(false, () => x.FlowNode.Is8Bit());
                    AreEqual(false, () => x.ConfigWishes.Is8Bit());

                    // Tape-Bound
                    AreEqual(false, () => x.Tape.Is8Bit());
                    AreEqual(false, () => x.TapeConfig.Is8Bit());
                    AreEqual(false, () => x.TapeActions.Is8Bit());
                    AreEqual(false, () => x.TapeAction.Is8Bit());
                    
                    // Buff-Bound
                    AreEqual(false, () => x.Buff.Is8Bit());
                    AreEqual(false, () => x.AudioFileOutput.Is8Bit());
                    
                    // Independent after Taping
                    AreEqual(false, () => x.Sample.Is8Bit());
                    AreEqual(false, () => x.AudioInfoWish.Is8Bit());
                    AreEqual(false, () => x.AudioFileInfo.Is8Bit());
                    
                    // Immutable
                    AreEqual(false, () => x.WavHeader.Is8Bit());
                    AreEqual(false, () => x.SampleDataTypeEnum.Is8Bit());
                    AreEqual(false, () => x.SampleDataType.Is8Bit());
                    AreEqual(false, () => x.Type.Is8Bit());
                }

                { // SynthWishes-Bound Change
                    
                    var x = new TestEntities(s => s.WithBits(from));

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
                    
                    var x = new TestEntities(s => s.WithBits(from));

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

                    var x = new TestEntities(s => s.WithBits(from));
                    
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

                { // Independent/Immutables Change
                    
                    var x = new TestEntities(s => s.WithBits(from));

                    WavHeaderStruct wavHeaderAfter;
                    SampleDataTypeEnum sampleDataTypeEnumAfter;
                    SampleDataType sampleDataTypeAfter;
                    Type typeAfter;

                    { // Change-Check

                        // SynthWishes-Bound
                        IsFalse(() => x.SynthWishes.Is8Bit());
                        IsFalse(() => x.FlowNode.Is8Bit());
                        IsFalse(() => x.ConfigWishes.Is8Bit());
                        
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
                        wavHeaderAfter = x.WavHeader.With8Bit();
                        IsFalse(() => x.WavHeader.Is8Bit());
                        IsTrue(() => wavHeaderAfter.Is8Bit());
                        
                        IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                        sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With8Bit();
                        IsFalse(() => x.SampleDataTypeEnum.Is8Bit());
                        IsTrue(() => sampleDataTypeEnumAfter.Is8Bit());

                        IsFalse(() => x.SampleDataType.Is8Bit());
                        sampleDataTypeAfter = x.SampleDataType.With8Bit(x.Context);
                        IsFalse(() => x.SampleDataType.Is8Bit());
                        IsTrue(() => sampleDataTypeAfter.Is8Bit());

                        IsFalse(() => x.Type.Is8Bit());
                        typeAfter = x.Type.With8Bit();
                        IsFalse(() => x.Type.Is8Bit());
                        IsTrue(() => typeAfter.Is8Bit());
                    }
                }
            }
        }
        
        [TestMethod] public void TestBitSetters_FromTypeArguments()
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