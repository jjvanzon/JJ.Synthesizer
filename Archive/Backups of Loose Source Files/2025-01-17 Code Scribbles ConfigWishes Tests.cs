Code Scribbles ConfigWishes testing

static IEnumerable<object[]> TestParametersInit => new[]
{
    new object[] { (double?) null },
    new object[] { (double?) 0 },
    new object[] { (double?) 1.6 },
    new object[] { (double?) 2 },
    new object[] { (double?) E },
    new object[] { (double?) PI },
    new object[] { (double?) 5 },
    new object[] { (double?) 17 }
};

sample = sample; audioLength = audioLength; // Tame ReSharper

LogTitle(GetText(() => sample.AudioLength()));
var title = $"{nameof(Sample)} {nameof(AudioLength)}";

        
private double GetToleranceByPercent(double value, double percent)
{
    return value * (1 + percent) - 1;
}

return value * (1 + _tolerancePercent / 100) - value;

private const double _tolerance = 0.007;

x = x; audioLength = audioLength; // Tame ReSharper

throw new Exception($"{nameof(frameCount)} - {nameof(courtesyFrames)} < 0: {new { frameCount, courtesyFrames }}");
throw new Exception($"{nameof(frameCount)} should be a minimum of {courtesyFrames} {nameof(courtesyFrames)}. {nameof(frameCount)} = {frameCount}");

new object[] { 0 },
new object[] { null },
new object[] { 1 },

new object[] {   22050 ,     0 },
new object[] {       0 , 11025 },

new object[] { 48000, 1 },
new object[] { 1, 48000 },

public static int    DefaultByteCount      { get; } = ByteCountFromAudioLength(DefaultAudioLength, DefaultSamplingRate, DefaultBits, DefaultChannels, DefaultAudioFormat, DefaultCourtesyFrames);


public static int ByteCountFromAudioLength(double audioLength, int samplingRate, int bits, int channels, AudioFileFormatEnum audioFormat, int courtesyFrames)
{
    int frameCount = FrameCountFromAudioLength(audioLength, samplingRate, courtesyFrames);
    int frameSize = FrameSize(bits, channels);
    int headerLength = HeaderLength(audioFormat);
    int courtesyBytes = CourtesyBytes(courtesyFrames, bits, channels);
    return frameCount * frameSize + headerLength + courtesyBytes;
}

public int FromFrameCount
{
    get
    {
        if (FromFrameCountNully == FromFrameCountCoalesced) return FromFrameCountCoalesced;
        throw new Exception($"Cannot use {nameof(FromFrameCount)} unless {nameof(FromFrameCountNully)} and {nameof(FromFrameCountCoalesced)} are the same.");
    }
    set { FromFrameCountNully = value; FromFrameCountCoalesced = value; }
}

            
/// <summary>
/// Constructor that only initializes what you supply.
/// </summary>
public Case(
    int?    frameCount     = null,
    int?    samplingRate   = null,
    double? audioLength    = null,
    int?    courtesyFrames = null,
    int?    channels       = null)
{
    if (frameCount     != null) FrameCount     = frameCount.Value;
    if (samplingRate   != null) SamplingRate   = samplingRate.Value;
    if (audioLength    != null) AudioLength    = audioLength.Value;
    if (courtesyFrames != null) CourtesyFrames = courtesyFrames.Value;
    if (channels       != null) Channels       = channels.Value;
}

if (FromFrameCountNully     == FromFrameCountCoalesced &&
    ToFrameCountNully       == ToFrameCountCoalesced &&
    FromFrameCountCoalesced == ToFrameCountCoalesced)
{
 
}


