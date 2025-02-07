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
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceBits(init));
                
                setter(x);
                
                Assert_SynthBound_Getters (x, CoalesceBits(value));
                Assert_TapeBound_Getters  (x, CoalesceBits(init ));
                Assert_BuffBound_Getters  (x, CoalesceBits(init ));
                Assert_Independent_Getters(x, CoalesceBits(init ));
                Assert_Immutable_Getters  (x, CoalesceBits(init ));
                
                x.Record();
                Assert_All_Getters(x, CoalesceBits(value));
            }

            AssertProp(x => AreEqual(x.SynthBound.Derived,        x.SynthBound.Derived       .Bits_Call    (value)));
            AssertProp(x => AreEqual(x.SynthBound.Derived,        x.SynthBound.Derived       .WithBits_Call(value)));
            AssertProp(x => AreEqual(x.SynthBound.Derived,        x.SynthBound.Derived       .AsBits_Call  (value)));
            AssertProp(x => AreEqual(x.SynthBound.Derived,        x.SynthBound.Derived       .SetBits_Call (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Bits         (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Bits         (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Bits         (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithBits     (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithBits     (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithBits     (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AsBits       (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AsBits       (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AsBits       (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetBits      (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetBits      (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetBits      (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    Bits    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       Bits    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, Bits    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithBits(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithBits(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithBits(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    AsBits  (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       AsBits  (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, AsBits  (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetBits (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetBits (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetBits (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .Bits    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .Bits    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.Bits    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithBits(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithBits(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithBits(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .AsBits  (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .AsBits  (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.AsBits  (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetBits (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetBits (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetBits (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    BitExtensionWishes        .WithBits(x.SynthBound.SynthWishes   , value))); 
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       BitExtensionWishes        .WithBits(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, BitExtensionWishesAccessor.WithBits(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.Derived,        () => x.SynthBound.Derived       .With8Bit_Call  (     ));
                              if (value == 16) AreEqual(x.SynthBound.Derived,        () => x.SynthBound.Derived       .With16Bit_Call (     ));
                              if (value == 32) AreEqual(x.SynthBound.Derived,        () => x.SynthBound.Derived       .With32Bit_Call (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.Derived,              x.SynthBound.Derived       .WithBits_Call  (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.Derived,        () => x.SynthBound.Derived       .As8Bit_Call    (     ));
                              if (value == 16) AreEqual(x.SynthBound.Derived,        () => x.SynthBound.Derived       .As16Bit_Call   (     ));
                              if (value == 32) AreEqual(x.SynthBound.Derived,        () => x.SynthBound.Derived       .As32Bit_Call   (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.Derived,              x.SynthBound.Derived       .AsBits_Call    (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.Derived,        () => x.SynthBound.Derived       .Set8Bit_Call   (     ));
                              if (value == 16) AreEqual(x.SynthBound.Derived,        () => x.SynthBound.Derived       .Set16Bit_Call  (     ));
                              if (value == 32) AreEqual(x.SynthBound.Derived,        () => x.SynthBound.Derived       .Set32Bit_Call  (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.Derived,              x.SynthBound.Derived       .SetBits_Call   (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .With8Bit       (     ));
                              if (value == 16) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .With16Bit      (     ));
                              if (value == 32) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .With32Bit      (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .WithBits       (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .With8Bit       (     ));
                              if (value == 16) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .With16Bit      (     ));
                              if (value == 32) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .With32Bit      (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .WithBits       (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With8Bit       (     ));
                              if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With16Bit      (     ));
                              if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With32Bit      (     ));
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.WithBits       (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .As8Bit         (     ));
                              if (value == 16) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .As16Bit        (     ));
                              if (value == 32) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .As32Bit        (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .AsBits         (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .As8Bit         (     ));
                              if (value == 16) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .As16Bit        (     ));
                              if (value == 32) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .As32Bit        (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .AsBits         (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.As8Bit         (     ));
                              if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.As16Bit        (     ));
                              if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.As32Bit        (     ));
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AsBits         (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .Set8Bit        (     ));
                              if (value == 16) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .Set16Bit       (     ));
                              if (value == 32) AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .Set32Bit       (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .SetBits        (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .Set8Bit        (     ));
                              if (value == 16) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .Set16Bit       (     ));
                              if (value == 32) AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .Set32Bit       (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .SetBits        (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Set8Bit        (     ));
                              if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Set16Bit       (     ));
                              if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Set32Bit       (     ));
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.SetBits        (value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.SynthWishes,    () => With8Bit (x.SynthBound.SynthWishes          ));
                              if (value == 16) AreEqual(x.SynthBound.SynthWishes,    () => With16Bit(x.SynthBound.SynthWishes          ));
                              if (value == 32) AreEqual(x.SynthBound.SynthWishes,    () => With32Bit(x.SynthBound.SynthWishes          )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,          WithBits (x.SynthBound.SynthWishes,    value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.FlowNode,       () => With8Bit (x.SynthBound.FlowNode             ));
                              if (value == 16) AreEqual(x.SynthBound.FlowNode,       () => With16Bit(x.SynthBound.FlowNode             ));
                              if (value == 32) AreEqual(x.SynthBound.FlowNode,       () => With32Bit(x.SynthBound.FlowNode             )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,             WithBits (x.SynthBound.FlowNode,       value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => With8Bit (x.SynthBound.ConfigResolver       ));
                              if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => With16Bit(x.SynthBound.ConfigResolver       ));
                              if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => With32Bit(x.SynthBound.ConfigResolver       ));
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       WithBits (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.SynthWishes,    () => As8Bit   (x.SynthBound.SynthWishes          ));
                              if (value == 16) AreEqual(x.SynthBound.SynthWishes,    () => As16Bit  (x.SynthBound.SynthWishes          ));
                              if (value == 32) AreEqual(x.SynthBound.SynthWishes,    () => As32Bit  (x.SynthBound.SynthWishes          )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,          AsBits   (x.SynthBound.SynthWishes,    value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.FlowNode,       () => As8Bit   (x.SynthBound.FlowNode             ));
                              if (value == 16) AreEqual(x.SynthBound.FlowNode,       () => As16Bit  (x.SynthBound.FlowNode             ));
                              if (value == 32) AreEqual(x.SynthBound.FlowNode,       () => As32Bit  (x.SynthBound.FlowNode             )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,             AsBits   (x.SynthBound.FlowNode,       value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => As8Bit   (x.SynthBound.ConfigResolver       ));
                              if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => As16Bit  (x.SynthBound.ConfigResolver       ));
                              if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => As32Bit  (x.SynthBound.ConfigResolver       ));
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       AsBits   (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.SynthWishes,    () => Set8Bit  (x.SynthBound.SynthWishes          ));
                              if (value == 16) AreEqual(x.SynthBound.SynthWishes,    () => Set16Bit (x.SynthBound.SynthWishes          ));
                              if (value == 32) AreEqual(x.SynthBound.SynthWishes,    () => Set32Bit (x.SynthBound.SynthWishes          )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,          SetBits  (x.SynthBound.SynthWishes,    value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.FlowNode,       () => Set8Bit  (x.SynthBound.FlowNode             ));
                              if (value == 16) AreEqual(x.SynthBound.FlowNode,       () => Set16Bit (x.SynthBound.FlowNode             ));
                              if (value == 32) AreEqual(x.SynthBound.FlowNode,       () => Set32Bit (x.SynthBound.FlowNode             )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,             SetBits  (x.SynthBound.FlowNode,       value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => Set8Bit  (x.SynthBound.ConfigResolver       ));
                              if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => Set16Bit (x.SynthBound.ConfigResolver       ));
                              if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => Set32Bit (x.SynthBound.ConfigResolver       ));
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       SetBits  (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .With8Bit (x.SynthBound.SynthWishes          ));
                              if (value == 16) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .With16Bit(x.SynthBound.SynthWishes          ));
                              if (value == 32) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .With32Bit(x.SynthBound.SynthWishes          )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,          ConfigWishes        .WithBits (x.SynthBound.SynthWishes,    value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .With8Bit (x.SynthBound.FlowNode             ));
                              if (value == 16) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .With16Bit(x.SynthBound.FlowNode             ));
                              if (value == 32) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .With32Bit(x.SynthBound.FlowNode             )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,             ConfigWishes        .WithBits (x.SynthBound.FlowNode,       value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.With8Bit (x.SynthBound.ConfigResolver       ));
                              if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.With16Bit(x.SynthBound.ConfigResolver       ));
                              if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.With32Bit(x.SynthBound.ConfigResolver       ));
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.WithBits (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .As8Bit   (x.SynthBound.SynthWishes          ));
                              if (value == 16) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .As16Bit  (x.SynthBound.SynthWishes          ));
                              if (value == 32) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .As32Bit  (x.SynthBound.SynthWishes          )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,          ConfigWishes        .AsBits   (x.SynthBound.SynthWishes,    value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .As8Bit   (x.SynthBound.FlowNode             ));
                              if (value == 16) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .As16Bit  (x.SynthBound.FlowNode             ));
                              if (value == 32) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .As32Bit  (x.SynthBound.FlowNode             )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,             ConfigWishes        .AsBits   (x.SynthBound.FlowNode,       value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.As8Bit   (x.SynthBound.ConfigResolver       ));
                              if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.As16Bit  (x.SynthBound.ConfigResolver       ));
                              if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.As32Bit  (x.SynthBound.ConfigResolver       ));
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.AsBits   (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .Set8Bit  (x.SynthBound.SynthWishes          ));
                              if (value == 16) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .Set16Bit (x.SynthBound.SynthWishes          ));
                              if (value == 32) AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .Set32Bit (x.SynthBound.SynthWishes          )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,          ConfigWishes        .SetBits  (x.SynthBound.SynthWishes,    value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .Set8Bit  (x.SynthBound.FlowNode             ));
                              if (value == 16) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .Set16Bit (x.SynthBound.FlowNode             ));
                              if (value == 32) AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .Set32Bit (x.SynthBound.FlowNode             )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,             ConfigWishes        .SetBits  (x.SynthBound.FlowNode,       value)); });
            AssertProp(x => { if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.Set8Bit  (x.SynthBound.ConfigResolver       ));
                              if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.Set16Bit (x.SynthBound.ConfigResolver       ));
                              if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.Set32Bit (x.SynthBound.ConfigResolver       ));
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.SetBits  (x.SynthBound.ConfigResolver, value)); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_Bits(int init, int value)
        {
            void AssertProp(Action<ConfigTestEntities> setter)
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

            AssertProp(x =>                                         x.TapeBound.TapeConfig .Bits    = value);
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Bits     (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Bits     (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Bits     (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Bits     (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithBits (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithBits (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithBits (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithBits (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .AsBits   (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .AsBits   (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsBits   (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .AsBits   (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetBits  (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetBits  (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetBits  (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetBits  (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => Bits     (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => Bits     (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => Bits     (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => Bits     (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithBits (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithBits (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithBits (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithBits (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => AsBits   (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => AsBits   (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => AsBits   (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => AsBits   (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetBits  (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetBits  (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetBits  (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetBits  (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.Bits     (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.Bits     (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Bits     (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.Bits     (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithBits (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithBits (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithBits (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithBits (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.AsBits   (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.AsBits   (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsBits   (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.AsBits   (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetBits  (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetBits  (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetBits  (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetBits  (x.TapeBound.TapeAction , value)));
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .With8Bit ());
                              if (value == 16) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .With16Bit());
                              if (value == 32) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .With32Bit()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .With8Bit ());
                              if (value == 16) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .With16Bit());
                              if (value == 32) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .With32Bit()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With8Bit ());
                              if (value == 16) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With16Bit());
                              if (value == 32) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With32Bit()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .With8Bit ());
                              if (value == 16) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .With16Bit());
                              if (value == 32) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .With32Bit()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .As8Bit   ());
                              if (value == 16) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .As16Bit  ());
                              if (value == 32) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .As32Bit  ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .As8Bit   ());
                              if (value == 16) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .As16Bit  ());
                              if (value == 32) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .As32Bit  ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.As8Bit   ());
                              if (value == 16) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.As16Bit  ());
                              if (value == 32) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.As32Bit  ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .As8Bit   ());
                              if (value == 16) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .As16Bit  ());
                              if (value == 32) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .As32Bit  ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Set8Bit  ());
                              if (value == 16) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Set16Bit ());
                              if (value == 32) AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Set32Bit ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Set8Bit  ());
                              if (value == 16) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Set16Bit ());
                              if (value == 32) AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Set32Bit ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Set8Bit  ());
                              if (value == 16) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Set16Bit ());
                              if (value == 32) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Set32Bit ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Set8Bit  ());
                              if (value == 16) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Set16Bit ());
                              if (value == 32) AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Set32Bit ()); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.Tape,        () => With8Bit (x.TapeBound.Tape       ));
                              if (value == 16) AreEqual(x.TapeBound.Tape,        () => With16Bit(x.TapeBound.Tape       ));
                              if (value == 32) AreEqual(x.TapeBound.Tape,        () => With32Bit(x.TapeBound.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeConfig,  () => With8Bit (x.TapeBound.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeBound.TapeConfig,  () => With16Bit(x.TapeBound.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeBound.TapeConfig,  () => With32Bit(x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => With8Bit (x.TapeBound.TapeActions));
                              if (value == 16) AreEqual(x.TapeBound.TapeActions, () => With16Bit(x.TapeBound.TapeActions));
                              if (value == 32) AreEqual(x.TapeBound.TapeActions, () => With32Bit(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeAction,  () => With8Bit (x.TapeBound.TapeAction ));
                              if (value == 16) AreEqual(x.TapeBound.TapeAction,  () => With16Bit(x.TapeBound.TapeAction ));
                              if (value == 32) AreEqual(x.TapeBound.TapeAction,  () => With32Bit(x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.Tape,        () => As8Bit   (x.TapeBound.Tape       ));
                              if (value == 16) AreEqual(x.TapeBound.Tape,        () => As16Bit  (x.TapeBound.Tape       ));
                              if (value == 32) AreEqual(x.TapeBound.Tape,        () => As32Bit  (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeConfig,  () => As8Bit   (x.TapeBound.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeBound.TapeConfig,  () => As16Bit  (x.TapeBound.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeBound.TapeConfig,  () => As32Bit  (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => As8Bit   (x.TapeBound.TapeActions));
                              if (value == 16) AreEqual(x.TapeBound.TapeActions, () => As16Bit  (x.TapeBound.TapeActions));
                              if (value == 32) AreEqual(x.TapeBound.TapeActions, () => As32Bit  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeAction,  () => As8Bit   (x.TapeBound.TapeAction ));
                              if (value == 16) AreEqual(x.TapeBound.TapeAction,  () => As16Bit  (x.TapeBound.TapeAction ));
                              if (value == 32) AreEqual(x.TapeBound.TapeAction,  () => As32Bit  (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.Tape,        () => Set8Bit  (x.TapeBound.Tape       ));
                              if (value == 16) AreEqual(x.TapeBound.Tape,        () => Set16Bit (x.TapeBound.Tape       ));
                              if (value == 32) AreEqual(x.TapeBound.Tape,        () => Set32Bit (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeConfig,  () => Set8Bit  (x.TapeBound.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeBound.TapeConfig,  () => Set16Bit (x.TapeBound.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeBound.TapeConfig,  () => Set32Bit (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => Set8Bit  (x.TapeBound.TapeActions));
                              if (value == 16) AreEqual(x.TapeBound.TapeActions, () => Set16Bit (x.TapeBound.TapeActions));
                              if (value == 32) AreEqual(x.TapeBound.TapeActions, () => Set32Bit (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeAction,  () => Set8Bit  (x.TapeBound.TapeAction ));
                              if (value == 16) AreEqual(x.TapeBound.TapeAction,  () => Set16Bit (x.TapeBound.TapeAction ));
                              if (value == 32) AreEqual(x.TapeBound.TapeAction,  () => Set32Bit (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.With8Bit (x.TapeBound.Tape       ));
                              if (value == 16) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.With16Bit(x.TapeBound.Tape       ));
                              if (value == 32) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.With32Bit(x.TapeBound.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.With8Bit (x.TapeBound.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.With16Bit(x.TapeBound.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.With32Bit(x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.With8Bit (x.TapeBound.TapeActions));
                              if (value == 16) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.With16Bit(x.TapeBound.TapeActions));
                              if (value == 32) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.With32Bit(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.With8Bit (x.TapeBound.TapeAction ));
                              if (value == 16) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.With16Bit(x.TapeBound.TapeAction ));
                              if (value == 32) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.With32Bit(x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.As8Bit   (x.TapeBound.Tape       ));
                              if (value == 16) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.As16Bit  (x.TapeBound.Tape       ));
                              if (value == 32) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.As32Bit  (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.As8Bit   (x.TapeBound.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.As16Bit  (x.TapeBound.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.As32Bit  (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.As8Bit   (x.TapeBound.TapeActions));
                              if (value == 16) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.As16Bit  (x.TapeBound.TapeActions));
                              if (value == 32) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.As32Bit  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.As8Bit   (x.TapeBound.TapeAction ));
                              if (value == 16) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.As16Bit  (x.TapeBound.TapeAction ));
                              if (value == 32) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.As32Bit  (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.Set8Bit  (x.TapeBound.Tape       ));
                              if (value == 16) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.Set16Bit (x.TapeBound.Tape       ));
                              if (value == 32) AreEqual(x.TapeBound.Tape,        () => ConfigWishes.Set32Bit (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.Set8Bit  (x.TapeBound.TapeConfig ));
                              if (value == 16) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.Set16Bit (x.TapeBound.TapeConfig ));
                              if (value == 32) AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.Set32Bit (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Set8Bit  (x.TapeBound.TapeActions));
                              if (value == 16) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Set16Bit (x.TapeBound.TapeActions));
                              if (value == 32) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Set32Bit (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.Set8Bit  (x.TapeBound.TapeAction ));
                              if (value == 16) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.Set16Bit (x.TapeBound.TapeAction ));
                              if (value == 32) AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.Set32Bit (x.TapeBound.TapeAction )); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_Bits(int init, int value)
        {
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, init);
                Assert_BuffBound_Getters(x, value);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .Bits     (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Bits     (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithBits (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithBits (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .AsBits   (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsBits   (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetBits  (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetBits  (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => Bits    (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => Bits    (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => WithBits(x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => WithBits(x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => AsBits  (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => AsBits  (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => SetBits (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => SetBits (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.Bits    (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Bits    (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithBits(x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithBits(x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.AsBits  (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.AsBits  (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetBits (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetBits (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .With8Bit (x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .With16Bit(x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .With32Bit(x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With8Bit (x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With16Bit(x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With32Bit(x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .As8Bit   (x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .As16Bit  (x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .As32Bit  (x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.As8Bit   (x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.As16Bit  (x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.As32Bit  (x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .Set8Bit  (x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .Set16Bit (x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .Set32Bit (x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Set8Bit  (x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Set16Bit (x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Set32Bit (x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.Buff,            () => With8Bit (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.Buff,            () => With16Bit(x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.Buff,            () => With32Bit(x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => With8Bit (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => With16Bit(x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => With32Bit(x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.Buff,            () => As8Bit   (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.Buff,            () => As16Bit  (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.Buff,            () => As32Bit  (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => As8Bit   (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => As16Bit  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => As32Bit  (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.Buff,            () => Set8Bit  (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.Buff,            () => Set16Bit (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.Buff,            () => Set32Bit (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => Set8Bit  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => Set16Bit (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => Set32Bit (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.With8Bit (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.With16Bit(x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.With32Bit(x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.With8Bit (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.With16Bit(x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.With32Bit(x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.As8Bit   (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.As16Bit  (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.As32Bit  (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.As8Bit   (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.As16Bit  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.As32Bit  (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.Set8Bit  (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.Set16Bit (x.BuffBound.Buff           , x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.Buff,            () => ConfigWishes.Set32Bit (x.BuffBound.Buff           , x.SynthBound.Context)); });
            AssertProp(x => { if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Set8Bit  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Set16Bit (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Set32Bit (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
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
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Bits    (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithBits(value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsBits  (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetBits (value, x.SynthBound.Context)));
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
                
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Bits = value);
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