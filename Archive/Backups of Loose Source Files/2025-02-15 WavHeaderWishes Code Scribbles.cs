        
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


                                          FilePath        = t.Buff.FilePath, // TODO: Save the file too.
                              Bytes           = t.Buff.Bytes,
                              Stream          = new MemoryStream(t.Buff.Bytes),
                              BinaryReader    = new BinaryReader(new MemoryStream(t.Buff.Bytes))

        
        //public static string GetNumberedFilePathSafe(
        //    string originalFilePath,
        //    string numberPrefix = " (",
        //    string numberSuffix = ")",
        //    bool mustNumberFirstFile = false,
        //    int maxExtensionLength = DEFAULT_MAX_EXTENSION_LENGTH)
        //{
        //    CreateSafeFileStream(originalFilePath, numberPrefix, numberSuffix, mustNumberFirstFile, maxExtensionLength);
        //}

            //var testEntities = new TestEntities(x => x.WithBits(test.Bits.Init)
            //                                          .WithChannels(test.Channels.Init)
            //                                          .WithSamplingRate(test.SamplingRate.Init)
            //                                          .WithFrameCount(test.FrameCount.Init)
            //                                          .WithCourtesyFrames(test.CourtesyFrames.Init));


private void Assert(AudioFileOutput entity, Case test/*, int? courtesyFrames = null*/)
            //int courtesyFramesCoalesced = courtesyFrames ?? test.CourtesyFrames;
            //AreEqual(test.FrameCount,   () => entity.FrameCount(courtesyFramesCoalesced), Tolerance);
            //AreEqual(test.FrameCount,   () => entity.FrameCount(courtesyFrames), Tolerance);

            //string filePathResolved = ResolveFilePath(buff.AudioFormat(), filePath, buff.FilePath, callerMemberName); // Resolve to use AudioFormat

                
                //(_, BuffBound.DestStream) = CreateSafeFileStream(filePathBase);
                
                //(_, buffEntities.DestStream) = CreateSafeFileStream(filePathBase);
                //buffEntities.BinaryWriter    = new BinaryWriter(buffEntities.DestStream);

            new Case { FrameCount     = { From = 256, To = 100+3 } },

) .FromTemplate(new Case
        
            { SamplingRate = 48000, Bits = 32, Channels = 2, CourtesyFrames = 3, FrameCount = 100+3 },
            
            new Case { Bits           = { To =     8 } },
            new Case { Bits           = { To =    16 } },
            new Case { Channels       = { To =     1 } },
            new Case { Channels       = { From =   1 } },
            new Case { Channels       =            1   },
            new Case { SamplingRate   = { To = 96000 } },
            new Case { FrameCount     = { To =   256 } },
            new Case { CourtesyFrames = { To =     4 } }

            //subCollection._rootCollection = _rootCollection ?? this;

            //var rootCollection = _rootCollection ?? this;
            //subCollection._rootCollection = rootCollection;

            bool isRoot = _parentCollection == null;

            var rootCollection = this;
            while (rootCollection._parentCollection != null)
            {
                rootCollection = rootCollection._parentCollection;
            }

public CaseCollection<TCase> Concat(CaseCollection<TCase> otherCases)
{
    if (otherCases == null) throw new NullException(() => otherCases);
    return new CaseCollection<TCase>(otherCases.GetAll().Concat(Parent.GetAll()).ToArray());
}

internal CaseCollection<TCase> ConcatWithParent()
{

    return new CaseCollection<TCase>(this.GetAll().Concat(Parent.GetAll()).ToArray());
}
        

        //internal CaseCollection<TCase> Root
        //{
        //    get
        //    {
        //        var root = this;
        //        while (root.Parent != null)
        //        {
        //            root = root.Parent;
        //        }
        //        return root;
        //    }
        //}

                
                //if (!AllowDuplicates && _caseDictionary.ContainsKey(newCase.Key))
                //{
                //    throw new Exception($"Duplicate key '{key}' found while adding Cases.");
                //}

            
            if (IsRoot)
            {
                return subCollection;
            }
            else
            {
                return new CaseCollection<TCase>(GetAll().Union(subCases));
            }



            
            
            
            
            
            
            
            
            

            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.SynthBound .SynthWishes                ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.SynthBound .FlowNode                   ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.SynthBound .ConfigResolver, synthWishes), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.SynthBound .ConfigSection              ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.TapeBound  .Tape                       ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.TapeBound  .TapeConfig                 ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.TapeBound  .TapeActions                ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.TapeBound  .TapeAction                 ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.BuffBound  .Buff,            frameCount), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.BuffBound  .AudioFileOutput, frameCount), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.Independent.Sample                     ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.Independent.AudioInfoWish              ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.Independent.AudioFileInfo              ), ForDestBytes);
            //AssertSetter(() => binaries.DestBytes.WriteWavHeader(entities.Immutable  .WavHeader                  ), ForDestBytes);

            //AssertSetter(() => binaries.DestFilePath       .Write(entities.Immutable.WavHeader), ForDestFilePath);
            //AssertSetter(() => binaries.DestBytes          .Write(entities.Immutable.WavHeader), ForDestBytes   );
            //AssertSetter(() => binaries.DestStream         .Write(entities.Immutable.WavHeader), ForDestStream  );
            //AssertSetter(() => binaries.BinaryWriter       .Write(entities.Immutable.WavHeader), ForBinaryWriter);

            //using (var x = CreateEntities(test, withDisk: true))
            //{
            //    x.BuffBound.DestFilePath.WriteWavHeader(x.Immutable.WavHeader   );
            //    x.BuffBound.DestStream  .WriteWavHeader(x.Immutable.WavHeader   );
            //    x.BuffBound.BinaryWriter.WriteWavHeader(x.Immutable.WavHeader   );
            //}