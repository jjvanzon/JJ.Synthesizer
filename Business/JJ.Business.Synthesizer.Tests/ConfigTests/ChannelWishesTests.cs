using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0618 
#pragma warning disable MSTEST0018 
#pragma warning disable IDE0002

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class ChannelWishesTests
    {
        private static int? _ = null;

        [TestMethod]
        [DynamicData(nameof(CaseKeysInit))]
        public void Init_Channel(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            var  nully    = testCase.init.nully;
            var  coalesce = testCase.val.coalesce;
            var  x        = CreateTestEntities(nully);
            Assert_All_Getters(x, coalesce);
        }
        
        [TestMethod]
        [DynamicData(nameof(CaseKeysWithEmpties))]
        public void SynthBound_Channel(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            Values init = testCase.init;
            Values val  = testCase.val;

            void AssertProp(Action<SynthBoundEntities> setter)
            {
                var x = CreateTestEntities(init.nully);
                Assert_All_Getters(x, init.coalesce);
                
                setter(x.SynthBound);
                
                Assert_SynthBound_Getters(x, val.coalesce);
                Assert_TapeBound_Getters_Complete(x, init.coalesce);
                Assert_BuffBound_Getters(x, init.coalesce);
                Assert_Immutable_Getters(x, init.coalesce);
                
                x.Record();
                Assert_All_Getters(x, val.coalesce);
            }
            
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .Channel    (val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .Channel    (val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.Channel    (val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .WithChannel(val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .WithChannel(val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.WithChannel(val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .AsChannel  (val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .AsChannel  (val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.AsChannel  (val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetChannel (val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetChannel (val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetChannel (val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    Channel    (x.SynthWishes   , val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       Channel    (x.FlowNode      , val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, Channel    (x.ConfigResolver, val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    WithChannel(x.SynthWishes   , val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       WithChannel(x.FlowNode      , val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, WithChannel(x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    AsChannel  (x.SynthWishes   , val.channel.nully).SetChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       AsChannel  (x.FlowNode      , val.channel.nully).SetChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, AsChannel  (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    SetChannel (x.SynthWishes   , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       SetChannel (x.FlowNode      , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, SetChannel (x.ConfigResolver, val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .Channel    (x.SynthWishes   , val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .Channel    (x.FlowNode      , val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.Channel    (x.ConfigResolver, val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .WithChannel(x.SynthWishes   , val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .WithChannel(x.FlowNode      , val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.WithChannel(x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .AsChannel  (x.SynthWishes   , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .AsChannel  (x.FlowNode      , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.AsChannel  (x.ConfigResolver, val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .SetChannel (x.SynthWishes   , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .SetChannel (x.FlowNode      , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.SetChannel (x.ConfigResolver, val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    ChannelExtensionWishes        .WithChannel(x.SynthWishes   , val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       ChannelExtensionWishes        .WithChannel(x.FlowNode      , val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, ChannelExtensionWishesAccessor.WithChannel(x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .Channels    (val.channels.nully).Channel    (val.channel.nully))); // Switched Channel and ChannelS calls
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .Channels    (val.channels.nully).Channel    (val.channel.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.Channels    (val.channels.nully).Channel    (val.channel.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .WithChannels(val.channels.nully).WithChannel(val.channel.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .WithChannels(val.channels.nully).WithChannel(val.channel.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.WithChannels(val.channels.nully).WithChannel(val.channel.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetChannels (val.channels.nully).AsChannel  (val.channel.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetChannels (val.channels.nully).AsChannel  (val.channel.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetChannels (val.channels.nully).AsChannel  (val.channel.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetChannels (val.channels.nully).SetChannel (val.channel.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetChannels (val.channels.nully).SetChannel (val.channel.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetChannels (val.channels.nully).SetChannel (val.channel.nully)));
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Center       ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Left         ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Right        ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .NoChannel    ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .Channel      (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .Center       ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .Left         ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .Right        ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .NoChannel    ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .Channel      (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Center       ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Left         ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Right        ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.NoChannel    ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.Channel      (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithCenter   ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithLeft     ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithRight    ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithNoChannel()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithChannel  (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithCenter   ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithLeft     ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithRight    ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithNoChannel()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .WithChannel  (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithCenter   ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithLeft     ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithRight    ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithNoChannel()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithChannel  (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsCenter     ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsLeft       ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsRight      ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsNoChannel  ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsChannel    (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsCenter     ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsLeft       ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsRight      ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsNoChannel  ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .AsChannel    (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsCenter     ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsLeft       ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsRight      ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsNoChannel  ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsChannel    (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetCenter    ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetLeft      ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetRight     ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetNoChannel ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetChannel   (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetCenter    ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetLeft      ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetRight     ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetNoChannel ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .SetChannel   (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetCenter    ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetLeft      ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetRight     ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetNoChannel ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetChannel   (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => Center       (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => Left         (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => Right        (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => NoChannel    (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => Channel      (x.SynthWishes,    val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => Center       (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => Left         (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => Right        (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => NoChannel    (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => Channel      (x.FlowNode,       val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => Center       (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => Left         (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => Right        (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => NoChannel    (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => Channel      (x.ConfigResolver, val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => WithCenter   (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => WithLeft     (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => WithRight    (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => WithNoChannel(x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => WithChannel  (x.SynthWishes,    val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => WithCenter   (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => WithLeft     (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => WithRight    (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => WithNoChannel(x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => WithChannel  (x.FlowNode,       val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => WithCenter   (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => WithLeft     (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => WithRight    (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => WithNoChannel(x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => WithChannel  (x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => AsCenter     (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => AsLeft       (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => AsRight      (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => AsNoChannel  (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => AsChannel    (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => AsCenter     (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => AsLeft       (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => AsRight      (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => AsNoChannel  (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => AsChannel    (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => AsCenter     (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => AsLeft       (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => AsRight      (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => AsNoChannel  (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => AsChannel    (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => SetCenter    (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => SetLeft      (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => SetRight     (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => SetNoChannel (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => SetChannel   (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => SetCenter    (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => SetLeft      (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => SetRight     (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => SetNoChannel (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => SetChannel   (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => SetCenter    (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => SetLeft      (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => SetRight     (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => SetNoChannel (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => SetChannel   (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Center       (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Left         (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Right        (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .NoChannel    (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .Channel      (x.SynthWishes,    val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .Center       (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .Left         (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .Right        (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .NoChannel    (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .Channel      (x.FlowNode,       val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Center       (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Left         (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Right        (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.NoChannel    (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Channel      (x.ConfigResolver, val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithCenter   (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithLeft     (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithRight    (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithNoChannel(x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .WithChannel  (x.SynthWishes,    val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithCenter   (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithLeft     (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithRight    (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithNoChannel(x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .WithChannel  (x.FlowNode,       val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithCenter   (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithLeft     (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithRight    (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithNoChannel(x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithChannel  (x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsCenter     (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsLeft       (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsRight      (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsNoChannel  (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .AsChannel    (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsCenter     (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsLeft       (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsRight      (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsNoChannel  (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .AsChannel    (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsCenter     (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsLeft       (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsRight      (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsNoChannel  (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsChannel    (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetCenter    (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetLeft      (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetRight     (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetNoChannel (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .SetChannel   (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetCenter    (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetLeft      (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetRight     (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetNoChannel (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .SetChannel   (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetCenter    (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetLeft      (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetRight     (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetNoChannel (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetChannel   (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithCenter   (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithLeft     (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithRight    (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithNoChannel(x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithChannel  (x.SynthWishes,    val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithCenter   (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithLeft     (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithRight    (x.FlowNode        ));
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithNoChannel(x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithChannel  (x.FlowNode,       val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithCenter   (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithLeft     (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithRight    (x.ConfigResolver  ));
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithNoChannel(x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithChannel  (x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)); });
        }
        
        [TestMethod]
        [DynamicData(nameof(CaseKeys))]
        public void TapeBound_Channel(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            var init = testCase.init.coalesce;
            var val  = testCase.val.coalesce;

            void AssertProp(Action<TapeBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x.TapeBound);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters_SingleTape(x, val);
                Assert_BuffBound_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }
            
            AssertProp(x => { x.TapeConfig.Channels = val.channels; x.TapeConfig.Channel = val.channel; });
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .Channel     (val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .Channel     (val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  x.TapeConfig .Channel     (val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, x.TapeActions.Channel     (val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  x.TapeAction .Channel     (val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .WithChannel (val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .WithChannel (val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  x.TapeConfig .WithChannel (val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, x.TapeActions.WithChannel (val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  x.TapeAction .WithChannel (val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .AsChannel   (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .AsChannel   (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  x.TapeConfig .AsChannel   (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, x.TapeActions.AsChannel   (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  x.TapeAction .AsChannel   (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .SetChannel  (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .SetChannel  (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  x.TapeConfig .SetChannel  (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, x.TapeActions.SetChannel  (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  x.TapeAction .SetChannel  (val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        Channel    (x.Tape       , val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        Channel    (x.Tape       , val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  Channel    (x.TapeConfig , val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, Channel    (x.TapeActions, val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  Channel    (x.TapeAction , val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        WithChannel(x.Tape       , val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.Tape,        WithChannel(x.Tape       , val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  WithChannel(x.TapeConfig , val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, WithChannel(x.TapeActions, val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  WithChannel(x.TapeAction , val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.Tape,        AsChannel  (x.Tape       , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        AsChannel  (x.Tape       , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  AsChannel  (x.TapeConfig , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, AsChannel  (x.TapeActions, val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  AsChannel  (x.TapeAction , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        SetChannel (x.Tape       , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        SetChannel (x.Tape       , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  SetChannel (x.TapeConfig , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, SetChannel (x.TapeActions, val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  SetChannel (x.TapeAction , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        ConfigWishes.Channel    (x.Tape       , val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        ConfigWishes.Channel    (x.Tape       , val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  ConfigWishes.Channel    (x.TapeConfig , val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, ConfigWishes.Channel    (x.TapeActions, val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  ConfigWishes.Channel    (x.TapeAction , val.channel).Channels    (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        ConfigWishes.WithChannel(x.Tape       , val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.Tape,        ConfigWishes.WithChannel(x.Tape       , val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  ConfigWishes.WithChannel(x.TapeConfig , val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, ConfigWishes.WithChannel(x.TapeActions, val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  ConfigWishes.WithChannel(x.TapeAction , val.channel).WithChannels(val.channels)));
            AssertProp(x => AreEqual(x.Tape,        ConfigWishes.AsChannel  (x.Tape       , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        ConfigWishes.AsChannel  (x.Tape       , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  ConfigWishes.AsChannel  (x.TapeConfig , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, ConfigWishes.AsChannel  (x.TapeActions, val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  ConfigWishes.AsChannel  (x.TapeAction , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        ConfigWishes.SetChannel (x.Tape       , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        ConfigWishes.SetChannel (x.Tape       , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeConfig,  ConfigWishes.SetChannel (x.TapeConfig , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeActions, ConfigWishes.SetChannel (x.TapeActions, val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.TapeAction,  ConfigWishes.SetChannel (x.TapeAction , val.channel).SetChannels (val.channels)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .Channels    (val.channels).Channel    (val.channel))); // Switched Channel and ChannelS calls
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .Channels    (val.channels).Channel    (val.channel)));
            AssertProp(x => AreEqual(x.TapeConfig,  x.TapeConfig .Channels    (val.channels).Channel    (val.channel)));
            AssertProp(x => AreEqual(x.TapeActions, x.TapeActions.Channels    (val.channels).Channel    (val.channel)));
            AssertProp(x => AreEqual(x.TapeAction,  x.TapeAction .Channels    (val.channels).Channel    (val.channel)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .WithChannels(val.channels).WithChannel(val.channel)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .WithChannels(val.channels).WithChannel(val.channel)));
            AssertProp(x => AreEqual(x.TapeConfig,  x.TapeConfig .WithChannels(val.channels).WithChannel(val.channel)));
            AssertProp(x => AreEqual(x.TapeActions, x.TapeActions.WithChannels(val.channels).WithChannel(val.channel)));
            AssertProp(x => AreEqual(x.TapeAction,  x.TapeAction .WithChannels(val.channels).WithChannel(val.channel)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .SetChannels (val.channels).SetChannel (val.channel)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .SetChannels (val.channels).SetChannel (val.channel)));
            AssertProp(x => AreEqual(x.TapeConfig,  x.TapeConfig .SetChannels (val.channels).SetChannel (val.channel)));
            AssertProp(x => AreEqual(x.TapeActions, x.TapeActions.SetChannels (val.channels).SetChannel (val.channel)));
            AssertProp(x => AreEqual(x.TapeAction,  x.TapeAction .SetChannels (val.channels).SetChannel (val.channel)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .SetChannels (val.channels).AsChannel  (val.channel)));
            AssertProp(x => AreEqual(x.Tape,        x.Tape       .SetChannels (val.channels).AsChannel  (val.channel)));
            AssertProp(x => AreEqual(x.TapeConfig,  x.TapeConfig .SetChannels (val.channels).AsChannel  (val.channel)));
            AssertProp(x => AreEqual(x.TapeActions, x.TapeActions.SetChannels (val.channels).AsChannel  (val.channel)));
            AssertProp(x => AreEqual(x.TapeAction,  x.TapeAction .SetChannels (val.channels).AsChannel  (val.channel)));
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .Center       ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .Left         ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .Right        ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .NoChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Center       ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Left         ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Right        ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .NoChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.Center       ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.Left         ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.Right        ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.NoChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .Center       ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .Left         ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .Right        ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .NoChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .WithCenter   ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .WithLeft     ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .WithRight    ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .WithNoChannel()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithCenter   ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithLeft     ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithRight    ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithNoChannel()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.WithCenter   ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.WithLeft     ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.WithRight    ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.WithNoChannel()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .WithCenter   ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .WithLeft     ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .WithRight    ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .WithNoChannel()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .AsCenter     ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .AsLeft       ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .AsRight      ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .AsNoChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsCenter     ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsLeft       ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsRight      ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsNoChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.AsCenter     ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.AsLeft       ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.AsRight      ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.AsNoChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .AsCenter     ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .AsLeft       ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .AsRight      ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .AsNoChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .SetCenter    ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .SetLeft      ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .SetRight     ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .SetNoChannel ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetCenter    ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetLeft      ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetRight     ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetNoChannel ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.SetCenter    ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.SetLeft      ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.SetRight     ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.SetNoChannel ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .SetCenter    ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .SetLeft      ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .SetRight     ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .SetNoChannel ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => Center       (x.Tape       ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => Left         (x.Tape       ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => Right        (x.Tape       ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => NoChannel    (x.Tape       )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => Center       (x.TapeConfig ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => Left         (x.TapeConfig ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => Right        (x.TapeConfig ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => NoChannel    (x.TapeConfig )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => Center       (x.TapeActions));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => Left         (x.TapeActions));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => Right        (x.TapeActions));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => NoChannel    (x.TapeActions)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => Center       (x.TapeAction ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => Left         (x.TapeAction ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => Right        (x.TapeAction ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => NoChannel    (x.TapeAction )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => WithCenter   (x.Tape       ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => WithLeft     (x.Tape       ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => WithRight    (x.Tape       ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => WithNoChannel(x.Tape       )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => WithCenter   (x.TapeConfig ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => WithLeft     (x.TapeConfig ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => WithRight    (x.TapeConfig ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => WithNoChannel(x.TapeConfig )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => WithCenter   (x.TapeActions));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => WithLeft     (x.TapeActions));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => WithRight    (x.TapeActions));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => WithNoChannel(x.TapeActions)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => WithCenter   (x.TapeAction ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => WithLeft     (x.TapeAction ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => WithRight    (x.TapeAction ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => WithNoChannel(x.TapeAction )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => AsCenter     (x.Tape       ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => AsLeft       (x.Tape       ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => AsRight      (x.Tape       ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => AsNoChannel  (x.Tape       )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => AsCenter     (x.TapeConfig ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => AsLeft       (x.TapeConfig ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => AsRight      (x.TapeConfig ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => AsNoChannel  (x.TapeConfig )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => AsCenter     (x.TapeActions));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => AsLeft       (x.TapeActions));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => AsRight      (x.TapeActions));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => AsNoChannel  (x.TapeActions)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => AsCenter     (x.TapeAction ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => AsLeft       (x.TapeAction ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => AsRight      (x.TapeAction ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => AsNoChannel  (x.TapeAction )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => SetCenter    (x.Tape       ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => SetLeft      (x.Tape       ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => SetRight     (x.Tape       ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => SetNoChannel (x.Tape       )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => SetCenter    (x.TapeConfig ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => SetLeft      (x.TapeConfig ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => SetRight     (x.TapeConfig ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => SetNoChannel (x.TapeConfig )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => SetCenter    (x.TapeActions));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => SetLeft      (x.TapeActions));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => SetRight     (x.TapeActions));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => SetNoChannel (x.TapeActions)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => SetCenter    (x.TapeAction ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => SetLeft      (x.TapeAction ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => SetRight     (x.TapeAction ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => SetNoChannel (x.TapeAction )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.Center       (x.Tape       ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.Left         (x.Tape       ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.Right        (x.Tape       ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.NoChannel    (x.Tape       )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.Center       (x.TapeConfig ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.Left         (x.TapeConfig ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.Right        (x.TapeConfig ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.NoChannel    (x.TapeConfig )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.Center       (x.TapeActions));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.Left         (x.TapeActions));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.Right        (x.TapeActions));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.NoChannel    (x.TapeActions)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.Center       (x.TapeAction ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.Left         (x.TapeAction ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.Right        (x.TapeAction ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.NoChannel    (x.TapeAction )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.WithCenter   (x.Tape       ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.WithLeft     (x.Tape       ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.WithRight    (x.Tape       ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.WithNoChannel(x.Tape       )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithCenter   (x.TapeConfig ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithLeft     (x.TapeConfig ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithRight    (x.TapeConfig ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithNoChannel(x.TapeConfig )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.WithCenter   (x.TapeActions));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.WithLeft     (x.TapeActions));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.WithRight    (x.TapeActions));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.WithNoChannel(x.TapeActions)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.WithCenter   (x.TapeAction ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.WithLeft     (x.TapeAction ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.WithRight    (x.TapeAction ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.WithNoChannel(x.TapeAction )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.AsCenter     (x.Tape       ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.AsLeft       (x.Tape       ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.AsRight      (x.Tape       ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.AsNoChannel  (x.Tape       )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsCenter     (x.TapeConfig ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsLeft       (x.TapeConfig ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsRight      (x.TapeConfig ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsNoChannel  (x.TapeConfig )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.AsCenter     (x.TapeActions));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.AsLeft       (x.TapeActions));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.AsRight      (x.TapeActions));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.AsNoChannel  (x.TapeActions)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.AsCenter     (x.TapeAction ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.AsLeft       (x.TapeAction ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.AsRight      (x.TapeAction ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.AsNoChannel  (x.TapeAction )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.SetCenter    (x.Tape       ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.SetLeft      (x.Tape       ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.SetRight     (x.Tape       ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.SetNoChannel (x.Tape       )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetCenter    (x.TapeConfig ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetLeft      (x.TapeConfig ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetRight     (x.TapeConfig ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetNoChannel (x.TapeConfig )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.SetCenter    (x.TapeActions));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.SetLeft      (x.TapeActions));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.SetRight     (x.TapeActions));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.SetNoChannel (x.TapeActions)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.SetCenter    (x.TapeAction ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.SetLeft      (x.TapeAction ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.SetRight     (x.TapeAction ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.SetNoChannel (x.TapeAction )); });
        }
                
        [TestMethod]
        [DynamicData(nameof(CaseKeys))]
        public void BuffBound_Channel(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            var init = testCase.init.coalesce;
            var val  = testCase.val.coalesce;
            IContext context = null;

            void AssertProp(Action<BuffBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                context = x.SynthBound.Context;
                
                setter(x.BuffBound);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters_Complete(x, init);
                Assert_BuffBound_Getters(x, val);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init);
            }
                        
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .Channel     (val.channel, context).Channels    (val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Channel     (val.channel, context).Channels    (val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .WithChannel (val.channel, context).WithChannels(val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithChannel (val.channel, context).WithChannels(val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .AsChannel   (val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsChannel   (val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .SetChannel  (val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetChannel  (val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => Channel    (x.Buff,            val.channel, context).Channels    (val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => Channel    (x.AudioFileOutput, val.channel, context).Channels    (val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => WithChannel(x.Buff,            val.channel, context).WithChannels(val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => WithChannel(x.AudioFileOutput, val.channel, context).WithChannels(val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => AsChannel  (x.Buff,            val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => AsChannel  (x.AudioFileOutput, val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => SetChannel (x.Buff,            val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => SetChannel (x.AudioFileOutput, val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.Channel    (x.Buff,            val.channel, context).Channels    (val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.Channel    (x.AudioFileOutput, val.channel, context).Channels    (val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.WithChannel(x.Buff,            val.channel, context).WithChannels(val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.WithChannel(x.AudioFileOutput, val.channel, context).WithChannels(val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.AsChannel  (x.Buff,            val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.AsChannel  (x.AudioFileOutput, val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.SetChannel (x.Buff,            val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.SetChannel (x.AudioFileOutput, val.channel, context).SetChannels (val.channels, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .Channels    (val.channels, context).Channel    (val.channel, context))); // Switched Channel and ChannelS calls
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Channels    (val.channels, context).Channel    (val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .WithChannels(val.channels, context).WithChannel(val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithChannels(val.channels, context).WithChannel(val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .SetChannels (val.channels, context).AsChannel  (val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetChannels (val.channels, context).AsChannel  (val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .SetChannels (val.channels, context).SetChannel (val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetChannels (val.channels, context).SetChannel (val.channel, context)));
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .Center       (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .Left         (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .Right        (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .NoChannel    (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Center       (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Left         (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Right        (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.NoChannel    (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .WithCenter   (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .WithLeft     (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .WithRight    (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .WithNoChannel(context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithCenter   (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithLeft     (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithRight    (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithNoChannel(context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .AsCenter     (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .AsLeft       (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .AsRight      (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .AsNoChannel  (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsCenter     (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsLeft       (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsRight      (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsNoChannel  (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .SetCenter    (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .SetLeft      (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .SetRight     (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .SetNoChannel (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetCenter    (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetLeft      (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetRight     (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetNoChannel (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => Center       (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => Left         (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => Right        (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => NoChannel    (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => Center       (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => Left         (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => Right        (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => NoChannel    (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => WithCenter   (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => WithLeft     (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => WithRight    (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => WithNoChannel(x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => WithCenter   (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => WithLeft     (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => WithRight    (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => WithNoChannel(x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => AsCenter     (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => AsLeft       (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => AsRight      (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => AsNoChannel  (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => AsCenter     (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => AsLeft       (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => AsRight      (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => AsNoChannel  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => SetCenter    (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => SetLeft      (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => SetRight     (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => SetNoChannel (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => SetCenter    (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => SetLeft      (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => SetRight     (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => SetNoChannel (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.Center       (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.Left         (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.Right        (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.NoChannel    (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Center       (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Left         (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Right        (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.NoChannel    (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.WithCenter   (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.WithLeft     (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.WithRight    (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.WithNoChannel(x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithCenter   (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithLeft     (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithRight    (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithNoChannel(x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.AsCenter     (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.AsLeft       (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.AsRight      (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.AsNoChannel  (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsCenter     (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsLeft       (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsRight      (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsNoChannel  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.SetCenter    (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.SetLeft      (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.SetRight     (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.SetNoChannel (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetCenter    (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetLeft      (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetRight     (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetNoChannel (x.AudioFileOutput, context)); });
        }
        
        [TestMethod]
        [DynamicData(nameof(CaseKeys))]
        public void Immutables_Channel(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            var init = testCase.init.coalesce;
            var val  = testCase.val.coalesce;
            var x = CreateTestEntities(init);
            
            // ChannelEnum
            
            var channelEnums = new List<ChannelEnum>();
            {
                ChannelEnum channelEnum = default;
                
                void AssertProp(Func<ChannelEnum> setter)
                {
                    channelEnum = x.Immutable.ChannelEnum;
                        
                    Assert_Immutable_Getters(channelEnum, init);
                    
                    ChannelEnum channelEnum2 = setter();
                    
                    Assert_Immutable_Getters(channelEnum, init);
                    Assert_Immutable_Getters(channelEnum2, val);
                    
                    channelEnums.Add(channelEnum2);
                }

                AssertProp(() => val.channel .ChannelToEnum(val.channels             ));
                AssertProp(() =>              ChannelToEnum(val.channel, val.channels));
                AssertProp(() => ConfigWishes.ChannelToEnum(val.channel, val.channels));
                AssertProp(() => channelEnum .Channel                 (val.channel).SetChannels(val.channels).Channel    (val.channel));
                AssertProp(() => channelEnum .WithChannel             (val.channel).SetChannels(val.channels).WithChannel(val.channel));
                AssertProp(() => channelEnum .ToChannel               (val.channel).SetChannels(val.channels).ToChannel  (val.channel));
                AssertProp(() => channelEnum .AsChannel               (val.channel).SetChannels(val.channels).AsChannel  (val.channel));
                AssertProp(() => channelEnum .SetChannel              (val.channel).SetChannels(val.channels).SetChannel (val.channel));
                AssertProp(() =>              Channel    (channelEnum, val.channel).SetChannels(val.channels).Channel    (val.channel));
                AssertProp(() =>              WithChannel(channelEnum, val.channel).SetChannels(val.channels).WithChannel(val.channel));
                AssertProp(() =>              ToChannel  (channelEnum, val.channel).SetChannels(val.channels).ToChannel  (val.channel));
                AssertProp(() =>              AsChannel  (channelEnum, val.channel).SetChannels(val.channels).AsChannel  (val.channel));
                AssertProp(() =>              SetChannel (channelEnum, val.channel).SetChannels(val.channels).SetChannel (val.channel));
                AssertProp(() => ConfigWishes.Channel    (channelEnum, val.channel).SetChannels(val.channels).Channel    (val.channel));
                AssertProp(() => ConfigWishes.WithChannel(channelEnum, val.channel).SetChannels(val.channels).WithChannel(val.channel));
                AssertProp(() => ConfigWishes.ToChannel  (channelEnum, val.channel).SetChannels(val.channels).ToChannel  (val.channel));
                AssertProp(() => ConfigWishes.AsChannel  (channelEnum, val.channel).SetChannels(val.channels).AsChannel  (val.channel));
                AssertProp(() => ConfigWishes.SetChannel (channelEnum, val.channel).SetChannels(val.channels).SetChannel (val.channel));
                AssertProp(() => channelEnum .SetChannels            (val.channels).SetChannel (val.channel) .SetChannels(val.channels)); // Switched Channel and ChannelS calls
                AssertProp(() => { if (val == (1,0)) return channelEnum.Center        ();
                                   if (val == (2,0)) return channelEnum.Left          ();
                                   if (val == (2,1)) return channelEnum.Right         ();
                                   if (val == (2,_)) return channelEnum.NoChannel     (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.WithCenter    ();
                                   if (val == (2,0)) return channelEnum.WithLeft      ();
                                   if (val == (2,1)) return channelEnum.WithRight     ();
                                   if (val == (2,_)) return channelEnum.WithNoChannel (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.AsCenter      ();
                                   if (val == (2,0)) return channelEnum.AsLeft        ();
                                   if (val == (2,1)) return channelEnum.AsRight       ();
                                   if (val == (2,_)) return channelEnum.AsNoChannel   (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.ToCenter      ();
                                   if (val == (2,0)) return channelEnum.ToLeft        ();
                                   if (val == (2,1)) return channelEnum.ToRight       ();
                                   if (val == (2,_)) return channelEnum.ToNoChannel   (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.SetCenter     ();
                                   if (val == (2,0)) return channelEnum.SetLeft       ();
                                   if (val == (2,1)) return channelEnum.SetRight      ();
                                   if (val == (2,_)) return channelEnum.SetNoChannel  (); return default; });
                AssertProp(() => { if (val == (1,0)) return Center        (channelEnum);
                                   if (val == (2,0)) return Left          (channelEnum);
                                   if (val == (2,1)) return Right         (channelEnum);
                                   if (val == (2,_)) return NoChannel     (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return WithCenter    (channelEnum);
                                   if (val == (2,0)) return WithLeft      (channelEnum);
                                   if (val == (2,1)) return WithRight     (channelEnum);
                                   if (val == (2,_)) return WithNoChannel (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return AsCenter      (channelEnum);
                                   if (val == (2,0)) return AsLeft        (channelEnum);
                                   if (val == (2,1)) return AsRight       (channelEnum);
                                   if (val == (2,_)) return AsNoChannel   (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ToCenter      (channelEnum);
                                   if (val == (2,0)) return ToLeft        (channelEnum);
                                   if (val == (2,1)) return ToRight       (channelEnum);
                                   if (val == (2,_)) return ToNoChannel   (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return SetCenter     (channelEnum);
                                   if (val == (2,0)) return SetLeft       (channelEnum);
                                   if (val == (2,1)) return SetRight      (channelEnum);
                                   if (val == (2,_)) return SetNoChannel  (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.Center        (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.Left          (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.Right         (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.NoChannel     (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.WithCenter    (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.WithLeft      (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.WithRight     (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.WithNoChannel (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.AsCenter      (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.AsLeft        (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.AsRight       (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.AsNoChannel   (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.ToCenter      (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.ToLeft        (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.ToRight       (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.ToNoChannel   (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.SetCenter     (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.SetLeft       (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.SetRight      (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.SetNoChannel  (channelEnum); return default; });
            }
                        
            // Channel Entity
            
            var channelEntities = new List<Channel>();
            {
                Channel entity = null;
                IContext context = null;
                
                void AssertProp(Func<Channel> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.ChannelEntity, init);
                    
                    entity = x.Immutable.ChannelEntity;
                    context = x.SynthBound.Context;

                    Channel channelEntity2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.ChannelEntity, init);
                    Assert_Immutable_Getters(channelEntity2, val);
                    
                    channelEntities.Add(channelEntity2);
                }

                AssertProp(() => val.channel .ChannelToEntity(             val.channels, context));
                AssertProp(() =>              ChannelToEntity(val.channel, val.channels, context));
                AssertProp(() => ConfigWishes.ChannelToEntity(val.channel, val.channels, context));
                AssertProp(() =>       entity.Channel    (        val.channel, context).SetChannels(val.channels, context).Channel    (val.channel, context));
                AssertProp(() =>       entity.WithChannel(        val.channel, context).SetChannels(val.channels, context).WithChannel(val.channel, context));
                AssertProp(() =>       entity.AsChannel  (        val.channel, context).SetChannels(val.channels, context).AsChannel  (val.channel, context));
                AssertProp(() =>       entity.ToChannel  (        val.channel, context).SetChannels(val.channels, context).ToChannel  (val.channel, context));
                AssertProp(() =>       entity.SetChannel (        val.channel, context).SetChannels(val.channels, context).SetChannel (val.channel, context));
                AssertProp(() =>              Channel    (entity, val.channel, context).SetChannels(val.channels, context).Channel    (val.channel, context));
                AssertProp(() =>              WithChannel(entity, val.channel, context).SetChannels(val.channels, context).WithChannel(val.channel, context));
                AssertProp(() =>              AsChannel  (entity, val.channel, context).SetChannels(val.channels, context).AsChannel  (val.channel, context));
                AssertProp(() =>              ToChannel  (entity, val.channel, context).SetChannels(val.channels, context).ToChannel  (val.channel, context));
                AssertProp(() =>              SetChannel (entity, val.channel, context).SetChannels(val.channels, context).SetChannel (val.channel, context));
                AssertProp(() => ConfigWishes.Channel    (entity, val.channel, context).SetChannels(val.channels, context).Channel    (val.channel, context));
                AssertProp(() => ConfigWishes.WithChannel(entity, val.channel, context).SetChannels(val.channels, context).WithChannel(val.channel, context));
                AssertProp(() => ConfigWishes.AsChannel  (entity, val.channel, context).SetChannels(val.channels, context).AsChannel  (val.channel, context));
                AssertProp(() => ConfigWishes.ToChannel  (entity, val.channel, context).SetChannels(val.channels, context).ToChannel  (val.channel, context));
                AssertProp(() => ConfigWishes.SetChannel (entity, val.channel, context).SetChannels(val.channels, context).SetChannel (val.channel, context));
                AssertProp(() =>       entity.SetChannels(val.channels, context)       .SetChannel (val.channel , context).SetChannels(val.channels, context)); // Switched Channel and ChannelS calls
                AssertProp(() => { if (val == (1,0)) return entity.Center       (context);
                                   if (val == (2,0)) return entity.Left         (context);
                                   if (val == (2,1)) return entity.Right        (context);
                                   if (val == (2,_)) return entity.NoChannel    (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.WithCenter   (context);
                                   if (val == (2,0)) return entity.WithLeft     (context);
                                   if (val == (2,1)) return entity.WithRight    (context);
                                   if (val == (2,_)) return entity.WithNoChannel(       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.AsCenter     (context);
                                   if (val == (2,0)) return entity.AsLeft       (context);
                                   if (val == (2,1)) return entity.AsRight      (context);
                                   if (val == (2,_)) return entity.AsNoChannel  (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.ToCenter     (context);
                                   if (val == (2,0)) return entity.ToLeft       (context);
                                   if (val == (2,1)) return entity.ToRight      (context);
                                   if (val == (2,_)) return entity.ToNoChannel  (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.SetCenter    (context);
                                   if (val == (2,0)) return entity.SetLeft      (context);
                                   if (val == (2,1)) return entity.SetRight     (context);
                                   if (val == (2,_)) return entity.SetNoChannel (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return Center       (entity, context);
                                   if (val == (2,0)) return Left         (entity, context);
                                   if (val == (2,1)) return Right        (entity, context);
                                   if (val == (2,_)) return NoChannel    (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return WithCenter   (entity, context);
                                   if (val == (2,0)) return WithLeft     (entity, context);
                                   if (val == (2,1)) return WithRight    (entity, context);
                                   if (val == (2,_)) return WithNoChannel(entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return AsCenter     (entity, context);
                                   if (val == (2,0)) return AsLeft       (entity, context);
                                   if (val == (2,1)) return AsRight      (entity, context);
                                   if (val == (2,_)) return AsNoChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ToCenter     (entity, context);
                                   if (val == (2,0)) return ToLeft       (entity, context);
                                   if (val == (2,1)) return ToRight      (entity, context);
                                   if (val == (2,_)) return ToNoChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return SetCenter    (entity, context);
                                   if (val == (2,0)) return SetLeft      (entity, context);
                                   if (val == (2,1)) return SetRight     (entity, context);
                                   if (val == (2,_)) return SetNoChannel (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.Center       (entity, context);
                                   if (val == (2,0)) return ConfigWishes.Left         (entity, context);
                                   if (val == (2,1)) return ConfigWishes.Right        (entity, context);
                                   if (val == (2,_)) return ConfigWishes.NoChannel    (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.WithCenter   (entity, context);
                                   if (val == (2,0)) return ConfigWishes.WithLeft     (entity, context);
                                   if (val == (2,1)) return ConfigWishes.WithRight    (entity, context);
                                   if (val == (2,_)) return ConfigWishes.WithNoChannel(entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.AsCenter     (entity, context);
                                   if (val == (2,0)) return ConfigWishes.AsLeft       (entity, context);
                                   if (val == (2,1)) return ConfigWishes.AsRight      (entity, context);
                                   if (val == (2,_)) return ConfigWishes.AsNoChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.ToCenter     (entity, context);
                                   if (val == (2,0)) return ConfigWishes.ToLeft       (entity, context);
                                   if (val == (2,1)) return ConfigWishes.ToRight      (entity, context);
                                   if (val == (2,_)) return ConfigWishes.ToNoChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.SetCenter    (entity, context);
                                   if (val == (2,0)) return ConfigWishes.SetLeft      (entity, context);
                                   if (val == (2,1)) return ConfigWishes.SetRight     (entity, context);
                                   if (val == (2,_)) return ConfigWishes.SetNoChannel (entity         ); return default; });
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            channelEnums.ForEach(e => Assert_Immutable_Getters(e, val));
            channelEntities.ForEach(e => Assert_Immutable_Getters(e, val));
        }
        
        // Getter Helpers

        private void Assert_All_Getters(ConfigTestEntities x, (int, int?) values)
        {
            Assert_SynthBound_Getters(x, values);
            Assert_TapeBound_Getters_Complete(x, values);
            Assert_BuffBound_Getters(x, values);
            Assert_Immutable_Getters(x, values);
        }

        // NOTE: Test ChannelS alongside Channel since shorthand can change both.

        private void Assert_SynthBound_Getters(ConfigTestEntities x, (int channels, int? channel) c)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigResolver);
            AreEqual(c.channel,       () => x.SynthBound.SynthWishes   .GetChannel      );
            AreEqual(c.channel,       () => x.SynthBound.FlowNode      .GetChannel      );
            AreEqual(c.channel,       () => x.SynthBound.ConfigResolver.GetChannel      );
            AreEqual(c.channels,      () => x.SynthBound.SynthWishes   .GetChannels     );
            AreEqual(c.channels,      () => x.SynthBound.FlowNode      .GetChannels     );
            AreEqual(c.channels,      () => x.SynthBound.ConfigResolver.GetChannels     );
            AreEqual(c == (1,0),      () => x.SynthBound.SynthWishes   .IsCenter        );
            AreEqual(c == (1,0),      () => x.SynthBound.FlowNode      .IsCenter        );
            AreEqual(c == (1,0),      () => x.SynthBound.ConfigResolver.IsCenter        );
            AreEqual(c == (2,0),      () => x.SynthBound.SynthWishes   .IsLeft          );
            AreEqual(c == (2,0),      () => x.SynthBound.FlowNode      .IsLeft          );
            AreEqual(c == (2,0),      () => x.SynthBound.ConfigResolver.IsLeft          );
            AreEqual(c == (2,1),      () => x.SynthBound.SynthWishes   .IsRight         );
            AreEqual(c == (2,1),      () => x.SynthBound.FlowNode      .IsRight         );
            AreEqual(c == (2,1),      () => x.SynthBound.ConfigResolver.IsRight         );
            AreEqual(c == (2,_),      () => x.SynthBound.SynthWishes   .IsAnyChannel    );
            AreEqual(c == (2,_),      () => x.SynthBound.FlowNode      .IsAnyChannel    );
            AreEqual(c == (2,_),      () => x.SynthBound.ConfigResolver.IsAnyChannel    );
            AreEqual(c == (2,_),      () => x.SynthBound.SynthWishes   .IsEveryChannel  );
            AreEqual(c == (2,_),      () => x.SynthBound.FlowNode      .IsEveryChannel  );
            AreEqual(c == (2,_),      () => x.SynthBound.ConfigResolver.IsEveryChannel  );
            AreEqual(c == (2,_),      () => x.SynthBound.SynthWishes   .IsNoChannel     );
            AreEqual(c == (2,_),      () => x.SynthBound.FlowNode      .IsNoChannel     );
            AreEqual(c == (2,_),      () => x.SynthBound.ConfigResolver.IsNoChannel     );
            AreEqual(c == (2,_),      () => x.SynthBound.SynthWishes   .IsChannelEmpty  );
            AreEqual(c == (2,_),      () => x.SynthBound.FlowNode      .IsChannelEmpty  );
            AreEqual(c == (2,_),      () => x.SynthBound.ConfigResolver.IsChannelEmpty  );
            AreEqual(c.channels == 1, () => x.SynthBound.SynthWishes   .IsMono          );
            AreEqual(c.channels == 1, () => x.SynthBound.FlowNode      .IsMono          );
            AreEqual(c.channels == 1, () => x.SynthBound.ConfigResolver.IsMono          );
            AreEqual(c.channels == 2, () => x.SynthBound.SynthWishes   .IsStereo        );
            AreEqual(c.channels == 2, () => x.SynthBound.FlowNode      .IsStereo        );
            AreEqual(c.channels == 2, () => x.SynthBound.ConfigResolver.IsStereo        );
            AreEqual(c.channel,       () => x.SynthBound.SynthWishes.   Channel       ());
            AreEqual(c.channel,       () => x.SynthBound.FlowNode      .Channel       ());
            AreEqual(c.channel,       () => x.SynthBound.ConfigResolver.Channel       ());
            AreEqual(c.channels,      () => x.SynthBound.SynthWishes   .Channels      ());
            AreEqual(c.channels,      () => x.SynthBound.FlowNode      .Channels      ());
            AreEqual(c.channels,      () => x.SynthBound.ConfigResolver.Channels      ());
            AreEqual(c.channel,       () => x.SynthBound.SynthWishes.   GetChannel    ());
            AreEqual(c.channel,       () => x.SynthBound.FlowNode      .GetChannel    ());
            AreEqual(c.channel,       () => x.SynthBound.ConfigResolver.GetChannel    ());
            AreEqual(c.channels,      () => x.SynthBound.SynthWishes   .GetChannels   ());
            AreEqual(c.channels,      () => x.SynthBound.FlowNode      .GetChannels   ());
            AreEqual(c.channels,      () => x.SynthBound.ConfigResolver.GetChannels   ());
            AreEqual(c == (1,0),      () => x.SynthBound.SynthWishes   .IsCenter      ());
            AreEqual(c == (1,0),      () => x.SynthBound.FlowNode      .IsCenter      ());
            AreEqual(c == (1,0),      () => x.SynthBound.ConfigResolver.IsCenter      ());
            AreEqual(c == (2,0),      () => x.SynthBound.SynthWishes   .IsLeft        ());
            AreEqual(c == (2,0),      () => x.SynthBound.FlowNode      .IsLeft        ());
            AreEqual(c == (2,0),      () => x.SynthBound.ConfigResolver.IsLeft        ());
            AreEqual(c == (2,1),      () => x.SynthBound.SynthWishes   .IsRight       ());
            AreEqual(c == (2,1),      () => x.SynthBound.FlowNode      .IsRight       ());
            AreEqual(c == (2,1),      () => x.SynthBound.ConfigResolver.IsRight       ());
            AreEqual(c == (2,_),      () => x.SynthBound.SynthWishes   .IsAnyChannel  ());
            AreEqual(c == (2,_),      () => x.SynthBound.FlowNode      .IsAnyChannel  ());
            AreEqual(c == (2,_),      () => x.SynthBound.ConfigResolver.IsAnyChannel  ());
            AreEqual(c == (2,_),      () => x.SynthBound.SynthWishes   .IsEveryChannel());
            AreEqual(c == (2,_),      () => x.SynthBound.FlowNode      .IsEveryChannel());
            AreEqual(c == (2,_),      () => x.SynthBound.ConfigResolver.IsEveryChannel());
            AreEqual(c == (2,_),      () => x.SynthBound.SynthWishes   .IsNoChannel   ());
            AreEqual(c == (2,_),      () => x.SynthBound.FlowNode      .IsNoChannel   ());
            AreEqual(c == (2,_),      () => x.SynthBound.ConfigResolver.IsNoChannel   ());
            AreEqual(c == (2,_),      () => x.SynthBound.SynthWishes   .IsChannelEmpty());
            AreEqual(c == (2,_),      () => x.SynthBound.FlowNode      .IsChannelEmpty());
            AreEqual(c == (2,_),      () => x.SynthBound.ConfigResolver.IsChannelEmpty());
            AreEqual(c.channels == 1, () => x.SynthBound.SynthWishes   .IsMono        ());
            AreEqual(c.channels == 1, () => x.SynthBound.FlowNode      .IsMono        ());
            AreEqual(c.channels == 1, () => x.SynthBound.ConfigResolver.IsMono        ());
            AreEqual(c.channels == 2, () => x.SynthBound.SynthWishes   .IsStereo      ());
            AreEqual(c.channels == 2, () => x.SynthBound.FlowNode      .IsStereo      ());
            AreEqual(c.channels == 2, () => x.SynthBound.ConfigResolver.IsStereo      ());
            AreEqual(c.channel,       () => Channel       (x.SynthBound.SynthWishes    ));
            AreEqual(c.channel,       () => Channel       (x.SynthBound.FlowNode       ));
            AreEqual(c.channel,       () => Channel       (x.SynthBound.ConfigResolver ));
            AreEqual(c.channels,      () => Channels      (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels,      () => Channels      (x.SynthBound.FlowNode       ));
            AreEqual(c.channels,      () => Channels      (x.SynthBound.ConfigResolver ));
            AreEqual(c.channel,       () => GetChannel    (x.SynthBound.SynthWishes    ));
            AreEqual(c.channel,       () => GetChannel    (x.SynthBound.FlowNode       ));
            AreEqual(c.channel,       () => GetChannel    (x.SynthBound.ConfigResolver ));
            AreEqual(c.channels,      () => GetChannels   (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels,      () => GetChannels   (x.SynthBound.FlowNode       ));
            AreEqual(c.channels,      () => GetChannels   (x.SynthBound.ConfigResolver ));
            AreEqual(c == (1,0),      () => IsCenter      (x.SynthBound.SynthWishes    ));
            AreEqual(c == (1,0),      () => IsCenter      (x.SynthBound.FlowNode       ));
            AreEqual(c == (1,0),      () => IsCenter      (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,0),      () => IsLeft        (x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,0),      () => IsLeft        (x.SynthBound.FlowNode       ));
            AreEqual(c == (2,0),      () => IsLeft        (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,1),      () => IsRight       (x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,1),      () => IsRight       (x.SynthBound.FlowNode       ));
            AreEqual(c == (2,1),      () => IsRight       (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,_),      () => IsNoChannel   (x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,_),      () => IsNoChannel   (x.SynthBound.FlowNode       ));
            AreEqual(c == (2,_),      () => IsNoChannel   (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,_),      () => IsAnyChannel  (x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,_),      () => IsAnyChannel  (x.SynthBound.FlowNode       ));
            AreEqual(c == (2,_),      () => IsAnyChannel  (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,_),      () => IsEveryChannel(x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,_),      () => IsEveryChannel(x.SynthBound.FlowNode       ));
            AreEqual(c == (2,_),      () => IsEveryChannel(x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,_),      () => IsChannelEmpty(x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,_),      () => IsChannelEmpty(x.SynthBound.FlowNode       ));
            AreEqual(c == (2,_),      () => IsChannelEmpty(x.SynthBound.ConfigResolver ));
            AreEqual(c.channels == 1, () => IsMono        (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels == 1, () => IsMono        (x.SynthBound.FlowNode       ));
            AreEqual(c.channels == 1, () => IsMono        (x.SynthBound.ConfigResolver ));
            AreEqual(c.channels == 2, () => IsStereo      (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels == 2, () => IsStereo      (x.SynthBound.FlowNode       ));
            AreEqual(c.channels == 2, () => IsStereo      (x.SynthBound.ConfigResolver ));
            AreEqual(c.channel,       () => ConfigWishes        .Channel       (x.SynthBound.SynthWishes    ));
            AreEqual(c.channel,       () => ConfigWishes        .Channel       (x.SynthBound.FlowNode       ));
            AreEqual(c.channel,       () => ConfigWishesAccessor.Channel       (x.SynthBound.ConfigResolver ));
            AreEqual(c.channels,      () => ConfigWishes        .Channels      (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels,      () => ConfigWishes        .Channels      (x.SynthBound.FlowNode       ));
            AreEqual(c.channels,      () => ConfigWishesAccessor.Channels      (x.SynthBound.ConfigResolver ));
            AreEqual(c.channel,       () => ConfigWishes        .GetChannel    (x.SynthBound.SynthWishes    ));
            AreEqual(c.channel,       () => ConfigWishes        .GetChannel    (x.SynthBound.FlowNode       ));
            AreEqual(c.channel,       () => ConfigWishesAccessor.GetChannel    (x.SynthBound.ConfigResolver ));
            AreEqual(c.channels,      () => ConfigWishes        .GetChannels   (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels,      () => ConfigWishes        .GetChannels   (x.SynthBound.FlowNode       ));
            AreEqual(c.channels,      () => ConfigWishesAccessor.GetChannels   (x.SynthBound.ConfigResolver ));
            AreEqual(c == (1,0),      () => ConfigWishes        .IsCenter      (x.SynthBound.SynthWishes    ));
            AreEqual(c == (1,0),      () => ConfigWishes        .IsCenter      (x.SynthBound.FlowNode       ));
            AreEqual(c == (1,0),      () => ConfigWishesAccessor.IsCenter      (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,0),      () => ConfigWishes        .IsLeft        (x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,0),      () => ConfigWishes        .IsLeft        (x.SynthBound.FlowNode       ));
            AreEqual(c == (2,0),      () => ConfigWishesAccessor.IsLeft        (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,1),      () => ConfigWishes        .IsRight       (x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,1),      () => ConfigWishes        .IsRight       (x.SynthBound.FlowNode       ));
            AreEqual(c == (2,1),      () => ConfigWishesAccessor.IsRight       (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,_),      () => ConfigWishes        .IsNoChannel   (x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,_),      () => ConfigWishes        .IsNoChannel   (x.SynthBound.FlowNode       ));
            AreEqual(c == (2,_),      () => ConfigWishesAccessor.IsNoChannel   (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,_),      () => ConfigWishes        .IsAnyChannel  (x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,_),      () => ConfigWishes        .IsAnyChannel  (x.SynthBound.FlowNode       ));
            AreEqual(c == (2,_),      () => ConfigWishesAccessor.IsAnyChannel  (x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,_),      () => ConfigWishes        .IsEveryChannel(x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,_),      () => ConfigWishes        .IsEveryChannel(x.SynthBound.FlowNode       ));
            AreEqual(c == (2,_),      () => ConfigWishesAccessor.IsEveryChannel(x.SynthBound.ConfigResolver ));
            AreEqual(c == (2,_),      () => ConfigWishes        .IsChannelEmpty(x.SynthBound.SynthWishes    ));
            AreEqual(c == (2,_),      () => ConfigWishes        .IsChannelEmpty(x.SynthBound.FlowNode       ));
            AreEqual(c == (2,_),      () => ConfigWishesAccessor.IsChannelEmpty(x.SynthBound.ConfigResolver ));
            AreEqual(c.channels == 1, () => ConfigWishes        .IsMono        (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels == 1, () => ConfigWishes        .IsMono        (x.SynthBound.FlowNode       ));
            AreEqual(c.channels == 1, () => ConfigWishesAccessor.IsMono        (x.SynthBound.ConfigResolver ));
            AreEqual(c.channels == 2, () => ConfigWishes        .IsStereo      (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels == 2, () => ConfigWishes        .IsStereo      (x.SynthBound.FlowNode       ));
            AreEqual(c.channels == 2, () => ConfigWishesAccessor.IsStereo      (x.SynthBound.ConfigResolver ));
        }
        
        private void Assert_TapeBound_Getters_SingleTape(ConfigTestEntities x, (int channels, int? channel) c)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);
            AreEqual(c.channels,      () => x.TapeBound.TapeConfig.Channels      );
            AreEqual(c.channel,       () => x.TapeBound.TapeConfig.Channel       );
            AreEqual(c.channels == 1, () => x.TapeBound.TapeConfig.IsMono        );
            AreEqual(c.channels == 2, () => x.TapeBound.TapeConfig.IsStereo      );
            AreEqual(c == (1,0),      () => x.TapeBound.TapeConfig.IsCenter      );
            AreEqual(c == (2,0),      () => x.TapeBound.TapeConfig.IsLeft        );
            AreEqual(c == (2,1),      () => x.TapeBound.TapeConfig.IsRight       );
            AreEqual(c == (2,_),      () => x.TapeBound.TapeConfig.IsNoChannel   );
            AreEqual(c == (2,_),      () => x.TapeBound.TapeConfig.IsAnyChannel  );
            AreEqual(c == (2,_),      () => x.TapeBound.TapeConfig.IsEveryChannel);
            AreEqual(c == (2,_),      () => x.TapeBound.TapeConfig.IsChannelEmpty);
            AreEqual(c.channel,       () => x.TapeBound.Tape       .Channel       ());
            AreEqual(c.channel,       () => x.TapeBound.TapeConfig .Channel       ());
            AreEqual(c.channel,       () => x.TapeBound.TapeActions.Channel       ());
            AreEqual(c.channel,       () => x.TapeBound.TapeAction .Channel       ());
            AreEqual(c.channels,      () => x.TapeBound.Tape       .Channels      ());
            AreEqual(c.channels,      () => x.TapeBound.TapeConfig .Channels      ());
            AreEqual(c.channels,      () => x.TapeBound.TapeActions.Channels      ());
            AreEqual(c.channels,      () => x.TapeBound.TapeAction .Channels      ());
            AreEqual(c.channel,       () => x.TapeBound.Tape       .GetChannel    ());
            AreEqual(c.channel,       () => x.TapeBound.TapeConfig .GetChannel    ());
            AreEqual(c.channel,       () => x.TapeBound.TapeActions.GetChannel    ());
            AreEqual(c.channel,       () => x.TapeBound.TapeAction .GetChannel    ());
            AreEqual(c.channels,      () => x.TapeBound.Tape       .GetChannels   ());
            AreEqual(c.channels,      () => x.TapeBound.TapeConfig .GetChannels   ());
            AreEqual(c.channels,      () => x.TapeBound.TapeActions.GetChannels   ());
            AreEqual(c.channels,      () => x.TapeBound.TapeAction .GetChannels   ());
            AreEqual(c == (1,0),      () => x.TapeBound.Tape       .IsCenter      ());
            AreEqual(c == (1,0),      () => x.TapeBound.TapeConfig .IsCenter      ());
            AreEqual(c == (1,0),      () => x.TapeBound.TapeActions.IsCenter      ());
            AreEqual(c == (1,0),      () => x.TapeBound.TapeAction .IsCenter      ());
            AreEqual(c == (2,0),      () => x.TapeBound.Tape       .IsLeft        ());
            AreEqual(c == (2,0),      () => x.TapeBound.TapeConfig .IsLeft        ());
            AreEqual(c == (2,0),      () => x.TapeBound.TapeActions.IsLeft        ());
            AreEqual(c == (2,0),      () => x.TapeBound.TapeAction .IsLeft        ());
            AreEqual(c == (2,1),      () => x.TapeBound.Tape       .IsRight       ());
            AreEqual(c == (2,1),      () => x.TapeBound.TapeConfig .IsRight       ());
            AreEqual(c == (2,1),      () => x.TapeBound.TapeActions.IsRight       ());
            AreEqual(c == (2,1),      () => x.TapeBound.TapeAction .IsRight       ());
            AreEqual(c == (2,_),      () => x.TapeBound.Tape       .IsNoChannel   ());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeConfig .IsNoChannel   ());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeActions.IsNoChannel   ());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeAction .IsNoChannel   ());
            AreEqual(c == (2,_),      () => x.TapeBound.Tape       .IsAnyChannel  ());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeConfig .IsAnyChannel  ());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeActions.IsAnyChannel  ());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeAction .IsAnyChannel  ());
            AreEqual(c == (2,_),      () => x.TapeBound.Tape       .IsEveryChannel());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeConfig .IsEveryChannel());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeActions.IsEveryChannel());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeAction .IsEveryChannel());
            AreEqual(c == (2,_),      () => x.TapeBound.Tape       .IsChannelEmpty());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeConfig .IsChannelEmpty());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeActions.IsChannelEmpty());
            AreEqual(c == (2,_),      () => x.TapeBound.TapeAction .IsChannelEmpty());
            AreEqual(c.channels == 1, () => x.TapeBound.Tape       .IsMono        ());
            AreEqual(c.channels == 1, () => x.TapeBound.TapeConfig .IsMono        ());
            AreEqual(c.channels == 1, () => x.TapeBound.TapeActions.IsMono        ());
            AreEqual(c.channels == 1, () => x.TapeBound.TapeAction .IsMono        ());
            AreEqual(c.channels == 2, () => x.TapeBound.Tape       .IsStereo      ());
            AreEqual(c.channels == 2, () => x.TapeBound.TapeConfig .IsStereo      ());
            AreEqual(c.channels == 2, () => x.TapeBound.TapeActions.IsStereo      ());
            AreEqual(c.channels == 2, () => x.TapeBound.TapeAction .IsStereo      ());
            AreEqual(c.channel,       () => Channel       (x.TapeBound.Tape       ));
            AreEqual(c.channel,       () => Channel       (x.TapeBound.TapeConfig ));
            AreEqual(c.channel,       () => Channel       (x.TapeBound.TapeActions));
            AreEqual(c.channel,       () => Channel       (x.TapeBound.TapeAction ));
            AreEqual(c.channels,      () => Channels      (x.TapeBound.Tape       ));
            AreEqual(c.channels,      () => Channels      (x.TapeBound.TapeConfig ));
            AreEqual(c.channels,      () => Channels      (x.TapeBound.TapeActions));
            AreEqual(c.channels,      () => Channels      (x.TapeBound.TapeAction ));
            AreEqual(c.channel,       () => GetChannel    (x.TapeBound.Tape       ));
            AreEqual(c.channel,       () => GetChannel    (x.TapeBound.TapeConfig ));
            AreEqual(c.channel,       () => GetChannel    (x.TapeBound.TapeActions));
            AreEqual(c.channel,       () => GetChannel    (x.TapeBound.TapeAction ));
            AreEqual(c.channels,      () => GetChannels   (x.TapeBound.Tape       ));
            AreEqual(c.channels,      () => GetChannels   (x.TapeBound.TapeConfig ));
            AreEqual(c.channels,      () => GetChannels   (x.TapeBound.TapeActions));
            AreEqual(c.channels,      () => GetChannels   (x.TapeBound.TapeAction ));
            AreEqual(c == (1,0),      () => IsCenter      (x.TapeBound.Tape       ));
            AreEqual(c == (1,0),      () => IsCenter      (x.TapeBound.TapeConfig ));
            AreEqual(c == (1,0),      () => IsCenter      (x.TapeBound.TapeActions));
            AreEqual(c == (1,0),      () => IsCenter      (x.TapeBound.TapeAction ));
            AreEqual(c == (2,0),      () => IsLeft        (x.TapeBound.Tape       ));
            AreEqual(c == (2,0),      () => IsLeft        (x.TapeBound.TapeConfig ));
            AreEqual(c == (2,0),      () => IsLeft        (x.TapeBound.TapeActions));
            AreEqual(c == (2,0),      () => IsLeft        (x.TapeBound.TapeAction ));
            AreEqual(c == (2,1),      () => IsRight       (x.TapeBound.Tape       ));
            AreEqual(c == (2,1),      () => IsRight       (x.TapeBound.TapeConfig ));
            AreEqual(c == (2,1),      () => IsRight       (x.TapeBound.TapeActions));
            AreEqual(c == (2,1),      () => IsRight       (x.TapeBound.TapeAction ));
            AreEqual(c == (2,_),      () => IsNoChannel   (x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => IsNoChannel   (x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => IsNoChannel   (x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => IsNoChannel   (x.TapeBound.TapeAction ));
            AreEqual(c == (2,_),      () => IsAnyChannel  (x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => IsAnyChannel  (x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => IsAnyChannel  (x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => IsAnyChannel  (x.TapeBound.TapeAction ));
            AreEqual(c == (2,_),      () => IsEveryChannel(x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => IsEveryChannel(x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => IsEveryChannel(x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => IsEveryChannel(x.TapeBound.TapeAction ));
            AreEqual(c == (2,_),      () => IsChannelEmpty(x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => IsChannelEmpty(x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => IsChannelEmpty(x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => IsChannelEmpty(x.TapeBound.TapeAction ));
            AreEqual(c.channels == 1, () => IsMono        (x.TapeBound.Tape       ));
            AreEqual(c.channels == 1, () => IsMono        (x.TapeBound.TapeConfig ));
            AreEqual(c.channels == 1, () => IsMono        (x.TapeBound.TapeActions));
            AreEqual(c.channels == 1, () => IsMono        (x.TapeBound.TapeAction ));
            AreEqual(c.channels == 2, () => IsStereo      (x.TapeBound.Tape       ));
            AreEqual(c.channels == 2, () => IsStereo      (x.TapeBound.TapeConfig ));
            AreEqual(c.channels == 2, () => IsStereo      (x.TapeBound.TapeActions));
            AreEqual(c.channels == 2, () => IsStereo      (x.TapeBound.TapeAction ));
            AreEqual(c.channel,       () => ConfigWishes.Channel       (x.TapeBound.Tape       ));
            AreEqual(c.channel,       () => ConfigWishes.Channel       (x.TapeBound.TapeConfig ));
            AreEqual(c.channel,       () => ConfigWishes.Channel       (x.TapeBound.TapeActions));
            AreEqual(c.channel,       () => ConfigWishes.Channel       (x.TapeBound.TapeAction ));
            AreEqual(c.channels,      () => ConfigWishes.Channels      (x.TapeBound.Tape       ));
            AreEqual(c.channels,      () => ConfigWishes.Channels      (x.TapeBound.TapeConfig ));
            AreEqual(c.channels,      () => ConfigWishes.Channels      (x.TapeBound.TapeActions));
            AreEqual(c.channels,      () => ConfigWishes.Channels      (x.TapeBound.TapeAction ));
            AreEqual(c.channel,       () => ConfigWishes.GetChannel    (x.TapeBound.Tape       ));
            AreEqual(c.channel,       () => ConfigWishes.GetChannel    (x.TapeBound.TapeConfig ));
            AreEqual(c.channel,       () => ConfigWishes.GetChannel    (x.TapeBound.TapeActions));
            AreEqual(c.channel,       () => ConfigWishes.GetChannel    (x.TapeBound.TapeAction ));
            AreEqual(c.channels,      () => ConfigWishes.GetChannels   (x.TapeBound.Tape       ));
            AreEqual(c.channels,      () => ConfigWishes.GetChannels   (x.TapeBound.TapeConfig ));
            AreEqual(c.channels,      () => ConfigWishes.GetChannels   (x.TapeBound.TapeActions));
            AreEqual(c.channels,      () => ConfigWishes.GetChannels   (x.TapeBound.TapeAction ));
            AreEqual(c == (1,0),      () => ConfigWishes.IsCenter      (x.TapeBound.Tape       ));
            AreEqual(c == (1,0),      () => ConfigWishes.IsCenter      (x.TapeBound.TapeConfig ));
            AreEqual(c == (1,0),      () => ConfigWishes.IsCenter      (x.TapeBound.TapeActions));
            AreEqual(c == (1,0),      () => ConfigWishes.IsCenter      (x.TapeBound.TapeAction ));
            AreEqual(c == (2,0),      () => ConfigWishes.IsLeft        (x.TapeBound.Tape       ));
            AreEqual(c == (2,0),      () => ConfigWishes.IsLeft        (x.TapeBound.TapeConfig ));
            AreEqual(c == (2,0),      () => ConfigWishes.IsLeft        (x.TapeBound.TapeActions));
            AreEqual(c == (2,0),      () => ConfigWishes.IsLeft        (x.TapeBound.TapeAction ));
            AreEqual(c == (2,1),      () => ConfigWishes.IsRight       (x.TapeBound.Tape       ));
            AreEqual(c == (2,1),      () => ConfigWishes.IsRight       (x.TapeBound.TapeConfig ));
            AreEqual(c == (2,1),      () => ConfigWishes.IsRight       (x.TapeBound.TapeActions));
            AreEqual(c == (2,1),      () => ConfigWishes.IsRight       (x.TapeBound.TapeAction ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel   (x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel   (x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel   (x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel   (x.TapeBound.TapeAction ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsAnyChannel  (x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeAction ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsEveryChannel(x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsEveryChannel(x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsEveryChannel(x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => ConfigWishes.IsEveryChannel(x.TapeBound.TapeAction ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsChannelEmpty(x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsChannelEmpty(x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsChannelEmpty(x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => ConfigWishes.IsChannelEmpty(x.TapeBound.TapeAction ));
            AreEqual(c.channels == 1, () => ConfigWishes.IsMono        (x.TapeBound.Tape       ));
            AreEqual(c.channels == 1, () => ConfigWishes.IsMono        (x.TapeBound.TapeConfig ));
            AreEqual(c.channels == 1, () => ConfigWishes.IsMono        (x.TapeBound.TapeActions));
            AreEqual(c.channels == 1, () => ConfigWishes.IsMono        (x.TapeBound.TapeAction ));
            AreEqual(c.channels == 2, () => ConfigWishes.IsStereo      (x.TapeBound.Tape       ));
            AreEqual(c.channels == 2, () => ConfigWishes.IsStereo      (x.TapeBound.TapeConfig ));
            AreEqual(c.channels == 2, () => ConfigWishes.IsStereo      (x.TapeBound.TapeActions));
            AreEqual(c.channels == 2, () => ConfigWishes.IsStereo      (x.TapeBound.TapeAction ));
        }
        
        private void Assert_TapeBound_Getters_Complete(ConfigTestEntities x, (int channels, int? channel) c)
        {
            IsNotNull(() => x.ChannelEntities);
            AreEqual(c.channels, () => x.ChannelEntities.Count);
            IsFalse(() => x.ChannelEntities.Contains(null));
            
            if (c.channels == MonoChannels)
            {
                AreSame(x.TapeBound.Tape, () => x.ChannelEntities[0].TapeBound.Tape); 
                Assert_MonoTape_Getters(x);
                Assert_MonoTape_Getters(x.ChannelEntities[0]);
            }
            if (c.channels == StereoChannels)
            {
                Assert_StereoTape_Getters(x);
                Assert_LeftTape_Getters(x.ChannelEntities[0]);
                Assert_RightTape_Getters(x.ChannelEntities[1]);
            }
        }

        private void Assert_MonoTape_Getters(TapeEntities x)
        {
                          IsNotNull(() => x                      );
                          IsNotNull(() => x.TapeBound.Tape       );
                          IsNotNull(() => x.TapeBound.TapeConfig );
                          IsNotNull(() => x.TapeBound.TapeActions);
                          IsNotNull(() => x.TapeBound.TapeAction );
            AreEqual(CenterChannel, () => x.TapeBound.TapeConfig .Channel       );
            AreEqual(MonoChannels , () => x.TapeBound.TapeConfig .Channels      );
                            IsTrue (() => x.TapeBound.TapeConfig .IsCenter      );
                            IsTrue (() => x.TapeBound.TapeConfig .IsMono        );
                            IsFalse(() => x.TapeBound.TapeConfig .IsLeft        );
                            IsFalse(() => x.TapeBound.TapeConfig .IsRight       );
                            IsFalse(() => x.TapeBound.TapeConfig .IsStereo      );
                            IsFalse(() => x.TapeBound.TapeConfig .IsNoChannel   );
                            IsFalse(() => x.TapeBound.TapeConfig .IsAnyChannel  );
                            IsFalse(() => x.TapeBound.TapeConfig .IsEveryChannel);
                            IsFalse(() => x.TapeBound.TapeConfig .IsChannelEmpty);
            AreEqual(CenterChannel, () => x.TapeBound.Tape       .Channel       ());
            AreEqual(CenterChannel, () => x.TapeBound.TapeConfig .Channel       ());
            AreEqual(CenterChannel, () => x.TapeBound.TapeActions.Channel       ());
            AreEqual(CenterChannel, () => x.TapeBound.TapeAction .Channel       ());
            AreEqual(MonoChannels , () => x.TapeBound.Tape       .Channels      ());
            AreEqual(MonoChannels , () => x.TapeBound.TapeConfig .Channels      ());
            AreEqual(MonoChannels , () => x.TapeBound.TapeActions.Channels      ());
            AreEqual(MonoChannels , () => x.TapeBound.TapeAction .Channels      ());
            AreEqual(CenterChannel, () => x.TapeBound.Tape       .GetChannel    ());
            AreEqual(CenterChannel, () => x.TapeBound.TapeConfig .GetChannel    ());
            AreEqual(CenterChannel, () => x.TapeBound.TapeActions.GetChannel    ());
            AreEqual(CenterChannel, () => x.TapeBound.TapeAction .GetChannel    ());
            AreEqual(MonoChannels , () => x.TapeBound.Tape       .GetChannels   ());
            AreEqual(MonoChannels , () => x.TapeBound.TapeConfig .GetChannels   ());
            AreEqual(MonoChannels , () => x.TapeBound.TapeActions.GetChannels   ());
            AreEqual(MonoChannels , () => x.TapeBound.TapeAction .GetChannels   ());
                            IsTrue (() => x.TapeBound.Tape       .IsCenter      ());
                            IsTrue (() => x.TapeBound.TapeConfig .IsCenter      ());
                            IsTrue (() => x.TapeBound.TapeActions.IsCenter      ());
                            IsTrue (() => x.TapeBound.TapeAction .IsCenter      ());
                            IsTrue (() => x.TapeBound.Tape       .IsMono        ());
                            IsTrue (() => x.TapeBound.TapeConfig .IsMono        ());
                            IsTrue (() => x.TapeBound.TapeActions.IsMono        ());
                            IsTrue (() => x.TapeBound.TapeAction .IsMono        ());
                            IsFalse(() => x.TapeBound.Tape       .IsLeft        ());
                            IsFalse(() => x.TapeBound.TapeConfig .IsLeft        ());
                            IsFalse(() => x.TapeBound.TapeActions.IsLeft        ());
                            IsFalse(() => x.TapeBound.TapeAction .IsLeft        ());
                            IsFalse(() => x.TapeBound.Tape       .IsRight       ());
                            IsFalse(() => x.TapeBound.TapeConfig .IsRight       ());
                            IsFalse(() => x.TapeBound.TapeActions.IsRight       ());
                            IsFalse(() => x.TapeBound.TapeAction .IsRight       ());
                            IsFalse(() => x.TapeBound.Tape       .IsNoChannel   ());
                            IsFalse(() => x.TapeBound.TapeConfig .IsNoChannel   ());
                            IsFalse(() => x.TapeBound.TapeActions.IsNoChannel   ());
                            IsFalse(() => x.TapeBound.TapeAction .IsNoChannel   ());
                            IsFalse(() => x.TapeBound.Tape       .IsAnyChannel  ());
                            IsFalse(() => x.TapeBound.TapeConfig .IsAnyChannel  ());
                            IsFalse(() => x.TapeBound.TapeActions.IsAnyChannel  ());
                            IsFalse(() => x.TapeBound.TapeAction .IsAnyChannel  ());
                            IsFalse(() => x.TapeBound.Tape       .IsEveryChannel());
                            IsFalse(() => x.TapeBound.TapeConfig .IsEveryChannel());
                            IsFalse(() => x.TapeBound.TapeActions.IsEveryChannel());
                            IsFalse(() => x.TapeBound.TapeAction .IsEveryChannel());
                            IsFalse(() => x.TapeBound.Tape       .IsChannelEmpty());
                            IsFalse(() => x.TapeBound.TapeConfig .IsChannelEmpty());
                            IsFalse(() => x.TapeBound.TapeActions.IsChannelEmpty());
                            IsFalse(() => x.TapeBound.TapeAction .IsChannelEmpty());
                            IsFalse(() => x.TapeBound.Tape       .IsStereo      ());
                            IsFalse(() => x.TapeBound.TapeConfig .IsStereo      ());
                            IsFalse(() => x.TapeBound.TapeActions.IsStereo      ());
                            IsFalse(() => x.TapeBound.TapeAction .IsStereo      ());
            AreEqual(CenterChannel, () => Channel       (x.TapeBound.Tape        ));
            AreEqual(CenterChannel, () => Channel       (x.TapeBound.TapeConfig  ));
            AreEqual(CenterChannel, () => Channel       (x.TapeBound.TapeActions ));
            AreEqual(CenterChannel, () => Channel       (x.TapeBound.TapeAction  ));
            AreEqual(MonoChannels , () => Channels      (x.TapeBound.Tape        ));
            AreEqual(MonoChannels , () => Channels      (x.TapeBound.TapeConfig  ));
            AreEqual(MonoChannels , () => Channels      (x.TapeBound.TapeActions ));
            AreEqual(MonoChannels , () => Channels      (x.TapeBound.TapeAction  ));
            AreEqual(CenterChannel, () => GetChannel    (x.TapeBound.Tape        ));
            AreEqual(CenterChannel, () => GetChannel    (x.TapeBound.TapeConfig  ));
            AreEqual(CenterChannel, () => GetChannel    (x.TapeBound.TapeActions ));
            AreEqual(CenterChannel, () => GetChannel    (x.TapeBound.TapeAction  ));
            AreEqual(MonoChannels , () => GetChannels   (x.TapeBound.Tape        ));
            AreEqual(MonoChannels , () => GetChannels   (x.TapeBound.TapeConfig  ));
            AreEqual(MonoChannels , () => GetChannels   (x.TapeBound.TapeActions ));
            AreEqual(MonoChannels , () => GetChannels   (x.TapeBound.TapeAction  ));
                            IsTrue (() => IsCenter      (x.TapeBound.Tape        ));
                            IsTrue (() => IsCenter      (x.TapeBound.TapeConfig  ));
                            IsTrue (() => IsCenter      (x.TapeBound.TapeActions ));
                            IsTrue (() => IsCenter      (x.TapeBound.TapeAction  ));
                            IsTrue (() => IsMono        (x.TapeBound.Tape        ));
                            IsTrue (() => IsMono        (x.TapeBound.TapeConfig  ));
                            IsTrue (() => IsMono        (x.TapeBound.TapeActions ));
                            IsTrue (() => IsMono        (x.TapeBound.TapeAction  ));
                            IsFalse(() => IsLeft        (x.TapeBound.Tape        ));
                            IsFalse(() => IsLeft        (x.TapeBound.TapeConfig  ));
                            IsFalse(() => IsLeft        (x.TapeBound.TapeActions ));
                            IsFalse(() => IsLeft        (x.TapeBound.TapeAction  ));
                            IsFalse(() => IsRight       (x.TapeBound.Tape        ));
                            IsFalse(() => IsRight       (x.TapeBound.TapeConfig  ));
                            IsFalse(() => IsRight       (x.TapeBound.TapeActions ));
                            IsFalse(() => IsRight       (x.TapeBound.TapeAction  ));
                            IsFalse(() => IsNoChannel   (x.TapeBound.Tape        ));
                            IsFalse(() => IsNoChannel   (x.TapeBound.TapeConfig  ));
                            IsFalse(() => IsNoChannel   (x.TapeBound.TapeActions ));
                            IsFalse(() => IsNoChannel   (x.TapeBound.TapeAction  ));
                            IsFalse(() => IsAnyChannel  (x.TapeBound.Tape        ));
                            IsFalse(() => IsAnyChannel  (x.TapeBound.TapeConfig  ));
                            IsFalse(() => IsAnyChannel  (x.TapeBound.TapeActions ));
                            IsFalse(() => IsAnyChannel  (x.TapeBound.TapeAction  ));
                            IsFalse(() => IsEveryChannel(x.TapeBound.Tape        ));
                            IsFalse(() => IsEveryChannel(x.TapeBound.TapeConfig  ));
                            IsFalse(() => IsEveryChannel(x.TapeBound.TapeActions ));
                            IsFalse(() => IsEveryChannel(x.TapeBound.TapeAction  ));
                            IsFalse(() => IsChannelEmpty(x.TapeBound.Tape        ));
                            IsFalse(() => IsChannelEmpty(x.TapeBound.TapeConfig  ));
                            IsFalse(() => IsChannelEmpty(x.TapeBound.TapeActions ));
                            IsFalse(() => IsChannelEmpty(x.TapeBound.TapeAction  ));
                            IsFalse(() => IsStereo      (x.TapeBound.Tape        ));
                            IsFalse(() => IsStereo      (x.TapeBound.TapeConfig  ));
                            IsFalse(() => IsStereo      (x.TapeBound.TapeActions ));
                            IsFalse(() => IsStereo      (x.TapeBound.TapeAction  ));
            AreEqual(CenterChannel, () => ConfigWishes.Channel       (x.TapeBound.Tape        ));
            AreEqual(CenterChannel, () => ConfigWishes.Channel       (x.TapeBound.TapeConfig  ));
            AreEqual(CenterChannel, () => ConfigWishes.Channel       (x.TapeBound.TapeActions ));
            AreEqual(CenterChannel, () => ConfigWishes.Channel       (x.TapeBound.TapeAction  ));
            AreEqual(MonoChannels , () => ConfigWishes.Channels      (x.TapeBound.Tape        ));
            AreEqual(MonoChannels , () => ConfigWishes.Channels      (x.TapeBound.TapeConfig  ));
            AreEqual(MonoChannels , () => ConfigWishes.Channels      (x.TapeBound.TapeActions ));
            AreEqual(MonoChannels , () => ConfigWishes.Channels      (x.TapeBound.TapeAction  ));
            AreEqual(CenterChannel, () => ConfigWishes.GetChannel    (x.TapeBound.Tape        ));
            AreEqual(CenterChannel, () => ConfigWishes.GetChannel    (x.TapeBound.TapeConfig  ));
            AreEqual(CenterChannel, () => ConfigWishes.GetChannel    (x.TapeBound.TapeActions ));
            AreEqual(CenterChannel, () => ConfigWishes.GetChannel    (x.TapeBound.TapeAction  ));
            AreEqual(MonoChannels , () => ConfigWishes.GetChannels   (x.TapeBound.Tape        ));
            AreEqual(MonoChannels , () => ConfigWishes.GetChannels   (x.TapeBound.TapeConfig  ));
            AreEqual(MonoChannels , () => ConfigWishes.GetChannels   (x.TapeBound.TapeActions ));
            AreEqual(MonoChannels , () => ConfigWishes.GetChannels   (x.TapeBound.TapeAction  ));
                            IsTrue (() => ConfigWishes.IsCenter      (x.TapeBound.Tape        ));
                            IsTrue (() => ConfigWishes.IsCenter      (x.TapeBound.TapeConfig  ));
                            IsTrue (() => ConfigWishes.IsCenter      (x.TapeBound.TapeActions ));
                            IsTrue (() => ConfigWishes.IsCenter      (x.TapeBound.TapeAction  ));
                            IsTrue (() => ConfigWishes.IsMono        (x.TapeBound.Tape        ));
                            IsTrue (() => ConfigWishes.IsMono        (x.TapeBound.TapeConfig  ));
                            IsTrue (() => ConfigWishes.IsMono        (x.TapeBound.TapeActions ));
                            IsTrue (() => ConfigWishes.IsMono        (x.TapeBound.TapeAction  ));
                            IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.Tape        ));
                            IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.TapeConfig  ));
                            IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.TapeActions ));
                            IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.TapeAction  ));
                            IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.Tape        ));
                            IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.TapeConfig  ));
                            IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.TapeActions ));
                            IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.TapeAction  ));
                            IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.Tape        ));
                            IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeConfig  ));
                            IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeActions ));
                            IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeAction  ));
                            IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.Tape        ));
                            IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeConfig  ));
                            IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeActions ));
                            IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeAction  ));
                            IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.Tape        ));
                            IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeConfig  ));
                            IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeActions ));
                            IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeAction  ));
                            IsFalse(() => ConfigWishes.IsChannelEmpty(x.TapeBound.Tape        ));
                            IsFalse(() => ConfigWishes.IsChannelEmpty(x.TapeBound.TapeConfig  ));
                            IsFalse(() => ConfigWishes.IsChannelEmpty(x.TapeBound.TapeActions ));
                            IsFalse(() => ConfigWishes.IsChannelEmpty(x.TapeBound.TapeAction  ));
                            IsFalse(() => ConfigWishes.IsStereo       (x.TapeBound.Tape       ));
                            IsFalse(() => ConfigWishes.IsStereo       (x.TapeBound.TapeConfig ));
                            IsFalse(() => ConfigWishes.IsStereo       (x.TapeBound.TapeActions));
                            IsFalse(() => ConfigWishes.IsStereo       (x.TapeBound.TapeAction ));
        }

        private void Assert_StereoTape_Getters(TapeEntities x)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);
            
            AreEqual(ChannelEmpty,   () => x.TapeBound.TapeConfig.Channel );
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels);
                             IsTrue (() => x.TapeBound.TapeConfig.IsStereo);
                             IsTrue (() => x.TapeBound.TapeConfig.IsNoChannel);
                             IsFalse(() => x.TapeBound.TapeConfig.IsMono  );
                             IsFalse(() => x.TapeBound.TapeConfig.IsCenter);
                             IsFalse(() => x.TapeBound.TapeConfig.IsLeft  );
                             IsFalse(() => x.TapeBound.TapeConfig.IsRight );

            AreEqual(ChannelEmpty,   () => x.TapeBound.Tape       .Channel ());
            AreEqual(ChannelEmpty,   () => x.TapeBound.TapeConfig .Channel ());
            AreEqual(ChannelEmpty,   () => x.TapeBound.TapeActions.Channel ());
            AreEqual(ChannelEmpty,   () => x.TapeBound.TapeAction .Channel ());
            AreEqual(StereoChannels, () => x.TapeBound.Tape       .Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeActions.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeAction .Channels());
            
            IsTrue(() => x.TapeBound.Tape       .IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig .IsStereo());
            IsTrue(() => x.TapeBound.TapeActions.IsStereo());
            IsTrue(() => x.TapeBound.TapeAction .IsStereo());
            
            IsTrue(() => x.TapeBound.Tape       .IsNoChannel());
            IsTrue(() => x.TapeBound.TapeConfig .IsNoChannel());
            IsTrue(() => x.TapeBound.TapeActions.IsNoChannel());
            IsTrue(() => x.TapeBound.TapeAction .IsNoChannel());

            IsFalse(() => x.TapeBound.Tape       .IsMono());
            IsFalse(() => x.TapeBound.TapeConfig .IsMono());
            IsFalse(() => x.TapeBound.TapeActions.IsMono());
            IsFalse(() => x.TapeBound.TapeAction .IsMono());


            IsFalse(() => x.TapeBound.Tape       .IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig .IsCenter());
            IsFalse(() => x.TapeBound.TapeActions.IsCenter());
            IsFalse(() => x.TapeBound.TapeAction .IsCenter());
                                                                    
            IsFalse(() => x.TapeBound.Tape       .IsLeft());
            IsFalse(() => x.TapeBound.TapeConfig .IsLeft());
            IsFalse(() => x.TapeBound.TapeActions.IsLeft());
            IsFalse(() => x.TapeBound.TapeAction .IsLeft());
                                                                    
            IsFalse(() => x.TapeBound.Tape       .IsRight());
            IsFalse(() => x.TapeBound.TapeConfig .IsRight());
            IsFalse(() => x.TapeBound.TapeActions.IsRight());
            IsFalse(() => x.TapeBound.TapeAction .IsRight());
        }

        private void Assert_LeftTape_Getters(TapeEntities x)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);
            
            AreEqual(StereoChannels, () => x.TapeBound.Tape.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels);
            AreEqual(StereoChannels, () => x.TapeBound.TapeActions.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeAction.Channels());
                                                    
            IsFalse(() => x.TapeBound.Tape.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono);
            IsFalse(() => x.TapeBound.TapeActions.IsMono());
            IsFalse(() => x.TapeBound.TapeAction.IsMono());
            
            IsTrue(() => x.TapeBound.Tape.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo);
            IsTrue(() => x.TapeBound.TapeActions.IsStereo());
            IsTrue(() => x.TapeBound.TapeAction.IsStereo());

            AreEqual(LeftChannel, () => x.TapeBound.Tape.Channel());
            AreEqual(LeftChannel, () => x.TapeBound.TapeConfig.Channel());
            AreEqual(LeftChannel, () => x.TapeBound.TapeConfig.Channel);
            AreEqual(LeftChannel, () => x.TapeBound.TapeActions.Channel());
            AreEqual(LeftChannel, () => x.TapeBound.TapeAction.Channel());

            IsFalse(() => x.TapeBound.Tape.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter);
            IsFalse(() => x.TapeBound.TapeActions.IsCenter());
            IsFalse(() => x.TapeBound.TapeAction.IsCenter());
                                                                    
            IsTrue(() => x.TapeBound.Tape.IsLeft());
            IsTrue(() => x.TapeBound.TapeConfig.IsLeft());
            IsTrue(() => x.TapeBound.TapeConfig.IsLeft);
            IsTrue(() => x.TapeBound.TapeActions.IsLeft());
            IsTrue(() => x.TapeBound.TapeAction.IsLeft());
                                                                    
            IsFalse(() => x.TapeBound.Tape.IsRight());
            IsFalse(() => x.TapeBound.TapeConfig.IsRight());
            IsFalse(() => x.TapeBound.TapeConfig.IsRight);
            IsFalse(() => x.TapeBound.TapeActions.IsRight());
            IsFalse(() => x.TapeBound.TapeAction.IsRight());
        }

        private void Assert_RightTape_Getters(TapeEntities x)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);
            
            AreEqual(StereoChannels, () => x.TapeBound.Tape.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeConfig.Channels);
            AreEqual(StereoChannels, () => x.TapeBound.TapeActions.Channels());
            AreEqual(StereoChannels, () => x.TapeBound.TapeAction.Channels());
                                                    
            IsFalse(() => x.TapeBound.Tape.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono());
            IsFalse(() => x.TapeBound.TapeConfig.IsMono);
            IsFalse(() => x.TapeBound.TapeActions.IsMono());
            IsFalse(() => x.TapeBound.TapeAction.IsMono());
            
            IsTrue(() => x.TapeBound.Tape.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo());
            IsTrue(() => x.TapeBound.TapeConfig.IsStereo);
            IsTrue(() => x.TapeBound.TapeActions.IsStereo());
            IsTrue(() => x.TapeBound.TapeAction.IsStereo());

            AreEqual(RightChannel, () => x.TapeBound.Tape.Channel());
            AreEqual(RightChannel, () => x.TapeBound.TapeConfig.Channel());
            AreEqual(RightChannel, () => x.TapeBound.TapeConfig.Channel);
            AreEqual(RightChannel, () => x.TapeBound.TapeActions.Channel());
            AreEqual(RightChannel, () => x.TapeBound.TapeAction.Channel());

            IsFalse(() => x.TapeBound.Tape.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter());
            IsFalse(() => x.TapeBound.TapeConfig.IsCenter);
            IsFalse(() => x.TapeBound.TapeActions.IsCenter());
            IsFalse(() => x.TapeBound.TapeAction.IsCenter());
                                                                    
            IsFalse(() => x.TapeBound.Tape.IsLeft());
            IsFalse(() => x.TapeBound.TapeConfig.IsLeft());
            IsFalse(() => x.TapeBound.TapeConfig.IsLeft);
            IsFalse(() => x.TapeBound.TapeActions.IsLeft());
            IsFalse(() => x.TapeBound.TapeAction.IsLeft());
                                                                    
            IsTrue(() => x.TapeBound.Tape.IsRight());
            IsTrue(() => x.TapeBound.TapeConfig.IsRight());
            IsTrue(() => x.TapeBound.TapeConfig.IsRight);
            IsTrue(() => x.TapeBound.TapeActions.IsRight());
            IsTrue(() => x.TapeBound.TapeAction.IsRight());
        }
        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, (int channels, int? channel) c)
        {
            // TODO: Handle Mono/Stereo gracefully.

            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);
            IsNotNull(() => x.BuffBound.AudioFileOutput);
            
            AreEqual(c.channels, () => x.BuffBound.Buff.Channels());
            AreEqual(c.channels, () => x.BuffBound.AudioFileOutput.Channels());
            AreEqual(c.channels == MonoChannels, () => x.BuffBound.Buff.IsMono());
            AreEqual(c.channels == MonoChannels, () => x.BuffBound.AudioFileOutput.IsMono());
            AreEqual(c.channels == StereoChannels, () => x.BuffBound.Buff.IsStereo());
            AreEqual(c.channels == StereoChannels, () => x.BuffBound.AudioFileOutput.IsStereo());

            if (c.channels == MonoChannels)
            { 
                // TODO: More getters!
                AreEqual(CenterChannel, () => x.BuffBound.Buff.Channel());
                AreEqual(CenterChannel, () => x.BuffBound.AudioFileOutput.Channel());
                
                IsTrue(() => x.BuffBound.Buff.IsCenter());
                IsTrue(() => x.BuffBound.AudioFileOutput.IsCenter());
            }
            
            if (c.channels == StereoChannels)
            {
                // TODO: More getters!

                //AreEqual(c.channel, () => x.BuffBound.Buff.Channel());
                //AreEqual(c.channel, () => x.BuffBound.AudioFileOutput.Channel());
                
                //AreEqual(ChannelEmpty, () => x.BuffBound.Buff.Channel());
                //AreEqual(ChannelEmpty, () => x.BuffBound.AudioFileOutput.Channel());


                // TODO: Buffs per tape etc.
            
                //AreEqual(c == (2,0), () => x.BuffBound.Buff.IsLeft());
                //AreEqual(c == (2,0), () => x.BuffBound.AudioFileOutput.IsLeft());
                
                //AreEqual(c == (2,1), () => x.BuffBound.Buff.IsRight());
                //AreEqual(c == (2,1), () => x.BuffBound.AudioFileOutput.IsRight());
            }
        }

        private void Assert_Immutable_Getters(ConfigTestEntities x, (int, int?) c)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            
            Assert_Immutable_Getters(x.Immutable.ChannelEnum, c);
            Assert_Immutable_Getters(x.Immutable.ChannelEntity, c);
        }
        
        private void Assert_Immutable_Getters(ChannelEnum channelEnum, (int channels, int? channel) c)
        {
            if (c.channels == MonoChannels)
            {
                IsTrue (() => channelEnum.IsMono  ());
                IsTrue (() => channelEnum.IsCenter());
                
                IsFalse(() => channelEnum.IsStereo());
                IsFalse(() => channelEnum.IsLeft  ());
                IsFalse(() => channelEnum.IsRight ());

                AreEqual(MonoChannels, () => channelEnum.Channels());
                AreEqual(MonoChannels, () => channelEnum.ChannelEnumToChannels());
                
                AreEqual(CenterChannel, () => channelEnum.Channel());
                AreEqual(CenterChannel, () => channelEnum.EnumToChannel());
            }
            else if (c.channels == StereoChannels)
            {
                IsTrue(() => channelEnum.IsStereo());
                AreEqual(c.channel == 0, () => channelEnum.IsLeft());
                AreEqual(c.channel == 1, () => channelEnum.IsRight());
                
                IsFalse(() => channelEnum.IsMono());
                IsFalse(() => channelEnum.IsCenter());

                AreEqual(StereoChannels, () => channelEnum.Channels());
                AreEqual(StereoChannels, () => channelEnum.ChannelEnumToChannels());

                AreEqual(c.channel, () => channelEnum.Channel());
                AreEqual(c.channel, () => channelEnum.EnumToChannel());
            }
            else
            {
                Fail($"Unsupported combination of values: {new{ channelEnum, c.channels, c.channel }}");
            }
        }
            
        private void Assert_Immutable_Getters(Channel channelEntity, (int channels, int? channel) c)
        {
            if (c.channels == MonoChannels)
            {
                IsTrue (() => channelEntity.IsMono  ());
                IsTrue (() => channelEntity.IsCenter());
                
                IsFalse(() => channelEntity.IsStereo());
                IsFalse(() => channelEntity.IsLeft  ());
                IsFalse(() => channelEntity.IsRight ());

                AreEqual(MonoChannels, () => channelEntity.Channels());
                AreEqual(CenterChannel, () => channelEntity.Channel());
            }
            else if (c.channels == StereoChannels)
            {
                IsTrue(() => channelEntity.IsStereo());
                AreEqual(c.channel == 0, () => channelEntity.IsLeft());
                AreEqual(c.channel == 1, () => channelEntity.IsRight());
                
                IsFalse(() => channelEntity.IsMono());
                IsFalse(() => channelEntity.IsCenter());

                AreEqual(StereoChannels, () => channelEntity.Channels());
                AreEqual(StereoChannels, () => channelEntity.ChannelEntityToChannels());

                AreEqual(c.channel, () => channelEntity.Channel());
                AreEqual(c.channel, () => channelEntity.EntityToChannel());
            }
            else
            {
                Fail($"Unsupported combination of values: {channelEntity?.ID} - {channelEntity?.Name}, {new{ c.channels, c.channel }}");
            }
        }
        
        // Test Data Helpers

        private ConfigTestEntities CreateTestEntities((int? channels, int? channel) c)
            => new ConfigTestEntities(x => x.WithChannels(c.channels)
                                            .WithChannel (c.channel));
        
        // ncrunch: no coverage start

        static object CaseKeys            => _cases           .Select(x => new object[] { x.Descriptor }).ToArray();
        static object CaseKeysInit        => _casesInit       .Select(x => new object[] { x.Descriptor }).ToArray();
        static object CaseKeysWithEmpties => _casesWithEmpties.Select(x => new object[] { x.Descriptor }).ToArray();

        static Case[] _casesInit =
        {
            // Stereo configurations
            new Case (2,0),
            new Case (2,1),
            new Case (2,_),

            // Mono: channel ignored (defaults to CenterChannel)
            new Case ( (1,_), (1,0) ),
            new Case   (1,0),
            new Case ( (1,1), (1,0) ),
            
            // All Mono: null / 0 Channels => defaults to Mono => ignores the channel.
            new Case ( (_,_) , (1,0) ),
            new Case ( (0,_) , (1,0) ), 
            new Case ( (_,0) , (1,0) ), 
            new Case ( (0,0) , (1,0) ), 
            new Case ( (_,1) , (1,0) ), 
            new Case ( (0,1) , (1,0) ) 
        };

        static Case[] _cases =
        {
            new Case( init:(1,0) , val:(2,0) ),
            new Case( init:(1,0) , val:(2,1) ),
            new Case( init:(1,0) , val:(2,_) ),
                                             
            new Case( init:(2,0) , val:(1,0) ),
            new Case( init:(2,0) , val:(2,1) ),
            new Case( init:(2,0) , val:(2,_) ),
                                             
            new Case( init:(2,1) , val:(1,0) ),
            new Case( init:(2,1) , val:(2,0) ),
            new Case( init:(2,1) , val:(2,_) ),
                                             
            new Case( init:(2,_) , val:(1,0) ),
            new Case( init:(2,_) , val:(2,0) ),
            new Case( init:(2,_) , val:(2,1) ),
        };

        static Case[] _casesWithEmpties = _cases.Concat(new[]
        {
            // Most vals should all coalesce to Mono: null / 0 / 1 channels => defaults to Mono => ignores the channel.
            new Case( init: (2,1)          , val: ((_,_), (1,0)) ),
            new Case( init: (2,0)          , val: ((0,_), (1,0)) ),
            new Case( init: (2,_)          , val: ((1,1), (1,0)) ),
            new Case( init: ((_,_), (1,0)) , val: (2,1)          )
        }).ToArray();
        
        static Dictionary<string, Case> _caseDictionary = _casesWithEmpties.Union(_casesInit).ToDictionary(x => x.Descriptor);

        struct Case
        {
            private int? _fromChannelsNully;
            private int  _fromChannelsCoalesced;
            private int? _fromChannelNully;
            private int? _fromChannelCoalesced;
            private int? _toChannelsNully;
            private int  _toChannelsCoalesced;
            private int? _toChannelNully;
            private int? _toChannelCoalesced;
            
            public readonly Values init;
            public readonly Values val;
            
            public string Descriptor { get; }

            public Case(
                ((int? channels, int? channel) nully, (int? channels, int? channel) coalesce) init,
                ((int? channels, int? channel) nully, (int? channels, int? channel) coalesce) val )
                : this(fromChannelsNully     : init.nully   .channels,
                       fromChannelsCoalesced : init.coalesce.channels,
                       fromChannelNully      : init.nully   .channel ,
                       fromChannelCoalesced  : init.coalesce.channel ,
                       toChannelsNully       : val .nully   .channels,
                       toChannelsCoalesced   : val .coalesce.channels,
                       toChannelNully        : val .nully   .channel ,
                       toChannelCoalesced    : val .coalesce.channel ) { }

            public Case(
                ((int? channels, int? channel) nully, (int? channels, int? channel) coalesce) init,
                ( int? channels, int? channel) val) 
                : this(fromChannelsNully     : init.nully   .channels,
                       fromChannelsCoalesced : init.coalesce.channels,
                       fromChannelNully      : init.nully   .channel ,
                       fromChannelCoalesced  : init.coalesce.channel ,
                       toChannelsNully       : val          .channels,
                       toChannelsCoalesced   : val          .channels,
                       toChannelNully        : val          .channel,
                       toChannelCoalesced    : val          .channel) { }

            public Case(
                ( int? channels, int? channel) init,
                ((int? channels, int? channel) nully, (int? channels, int? channel) coalesce) val )
                : this(fromChannelsNully     : init         .channels,
                       fromChannelsCoalesced : init         .channels,
                       fromChannelNully      : init         .channel,
                       fromChannelCoalesced  : init         .channel,
                       toChannelsNully       : val .nully   .channels,
                       toChannelsCoalesced   : val .coalesce.channels,
                       toChannelNully        : val .nully   .channel ,
                       toChannelCoalesced    : val .coalesce.channel )
            { }
            
            public Case(
                (int? channels, int? channel) init,
                (int? channels, int? channel) val) 
                : this(fromChannelsNully     : init.channels,
                       fromChannelsCoalesced : init.channels,
                       fromChannelNully      : init.channel,
                       fromChannelCoalesced  : init.channel,
                       toChannelsNully       : val .channels,
                       toChannelsCoalesced   : val .channels,
                       toChannelNully        : val .channel,
                       toChannelCoalesced    : val .channel) { }

            public Case(int? channels, int? channel)
                : this(fromChannelsNully     : channels,
                       fromChannelsCoalesced : channels,
                       fromChannelNully      : channel,
                       fromChannelCoalesced  : channel,
                       toChannelsNully       : channels,
                       toChannelsCoalesced   : channels,
                       toChannelNully        : channel,
                       toChannelCoalesced    : channel) { }
                
            public Case(
                int? fromChannelsNully, int? fromChannelsCoalesced, int? fromChannelNully, int? fromChannelCoalesced, 
                int? toChannelsNully,   int? toChannelsCoalesced,   int? toChannelNully,   int? toChannelCoalesced)
            {
                if (fromChannelsCoalesced == default && fromChannelCoalesced == default)
                {
                    fromChannelsCoalesced = fromChannelsNully; 
                    fromChannelCoalesced  = fromChannelNully;
                }
                
                if (toChannelsCoalesced == default && toChannelCoalesced == default)
                {
                    toChannelsCoalesced = toChannelsNully;
                    toChannelCoalesced  = toChannelNully;
                }

                _fromChannelsNully     = fromChannelsNully;
                _fromChannelsCoalesced = fromChannelsCoalesced ?? 0;
                _fromChannelNully      = fromChannelNully;
                _fromChannelCoalesced  = fromChannelCoalesced;
                _toChannelsNully       = toChannelsNully;
                _toChannelsCoalesced   = toChannelsCoalesced ?? 0;
                _toChannelNully        = toChannelNully;
                _toChannelCoalesced    = toChannelCoalesced;
                
                init = new Values(_fromChannelsNully, _fromChannelNully, _fromChannelsCoalesced, _fromChannelCoalesced);
                val  = new Values(_toChannelsNully,   _toChannelNully,   _toChannelsCoalesced,   _toChannelCoalesced);
                
                Descriptor = $"({init.channels.nully},{init.channel.nully}) => ({val.channels.nully},{val.channel.nully})";
            }
        }
 
        struct Values
        {
            private readonly int? _channelsNully;
            private readonly int  _channelsCoalesced;
            private readonly int? _channelNully;
            private readonly int? _channelCoalesced;
            
            public readonly (int? nully   , int  coalesce) channels;
            public readonly (int? nully   , int? coalesce) channel ;
            public readonly (int? channels, int? channel)  nully   ;
            public readonly (int  channels, int? channel)  coalesce;

            public Values( 
                int? channelsNully, int? channelNully, 
                int  channelsCoalesced, int? channelCoalesced )
            {
                _channelsNully     = channelsNully;
                _channelNully      = channelNully;
                _channelsCoalesced = channelsCoalesced;
                _channelCoalesced  = channelCoalesced;
                
                channels = (    _channelsNully, _channelsCoalesced);
                channel  = (     _channelNully,  _channelCoalesced);
                nully    = (    _channelsNully,      _channelNully);
                coalesce = (_channelsCoalesced,  _channelCoalesced);
            }
        }
       
        // ncrunch: no coverage end
    } 
}