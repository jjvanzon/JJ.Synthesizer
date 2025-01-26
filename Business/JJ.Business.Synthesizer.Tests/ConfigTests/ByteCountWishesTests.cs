using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.ConfigTests.FrameCountWishesTests;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
            
            AreEqual(DefaultSizeOfBitDepth, () => entities.Immutable.Bits              .ByteCount());
            AreEqual(DefaultSizeOfBitDepth, () => entities.Immutable.Type              .ByteCount());
            AreEqual(DefaultSizeOfBitDepth, () => entities.Immutable.SampleDataType    .ByteCount());
            AreEqual(DefaultSizeOfBitDepth, () => entities.Immutable.SampleDataTypeEnum.ByteCount());
            
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

                AreEqual(DefaultSizeOfBitDepth, () => channelEntities.Immutable.Bits              .ByteCount());
                AreEqual(DefaultSizeOfBitDepth, () => channelEntities.Immutable.Type              .ByteCount());
                AreEqual(DefaultSizeOfBitDepth, () => channelEntities.Immutable.SampleDataType    .ByteCount());
                AreEqual(DefaultSizeOfBitDepth, () => channelEntities.Immutable.SampleDataTypeEnum.ByteCount());
            }
        }
        
        [TestMethod]
        public void ByteCount_Basic_Setters()
        {
            int init = 100;
            int value = 200;
            var x = CreateTestEntities(init);
            
            x.SynthBound.SynthWishes      .ByteCount(value);
            x.SynthBound.FlowNode         .ByteCount(value);
            x.SynthBound.FlowNode2        .ByteCount(value);
            x.SynthBound.ConfigResolver   .ByteCount(value, x.SynthBound.SynthWishes);
            x.TapeBound.Tape              .ByteCount(value);
            x.TapeBound.TapeConfig        .ByteCount(value);
            x.TapeBound.TapeActions       .ByteCount(value);
            x.TapeBound.TapeAction        .ByteCount(value);
            x.BuffBound.Buff              .ByteCount(value, DefaultCourtesyFrames);
            x.BuffBound.AudioFileOutput   .ByteCount(value, DefaultCourtesyFrames);
            x.Immutable.WavHeader         .ByteCount(value);
            x.Immutable.SampleDataType    .ByteCount(2, x.SynthBound.Context);
            x.Immutable.SampleDataTypeEnum.ByteCount(2);
            x.Immutable.Bits              .ByteCount(2);
            x.Immutable.Type              .ByteCount(2);
            
            foreach (TapeEntities channelEntities in x.ChannelEntities)
            {
                channelEntities.TapeBound.Tape              .ByteCount(value);
                channelEntities.TapeBound.TapeConfig        .ByteCount(value);
                channelEntities.TapeBound.TapeActions       .ByteCount(value);
                channelEntities.TapeBound.TapeAction        .ByteCount(value);
                channelEntities.BuffBound.Buff              .ByteCount(value, DefaultCourtesyFrames);
                channelEntities.BuffBound.AudioFileOutput   .ByteCount(value, DefaultCourtesyFrames);
                channelEntities.Immutable.WavHeader         .ByteCount(value);
                channelEntities.Immutable.Bits              .ByteCount(2);
                channelEntities.Immutable.Type              .ByteCount(2);
                channelEntities.Immutable.SampleDataType    .ByteCount(2, x.SynthBound.Context);
                channelEntities.Immutable.SampleDataTypeEnum.ByteCount(2);
            }
        }
        
        [TestMethod] 
        public void SynthBound_ByteCount()
        {            
            int init  = 100;
            var value = 200;
            
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters          (x, init );
                
                setter(x);
                
                Assert_SynthBound_Getters   (x, value);
                //Assert_TapeBound_Getters  (x, init );
                //Assert_BuffBound_Getters  (x, init );
                //Assert_Independent_Getters(x, init );
                //Assert_Immutable_Getters  (x, init );
                
                x.Record();
                Assert_All_Getters          (x, value);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .ByteCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .ByteCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.ByteCount(value, x.SynthBound.SynthWishes)));
        }

        [TestMethod] 
        public void TapeBound_ByteCount()
        {
            int init  = 100;
            int value = 200;

            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters          (x, init);
                
                setter(x);
                
                //Assert_SynthBound_Getters (x, init);
                Assert_TapeBound_Getters    (x, init); // By Design: Tape is too buff to change. FrameCount will be based on buff.
                //Assert_BuffBound_Getters  (x, init);
                //Assert_Independent_Getters(x, init);
                //Assert_Immutable_Getters  (x, init);
                
                x.Record();
                Assert_All_Getters          (x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .ByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .ByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.ByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .ByteCount(value)));
        }

        [TestMethod] 
        public void BuffBound_ByteCount()
        {
            int init = 100;
            int value = 200;
            
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters            (x, init );
                
                setter(x);
                
                //Assert_SynthBound_Getters   (x, init );
                //Assert_TapeBound_Getters    (x, init );
                Assert_Buff_Getters           (x, init ); // By Design: Buff's "too buff" to change! FrameCount will be based on bytes!
                Assert_AudioFileOutput_Getters(x, value); // By Design: "Out" will take on new properties when asked.
                //Assert_Independent_Getters  (x, init );
                //Assert_Immutable_Getters    (x, init );
                
                x.Record();
                Assert_All_Getters            (x, init );
            }

            // TODO: Why the dependency on CourtesyFrames?
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .ByteCount(value, DefaultCourtesyFrames)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.ByteCount(value, DefaultCourtesyFrames)));
        }

        // Getter Helpers
        
        private void Assert_All_Getters(ConfigTestEntities x, int byteCount)
        {
            Assert_SynthBound_Getters (x, byteCount);
            Assert_TapeBound_Getters  (x, byteCount);
            Assert_BuffBound_Getters  (x, byteCount);
            //Assert_Independent_Getters(x, byteCount);
            //Assert_Immutable_Getters  (x, byteCount);
        }
        
        private void Assert_SynthBound_Getters(ConfigTestEntities x, int byteCount)
        {
            AreEqual(byteCount, () => x.SynthBound.SynthWishes   .ByteCount());
            AreEqual(byteCount, () => x.SynthBound.FlowNode      .ByteCount());
            AreEqual(byteCount, () => x.SynthBound.ConfigResolver.ByteCount(x.SynthBound.SynthWishes));
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, int byteCount)
        {
            AreEqual(byteCount, () => x.TapeBound.Tape       .ByteCount());
            AreEqual(byteCount, () => x.TapeBound.TapeConfig .ByteCount());
            AreEqual(byteCount, () => x.TapeBound.TapeActions.ByteCount());
            AreEqual(byteCount, () => x.TapeBound.TapeAction .ByteCount());
        }
                
        private void Assert_BuffBound_Getters(ConfigTestEntities x, int byteCount)
        {
            Assert_Buff_Getters           (x, byteCount);
            Assert_AudioFileOutput_Getters(x, byteCount);
        }

        private void Assert_AudioFileOutput_Getters(ConfigTestEntities x, int byteCount)
        {
            AreEqual(byteCount, () => x.BuffBound.AudioFileOutput.ByteCount(x.Immutable.CourtesyFrames));
        }
        
        private void Assert_Buff_Getters(ConfigTestEntities x, int byteCount)
        {
            AreEqual(byteCount, () => x.BuffBound.Buff.ByteCount(x.Immutable.CourtesyFrames));
        }

        // Test Data Helpers
        
        private static ConfigTestEntities CreateTestEntities(int init) => new ConfigTestEntities(x => x.ByteCount(init));
    }
}
