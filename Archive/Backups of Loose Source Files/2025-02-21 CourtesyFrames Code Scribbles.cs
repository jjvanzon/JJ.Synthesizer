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