public string Descriptor
{
    get
    {
        string frameCountPart;  
        if (FrameCount != default)
        {
            frameCountPart = $"{FrameCount}";
        }
        else if (FromFrameCount != default && ToFrameCount != default) 
        {
            frameCountPart = $"{FromFrameCount} => {ToFrameCount}";
        }
        else if (FrameCountNully != default && FrameCountCoalesced != default)
        {
            frameCountPart = $"({FrameCountNully},{FrameCountCoalesced})";
        }
        else
        {
            frameCountPart = $"({FromFrameCountNully},{FromFrameCountCoalesced}) => ({ToFrameCountNully},{ToFrameCountCoalesced})";
        }

        string samplingRatePart;
        if (SamplingRate != default)
        {
            samplingRatePart = $"{SamplingRate}";
        }
        else if (FromSamplingRate != default && ToSamplingRate != default)
        {
            samplingRatePart = $"{FromSamplingRate} => {ToSamplingRate}";
        }
        else if (SamplingRateNully != default && SamplingRateCoalesced != default)
        {
            samplingRatePart = $"({SamplingRateNully},{SamplingRateCoalesced})";
        }
        else
        {
            samplingRatePart = $"({FromSamplingRateNully},{FromSamplingRateCoalesced}) => ({ToSamplingRateNully},{ToSamplingRateCoalesced})";
        }
        
        return $"{frameCountPart} ({samplingRatePart})";
    }
}

static IEnumerable<object[]> TestParametersInit => new[]
{
    new object[] { 96000 },
    new object[] { 88200 },
    new object[] { 48000 },
    new object[] { 44100 },
    new object[] { 22050 },
    new object[] { 11025 },
    new object[] { 8 },
    new object[] { 16 },
    new object[] { 19 },
    new object[] { 31 },
    new object[] { 61 },
    new object[] { 100 },
    new object[] { 1000 },
    new object[] { 12345 },
    new object[] { 1234567 }
};


private string GetDescriptorPart(double? fromNully, double fromCoalesced, double? toNully, double toCoalesced)
{
    double  from      = fromNully     == fromCoalesced ? fromCoalesced : default;
    double  to        = toNully       == toCoalesced   ? toCoalesced   : default;
    double? nully     = fromNully     == toNully       ? toNully       : default;
    double  coalesced = fromCoalesced == toCoalesced   ? toCoalesced   : default;
    double  value     = from          == to            ? to            : default;
    
    if (value != default)
    {
        return $"{value}";
    }
    else if (from != default && to != default)
    {
        return $"{from} => {to}";
    }
    else if (nully != default && coalesced != default)
    {
        return $"({nully},{coalesced})";
    }
    else if (fromNully == default  && fromCoalesced == default && toNully == default && toCoalesced == default)
    {
        return default;
    }
    else
    {
        return $"({fromNully},{fromCoalesced}) => ({toNully},{toCoalesced})";
    }
}

/// <summary>
/// Constructor that initializes main property.
/// </summary>

//private bool IsNully(T value) => Equals(value, default(T));
private bool IsNully(T? value) => Equals(value, default(T?)) || Equals(value, default(T));


/// <summary>
/// Constructor that initializes dependencies to practical defaults for testing.
/// (Not exactly the same as the defaults, and a bit of variation in numbers.)
/// </summary>

static IEnumerable<object[]> TestParametersWithEmpties => new[]
{
    new object[] { 1234567 ,  null },
    new object[] {    null , 12345 },
    
}.Concat(TestParameters);

[DynamicData(nameof(TestParameters))]
public void TapeBound_FrameCount(int init, int value)


static IEnumerable<object[]> TestParameters => new[] 
{
    new object[] { 48000, 96000 },
    new object[] { 48000, 88200 },
    new object[] { 48000, 48000 },
    new object[] { 48000, 44100 },
    new object[] { 48000, 22050 },
    new object[] { 48000, 11025 },
    new object[] { 48000, 8 },
    new object[] { 96000, 48000 },
    new object[] { 88200, 44100 },
    new object[] { 44100, 48000 },
    new object[] { 22050, 44100 },
    new object[] { 11025, 44100 },
    new object[] { 8, 48000 },
    new object[] { 48000, 16 },
    new object[] { 48000, 19 },
    new object[] { 48000, 31 },
    new object[] { 48000, 61 },
    new object[] { 48000, 100 },
    new object[] { 48000, 1000 },
    new object[] { 48000, 12345 },
    new object[] { 48000, 1234567 },
};

