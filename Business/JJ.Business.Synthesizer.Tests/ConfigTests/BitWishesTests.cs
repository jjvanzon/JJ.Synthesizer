using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using JJ.Framework.Persistence;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0618
#pragma warning disable MSTEST0018
#pragma warning disable IDE0002

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class BitWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_Bits(int? init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, CoalesceBits(init));
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_Bits(int? init, int? value)
        {
            void AssertProp(Action<SynthBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceBits(init));
                
                setter(x.SynthBound);
                
                Assert_SynthBound_Getters (x, CoalesceBits(value));
                Assert_TapeBound_Getters  (x, CoalesceBits(init ));
                Assert_BuffBound_Getters  (x, CoalesceBits(init ));
                Assert_Independent_Getters(x, CoalesceBits(init ));
                Assert_Immutable_Getters  (x, CoalesceBits(init ));
                
                x.Record();
                Assert_All_Getters(x, CoalesceBits(value));
            }

            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .Bits    (value)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .Bits    (value)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.Bits    (value)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .WithBits(value)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .WithBits(value)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.WithBits(value)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .AsBits  (value)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .AsBits  (value)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.AsBits  (value)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetBits (value)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetBits (value)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetBits (value)));
            AssertProp(x => AreEqual(x.SynthWishes,    Bits    (x.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.FlowNode,       Bits    (x.FlowNode      , value)));
            AssertProp(x => AreEqual(x.ConfigResolver, Bits    (x.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthWishes,    WithBits(x.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.FlowNode,       WithBits(x.FlowNode      , value)));
            AssertProp(x => AreEqual(x.ConfigResolver, WithBits(x.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthWishes,    AsBits  (x.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.FlowNode,       AsBits  (x.FlowNode      , value)));
            AssertProp(x => AreEqual(x.ConfigResolver, AsBits  (x.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthWishes,    SetBits (x.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.FlowNode,       SetBits (x.FlowNode      , value)));
            AssertProp(x => AreEqual(x.ConfigResolver, SetBits (x.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .Bits    (x.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .Bits    (x.FlowNode      , value)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.Bits    (x.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .WithBits(x.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .WithBits(x.FlowNode      , value)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.WithBits(x.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .AsBits  (x.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .AsBits  (x.FlowNode      , value)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.AsBits  (x.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .SetBits (x.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .SetBits (x.FlowNode      , value)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.SetBits (x.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthWishes,    BitExtensionWishes        .WithBits(x.SynthWishes   , value))); 
            AssertProp(x => AreEqual(x.FlowNode,       BitExtensionWishes        .WithBits(x.FlowNode      , value)));
            AssertProp(x => AreEqual(x.ConfigResolver, BitExtensionWishesAccessor.WithBits(x.ConfigResolver, value)));
            AssertProp(x => { if (value ==  8) AreEqual(x.SynthWishes,    () => x.SynthWishes   .With8Bit (        ));
                              if (value == 16) AreEqual(x.SynthWishes,    () => x.SynthWishes   .With16Bit(        )); 
                              if (value == 32) AreEqual(x.SynthWishes,    () => x.SynthWishes   .With32Bit(        ));
                              if (!Has(value)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithBits (value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.SynthWishes,    () => x.SynthWishes   .As8Bit   (        ));
                              if (value == 16) AreEqual(x.SynthWishes,    () => x.SynthWishes   .As16Bit  (        )); 
                              if (value == 32) AreEqual(x.SynthWishes,    () => x.SynthWishes   .As32Bit  (        ));
                              if (!Has(value)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsBits   (value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Set8Bit  (        ));
                              if (value == 16) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Set16Bit (        )); 
                              if (value == 32) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Set32Bit (        ));
                              if (!Has(value)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetBits  (value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.FlowNode,       () => x.FlowNode      .With8Bit (        ));
                              if (value == 16) AreEqual(x.FlowNode,       () => x.FlowNode      .With16Bit(        )); 
                              if (value == 32) AreEqual(x.FlowNode,       () => x.FlowNode      .With32Bit(        ));
                              if (!Has(value)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithBits (value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.FlowNode,       () => x.FlowNode      .As8Bit   (        ));
                              if (value == 16) AreEqual(x.FlowNode,       () => x.FlowNode      .As16Bit  (        )); 
                              if (value == 32) AreEqual(x.FlowNode,       () => x.FlowNode      .As32Bit  (        ));
                              if (!Has(value)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsBits   (value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.FlowNode,       () => x.FlowNode      .Set8Bit  (        ));
                              if (value == 16) AreEqual(x.FlowNode,       () => x.FlowNode      .Set16Bit (        )); 
                              if (value == 32) AreEqual(x.FlowNode,       () => x.FlowNode      .Set32Bit (        ));
                              if (!Has(value)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetBits  (value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.ConfigResolver, () => x.ConfigResolver.With8Bit (        ));
                              if (value == 16) AreEqual(x.ConfigResolver, () => x.ConfigResolver.With16Bit(        )); 
                              if (value == 32) AreEqual(x.ConfigResolver, () => x.ConfigResolver.With32Bit(        ));
                              if (!Has(value)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithBits (value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.ConfigResolver, () => x.ConfigResolver.As8Bit   (        ));
                              if (value == 16) AreEqual(x.ConfigResolver, () => x.ConfigResolver.As16Bit  (        )); 
                              if (value == 32) AreEqual(x.ConfigResolver, () => x.ConfigResolver.As32Bit  (        ));
                              if (!Has(value)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsBits   (value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Set8Bit  (        ));
                              if (value == 16) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Set16Bit (        )); 
                              if (value == 32) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Set32Bit (        ));
                              if (!Has(value)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetBits  (value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.SynthWishes,    () => With8Bit (x.SynthWishes             ));
                              if (value == 16) AreEqual(x.SynthWishes,    () => With16Bit(x.SynthWishes             )); 
                              if (value == 32) AreEqual(x.SynthWishes,    () => With32Bit(x.SynthWishes             ));
                              if (!Has(value)) AreEqual(x.SynthWishes,    () => WithBits (x.SynthWishes,    value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.SynthWishes,    () => As8Bit   (x.SynthWishes             ));
                              if (value == 16) AreEqual(x.SynthWishes,    () => As16Bit  (x.SynthWishes             )); 
                              if (value == 32) AreEqual(x.SynthWishes,    () => As32Bit  (x.SynthWishes             ));
                              if (!Has(value)) AreEqual(x.SynthWishes,    () => AsBits   (x.SynthWishes,    value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.SynthWishes,    () => Set8Bit  (x.SynthWishes             ));
                              if (value == 16) AreEqual(x.SynthWishes,    () => Set16Bit (x.SynthWishes             )); 
                              if (value == 32) AreEqual(x.SynthWishes,    () => Set32Bit (x.SynthWishes             ));
                              if (!Has(value)) AreEqual(x.SynthWishes,    () => SetBits  (x.SynthWishes,    value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.FlowNode,       () => With8Bit (x.FlowNode                ));
                              if (value == 16) AreEqual(x.FlowNode,       () => With16Bit(x.FlowNode                )); 
                              if (value == 32) AreEqual(x.FlowNode,       () => With32Bit(x.FlowNode                ));
                              if (!Has(value)) AreEqual(x.FlowNode,       () => WithBits (x.FlowNode,       value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.FlowNode,       () => As8Bit   (x.FlowNode                ));
                              if (value == 16) AreEqual(x.FlowNode,       () => As16Bit  (x.FlowNode                )); 
                              if (value == 32) AreEqual(x.FlowNode,       () => As32Bit  (x.FlowNode                ));
                              if (!Has(value)) AreEqual(x.FlowNode,       () => AsBits   (x.FlowNode,       value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.FlowNode,       () => Set8Bit  (x.FlowNode                ));
                              if (value == 16) AreEqual(x.FlowNode,       () => Set16Bit (x.FlowNode                )); 
                              if (value == 32) AreEqual(x.FlowNode,       () => Set32Bit (x.FlowNode                ));
                              if (!Has(value)) AreEqual(x.FlowNode,       () => SetBits  (x.FlowNode,       value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.ConfigResolver, () => With8Bit (x.ConfigResolver          ));
                              if (value == 16) AreEqual(x.ConfigResolver, () => With16Bit(x.ConfigResolver          )); 
                              if (value == 32) AreEqual(x.ConfigResolver, () => With32Bit(x.ConfigResolver          ));
                              if (!Has(value)) AreEqual(x.ConfigResolver, () => WithBits (x.ConfigResolver, value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.ConfigResolver, () => As8Bit   (x.ConfigResolver          ));
                              if (value == 16) AreEqual(x.ConfigResolver, () => As16Bit  (x.ConfigResolver          )); 
                              if (value == 32) AreEqual(x.ConfigResolver, () => As32Bit  (x.ConfigResolver          ));
                              if (!Has(value)) AreEqual(x.ConfigResolver, () => AsBits   (x.ConfigResolver, value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.ConfigResolver, () => Set8Bit  (x.ConfigResolver          ));
                              if (value == 16) AreEqual(x.ConfigResolver, () => Set16Bit (x.ConfigResolver          )); 
                              if (value == 32) AreEqual(x.ConfigResolver, () => Set32Bit (x.ConfigResolver          ));
                              if (!Has(value)) AreEqual(x.ConfigResolver, () => SetBits  (x.ConfigResolver, value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.SynthWishes,    () => ConfigWishes        .With8Bit (x.SynthWishes             ));
                              if (value == 16) AreEqual(x.SynthWishes,    () => ConfigWishes        .With16Bit(x.SynthWishes             )); 
                              if (value == 32) AreEqual(x.SynthWishes,    () => ConfigWishes        .With32Bit(x.SynthWishes             ));
                              if (!Has(value)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithBits (x.SynthWishes,    value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.SynthWishes,    () => ConfigWishes        .As8Bit   (x.SynthWishes             ));
                              if (value == 16) AreEqual(x.SynthWishes,    () => ConfigWishes        .As16Bit  (x.SynthWishes             )); 
                              if (value == 32) AreEqual(x.SynthWishes,    () => ConfigWishes        .As32Bit  (x.SynthWishes             ));
                              if (!Has(value)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsBits   (x.SynthWishes,    value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.SynthWishes,    () => ConfigWishes        .Set8Bit  (x.SynthWishes             ));
                              if (value == 16) AreEqual(x.SynthWishes,    () => ConfigWishes        .Set16Bit (x.SynthWishes             )); 
                              if (value == 32) AreEqual(x.SynthWishes,    () => ConfigWishes        .Set32Bit (x.SynthWishes             ));
                              if (!Has(value)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetBits  (x.SynthWishes,    value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.FlowNode,       () => ConfigWishes        .With8Bit (x.FlowNode                ));
                              if (value == 16) AreEqual(x.FlowNode,       () => ConfigWishes        .With16Bit(x.FlowNode                )); 
                              if (value == 32) AreEqual(x.FlowNode,       () => ConfigWishes        .With32Bit(x.FlowNode                ));
                              if (!Has(value)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithBits (x.FlowNode,       value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.FlowNode,       () => ConfigWishes        .As8Bit   (x.FlowNode                ));
                              if (value == 16) AreEqual(x.FlowNode,       () => ConfigWishes        .As16Bit  (x.FlowNode                )); 
                              if (value == 32) AreEqual(x.FlowNode,       () => ConfigWishes        .As32Bit  (x.FlowNode                ));
                              if (!Has(value)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsBits   (x.FlowNode,       value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.FlowNode,       () => ConfigWishes        .Set8Bit  (x.FlowNode                ));
                              if (value == 16) AreEqual(x.FlowNode,       () => ConfigWishes        .Set16Bit (x.FlowNode                )); 
                              if (value == 32) AreEqual(x.FlowNode,       () => ConfigWishes        .Set32Bit (x.FlowNode                ));
                              if (!Has(value)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetBits  (x.FlowNode,       value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.With8Bit (x.ConfigResolver          ));
                              if (value == 16) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.With16Bit(x.ConfigResolver          )); 
                              if (value == 32) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.With32Bit(x.ConfigResolver          ));
                              if (!Has(value)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithBits (x.ConfigResolver, value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.As8Bit   (x.ConfigResolver          ));
                              if (value == 16) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.As16Bit  (x.ConfigResolver          )); 
                              if (value == 32) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.As32Bit  (x.ConfigResolver          ));
                              if (!Has(value)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsBits   (x.ConfigResolver, value)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Set8Bit  (x.ConfigResolver          ));
                              if (value == 16) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Set16Bit (x.ConfigResolver          )); 
                              if (value == 32) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Set32Bit (x.ConfigResolver          ));
                              if (!Has(value)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetBits  (x.ConfigResolver, value)); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_Bits(int init, int value)
        {
            void AssertProp(Action<TapeBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x.TapeBound);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, value);
                Assert_BuffBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x =>                               x.TapeConfig .Bits    = value);
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .Bits     (value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .Bits     (value)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.Bits     (value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .Bits     (value)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .WithBits (value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .WithBits (value)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.WithBits (value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .WithBits (value)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .AsBits   (value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .AsBits   (value)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.AsBits   (value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .AsBits   (value)));
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .SetBits  (value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .SetBits  (value)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.SetBits  (value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .SetBits  (value)));
            AssertProp(x => AreEqual(x.Tape,        () => Bits     (x.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => Bits     (x.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeActions, () => Bits     (x.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => Bits     (x.TapeAction , value)));
            AssertProp(x => AreEqual(x.Tape,        () => WithBits (x.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => WithBits (x.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeActions, () => WithBits (x.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => WithBits (x.TapeAction , value)));
            AssertProp(x => AreEqual(x.Tape,        () => AsBits   (x.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => AsBits   (x.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeActions, () => AsBits   (x.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => AsBits   (x.TapeAction , value)));
            AssertProp(x => AreEqual(x.Tape,        () => SetBits  (x.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => SetBits  (x.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeActions, () => SetBits  (x.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => SetBits  (x.TapeAction , value)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.Bits     (x.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.Bits     (x.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.Bits     (x.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.Bits     (x.TapeAction , value)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.WithBits (x.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.WithBits (x.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.WithBits (x.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.WithBits (x.TapeAction , value)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.AsBits   (x.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.AsBits   (x.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.AsBits   (x.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.AsBits   (x.TapeAction , value)));
            AssertProp(x => AreEqual(x.Tape,        () => ConfigWishes.SetBits  (x.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => ConfigWishes.SetBits  (x.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeActions, () => ConfigWishes.SetBits  (x.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeAction,  () => ConfigWishes.SetBits  (x.TapeAction , value)));
            AssertProp(x => { if (value ==  8) AreEqual(x.Tape,        () => x.Tape       .With8Bit ());
                              if (value == 16) AreEqual(x.Tape,        () => x.Tape       .With16Bit());
                              if (value == 32) AreEqual(x.Tape,        () => x.Tape       .With32Bit()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeConfig,  () => x.TapeConfig .With8Bit ());
                              if (value == 16) AreEqual(x.TapeConfig,  () => x.TapeConfig .With16Bit());
                              if (value == 32) AreEqual(x.TapeConfig,  () => x.TapeConfig .With32Bit()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeActions, () => x.TapeActions.With8Bit ());
                              if (value == 16) AreEqual(x.TapeActions, () => x.TapeActions.With16Bit());
                              if (value == 32) AreEqual(x.TapeActions, () => x.TapeActions.With32Bit()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeAction,  () => x.TapeAction .With8Bit ());
                              if (value == 16) AreEqual(x.TapeAction,  () => x.TapeAction .With16Bit());
                              if (value == 32) AreEqual(x.TapeAction,  () => x.TapeAction .With32Bit()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Tape,        () => x.Tape       .As8Bit   ());
                              if (value == 16) AreEqual(x.Tape,        () => x.Tape       .As16Bit  ());
                              if (value == 32) AreEqual(x.Tape,        () => x.Tape       .As32Bit  ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeConfig,  () => x.TapeConfig .As8Bit   ());
                              if (value == 16) AreEqual(x.TapeConfig,  () => x.TapeConfig .As16Bit  ());
                              if (value == 32) AreEqual(x.TapeConfig,  () => x.TapeConfig .As32Bit  ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeActions, () => x.TapeActions.As8Bit   ());
                              if (value == 16) AreEqual(x.TapeActions, () => x.TapeActions.As16Bit  ());
                              if (value == 32) AreEqual(x.TapeActions, () => x.TapeActions.As32Bit  ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeAction,  () => x.TapeAction .As8Bit   ());
                              if (value == 16) AreEqual(x.TapeAction,  () => x.TapeAction .As16Bit  ());
                              if (value == 32) AreEqual(x.TapeAction,  () => x.TapeAction .As32Bit  ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Tape,        () => x.Tape       .Set8Bit  ());
                              if (value == 16) AreEqual(x.Tape,        () => x.Tape       .Set16Bit ());
                              if (value == 32) AreEqual(x.Tape,        () => x.Tape       .Set32Bit ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeConfig,  () => x.TapeConfig .Set8Bit  ());
                              if (value == 16) AreEqual(x.TapeConfig,  () => x.TapeConfig .Set16Bit ());
                              if (value == 32) AreEqual(x.TapeConfig,  () => x.TapeConfig .Set32Bit ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeActions, () => x.TapeActions.Set8Bit  ());
                              if (value == 16) AreEqual(x.TapeActions, () => x.TapeActions.Set16Bit ());
                              if (value == 32) AreEqual(x.TapeActions, () => x.TapeActions.Set32Bit ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeAction,  () => x.TapeAction .Set8Bit  ());
                              if (value == 16) AreEqual(x.TapeAction,  () => x.TapeAction .Set16Bit ());
                              if (value == 32) AreEqual(x.TapeAction,  () => x.TapeAction .Set32Bit ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Tape,        () => With8Bit (x.Tape       ));
                              if (value == 16) AreEqual(x.Tape,        () => With16Bit(x.Tape       ));
                              if (value == 32) AreEqual(x.Tape,        () => With32Bit(x.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeConfig,  () => With8Bit (x.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeConfig,  () => With16Bit(x.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeConfig,  () => With32Bit(x.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeActions, () => With8Bit (x.TapeActions));
                              if (value == 16) AreEqual(x.TapeActions, () => With16Bit(x.TapeActions));
                              if (value == 32) AreEqual(x.TapeActions, () => With32Bit(x.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeAction,  () => With8Bit (x.TapeAction ));
                              if (value == 16) AreEqual(x.TapeAction,  () => With16Bit(x.TapeAction ));
                              if (value == 32) AreEqual(x.TapeAction,  () => With32Bit(x.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Tape,        () => As8Bit   (x.Tape       ));
                              if (value == 16) AreEqual(x.Tape,        () => As16Bit  (x.Tape       ));
                              if (value == 32) AreEqual(x.Tape,        () => As32Bit  (x.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeConfig,  () => As8Bit   (x.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeConfig,  () => As16Bit  (x.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeConfig,  () => As32Bit  (x.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeActions, () => As8Bit   (x.TapeActions));
                              if (value == 16) AreEqual(x.TapeActions, () => As16Bit  (x.TapeActions));
                              if (value == 32) AreEqual(x.TapeActions, () => As32Bit  (x.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeAction,  () => As8Bit   (x.TapeAction ));
                              if (value == 16) AreEqual(x.TapeAction,  () => As16Bit  (x.TapeAction ));
                              if (value == 32) AreEqual(x.TapeAction,  () => As32Bit  (x.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Tape,        () => Set8Bit  (x.Tape       ));
                              if (value == 16) AreEqual(x.Tape,        () => Set16Bit (x.Tape       ));
                              if (value == 32) AreEqual(x.Tape,        () => Set32Bit (x.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeConfig,  () => Set8Bit  (x.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeConfig,  () => Set16Bit (x.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeConfig,  () => Set32Bit (x.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeActions, () => Set8Bit  (x.TapeActions));
                              if (value == 16) AreEqual(x.TapeActions, () => Set16Bit (x.TapeActions));
                              if (value == 32) AreEqual(x.TapeActions, () => Set32Bit (x.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeAction,  () => Set8Bit  (x.TapeAction ));
                              if (value == 16) AreEqual(x.TapeAction,  () => Set16Bit (x.TapeAction ));
                              if (value == 32) AreEqual(x.TapeAction,  () => Set32Bit (x.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Tape,        () => ConfigWishes.With8Bit (x.Tape       ));
                              if (value == 16) AreEqual(x.Tape,        () => ConfigWishes.With16Bit(x.Tape       ));
                              if (value == 32) AreEqual(x.Tape,        () => ConfigWishes.With32Bit(x.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeConfig,  () => ConfigWishes.With8Bit (x.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeConfig,  () => ConfigWishes.With16Bit(x.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeConfig,  () => ConfigWishes.With32Bit(x.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeActions, () => ConfigWishes.With8Bit (x.TapeActions));
                              if (value == 16) AreEqual(x.TapeActions, () => ConfigWishes.With16Bit(x.TapeActions));
                              if (value == 32) AreEqual(x.TapeActions, () => ConfigWishes.With32Bit(x.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeAction,  () => ConfigWishes.With8Bit (x.TapeAction ));
                              if (value == 16) AreEqual(x.TapeAction,  () => ConfigWishes.With16Bit(x.TapeAction ));
                              if (value == 32) AreEqual(x.TapeAction,  () => ConfigWishes.With32Bit(x.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Tape,        () => ConfigWishes.As8Bit   (x.Tape       ));
                              if (value == 16) AreEqual(x.Tape,        () => ConfigWishes.As16Bit  (x.Tape       ));
                              if (value == 32) AreEqual(x.Tape,        () => ConfigWishes.As32Bit  (x.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeConfig,  () => ConfigWishes.As8Bit   (x.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeConfig,  () => ConfigWishes.As16Bit  (x.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeConfig,  () => ConfigWishes.As32Bit  (x.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeActions, () => ConfigWishes.As8Bit   (x.TapeActions));
                              if (value == 16) AreEqual(x.TapeActions, () => ConfigWishes.As16Bit  (x.TapeActions));
                              if (value == 32) AreEqual(x.TapeActions, () => ConfigWishes.As32Bit  (x.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeAction,  () => ConfigWishes.As8Bit   (x.TapeAction ));
                              if (value == 16) AreEqual(x.TapeAction,  () => ConfigWishes.As16Bit  (x.TapeAction ));
                              if (value == 32) AreEqual(x.TapeAction,  () => ConfigWishes.As32Bit  (x.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Tape,        () => ConfigWishes.Set8Bit  (x.Tape       ));
                              if (value == 16) AreEqual(x.Tape,        () => ConfigWishes.Set16Bit (x.Tape       ));
                              if (value == 32) AreEqual(x.Tape,        () => ConfigWishes.Set32Bit (x.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeConfig,  () => ConfigWishes.Set8Bit  (x.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeConfig,  () => ConfigWishes.Set16Bit (x.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeConfig,  () => ConfigWishes.Set32Bit (x.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeActions, () => ConfigWishes.Set8Bit  (x.TapeActions));
                              if (value == 16) AreEqual(x.TapeActions, () => ConfigWishes.Set16Bit (x.TapeActions));
                              if (value == 32) AreEqual(x.TapeActions, () => ConfigWishes.Set32Bit (x.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeAction,  () => ConfigWishes.Set8Bit  (x.TapeAction ));
                              if (value == 16) AreEqual(x.TapeAction,  () => ConfigWishes.Set16Bit (x.TapeAction ));
                              if (value == 32) AreEqual(x.TapeAction,  () => ConfigWishes.Set32Bit (x.TapeAction )); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_Bits(int init, int value)
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

            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .Bits     (value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Bits     (value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .WithBits (value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithBits (value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .AsBits   (value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsBits   (value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .SetBits  (value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetBits  (value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => Bits    (x.Buff,            value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => Bits    (x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => WithBits(x.Buff,            value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => WithBits(x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => AsBits  (x.Buff,            value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => AsBits  (x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => SetBits (x.Buff,            value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => SetBits (x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.Bits    (x.Buff,            value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.Bits    (x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.WithBits(x.Buff,            value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.WithBits(x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.AsBits  (x.Buff,            value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.AsBits  (x.AudioFileOutput, value, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.SetBits (x.Buff,            value, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.SetBits (x.AudioFileOutput, value, context)));
            AssertProp(x => { if (value ==  8) AreEqual(x.Buff,            () => x.Buff           .With8Bit (context));
                              if (value == 16) AreEqual(x.Buff,            () => x.Buff           .With16Bit(context));
                              if (value == 32) AreEqual(x.Buff,            () => x.Buff           .With32Bit(context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With8Bit (context));
                              if (value == 16) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With16Bit(context));
                              if (value == 32) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With32Bit(context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Buff,            () => x.Buff           .As8Bit   (context));
                              if (value == 16) AreEqual(x.Buff,            () => x.Buff           .As16Bit  (context));
                              if (value == 32) AreEqual(x.Buff,            () => x.Buff           .As32Bit  (context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.As8Bit   (context));
                              if (value == 16) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.As16Bit  (context));
                              if (value == 32) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.As32Bit  (context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Buff,            () => x.Buff           .Set8Bit  (context));
                              if (value == 16) AreEqual(x.Buff,            () => x.Buff           .Set16Bit (context));
                              if (value == 32) AreEqual(x.Buff,            () => x.Buff           .Set32Bit (context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Set8Bit  (context));
                              if (value == 16) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Set16Bit (context));
                              if (value == 32) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Set32Bit (context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Buff,            () => With8Bit (x.Buff           , context));
                              if (value == 16) AreEqual(x.Buff,            () => With16Bit(x.Buff           , context));
                              if (value == 32) AreEqual(x.Buff,            () => With32Bit(x.Buff           , context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.AudioFileOutput, () => With8Bit (x.AudioFileOutput, context));
                              if (value == 16) AreEqual(x.AudioFileOutput, () => With16Bit(x.AudioFileOutput, context));
                              if (value == 32) AreEqual(x.AudioFileOutput, () => With32Bit(x.AudioFileOutput, context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Buff,            () => As8Bit   (x.Buff           , context));
                              if (value == 16) AreEqual(x.Buff,            () => As16Bit  (x.Buff           , context));
                              if (value == 32) AreEqual(x.Buff,            () => As32Bit  (x.Buff           , context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.AudioFileOutput, () => As8Bit   (x.AudioFileOutput, context));
                              if (value == 16) AreEqual(x.AudioFileOutput, () => As16Bit  (x.AudioFileOutput, context));
                              if (value == 32) AreEqual(x.AudioFileOutput, () => As32Bit  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Buff,            () => Set8Bit  (x.Buff           , context));
                              if (value == 16) AreEqual(x.Buff,            () => Set16Bit (x.Buff           , context));
                              if (value == 32) AreEqual(x.Buff,            () => Set32Bit (x.Buff           , context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.AudioFileOutput, () => Set8Bit  (x.AudioFileOutput, context));
                              if (value == 16) AreEqual(x.AudioFileOutput, () => Set16Bit (x.AudioFileOutput, context));
                              if (value == 32) AreEqual(x.AudioFileOutput, () => Set32Bit (x.AudioFileOutput, context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Buff,            () => ConfigWishes.With8Bit (x.Buff           , context));
                              if (value == 16) AreEqual(x.Buff,            () => ConfigWishes.With16Bit(x.Buff           , context));
                              if (value == 32) AreEqual(x.Buff,            () => ConfigWishes.With32Bit(x.Buff           , context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.AudioFileOutput, () => ConfigWishes.With8Bit (x.AudioFileOutput, context));
                              if (value == 16) AreEqual(x.AudioFileOutput, () => ConfigWishes.With16Bit(x.AudioFileOutput, context));
                              if (value == 32) AreEqual(x.AudioFileOutput, () => ConfigWishes.With32Bit(x.AudioFileOutput, context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Buff,            () => ConfigWishes.As8Bit   (x.Buff           , context));
                              if (value == 16) AreEqual(x.Buff,            () => ConfigWishes.As16Bit  (x.Buff           , context));
                              if (value == 32) AreEqual(x.Buff,            () => ConfigWishes.As32Bit  (x.Buff           , context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.AudioFileOutput, () => ConfigWishes.As8Bit   (x.AudioFileOutput, context));
                              if (value == 16) AreEqual(x.AudioFileOutput, () => ConfigWishes.As16Bit  (x.AudioFileOutput, context));
                              if (value == 32) AreEqual(x.AudioFileOutput, () => ConfigWishes.As32Bit  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.Buff,            () => ConfigWishes.Set8Bit  (x.Buff           , context));
                              if (value == 16) AreEqual(x.Buff,            () => ConfigWishes.Set16Bit (x.Buff           , context));
                              if (value == 32) AreEqual(x.Buff,            () => ConfigWishes.Set32Bit (x.Buff           , context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.AudioFileOutput, () => ConfigWishes.Set8Bit  (x.AudioFileOutput, context));
                              if (value == 16) AreEqual(x.AudioFileOutput, () => ConfigWishes.Set16Bit (x.AudioFileOutput, context));
                              if (value == 32) AreEqual(x.AudioFileOutput, () => ConfigWishes.Set32Bit (x.AudioFileOutput, context)); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_Bits(int init, int value)
        {
            // Independent after Taping

            // Sample
            {
                ConfigTestEntities x = default;
                
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
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Bits     (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithBits (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsBits   (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetBits  (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => Bits    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithBits(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => AsBits  (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetBits (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.Bits    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithBits(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.AsBits  (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetBits (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With8Bit (x.SynthBound.Context));
                                   if (value == 16) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With16Bit(x.SynthBound.Context));
                                   if (value == 32) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With32Bit(x.SynthBound.Context)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.Sample, () => x.Independent.Sample.As8Bit   (x.SynthBound.Context));
                                   if (value == 16) AreEqual(x.Independent.Sample, () => x.Independent.Sample.As16Bit  (x.SynthBound.Context));
                                   if (value == 32) AreEqual(x.Independent.Sample, () => x.Independent.Sample.As32Bit  (x.SynthBound.Context)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Set8Bit  (x.SynthBound.Context));
                                   if (value == 16) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Set16Bit (x.SynthBound.Context));
                                   if (value == 32) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Set32Bit (x.SynthBound.Context)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.Sample, () => With8Bit (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 16) AreEqual(x.Independent.Sample, () => With16Bit(x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 32) AreEqual(x.Independent.Sample, () => With32Bit(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.Sample, () => As8Bit   (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 16) AreEqual(x.Independent.Sample, () => As16Bit  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 32) AreEqual(x.Independent.Sample, () => As32Bit  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.Sample, () => Set8Bit  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 16) AreEqual(x.Independent.Sample, () => Set16Bit (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 32) AreEqual(x.Independent.Sample, () => Set32Bit (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.Sample, () => ConfigWishes.With8Bit (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 16) AreEqual(x.Independent.Sample, () => ConfigWishes.With16Bit(x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 32) AreEqual(x.Independent.Sample, () => ConfigWishes.With32Bit(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.Sample, () => ConfigWishes.As8Bit   (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 16) AreEqual(x.Independent.Sample, () => ConfigWishes.As16Bit  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 32) AreEqual(x.Independent.Sample, () => ConfigWishes.As32Bit  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.Sample, () => ConfigWishes.Set8Bit  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 16) AreEqual(x.Independent.Sample, () => ConfigWishes.Set16Bit (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 32) AreEqual(x.Independent.Sample, () => ConfigWishes.Set32Bit (x.Independent.Sample, x.SynthBound.Context)); });
            }
            
            // AudioInfoWish
            {
                ConfigTestEntities x = default;

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
                
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Bits   = value);
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Bits    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithBits(value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.AsBits  (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetBits (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => Bits    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => WithBits(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => AsBits  (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SetBits (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Bits    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithBits(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.AsBits  (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetBits (x.Independent.AudioInfoWish, value)));
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With8Bit ());
                                   if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With16Bit());
                                   if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With32Bit()); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.As8Bit   ());
                                   if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.As16Bit  ());
                                   if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.As32Bit  ()); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Set8Bit  ());
                                   if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Set16Bit ());
                                   if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Set32Bit ()); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => With8Bit (x.Independent.AudioInfoWish));
                                   if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => With16Bit(x.Independent.AudioInfoWish));
                                   if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => With32Bit(x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => As8Bit   (x.Independent.AudioInfoWish));
                                   if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => As16Bit  (x.Independent.AudioInfoWish));
                                   if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => As32Bit  (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => Set8Bit  (x.Independent.AudioInfoWish));
                                   if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => Set16Bit (x.Independent.AudioInfoWish));
                                   if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => Set32Bit (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.With8Bit (x.Independent.AudioInfoWish));
                                   if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.With16Bit(x.Independent.AudioInfoWish));
                                   if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.With32Bit(x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.As8Bit   (x.Independent.AudioInfoWish));
                                   if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.As16Bit  (x.Independent.AudioInfoWish));
                                   if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.As32Bit  (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Set8Bit  (x.Independent.AudioInfoWish));
                                   if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Set16Bit (x.Independent.AudioInfoWish));
                                   if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Set32Bit (x.Independent.AudioInfoWish)); });
            }
                        
            // AudioFileInfo
            {
                ConfigTestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, value);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                

                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Bits    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithBits(value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.AsBits  (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetBits (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => Bits    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => WithBits(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => AsBits  (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => SetBits (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Bits    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithBits(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.AsBits  (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetBits (x.Independent.AudioFileInfo, value)));
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With8Bit ());
                                   if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With16Bit());
                                   if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With32Bit()); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.As8Bit   ());
                                   if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.As16Bit  ());
                                   if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.As32Bit  ()); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Set8Bit  ());
                                   if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Set16Bit ());
                                   if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Set32Bit ()); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => With8Bit (x.Independent.AudioFileInfo));
                                   if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => With16Bit(x.Independent.AudioFileInfo));
                                   if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => With32Bit(x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => As8Bit   (x.Independent.AudioFileInfo));
                                   if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => As16Bit  (x.Independent.AudioFileInfo));
                                   if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => As32Bit  (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => Set8Bit  (x.Independent.AudioFileInfo));
                                   if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => Set16Bit (x.Independent.AudioFileInfo));
                                   if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => Set32Bit (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.With8Bit (x.Independent.AudioFileInfo));
                                   if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.With16Bit(x.Independent.AudioFileInfo));
                                   if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.With32Bit(x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.As8Bit   (x.Independent.AudioFileInfo));
                                   if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.As16Bit  (x.Independent.AudioFileInfo));
                                   if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.As32Bit  (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Set8Bit  (x.Independent.AudioFileInfo));
                                   if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Set16Bit (x.Independent.AudioFileInfo));
                                   if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Set32Bit (x.Independent.AudioFileInfo)); });
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Immutable_Bits(int init, int value)
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
                
                AssertProp(() => x.Immutable.WavHeader.Bits    (value));
                AssertProp(() => x.Immutable.WavHeader.WithBits(value));
                AssertProp(() => x.Immutable.WavHeader.AsBits  (value));
                AssertProp(() => x.Immutable.WavHeader.SetBits (value));
                AssertProp(() => Bits    (x.Immutable.WavHeader, value));
                AssertProp(() => WithBits(x.Immutable.WavHeader, value));
                AssertProp(() => AsBits  (x.Immutable.WavHeader, value));
                AssertProp(() => SetBits (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.Bits    (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.WithBits(x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.AsBits  (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.SetBits (x.Immutable.WavHeader, value));
                AssertProp(() => { if (value ==  8) return x.Immutable.WavHeader.With8Bit ();
                                   if (value == 16) return x.Immutable.WavHeader.With16Bit();
                                   if (value == 32) return x.Immutable.WavHeader.With32Bit(); return default; });
                AssertProp(() => { if (value ==  8) return x.Immutable.WavHeader.As8Bit   ();
                                   if (value == 16) return x.Immutable.WavHeader.As16Bit  ();
                                   if (value == 32) return x.Immutable.WavHeader.As32Bit  (); return default; });
                AssertProp(() => { if (value ==  8) return x.Immutable.WavHeader.Set8Bit  ();
                                   if (value == 16) return x.Immutable.WavHeader.Set16Bit ();
                                   if (value == 32) return x.Immutable.WavHeader.Set32Bit (); return default; });
                AssertProp(() => { if (value ==  8) return With8Bit (x.Immutable.WavHeader);
                                   if (value == 16) return With16Bit(x.Immutable.WavHeader);
                                   if (value == 32) return With32Bit(x.Immutable.WavHeader); return default; });
                AssertProp(() => { if (value ==  8) return As8Bit   (x.Immutable.WavHeader);
                                   if (value == 16) return As16Bit  (x.Immutable.WavHeader);
                                   if (value == 32) return As32Bit  (x.Immutable.WavHeader); return default; });
                AssertProp(() => { if (value ==  8) return Set8Bit  (x.Immutable.WavHeader);
                                   if (value == 16) return Set16Bit (x.Immutable.WavHeader);
                                   if (value == 32) return Set32Bit (x.Immutable.WavHeader); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.With8Bit (x.Immutable.WavHeader);
                                   if (value == 16) return ConfigWishes.With16Bit(x.Immutable.WavHeader);
                                   if (value == 32) return ConfigWishes.With32Bit(x.Immutable.WavHeader); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.As8Bit   (x.Immutable.WavHeader);
                                   if (value == 16) return ConfigWishes.As16Bit  (x.Immutable.WavHeader);
                                   if (value == 32) return ConfigWishes.As32Bit  (x.Immutable.WavHeader); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.Set8Bit  (x.Immutable.WavHeader);
                                   if (value == 16) return ConfigWishes.Set16Bit (x.Immutable.WavHeader);
                                   if (value == 32) return ConfigWishes.Set32Bit (x.Immutable.WavHeader); return default; });
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
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.Bits    (value));
                AssertProp(() => x.Immutable.SampleDataTypeEnum.WithBits(value));
                AssertProp(() => x.Immutable.SampleDataTypeEnum.AsBits  (value));
                AssertProp(() => x.Immutable.SampleDataTypeEnum.SetBits (value));
                AssertProp(() => value.BitsToEnum());
                AssertProp(() => Bits    (x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => WithBits(x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => AsBits  (x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => SetBits (x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => BitsToEnum(value));
                AssertProp(() => ConfigWishes.Bits    (x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => ConfigWishes.WithBits(x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => ConfigWishes.AsBits  (x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => ConfigWishes.SetBits (x.Immutable.SampleDataTypeEnum, value));
                AssertProp(() => ConfigWishes.BitsToEnum(value));
                
                AssertProp(() => { if (value ==  8) return x.Immutable.SampleDataTypeEnum.With8Bit ();
                                   if (value == 16) return x.Immutable.SampleDataTypeEnum.With16Bit();
                                   if (value == 32) return x.Immutable.SampleDataTypeEnum.With32Bit(); return default; });
                AssertProp(() => { if (value ==  8) return x.Immutable.SampleDataTypeEnum.As8Bit   ();
                                   if (value == 16) return x.Immutable.SampleDataTypeEnum.As16Bit  ();
                                   if (value == 32) return x.Immutable.SampleDataTypeEnum.As32Bit  (); return default; });
                AssertProp(() => { if (value ==  8) return x.Immutable.SampleDataTypeEnum.Set8Bit  ();
                                   if (value == 16) return x.Immutable.SampleDataTypeEnum.Set16Bit ();
                                   if (value == 32) return x.Immutable.SampleDataTypeEnum.Set32Bit (); return default; });
                AssertProp(() => { if (value ==  8) return With8Bit (x.Immutable.SampleDataTypeEnum);
                                   if (value == 16) return With16Bit(x.Immutable.SampleDataTypeEnum);
                                   if (value == 32) return With32Bit(x.Immutable.SampleDataTypeEnum); return default; });
                AssertProp(() => { if (value ==  8) return As8Bit   (x.Immutable.SampleDataTypeEnum);
                                   if (value == 16) return As16Bit  (x.Immutable.SampleDataTypeEnum);
                                   if (value == 32) return As32Bit  (x.Immutable.SampleDataTypeEnum); return default; });
                AssertProp(() => { if (value ==  8) return Set8Bit  (x.Immutable.SampleDataTypeEnum);
                                   if (value == 16) return Set16Bit (x.Immutable.SampleDataTypeEnum);
                                   if (value == 32) return Set32Bit (x.Immutable.SampleDataTypeEnum); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.With8Bit (x.Immutable.SampleDataTypeEnum);
                                   if (value == 16) return ConfigWishes.With16Bit(x.Immutable.SampleDataTypeEnum);
                                   if (value == 32) return ConfigWishes.With32Bit(x.Immutable.SampleDataTypeEnum); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.As8Bit   (x.Immutable.SampleDataTypeEnum);
                                   if (value == 16) return ConfigWishes.As16Bit  (x.Immutable.SampleDataTypeEnum);
                                   if (value == 32) return ConfigWishes.As32Bit  (x.Immutable.SampleDataTypeEnum); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.Set8Bit  (x.Immutable.SampleDataTypeEnum);
                                   if (value == 16) return ConfigWishes.Set16Bit (x.Immutable.SampleDataTypeEnum);
                                   if (value == 32) return ConfigWishes.Set32Bit (x.Immutable.SampleDataTypeEnum); return default; });
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
            
                AssertProp(() => x.Immutable.SampleDataType.Bits    (value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SampleDataType.WithBits(value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SampleDataType.AsBits  (value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SampleDataType.SetBits (value, x.SynthBound.Context));
                AssertProp(() => value.BitsToEntity(x.SynthBound.Context));
                AssertProp(() => Bits    (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => WithBits(x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => AsBits  (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => SetBits (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => BitsToEntity(value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.Bits    (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.WithBits(x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.AsBits  (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.SetBits (x.Immutable.SampleDataType, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.BitsToEntity(value, x.SynthBound.Context));
                AssertProp(() => { if (value ==  8) return x.Immutable.SampleDataType.With8Bit  (x.SynthBound.Context);
                                   if (value == 16) return x.Immutable.SampleDataType.With16Bit (x.SynthBound.Context);
                                   if (value == 32) return x.Immutable.SampleDataType.With32Bit (x.SynthBound.Context); return default; });
                AssertProp(() => { if (value ==  8) return x.Immutable.SampleDataType.As8Bit    (x.SynthBound.Context);
                                   if (value == 16) return x.Immutable.SampleDataType.As16Bit   (x.SynthBound.Context);
                                   if (value == 32) return x.Immutable.SampleDataType.As32Bit   (x.SynthBound.Context); return default; });
                AssertProp(() => { if (value ==  8) return x.Immutable.SampleDataType.Set8Bit   (x.SynthBound.Context);
                                   if (value == 16) return x.Immutable.SampleDataType.Set16Bit  (x.SynthBound.Context);
                                   if (value == 32) return x.Immutable.SampleDataType.Set32Bit  (x.SynthBound.Context); return default; });
                AssertProp(() => { if (value ==  8) return With8Bit  (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 16) return With16Bit (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 32) return With32Bit (x.Immutable.SampleDataType, x.SynthBound.Context); return default; });
                AssertProp(() => { if (value ==  8) return As8Bit    (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 16) return As16Bit   (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 32) return As32Bit   (x.Immutable.SampleDataType, x.SynthBound.Context); return default; });
                AssertProp(() => { if (value ==  8) return Set8Bit   (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 16) return Set16Bit  (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 32) return Set32Bit  (x.Immutable.SampleDataType, x.SynthBound.Context); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.With8Bit  (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 16) return ConfigWishes.With16Bit (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 32) return ConfigWishes.With32Bit (x.Immutable.SampleDataType, x.SynthBound.Context); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.As8Bit    (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 16) return ConfigWishes.As16Bit   (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 32) return ConfigWishes.As32Bit   (x.Immutable.SampleDataType, x.SynthBound.Context); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.Set8Bit   (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 16) return ConfigWishes.Set16Bit  (x.Immutable.SampleDataType, x.SynthBound.Context);
                                   if (value == 32) return ConfigWishes.Set32Bit  (x.Immutable.SampleDataType, x.SynthBound.Context); return default; });
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

                AssertProp(() => x.Immutable.Type.Bits    (value));
                AssertProp(() => x.Immutable.Type.WithBits(value));
                AssertProp(() => x.Immutable.Type.AsBits  (value));
                AssertProp(() => x.Immutable.Type.SetBits (value));
                AssertProp(() => value.BitsToType());
                AssertProp(() => Bits    (x.Immutable.Type, value));
                AssertProp(() => WithBits(x.Immutable.Type, value));
                AssertProp(() => AsBits  (x.Immutable.Type, value));
                AssertProp(() => SetBits (x.Immutable.Type, value));
                AssertProp(() => BitsToType(value));
                AssertProp(() => ConfigWishes.Bits    (x.Immutable.Type, value));
                AssertProp(() => ConfigWishes.WithBits(x.Immutable.Type, value));
                AssertProp(() => ConfigWishes.AsBits  (x.Immutable.Type, value));
                AssertProp(() => ConfigWishes.SetBits (x.Immutable.Type, value));
                AssertProp(() => ConfigWishes.BitsToType(value));
                AssertProp(() => { if (value ==  8) return x.Immutable.Type.With8Bit ();
                                   if (value == 16) return x.Immutable.Type.With16Bit();
                                   if (value == 32) return x.Immutable.Type.With32Bit(); return default; });
                AssertProp(() => { if (value ==  8) return x.Immutable.Type.As8Bit   ();
                                   if (value == 16) return x.Immutable.Type.As16Bit  ();
                                   if (value == 32) return x.Immutable.Type.As32Bit  (); return default; });
                AssertProp(() => { if (value ==  8) return x.Immutable.Type.Set8Bit  ();
                                   if (value == 16) return x.Immutable.Type.Set16Bit ();
                                   if (value == 32) return x.Immutable.Type.Set32Bit (); return default; });
                AssertProp(() => { if (value ==  8) return With8Bit (x.Immutable.Type);
                                   if (value == 16) return With16Bit(x.Immutable.Type);
                                   if (value == 32) return With32Bit(x.Immutable.Type); return default; });
                AssertProp(() => { if (value ==  8) return As8Bit   (x.Immutable.Type);
                                   if (value == 16) return As16Bit  (x.Immutable.Type);
                                   if (value == 32) return As32Bit  (x.Immutable.Type); return default; });
                AssertProp(() => { if (value ==  8) return Set8Bit  (x.Immutable.Type);
                                   if (value == 16) return Set16Bit (x.Immutable.Type);
                                   if (value == 32) return Set32Bit (x.Immutable.Type); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.With8Bit (x.Immutable.Type);
                                   if (value == 16) return ConfigWishes.With16Bit(x.Immutable.Type);
                                   if (value == 32) return ConfigWishes.With32Bit(x.Immutable.Type); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.As8Bit   (x.Immutable.Type);
                                   if (value == 16) return ConfigWishes.As16Bit  (x.Immutable.Type);
                                   if (value == 32) return ConfigWishes.As32Bit  (x.Immutable.Type); return default; });
                AssertProp(() => { if (value ==  8) return ConfigWishes.Set8Bit  (x.Immutable.Type);
                                   if (value == 16) return ConfigWishes.Set16Bit (x.Immutable.Type);
                                   if (value == 32) return ConfigWishes.Set32Bit (x.Immutable.Type); return default; });
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
        }

        [TestMethod]
        public void ConfigSection_Bits()
        {
            // Synth-Bound. Immutable. Get-only.
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            AreEqual(DefaultBits,       () => configSection.Bits);
            AreEqual(DefaultBits,       () => configSection.Bits());
            AreEqual(DefaultBits,       () => configSection.GetBits());
            AreEqual(DefaultBits == 8,  () => configSection.Is8Bit());
            AreEqual(DefaultBits == 16, () => configSection.Is16Bit());
            AreEqual(DefaultBits == 32, () => configSection.Is32Bit());
        }

        [TestMethod]
        public void Bits_WithTypeArguments()
        {
            // Getters
            AreEqual( 8, () => Bits      <byte>());
            AreEqual(16, () => Bits      <short>());
            AreEqual(32, () => Bits      <float>());
            AreEqual( 8, () => GetBits   <byte>());
            AreEqual(16, () => GetBits   <short>());
            AreEqual(32, () => GetBits   <float>());
            AreEqual( 8, () => ToBits    <byte>());
            AreEqual(16, () => ToBits    <short>());
            AreEqual(32, () => ToBits    <float>());
            AreEqual( 8, () => TypeToBits<byte>());
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
            AreEqual(typeof(byte),  () => Bits<byte> (8));
            AreEqual(typeof(byte),  () => Bits<short>(8));
            AreEqual(typeof(byte),  () => Bits<float>(8));
            AreEqual(typeof(short), () => Bits<byte> (16));
            AreEqual(typeof(short), () => Bits<short>(16));
            AreEqual(typeof(short), () => Bits<float>(16));
            AreEqual(typeof(float), () => Bits<byte> (32));
            AreEqual(typeof(float), () => Bits<short>(32));
            AreEqual(typeof(float), () => Bits<float>(32));

            AreEqual(typeof(byte),  () => WithBits<byte> (8));
            AreEqual(typeof(byte),  () => WithBits<short>(8));
            AreEqual(typeof(byte),  () => WithBits<float>(8));
            AreEqual(typeof(short), () => WithBits<byte> (16));
            AreEqual(typeof(short), () => WithBits<short>(16));
            AreEqual(typeof(short), () => WithBits<float>(16));
            AreEqual(typeof(float), () => WithBits<byte> (32));
            AreEqual(typeof(float), () => WithBits<short>(32));
            AreEqual(typeof(float), () => WithBits<float>(32));
            
            // Shorthand Setters
            AreEqual(typeof(byte),  () => With8Bit<byte>());
            AreEqual(typeof(byte),  () => With8Bit<short>());
            AreEqual(typeof(byte),  () => With8Bit<float>());
            AreEqual(typeof(short), () => With16Bit<byte>());
            AreEqual(typeof(short), () => With16Bit<short>());
            AreEqual(typeof(short), () => With16Bit<float>());
            AreEqual(typeof(float), () => With32Bit<byte>());
            AreEqual(typeof(float), () => With32Bit<short>());
            AreEqual(typeof(float), () => With32Bit<float>());

            AreEqual(typeof(byte),  () => As8Bit<byte>());
            AreEqual(typeof(byte),  () => As8Bit<short>());
            AreEqual(typeof(byte),  () => As8Bit<float>());
            AreEqual(typeof(short), () => As16Bit<byte>());
            AreEqual(typeof(short), () => As16Bit<short>());
            AreEqual(typeof(short), () => As16Bit<float>());
            AreEqual(typeof(float), () => As32Bit<byte>());
            AreEqual(typeof(float), () => As32Bit<short>());
            AreEqual(typeof(float), () => As32Bit<float>());

            AreEqual(typeof(byte),  () => Set8Bit<byte>());
            AreEqual(typeof(byte),  () => Set8Bit<short>());
            AreEqual(typeof(byte),  () => Set8Bit<float>());
            AreEqual(typeof(short), () => Set16Bit<byte>());
            AreEqual(typeof(short), () => Set16Bit<short>());
            AreEqual(typeof(short), () => Set16Bit<float>());
            AreEqual(typeof(float), () => Set32Bit<byte>());
            AreEqual(typeof(float), () => Set32Bit<short>());
            AreEqual(typeof(float), () => Set32Bit<float>());
        }
        
        [TestMethod]
        public void Bits_EdgeCases()
        {
            var x = CreateTestEntities(bits: 32);
            ThrowsException(() => typeof(string).TypeToBits());
            ThrowsException(() => x.TapeBound.TapeConfig.Bits = 0);
            ThrowsException(() => x.TapeBound.TapeConfig.Bits = 3);
        }
        
        // Getter Helpers

        private void Assert_All_Getters(ConfigTestEntities x, int bits)
        {
            Assert_Bound_Getters(x, bits);
            Assert_Independent_Getters(x, bits);
            Assert_Immutable_Getters(x, bits);
        }

        private void Assert_Bound_Getters(ConfigTestEntities x, int bits)
        {
            Assert_SynthBound_Getters(x, bits);
            Assert_TapeBound_Getters(x, bits);
            Assert_BuffBound_Getters(x, bits);
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, int bits)
        {
            // Independent after Taping
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            Assert_Independent_Getters(x.Independent.Sample, bits);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, bits);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, bits);
        }

        private void Assert_Immutable_Getters(ConfigTestEntities x, int bits)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, bits);
            Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, bits);
            Assert_Immutable_Getters(x.Immutable.SampleDataType, bits);
            Assert_Immutable_Getters(x.Immutable.Type, bits);
        }

        private void Assert_SynthBound_Getters(ConfigTestEntities x, int bits)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigResolver);
            AreEqual(bits,       () => x.SynthBound.SynthWishes   .GetBits);
            AreEqual(bits,       () => x.SynthBound.FlowNode      .GetBits);
            AreEqual(bits,       () => x.SynthBound.ConfigResolver.GetBits);
            AreEqual(bits ==  8, () => x.SynthBound.SynthWishes   .Is8Bit );
            AreEqual(bits ==  8, () => x.SynthBound.FlowNode      .Is8Bit );
            AreEqual(bits ==  8, () => x.SynthBound.ConfigResolver.Is8Bit );
            AreEqual(bits == 16, () => x.SynthBound.SynthWishes   .Is16Bit);
            AreEqual(bits == 16, () => x.SynthBound.FlowNode      .Is16Bit);
            AreEqual(bits == 16, () => x.SynthBound.ConfigResolver.Is16Bit);
            AreEqual(bits == 32, () => x.SynthBound.SynthWishes   .Is32Bit);
            AreEqual(bits == 32, () => x.SynthBound.FlowNode      .Is32Bit);
            AreEqual(bits == 32, () => x.SynthBound.ConfigResolver.Is32Bit);
            AreEqual(bits,       () => x.SynthBound.SynthWishes   .Bits   ());
            AreEqual(bits,       () => x.SynthBound.FlowNode      .Bits   ());
            AreEqual(bits,       () => x.SynthBound.ConfigResolver.Bits   ());
            AreEqual(bits,       () => x.SynthBound.SynthWishes   .GetBits());
            AreEqual(bits,       () => x.SynthBound.FlowNode      .GetBits());
            AreEqual(bits,       () => x.SynthBound.ConfigResolver.GetBits());
            AreEqual(bits ==  8, () => x.SynthBound.SynthWishes   .Is8Bit ());
            AreEqual(bits ==  8, () => x.SynthBound.FlowNode      .Is8Bit ());
            AreEqual(bits ==  8, () => x.SynthBound.ConfigResolver.Is8Bit ());
            AreEqual(bits == 16, () => x.SynthBound.SynthWishes   .Is16Bit());
            AreEqual(bits == 16, () => x.SynthBound.FlowNode      .Is16Bit());
            AreEqual(bits == 16, () => x.SynthBound.ConfigResolver.Is16Bit());
            AreEqual(bits == 32, () => x.SynthBound.SynthWishes   .Is32Bit());
            AreEqual(bits == 32, () => x.SynthBound.FlowNode      .Is32Bit());
            AreEqual(bits == 32, () => x.SynthBound.ConfigResolver.Is32Bit());
            AreEqual(bits,       () => Bits   (x.SynthBound.SynthWishes   ));
            AreEqual(bits,       () => Bits   (x.SynthBound.FlowNode      ));
            AreEqual(bits,       () => Bits   (x.SynthBound.ConfigResolver));
            AreEqual(bits,       () => GetBits(x.SynthBound.SynthWishes   ));
            AreEqual(bits,       () => GetBits(x.SynthBound.FlowNode      ));
            AreEqual(bits,       () => GetBits(x.SynthBound.ConfigResolver));
            AreEqual(bits ==  8, () => Is8Bit (x.SynthBound.SynthWishes   ));
            AreEqual(bits ==  8, () => Is8Bit (x.SynthBound.FlowNode      ));
            AreEqual(bits ==  8, () => Is8Bit (x.SynthBound.ConfigResolver));
            AreEqual(bits == 16, () => Is16Bit(x.SynthBound.SynthWishes   ));
            AreEqual(bits == 16, () => Is16Bit(x.SynthBound.FlowNode      ));
            AreEqual(bits == 16, () => Is16Bit(x.SynthBound.ConfigResolver));
            AreEqual(bits == 32, () => Is32Bit(x.SynthBound.SynthWishes   ));
            AreEqual(bits == 32, () => Is32Bit(x.SynthBound.FlowNode      ));
            AreEqual(bits == 32, () => Is32Bit(x.SynthBound.ConfigResolver));
            AreEqual(bits,       () => ConfigWishes        .Bits   (x.SynthBound.SynthWishes   ));
            AreEqual(bits,       () => ConfigWishes        .Bits   (x.SynthBound.FlowNode      ));
            AreEqual(bits,       () => ConfigWishesAccessor.Bits   (x.SynthBound.ConfigResolver));
            AreEqual(bits,       () => ConfigWishes        .GetBits(x.SynthBound.SynthWishes   ));
            AreEqual(bits,       () => ConfigWishes        .GetBits(x.SynthBound.FlowNode      ));
            AreEqual(bits,       () => ConfigWishesAccessor.GetBits(x.SynthBound.ConfigResolver));
            AreEqual(bits ==  8, () => ConfigWishes        .Is8Bit (x.SynthBound.SynthWishes   ));
            AreEqual(bits ==  8, () => ConfigWishes        .Is8Bit (x.SynthBound.FlowNode      ));
            AreEqual(bits ==  8, () => ConfigWishesAccessor.Is8Bit (x.SynthBound.ConfigResolver));
            AreEqual(bits == 16, () => ConfigWishes        .Is16Bit(x.SynthBound.SynthWishes   ));
            AreEqual(bits == 16, () => ConfigWishes        .Is16Bit(x.SynthBound.FlowNode      ));
            AreEqual(bits == 16, () => ConfigWishesAccessor.Is16Bit(x.SynthBound.ConfigResolver));
            AreEqual(bits == 32, () => ConfigWishes        .Is32Bit(x.SynthBound.SynthWishes   ));
            AreEqual(bits == 32, () => ConfigWishes        .Is32Bit(x.SynthBound.FlowNode      ));
            AreEqual(bits == 32, () => ConfigWishesAccessor.Is32Bit(x.SynthBound.ConfigResolver));
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, int bits)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);
            AreEqual(bits,       () => x.TapeBound.TapeConfig .Bits);
            AreEqual(bits,       () => x.TapeBound.Tape       .Bits   ());
            AreEqual(bits,       () => x.TapeBound.TapeConfig .Bits   ());
            AreEqual(bits,       () => x.TapeBound.TapeActions.Bits   ());
            AreEqual(bits,       () => x.TapeBound.TapeAction .Bits   ());
            AreEqual(bits,       () => x.TapeBound.Tape       .GetBits());
            AreEqual(bits,       () => x.TapeBound.TapeConfig .GetBits());
            AreEqual(bits,       () => x.TapeBound.TapeActions.GetBits());
            AreEqual(bits,       () => x.TapeBound.TapeAction .GetBits());
            AreEqual(bits ==  8, () => x.TapeBound.Tape       .Is8Bit ());
            AreEqual(bits ==  8, () => x.TapeBound.TapeConfig .Is8Bit ());
            AreEqual(bits ==  8, () => x.TapeBound.TapeActions.Is8Bit ());
            AreEqual(bits ==  8, () => x.TapeBound.TapeAction .Is8Bit ());
            AreEqual(bits == 16, () => x.TapeBound.Tape       .Is16Bit());
            AreEqual(bits == 16, () => x.TapeBound.TapeConfig .Is16Bit());
            AreEqual(bits == 16, () => x.TapeBound.TapeActions.Is16Bit());
            AreEqual(bits == 16, () => x.TapeBound.TapeAction .Is16Bit());
            AreEqual(bits == 32, () => x.TapeBound.Tape       .Is32Bit());
            AreEqual(bits == 32, () => x.TapeBound.TapeConfig .Is32Bit());
            AreEqual(bits == 32, () => x.TapeBound.TapeActions.Is32Bit());
            AreEqual(bits == 32, () => x.TapeBound.TapeAction .Is32Bit());
            AreEqual(bits,       () => Bits   (x.TapeBound.Tape       ));
            AreEqual(bits,       () => Bits   (x.TapeBound.TapeConfig ));
            AreEqual(bits,       () => Bits   (x.TapeBound.TapeActions));
            AreEqual(bits,       () => Bits   (x.TapeBound.TapeAction ));
            AreEqual(bits,       () => GetBits(x.TapeBound.Tape       ));
            AreEqual(bits,       () => GetBits(x.TapeBound.TapeConfig ));
            AreEqual(bits,       () => GetBits(x.TapeBound.TapeActions));
            AreEqual(bits,       () => GetBits(x.TapeBound.TapeAction ));
            AreEqual(bits ==  8, () => Is8Bit (x.TapeBound.Tape       ));
            AreEqual(bits ==  8, () => Is8Bit (x.TapeBound.TapeConfig ));
            AreEqual(bits ==  8, () => Is8Bit (x.TapeBound.TapeActions));
            AreEqual(bits ==  8, () => Is8Bit (x.TapeBound.TapeAction ));
            AreEqual(bits == 16, () => Is16Bit(x.TapeBound.Tape       ));
            AreEqual(bits == 16, () => Is16Bit(x.TapeBound.TapeConfig ));
            AreEqual(bits == 16, () => Is16Bit(x.TapeBound.TapeActions));
            AreEqual(bits == 16, () => Is16Bit(x.TapeBound.TapeAction ));
            AreEqual(bits == 32, () => Is32Bit(x.TapeBound.Tape       ));
            AreEqual(bits == 32, () => Is32Bit(x.TapeBound.TapeConfig ));
            AreEqual(bits == 32, () => Is32Bit(x.TapeBound.TapeActions));
            AreEqual(bits == 32, () => Is32Bit(x.TapeBound.TapeAction ));
            AreEqual(bits,       () => ConfigWishes.Bits   (x.TapeBound.Tape       ));
            AreEqual(bits,       () => ConfigWishes.Bits   (x.TapeBound.TapeConfig ));
            AreEqual(bits,       () => ConfigWishes.Bits   (x.TapeBound.TapeActions));
            AreEqual(bits,       () => ConfigWishes.Bits   (x.TapeBound.TapeAction ));
            AreEqual(bits,       () => ConfigWishes.GetBits(x.TapeBound.Tape       ));
            AreEqual(bits,       () => ConfigWishes.GetBits(x.TapeBound.TapeConfig ));
            AreEqual(bits,       () => ConfigWishes.GetBits(x.TapeBound.TapeActions));
            AreEqual(bits,       () => ConfigWishes.GetBits(x.TapeBound.TapeAction ));
            AreEqual(bits ==  8, () => ConfigWishes.Is8Bit (x.TapeBound.Tape       ));
            AreEqual(bits ==  8, () => ConfigWishes.Is8Bit (x.TapeBound.TapeConfig ));
            AreEqual(bits ==  8, () => ConfigWishes.Is8Bit (x.TapeBound.TapeActions));
            AreEqual(bits ==  8, () => ConfigWishes.Is8Bit (x.TapeBound.TapeAction ));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(x.TapeBound.Tape       ));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(x.TapeBound.TapeConfig ));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(x.TapeBound.TapeActions));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(x.TapeBound.TapeAction ));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(x.TapeBound.Tape       ));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(x.TapeBound.TapeConfig ));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(x.TapeBound.TapeActions));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(x.TapeBound.TapeAction ));
        }
        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, int bits)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);
            AreEqual(bits,       () => x.BuffBound.Buff           .Bits   ());
            AreEqual(bits,       () => x.BuffBound.AudioFileOutput.Bits   ());
            AreEqual(bits,       () => x.BuffBound.Buff           .GetBits());
            AreEqual(bits,       () => x.BuffBound.AudioFileOutput.GetBits());
            AreEqual(bits ==  8, () => x.BuffBound.Buff           .Is8Bit ());
            AreEqual(bits ==  8, () => x.BuffBound.AudioFileOutput.Is8Bit ());
            AreEqual(bits == 16, () => x.BuffBound.Buff           .Is16Bit());
            AreEqual(bits == 16, () => x.BuffBound.AudioFileOutput.Is16Bit());
            AreEqual(bits == 32, () => x.BuffBound.Buff           .Is32Bit());
            AreEqual(bits == 32, () => x.BuffBound.AudioFileOutput.Is32Bit());
            AreEqual(bits,       () => Bits   (x.BuffBound.Buff));
            AreEqual(bits,       () => Bits   (x.BuffBound.AudioFileOutput));
            AreEqual(bits,       () => GetBits(x.BuffBound.Buff));
            AreEqual(bits,       () => GetBits(x.BuffBound.AudioFileOutput));
            AreEqual(bits ==  8, () => Is8Bit (x.BuffBound.Buff));
            AreEqual(bits ==  8, () => Is8Bit (x.BuffBound.AudioFileOutput));
            AreEqual(bits == 16, () => Is16Bit(x.BuffBound.Buff));
            AreEqual(bits == 16, () => Is16Bit(x.BuffBound.AudioFileOutput));
            AreEqual(bits == 32, () => Is32Bit(x.BuffBound.Buff));
            AreEqual(bits == 32, () => Is32Bit(x.BuffBound.AudioFileOutput));
            AreEqual(bits,       () => ConfigWishes.Bits   (x.BuffBound.Buff));
            AreEqual(bits,       () => ConfigWishes.Bits   (x.BuffBound.AudioFileOutput));
            AreEqual(bits,       () => ConfigWishes.GetBits(x.BuffBound.Buff));
            AreEqual(bits,       () => ConfigWishes.GetBits(x.BuffBound.AudioFileOutput));
            AreEqual(bits ==  8, () => ConfigWishes.Is8Bit (x.BuffBound.Buff));
            AreEqual(bits ==  8, () => ConfigWishes.Is8Bit (x.BuffBound.AudioFileOutput));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(x.BuffBound.Buff));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(x.BuffBound.AudioFileOutput));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(x.BuffBound.Buff));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(x.BuffBound.AudioFileOutput));
        }
        
        private void Assert_Independent_Getters(Sample sample, int bits)
        {
            IsNotNull(() => sample);
            AreEqual(bits,       () => sample.Bits   ());
            AreEqual(bits,       () => sample.GetBits());
            AreEqual(bits == 8,  () => sample.Is8Bit ());
            AreEqual(bits == 16, () => sample.Is16Bit());
            AreEqual(bits == 32, () => sample.Is32Bit());
            AreEqual(bits,       () => Bits   (sample));
            AreEqual(bits,       () => GetBits(sample));
            AreEqual(bits == 8,  () => Is8Bit (sample));
            AreEqual(bits == 16, () => Is16Bit(sample));
            AreEqual(bits == 32, () => Is32Bit(sample));
            AreEqual(bits,       () => ConfigWishes.Bits   (sample));
            AreEqual(bits,       () => ConfigWishes.GetBits(sample));
            AreEqual(bits == 8,  () => ConfigWishes.Is8Bit (sample));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(sample));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(sample));
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int bits)
        {
            IsNotNull(() => audioInfoWish);
            AreEqual(bits,       () => audioInfoWish.Bits     );
            AreEqual(bits,       () => audioInfoWish.Bits   ());
            AreEqual(bits,       () => audioInfoWish.GetBits());
            AreEqual(bits ==  8, () => audioInfoWish.Is8Bit ());
            AreEqual(bits == 16, () => audioInfoWish.Is16Bit());
            AreEqual(bits == 32, () => audioInfoWish.Is32Bit());
            AreEqual(bits,       () => Bits   (audioInfoWish));
            AreEqual(bits,       () => GetBits(audioInfoWish));
            AreEqual(bits ==  8, () => Is8Bit (audioInfoWish));
            AreEqual(bits == 16, () => Is16Bit(audioInfoWish));
            AreEqual(bits == 32, () => Is32Bit(audioInfoWish));
            AreEqual(bits,       () => ConfigWishes.Bits   (audioInfoWish));
            AreEqual(bits,       () => ConfigWishes.GetBits(audioInfoWish));
            AreEqual(bits ==  8, () => ConfigWishes.Is8Bit (audioInfoWish));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(audioInfoWish));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(audioInfoWish));
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int bits)
        {
            IsNotNull(() => audioFileInfo);
            AreEqual(bits,       () => audioFileInfo.Bits   ());
            AreEqual(bits,       () => audioFileInfo.GetBits());
            AreEqual(bits == 8,  () => audioFileInfo.Is8Bit ());
            AreEqual(bits == 16, () => audioFileInfo.Is16Bit());
            AreEqual(bits == 32, () => audioFileInfo.Is32Bit());
            AreEqual(bits,       () => Bits   (audioFileInfo));
            AreEqual(bits,       () => GetBits(audioFileInfo));
            AreEqual(bits == 8,  () => Is8Bit (audioFileInfo));
            AreEqual(bits == 16, () => Is16Bit(audioFileInfo));
            AreEqual(bits == 32, () => Is32Bit(audioFileInfo));
            AreEqual(bits,       () => ConfigWishes.Bits   (audioFileInfo));
            AreEqual(bits,       () => ConfigWishes.GetBits(audioFileInfo));
            AreEqual(bits == 8,  () => ConfigWishes.Is8Bit (audioFileInfo));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(audioFileInfo));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(audioFileInfo));
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int bits)
        {
            AreEqual(bits,       () => wavHeader.BitsPerValue);
            AreEqual(bits,       () => wavHeader.Bits   ());
            AreEqual(bits,       () => wavHeader.GetBits());
            AreEqual(bits == 8,  () => wavHeader.Is8Bit ());
            AreEqual(bits == 16, () => wavHeader.Is16Bit());
            AreEqual(bits == 32, () => wavHeader.Is32Bit());
            AreEqual(bits,       () => Bits   (wavHeader));
            AreEqual(bits,       () => GetBits(wavHeader));
            AreEqual(bits == 8,  () => Is8Bit (wavHeader));
            AreEqual(bits == 16, () => Is16Bit(wavHeader));
            AreEqual(bits == 32, () => Is32Bit(wavHeader));
            AreEqual(bits,       () => ConfigWishes.Bits   (wavHeader));
            AreEqual(bits,       () => ConfigWishes.GetBits(wavHeader));
            AreEqual(bits == 8,  () => ConfigWishes.Is8Bit (wavHeader));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit(wavHeader));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit(wavHeader));
        }
        
        private void Assert_Immutable_Getters(SampleDataTypeEnum sampleDataTypeEnum, int bits)
        {
            AreEqual(bits,       () => sampleDataTypeEnum.Bits      ());
            AreEqual(bits,       () => sampleDataTypeEnum.GetBits   ());
            AreEqual(bits,       () => sampleDataTypeEnum.AsBits    ());
            AreEqual(bits,       () => sampleDataTypeEnum.ToBits    ());
            AreEqual(bits,       () => sampleDataTypeEnum.EnumToBits());
            AreEqual(bits == 8,  () => sampleDataTypeEnum.Is8Bit    ());
            AreEqual(bits == 16, () => sampleDataTypeEnum.Is16Bit   ());
            AreEqual(bits == 32, () => sampleDataTypeEnum.Is32Bit   ());
            AreEqual(bits,       () => Bits      (sampleDataTypeEnum));
            AreEqual(bits,       () => GetBits   (sampleDataTypeEnum));
            AreEqual(bits,       () => AsBits    (sampleDataTypeEnum));
            AreEqual(bits,       () => ToBits    (sampleDataTypeEnum));
            AreEqual(bits,       () => EnumToBits(sampleDataTypeEnum));
            AreEqual(bits == 8,  () => Is8Bit    (sampleDataTypeEnum));
            AreEqual(bits == 16, () => Is16Bit   (sampleDataTypeEnum));
            AreEqual(bits == 32, () => Is32Bit   (sampleDataTypeEnum));
            AreEqual(bits,       () => ConfigWishes.Bits      (sampleDataTypeEnum));
            AreEqual(bits,       () => ConfigWishes.GetBits   (sampleDataTypeEnum));
            AreEqual(bits,       () => ConfigWishes.AsBits    (sampleDataTypeEnum));
            AreEqual(bits,       () => ConfigWishes.ToBits    (sampleDataTypeEnum));
            AreEqual(bits,       () => ConfigWishes.EnumToBits(sampleDataTypeEnum));
            AreEqual(bits == 8,  () => ConfigWishes.Is8Bit    (sampleDataTypeEnum));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit   (sampleDataTypeEnum));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit   (sampleDataTypeEnum));
        }
        
        private void Assert_Immutable_Getters(SampleDataType sampleDataType, int bits)
        {
            IsNotNull(() => sampleDataType);
            AreEqual(bits,       () => sampleDataType.Bits        ());
            AreEqual(bits,       () => sampleDataType.GetBits     ());
            AreEqual(bits,       () => sampleDataType.AsBits      ());
            AreEqual(bits,       () => sampleDataType.ToBits      ());
            AreEqual(bits,       () => sampleDataType.EntityToBits());
            AreEqual(bits == 8,  () => sampleDataType.Is8Bit      ());
            AreEqual(bits == 16, () => sampleDataType.Is16Bit     ());
            AreEqual(bits == 32, () => sampleDataType.Is32Bit     ());
            AreEqual(bits,       () => Bits        (sampleDataType));
            AreEqual(bits,       () => GetBits     (sampleDataType));
            AreEqual(bits,       () => AsBits      (sampleDataType));
            AreEqual(bits,       () => ToBits      (sampleDataType));
            AreEqual(bits,       () => EntityToBits(sampleDataType));
            AreEqual(bits == 8,  () => Is8Bit      (sampleDataType));
            AreEqual(bits == 16, () => Is16Bit     (sampleDataType));
            AreEqual(bits == 32, () => Is32Bit     (sampleDataType));
            AreEqual(bits,       () => ConfigWishes.Bits        (sampleDataType));
            AreEqual(bits,       () => ConfigWishes.GetBits     (sampleDataType));
            AreEqual(bits,       () => ConfigWishes.AsBits      (sampleDataType));
            AreEqual(bits,       () => ConfigWishes.ToBits      (sampleDataType));
            AreEqual(bits,       () => ConfigWishes.EntityToBits(sampleDataType));
            AreEqual(bits == 8,  () => ConfigWishes.Is8Bit      (sampleDataType));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit     (sampleDataType));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit     (sampleDataType));
        }
        
        private void Assert_Immutable_Getters(Type type, int bits)
        {
            IsNotNull(() => type);
            AreEqual(bits,       () => type.Bits      ());
            AreEqual(bits,       () => type.GetBits   ());
            AreEqual(bits,       () => type.ToBits    ());
            AreEqual(bits,       () => type.TypeToBits());
            AreEqual(bits ==  8, () => type.Is8Bit    ());
            AreEqual(bits == 16, () => type.Is16Bit   ());
            AreEqual(bits == 32, () => type.Is32Bit   ());
            AreEqual(bits,       () => Bits      (type));
            AreEqual(bits,       () => GetBits   (type));
            AreEqual(bits,       () => ToBits    (type));
            AreEqual(bits,       () => TypeToBits(type));
            AreEqual(bits ==  8, () => Is8Bit    (type));
            AreEqual(bits == 16, () => Is16Bit   (type));
            AreEqual(bits == 32, () => Is32Bit   (type));
            AreEqual(bits,       () => ConfigWishes.Bits      (type));
            AreEqual(bits,       () => ConfigWishes.GetBits   (type));
            AreEqual(bits,       () => ConfigWishes.ToBits    (type));
            AreEqual(bits,       () => ConfigWishes.TypeToBits(type));
            AreEqual(bits ==  8, () => ConfigWishes.Is8Bit    (type));
            AreEqual(bits == 16, () => ConfigWishes.Is16Bit   (type));
            AreEqual(bits == 32, () => ConfigWishes.Is32Bit   (type));
        }
 
        // Test Data Helpers

        private ConfigTestEntities CreateTestEntities(int? bits) => new ConfigTestEntities(x => x.Bits(bits));
                
        static object TestParametersInit => new[] // ncrunch: no coverage
        { 
            new object[] { null },
            new object[] { 0 },
            new object[] { 8 },
            new object[] { 16 },
            new object[] { 32 },
        };

        static object TestParameters => new[] // ncrunch: no coverage
        {
            new object[] { 32, 8 },
            new object[] { 32, 16 },
            new object[] { 16, 32 },
        };

        static object TestParametersWithEmpty => new[] // ncrunch: no coverage
        {
            new object[] { 32, 8 },
            new object[] { 32, 16 },
            new object[] { 16, 32 },
            new object[] { 16, null },
            new object[] { 16, 0 },
            new object[] { null, 16 },
            new object[] { 0, 16 },
        };
    } 
}