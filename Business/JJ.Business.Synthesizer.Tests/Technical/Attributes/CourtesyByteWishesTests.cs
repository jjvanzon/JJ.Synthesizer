using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
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
        [DynamicData(nameof(TestParametersInit2))]
        public void Init_CourtesyBytes2(string descriptor, int courtesyBytes, int courtesyFrames, int bits, int channels)
        { 
            var init = (courtesyBytes, courtesyFrames, bits, channels);
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init.courtesyBytes);
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_CourtesyBytes(int init, int val)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, val);
                Assert_TapeBound_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, val);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes.CourtesyBytes(val)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode.CourtesyBytes(val)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.CourtesyBytes(val)));
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters2))]
        public void SynthBound_CourtesyBytes2(
            string descriptor,
            int initCourtesyBytes, int initCourtesyFrames, int initBits, int initChannels,
            int courtesyBytes, int courtesyFrames, int bits, int channels)
        {
            var init = (courtesyBytes: initCourtesyBytes, courtesyFrames: initCourtesyFrames, 
                        bits: initBits, channels: initChannels);
            var val  = (courtesyBytes, courtesyFrames, bits, channels);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.courtesyBytes);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, val.courtesyBytes);
                Assert_TapeBound_Getters(x, init.courtesyBytes);
                
                x.Record();
                Assert_All_Getters(x, val.courtesyBytes);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes .Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode    .Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode    .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
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

        [TestMethod]
        [DynamicData(nameof(TestParameters2))]
        public void TapeBound_CourtesyBytes2(
            string descriptor,
            int initCourtesyBytes, int initCourtesyFrames, int initBits, int initChannels,
            int courtesyBytes, int courtesyFrames, int bits, int channels)
        {
            var init = (courtesyBytes: initCourtesyBytes, courtesyFrames: initCourtesyFrames, 
                bits: initBits, channels: initChannels);
            var val  = (courtesyBytes, courtesyFrames, bits, channels);
            

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.courtesyBytes);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.courtesyBytes);
                Assert_TapeBound_Getters(x, val.courtesyBytes);
                
                x.Record();
                
                Assert_All_Getters(x, init.courtesyBytes); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
        }

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

        // Helpers         
        
        private TestEntities CreateTestEntities(int courtesyBytes) 
            => new TestEntities(x => x.CourtesyBytes(courtesyBytes));

        private TestEntities CreateTestEntities((int courtesyBytes, int courtesyFrames, int bits, int channels) init) 
            => new TestEntities(x => x.CourtesyFrames(init.courtesyFrames).Bits(init.bits).Channels(init.channels));

        // ncrunch: no coverage start
        
        static readonly int[] _courtesyFramesValues = { 2, 3, 4, 5, 8 };
        static readonly int[] _bitsValues           = { 8, 16, 32 };
        static readonly int[] _channelsValues       = { 1, 2 };
        static readonly int[] _courtesyBytesValues  = { 8, 12, 16, 20, 24, 28, 32 };
        
        static IEnumerable<object[]> TestParametersInit
        {
            get 
            {
                foreach (int value in _courtesyBytesValues)
                {
                    yield return new object[] { value };
                }
            }
        }

        static IEnumerable<object[]> TestParametersInit2
        {
            get
            {
                foreach (int courtesyFrames in _courtesyFramesValues)
                foreach (int bits in _bitsValues)
                foreach (int channels in _channelsValues)
                {
                    int courtesyBytes = courtesyFrames * bits / 8 * channels;
                    yield return new object[]
                    { 
                        GetDescriptor(courtesyFrames, bits, channels),
                        courtesyBytes, 
                        courtesyFrames, 
                        bits,
                        channels
                    };
                }
            }
        }
        
        static IEnumerable<object[]> TestParameters
        {
            get
            {
                foreach (int init in _courtesyBytesValues)
                foreach (int value in _courtesyBytesValues)
                {
                    yield return new object[] { init, value };
                }
            }
        }

        static IEnumerable<object[]> TestParameters2
        {
            get
            {
                foreach (int initCourtesyFrames in _courtesyFramesValues)
                foreach (int initBits in _bitsValues)
                foreach (int initChannels in _channelsValues)
                foreach (int courtesyFrames in _courtesyFramesValues)
                foreach (int bits in _bitsValues)
                foreach (int channels in _channelsValues)
                {
                    if (initCourtesyFrames == courtesyFrames && initBits == bits && initChannels == channels) 
                    {
                        continue;
                    }
                                        
                    int initCourtesyBytes = initCourtesyFrames * initBits / 8 * initChannels;
                    int courtesyBytes = courtesyFrames * bits / 8 * channels;
                    
                    yield return new object[]
                    {
                        GetDescriptor(initCourtesyFrames, initBits, initChannels, courtesyFrames, bits, channels),
                        initCourtesyBytes, 
                        initCourtesyFrames, 
                        initBits,
                        initChannels,
                        courtesyBytes, 
                        courtesyFrames, 
                        bits,
                        channels
                    };
                }
            }
        }
        
        static string GetDescriptor(int courtesyFrames, int bits, int channels) 
            => $"{courtesyFrames}x{bits}bit-{ChannelDescriptor(channels).ToLower()} ";

        static string GetDescriptor(
            int initCourtesyFrames, int initBits, int initChannels,
            int courtesyFrames, int bits, int channels)
            => $"{GetDescriptor(initCourtesyFrames, initBits, initChannels)} => {GetDescriptor(courtesyFrames, bits, channels)}";

        // ncrunch: no coverage end
   } 
}