using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Tests.Technical.Configuration.TestEntities;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;

// ReSharper disable UnusedMember.Local

#pragma warning disable CS0618
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.Technical.Configuration
{
    [TestClass]
    [TestCategory("Technical")]
    public class CourtesyByteWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(ParameterSetInit))]
        public void Init_CourtesyBytes(string descriptor, int courtesyBytes, int courtesyFrames, int bits, int channels)
        { 
            var init = (courtesyBytes, courtesyFrames, bits, channels);
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init.courtesyBytes);
        }

        [TestMethod]
        [DynamicData(nameof(ParameterSetSmall))]
        public void SynthBound_CourtesyBytes(
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Bits(val.bits).Channels(val.channels).CourtesyBytes(val.courtesyBytes)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Bits(val.bits).Channels(val.channels).CourtesyFrames(val.courtesyFrames)));
        }

        [TestMethod]
        [DynamicData(nameof(ParameterSetSmall))]
        public void TapeBound_CourtesyBytes(
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
        public void GlobalBound_CourtesyBytes()
        {
            // Immutable. Get-only.
            var configSection = GetConfigSectionAccessor();
            int circumstantialCourtesyFrames = 2;
            int? circumstantialCourtesyBytes = CourtesyBytes(circumstantialCourtesyFrames, configSection.Bits, configSection.Channels);
            AreEqual(circumstantialCourtesyBytes, () => configSection.CourtesyBytes());
            AreEqual(4 * 32 / 8, () => DefaultCourtesyBytes);
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
            AreEqual(courtesyBytes, () => x.SynthBound.ConfigResolver.CourtesyBytes());
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

        private TestEntities CreateTestEntities((int courtesyBytes, int courtesyFrames, int bits, int channels) init) 
            => new TestEntities(x => x.CourtesyFrames(init.courtesyFrames).Bits(init.bits).Channels(init.channels));

        // ncrunch: no coverage start
        
        static IEnumerable<object[]> ParameterSetInit
        {
            get
            {
                foreach (int frames in _framesValues)
                foreach (int bits in _bitsValues)
                foreach (int channels in _channelsValues)
                {
                    yield return GetParameters(frames, bits, channels);
                }
            }
        }

        static IEnumerable<object[]> ParameterSetLarge
        {
            get
            {
                foreach (int initFrames in _framesValues)
                foreach ((int initBits, int initChannels) in _bitsChannelsCombos)
                foreach (int frames in _framesValues)
                foreach ((int bits, int channels) in _bitsChannelsCombos)
                {
                    yield return GetParameters(initFrames, initBits, initChannels, frames, bits, channels);
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
                        
        static object[] GetParameters(
            int frames1, int bits1, int channels1, 
            int frames2, int bits2, int channels2)
            => new object[]
            {
                Descriptor(frames1, bits1, channels1, frames2, bits2, channels2),
                frames1 * bits1 / 8 * channels1, // = Courtesy Bytes
                frames1, bits1, channels1,
                frames2 * bits2 / 8 * channels2, // = Courtesy Bytes
                frames2, bits2, channels2
            };
        
        static object[] GetParameters(int frames, int bits, int channels) => new object[]
        {
            Descriptor(frames, bits, channels),
            frames * bits / 8 * channels, // = Courtesy Bytes
            frames, bits, channels
        };

        static string Descriptor(int courtesyFrames, int bits, int channels) 
            => $"{courtesyFrames}x{bits}bit-{ChannelDescriptor(channels).ToLower()} ";

        static string Descriptor(
            int initCourtesyFrames, int initBits, int initChannels,
            int courtesyFrames, int bits, int channels)
            => $"{Descriptor(initCourtesyFrames, initBits, initChannels)} => {Descriptor(courtesyFrames, bits, channels)}";

        const int _2Frames = 2;
        const int _3Frames = 3;
        const int _4Frames = 4;
        
        static readonly int[]        _channelsValues     = { 1, 2 };
        static readonly int[]        _bitsValues         = { 8, 16, 32 };
        static readonly int[]        _framesValues       = { 3, 4, 8 };
        static readonly int[]        _bytesValues        = { 8, 12, 16, 20, 24, 28, 32 };
        static readonly (int, int)[] _bitsChannelsCombos = { (8, 1), (16, 2), (32, 1), (32, 2) };

        // ncrunch: no coverage end
   } 
}