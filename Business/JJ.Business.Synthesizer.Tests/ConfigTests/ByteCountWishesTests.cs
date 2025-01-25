using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    public class ByteCountWishesTests
    {
        [TestMethod]
        public void ByteCount_Basics_ConversionFormula()
        {
            const double audioLength    = 0.5;
            const int    samplingRate   = 24000;
            const int    bits           = 16;
            const int    channels       = 2;
            const int    headerLength   = 44;
            const int    courtesyFrames = 3;
            
            const int frameCount = (int)(audioLength * samplingRate) + courtesyFrames;
            const int frameSize  = bits / 8 * channels;
            
            const int byteCountExpected = frameCount * frameSize + headerLength;
            
            AreEqual(byteCountExpected, () => audioLength.ByteCount(samplingRate, bits, channels, headerLength, courtesyFrames));
            AreEqual(byteCountExpected, () => audioLength.ByteCount(samplingRate, frameSize, headerLength, courtesyFrames));
            AreEqual(byteCountExpected, () => frameCount.ByteCount(bits, channels, headerLength));
            AreEqual(byteCountExpected, () => frameCount.ByteCount(frameSize, headerLength));
        }
        
        [TestMethod]
        public void ByteCount_Basics_InitEntities()
        {
            int byteCount = 100;
            
            var entities = new ConfigTestEntities(x => x.ByteCount(byteCount));
            
            AreEqual(byteCount, () => entities.SynthBound.SynthWishes   .ByteCount());
            
            AreEqual(byteCount, () => entities.SynthBound.FlowNode      .ByteCount());
            AreEqual(byteCount, () => entities.SynthBound.FlowNode2     .ByteCount());
            AreEqual(byteCount, () => entities.SynthBound.ConfigResolver.ByteCount(entities.SynthBound.SynthWishes));
            
            // TODO: Add ByteCount to the ConfigSection extensions and Accessor
            //entities.SynthBound.ConfigSection .ByteCount();
            
            AreEqual(byteCount, () => entities.TapeBound.Tape           .ByteCount());
            AreEqual(byteCount, () => entities.TapeBound.TapeConfig     .ByteCount());
            AreEqual(byteCount, () => entities.TapeBound.TapeActions    .ByteCount());
            AreEqual(byteCount, () => entities.TapeBound.TapeAction     .ByteCount());
            AreEqual(byteCount, () => entities.BuffBound.Buff           .ByteCount(DefaultCourtesyFrames));
            //AreEqual(byteCount, () => entities.BuffBound.AudioFileOutput.ByteCount()); // TODO: Assert Failed
            AreEqual(byteCount, () => entities.Independent.Sample       .ByteCount());
            AreEqual(byteCount, () => entities.Immutable.WavHeader      .ByteCount());
            
            // TODO: ByteCount might serve as an alias for SizeOfBitDepth here.
            //entities.Immutable.Bits           .ByteCount();
            //entities.Immutable.Type           .ByteCount();
            
            foreach (TapeEntities channelEntities in entities.ChannelEntities)
            {
                AreEqual(byteCount, () => channelEntities.TapeBound.Tape           .ByteCount());
                AreEqual(byteCount, () => channelEntities.TapeBound.TapeConfig     .ByteCount());
                AreEqual(byteCount, () => channelEntities.TapeBound.TapeActions    .ByteCount());
                AreEqual(byteCount, () => channelEntities.TapeBound.TapeAction     .ByteCount());
                AreEqual(byteCount, () => channelEntities.BuffBound.Buff           .ByteCount(DefaultCourtesyFrames));
                //AreEqual(byteCount, () => channelEntities.BuffBound.AudioFileOutput.ByteCount()); // TODO: Assert Failed
                AreEqual(byteCount, () => channelEntities.Independent.Sample       .ByteCount());
                AreEqual(byteCount, () => channelEntities.Immutable.WavHeader      .ByteCount());

                // TODO: ByteCount might serve as an alias for SizeOfBitDepth here.
                //channelEntities.Immutable.Bits           .ByteCount();
                //channelEntities.Immutable.Type           .ByteCount();
            }
        }
    }
}
