using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Tests.Technical.Attributes.TestEntities;
using static JJ.Business.Synthesizer.Wishes.AttributeWishes.AttributeExtensionWishes;

#pragma warning disable CS0618
#pragma warning disable MSTEST0018

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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes.CourtesyBytes(val)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode.CourtesyBytes(val)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.CourtesyBytes(val)));
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes .Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode    .Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode    .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
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
        
        [TestMethod]
        public void ConfigSection_CourtesyBytes()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = GetConfigSectionAccessor();
            int circumstantialCourtesyFrames = 2;
            int? courtesyBytes = circumstantialCourtesyFrames * configSection.Channels * configSection.SizeOfBitDepth();
            AreEqual(courtesyBytes, () => configSection.CourtesyBytes());
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
                
        private void Assert_Immutable_Getters(int courtesyFrames, int frameSize, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => CourtesyBytes(courtesyFrames, frameSize));
        }

        // Test Data Helpers
        
        private TestEntities CreateTestEntities(int courtesyBytes) 
            => new TestEntities(x => x.CourtesyBytes(courtesyBytes));

        private TestEntities CreateTestEntities((int courtesyBytes, int courtesyFrames, int bits, int channels) init) 
            => new TestEntities(x => x.CourtesyFrames(init.courtesyFrames).Bits(init.bits).Channels(init.channels));

        // ncrunch: no coverage start
        
        static IEnumerable<object[]> TestParametersInit => _courtesyBytesValues.Select(value => new object[] { value });
        
        static IEnumerable<object[]> TestParametersInit2
        {
            get
            {
                foreach (int courtesyFrames in _courtesyFramesValues)
                foreach (int bits in _bitsValues)
                foreach (int channels in _channelsValues)
                {
                    yield return new object[]
                    { 
                        GetDescriptor(courtesyFrames, bits, channels),
                        courtesyFrames * bits / 8 * channels, 
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
                foreach ((int initBits, int initChannels )in _bitsChannelsCombos)
                foreach (int courtesyFrames in _courtesyFramesValues)
                foreach ((int bits, int channels )in _bitsChannelsCombos)
                {
                    if (initCourtesyFrames == courtesyFrames && initBits == bits && initChannels == channels) 
                    {
                        continue;
                    }
                    
                    yield return new object[]
                    {
                        GetDescriptor(initCourtesyFrames, initBits, initChannels, courtesyFrames, bits, channels),
                        initCourtesyFrames * initBits / 8 * initChannels, 
                        initCourtesyFrames, 
                        initBits,
                        initChannels,
                        courtesyFrames * bits / 8 * channels, 
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
        
        // TODO: Choose more literal combinations in next time,
        // avoiding cartesian products where the number of combinations grows unwieldy.
        static readonly int[]        _channelsValues       = { 1, 2 };
        static readonly int[]        _bitsValues           = { 8, 16, 32 };
        static readonly int[]        _courtesyFramesValues = { 3, 4, 8 };
        static readonly int[]        _courtesyBytesValues  = { 8, 12, 16, 20, 24, 28, 32 };
        static readonly (int, int)[] _bitsChannelsCombos   = { (8, 1), (16, 2), (32, 1), (32, 2) };

        // ncrunch: no coverage end
   } 
}