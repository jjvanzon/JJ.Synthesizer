using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Existence.Core.FilledInHelper;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
using System.Runtime.CompilerServices;
// ReSharper disable ArrangeStaticMemberQualifier
// ReSharper disable UnusedMember.Local

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class CourtesyByteWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(ParameterSetInitWithEmpties))]
        public void Init_CourtesyBytes(string descriptor, int courtesyBytes, int? courtesyFrames, int? bits, int? channels)
        { 
            var init = (courtesyBytes, courtesyFrames, bits, channels);
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init.courtesyBytes);
        }

        [TestMethod]
        [DynamicData(nameof(ParameterSetSmallWithEmpties))]
        public void SynthBound_CourtesyBytes(
            string descriptor,
            int initCourtesyBytes, int? initCourtesyFrames, int? initBits, int? initChannels,
            int courtesyBytes, int? courtesyFrames, int? bits, int? channels)
        {
            var init = (courtesyBytes: initCourtesyBytes, courtesyFrames: initCourtesyFrames, 
                        bits: initBits, channels: initChannels);
            var val  = (courtesyBytes, courtesyFrames, bits, channels);
            
            void AssertProp(Action<SynthBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.courtesyBytes);
                
                setter(x.SynthBound);
                
                Assert_SynthBound_Getters(x, val.courtesyBytes);
                Assert_TapeBound_Getters(x, init.courtesyBytes);
                
                x.Record();
                Assert_All_Getters(x, val.courtesyBytes);
            }

            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels); AreEqual(x.SynthWishes,    x.SynthWishes   .CourtesyBytes    (val.courtesyBytes)); });
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels); AreEqual(x.FlowNode,       x.FlowNode      .CourtesyBytes    (val.courtesyBytes)); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels); AreEqual(x.ConfigResolver, x.ConfigResolver.CourtesyBytes    (val.courtesyBytes)); });
            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels); AreEqual(x.SynthWishes,    x.SynthWishes   .WithCourtesyBytes(val.courtesyBytes)); });
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels); AreEqual(x.FlowNode,       x.FlowNode      .WithCourtesyBytes(val.courtesyBytes)); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels); AreEqual(x.ConfigResolver, x.ConfigResolver.WithCourtesyBytes(val.courtesyBytes)); });
            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels); AreEqual(x.SynthWishes,    x.SynthWishes   .SetCourtesyBytes (val.courtesyBytes)); });
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels); AreEqual(x.FlowNode,       x.FlowNode      .SetCourtesyBytes (val.courtesyBytes)); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels); AreEqual(x.ConfigResolver, x.ConfigResolver.SetCourtesyBytes (val.courtesyBytes)); });
            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels); AreEqual(x.SynthWishes,    CourtesyBytes    (x.SynthWishes,    val.courtesyBytes)); });
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels); AreEqual(x.FlowNode,       CourtesyBytes    (x.FlowNode,       val.courtesyBytes)); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels); AreEqual(x.ConfigResolver, CourtesyBytes    (x.ConfigResolver, val.courtesyBytes)); });
            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels); AreEqual(x.SynthWishes,    WithCourtesyBytes(x.SynthWishes,    val.courtesyBytes)); });
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels); AreEqual(x.FlowNode,       WithCourtesyBytes(x.FlowNode,       val.courtesyBytes)); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels); AreEqual(x.ConfigResolver, WithCourtesyBytes(x.ConfigResolver, val.courtesyBytes)); });
            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels); AreEqual(x.SynthWishes,    SetCourtesyBytes (x.SynthWishes,    val.courtesyBytes)); });
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels); AreEqual(x.FlowNode,       SetCourtesyBytes (x.FlowNode,       val.courtesyBytes)); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels); AreEqual(x.ConfigResolver, SetCourtesyBytes (x.ConfigResolver, val.courtesyBytes)); });
            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels); AreEqual(x.SynthWishes,    ConfigWishes        .CourtesyBytes    (x.SynthWishes,    val.courtesyBytes)); });
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels); AreEqual(x.FlowNode,       ConfigWishes        .CourtesyBytes    (x.FlowNode,       val.courtesyBytes)); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels); AreEqual(x.ConfigResolver, ConfigWishesAccessor.CourtesyBytes    (x.ConfigResolver, val.courtesyBytes)); });
            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels); AreEqual(x.SynthWishes,    ConfigWishes        .WithCourtesyBytes(x.SynthWishes,    val.courtesyBytes)); });
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels); AreEqual(x.FlowNode,       ConfigWishes        .WithCourtesyBytes(x.FlowNode,       val.courtesyBytes)); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels); AreEqual(x.ConfigResolver, ConfigWishesAccessor.WithCourtesyBytes(x.ConfigResolver, val.courtesyBytes)); });
            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels); AreEqual(x.SynthWishes,    ConfigWishes        .SetCourtesyBytes (x.SynthWishes,    val.courtesyBytes)); });
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels); AreEqual(x.FlowNode,       ConfigWishes        .SetCourtesyBytes (x.FlowNode,       val.courtesyBytes)); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels); AreEqual(x.ConfigResolver, ConfigWishesAccessor.SetCourtesyBytes (x.ConfigResolver, val.courtesyBytes)); });
            AssertProp(x => { x.SynthWishes   .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames); }); // Vary CourtesyBytes using CourtesyFrames
            AssertProp(x => { x.FlowNode      .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames); });
            AssertProp(x => { x.ConfigResolver.Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames); });

        }

        [TestMethod]
        [DynamicData(nameof(ParameterSetSmall))]
        public void TapeBound_CourtesyBytes(
            string descriptor,
            int initCourtesyBytes, int initCourtesyFrames, int initBits, int initChannels,
            int courtesyBytes, int courtesyFrames, int bits, int channels)
        {
            var init = (courtesyBytes: initCourtesyBytes, courtesyFrames: initCourtesyFrames, bits: initBits, channels: initChannels);
            var val  = (courtesyBytes, courtesyFrames, bits, channels);

            void AssertProp(Action<TapeBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.courtesyBytes);
                
                setter(x.TapeBound);
                
                Assert_SynthBound_Getters(x, init.courtesyBytes);
                Assert_TapeBound_Getters(x, val.courtesyBytes);
                
                x.Record();
                
                Assert_All_Getters(x, init.courtesyBytes); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => { x.Tape       .Bits(val.bits).Channels(val.channels); AreEqual(x.Tape,        x.Tape       .CourtesyBytes    (val.courtesyBytes)); });
            AssertProp(x => { x.TapeConfig .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeConfig,  x.TapeConfig .CourtesyBytes    (val.courtesyBytes)); });
            AssertProp(x => { x.TapeActions.Bits(val.bits).Channels(val.channels); AreEqual(x.TapeActions, x.TapeActions.CourtesyBytes    (val.courtesyBytes)); });
            AssertProp(x => { x.TapeAction .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeAction,  x.TapeAction .CourtesyBytes    (val.courtesyBytes)); });
            AssertProp(x => { x.Tape       .Bits(val.bits).Channels(val.channels); AreEqual(x.Tape,        x.Tape       .WithCourtesyBytes(val.courtesyBytes)); });
            AssertProp(x => { x.TapeConfig .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeConfig,  x.TapeConfig .WithCourtesyBytes(val.courtesyBytes)); });
            AssertProp(x => { x.TapeActions.Bits(val.bits).Channels(val.channels); AreEqual(x.TapeActions, x.TapeActions.WithCourtesyBytes(val.courtesyBytes)); });
            AssertProp(x => { x.TapeAction .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeAction,  x.TapeAction .WithCourtesyBytes(val.courtesyBytes)); });
            AssertProp(x => { x.Tape       .Bits(val.bits).Channels(val.channels); AreEqual(x.Tape,        x.Tape       .SetCourtesyBytes (val.courtesyBytes)); });
            AssertProp(x => { x.TapeConfig .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeConfig,  x.TapeConfig .SetCourtesyBytes (val.courtesyBytes)); });
            AssertProp(x => { x.TapeActions.Bits(val.bits).Channels(val.channels); AreEqual(x.TapeActions, x.TapeActions.SetCourtesyBytes (val.courtesyBytes)); });
            AssertProp(x => { x.TapeAction .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeAction,  x.TapeAction .SetCourtesyBytes (val.courtesyBytes)); });
            AssertProp(x => { x.Tape       .Bits(val.bits).Channels(val.channels); AreEqual(x.Tape,        CourtesyBytes    (x.Tape       , val.courtesyBytes)); });
            AssertProp(x => { x.TapeConfig .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeConfig,  CourtesyBytes    (x.TapeConfig , val.courtesyBytes)); });
            AssertProp(x => { x.TapeActions.Bits(val.bits).Channels(val.channels); AreEqual(x.TapeActions, CourtesyBytes    (x.TapeActions, val.courtesyBytes)); });
            AssertProp(x => { x.TapeAction .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeAction,  CourtesyBytes    (x.TapeAction , val.courtesyBytes)); });
            AssertProp(x => { x.Tape       .Bits(val.bits).Channels(val.channels); AreEqual(x.Tape,        WithCourtesyBytes(x.Tape       , val.courtesyBytes)); });
            AssertProp(x => { x.TapeConfig .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeConfig,  WithCourtesyBytes(x.TapeConfig , val.courtesyBytes)); });
            AssertProp(x => { x.TapeActions.Bits(val.bits).Channels(val.channels); AreEqual(x.TapeActions, WithCourtesyBytes(x.TapeActions, val.courtesyBytes)); });
            AssertProp(x => { x.TapeAction .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeAction,  WithCourtesyBytes(x.TapeAction , val.courtesyBytes)); });
            AssertProp(x => { x.Tape       .Bits(val.bits).Channels(val.channels); AreEqual(x.Tape,        SetCourtesyBytes (x.Tape       , val.courtesyBytes)); });
            AssertProp(x => { x.TapeConfig .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeConfig,  SetCourtesyBytes (x.TapeConfig , val.courtesyBytes)); });
            AssertProp(x => { x.TapeActions.Bits(val.bits).Channels(val.channels); AreEqual(x.TapeActions, SetCourtesyBytes (x.TapeActions, val.courtesyBytes)); });
            AssertProp(x => { x.TapeAction .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeAction,  SetCourtesyBytes (x.TapeAction , val.courtesyBytes)); });
            AssertProp(x => { x.Tape       .Bits(val.bits).Channels(val.channels); AreEqual(x.Tape,        ConfigWishes.CourtesyBytes    (x.Tape       , val.courtesyBytes)); });
            AssertProp(x => { x.TapeConfig .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeConfig,  ConfigWishes.CourtesyBytes    (x.TapeConfig , val.courtesyBytes)); });
            AssertProp(x => { x.TapeActions.Bits(val.bits).Channels(val.channels); AreEqual(x.TapeActions, ConfigWishes.CourtesyBytes    (x.TapeActions, val.courtesyBytes)); });
            AssertProp(x => { x.TapeAction .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeAction,  ConfigWishes.CourtesyBytes    (x.TapeAction , val.courtesyBytes)); });
            AssertProp(x => { x.Tape       .Bits(val.bits).Channels(val.channels); AreEqual(x.Tape,        ConfigWishes.WithCourtesyBytes(x.Tape       , val.courtesyBytes)); });
            AssertProp(x => { x.TapeConfig .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeConfig,  ConfigWishes.WithCourtesyBytes(x.TapeConfig , val.courtesyBytes)); });
            AssertProp(x => { x.TapeActions.Bits(val.bits).Channels(val.channels); AreEqual(x.TapeActions, ConfigWishes.WithCourtesyBytes(x.TapeActions, val.courtesyBytes)); });
            AssertProp(x => { x.TapeAction .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeAction,  ConfigWishes.WithCourtesyBytes(x.TapeAction , val.courtesyBytes)); });
            AssertProp(x => { x.Tape       .Bits(val.bits).Channels(val.channels); AreEqual(x.Tape,        ConfigWishes.SetCourtesyBytes (x.Tape       , val.courtesyBytes)); });
            AssertProp(x => { x.TapeConfig .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeConfig,  ConfigWishes.SetCourtesyBytes (x.TapeConfig , val.courtesyBytes)); });
            AssertProp(x => { x.TapeActions.Bits(val.bits).Channels(val.channels); AreEqual(x.TapeActions, ConfigWishes.SetCourtesyBytes (x.TapeActions, val.courtesyBytes)); });
            AssertProp(x => { x.TapeAction .Bits(val.bits).Channels(val.channels); AreEqual(x.TapeAction,  ConfigWishes.SetCourtesyBytes (x.TapeAction , val.courtesyBytes)); });
            AssertProp(x => AreEqual(x.Tape,        () => x.Tape       .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
        }
        
        [TestMethod]
        public void ConfigSection_CourtesyBytes()
        {
            // Get-only
            var configSection = CreateTestEntities().SynthBound.ConfigSection;
            int? expected = configSection.CourtesyFrames * configSection.Bits / 8 * configSection.Channels;
            AreEqual(expected, () => configSection.CourtesyBytes   ());
            AreEqual(expected, () => configSection.GetCourtesyBytes());
            AreEqual(expected, () => CourtesyBytes   (configSection));
            AreEqual(expected, () => GetCourtesyBytes(configSection));
            AreEqual(expected, () => ConfigWishesAccessor.CourtesyBytes   (configSection));
            AreEqual(expected, () => ConfigWishesAccessor.GetCourtesyBytes(configSection));
        }
        
        [TestMethod]
        public void Default_CourtesyBytes()
        {
            int expected = DefaultCourtesyFrames * DefaultBits / 8 * DefaultChannels;
            AreEqual(expected, () => DefaultCourtesyBytes);
        }

        [TestMethod]
        public void CourtesyBytes_EdgeCases()
        {
            // For code coverage
            ThrowsException(() => CourtesyFrames(courtesyBytes: 8, frameSize: 3));
        }
        
        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, int courtesyBytes)
        {
            Assert_Bound_Getters(x, courtesyBytes);
            Assert_Immutable_Getters(x.Immutable.CourtesyFrames, x.Immutable.FrameSize, courtesyBytes);
            Assert_Immutable_Getters(x.Immutable.CourtesyFrames, x.Immutable.Bits, x.Immutable.Channels, courtesyBytes);
        }

        private void Assert_Bound_Getters(TestEntities x, int courtesyBytes)
        {
            Assert_SynthBound_Getters(x, courtesyBytes);
            Assert_TapeBound_Getters(x, courtesyBytes);
        }
        
        private void Assert_SynthBound_Getters(TestEntities x, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => x.SynthBound.SynthWishes   .GetCourtesyBytes);
            AreEqual(courtesyBytes, () => x.SynthBound.FlowNode      .GetCourtesyBytes);
            AreEqual(courtesyBytes, () => x.SynthBound.SynthWishes   .GetCourtesyBytes());
            AreEqual(courtesyBytes, () => x.SynthBound.FlowNode      .GetCourtesyBytes());
            AreEqual(courtesyBytes, () => x.SynthBound.ConfigResolver.GetCourtesyBytes());
            AreEqual(courtesyBytes, () => x.SynthBound.SynthWishes   .CourtesyBytes   ());
            AreEqual(courtesyBytes, () => x.SynthBound.FlowNode      .CourtesyBytes   ());
            AreEqual(courtesyBytes, () => x.SynthBound.ConfigResolver.CourtesyBytes   ());
            AreEqual(courtesyBytes, () => CourtesyBytes   (x.SynthBound.SynthWishes   ));
            AreEqual(courtesyBytes, () => CourtesyBytes   (x.SynthBound.FlowNode      ));
            AreEqual(courtesyBytes, () => CourtesyBytes   (x.SynthBound.ConfigResolver));
            AreEqual(courtesyBytes, () => GetCourtesyBytes(x.SynthBound.SynthWishes   ));
            AreEqual(courtesyBytes, () => GetCourtesyBytes(x.SynthBound.FlowNode      ));
            AreEqual(courtesyBytes, () => GetCourtesyBytes(x.SynthBound.ConfigResolver));
            AreEqual(courtesyBytes, () => ConfigWishes        .CourtesyBytes   (x.SynthBound.SynthWishes   ));
            AreEqual(courtesyBytes, () => ConfigWishes        .CourtesyBytes   (x.SynthBound.FlowNode      ));
            AreEqual(courtesyBytes, () => ConfigWishesAccessor.CourtesyBytes   (x.SynthBound.ConfigResolver));
            AreEqual(courtesyBytes, () => ConfigWishes        .GetCourtesyBytes(x.SynthBound.SynthWishes   ));
            AreEqual(courtesyBytes, () => ConfigWishes        .GetCourtesyBytes(x.SynthBound.FlowNode      ));
            AreEqual(courtesyBytes, () => ConfigWishesAccessor.GetCourtesyBytes(x.SynthBound.ConfigResolver));
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => x.TapeBound.Tape       .CourtesyBytes   ());
            AreEqual(courtesyBytes, () => x.TapeBound.TapeConfig .CourtesyBytes   ());
            AreEqual(courtesyBytes, () => x.TapeBound.TapeActions.CourtesyBytes   ());
            AreEqual(courtesyBytes, () => x.TapeBound.TapeAction .CourtesyBytes   ());
            AreEqual(courtesyBytes, () => x.TapeBound.Tape       .GetCourtesyBytes());
            AreEqual(courtesyBytes, () => x.TapeBound.TapeConfig .GetCourtesyBytes());
            AreEqual(courtesyBytes, () => x.TapeBound.TapeActions.GetCourtesyBytes());
            AreEqual(courtesyBytes, () => x.TapeBound.TapeAction .GetCourtesyBytes());
            AreEqual(courtesyBytes, () => CourtesyBytes   (x.TapeBound.Tape       ));
            AreEqual(courtesyBytes, () => CourtesyBytes   (x.TapeBound.TapeConfig ));
            AreEqual(courtesyBytes, () => CourtesyBytes   (x.TapeBound.TapeActions));
            AreEqual(courtesyBytes, () => CourtesyBytes   (x.TapeBound.TapeAction ));
            AreEqual(courtesyBytes, () => GetCourtesyBytes(x.TapeBound.Tape       ));
            AreEqual(courtesyBytes, () => GetCourtesyBytes(x.TapeBound.TapeConfig ));
            AreEqual(courtesyBytes, () => GetCourtesyBytes(x.TapeBound.TapeActions));
            AreEqual(courtesyBytes, () => GetCourtesyBytes(x.TapeBound.TapeAction ));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyBytes   (x.TapeBound.Tape       ));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyBytes   (x.TapeBound.TapeConfig ));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyBytes   (x.TapeBound.TapeActions));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyBytes   (x.TapeBound.TapeAction ));
            AreEqual(courtesyBytes, () => ConfigWishes.GetCourtesyBytes(x.TapeBound.Tape       ));
            AreEqual(courtesyBytes, () => ConfigWishes.GetCourtesyBytes(x.TapeBound.TapeConfig ));
            AreEqual(courtesyBytes, () => ConfigWishes.GetCourtesyBytes(x.TapeBound.TapeActions));
            AreEqual(courtesyBytes, () => ConfigWishes.GetCourtesyBytes(x.TapeBound.TapeAction ));
        }

        private void Assert_Immutable_Getters(int courtesyFrames, int bits, int channels, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => courtesyFrames.CourtesyBytes        (bits, channels));
            AreEqual(courtesyBytes, () => courtesyFrames.GetCourtesyBytes     (bits, channels));
            AreEqual(courtesyBytes, () => courtesyFrames.ToCourtesyBytes      (bits, channels));
            AreEqual(courtesyBytes, () => courtesyFrames.CourtesyFramesToBytes(bits, channels));
            AreEqual(courtesyBytes, () => CourtesyBytes        (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => GetCourtesyBytes     (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ToCourtesyBytes      (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => CourtesyFramesToBytes(courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyBytes        (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ConfigWishes.GetCourtesyBytes     (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ConfigWishes.ToCourtesyBytes      (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyFramesToBytes(courtesyFrames, bits, channels));
            
            Assert_Immutable_Getters((int?)courtesyFrames, (int?)bits, (int?)channels, courtesyBytes);
        }

        private void Assert_Immutable_Getters(int? courtesyFrames, int? bits, int? channels, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => courtesyFrames.CourtesyBytes        (bits, channels));
            AreEqual(courtesyBytes, () => courtesyFrames.GetCourtesyBytes     (bits, channels));
            AreEqual(courtesyBytes, () => courtesyFrames.ToCourtesyBytes      (bits, channels));
            AreEqual(courtesyBytes, () => courtesyFrames.CourtesyFramesToBytes(bits, channels));
            AreEqual(courtesyBytes, () => CourtesyBytes        (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => GetCourtesyBytes     (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ToCourtesyBytes      (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => CourtesyFramesToBytes(courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyBytes        (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ConfigWishes.GetCourtesyBytes     (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ConfigWishes.ToCourtesyBytes      (courtesyFrames, bits, channels));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyFramesToBytes(courtesyFrames, bits, channels));
        }
        
        private void Assert_Immutable_Getters(int courtesyFrames, int frameSize, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => courtesyFrames.CourtesyBytes        (frameSize));
            AreEqual(courtesyBytes, () => courtesyFrames.GetCourtesyBytes     (frameSize));
            AreEqual(courtesyBytes, () => courtesyFrames.ToCourtesyBytes      (frameSize));
            AreEqual(courtesyBytes, () => courtesyFrames.CourtesyFramesToBytes(frameSize));
            AreEqual(courtesyBytes, () => CourtesyBytes        (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => GetCourtesyBytes     (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ToCourtesyBytes      (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => CourtesyFramesToBytes(courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyBytes        (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ConfigWishes.GetCourtesyBytes     (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ConfigWishes.ToCourtesyBytes      (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyFramesToBytes(courtesyFrames, frameSize));
            AreEqual(courtesyFrames, () => courtesyBytes.CourtesyFrames       (frameSize));
            AreEqual(courtesyFrames, () => courtesyBytes.GetCourtesyFrames    (frameSize));
            AreEqual(courtesyFrames, () => courtesyBytes.ToCourtesyFrames     (frameSize));
            AreEqual(courtesyFrames, () => courtesyBytes.CourtesyBytesToFrames(frameSize));
            AreEqual(courtesyFrames, () => CourtesyFrames       (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, () => GetCourtesyFrames    (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, () => ToCourtesyFrames     (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, () => CourtesyBytesToFrames(courtesyBytes, frameSize));
            AreEqual(courtesyFrames, () => ConfigWishes.CourtesyFrames       (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, () => ConfigWishes.GetCourtesyFrames    (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, () => ConfigWishes.ToCourtesyFrames     (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, () => ConfigWishes.CourtesyBytesToFrames(courtesyBytes, frameSize));
            
            // TODO: These touch the nully overloads, but don't actually test nully values.
            Assert_Immutable_Getters((int?)courtesyFrames,       frameSize,       courtesyBytes);
            Assert_Immutable_Getters((int?)courtesyFrames,       frameSize, (int?)courtesyBytes);
            Assert_Immutable_Getters((int?)courtesyFrames, (int?)frameSize,       courtesyBytes);
            Assert_Immutable_Getters((int?)courtesyFrames, (int?)frameSize, (int?)courtesyBytes);
        }
        
        private void Assert_Immutable_Getters(int? courtesyFrames, int frameSize, int?courtesyBytes)
        {
            AreEqual(courtesyFrames, courtesyBytes.CourtesyFrames       (frameSize));
            AreEqual(courtesyFrames, courtesyBytes.GetCourtesyFrames    (frameSize));
            AreEqual(courtesyFrames, courtesyBytes.ToCourtesyFrames     (frameSize));
            AreEqual(courtesyFrames, courtesyBytes.CourtesyBytesToFrames(frameSize));
            AreEqual(courtesyFrames, CourtesyFrames       (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, GetCourtesyFrames    (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, ToCourtesyFrames     (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, CourtesyBytesToFrames(courtesyBytes, frameSize));
            AreEqual(courtesyFrames, ConfigWishes.CourtesyFrames       (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, ConfigWishes.GetCourtesyFrames    (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, ConfigWishes.ToCourtesyFrames     (courtesyBytes, frameSize));
            AreEqual(courtesyFrames, ConfigWishes.CourtesyBytesToFrames(courtesyBytes, frameSize));
        }
        
        private void Assert_Immutable_Getters(int? courtesyFrames, int frameSize, int courtesyBytes)
        {
            AreEqual(courtesyBytes, courtesyFrames.CourtesyBytes        (frameSize));
            AreEqual(courtesyBytes, courtesyFrames.GetCourtesyBytes     (frameSize));
            AreEqual(courtesyBytes, courtesyFrames.ToCourtesyBytes      (frameSize));
            AreEqual(courtesyBytes, courtesyFrames.CourtesyFramesToBytes(frameSize));
            AreEqual(courtesyBytes, CourtesyBytes        (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, GetCourtesyBytes     (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ToCourtesyBytes      (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, CourtesyFramesToBytes(courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ConfigWishes.CourtesyBytes        (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ConfigWishes.GetCourtesyBytes     (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ConfigWishes.ToCourtesyBytes      (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ConfigWishes.CourtesyFramesToBytes(courtesyFrames, frameSize));
        }
        
        private void Assert_Immutable_Getters(int? courtesyFrames, int? frameSize, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => courtesyFrames.CourtesyBytes        (frameSize));
            AreEqual(courtesyBytes, () => courtesyFrames.GetCourtesyBytes     (frameSize));
            AreEqual(courtesyBytes, () => courtesyFrames.ToCourtesyBytes      (frameSize));
            AreEqual(courtesyBytes, () => courtesyFrames.CourtesyFramesToBytes(frameSize));
            AreEqual(courtesyBytes, () => CourtesyBytes        (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => GetCourtesyBytes     (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ToCourtesyBytes      (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => CourtesyFramesToBytes(courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyBytes        (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ConfigWishes.GetCourtesyBytes     (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ConfigWishes.ToCourtesyBytes      (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, () => ConfigWishes.CourtesyFramesToBytes(courtesyFrames, frameSize));
        }

        private void Assert_Immutable_Getters(int? courtesyFrames, int? frameSize, int? courtesyBytes)
        {
            AreEqual(courtesyBytes, courtesyFrames.CourtesyBytes        (frameSize));
            AreEqual(courtesyBytes, courtesyFrames.GetCourtesyBytes     (frameSize));
            AreEqual(courtesyBytes, courtesyFrames.ToCourtesyBytes      (frameSize));
            AreEqual(courtesyBytes, courtesyFrames.CourtesyFramesToBytes(frameSize));
            AreEqual(courtesyBytes, CourtesyBytes        (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, GetCourtesyBytes     (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ToCourtesyBytes      (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, CourtesyFramesToBytes(courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ConfigWishes.CourtesyBytes        (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ConfigWishes.GetCourtesyBytes     (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ConfigWishes.ToCourtesyBytes      (courtesyFrames, frameSize));
            AreEqual(courtesyBytes, ConfigWishes.CourtesyFramesToBytes(courtesyFrames, frameSize));
        }


        // Test Data Helpers

        private static TestEntities CreateTestEntities((int courtesyBytes, int? courtesyFrames, int? bits, int? channels) init = default, [CallerMemberName] string name = null) 
            => new TestEntities(x => x.NoLog().CourtesyFrames(init.courtesyFrames).Bits(init.bits).Channels(init.channels).SamplingRate(HighPerfHz), name);

        // ncrunch: no coverage start
        
        static IEnumerable<object[]> ParameterSetInitWithEmpties
        {
            get
            {
                foreach (int? frames   in   _framesValuesWithEmpty)
                foreach (int? bits     in     _bitsValuesWithEmpty)
                foreach (int? channels in _channelsValuesWithEmpty)
                {
                    yield return GetParameters(frames, bits, channels);
                }
            }
        }

        static IEnumerable<object[]> ParameterSetLarge
        {
            get
            {
                foreach ( int? frames1 in _framesValues)
                foreach ((int? bits1, int? channels1) in _bitsChannelsCombos)
                foreach ( int? frames2 in _framesValues)
                foreach ((int? bits2, int? channels2) in _bitsChannelsCombos)
                {
                    yield return GetParameters(frames1, bits1, channels1, frames2, bits2, channels2);
                }
            }
        }
        
        static IEnumerable<object> ParameterSetSmall => new[]
        {
            GetParameters(_3Frames, 32, StereoChannels, _2Frames, 32, StereoChannels), // Change frames
            GetParameters(_3Frames, 32, StereoChannels, _3Frames, 16, StereoChannels), // Change bits
            GetParameters(_3Frames, 32, StereoChannels, _3Frames, 32,   MonoChannels), // Change channels
            GetParameters(_2Frames, 16,   MonoChannels, _3Frames, 32, StereoChannels), // Change all
            GetParameters(_3Frames, 32, StereoChannels, _3Frames,  8, StereoChannels), // 8-bit
        };
        
        static IEnumerable<object> ParameterSetSmallWithEmpties => new[]
        {
            GetParameters(_3Frames , 32 , StereoChannels , _2Frames ,   32 , StereoChannels ), // Change frames
            GetParameters(_3Frames , 32 , StereoChannels , _3Frames ,   16 , StereoChannels ), // Change bits
            GetParameters(_3Frames , 32 , StereoChannels , _3Frames ,   32 ,   MonoChannels ), // Change channels
            GetParameters(_2Frames , 16 ,   MonoChannels , _3Frames ,   32 , StereoChannels ), // Change all
            GetParameters(_3Frames , 32 , StereoChannels , _3Frames ,    8 , StereoChannels ), // 8-bit
            GetParameters(_3Frames , 32 , StereoChannels ,     null ,   32 , StereoChannels ), // Null frames
            GetParameters(_3Frames , 32 , StereoChannels , _3Frames , null , StereoChannels ), // Null bits
            GetParameters(_3Frames , 32 , StereoChannels , _3Frames ,   32 ,           null ), // Null channels
            GetParameters(_2Frames , 16 ,   MonoChannels ,     null , null ,           null ), // Null all
            GetParameters(_3Frames , 32 , StereoChannels , _3Frames ,    0 , StereoChannels ), // 0 bits
            GetParameters(_3Frames , 32 , StereoChannels , _3Frames ,   32 ,              0 ), // 0 channels
            GetParameters(_2Frames , 16 ,   MonoChannels ,     null ,    0 ,              0 ), // 0 bits and channels
        };
           
        static object[] GetParameters(
            int? frames1, int? bits1, int? channels1, 
            int? frames2, int? bits2, int? channels2)
            => new object[]
            {
                Descriptor(frames1, bits1, channels1, frames2, bits2, channels2),
                GetExpectedCourtesyBytes(frames1, bits1, channels1), 
                frames1, bits1, channels1,
                GetExpectedCourtesyBytes(frames2, bits2, channels2), 
                frames2, bits2, channels2
            };
        
        static object[] GetParameters(int? frames, int? bits, int? channels)
        {
            return new object[]
            {
                Descriptor(frames, bits, channels),
                GetExpectedCourtesyBytes(frames, bits, channels),
                frames, bits, channels
            };
        }
        
        static int GetExpectedCourtesyBytes(int? courtesyFrames, int? bits, int? channels)
        {
            // Immutable. Get-only.
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;

            int? courtesyFramesSetting = configSection.CourtesyFrames;
            
            int coalescedCourtesyFrames = courtesyFrames ?? courtesyFramesSetting ?? DefaultCourtesyFrames;
            int coalescedBits           = Has(bits)      ?  bits.Value            :  DefaultBits;
            int coalescedChannels       = Has(channels)  ?  channels.Value        :  DefaultChannels;
            
            return coalescedCourtesyFrames * coalescedBits / 8 * coalescedChannels;
        }
        
        static string Descriptor(int? frames, int? bits, int? channels)
        {
            string formattedFrames   = frames   == null ? "(null)" : frames.ToString();
            string formattedBits     = bits     == null ? "(null)" : bits + "bit";
            string formattedChannels = channels == null ? "(null)" : channels == 0 ? "0" : channels.ChannelDescriptor().ToLower();
            return $"{formattedFrames}x{formattedBits}x{formattedChannels} ";
        }
        
        static string Descriptor(
            int? frames1, int? bits1, int? channels1,
            int? frames2, int? bits2, int? channels2)
            => $"{Descriptor(frames1, bits1, channels1)} => {Descriptor(frames2, bits2, channels2)}";

        const int _2Frames = 2;
        const int _3Frames = 3;
        const int _4Frames = 4;
        
        static readonly int?[]         _channelsValues          = { 1, 2 };
        static readonly int?[]         _channelsValuesWithEmpty = { 1, 2, 0, null };
        static readonly int?[]         _bitsValues              = { 8, 16, 32 };
        static readonly int?[]         _bitsValuesWithEmpty     = { 8, 16, 32, 0, null };
        static readonly int?[]         _framesValues            = { 3, 4, 8 };
        static readonly int?[]         _framesValuesWithEmpty   = { 3, 4, 8, null };
        static readonly int?[]         _bytesValues             = { 8, 12, 16, 20, 24, 28, 32 };
        static readonly (int?, int?)[] _bitsChannelsCombos      = { (8, 1), (16, 2), (32, 1), (32, 2) };

        // ncrunch: no coverage end
    } 
}