using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Framework.Testing;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using JJ.Framework.Common;
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
                Assert_BuffBound_Getters_Complete(x, init.coalesce);
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
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes                  .Channel    (x.SynthWishes   , val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes                  .Channel    (x.FlowNode      , val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor          .Channel    (x.ConfigResolver, val.channel.nully).Channels    (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes                  .WithChannel(x.SynthWishes   , val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes                  .WithChannel(x.FlowNode      , val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor          .WithChannel(x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes                  .AsChannel  (x.SynthWishes   , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes                  .AsChannel  (x.FlowNode      , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor          .AsChannel  (x.ConfigResolver, val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes                  .SetChannel (x.SynthWishes   , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes                  .SetChannel (x.FlowNode      , val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor          .SetChannel (x.ConfigResolver, val.channel.nully).SetChannels (val.channels.nully)));
            AssertProp(x => AreEqual(x.SynthWishes,    ChannelExtensionWishes        .WithChannel(x.SynthWishes   , val.channel.nully).WithChannels(val.channels.nully))); // These are shadowed by class-native members. Call as static instead.
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
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Center          ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Left            ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Right           ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AnyChannel     ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .Channel         (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Center          ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Left            ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Right           ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .EveryChannel    ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .Channel         (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Center          ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Left            ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Right           ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .NoChannel       ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .Channel         (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .Center          ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .Left            ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .Right           ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .AnyChannel      ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .Channel         (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .Center          ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .Left            ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .Right           ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .EveryChannel    ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .Channel         (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .Center          ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .Left            ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .Right           ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .NoChannel       ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .Channel         (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Center          ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Left            ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Right           ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AnyChannel      ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.Channel         (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Center          ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Left            ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Right           ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.EveryChannel    ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.Channel         (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Center          ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Left            ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Right           ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.NoChannel       ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.Channel         (val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithCenter      ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithLeft        ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithRight       ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithAnyChannel  ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithChannel     (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithCenter      ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithLeft        ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithRight       ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithEveryChannel()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithChannel     (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithCenter      ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithLeft        ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithRight       ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithNoChannel   ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithChannel     (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithCenter      ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithLeft        ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithRight       ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithAnyChannel  ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .WithChannel     (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithCenter      ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithLeft        ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithRight       ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithEveryChannel()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .WithChannel     (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithCenter      ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithLeft        ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithRight       ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .WithNoChannel   ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .WithChannel     (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithCenter      ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithLeft        ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithRight       ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithAnyChannel   ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithChannel     (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithCenter      ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithLeft        ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithRight       ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithEveryChannel()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithChannel     (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithCenter      ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithLeft        ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithRight       ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithNoChannel   ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithChannel     (val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsCenter        ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsLeft          ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsRight         ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsAnyChannel    ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsChannel       (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsCenter        ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsLeft          ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsRight         ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsEveryChannel  ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsChannel       (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsCenter        ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsLeft          ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsRight         ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsNoChannel     ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsChannel       (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsCenter        ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsLeft          ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsRight         ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsAnyChannel    ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .AsChannel       (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsCenter        ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsLeft          ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsRight         ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsEveryChannel  ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .AsChannel       (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsCenter        ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsLeft          ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsRight         ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .AsNoChannel     ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .AsChannel       (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsCenter        ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsLeft          ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsRight         ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsAnyChannel    ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsChannel       (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsCenter        ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsLeft          ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsRight         ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsEveryChannel  ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsChannel       (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsCenter        ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsLeft          ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsRight         ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsNoChannel     ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsChannel       (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetCenter       ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetLeft         ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetRight        ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetAnyChannel   ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetChannel      (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetCenter       ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetLeft         ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetRight        ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetEveryChannel ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetChannel      (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetCenter       ());
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetLeft         ());
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetRight        ());
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetNoChannel    ()); 
                         else                         AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetChannel      (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetCenter       ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetLeft         ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetRight        ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetAnyChannel   ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .SetChannel      (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetCenter       ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetLeft         ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetRight        ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetEveryChannel ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .SetChannel      (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetCenter       ());
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetLeft         ());
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetRight        ()); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => x.FlowNode      .SetNoChannel    ()); 
                         else                         AreEqual(x.FlowNode,       () => x.FlowNode      .SetChannel      (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetCenter       ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetLeft         ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetRight        ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetAnyChannel   ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetChannel      (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetCenter       ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetLeft         ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetRight        ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetEveryChannel ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetChannel      (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetCenter       ());
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetLeft         ());
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetRight        ()); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetNoChannel    ()); 
                         else                         AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetChannel      (val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => Center          (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => Left            (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => Right           (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => AnyChannel      (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => Channel         (x.SynthWishes,      val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => Center          (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => Left            (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => Right           (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => EveryChannel    (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => Channel         (x.SynthWishes,      val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => Center          (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => Left            (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => Right           (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => NoChannel       (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => Channel         (x.SynthWishes,      val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => Center          (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => Left            (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => Right           (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => AnyChannel      (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => Channel         (x.FlowNode,         val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => Center          (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => Left            (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => Right           (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => EveryChannel    (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => Channel         (x.FlowNode,         val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => Center          (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => Left            (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => Right           (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => NoChannel       (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => Channel         (x.FlowNode,         val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => Center          (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => Left            (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => Right           (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => AnyChannel      (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => Channel         (x.ConfigResolver,   val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => Center          (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => Left            (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => Right           (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => EveryChannel    (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => Channel         (x.ConfigResolver,   val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => Center          (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => Left            (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => Right           (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => NoChannel       (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => Channel         (x.ConfigResolver,   val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => WithCenter      (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => WithLeft        (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => WithRight       (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => WithAnyChannel  (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => WithChannel     (x.SynthWishes,      val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => WithCenter      (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => WithLeft        (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => WithRight       (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => WithEveryChannel(x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => WithChannel     (x.SynthWishes,      val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => WithCenter      (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => WithLeft        (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => WithRight       (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => WithNoChannel   (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => WithChannel     (x.SynthWishes,      val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => WithCenter      (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => WithLeft        (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => WithRight       (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => WithAnyChannel  (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => WithChannel     (x.FlowNode,         val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => WithCenter      (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => WithLeft        (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => WithRight       (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => WithEveryChannel(x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => WithChannel     (x.FlowNode,         val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => WithCenter      (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => WithLeft        (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => WithRight       (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => WithNoChannel   (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => WithChannel     (x.FlowNode,         val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => WithCenter      (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => WithLeft        (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => WithRight       (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => WithAnyChannel  (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => WithChannel     (x.ConfigResolver,   val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => WithCenter      (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => WithLeft        (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => WithRight       (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => WithEveryChannel(x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => WithChannel     (x.ConfigResolver,   val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => WithCenter      (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => WithLeft        (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => WithRight       (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => WithNoChannel   (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => WithChannel     (x.ConfigResolver,   val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => AsCenter        (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => AsLeft          (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => AsRight         (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => AsAnyChannel    (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => AsChannel       (x.SynthWishes,      val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => AsCenter        (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => AsLeft          (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => AsRight         (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => AsEveryChannel  (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => AsChannel       (x.SynthWishes,      val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => AsCenter        (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => AsLeft          (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => AsRight         (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => AsNoChannel     (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => AsChannel       (x.SynthWishes,      val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => AsCenter        (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => AsLeft          (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => AsRight         (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => AsAnyChannel    (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => AsChannel       (x.FlowNode,         val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => AsCenter        (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => AsLeft          (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => AsRight         (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => AsEveryChannel  (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => AsChannel       (x.FlowNode,         val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => AsCenter        (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => AsLeft          (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => AsRight         (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => AsNoChannel     (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => AsChannel       (x.FlowNode,         val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => AsCenter        (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => AsLeft          (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => AsRight         (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => AsAnyChannel    (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => AsChannel       (x.ConfigResolver,   val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => AsCenter        (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => AsLeft          (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => AsRight         (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => AsEveryChannel  (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => AsChannel       (x.ConfigResolver,   val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => AsCenter        (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => AsLeft          (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => AsRight         (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => AsNoChannel     (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => AsChannel       (x.ConfigResolver,   val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => SetCenter       (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => SetLeft         (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => SetRight        (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => SetAnyChannel   (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => SetChannel      (x.SynthWishes,      val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => SetCenter       (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => SetLeft         (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => SetRight        (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => SetEveryChannel (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => SetChannel      (x.SynthWishes,      val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => SetCenter       (x.SynthWishes       ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => SetLeft         (x.SynthWishes       ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => SetRight        (x.SynthWishes       ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => SetNoChannel    (x.SynthWishes       )); 
                         else                         AreEqual(x.SynthWishes,    () => SetChannel      (x.SynthWishes,      val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => SetCenter       (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => SetLeft         (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => SetRight        (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => SetAnyChannel   (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => SetChannel      (x.FlowNode,         val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => SetCenter       (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => SetLeft         (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => SetRight        (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => SetEveryChannel (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => SetChannel      (x.FlowNode,         val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => SetCenter       (x.FlowNode          ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => SetLeft         (x.FlowNode          ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => SetRight        (x.FlowNode          )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => SetNoChannel    (x.FlowNode          )); 
                         else                         AreEqual(x.FlowNode,       () => SetChannel      (x.FlowNode,         val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => SetCenter       (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => SetLeft         (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => SetRight        (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => SetAnyChannel   (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => SetChannel      (x.ConfigResolver,   val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => SetCenter       (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => SetLeft         (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => SetRight        (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => SetEveryChannel (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => SetChannel      (x.ConfigResolver,   val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => SetCenter       (x.ConfigResolver    ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => SetLeft         (x.ConfigResolver    ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => SetRight        (x.ConfigResolver    )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => SetNoChannel    (x.ConfigResolver    )); 
                         else                         AreEqual(x.ConfigResolver, () => SetChannel      (x.ConfigResolver,   val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Center          (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Left            (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Right           (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AnyChannel      (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .Channel         (x.SynthWishes,    val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Center          (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Left            (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Right           (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .EveryChannel    (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .Channel         (x.SynthWishes,    val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Center          (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Left            (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .Right           (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .NoChannel       (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .Channel         (x.SynthWishes,    val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .Center          (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .Left            (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .Right           (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .AnyChannel      (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .Channel         (x.FlowNode,       val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .Center          (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .Left            (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .Right           (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .EveryChannel    (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .Channel         (x.FlowNode,       val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .Center          (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .Left            (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .Right           (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .NoChannel       (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .Channel         (x.FlowNode,       val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Center          (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Left            (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Right           (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AnyChannel      (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Channel         (x.ConfigResolver, val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Center          (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Left            (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Right           (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.EveryChannel    (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Channel         (x.ConfigResolver, val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Center          (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Left            (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Right           (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.NoChannel       (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Channel         (x.ConfigResolver, val.channel.nully).Channels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithCenter      (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithLeft        (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithRight       (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithAnyChannel  (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .WithChannel     (x.SynthWishes,    val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithCenter      (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithLeft        (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithRight       (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithEveryChannel(x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .WithChannel     (x.SynthWishes,    val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithCenter      (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithLeft        (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithRight       (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithNoChannel   (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .WithChannel     (x.SynthWishes,    val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithCenter      (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithLeft        (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithRight       (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithAnyChannel  (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .WithChannel     (x.FlowNode,       val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithCenter      (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithLeft        (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithRight       (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithEveryChannel(x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .WithChannel     (x.FlowNode,       val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithCenter      (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithLeft        (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithRight       (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .WithNoChannel   (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .WithChannel     (x.FlowNode,       val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithCenter      (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithLeft        (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithRight       (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithAnyChannel  (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithChannel     (x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithCenter      (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithLeft        (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithRight       (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithEveryChannel(x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithChannel     (x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithCenter      (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithLeft        (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithRight       (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithNoChannel   (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithChannel     (x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsCenter        (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsLeft          (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsRight         (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsAnyChannel    (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .AsChannel       (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsCenter        (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsLeft          (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsRight         (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsEveryChannel  (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .AsChannel       (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsCenter        (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsLeft          (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsRight         (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsNoChannel     (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .AsChannel       (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsCenter        (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsLeft          (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsRight         (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsAnyChannel    (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .AsChannel       (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsCenter        (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsLeft          (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsRight         (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsEveryChannel  (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .AsChannel       (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsCenter        (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsLeft          (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsRight         (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .AsNoChannel     (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .AsChannel       (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsCenter        (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsLeft          (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsRight         (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsAnyChannel    (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsChannel       (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsCenter        (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsLeft          (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsRight         (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsEveryChannel  (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsChannel       (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsCenter        (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsLeft          (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsRight         (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsNoChannel     (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsChannel       (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetCenter       (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetLeft         (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetRight        (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetAnyChannel   (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .SetChannel      (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetCenter       (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetLeft         (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetRight        (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetEveryChannel (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .SetChannel      (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetCenter       (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetLeft         (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetRight        (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetNoChannel    (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ConfigWishes        .SetChannel      (x.SynthWishes,    val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetCenter       (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetLeft         (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetRight        (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetAnyChannel  (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .SetChannel      (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetCenter       (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetLeft         (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetRight        (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetEveryChannel (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .SetChannel      (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetCenter       (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetLeft         (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetRight        (x.FlowNode        )); 
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ConfigWishes        .SetNoChannel    (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ConfigWishes        .SetChannel      (x.FlowNode,       val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetCenter       (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetLeft         (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetRight        (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetAnyChannel   (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetChannel      (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetCenter       (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetLeft         (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetRight        (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetEveryChannel (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetChannel      (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetCenter       (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetLeft         (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetRight        (x.ConfigResolver  )); 
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetNoChannel    (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetChannel      (x.ConfigResolver, val.channel.nully).SetChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithCenter      (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithLeft        (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithRight       (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithAnyChannel  (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithChannel     (x.SynthWishes,    val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithCenter      (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithLeft        (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithRight       (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithEveryChannel(x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithChannel     (x.SynthWishes,    val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithCenter      (x.SynthWishes     ));
                         else if (val.nully == (2,0)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithLeft        (x.SynthWishes     ));
                         else if (val.nully == (2,1)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithRight       (x.SynthWishes     ));
                         else if (val.nully == (2,_)) AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithNoChannel   (x.SynthWishes     )); 
                         else                         AreEqual(x.SynthWishes,    () => ChannelExtensionWishes        .WithChannel     (x.SynthWishes,    val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithCenter      (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithLeft        (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithRight       (x.FlowNode        ));
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithAnyChannel  (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithChannel     (x.FlowNode,       val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithCenter      (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithLeft        (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithRight       (x.FlowNode        ));
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithEveryChannel(x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithChannel     (x.FlowNode,       val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithCenter      (x.FlowNode        ));
                         else if (val.nully == (2,0)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithLeft        (x.FlowNode        ));
                         else if (val.nully == (2,1)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithRight       (x.FlowNode        ));
                         else if (val.nully == (2,_)) AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithNoChannel   (x.FlowNode        )); 
                         else                         AreEqual(x.FlowNode,       () => ChannelExtensionWishes        .WithChannel     (x.FlowNode,       val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithCenter      (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithLeft        (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithRight       (x.ConfigResolver  ));
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithAnyChannel  (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithChannel     (x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithCenter      (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithLeft        (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithRight       (x.ConfigResolver  ));
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithEveryChannel(x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithChannel     (x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)); });
            AssertProp(x => { if (val.nully == (1,0)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithCenter      (x.ConfigResolver  ));
                         else if (val.nully == (2,0)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithLeft        (x.ConfigResolver  ));
                         else if (val.nully == (2,1)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithRight       (x.ConfigResolver  ));
                         else if (val.nully == (2,_)) AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithNoChannel   (x.ConfigResolver  )); 
                         else                         AreEqual(x.ConfigResolver, () => ChannelExtensionWishesAccessor.WithChannel     (x.ConfigResolver, val.channel.nully).WithChannels(val.channels.nully)); });
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
                Assert_TapeBound_Getters_Single(x, val);
                Assert_BuffBound_Getters_Complete(x, init);
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
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .Center          ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .Left            ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .Right           ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .AnyChannel      ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .Center          ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .Left            ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .Right           ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .EveryChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .Center          ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .Left            ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .Right           ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .NoChannel       ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Center          ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Left            ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Right           ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AnyChannel      ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Center          ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Left            ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Right           ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .EveryChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Center          ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Left            ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .Right           ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .NoChannel       ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.Center          ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.Left            ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.Right           ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.AnyChannel      ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.Center          ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.Left            ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.Right           ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.EveryChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.Center          ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.Left            ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.Right           ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.NoChannel       ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .Center          ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .Left            ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .Right           ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .AnyChannel      ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .Center          ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .Left            ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .Right           ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .EveryChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .Center          ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .Left            ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .Right           ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .NoChannel       ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .WithCenter      ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .WithLeft        ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .WithRight       ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .WithAnyChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .WithCenter      ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .WithLeft        ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .WithRight       ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .WithEveryChannel()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .WithCenter      ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .WithLeft        ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .WithRight       ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .WithNoChannel   ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithCenter      ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithLeft        ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithRight       ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithAnyChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithCenter      ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithLeft        ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithRight       ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithEveryChannel()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithCenter      ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithLeft        ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithRight       ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .WithNoChannel   ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.WithCenter      ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.WithLeft        ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.WithRight       ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.WithAnyChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.WithCenter      ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.WithLeft        ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.WithRight       ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.WithEveryChannel()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.WithCenter      ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.WithLeft        ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.WithRight       ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.WithNoChannel   ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .WithCenter      ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .WithLeft        ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .WithRight       ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .WithAnyChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .WithCenter      ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .WithLeft        ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .WithRight       ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .WithEveryChannel()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .WithCenter      ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .WithLeft        ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .WithRight       ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .WithNoChannel   ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .AsCenter        ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .AsLeft          ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .AsRight         ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .AsAnyChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .AsCenter        ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .AsLeft          ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .AsRight         ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .AsEveryChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .AsCenter        ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .AsLeft          ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .AsRight         ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .AsNoChannel     ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsCenter        ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsLeft          ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsRight         ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsAnyChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsCenter        ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsLeft          ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsRight         ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsEveryChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsCenter        ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsLeft          ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsRight         ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .AsNoChannel     ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.AsCenter        ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.AsLeft          ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.AsRight         ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.AsAnyChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.AsCenter        ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.AsLeft          ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.AsRight         ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.AsEveryChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.AsCenter        ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.AsLeft          ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.AsRight         ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.AsNoChannel     ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .AsCenter        ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .AsLeft          ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .AsRight         ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .AsAnyChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .AsCenter        ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .AsLeft          ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .AsRight         ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .AsEveryChannel  ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .AsCenter        ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .AsLeft          ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .AsRight         ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .AsNoChannel     ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .SetCenter       ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .SetLeft         ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .SetRight        ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .SetAnyChannel   ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .SetCenter       ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .SetLeft         ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .SetRight        ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .SetEveryChannel ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => x.Tape       .SetCenter       ());
                              if (val == (2,0)) AreEqual(x.Tape,        () => x.Tape       .SetLeft         ());
                              if (val == (2,1)) AreEqual(x.Tape,        () => x.Tape       .SetRight        ());
                              if (val == (2,_)) AreEqual(x.Tape,        () => x.Tape       .SetNoChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetCenter       ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetLeft         ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetRight        ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetAnyChannel   ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetCenter       ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetLeft         ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetRight        ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetEveryChannel ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetCenter       ());
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetLeft         ());
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetRight        ());
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => x.TapeConfig .SetNoChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.SetCenter       ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.SetLeft         ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.SetRight        ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.SetAnyChannel   ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.SetCenter       ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.SetLeft         ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.SetRight        ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.SetEveryChannel ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => x.TapeActions.SetCenter       ());
                              if (val == (2,0)) AreEqual(x.TapeActions, () => x.TapeActions.SetLeft         ());
                              if (val == (2,1)) AreEqual(x.TapeActions, () => x.TapeActions.SetRight        ());
                              if (val == (2,_)) AreEqual(x.TapeActions, () => x.TapeActions.SetNoChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .SetCenter       ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .SetLeft         ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .SetRight        ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .SetAnyChannel   ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .SetCenter       ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .SetLeft         ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .SetRight        ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .SetEveryChannel ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => x.TapeAction .SetCenter       ());
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => x.TapeAction .SetLeft         ());
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => x.TapeAction .SetRight        ());
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => x.TapeAction .SetNoChannel    ()); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => Center          (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => Left            (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => Right           (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => AnyChannel      (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => Center          (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => Left            (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => Right           (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => EveryChannel    (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => Center          (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => Left            (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => Right           (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => NoChannel       (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => Center          (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => Left            (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => Right           (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => AnyChannel      (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => Center          (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => Left            (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => Right           (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => EveryChannel    (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => Center          (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => Left            (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => Right           (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => NoChannel       (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => Center          (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => Left            (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => Right           (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => AnyChannel      (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => Center          (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => Left            (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => Right           (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => EveryChannel    (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => Center          (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => Left            (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => Right           (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => NoChannel       (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => Center          (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => Left            (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => Right           (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => AnyChannel      (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => Center          (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => Left            (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => Right           (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => EveryChannel    (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => Center          (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => Left            (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => Right           (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => NoChannel       (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => WithCenter      (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => WithLeft        (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => WithRight       (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => WithAnyChannel  (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => WithCenter      (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => WithLeft        (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => WithRight       (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => WithEveryChannel(x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => WithCenter      (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => WithLeft        (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => WithRight       (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => WithNoChannel   (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => WithCenter      (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => WithLeft        (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => WithRight       (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => WithAnyChannel  (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => WithCenter      (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => WithLeft        (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => WithRight       (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => WithEveryChannel(x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => WithCenter      (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => WithLeft        (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => WithRight       (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => WithNoChannel   (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => WithCenter      (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => WithLeft        (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => WithRight       (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => WithAnyChannel  (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => WithCenter      (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => WithLeft        (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => WithRight       (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => WithEveryChannel(x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => WithCenter      (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => WithLeft        (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => WithRight       (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => WithNoChannel   (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => WithCenter      (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => WithLeft        (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => WithRight       (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => WithAnyChannel  (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => WithCenter      (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => WithLeft        (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => WithRight       (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => WithEveryChannel(x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => WithCenter      (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => WithLeft        (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => WithRight       (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => WithNoChannel   (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => AsCenter        (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => AsLeft          (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => AsRight         (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => AsAnyChannel    (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => AsCenter        (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => AsLeft          (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => AsRight         (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => AsEveryChannel  (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => AsCenter        (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => AsLeft          (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => AsRight         (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => AsNoChannel     (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => AsCenter        (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => AsLeft          (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => AsRight         (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => AsAnyChannel    (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => AsCenter        (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => AsLeft          (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => AsRight         (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => AsEveryChannel  (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => AsCenter        (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => AsLeft          (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => AsRight         (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => AsNoChannel     (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => AsCenter        (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => AsLeft          (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => AsRight         (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => AsAnyChannel    (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => AsCenter        (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => AsLeft          (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => AsRight         (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => AsEveryChannel  (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => AsCenter        (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => AsLeft          (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => AsRight         (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => AsNoChannel     (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => AsCenter        (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => AsLeft          (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => AsRight         (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => AsAnyChannel    (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => AsCenter        (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => AsLeft          (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => AsRight         (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => AsEveryChannel  (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => AsCenter        (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => AsLeft          (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => AsRight         (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => AsNoChannel     (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => SetCenter       (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => SetLeft         (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => SetRight        (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => SetAnyChannel   (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => SetCenter       (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => SetLeft         (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => SetRight        (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => SetEveryChannel (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => SetCenter       (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => SetLeft         (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => SetRight        (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => SetNoChannel    (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => SetCenter       (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => SetLeft         (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => SetRight        (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => SetAnyChannel   (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => SetCenter       (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => SetLeft         (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => SetRight        (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => SetEveryChannel (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => SetCenter       (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => SetLeft         (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => SetRight        (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => SetNoChannel    (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => SetCenter       (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => SetLeft         (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => SetRight        (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => SetAnyChannel   (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => SetCenter       (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => SetLeft         (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => SetRight        (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => SetEveryChannel (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => SetCenter       (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => SetLeft         (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => SetRight        (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => SetNoChannel    (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => SetCenter       (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => SetLeft         (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => SetRight        (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => SetAnyChannel   (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => SetCenter       (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => SetLeft         (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => SetRight        (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => SetEveryChannel (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => SetCenter       (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => SetLeft         (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => SetRight        (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => SetNoChannel    (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.Center          (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.Left            (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.Right           (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.AnyChannel      (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.Center          (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.Left            (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.Right           (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.EveryChannel    (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.Center          (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.Left            (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.Right           (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.NoChannel       (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.Center          (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.Left            (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.Right           (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.AnyChannel      (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.Center          (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.Left            (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.Right           (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.EveryChannel    (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.Center          (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.Left            (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.Right           (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.NoChannel       (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.Center          (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.Left            (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.Right           (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.AnyChannel      (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.Center          (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.Left            (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.Right           (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.EveryChannel    (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.Center          (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.Left            (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.Right           (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.NoChannel       (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.Center          (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.Left            (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.Right           (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.AnyChannel      (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.Center          (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.Left            (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.Right           (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.EveryChannel    (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.Center          (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.Left            (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.Right           (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.NoChannel       (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.WithCenter      (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.WithLeft        (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.WithRight       (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.WithAnyChannel  (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.WithCenter      (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.WithLeft        (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.WithRight       (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.WithEveryChannel(x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.WithCenter      (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.WithLeft        (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.WithRight       (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.WithNoChannel   (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithCenter      (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithLeft        (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithRight       (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithAnyChannel  (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithCenter      (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithLeft        (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithRight       (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithEveryChannel(x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithCenter      (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithLeft        (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithRight       (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.WithNoChannel   (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.WithCenter      (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.WithLeft        (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.WithRight       (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.WithAnyChannel  (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.WithCenter      (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.WithLeft        (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.WithRight       (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.WithEveryChannel(x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.WithCenter      (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.WithLeft        (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.WithRight       (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.WithNoChannel   (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.WithCenter      (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.WithLeft        (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.WithRight       (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.WithAnyChannel  (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.WithCenter      (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.WithLeft        (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.WithRight       (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.WithEveryChannel(x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.WithCenter      (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.WithLeft        (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.WithRight       (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.WithNoChannel   (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.AsCenter        (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.AsLeft          (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.AsRight         (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.AsAnyChannel    (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.AsCenter        (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.AsLeft          (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.AsRight         (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.AsEveryChannel  (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.AsCenter        (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.AsLeft          (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.AsRight         (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.AsNoChannel     (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsCenter        (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsLeft          (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsRight         (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsAnyChannel    (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsCenter        (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsLeft          (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsRight         (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsEveryChannel  (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsCenter        (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsLeft          (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsRight         (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.AsNoChannel     (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.AsCenter        (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.AsLeft          (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.AsRight         (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.AsAnyChannel    (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.AsCenter        (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.AsLeft          (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.AsRight         (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.AsEveryChannel  (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.AsCenter        (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.AsLeft          (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.AsRight         (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.AsNoChannel     (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.AsCenter        (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.AsLeft          (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.AsRight         (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.AsAnyChannel    (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.AsCenter        (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.AsLeft          (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.AsRight         (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.AsEveryChannel  (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.AsCenter        (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.AsLeft          (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.AsRight         (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.AsNoChannel     (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.SetCenter       (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.SetLeft         (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.SetRight        (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.SetAnyChannel   (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.SetCenter       (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.SetLeft         (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.SetRight        (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.SetEveryChannel (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Tape,        () => ConfigWishes.SetCenter       (x.Tape        ));
                              if (val == (2,0)) AreEqual(x.Tape,        () => ConfigWishes.SetLeft         (x.Tape        ));
                              if (val == (2,1)) AreEqual(x.Tape,        () => ConfigWishes.SetRight        (x.Tape        ));
                              if (val == (2,_)) AreEqual(x.Tape,        () => ConfigWishes.SetNoChannel    (x.Tape        )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetCenter       (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetLeft         (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetRight        (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetAnyChannel   (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetCenter       (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetLeft         (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetRight        (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetEveryChannel (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetCenter       (x.TapeConfig  ));
                              if (val == (2,0)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetLeft         (x.TapeConfig  ));
                              if (val == (2,1)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetRight        (x.TapeConfig  ));
                              if (val == (2,_)) AreEqual(x.TapeConfig,  () => ConfigWishes.SetNoChannel    (x.TapeConfig  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.SetCenter       (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.SetLeft         (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.SetRight        (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.SetAnyChannel   (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.SetCenter       (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.SetLeft         (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.SetRight        (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.SetEveryChannel (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeActions, () => ConfigWishes.SetCenter       (x.TapeActions ));
                              if (val == (2,0)) AreEqual(x.TapeActions, () => ConfigWishes.SetLeft         (x.TapeActions ));
                              if (val == (2,1)) AreEqual(x.TapeActions, () => ConfigWishes.SetRight        (x.TapeActions ));
                              if (val == (2,_)) AreEqual(x.TapeActions, () => ConfigWishes.SetNoChannel    (x.TapeActions )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.SetCenter       (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.SetLeft         (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.SetRight        (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.SetAnyChannel   (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.SetCenter       (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.SetLeft         (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.SetRight        (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.SetEveryChannel (x.TapeAction  )); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.TapeAction,  () => ConfigWishes.SetCenter       (x.TapeAction  ));
                              if (val == (2,0)) AreEqual(x.TapeAction,  () => ConfigWishes.SetLeft         (x.TapeAction  ));
                              if (val == (2,1)) AreEqual(x.TapeAction,  () => ConfigWishes.SetRight        (x.TapeAction  ));
                              if (val == (2,_)) AreEqual(x.TapeAction,  () => ConfigWishes.SetNoChannel    (x.TapeAction  )); });
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
                Assert_BuffBound_Getters_Single(x, val);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init);
            }
                        

            // By Design: Channels not set, avoiding interference with Channel state.
            // Getters assertions will work despite ambiguous states, because the same Channel number results in the same state.
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .Channel     (val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Channel     (val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .WithChannel (val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithChannel (val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .AsChannel   (val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsChannel   (val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => x.Buff           .SetChannel  (val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetChannel  (val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => Channel    (x.Buff,            val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => Channel    (x.AudioFileOutput, val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => WithChannel(x.Buff,            val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => WithChannel(x.AudioFileOutput, val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => AsChannel  (x.Buff,            val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => AsChannel  (x.AudioFileOutput, val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => SetChannel (x.Buff,            val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => SetChannel (x.AudioFileOutput, val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.Channel    (x.Buff,            val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.Channel    (x.AudioFileOutput, val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.WithChannel(x.Buff,            val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.WithChannel(x.AudioFileOutput, val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.AsChannel  (x.Buff,            val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.AsChannel  (x.AudioFileOutput, val.channel, context)));
            AssertProp(x => AreEqual(x.Buff,            () => ConfigWishes.SetChannel (x.Buff,            val.channel, context)));
            AssertProp(x => AreEqual(x.AudioFileOutput, () => ConfigWishes.SetChannel (x.AudioFileOutput, val.channel, context)));
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .Center          (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .Left            (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .Right           (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .AnyChannel      (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .Center          (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .Left            (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .Right           (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .EveryChannel    (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .Center          (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .Left            (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .Right           (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .NoChannel       (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Center          (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Left            (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Right           (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AnyChannel      (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Center          (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Left            (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Right           (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.EveryChannel    (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Center          (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Left            (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Right           (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.NoChannel       (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .WithCenter      (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .WithLeft        (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .WithRight       (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .WithAnyChannel  (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .WithCenter      (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .WithLeft        (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .WithRight       (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .WithEveryChannel(context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .WithCenter      (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .WithLeft        (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .WithRight       (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .WithNoChannel   (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithCenter      (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithLeft        (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithRight       (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithAnyChannel  (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithCenter      (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithLeft        (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithRight       (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithEveryChannel(context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithCenter      (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithLeft        (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithRight       (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.WithNoChannel   (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .AsCenter        (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .AsLeft          (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .AsRight         (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .AsAnyChannel    (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .AsCenter        (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .AsLeft          (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .AsRight         (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .AsEveryChannel  (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .AsCenter        (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .AsLeft          (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .AsRight         (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .AsNoChannel     (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsCenter        (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsLeft          (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsRight         (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsAnyChannel    (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsCenter        (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsLeft          (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsRight         (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsEveryChannel  (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsCenter        (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsLeft          (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsRight         (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.AsNoChannel     (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .SetCenter       (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .SetLeft         (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .SetRight        (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .SetAnyChannel   (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .SetCenter       (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .SetLeft         (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .SetRight        (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .SetEveryChannel (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => x.Buff           .SetCenter       (context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => x.Buff           .SetLeft         (context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => x.Buff           .SetRight        (context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => x.Buff           .SetNoChannel    (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetCenter       (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetLeft         (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetRight        (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetAnyChannel   (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetCenter       (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetLeft         (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetRight        (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetEveryChannel (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetCenter       (context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetLeft         (context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetRight        (context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.SetNoChannel    (context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => Center          (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => Left            (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => Right           (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => AnyChannel      (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => Center          (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => Left            (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => Right           (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => EveryChannel    (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => Center          (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => Left            (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => Right           (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => NoChannel       (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => Center          (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => Left            (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => Right           (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => AnyChannel      (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => Center          (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => Left            (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => Right           (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => EveryChannel    (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => Center          (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => Left            (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => Right           (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => NoChannel       (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => WithCenter      (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => WithLeft        (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => WithRight       (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => WithAnyChannel  (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => WithCenter      (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => WithLeft        (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => WithRight       (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => WithEveryChannel(x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => WithCenter      (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => WithLeft        (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => WithRight       (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => WithNoChannel   (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => WithCenter      (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => WithLeft        (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => WithRight       (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => WithAnyChannel  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => WithCenter      (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => WithLeft        (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => WithRight       (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => WithEveryChannel(x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => WithCenter      (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => WithLeft        (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => WithRight       (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => WithNoChannel   (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => AsCenter        (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => AsLeft          (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => AsRight         (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => AsAnyChannel    (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => AsCenter        (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => AsLeft          (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => AsRight         (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => AsEveryChannel  (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => AsCenter        (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => AsLeft          (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => AsRight         (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => AsNoChannel     (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => AsCenter        (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => AsLeft          (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => AsRight         (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => AsAnyChannel    (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => AsCenter        (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => AsLeft          (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => AsRight         (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => AsEveryChannel  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => AsCenter        (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => AsLeft          (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => AsRight         (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => AsNoChannel     (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => SetCenter       (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => SetLeft         (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => SetRight        (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => SetAnyChannel   (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => SetCenter       (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => SetLeft         (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => SetRight        (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => SetEveryChannel (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => SetCenter       (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => SetLeft         (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => SetRight        (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => SetNoChannel    (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => SetCenter       (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => SetLeft         (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => SetRight        (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => SetAnyChannel   (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => SetCenter       (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => SetLeft         (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => SetRight        (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => SetEveryChannel (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => SetCenter       (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => SetLeft         (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => SetRight        (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => SetNoChannel    (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.Center          (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.Left            (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.Right           (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.AnyChannel      (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.Center          (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.Left            (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.Right           (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.EveryChannel    (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.Center          (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.Left            (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.Right           (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.NoChannel       (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Center          (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Left            (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Right           (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AnyChannel      (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Center          (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Left            (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Right           (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.EveryChannel    (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Center          (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Left            (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.Right           (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.NoChannel       (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.WithCenter      (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.WithLeft        (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.WithRight       (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.WithAnyChannel  (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.WithCenter      (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.WithLeft        (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.WithRight       (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.WithEveryChannel(x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.WithCenter      (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.WithLeft        (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.WithRight       (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.WithNoChannel   (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithCenter      (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithLeft        (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithRight       (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithAnyChannel  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithCenter      (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithLeft        (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithRight       (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithEveryChannel(x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithCenter      (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithLeft        (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithRight       (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.WithNoChannel   (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.AsCenter        (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.AsLeft          (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.AsRight         (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.AsAnyChannel    (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.AsCenter        (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.AsLeft          (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.AsRight         (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.AsEveryChannel  (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.AsCenter        (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.AsLeft          (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.AsRight         (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.AsNoChannel     (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsCenter        (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsLeft          (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsRight         (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsAnyChannel    (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsCenter        (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsLeft          (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsRight         (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsEveryChannel  (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsCenter        (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsLeft          (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsRight         (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.AsNoChannel     (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.SetCenter       (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.SetLeft         (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.SetRight        (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.SetAnyChannel   (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.SetCenter       (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.SetLeft         (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.SetRight        (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.SetEveryChannel (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.Buff,            () => ConfigWishes.SetCenter       (x.Buff           , context));
                              if (val == (2,0)) AreEqual(x.Buff,            () => ConfigWishes.SetLeft         (x.Buff           , context));
                              if (val == (2,1)) AreEqual(x.Buff,            () => ConfigWishes.SetRight        (x.Buff           , context)); 
                              if (val == (2,_)) AreEqual(x.Buff,            () => ConfigWishes.SetNoChannel    (x.Buff           , context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetCenter       (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetLeft         (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetRight        (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetAnyChannel   (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetCenter       (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetLeft         (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetRight        (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetEveryChannel (x.AudioFileOutput, context)); });
            AssertProp(x => { if (val == (1,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetCenter       (x.AudioFileOutput, context));
                              if (val == (2,0)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetLeft         (x.AudioFileOutput, context));
                              if (val == (2,1)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetRight        (x.AudioFileOutput, context));
                              if (val == (2,_)) AreEqual(x.AudioFileOutput, () => ConfigWishes.SetNoChannel    (x.AudioFileOutput, context)); });
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
                AssertProp(() => { if (val == (1,0)) return channelEnum.Center          ();
                                   if (val == (2,0)) return channelEnum.Left            ();
                                   if (val == (2,1)) return channelEnum.Right           ();
                                   if (val == (2,_)) return channelEnum.AnyChannel      (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.Center          ();
                                   if (val == (2,0)) return channelEnum.Left            ();
                                   if (val == (2,1)) return channelEnum.Right           ();
                                   if (val == (2,_)) return channelEnum.EveryChannel    (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.Center          ();
                                   if (val == (2,0)) return channelEnum.Left            ();
                                   if (val == (2,1)) return channelEnum.Right           ();
                                   if (val == (2,_)) return channelEnum.NoChannel       (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.WithCenter      ();
                                   if (val == (2,0)) return channelEnum.WithLeft        ();
                                   if (val == (2,1)) return channelEnum.WithRight       ();
                                   if (val == (2,_)) return channelEnum.WithAnyChannel  (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.WithCenter      ();
                                   if (val == (2,0)) return channelEnum.WithLeft        ();
                                   if (val == (2,1)) return channelEnum.WithRight       ();
                                   if (val == (2,_)) return channelEnum.WithEveryChannel(); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.WithCenter      ();
                                   if (val == (2,0)) return channelEnum.WithLeft        ();
                                   if (val == (2,1)) return channelEnum.WithRight       ();
                                   if (val == (2,_)) return channelEnum.WithNoChannel   (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.AsCenter        ();
                                   if (val == (2,0)) return channelEnum.AsLeft          ();
                                   if (val == (2,1)) return channelEnum.AsRight         ();
                                   if (val == (2,_)) return channelEnum.AsAnyChannel    (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.AsCenter        ();
                                   if (val == (2,0)) return channelEnum.AsLeft          ();
                                   if (val == (2,1)) return channelEnum.AsRight         ();
                                   if (val == (2,_)) return channelEnum.AsEveryChannel  (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.AsCenter        ();
                                   if (val == (2,0)) return channelEnum.AsLeft          ();
                                   if (val == (2,1)) return channelEnum.AsRight         ();
                                   if (val == (2,_)) return channelEnum.AsNoChannel     (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.ToCenter        ();
                                   if (val == (2,0)) return channelEnum.ToLeft          ();
                                   if (val == (2,1)) return channelEnum.ToRight         ();
                                   if (val == (2,_)) return channelEnum.ToAnyChannel    (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.ToCenter        ();
                                   if (val == (2,0)) return channelEnum.ToLeft          ();
                                   if (val == (2,1)) return channelEnum.ToRight         ();
                                   if (val == (2,_)) return channelEnum.ToEveryChannel  (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.ToCenter        ();
                                   if (val == (2,0)) return channelEnum.ToLeft          ();
                                   if (val == (2,1)) return channelEnum.ToRight         ();
                                   if (val == (2,_)) return channelEnum.ToNoChannel     (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.SetCenter       ();
                                   if (val == (2,0)) return channelEnum.SetLeft         ();
                                   if (val == (2,1)) return channelEnum.SetRight        ();
                                   if (val == (2,_)) return channelEnum.SetAnyChannel   (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.SetCenter       ();
                                   if (val == (2,0)) return channelEnum.SetLeft         ();
                                   if (val == (2,1)) return channelEnum.SetRight        ();
                                   if (val == (2,_)) return channelEnum.SetEveryChannel (); return default; });
                AssertProp(() => { if (val == (1,0)) return channelEnum.SetCenter       ();
                                   if (val == (2,0)) return channelEnum.SetLeft         ();
                                   if (val == (2,1)) return channelEnum.SetRight        ();
                                   if (val == (2,_)) return channelEnum.SetNoChannel    (); return default; });
                AssertProp(() => { if (val == (1,0)) return Center          (channelEnum);
                                   if (val == (2,0)) return Left            (channelEnum);
                                   if (val == (2,1)) return Right           (channelEnum);
                                   if (val == (2,_)) return AnyChannel      (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return Center          (channelEnum);
                                   if (val == (2,0)) return Left            (channelEnum);
                                   if (val == (2,1)) return Right           (channelEnum);
                                   if (val == (2,_)) return EveryChannel    (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return Center          (channelEnum);
                                   if (val == (2,0)) return Left            (channelEnum);
                                   if (val == (2,1)) return Right           (channelEnum);
                                   if (val == (2,_)) return NoChannel       (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return WithCenter      (channelEnum);
                                   if (val == (2,0)) return WithLeft        (channelEnum);
                                   if (val == (2,1)) return WithRight       (channelEnum);
                                   if (val == (2,_)) return WithAnyChannel  (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return WithCenter      (channelEnum);
                                   if (val == (2,0)) return WithLeft        (channelEnum);
                                   if (val == (2,1)) return WithRight       (channelEnum);
                                   if (val == (2,_)) return WithEveryChannel(channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return WithCenter      (channelEnum);
                                   if (val == (2,0)) return WithLeft        (channelEnum);
                                   if (val == (2,1)) return WithRight       (channelEnum);
                                   if (val == (2,_)) return WithNoChannel   (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return AsCenter        (channelEnum);
                                   if (val == (2,0)) return AsLeft          (channelEnum);
                                   if (val == (2,1)) return AsRight         (channelEnum);
                                   if (val == (2,_)) return AsAnyChannel    (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return AsCenter        (channelEnum);
                                   if (val == (2,0)) return AsLeft          (channelEnum);
                                   if (val == (2,1)) return AsRight         (channelEnum);
                                   if (val == (2,_)) return AsNoChannel     (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return AsCenter        (channelEnum);
                                   if (val == (2,0)) return AsLeft          (channelEnum);
                                   if (val == (2,1)) return AsRight         (channelEnum);
                                   if (val == (2,_)) return AsNoChannel     (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ToCenter        (channelEnum);
                                   if (val == (2,0)) return ToLeft          (channelEnum);
                                   if (val == (2,1)) return ToRight         (channelEnum);
                                   if (val == (2,_)) return ToAnyChannel    (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ToCenter        (channelEnum);
                                   if (val == (2,0)) return ToLeft          (channelEnum);
                                   if (val == (2,1)) return ToRight         (channelEnum);
                                   if (val == (2,_)) return ToEveryChannel  (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ToCenter        (channelEnum);
                                   if (val == (2,0)) return ToLeft          (channelEnum);
                                   if (val == (2,1)) return ToRight         (channelEnum);
                                   if (val == (2,_)) return ToNoChannel     (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return SetCenter       (channelEnum);
                                   if (val == (2,0)) return SetLeft         (channelEnum);
                                   if (val == (2,1)) return SetRight        (channelEnum);
                                   if (val == (2,_)) return SetAnyChannel   (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return SetCenter       (channelEnum);
                                   if (val == (2,0)) return SetLeft         (channelEnum);
                                   if (val == (2,1)) return SetRight        (channelEnum);
                                   if (val == (2,_)) return SetEveryChannel (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return SetCenter       (channelEnum);
                                   if (val == (2,0)) return SetLeft         (channelEnum);
                                   if (val == (2,1)) return SetRight        (channelEnum);
                                   if (val == (2,_)) return SetNoChannel    (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.Center          (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.Left            (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.Right           (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.AnyChannel      (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.Center          (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.Left            (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.Right           (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.EveryChannel    (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.Center          (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.Left            (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.Right           (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.NoChannel       (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.WithCenter      (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.WithLeft        (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.WithRight       (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.WithAnyChannel  (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.WithCenter      (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.WithLeft        (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.WithRight       (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.WithEveryChannel(channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.WithCenter      (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.WithLeft        (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.WithRight       (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.WithNoChannel   (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.AsCenter        (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.AsLeft          (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.AsRight         (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.AsAnyChannel    (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.AsCenter        (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.AsLeft          (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.AsRight         (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.AsNoChannel     (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.AsCenter        (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.AsLeft          (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.AsRight         (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.AsNoChannel     (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.ToCenter        (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.ToLeft          (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.ToRight         (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.ToAnyChannel    (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.ToCenter        (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.ToLeft          (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.ToRight         (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.ToEveryChannel  (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.ToCenter        (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.ToLeft          (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.ToRight         (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.ToNoChannel     (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.SetCenter       (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.SetLeft         (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.SetRight        (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.SetAnyChannel   (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.SetCenter       (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.SetLeft         (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.SetRight        (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.SetEveryChannel (channelEnum); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.SetCenter       (channelEnum);
                                   if (val == (2,0)) return ConfigWishes.SetLeft         (channelEnum);
                                   if (val == (2,1)) return ConfigWishes.SetRight        (channelEnum);
                                   if (val == (2,_)) return ConfigWishes.SetNoChannel    (channelEnum); return default; });
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
                
                AssertProp(() => { if (val == (1,0)) return entity.Center          (context);
                                   if (val == (2,0)) return entity.Left            (context);
                                   if (val == (2,1)) return entity.Right           (context);
                                   if (val == (2,_)) return entity.AnyChannel      (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.Center          (context);
                                   if (val == (2,0)) return entity.Left            (context);
                                   if (val == (2,1)) return entity.Right           (context);
                                   if (val == (2,_)) return entity.EveryChannel    (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.Center          (context);
                                   if (val == (2,0)) return entity.Left            (context);
                                   if (val == (2,1)) return entity.Right           (context);
                                   if (val == (2,_)) return entity.NoChannel       (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.WithCenter      (context);
                                   if (val == (2,0)) return entity.WithLeft        (context);
                                   if (val == (2,1)) return entity.WithRight       (context);
                                   if (val == (2,_)) return entity.WithAnyChannel  (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.WithCenter      (context);
                                   if (val == (2,0)) return entity.WithLeft        (context);
                                   if (val == (2,1)) return entity.WithRight       (context);
                                   if (val == (2,_)) return entity.WithEveryChannel(       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.WithCenter      (context);
                                   if (val == (2,0)) return entity.WithLeft        (context);
                                   if (val == (2,1)) return entity.WithRight       (context);
                                   if (val == (2,_)) return entity.WithNoChannel   (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.AsCenter        (context);
                                   if (val == (2,0)) return entity.AsLeft          (context);
                                   if (val == (2,1)) return entity.AsRight         (context);
                                   if (val == (2,_)) return entity.AsAnyChannel    (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.AsCenter        (context);
                                   if (val == (2,0)) return entity.AsLeft          (context);
                                   if (val == (2,1)) return entity.AsRight         (context);
                                   if (val == (2,_)) return entity.AsEveryChannel  (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.AsCenter        (context);
                                   if (val == (2,0)) return entity.AsLeft          (context);
                                   if (val == (2,1)) return entity.AsRight         (context);
                                   if (val == (2,_)) return entity.AsNoChannel     (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.ToCenter        (context);
                                   if (val == (2,0)) return entity.ToLeft          (context);
                                   if (val == (2,1)) return entity.ToRight         (context);
                                   if (val == (2,_)) return entity.ToAnyChannel    (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.ToCenter        (context);
                                   if (val == (2,0)) return entity.ToLeft          (context);
                                   if (val == (2,1)) return entity.ToRight         (context);
                                   if (val == (2,_)) return entity.ToEveryChannel  (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.ToCenter        (context);
                                   if (val == (2,0)) return entity.ToLeft          (context);
                                   if (val == (2,1)) return entity.ToRight         (context);
                                   if (val == (2,_)) return entity.ToNoChannel     (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.SetCenter       (context);
                                   if (val == (2,0)) return entity.SetLeft         (context);
                                   if (val == (2,1)) return entity.SetRight        (context);
                                   if (val == (2,_)) return entity.SetAnyChannel   (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.SetCenter       (context);
                                   if (val == (2,0)) return entity.SetLeft         (context);
                                   if (val == (2,1)) return entity.SetRight        (context);
                                   if (val == (2,_)) return entity.SetEveryChannel (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return entity.SetCenter       (context);
                                   if (val == (2,0)) return entity.SetLeft         (context);
                                   if (val == (2,1)) return entity.SetRight        (context);
                                   if (val == (2,_)) return entity.SetNoChannel    (       ); return default; });
                AssertProp(() => { if (val == (1,0)) return Center          (entity, context);
                                   if (val == (2,0)) return Left            (entity, context);
                                   if (val == (2,1)) return Right           (entity, context);
                                   if (val == (2,_)) return AnyChannel      (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return Center          (entity, context);
                                   if (val == (2,0)) return Left            (entity, context);
                                   if (val == (2,1)) return Right           (entity, context);
                                   if (val == (2,_)) return EveryChannel    (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return Center          (entity, context);
                                   if (val == (2,0)) return Left            (entity, context);
                                   if (val == (2,1)) return Right           (entity, context);
                                   if (val == (2,_)) return NoChannel       (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return WithCenter      (entity, context);
                                   if (val == (2,0)) return WithLeft        (entity, context);
                                   if (val == (2,1)) return WithRight       (entity, context);
                                   if (val == (2,_)) return WithAnyChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return WithCenter      (entity, context);
                                   if (val == (2,0)) return WithLeft        (entity, context);
                                   if (val == (2,1)) return WithRight       (entity, context);
                                   if (val == (2,_)) return WithEveryChannel(entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return WithCenter      (entity, context);
                                   if (val == (2,0)) return WithLeft        (entity, context);
                                   if (val == (2,1)) return WithRight       (entity, context);
                                   if (val == (2,_)) return WithNoChannel   (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return AsCenter        (entity, context);
                                   if (val == (2,0)) return AsLeft          (entity, context);
                                   if (val == (2,1)) return AsRight         (entity, context);
                                   if (val == (2,_)) return AsAnyChannel    (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return AsCenter        (entity, context);
                                   if (val == (2,0)) return AsLeft          (entity, context);
                                   if (val == (2,1)) return AsRight         (entity, context);
                                   if (val == (2,_)) return AsEveryChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return AsCenter        (entity, context);
                                   if (val == (2,0)) return AsLeft          (entity, context);
                                   if (val == (2,1)) return AsRight         (entity, context);
                                   if (val == (2,_)) return AsNoChannel     (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ToCenter        (entity, context);
                                   if (val == (2,0)) return ToLeft          (entity, context);
                                   if (val == (2,1)) return ToRight         (entity, context);
                                   if (val == (2,_)) return ToAnyChannel    (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ToCenter        (entity, context);
                                   if (val == (2,0)) return ToLeft          (entity, context);
                                   if (val == (2,1)) return ToRight         (entity, context);
                                   if (val == (2,_)) return ToEveryChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ToCenter        (entity, context);
                                   if (val == (2,0)) return ToLeft          (entity, context);
                                   if (val == (2,1)) return ToRight         (entity, context);
                                   if (val == (2,_)) return ToNoChannel     (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return SetCenter       (entity, context);
                                   if (val == (2,0)) return SetLeft         (entity, context);
                                   if (val == (2,1)) return SetRight        (entity, context);
                                   if (val == (2,_)) return SetAnyChannel   (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return SetCenter       (entity, context);
                                   if (val == (2,0)) return SetLeft         (entity, context);
                                   if (val == (2,1)) return SetRight        (entity, context);
                                   if (val == (2,_)) return SetEveryChannel (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return SetCenter       (entity, context);
                                   if (val == (2,0)) return SetLeft         (entity, context);
                                   if (val == (2,1)) return SetRight        (entity, context);
                                   if (val == (2,_)) return SetNoChannel    (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.Center          (entity, context);
                                   if (val == (2,0)) return ConfigWishes.Left            (entity, context);
                                   if (val == (2,1)) return ConfigWishes.Right           (entity, context);
                                   if (val == (2,_)) return ConfigWishes.AnyChannel      (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.Center          (entity, context);
                                   if (val == (2,0)) return ConfigWishes.Left            (entity, context);
                                   if (val == (2,1)) return ConfigWishes.Right           (entity, context);
                                   if (val == (2,_)) return ConfigWishes.EveryChannel    (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.Center          (entity, context);
                                   if (val == (2,0)) return ConfigWishes.Left            (entity, context);
                                   if (val == (2,1)) return ConfigWishes.Right           (entity, context);
                                   if (val == (2,_)) return ConfigWishes.NoChannel       (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.WithCenter      (entity, context);
                                   if (val == (2,0)) return ConfigWishes.WithLeft        (entity, context);
                                   if (val == (2,1)) return ConfigWishes.WithRight       (entity, context);
                                   if (val == (2,_)) return ConfigWishes.WithAnyChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.WithCenter      (entity, context);
                                   if (val == (2,0)) return ConfigWishes.WithLeft        (entity, context);
                                   if (val == (2,1)) return ConfigWishes.WithRight       (entity, context);
                                   if (val == (2,_)) return ConfigWishes.WithEveryChannel(entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.WithCenter      (entity, context);
                                   if (val == (2,0)) return ConfigWishes.WithLeft        (entity, context);
                                   if (val == (2,1)) return ConfigWishes.WithRight       (entity, context);
                                   if (val == (2,_)) return ConfigWishes.WithNoChannel   (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.AsCenter        (entity, context);
                                   if (val == (2,0)) return ConfigWishes.AsLeft          (entity, context);
                                   if (val == (2,1)) return ConfigWishes.AsRight         (entity, context);
                                   if (val == (2,_)) return ConfigWishes.AsAnyChannel    (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.AsCenter        (entity, context);
                                   if (val == (2,0)) return ConfigWishes.AsLeft          (entity, context);
                                   if (val == (2,1)) return ConfigWishes.AsRight         (entity, context);
                                   if (val == (2,_)) return ConfigWishes.AsEveryChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.AsCenter        (entity, context);
                                   if (val == (2,0)) return ConfigWishes.AsLeft          (entity, context);
                                   if (val == (2,1)) return ConfigWishes.AsRight         (entity, context);
                                   if (val == (2,_)) return ConfigWishes.AsNoChannel     (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.ToCenter        (entity, context);
                                   if (val == (2,0)) return ConfigWishes.ToLeft          (entity, context);
                                   if (val == (2,1)) return ConfigWishes.ToRight         (entity, context);
                                   if (val == (2,_)) return ConfigWishes.ToAnyChannel    (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.ToCenter        (entity, context);
                                   if (val == (2,0)) return ConfigWishes.ToLeft          (entity, context);
                                   if (val == (2,1)) return ConfigWishes.ToRight         (entity, context);
                                   if (val == (2,_)) return ConfigWishes.ToEveryChannel  (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.ToCenter        (entity, context);
                                   if (val == (2,0)) return ConfigWishes.ToLeft          (entity, context);
                                   if (val == (2,1)) return ConfigWishes.ToRight         (entity, context);
                                   if (val == (2,_)) return ConfigWishes.ToNoChannel     (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.SetCenter       (entity, context);
                                   if (val == (2,0)) return ConfigWishes.SetLeft         (entity, context);
                                   if (val == (2,1)) return ConfigWishes.SetRight        (entity, context);
                                   if (val == (2,_)) return ConfigWishes.SetAnyChannel   (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.SetCenter       (entity, context);
                                   if (val == (2,0)) return ConfigWishes.SetLeft         (entity, context);
                                   if (val == (2,1)) return ConfigWishes.SetRight        (entity, context);
                                   if (val == (2,_)) return ConfigWishes.SetEveryChannel (entity         ); return default; });
                AssertProp(() => { if (val == (1,0)) return ConfigWishes.SetCenter       (entity, context);
                                   if (val == (2,0)) return ConfigWishes.SetLeft         (entity, context);
                                   if (val == (2,1)) return ConfigWishes.SetRight        (entity, context);
                                   if (val == (2,_)) return ConfigWishes.SetNoChannel    (entity         ); return default; });
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            channelEnums.ForEach(e => Assert_Immutable_Getters(e, val));
            channelEntities.ForEach(e => Assert_Immutable_Getters(e, val));
        }
        
        [TestMethod]
        public void AudioFileOutputChannel_Channel()
        {
            var x = CreateTestEntities(channels: 2);

            // Verify Constants
            AreEqual (0,              () => LeftChannel);
            AreEqual (1,              () => RightChannel);
            AreEqual (2,              () => StereoChannels);
            
            // Verify Test Entity Structure
            IsNotNull(                () => x);
            IsNotNull(                () => x.BuffBound);
            IsNotNull(                () => x.BuffBound.AudioFileOutput);
            IsNotNull(                () => x.BuffBound.AudioFileOutput.AudioFileOutputChannels);
            AreEqual (StereoChannels, () => x.BuffBound.AudioFileOutput.AudioFileOutputChannels.Count);
            IsNotNull(                () => x.BuffBound.AudioFileOutput.AudioFileOutputChannels[LeftChannel]);
            IsNotNull(                () => x.BuffBound.AudioFileOutput.AudioFileOutputChannels[RightChannel]);
            IsNotNull(                () => x.ChannelEntities);
            AreEqual (StereoChannels, () => x.ChannelEntities.Count);
            IsNotNull(                () => x.ChannelEntities[LeftChannel]);
            IsNotNull(                () => x.ChannelEntities[LeftChannel].BuffBound);
            IsNotNull(                () => x.ChannelEntities[LeftChannel].BuffBound.AudioFileOutput);
            IsNotNull(                () => x.ChannelEntities[LeftChannel].BuffBound.AudioFileOutput.AudioFileOutputChannels);
            AreEqual (MonoChannels,   () => x.ChannelEntities[LeftChannel].BuffBound.AudioFileOutput.AudioFileOutputChannels.Count);
            IsNotNull(                () => x.ChannelEntities[LeftChannel].BuffBound.AudioFileOutput.AudioFileOutputChannels[0]);
            IsNotNull(                () => x.ChannelEntities[RightChannel]);
            IsNotNull(                () => x.ChannelEntities[RightChannel].BuffBound);
            IsNotNull(                () => x.ChannelEntities[RightChannel].BuffBound.AudioFileOutput);
            IsNotNull(                () => x.ChannelEntities[RightChannel].BuffBound.AudioFileOutput.AudioFileOutputChannels);
            AreEqual (MonoChannels,   () => x.ChannelEntities[RightChannel].BuffBound.AudioFileOutput.AudioFileOutputChannels.Count);
            IsNotNull(                () => x.ChannelEntities[RightChannel].BuffBound.AudioFileOutput.AudioFileOutputChannels[0]);

            // Get AudioFileOutputChannel Variations
            var stereoLeft  = x                              .BuffBound.AudioFileOutput.AudioFileOutputChannels[LeftChannel];
            var stereoRight = x                              .BuffBound.AudioFileOutput.AudioFileOutputChannels[RightChannel];
            var leftOnly    = x.ChannelEntities[LeftChannel ].BuffBound.AudioFileOutput.AudioFileOutputChannels[0];
            var rightOnly   = x.ChannelEntities[RightChannel].BuffBound.AudioFileOutput.AudioFileOutputChannels[0];
            
            // Assert Getters
            AreEqual (LeftChannel , () => stereoLeft .Index       );
            AreEqual (LeftChannel , () => stereoLeft .Channel   ());
            AreEqual (LeftChannel , () => stereoLeft .GetChannel());
            AreEqual (RightChannel, () => stereoRight.Index       );
            AreEqual (RightChannel, () => stereoRight.Channel   ());
            AreEqual (RightChannel, () => stereoRight.GetChannel());
            AreEqual (LeftChannel , () => leftOnly   .Index       );
            AreEqual (LeftChannel , () => leftOnly   .Channel   ());
            AreEqual (LeftChannel , () => leftOnly   .GetChannel());
            AreEqual (RightChannel, () => rightOnly  .Index       );
            AreEqual (RightChannel, () => rightOnly  .Channel   ());
            AreEqual (RightChannel, () => rightOnly  .GetChannel());
        }
        
        [TestMethod]
        public void Channel_EdgeCases()
        {
            ThrowsException<ValueNotSupportedException>(() => ChannelEnumToChannel((ChannelEnum)(-1)));
        }

        // Getter Helpers

        private void Assert_All_Getters(ConfigTestEntities x, (int, int?) values)
        {
            Assert_SynthBound_Getters(x, values);
            Assert_TapeBound_Getters_Complete(x, values);
            Assert_BuffBound_Getters_Complete(x, values);
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
            AreEqual(c.channels == 1, () => ConfigWishes        .IsMono        (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels == 1, () => ConfigWishes        .IsMono        (x.SynthBound.FlowNode       ));
            AreEqual(c.channels == 1, () => ConfigWishesAccessor.IsMono        (x.SynthBound.ConfigResolver ));
            AreEqual(c.channels == 2, () => ConfigWishes        .IsStereo      (x.SynthBound.SynthWishes    ));
            AreEqual(c.channels == 2, () => ConfigWishes        .IsStereo      (x.SynthBound.FlowNode       ));
            AreEqual(c.channels == 2, () => ConfigWishesAccessor.IsStereo      (x.SynthBound.ConfigResolver ));
        }
        
        /// <inheritdoc cref="docs._singletapeassertion" />
        private void Assert_TapeBound_Getters_Single(ConfigTestEntities x, (int channels, int? channel) c)
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
            AreEqual(c == (2,_),      () => IsNoChannel(x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => IsNoChannel(x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => IsNoChannel(x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => IsNoChannel(x.TapeBound.TapeAction ));
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
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel(x.TapeBound.Tape       ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel(x.TapeBound.TapeConfig ));
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel(x.TapeBound.TapeActions));
            AreEqual(c == (2,_),      () => ConfigWishes.IsNoChannel(x.TapeBound.TapeAction ));
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
                        IsFalse(() => IsNoChannel(x.TapeBound.Tape        ));
                        IsFalse(() => IsNoChannel(x.TapeBound.TapeConfig  ));
                        IsFalse(() => IsNoChannel(x.TapeBound.TapeActions ));
                        IsFalse(() => IsNoChannel(x.TapeBound.TapeAction  ));
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
                        IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.Tape        ));
                        IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.TapeConfig  ));
                        IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.TapeActions ));
                        IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.TapeAction  ));
                        IsFalse(() => ConfigWishes.IsStereo       (x.TapeBound.Tape       ));
                        IsFalse(() => ConfigWishes.IsStereo       (x.TapeBound.TapeConfig ));
                        IsFalse(() => ConfigWishes.IsStereo       (x.TapeBound.TapeActions));
                        IsFalse(() => ConfigWishes.IsStereo       (x.TapeBound.TapeAction ));
        }

        private void Assert_StereoTape_Getters(TapeEntities x)
        {
                       IsNotNull(() => x                      );
                       IsNotNull(() => x.TapeBound.Tape       );
                       IsNotNull(() => x.TapeBound.TapeConfig );
                       IsNotNull(() => x.TapeBound.TapeActions);
                       IsNotNull(() => x.TapeBound.TapeAction );
        AreEqual(EmptyChannel  , () => x.TapeBound.TapeConfig .Channel       );
        AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .Channels      );
                         IsTrue (() => x.TapeBound.TapeConfig .IsAnyChannel  );
                         IsTrue (() => x.TapeBound.TapeConfig .IsEveryChannel);
                         IsTrue (() => x.TapeBound.TapeConfig .IsNoChannel   );
                         IsTrue (() => x.TapeBound.TapeConfig .IsStereo      );
                         IsFalse(() => x.TapeBound.TapeConfig .IsCenter      );
                         IsFalse(() => x.TapeBound.TapeConfig .IsLeft        );
                         IsFalse(() => x.TapeBound.TapeConfig .IsRight       );
                         IsFalse(() => x.TapeBound.TapeConfig .IsMono        );
        AreEqual(EmptyChannel  , () => x.TapeBound.Tape       .Channel       ());
        AreEqual(EmptyChannel  , () => x.TapeBound.TapeConfig .Channel       ());
        AreEqual(EmptyChannel  , () => x.TapeBound.TapeActions.Channel       ());
        AreEqual(EmptyChannel  , () => x.TapeBound.TapeAction .Channel       ());
        AreEqual(StereoChannels, () => x.TapeBound.Tape       .Channels      ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .Channels      ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeActions.Channels      ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeAction .Channels      ());
        AreEqual(EmptyChannel  , () => x.TapeBound.Tape       .GetChannel    ());
        AreEqual(EmptyChannel  , () => x.TapeBound.TapeConfig .GetChannel    ());
        AreEqual(EmptyChannel  , () => x.TapeBound.TapeActions.GetChannel    ());
        AreEqual(EmptyChannel  , () => x.TapeBound.TapeAction .GetChannel    ());
        AreEqual(StereoChannels, () => x.TapeBound.Tape       .GetChannels   ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .GetChannels   ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeActions.GetChannels   ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeAction .GetChannels   ());
                         IsTrue (() => x.TapeBound.Tape       .IsAnyChannel  ());
                         IsTrue (() => x.TapeBound.TapeConfig .IsAnyChannel  ());
                         IsTrue (() => x.TapeBound.TapeActions.IsAnyChannel  ());
                         IsTrue (() => x.TapeBound.TapeAction .IsAnyChannel  ());
                         IsTrue (() => x.TapeBound.Tape       .IsEveryChannel());
                         IsTrue (() => x.TapeBound.TapeConfig .IsEveryChannel());
                         IsTrue (() => x.TapeBound.TapeActions.IsEveryChannel());
                         IsTrue (() => x.TapeBound.TapeAction .IsEveryChannel());
                         IsTrue (() => x.TapeBound.Tape       .IsNoChannel   ());
                         IsTrue (() => x.TapeBound.TapeConfig .IsNoChannel   ());
                         IsTrue (() => x.TapeBound.TapeActions.IsNoChannel   ());
                         IsTrue (() => x.TapeBound.TapeAction .IsNoChannel   ());
                         IsTrue (() => x.TapeBound.Tape       .IsStereo      ());
                         IsTrue (() => x.TapeBound.TapeConfig .IsStereo      ());
                         IsTrue (() => x.TapeBound.TapeActions.IsStereo      ());
                         IsTrue (() => x.TapeBound.TapeAction .IsStereo      ());
                         IsFalse(() => x.TapeBound.Tape       .IsCenter      ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsCenter      ());
                         IsFalse(() => x.TapeBound.TapeActions.IsCenter      ());
                         IsFalse(() => x.TapeBound.TapeAction .IsCenter      ());
                         IsFalse(() => x.TapeBound.Tape       .IsLeft        ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsLeft        ());
                         IsFalse(() => x.TapeBound.TapeActions.IsLeft        ());
                         IsFalse(() => x.TapeBound.TapeAction .IsLeft        ());
                         IsFalse(() => x.TapeBound.Tape       .IsRight       ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsRight       ());
                         IsFalse(() => x.TapeBound.TapeActions.IsRight       ());
                         IsFalse(() => x.TapeBound.TapeAction .IsRight       ());
                         IsFalse(() => x.TapeBound.Tape       .IsMono        ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsMono        ());
                         IsFalse(() => x.TapeBound.TapeActions.IsMono        ());
                         IsFalse(() => x.TapeBound.TapeAction .IsMono        ());
        AreEqual(EmptyChannel  , () => Channel       (x.TapeBound.Tape        ));
        AreEqual(EmptyChannel  , () => Channel       (x.TapeBound.TapeConfig  ));
        AreEqual(EmptyChannel  , () => Channel       (x.TapeBound.TapeActions ));
        AreEqual(EmptyChannel  , () => Channel       (x.TapeBound.TapeAction  ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.Tape        ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.TapeConfig  ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.TapeActions ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.TapeAction  ));
        AreEqual(EmptyChannel  , () => GetChannel    (x.TapeBound.Tape        ));
        AreEqual(EmptyChannel  , () => GetChannel    (x.TapeBound.TapeConfig  ));
        AreEqual(EmptyChannel  , () => GetChannel    (x.TapeBound.TapeActions ));
        AreEqual(EmptyChannel  , () => GetChannel    (x.TapeBound.TapeAction  ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.Tape        ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.TapeConfig  ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.TapeActions ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.TapeAction  ));
                         IsTrue (() => IsAnyChannel  (x.TapeBound.Tape        ));
                         IsTrue (() => IsAnyChannel  (x.TapeBound.TapeConfig  ));
                         IsTrue (() => IsAnyChannel  (x.TapeBound.TapeActions ));
                         IsTrue (() => IsAnyChannel  (x.TapeBound.TapeAction  ));
                         IsTrue (() => IsEveryChannel(x.TapeBound.Tape        ));
                         IsTrue (() => IsEveryChannel(x.TapeBound.TapeConfig  ));
                         IsTrue (() => IsEveryChannel(x.TapeBound.TapeActions ));
                         IsTrue (() => IsEveryChannel(x.TapeBound.TapeAction  ));
                         IsTrue (() => IsNoChannel   (x.TapeBound.Tape        ));
                         IsTrue (() => IsNoChannel   (x.TapeBound.TapeConfig  ));
                         IsTrue (() => IsNoChannel   (x.TapeBound.TapeActions ));
                         IsTrue (() => IsNoChannel   (x.TapeBound.TapeAction  ));
                         IsTrue (() => IsNoChannel(x.TapeBound.Tape        ));
                         IsTrue (() => IsNoChannel(x.TapeBound.TapeConfig  ));
                         IsTrue (() => IsNoChannel(x.TapeBound.TapeActions ));
                         IsTrue (() => IsNoChannel(x.TapeBound.TapeAction  ));
                         IsTrue (() => IsStereo      (x.TapeBound.Tape        ));
                         IsTrue (() => IsStereo      (x.TapeBound.TapeConfig  ));
                         IsTrue (() => IsStereo      (x.TapeBound.TapeActions ));
                         IsTrue (() => IsStereo      (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsCenter      (x.TapeBound.Tape        ));
                         IsFalse(() => IsCenter      (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsCenter      (x.TapeBound.TapeActions ));
                         IsFalse(() => IsCenter      (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsLeft        (x.TapeBound.Tape        ));
                         IsFalse(() => IsLeft        (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsLeft        (x.TapeBound.TapeActions ));
                         IsFalse(() => IsLeft        (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsRight       (x.TapeBound.Tape        ));
                         IsFalse(() => IsRight       (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsRight       (x.TapeBound.TapeActions ));
                         IsFalse(() => IsRight       (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsMono        (x.TapeBound.Tape        ));
                         IsFalse(() => IsMono        (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsMono        (x.TapeBound.TapeActions ));
                         IsFalse(() => IsMono        (x.TapeBound.TapeAction  ));
        AreEqual(EmptyChannel  , () => ConfigWishes.Channel       (x.TapeBound.Tape       ));
        AreEqual(EmptyChannel  , () => ConfigWishes.Channel       (x.TapeBound.TapeConfig ));
        AreEqual(EmptyChannel  , () => ConfigWishes.Channel       (x.TapeBound.TapeActions));
        AreEqual(EmptyChannel  , () => ConfigWishes.Channel       (x.TapeBound.TapeAction ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.Tape       ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.TapeConfig ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.TapeActions));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.TapeAction ));
        AreEqual(EmptyChannel  , () => ConfigWishes.GetChannel    (x.TapeBound.Tape       ));
        AreEqual(EmptyChannel  , () => ConfigWishes.GetChannel    (x.TapeBound.TapeConfig ));
        AreEqual(EmptyChannel  , () => ConfigWishes.GetChannel    (x.TapeBound.TapeActions));
        AreEqual(EmptyChannel  , () => ConfigWishes.GetChannel    (x.TapeBound.TapeAction ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.Tape       ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.TapeConfig ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.TapeActions));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.TapeAction ));
                         IsTrue (() => ConfigWishes.IsAnyChannel  (x.TapeBound.Tape       ));
                         IsTrue (() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeConfig ));
                         IsTrue (() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeActions));
                         IsTrue (() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeAction ));
                         IsTrue (() => ConfigWishes.IsEveryChannel(x.TapeBound.Tape       ));
                         IsTrue (() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeConfig ));
                         IsTrue (() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeActions));
                         IsTrue (() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeAction ));
                         IsTrue (() => ConfigWishes.IsNoChannel   (x.TapeBound.Tape       ));
                         IsTrue (() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeConfig ));
                         IsTrue (() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeActions));
                         IsTrue (() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeAction ));
                         IsTrue (() => ConfigWishes.IsNoChannel(x.TapeBound.Tape       ));
                         IsTrue (() => ConfigWishes.IsNoChannel(x.TapeBound.TapeConfig ));
                         IsTrue (() => ConfigWishes.IsNoChannel(x.TapeBound.TapeActions));
                         IsTrue (() => ConfigWishes.IsNoChannel(x.TapeBound.TapeAction ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.Tape       ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.TapeConfig ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.TapeActions));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.TapeAction ));
        }

        private void Assert_LeftTape_Getters(TapeEntities x)
        {   
                       IsNotNull(() => x                      );
                       IsNotNull(() => x.TapeBound.Tape       );
                       IsNotNull(() => x.TapeBound.TapeConfig );
                       IsNotNull(() => x.TapeBound.TapeActions);
                       IsNotNull(() => x.TapeBound.TapeAction );
        AreEqual(LeftChannel   , () => x.TapeBound.TapeConfig .Channel       );
        AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .Channels      );
                         IsTrue (() => x.TapeBound.TapeConfig .IsLeft        );
                         IsTrue (() => x.TapeBound.TapeConfig .IsStereo      );
                         IsFalse(() => x.TapeBound.TapeConfig .IsAnyChannel  );
                         IsFalse(() => x.TapeBound.TapeConfig .IsEveryChannel);
                         IsFalse(() => x.TapeBound.TapeConfig .IsNoChannel   );
                         IsFalse(() => x.TapeBound.TapeConfig .IsCenter      );
                         IsFalse(() => x.TapeBound.TapeConfig .IsRight       );
                         IsFalse(() => x.TapeBound.TapeConfig .IsMono        );
        AreEqual(LeftChannel   , () => x.TapeBound.Tape       .Channel       ());
        AreEqual(LeftChannel   , () => x.TapeBound.TapeConfig .Channel       ());
        AreEqual(LeftChannel   , () => x.TapeBound.TapeActions.Channel       ());
        AreEqual(LeftChannel   , () => x.TapeBound.TapeAction .Channel       ());
        AreEqual(StereoChannels, () => x.TapeBound.Tape       .Channels      ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .Channels      ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeActions.Channels      ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeAction .Channels      ());
        AreEqual(LeftChannel   , () => x.TapeBound.Tape       .GetChannel    ());
        AreEqual(LeftChannel   , () => x.TapeBound.TapeConfig .GetChannel    ());
        AreEqual(LeftChannel   , () => x.TapeBound.TapeActions.GetChannel    ());
        AreEqual(LeftChannel   , () => x.TapeBound.TapeAction .GetChannel    ());
        AreEqual(StereoChannels, () => x.TapeBound.Tape       .GetChannels   ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .GetChannels   ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeActions.GetChannels   ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeAction .GetChannels   ());
                         IsTrue (() => x.TapeBound.Tape       .IsLeft        ());
                         IsTrue (() => x.TapeBound.TapeConfig .IsLeft        ());
                         IsTrue (() => x.TapeBound.TapeActions.IsLeft        ());
                         IsTrue (() => x.TapeBound.TapeAction .IsLeft        ());
                         IsTrue (() => x.TapeBound.Tape       .IsStereo      ());
                         IsTrue (() => x.TapeBound.TapeConfig .IsStereo      ());
                         IsTrue (() => x.TapeBound.TapeActions.IsStereo      ());
                         IsTrue (() => x.TapeBound.TapeAction .IsStereo      ());
                         IsFalse(() => x.TapeBound.Tape       .IsAnyChannel  ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsAnyChannel  ());
                         IsFalse(() => x.TapeBound.TapeActions.IsAnyChannel  ());
                         IsFalse(() => x.TapeBound.TapeAction .IsAnyChannel  ());
                         IsFalse(() => x.TapeBound.Tape       .IsEveryChannel());
                         IsFalse(() => x.TapeBound.TapeConfig .IsEveryChannel());
                         IsFalse(() => x.TapeBound.TapeActions.IsEveryChannel());
                         IsFalse(() => x.TapeBound.TapeAction .IsEveryChannel());
                         IsFalse(() => x.TapeBound.Tape       .IsNoChannel   ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsNoChannel   ());
                         IsFalse(() => x.TapeBound.TapeActions.IsNoChannel   ());
                         IsFalse(() => x.TapeBound.TapeAction .IsNoChannel   ());
                         IsFalse(() => x.TapeBound.Tape       .IsCenter      ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsCenter      ());
                         IsFalse(() => x.TapeBound.TapeActions.IsCenter      ());
                         IsFalse(() => x.TapeBound.TapeAction .IsCenter      ());
                         IsFalse(() => x.TapeBound.Tape       .IsRight       ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsRight       ());
                         IsFalse(() => x.TapeBound.TapeActions.IsRight       ());
                         IsFalse(() => x.TapeBound.TapeAction .IsRight       ());
                         IsFalse(() => x.TapeBound.Tape       .IsMono        ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsMono        ());
                         IsFalse(() => x.TapeBound.TapeActions.IsMono        ());
                         IsFalse(() => x.TapeBound.TapeAction .IsMono        ());
        AreEqual(LeftChannel   , () => Channel       (x.TapeBound.Tape        ));
        AreEqual(LeftChannel   , () => Channel       (x.TapeBound.TapeConfig  ));
        AreEqual(LeftChannel   , () => Channel       (x.TapeBound.TapeActions ));
        AreEqual(LeftChannel   , () => Channel       (x.TapeBound.TapeAction  ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.Tape        ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.TapeConfig  ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.TapeActions ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.TapeAction  ));
        AreEqual(LeftChannel   , () => GetChannel    (x.TapeBound.Tape        ));
        AreEqual(LeftChannel   , () => GetChannel    (x.TapeBound.TapeConfig  ));
        AreEqual(LeftChannel   , () => GetChannel    (x.TapeBound.TapeActions ));
        AreEqual(LeftChannel   , () => GetChannel    (x.TapeBound.TapeAction  ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.Tape        ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.TapeConfig  ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.TapeActions ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.TapeAction  ));
                         IsTrue (() => IsLeft        (x.TapeBound.Tape        ));
                         IsTrue (() => IsLeft        (x.TapeBound.TapeConfig  ));
                         IsTrue (() => IsLeft        (x.TapeBound.TapeActions ));
                         IsTrue (() => IsLeft        (x.TapeBound.TapeAction  ));
                         IsTrue (() => IsStereo      (x.TapeBound.Tape        ));
                         IsTrue (() => IsStereo      (x.TapeBound.TapeConfig  ));
                         IsTrue (() => IsStereo      (x.TapeBound.TapeActions ));
                         IsTrue (() => IsStereo      (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsAnyChannel  (x.TapeBound.Tape        ));
                         IsFalse(() => IsAnyChannel  (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsAnyChannel  (x.TapeBound.TapeActions ));
                         IsFalse(() => IsAnyChannel  (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsEveryChannel(x.TapeBound.Tape        ));
                         IsFalse(() => IsEveryChannel(x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsEveryChannel(x.TapeBound.TapeActions ));
                         IsFalse(() => IsEveryChannel(x.TapeBound.TapeAction  ));
                         IsFalse(() => IsNoChannel   (x.TapeBound.Tape        ));
                         IsFalse(() => IsNoChannel   (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsNoChannel   (x.TapeBound.TapeActions ));
                         IsFalse(() => IsNoChannel   (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsNoChannel(x.TapeBound.Tape        ));
                         IsFalse(() => IsNoChannel(x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsNoChannel(x.TapeBound.TapeActions ));
                         IsFalse(() => IsNoChannel(x.TapeBound.TapeAction  ));
                         IsFalse(() => IsCenter      (x.TapeBound.Tape        ));
                         IsFalse(() => IsCenter      (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsCenter      (x.TapeBound.TapeActions ));
                         IsFalse(() => IsCenter      (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsRight       (x.TapeBound.Tape        ));
                         IsFalse(() => IsRight       (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsRight       (x.TapeBound.TapeActions ));
                         IsFalse(() => IsRight       (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsMono        (x.TapeBound.Tape        ));
                         IsFalse(() => IsMono        (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsMono        (x.TapeBound.TapeActions ));
                         IsFalse(() => IsMono        (x.TapeBound.TapeAction  ));
        AreEqual(LeftChannel   , () => ConfigWishes.Channel       (x.TapeBound.Tape       ));
        AreEqual(LeftChannel   , () => ConfigWishes.Channel       (x.TapeBound.TapeConfig ));
        AreEqual(LeftChannel   , () => ConfigWishes.Channel       (x.TapeBound.TapeActions));
        AreEqual(LeftChannel   , () => ConfigWishes.Channel       (x.TapeBound.TapeAction ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.Tape       ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.TapeConfig ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.TapeActions));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.TapeAction ));
        AreEqual(LeftChannel   , () => ConfigWishes.GetChannel    (x.TapeBound.Tape       ));
        AreEqual(LeftChannel   , () => ConfigWishes.GetChannel    (x.TapeBound.TapeConfig ));
        AreEqual(LeftChannel   , () => ConfigWishes.GetChannel    (x.TapeBound.TapeActions));
        AreEqual(LeftChannel   , () => ConfigWishes.GetChannel    (x.TapeBound.TapeAction ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.Tape       ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.TapeConfig ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.TapeActions));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.TapeAction ));
                         IsTrue (() => ConfigWishes.IsLeft        (x.TapeBound.Tape       ));
                         IsTrue (() => ConfigWishes.IsLeft        (x.TapeBound.TapeConfig ));
                         IsTrue (() => ConfigWishes.IsLeft        (x.TapeBound.TapeActions));
                         IsTrue (() => ConfigWishes.IsLeft        (x.TapeBound.TapeAction ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.Tape       ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.TapeConfig ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.TapeActions));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsRight       (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.TapeAction ));
        }

        private void Assert_RightTape_Getters(TapeEntities x)
        {
                       IsNotNull(() => x                      );
                       IsNotNull(() => x.TapeBound.Tape       );
                       IsNotNull(() => x.TapeBound.TapeConfig );
                       IsNotNull(() => x.TapeBound.TapeActions);
                       IsNotNull(() => x.TapeBound.TapeAction );
        AreEqual(RightChannel  , () => x.TapeBound.TapeConfig .Channel       );
        AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .Channels      );
                         IsTrue (() => x.TapeBound.TapeConfig .IsRight       );
                         IsTrue (() => x.TapeBound.TapeConfig .IsStereo      );
                         IsFalse(() => x.TapeBound.TapeConfig .IsAnyChannel  );
                         IsFalse(() => x.TapeBound.TapeConfig .IsEveryChannel);
                         IsFalse(() => x.TapeBound.TapeConfig .IsNoChannel   );
                         IsFalse(() => x.TapeBound.TapeConfig .IsCenter      );
                         IsFalse(() => x.TapeBound.TapeConfig .IsLeft       );
                         IsFalse(() => x.TapeBound.TapeConfig .IsMono        );
        AreEqual(RightChannel  , () => x.TapeBound.Tape       .Channel       ());
        AreEqual(RightChannel  , () => x.TapeBound.TapeConfig .Channel       ());
        AreEqual(RightChannel  , () => x.TapeBound.TapeActions.Channel       ());
        AreEqual(RightChannel  , () => x.TapeBound.TapeAction .Channel       ());
        AreEqual(StereoChannels, () => x.TapeBound.Tape       .Channels      ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .Channels      ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeActions.Channels      ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeAction .Channels      ());
        AreEqual(RightChannel  , () => x.TapeBound.Tape       .GetChannel    ());
        AreEqual(RightChannel  , () => x.TapeBound.TapeConfig .GetChannel    ());
        AreEqual(RightChannel  , () => x.TapeBound.TapeActions.GetChannel    ());
        AreEqual(RightChannel  , () => x.TapeBound.TapeAction .GetChannel    ());
        AreEqual(StereoChannels, () => x.TapeBound.Tape       .GetChannels   ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeConfig .GetChannels   ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeActions.GetChannels   ());
        AreEqual(StereoChannels, () => x.TapeBound.TapeAction .GetChannels   ());
                         IsTrue (() => x.TapeBound.Tape       .IsRight       ());
                         IsTrue (() => x.TapeBound.TapeConfig .IsRight       ());
                         IsTrue (() => x.TapeBound.TapeActions.IsRight       ());
                         IsTrue (() => x.TapeBound.TapeAction .IsRight       ());
                         IsTrue (() => x.TapeBound.Tape       .IsStereo      ());
                         IsTrue (() => x.TapeBound.TapeConfig .IsStereo      ());
                         IsTrue (() => x.TapeBound.TapeActions.IsStereo      ());
                         IsTrue (() => x.TapeBound.TapeAction .IsStereo      ());
                         IsFalse(() => x.TapeBound.Tape       .IsAnyChannel  ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsAnyChannel  ());
                         IsFalse(() => x.TapeBound.TapeActions.IsAnyChannel  ());
                         IsFalse(() => x.TapeBound.TapeAction .IsAnyChannel  ());
                         IsFalse(() => x.TapeBound.Tape       .IsEveryChannel());
                         IsFalse(() => x.TapeBound.TapeConfig .IsEveryChannel());
                         IsFalse(() => x.TapeBound.TapeActions.IsEveryChannel());
                         IsFalse(() => x.TapeBound.TapeAction .IsEveryChannel());
                         IsFalse(() => x.TapeBound.Tape       .IsNoChannel   ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsNoChannel   ());
                         IsFalse(() => x.TapeBound.TapeActions.IsNoChannel   ());
                         IsFalse(() => x.TapeBound.TapeAction .IsNoChannel   ());
                         IsFalse(() => x.TapeBound.Tape       .IsCenter      ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsCenter      ());
                         IsFalse(() => x.TapeBound.TapeActions.IsCenter      ());
                         IsFalse(() => x.TapeBound.TapeAction .IsCenter      ());
                         IsFalse(() => x.TapeBound.Tape       .IsLeft        ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsLeft        ());
                         IsFalse(() => x.TapeBound.TapeActions.IsLeft        ());
                         IsFalse(() => x.TapeBound.TapeAction .IsLeft        ());
                         IsFalse(() => x.TapeBound.Tape       .IsMono        ());
                         IsFalse(() => x.TapeBound.TapeConfig .IsMono        ());
                         IsFalse(() => x.TapeBound.TapeActions.IsMono        ());
                         IsFalse(() => x.TapeBound.TapeAction .IsMono        ());
        AreEqual(RightChannel  , () => Channel       (x.TapeBound.Tape        ));
        AreEqual(RightChannel  , () => Channel       (x.TapeBound.TapeConfig  ));
        AreEqual(RightChannel  , () => Channel       (x.TapeBound.TapeActions ));
        AreEqual(RightChannel  , () => Channel       (x.TapeBound.TapeAction  ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.Tape        ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.TapeConfig  ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.TapeActions ));
        AreEqual(StereoChannels, () => Channels      (x.TapeBound.TapeAction  ));
        AreEqual(RightChannel  , () => GetChannel    (x.TapeBound.Tape        ));
        AreEqual(RightChannel  , () => GetChannel    (x.TapeBound.TapeConfig  ));
        AreEqual(RightChannel  , () => GetChannel    (x.TapeBound.TapeActions ));
        AreEqual(RightChannel  , () => GetChannel    (x.TapeBound.TapeAction  ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.Tape        ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.TapeConfig  ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.TapeActions ));
        AreEqual(StereoChannels, () => GetChannels   (x.TapeBound.TapeAction  ));
                         IsTrue (() => IsRight       (x.TapeBound.Tape        ));
                         IsTrue (() => IsRight       (x.TapeBound.TapeConfig  ));
                         IsTrue (() => IsRight       (x.TapeBound.TapeActions ));
                         IsTrue (() => IsRight       (x.TapeBound.TapeAction  ));
                         IsTrue (() => IsStereo      (x.TapeBound.Tape        ));
                         IsTrue (() => IsStereo      (x.TapeBound.TapeConfig  ));
                         IsTrue (() => IsStereo      (x.TapeBound.TapeActions ));
                         IsTrue (() => IsStereo      (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsAnyChannel  (x.TapeBound.Tape        ));
                         IsFalse(() => IsAnyChannel  (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsAnyChannel  (x.TapeBound.TapeActions ));
                         IsFalse(() => IsAnyChannel  (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsEveryChannel(x.TapeBound.Tape        ));
                         IsFalse(() => IsEveryChannel(x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsEveryChannel(x.TapeBound.TapeActions ));
                         IsFalse(() => IsEveryChannel(x.TapeBound.TapeAction  ));
                         IsFalse(() => IsNoChannel   (x.TapeBound.Tape        ));
                         IsFalse(() => IsNoChannel   (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsNoChannel   (x.TapeBound.TapeActions ));
                         IsFalse(() => IsNoChannel   (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsNoChannel(x.TapeBound.Tape        ));
                         IsFalse(() => IsNoChannel(x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsNoChannel(x.TapeBound.TapeActions ));
                         IsFalse(() => IsNoChannel(x.TapeBound.TapeAction  ));
                         IsFalse(() => IsCenter      (x.TapeBound.Tape        ));
                         IsFalse(() => IsCenter      (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsCenter      (x.TapeBound.TapeActions ));
                         IsFalse(() => IsCenter      (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsLeft        (x.TapeBound.Tape        ));
                         IsFalse(() => IsLeft        (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsLeft        (x.TapeBound.TapeActions ));
                         IsFalse(() => IsLeft        (x.TapeBound.TapeAction  ));
                         IsFalse(() => IsMono        (x.TapeBound.Tape        ));
                         IsFalse(() => IsMono        (x.TapeBound.TapeConfig  ));
                         IsFalse(() => IsMono        (x.TapeBound.TapeActions ));
                         IsFalse(() => IsMono        (x.TapeBound.TapeAction  ));
        AreEqual(RightChannel  , () => ConfigWishes.Channel       (x.TapeBound.Tape       ));
        AreEqual(RightChannel  , () => ConfigWishes.Channel       (x.TapeBound.TapeConfig ));
        AreEqual(RightChannel  , () => ConfigWishes.Channel       (x.TapeBound.TapeActions));
        AreEqual(RightChannel  , () => ConfigWishes.Channel       (x.TapeBound.TapeAction ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.Tape       ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.TapeConfig ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.TapeActions));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.TapeBound.TapeAction ));
        AreEqual(RightChannel  , () => ConfigWishes.GetChannel    (x.TapeBound.Tape       ));
        AreEqual(RightChannel  , () => ConfigWishes.GetChannel    (x.TapeBound.TapeConfig ));
        AreEqual(RightChannel  , () => ConfigWishes.GetChannel    (x.TapeBound.TapeActions));
        AreEqual(RightChannel  , () => ConfigWishes.GetChannel    (x.TapeBound.TapeAction ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.Tape       ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.TapeConfig ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.TapeActions));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.TapeBound.TapeAction ));
                         IsTrue (() => ConfigWishes.IsRight       (x.TapeBound.Tape       ));
                         IsTrue (() => ConfigWishes.IsRight       (x.TapeBound.TapeConfig ));
                         IsTrue (() => ConfigWishes.IsRight       (x.TapeBound.TapeActions));
                         IsTrue (() => ConfigWishes.IsRight       (x.TapeBound.TapeAction ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.Tape       ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.TapeConfig ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.TapeActions));
                         IsTrue (() => ConfigWishes.IsStereo      (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsAnyChannel  (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsEveryChannel(x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsNoChannel   (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsNoChannel(x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsCenter      (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsLeft        (x.TapeBound.TapeAction ));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.Tape       ));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.TapeConfig ));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.TapeActions));
                         IsFalse(() => ConfigWishes.IsMono        (x.TapeBound.TapeAction ));
        }
        
        /// <inheritdoc cref="docs._singletapeassertion" />
        private void Assert_BuffBound_Getters_Single(ConfigTestEntities x, (int channels, int? channel) c)
        { 
            if (c == (1,0))
            {
                Assert_MonoBuff_Getters(x);
            }
            else if (c == (2,0))
            {
                Assert_LeftBuff_Getters(x);
            }
            else if (c == (2,1))
            {
                Assert_RightBuff_Getters(x);
            }
            else if (c == (2,_))
            {
                Assert_StereoBuff_Getters(x);
            }
            else
            {
                throw new Exception("Unsupported combination of values: " + c);
            }
        }
        
        /// <inheritdoc cref="docs._singletapeassertion" />
        private void Assert_BuffBound_Getters_Complete(ConfigTestEntities x, (int channels, int? channel) c)
        {
            IsNotNull(() => x.ChannelEntities);
            AreEqual(c.channels, () => x.ChannelEntities.Count);
            IsFalse(() => x.ChannelEntities.Contains(null));
            
            if (c.channels == MonoChannels)
            {
                AreSame(x.BuffBound.Buff, () => x.ChannelEntities[0].BuffBound.Buff); 
                Assert_MonoBuff_Getters(x);
                Assert_MonoBuff_Getters(x.ChannelEntities[0]);
            }
            if (c.channels == StereoChannels)
            {
                Assert_StereoBuff_Getters(x);
                Assert_LeftBuff_Getters(x.ChannelEntities[0]);
                Assert_RightBuff_Getters(x.ChannelEntities[1]);
            }
        }

        private void Assert_MonoBuff_Getters(TapeEntities x)
        {
                      IsNotNull(() => x                          );
                      IsNotNull(() => x.BuffBound.Buff           );
                      IsNotNull(() => x.BuffBound.AudioFileOutput);
        AreEqual(CenterChannel, () => x.BuffBound.Buff           .Channel       ());
        AreEqual(CenterChannel, () => x.BuffBound.AudioFileOutput.Channel       ());
        AreEqual(CenterChannel, () => x.BuffBound.Buff           .GetChannel    ());
        AreEqual(CenterChannel, () => x.BuffBound.AudioFileOutput.GetChannel    ());
        AreEqual(MonoChannels , () => x.BuffBound.Buff           .Channels      ());
        AreEqual(MonoChannels , () => x.BuffBound.AudioFileOutput.Channels      ());
        AreEqual(MonoChannels , () => x.BuffBound.Buff           .GetChannels   ());
        AreEqual(MonoChannels , () => x.BuffBound.AudioFileOutput.GetChannels   ());
                        IsTrue (() => x.BuffBound.Buff           .IsCenter      ());
                        IsTrue (() => x.BuffBound.AudioFileOutput.IsCenter      ());
                        IsTrue (() => x.BuffBound.Buff           .IsLeft        ()); // By Design: Buff thinks Channel 0 are Left & Center.
                        IsTrue (() => x.BuffBound.AudioFileOutput.IsLeft        ());
                        IsTrue (() => x.BuffBound.Buff           .IsMono        ());
                        IsTrue (() => x.BuffBound.AudioFileOutput.IsMono        ());
                        IsFalse(() => x.BuffBound.Buff           .IsRight       ());
                        IsFalse(() => x.BuffBound.AudioFileOutput.IsRight       ());
                        IsFalse(() => x.BuffBound.Buff           .IsNoChannel   ());
                        IsFalse(() => x.BuffBound.AudioFileOutput.IsNoChannel   ());
                        IsFalse(() => x.BuffBound.Buff           .IsAnyChannel  ());
                        IsFalse(() => x.BuffBound.AudioFileOutput.IsAnyChannel  ());
                        IsFalse(() => x.BuffBound.Buff           .IsEveryChannel());
                        IsFalse(() => x.BuffBound.AudioFileOutput.IsEveryChannel());
                        IsTrue (() => x.BuffBound.Buff           .IsStereo      ()); // By Design: Every state maps to stereo, 2 channels, or single L/R.
                        IsTrue (() => x.BuffBound.AudioFileOutput.IsStereo      ());
        AreEqual(CenterChannel, () => Channel       (x.BuffBound.Buff            ));
        AreEqual(CenterChannel, () => Channel       (x.BuffBound.AudioFileOutput ));
        AreEqual(CenterChannel, () => GetChannel    (x.BuffBound.Buff            ));
        AreEqual(CenterChannel, () => GetChannel    (x.BuffBound.AudioFileOutput ));
        AreEqual(MonoChannels , () => Channels      (x.BuffBound.Buff            ));
        AreEqual(MonoChannels , () => Channels      (x.BuffBound.AudioFileOutput ));
        AreEqual(MonoChannels , () => GetChannels   (x.BuffBound.Buff            ));
        AreEqual(MonoChannels , () => GetChannels   (x.BuffBound.AudioFileOutput ));
                        IsTrue (() => IsCenter      (x.BuffBound.Buff            ));
                        IsTrue (() => IsCenter      (x.BuffBound.AudioFileOutput ));
                        IsTrue (() => IsLeft        (x.BuffBound.Buff            )); // By Design: Buff thinks Channel 0 are Left & Center.
                        IsTrue (() => IsLeft        (x.BuffBound.AudioFileOutput ));
                        IsTrue (() => IsMono        (x.BuffBound.Buff            ));
                        IsTrue (() => IsMono        (x.BuffBound.AudioFileOutput ));
                        IsFalse(() => IsRight       (x.BuffBound.Buff            ));
                        IsFalse(() => IsRight       (x.BuffBound.AudioFileOutput ));
                        IsFalse(() => IsNoChannel   (x.BuffBound.Buff            ));
                        IsFalse(() => IsNoChannel   (x.BuffBound.AudioFileOutput ));
                        IsFalse(() => IsAnyChannel  (x.BuffBound.Buff            ));
                        IsFalse(() => IsAnyChannel  (x.BuffBound.AudioFileOutput ));
                        IsFalse(() => IsEveryChannel(x.BuffBound.Buff            ));
                        IsFalse(() => IsEveryChannel(x.BuffBound.AudioFileOutput ));
                        IsTrue (() => IsStereo      (x.BuffBound.Buff            )); // By Design: Every state maps to stereo, 2 channels, or single L/R.
                        IsTrue (() => IsStereo      (x.BuffBound.AudioFileOutput ));
        AreEqual(CenterChannel, () => ConfigWishes.Channel       (x.BuffBound.Buff           ));
        AreEqual(CenterChannel, () => ConfigWishes.Channel       (x.BuffBound.AudioFileOutput));
        AreEqual(CenterChannel, () => ConfigWishes.GetChannel    (x.BuffBound.Buff           ));
        AreEqual(CenterChannel, () => ConfigWishes.GetChannel    (x.BuffBound.AudioFileOutput));
        AreEqual(MonoChannels , () => ConfigWishes.Channels      (x.BuffBound.Buff           ));
        AreEqual(MonoChannels , () => ConfigWishes.Channels      (x.BuffBound.AudioFileOutput));
        AreEqual(MonoChannels , () => ConfigWishes.GetChannels   (x.BuffBound.Buff           ));
        AreEqual(MonoChannels , () => ConfigWishes.GetChannels   (x.BuffBound.AudioFileOutput));
                        IsTrue (() => ConfigWishes.IsCenter      (x.BuffBound.Buff           ));
                        IsTrue (() => ConfigWishes.IsCenter      (x.BuffBound.AudioFileOutput));
                        IsTrue (() => ConfigWishes.IsLeft        (x.BuffBound.Buff           )); // By Design: Buff thinks Channel 0 are Left & Center.
                        IsTrue (() => ConfigWishes.IsLeft        (x.BuffBound.AudioFileOutput));
                        IsTrue (() => ConfigWishes.IsMono        (x.BuffBound.Buff           ));
                        IsTrue (() => ConfigWishes.IsMono        (x.BuffBound.AudioFileOutput));
                        IsFalse(() => ConfigWishes.IsRight       (x.BuffBound.Buff           ));
                        IsFalse(() => ConfigWishes.IsRight       (x.BuffBound.AudioFileOutput));
                        IsFalse(() => ConfigWishes.IsNoChannel   (x.BuffBound.Buff           ));
                        IsFalse(() => ConfigWishes.IsNoChannel   (x.BuffBound.AudioFileOutput));
                        IsFalse(() => ConfigWishes.IsAnyChannel  (x.BuffBound.Buff           ));
                        IsFalse(() => ConfigWishes.IsAnyChannel  (x.BuffBound.AudioFileOutput));
                        IsFalse(() => ConfigWishes.IsEveryChannel(x.BuffBound.Buff           ));
                        IsFalse(() => ConfigWishes.IsEveryChannel(x.BuffBound.AudioFileOutput));
                        IsTrue (() => ConfigWishes.IsStereo      (x.BuffBound.Buff           )); // By Design: Every state maps to stereo, 2 channels, or single L/R.
                        IsTrue (() => ConfigWishes.IsStereo      (x.BuffBound.AudioFileOutput));
        }

        private void Assert_StereoBuff_Getters(TapeEntities x)
        {
                       IsNotNull(() => x                          );
                       IsNotNull(() => x.BuffBound.Buff           );
                       IsNotNull(() => x.BuffBound.AudioFileOutput);
        AreEqual(EmptyChannel  , () => x.BuffBound.Buff           .Channel       ());
        AreEqual(EmptyChannel  , () => x.BuffBound.AudioFileOutput.Channel       ());
        AreEqual(EmptyChannel  , () => x.BuffBound.Buff           .GetChannel    ());
        AreEqual(EmptyChannel  , () => x.BuffBound.AudioFileOutput.GetChannel    ());
        AreEqual(StereoChannels, () => x.BuffBound.Buff           .Channels      ());
        AreEqual(StereoChannels, () => x.BuffBound.AudioFileOutput.Channels      ());
        AreEqual(StereoChannels, () => x.BuffBound.Buff           .GetChannels   ());
        AreEqual(StereoChannels, () => x.BuffBound.AudioFileOutput.GetChannels   ());
                         IsTrue (() => x.BuffBound.Buff           .IsAnyChannel  ());
                         IsTrue (() => x.BuffBound.AudioFileOutput.IsAnyChannel  ());
                         IsTrue (() => x.BuffBound.Buff           .IsEveryChannel());
                         IsTrue (() => x.BuffBound.AudioFileOutput.IsEveryChannel());
                         IsTrue (() => x.BuffBound.Buff           .IsNoChannel   ());
                         IsTrue (() => x.BuffBound.AudioFileOutput.IsNoChannel   ());
                         IsTrue (() => x.BuffBound.Buff           .IsStereo      ());
                         IsTrue (() => x.BuffBound.AudioFileOutput.IsStereo      ());
                         IsFalse(() => x.BuffBound.Buff           .IsCenter      ());
                         IsFalse(() => x.BuffBound.AudioFileOutput.IsCenter      ());
                         IsFalse(() => x.BuffBound.Buff           .IsLeft        ());
                         IsFalse(() => x.BuffBound.AudioFileOutput.IsLeft        ());
                         IsFalse(() => x.BuffBound.Buff           .IsRight       ());
                         IsFalse(() => x.BuffBound.AudioFileOutput.IsRight       ());
                         IsFalse(() => x.BuffBound.Buff           .IsMono        ());
                         IsFalse(() => x.BuffBound.AudioFileOutput.IsMono        ());
        AreEqual(EmptyChannel  , () => Channel       (x.BuffBound.Buff            ));
        AreEqual(EmptyChannel  , () => Channel       (x.BuffBound.AudioFileOutput ));
        AreEqual(EmptyChannel  , () => GetChannel    (x.BuffBound.Buff            ));
        AreEqual(EmptyChannel  , () => GetChannel    (x.BuffBound.AudioFileOutput ));
        AreEqual(StereoChannels, () => Channels      (x.BuffBound.Buff            ));
        AreEqual(StereoChannels, () => Channels      (x.BuffBound.AudioFileOutput ));
        AreEqual(StereoChannels, () => GetChannels   (x.BuffBound.Buff            ));
        AreEqual(StereoChannels, () => GetChannels   (x.BuffBound.AudioFileOutput ));
                         IsTrue (() => IsAnyChannel  (x.BuffBound.Buff            ));
                         IsTrue (() => IsAnyChannel  (x.BuffBound.AudioFileOutput ));
                         IsTrue (() => IsEveryChannel(x.BuffBound.Buff            ));
                         IsTrue (() => IsEveryChannel(x.BuffBound.AudioFileOutput ));
                         IsTrue (() => IsNoChannel   (x.BuffBound.Buff            ));
                         IsTrue (() => IsNoChannel   (x.BuffBound.AudioFileOutput ));
                         IsTrue (() => IsStereo      (x.BuffBound.Buff            ));
                         IsTrue (() => IsStereo      (x.BuffBound.AudioFileOutput ));
                         IsFalse(() => IsCenter      (x.BuffBound.Buff            ));
                         IsFalse(() => IsCenter      (x.BuffBound.AudioFileOutput ));
                         IsFalse(() => IsLeft        (x.BuffBound.Buff            ));
                         IsFalse(() => IsLeft        (x.BuffBound.AudioFileOutput ));
                         IsFalse(() => IsRight       (x.BuffBound.Buff            ));
                         IsFalse(() => IsRight       (x.BuffBound.AudioFileOutput ));
                         IsFalse(() => IsMono        (x.BuffBound.Buff            ));
                         IsFalse(() => IsMono        (x.BuffBound.AudioFileOutput ));
        AreEqual(EmptyChannel  , () => ConfigWishes.Channel       (x.BuffBound.Buff           ));
        AreEqual(EmptyChannel  , () => ConfigWishes.Channel       (x.BuffBound.AudioFileOutput));
        AreEqual(EmptyChannel  , () => ConfigWishes.GetChannel    (x.BuffBound.Buff           ));
        AreEqual(EmptyChannel  , () => ConfigWishes.GetChannel    (x.BuffBound.AudioFileOutput));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.BuffBound.Buff           ));
        AreEqual(StereoChannels, () => ConfigWishes.Channels      (x.BuffBound.AudioFileOutput));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.BuffBound.Buff           ));
        AreEqual(StereoChannels, () => ConfigWishes.GetChannels   (x.BuffBound.AudioFileOutput));
                         IsTrue (() => ConfigWishes.IsAnyChannel  (x.BuffBound.Buff           ));
                         IsTrue (() => ConfigWishes.IsAnyChannel  (x.BuffBound.AudioFileOutput));
                         IsTrue (() => ConfigWishes.IsEveryChannel(x.BuffBound.Buff           ));
                         IsTrue (() => ConfigWishes.IsEveryChannel(x.BuffBound.AudioFileOutput));
                         IsTrue (() => ConfigWishes.IsNoChannel   (x.BuffBound.Buff           ));
                         IsTrue (() => ConfigWishes.IsNoChannel   (x.BuffBound.AudioFileOutput));
                         IsTrue (() => ConfigWishes.IsStereo      (x.BuffBound.Buff           ));
                         IsTrue (() => ConfigWishes.IsStereo      (x.BuffBound.AudioFileOutput));
                         IsFalse(() => ConfigWishes.IsCenter      (x.BuffBound.Buff           ));
                         IsFalse(() => ConfigWishes.IsCenter      (x.BuffBound.AudioFileOutput));
                         IsFalse(() => ConfigWishes.IsLeft        (x.BuffBound.Buff           ));
                         IsFalse(() => ConfigWishes.IsLeft        (x.BuffBound.AudioFileOutput));
                         IsFalse(() => ConfigWishes.IsRight       (x.BuffBound.Buff           ));
                         IsFalse(() => ConfigWishes.IsRight       (x.BuffBound.AudioFileOutput));
                         IsFalse(() => ConfigWishes.IsMono        (x.BuffBound.Buff           ));
                         IsFalse(() => ConfigWishes.IsMono        (x.BuffBound.AudioFileOutput));
        }

        private void Assert_LeftBuff_Getters(TapeEntities x)
        {   
                     IsNotNull(() => x                          );
                     IsNotNull(() => x.BuffBound.Buff           );
                     IsNotNull(() => x.BuffBound.AudioFileOutput);
        AreEqual(LeftChannel , () => x.BuffBound.Buff           .Channel       ());
        AreEqual(LeftChannel , () => x.BuffBound.AudioFileOutput.Channel       ());
        AreEqual(LeftChannel , () => x.BuffBound.Buff           .GetChannel    ());
        AreEqual(LeftChannel , () => x.BuffBound.AudioFileOutput.GetChannel    ());
        AreEqual(MonoChannels, () => x.BuffBound.Buff           .Channels      ()); // By Design: Left-only is set as SpeakerSetup Mono to trick the back-end.
        AreEqual(MonoChannels, () => x.BuffBound.AudioFileOutput.Channels      ());
        AreEqual(MonoChannels, () => x.BuffBound.Buff           .GetChannels   ());
        AreEqual(MonoChannels, () => x.BuffBound.AudioFileOutput.GetChannels   ());
                       IsTrue (() => x.BuffBound.Buff           .IsLeft        ());
                       IsTrue (() => x.BuffBound.AudioFileOutput.IsLeft        ());
                       IsTrue (() => x.BuffBound.Buff           .IsCenter      ()); // By Design: Buff thinks Channel 0 are Left & Center.
                       IsTrue (() => x.BuffBound.AudioFileOutput.IsCenter      ());
                       IsTrue (() => x.BuffBound.Buff           .IsMono        ()); // By Design: Buff & AudioFileOutput treat Left as Mono.
                       IsTrue (() => x.BuffBound.AudioFileOutput.IsMono        ());
                       IsTrue (() => x.BuffBound.Buff           .IsStereo      ()); // By Design: Every state maps to stereo, 2 channels, or single L/R.
                       IsTrue (() => x.BuffBound.AudioFileOutput.IsStereo      ());
                       IsFalse(() => x.BuffBound.Buff           .IsAnyChannel  ());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsAnyChannel  ());
                       IsFalse(() => x.BuffBound.Buff           .IsEveryChannel());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsEveryChannel());
                       IsFalse(() => x.BuffBound.Buff           .IsNoChannel   ());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsNoChannel   ());
                       IsFalse(() => x.BuffBound.Buff           .IsRight       ());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsRight       ());
        AreEqual(LeftChannel , () => Channel       (x.BuffBound.Buff            ));
        AreEqual(LeftChannel , () => Channel       (x.BuffBound.AudioFileOutput ));
        AreEqual(LeftChannel , () => GetChannel    (x.BuffBound.Buff            ));
        AreEqual(LeftChannel , () => GetChannel    (x.BuffBound.AudioFileOutput ));
        AreEqual(MonoChannels, () => Channels      (x.BuffBound.Buff            )); // By Design: Left-only is set as SpeakerSetup Mono to trick the back-end.
        AreEqual(MonoChannels, () => Channels      (x.BuffBound.AudioFileOutput ));
        AreEqual(MonoChannels, () => GetChannels   (x.BuffBound.Buff            ));
        AreEqual(MonoChannels, () => GetChannels   (x.BuffBound.AudioFileOutput ));
                       IsTrue (() => IsLeft        (x.BuffBound.Buff            ));
                       IsTrue (() => IsLeft        (x.BuffBound.AudioFileOutput ));
                       IsTrue (() => IsCenter      (x.BuffBound.Buff            )); // By Design: Buff thinks Channel 0 are Left & Center.
                       IsTrue (() => IsCenter      (x.BuffBound.AudioFileOutput ));
                       IsTrue (() => IsMono        (x.BuffBound.Buff            )); // By Design: Buff & AudioFileOutput treat Left as Mono.
                       IsTrue (() => IsMono        (x.BuffBound.AudioFileOutput ));
                       IsTrue (() => IsStereo      (x.BuffBound.Buff            )); // By Design: Every state maps to stereo, 2 channels, or single L/R.
                       IsTrue (() => IsStereo      (x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsAnyChannel  (x.BuffBound.Buff            ));
                       IsFalse(() => IsAnyChannel  (x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsEveryChannel(x.BuffBound.Buff            ));
                       IsFalse(() => IsEveryChannel(x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsNoChannel   (x.BuffBound.Buff            ));
                       IsFalse(() => IsNoChannel   (x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsRight       (x.BuffBound.Buff            ));
                       IsFalse(() => IsRight       (x.BuffBound.AudioFileOutput ));
        AreEqual(LeftChannel,  () => ConfigWishes.Channel       (x.BuffBound.Buff           ));
        AreEqual(LeftChannel,  () => ConfigWishes.Channel       (x.BuffBound.AudioFileOutput));
        AreEqual(LeftChannel,  () => ConfigWishes.GetChannel    (x.BuffBound.Buff           ));
        AreEqual(LeftChannel,  () => ConfigWishes.GetChannel    (x.BuffBound.AudioFileOutput));
        AreEqual(MonoChannels, () => ConfigWishes.Channels      (x.BuffBound.Buff           )); // By Design: Left-only is set as SpeakerSetup Mono to trick the back-end.
        AreEqual(MonoChannels, () => ConfigWishes.Channels      (x.BuffBound.AudioFileOutput));
        AreEqual(MonoChannels, () => ConfigWishes.GetChannels   (x.BuffBound.Buff           ));
        AreEqual(MonoChannels, () => ConfigWishes.GetChannels   (x.BuffBound.AudioFileOutput));
                       IsTrue (() => ConfigWishes.IsLeft        (x.BuffBound.Buff           ));
                       IsTrue (() => ConfigWishes.IsLeft        (x.BuffBound.AudioFileOutput));
                       IsTrue (() => ConfigWishes.IsCenter      (x.BuffBound.Buff           )); // By Design: Buff thinks Channel 0 are Left & Center.
                       IsTrue (() => ConfigWishes.IsCenter      (x.BuffBound.AudioFileOutput));
                       IsTrue (() => ConfigWishes.IsMono        (x.BuffBound.Buff           )); // By Design: Buff & AudioFileOutput treat Left as Mono.
                       IsTrue (() => ConfigWishes.IsMono        (x.BuffBound.AudioFileOutput));
                       IsTrue (() => ConfigWishes.IsStereo      (x.BuffBound.Buff           )); // By Design: Every state maps to stereo, 2 channels, or single L/R.
                       IsTrue (() => ConfigWishes.IsStereo      (x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsAnyChannel  (x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsAnyChannel  (x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsEveryChannel(x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsEveryChannel(x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsNoChannel   (x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsNoChannel   (x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsRight       (x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsRight       (x.BuffBound.AudioFileOutput));
        }

        private void Assert_RightBuff_Getters(TapeEntities x)
        {
                     IsNotNull(() => x                          );
                     IsNotNull(() => x.BuffBound.Buff           );
                     IsNotNull(() => x.BuffBound.AudioFileOutput);
        AreEqual(RightChannel, () => x.BuffBound.Buff           .Channel       ());
        AreEqual(RightChannel, () => x.BuffBound.AudioFileOutput.Channel       ());
        AreEqual(RightChannel, () => x.BuffBound.Buff           .GetChannel    ());
        AreEqual(RightChannel, () => x.BuffBound.AudioFileOutput.GetChannel    ());
        AreEqual(MonoChannels, () => x.BuffBound.Buff           .Channels      ()); // By Design: Left-only is set as SpeakerSetup Mono to trick the back-end.
        AreEqual(MonoChannels, () => x.BuffBound.AudioFileOutput.Channels      ());
        AreEqual(MonoChannels, () => x.BuffBound.Buff           .GetChannels   ());
        AreEqual(MonoChannels, () => x.BuffBound.AudioFileOutput.GetChannels   ());
                       IsTrue (() => x.BuffBound.Buff           .IsRight       ());
                       IsTrue (() => x.BuffBound.AudioFileOutput.IsRight       ());
                       IsTrue (() => x.BuffBound.Buff           .IsStereo      ());
                       IsTrue (() => x.BuffBound.AudioFileOutput.IsStereo      ());
                       IsFalse(() => x.BuffBound.Buff           .IsAnyChannel  ());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsAnyChannel  ());
                       IsFalse(() => x.BuffBound.Buff           .IsEveryChannel());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsEveryChannel());
                       IsFalse(() => x.BuffBound.Buff           .IsNoChannel   ());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsNoChannel   ());
                       IsFalse(() => x.BuffBound.Buff           .IsCenter      ());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsCenter      ());
                       IsFalse(() => x.BuffBound.Buff           .IsLeft        ());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsLeft        ());
                       IsFalse(() => x.BuffBound.Buff           .IsMono        ());
                       IsFalse(() => x.BuffBound.AudioFileOutput.IsMono        ());
        AreEqual(RightChannel, () => Channel       (x.BuffBound.Buff            ));
        AreEqual(RightChannel, () => Channel       (x.BuffBound.AudioFileOutput ));
        AreEqual(RightChannel, () => GetChannel    (x.BuffBound.Buff            ));
        AreEqual(RightChannel, () => GetChannel    (x.BuffBound.AudioFileOutput ));
        AreEqual(MonoChannels, () => Channels      (x.BuffBound.Buff            )); // By Design: Left-only is set as SpeakerSetup Mono to trick the back-end.
        AreEqual(MonoChannels, () => Channels      (x.BuffBound.AudioFileOutput ));
        AreEqual(MonoChannels, () => GetChannels   (x.BuffBound.Buff            ));
        AreEqual(MonoChannels, () => GetChannels   (x.BuffBound.AudioFileOutput ));
                       IsTrue (() => IsRight       (x.BuffBound.Buff            ));
                       IsTrue (() => IsRight       (x.BuffBound.AudioFileOutput ));
                       IsTrue (() => IsStereo      (x.BuffBound.Buff            ));
                       IsTrue (() => IsStereo      (x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsAnyChannel  (x.BuffBound.Buff            ));
                       IsFalse(() => IsAnyChannel  (x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsEveryChannel(x.BuffBound.Buff            ));
                       IsFalse(() => IsEveryChannel(x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsNoChannel   (x.BuffBound.Buff            ));
                       IsFalse(() => IsNoChannel   (x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsCenter      (x.BuffBound.Buff            ));
                       IsFalse(() => IsCenter      (x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsLeft        (x.BuffBound.Buff            ));
                       IsFalse(() => IsLeft        (x.BuffBound.AudioFileOutput ));
                       IsFalse(() => IsMono        (x.BuffBound.Buff            ));
                       IsFalse(() => IsMono        (x.BuffBound.AudioFileOutput ));
        AreEqual(RightChannel, () => ConfigWishes.Channel       (x.BuffBound.Buff           ));
        AreEqual(RightChannel, () => ConfigWishes.Channel       (x.BuffBound.AudioFileOutput));
        AreEqual(RightChannel, () => ConfigWishes.GetChannel    (x.BuffBound.Buff           ));
        AreEqual(RightChannel, () => ConfigWishes.GetChannel    (x.BuffBound.AudioFileOutput));
        AreEqual(MonoChannels, () => ConfigWishes.Channels      (x.BuffBound.Buff           )); // By Design: Left-only is set as SpeakerSetup Mono to trick the back-end.
        AreEqual(MonoChannels, () => ConfigWishes.Channels      (x.BuffBound.AudioFileOutput));
        AreEqual(MonoChannels, () => ConfigWishes.GetChannels   (x.BuffBound.Buff           ));
        AreEqual(MonoChannels, () => ConfigWishes.GetChannels   (x.BuffBound.AudioFileOutput));
                       IsTrue (() => ConfigWishes.IsRight       (x.BuffBound.Buff           ));
                       IsTrue (() => ConfigWishes.IsRight       (x.BuffBound.AudioFileOutput));
                       IsTrue (() => ConfigWishes.IsStereo      (x.BuffBound.Buff           ));
                       IsTrue (() => ConfigWishes.IsStereo      (x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsAnyChannel  (x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsAnyChannel  (x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsEveryChannel(x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsEveryChannel(x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsNoChannel   (x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsNoChannel   (x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsCenter      (x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsCenter      (x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsLeft        (x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsLeft        (x.BuffBound.AudioFileOutput));
                       IsFalse(() => ConfigWishes.IsMono        (x.BuffBound.Buff           ));
                       IsFalse(() => ConfigWishes.IsMono        (x.BuffBound.AudioFileOutput));
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
                                IsTrue (() => channelEnum.IsCenter             ());
                                IsTrue (() => channelEnum.IsMono               ());
                                IsFalse(() => channelEnum.IsLeft               ());
                                IsFalse(() => channelEnum.IsRight              ());
                                IsFalse(() => channelEnum.IsStereo             ());
                AreEqual(CenterChannel, () => channelEnum.Channel              ());
                AreEqual(CenterChannel, () => channelEnum.GetChannel           ());
                AreEqual(CenterChannel, () => channelEnum.EnumToChannel        ());
                AreEqual(CenterChannel, () => channelEnum.ChannelEnumToChannel ());
                AreEqual(MonoChannels , () => channelEnum.Channels             ());
                AreEqual(MonoChannels , () => channelEnum.GetChannels          ());
                AreEqual(MonoChannels , () => channelEnum.ChannelEnumToChannels());
                                IsTrue (() => IsCenter             (channelEnum));
                                IsTrue (() => IsMono               (channelEnum));
                                IsFalse(() => IsLeft               (channelEnum));
                                IsFalse(() => IsRight              (channelEnum));
                                IsFalse(() => IsStereo             (channelEnum));
                AreEqual(CenterChannel, () => Channel              (channelEnum));
                AreEqual(CenterChannel, () => GetChannel           (channelEnum));
                AreEqual(CenterChannel, () => EnumToChannel        (channelEnum));
                AreEqual(CenterChannel, () => ChannelEnumToChannel (channelEnum));
                AreEqual(MonoChannels , () => Channels             (channelEnum));
                AreEqual(MonoChannels , () => GetChannels          (channelEnum));
                AreEqual(MonoChannels , () => ChannelEnumToChannels(channelEnum));
                                IsTrue (() => ConfigWishes.IsCenter             (channelEnum));
                                IsTrue (() => ConfigWishes.IsMono               (channelEnum));
                                IsFalse(() => ConfigWishes.IsLeft               (channelEnum));
                                IsFalse(() => ConfigWishes.IsRight              (channelEnum));
                                IsFalse(() => ConfigWishes.IsStereo             (channelEnum));
                AreEqual(CenterChannel, () => ConfigWishes.Channel              (channelEnum));
                AreEqual(CenterChannel, () => ConfigWishes.GetChannel           (channelEnum));
                AreEqual(CenterChannel, () => ConfigWishes.EnumToChannel        (channelEnum));
                AreEqual(CenterChannel, () => ConfigWishes.ChannelEnumToChannel (channelEnum));
                AreEqual(MonoChannels , () => ConfigWishes.Channels             (channelEnum));
                AreEqual(MonoChannels , () => ConfigWishes.GetChannels          (channelEnum));
                AreEqual(MonoChannels , () => ConfigWishes.ChannelEnumToChannels(channelEnum));
            }
            else if (c.channels == StereoChannels)
            {
                IsFalse(                    () => channelEnum.IsCenter             ());
                AreEqual(c.channel == 0,    () => channelEnum.IsLeft               ());
                AreEqual(c.channel == 1,    () => channelEnum.IsRight              ());
                AreEqual(c.channel == null, () => channelEnum.IsAnyChannel         ());
                AreEqual(c.channel == null, () => channelEnum.IsEveryChannel       ());
                AreEqual(c.channel == null, () => channelEnum.IsNoChannel          ());
                AreEqual(c.channel,         () => channelEnum.Channel              ());
                AreEqual(c.channel,         () => channelEnum.GetChannel           ());
                AreEqual(c.channel,         () => channelEnum.EnumToChannel        ());
                AreEqual(c.channel,         () => channelEnum.ChannelEnumToChannel ());
                IsTrue (                    () => channelEnum.IsStereo             ());
                IsFalse(                    () => channelEnum.IsMono               ());
                AreEqual(StereoChannels,    () => channelEnum.Channels             ());
                AreEqual(StereoChannels,    () => channelEnum.GetChannels          ());
                AreEqual(StereoChannels,    () => channelEnum.ChannelEnumToChannels());
                IsFalse(                    () => IsCenter             (channelEnum));
                AreEqual(c.channel == 0,    () => IsLeft               (channelEnum));
                AreEqual(c.channel == 1,    () => IsRight              (channelEnum));
                AreEqual(c.channel == null, () => IsAnyChannel         (channelEnum));
                AreEqual(c.channel == null, () => IsEveryChannel       (channelEnum));
                AreEqual(c.channel == null, () => IsNoChannel          (channelEnum));
                AreEqual(c.channel,         () => Channel              (channelEnum));
                AreEqual(c.channel,         () => GetChannel           (channelEnum));
                AreEqual(c.channel,         () => EnumToChannel        (channelEnum));
                AreEqual(c.channel,         () => ChannelEnumToChannel (channelEnum));
                IsTrue (                    () => IsStereo             (channelEnum));
                IsFalse(                    () => IsMono               (channelEnum));
                AreEqual(StereoChannels,    () => Channels             (channelEnum));
                AreEqual(StereoChannels,    () => GetChannels          (channelEnum));
                AreEqual(StereoChannels,    () => ChannelEnumToChannels(channelEnum));
                IsFalse(                    () => ConfigWishes.IsCenter             (channelEnum));
                AreEqual(c.channel == 0,    () => ConfigWishes.IsLeft               (channelEnum));
                AreEqual(c.channel == 1,    () => ConfigWishes.IsRight              (channelEnum));
                AreEqual(c.channel == null, () => ConfigWishes.IsAnyChannel         (channelEnum));
                AreEqual(c.channel == null, () => ConfigWishes.IsEveryChannel       (channelEnum));
                AreEqual(c.channel == null, () => ConfigWishes.IsNoChannel          (channelEnum));
                AreEqual(c.channel,         () => ConfigWishes.Channel              (channelEnum));
                AreEqual(c.channel,         () => ConfigWishes.GetChannel           (channelEnum));
                AreEqual(c.channel,         () => ConfigWishes.EnumToChannel        (channelEnum));
                AreEqual(c.channel,         () => ConfigWishes.ChannelEnumToChannel (channelEnum));
                IsTrue (                    () => ConfigWishes.IsStereo             (channelEnum));
                IsFalse(                    () => ConfigWishes.IsMono               (channelEnum));
                AreEqual(StereoChannels,    () => ConfigWishes.Channels             (channelEnum));
                AreEqual(StereoChannels,    () => ConfigWishes.GetChannels          (channelEnum));
                AreEqual(StereoChannels,    () => ConfigWishes.ChannelEnumToChannels(channelEnum));
            }
            // ncrunch: no coverage start
            else
            {
                Fail($"Unsupported combination of values: {new{ channelEnum, c.channels, c.channel }}"); 
            }
            // ncrunch: no coverage end
        }
            
        private void Assert_Immutable_Getters(Channel channelEntity, (int channels, int? channel) c)
        {
            if (c.channels == MonoChannels)
            {
                                IsTrue (() => channelEntity.IsCenter               ());
                                IsTrue (() => channelEntity.IsMono                 ());
                                IsFalse(() => channelEntity.IsLeft                 ());
                                IsFalse(() => channelEntity.IsRight                ());
                                IsFalse(() => channelEntity.IsStereo               ());
                AreEqual(CenterChannel, () => channelEntity.Channel                ());
                AreEqual(CenterChannel, () => channelEntity.GetChannel             ());
                AreEqual(CenterChannel, () => channelEntity.EntityToChannel        ());
                AreEqual(CenterChannel, () => channelEntity.ChannelEntityToChannel ());
                AreEqual(MonoChannels , () => channelEntity.Channels               ());
                AreEqual(MonoChannels , () => channelEntity.GetChannels            ());
                AreEqual(MonoChannels , () => channelEntity.ChannelEntityToChannels());
                                IsTrue (() => IsCenter               (channelEntity));
                                IsTrue (() => IsMono                 (channelEntity));
                                IsFalse(() => IsLeft                 (channelEntity));
                                IsFalse(() => IsRight                (channelEntity));
                                IsFalse(() => IsStereo               (channelEntity));
                AreEqual(CenterChannel, () => Channel                (channelEntity));
                AreEqual(CenterChannel, () => GetChannel             (channelEntity));
                AreEqual(CenterChannel, () => EntityToChannel        (channelEntity));
                AreEqual(CenterChannel, () => ChannelEntityToChannel (channelEntity));
                AreEqual(MonoChannels , () => Channels               (channelEntity));
                AreEqual(MonoChannels , () => GetChannels            (channelEntity));
                AreEqual(MonoChannels , () => ChannelEntityToChannels(channelEntity));
                                IsTrue (() => ConfigWishes.IsCenter               (channelEntity));
                                IsTrue (() => ConfigWishes.IsMono                 (channelEntity));
                                IsFalse(() => ConfigWishes.IsLeft                 (channelEntity));
                                IsFalse(() => ConfigWishes.IsRight                (channelEntity));
                                IsFalse(() => ConfigWishes.IsStereo               (channelEntity));
                AreEqual(CenterChannel, () => ConfigWishes.Channel                (channelEntity));
                AreEqual(CenterChannel, () => ConfigWishes.GetChannel             (channelEntity));
                AreEqual(CenterChannel, () => ConfigWishes.EntityToChannel        (channelEntity));
                AreEqual(CenterChannel, () => ConfigWishes.ChannelEntityToChannel (channelEntity));
                AreEqual(MonoChannels , () => ConfigWishes.Channels               (channelEntity));
                AreEqual(MonoChannels , () => ConfigWishes.GetChannels            (channelEntity));
                AreEqual(MonoChannels , () => ConfigWishes.ChannelEntityToChannels(channelEntity));
            }
            else if (c.channels == StereoChannels)
            {
                IsFalse(                    () => channelEntity.IsCenter               ());
                AreEqual(c.channel == 0,    () => channelEntity.IsLeft                 ());
                AreEqual(c.channel == 1,    () => channelEntity.IsRight                ());
                AreEqual(c.channel == null, () => channelEntity.IsAnyChannel           ());
                AreEqual(c.channel == null, () => channelEntity.IsEveryChannel         ());
                AreEqual(c.channel == null, () => channelEntity.IsNoChannel            ());
                AreEqual(c.channel,         () => channelEntity.Channel                ());
                AreEqual(c.channel,         () => channelEntity.GetChannel             ());
                AreEqual(c.channel,         () => channelEntity.EntityToChannel        ());
                AreEqual(c.channel,         () => channelEntity.ChannelEntityToChannel ());
                IsTrue (                    () => channelEntity.IsStereo               ());
                IsFalse(                    () => channelEntity.IsMono                 ());
                AreEqual(StereoChannels,    () => channelEntity.Channels               ());
                AreEqual(StereoChannels,    () => channelEntity.GetChannels            ());
                AreEqual(StereoChannels,    () => channelEntity.ChannelEntityToChannels());
                IsFalse(                    () => IsCenter               (channelEntity));
                AreEqual(c.channel == 0,    () => IsLeft                 (channelEntity));
                AreEqual(c.channel == 1,    () => IsRight                (channelEntity));
                AreEqual(c.channel == null, () => IsAnyChannel           (channelEntity));
                AreEqual(c.channel == null, () => IsEveryChannel         (channelEntity));
                AreEqual(c.channel == null, () => IsNoChannel            (channelEntity));
                AreEqual(c.channel,         () => Channel                (channelEntity));
                AreEqual(c.channel,         () => GetChannel             (channelEntity));
                AreEqual(c.channel,         () => EntityToChannel        (channelEntity));
                AreEqual(c.channel,         () => ChannelEntityToChannel (channelEntity));
                IsTrue (                    () => IsStereo               (channelEntity));
                IsFalse(                    () => IsMono                 (channelEntity));
                AreEqual(StereoChannels,    () => Channels               (channelEntity));
                AreEqual(StereoChannels,    () => GetChannels            (channelEntity));
                AreEqual(StereoChannels,    () => ChannelEntityToChannels(channelEntity));
                IsFalse(                    () => ConfigWishes.IsCenter               (channelEntity));
                AreEqual(c.channel == 0,    () => ConfigWishes.IsLeft                 (channelEntity));
                AreEqual(c.channel == 1,    () => ConfigWishes.IsRight                (channelEntity));
                AreEqual(c.channel == null, () => ConfigWishes.IsAnyChannel           (channelEntity));
                AreEqual(c.channel == null, () => ConfigWishes.IsEveryChannel         (channelEntity));
                AreEqual(c.channel == null, () => ConfigWishes.IsNoChannel            (channelEntity));
                AreEqual(c.channel,         () => ConfigWishes.Channel                (channelEntity));
                AreEqual(c.channel,         () => ConfigWishes.GetChannel             (channelEntity));
                AreEqual(c.channel,         () => ConfigWishes.EntityToChannel        (channelEntity));
                AreEqual(c.channel,         () => ConfigWishes.ChannelEntityToChannel (channelEntity));
                IsTrue (                    () => ConfigWishes.IsStereo               (channelEntity));
                IsFalse(                    () => ConfigWishes.IsMono                 (channelEntity));
                AreEqual(StereoChannels,    () => ConfigWishes.Channels               (channelEntity));
                AreEqual(StereoChannels,    () => ConfigWishes.GetChannels            (channelEntity));
                AreEqual(StereoChannels,    () => ConfigWishes.ChannelEntityToChannels(channelEntity));
            }
            // ncrunch: no coverage start
            else
            {
                Fail($"Unsupported combination of values: {channelEntity?.ID} - {channelEntity?.Name}, {new{ c.channels, c.channel }}");
            }
            // ncrunch: no coverage end
        }
        
        // Test Data Helpers

        private ConfigTestEntities CreateTestEntities((int? channels, int? channel) c)
            => CreateTestEntities(c.channels, c.channel);

        private ConfigTestEntities CreateTestEntities(int? channels = null, int? channel = null)
            => new ConfigTestEntities(x => x.WithChannels(channels)
                                            .WithChannel (channel));
        
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