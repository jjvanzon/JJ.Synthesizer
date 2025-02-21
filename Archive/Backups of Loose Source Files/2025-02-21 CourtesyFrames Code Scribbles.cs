        // FrameCount and CourtesyFrames

        //public static int AssertFrameCountMinusCourtesyFrames(int frameCount, int courtesyFrames, bool strict = true) => AssertFrameCountMinusCourtesyFrames((int?)frameCount, (int?)courtesyFrames, strict) ?? default;
        //public static int? AssertFrameCountMinusCourtesyFrames(int? frameCount, int? courtesyFrames, bool strict = false)
        //{
        //    AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);
        //    return frameCount - courtesyFrames;
        //}
        
        //public static void AssertFrameCountWithCourtesyFrames(int  frameCount, int  courtesyFrames, bool strict = true)  => AssertFrameCountWithCourtesyFrames((int?)frameCount, (int?)courtesyFrames, strict);
        //public static void AssertFrameCountWithCourtesyFrames(int  frameCount, int? courtesyFrames, bool strict = false) => AssertFrameCountWithCourtesyFrames((int?)frameCount, courtesyFrames,       strict);
        //public static void AssertFrameCountWithCourtesyFrames(int? frameCount, int  courtesyFrames, bool strict = false) => AssertFrameCountWithCourtesyFrames(frameCount,       (int?)courtesyFrames, strict);
        //public static void AssertFrameCountWithCourtesyFrames(int? frameCount, int? courtesyFrames, bool strict = false)
        //{
        //    AssertFrameCount(frameCount, strict);
        //    AssertCourtesyFrames(courtesyFrames, strict);

        //    if (frameCount < courtesyFrames)
        //    {
        //        throw new Exception(
        //            $"{nameof(FrameCountExtensionWishes.FrameCount)} = {frameCount} " +
        //            $"but should be a minimum of {courtesyFrames} {nameof(CourtesyFrames)}.");
        //    }
        //}
        
        //// FrameCount and CourtesyFrames

        //public static int AssertFrameCountMinusCourtesyFrames(this int frameCount, int courtesyFrames, bool strict = true)
        //    => ConfigWishes.AssertFrameCountMinusCourtesyFrames(frameCount, courtesyFrames, strict);
        
        //public static int? AssertFrameCountMinusCourtesyFrames(this int? frameCount, int? courtesyFrames, bool strict = false)
        //    => ConfigWishes.AssertFrameCountMinusCourtesyFrames(frameCount, courtesyFrames, strict);
        
        //public static void AssertFrameCountWithCourtesyFrames(this int  frameCount, int  courtesyFrames, bool strict = true)
        //    => ConfigWishes.AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);
        
        //public static void AssertFrameCountWithCourtesyFrames(this int  frameCount, int? courtesyFrames, bool strict = false)
        //    => ConfigWishes.AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);
        
        //public static void AssertFrameCountWithCourtesyFrames(this int? frameCount, int  courtesyFrames, bool strict = false)
        //    => ConfigWishes.AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);
        
        //public static void AssertFrameCountWithCourtesyFrames(this int? frameCount, int? courtesyFrames, bool strict = false)
        //    => ConfigWishes.AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);


        public static Buff FrameCount(Buff obj, int value) => SetFrameCount(obj, value);
        public static Buff WithFrameCount(Buff obj, int value) => SetFrameCount(obj, value);
        public static Buff SetFrameCount(Buff obj, int value)
        {
            return FrameCount(obj, value, CoalesceCourtesyFrames(courtesyFrames));
        }

        public static Buff            WriteWavHeader(Buff            entity,    string          filePath, int courtesyFrames) { entity.ToWavHeader().Write(filePath); return entity; }
        public static Buff            WriteWavHeader(Buff            entity,    byte[]          dest,     int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static Buff            WriteWavHeader(Buff            entity,    BinaryWriter    dest,     int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static Buff            WriteWavHeader(Buff            entity,    Stream          dest,     int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  Buff            entity,   int courtesyFrames) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      Buff            entity,   int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      Buff            entity,   int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      Buff            entity,   int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    string          filePath, int courtesyFrames) { entity.ToWavHeader().Write(filePath); return entity; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    byte[]          dest,     int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    BinaryWriter    dest,     int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static AudioFileOutput WriteWavHeader(AudioFileOutput entity,    Stream          dest,     int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return entity; }
        public static string          WriteWavHeader(string          filePath,  AudioFileOutput entity,   int courtesyFrames) { entity.ToWavHeader().Write(filePath); return filePath; }
        public static byte[]          WriteWavHeader(byte[]          dest,      AudioFileOutput entity,   int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static Stream          WriteWavHeader(Stream          dest,      AudioFileOutput entity,   int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return dest; }
        public static BinaryWriter    WriteWavHeader(BinaryWriter    dest,      AudioFileOutput entity,   int courtesyFrames) { entity.ToWavHeader().Write(dest    ); return dest; }

        public static Buff            WriteWavHeader(this Buff            entity,    string          filePath, int courtesyFrames) => WavWishes.WriteWavHeader(entity,   filePath, courtesyFrames);
        public static Buff            WriteWavHeader(this Buff            entity,    byte[]          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static Buff            WriteWavHeader(this Buff            entity,    BinaryWriter    dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static Buff            WriteWavHeader(this Buff            entity,    Stream          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static string          WriteWavHeader(this string          filePath,  Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(filePath, entity,   courtesyFrames);
        public static byte[]          WriteWavHeader(this byte[]          dest,      Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static Stream          WriteWavHeader(this Stream          dest,      Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      Buff            entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    string          filePath, int courtesyFrames) => WavWishes.WriteWavHeader(entity,   filePath, courtesyFrames);
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    byte[]          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    BinaryWriter    dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static AudioFileOutput WriteWavHeader(this AudioFileOutput entity,    Stream          dest,     int courtesyFrames) => WavWishes.WriteWavHeader(entity,   dest,     courtesyFrames);
        public static string          WriteWavHeader(this string          filePath,  AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(filePath, entity,   courtesyFrames);
        public static byte[]          WriteWavHeader(this byte[]          dest,      AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static Stream          WriteWavHeader(this Stream          dest,      AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);
        public static BinaryWriter    WriteWavHeader(this BinaryWriter    dest,      AudioFileOutput entity,   int courtesyFrames) => WavWishes.WriteWavHeader(dest,     entity,   courtesyFrames);

            
            AreEqual(byteCount, () => x.BuffBound.Buff.ByteCount   (                ), courtesyBytes, Up);
            AreEqual(byteCount, () => x.BuffBound.Buff.GetByteCount(                ), courtesyBytes, Up);
            AreEqual(byteCount, () =>                  ByteCount   (x.BuffBound.Buff), courtesyBytes, Up);
            AreEqual(byteCount, () =>                  GetByteCount(x.BuffBound.Buff), courtesyBytes, Up);
            AreEqual(byteCount, () => ConfigWishes    .ByteCount   (x.BuffBound.Buff), courtesyBytes, Up);
            AreEqual(byteCount, () => ConfigWishes    .GetByteCount(x.BuffBound.Buff), courtesyBytes, Up);

            // Test nullable and non-nullable courtesyFrames separately.
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .ByteCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.ByteCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .SetByteCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetByteCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .WithByteCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.WithByteCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ByteCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ByteCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , SetByteCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, SetByteCount (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , WithByteCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, WithByteCount(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.ByteCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.ByteCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.SetByteCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.SetByteCount (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.WithByteCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.WithByteCount(x.BuffBound.AudioFileOutput, value)));


        //static CaseCollection<Case> DependencyCasesPlus { get; } = Cases.FromTemplate(new Case
        //    {
        //        Name = "DependencyPlus",
        //        Bits = 32,
        //        Channels = 1,
        //        SamplingRate = 1000, 
        //        AudioLength = 0.1, 
        //        HeaderLength = 0,
        //        CourtesyFrames = 2,
        //        Plus = 8,
        //        ByteCount = { From = 400+8, To = 800+8 }
        //    },
        //    new Case { FrameCount = { From = 100+2, To = 200+2 } },
        //    new Case { AudioLength = { To = 0.2 } },
        //    new Case { SamplingRate = { To = 2000 } },
        //    new Case { Channels = { To = 2 }, ByteCount = { To = 800+16 } },
        //    new Case { Bits = { To = 16 }, ByteCount = { To = 200+4 } },
        //    new Case { HeaderLength = { To = WavHeaderLength }, ByteCount = { To = 400+8 + WavHeaderLength } },
        //    new Case { CourtesyFrames = { To = 3 }, Plus = { To = 12 }, ByteCount = { To = 400+12 } }
        //);


        //[UsedImplicitly]
        //static CaseCollection<Case> WavDependencyCasesPlus { get; } = Cases.FromTemplate(new Case
        //    {
        //        Name = "WavPlus",
        //        Bits = 32,
        //        Channels = 1,
        //        SamplingRate = 1000, 
        //        AudioLength = 0.1, 
        //        HeaderLength = WavHeaderLength,
        //        CourtesyFrames = 2,
        //        Plus = 8,
        //        ByteCount = { From = 400+8 + WavHeaderLength, To = 800+8 + WavHeaderLength }
        //    },
        //    new Case { FrameCount = { From = 100+2, To = 200+2 } },
        //    new Case { AudioLength = { To = 0.2 } },
        //    new Case { SamplingRate = { To = 2000 } },
        //    new Case { Channels = { To = 2 }, ByteCount = { To = 800 + 16 + WavHeaderLength } },
        //    new Case { Bits = { To = 16 }, ByteCount = { To = 200 + 4 + WavHeaderLength } },
        //    // TODO: { To = 0 } becomes (0,44). Separate test for case definition?
        //    //new Case { HeaderLength = { To = 0 }, ByteCount = { To = 400 + 8 } }, 
        //    new Case { CourtesyFrames = { To = 3 }, Plus = { To = 12 }, ByteCount = { To = 400 + 12 + WavHeaderLength } }
        //);

        //static object PlusCases { get; } = BasicCases.Concat(DependencyCasesPlus).Concat(WavDependencyCasesPlus);
