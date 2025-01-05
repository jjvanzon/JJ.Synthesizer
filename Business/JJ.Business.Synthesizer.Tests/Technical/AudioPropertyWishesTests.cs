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
            
            TestEntities x;

            // Assert Setters
            {
                x = new TestEntities(s => s.WithBits(differentBits));
            
                // SynthWishes-Bound
                AreEqual(x.SynthWishes, () => x.SynthWishes.Bits(bits));
                AreEqual(x.FlowNode, () => x.FlowNode.Bits(bits));
                AreEqual(x.ConfigWishes, () => x.ConfigWishes.Bits(bits));

                // Tape-Bound
                AreEqual(x.Tape, () => x.Tape.Bits(bits));
                AreEqual(x.TapeConfig, () => x.TapeConfig.Bits(bits));
                AreEqual(x.TapeActions, () => x.TapeActions.Bits(bits));
                AreEqual(x.TapeAction, () => x.TapeAction.Bits(bits));

                // Buff-Bound
                AreEqual(x.Buff, () => x.Buff.Bits(bits, x.Context));
                AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Bits(bits, x.Context));

                // Independent after Taping
                AreEqual(x.Sample, () => x.Sample.Bits(bits, x.Context));
                AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.Bits(bits));
                AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.Bits(bits));

                // Immutable
                NotEqual(x.WavHeader, () => x.WavHeader.Bits(bits));
                NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.Bits(bits));
                NotEqual(x.SampleDataType, () => x.SampleDataType.Bits(bits, x.Context));
                NotEqual(x.Type, () => x.Type.Bits(bits));
            }
            // Assert Conversion-Style Setters
            {
                x = new TestEntities(s => s.WithBits(bits));

                AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
                AreEqual(x.SampleDataType, () => bits.BitsToEntity(x.Context));
                AreEqual(x.Type, () => bits.BitsToType());
            }

            // Assert Shorthand Setters
            {
                x = new TestEntities(s => s.WithBits(differentBits));

                // SynthWishes-Bound
                AreEqual(x.SynthWishes, () => x.SynthWishes.With8Bit());
                AreEqual(x.FlowNode, () => x.FlowNode.With8Bit());
                AreEqual(x.ConfigWishes, () => x.ConfigWishes.With8Bit());

                // Tape-Bound
                AreEqual(x.Tape, () => x.Tape.With8Bit());
                AreEqual(x.TapeConfig, () => x.TapeConfig.With8Bit());
                AreEqual(x.TapeActions, () => x.TapeActions.With8Bit());
                AreEqual(x.TapeAction, () => x.TapeAction.With8Bit());

                // Buff-Bound
                AreEqual(x.Buff, () => x.Buff.With8Bit(x.Context));
                AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With8Bit(x.Context));

                // Independent after Taping
                AreEqual(x.Sample, () => x.Sample.With8Bit(x.Context));
                AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With8Bit());
                AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With8Bit());

                // Immutable
                NotEqual(x.WavHeader, () => x.WavHeader.With8Bit());
                NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.With8Bit());
                NotEqual(x.SampleDataType, () => x.SampleDataType.With8Bit(x.Context));
                NotEqual(x.Type, () => x.Type.With8Bit());

                AreEqual(typeof(byte), () => With8Bit<byte>());
            }
        }

        [TestMethod]
        public void TestBitsSetters_8Bit_Thorough()
        {
            // TODO: Test more thoroughly, because one call can determine setting for another, making certain assertions ineffective.

            // Arrange
            int before = 16;
            int after = 8;
            
            { // Assert Setters
                
                { // Check Before Change
                    
                    var x = new TestEntities(s => s.WithBits(before));
                        
                    // SynthWishes-Bound
                    AreEqual(before, () => x.SynthWishes.Bits());
                    AreEqual(before, () => x.FlowNode.Bits());
                    AreEqual(before, () => x.ConfigWishes.Bits());
                    
                    // SynthWishes-Bound
                    AreEqual(before, () => x.SynthWishes.Bits());
                    AreEqual(before, () => x.FlowNode.Bits());
                    AreEqual(before, () => x.ConfigWishes.Bits());
                                        
                    // Tape-Bound
                    AreEqual(before, () => x.Tape.Bits());
                    AreEqual(before, () => x.TapeConfig.Bits());
                    AreEqual(before, () => x.TapeActions.Bits());
                    AreEqual(before, () => x.TapeAction.Bits());

                    // Buff-Bound
                    AreEqual(before, () => x.Buff.Bits());
                    AreEqual(before, () => x.AudioFileOutput.Bits());

                    // Independent after Taping
                    AreEqual(before, () => x.Sample.Bits());
                    AreEqual(before, () => x.AudioInfoWish.Bits());
                    AreEqual(before, () => x.AudioFileInfo.Bits());

                    // Immutable
                    AreEqual(before, () => x.WavHeader.Bits());
                    AreEqual(before, () => x.SampleDataTypeEnum.Bits());
                    AreEqual(before, () => x.SampleDataType.Bits());
                    AreEqual(before, () => x.Type.Bits());
                }
                
                { // SynthWishes-Bound Change
                    
                    var x = new TestEntities(s => s.WithBits(before));

                    { // Change-Check

                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());
                                            
                        // Change!
                        x.ConfigWishes.Bits(after);
                        
                        // SynthWishes-Bound
                        AreEqual(after, () => x.SynthWishes.Bits());
                        AreEqual(after, () => x.FlowNode.Bits());
                        AreEqual(after, () => x.ConfigWishes.Bits());
                                            
                        // Tape-Bound
                        AreEqual(before, () => x.Tape.Bits());
                        AreEqual(before, () => x.TapeConfig.Bits());
                        AreEqual(before, () => x.TapeActions.Bits());
                        AreEqual(before, () => x.TapeAction.Bits());

                        // Buff-Bound
                        AreEqual(before, () => x.Buff.Bits());
                        AreEqual(before, () => x.AudioFileOutput.Bits());

                        // Independent after Taping
                        AreEqual(before, () => x.Sample.Bits());
                        AreEqual(before, () => x.AudioInfoWish.Bits());
                        AreEqual(before, () => x.AudioFileInfo.Bits());

                        // Immutable
                        AreEqual(before, () => x.WavHeader.Bits());
                        AreEqual(before, () => x.SampleDataTypeEnum.Bits());
                        AreEqual(before, () => x.SampleDataType.Bits());
                        AreEqual(before, () => x.Type.Bits());
                    }

                    { // After-Record Checks
                        x.Record();
                        
                        // SynthWishes-Bound
                        AreEqual(after, () => x.SynthWishes.Bits());
                        AreEqual(after, () => x.FlowNode.Bits());
                        AreEqual(after, () => x.ConfigWishes.Bits());

                        // Tape-Bound
                        AreEqual(after, () => x.Tape.Bits());
                        AreEqual(after, () => x.TapeConfig.Bits());
                        AreEqual(after, () => x.TapeActions.Bits());
                        AreEqual(after, () => x.TapeAction.Bits());

                        // Buff-Bound
                        AreEqual(after, () => x.Buff.Bits());
                        AreEqual(after, () => x.AudioFileOutput.Bits());

                        // Independent after Taping
                        AreEqual(after, () => x.Sample.Bits());
                        AreEqual(after, () => x.AudioInfoWish.Bits());
                        AreEqual(after, () => x.AudioFileInfo.Bits());

                        // Immutable
                        AreEqual(after, () => x.WavHeader.Bits());
                        AreEqual(after, () => x.SampleDataTypeEnum.Bits());
                        AreEqual(after, () => x.SampleDataType.Bits());
                        AreEqual(after, () => x.Type.Bits());
                    }
                }
                
                { // Tape-Bound Change
                    
                    var x = new TestEntities(s => s.WithBits(before));

                    { // Change-Check
                        
                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());

                        // Tape-Bound
                        AreEqual(before, () => x.Tape.Bits());
                        AreEqual(before, () => x.TapeConfig.Bits());
                        AreEqual(before, () => x.TapeActions.Bits());
                        AreEqual(before, () => x.TapeAction.Bits());

                        // Change!
                        x.TapeAction.Bits(after);

                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());

                        // Tape-Bound
                        AreEqual(after, () => x.Tape.Bits());
                        AreEqual(after, () => x.TapeConfig.Bits());
                        AreEqual(after, () => x.TapeActions.Bits());
                        AreEqual(after, () => x.TapeAction.Bits());
                        
                        // Buff-Bound
                        AreEqual(before, () => x.Buff.Bits());
                        AreEqual(before, () => x.AudioFileOutput.Bits());

                        // Independent after Taping
                        AreEqual(before, () => x.Sample.Bits());
                        AreEqual(before, () => x.AudioInfoWish.Bits());
                        AreEqual(before, () => x.AudioFileInfo.Bits());

                        // Immutable
                        AreEqual(before, () => x.WavHeader.Bits());
                        AreEqual(before, () => x.SampleDataTypeEnum.Bits());
                        AreEqual(before, () => x.SampleDataType.Bits());
                        AreEqual(before, () => x.Type.Bits());
                    }
                    
                    { // After-Record Checks
                        x.Record();
                        
                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());

                        // By Design: Currently can't re-record over the same tape, so you always get a new tape.
                        
                        // Tape-Bound
                        AreEqual(before, () => x.Tape.Bits());
                        AreEqual(before, () => x.TapeConfig.Bits());
                        AreEqual(before, () => x.TapeActions.Bits());
                        AreEqual(before, () => x.TapeAction.Bits());

                        // Buff-Bound
                        AreEqual(before, () => x.Buff.Bits());
                        AreEqual(before, () => x.AudioFileOutput.Bits());

                        // Independent after Taping
                        AreEqual(before, () => x.Sample.Bits());
                        AreEqual(before, () => x.AudioInfoWish.Bits());
                        AreEqual(before, () => x.AudioFileInfo.Bits());

                        // Immutable
                        AreEqual(before, () => x.WavHeader.Bits());
                        AreEqual(before, () => x.SampleDataTypeEnum.Bits());
                        AreEqual(before, () => x.SampleDataType.Bits());
                        AreEqual(before, () => x.Type.Bits());
                    }
                }
                
                { // Buff-Bound Change
                    
                    var x = new TestEntities(s => s.WithBits(before));

                    { // Change-Check

                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());
                                            
                        // Tape-Bound
                        AreEqual(before, () => x.Tape.Bits());
                        AreEqual(before, () => x.TapeConfig.Bits());
                        AreEqual(before, () => x.TapeActions.Bits());
                        AreEqual(before, () => x.TapeAction.Bits());

                        // Buff-Bound
                        AreEqual(before, () => x.Buff.Bits());
                        AreEqual(before, () => x.AudioFileOutput.Bits());
                                            
                        // Change!
                        x.AudioFileOutput.Bits(after, x.Context);
                        
                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());
                                            
                        // Tape-Bound
                        AreEqual(before, () => x.Tape.Bits());
                        AreEqual(before, () => x.TapeConfig.Bits());
                        AreEqual(before, () => x.TapeActions.Bits());
                        AreEqual(before, () => x.TapeAction.Bits());
                        
                        // Buff-Bound
                        AreEqual(after, () => x.Buff.Bits());
                        AreEqual(after, () => x.AudioFileOutput.Bits());

                        // Independent after Taping
                        AreEqual(before, () => x.Sample.Bits());
                        AreEqual(before, () => x.AudioInfoWish.Bits());
                        AreEqual(before, () => x.AudioFileInfo.Bits());

                        // Immutable
                        AreEqual(before, () => x.WavHeader.Bits());
                        AreEqual(before, () => x.SampleDataTypeEnum.Bits());
                        AreEqual(before, () => x.SampleDataType.Bits());
                        AreEqual(before, () => x.Type.Bits());
                    }

                    { // After-Record Checks
                        x.Record();
                        
                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());

                        // Tape-Bound
                        AreEqual(before, () => x.Tape.Bits());
                        AreEqual(before, () => x.TapeConfig.Bits());
                        AreEqual(before, () => x.TapeActions.Bits());
                        AreEqual(before, () => x.TapeAction.Bits());

                        // Buff-Bound
                        AreEqual(before, () => x.Buff.Bits());
                        AreEqual(before, () => x.AudioFileOutput.Bits());

                        // Independent after Taping
                        AreEqual(before, () => x.Sample.Bits());
                        AreEqual(before, () => x.AudioInfoWish.Bits());
                        AreEqual(before, () => x.AudioFileInfo.Bits());

                        // Immutable
                        AreEqual(before, () => x.WavHeader.Bits());
                        AreEqual(before, () => x.SampleDataTypeEnum.Bits());
                        AreEqual(before, () => x.SampleDataType.Bits());
                        AreEqual(before, () => x.Type.Bits());
                    }
                }
                
                { // Independent/Immutables Change
                    
                    var x = new TestEntities(s => s.WithBits(before));

                    WavHeaderStruct wavHeaderAfter;
                    SampleDataTypeEnum sampleDataTypeEnumAfter;
                    SampleDataType sampleDataTypeAfter;
                    Type typeAfter;
                    
                    { // Change-Check

                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());
                        
                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());
                                            
                        // Tape-Bound
                        AreEqual(before, () => x.Tape.Bits());
                        AreEqual(before, () => x.TapeConfig.Bits());
                        AreEqual(before, () => x.TapeActions.Bits());
                        AreEqual(before, () => x.TapeAction.Bits());

                        // Buff-Bound
                        AreEqual(before, () => x.Buff.Bits());
                        AreEqual(before, () => x.AudioFileOutput.Bits());

                        // Independent after Taping
                        AreEqual(before, () => x.Sample.Bits());
                        x.Sample.Bits(after, x.Context);
                        AreEqual(after, () => x.Sample.Bits());
                        
                        AreEqual(before, () => x.AudioInfoWish.Bits());
                        x.AudioInfoWish.Bits(after);
                        AreEqual(after, () => x.AudioInfoWish.Bits());
                                                
                        AreEqual(before, () => x.AudioFileInfo.Bits());
                        x.AudioFileInfo.Bits(after);
                        AreEqual(after, () => x.AudioFileInfo.Bits());

                        // Immutable                        
                        AreEqual(before, () => x.WavHeader.Bits());
                        wavHeaderAfter = x.WavHeader.Bits(after);
                        AreEqual(before, () => x.WavHeader.Bits());
                        AreEqual(after, () => wavHeaderAfter.Bits());
                        
                        AreEqual(before, () => x.SampleDataTypeEnum.Bits());
                        sampleDataTypeEnumAfter = x.SampleDataTypeEnum.Bits(after);
                        AreEqual(before, () => x.SampleDataTypeEnum.Bits());
                        AreEqual(after, () => sampleDataTypeEnumAfter.Bits());

                        AreEqual(before, () => x.SampleDataType.Bits());
                        sampleDataTypeAfter = x.SampleDataType.Bits(after, x.Context);
                        AreEqual(before, () => x.SampleDataType.Bits());
                        AreEqual(after, () => sampleDataTypeAfter.Bits());

                        AreEqual(before, () => x.Type.Bits());
                        typeAfter = x.Type.Bits(after);
                        AreEqual(before, () => x.Type.Bits());
                        AreEqual(after, () => typeAfter.Bits());
                    }

                    { // After-Record Checks
                        x.Record();
                        
                        // SynthWishes-Bound
                        AreEqual(before, () => x.SynthWishes.Bits());
                        AreEqual(before, () => x.FlowNode.Bits());
                        AreEqual(before, () => x.ConfigWishes.Bits());

                        // Tape-Bound
                        AreEqual(before, () => x.Tape.Bits());
                        AreEqual(before, () => x.TapeConfig.Bits());
                        AreEqual(before, () => x.TapeActions.Bits());
                        AreEqual(before, () => x.TapeAction.Bits());

                        // Buff-Bound
                        AreEqual(before, () => x.Buff.Bits());
                        AreEqual(before, () => x.AudioFileOutput.Bits());

                        // Independent after Taping
                        AreEqual(before, () => x.Sample.Bits());
                        AreEqual(before, () => x.AudioInfoWish.Bits());
                        AreEqual(before, () => x.AudioFileInfo.Bits());

                        // Immutable
                        AreEqual(before, () => x.WavHeader.Bits());
                        AreEqual(before, () => x.SampleDataTypeEnum.Bits());
                        AreEqual(before, () => x.SampleDataType.Bits());
                        AreEqual(before, () => x.Type.Bits());
                    
                        AreEqual(after, () => wavHeaderAfter.Bits());
                        AreEqual(after, () => sampleDataTypeEnumAfter.Bits());
                        AreEqual(after, () => sampleDataTypeAfter.Bits());
                        AreEqual(after, () => typeAfter.Bits());

                    }
                }
            }

            // Assert Conversion-Style Setters
            {
                var x = new TestEntities(s => s.WithBits(after));
                
                // Immutable
                AreEqual(x.SampleDataTypeEnum, () => after.BitsToEnum());
                AreEqual(x.SampleDataType, () => after.BitsToEntity(x.Context));
                AreEqual(x.Type, () => after.BitsToType());
            }

            // Assert Shorthand Setters
            {
                var x = new TestEntities(s => s.WithBits(before));
                
                // SynthWishes-Bound
                AreEqual(x.SynthWishes, () => x.SynthWishes.With8Bit());
                AreEqual(x.FlowNode, () => x.FlowNode.With8Bit());
                AreEqual(x.ConfigWishes, () => x.ConfigWishes.With8Bit());

                // Tape-Bound
                AreEqual(x.Tape, () => x.Tape.With8Bit());
                AreEqual(x.TapeConfig, () => x.TapeConfig.With8Bit());
                AreEqual(x.TapeActions, () => x.TapeActions.With8Bit());
                AreEqual(x.TapeAction, () => x.TapeAction.With8Bit());

                // Buff-Bound
                AreEqual(x.Buff, () => x.Buff.With8Bit(x.Context));
                AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With8Bit(x.Context));

                // Independent after Taping
                AreEqual(x.Sample, () => x.Sample.With8Bit(x.Context));
                AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With8Bit());
                AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With8Bit());

                // Immutable
                NotEqual(x.WavHeader, () => x.WavHeader.With8Bit());
                NotEqual(x.SampleDataTypeEnum, () => x.SampleDataTypeEnum.With8Bit());
                NotEqual(x.SampleDataType, () => x.SampleDataType.With8Bit(x.Context));
                NotEqual(x.Type, () => x.Type.With8Bit());

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
                FlowNode     = SynthWishes.Sine(SynthWishes[440]);
                
                // Global-Bound
                ConfigSection = new ConfigWishesAccessor(ConfigWishes)._section; 

                // Initialize
                SynthWishes.WithAudioLength(1 / 440d); // Records 1 sinusoid cycle
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