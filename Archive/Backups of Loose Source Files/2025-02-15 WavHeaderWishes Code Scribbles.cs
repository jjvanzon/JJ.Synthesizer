        
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

        //static CaseCollection<int> ConfigSectionCase = Cases.Add(new Case(
            //Case test = Cases[caseKey];

            
            (int bits,      int channels, int samplingRate, int frameCount) infoTupleWithInts     = (x.Immutable.Bits,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            (Type bitsType, int channels, int samplingRate, int frameCount) infoTupleWithType     = (x.Immutable.Type,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            (               int channels, int samplingRate, int frameCount)  infoTupleWithoutBits  = (                                x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            (SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) infoTupleWithEnums    = (x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum, x.Immutable.SamplingRate, frameCount);
            (SampleDataType   bitsEntity, SpeakerSetup   channelsEntity, int samplingRate, int frameCount) infoTupleWithEntities = (x.Immutable.SampleDataType,     x.Immutable.SpeakerSetup,     x.Immutable.SamplingRate, frameCount);

            
            var InfoTupleWithInts      = (x.Immutable.Bits,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var InfoTupleWithType     = (x.Immutable.Type,               x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var InfoTupleWithoutBits = (                                x.Immutable.Channels,         x.Immutable.SamplingRate, frameCount);
            var InfoTupleWithEnums     = (x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum, x.Immutable.SamplingRate, frameCount);
            var InfoTuplesWithEntities   = (x.Immutable.SampleDataType,     x.Immutable.SpeakerSetup,     x.Immutable.SamplingRate, frameCount);

            AreEqual(test.Bits,           () => x.SynthBound.SynthWishes         .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.SynthBound.SynthWishes         .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.SynthBound.SynthWishes         .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.SynthBound.SynthWishes         .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.SynthBound.FlowNode            .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.SynthBound.FlowNode            .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.SynthBound.FlowNode            .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.SynthBound.FlowNode            .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.SynthBound.ConfigResolver      .ToWish(synthWishes).Bits        );
            AreEqual(test.Channels,       () => x.SynthBound.ConfigResolver      .ToWish(synthWishes).Channels    );
            AreEqual(test.SamplingRate,   () => x.SynthBound.ConfigResolver      .ToWish(synthWishes).SamplingRate);
            AreEqual(test.FrameCount,     () => x.SynthBound.ConfigResolver      .ToWish(synthWishes).FrameCount, -Tolerance);
            AreEqual(DefaultBits,         () => x.SynthBound.ConfigSection       .ToWish().Bits                  );
            AreEqual(DefaultChannels,     () => x.SynthBound.ConfigSection       .ToWish().Channels              );
            AreEqual(DefaultSamplingRate, () => x.SynthBound.ConfigSection       .ToWish().SamplingRate          );
            AreEqual(DefaultFrameCount,   () => x.SynthBound.ConfigSection       .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound.Tape                 .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.TapeBound.Tape                 .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.TapeBound.Tape                 .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.TapeBound.Tape                 .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound.TapeConfig           .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.TapeBound.TapeConfig           .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.TapeBound.TapeConfig           .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.TapeBound.TapeConfig           .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound.TapeActions          .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.TapeBound.TapeActions          .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.TapeBound.TapeActions          .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.TapeBound.TapeActions          .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.TapeBound.TapeAction           .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.TapeBound.TapeAction           .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.TapeBound.TapeAction           .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.TapeBound.TapeAction           .ToWish().FrameCount, -Tolerance);

            // TODO: More syntax sugar
            //Case zeroFramesCase = (Case)Case.FromTemplate(test, new Case { FrameCount = 0 }).Single();

            AreEqual(test.Bits,           () => x.BuffBound.Buff                 .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.BuffBound.Buff                 .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.BuffBound.Buff                 .ToWish().SamplingRate          );
            AreEqual(0,                   () => x.BuffBound.Buff                 .ToWish().FrameCount, -Tolerance); // By Design: FrameCount stays 0 without courtesyBytes
            AreEqual(test.Bits,           () => x.BuffBound.AudioFileOutput      .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.BuffBound.AudioFileOutput      .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.BuffBound.AudioFileOutput      .ToWish().SamplingRate          );
            AreEqual(0,                   () => x.BuffBound.AudioFileOutput      .ToWish().FrameCount, -Tolerance); // By Design: FrameCount stays 0 without courtesyBytes
            AreEqual(test.Bits,           () => x.BuffBound.Buff                 .ToWish(courtesy).Bits     );
            AreEqual(test.Channels,       () => x.BuffBound.Buff                 .ToWish(courtesy).Channels );
            AreEqual(test.SamplingRate,   () => x.BuffBound.Buff                 .ToWish(courtesy).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound.Buff                 .ToWish(courtesy).FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound.AudioFileOutput      .ToWish(courtesy).Bits    );
            AreEqual(test.Channels,       () => x.BuffBound.AudioFileOutput      .ToWish(courtesy).Channels);
            AreEqual(test.SamplingRate,   () => x.BuffBound.AudioFileOutput      .ToWish(courtesy).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound.AudioFileOutput      .ToWish(courtesy).FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound.Buff                 .ToWish().FrameCount(frameCount).Bits);
            AreEqual(test.Channels,       () => x.BuffBound.Buff                 .ToWish().FrameCount(frameCount).Channels);
            AreEqual(test.SamplingRate,   () => x.BuffBound.Buff                 .ToWish().FrameCount(frameCount).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound.Buff                 .ToWish().FrameCount(frameCount).FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.BuffBound.AudioFileOutput      .ToWish().FrameCount(frameCount).Bits);
            AreEqual(test.Channels,       () => x.BuffBound.AudioFileOutput      .ToWish().FrameCount(frameCount).Channels);
            AreEqual(test.SamplingRate,   () => x.BuffBound.AudioFileOutput      .ToWish().FrameCount(frameCount).SamplingRate);
            AreEqual(test.FrameCount,     () => x.BuffBound.AudioFileOutput      .ToWish().FrameCount(frameCount).FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Independent.Sample             .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Independent.Sample             .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Independent.Sample             .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Independent.Sample             .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Independent.AudioFileInfo      .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Independent.AudioFileInfo      .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Independent.AudioFileInfo      .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Independent.AudioFileInfo      .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Immutable.WavHeader            .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Immutable.WavHeader            .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Immutable.WavHeader            .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Immutable.WavHeader            .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Immutable.InfoTupleWithInts    .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Immutable.InfoTupleWithInts    .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Immutable.InfoTupleWithInts    .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Immutable.InfoTupleWithInts    .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Immutable.InfoTupleWithType    .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Immutable.InfoTupleWithType    .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Immutable.InfoTupleWithType    .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Immutable.InfoTupleWithType    .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Immutable.InfoTupleWithEnums   .ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Immutable.InfoTupleWithEnums   .ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Immutable.InfoTupleWithEnums   .ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Immutable.InfoTupleWithEnums   .ToWish().FrameCount, -Tolerance);
            AreEqual(test.Bits,           () => x.Immutable.InfoTupleWithEntities.ToWish().Bits                  );
            AreEqual(test.Channels,       () => x.Immutable.InfoTupleWithEntities.ToWish().Channels              );
            AreEqual(test.SamplingRate,   () => x.Immutable.InfoTupleWithEntities.ToWish().SamplingRate          );
            AreEqual(test.FrameCount,     () => x.Immutable.InfoTupleWithEntities.ToWish().FrameCount, -Tolerance);
                AreEqual(test.Bits,         () => x.Immutable.InfoTupleWithoutBits.ToWish<byte> ().Bits                 );
                AreEqual(test.Channels,     () => x.Immutable.InfoTupleWithoutBits.ToWish<byte> ().Channels             );
                AreEqual(test.SamplingRate, () => x.Immutable.InfoTupleWithoutBits.ToWish<byte> ().SamplingRate         );
                AreEqual(test.FrameCount,   () => x.Immutable.InfoTupleWithoutBits.ToWish<byte> ().FrameCount, -Tolerance);
                AreEqual(test.Bits,         () => x.Immutable.InfoTupleWithoutBits.ToWish<short>().Bits                 );
                AreEqual(test.Channels,     () => x.Immutable.InfoTupleWithoutBits.ToWish<short>().Channels             );
                AreEqual(test.SamplingRate, () => x.Immutable.InfoTupleWithoutBits.ToWish<short>().SamplingRate         );
                AreEqual(test.FrameCount,   () => x.Immutable.InfoTupleWithoutBits.ToWish<short>().FrameCount, -Tolerance);
                AreEqual(test.Bits,         () => x.Immutable.InfoTupleWithoutBits.ToWish<float>().Bits                 );
                AreEqual(test.Channels,     () => x.Immutable.InfoTupleWithoutBits.ToWish<float>().Channels             );
                AreEqual(test.SamplingRate, () => x.Immutable.InfoTupleWithoutBits.ToWish<float>().SamplingRate         );
                AreEqual(test.FrameCount,   () => x.Immutable.InfoTupleWithoutBits.ToWish<float>().FrameCount, -Tolerance);

        
        private void ThrowBitsNotSupported(int bits) => throw new Exception(NotSupportedMessage(nameof(bits), bits, ValidBits));

// Duplicate
AreEqual(test.Bits,           () => x.BuffBound.AudioFileOutput      .ToWavHeader(courtesy).Bits());
AreEqual(test.Channels,       () => x.BuffBound.AudioFileOutput      .ToWavHeader(courtesy).Channels());
AreEqual(test.SamplingRate,   () => x.BuffBound.AudioFileOutput      .ToWavHeader(courtesy).SamplingRate);
AreEqual(test.FrameCount,     () => x.BuffBound.AudioFileOutput      .ToWavHeader(courtesy).FrameCount(), -Tolerance);

            //AreEqual(test.Bits,           () => x.SynthBound.SynthWishes         .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.SynthBound.SynthWishes         .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.SynthBound.SynthWishes         .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.SynthBound.SynthWishes         .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.SynthBound.FlowNode            .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.SynthBound.FlowNode            .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.SynthBound.FlowNode            .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.SynthBound.FlowNode            .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.SynthBound.ConfigResolver      .ToWavHeader(synthWishes).BitsPerValue);
            //AreEqual(test.Channels,       () => x.SynthBound.ConfigResolver      .ToWavHeader(synthWishes).ChannelCount);
            //AreEqual(test.SamplingRate,   () => x.SynthBound.ConfigResolver      .ToWavHeader(synthWishes).SamplingRate);
            //AreEqual(test.FrameCount,     () => x.SynthBound.ConfigResolver      .ToWavHeader(synthWishes).FrameCount(), -Tolerance);
            //AreEqual(DefaultBits,         () => x.SynthBound.ConfigSection       .ToWavHeader().BitsPerValue            );
            //AreEqual(DefaultChannels,     () => x.SynthBound.ConfigSection       .ToWavHeader().ChannelCount            );
            //AreEqual(DefaultSamplingRate, () => x.SynthBound.ConfigSection       .ToWavHeader().SamplingRate            );
            //AreEqual(DefaultFrameCount,   () => x.SynthBound.ConfigSection       .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.TapeBound.Tape                 .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.TapeBound.Tape                 .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.TapeBound.Tape                 .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.TapeBound.Tape                 .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.TapeBound.TapeConfig           .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.TapeBound.TapeConfig           .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.TapeBound.TapeConfig           .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.TapeBound.TapeConfig           .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.TapeBound.TapeActions          .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.TapeBound.TapeActions          .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.TapeBound.TapeActions          .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.TapeBound.TapeActions          .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.TapeBound.TapeAction           .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.TapeBound.TapeAction           .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.TapeBound.TapeAction           .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.TapeBound.TapeAction           .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.BuffBound.Buff                 .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.BuffBound.Buff                 .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.BuffBound.Buff                 .ToWavHeader().SamplingRate            );
            //AreEqual(0,                   () => x.BuffBound.Buff                 .ToWavHeader().FrameCount(), -Tolerance); // By Design: FrameCount stays 0 without courtesyBytes
            //AreEqual(test.Bits,           () => x.BuffBound.Buff                 .ToWavHeader(courtesy).Bits()    );
            //AreEqual(test.Channels,       () => x.BuffBound.Buff                 .ToWavHeader(courtesy).Channels());
            //AreEqual(test.SamplingRate,   () => x.BuffBound.Buff                 .ToWavHeader(courtesy).SamplingRate);
            //AreEqual(test.FrameCount,     () => x.BuffBound.Buff                 .ToWavHeader(courtesy).FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.BuffBound.Buff                 .ToWavHeader().FrameCount(frameCount, courtesy).Bits()); // TODO: Why is courtesyFrames needed here?
            //AreEqual(test.Channels,       () => x.BuffBound.Buff                 .ToWavHeader().FrameCount(frameCount, courtesy).Channels());
            //AreEqual(test.SamplingRate,   () => x.BuffBound.Buff                 .ToWavHeader().FrameCount(frameCount, courtesy).SamplingRate);
            //AreEqual(test.FrameCount,     () => x.BuffBound.Buff                 .ToWavHeader().FrameCount(frameCount, courtesy).FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.BuffBound.AudioFileOutput      .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.BuffBound.AudioFileOutput      .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.BuffBound.AudioFileOutput      .ToWavHeader().SamplingRate            );
            //AreEqual(0,                   () => x.BuffBound.AudioFileOutput      .ToWavHeader().FrameCount(), -Tolerance); // By Design: FrameCount stays 0 without courtesyBytes
            //AreEqual(test.Bits,           () => x.BuffBound.AudioFileOutput      .ToWavHeader(courtesy).Bits()    );
            //AreEqual(test.Channels,       () => x.BuffBound.AudioFileOutput      .ToWavHeader(courtesy).Channels());
            //AreEqual(test.SamplingRate,   () => x.BuffBound.AudioFileOutput      .ToWavHeader(courtesy).SamplingRate);
            //AreEqual(test.FrameCount,     () => x.BuffBound.AudioFileOutput      .ToWavHeader(courtesy).FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.BuffBound.AudioFileOutput      .ToWavHeader().FrameCount(frameCount, courtesy).Bits()); // TODO: Why is courtesyFrames needed here?
            //AreEqual(test.Channels,       () => x.BuffBound.AudioFileOutput      .ToWavHeader().FrameCount(frameCount, courtesy).Channels());
            //AreEqual(test.SamplingRate,   () => x.BuffBound.AudioFileOutput      .ToWavHeader().FrameCount(frameCount, courtesy).SamplingRate);
            //AreEqual(test.FrameCount,     () => x.BuffBound.AudioFileOutput      .ToWavHeader().FrameCount(frameCount, courtesy).FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.Independent.Sample             .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.Independent.Sample             .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.Independent.Sample             .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.Independent.Sample             .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.Independent.AudioFileInfo      .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.Independent.AudioFileInfo      .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.Independent.AudioFileInfo      .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.Independent.AudioFileInfo      .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.Immutable.InfoTupleWithInts    .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.Immutable.InfoTupleWithInts    .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.Immutable.InfoTupleWithInts    .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.Immutable.InfoTupleWithInts    .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.Immutable.InfoTupleWithType    .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.Immutable.InfoTupleWithType    .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.Immutable.InfoTupleWithType    .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.Immutable.InfoTupleWithType    .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.Immutable.InfoTupleWithEnums   .ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.Immutable.InfoTupleWithEnums   .ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.Immutable.InfoTupleWithEnums   .ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.Immutable.InfoTupleWithEnums   .ToWavHeader().FrameCount(), -Tolerance);
            //AreEqual(test.Bits,           () => x.Immutable.InfoTupleWithEntities.ToWavHeader().BitsPerValue            );
            //AreEqual(test.Channels,       () => x.Immutable.InfoTupleWithEntities.ToWavHeader().ChannelCount            );
            //AreEqual(test.SamplingRate,   () => x.Immutable.InfoTupleWithEntities.ToWavHeader().SamplingRate            );
            //AreEqual(test.FrameCount,     () => x.Immutable.InfoTupleWithEntities.ToWavHeader().FrameCount(), -Tolerance);
                //AreEqual(test.Bits,         () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<byte> ().BitsPerValue            );
                //AreEqual(test.Channels,     () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<byte> ().ChannelCount            );
                //AreEqual(test.SamplingRate, () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<byte> ().SamplingRate            );
                //AreEqual(test.FrameCount,   () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<byte> ().FrameCount(), -Tolerance);
                //AreEqual(test.Bits,         () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<short>().BitsPerValue            );
                //AreEqual(test.Channels,     () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<short>().ChannelCount            );
                //AreEqual(test.SamplingRate, () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<short>().SamplingRate            );
                //AreEqual(test.FrameCount,   () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<short>().FrameCount(), -Tolerance);
                //AreEqual(test.Bits,         () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<float>().BitsPerValue            );
                //AreEqual(test.Channels,     () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<float>().ChannelCount            );
                //AreEqual(test.SamplingRate, () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<float>().SamplingRate            );
                //AreEqual(test.FrameCount,   () => x.Immutable.InfoTupleWithoutBits.ToWavHeader<float>().FrameCount(), -Tolerance);
            //else
            //{   // ncrunch: no coverage start
            //    throw new Exception(NotSupportedMessage(nameof(test.Bits), test.Bits, ValidBits));
            //    // ncrunch: no coverage end
            //}

            TestProp((x, y) => { y.SourceFilePath     .ReadWavHeader(x.SynthBound.SynthWishes)                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, y) => { y.SourceBytes        .ReadWavHeader(x.SynthBound.SynthWishes)                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, y) => { y.SourceStream       .ReadWavHeader(x.SynthBound.SynthWishes)                   ; Assert(x.SynthBound.SynthWishes,    test); });
            TestProp((x, y) => { y.BinaryReader       .ReadWavHeader(x.SynthBound.SynthWishes)                   ; Assert(x.SynthBound.SynthWishes,    test); });


            //BuffBoundEntities bin = null;
            
            //void AssertWrite(Action setter, TestEntityEnum entity)
            //{
            //    using (var changedEntities = CreateModifiedEntities(test, withDisk: entity == ForDestFilePath))
            //    {
            //        bin = changedEntities.BuffBound;
            //        AssertInvariant(changedEntities, test);
                    
            //        setter();
                    
            //        if (entity == ForDestFilePath) Assert(bin.DestFilePath, test);
            //        if (entity == ForDestBytes)    Assert(bin.DestBytes,    test);
            //        if (entity == ForDestStream)   Assert(bin.DestStream,   test);
            //        if (entity == ForBinaryWriter) Assert(bin.BinaryWriter, test);
            //    }
            //}

            //TestProp(x => { AreEqual(x.SynthBound.SynthWishes, x.SynthBound.SynthWishes    .ApplyInfo(info))                   ; AssertEntity(x.SynthBound.SynthWishes,    test             ); });
            //TestProp(x => { x.SynthBound.SynthWishes    .ApplyInfo(info)                   ; AssertEntity(x.SynthBound.SynthWishes,    test             ); });

                AssertWrite(bin => AreEqual(bin.DestFilePath      , WriteWavHeader<byte>(bin.DestFilePath, x.Channels, x.SamplingRate, frameCount  )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes         , WriteWavHeader<byte>(bin.DestBytes,    x.Channels, x.SamplingRate, frameCount  )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream        , WriteWavHeader<byte>(bin.DestStream,   x.Channels, x.SamplingRate, frameCount  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter      , WriteWavHeader<byte>(bin.BinaryWriter, x.Channels, x.SamplingRate, frameCount  )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(bin.DestFilePath      , WriteWavHeader<byte>(bin.DestFilePath, x.Channels, x.SamplingRate, frameCount  )), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(bin.DestBytes         , WriteWavHeader<byte>(bin.DestBytes,    x.Channels, x.SamplingRate, frameCount  )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(bin.DestStream        , WriteWavHeader<byte>(bin.DestStream,   x.Channels, x.SamplingRate, frameCount  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(bin.BinaryWriter      , WriteWavHeader<byte>(bin.BinaryWriter, x.Channels, x.SamplingRate, frameCount  )), ForBinaryWriter, test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.DestFilePath)), ForDestFilePath, test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.DestBytes   )), ForDestBytes,    test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.DestStream  )), ForDestStream,   test);
                AssertWrite(bin => AreEqual(x.InfoTupleWithoutBits, WriteWavHeader<byte>((x.Channels, x.SamplingRate, frameCount), bin.BinaryWriter)), ForBinaryWriter, test);
                AssertWrite(bin =>                                  WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.DestFilePath ), ForDestFilePath, test);
                AssertWrite(bin =>                                  WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.DestBytes    ), ForDestBytes,    test);
                AssertWrite(bin =>                                  WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.DestStream   ), ForDestStream,   test);
                AssertWrite(bin =>                                  WriteWavHeader<byte>( x.Channels, x.SamplingRate, frameCount,  bin.BinaryWriter ), ForBinaryWriter, test);

    
    internal static class WavWishesTestExtensions
    {

        public static SynthWishes             AssertEntity(this SynthWishes            entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static FlowNode                AssertEntity(this FlowNode               entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static ConfigResolverAccessor  AssertEntity(this ConfigResolverAccessor entity,   WavWishesTests.Case test, SynthWishes synthWishes) { WavWishesTests.AssertEntity(entity, test, synthWishes); return entity;   }
        public static Tape                    AssertEntity(this Tape                   entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static TapeConfig              AssertEntity(this TapeConfig             entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static TapeActions             AssertEntity(this TapeActions            entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static TapeAction              AssertEntity(this TapeAction             entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static Buff                    AssertEntity(this Buff                   entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static AudioFileOutput         AssertEntity(this AudioFileOutput        entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static Sample                  AssertEntity(this Sample                 entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static AudioFileInfo           AssertEntity(this AudioFileInfo          entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static AudioInfoWish           AssertEntity(this AudioInfoWish          entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static WavHeaderStruct         AssertEntity(this WavHeaderStruct        entity,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(entity,   test); return entity;   }
        public static string                  AssertEntity(this string                 filePath, WavWishesTests.Case test) { WavWishesTests.AssertEntity(filePath, test); return filePath; }
        public static byte[]                  AssertEntity(this byte[]                 bytes,    WavWishesTests.Case test) { WavWishesTests.AssertEntity(bytes,    test); return bytes;    }
        public static Stream                  AssertEntity(this Stream                 stream,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(stream,   test); return stream;   }
        public static BinaryWriter            AssertEntity(this BinaryWriter           writer,   WavWishesTests.Case test) { WavWishesTests.AssertEntity(writer,   test); return writer;   }
    }
