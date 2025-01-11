using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable MSTEST0018 // DynamicData members should be IEnumerable<object[]>

namespace JJ.Business.Synthesizer.Tests.Technical.Attributes
{
    [TestClass]
    [TestCategory("Technical")]
    public class CourtesyByteWishesTests
    {
        // TODO: Preliminary. Vary the dependencies to cover more cases.

        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_CourtesyBytes(int init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init);
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_CourtesyBytes(int init, int value)
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes.CourtesyBytes(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode.CourtesyBytes(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.CourtesyBytes(value)));
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_CourtesyBytes(int init, int value)
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

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.CourtesyBytes(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.CourtesyBytes(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.CourtesyBytes(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.CourtesyBytes(value)));
        }

        // Helpers

        private TestEntities CreateTestEntities(int courtesyBytes) => new TestEntities(x => x.CourtesyBytes(courtesyBytes));

        private void Assert_All_Getters(TestEntities x, int courtesyBytes)
        {
            Assert_Bound_Getters(x, courtesyBytes);
        }

        private void Assert_Bound_Getters(TestEntities x, int courtesyBytes)
        {
            Assert_SynthBound_Getters(x, courtesyBytes);
            Assert_TapeBound_Getters(x, courtesyBytes);
        }
        
        private void Assert_SynthBound_Getters(TestEntities x, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => x.SynthBound.SynthWishes.CourtesyBytes());
            AreEqual(courtesyBytes, () => x.SynthBound.FlowNode.CourtesyBytes());
            AreEqual(courtesyBytes, () => x.SynthBound.ConfigWishes.CourtesyBytes());
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => x.TapeBound.Tape.CourtesyBytes());
            AreEqual(courtesyBytes, () => x.TapeBound.TapeConfig.CourtesyBytes());
            AreEqual(courtesyBytes, () => x.TapeBound.TapeActions.CourtesyBytes());
            AreEqual(courtesyBytes, () => x.TapeBound.TapeAction.CourtesyBytes());
        }
         
        // ncrunch: no coverage start
        
        static int[] TestValues => new[] { 8, 12, 16, 20, 24, 28, 32 };

        static IEnumerable<object[]> TestParameters
        {
            get
            {
                foreach (int init in TestValues)
                foreach (int value in TestValues)
                {
                    yield return new object[] { init, value };
                }
            }
        }
        
        static IEnumerable<object[]> TestParametersInit
        {
            get 
            {
                foreach (int value in TestValues)
                {
                    yield return new object[] { value };
                }
            }
        }

        // ncrunch: no coverage end
   } 
}