public int? Nully     { get => FrameCount.Nully    ; set => FrameCount.Nully     = value; }
public int  Coalesced { get => FrameCount.Coalesced; set => FrameCount.Coalesced = value; }

            
public T Value
{
    get
    {
        if (Equals(From.Nully    , To.Nully      ) &&
            Equals(From.Coalesced, To.Coalesced  ) &&
            Equals(From.Nully    , From.Coalesced) &&
            Equals(To.Nully      , To.Coalesced  ))
        {
            return To.Coalesced;
        }
        else
        {
            return default;
        }
    }
    set => From = To = value;
}

[TestMethod]
public void Test_AssertWishes_AreEqual_WithNegativeValues()
{
    int[] deltas = { -2, -1, 0, 1, 2 };
    int[] expectations = { -10, -5, 0, 5, 10 };

    foreach (var delta in deltas)
    {
        foreach (var expected in expectations)
        {
            // Generate cases dynamically
            var actual = expected + delta;
            AreEqual(expected, () => actual, delta, Both);
            var actualOverrun = expected + delta + 1;
            ThrowsException(() => AreEqual(expected, () => actualOverrun, delta, Both));
        }
    }
}


[TestMethod]
public void Test_AssertWishes_AreEqual_OfInt_DeltaDirection_Down()
{
                            AreEqual(10, () =>  8, delta: -2, Down);
                            AreEqual(10, () =>  9, delta: -2, Down); 
                            AreEqual(10, () => 10, delta: -2, Down); 
    ThrowsException(() => AreEqual(10, () => 11, delta: -2, Down));
    ThrowsException(() => AreEqual(10, () => 12, delta: -2, Down));

    ThrowsException(() => AreEqual(10, () =>  8, delta: -1, Down));
                            AreEqual(10, () =>  9, delta: -1, Down); 
                            AreEqual(10, () => 10, delta: -1, Down); 
    ThrowsException(() => AreEqual(10, () => 11, delta: -1, Down));
    ThrowsException(() => AreEqual(10, () => 12, delta: -1, Down));

    ThrowsException(() => AreEqual(10, () =>  8, delta: 0, Down));
    ThrowsException(() => AreEqual(10, () =>  9, delta: 0, Down)); 
                            AreEqual(10, () => 10, delta: 0, Down); 
    ThrowsException(() => AreEqual(10, () => 11, delta: 0, Down));
    ThrowsException(() => AreEqual(10, () => 12, delta: 0, Down));

    ThrowsException(() => AreEqual(10, () =>  8, delta: 1, Down));
                            AreEqual(10, () =>  9, delta: 1, Down); 
                            AreEqual(10, () => 10, delta: 1, Down); 
    ThrowsException(() => AreEqual(10, () => 11, delta: 1, Down));
    ThrowsException(() => AreEqual(10, () => 12, delta: 1, Down));

                            AreEqual(10, () =>  8, delta: 2, Down);
                            AreEqual(10, () =>  9, delta: 2, Down); 
                            AreEqual(10, () => 10, delta: 2, Down); 
    ThrowsException(() => AreEqual(10, () => 11, delta: 2, Down));
    ThrowsException(() => AreEqual(10, () => 12, delta: 2, Down));
}

        
private static int Coalesce(int? frameCount)
{
    int defaultValue = 1 /*sec*/ * 10 /*Hz*/ + 2 /*CourtesyFrames*/;
    return CoalesceFrameCount(frameCount, defaultValue);
}

From.Coalesced = Coalesce(from); 
To.Coalesced = Coalesce(to);


