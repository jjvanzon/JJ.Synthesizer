using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0611 
#pragma warning disable MSTEST0018 
#pragma warning disable IDE0002


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
            void AssertProp(Action<ConfigTestEntities> setter)
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
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.SetMono_Call     ());
                              if (value == 2 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.SetStereo_Call   ()); 
                              if (!Has(value)) AreEqual(x.SynthBound.Derived,       x.SynthBound.Derived.SetChannels_Call (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.WithMono_Call    ());
                              if (value == 2 ) AreEqual(x.SynthBound.Derived, () => x.SynthBound.Derived.WithStereo_Call  ()); 
                              if (!Has(value)) AreEqual(x.SynthBound.Derived,       x.SynthBound.Derived.WithChannels_Call(value)); });
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Channels         (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Channels         (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Channels         (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetChannels      (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetChannels      (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetChannels      (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithChannels     (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithChannels     (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithChannels     (value)));
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
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Mono        (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Stereo      (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.Channels    (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.SetMono     (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.SetStereo   (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.SetChannels (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithMono    (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithStereo  (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.WithChannels(value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => Mono        (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => Stereo      (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       Channels    (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => SetMono     (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => SetStereo   (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       SetChannels (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => WithMono    (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => WithStereo  (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       WithChannels(x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.Mono        (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.Stereo      (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       ConfigWishes.Channels    (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.SetMono     (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.SetStereo   (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       ConfigWishes.SetChannels (x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.WithMono    (x.SynthBound.SynthWishes       ));
                              if (value == 2 ) AreEqual(x.SynthBound.SynthWishes, () => ConfigWishes.WithStereo  (x.SynthBound.SynthWishes       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       ConfigWishes.WithChannels(x.SynthBound.SynthWishes, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Mono        (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Stereo      (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.Channels    (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.SetMono     (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.SetStereo   (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.SetChannels (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithMono    (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithStereo  (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.WithChannels(value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => Mono        (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => Stereo      (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       Channels    (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => SetMono     (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => SetStereo   (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       SetChannels (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => WithMono    (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => WithStereo  (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       WithChannels(x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.Mono        (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.Stereo      (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       ConfigWishes.Channels    (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.SetMono     (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.SetStereo   (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       ConfigWishes.SetChannels (x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.WithMono    (x.SynthBound.FlowNode       ));
                              if (value == 2 ) AreEqual(x.SynthBound.FlowNode, () => ConfigWishes.WithStereo  (x.SynthBound.FlowNode       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       ConfigWishes.WithChannels(x.SynthBound.FlowNode, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Mono        (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Stereo      (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.Channels    (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.SetMono     (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.SetStereo   (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.SetChannels (value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithMono    (     ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithStereo  (     )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.WithChannels(value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => Mono        (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => Stereo      (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       Channels    (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => SetMono     (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => SetStereo   (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       SetChannels (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => WithMono    (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => WithStereo  (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       WithChannels(x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.Mono        (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.Stereo      (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.Channels    (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.SetMono     (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.SetStereo   (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.SetChannels (x.SynthBound.ConfigResolver, value)); });
            AssertProp(x => { if (value == 1 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.WithMono    (x.SynthBound.ConfigResolver       ));
                              if (value == 2 ) AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.WithStereo  (x.SynthBound.ConfigResolver       )); 
                              if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       ConfigWishesAccessor.WithChannels(x.SynthBound.ConfigResolver, value)); });
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_Channels(int init, int value)
        {
            void AssertProp(Action<ConfigTestEntities> setter)
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
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetChannels (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetChannels (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetChannels (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetChannels (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithChannels(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithChannels(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithChannels(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithChannels(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => Channels    (x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => Channels    (x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => Channels    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => Channels    (x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetChannels (x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetChannels (x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetChannels (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetChannels (x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithChannels(x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithChannels(x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithChannels(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithChannels(x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.Channels    (x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.Channels    (x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Channels    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.Channels    (x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetChannels (x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetChannels (x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetChannels (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetChannels (x.TapeBound.TapeAction,  value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithChannels(x.TapeBound.Tape,        value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithChannels(x.TapeBound.TapeConfig,  value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithChannels(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithChannels(x.TapeBound.TapeAction,  value)));
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Mono      ());
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Stereo    ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.SetMono   ());
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.SetStereo ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.WithMono  ());
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.WithStereo()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => Mono      (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => Stereo    (x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => SetMono   (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => SetStereo (x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => WithMono  (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => WithStereo(x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => ConfigWishes.Mono      (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => ConfigWishes.Stereo    (x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => ConfigWishes.SetMono   (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => ConfigWishes.SetStereo (x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.Tape, () => ConfigWishes.WithMono  (x.TapeBound.Tape));
                              if (value == 2) AreEqual(x.TapeBound.Tape, () => ConfigWishes.WithStereo(x.TapeBound.Tape)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Mono      ());
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Stereo    ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.SetMono   ());
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.SetStereo ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.WithMono  ());
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.WithStereo()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => Mono      (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => Stereo    (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => SetMono   (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => SetStereo (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => WithMono  (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => WithStereo(x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.Mono      (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.Stereo    (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.SetMono   (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.SetStereo (x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.WithMono  (x.TapeBound.TapeConfig));
                              if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => ConfigWishes.WithStereo(x.TapeBound.TapeConfig)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Mono      ());
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Stereo    ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetMono   ());
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetStereo ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithMono  ());
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithStereo()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => Mono      (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => Stereo    (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => SetMono   (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => SetStereo (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => WithMono  (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => WithStereo(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Mono      (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Stereo    (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetMono   (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetStereo (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithMono  (x.TapeBound.TapeActions));
                              if (value == 2) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithStereo(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Mono(     ));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Stereo    ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.SetMono   ());
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.SetStereo ()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.WithMono  ());
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.WithStereo()); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => Mono      (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => Stereo    (x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => SetMono   (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => SetStereo (x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => WithMono  (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => WithStereo(x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.Mono      (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.Stereo    (x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.SetMono   (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.SetStereo (x.TapeBound.TapeAction)); });
            AssertProp(x => { if (value == 1) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.WithMono  (x.TapeBound.TapeAction));
                              if (value == 2) AreEqual(x.TapeBound.TapeAction, () => ConfigWishes.WithStereo(x.TapeBound.TapeAction)); });
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_Channels(int init, int value)
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

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .Channels    (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channels    (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetChannels (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetChannels (value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithChannels(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithChannels(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => Channels    (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => Channels    (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => SetChannels (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => SetChannels (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => WithChannels(x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => WithChannels(x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.Channels    (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Channels    (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetChannels (x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetChannels (x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithChannels(x.BuffBound.Buff,            value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithChannels(x.BuffBound.AudioFileOutput, value, x.SynthBound.Context)));
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Mono      (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Stereo    (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.SetMono   (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.SetStereo (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.WithMono  (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.WithStereo(x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => Mono      (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => Stereo    (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => SetMono   (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => SetStereo (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => WithMono  (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => WithStereo(x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => ConfigWishes.Mono      (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => ConfigWishes.Stereo    (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => ConfigWishes.SetMono   (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => ConfigWishes.SetStereo (x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.Buff, () => ConfigWishes.WithMono  (x.BuffBound.Buff, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.Buff, () => ConfigWishes.WithStereo(x.BuffBound.Buff, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Mono      (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Stereo    (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetMono   (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetStereo (x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithMono  (x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithStereo(x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => Mono      (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => Stereo    (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => SetMono   (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => SetStereo (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => WithMono  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => WithStereo(x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Mono      (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.Stereo    (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetMono   (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetStereo (x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
            AssertProp(x => { if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithMono  (x.BuffBound.AudioFileOutput, x.SynthBound.Context));
                              if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithStereo(x.BuffBound.AudioFileOutput, x.SynthBound.Context)); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_Channels(int init, int value)
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
                    Assert_Independent_Getters(x.Independent.AudioInfoWish,init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Channels    (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetChannels (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithChannels(value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => Channels    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetChannels (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithChannels(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.Channels    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetChannels (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithChannels(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Mono      (x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Stereo    (x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetMono   (x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetStereo (x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithMono  (x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithStereo(x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => Mono      (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => Stereo    (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => SetMono   (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => SetStereo (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => WithMono  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => WithStereo(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => ConfigWishes.Mono      (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => ConfigWishes.Stereo    (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => ConfigWishes.SetMono   (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => ConfigWishes.SetStereo (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.Sample, () => ConfigWishes.WithMono  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == 2) AreEqual(x.Independent.Sample, () => ConfigWishes.WithStereo(x.Independent.Sample, x.SynthBound.Context)); });
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
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, value);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                                                                
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Channels   = value);
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Channels    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetChannels (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithChannels(value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => Channels    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SetChannels (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => WithChannels(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Channels    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetChannels (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithChannels(x.Independent.AudioInfoWish, value)));
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Mono      ());
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Stereo    ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetMono   ());
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetStereo ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithMono  ());
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithStereo()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => Mono      (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => Stereo    (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => SetMono   (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => SetStereo (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => WithMono  (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => WithStereo(x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Mono      (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.Stereo    (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetMono   (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetStereo (x.Independent.AudioInfoWish)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithMono  (x.Independent.AudioInfoWish));
                                   if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithStereo(x.Independent.AudioInfoWish)); });
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
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, value);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                
                AssertProp(() =>                                             x.Independent.AudioFileInfo.ChannelCount = value);
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Channels    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetChannels (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithChannels(value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => Channels    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => SetChannels (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => WithChannels(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Channels    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetChannels (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithChannels(x.Independent.AudioFileInfo, value)));
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Mono      ());
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Stereo    ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetMono   ());
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetStereo ()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithMono  ());
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithStereo()); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => Mono      (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => Stereo    (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => SetMono   (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => SetStereo (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => WithMono  (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => WithStereo(x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Mono      (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.Stereo    (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetMono   (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetStereo (x.Independent.AudioFileInfo)); });
                AssertProp(() => { if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithMono  (x.Independent.AudioFileInfo));
                                   if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithStereo(x.Independent.AudioFileInfo)); });
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_Channels(int init, int value)
        {
            ConfigTestEntities x = CreateTestEntities(init);

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

                AssertProp(() => x.Immutable.WavHeader.Channels    (value));
                AssertProp(() => x.Immutable.WavHeader.SetChannels (value));
                AssertProp(() => x.Immutable.WavHeader.WithChannels(value));
                AssertProp(() => Channels    (x.Immutable.WavHeader, value));
                AssertProp(() => SetChannels (x.Immutable.WavHeader, value));
                AssertProp(() => WithChannels(x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.Channels    (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.SetChannels (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.WithChannels(x.Immutable.WavHeader, value));
                AssertProp(() => value == 1 ? x.Immutable.WavHeader.Mono    () : x.Immutable.WavHeader.Stereo    ());
                AssertProp(() => value == 1 ? x.Immutable.WavHeader.SetMono () : x.Immutable.WavHeader.SetStereo ());
                AssertProp(() => value == 1 ? x.Immutable.WavHeader.WithMono() : x.Immutable.WavHeader.WithStereo());
                AssertProp(() => value == 1 ? Mono    (x.Immutable.WavHeader) : Stereo    (x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? SetMono (x.Immutable.WavHeader) : SetStereo (x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? WithMono(x.Immutable.WavHeader) : WithStereo(x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? ConfigWishes.Mono    (x.Immutable.WavHeader) : ConfigWishes.Stereo    (x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? ConfigWishes.SetMono (x.Immutable.WavHeader) : ConfigWishes.SetStereo (x.Immutable.WavHeader));
                AssertProp(() => value == 1 ? ConfigWishes.WithMono(x.Immutable.WavHeader) : ConfigWishes.WithStereo(x.Immutable.WavHeader));
            }

            // SpeakerSetupEnum
            
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
                AssertProp(() => x.Immutable.SpeakerSetupEnum.SetChannels (value));
                AssertProp(() => x.Immutable.SpeakerSetupEnum.WithChannels(value));
                AssertProp(() => value.ChannelsToEnum());
                AssertProp(() => Channels    (x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => SetChannels (x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => WithChannels(x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => ChannelsToEnum(value));
                AssertProp(() => ConfigWishes.Channels    (x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => ConfigWishes.SetChannels (x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => ConfigWishes.WithChannels(x.Immutable.SpeakerSetupEnum, value));
                AssertProp(() => ConfigWishes.ChannelsToEnum(value));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.Mono    () : x.Immutable.SpeakerSetupEnum.Stereo    ());
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.SetMono () : x.Immutable.SpeakerSetupEnum.SetStereo ());
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.WithMono() : x.Immutable.SpeakerSetupEnum.WithStereo());
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.Mono    () : Stereo    (x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.SetMono () : SetStereo (x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.WithMono() : WithStereo(x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.Mono    () : ConfigWishes.Stereo    (x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.SetMono () : ConfigWishes.SetStereo (x.Immutable.SpeakerSetupEnum));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.WithMono() : ConfigWishes.WithStereo(x.Immutable.SpeakerSetupEnum));
            }

            // SpeakerSetup Entity
            
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
                AssertProp(() => x.Immutable.SpeakerSetup.SetChannels (value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SpeakerSetup.WithChannels(value, x.SynthBound.Context));
                AssertProp(() => value.ChannelsToEntity(x.SynthBound.Context));
                AssertProp(() => Channels    (x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => SetChannels (x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => WithChannels(x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => ChannelsToEntity(value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.Channels    (x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.SetChannels (x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.WithChannels(x.Immutable.SpeakerSetup, value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.ChannelsToEntity(value, x.SynthBound.Context));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetup.Mono    (x.SynthBound.Context) : x.Immutable.SpeakerSetup.Stereo    (x.SynthBound.Context));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetup.SetMono (x.SynthBound.Context) : x.Immutable.SpeakerSetup.SetStereo (x.SynthBound.Context));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetup.WithMono(x.SynthBound.Context) : x.Immutable.SpeakerSetup.WithStereo(x.SynthBound.Context));
                AssertProp(() => value == 1 ? Mono    (x.Immutable.SpeakerSetup, x.SynthBound.Context) : Stereo    (x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? SetMono (x.Immutable.SpeakerSetup, x.SynthBound.Context) : SetStereo (x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? WithMono(x.Immutable.SpeakerSetup, x.SynthBound.Context) : WithStereo(x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? ConfigWishes.Mono    (x.Immutable.SpeakerSetup, x.SynthBound.Context) : Stereo    (x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? ConfigWishes.SetMono (x.Immutable.SpeakerSetup, x.SynthBound.Context) : SetStereo (x.Immutable.SpeakerSetup, x.SynthBound.Context));
                AssertProp(() => value == 1 ? ConfigWishes.WithMono(x.Immutable.SpeakerSetup, x.SynthBound.Context) : WithStereo(x.Immutable.SpeakerSetup, x.SynthBound.Context));
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
            AreEqual(DefaultChannels,      () => configSection.Channels());
            AreEqual(DefaultChannels == 1, () => configSection.IsMono  ());
            AreEqual(DefaultChannels == 2, () => configSection.IsStereo());
            AreEqual(DefaultChannels,      () => Channels(configSection));
            AreEqual(DefaultChannels == 1, () => IsMono  (configSection));
            AreEqual(DefaultChannels == 2, () => IsStereo(configSection));
            AreEqual(DefaultChannels,      () => ConfigWishesAccessor.Channels(configSection));
            AreEqual(DefaultChannels == 1, () => ConfigWishesAccessor.IsMono  (configSection));
            AreEqual(DefaultChannels == 2, () => ConfigWishesAccessor.IsStereo(configSection));
        }

        // Getter Helpers

        private void Assert_All_Getters(ConfigTestEntities x, int channels)
        {
            Assert_Bound_Getters      (x, channels);
            Assert_Independent_Getters(x, channels);
            Assert_Immutable_Getters  (x, channels);
        }

        private void Assert_Bound_Getters(ConfigTestEntities x, int channels)
        {
            Assert_SynthBound_Getters(x, channels);
            Assert_TapeBound_Getters (x, channels);
            Assert_BuffBound_Getters (x, channels);
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, int channels)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample,        channels);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, channels);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, channels);
        }

        private void Assert_Immutable_Getters(ConfigTestEntities x, int channels)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader,        channels);
            Assert_Immutable_Getters(x.Immutable.SpeakerSetupEnum, channels);
            Assert_Immutable_Getters(x.Immutable.SpeakerSetup,     channels);
        }

        private void Assert_SynthBound_Getters(ConfigTestEntities x, int channels)
        {
            AreEqual(channels,      () => x.SynthBound.SynthWishes   .GetChannels);
            AreEqual(channels,      () => x.SynthBound.FlowNode      .GetChannels);
            AreEqual(channels,      () => x.SynthBound.ConfigResolver.GetChannels);
            AreEqual(channels == 1, () => x.SynthBound.SynthWishes   .IsMono     );
            AreEqual(channels == 1, () => x.SynthBound.FlowNode      .IsMono     );
            AreEqual(channels == 1, () => x.SynthBound.ConfigResolver.IsMono     );
            AreEqual(channels == 2, () => x.SynthBound.SynthWishes   .IsStereo   );
            AreEqual(channels == 2, () => x.SynthBound.FlowNode      .IsStereo   );
            AreEqual(channels == 2, () => x.SynthBound.ConfigResolver.IsStereo   );
            AreEqual(channels,      () => x.SynthBound.SynthWishes   .Channels());
            AreEqual(channels,      () => x.SynthBound.FlowNode      .Channels());
            AreEqual(channels,      () => x.SynthBound.ConfigResolver.Channels());
            AreEqual(channels == 1, () => x.SynthBound.SynthWishes   .IsMono  ());
            AreEqual(channels == 1, () => x.SynthBound.FlowNode      .IsMono  ());
            AreEqual(channels == 1, () => x.SynthBound.ConfigResolver.IsMono  ());
            AreEqual(channels == 2, () => x.SynthBound.SynthWishes   .IsStereo());
            AreEqual(channels == 2, () => x.SynthBound.FlowNode      .IsStereo());
            AreEqual(channels == 2, () => x.SynthBound.ConfigResolver.IsStereo());
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, int channels)
        {
            AreEqual(channels,      () => x.TapeBound.TapeConfig .Channels);
            AreEqual(channels,      () => x.TapeBound.Tape       .Channels());
            AreEqual(channels,      () => x.TapeBound.TapeConfig .Channels());
            AreEqual(channels,      () => x.TapeBound.TapeActions.Channels());
            AreEqual(channels,      () => x.TapeBound.TapeAction .Channels());
            AreEqual(channels == 1, () => x.TapeBound.Tape       .IsMono  ());
            AreEqual(channels == 1, () => x.TapeBound.TapeConfig .IsMono  ());
            AreEqual(channels == 1, () => x.TapeBound.TapeActions.IsMono  ());
            AreEqual(channels == 1, () => x.TapeBound.TapeAction .IsMono  ());
            AreEqual(channels == 2, () => x.TapeBound.Tape       .IsStereo());
            AreEqual(channels == 2, () => x.TapeBound.TapeConfig .IsStereo());
            AreEqual(channels == 2, () => x.TapeBound.TapeActions.IsStereo());
            AreEqual(channels == 2, () => x.TapeBound.TapeAction .IsStereo());
        }
        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, int channels)
        {
            AreEqual(channels,      () => x.BuffBound.Buff           .Channels());
            AreEqual(channels,      () => x.BuffBound.AudioFileOutput.Channels());
            AreEqual(channels == 1, () => x.BuffBound.Buff           .IsMono  ());
            AreEqual(channels == 1, () => x.BuffBound.AudioFileOutput.IsMono  ());
            AreEqual(channels == 2, () => x.BuffBound.Buff           .IsStereo());
            AreEqual(channels == 2, () => x.BuffBound.AudioFileOutput.IsStereo());
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int channels)
        {
            AreEqual(channels,      () => audioFileInfo.Channels());
            AreEqual(channels == 1, () => audioFileInfo.IsMono  ());
            AreEqual(channels == 2, () => audioFileInfo.IsStereo());
        }
        
        private void Assert_Independent_Getters(Sample sample, int channels)
        {
            AreEqual(channels,      () => sample.Channels());
            AreEqual(channels == 1, () => sample.IsMono  ());
            AreEqual(channels == 2, () => sample.IsStereo());
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int channels)
        {
            AreEqual(channels,      () => audioInfoWish.Channels);
            AreEqual(channels,      () => audioInfoWish.Channels());
            AreEqual(channels == 1, () => audioInfoWish.IsMono  ());
            AreEqual(channels == 2, () => audioInfoWish.IsStereo());
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int channels)
        {
            AreEqual(channels,      () => wavHeader.ChannelCount);
            AreEqual(channels,      () => wavHeader.Channels());
            AreEqual(channels == 1, () => wavHeader.IsMono  ());
            AreEqual(channels == 2, () => wavHeader.IsStereo());
        }
        
        private void Assert_Immutable_Getters(SpeakerSetupEnum speakerSetupEnum, int channels)
        {
            AreEqual(channels,      () => speakerSetupEnum.Channels      ());
            AreEqual(channels,      () => speakerSetupEnum.GetChannels   ());
            AreEqual(channels,      () => speakerSetupEnum.ToChannels    ());
            AreEqual(channels,      () => speakerSetupEnum.EnumToChannels());
            AreEqual(channels == 1, () => speakerSetupEnum.IsMono        ());
            AreEqual(channels == 2, () => speakerSetupEnum.IsStereo      ());
        }
                
        private void Assert_Immutable_Getters(SpeakerSetup speakerSetup, int channels)
        {
            if (speakerSetup == null) throw new NullException(() => speakerSetup);
            AreEqual(channels,      () => speakerSetup.Channels        ());
            AreEqual(channels,      () => speakerSetup.GetChannels     ());
            AreEqual(channels,      () => speakerSetup.ToChannels      ());
            AreEqual(channels,      () => speakerSetup.EntityToChannels());
            AreEqual(channels == 1, () => speakerSetup.IsMono          ());
            AreEqual(channels == 2, () => speakerSetup.IsStereo        ());
        }
        
        // Test Data Helpers
        
        private ConfigTestEntities CreateTestEntities(int? channels) => new ConfigTestEntities(x => x.WithChannels(channels));

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