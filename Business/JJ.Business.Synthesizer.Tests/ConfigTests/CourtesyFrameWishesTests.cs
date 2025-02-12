using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Tests.ConfigTests.ConfigTestEntities;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
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
    public class CourtesyFrameWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_CourtesyFrames(int? init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, CoalesceDefault(init));
        }

        [TestMethod]
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_CourtesyFrames(int? init, int? value)
        {
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceDefault(init));
                
                setter(x);
                
                Assert_SynthBound_Getters(x, CoalesceDefault(value));
                Assert_TapeBound_Getters(x, CoalesceDefault(init));
                
                x.Record();
                Assert_All_Getters(x, CoalesceDefault(value));
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .CourtesyFrames    (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .CourtesyFrames    (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.CourtesyFrames    (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetCourtesyFrames (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetCourtesyFrames (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetCourtesyFrames (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    CourtesyFrames    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       CourtesyFrames    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, CourtesyFrames    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithCourtesyFrames(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithCourtesyFrames(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithCourtesyFrames(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetCourtesyFrames (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetCourtesyFrames (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetCourtesyFrames (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .CourtesyFrames    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .CourtesyFrames    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.CourtesyFrames    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithCourtesyFrames(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithCourtesyFrames(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithCourtesyFrames(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetCourtesyFrames (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetCourtesyFrames (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetCourtesyFrames (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    CourtesyFrameExtensionWishes        .WithCourtesyFrames(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       CourtesyFrameExtensionWishes        .WithCourtesyFrames(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, CourtesyFrameExtensionWishesAccessor.WithCourtesyFrames(x.SynthBound.ConfigResolver, value)));
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_CourtesyFrames(int init, int value)
        {
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, value);
                
                x.Record();
                
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x =>                                         x.TapeBound.TapeConfig .CourtesyFrames   = value);
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .CourtesyFrames    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .CourtesyFrames    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.CourtesyFrames    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .CourtesyFrames    (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetCourtesyFrames (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetCourtesyFrames (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetCourtesyFrames (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetCourtesyFrames (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => CourtesyFrames    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => CourtesyFrames    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => CourtesyFrames    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => CourtesyFrames    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithCourtesyFrames(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithCourtesyFrames(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithCourtesyFrames(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithCourtesyFrames(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetCourtesyFrames (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetCourtesyFrames (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetCourtesyFrames (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetCourtesyFrames (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.CourtesyFrames    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.CourtesyFrames    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.CourtesyFrames    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.CourtesyFrames    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithCourtesyFrames(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithCourtesyFrames(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithCourtesyFrames(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithCourtesyFrames(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetCourtesyFrames (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetCourtesyFrames (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetCourtesyFrames (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetCourtesyFrames (x.TapeBound.TapeAction , value)));
        }

        [TestMethod]
        public void ConfigSection_CourtesyFrames()
        {
            // Get-only
            var configSection = CreateTestEntities().SynthBound.ConfigSection;
            AreEqual(DefaultCourtesyFrames, () => configSection.CourtesyFrames);
            AreEqual(DefaultCourtesyFrames, () => configSection.CourtesyFrames());
            AreEqual(DefaultCourtesyFrames, () => configSection.GetCourtesyFrames());
        }
        
        [TestMethod]
        public void Default_CourtesyFrames()
        {
            AreEqual(4, () => DefaultCourtesyFrames);
        }
        
        // Getter Helpers
        
        private void Assert_All_Getters(ConfigTestEntities x, int courtesyFrames)
        {
            Assert_Bound_Getters(x, courtesyFrames);
        }

        private void Assert_Bound_Getters(ConfigTestEntities x, int courtesyFrames)
        {
            Assert_SynthBound_Getters(x, courtesyFrames);
            Assert_TapeBound_Getters(x, courtesyFrames);
        }
        
        private void Assert_SynthBound_Getters(ConfigTestEntities x, int courtesyFrames)
        {
            AreEqual(courtesyFrames, () => x.SynthBound.SynthWishes   .GetCourtesyFrames);
            AreEqual(courtesyFrames, () => x.SynthBound.FlowNode      .GetCourtesyFrames);
            AreEqual(courtesyFrames, () => x.SynthBound.ConfigResolver.GetCourtesyFrames);
            AreEqual(courtesyFrames, () => x.SynthBound.SynthWishes   .CourtesyFrames   ());
            AreEqual(courtesyFrames, () => x.SynthBound.FlowNode      .CourtesyFrames   ());
            AreEqual(courtesyFrames, () => x.SynthBound.ConfigResolver.CourtesyFrames   ());
            AreEqual(courtesyFrames, () => x.SynthBound.SynthWishes   .GetCourtesyFrames());
            AreEqual(courtesyFrames, () => x.SynthBound.FlowNode      .GetCourtesyFrames());
            AreEqual(courtesyFrames, () => x.SynthBound.ConfigResolver.GetCourtesyFrames());
            AreEqual(courtesyFrames, () => CourtesyFrames   (x.SynthBound.SynthWishes   ));
            AreEqual(courtesyFrames, () => CourtesyFrames   (x.SynthBound.FlowNode      ));
            AreEqual(courtesyFrames, () => CourtesyFrames   (x.SynthBound.ConfigResolver));
            AreEqual(courtesyFrames, () => GetCourtesyFrames(x.SynthBound.SynthWishes   ));
            AreEqual(courtesyFrames, () => GetCourtesyFrames(x.SynthBound.FlowNode      ));
            AreEqual(courtesyFrames, () => GetCourtesyFrames(x.SynthBound.ConfigResolver));
            AreEqual(courtesyFrames, () => ConfigWishes        .CourtesyFrames   (x.SynthBound.SynthWishes   ));
            AreEqual(courtesyFrames, () => ConfigWishes        .CourtesyFrames   (x.SynthBound.FlowNode      ));
            AreEqual(courtesyFrames, () => ConfigWishesAccessor.CourtesyFrames   (x.SynthBound.ConfigResolver));
            AreEqual(courtesyFrames, () => ConfigWishes        .GetCourtesyFrames(x.SynthBound.SynthWishes   ));
            AreEqual(courtesyFrames, () => ConfigWishes        .GetCourtesyFrames(x.SynthBound.FlowNode      ));
            AreEqual(courtesyFrames, () => ConfigWishesAccessor.GetCourtesyFrames(x.SynthBound.ConfigResolver));
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, int courtesyFrames)
        {
            AreEqual(courtesyFrames, () => x.TapeBound.TapeConfig .CourtesyFrames);
            AreEqual(courtesyFrames, () => x.TapeBound.Tape       .CourtesyFrames   ());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeConfig .CourtesyFrames   ());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeActions.CourtesyFrames   ());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeAction .CourtesyFrames   ());
            AreEqual(courtesyFrames, () => x.TapeBound.Tape       .GetCourtesyFrames());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeConfig .GetCourtesyFrames());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeActions.GetCourtesyFrames());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeAction .GetCourtesyFrames());
            AreEqual(courtesyFrames, () => CourtesyFrames   (x.TapeBound.Tape       ));
            AreEqual(courtesyFrames, () => CourtesyFrames   (x.TapeBound.TapeConfig ));
            AreEqual(courtesyFrames, () => CourtesyFrames   (x.TapeBound.TapeActions));
            AreEqual(courtesyFrames, () => CourtesyFrames   (x.TapeBound.TapeAction ));
            AreEqual(courtesyFrames, () => GetCourtesyFrames(x.TapeBound.Tape       ));
            AreEqual(courtesyFrames, () => GetCourtesyFrames(x.TapeBound.TapeConfig ));
            AreEqual(courtesyFrames, () => GetCourtesyFrames(x.TapeBound.TapeActions));
            AreEqual(courtesyFrames, () => GetCourtesyFrames(x.TapeBound.TapeAction ));
            AreEqual(courtesyFrames, () => ConfigWishes.CourtesyFrames   (x.TapeBound.Tape       ));
            AreEqual(courtesyFrames, () => ConfigWishes.CourtesyFrames   (x.TapeBound.TapeConfig ));
            AreEqual(courtesyFrames, () => ConfigWishes.CourtesyFrames   (x.TapeBound.TapeActions));
            AreEqual(courtesyFrames, () => ConfigWishes.CourtesyFrames   (x.TapeBound.TapeAction ));
            AreEqual(courtesyFrames, () => ConfigWishes.GetCourtesyFrames(x.TapeBound.Tape       ));
            AreEqual(courtesyFrames, () => ConfigWishes.GetCourtesyFrames(x.TapeBound.TapeConfig ));
            AreEqual(courtesyFrames, () => ConfigWishes.GetCourtesyFrames(x.TapeBound.TapeActions));
            AreEqual(courtesyFrames, () => ConfigWishes.GetCourtesyFrames(x.TapeBound.TapeAction ));
        }
 
        // Test Data Helpers

        private ConfigTestEntities CreateTestEntities(int? courtesyFrames = default) => new ConfigTestEntities(x => x.CourtesyFrames(courtesyFrames));
        
        // ncrunch: no coverage start
        
        static object TestParametersInit => new [] 
        {        
            new object[] { null },
            new object[] { 2 },
            new object[] { 3 },
            new object[] { 4 },
            new object[] { 5 },
            new object[] { 100 }
        };

        static object TestParameters => new[]
        {
            new object[] { 2, 3 },
            new object[] { 2, 4 },
            new object[] { 3, 2 },
            new object[] { 3, 4 },
            new object[] { 4, 2 },
            new object[] { 4, 3 },
            new object[] { 4, 5 },
            new object[] { 4, 100 },
        };
        
        static object TestParametersWithEmpty => new[] 
        {
            new object[] { 2, null },
            new object[] { null, 4 },
            new object[] { 2, 3 },
            new object[] { 2, 4 },
            new object[] { 3, 2 },
            new object[] { 3, 4 },
            new object[] { 4, 2 },
            new object[] { 4, 3 },
            new object[] { 4, 5 },
            new object[] { 4, 100 },
        };
        
        // ncrunch: no coverage end
        
        static int CoalesceDefault(int? courtesyFrames) => CoalesceCourtesyFrames(courtesyFrames);
    } 
}