new SynthWishesAccessor(x).Config._section = CreateConfigSectionWithDefaults();

entities.SynthBound.ConfigResolver._section = TestEntities.CreateConfigSectionWithDefaults();

public ConfigSectionAccessor  ConfigSection 
{ 
    get => ConfigResolver._section;
    set => ConfigResolver._section = value;
}


internal static string DebuggerDisplay(TapeEntities.IndependentEntities obj)
{
    string audioFormatDescriptor = AudioFormatDescriptor(samplingRate: obj.Sample.SamplingRate, bits: obj.Sample.Bits, channelCount: obj.Sample.ChannelCount, channel: obj.Sample.Channel, audioFormat: obj.Sample.AudioFormat, interpolation: obj.Sample.Interpolation);
    ConfigLog(audioFormatDescriptor, DurationsDescriptor())
}

public static bool operator ==(NullyPair<T> a, NullyPair<T> b) => Equals(a?.Nully, b?.Nully) && Equals(a?.Coalesced, b?.Coalesced);
public static bool operator !=(NullyPair<T> a, NullyPair<T> b) => !Equals(a?.Nully, b?.Nully) || !Equals(a?.Coalesced, b?.Coalesced);

new Case(1102+3,  4410+3) { Hertz = { From = 48000, To = (int)(48000*(4410/1102.0)) }, PlusFrames = 3 },

//= Cases.Create(/*audioLengthFrom: 0.1,*/ courtesyFrames: 3, cases:



// Before Case template cloning, allowing reuse of shared property initialization:

