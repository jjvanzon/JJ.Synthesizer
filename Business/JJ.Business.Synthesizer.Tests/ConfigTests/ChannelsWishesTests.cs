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
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.docs;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Core.Common.FilledInWishes;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
using System.Runtime.CompilerServices;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0611 


namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class ChannelsWishesTests
    {
        [TestMethod, DataRow(null), DataRow(0), DataRow(1) ,DataRow(2)]
        public void Init_Channels(int? init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, CoalesceChannels(init));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpties))]
        public void SynthBound_Channels(int? init, int? value)
        {            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceChannels(init));
                
                setter(x);
                
                Assert_SynthBound_Getters (x, CoalesceChannels(value));
                Assert_TapeBound_Getters  (x, CoalesceChannels(init));
                Assert_BuffBound_Getters  (x, CoalesceChannels(init));
                Assert_Independent_Getters(x, CoalesceChannels(init));
                Assert_Immutable_Getters  (x, CoalesceChannels(init));
                
                x.Record();
                Assert_All_Getters(x, CoalesceChannels(value));
            }

            AssertProp(x =>                    AreEqual(x.SynthBound.Derived,       x.SynthBound.Derived.Channels_Call    (value)));
            AssertProp(x =>                    AreEqual(x.SynthBound.Derived,       x.SynthBound.Derived.SetChannels_Call (value)));
            AssertProp(x =>                    AreEqual(x.SynthBound.Derived,       x.SynthBound.Derived.WithChannels_Call(value)));
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.Mono_Call        ());
                              if (value == 2 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.Stereo_Call      ()); 
                              if (!Has(value)) AreEqual(x.SynthBound.Derived,       x.SynthBound.Derived.Channels_Call    (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.WithMono_Call    ());
                              if (value == 2 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.WithStereo_Call  ()); 
                              if (!Has(value)) AreEqual(x.SynthBound.Derived,       x.SynthBound.Derived.WithChannels_Call(value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.AsMono_Call      ());
                              if (value == 2 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.AsStereo_Call    ()); 
                              if (!Has(value)) AreEqual(x.SynthBound.Derived,       x.SynthBound.Derived.SetChannels_Call(value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.SetMono_Call     ());
                              if (value == 2 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.SetStereo_Call   ()); 
                              if (!Has(value)) AreEqual(x.SynthBound.Derived,       x.SynthBound.Derived.SetChannels_Call (value)); });
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Channels         (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Channels         (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Channels         (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithChannels     (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithChannels     (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithChannels     (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetChannels      (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetChannels      (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetChannels      (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    Channels    (x.SynthBound.SynthWishes,    value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       Channels    (x.SynthBound.FlowNode,       value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, Channels    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetChannels (x.SynthBound.SynthWishes,    value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetChannels (x.SynthBound.FlowNode,       value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetChannels (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithChannels(x.SynthBound.SynthWishes,    value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithChannels(x.SynthBound.FlowNode,       value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithChannels(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .Channels    (x.SynthBound.SynthWishes,    value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .Channels    (x.SynthBound.FlowNode,       value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.Channels    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetChannels (x.SynthBound.SynthWishes,    value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetChannels (x.SynthBound.FlowNode,       value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetChannels (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithChannels(x.SynthBound.SynthWishes,    value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithChannels(x.SynthBound.FlowNode,       value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithChannels(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ChannelsExtensionWishes        .WithChannels(x.SynthBound.FlowNode,       value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ChannelsExtensionWishesAccessor.WithChannels(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Mono        (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Stereo      (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.Channels    (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithMono    (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithStereo  (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.WithChannels(value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.AsMono      (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.AsStereo    (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.SetChannels (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.SetMono     (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.SetStereo   (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.SetChannels (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => Mono        (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => Stereo      (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       Channels    (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => WithMono    (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => WithStereo  (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       WithChannels(x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => AsMono      (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => AsStereo    (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       SetChannels (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => SetMono     (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => SetStereo   (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       SetChannels (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.Mono        (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.Stereo      (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       ConfigWishes.Channels    (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.WithMono    (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.WithStereo  (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       ConfigWishes.WithChannels(x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.AsMono      (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.AsStereo    (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       ConfigWishes.SetChannels (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.SetMono     (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.SetStereo   (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       ConfigWishes.SetChannels (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Mono        (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Stereo      (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.Channels    (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithMono    (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithStereo  (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.WithChannels(value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.SetMono     (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.SetStereo   (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.SetChannels (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.AsMono      (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.AsStereo    (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.SetChannels (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => Mono        (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => Stereo      (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       Channels    (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => WithMono    (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => WithStereo  (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       WithChannels(x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => AsMono      (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => AsStereo    (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       SetChannels (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => SetMono     (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => SetStereo   (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       SetChannels (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.Mono        (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.Stereo      (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       ConfigWishes.Channels    (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.WithMono    (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.WithStereo  (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       ConfigWishes.WithChannels(x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.AsMono      (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.AsStereo    (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       ConfigWishes.SetChannels (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.SetMono     (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.SetStereo   (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       ConfigWishes.SetChannels (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Mono        (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Stereo      (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.Channels    (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.SetMono     (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.SetStereo   (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.SetChannels (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsMono      (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsStereo    (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.SetChannels (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithMono    (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithStereo  (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.WithChannels(value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => Mono        (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => Stereo      (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       Channels    (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => WithMono    (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => WithStereo  (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       WithChannels(x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => AsMono      (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => AsStereo    (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       SetChannels (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => SetMono     (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => SetStereo   (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       SetChannels (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.Mono        (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.Stereo      (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.Channels    (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.WithMono    (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.WithStereo  (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.WithChannels(x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.SetMono     (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.SetStereo   (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.SetChannels (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.AsMono      (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.AsStereo    (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.SetChannels (x.SynthBound.ConfigResolver, value)); });
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_Channels(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, init);
                Assert_TapeBound_Getters  (x, value);
                Assert_BuffBound_Getters  (x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters  (x, init);
                
                x.Record();
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x =>                                         x.TapeBound.TapeConfig .Channels   = value);
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Channels    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Channels    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Channels    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Channels    (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithChannels(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithChannels(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithChannels(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithChannels(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetChannels (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetChannels (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetChannels (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetChannels (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => Channels    (x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => Channels    (x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => Channels    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => Channels    (x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithChannels(x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithChannels(x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithChannels(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithChannels(x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetChannels (x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetChannels (x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetChannels (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetChannels (x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.Channels    (x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.Channels    (x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Channels    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.Channels    (x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithChannels(x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithChannels(x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithChannels(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithChannels(x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetChannels (x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetChannels (x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetChannels (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetChannels (x.TapeBound.TapeAction,  value)));
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Mono      ());
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Stereo    ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.WithMono  ());
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.WithStereo()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.AsMono    ());
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.AsStereo  ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.SetMono   ());
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.SetStereo ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => Mono      (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => Stereo    (x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => SetMono   (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => SetStereo (x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => WithMono  (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => WithStereo(x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => ConfigWishes.Mono      (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => ConfigWishes.Stereo    (x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => ConfigWishes.WithMono  (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => ConfigWishes.WithStereo(x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => ConfigWishes.AsMono    (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => ConfigWishes.AsStereo  (x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => ConfigWishes.SetMono   (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => ConfigWishes.SetStereo (x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Mono      ());
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Stereo    ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.WithMono  ());
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.WithStereo()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.AsMono    ());
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.AsStereo  ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.SetMono   ());
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.SetStereo ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => Mono      (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => Stereo    (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => WithMono  (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => WithStereo(x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => AsMono    (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => AsStereo  (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => SetMono   (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => SetStereo (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.Mono      (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.Stereo    (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.WithMono  (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.WithStereo(x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.AsMono    (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.AsStereo  (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.SetMono   (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.SetStereo (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Mono      ());
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Stereo    ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithMono  ());
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithStereo()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsMono    ());
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsStereo  ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetMono   ());
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetStereo ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => Mono      (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => Stereo    (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => WithMono  (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => WithStereo(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => AsMono    (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => AsStereo  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => SetMono   (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => SetStereo (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Mono      (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Stereo    (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithMono  (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithStereo(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsMono    (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsStereo  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetMono   (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetStereo (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Mono(     ));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Stereo    ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.WithMono  ());
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.WithStereo()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.AsMono    ());
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.AsStereo  ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.SetMono   ());
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.SetStereo ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => Mono      (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => Stereo    (x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => WithMono  (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => WithStereo(x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => AsMono    (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => AsStereo  (x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => SetMono   (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => SetStereo (x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.Mono      (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.Stereo    (x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.WithMono  (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.WithStereo(x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.AsMono    (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.AsStereo  (x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.SetMono   (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.SetStereo (x.TapeBound.TapeAction)); });
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_Channels(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, init);
                Assert_TapeBound_Getters  (x, init);
                Assert_BuffBound_Getters  (x, value);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters  (x, init);
                
                x.Record();
                Assert_All_Getters(x, init);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .Channels    (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channels    (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithChannels(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithChannels(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetChannels (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetChannels (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => Channels    (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => Channels    (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => WithChannels(x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => WithChannels(x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => SetChannels (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => SetChannels (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.Channels    (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Channels    (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithChannels(x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithChannels(x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetChannels (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetChannels (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Mono      (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Stereo    (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.WithMono  (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.WithStereo(x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.AsMono    (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.AsStereo  (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.SetMono   (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.SetStereo (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => Mono      (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => Stereo    (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => WithMono  (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => WithStereo(x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => AsMono    (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => AsStereo  (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => SetMono   (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => SetStereo (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => ConfigWishes.Mono      (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => ConfigWishes.Stereo    (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => ConfigWishes.WithMono  (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => ConfigWishes.WithStereo(x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => ConfigWishes.AsMono    (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => ConfigWishes.AsStereo  (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => ConfigWishes.SetMono   (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => ConfigWishes.SetStereo (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Mono      (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Stereo    (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithMono  (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithStereo(x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsMono    (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsStereo  (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetMono   (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetStereo (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => Mono      (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => Stereo    (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => WithMono  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => WithStereo(x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => AsMono    (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => AsStereo  (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => SetMono   (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => SetStereo (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Mono      (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Stereo    (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithMono  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithStereo(x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.AsMono    (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.AsStereo  (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetMono   (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetStereo (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_Channels(int init, int value)
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
                    Assert_Independent_Getters(x.Independent.AudioInfoWish,init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Channels    (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithChannels(value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetChannels (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => Channels    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithChannels(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetChannels (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.Channels    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithChannels(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetChannels (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Mono      (x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Stereo    (x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithMono  (x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithStereo(x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsMono    (x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsStereo  (x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetMono   (x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetStereo (x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => Mono      (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => Stereo    (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => WithMono  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => WithStereo(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => AsMono    (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => AsStereo  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => SetMono   (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => SetStereo (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => ConfigWishes.Mono      (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => ConfigWishes.Stereo    (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => ConfigWishes.WithMono  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => ConfigWishes.WithStereo(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => ConfigWishes.AsMono    (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => ConfigWishes.AsStereo  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => ConfigWishes.SetMono   (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => ConfigWishes.SetStereo (x.Independent.Sample, x.SynthBound.Context)); });
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
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, value);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                                                                
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Channels   = value);
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Channels    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithChannels(value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetChannels (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => Channels    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => WithChannels(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SetChannels (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Channels    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithChannels(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetChannels (x.Independent.AudioInfoWish, value)));
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Mono      ());
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Stereo    ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithMono  ());
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithStereo()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.AsMono    ());
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.AsStereo  ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetMono   ());
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetStereo ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => Mono      (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => Stereo    (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => WithMono  (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => WithStereo(x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => AsMono    (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => AsStereo  (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => SetMono   (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => SetStereo (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Mono      (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Stereo    (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithMono  (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithStereo(x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.AsMono    (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.AsStereo  (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetMono   (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetStereo (x.Independent.AudioInfoWish)); });
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
                
                AssertProp(() =>                                             x.Independent.AudioFileInfo.ChannelCount = value);
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Channels     (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithChannels (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetChannels  (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => Channels    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => WithChannels(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => SetChannels (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Channels    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithChannels(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetChannels (x.Independent.AudioFileInfo, value)));
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Mono      ());
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Stereo    ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithMono  ());
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithStereo()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.AsMono    ());
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.AsStereo  ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetMono   ());
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetStereo ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => Mono      (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => Stereo    (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => WithMono  (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => WithStereo(x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => AsMono    (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => AsStereo  (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => SetMono   (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => SetStereo (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Mono      (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Stereo    (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithMono  (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithStereo(x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.AsMono    (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.AsStereo  (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetMono   (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetStereo (x.Independent.AudioFileInfo)); });
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_Channels(int init, int value)
        {
            TestEntities x = CreateTestEntities(init);

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

                AssertProp(() => x.Immutable.WavHeader.Channels    (value));
                AssertProp(() => x.Immutable.WavHeader.WithChannels(value));
                AssertProp(() => x.Immutable.WavHeader.SetChannels (value));
                AssertProp(() => Channels    (x.Immutable.WavHeader, value));
                AssertProp(() => WithChannels(x.Immutable.WavHeader, value));
                AssertProp(() => SetChannels (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.Channels    (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.WithChannels(x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.SetChannels (x.Immutable.WavHeader, value));
                AssertProp(() => value == 1 ? x.Immutable.WavHeader.Mono    () : x.Immutable.WavHeader.Stereo    ());
                AssertProp(() => value == 1 ? x.Immutable.WavHeader.WithMono() : x.Immutable.WavHeader.WithStereo());
                AssertProp(() => value == 1 ? x.Immutable.WavHeader.AsMono  () : x.Immutable.WavHeader.AsStereo  ());
                AssertProp(() => value == 1 ? x.Immutable.WavHeader.SetMono () : x.Immutable.WavHeader.SetStereo ());
                AssertProp(() => value == 1 ? Mono    (x.Immutable.WavHeader) : Stereo    (x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? WithMono(x.Immutable.WavHeader) : WithStereo(x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? AsMono  (x.Immutable.WavHeader) : AsStereo  (x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? SetMono (x.Immutable.WavHeader) : SetStereo (x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? ConfigWishes.Mono    (x.Immutable.WavHeader) : ConfigWishes.Stereo    (x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? ConfigWishes.WithMono(x.Immutable.WavHeader) : ConfigWishes.WithStereo(x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? ConfigWishes.SetMono (x.Immutable.WavHeader) : ConfigWishes.SetStereo (x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? ConfigWishes.AsMono  (x.Immutable.WavHeader) : ConfigWishes.AsStereo  (x.Immutable.WavHeader));
            }

            var speakerSetupEnums = new List<SpeakerSetupEnum>();
            {
                void AssertProp(Func<SpeakerSetupEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SpeakerSetupEnum, init);
                    
                    SpeakerSetupEnum speakerSetupEnum2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SpeakerSetupEnum, init);
                    Assert_Immutable_Getters(speakerSetupEnum2, value);
                    
                    speakerSetupEnums.Add(speakerSetupEnum2);
                }

                AssertProp(() => x.Immutable.SpeakerSetupEnum.Channels    (value));
                AssertProp(() => x.Immutable.SpeakerSetupEnum.WithChannels(value));
                AssertProp(() => x.Immutable.SpeakerSetupEnum.SetChannels (value));
                AssertProp(() => value.ChannelsToEnum());
                AssertProp(() => Channels    (x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => WithChannels(x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => SetChannels (x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => ChannelsToEnum(value));
                AssertProp(() => ConfigWishes.Channels    (x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => ConfigWishes.WithChannels(x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => ConfigWishes.SetChannels (x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => ConfigWishes.ChannelsToEnum(value));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.Mono    () : x.Immutable.SpeakerSetupEnum.Stereo    ());
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.WithMono() : x.Immutable.SpeakerSetupEnum.WithStereo());
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.AsMono  () : x.Immutable.SpeakerSetupEnum.AsStereo  ());
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.SetMono () : x.Immutable.SpeakerSetupEnum.SetStereo ());
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.Mono    () : Stereo    (x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.WithMono() : WithStereo(x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.AsMono  () : AsStereo  (x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.SetMono () : SetStereo (x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.Mono    () : ConfigWishes.Stereo    (x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.WithMono() : ConfigWishes.WithStereo(x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.AsMono  () : ConfigWishes.AsStereo  (x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.SetMono () : ConfigWishes.SetStereo (x.Immutable.SpeakerSetupEnum));
            }
            
            var speakerSetups = new List<SpeakerSetup>();
            {
                void AssertProp(Func<SpeakerSetup> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SpeakerSetup, init);

                    SpeakerSetup speakerSetup2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SpeakerSetup, init);
                    Assert_Immutable_Getters(speakerSetup2, value);
                    
                    speakerSetups.Add(speakerSetup2);
                }

                AssertProp(() => x.Immutable.SpeakerSetup.Channels    (value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SpeakerSetup.WithChannels(value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SpeakerSetup.SetChannels (value, x.SynthBound.Context));
                AssertProp(() => value.ChannelsToEntity(x.SynthBound.Context));
                AssertProp(() => Channels    (x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => WithChannels(x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => SetChannels (x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => ChannelsToEntity(value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.Channels    (x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.WithChannels(x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.SetChannels (x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.ChannelsToEntity(value, x.SynthBound.Context));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetup.Mono    (x.SynthBound.Context) : x.Immutable.SpeakerSetup.Stereo    (x.SynthBound.Context));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetup.WithMono(x.SynthBound.Context) : x.Immutable.SpeakerSetup.WithStereo(x.SynthBound.Context));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetup.AsMono  (x.SynthBound.Context) : x.Immutable.SpeakerSetup.AsStereo  (x.SynthBound.Context));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetup.SetMono (x.SynthBound.Context) : x.Immutable.SpeakerSetup.SetStereo (x.SynthBound.Context));
                AssertProp(() => value == 1 ? Mono    (x.Immutable.SpeakerSetup, x.SynthBound.Context) : Stereo    (x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? WithMono(x.Immutable.SpeakerSetup, x.SynthBound.Context) : WithStereo(x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? AsMono  (x.Immutable.SpeakerSetup, x.SynthBound.Context) : AsStereo  (x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? SetMono (x.Immutable.SpeakerSetup, x.SynthBound.Context) : SetStereo (x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? ConfigWishes.Mono    (x.Immutable.SpeakerSetup, x.SynthBound.Context) : ConfigWishes.Stereo    (x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? ConfigWishes.WithMono(x.Immutable.SpeakerSetup, x.SynthBound.Context) : ConfigWishes.WithStereo(x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? ConfigWishes.AsMono  (x.Immutable.SpeakerSetup, x.SynthBound.Context) : ConfigWishes.AsStereo  (x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? ConfigWishes.SetMono (x.Immutable.SpeakerSetup, x.SynthBound.Context) : ConfigWishes.SetStereo (x.Immutable.SpeakerSetup, x.SynthBound.Context));
            }
            
            var channelEnums = new List<ChannelEnum>();
            {
                ChannelEnum channelEnum;
                
                void AssertProp(Func<ChannelEnum> setter)
                {
                    channelEnum = x.Immutable.ChannelEnum;
                    
                    Assert_Immutable_Getters(channelEnum, init);
                    
                    ChannelEnum channelEnum2 = setter();
                    
                    Assert_Immutable_Getters(channelEnum, init);
                    Assert_Immutable_Getters(channelEnum2, value);
                    
                    channelEnums.Add(channelEnum2);
                }

                AssertProp(() =>        value.ChannelsToChannelEnum(       x.Immutable.Channel));
                AssertProp(() =>              ChannelsToChannelEnum(value, x.Immutable.Channel));
                AssertProp(() => ConfigWishes.ChannelsToChannelEnum(value, x.Immutable.Channel));
                AssertProp(() => channelEnum.Channels    (value));
                AssertProp(() => channelEnum.WithChannels(value));
                AssertProp(() => channelEnum.SetChannels (value));
                AssertProp(() => channelEnum.ToChannels  (value));
                AssertProp(() => Channels    (channelEnum, value));
                AssertProp(() => WithChannels(channelEnum, value));
                AssertProp(() => SetChannels (channelEnum, value));
                AssertProp(() => ToChannels  (channelEnum, value));
                AssertProp(() => ConfigWishes.Channels    (channelEnum, value));
                AssertProp(() => ConfigWishes.WithChannels(channelEnum, value));
                AssertProp(() => ConfigWishes.SetChannels (channelEnum, value));
                AssertProp(() => ConfigWishes.ToChannels  (channelEnum, value));
                AssertProp(() => value == 1 ? channelEnum.Mono    () : channelEnum.Stereo    ());
                AssertProp(() => value == 1 ? channelEnum.WithMono() : channelEnum.WithStereo());
                AssertProp(() => value == 1 ? channelEnum.AsMono  () : channelEnum.AsStereo  ());
                AssertProp(() => value == 1 ? channelEnum.ToMono  () : channelEnum.ToStereo  ());
                AssertProp(() => value == 1 ? channelEnum.SetMono () : channelEnum.SetStereo ());
                AssertProp(() => value == 1 ? Mono    (channelEnum) : Stereo    (channelEnum));
                AssertProp(() => value == 1 ? WithMono(channelEnum) : WithStereo(channelEnum));
                AssertProp(() => value == 1 ? AsMono  (channelEnum) : AsStereo  (channelEnum));
                AssertProp(() => value == 1 ? ToMono  (channelEnum) : ToStereo  (channelEnum));
                AssertProp(() => value == 1 ? SetMono (channelEnum) : SetStereo (channelEnum));
                AssertProp(() => value == 1 ? ConfigWishes.Mono    (channelEnum) : ConfigWishes.Stereo    (channelEnum));
                AssertProp(() => value == 1 ? ConfigWishes.WithMono(channelEnum) : ConfigWishes.WithStereo(channelEnum));
                AssertProp(() => value == 1 ? ConfigWishes.AsMono  (channelEnum) : ConfigWishes.AsStereo  (channelEnum));
                AssertProp(() => value == 1 ? ConfigWishes.ToMono  (channelEnum) : ConfigWishes.ToStereo  (channelEnum));
                AssertProp(() => value == 1 ? ConfigWishes.SetMono (channelEnum) : ConfigWishes.SetStereo (channelEnum));
            }
            
            var channelEntities = new List<Channel>();
            {
                Channel channelEntity;
                IContext context = null;
                
                void AssertProp(Func<Channel> setter)
                {
                    channelEntity = x.Immutable.ChannelEntity;
                    context = x.SynthBound.Context;
                    
                    Assert_Immutable_Getters(channelEntity, init);
                    
                    Channel channelEntity2 = setter();
                    
                    Assert_Immutable_Getters(channelEntity, init);
                    Assert_Immutable_Getters(channelEntity2, value);
                    
                    channelEntities.Add(channelEntity2);
                }

                AssertProp(() =>        value.ChannelsToChannelEntity(       x.Immutable.Channel, context));
                AssertProp(() =>              ChannelsToChannelEntity(value, x.Immutable.Channel, context));
                AssertProp(() => ConfigWishes.ChannelsToChannelEntity(value, x.Immutable.Channel, context));
                AssertProp(() => channelEntity.Channels    (value, context));
                AssertProp(() => channelEntity.WithChannels(value, context));
                AssertProp(() => channelEntity.SetChannels (value, context));
                AssertProp(() => channelEntity.ToChannels  (value, context));
                AssertProp(() => Channels    (channelEntity, value, context));
                AssertProp(() => WithChannels(channelEntity, value, context));
                AssertProp(() => SetChannels (channelEntity, value, context));
                AssertProp(() => ToChannels  (channelEntity, value, context));
                AssertProp(() => ConfigWishes.Channels    (channelEntity, value, context));
                AssertProp(() => ConfigWishes.WithChannels(channelEntity, value, context));
                AssertProp(() => ConfigWishes.SetChannels (channelEntity, value, context));
                AssertProp(() => ConfigWishes.ToChannels  (channelEntity, value, context));
                AssertProp(() => value == 1 ? channelEntity.Mono    (context) : channelEntity.Stereo    (context));
                AssertProp(() => value == 1 ? channelEntity.WithMono(context) : channelEntity.WithStereo(context));
                AssertProp(() => value == 1 ? channelEntity.AsMono  (context) : channelEntity.AsStereo  (context));
                AssertProp(() => value == 1 ? channelEntity.ToMono  (context) : channelEntity.ToStereo  (context));
                AssertProp(() => value == 1 ? channelEntity.SetMono (context) : channelEntity.SetStereo (context));
                AssertProp(() => value == 1 ? Mono    (channelEntity, context) : Stereo    (channelEntity, context));
                AssertProp(() => value == 1 ? WithMono(channelEntity, context) : WithStereo(channelEntity, context));
                AssertProp(() => value == 1 ? AsMono  (channelEntity, context) : AsStereo  (channelEntity, context));
                AssertProp(() => value == 1 ? ToMono  (channelEntity, context) : ToStereo  (channelEntity, context));
                AssertProp(() => value == 1 ? SetMono (channelEntity, context) : SetStereo (channelEntity, context));
                AssertProp(() => value == 1 ? ConfigWishes.Mono    (channelEntity, context) : ConfigWishes.Stereo    (channelEntity, context));
                AssertProp(() => value == 1 ? ConfigWishes.WithMono(channelEntity, context) : ConfigWishes.WithStereo(channelEntity, context));
                AssertProp(() => value == 1 ? ConfigWishes.AsMono  (channelEntity, context) : ConfigWishes.AsStereo  (channelEntity, context));
                AssertProp(() => value == 1 ? ConfigWishes.ToMono  (channelEntity, context) : ConfigWishes.ToStereo  (channelEntity, context));
                AssertProp(() => value == 1 ? ConfigWishes.SetMono (channelEntity, context) : ConfigWishes.SetStereo (channelEntity, context));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders       .ForEach(w => Assert_Immutable_Getters(w, value));
            speakerSetupEnums.ForEach(e => Assert_Immutable_Getters(e, value));
            speakerSetups    .ForEach(s => Assert_Immutable_Getters(s, value));
        }

        [TestMethod]
        public void ConfigSection_Channels()
        {
            // Synth-Bound. Immutable. Get-only.
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            AreEqual(DefaultChannels,      () => configSection.Channels);
            AreEqual(DefaultChannels,      () => configSection.Channels   ());
            AreEqual(DefaultChannels,      () => configSection.GetChannels());
            AreEqual(DefaultChannels == 1, () => configSection.IsMono     ());
            AreEqual(DefaultChannels == 2, () => configSection.IsStereo   ());
            AreEqual(DefaultChannels,      () => Channels   (configSection));
            AreEqual(DefaultChannels,      () => GetChannels(configSection));
            AreEqual(DefaultChannels == 1, () => IsMono     (configSection));
            AreEqual(DefaultChannels == 2, () => IsStereo   (configSection));
            AreEqual(DefaultChannels,      () => ConfigWishesAccessor.Channels   (configSection));
            AreEqual(DefaultChannels,      () => ConfigWishesAccessor.GetChannels(configSection));
            AreEqual(DefaultChannels == 1, () => ConfigWishesAccessor.IsMono     (configSection));
            AreEqual(DefaultChannels == 2, () => ConfigWishesAccessor.IsStereo   (configSection));
        }

        // Getter Helpers

        internal static void Assert_All_Getters(TestEntities x, int channels)
        {
            Assert_Bound_Getters      (x, channels);
            Assert_Independent_Getters(x, channels);
            Assert_Immutable_Getters  (x, channels);
        }

        private static void Assert_Bound_Getters(TestEntities x, int channels)
        {
            Assert_SynthBound_Getters(x, channels);
            Assert_TapeBound_Getters (x, channels);
            Assert_BuffBound_Getters (x, channels);
        }
        
        private static void Assert_Independent_Getters(TestEntities x, int channels)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample,        channels);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, channels);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, channels);
        }

        private static void Assert_Immutable_Getters(TestEntities x, int channels)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader,        channels);
            Assert_Immutable_Getters(x.Immutable.SpeakerSetupEnum, channels);
            Assert_Immutable_Getters(x.Immutable.SpeakerSetup,     channels);
            Assert_Immutable_Getters(x.Immutable.ChannelEnum,      channels);
            Assert_Immutable_Getters(x.Immutable.ChannelEntity,    channels);
        }

        private static void Assert_SynthBound_Getters(TestEntities x, int channels)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigResolver);
            AreEqual(channels,      () => x.SynthBound.SynthWishes   .GetChannels  );
            AreEqual(channels,      () => x.SynthBound.FlowNode      .GetChannels  );
            AreEqual(channels,      () => x.SynthBound.ConfigResolver.GetChannels  );
            AreEqual(channels == 1, () => x.SynthBound.SynthWishes   .IsMono       );
            AreEqual(channels == 1, () => x.SynthBound.FlowNode      .IsMono       );
            AreEqual(channels == 1, () => x.SynthBound.ConfigResolver.IsMono       );
            AreEqual(channels == 2, () => x.SynthBound.SynthWishes   .IsStereo     );
            AreEqual(channels == 2, () => x.SynthBound.FlowNode      .IsStereo     );
            AreEqual(channels == 2, () => x.SynthBound.ConfigResolver.IsStereo     );
            AreEqual(channels,      () => x.SynthBound.SynthWishes   .Channels   ());
            AreEqual(channels,      () => x.SynthBound.FlowNode      .Channels   ());
            AreEqual(channels,      () => x.SynthBound.ConfigResolver.Channels   ());
            AreEqual(channels,      () => x.SynthBound.SynthWishes   .GetChannels());
            AreEqual(channels,      () => x.SynthBound.FlowNode      .GetChannels());
            AreEqual(channels,      () => x.SynthBound.ConfigResolver.GetChannels());
            AreEqual(channels == 1, () => x.SynthBound.SynthWishes   .IsMono     ());
            AreEqual(channels == 1, () => x.SynthBound.FlowNode      .IsMono     ());
            AreEqual(channels == 1, () => x.SynthBound.ConfigResolver.IsMono     ());
            AreEqual(channels == 2, () => x.SynthBound.SynthWishes   .IsStereo   ());
            AreEqual(channels == 2, () => x.SynthBound.FlowNode      .IsStereo   ());
            AreEqual(channels == 2, () => x.SynthBound.ConfigResolver.IsStereo   ());
            AreEqual(channels,      () => Channels   (x.SynthBound.SynthWishes   ));
            AreEqual(channels,      () => Channels   (x.SynthBound.FlowNode      ));
            AreEqual(channels,      () => Channels   (x.SynthBound.ConfigResolver));
            AreEqual(channels,      () => GetChannels(x.SynthBound.SynthWishes   ));
            AreEqual(channels,      () => GetChannels(x.SynthBound.FlowNode      ));
            AreEqual(channels,      () => GetChannels(x.SynthBound.ConfigResolver));
            AreEqual(channels == 1, () => IsMono     (x.SynthBound.SynthWishes   ));
            AreEqual(channels == 1, () => IsMono     (x.SynthBound.FlowNode      ));
            AreEqual(channels == 1, () => IsMono     (x.SynthBound.ConfigResolver));
            AreEqual(channels == 2, () => IsStereo   (x.SynthBound.SynthWishes   ));
            AreEqual(channels == 2, () => IsStereo   (x.SynthBound.FlowNode      ));
            AreEqual(channels == 2, () => IsStereo   (x.SynthBound.ConfigResolver));
            AreEqual(channels,      () => ConfigWishes        .Channels   (x.SynthBound.SynthWishes   ));
            AreEqual(channels,      () => ConfigWishes        .Channels   (x.SynthBound.FlowNode      ));
            AreEqual(channels,      () => ConfigWishesAccessor.Channels   (x.SynthBound.ConfigResolver));
            AreEqual(channels,      () => ConfigWishes        .GetChannels(x.SynthBound.SynthWishes   ));
            AreEqual(channels,      () => ConfigWishes        .GetChannels(x.SynthBound.FlowNode      ));
            AreEqual(channels,      () => ConfigWishesAccessor.GetChannels(x.SynthBound.ConfigResolver));
            AreEqual(channels == 1, () => ConfigWishes        .IsMono     (x.SynthBound.SynthWishes   ));
            AreEqual(channels == 1, () => ConfigWishes        .IsMono     (x.SynthBound.FlowNode      ));
            AreEqual(channels == 1, () => ConfigWishesAccessor.IsMono     (x.SynthBound.ConfigResolver));
            AreEqual(channels == 2, () => ConfigWishes        .IsStereo   (x.SynthBound.SynthWishes   ));
            AreEqual(channels == 2, () => ConfigWishes        .IsStereo   (x.SynthBound.FlowNode      ));
            AreEqual(channels == 2, () => ConfigWishesAccessor.IsStereo   (x.SynthBound.ConfigResolver));

        }
        
        private static void Assert_TapeBound_Getters(TestEntities x, int channels)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);
            AreEqual(channels,      () => x.TapeBound.TapeConfig .Channels);
            AreEqual(channels,      () => x.TapeBound.Tape       .Channels   ());
            AreEqual(channels,      () => x.TapeBound.TapeConfig .Channels   ());
            AreEqual(channels,      () => x.TapeBound.TapeActions.Channels   ());
            AreEqual(channels,      () => x.TapeBound.TapeAction .Channels   ());
            AreEqual(channels,      () => x.TapeBound.Tape       .GetChannels());
            AreEqual(channels,      () => x.TapeBound.TapeConfig .GetChannels());
            AreEqual(channels,      () => x.TapeBound.TapeActions.GetChannels());
            AreEqual(channels,      () => x.TapeBound.TapeAction .GetChannels());
            AreEqual(channels == 1, () => x.TapeBound.Tape       .IsMono     ());
            AreEqual(channels == 1, () => x.TapeBound.TapeConfig .IsMono     ());
            AreEqual(channels == 1, () => x.TapeBound.TapeActions.IsMono     ());
            AreEqual(channels == 1, () => x.TapeBound.TapeAction .IsMono     ());
            AreEqual(channels == 2, () => x.TapeBound.Tape       .IsStereo   ());
            AreEqual(channels == 2, () => x.TapeBound.TapeConfig .IsStereo   ());
            AreEqual(channels == 2, () => x.TapeBound.TapeActions.IsStereo   ());
            AreEqual(channels == 2, () => x.TapeBound.TapeAction .IsStereo   ());
            AreEqual(channels,      () => Channels   (x.TapeBound.Tape       ));
            AreEqual(channels,      () => Channels   (x.TapeBound.TapeConfig ));
            AreEqual(channels,      () => Channels   (x.TapeBound.TapeActions));
            AreEqual(channels,      () => Channels   (x.TapeBound.TapeAction ));
            AreEqual(channels,      () => GetChannels(x.TapeBound.Tape       ));
            AreEqual(channels,      () => GetChannels(x.TapeBound.TapeConfig ));
            AreEqual(channels,      () => GetChannels(x.TapeBound.TapeActions));
            AreEqual(channels,      () => GetChannels(x.TapeBound.TapeAction ));
            AreEqual(channels == 1, () => IsMono     (x.TapeBound.Tape       ));
            AreEqual(channels == 1, () => IsMono     (x.TapeBound.TapeConfig ));
            AreEqual(channels == 1, () => IsMono     (x.TapeBound.TapeActions));
            AreEqual(channels == 1, () => IsMono     (x.TapeBound.TapeAction ));
            AreEqual(channels == 2, () => IsStereo   (x.TapeBound.Tape       ));
            AreEqual(channels == 2, () => IsStereo   (x.TapeBound.TapeConfig ));
            AreEqual(channels == 2, () => IsStereo   (x.TapeBound.TapeActions));
            AreEqual(channels == 2, () => IsStereo   (x.TapeBound.TapeAction ));
            AreEqual(channels,      () => ConfigWishes.Channels   (x.TapeBound.Tape       ));
            AreEqual(channels,      () => ConfigWishes.Channels   (x.TapeBound.TapeConfig ));
            AreEqual(channels,      () => ConfigWishes.Channels   (x.TapeBound.TapeActions));
            AreEqual(channels,      () => ConfigWishes.Channels   (x.TapeBound.TapeAction ));
            AreEqual(channels,      () => ConfigWishes.GetChannels(x.TapeBound.Tape       ));
            AreEqual(channels,      () => ConfigWishes.GetChannels(x.TapeBound.TapeConfig ));
            AreEqual(channels,      () => ConfigWishes.GetChannels(x.TapeBound.TapeActions));
            AreEqual(channels,      () => ConfigWishes.GetChannels(x.TapeBound.TapeAction ));
            AreEqual(channels == 1, () => ConfigWishes.IsMono     (x.TapeBound.Tape       ));
            AreEqual(channels == 1, () => ConfigWishes.IsMono     (x.TapeBound.TapeConfig ));
            AreEqual(channels == 1, () => ConfigWishes.IsMono     (x.TapeBound.TapeActions));
            AreEqual(channels == 1, () => ConfigWishes.IsMono     (x.TapeBound.TapeAction ));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo   (x.TapeBound.Tape       ));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo   (x.TapeBound.TapeConfig ));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo   (x.TapeBound.TapeActions));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo   (x.TapeBound.TapeAction ));
        }
        
        /// <inheritdoc cref="_channeltoaudiofileoutput" />
        private static void Assert_BuffBound_Getters(TestEntities x, int channels)
        {
            IsNotNull (               () => x                                                    );
            IsNotNull (               () => x.BuffBound                                          );
            IsNotNull (               () => x.BuffBound.Buff                                     );
            IsNotNull (               () => x.BuffBound.AudioFileOutput                          );
            AreEqual  (channels,      () => x.BuffBound.Buff           .Channels()               );
            AreEqual  (channels,      () => x.BuffBound.AudioFileOutput.Channels()               );
            AreEqual  (channels,      () => x.BuffBound.Buff           .GetChannels()            );
            AreEqual  (channels,      () => x.BuffBound.AudioFileOutput.GetChannels()            );
            AreEqual  (channels,      () => Channels   (x.BuffBound.Buff)                        );
            AreEqual  (channels,      () => Channels   (x.BuffBound.AudioFileOutput)             );
            AreEqual  (channels,      () => GetChannels(x.BuffBound.Buff)                        );
            AreEqual  (channels,      () => GetChannels(x.BuffBound.AudioFileOutput)             );
            AreEqual  (channels,      () => ConfigWishes.Channels   (x.BuffBound.Buff)           );
            AreEqual  (channels,      () => ConfigWishes.Channels   (x.BuffBound.AudioFileOutput));
            AreEqual  (channels,      () => ConfigWishes.GetChannels(x.BuffBound.Buff)           );
            AreEqual  (channels,      () => ConfigWishes.GetChannels(x.BuffBound.AudioFileOutput));
            // Stereo can be 2 channels, but also 1 channel represented as either Left (0) or Right (1).
            // This makes each possible state a representation of a Stereo state, making IsStereo always true.
            IsTrue    (               () => x.BuffBound.Buff           .IsStereo()               );
            IsTrue    (               () => x.BuffBound.AudioFileOutput.IsStereo()               );
            IsTrue    (               () =>              IsStereo(x.BuffBound.Buff)              );
            IsTrue    (               () =>              IsStereo(x.BuffBound.AudioFileOutput)   );
            IsTrue    (               () => ConfigWishes.IsStereo(x.BuffBound.Buff)              );
            IsTrue    (               () => ConfigWishes.IsStereo(x.BuffBound.AudioFileOutput)   );
            // Old implementation: These previously assumed that Stereo = exactly 2 channels, which is no longer the case.
            //AreEqual(channels == 2, () => x.BuffBound.Buff           .IsStereo()               );
            //AreEqual(channels == 2, () => x.BuffBound.AudioFileOutput.IsStereo()               );
            //AreEqual(channels == 2, () =>              IsStereo(x.BuffBound.Buff)              );
            //AreEqual(channels == 2, () =>              IsStereo(x.BuffBound.AudioFileOutput)   );
            //AreEqual(channels == 2, () => ConfigWishes.IsStereo(x.BuffBound.Buff           )   );
            //AreEqual(channels == 2, () => ConfigWishes.IsStereo(x.BuffBound.AudioFileOutput)   );
            // 1 Channel can mean Mono, so IsMono is true. However, it could also mean a single channel of a stereo pair, but it's still also a mono state.
            AreEqual  (channels == 1, () => x.BuffBound.Buff           .IsMono()                 );
            AreEqual  (channels == 1, () => x.BuffBound.AudioFileOutput.IsMono()                 );
            AreEqual  (channels == 1, () =>              IsMono(x.BuffBound.Buff)                );
            AreEqual  (channels == 1, () =>              IsMono(x.BuffBound.AudioFileOutput)     );
            AreEqual  (channels == 1, () => ConfigWishes.IsMono(x.BuffBound.Buff)                );
            AreEqual  (channels == 1, () => ConfigWishes.IsMono(x.BuffBound.AudioFileOutput)     );
        }

        private static void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int channels)
        {
            IsNotNull(() => audioFileInfo);
            AreEqual(channels,      () => audioFileInfo.Channels   ());
            AreEqual(channels,      () => audioFileInfo.GetChannels());
            AreEqual(channels == 1, () => audioFileInfo.IsMono     ());
            AreEqual(channels == 2, () => audioFileInfo.IsStereo   ());
            AreEqual(channels,      () => Channels   (audioFileInfo));
            AreEqual(channels,      () => GetChannels(audioFileInfo));
            AreEqual(channels == 1, () => IsMono     (audioFileInfo));
            AreEqual(channels == 2, () => IsStereo   (audioFileInfo));
            AreEqual(channels,      () => ConfigWishes.Channels   (audioFileInfo));
            AreEqual(channels,      () => ConfigWishes.GetChannels(audioFileInfo));
            AreEqual(channels == 1, () => ConfigWishes.IsMono     (audioFileInfo));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo   (audioFileInfo));
        }
        
        private static void Assert_Independent_Getters(Sample sample, int channels)
        {
            IsNotNull(() => sample);
            AreEqual(channels,      () => sample.Channels   ());
            AreEqual(channels,      () => sample.GetChannels());
            AreEqual(channels == 1, () => sample.IsMono     ());
            AreEqual(channels == 2, () => sample.IsStereo   ());
            AreEqual(channels,      () => Channels   (sample));
            AreEqual(channels,      () => GetChannels(sample));
            AreEqual(channels == 1, () => IsMono     (sample));
            AreEqual(channels == 2, () => IsStereo   (sample));
            AreEqual(channels,      () => ConfigWishes.Channels   (sample));
            AreEqual(channels,      () => ConfigWishes.GetChannels(sample));
            AreEqual(channels == 1, () => ConfigWishes.IsMono     (sample));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo   (sample));
        }
        
        private static void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int channels)
        {
            IsNotNull(() => audioInfoWish);
            AreEqual(channels,      () => audioInfoWish.Channels);
            AreEqual(channels,      () => audioInfoWish.Channels   ());
            AreEqual(channels,      () => audioInfoWish.GetChannels());
            AreEqual(channels == 1, () => audioInfoWish.IsMono     ());
            AreEqual(channels == 2, () => audioInfoWish.IsStereo   ());
            AreEqual(channels,      () => Channels   (audioInfoWish));
            AreEqual(channels,      () => GetChannels(audioInfoWish));
            AreEqual(channels == 1, () => IsMono     (audioInfoWish));
            AreEqual(channels == 2, () => IsStereo   (audioInfoWish));
            AreEqual(channels,      () => ConfigWishes.Channels   (audioInfoWish));
            AreEqual(channels,      () => ConfigWishes.GetChannels(audioInfoWish));
            AreEqual(channels == 1, () => ConfigWishes.IsMono     (audioInfoWish));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo   (audioInfoWish));
        }

        private static void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int channels)
        {
            IsTrue(() => Has(wavHeader));
            AreEqual(channels,      () => wavHeader.ChannelCount);
            AreEqual(channels,      () => wavHeader.Channels   ());
            AreEqual(channels,      () => wavHeader.GetChannels());
            AreEqual(channels == 1, () => wavHeader.IsMono     ());
            AreEqual(channels == 2, () => wavHeader.IsStereo   ());
            AreEqual(channels,      () => Channels   (wavHeader));
            AreEqual(channels,      () => GetChannels(wavHeader));
            AreEqual(channels == 1, () => IsMono     (wavHeader));
            AreEqual(channels == 2, () => IsStereo   (wavHeader));
            AreEqual(channels,      () => ConfigWishes.Channels   (wavHeader));
            AreEqual(channels,      () => ConfigWishes.GetChannels(wavHeader));
            AreEqual(channels == 1, () => ConfigWishes.IsMono     (wavHeader));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo   (wavHeader));
        }
        
        private static void Assert_Immutable_Getters(SpeakerSetupEnum speakerSetupEnum, int channels)
        {
            IsTrue(() => Has(speakerSetupEnum));
            AreEqual(channels,      () => speakerSetupEnum.Channels      ());
            AreEqual(channels,      () => speakerSetupEnum.GetChannels   ());
            AreEqual(channels,      () => speakerSetupEnum.ToChannels    ());
            AreEqual(channels,      () => speakerSetupEnum.EnumToChannels());
            AreEqual(channels == 1, () => speakerSetupEnum.IsMono        ());
            AreEqual(channels == 2, () => speakerSetupEnum.IsStereo      ());
            AreEqual(channels,      () => Channels      (speakerSetupEnum));
            AreEqual(channels,      () => GetChannels   (speakerSetupEnum));
            AreEqual(channels,      () => ToChannels    (speakerSetupEnum));
            AreEqual(channels,      () => EnumToChannels(speakerSetupEnum));
            AreEqual(channels == 1, () => IsMono        (speakerSetupEnum));
            AreEqual(channels == 2, () => IsStereo      (speakerSetupEnum));
            AreEqual(channels,      () => ConfigWishes.Channels      (speakerSetupEnum));
            AreEqual(channels,      () => ConfigWishes.GetChannels   (speakerSetupEnum));
            AreEqual(channels,      () => ConfigWishes.ToChannels    (speakerSetupEnum));
            AreEqual(channels,      () => ConfigWishes.EnumToChannels(speakerSetupEnum));
            AreEqual(channels == 1, () => ConfigWishes.IsMono        (speakerSetupEnum));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo      (speakerSetupEnum));
        }
                
        private static void Assert_Immutable_Getters(SpeakerSetup speakerSetup, int channels)
        {
            IsNotNull(() => speakerSetup);
            AreEqual(channels,      () => speakerSetup.Channels        ());
            AreEqual(channels,      () => speakerSetup.GetChannels     ());
            AreEqual(channels,      () => speakerSetup.ToChannels      ());
            AreEqual(channels,      () => speakerSetup.EntityToChannels());
            AreEqual(channels == 1, () => speakerSetup.IsMono          ());
            AreEqual(channels == 2, () => speakerSetup.IsStereo        ());
            AreEqual(channels,      () => Channels        (speakerSetup));
            AreEqual(channels,      () => GetChannels     (speakerSetup));
            AreEqual(channels,      () => ToChannels      (speakerSetup));
            AreEqual(channels,      () => EntityToChannels(speakerSetup));
            AreEqual(channels == 1, () => IsMono          (speakerSetup));
            AreEqual(channels == 2, () => IsStereo        (speakerSetup));
            AreEqual(channels,      () => ConfigWishes.Channels        (speakerSetup));
            AreEqual(channels,      () => ConfigWishes.GetChannels     (speakerSetup));
            AreEqual(channels,      () => ConfigWishes.ToChannels      (speakerSetup));
            AreEqual(channels,      () => ConfigWishes.EntityToChannels(speakerSetup));
            AreEqual(channels == 1, () => ConfigWishes.IsMono          (speakerSetup));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo        (speakerSetup));
        }
                
        private static void Assert_Immutable_Getters(ChannelEnum channelEnum, int channels)
        {
            AreEqual(channels,      () => channelEnum.ChannelEnumToChannels());
            AreEqual(channels,      () => channelEnum.Channels             ());
            AreEqual(channels,      () => channelEnum.GetChannels          ());
            AreEqual(channels,      () => channelEnum.ToChannels           ());
            AreEqual(channels == 1, () => channelEnum.IsMono               ());
            AreEqual(channels == 2, () => channelEnum.IsStereo             ());
            AreEqual(channels,      () => ChannelEnumToChannels(channelEnum));
            AreEqual(channels,      () => Channels             (channelEnum));
            AreEqual(channels,      () => GetChannels          (channelEnum));
            AreEqual(channels,      () => ToChannels           (channelEnum));
            AreEqual(channels == 1, () => IsMono               (channelEnum));
            AreEqual(channels == 2, () => IsStereo             (channelEnum));
            AreEqual(channels,      () => ConfigWishes.ChannelEnumToChannels(channelEnum));
            AreEqual(channels,      () => ConfigWishes.Channels             (channelEnum));
            AreEqual(channels,      () => ConfigWishes.GetChannels          (channelEnum));
            AreEqual(channels,      () => ConfigWishes.ToChannels           (channelEnum));
            AreEqual(channels == 1, () => ConfigWishes.IsMono               (channelEnum));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo             (channelEnum));
        }
                
        private static void Assert_Immutable_Getters(Channel channelEntity, int channels)
        {
            AreEqual(channels,      () => channelEntity.ChannelEntityToChannels());
            AreEqual(channels,      () => channelEntity.Channels               ());
            AreEqual(channels,      () => channelEntity.GetChannels            ());
            AreEqual(channels,      () => channelEntity.ToChannels             ());
            AreEqual(channels == 1, () => channelEntity.IsMono                 ());
            AreEqual(channels == 2, () => channelEntity.IsStereo               ());
            AreEqual(channels,      () => ChannelEntityToChannels(channelEntity));
            AreEqual(channels,      () => Channels               (channelEntity));
            AreEqual(channels,      () => GetChannels            (channelEntity));
            AreEqual(channels,      () => ToChannels             (channelEntity));
            AreEqual(channels == 1, () => IsMono                 (channelEntity));
            AreEqual(channels == 2, () => IsStereo               (channelEntity));
            AreEqual(channels,      () => ConfigWishes.ChannelEntityToChannels(channelEntity));
            AreEqual(channels,      () => ConfigWishes.Channels               (channelEntity));
            AreEqual(channels,      () => ConfigWishes.GetChannels            (channelEntity));
            AreEqual(channels,      () => ConfigWishes.ToChannels             (channelEntity));
            AreEqual(channels == 1, () => ConfigWishes.IsMono                 (channelEntity));
            AreEqual(channels == 2, () => ConfigWishes.IsStereo               (channelEntity));
        }

        // Test Data Helpers
        
        private TestEntities CreateTestEntities(int? channels, [CallerMemberName] string name = null) 
            => new TestEntities(x => x.NoLog().WithChannels(channels).SamplingRate(HighPerfHz), name);

        static object TestParameters => new[] // ncrunch: no coverage
        {
            new object[] { 1, 2 },
            new object[] { 2, 1 }
        };

        static object TestParametersWithEmpties => new[] // ncrunch: no coverage
        {
            new object[] { 1   , 2    },
            new object[] { 2   , 1    },
            new object[] { 0   , 2    },
            new object[] { 2   , null },
            new object[] { 1   , 0    },
            new object[] { null, 1    }
        };
    } 
}