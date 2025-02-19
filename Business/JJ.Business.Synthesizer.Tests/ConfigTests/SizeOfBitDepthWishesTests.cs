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
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
// ReSharper disable ArrangeStaticMemberQualifier

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class SizeOfBitDepthWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_SizeOfBitDepth(int? init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, CoalesceSizeOfBitDepth(init));
        }

        [TestMethod]
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_SizeOfBitDepth(int? init, int? value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters        (x, CoalesceSizeOfBitDepth(init ));
                
                setter(x);
                
                Assert_SynthBound_Getters (x, CoalesceSizeOfBitDepth(value));
                Assert_TapeBound_Getters  (x, CoalesceSizeOfBitDepth(init ));
                Assert_BuffBound_Getters  (x, CoalesceSizeOfBitDepth(init ));
                Assert_Independent_Getters(x, CoalesceSizeOfBitDepth(init ));
                Assert_Immutable_Getters  (x, CoalesceSizeOfBitDepth(init ));
                
                x.Record();
                Assert_All_Getters        (x, CoalesceSizeOfBitDepth(value));
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SizeOfBitDepth    (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SizeOfBitDepth    (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SizeOfBitDepth    (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithSizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithSizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithSizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetSizeOfBitDepth (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetSizeOfBitDepth (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetSizeOfBitDepth (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SizeOfBitDepth    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SizeOfBitDepth    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SizeOfBitDepth    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithSizeOfBitDepth(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithSizeOfBitDepth(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithSizeOfBitDepth(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetSizeOfBitDepth (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetSizeOfBitDepth (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetSizeOfBitDepth (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SizeOfBitDepth    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SizeOfBitDepth    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SizeOfBitDepth    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithSizeOfBitDepth(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithSizeOfBitDepth(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithSizeOfBitDepth(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetSizeOfBitDepth (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetSizeOfBitDepth (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetSizeOfBitDepth (x.SynthBound.ConfigResolver, value)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetBits(value * 8)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetBits(value * 8)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetBits(value * 8)));
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_SizeOfBitDepth(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, value);
                Assert_BuffBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SizeOfBitDepth    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SizeOfBitDepth    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SizeOfBitDepth    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SizeOfBitDepth    (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithSizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithSizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithSizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithSizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetSizeOfBitDepth (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetSizeOfBitDepth (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetSizeOfBitDepth (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetSizeOfBitDepth (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SizeOfBitDepth    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SizeOfBitDepth    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SizeOfBitDepth    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SizeOfBitDepth    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithSizeOfBitDepth(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithSizeOfBitDepth(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithSizeOfBitDepth(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithSizeOfBitDepth(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetSizeOfBitDepth (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetSizeOfBitDepth (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetSizeOfBitDepth (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetSizeOfBitDepth (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SizeOfBitDepth    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SizeOfBitDepth    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SizeOfBitDepth    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SizeOfBitDepth    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithSizeOfBitDepth(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithSizeOfBitDepth(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithSizeOfBitDepth(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithSizeOfBitDepth(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetSizeOfBitDepth (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetSizeOfBitDepth (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetSizeOfBitDepth (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetSizeOfBitDepth (x.TapeBound.TapeAction , value)));
            
            AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetBits(value * 8)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetBits(value * 8)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetBits(value * 8)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetBits(value * 8)));
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_SizeOfBitDepth(int init, int value)
        {
            IContext context = null;
            void AssertProp(Action<BuffBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                context = x.SynthBound.Context;

                Assert_All_Getters(x, init);
                
                setter(x.BuffBound);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, init);
                Assert_BuffBound_Getters(x, value);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init);
            }
            
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .SizeOfBitDepth    (value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SizeOfBitDepth    (value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .WithSizeOfBitDepth(value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithSizeOfBitDepth(value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .SetSizeOfBitDepth (value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetSizeOfBitDepth (value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => SizeOfBitDepth    (x.Buff           , value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => SizeOfBitDepth    (x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => WithSizeOfBitDepth(x.Buff           , value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => WithSizeOfBitDepth(x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => SetSizeOfBitDepth (x.Buff           , value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => SetSizeOfBitDepth (x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.SizeOfBitDepth    (x.Buff           , value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.SizeOfBitDepth    (x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.WithSizeOfBitDepth(x.Buff           , value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.WithSizeOfBitDepth(x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.SetSizeOfBitDepth (x.Buff           , value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.SetSizeOfBitDepth (x.AudioFileOutput, value, context)));

            AssertProp(x => AreEqual(x.Buff,            x.Buff           .SetBits(value * 8, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, x.AudioFileOutput.SetBits(value * 8, context)));
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_SizeOfBitDepth(int init, int value)
        {
            // Independent after Taping

            // Sample
            {
                TestEntities x = default;
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, value);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SizeOfBitDepth    (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithSizeOfBitDepth(value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetSizeOfBitDepth (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SizeOfBitDepth    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithSizeOfBitDepth(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetSizeOfBitDepth (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SizeOfBitDepth    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithSizeOfBitDepth(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetSizeOfBitDepth (x.Independent.Sample, value, x.SynthBound.Context)));
                
                AssertProp(() => AreEqual(x.Independent.Sample, x.Independent.Sample.SetBits(value * 8, x.SynthBound.Context)));
            }
            
            // AudioInfoWish
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, value);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SizeOfBitDepth    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithSizeOfBitDepth(value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetSizeOfBitDepth (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SizeOfBitDepth    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => WithSizeOfBitDepth(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SetSizeOfBitDepth (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SizeOfBitDepth    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => WithSizeOfBitDepth(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SetSizeOfBitDepth (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SizeOfBitDepth    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithSizeOfBitDepth(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetSizeOfBitDepth (x.Independent.AudioInfoWish, value)));

                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, x.Independent.AudioInfoWish.SetBits(value * 8)));
            }
                        
            // AudioFileInfo
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, value);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SizeOfBitDepth    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithSizeOfBitDepth(value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetSizeOfBitDepth (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => SizeOfBitDepth    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => WithSizeOfBitDepth(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => SetSizeOfBitDepth (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SizeOfBitDepth    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithSizeOfBitDepth(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetSizeOfBitDepth (x.Independent.AudioFileInfo, value)));
                
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, x.Independent.AudioFileInfo.SetBits(value * 8)));
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Immutable_SizeOfBitDepth(int init, int value)
        {
            var x = CreateTestEntities(init);

            // WavHeader
            
            var wavHeaders = new List<WavHeaderStruct>();
            {
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    Assert_Immutable_Getters(wavHeader2, value);
                    
                    wavHeaders.Add(wavHeader2);
                }
                
                AssertProp(() => x.Immutable.WavHeader.SizeOfBitDepth    (value));
                AssertProp(() => x.Immutable.WavHeader.WithSizeOfBitDepth(value));
                AssertProp(() => x.Immutable.WavHeader.SetSizeOfBitDepth (value));
                AssertProp(() => SizeOfBitDepth    (x.Immutable.WavHeader, value));
                AssertProp(() => WithSizeOfBitDepth(x.Immutable.WavHeader, value));
                AssertProp(() => SetSizeOfBitDepth (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.SizeOfBitDepth    (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.WithSizeOfBitDepth(x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.SetSizeOfBitDepth (x.Immutable.WavHeader, value));

                AssertProp(() => x.Immutable.WavHeader.SetBits(value * 8));
            }

            // SampleDataTypeEnum

            var sampleDataTypeEnums = new List<SampleDataTypeEnum>();
            {
                void AssertProp(Func<SampleDataTypeEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init);
                    
                    SampleDataTypeEnum sampleDataTypeEnum2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init);
                    Assert_Immutable_Getters(sampleDataTypeEnum2, value);
                    
                    sampleDataTypeEnums.Add(sampleDataTypeEnum2);
                }
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.SizeOfBitDepth    (value));
                AssertProp(() => x.Immutable.SampleDataTypeEnum.WithSizeOfBitDepth(value));
                AssertProp(() => x.Immutable.SampleDataTypeEnum.SetSizeOfBitDepth (value));
                AssertProp(() => SizeOfBitDepth    (x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => WithSizeOfBitDepth(x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => SetSizeOfBitDepth (x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => ConfigWishes.SizeOfBitDepth    (x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => ConfigWishes.WithSizeOfBitDepth(x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => ConfigWishes.SetSizeOfBitDepth (x.Immutable.SampleDataTypeEnum, value));

                AssertProp(() => x.Immutable.SampleDataTypeEnum.SetBits(value * 8));
            }

            // SampleDataType

            var sampleDataTypes = new List<SampleDataType>();
            {
                void AssertProp(Func<SampleDataType> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init);

                    SampleDataType sampleDataType2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init);
                    Assert_Immutable_Getters(sampleDataType2, value);
                    
                    sampleDataTypes.Add(sampleDataType2);
                }
            
                AssertProp(() => x.Immutable.SampleDataType.SizeOfBitDepth    (value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SampleDataType.WithSizeOfBitDepth(value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SampleDataType.SetSizeOfBitDepth (value, x.SynthBound.Context));
                AssertProp(() => SizeOfBitDepth    (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => WithSizeOfBitDepth(x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => SetSizeOfBitDepth (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.SizeOfBitDepth    (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.WithSizeOfBitDepth(x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.SetSizeOfBitDepth (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                
                AssertProp(() => x.Immutable.SampleDataType.SetBits(value * 8, x.SynthBound.Context));
            }
            
            // Type

            var types = new List<Type>();
            {
                void AssertProp(Func<Type> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.Type, init);
                    
                    var type2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.Type, init);
                    Assert_Immutable_Getters(type2, value);
                    
                    types.Add(type2);
                }

                AssertProp(() => x.Immutable.Type.SizeOfBitDepth    (value));
                AssertProp(() => x.Immutable.Type.WithSizeOfBitDepth(value));
                AssertProp(() => x.Immutable.Type.AsSizeOfBitDepth  (value));
                AssertProp(() => x.Immutable.Type.ToSizeOfBitDepth  (value));
                AssertProp(() => x.Immutable.Type.SetSizeOfBitDepth (value));
                AssertProp(() => SizeOfBitDepth    (x.Immutable.Type, value));
                AssertProp(() => WithSizeOfBitDepth(x.Immutable.Type, value));
                AssertProp(() => AsSizeOfBitDepth  (x.Immutable.Type, value));
                AssertProp(() => ToSizeOfBitDepth  (x.Immutable.Type, value));
                AssertProp(() => SetSizeOfBitDepth (x.Immutable.Type, value));
                AssertProp(() => ConfigWishes.SizeOfBitDepth    (x.Immutable.Type, value));
                AssertProp(() => ConfigWishes.WithSizeOfBitDepth(x.Immutable.Type, value));
                AssertProp(() => ConfigWishes.AsSizeOfBitDepth  (x.Immutable.Type, value));
                AssertProp(() => ConfigWishes.ToSizeOfBitDepth  (x.Immutable.Type, value));
                AssertProp(() => ConfigWishes.SetSizeOfBitDepth (x.Immutable.Type, value));

                AssertProp(() => x.Immutable.Type.SetBits(value * 8));
            }
                
            // Bits

            var bitsList = new List<int>();
            {
                void AssertProp(Func<int> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.Bits, init);

                    int bits2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.Bits, init);
                    Assert_Immutable_Getters(bits2, value);
                    
                    bitsList.Add(bits2);
                }
            
                AssertProp(() => x.Immutable.Bits.SizeOfBitDepth    (value));
                AssertProp(() => x.Immutable.Bits.WithSizeOfBitDepth(value));
                AssertProp(() => x.Immutable.Bits.SetSizeOfBitDepth (value));
                AssertProp(() => SizeOfBitDepth    (x.Immutable.Bits, value));
                AssertProp(() => WithSizeOfBitDepth(x.Immutable.Bits, value));
                AssertProp(() => SetSizeOfBitDepth (x.Immutable.Bits, value));
                AssertProp(() => SizeOfBitDepthToBits(value));
                AssertProp(() => ConfigWishes.SizeOfBitDepth    (x.Immutable.Bits, value));
                AssertProp(() => ConfigWishes.WithSizeOfBitDepth(x.Immutable.Bits, value));
                AssertProp(() => ConfigWishes.SetSizeOfBitDepth (x.Immutable.Bits, value));
            }
            
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders         .ForEach(w => Assert_Immutable_Getters(w, value));
            sampleDataTypeEnums.ForEach(e => Assert_Immutable_Getters(e, value));
            sampleDataTypes    .ForEach(s => Assert_Immutable_Getters(s, value));
            types              .ForEach(t => Assert_Immutable_Getters(t, value));
            bitsList           .ForEach(b => Assert_Immutable_Getters(b, value));
        }
        
        [TestMethod]
        public void ConfigSection_SizeOfBitDepth()
        {
            var x = CreateTestEntities(default);
            AreEqual(DefaultSizeOfBitDepth, () => x.SynthBound.ConfigSection.SizeOfBitDepth   ());
            AreEqual(DefaultSizeOfBitDepth, () => x.SynthBound.ConfigSection.GetSizeOfBitDepth());
            AreEqual(DefaultSizeOfBitDepth, () => SizeOfBitDepth   (x.SynthBound.ConfigSection));
            AreEqual(DefaultSizeOfBitDepth, () => GetSizeOfBitDepth(x.SynthBound.ConfigSection));
            AreEqual(DefaultSizeOfBitDepth, () => ConfigWishesAccessor.SizeOfBitDepth   (x.SynthBound.ConfigSection));
            AreEqual(DefaultSizeOfBitDepth, () => ConfigWishesAccessor.GetSizeOfBitDepth(x.SynthBound.ConfigSection));
        }

        [TestMethod]
        public void ConfigSection_Default()
        { 
            AreEqual(4, () => DefaultSizeOfBitDepth);
        }

        [TestMethod]
        public void SizeOfBitDepth_WithTypeArguments()
        {
            // Getters
            AreEqual(1, () => SizeOfBitDepth<byte>());
            AreEqual(2, () => SizeOfBitDepth<short>());
            AreEqual(4, () => SizeOfBitDepth<float>());
            AreEqual(1, () => GetSizeOfBitDepth<byte>());
            AreEqual(2, () => GetSizeOfBitDepth<short>());
            AreEqual(4, () => GetSizeOfBitDepth<float>());
            AreEqual(1, () => AsSizeOfBitDepth<byte>());
            AreEqual(2, () => AsSizeOfBitDepth<short>());
            AreEqual(4, () => AsSizeOfBitDepth<float>());
            AreEqual(1, () => ToSizeOfBitDepth<byte>());
            AreEqual(2, () => ToSizeOfBitDepth<short>());
            AreEqual(4, () => ToSizeOfBitDepth<float>());
            AreEqual(1, () => ConfigWishes.SizeOfBitDepth<byte>());
            AreEqual(2, () => ConfigWishes.SizeOfBitDepth<short>());
            AreEqual(4, () => ConfigWishes.SizeOfBitDepth<float>());
            AreEqual(1, () => ConfigWishes.GetSizeOfBitDepth<byte>());
            AreEqual(2, () => ConfigWishes.GetSizeOfBitDepth<short>());
            AreEqual(4, () => ConfigWishes.GetSizeOfBitDepth<float>());
            AreEqual(1, () => ConfigWishes.AsSizeOfBitDepth<byte>());
            AreEqual(2, () => ConfigWishes.AsSizeOfBitDepth<short>());
            AreEqual(4, () => ConfigWishes.AsSizeOfBitDepth<float>());
            AreEqual(1, () => ConfigWishes.ToSizeOfBitDepth<byte>());
            AreEqual(2, () => ConfigWishes.ToSizeOfBitDepth<short>());
            AreEqual(4, () => ConfigWishes.ToSizeOfBitDepth<float>());
        
            // Setters
            AreEqual(typeof(byte),  () => SizeOfBitDepth     <byte>(1));
            AreEqual(typeof(byte),  () => SizeOfBitDepth    <short>(1));
            AreEqual(typeof(byte),  () => SizeOfBitDepth    <float>(1));
            AreEqual(typeof(short), () => SizeOfBitDepth     <byte>(2));
            AreEqual(typeof(short), () => SizeOfBitDepth    <short>(2));
            AreEqual(typeof(short), () => SizeOfBitDepth    <float>(2));
            AreEqual(typeof(float), () => SizeOfBitDepth     <byte>(4));
            AreEqual(typeof(float), () => SizeOfBitDepth    <short>(4));
            AreEqual(typeof(float), () => SizeOfBitDepth    <float>(4));
            AreEqual(typeof(byte),  () => AsSizeOfBitDepth   <byte>(1));
            AreEqual(typeof(byte),  () => AsSizeOfBitDepth  <short>(1));
            AreEqual(typeof(byte),  () => AsSizeOfBitDepth  <float>(1));
            AreEqual(typeof(short), () => AsSizeOfBitDepth   <byte>(2));
            AreEqual(typeof(short), () => AsSizeOfBitDepth  <short>(2));
            AreEqual(typeof(short), () => AsSizeOfBitDepth  <float>(2));
            AreEqual(typeof(float), () => AsSizeOfBitDepth   <byte>(4));
            AreEqual(typeof(float), () => AsSizeOfBitDepth  <short>(4));
            AreEqual(typeof(float), () => AsSizeOfBitDepth  <float>(4));
            AreEqual(typeof(byte),  () => ToSizeOfBitDepth   <byte>(1));
            AreEqual(typeof(byte),  () => ToSizeOfBitDepth  <short>(1));
            AreEqual(typeof(byte),  () => ToSizeOfBitDepth  <float>(1));
            AreEqual(typeof(short), () => ToSizeOfBitDepth   <byte>(2));
            AreEqual(typeof(short), () => ToSizeOfBitDepth  <short>(2));
            AreEqual(typeof(short), () => ToSizeOfBitDepth  <float>(2));
            AreEqual(typeof(float), () => ToSizeOfBitDepth   <byte>(4));
            AreEqual(typeof(float), () => ToSizeOfBitDepth  <short>(4));
            AreEqual(typeof(float), () => ToSizeOfBitDepth  <float>(4));
            AreEqual(typeof(byte),  () => WithSizeOfBitDepth <byte>(1));
            AreEqual(typeof(byte),  () => WithSizeOfBitDepth<short>(1));
            AreEqual(typeof(byte),  () => WithSizeOfBitDepth<float>(1));
            AreEqual(typeof(short), () => WithSizeOfBitDepth <byte>(2));
            AreEqual(typeof(short), () => WithSizeOfBitDepth<short>(2));
            AreEqual(typeof(short), () => WithSizeOfBitDepth<float>(2));
            AreEqual(typeof(float), () => WithSizeOfBitDepth <byte>(4));
            AreEqual(typeof(float), () => WithSizeOfBitDepth<short>(4));
            AreEqual(typeof(float), () => WithSizeOfBitDepth<float>(4));
            AreEqual(typeof(byte),  () => SetSizeOfBitDepth  <byte>(1));
            AreEqual(typeof(byte),  () => SetSizeOfBitDepth <short>(1));
            AreEqual(typeof(byte),  () => SetSizeOfBitDepth <float>(1));
            AreEqual(typeof(short), () => SetSizeOfBitDepth  <byte>(2));
            AreEqual(typeof(short), () => SetSizeOfBitDepth <short>(2));
            AreEqual(typeof(short), () => SetSizeOfBitDepth <float>(2));
            AreEqual(typeof(float), () => SetSizeOfBitDepth  <byte>(4));
            AreEqual(typeof(float), () => SetSizeOfBitDepth <short>(4));
            AreEqual(typeof(float), () => SetSizeOfBitDepth <float>(4));
            AreEqual(typeof(byte),  () => ConfigWishes.SizeOfBitDepth     <byte>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.SizeOfBitDepth    <short>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.SizeOfBitDepth    <float>(1));
            AreEqual(typeof(short), () => ConfigWishes.SizeOfBitDepth     <byte>(2));
            AreEqual(typeof(short), () => ConfigWishes.SizeOfBitDepth    <short>(2));
            AreEqual(typeof(short), () => ConfigWishes.SizeOfBitDepth    <float>(2));
            AreEqual(typeof(float), () => ConfigWishes.SizeOfBitDepth     <byte>(4));
            AreEqual(typeof(float), () => ConfigWishes.SizeOfBitDepth    <short>(4));
            AreEqual(typeof(float), () => ConfigWishes.SizeOfBitDepth    <float>(4));
            AreEqual(typeof(byte),  () => ConfigWishes.WithSizeOfBitDepth <byte>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.WithSizeOfBitDepth<short>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.WithSizeOfBitDepth<float>(1));
            AreEqual(typeof(short), () => ConfigWishes.WithSizeOfBitDepth <byte>(2));
            AreEqual(typeof(short), () => ConfigWishes.WithSizeOfBitDepth<short>(2));
            AreEqual(typeof(short), () => ConfigWishes.WithSizeOfBitDepth<float>(2));
            AreEqual(typeof(float), () => ConfigWishes.WithSizeOfBitDepth <byte>(4));
            AreEqual(typeof(float), () => ConfigWishes.WithSizeOfBitDepth<short>(4));
            AreEqual(typeof(float), () => ConfigWishes.WithSizeOfBitDepth<float>(4));
            AreEqual(typeof(byte),  () => ConfigWishes.AsSizeOfBitDepth   <byte>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.AsSizeOfBitDepth  <short>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.AsSizeOfBitDepth  <float>(1));
            AreEqual(typeof(short), () => ConfigWishes.AsSizeOfBitDepth   <byte>(2));
            AreEqual(typeof(short), () => ConfigWishes.AsSizeOfBitDepth  <short>(2));
            AreEqual(typeof(short), () => ConfigWishes.AsSizeOfBitDepth  <float>(2));
            AreEqual(typeof(float), () => ConfigWishes.AsSizeOfBitDepth   <byte>(4));
            AreEqual(typeof(float), () => ConfigWishes.AsSizeOfBitDepth  <short>(4));
            AreEqual(typeof(float), () => ConfigWishes.AsSizeOfBitDepth  <float>(4));
            AreEqual(typeof(byte),  () => ConfigWishes.ToSizeOfBitDepth   <byte>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.ToSizeOfBitDepth  <short>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.ToSizeOfBitDepth  <float>(1));
            AreEqual(typeof(short), () => ConfigWishes.ToSizeOfBitDepth   <byte>(2));
            AreEqual(typeof(short), () => ConfigWishes.ToSizeOfBitDepth  <short>(2));
            AreEqual(typeof(short), () => ConfigWishes.ToSizeOfBitDepth  <float>(2));
            AreEqual(typeof(float), () => ConfigWishes.ToSizeOfBitDepth   <byte>(4));
            AreEqual(typeof(float), () => ConfigWishes.ToSizeOfBitDepth  <short>(4));
            AreEqual(typeof(float), () => ConfigWishes.ToSizeOfBitDepth  <float>(4));
            AreEqual(typeof(byte),  () => ConfigWishes.SetSizeOfBitDepth  <byte>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.SetSizeOfBitDepth <short>(1));
            AreEqual(typeof(byte),  () => ConfigWishes.SetSizeOfBitDepth <float>(1));
            AreEqual(typeof(short), () => ConfigWishes.SetSizeOfBitDepth  <byte>(2));
            AreEqual(typeof(short), () => ConfigWishes.SetSizeOfBitDepth <short>(2));
            AreEqual(typeof(short), () => ConfigWishes.SetSizeOfBitDepth <float>(2));
            AreEqual(typeof(float), () => ConfigWishes.SetSizeOfBitDepth  <byte>(4));
            AreEqual(typeof(float), () => ConfigWishes.SetSizeOfBitDepth <short>(4));
            AreEqual(typeof(float), () => ConfigWishes.SetSizeOfBitDepth <float>(4));
            
            // Conversion-Style Getters
            AreEqual(1, () => TypeToSizeOfBitDepth <byte>());
            AreEqual(2, () => TypeToSizeOfBitDepth<short>());
            AreEqual(4, () => TypeToSizeOfBitDepth<float>());
            AreEqual(1, () => ConfigWishes.TypeToSizeOfBitDepth <byte>());
            AreEqual(2, () => ConfigWishes.TypeToSizeOfBitDepth<short>());
            AreEqual(4, () => ConfigWishes.TypeToSizeOfBitDepth<float>());

            // 'Shorthand' Setters
            AreEqual(typeof(byte),  () => With8Bit  <byte>());
            AreEqual(typeof(byte),  () => With8Bit <short>());
            AreEqual(typeof(byte),  () => With8Bit <float>());
            AreEqual(typeof(short), () => With16Bit <byte>());
            AreEqual(typeof(short), () => With16Bit<short>());
            AreEqual(typeof(short), () => With16Bit<float>());
            AreEqual(typeof(float), () => With32Bit <byte>());
            AreEqual(typeof(float), () => With32Bit<short>());
            AreEqual(typeof(float), () => With32Bit<float>());
            AreEqual(typeof(byte),  () => As8Bit    <byte>());
            AreEqual(typeof(byte),  () => As8Bit   <short>());
            AreEqual(typeof(byte),  () => As8Bit   <float>());
            AreEqual(typeof(short), () => As16Bit   <byte>());
            AreEqual(typeof(short), () => As16Bit  <short>());
            AreEqual(typeof(short), () => As16Bit  <float>());
            AreEqual(typeof(float), () => As32Bit   <byte>());
            AreEqual(typeof(float), () => As32Bit  <short>());
            AreEqual(typeof(float), () => As32Bit  <float>());
            AreEqual(typeof(byte),  () => Set8Bit   <byte>());
            AreEqual(typeof(byte),  () => Set8Bit  <short>());
            AreEqual(typeof(byte),  () => Set8Bit  <float>());
            AreEqual(typeof(short), () => Set16Bit  <byte>());
            AreEqual(typeof(short), () => Set16Bit <short>());
            AreEqual(typeof(short), () => Set16Bit <float>());
            AreEqual(typeof(float), () => Set32Bit  <byte>());
            AreEqual(typeof(float), () => Set32Bit <short>());
            AreEqual(typeof(float), () => Set32Bit <float>());
            AreEqual(typeof(byte),  () => ConfigWishes.With8Bit <byte>());
            AreEqual(typeof(byte),  () => ConfigWishes.With8Bit <short>());
            AreEqual(typeof(byte),  () => ConfigWishes.With8Bit <float>());
            AreEqual(typeof(short), () => ConfigWishes.With16Bit <byte>());
            AreEqual(typeof(short), () => ConfigWishes.With16Bit<short>());
            AreEqual(typeof(short), () => ConfigWishes.With16Bit<float>());
            AreEqual(typeof(float), () => ConfigWishes.With32Bit <byte>());
            AreEqual(typeof(float), () => ConfigWishes.With32Bit<short>());
            AreEqual(typeof(float), () => ConfigWishes.With32Bit<float>());
            AreEqual(typeof(byte),  () => ConfigWishes.As8Bit  <byte>());
            AreEqual(typeof(byte),  () => ConfigWishes.As8Bit  <short>());
            AreEqual(typeof(byte),  () => ConfigWishes.As8Bit  <float>());
            AreEqual(typeof(short), () => ConfigWishes.As16Bit  <byte>());
            AreEqual(typeof(short), () => ConfigWishes.As16Bit <short>());
            AreEqual(typeof(short), () => ConfigWishes.As16Bit <float>());
            AreEqual(typeof(float), () => ConfigWishes.As32Bit  <byte>());
            AreEqual(typeof(float), () => ConfigWishes.As32Bit <short>());
            AreEqual(typeof(float), () => ConfigWishes.As32Bit <float>());
            AreEqual(typeof(byte),  () => ConfigWishes.Set8Bit  <byte>());
            AreEqual(typeof(byte),  () => ConfigWishes.Set8Bit  <short>());
            AreEqual(typeof(byte),  () => ConfigWishes.Set8Bit  <float>());
            AreEqual(typeof(short), () => ConfigWishes.Set16Bit  <byte>());
            AreEqual(typeof(short), () => ConfigWishes.Set16Bit <short>());
            AreEqual(typeof(short), () => ConfigWishes.Set16Bit <float>());
            AreEqual(typeof(float), () => ConfigWishes.Set32Bit  <byte>());
            AreEqual(typeof(float), () => ConfigWishes.Set32Bit <short>());
            AreEqual(typeof(float), () => ConfigWishes.Set32Bit <float>());
        }
        
        [TestMethod]
        public void SizeOfBitDepth_EdgeCases()
        {
            // For code coverage
            ThrowsException(() => typeof(string).TypeToSizeOfBitDepth());
            ThrowsException(() => (-1).SizeOfBitDepthToType());
        }

        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, int sizeOfBitDepth)
        {
            Assert_Bound_Getters(x, sizeOfBitDepth);
            Assert_Independent_Getters(x, sizeOfBitDepth);
            Assert_Immutable_Getters(x, sizeOfBitDepth);
        }

        private void Assert_Bound_Getters(TestEntities x, int sizeOfBitDepth)
        {
            Assert_SynthBound_Getters(x, sizeOfBitDepth);
            Assert_TapeBound_Getters(x, sizeOfBitDepth);
            Assert_BuffBound_Getters(x, sizeOfBitDepth);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int sizeOfBitDepth)
        {
            // Independent after Taping
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            Assert_Independent_Getters(x.Independent.Sample, sizeOfBitDepth);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, sizeOfBitDepth);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, sizeOfBitDepth);
        }

        private void Assert_Immutable_Getters(TestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, sizeOfBitDepth);
            Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, sizeOfBitDepth);
            Assert_Immutable_Getters(x.Immutable.SampleDataType, sizeOfBitDepth);
            Assert_Immutable_Getters(x.Immutable.Type, sizeOfBitDepth);
            Assert_Immutable_Getters(x.Immutable.Bits, sizeOfBitDepth);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(               () => x);
            IsNotNull(               () => x.SynthBound);
            IsNotNull(               () => x.SynthBound.SynthWishes);
            IsNotNull(               () => x.SynthBound.FlowNode);
            IsNotNull(               () => x.SynthBound.ConfigResolver);
            AreEqual(sizeOfBitDepth, () => x.SynthBound.SynthWishes   .SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => x.SynthBound.FlowNode      .SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => x.SynthBound.ConfigResolver.SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => x.SynthBound.SynthWishes   .GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.SynthBound.FlowNode      .GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.SynthBound.ConfigResolver.GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (x.SynthBound.SynthWishes   ));
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (x.SynthBound.FlowNode      ));
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (x.SynthBound.ConfigResolver));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(x.SynthBound.SynthWishes   ));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(x.SynthBound.FlowNode      ));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(x.SynthBound.ConfigResolver));
            AreEqual(sizeOfBitDepth, () => ConfigWishes        .SizeOfBitDepth   (x.SynthBound.SynthWishes   ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes        .SizeOfBitDepth   (x.SynthBound.FlowNode      ));
            AreEqual(sizeOfBitDepth, () => ConfigWishesAccessor.SizeOfBitDepth   (x.SynthBound.ConfigResolver));
            AreEqual(sizeOfBitDepth, () => ConfigWishes        .GetSizeOfBitDepth(x.SynthBound.SynthWishes   ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes        .GetSizeOfBitDepth(x.SynthBound.FlowNode      ));
            AreEqual(sizeOfBitDepth, () => ConfigWishesAccessor.GetSizeOfBitDepth(x.SynthBound.ConfigResolver));
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(               () => x);
            IsNotNull(               () => x.TapeBound);
            IsNotNull(               () => x.TapeBound.Tape);
            IsNotNull(               () => x.TapeBound.TapeConfig);
            IsNotNull(               () => x.TapeBound.TapeActions);
            IsNotNull(               () => x.TapeBound.TapeAction);
            AreEqual(sizeOfBitDepth, () => x.TapeBound.Tape       .SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.TapeConfig .SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.TapeActions.SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.TapeAction .SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.Tape       .GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.TapeConfig .GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.TapeActions.GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.TapeAction .GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (x.TapeBound.Tape       ));
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (x.TapeBound.TapeConfig ));
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (x.TapeBound.TapeActions));
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (x.TapeBound.TapeAction ));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(x.TapeBound.Tape       ));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(x.TapeBound.TapeConfig ));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(x.TapeBound.TapeActions));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(x.TapeBound.TapeAction ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (x.TapeBound.Tape       ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (x.TapeBound.TapeConfig ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (x.TapeBound.TapeActions));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (x.TapeBound.TapeAction ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(x.TapeBound.Tape       ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(x.TapeBound.TapeConfig ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(x.TapeBound.TapeActions));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(x.TapeBound.TapeAction ));
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(               () => x);
            IsNotNull(               () => x.BuffBound);
            IsNotNull(               () => x.BuffBound.Buff);
            AreEqual(sizeOfBitDepth, () => x.BuffBound.Buff           .SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => x.BuffBound.AudioFileOutput.SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => x.BuffBound.Buff           .GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.BuffBound.AudioFileOutput.GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (x.BuffBound.Buff           ));
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (x.BuffBound.AudioFileOutput));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(x.BuffBound.Buff           ));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(x.BuffBound.AudioFileOutput));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (x.BuffBound.Buff           ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (x.BuffBound.AudioFileOutput));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(x.BuffBound.Buff           ));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(x.BuffBound.AudioFileOutput));
        }
                
        private void Assert_Independent_Getters(Sample sample, int sizeOfBitDepth)
        {
            IsNotNull(               () => sample);
            AreEqual(sizeOfBitDepth, () => sample.SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => sample.GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (sample));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(sample));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (sample));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(sample));
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int sizeOfBitDepth)
        {
            IsNotNull(               () => audioInfoWish);
            AreEqual(sizeOfBitDepth, () => audioInfoWish.SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => audioInfoWish.GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (audioInfoWish));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(audioInfoWish));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (audioInfoWish));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(audioInfoWish));
        }
        
        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int sizeOfBitDepth)
        {
            IsNotNull(               () => audioFileInfo);
            AreEqual(sizeOfBitDepth, () => audioFileInfo.SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => audioFileInfo.GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (audioFileInfo));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(audioFileInfo));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (audioFileInfo));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(audioFileInfo));
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int sizeOfBitDepth)
        {
            AreEqual(sizeOfBitDepth, () => wavHeader.SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => wavHeader.GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (wavHeader));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(wavHeader));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (wavHeader));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(wavHeader));
        }
        
        private void Assert_Immutable_Getters(SampleDataTypeEnum sampleDataTypeEnum, int sizeOfBitDepth)
        {
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.ToSizeOfBitDepth ());
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.AsSizeOfBitDepth ());
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ToSizeOfBitDepth (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => AsSizeOfBitDepth (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ToSizeOfBitDepth (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.AsSizeOfBitDepth (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(sampleDataTypeEnum));
        }
        
        private void Assert_Immutable_Getters(SampleDataType sampleDataType, int sizeOfBitDepth)
        {
            IsNotNull(               () => sampleDataType);
            AreEqual(sizeOfBitDepth, () => sampleDataType.SizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => sampleDataType.ToSizeOfBitDepth ());
            AreEqual(sizeOfBitDepth, () => sampleDataType.AsSizeOfBitDepth ());
            AreEqual(sizeOfBitDepth, () => sampleDataType.GetSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth   (sampleDataType));
            AreEqual(sizeOfBitDepth, () => ToSizeOfBitDepth (sampleDataType));
            AreEqual(sizeOfBitDepth, () => AsSizeOfBitDepth (sampleDataType));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth(sampleDataType));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth   (sampleDataType));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ToSizeOfBitDepth (sampleDataType));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.AsSizeOfBitDepth (sampleDataType));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth(sampleDataType));
        }
        
        private void Assert_Immutable_Getters(Type type, int sizeOfBitDepth)
        {
            IsNotNull(               () => type);
            AreEqual(sizeOfBitDepth, () => type.SizeOfBitDepth      ());
            AreEqual(sizeOfBitDepth, () => type.AsSizeOfBitDepth    ());
            AreEqual(sizeOfBitDepth, () => type.ToSizeOfBitDepth    ());
            AreEqual(sizeOfBitDepth, () => type.GetSizeOfBitDepth   ());
            AreEqual(sizeOfBitDepth, () => type.TypeToSizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth      (type));
            AreEqual(sizeOfBitDepth, () => AsSizeOfBitDepth    (type));
            AreEqual(sizeOfBitDepth, () => ToSizeOfBitDepth    (type));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth   (type));
            AreEqual(sizeOfBitDepth, () => TypeToSizeOfBitDepth(type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth      (type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.AsSizeOfBitDepth    (type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ToSizeOfBitDepth    (type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth   (type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.TypeToSizeOfBitDepth(type));
        }
        
        private void Assert_Immutable_Getters(int bits, int sizeOfBitDepth)
        {
            int otherSizeOfBitDepth = 1;
            
            AreEqual(sizeOfBitDepth, () => bits.SizeOfBitDepth              ());
            AreEqual(sizeOfBitDepth, () => bits.GetSizeOfBitDepth           ());
            AreEqual(sizeOfBitDepth, () => bits.AsSizeOfBitDepth            ());
            AreEqual(sizeOfBitDepth, () => bits.ToSizeOfBitDepth            ());
            AreEqual(sizeOfBitDepth, () => bits.BitsToSizeOfBitDepth        ());
            AreEqual(sizeOfBitDepth, () => otherSizeOfBitDepth.Bits     (bits));
            AreEqual(sizeOfBitDepth, () => otherSizeOfBitDepth.WithBits (bits));
            AreEqual(sizeOfBitDepth, () => otherSizeOfBitDepth.SetBits  (bits));
            AreEqual(sizeOfBitDepth, () => SizeOfBitDepth               (bits));
            AreEqual(sizeOfBitDepth, () => GetSizeOfBitDepth            (bits));
            AreEqual(sizeOfBitDepth, () => AsSizeOfBitDepth             (bits));
            AreEqual(sizeOfBitDepth, () => ToSizeOfBitDepth             (bits));
            AreEqual(sizeOfBitDepth, () => BitsToSizeOfBitDepth         (bits));
            AreEqual(sizeOfBitDepth, () => Bits    (otherSizeOfBitDepth, bits));
            AreEqual(sizeOfBitDepth, () => WithBits(otherSizeOfBitDepth, bits));
            AreEqual(sizeOfBitDepth, () => SetBits (otherSizeOfBitDepth, bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth               (bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth            (bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.AsSizeOfBitDepth             (bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ToSizeOfBitDepth             (bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.BitsToSizeOfBitDepth         (bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.Bits    (otherSizeOfBitDepth, bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.WithBits(otherSizeOfBitDepth, bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.SetBits (otherSizeOfBitDepth, bits));

            int? nullyBits = bits;
            int? nullySizeOfBitDepth = sizeOfBitDepth;
            int? nullyOtherSizeOfBitDepth = otherSizeOfBitDepth;
            AreEqual(nullySizeOfBitDepth, () => nullyBits.SizeOfBitDepth                   ());
            AreEqual(nullySizeOfBitDepth, () => nullyBits.GetSizeOfBitDepth                ());
            AreEqual(nullySizeOfBitDepth, () => nullyBits.AsSizeOfBitDepth                 ());
            AreEqual(nullySizeOfBitDepth, () => nullyBits.ToSizeOfBitDepth                 ());
            AreEqual(nullySizeOfBitDepth, () => nullyBits.BitsToSizeOfBitDepth             ());
            AreEqual(nullySizeOfBitDepth, () => nullyOtherSizeOfBitDepth.Bits     (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => nullyOtherSizeOfBitDepth.WithBits (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => nullyOtherSizeOfBitDepth.SetBits  (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => SizeOfBitDepth                    (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => GetSizeOfBitDepth                 (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => AsSizeOfBitDepth                  (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => ToSizeOfBitDepth                  (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => BitsToSizeOfBitDepth              (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => Bits    (nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(nullySizeOfBitDepth, () => WithBits(nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(nullySizeOfBitDepth, () => SetBits (nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(nullySizeOfBitDepth, () => ConfigWishes.SizeOfBitDepth                    (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => ConfigWishes.GetSizeOfBitDepth                 (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => ConfigWishes.AsSizeOfBitDepth                  (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => ConfigWishes.ToSizeOfBitDepth                  (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => ConfigWishes.BitsToSizeOfBitDepth              (nullyBits));
            AreEqual(nullySizeOfBitDepth, () => ConfigWishes.Bits    (nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(nullySizeOfBitDepth, () => ConfigWishes.WithBits(nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(nullySizeOfBitDepth, () => ConfigWishes.SetBits (nullyOtherSizeOfBitDepth, nullyBits));

            nullyBits = 0;
            AreEqual(0, () => nullyBits.SizeOfBitDepth                   ());
            AreEqual(0, () => nullyBits.GetSizeOfBitDepth                ());
            AreEqual(0, () => nullyBits.AsSizeOfBitDepth                 ());
            AreEqual(0, () => nullyBits.ToSizeOfBitDepth                 ());
            AreEqual(0, () => nullyBits.BitsToSizeOfBitDepth             ());
            AreEqual(0, () => nullyOtherSizeOfBitDepth.Bits     (nullyBits));
            AreEqual(0, () => nullyOtherSizeOfBitDepth.WithBits (nullyBits));
            AreEqual(0, () => nullyOtherSizeOfBitDepth.SetBits  (nullyBits));
            AreEqual(0, () => SizeOfBitDepth                    (nullyBits));
            AreEqual(0, () => GetSizeOfBitDepth                 (nullyBits));
            AreEqual(0, () => AsSizeOfBitDepth                  (nullyBits));
            AreEqual(0, () => ToSizeOfBitDepth                  (nullyBits));
            AreEqual(0, () => BitsToSizeOfBitDepth              (nullyBits));
            AreEqual(0, () => Bits    (nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(0, () => WithBits(nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(0, () => SetBits (nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(0, () => ConfigWishes.SizeOfBitDepth                    (nullyBits));
            AreEqual(0, () => ConfigWishes.GetSizeOfBitDepth                 (nullyBits));
            AreEqual(0, () => ConfigWishes.AsSizeOfBitDepth                  (nullyBits));
            AreEqual(0, () => ConfigWishes.ToSizeOfBitDepth                  (nullyBits));
            AreEqual(0, () => ConfigWishes.BitsToSizeOfBitDepth              (nullyBits));
            AreEqual(0, () => ConfigWishes.Bits    (nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(0, () => ConfigWishes.WithBits(nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(0, () => ConfigWishes.SetBits (nullyOtherSizeOfBitDepth, nullyBits));

            nullyBits = null;
            AreEqual(null, () => nullyBits.SizeOfBitDepth                   ());
            AreEqual(null, () => nullyBits.GetSizeOfBitDepth                ());
            AreEqual(null, () => nullyBits.AsSizeOfBitDepth                 ());
            AreEqual(null, () => nullyBits.ToSizeOfBitDepth                 ());
            AreEqual(null, () => nullyBits.BitsToSizeOfBitDepth             ());
            AreEqual(null, () => nullyOtherSizeOfBitDepth.Bits     (nullyBits));
            AreEqual(null, () => nullyOtherSizeOfBitDepth.WithBits (nullyBits));
            AreEqual(null, () => nullyOtherSizeOfBitDepth.SetBits  (nullyBits));
            AreEqual(null, () => SizeOfBitDepth                    (nullyBits));
            AreEqual(null, () => GetSizeOfBitDepth                 (nullyBits));
            AreEqual(null, () => AsSizeOfBitDepth                  (nullyBits));
            AreEqual(null, () => ToSizeOfBitDepth                  (nullyBits));
            AreEqual(null, () => BitsToSizeOfBitDepth              (nullyBits));
            AreEqual(null, () => Bits    (nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(null, () => WithBits(nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(null, () => SetBits (nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(null, () => ConfigWishes.SizeOfBitDepth                    (nullyBits));
            AreEqual(null, () => ConfigWishes.GetSizeOfBitDepth                 (nullyBits));
            AreEqual(null, () => ConfigWishes.AsSizeOfBitDepth                  (nullyBits));
            AreEqual(null, () => ConfigWishes.ToSizeOfBitDepth                  (nullyBits));
            AreEqual(null, () => ConfigWishes.BitsToSizeOfBitDepth              (nullyBits));
            AreEqual(null, () => ConfigWishes.Bits    (nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(null, () => ConfigWishes.WithBits(nullyOtherSizeOfBitDepth, nullyBits));
            AreEqual(null, () => ConfigWishes.SetBits (nullyOtherSizeOfBitDepth, nullyBits));
        }
         
        // Data Helpers

        private TestEntities CreateTestEntities(int? sizeOfBitDepth) => new TestEntities(x => x.SizeOfBitDepth(sizeOfBitDepth).SamplingRate(HighPerfHz));

        // ncrunch: no coverage start
        
        static object TestParametersInit => new[] 
        {
            new object[] { null },
            new object[] { 0 },
            new object[] { 1 },
            new object[] { 2 },
            new object[] { 4 }
        };

        static object TestParameters => new[] 
        {
            new object[] { 1, 2 },
            new object[] { 1, 4 },
            new object[] { 2, 1 },
            new object[] { 2, 4 },
            new object[] { 4, 1 },
            new object[] { 4, 2 },
            new object[] { 4, 4 },
        };

        static object TestParametersWithEmpty => new[] 
        {
            new object[] {    0 ,    1 },
            new object[] {    2 ,    0 },
            new object[] { null ,    2 },
            new object[] {    1 , null },
            
            new object[] {    1 ,    2 },
            new object[] {    1 ,    4 },
            new object[] {    2 ,    1 },
            new object[] {    2 ,    4 },
            new object[] {    4 ,    1 },
            new object[] {    4 ,    2 },
            new object[] {    4 ,    4 },
        };

        // ncrunch: no coverage end
    } 
}