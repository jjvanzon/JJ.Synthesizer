using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
using System.Runtime.CompilerServices;
// ReSharper disable ArrangeStaticMemberQualifier
// ReSharper disable PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class MaxAmplitudeWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_MaxAmplitude(int maxAmplitude, int? bits)
        { 
            var init = (maxAmplitude, bits);
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init.maxAmplitude);
        }

        [TestMethod]
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_MaxAmplitude(int initMaxAmplitude, int? initBits, int maxAmplitude, int? bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters        (x, init.maxAmplitude);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, val .maxAmplitude);
                Assert_TapeBound_Getters  (x, init.maxAmplitude);
                Assert_BuffBound_Getters  (x, init.maxAmplitude);
                Assert_Independent_Getters(x, init.maxAmplitude);
                Assert_Immutable_Getters  (x, init.maxAmplitude);
                
                x.Record();
                Assert_All_Getters        (x, val .maxAmplitude);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetBits (val.bits)));

        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.maxAmplitude);
                
                setter(x);

                Assert_SynthBound_Getters(x, init.maxAmplitude);
                Assert_TapeBound_Getters(x, val.maxAmplitude);
                Assert_BuffBound_Getters(x, init.maxAmplitude);
                Assert_Independent_Getters(x, init.maxAmplitude);
                Assert_Immutable_Getters(x, init.maxAmplitude);
                
                x.Record();
                
                Assert_All_Getters(x, init.maxAmplitude); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetBits(val.bits)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetBits(val.bits)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetBits(val.bits)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetBits(val.bits)));
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);

                Assert_All_Getters(x, init.maxAmplitude);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.maxAmplitude);
                Assert_TapeBound_Getters(x, init.maxAmplitude);
                Assert_BuffBound_Getters(x, val.maxAmplitude);
                Assert_Independent_Getters(x, init.maxAmplitude);
                Assert_Immutable_Getters(x, init.maxAmplitude);
                
                x.Record();
                Assert_All_Getters(x, init.maxAmplitude);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetBits (val.bits, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetBits (val.bits, x.SynthBound.Context)));
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            // Independent after Taping

            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);

            // Sample
            {
                TestEntities x = default;
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.maxAmplitude);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.Sample, val.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init.maxAmplitude);
                    Assert_Immutable_Getters(x, init.maxAmplitude);

                    x.Record();
                    Assert_All_Getters(x, init.maxAmplitude);
                }
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetBits(val.bits, x.SynthBound.Context)));
            }
            
            // AudioInfoWish
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.maxAmplitude);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.Sample, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, val.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init.maxAmplitude);
                    Assert_Immutable_Getters(x, init.maxAmplitude);

                    x.Record();
                    Assert_All_Getters(x, init.maxAmplitude);
                }
                
                AssertProp(() => x.Independent.AudioInfoWish.Bits = val.bits);
            }
                        
            // AudioFileInfo
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.maxAmplitude);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.Sample, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, val.maxAmplitude);
                    Assert_Immutable_Getters(x, init.maxAmplitude);

                    x.Record();
                    Assert_All_Getters(x, init.maxAmplitude);
                }

                AssertProp(() => x.Independent.AudioFileInfo.SetBits(val.bits));
                AssertProp(() => x.Independent.AudioFileInfo.BytesPerValue = val.bits / 8);
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Immutable_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);
            var x = CreateTestEntities(init);

            // WavHeader
            
            var wavHeaders = new List<WavHeaderStruct>();
            {
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init.maxAmplitude);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init.maxAmplitude);
                    Assert_Immutable_Getters(wavHeader2, val.maxAmplitude);
                    
                    wavHeaders.Add(wavHeader2);
                }
                
                AssertProp(() => x.Immutable.WavHeader.SetBits(val.bits));
            }

            // SampleDataTypeEnum

            var sampleDataTypeEnums = new List<SampleDataTypeEnum>();
            {
                void AssertProp(Func<SampleDataTypeEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init.maxAmplitude);
                    
                    SampleDataTypeEnum sampleDataTypeEnum2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init.maxAmplitude);
                    Assert_Immutable_Getters(sampleDataTypeEnum2, val.maxAmplitude);
                    
                    sampleDataTypeEnums.Add(sampleDataTypeEnum2);
                }
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.SetBits(val.bits));
            }

            // SampleDataType

            var sampleDataTypes = new List<SampleDataType>();
            {
                void AssertProp(Func<SampleDataType> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init.maxAmplitude);

                    SampleDataType sampleDataType2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init.maxAmplitude);
                    Assert_Immutable_Getters(sampleDataType2, val.maxAmplitude);
                    
                    sampleDataTypes.Add(sampleDataType2);
                }
            
                AssertProp(() => x.Immutable.SampleDataType.SetBits(val.bits, x.SynthBound.Context));
            }

            // Type

            var types = new List<Type>();
            {
                void AssertProp(Func<Type> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.Type, init.maxAmplitude);
                    
                    var type2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.Type, init.maxAmplitude);
                    Assert_Immutable_Getters(type2, val.maxAmplitude);
                    
                    types.Add(type2);
                }

                AssertProp(() => x.Immutable.Type.SetBits(val.bits));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init.maxAmplitude);
            
            // Except for our variables
            wavHeaders         .ForEach(w => Assert_Immutable_Getters(w, val.maxAmplitude));
            sampleDataTypeEnums.ForEach(e => Assert_Immutable_Getters(e, val.maxAmplitude));
            sampleDataTypes    .ForEach(s => Assert_Immutable_Getters(s, val.maxAmplitude));
            types              .ForEach(t => Assert_Immutable_Getters(t, val.maxAmplitude));
        }
        [TestMethod]
        public void MaxAmplitude_WithTypeArguments()
        {
            AreEqual(byte .MaxValue/ 2, () =>    MaxAmplitude<byte> ());
            AreEqual(byte .MaxValue/ 2, () =>  ToMaxAmplitude<byte> ());
            AreEqual(byte .MaxValue/ 2, () => GetMaxAmplitude<byte> ());
            AreEqual(short.MaxValue,    () =>    MaxAmplitude<short>());
            AreEqual(short.MaxValue,    () =>  ToMaxAmplitude<short>());
            AreEqual(short.MaxValue,    () => GetMaxAmplitude<short>());
            AreEqual(1,                 () =>    MaxAmplitude<float>());
            AreEqual(1,                 () =>  ToMaxAmplitude<float>());
            AreEqual(1,                 () => GetMaxAmplitude<float>());
            AreEqual(byte .MaxValue/ 2, () => ConfigWishes.   MaxAmplitude<byte> ());
            AreEqual(byte .MaxValue/ 2, () => ConfigWishes. ToMaxAmplitude<byte> ());
            AreEqual(byte .MaxValue/ 2, () => ConfigWishes.GetMaxAmplitude<byte> ());
            AreEqual(short.MaxValue,    () => ConfigWishes.   MaxAmplitude<short>());
            AreEqual(short.MaxValue,    () => ConfigWishes. ToMaxAmplitude<short>());
            AreEqual(short.MaxValue,    () => ConfigWishes.GetMaxAmplitude<short>());
            AreEqual(1,                 () => ConfigWishes.   MaxAmplitude<float>());
            AreEqual(1,                 () => ConfigWishes. ToMaxAmplitude<float>());
            AreEqual(1,                 () => ConfigWishes.GetMaxAmplitude<float>());

        }
        
        [TestMethod]
        public void ConfigSection_MaxAmplitude()
        {
            // Immutable. Get-only.
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            AreEqual(DefaultMaxAmplitude, () => configSection.MaxAmplitude());
            AreEqual(DefaultMaxAmplitude, () => configSection.GetMaxAmplitude());
            AreEqual(DefaultMaxAmplitude, () => MaxAmplitude(configSection));
            AreEqual(DefaultMaxAmplitude, () => GetMaxAmplitude(configSection));
            AreEqual(DefaultMaxAmplitude, () => ConfigWishesAccessor.MaxAmplitude(configSection));
            AreEqual(DefaultMaxAmplitude, () => ConfigWishesAccessor.GetMaxAmplitude(configSection));
        }

        [TestMethod]
        public void Default_MaxAmplitude()
        {
            AreEqual(1, () => DefaultMaxAmplitude);
        }

        [TestMethod]
        public void MaxAmplitude_EdgeCases()
        {
            ThrowsException(() => GetMaxAmplitude(bits: -1), "Bits = -1 not valid. Supported values: 8, 16, 32");
        }

        // Getter Helpers

        private void Assert_All_Getters(TestEntities x, int maxAmplitude)
        {
            Assert_Bound_Getters(x, maxAmplitude);
            Assert_Independent_Getters(x, maxAmplitude);
            Assert_Immutable_Getters(x, maxAmplitude);
        }

        private void Assert_Bound_Getters(TestEntities x, int maxAmplitude)
        {
            Assert_SynthBound_Getters(x, maxAmplitude);
            Assert_TapeBound_Getters(x, maxAmplitude);
            Assert_BuffBound_Getters(x, maxAmplitude);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int maxAmplitude)
        {
            // Independent after Taping
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            Assert_Independent_Getters(x.Independent.Sample, maxAmplitude);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, maxAmplitude);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, maxAmplitude);
        }

        private void Assert_Immutable_Getters(TestEntities x, int maxAmplitude)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.SampleDataType, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.Bits, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.Type, maxAmplitude);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int maxAmplitude)
        {
            IsNotNull(             () => x);
            IsNotNull(             () => x.SynthBound);
            IsNotNull(             () => x.SynthBound.SynthWishes);
            IsNotNull(             () => x.SynthBound.FlowNode);
            IsNotNull(             () => x.SynthBound.ConfigResolver);
            AreEqual(maxAmplitude, () => x.SynthBound.SynthWishes   .GetMaxAmplitude);
            AreEqual(maxAmplitude, () => x.SynthBound.FlowNode      .GetMaxAmplitude);
            AreEqual(maxAmplitude, () => x.SynthBound.SynthWishes   .GetMaxAmplitude());
            AreEqual(maxAmplitude, () => x.SynthBound.FlowNode      .GetMaxAmplitude());
            AreEqual(maxAmplitude, () => x.SynthBound.ConfigResolver.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => x.SynthBound.SynthWishes   .MaxAmplitude   ());
            AreEqual(maxAmplitude, () => x.SynthBound.FlowNode      .MaxAmplitude   ());
            AreEqual(maxAmplitude, () => x.SynthBound.ConfigResolver.MaxAmplitude   ());
            AreEqual(maxAmplitude, () => MaxAmplitude   (x.SynthBound.SynthWishes   ));
            AreEqual(maxAmplitude, () => MaxAmplitude   (x.SynthBound.FlowNode      ));
            AreEqual(maxAmplitude, () => MaxAmplitude   (x.SynthBound.ConfigResolver));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(x.SynthBound.SynthWishes   ));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(x.SynthBound.FlowNode      ));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(x.SynthBound.ConfigResolver));
            AreEqual(maxAmplitude, () => ConfigWishes        .MaxAmplitude   (x.SynthBound.SynthWishes   ));
            AreEqual(maxAmplitude, () => ConfigWishes        .MaxAmplitude   (x.SynthBound.FlowNode      ));
            AreEqual(maxAmplitude, () => ConfigWishesAccessor.MaxAmplitude   (x.SynthBound.ConfigResolver));
            AreEqual(maxAmplitude, () => ConfigWishes        .GetMaxAmplitude(x.SynthBound.SynthWishes   ));
            AreEqual(maxAmplitude, () => ConfigWishes        .GetMaxAmplitude(x.SynthBound.FlowNode      ));
            AreEqual(maxAmplitude, () => ConfigWishesAccessor.GetMaxAmplitude(x.SynthBound.ConfigResolver));
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int maxAmplitude)
        {
            IsNotNull(             () => x);
            IsNotNull(             () => x.TapeBound);
            IsNotNull(             () => x.TapeBound.Tape);
            IsNotNull(             () => x.TapeBound.TapeConfig);
            IsNotNull(             () => x.TapeBound.TapeActions);
            IsNotNull(             () => x.TapeBound.TapeAction);
            AreEqual(maxAmplitude, () => x.TapeBound.Tape       .MaxAmplitude   ());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeConfig .MaxAmplitude   ());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeActions.MaxAmplitude   ());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeAction .MaxAmplitude   ());
            AreEqual(maxAmplitude, () => x.TapeBound.Tape       .GetMaxAmplitude());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeConfig .GetMaxAmplitude());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeActions.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeAction .GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude   (x.TapeBound.Tape       ));
            AreEqual(maxAmplitude, () => MaxAmplitude   (x.TapeBound.TapeConfig ));
            AreEqual(maxAmplitude, () => MaxAmplitude   (x.TapeBound.TapeActions));
            AreEqual(maxAmplitude, () => MaxAmplitude   (x.TapeBound.TapeAction ));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(x.TapeBound.Tape       ));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(x.TapeBound.TapeConfig ));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(x.TapeBound.TapeActions));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(x.TapeBound.TapeAction ));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude   (x.TapeBound.Tape       ));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude   (x.TapeBound.TapeConfig ));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude   (x.TapeBound.TapeActions));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude   (x.TapeBound.TapeAction ));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(x.TapeBound.Tape       ));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(x.TapeBound.TapeConfig ));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(x.TapeBound.TapeActions));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(x.TapeBound.TapeAction ));
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int maxAmplitude)
        {
            IsNotNull(             () => x);
            IsNotNull(             () => x.BuffBound);
            IsNotNull(             () => x.BuffBound.Buff);
            AreEqual(maxAmplitude, () => x.BuffBound.Buff           .MaxAmplitude   ());
            AreEqual(maxAmplitude, () => x.BuffBound.AudioFileOutput.MaxAmplitude   ());
            AreEqual(maxAmplitude, () => x.BuffBound.Buff           .GetMaxAmplitude());
            AreEqual(maxAmplitude, () => x.BuffBound.AudioFileOutput.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude   (x.BuffBound.Buff           ));
            AreEqual(maxAmplitude, () => MaxAmplitude   (x.BuffBound.AudioFileOutput));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(x.BuffBound.Buff           ));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(x.BuffBound.AudioFileOutput));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude   (x.BuffBound.Buff           ));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude   (x.BuffBound.AudioFileOutput));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(x.BuffBound.Buff           ));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(x.BuffBound.AudioFileOutput));
        }
        
        private void Assert_Independent_Getters(Sample sample, int maxAmplitude)
        {
            IsNotNull(             () => sample);
            AreEqual(maxAmplitude, () => sample.MaxAmplitude   ());
            AreEqual(maxAmplitude, () => sample.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude   (sample));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(sample));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude   (sample));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(sample));
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int maxAmplitude)
        {
            IsNotNull(             () => audioInfoWish);
            AreEqual(maxAmplitude, () => audioInfoWish.MaxAmplitude   ());
            AreEqual(maxAmplitude, () => audioInfoWish.ToMaxAmplitude ());
            AreEqual(maxAmplitude, () => audioInfoWish.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude   (audioInfoWish));
            AreEqual(maxAmplitude, () => ToMaxAmplitude (audioInfoWish));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(audioInfoWish));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude   (audioInfoWish));
            AreEqual(maxAmplitude, () => ConfigWishes.ToMaxAmplitude (audioInfoWish));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(audioInfoWish));
        }
        
        void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int maxAmplitude)
        {
            IsNotNull(             () => audioFileInfo);
            AreEqual(maxAmplitude, () => audioFileInfo.MaxAmplitude   ());
            AreEqual(maxAmplitude, () => audioFileInfo.ToMaxAmplitude ());
            AreEqual(maxAmplitude, () => audioFileInfo.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude   (audioFileInfo));
            AreEqual(maxAmplitude, () => ToMaxAmplitude (audioFileInfo));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(audioFileInfo));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude   (audioFileInfo));
            AreEqual(maxAmplitude, () => ConfigWishes.ToMaxAmplitude (audioFileInfo));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(audioFileInfo));
        }
        
        void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int maxAmplitude)
        {
            AreEqual(maxAmplitude, () => wavHeader.MaxAmplitude());
            AreEqual(maxAmplitude, () => wavHeader.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude(wavHeader));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(wavHeader));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude(wavHeader));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(wavHeader));
        }
        
        private void Assert_Immutable_Getters(SampleDataTypeEnum sampleDataTypeEnum, int maxAmplitude)
        {
            AreEqual(maxAmplitude, () => sampleDataTypeEnum.MaxAmplitude());
            AreEqual(maxAmplitude, () => sampleDataTypeEnum.ToMaxAmplitude());
            AreEqual(maxAmplitude, () => sampleDataTypeEnum.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude(sampleDataTypeEnum));
            AreEqual(maxAmplitude, () => ToMaxAmplitude(sampleDataTypeEnum));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(sampleDataTypeEnum));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude(sampleDataTypeEnum));
            AreEqual(maxAmplitude, () => ConfigWishes.ToMaxAmplitude(sampleDataTypeEnum));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(sampleDataTypeEnum));
        }
        
        private void Assert_Immutable_Getters(SampleDataType sampleDataType, int maxAmplitude)
        {
            IsNotNull(             () => sampleDataType);
            AreEqual(maxAmplitude, () => sampleDataType.MaxAmplitude());
            AreEqual(maxAmplitude, () => sampleDataType.ToMaxAmplitude());
            AreEqual(maxAmplitude, () => sampleDataType.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude(sampleDataType));
            AreEqual(maxAmplitude, () => ToMaxAmplitude(sampleDataType));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(sampleDataType));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude(sampleDataType));
            AreEqual(maxAmplitude, () => ConfigWishes.ToMaxAmplitude(sampleDataType));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(sampleDataType));
        }
        
        private void Assert_Immutable_Getters(int bits, int maxAmplitude)
        {
            AreEqual(maxAmplitude, () => bits.MaxAmplitude());
            AreEqual(maxAmplitude, () => bits.ToMaxAmplitude());
            AreEqual(maxAmplitude, () => bits.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude(bits));
            AreEqual(maxAmplitude, () => ToMaxAmplitude(bits));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(bits));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude(bits));
            AreEqual(maxAmplitude, () => ConfigWishes.ToMaxAmplitude(bits));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(bits));

            int? nullyBits = bits;
            AreEqual(maxAmplitude, () => nullyBits.MaxAmplitude());
            AreEqual(maxAmplitude, () => nullyBits.ToMaxAmplitude());
            AreEqual(maxAmplitude, () => nullyBits.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude(nullyBits));
            AreEqual(maxAmplitude, () => ToMaxAmplitude(nullyBits));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(nullyBits));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude(nullyBits));
            AreEqual(maxAmplitude, () => ConfigWishes.ToMaxAmplitude(nullyBits));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(nullyBits));

            nullyBits = null;
            IsNull(() => nullyBits.MaxAmplitude());
            IsNull(() => nullyBits.ToMaxAmplitude());
            IsNull(() => nullyBits.GetMaxAmplitude());
            IsNull(() => MaxAmplitude(nullyBits));
            IsNull(() => ToMaxAmplitude(nullyBits));
            IsNull(() => GetMaxAmplitude(nullyBits));
            IsNull(() => ConfigWishes.MaxAmplitude(nullyBits));
            IsNull(() => ConfigWishes.ToMaxAmplitude(nullyBits));
            IsNull(() => ConfigWishes.GetMaxAmplitude(nullyBits));

            nullyBits = 0;
            AreEqual(0, () => nullyBits.MaxAmplitude());
            AreEqual(0, () => nullyBits.ToMaxAmplitude());
            AreEqual(0, () => nullyBits.GetMaxAmplitude());
            AreEqual(0, () => MaxAmplitude(nullyBits));
            AreEqual(0, () => ToMaxAmplitude(nullyBits));
            AreEqual(0, () => GetMaxAmplitude(nullyBits));
            AreEqual(0, () => ConfigWishes.MaxAmplitude(nullyBits));
            AreEqual(0, () => ConfigWishes.ToMaxAmplitude(nullyBits));
            AreEqual(0, () => ConfigWishes.GetMaxAmplitude(nullyBits));
        }

        private void Assert_Immutable_Getters(Type type, int maxAmplitude)
        {
            IsNotNull(             () => type);
            AreEqual(maxAmplitude, () => type.MaxAmplitude());
            AreEqual(maxAmplitude, () => type.ToMaxAmplitude());
            AreEqual(maxAmplitude, () => type.GetMaxAmplitude());
            AreEqual(maxAmplitude, () => MaxAmplitude(type));
            AreEqual(maxAmplitude, () => ToMaxAmplitude(type));
            AreEqual(maxAmplitude, () => GetMaxAmplitude(type));
            AreEqual(maxAmplitude, () => ConfigWishes.MaxAmplitude(type));
            AreEqual(maxAmplitude, () => ConfigWishes.ToMaxAmplitude(type));
            AreEqual(maxAmplitude, () => ConfigWishes.GetMaxAmplitude(type));
        }
        
         // Test Data Helpers

        private TestEntities CreateTestEntities((double maxAmplitude, int? bits) init, [CallerMemberName] string name = null)
            => new TestEntities(x => x.NoLog().Bits(init.bits).SamplingRate(HighPerfHz), name);
        
        // ncrunch: no coverage start
        
        static object TestParametersInit => new[]
        {
            new object[] { byte .MaxValue / 2 ,    8 },
            new object[] { short.MaxValue     ,   16 },
            new object[] { 1                  ,   32 },
            new object[] { 1                  , null },
            new object[] { 1                  ,    0 }
        };
        
        static object TestParameters => new[] 
        {
            new object[] { 1                  , 32 , short.MaxValue    ,   16 },
            new object[] { short.MaxValue     , 16 , 1                 ,   32 },
            new object[] { byte .MaxValue / 2 ,  8 , short.MaxValue    ,   16 },
            new object[] { byte .MaxValue / 2 ,  8 , 1                 ,   32 },
            new object[] { 1                  , 32 , byte.MaxValue / 2 ,    8 },
        };
        
        static object TestParametersWithEmpty => new[] 
        {
            new object[] { 1                  , 32 , short.MaxValue    ,   16 },
            new object[] { short.MaxValue     , 16 , 1                 ,   32 },
            new object[] { byte .MaxValue / 2 ,  8 , short.MaxValue    ,   16 },
            new object[] { byte .MaxValue / 2 ,  8 , 1                 ,   32 },
            new object[] { 1                  ,  0 , short.MaxValue    ,   16 },
            new object[] { byte .MaxValue / 2 ,  8 , 1                 , null },
            new object[] { 1                  , 32 , byte.MaxValue / 2 ,    8 },
        };
        
         // ncrunch: no coverage end
    } 
}