new Case(4800+3,  4800+3) { sec = { From = 0.1, To =  4800.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,  9600+3) { sec = { From = 0.1, To =  9600.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,  8820+3) { sec = { From = 0.1, To =  8820.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,  4410+3) { sec = { From = 0.1, To =  4410.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,  2205+3) { sec = { From = 0.1, To =  2205.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,  1102+3) { sec = { From = 0.1, To =  1102.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,     8+3) { sec = { From = 0.1, To =     8.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,    16+3) { sec = { From = 0.1, To =    16.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,    19+3) { sec = { From = 0.1, To =    19.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,    31+3) { sec = { From = 0.1, To =    31.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,    61+3) { sec = { From = 0.1, To =    61.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,   100+3) { sec = { From = 0.1, To =   100.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,  1000+3) { sec = { From = 0.1, To =  1000.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,  1234+3) { sec = { From = 0.1, To =  1234.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3, 12345+3) { sec = { From = 0.1, To = 12345.0 / DefaultHz }, PlusFrames = 3 },
new Case(   8+3,  4800+3) { sec = { From = 0.1, To =  4800.0 / DefaultHz }, PlusFrames = 3 },
new Case(1102+3,  4410+3) { sec = { From = 0.1, To =  4410.0 / DefaultHz }, PlusFrames = 3 },
new Case(2205+3,  4410+3) { sec = { From = 0.1, To =  4410.0 / DefaultHz }, PlusFrames = 3 },
new Case(4410+3,  4800+3) { sec = { From = 0.1, To =  4800.0 / DefaultHz }, PlusFrames = 3 },
new Case(8820+3,  4410+3) { sec = { From = 0.1, To =  4410.0 / DefaultHz }, PlusFrames = 3 },
new Case(9600+3,  4800+3) { sec = { From = 0.1, To =  4800.0 / DefaultHz }, PlusFrames = 3 },
new Case(4800+3,  4800+3) { Hertz = { From = 48000, To =  48000 }, PlusFrames = 3 },
new Case(4800+3,  9600+3) { Hertz = { From = 48000, To =  96000 }, PlusFrames = 3 },
new Case(4800+3,  8820+3) { Hertz = { From = 48000, To =  88200 }, PlusFrames = 3 },
new Case(4800+3,  4410+3) { Hertz = { From = 48000, To =  44100 }, PlusFrames = 3 },
new Case(4800+3,  2205+3) { Hertz = { From = 48000, To =  22050 }, PlusFrames = 3 },
new Case(4800+3,  1102+3) { Hertz = { From = 48000, To =  11020 }, PlusFrames = 3 },
new Case(4800+3,     8+3) { Hertz = { From = 48000, To =     80 }, PlusFrames = 3 },
new Case(4800+3,    16+3) { Hertz = { From = 48000, To =    160 }, PlusFrames = 3 },
new Case(4800+3,    19+3) { Hertz = { From = 48000, To =    190 }, PlusFrames = 3 },
new Case(4800+3,    31+3) { Hertz = { From = 48000, To =    310 }, PlusFrames = 3 },
new Case(4800+3,    61+3) { Hertz = { From = 48000, To =    610 }, PlusFrames = 3 },
new Case(4800+3,   100+3) { Hertz = { From = 48000, To =   1000 }, PlusFrames = 3 },
new Case(4800+3,  1000+3) { Hertz = { From = 48000, To =  10000 }, PlusFrames = 3 },
new Case(4800+3,  1234+3) { Hertz = { From = 48000, To =  12340 }, PlusFrames = 3 },
new Case(4800+3, 12345+3) { Hertz = { From = 48000, To = 123450 }, PlusFrames = 3 },
new Case(   8+3,  4800+3) { Hertz = { From =     8, To =   4800 }, PlusFrames = 3 },
new Case(1102+3,  4410+3) { Hertz = { From =  1102, To =   4410 }, PlusFrames = 3 },
new Case(2205+3,  4410+3) { Hertz = { From = 48000, To =  96000 }, PlusFrames = 3 },
new Case(4410+3,  4800+3) { Hertz = { From =  4410, To =   4800 }, PlusFrames = 3 },
new Case(8820+3,  4410+3) { Hertz = { From = 48000, To =  24000 }, PlusFrames = 3 },
new Case(9600+3,  4800+3) { Hertz = { From = 48000, To =  24000 }, PlusFrames = 3 },


// Couldn't get this to work,
// probably something to do with implicit conversions making it ambiguous which type you're evaluating,
// but I went fore another option:

internal class Cases
{
    public static Case[] Create(
        double? audioLengthFrom = default, 
        int? courtesyFrames = default, params Case[] cases)
    {
        if (cases == null) throw new NullException(() => cases);
        
        for (var i = 0; i < cases.Length; i++)
        {
            if (cases[i] == null) throw new NullException(() => cases[i]);
            
            Case testCase = cases[i];
            
            //LogAction(testCase, "Initialize", testCase.Descriptor);
            
            if (Has(audioLengthFrom))
            {
                testCase.AudioLength.From = audioLengthFrom;
            }
            if (courtesyFrames != default)
            {
                testCase.PlusFrames = courtesyFrames;
            }
            
            //LogAction(testCase, "Initialized", testCase.Descriptor);
            
            //if (Has(audioLengthFrom) && !Has(testCase.AudioLength.From.Nully))
            //{
            //    testCase.AudioLength.From.Nully = audioLengthFrom;
            //}
            //if (Has(courtesyFrames) && !Has(testCase.CourtesyFrames.From.Nully))
            //{
            //    testCase.CourtesyFrames.From.Nully = courtesyFrames;
            //}
            //if (Has(courtesyFrames) && !Has(testCase.CourtesyFrames.To.Coalesced))
            //{
            //    testCase.CourtesyFrames.To.Coalesced = courtesyFrames.Value;
            //}
        }
        
        return cases;
    }
}

// Channels: AudioLength vs FrameCount is invariant under Channels, but accidentally involved in formulas.


//double audioLengthBefore = x.AudioLength();
//double audioLengthAfter = x.AudioLength();

//if (Math.Abs(audioLengthBefore - audioLengthAfter) > 0.000000000000001)
//{
    //throw new Exception(
    //    $"Attempt to initialize AudioLength to {audioLengthBefore} " +
    //    $"is inconsistent with AudioLength {audioLengthAfter} "     +
    //    $"based on initial values for FrameCount ({CoalesceFrameCount(testCase.FrameCount.Init)}), " +
    //    $"SamplingRate ({CoalesceSamplingRate(testCase.SamplingRate.Init)}) " +
    //    $"and CourtesyFrames ({CoalesceCourtesyFrames(testCase.CourtesyFrames.Init)}).");

    // TODO: General Coalesce does use incorporate Synth-set values, just the Default.
    // Can I make a coalesce in synth that does it for us, so without having to set the actual value.
    // Actually, I could just get the value.
    // But this would hide the defaulting behavior.
        
    //throw new Exception(
    //    $"Attempt to initialize AudioLength to {audioLengthBefore} " +
    //    $"is inconsistent with AudioLength {audioLengthAfter} "     +
    //    $"based on initial values for FrameCount ({Coalesce(testCase.FrameCount.Init.Nully, "default")}), " +
    //    $"SamplingRate ({x.GetSamplingRate}) " +
    //    $"and CourtesyFrames ({x.GetCourtesyFrames}).");
        
    //throw new Exception(
    //    $"Attempt to initialize FrameCount to {frameCountAfter} " +
    //    $"is inconsistent with FrameCount {frameCountBefore} "     +
    //    $"based on initial values for AudioLength ({Coalesce(testCase.AudioLength.Init.Nully, "default " + DefaultAudioLength )}), " +
    //    $"SamplingRate ({Coalesce(testCase.SamplingRate.Init.Nully, "default " + DefaultSamplingRate)}) " +
    //    $"and CourtesyFrames ({Coalesce(testCase.CourtesyFrames.Init.Nully, "default " + DefaultCourtesyFrames)}).");
//}


// These should fail
//new Case ( from: (null,4800+3), to: 4800+3 ) { Hz = 4800, sec = 0.1 },
//new Case ( from: 4800+3, to: (null,4800+3) ) { Hz = 4800, sec = 0.1 },

// Inconsistent initialization: null = default = 1 s, not 0.1 second
//new Case ( from: (null,4800+3), to: 4800+3 ) { Hz = 48000, sec = 0.1 },
//new Case ( from: 4800+3, to: (null,4800+3) ) { Hz = 48000, sec = 0.1 },

// TODO: Replace inheritance with composition so FrameCount can be initialized same as other properties.
// Case can then delegate to CaseProp to semi-inherit its members.


// With explicit AudioLength (1s)
//new Case { From = (null,4803), To = 4803 , Hz = 4800, sec = 1 },
//new Case { From = 4803, To = (null,4803) , Hz = 4800, sec = 1 },

// Irrelevant ByteCount Init test entities

    //entities.SynthBound.SynthWishesAccessor.ByteCount();
    //entities.SynthBound.Context            .ByteCount();
    //entities.Independent.AudioInfoWish     .ByteCount();
    //entities.Independent.AudioFileInfo     .ByteCount();
    //entities.Immutable.SampleDataTypeEnum  .ByteCount();
    //entities.Immutable.SampleDataType      .ByteCount();
    //entities.Immutable.SamplingRate        .ByteCount();
    //entities.Immutable.Channels            .ByteCount();
    //entities.Immutable.SpeakerSetupEnum    .ByteCount();
    //entities.Immutable.SpeakerSetup        .ByteCount();
    //entities.Immutable.Channel             .ByteCount();
    //entities.Immutable.ChannelEnum         .ByteCount();
    //entities.Immutable.ChannelEntity       .ByteCount();
    //entities.Immutable.Interpolation       .ByteCount();
    //entities.Immutable.InterpolationEntity .ByteCount();
    //entities.Immutable.AudioFormat         .ByteCount();
    //entities.Immutable.AudioFormatEntity   .ByteCount();
    //entities.Immutable.FileExtension       .ByteCount();
    //entities.Immutable.CourtesyFrames      .ByteCount();
    //entities.Immutable.FrameSize           .ByteCount();
    //channelEntities.Independent.AudioInfoWish     .ByteCount();
    //channelEntities.Independent.AudioFileInfo     .ByteCount();
    //channelEntities.Immutable.SampleDataTypeEnum  .ByteCount();
    //channelEntities.Immutable.SampleDataType      .ByteCount();
    //channelEntities.Immutable.SamplingRate        .ByteCount();
    //channelEntities.Immutable.Channels            .ByteCount();
    //channelEntities.Immutable.SpeakerSetupEnum    .ByteCount();
    //channelEntities.Immutable.SpeakerSetup        .ByteCount();
    //channelEntities.Immutable.Channel             .ByteCount();
    //channelEntities.Immutable.ChannelEnum         .ByteCount();
    //channelEntities.Immutable.ChannelEntity       .ByteCount();
    //channelEntities.Immutable.Interpolation       .ByteCount();
    //channelEntities.Immutable.InterpolationEntity .ByteCount();
    //channelEntities.Immutable.AudioFormat         .ByteCount();
    //channelEntities.Immutable.AudioFormatEntity   .ByteCount();
    //channelEntities.Immutable.FileExtension       .ByteCount();
    //channelEntities.Immutable.CourtesyFrames      .ByteCount();
    //channelEntities.Immutable.FrameSize           .ByteCount();

ByteCount_Basic_Setters test:

    // TODO: No setter yet
    //entities.SynthBound.ConfigSection.ByteCount(value));
    // TODO: Sample is too buff
    //entities.Independent.Sample.ByteCount(value);
    // TODO: Sample is too buff
    //channelEntities.Independent.Sample.ByteCount(value);



FrameCountWishesTest: Refactor out repetition of local func code.

    //void AssertProp(Action setter, int value1, int value2, int value3)
    //{
    //    x = CreateTestEntities(testCase);
    //    Assert_All_Getters(x, init);
        
    //    setter();
        
    //    Assert_Bound_Getters(x, init);
        
    //    Assert_Independent_Getters(x.Independent.AudioFileInfo, value1);
    //    Assert_Independent_Getters(x.Independent.AudioInfoWish, value2);
    //    Assert_Independent_Getters(x.Independent.Sample, value3);
        
    //    Assert_Immutable_Getters(x, init);

    //    x.Record();
    //    Assert_All_Getters(x, init);
    //}

    //void AssertProp(Action setter)
    //{
    //    x = CreateTestEntities(testCase);
    //    Assert_All_Getters(x, init);
        
    //    setter();
        
    //    Assert_Bound_Getters(x, init);
    //    Assert_Independent_Getters(x.Independent.AudioInfoWish, value);
    //    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
    //    Assert_Independent_Getters(x.Independent.Sample, init);
    //    Assert_Immutable_Getters(x, init);

    //    x.Record();
    //    Assert_All_Getters(x, init);
    //}


    //void AssertProp(Action setter)
    //{
    //    x = CreateTestEntities(testCase);
    //    Assert_All_Getters(x, init);
        
    //    setter();
        
    //    Assert_Bound_Getters(x, init);
        
    //    Assert_Independent_Getters(x.Independent.AudioFileInfo, value);
    //    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
    //    Assert_Independent_Getters(x.Independent.Sample, init);
        
    //    Assert_Immutable_Getters(x, init);

    //    x.Record();
    //    Assert_All_Getters(x, init);
    //}

        {
            // Undefined is a valid enum, but bits = 0 is not a valid non-nullable bits value, so prevent error here.
            //if (newByteSize == 0) return SampleDataTypeEnum.Undefined; 
            return newByteSize.Bits().BitsToEnum();
        }

