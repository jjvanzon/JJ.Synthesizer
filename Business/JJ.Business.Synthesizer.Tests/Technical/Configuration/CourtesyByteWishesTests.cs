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
using static JJ.Framework.Wishes.Common.FilledInWishes;

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
            var init = (courtesyBytes: initCourtesyBytes, courtesyFrames: initCourtesyFrames, bits: initBits, channels: initChannels);
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
            int circumstantialCourtesyBytes = CourtesyBytes(circumstantialCourtesyFrames, configSection.Bits, configSection.Channels);
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
                
        private void Assert_Immutable_Getters(int? courtesyFrames, int? frameSize, int courtesyBytes)
        {
            AreEqual(courtesyBytes, () => CourtesyBytes(courtesyFrames, frameSize));
        }

        // Test Data Helpers

        private TestEntities CreateTestEntities((int courtesyBytes, int? courtesyFrames, int? bits, int? channels) init) 
            => new TestEntities(x => x.CourtesyFrames(init.courtesyFrames).Bits(init.bits).Channels(init.channels));

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
            int? courtesyFramesSetting = GetConfigSectionAccessor().CourtesyFrames;
            
            int coalescedCourtesyFrames = courtesyFrames ?? courtesyFramesSetting ?? DefaultCourtesyFrames;
            int coalescedBits           = Has(bits)      ?  bits.Value            :  DefaultBits;
            int coalescedChannels       = Has(channels)  ?  channels.Value        :  DefaultChannels;
            
            return coalescedCourtesyFrames * coalescedBits / 8 * coalescedChannels;
        }
        
        static string Descriptor(int? frames, int? bits, int? channels)
        {
            string formattedFrames   = frames   == null ? "(null)" : frames.ToString();
            string formattedBits     = bits     == null ? "(null)" : bits + "bit";
            string formattedChannels = channels == null ? "(null)" : channels == 0 ? "0" : ChannelDescriptor(channels).ToLower();
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