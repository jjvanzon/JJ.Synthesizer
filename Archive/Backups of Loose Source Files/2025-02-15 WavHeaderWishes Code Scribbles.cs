        
        //public static void WriteWavHeader<TBits>(
        //    this BinaryWriter writer, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
        //    => writer.WriteWavHeader(ToWish(typeof(TBits), channelsEnum, samplingRate, frameCount));

        //public static void WriteWavHeader(
        //    this BinaryWriter writer, SampleDataTypeEnum bitsEnum, int channels, int samplingRate, int frameCount)
        //    => writer.WriteWavHeader(ToWish(bitsEnum, channels, samplingRate, frameCount));

        //public static void WriteWavHeader<TBits>(
        //    this Stream stream, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
        //    => stream.WriteWavHeader(ToWish<TBits>(channelsEnum, samplingRate, frameCount));
        
        //public static void WriteWavHeader(
        //    this Stream stream, SampleDataTypeEnum bitsEnum, int channels, int samplingRate, int frameCount)
        //    => stream.WriteWavHeader(ToWish(bitsEnum, channels, samplingRate, frameCount));
                
        //public static void WriteWavHeader<TBits>(
        //    this string filePath, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
        //    => filePath.WriteWavHeader(ToWish(typeof(TBits), channelsEnum, samplingRate, frameCount));

        //public static void WriteWavHeader(
        //    this string filePath,
        //    SampleDataTypeEnum bitsEnum, int channels, int samplingRate, int frameCount)
        //    => filePath.WriteWavHeader(ToWish(bitsEnum, channels, samplingRate, frameCount));

        
        //public static AudioInfoWish ToWish<TBits>(SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
        //    => new AudioInfoWish
        //    {
        //        Bits         = TypeToBits<TBits>(),
        //        Channels     = channelsEnum.ToChannels(),
        //        SamplingRate = samplingRate,
        //        FrameCount   = frameCount
        //    };
        
        //public static AudioInfoWish ToWish(
        //    Type bitsType, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
        //    => new AudioInfoWish
        //    {
        //        Bits         = bitsType.ToBits(),
        //        Channels     = channelsEnum.ToChannels(),
        //        SamplingRate = samplingRate,
        //        FrameCount   = frameCount
        //    };
        
        //public static AudioInfoWish ToWish(
        //    SampleDataTypeEnum bitsEnum, int channels, int samplingRate, int frameCount)
        //    => new AudioInfoWish
        //    {
        //        Bits         = bitsEnum.ToBits(),
        //        Channels     = channels,
        //        SamplingRate = samplingRate,
        //        FrameCount   = frameCount
        //    };


        //public static AudioFileInfo FromWish(this AudioInfoWish wish) => new AudioFileInfo
        //{
        //    BytesPerValue = wish.SizeOfBitDepth(),
        //    ChannelCount  = wish.Channels(),
        //    SampleCount   = wish.FrameCount(),
        //    SamplingRate  = wish.SamplingRate()
        //};

        public static void ReadAudioInfo(this SynthWishes entity, string       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this SynthWishes entity, byte[]       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this SynthWishes entity, Stream       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this SynthWishes entity, BinaryReader source) => entity.ReadWavHeader(source);

        public static void ReadAudioInfo(this FlowNode entity, string       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this FlowNode entity, byte[]       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this FlowNode entity, Stream       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this FlowNode entity, BinaryReader source) => entity.ReadWavHeader(source);

        internal static void ReadAudioInfo(this ConfigResolver entity, string source, SynthWishes synthWishes) 
            => entity.ReadWavHeader(source, synthWishes);
        internal static void ReadAudioInfo(this ConfigResolver entity, byte[] source, SynthWishes synthWishes)
            => entity.ReadWavHeader(source, synthWishes);
        internal static void ReadAudioInfo(this ConfigResolver entity, Stream source, SynthWishes synthWishes) 
            => entity.ReadWavHeader(source, synthWishes);
        internal static void ReadAudioInfo(this ConfigResolver entity, BinaryReader source, SynthWishes synthWishes) 
            => entity.ReadWavHeader(source, synthWishes);
        
        public static void ReadAudioInfo(this Tape entity, string       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this Tape entity, byte[]       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this Tape entity, Stream       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this Tape entity, BinaryReader source) => entity.ReadWavHeader(source);
        
        public static void ReadAudioInfo(this TapeConfig entity, string       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this TapeConfig entity, byte[]       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this TapeConfig entity, Stream       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this TapeConfig entity, BinaryReader source) => entity.ReadWavHeader(source);
                
        public static void ReadAudioInfo(this TapeActions entity, string       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this TapeActions entity, byte[]       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this TapeActions entity, Stream       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this TapeActions entity, BinaryReader source) => entity.ReadWavHeader(source);
                
        public static void ReadAudioInfo(this TapeAction entity, string       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this TapeAction entity, byte[]       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this TapeAction entity, Stream       source) => entity.ReadWavHeader(source);
        public static void ReadAudioInfo(this TapeAction entity, BinaryReader source) => entity.ReadWavHeader(source);
                
        public static void ReadAudioInfo(this Buff entity, string source, int courtesyFrames, IContext context) 
            => entity.ReadWavHeader(source, courtesyFrames, context);
        public static void ReadAudioInfo(this Buff entity, byte[] source, int courtesyFrames, IContext context) 
            => entity.ReadWavHeader(source, courtesyFrames, context);
        public static void ReadAudioInfo(this Buff entity, Stream source, int courtesyFrames, IContext context) 
            => entity.ReadWavHeader(source, courtesyFrames, context);
        public static void ReadAudioInfo(this Buff entity, BinaryReader source, int courtesyFrames, IContext context) 
            => entity.ReadWavHeader(source, courtesyFrames, context);


        
        private TestEntities CreateEntities(Case testCase, int frameCount,double audioLength) 
            => new TestEntities(x => x.Bits(testCase.Bits)
                                      .Channels(testCase.Channels)
                                      .SamplingRate(testCase.SamplingRate)
                                      .CourtesyFrames(testCase.CourtesyFrames)
                                      .AudioLength(audioLength));

            public CaseProp<double> AudioLength { get; set; }
            
            AudioInfoWish wish = x.SynthBound.SynthWishes.ToWish();
            Assert(test, wish);
            
            AreEqual<int>(test.Bits,         () => x.SynthBound.SynthWishes.ToWish().Bits);
            AreEqual<int>(test.Channels,     () => x.SynthBound.SynthWishes.ToWish().Channels);
            AreEqual<int>(test.SamplingRate, () => x.SynthBound.SynthWishes.ToWish().SamplingRate);
            AreEqual<int>(test.FrameCount,   () => x.SynthBound.SynthWishes.ToWish().FrameCount);

        private void Assert(Case testCase, AudioInfoWish wish)
        {
            IsNotNull(() => testCase);
            IsNotNull(() => wish);
            
            int samplingRate = testCase.SamplingRate;
            int frameCount   = testCase.FrameCount;
            int bits         = testCase.Bits;
            int channels     = testCase.Channels;
            
            AreEqual(bits,         () => wish.Bits);
            AreEqual(channels,     () => wish.Channels);
            AreEqual(samplingRate, () => wish.SamplingRate);
            AreEqual(frameCount,   () => wish.FrameCount);
        }

            //AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWishWithFrameCount(frameCount).Bits         );
            //AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWishWithFrameCount(frameCount).Channels     );
            //AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWishWithFrameCount(frameCount).SamplingRate );
            //AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWishWithFrameCount(frameCount).FrameCount   );

            
            //AreEqual(test.Bits,           () => x.BuffBound  .AudioFileOutput.ToWishWithCourtesyFrames(courtesyFrames).Bits         );
            //AreEqual(test.Channels,       () => x.BuffBound  .AudioFileOutput.ToWishWithCourtesyFrames(courtesyFrames).Channels     );
            //AreEqual(test.SamplingRate,   () => x.BuffBound  .AudioFileOutput.ToWishWithCourtesyFrames(courtesyFrames).SamplingRate );
            //AreEqual(test.FrameCount,     () => x.BuffBound  .AudioFileOutput.ToWishWithCourtesyFrames(courtesyFrames).FrameCount   );

        
        //public static AudioInfoWish ToWish(this Buff entity, int frameCount) => new AudioInfoWish
        //{
        //    Bits         = entity.Bits(),
        //    Channels     = entity.Channels(),
        //    SamplingRate = entity.SamplingRate(),
        //    FrameCount   = frameCount.AssertFrameCount()
        //};
                
        //public static AudioInfoWish ToWish(this AudioFileOutput entity, int frameCount) => new AudioInfoWish
        //{
        //    Bits         = entity.Bits(),
        //    Channels     = entity.Channels(),
        //    SamplingRate = entity.SamplingRate(),
        //    FrameCount   = frameCount.AssertFrameCount()
        //};


// By Design: Use From value; FrameCount doesn't change for Tape when it's Buff.

            AssertProp((x, info) => { x.SynthBound.ConfigSection   .FromWish(info)                         ; Assert(x.SynthBound .ConfigSection,   test); });
