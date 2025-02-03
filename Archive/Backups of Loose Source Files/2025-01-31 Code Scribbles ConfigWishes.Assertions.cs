 ConfigWishes.Assertions:

public int GetBits => (Has(_bits) ? _bits.Value : Has(_section.Bits) ? _section.Bits.Value : DefaultBits).AssertBits();
public int GetChannels => (Has(_channels) ? _channels.Value : Has(_section.Channels) ? _section.Channels.Value : DefaultChannels).AssertChannels();
return Has(_section.SamplingRate) ? _section.SamplingRate.Value : DefaultSamplingRate;
public AudioFileFormatEnum GetAudioFormat => AssertAudioFormat(Has(_audioFormat) ? _audioFormat.Value : Has(_section.AudioFormat) ? _section.AudioFormat.Value : DefaultAudioFormat);
public InterpolationTypeEnum GetInterpolation => AssertInterpolation(Has(_interpolation) ? _interpolation.Value : _section.Interpolation ?? DefaultInterpolation);
public static int CoalesceZero(this int? value, int defaultValue) => ConfigWishes.Coalesce(value, defaultValue);
public static double CoalesceZero(this double? value, double defaultValue) => ConfigWishes.Coalesce(value, defaultValue);
public static double Coalesce(double? value, double defaultValue) => Has(value) ? value.Value : defaultValue;
public static int Coalesce(int? value, int defaultValue) => Has(value) ? value.Value : defaultValue;


        //public static bool FilledIn<T>(T      value)                  => !EqualityComparer<T>.Default.Equals(value, default(T));

        //{
        //    if (sizeOfBitDepth.In(ValidSizesOfBitDepth)) return sizeOfBitDepth;
            
        //    if (!Has(sizeOfBitDepth) && !strict)
        //    {
        //        return sizeOfBitDepth;
        //    }
            
        //    throw new Exception($"{nameof(SizeOfBitDepth)} = {sizeOfBitDepth} not supported. Supported values: " + Join(", ", ValidSizesOfBitDepth));
        //}

        //{
        //    if (bits.In(ValidBits)) return bits;
            
        //    if (!Has(bits) && !strict)
        //    {
        //        return bits;
        //    }
            
        //    throw new Exception($"{nameof(Bits)} = {bits} not valid. Supported values: " + Join(", ", ValidBits));
        //}

        //public static int      AssertChannel       (int     channel                     , bool strict = true ) => channel       .In(ValidChannel        ) ? channel        : throw new Exception($"{nameof(ChannelExtensionWishes.Channel)} = {channel} not valid. Supported values: " + Join(", ", ValidChannel));
        //public static int    ? AssertChannel       (int   ? channel                     , bool strict = false) => channel                                 ?.                 AssertChannel();

        //public static string   AssertFileExtension (string  fileExtension               , bool strict = true ) => fileExtension .In(ValidFileExtensions ) || !Has(fileExtension) ? fileExtension : throw new Exception($"{nameof(FileExtension)} = {fileExtension} not valid. Supported values:" + Join(", ", ValidFileExtensions));



        
        //private static int AssertNullOrMinValue(string name, int value, int min, bool strict = true)
        //    => AssertNullOrMinValue(name, (int?)value, min, strict) ?? default;

        //private static int? AssertNullOrMinValue(string name, int? value, int min, bool strict = true)
        //{
        //    if (!strict && value == null) return value;
        //    if (value < min) throw new Exception($"{name} {value} below {min}.");
        //    return value;
        //}
        
        //private static int AssertNullyMinValue(string name, int value, int min, bool strict = true)
        //    => AssertNullyMinValue(name, (int?)value, min, strict) ?? default;

        //private static int? AssertNullyMinValue(string name, int? value, int min, bool strict = true)
        //{
        //    if (!strict && !Has(value)) return value;
        //    if (value < min) throw new Exception($"{name} {value} below {min}.");
        //    return value;
        //}

        //private static double AssertNullOrMinValue(string name, double value, double min, bool strict = true)
        //    => AssertNullOrMinValue(name, (double?)value, min, strict) ?? default;

        //private static double? AssertNullOrMinValue(string name, double? value, double min, bool strict = true)
        //{
        //    if (!strict && value == null) return value;
        //    if (value < min) throw new Exception($"{name} {value} below {min}.");
        //    return value;
        //}
        
        //private static double AssertNullyMin(string name, double value, double min, bool strict = true)
        //    => AssertNullyMin(name, (double?)value, min, strict) ?? default;

        //private static double? AssertNullyMin(string name, double? value, double min, bool strict = true)
        //{
        //    if (!strict && !Has(value)) return value;
        //    if (value < min) throw new Exception($"{name} {value} below {min}.");
        //    return value;
        //}
        

        //public static int      AssertSamplingRate  (int     samplingRate  , bool strict = true ) => samplingRate        > 0 ? samplingRate   : throw new Exception($"{nameof(SamplingRate)} {samplingRate} below 0.");
        //public static int    ? AssertSamplingRate  (int   ? samplingRate  , bool strict = false) => !Has(samplingRate)      ? samplingRate   : AssertSamplingRate(samplingRate.Value);        //public static double  AssertAudioLength    (double  audioLength   , bool strict = true ) => audioLength        >= 0 ? audioLength    : throw new Exception($"{nameof(AudioLength)} {audioLength} below 0.");
        //public static double? AssertAudioLength    (double? audioLength   , bool strict = false) => !Has(audioLength)       ? audioLength    : AssertAudioLength(audioLength.Value);
        //public static int     AssertFrameCount     (int     frameCount    , bool strict = true ) => frameCount         >= 0 ? frameCount     : throw new Exception($"{nameof(FrameCount)} {frameCount} below 0.");
        //public static int?    AssertFrameCount     (int   ? frameCount    , bool strict = false) => !Has(frameCount)        ? frameCount     : AssertFrameCount(frameCount.Value);
        //public static int     AssertByteCount      (int     byteCount     , bool strict = true ) => byteCount          >= 0 ? byteCount      : throw new Exception($"{nameof(ByteCount)} {byteCount} below 0.");
        //public static int?    AssertByteCount      (int   ? byteCount     , bool strict = false) => byteCount               ?.                 AssertByteCount();


            if (!exists)
            {
                throw new Exception($"File not found: {filePath}");
            }



        // Old

        //public static int AssertFrameCountMinusCourtesyFrames(int frameCount, int courtesyFrames)
        //{
        //    AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames);
        //    return frameCount - courtesyFrames;
        //}

        //public static int? AssertFrameCountMinusCourtesyFrames(int? frameCount, int courtesyFrames)
        //{
        //    AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames);
        //    return frameCount - courtesyFrames;
        //}

        // Old 

        //public static void AssertFrameCountWithCourtesyFrames(int? frameCount, int courtesyFrames)
        //{
        //    if (frameCount == null) return;
        //    AssertFrameCountWithCourtesyFrames(frameCount.Value, courtesyFrames);
        //}        

        //public static void AssertFrameCountWithCourtesyFrames(int frameCount, int courtesyFrames)
        //{
        //    AssertFrameCount(frameCount);
        //    AssertCourtesyFrames(courtesyFrames);

        //    if (frameCount < courtesyFrames)
        //    {
        //        throw new Exception(
        //            $"{nameof(FrameCountExtensionWishes.FrameCount)} = {frameCount} " +
        //            $"but should be a minimum of {courtesyFrames} {nameof(CourtesyFrames)}.");
        //    }
        //}

