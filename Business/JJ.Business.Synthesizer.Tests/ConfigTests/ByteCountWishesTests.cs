using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    public class ByteCountWishesTests
    {
        [TestMethod]
        public void ByteCount_Basic_ConversionFormula()
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
        public void ByteCount_Basic_Init()
        {
            int byteCount = 100;
            // ReSharper disable once UnusedVariable
            var entities = new ConfigTestEntities(x => x.ByteCount(byteCount));
        }
        
        [TestMethod]
        public void ByteCount_Basic_Getters()
        {
            // Lambdas `() =>` improve assertion messages but can complicate debugging.
            // TODO: Remove `() =>` when complex tests ensure clear assertion messages.
            
            int byteCount = 100;
            var entities = new ConfigTestEntities(x => x.ByteCount(byteCount));
            
            AreEqual(DefaultByteCount, () => entities.SynthBound.ConfigSection.ByteCount());
            
            AreEqual(byteCount, () => entities.SynthBound.SynthWishes   .ByteCount());
            AreEqual(byteCount, () => entities.SynthBound.FlowNode      .ByteCount());
            AreEqual(byteCount, () => entities.SynthBound.FlowNode2     .ByteCount());
            AreEqual(byteCount, () => entities.SynthBound.ConfigResolver.ByteCount(entities.SynthBound.SynthWishes));
            
            AreEqual(byteCount, () => entities.TapeBound.Tape           .ByteCount());
            AreEqual(byteCount, () => entities.TapeBound.TapeConfig     .ByteCount());
            AreEqual(byteCount, () => entities.TapeBound.TapeActions    .ByteCount());
            AreEqual(byteCount, () => entities.TapeBound.TapeAction     .ByteCount());
            
            AreEqual(byteCount, () => entities.BuffBound.Buff           .ByteCount(DefaultCourtesyFrames));
            AreEqual(byteCount, () => entities.BuffBound.AudioFileOutput.ByteCount(DefaultCourtesyFrames));
            AreEqual(byteCount, () => entities.Independent.Sample       .ByteCount());
            AreEqual(byteCount, () => entities.Immutable.WavHeader      .ByteCount());
            
            AreEqual(DefaultSizeOfBitDepth, () => entities.Immutable.Bits.BitsToByteCount());
            AreEqual(DefaultSizeOfBitDepth, () => entities.Immutable.Type.ByteCount());
            
            foreach (TapeEntities channelEntities in entities.ChannelEntities)
            {
                AreEqual(byteCount, () => channelEntities.TapeBound.Tape           .ByteCount());
                AreEqual(byteCount, () => channelEntities.TapeBound.TapeConfig     .ByteCount());
                AreEqual(byteCount, () => channelEntities.TapeBound.TapeActions    .ByteCount());
                AreEqual(byteCount, () => channelEntities.TapeBound.TapeAction     .ByteCount());
                
                AreEqual(byteCount, () => channelEntities.BuffBound.Buff           .ByteCount(DefaultCourtesyFrames));
                AreEqual(byteCount, () => channelEntities.BuffBound.AudioFileOutput.ByteCount(DefaultCourtesyFrames));
                AreEqual(byteCount, () => channelEntities.Independent.Sample       .ByteCount());
                AreEqual(byteCount, () => channelEntities.Immutable.WavHeader      .ByteCount());

                AreEqual(DefaultSizeOfBitDepth, () => channelEntities.Immutable.Bits.BitsToByteCount());
                AreEqual(DefaultSizeOfBitDepth, () => channelEntities.Immutable.Type.ByteCount());
            }
        }
        
        [TestMethod]
        public void ByteCount_Basic_Setters()
        {
            int init = 100;
            int value = 200;
            var entities = new ConfigTestEntities(x => x.ByteCount(init));
            
            // TODO: No setter yet
            //entities.SynthBound.ConfigSection.ByteCount(value));
            
            entities.SynthBound.SynthWishes   .ByteCount(value);
            entities.SynthBound.FlowNode      .ByteCount(value);
            entities.SynthBound.FlowNode2     .ByteCount(value);
            entities.SynthBound.ConfigResolver.ByteCount(value, entities.SynthBound.SynthWishes);
            
            entities.TapeBound.Tape           .ByteCount(value);
            entities.TapeBound.TapeConfig     .ByteCount(value);
            entities.TapeBound.TapeActions    .ByteCount(value);
            entities.TapeBound.TapeAction     .ByteCount(value);
            
            // TODO: No setter yet
            //entities.BuffBound.Buff         .ByteCount(value, DefaultCourtesyFrames);
            entities.BuffBound.AudioFileOutput.ByteCount(value, DefaultCourtesyFrames);
            // TODO: Sample is too buff?
            //entities.Independent.Sample     .ByteCount(value);
            entities.Immutable.WavHeader      .ByteCount(value);
            
            // TODO: Add quasi-setters?
            //entities.Immutable.Bits.BitsToByteCount(2);
            //entities.Immutable.Type.ByteCount(2);
            
            foreach (TapeEntities channelEntities in entities.ChannelEntities)
            {
                channelEntities.TapeBound.Tape           .ByteCount(value);
                channelEntities.TapeBound.TapeConfig     .ByteCount(value);
                channelEntities.TapeBound.TapeActions    .ByteCount(value);
                channelEntities.TapeBound.TapeAction     .ByteCount(value);
                
                // TODO: No setter yet
                //channelEntities.BuffBound.Buff         .ByteCount(value, DefaultCourtesyFrames);
                channelEntities.BuffBound.AudioFileOutput.ByteCount(value, DefaultCourtesyFrames);
                // TODO: Sample is too buff?
                //channelEntities.Independent.Sample     .ByteCount(value);
                channelEntities.Immutable.WavHeader      .ByteCount(value);

                // TODO: Add quasi-setters?
                //ChannelEntities.Immutable.Bits.ByteCountToBits(2);
                //channelEntities.Immutable.Type.ByteCount(2);
            }
        }
    }
}
