using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.ConfigTests.TestEntities;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0618
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Configuration")]
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
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceDefault(init));
                
                setter(x);
                
                Assert_SynthBound_Getters(x, CoalesceDefault(value));
                Assert_TapeBound_Getters(x, CoalesceDefault(init));
                
                x.Record();
                Assert_All_Getters(x, CoalesceDefault(value));
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes.CourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.CourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.CourtesyFrames(value)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes.WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithCourtesyFrames(value)));
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_CourtesyFrames(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, value);
                
                x.Record();
                
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.CourtesyFrames(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.CourtesyFrames(value)));
            AssertProp(x =>                                         x.TapeBound.TapeConfig.CourtesyFrames = value);
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.CourtesyFrames(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.CourtesyFrames(value)));
        }

        [TestMethod]
        public void ConfigSection_CourtesyFrames()
        {
            // Get-only
            var configSection = CreateTestEntities().SynthBound.ConfigSection;
            AreEqual(DefaultCourtesyFrames, () => configSection.CourtesyFrames);
            AreEqual(DefaultCourtesyFrames, () => configSection.CourtesyFrames());
        }
        
        [TestMethod]
        public void Default_CourtesyFrames()
        {
            AreEqual(4, () => DefaultCourtesyFrames);
        }
        
        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, int courtesyFrames)
        {
            Assert_Bound_Getters(x, courtesyFrames);
        }

        private void Assert_Bound_Getters(TestEntities x, int courtesyFrames)
        {
            Assert_SynthBound_Getters(x, courtesyFrames);
            Assert_TapeBound_Getters(x, courtesyFrames);
        }
        
        private void Assert_SynthBound_Getters(TestEntities x, int courtesyFrames)
        {
            AreEqual(courtesyFrames, () => x.SynthBound.SynthWishes.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.SynthBound.SynthWishes.GetCourtesyFrames);
            AreEqual(courtesyFrames, () => x.SynthBound.FlowNode.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.SynthBound.FlowNode.GetCourtesyFrames);
            AreEqual(courtesyFrames, () => x.SynthBound.ConfigResolver.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.SynthBound.ConfigResolver.GetCourtesyFrames);
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int courtesyFrames)
        {
            AreEqual(courtesyFrames, () => x.TapeBound.Tape.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeConfig.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeConfig.CourtesyFrames);
            AreEqual(courtesyFrames, () => x.TapeBound.TapeActions.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeAction.CourtesyFrames());
        }
 
        // Test Data Helpers

        private TestEntities CreateTestEntities(int? courtesyFrames = default) => new TestEntities(x => x.CourtesyFrames(courtesyFrames));
        
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