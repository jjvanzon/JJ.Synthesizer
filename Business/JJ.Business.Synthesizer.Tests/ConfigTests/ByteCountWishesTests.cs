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
            var entities = new ConfigTestEntities(x => x.ByteCount(100));
            
            entities.SynthBound.SynthWishes   .ByteCount();
            entities.SynthBound.FlowNode      .ByteCount();
            entities.SynthBound.FlowNode2     .ByteCount();
            //entities.SynthBound.ConfigResolver.ByteCount();
            //entities.SynthBound.ConfigSection .ByteCount();
            entities.TapeBound.Tape           .ByteCount();
            entities.TapeBound.TapeConfig     .ByteCount();
            entities.TapeBound.TapeActions    .ByteCount();
            entities.TapeBound.TapeAction     .ByteCount();
            entities.BuffBound.Buff           .ByteCount();
            entities.BuffBound.AudioFileOutput.ByteCount();
            entities.Independent.Sample       .ByteCount();
            entities.Immutable.WavHeader      .ByteCount();
            //entities.Immutable.Bits           .ByteCount();
            //entities.Immutable.Type           .ByteCount();
            //entities.Immutable.AudioLength    .ByteCount();
            
            foreach (TapeEntities channelEntities in entities.ChannelEntities)
            {
                channelEntities.TapeBound.Tape           .ByteCount();
                channelEntities.TapeBound.TapeConfig     .ByteCount();
                channelEntities.TapeBound.TapeActions    .ByteCount();
                channelEntities.TapeBound.TapeAction     .ByteCount();
                channelEntities.BuffBound.Buff           .ByteCount();
                channelEntities.BuffBound.AudioFileOutput.ByteCount();
                channelEntities.Independent.Sample       .ByteCount();
                channelEntities.Immutable.WavHeader      .ByteCount();
                //channelEntities.Immutable.Bits           .ByteCount();
                //channelEntities.Immutable.Type           .ByteCount();
                //channelEntities.Immutable.AudioLength    .ByteCount();
            }
        }
    }
}
