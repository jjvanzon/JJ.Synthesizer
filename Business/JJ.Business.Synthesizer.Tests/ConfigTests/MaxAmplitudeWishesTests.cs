using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.ConfigTests;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Tests.ConfigTests.ConfigTestEntities;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0618
#pragma warning disable MSTEST0018
#pragma warning disable IDE0002

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
            
            void AssertProp(Action<SynthBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters        (x, init.maxAmplitude);
                
                setter(x.SynthBound);
                
                Assert_SynthBound_Getters (x, val .maxAmplitude);
                Assert_TapeBound_Getters  (x, init.maxAmplitude);
                Assert_BuffBound_Getters  (x, init.maxAmplitude);
                Assert_Independent_Getters(x, init.maxAmplitude);
                Assert_Immutable_Getters  (x, init.maxAmplitude);
                
                x.Record();
                Assert_All_Getters        (x, val .maxAmplitude);
            }

            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.Bits    (val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.WithBits(val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetBits (val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    Bits    (x.SynthWishes   , val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       Bits    (x.FlowNode      , val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, Bits    (x.ConfigResolver, val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    WithBits(x.SynthWishes   , val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       WithBits(x.FlowNode      , val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, WithBits(x.ConfigResolver, val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    AsBits  (x.SynthWishes   , val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       AsBits  (x.FlowNode      , val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, AsBits  (x.ConfigResolver, val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    SetBits (x.SynthWishes   , val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       SetBits (x.FlowNode      , val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, SetBits (x.ConfigResolver, val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .Bits    (x.SynthWishes   , val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .Bits    (x.FlowNode      , val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.Bits    (x.ConfigResolver, val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .WithBits(x.SynthWishes   , val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .WithBits(x.FlowNode      , val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.WithBits(x.ConfigResolver, val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .AsBits  (x.SynthWishes   , val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .AsBits  (x.FlowNode      , val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.AsBits  (x.ConfigResolver, val.bits)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .SetBits (x.SynthWishes   , val.bits)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .SetBits (x.FlowNode      , val.bits)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.SetBits (x.ConfigResolver, val.bits)));
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.SynthWishes,    () => x.SynthWishes   .With8Bit (        ));
                              if (val.bits == 16) AreEqual(x.SynthWishes,    () => x.SynthWishes   .With16Bit(        )); 
                              if (val.bits == 32) AreEqual(x.SynthWishes,    () => x.SynthWishes   .With32Bit(        ));
                              if (!Has(val.bits)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithBits (val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.SynthWishes,    () => x.SynthWishes   .As8Bit   (        ));
                              if (val.bits == 16) AreEqual(x.SynthWishes,    () => x.SynthWishes   .As16Bit  (        )); 
                              if (val.bits == 32) AreEqual(x.SynthWishes,    () => x.SynthWishes   .As32Bit  (        ));
                              if (!Has(val.bits)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsBits   (val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Set8Bit  (        ));
                              if (val.bits == 16) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Set16Bit (        )); 
                              if (val.bits == 32) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Set32Bit (        ));
                              if (!Has(val.bits)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetBits  (val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.FlowNode,       () => x.FlowNode      .With8Bit (        ));
                              if (val.bits == 16) AreEqual(x.FlowNode,       () => x.FlowNode      .With16Bit(        )); 
                              if (val.bits == 32) AreEqual(x.FlowNode,       () => x.FlowNode      .With32Bit(        ));
                              if (!Has(val.bits)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithBits (val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.FlowNode,       () => x.FlowNode      .As8Bit   (        ));
                              if (val.bits == 16) AreEqual(x.FlowNode,       () => x.FlowNode      .As16Bit  (        )); 
                              if (val.bits == 32) AreEqual(x.FlowNode,       () => x.FlowNode      .As32Bit  (        ));
                              if (!Has(val.bits)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsBits   (val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.FlowNode,       () => x.FlowNode      .Set8Bit  (        ));
                              if (val.bits == 16) AreEqual(x.FlowNode,       () => x.FlowNode      .Set16Bit (        )); 
                              if (val.bits == 32) AreEqual(x.FlowNode,       () => x.FlowNode      .Set32Bit (        ));
                              if (!Has(val.bits)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetBits  (val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.ConfigResolver, () => x.ConfigResolver.With8Bit (        ));
                              if (val.bits == 16) AreEqual(x.ConfigResolver, () => x.ConfigResolver.With16Bit(        )); 
                              if (val.bits == 32) AreEqual(x.ConfigResolver, () => x.ConfigResolver.With32Bit(        ));
                              if (!Has(val.bits)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithBits (val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.ConfigResolver, () => x.ConfigResolver.As8Bit   (        ));
                              if (val.bits == 16) AreEqual(x.ConfigResolver, () => x.ConfigResolver.As16Bit  (        )); 
                              if (val.bits == 32) AreEqual(x.ConfigResolver, () => x.ConfigResolver.As32Bit  (        ));
                              if (!Has(val.bits)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsBits   (val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Set8Bit  (        ));
                              if (val.bits == 16) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Set16Bit (        )); 
                              if (val.bits == 32) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Set32Bit (        ));
                              if (!Has(val.bits)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetBits  (val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.SynthWishes,    () => With8Bit (x.SynthWishes             ));
                              if (val.bits == 16) AreEqual(x.SynthWishes,    () => With16Bit(x.SynthWishes             )); 
                              if (val.bits == 32) AreEqual(x.SynthWishes,    () => With32Bit(x.SynthWishes             ));
                              if (!Has(val.bits)) AreEqual(x.SynthWishes,    () => WithBits (x.SynthWishes,    val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.SynthWishes,    () => As8Bit   (x.SynthWishes             ));
                              if (val.bits == 16) AreEqual(x.SynthWishes,    () => As16Bit  (x.SynthWishes             )); 
                              if (val.bits == 32) AreEqual(x.SynthWishes,    () => As32Bit  (x.SynthWishes             ));
                              if (!Has(val.bits)) AreEqual(x.SynthWishes,    () => AsBits   (x.SynthWishes,    val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.SynthWishes,    () => Set8Bit  (x.SynthWishes             ));
                              if (val.bits == 16) AreEqual(x.SynthWishes,    () => Set16Bit (x.SynthWishes             )); 
                              if (val.bits == 32) AreEqual(x.SynthWishes,    () => Set32Bit (x.SynthWishes             ));
                              if (!Has(val.bits)) AreEqual(x.SynthWishes,    () => SetBits  (x.SynthWishes,    val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.FlowNode,       () => With8Bit (x.FlowNode                ));
                              if (val.bits == 16) AreEqual(x.FlowNode,       () => With16Bit(x.FlowNode                )); 
                              if (val.bits == 32) AreEqual(x.FlowNode,       () => With32Bit(x.FlowNode                ));
                              if (!Has(val.bits)) AreEqual(x.FlowNode,       () => WithBits (x.FlowNode,       val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.FlowNode,       () => As8Bit   (x.FlowNode                ));
                              if (val.bits == 16) AreEqual(x.FlowNode,       () => As16Bit  (x.FlowNode                )); 
                              if (val.bits == 32) AreEqual(x.FlowNode,       () => As32Bit  (x.FlowNode                ));
                              if (!Has(val.bits)) AreEqual(x.FlowNode,       () => AsBits   (x.FlowNode,       val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.FlowNode,       () => Set8Bit  (x.FlowNode                ));
                              if (val.bits == 16) AreEqual(x.FlowNode,       () => Set16Bit (x.FlowNode                )); 
                              if (val.bits == 32) AreEqual(x.FlowNode,       () => Set32Bit (x.FlowNode                ));
                              if (!Has(val.bits)) AreEqual(x.FlowNode,       () => SetBits  (x.FlowNode,       val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.ConfigResolver, () => With8Bit (x.ConfigResolver          ));
                              if (val.bits == 16) AreEqual(x.ConfigResolver, () => With16Bit(x.ConfigResolver          )); 
                              if (val.bits == 32) AreEqual(x.ConfigResolver, () => With32Bit(x.ConfigResolver          ));
                              if (!Has(val.bits)) AreEqual(x.ConfigResolver, () => WithBits (x.ConfigResolver, val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.ConfigResolver, () => As8Bit   (x.ConfigResolver          ));
                              if (val.bits == 16) AreEqual(x.ConfigResolver, () => As16Bit  (x.ConfigResolver          )); 
                              if (val.bits == 32) AreEqual(x.ConfigResolver, () => As32Bit  (x.ConfigResolver          ));
                              if (!Has(val.bits)) AreEqual(x.ConfigResolver, () => AsBits   (x.ConfigResolver, val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.ConfigResolver, () => Set8Bit  (x.ConfigResolver          ));
                              if (val.bits == 16) AreEqual(x.ConfigResolver, () => Set16Bit (x.ConfigResolver          )); 
                              if (val.bits == 32) AreEqual(x.ConfigResolver, () => Set32Bit (x.ConfigResolver          ));
                              if (!Has(val.bits)) AreEqual(x.ConfigResolver, () => SetBits  (x.ConfigResolver, val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.SynthWishes,    () => ConfigWishes        .With8Bit (x.SynthWishes             ));
                              if (val.bits == 16) AreEqual(x.SynthWishes,    () => ConfigWishes        .With16Bit(x.SynthWishes             )); 
                              if (val.bits == 32) AreEqual(x.SynthWishes,    () => ConfigWishes        .With32Bit(x.SynthWishes             ));
                              if (!Has(val.bits)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithBits (x.SynthWishes,    val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.SynthWishes,    () => ConfigWishes        .As8Bit   (x.SynthWishes             ));
                              if (val.bits == 16) AreEqual(x.SynthWishes,    () => ConfigWishes        .As16Bit  (x.SynthWishes             )); 
                              if (val.bits == 32) AreEqual(x.SynthWishes,    () => ConfigWishes        .As32Bit  (x.SynthWishes             ));
                              if (!Has(val.bits)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsBits   (x.SynthWishes,    val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.SynthWishes,    () => ConfigWishes        .Set8Bit  (x.SynthWishes             ));
                              if (val.bits == 16) AreEqual(x.SynthWishes,    () => ConfigWishes        .Set16Bit (x.SynthWishes             )); 
                              if (val.bits == 32) AreEqual(x.SynthWishes,    () => ConfigWishes        .Set32Bit (x.SynthWishes             ));
                              if (!Has(val.bits)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetBits  (x.SynthWishes,    val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.FlowNode,       () => ConfigWishes        .With8Bit (x.FlowNode                ));
                              if (val.bits == 16) AreEqual(x.FlowNode,       () => ConfigWishes        .With16Bit(x.FlowNode                )); 
                              if (val.bits == 32) AreEqual(x.FlowNode,       () => ConfigWishes        .With32Bit(x.FlowNode                ));
                              if (!Has(val.bits)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithBits (x.FlowNode,       val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.FlowNode,       () => ConfigWishes        .As8Bit   (x.FlowNode                ));
                              if (val.bits == 16) AreEqual(x.FlowNode,       () => ConfigWishes        .As16Bit  (x.FlowNode                )); 
                              if (val.bits == 32) AreEqual(x.FlowNode,       () => ConfigWishes        .As32Bit  (x.FlowNode                ));
                              if (!Has(val.bits)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsBits   (x.FlowNode,       val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.FlowNode,       () => ConfigWishes        .Set8Bit  (x.FlowNode                ));
                              if (val.bits == 16) AreEqual(x.FlowNode,       () => ConfigWishes        .Set16Bit (x.FlowNode                )); 
                              if (val.bits == 32) AreEqual(x.FlowNode,       () => ConfigWishes        .Set32Bit (x.FlowNode                ));
                              if (!Has(val.bits)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetBits  (x.FlowNode,       val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.With8Bit (x.ConfigResolver          ));
                              if (val.bits == 16) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.With16Bit(x.ConfigResolver          )); 
                              if (val.bits == 32) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.With32Bit(x.ConfigResolver          ));
                              if (!Has(val.bits)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithBits (x.ConfigResolver, val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.As8Bit   (x.ConfigResolver          ));
                              if (val.bits == 16) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.As16Bit  (x.ConfigResolver          )); 
                              if (val.bits == 32) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.As32Bit  (x.ConfigResolver          ));
                              if (!Has(val.bits)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsBits   (x.ConfigResolver, val.bits)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Set8Bit  (x.ConfigResolver          ));
                              if (val.bits == 16) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Set16Bit (x.ConfigResolver          )); 
                              if (val.bits == 32) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Set32Bit (x.ConfigResolver          ));
                              if (!Has(val.bits)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetBits  (x.ConfigResolver, val.bits)); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);

            void AssertProp(Action<TapeBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.maxAmplitude);
                
                setter(x.TapeBound);
                
                Assert_SynthBound_Getters(x, init.maxAmplitude);
                Assert_TapeBound_Getters(x, val.maxAmplitude);
                Assert_BuffBound_Getters(x, init.maxAmplitude);
                Assert_Independent_Getters(x, init.maxAmplitude);
                Assert_Immutable_Getters(x, init.maxAmplitude);
                
                x.Record();
                
                Assert_All_Getters(x, init.maxAmplitude); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x =>                               x.TapeConfig .Bits   = val.bits);
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.Bits    (val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.Bits    (val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.WithBits(val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.WithBits(val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .AsBits  (val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.SetBits (val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.SetBits (val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .SetBits (val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => Bits    (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => Bits    (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => Bits    (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => Bits    (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => Bits    (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => Bits    (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => Bits    (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => Bits    (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => WithBits(x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => WithBits(x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => WithBits(x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => WithBits(x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => WithBits(x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => WithBits(x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => WithBits(x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => WithBits(x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => AsBits  (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => AsBits  (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => AsBits  (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => AsBits  (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => AsBits  (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => AsBits  (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => AsBits  (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => AsBits  (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => SetBits (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => SetBits (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => SetBits (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => SetBits (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => SetBits (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => SetBits (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => SetBits (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => SetBits (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.Bits    (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.Bits    (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.Bits    (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.Bits    (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.Bits    (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.Bits    (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.Bits    (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.Bits    (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.WithBits(x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.WithBits(x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.WithBits(x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.WithBits(x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.WithBits(x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.WithBits(x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.WithBits(x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.WithBits(x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.AsBits  (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.AsBits  (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.AsBits  (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.AsBits  (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.AsBits  (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.AsBits  (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.AsBits  (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.AsBits  (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.SetBits (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.SetBits (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.SetBits (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.SetBits (x.TapeAction , val.bits)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.SetBits (x.Tape       , val.bits)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.SetBits (x.TapeConfig , val.bits)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.SetBits (x.TapeActions, val.bits)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.SetBits (x.TapeAction , val.bits)));
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Tape,        () => x.Tape       .With8Bit ());
                              if (val.bits == 16) AreEqual(x.Tape,        () => x.Tape       .With16Bit());
                              if (val.bits == 32) AreEqual(x.Tape,        () => x.Tape       .With32Bit()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Tape,        () => x.Tape       .As8Bit   ());
                              if (val.bits == 16) AreEqual(x.Tape,        () => x.Tape       .As16Bit  ());
                              if (val.bits == 32) AreEqual(x.Tape,        () => x.Tape       .As32Bit  ()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Tape,        () => x.Tape       .Set8Bit  ());
                              if (val.bits == 16) AreEqual(x.Tape,        () => x.Tape       .Set16Bit ());
                              if (val.bits == 32) AreEqual(x.Tape,        () => x.Tape       .Set32Bit ()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeConfig,  () => x.TapeConfig .With8Bit ());
                              if (val.bits == 16) AreEqual(x.TapeConfig,  () => x.TapeConfig .With16Bit());
                              if (val.bits == 32) AreEqual(x.TapeConfig,  () => x.TapeConfig .With32Bit()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeConfig,  () => x.TapeConfig .As8Bit   ());
                              if (val.bits == 16) AreEqual(x.TapeConfig,  () => x.TapeConfig .As16Bit  ());
                              if (val.bits == 32) AreEqual(x.TapeConfig,  () => x.TapeConfig .As32Bit  ()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeConfig,  () => x.TapeConfig .Set8Bit  ());
                              if (val.bits == 16) AreEqual(x.TapeConfig,  () => x.TapeConfig .Set16Bit ());
                              if (val.bits == 32) AreEqual(x.TapeConfig,  () => x.TapeConfig .Set32Bit ()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeActions, () => x.TapeActions.With8Bit ());
                              if (val.bits == 16) AreEqual(x.TapeActions, () => x.TapeActions.With16Bit());
                              if (val.bits == 32) AreEqual(x.TapeActions, () => x.TapeActions.With32Bit()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeActions, () => x.TapeActions.As8Bit   ());
                              if (val.bits == 16) AreEqual(x.TapeActions, () => x.TapeActions.As16Bit  ());
                              if (val.bits == 32) AreEqual(x.TapeActions, () => x.TapeActions.As32Bit  ()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeActions, () => x.TapeActions.Set8Bit  ());
                              if (val.bits == 16) AreEqual(x.TapeActions, () => x.TapeActions.Set16Bit ());
                              if (val.bits == 32) AreEqual(x.TapeActions, () => x.TapeActions.Set32Bit ()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeAction,  () => x.TapeAction .With8Bit ());
                              if (val.bits == 16) AreEqual(x.TapeAction,  () => x.TapeAction .With16Bit());
                              if (val.bits == 32) AreEqual(x.TapeAction,  () => x.TapeAction .With32Bit()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeAction,  () => x.TapeAction .As8Bit   ());
                              if (val.bits == 16) AreEqual(x.TapeAction,  () => x.TapeAction .As16Bit  ());
                              if (val.bits == 32) AreEqual(x.TapeAction,  () => x.TapeAction .As32Bit  ()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeAction,  () => x.TapeAction .Set8Bit  ());
                              if (val.bits == 16) AreEqual(x.TapeAction,  () => x.TapeAction .Set16Bit ());
                              if (val.bits == 32) AreEqual(x.TapeAction,  () => x.TapeAction .Set32Bit ()); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Tape,        () => With8Bit (x.Tape       ));
                              if (val.bits == 16) AreEqual(x.Tape,        () => With16Bit(x.Tape       ));
                              if (val.bits == 32) AreEqual(x.Tape,        () => With32Bit(x.Tape       )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Tape,        () => As8Bit   (x.Tape       ));
                              if (val.bits == 16) AreEqual(x.Tape,        () => As16Bit  (x.Tape       ));
                              if (val.bits == 32) AreEqual(x.Tape,        () => As32Bit  (x.Tape       )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Tape,        () => Set8Bit  (x.Tape       ));
                              if (val.bits == 16) AreEqual(x.Tape,        () => Set16Bit (x.Tape       ));
                              if (val.bits == 32) AreEqual(x.Tape,        () => Set32Bit (x.Tape       )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeConfig,  () => With8Bit (x.TapeConfig ));
                              if (val.bits == 16) AreEqual(x.TapeConfig,  () => With16Bit(x.TapeConfig ));
                              if (val.bits == 32) AreEqual(x.TapeConfig,  () => With32Bit(x.TapeConfig )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeConfig,  () => As8Bit   (x.TapeConfig ));
                              if (val.bits == 16) AreEqual(x.TapeConfig,  () => As16Bit  (x.TapeConfig ));
                              if (val.bits == 32) AreEqual(x.TapeConfig,  () => As32Bit  (x.TapeConfig )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeConfig,  () => Set8Bit  (x.TapeConfig ));
                              if (val.bits == 16) AreEqual(x.TapeConfig,  () => Set16Bit (x.TapeConfig ));
                              if (val.bits == 32) AreEqual(x.TapeConfig,  () => Set32Bit (x.TapeConfig )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeActions, () => With8Bit (x.TapeActions));
                              if (val.bits == 16) AreEqual(x.TapeActions, () => With16Bit(x.TapeActions));
                              if (val.bits == 32) AreEqual(x.TapeActions, () => With32Bit(x.TapeActions)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeActions, () => As8Bit   (x.TapeActions));
                              if (val.bits == 16) AreEqual(x.TapeActions, () => As16Bit  (x.TapeActions));
                              if (val.bits == 32) AreEqual(x.TapeActions, () => As32Bit  (x.TapeActions)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeActions, () => Set8Bit  (x.TapeActions));
                              if (val.bits == 16) AreEqual(x.TapeActions, () => Set16Bit (x.TapeActions));
                              if (val.bits == 32) AreEqual(x.TapeActions, () => Set32Bit (x.TapeActions)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeAction,  () => With8Bit (x.TapeAction ));
                              if (val.bits == 16) AreEqual(x.TapeAction,  () => With16Bit(x.TapeAction ));
                              if (val.bits == 32) AreEqual(x.TapeAction,  () => With32Bit(x.TapeAction )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeAction,  () => As8Bit   (x.TapeAction ));
                              if (val.bits == 16) AreEqual(x.TapeAction,  () => As16Bit  (x.TapeAction ));
                              if (val.bits == 32) AreEqual(x.TapeAction,  () => As32Bit  (x.TapeAction )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeAction,  () => Set8Bit  (x.TapeAction ));
                              if (val.bits == 16) AreEqual(x.TapeAction,  () => Set16Bit (x.TapeAction ));
                              if (val.bits == 32) AreEqual(x.TapeAction,  () => Set32Bit (x.TapeAction )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Tape,        () => ConfigWishes.With8Bit (x.Tape       ));
                              if (val.bits == 16) AreEqual(x.Tape,        () => ConfigWishes.With16Bit(x.Tape       ));
                              if (val.bits == 32) AreEqual(x.Tape,        () => ConfigWishes.With32Bit(x.Tape       )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Tape,        () => ConfigWishes.As8Bit   (x.Tape       ));
                              if (val.bits == 16) AreEqual(x.Tape,        () => ConfigWishes.As16Bit  (x.Tape       ));
                              if (val.bits == 32) AreEqual(x.Tape,        () => ConfigWishes.As32Bit  (x.Tape       )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Tape,        () => ConfigWishes.Set8Bit  (x.Tape       ));
                              if (val.bits == 16) AreEqual(x.Tape,        () => ConfigWishes.Set16Bit (x.Tape       ));
                              if (val.bits == 32) AreEqual(x.Tape,        () => ConfigWishes.Set32Bit (x.Tape       )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeConfig,  () => ConfigWishes.With8Bit (x.TapeConfig ));
                              if (val.bits == 16) AreEqual(x.TapeConfig,  () => ConfigWishes.With16Bit(x.TapeConfig ));
                              if (val.bits == 32) AreEqual(x.TapeConfig,  () => ConfigWishes.With32Bit(x.TapeConfig )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeConfig,  () => ConfigWishes.As8Bit   (x.TapeConfig ));
                              if (val.bits == 16) AreEqual(x.TapeConfig,  () => ConfigWishes.As16Bit  (x.TapeConfig ));
                              if (val.bits == 32) AreEqual(x.TapeConfig,  () => ConfigWishes.As32Bit  (x.TapeConfig )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeConfig,  () => ConfigWishes.Set8Bit  (x.TapeConfig ));
                              if (val.bits == 16) AreEqual(x.TapeConfig,  () => ConfigWishes.Set16Bit (x.TapeConfig ));
                              if (val.bits == 32) AreEqual(x.TapeConfig,  () => ConfigWishes.Set32Bit (x.TapeConfig )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeActions, () => ConfigWishes.With8Bit (x.TapeActions));
                              if (val.bits == 16) AreEqual(x.TapeActions, () => ConfigWishes.With16Bit(x.TapeActions));
                              if (val.bits == 32) AreEqual(x.TapeActions, () => ConfigWishes.With32Bit(x.TapeActions)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeActions, () => ConfigWishes.As8Bit   (x.TapeActions));
                              if (val.bits == 16) AreEqual(x.TapeActions, () => ConfigWishes.As16Bit  (x.TapeActions));
                              if (val.bits == 32) AreEqual(x.TapeActions, () => ConfigWishes.As32Bit  (x.TapeActions)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeActions, () => ConfigWishes.Set8Bit  (x.TapeActions));
                              if (val.bits == 16) AreEqual(x.TapeActions, () => ConfigWishes.Set16Bit (x.TapeActions));
                              if (val.bits == 32) AreEqual(x.TapeActions, () => ConfigWishes.Set32Bit (x.TapeActions)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeAction,  () => ConfigWishes.With8Bit (x.TapeAction ));
                              if (val.bits == 16) AreEqual(x.TapeAction,  () => ConfigWishes.With16Bit(x.TapeAction ));
                              if (val.bits == 32) AreEqual(x.TapeAction,  () => ConfigWishes.With32Bit(x.TapeAction )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeAction,  () => ConfigWishes.As8Bit   (x.TapeAction ));
                              if (val.bits == 16) AreEqual(x.TapeAction,  () => ConfigWishes.As16Bit  (x.TapeAction ));
                              if (val.bits == 32) AreEqual(x.TapeAction,  () => ConfigWishes.As32Bit  (x.TapeAction )); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.TapeAction,  () => ConfigWishes.Set8Bit  (x.TapeAction ));
                              if (val.bits == 16) AreEqual(x.TapeAction,  () => ConfigWishes.Set16Bit (x.TapeAction ));
                              if (val.bits == 32) AreEqual(x.TapeAction,  () => ConfigWishes.Set32Bit (x.TapeAction )); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);
            IContext context = null;

            void AssertProp(Action<BuffBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                context = x.SynthBound.Context;

                Assert_All_Getters(x, init.maxAmplitude);
                
                setter(x.BuffBound);
                
                Assert_SynthBound_Getters(x, init.maxAmplitude);
                Assert_TapeBound_Getters(x, init.maxAmplitude);
                Assert_BuffBound_Getters(x, val.maxAmplitude);
                Assert_Independent_Getters(x, init.maxAmplitude);
                Assert_Immutable_Getters(x, init.maxAmplitude);
                
                x.Record();
                Assert_All_Getters(x, init.maxAmplitude);
            }

            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .Bits    (val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Bits    (val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .WithBits(val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithBits(val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .AsBits  (val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsBits  (val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .SetBits (val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetBits (val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => Bits    (x.Buff,            val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => Bits    (x.AudioFileOutput, val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => WithBits(x.Buff,            val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => WithBits(x.AudioFileOutput, val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => AsBits  (x.Buff,            val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => AsBits  (x.AudioFileOutput, val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => SetBits (x.Buff,            val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => SetBits (x.AudioFileOutput, val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.Bits    (x.Buff,            val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.Bits    (x.AudioFileOutput, val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.WithBits(x.Buff,            val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.WithBits(x.AudioFileOutput, val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.AsBits  (x.Buff,            val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.AsBits  (x.AudioFileOutput, val.bits, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.SetBits (x.Buff,            val.bits, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.SetBits (x.AudioFileOutput, val.bits, context)));
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Buff,            () => x.Buff           .With8Bit (context));
                              if (val.bits == 16) AreEqual(x.Buff,            () => x.Buff           .With16Bit(context));
                              if (val.bits == 32) AreEqual(x.Buff,            () => x.Buff           .With32Bit(context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With8Bit (context));
                              if (val.bits == 16) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With16Bit(context));
                              if (val.bits == 32) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With32Bit(context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Buff,            () => x.Buff           .As8Bit   (context));
                              if (val.bits == 16) AreEqual(x.Buff,            () => x.Buff           .As16Bit  (context));
                              if (val.bits == 32) AreEqual(x.Buff,            () => x.Buff           .As32Bit  (context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.As8Bit   (context));
                              if (val.bits == 16) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.As16Bit  (context));
                              if (val.bits == 32) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.As32Bit  (context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Buff,            () => x.Buff           .Set8Bit  (context));
                              if (val.bits == 16) AreEqual(x.Buff,            () => x.Buff           .Set16Bit (context));
                              if (val.bits == 32) AreEqual(x.Buff,            () => x.Buff           .Set32Bit (context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Set8Bit  (context));
                              if (val.bits == 16) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Set16Bit (context));
                              if (val.bits == 32) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Set32Bit (context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Buff,            () => With8Bit (x.Buff           , context));
                              if (val.bits == 16) AreEqual(x.Buff,            () => With16Bit(x.Buff           , context));
                              if (val.bits == 32) AreEqual(x.Buff,            () => With32Bit(x.Buff           , context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.AudioFileOutput, () => With8Bit (x.AudioFileOutput, context));
                              if (val.bits == 16) AreEqual(x.AudioFileOutput, () => With16Bit(x.AudioFileOutput, context));
                              if (val.bits == 32) AreEqual(x.AudioFileOutput, () => With32Bit(x.AudioFileOutput, context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Buff,            () => As8Bit   (x.Buff           , context));
                              if (val.bits == 16) AreEqual(x.Buff,            () => As16Bit  (x.Buff           , context));
                              if (val.bits == 32) AreEqual(x.Buff,            () => As32Bit  (x.Buff           , context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.AudioFileOutput, () => As8Bit   (x.AudioFileOutput, context));
                              if (val.bits == 16) AreEqual(x.AudioFileOutput, () => As16Bit  (x.AudioFileOutput, context));
                              if (val.bits == 32) AreEqual(x.AudioFileOutput, () => As32Bit  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Buff,            () => Set8Bit  (x.Buff           , context));
                              if (val.bits == 16) AreEqual(x.Buff,            () => Set16Bit (x.Buff           , context));
                              if (val.bits == 32) AreEqual(x.Buff,            () => Set32Bit (x.Buff           , context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.AudioFileOutput, () => Set8Bit  (x.AudioFileOutput, context));
                              if (val.bits == 16) AreEqual(x.AudioFileOutput, () => Set16Bit (x.AudioFileOutput, context));
                              if (val.bits == 32) AreEqual(x.AudioFileOutput, () => Set32Bit (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Buff,            () => ConfigWishes.With8Bit (x.Buff           , context));
                              if (val.bits == 16) AreEqual(x.Buff,            () => ConfigWishes.With16Bit(x.Buff           , context));
                              if (val.bits == 32) AreEqual(x.Buff,            () => ConfigWishes.With32Bit(x.Buff           , context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.AudioFileOutput, () => ConfigWishes.With8Bit (x.AudioFileOutput, context));
                              if (val.bits == 16) AreEqual(x.AudioFileOutput, () => ConfigWishes.With16Bit(x.AudioFileOutput, context));
                              if (val.bits == 32) AreEqual(x.AudioFileOutput, () => ConfigWishes.With32Bit(x.AudioFileOutput, context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Buff,            () => ConfigWishes.As8Bit   (x.Buff           , context));
                              if (val.bits == 16) AreEqual(x.Buff,            () => ConfigWishes.As16Bit  (x.Buff           , context));
                              if (val.bits == 32) AreEqual(x.Buff,            () => ConfigWishes.As32Bit  (x.Buff           , context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.AudioFileOutput, () => ConfigWishes.As8Bit   (x.AudioFileOutput, context));
                              if (val.bits == 16) AreEqual(x.AudioFileOutput, () => ConfigWishes.As16Bit  (x.AudioFileOutput, context));
                              if (val.bits == 32) AreEqual(x.AudioFileOutput, () => ConfigWishes.As32Bit  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.Buff,            () => ConfigWishes.Set8Bit  (x.Buff           , context));
                              if (val.bits == 16) AreEqual(x.Buff,            () => ConfigWishes.Set16Bit (x.Buff           , context));
                              if (val.bits == 32) AreEqual(x.Buff,            () => ConfigWishes.Set32Bit (x.Buff           , context)); });
            AssertProp(x => { if (val.bits ==  8) AreEqual(x.AudioFileOutput, () => ConfigWishes.Set8Bit  (x.AudioFileOutput, context));
                              if (val.bits == 16) AreEqual(x.AudioFileOutput, () => ConfigWishes.Set16Bit (x.AudioFileOutput, context));
                              if (val.bits == 32) AreEqual(x.AudioFileOutput, () => ConfigWishes.Set32Bit (x.AudioFileOutput, context)); });
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
                ConfigTestEntities x = default;
                
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
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Bits     (val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsBits   (val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithBits (val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetBits  (val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => Bits    (x.Independent.Sample, val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => AsBits  (x.Independent.Sample, val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithBits(x.Independent.Sample, val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetBits (x.Independent.Sample, val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.Bits    (x.Independent.Sample, val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.AsBits  (x.Independent.Sample, val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithBits(x.Independent.Sample, val.bits, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetBits (x.Independent.Sample, val.bits, x.SynthBound.Context)));
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With8Bit  (x.SynthBound.Context));
                                   if (val.bits == 16) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With16Bit (x.SynthBound.Context));
                                   if (val.bits == 32) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With32Bit (x.SynthBound.Context)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.Sample, () => x.Independent.Sample.As8Bit    (x.SynthBound.Context));
                                   if (val.bits == 16) AreEqual(x.Independent.Sample, () => x.Independent.Sample.As16Bit   (x.SynthBound.Context));
                                   if (val.bits == 32) AreEqual(x.Independent.Sample, () => x.Independent.Sample.As32Bit   (x.SynthBound.Context)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Set8Bit   (x.SynthBound.Context));
                                   if (val.bits == 16) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Set16Bit  (x.SynthBound.Context));
                                   if (val.bits == 32) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Set32Bit  (x.SynthBound.Context)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.Sample, () => With8Bit (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 16) AreEqual(x.Independent.Sample, () => With16Bit(x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 32) AreEqual(x.Independent.Sample, () => With32Bit(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.Sample, () => As8Bit   (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 16) AreEqual(x.Independent.Sample, () => As16Bit  (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 32) AreEqual(x.Independent.Sample, () => As32Bit  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.Sample, () => Set8Bit  (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 16) AreEqual(x.Independent.Sample, () => Set16Bit (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 32) AreEqual(x.Independent.Sample, () => Set32Bit (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.Sample, () => ConfigWishes.With8Bit (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 16) AreEqual(x.Independent.Sample, () => ConfigWishes.With16Bit(x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 32) AreEqual(x.Independent.Sample, () => ConfigWishes.With32Bit(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.Sample, () => ConfigWishes.As8Bit   (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 16) AreEqual(x.Independent.Sample, () => ConfigWishes.As16Bit  (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 32) AreEqual(x.Independent.Sample, () => ConfigWishes.As32Bit  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.Sample, () => ConfigWishes.Set8Bit  (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 16) AreEqual(x.Independent.Sample, () => ConfigWishes.Set16Bit (x.Independent.Sample, x.SynthBound.Context));
                                   if (val.bits == 32) AreEqual(x.Independent.Sample, () => ConfigWishes.Set32Bit (x.Independent.Sample, x.SynthBound.Context)); });
            }
            
            // AudioInfoWish
            {
                ConfigTestEntities x = default;

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
                
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Bits   = val.bits);
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Bits    (val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithBits(val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.AsBits  (val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetBits (val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => Bits    (x.Independent.AudioInfoWish, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => WithBits(x.Independent.AudioInfoWish, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => AsBits  (x.Independent.AudioInfoWish, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SetBits (x.Independent.AudioInfoWish, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Bits    (x.Independent.AudioInfoWish, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithBits(x.Independent.AudioInfoWish, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.AsBits  (x.Independent.AudioInfoWish, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetBits (x.Independent.AudioInfoWish, val.bits)));
                
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With8Bit ());
                                   if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With16Bit());
                                   if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With32Bit()); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.As8Bit   ());
                                   if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.As16Bit  ());
                                   if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.As32Bit  ()); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Set8Bit  ());
                                   if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Set16Bit ());
                                   if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Set32Bit ()); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => With8Bit (x.Independent.AudioInfoWish));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => With16Bit(x.Independent.AudioInfoWish));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => With32Bit(x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => As8Bit   (x.Independent.AudioInfoWish));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => As16Bit  (x.Independent.AudioInfoWish));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => As32Bit  (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => Set8Bit  (x.Independent.AudioInfoWish));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => Set16Bit (x.Independent.AudioInfoWish));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => Set32Bit (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.With8Bit (x.Independent.AudioInfoWish));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.With16Bit(x.Independent.AudioInfoWish));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.With32Bit(x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.As8Bit   (x.Independent.AudioInfoWish));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.As16Bit  (x.Independent.AudioInfoWish));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.As32Bit  (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Set8Bit  (x.Independent.AudioInfoWish));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Set16Bit (x.Independent.AudioInfoWish));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Set32Bit (x.Independent.AudioInfoWish)); });
            }
                        
            // AudioFileInfo
            {
                ConfigTestEntities x = default;

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
                
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Bits    (val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithBits(val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.AsBits  (val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetBits (val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => Bits    (x.Independent.AudioFileInfo, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => WithBits(x.Independent.AudioFileInfo, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => AsBits  (x.Independent.AudioFileInfo, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => SetBits (x.Independent.AudioFileInfo, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Bits    (x.Independent.AudioFileInfo, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithBits(x.Independent.AudioFileInfo, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.AsBits  (x.Independent.AudioFileInfo, val.bits)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetBits (x.Independent.AudioFileInfo, val.bits)));
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With8Bit ());
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With16Bit());
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With32Bit()); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Set8Bit  ());
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Set16Bit ());
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Set32Bit ()); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.As8Bit   ());
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.As16Bit  ());
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.As32Bit  ()); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Set8Bit  ());
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Set16Bit ());
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Set32Bit ()); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => With8Bit (x.Independent.AudioFileInfo));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => With16Bit(x.Independent.AudioFileInfo));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => With32Bit(x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => Set8Bit  (x.Independent.AudioFileInfo));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => Set16Bit (x.Independent.AudioFileInfo));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => Set32Bit (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => As8Bit   (x.Independent.AudioFileInfo));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => As16Bit  (x.Independent.AudioFileInfo));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => As32Bit  (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => Set8Bit  (x.Independent.AudioFileInfo));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => Set16Bit (x.Independent.AudioFileInfo));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => Set32Bit (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.With8Bit (x.Independent.AudioFileInfo));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.With16Bit(x.Independent.AudioFileInfo));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.With32Bit(x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Set8Bit  (x.Independent.AudioFileInfo));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Set16Bit (x.Independent.AudioFileInfo));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Set32Bit (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.As8Bit   (x.Independent.AudioFileInfo));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.As16Bit  (x.Independent.AudioFileInfo));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.As32Bit  (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Set8Bit  (x.Independent.AudioFileInfo));
                                   if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Set16Bit (x.Independent.AudioFileInfo));
                                   if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Set32Bit (x.Independent.AudioFileInfo)); });
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
                
                AssertProp(() => x.Immutable.WavHeader.Bits(val.bits));
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.WavHeader.With8Bit();
                    if (val.bits == 16) return x.Immutable.WavHeader.With16Bit();
                    if (val.bits == 32) return x.Immutable.WavHeader.With32Bit();
                    return default; // ncrunch: no coverage
                });
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
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.Bits(val.bits));
                AssertProp(() => val.bits.BitsToEnum());
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.SampleDataTypeEnum.With8Bit();
                    if (val.bits == 16) return x.Immutable.SampleDataTypeEnum.With16Bit();
                    if (val.bits == 32) return x.Immutable.SampleDataTypeEnum.With32Bit();
                    return default; // ncrunch: no coverage
                });
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
            
                AssertProp(() => x.Immutable.SampleDataType.Bits(val.bits, x.SynthBound.Context));
                AssertProp(() => val.bits.BitsToEntity(x.SynthBound.Context));
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.SampleDataType.With8Bit(x.SynthBound.Context);
                    if (val.bits == 16) return x.Immutable.SampleDataType.With16Bit(x.SynthBound.Context);
                    if (val.bits == 32) return x.Immutable.SampleDataType.With32Bit(x.SynthBound.Context);
                    return default; // ncrunch: no coverage
                });

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

                AssertProp(() => x.Immutable.Type.Bits(val.bits));
                AssertProp(() => val.bits.BitsToType());
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.Type.With8Bit();
                    if (val.bits == 16) return x.Immutable.Type.With16Bit();
                    if (val.bits == 32) return x.Immutable.Type.With32Bit();
                    return default; // ncrunch: no coverage
                });
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
        public void ConfigSection_MaxAmplitude()
        {
            // Immutable. Get-only.
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            AreEqual(1, () => DefaultBits.MaxAmplitude());
            AreEqual(DefaultBits.MaxAmplitude(), () => configSection.MaxAmplitude());
        }

        [TestMethod]
        public void MaxAmplitude_WithTypeArguments()
        {
            // ReSharper disable once PossibleLossOfFraction
            AreEqual(byte .MaxValue/ 2, () => MaxAmplitude<byte>());
            AreEqual(short.MaxValue,    () => MaxAmplitude<short>());
            AreEqual(1,                 () => MaxAmplitude<float>());
        }

        // Getter Helpers

        private void Assert_All_Getters(ConfigTestEntities x, int maxAmplitude)
        {
            Assert_Bound_Getters(x, maxAmplitude);
            Assert_Independent_Getters(x, maxAmplitude);
            Assert_Immutable_Getters(x, maxAmplitude);
        }

        private void Assert_Bound_Getters(ConfigTestEntities x, int maxAmplitude)
        {
            Assert_SynthBound_Getters(x, maxAmplitude);
            Assert_TapeBound_Getters(x, maxAmplitude);
            Assert_BuffBound_Getters(x, maxAmplitude);
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, int maxAmplitude)
        {
            // Independent after Taping
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            Assert_Independent_Getters(x.Independent.Sample, maxAmplitude);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, maxAmplitude);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, maxAmplitude);
        }

        private void Assert_Immutable_Getters(ConfigTestEntities x, int maxAmplitude)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.SampleDataType, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.Type, maxAmplitude);
        }

        private void Assert_SynthBound_Getters(ConfigTestEntities x, int maxAmplitude)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigResolver);

            AreEqual(maxAmplitude, () => x.SynthBound.SynthWishes.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.SynthBound.FlowNode.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.SynthBound.ConfigResolver.MaxAmplitude());
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, int maxAmplitude)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);

            AreEqual(maxAmplitude, () => x.TapeBound.Tape.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeConfig.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeActions.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeAction.MaxAmplitude());
        }
        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, int maxAmplitude)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);
            
            AreEqual(maxAmplitude, () => x.BuffBound.Buff.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.BuffBound.AudioFileOutput.MaxAmplitude());
        }
        
        private void Assert_Independent_Getters(Sample sample, int maxAmplitude)
        {
            IsNotNull(         () => sample);
            AreEqual(maxAmplitude, () => sample.MaxAmplitude());
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int maxAmplitude)
        {
            IsNotNull(         () => audioInfoWish);
            AreEqual(maxAmplitude, () => audioInfoWish.MaxAmplitude());
        }
        
        void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int maxAmplitude)
        {
            IsNotNull(         () => audioFileInfo);
            AreEqual(maxAmplitude, () => audioFileInfo.MaxAmplitude());
        }
        
        void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int maxAmplitude)
        {
            AreEqual(maxAmplitude, () => wavHeader.MaxAmplitude());
        }
        
        private void Assert_Immutable_Getters(SampleDataTypeEnum sampleDataTypeEnum, int maxAmplitude)
        {
            AreEqual(maxAmplitude, () => sampleDataTypeEnum.MaxAmplitude());
        }
        
        private void Assert_Immutable_Getters(SampleDataType sampleDataType, int maxAmplitude)
        {
            IsNotNull(         () => sampleDataType);
            AreEqual(maxAmplitude, () => sampleDataType.MaxAmplitude());
        }
        
        private void Assert_Immutable_Getters(Type type, int maxAmplitude)
        {
            IsNotNull(         () => type);
            AreEqual(maxAmplitude, () => type.MaxAmplitude());
        }
        
         // Test Data Helpers

        private ConfigTestEntities CreateTestEntities((double maxAmplitude, int? bits) init) => new ConfigTestEntities(x => x.Bits(init.bits));
        
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