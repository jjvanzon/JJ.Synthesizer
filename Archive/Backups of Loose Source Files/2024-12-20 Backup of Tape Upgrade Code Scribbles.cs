Tape Upgrade Code Scribbles:

var channelSignals = audioFileOutput.AudioFileOutputChannels.Select(x => x.Outlet).ToList();

internal Tape CreateDummyTape(
    IList<FlowNode> channelSignals, 
    FlowNode duration, 
    bool inMemory, 
    bool mustPad, 
    string name, 
    string filePath, 
    [CallerMemberName] string callerMemberName = null)
    => new Tape
    {
        Signals = channelSignals,
        Duration = duration ?? GetAudioLength,
        IsSave = !inMemory,
        IsPadding = mustPad,
        FilePath = filePath,
        FallbackName = ResolveName(name, callerMemberName),
        CacheToDisk = GetCacheToDisk,
        ExtraBufferFrames = GetExtraBufferFrames,
        Bits = GetBits,
        SamplingRate = GetSamplingRate,
        AudioFormat = GetAudioFormat
    };


// Help ReSharper not error over unused legacy parameter.
additionalMessages = additionalMessages;


// MakeBuff Other Variations

///// <inheritdoc cref="docs._makebuff" />
//internal void MakeBuffNewThroughOld(Tape tape)
//{
//    // Not yet used.
//    if (tape == null) throw new ArgumentNullException(nameof(tape));
//    
//    var signals = tape.Signals?.ToList() ?? new List<FlowNode>();
//    if (tape.Signal != null) signals.Add(tape.Signal);
//
//    bool inMemory = !tape.IsSave && !tape.IsSaveChannel;
//
//    tape.Buff = MakeBuffOld(signals, tape.Duration, inMemory, tape.IsPadding, null, tape.GetName, tape.FilePath);
//}
//
    
if (synthWishes == null)
{
    if (tape.IsPadded) flags.Add("pad");
}
else
{
    if (tape.LeadingSilence != synthWishes.GetLeadingSilence.Value ||
        tape.TrailingSilence != synthWishes.GetTrailingSilence.Value)
    {
        if (tape.LeadingSilence == tape.TrailingSilence)
        {
            flags.Add($"pad:{tape.LeadingSilence:F2}");
        }
        else
        {
            flags.Add($"pad:{tape.LeadingSilence:F2},{tape.TrailingSilence:F2}");
        }
    }
}


public static IList<string> GetSynthLog(Tape tape, double calculationDuration) => GetSynthLog(tape, null, calculationDuration);
public static IList<string> GetSynthLog(Buff buff, double calculationDuration) => GetSynthLog(null, buff, calculationDuration);

//if (tape.Buff  == null) return new [] { "SynthLog: Warning: No Tape Buff!" };

double realTimePercent = 0;
if (audioDuration != null)
{
    realTimePercent = audioDuration.Value / calculationDuration* 100;
}

//public Tape RecombineChannels((Tape Left, Tape Right) tapePair)
//{
//    AssertTapePair(tapePair);
//    var channelSignals = GetChannelSignals(tapePair);
//    var stereoTape = CloneTape(tapePair.Left);
//    RecordStereoTape(stereoTape, channelSignals);
//    return stereoTape;
//}


string dataDescriptor = default;
if (Has(bytesDescriptor) || Has(filePathDescriptor))
{
    var dataElements = new[] { bytesDescriptor, filePathDescriptor }.Where(FilledIn);
    dataDescriptor = "[ " + Join(" | ", dataElements) + " ]";
}


var sb = new StringBuilder();

sb.Append(typeString);
sb.Append(' ');

if (Has(bytesDescriptor))
{
    sb.Append(bytesDescriptor);
    sb.Append(" | ");
}

if (Has(configLog))
{
    sb.Append(configLog);
}

if (Has(filePathDescriptor))
{
    sb.Append(" | ");
    sb.Append(filePathDescriptor);
}


// HACK: Set channel as we omit Run call that iterates through the channels for us.
// TODO: Make channel control available through this accessor,
// and use it in ComplexityTests.
//WithLeft();


//public static string ResolveFilePath(
//    object filePathSource1,
//    object filePathSource2 = null,
//    object filePathSource3 = null,
//    [CallerMemberName] string callerMemberName = null)
//    => ResolveFilePath(default, default, filePathSource1, filePathSource2, filePathSource3, callerMemberName);

ResolveFilePath:

    //string resolvedFileExtension = ResolveFileExtension(fileExtension, audioFormat, filePathSource1, filePathSource2, filePathSource3, filePathSource4);
    //string resolvedName = ResolveName(filePathSource2, filePathSource3, filePathSource4, callerMemberName, explicitNameSource: filePathSource1);
    //string resolvedFilePath = ReformatFilePath(resolvedName, resolvedFileExtension);
    //return resolvedFilePath;

RunAllTapes:

    // HACK: Override sampling rate with currently resolved sampling rate.
    // (Can be customized for long-running tests,
    // but separate threads cannot check the test category.)
    //var originalSamplingRate = _synthWishes.Config._samplingRate;
    //try
    {
        //_synthWishes.WithSamplingRate(_synthWishes.GetSamplingRate);

    }
    //finally
    //{
    //    _synthWishes.Config._samplingRate = originalSamplingRate;
    //}

Panning:

    //output.SetName(MemberName() + " " + GetChannelDescriptor(GetChannels, GetChannel));
    //output.SetName(MemberName() + " " + GetChannelDescriptor(GetChannels, GetChannel));

AudioFormatTests:

    //return sound.SetName(callerMemberName + " " + GetChannelDescriptor(GetChannels, GetChannel));
