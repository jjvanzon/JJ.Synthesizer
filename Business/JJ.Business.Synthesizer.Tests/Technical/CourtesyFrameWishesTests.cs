using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.Accessors;
using static JJ.Business.Synthesizer.Tests.Technical.TestEntities;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable MSTEST0018 // DynamicData members should be IEnumerable<object[]>

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class CourtesyFrameWishesTests
    {
        [DataTestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(100)]
        public void Init_CourtesyFrames(int init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init);
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_CourtesyFrames(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, value);
                Assert_TapeBound_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, value);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes.CourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes.WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode.CourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode.WithCourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.CourtesyFrames(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.WithCourtesyFrames(value)));
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
            // Global-Bound. Immutable. Get-only.
            var configSection = GetConfigSectionAccessor();
            int circumstantialConfigValue = 2;
            AreEqual(circumstantialConfigValue, () => configSection.CourtesyFrames);
            AreEqual(circumstantialConfigValue, () => configSection.CourtesyFrames());
        }
        
        [TestMethod]
        public void Default_CourtesyFrames()
        {
            AreEqual(4, () => DefaultCourtesyFrames);
        }

        // Helpers

        private TestEntities CreateTestEntities(int courtesyFrames) => new TestEntities(x => x.CourtesyFrames(courtesyFrames));
        
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
            AreEqual(courtesyFrames, () => x.SynthBound.ConfigWishes.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.SynthBound.ConfigWishes.GetCourtesyFrames);
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int courtesyFrames)
        {
            AreEqual(courtesyFrames, () => x.TapeBound.Tape.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeConfig.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeConfig.CourtesyFrames);
            AreEqual(courtesyFrames, () => x.TapeBound.TapeActions.CourtesyFrames());
            AreEqual(courtesyFrames, () => x.TapeBound.TapeAction.CourtesyFrames());
        }
    } 
}