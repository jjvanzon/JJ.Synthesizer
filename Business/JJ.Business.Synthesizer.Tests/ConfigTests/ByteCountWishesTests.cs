using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            
            foreach (var channelEntities in entities.ChannelEntities)
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
            int sizeOfBitDepthInit = 4;
            int sizeOfBitDepthValue = 2;

            var x = CreateTestEntities(init, sizeOfBitDepthInit);
            
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
            x.Immutable.SampleDataType    .ByteCount(sizeOfBitDepthValue, x.SynthBound.Context);
            x.Immutable.SampleDataTypeEnum.ByteCount(sizeOfBitDepthValue);
            x.Immutable.Bits              .ByteCount(sizeOfBitDepthValue);
            x.Immutable.Type              .ByteCount(sizeOfBitDepthValue);
            
            foreach (var channelEntities in x.ChannelEntities)
            {
                channelEntities.TapeBound.Tape              .ByteCount(value);
                channelEntities.TapeBound.TapeConfig        .ByteCount(value);
                channelEntities.TapeBound.TapeActions       .ByteCount(value);
                channelEntities.TapeBound.TapeAction        .ByteCount(value);
                channelEntities.BuffBound.Buff              .ByteCount(value, DefaultCourtesyFrames);
                channelEntities.BuffBound.AudioFileOutput   .ByteCount(value, DefaultCourtesyFrames);
                channelEntities.Immutable.WavHeader         .ByteCount(value);
                channelEntities.Immutable.Bits              .ByteCount(sizeOfBitDepthValue);
                channelEntities.Immutable.Type              .ByteCount(sizeOfBitDepthValue);
                channelEntities.Immutable.SampleDataType    .ByteCount(sizeOfBitDepthValue, x.SynthBound.Context);
                channelEntities.Immutable.SampleDataTypeEnum.ByteCount(sizeOfBitDepthValue);
            }
        }
        
        [TestMethod] 
        public void SynthBound_ByteCount()
        {            
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init, sizeOfBitDepthInit);
                Assert_All_Getters     (x, init, sizeOfBitDepthInit);
                
                setter(x);
                
                Assert_SynthBound_Getters   (x, value);
                //Assert_TapeBound_Getters  (x, init );
                //Assert_BuffBound_Getters  (x, init );
                //Assert_Independent_Getters(x, init );
                //Assert_Immutable_Getters  (x, init );
                //Assert_Bitness_Getters    (x, sizeOfBitDepthInit);
                
                x.Record();
                Assert_All_Getters(x, value, sizeOfBitDepthInit); // By Design: Properties that express bit-ness don't change their ByteCount the same way.
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .ByteCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .ByteCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.ByteCount(value, x.SynthBound.SynthWishes)));
        }

        [TestMethod] 
        public void TapeBound_ByteCount()
        {
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init, sizeOfBitDepthInit);
                Assert_All_Getters     (x, init, sizeOfBitDepthInit);
                
                setter(x);
                
                //Assert_SynthBound_Getters (x, init);
                Assert_TapeBound_Getters    (x, init); // By Design: Tape is too buff to change. FrameCount will be based on buff.
                //Assert_BuffBound_Getters  (x, init);
                //Assert_Independent_Getters(x, init);
                //Assert_Immutable_Getters  (x, init);
                //Assert_Bitness_Getters    (x, sizeOfBitDepthInit);

                x.Record();
                Assert_All_Getters          (x, init, sizeOfBitDepthInit); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .ByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .ByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.ByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .ByteCount(value)));
        }

        [TestMethod] 
        public void BuffBound_ByteCount()
        {
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init, sizeOfBitDepthInit);
                Assert_All_Getters     (x, init, sizeOfBitDepthInit);
                
                setter(x);
                
                //Assert_SynthBound_Getters   (x, init );
                //Assert_TapeBound_Getters    (x, init );
                Assert_Buff_Getters           (x, init ); // By Design: Buff's "too buff" to change! FrameCount will be based on bytes!
                Assert_AudioFileOutput_Getters(x, value); // By Design: "Out" will take on new properties when asked.
                //Assert_Independent_Getters  (x, init );
                //Assert_Immutable_Getters    (x, init );
                //Assert_Bitness_Getters    (x, sizeOfBitDepthInit);

                x.Record();
                Assert_All_Getters            (x, init, sizeOfBitDepthInit);
            }

            // TODO: Why the dependency on CourtesyFrames?
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .ByteCount(value, DefaultCourtesyFrames)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.ByteCount(value, DefaultCourtesyFrames)));
        }
        
        [TestMethod]
        public void Immutable_SizeOfBitDepth()
        {
            var x = CreateTestEntities(init, sizeOfBitDepthInit);

            // WavHeader
            
            var wavHeaders = new List<WavHeaderStruct>();
            {
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    Assert_Immutable_Getters(wavHeader2, value);
                    
                    wavHeaders.Add(wavHeader2);
                }
                
                AssertProp(() => x.Immutable.WavHeader.ByteCount(value));
            }

            // SampleDataTypeEnum

            var sampleDataTypeEnums = new List<SampleDataTypeEnum>();
            {
                void AssertProp(Func<SampleDataTypeEnum> setter)
                {
                    Assert_Bitness_Getters(x.Immutable.SampleDataTypeEnum, sizeOfBitDepthInit);
                    
                    SampleDataTypeEnum sampleDataTypeEnum2 = setter();
                    
                    Assert_Bitness_Getters(x.Immutable.SampleDataTypeEnum, sizeOfBitDepthInit);
                    Assert_Bitness_Getters(sampleDataTypeEnum2, sizeOfBitDepthValue);
                    
                    sampleDataTypeEnums.Add(sampleDataTypeEnum2);
                }
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.ByteCount(sizeOfBitDepthValue));
            }

            // SampleDataType

            var sampleDataTypes = new List<SampleDataType>();
            {
                void AssertProp(Func<SampleDataType> setter)
                {
                    Assert_Bitness_Getters(x.Immutable.SampleDataType, sizeOfBitDepthInit);

                    SampleDataType sampleDataType2 = setter();
                    
                    Assert_Bitness_Getters(x.Immutable.SampleDataType, sizeOfBitDepthInit);
                    Assert_Bitness_Getters(sampleDataType2, sizeOfBitDepthValue);
                    
                    sampleDataTypes.Add(sampleDataType2);
                }
            
                AssertProp(() => x.Immutable.SampleDataType.ByteCount(sizeOfBitDepthValue, x.SynthBound.Context));
            }
            
            // Type

            var types = new List<Type>();
            {
                void AssertProp(Func<Type> setter)
                {
                    Assert_Bitness_Getters(x.Immutable.Type, sizeOfBitDepthInit);
                    
                    var type2 = setter();
                    
                    Assert_Bitness_Getters(x.Immutable.Type, sizeOfBitDepthInit);
                    Assert_Bitness_Getters(type2, sizeOfBitDepthValue);
                    
                    types.Add(type2);
                }

                AssertProp(() => x.Immutable.Type.ByteCount(sizeOfBitDepthValue));
            }
                
            // Bits

            var bitsList = new List<int>();
            {
                void AssertProp(Func<int> setter)
                {
                    Assert_Bitness_Getters(x.Immutable.Bits, sizeOfBitDepthInit);

                    int bits2 = setter();
                    
                    Assert_Bitness_Getters(x.Immutable.Bits, sizeOfBitDepthInit);
                    Assert_Bitness_Getters(bits2, sizeOfBitDepthValue);
                    
                    bitsList.Add(bits2);
                }
            
                AssertProp(() => x.Immutable.Bits.ByteCount(sizeOfBitDepthValue));
            }

            // After-Record
            x.Record();
            
            // All is reset
            Assert_SynthBound_Getters    (x, init);
            Assert_TapeBound_Getters     (x, init);
            Assert_BuffBound_Getters     (x, init);
            Assert_Independent_Getters   (x, init);
            Assert_Immutable_Getters     (x, init);
            Assert_Bitness_Getters(x, sizeOfBitDepthInit);
            
            // Except for our variables
            wavHeaders         .ForEach(w => Assert_Immutable_Getters(w, value));
            sampleDataTypeEnums.ForEach(e => Assert_Bitness_Getters(e, sizeOfBitDepthValue));
            sampleDataTypes    .ForEach(s => Assert_Bitness_Getters(s, sizeOfBitDepthValue));
            types              .ForEach(t => Assert_Bitness_Getters(t, sizeOfBitDepthValue));
            bitsList           .ForEach(b => Assert_Bitness_Getters(b, sizeOfBitDepthValue));
        }

        // Getter Helpers
        
        private void Assert_All_Getters(ConfigTestEntities x, int byteCount, int sizeOfBitDepth)
        {
            Assert_SynthBound_Getters    (x, byteCount);
            Assert_TapeBound_Getters     (x, byteCount);
            Assert_BuffBound_Getters     (x, byteCount);
            Assert_Independent_Getters   (x, byteCount);
            Assert_Immutable_Getters     (x, byteCount);
            Assert_Bitness_Getters(x, sizeOfBitDepth);
        }
        
        private void Assert_SynthBound_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigResolver);
            AreEqual(byteCount, () => x.SynthBound.SynthWishes   .ByteCount());
            AreEqual(byteCount, () => x.SynthBound.FlowNode      .ByteCount());
            AreEqual(byteCount, () => x.SynthBound.ConfigResolver.ByteCount(x.SynthBound.SynthWishes));
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);
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
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.AudioFileOutput);
            AreEqual(byteCount, () => x.BuffBound.AudioFileOutput.ByteCount(x.Immutable.CourtesyFrames));
        }
        
        private void Assert_Buff_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);
            AreEqual(byteCount, () => x.BuffBound.Buff.ByteCount(x.Immutable.CourtesyFrames));
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            IsNotNull(() => x.Independent.Sample);
            AreEqual(byteCount, () => x.Independent.Sample.ByteCount());
        }
        
        private void Assert_Immutable_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, byteCount);
        }
                
        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int byteCount)
        {
            AreEqual(byteCount, () => wavHeader.ByteCount());
        }

        private void Assert_Bitness_Getters(ConfigTestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Bitness_Getters(x.Immutable.SampleDataTypeEnum, sizeOfBitDepth);
            Assert_Bitness_Getters(x.Immutable.SampleDataType    , sizeOfBitDepth);
            Assert_Bitness_Getters(x.Immutable.Type              , sizeOfBitDepth);
            Assert_Bitness_Getters(x.Immutable.Bits              , sizeOfBitDepth);
        }
        
        private void Assert_Bitness_Getters(SampleDataTypeEnum sampleDataTypeEnum, int sizeOfBitDepth)
        {
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.ByteCount());
        }
        
        private void Assert_Bitness_Getters(SampleDataType sampleDataType, int sizeOfBitDepth)
        {
            IsNotNull(() => sampleDataType);
            AreEqual(sizeOfBitDepth, () => sampleDataType.ByteCount());
        }
        
        private void Assert_Bitness_Getters(Type type, int sizeOfBitDepth)
        {
            IsNotNull(() => type);
            AreEqual(sizeOfBitDepth, () => type.ByteCount());
        }
        
        private void Assert_Bitness_Getters(int bits, int sizeOfBitDepth)
        {
            AreEqual(sizeOfBitDepth, () => bits.ByteCount());
        }
        
        // Test Data Helpers
        
        int init = 100;
        int value = 200;
        int sizeOfBitDepthInit = 4;
        int sizeOfBitDepthValue = 2;

        //private static ConfigTestEntities CreateTestEntities(int init) 
        //    => new ConfigTestEntities(x => x.ByteCount(init));
        
        private static ConfigTestEntities CreateTestEntities(int init, int initSizeOfBitDepth) 
            => new ConfigTestEntities(x => x.SizeOfBitDepth(initSizeOfBitDepth).ByteCount(init));
    }
}
