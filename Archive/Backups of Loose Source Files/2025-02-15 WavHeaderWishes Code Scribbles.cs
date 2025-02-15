        
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
