// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global
#pragma warning disable IDE0001 // Simplify Names
#pragma warning disable IDE1006 // Naming Styles

namespace JJ.Business.Synthesizer.Tests.Accessors;

[DebuggerDisplay("{DebuggerDisplay}")]
internal class ConfigResolverAccessor(object obj) : AccessorCore(obj)
{
    private static readonly AccessorCore _staticAccessor = new ("ConfigResolver");
    
    public override bool Equals(object obj)
    {
        if (obj == null) return Obj == null;
        if (obj is ConfigResolverAccessor other) return Obj == other.Obj;
        return false;
    }
    
    public string DebuggerDisplay => Get<string>();
    
    public static ConfigResolverAccessor Static => new (_staticAccessor.Get());

    public ConfigSectionAccessor _section
    {
        get => new (Get());
        set => Set(value.Obj);
    }
    
    // Bits

    public bool Is8Bit  => (bool)Get();
    public bool Is16Bit => (bool)Get();
    public bool Is32Bit => (bool)Get();
    public int  GetBits => (int )Get();
    public int? _bits { get => (int?)Get(); set => Set(value); }

    public ConfigResolverAccessor WithBits(int? value) => new (Call(value));
    
    // Channels
    
    public int  NoChannels      => (int) Get();
    public int  MonoChannels    => (int) Get();
    public int  StereoChannels  => (int) Get();
    public bool IsMono          => (bool)Get();
    public bool IsStereo        => (bool)Get();
    public int  GetChannels     => (int) Get();
    public int? _channels { get => (int?)Get(); set => Set(value); }
    
    public ConfigResolverAccessor WithChannels(int? channels) => new(Call(channels));
            
    // Channel
    
    public int  CenterChannel  => (int) Get();
    public int  LeftChannel    => (int) Get();
    public int  RightChannel   => (int) Get();
    public int? EmptyChannel   => (int?)Get();
    public bool IsCenter       => (bool)Get();
    public bool IsLeft         => (bool)Get();
    public bool IsRight        => (bool)Get();
    public bool IsAnyChannel   => (bool)Get();
    public bool IsEveryChannel => (bool)Get();
    public bool IsNoChannel    => (bool)Get();
    public int? GetChannel     => (int?)Get();
    public int _channel  { get => (int) Get(); set => Set(value); }

    public ConfigResolverAccessor WithCenter   () => new(Call());
    public ConfigResolverAccessor WithLeft     () => new(Call());
    public ConfigResolverAccessor WithRight    () => new(Call());
    public ConfigResolverAccessor WithNoChannel() => new(Call());
    public ConfigResolverAccessor WithChannel(int? channels) => new (Call(channels));
    
    // SamplingRate
    
    /// <inheritdoc cref="_getsamplingrate" />
    public int? _samplingRate { get => (int?)Get(); set => Set(value); }
    
    /// <inheritdoc cref="_getsamplingrate" />
    public ConfigResolverAccessor WithSamplingRate(int? value) => new (Call(value));
    
    /// <inheritdoc cref="_getsamplingrate" />
    public int GetSamplingRate => (int)Get();
    
    public int ResolveSamplingRate() => (int)Call();

    // AudioFormat

    public bool IsRaw => (bool)Get();
    public bool IsWav => (bool)Get();

    public AudioFileFormatEnum? _audioFormat { get => Get(() => _audioFormat); set => Set(value); }

    public AudioFileFormatEnum GetAudioFormat => Get(() => GetAudioFormat);
    
    public ConfigResolverAccessor WithAudioFormat(AudioFileFormatEnum? value) => new (Call(value));

    // Interpolation
    
    public bool IsLinear => (bool)Get();
    public bool IsBlocky => (bool)Get();
    public InterpolationTypeEnum? _interpolation { get => Get(() => _interpolation); set => Set(value); }
    public InterpolationTypeEnum GetInterpolation => Get(() => GetInterpolation);
    public ConfigResolverAccessor WithLinear() => new(Call());
    public ConfigResolverAccessor WithBlocky() => new(Call());
    public ConfigResolverAccessor WithInterpolation(InterpolationTypeEnum? interpolation) => new (Call(interpolation));
    
    // NoteLength
    
    /// <inheritdoc cref="_notelength" />
    public FlowNode _noteLength { get => (FlowNode)Get(); set => Set(value); }

    /// <inheritdoc cref="_notelength" />
    public FlowNode GetNoteLength(SynthWishes synthWishes)
        => (FlowNode)Call(synthWishes);

    /// <inheritdoc cref="_notelength" />
    public FlowNode GetNoteLength(SynthWishes synthWishes, FlowNode noteLength) 
        => (FlowNode)Call(synthWishes, noteLength);

    /// <inheritdoc cref="_notelength" />
    public ConfigResolverAccessor WithNoteLength(FlowNode noteLength) 
        => new (Call(noteLength));

    /// <inheritdoc cref="_notelength" />
    public ConfigResolverAccessor WithNoteLength(double noteLength, SynthWishes synthWishes) 
        => new (Call(noteLength, synthWishes));
    
    /// <inheritdoc cref="_notelength" />
    public ConfigResolverAccessor ResetNoteLength()             
        => new (Call());

    /// <inheritdoc cref="_notelength" />
    public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time, int channel)
        => (FlowNode)Call(synthWishes, noteLength, time, channel);
    
    /// <inheritdoc cref="_notelength" />
    public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes) 
        => (FlowNode)Call(synthWishes);
    
    /// <inheritdoc cref="_notelength" />
    public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time) 
        => (FlowNode)Call(synthWishes, time);
    
    /// <inheritdoc cref="_notelength" />
    public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time, int channel)
        => (FlowNode)Call(synthWishes, time, channel);
    
    /// <inheritdoc cref="_notelength" />
    public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength) 
        => (FlowNode)Call(synthWishes, noteLength);
    
    /// <inheritdoc cref="_notelength" />
    public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time) 
        => (FlowNode)Call(synthWishes, noteLength, time);

    // BarLength

    public FlowNode _barLength { get => (FlowNode)Get(); set => Set(value); }

    public FlowNode GetBarLength(SynthWishes synthWishes) 
        => (FlowNode)Call(synthWishes);
    
    public ConfigResolverAccessor WithBarLength(FlowNode barLength)
        => new (Call(barLength));
    
    public ConfigResolverAccessor WithBarLength(double barLength, SynthWishes synthWishes)
        => new (Call(barLength, synthWishes));

    public ConfigResolverAccessor ResetBarLength()
        => new (Call());

    // BeatLength

    public FlowNode _beatLength { get => (FlowNode)Get(); set => Set(value); }
    
    public FlowNode GetBeatLength(SynthWishes synthWishes) 
        => (FlowNode)Call(synthWishes);
    
    public ConfigResolverAccessor WithBeatLength(FlowNode beatLength)
        => new (Call(beatLength));
    
    public ConfigResolverAccessor WithBeatLength(double beatLength, SynthWishes synthWishes)
        => new (Call(beatLength, synthWishes));
    
    public ConfigResolverAccessor ResetBeatLength() 
        => new (Call());
    
    // Audio Length
    
    /// <inheritdoc cref="_audiolength" />
    public FlowNode _audioLength { get => (FlowNode)Get(); set => Set(value); }
    
    /// <inheritdoc cref="_audiolength" />
    public FlowNode GetAudioLength(SynthWishes synthWishes) 
        => (FlowNode)Call(synthWishes);

    /// <inheritdoc cref="_audiolength" />
    public ConfigResolverAccessor WithAudioLength(FlowNode newAudioLength)
        => new (Call(newAudioLength));

    /// <inheritdoc cref="_audiolength" />
    public ConfigResolverAccessor WithAudioLength(double? newAudioLength, SynthWishes synthWishes)
        => new (Call(newAudioLength, synthWishes));
    
    /// <inheritdoc cref="_audiolength" />
    public ConfigResolverAccessor AddAudioLength(FlowNode additionalLength, SynthWishes synthWishes)
        => new (Call(additionalLength, synthWishes));

    /// <inheritdoc cref="_audiolength" />
    public ConfigResolverAccessor AddAudioLength(double additionalLength, SynthWishes synthWishes)
        => new (Call(additionalLength, synthWishes));

    public ConfigResolverAccessor AddEchoDuration(int count, FlowNode delay, SynthWishes synthWishes)
        => new (Call(count, delay, synthWishes));

    public ConfigResolverAccessor AddEchoDuration(int count, double delay, SynthWishes synthWishes)
        => new (Call(count, delay, synthWishes));

    /// <inheritdoc cref="_audiolength" />
    public ConfigResolverAccessor EnsureAudioLength(FlowNode audioLengthNeeded, SynthWishes synthWishes)
        => new (Call(audioLengthNeeded, synthWishes));

    /// <inheritdoc cref="_audiolength" />
    public ConfigResolverAccessor EnsureAudioLength(double audioLengthNeeded, SynthWishes synthWishes)
        => new (Call(audioLengthNeeded, synthWishes));
    
    /// <inheritdoc cref="_audiolength" />
    public ConfigResolverAccessor ResetAudioLength()
        => new (Call());
    
    // LeadingSilence
    
    /// <inheritdoc cref="_padding"/>
    public FlowNode _leadingSilence { get => (FlowNode)Get(); set => Set(value); }

    /// <inheritdoc cref="_padding"/>
    public FlowNode GetLeadingSilence(SynthWishes synthWishes) 
        => (FlowNode)Call(synthWishes);

    /// <inheritdoc cref="_padding"/>
    public ConfigResolverAccessor WithLeadingSilence(FlowNode seconds)
        => new (Call(seconds));

    /// <inheritdoc cref="_padding"/>
    public ConfigResolverAccessor WithLeadingSilence(double seconds, SynthWishes synthWishes)
        => new (Call(seconds, synthWishes));

    /// <inheritdoc cref="_padding"/>
    public ConfigResolverAccessor ResetLeadingSilence() 
        => new (Call());
    
    // TrailingSilence
    
    /// <inheritdoc cref="_padding"/>
    public FlowNode _trailingSilence { get => (FlowNode)Get(); set => Set(value);
    }

    /// <inheritdoc cref="_padding"/>
    public FlowNode GetTrailingSilence(SynthWishes synthWishes) 
        => (FlowNode)Call(synthWishes);

    /// <inheritdoc cref="_padding"/>
    public ConfigResolverAccessor WithTrailingSilence(FlowNode seconds) 
        => new (Call(seconds));
    
    /// <inheritdoc cref="_padding"/>
    public ConfigResolverAccessor WithTrailingSilence(double seconds, SynthWishes synthWishes) 
        => new (Call(seconds, synthWishes));

    /// <inheritdoc cref="_padding"/>
    public ConfigResolverAccessor ResetTrailingSilence() 
        => new (Call());

    // Padding

    /// <inheritdoc cref="_padding"/>
    public FlowNode GetPaddingOrNull(SynthWishes synthWishes) 
        => (FlowNode)Call(synthWishes);

    /// <inheritdoc cref="_padding"/>
    public ConfigResolverAccessor WithPadding(FlowNode seconds) 
        => new (Call(seconds));

    /// <inheritdoc cref="_padding"/>
    public ConfigResolverAccessor WithPadding(double seconds, SynthWishes synthWishes) 
        => new (Call(seconds, synthWishes));

    /// <inheritdoc cref="_padding"/>
    public ConfigResolverAccessor ResetPadding() 
        => new (Call());
    
    // AudioPlayback
    
    /// <inheritdoc cref="_audioplayback" />
    public bool? _audioPlayback { get => (bool?)Get(); set => Set(value); }

    /// <inheritdoc cref="_audioplayback" />
    public ConfigResolverAccessor WithAudioPlayback(bool? enabled = true) => new (Call(enabled));
    
    /// <inheritdoc cref="_audioplayback" />
    public bool GetAudioPlayback(string fileExtension = null) => (bool)Call(fileExtension);

    // DiskCache

    /// <inheritdoc cref="_diskcache" />
    public bool? _diskCache { get => (bool?)Get(); set => Set(value); }

    /// <inheritdoc cref="_diskcache" />
    public bool GetDiskCache => Get<bool>();
    
    /// <inheritdoc cref="_diskcache" />
    public ConfigResolverAccessor WithDiskCache(bool? enabled = true) => new (Call(enabled));
    
    // MathBoost
    
    public bool? _mathBoost { get => (bool?)Get(); set => Set(value); }
    
    public bool GetMathBoost => (bool)Get(); 
    
    public ConfigResolverAccessor WithMathBoost(bool? enabled = true) => new (Call(enabled));

    // ParallelProcessing

    /// <inheritdoc cref="_parallelprocessing" />
    public bool? _parallelProcessing { get => (bool?)Get(); set => Set(value); }
    
    /// <inheritdoc cref="_parallelprocessing" />
    public bool GetParallelProcessing => Get<bool>();
    
    /// <inheritdoc cref="_parallelprocessing" />
    public ConfigResolverAccessor WithParallelProcessing(bool? enabled = true) => new (Call(enabled));

    // PlayAllTapes

    /// <inheritdoc cref="_playalltapes" />
    public bool? _playAllTapes { get => (bool)Get(); set => Set(value);
    }
    /// <inheritdoc cref="_playalltapes" />
    public bool GetPlayAllTapes => (bool)Get();
    
    /// <inheritdoc cref="_playalltapes" />
    public ConfigResolverAccessor WithPlayAllTapes(bool? enabled = true) => new (Call(enabled));

    // Misc Settings

    /// <inheritdoc cref="_leafchecktimeout" />
    public double? _leafCheckTimeOut { get => (double?)Get(); set => Set(value); }
    /// <inheritdoc cref="_leafchecktimeout" />
    public double GetLeafCheckTimeOut => Get<double>();
    /// <inheritdoc cref="_leafchecktimeout" />
    public ConfigResolverAccessor WithLeafCheckTimeOut(double? seconds) => new (Call(seconds));

    /// <inheritdoc cref="_leafchecktimeout" />
    public TimeOutActionEnum? _timeOutAction { get => Get(() => _timeOutAction); set => Set(value); }
    /// <inheritdoc cref="_leafchecktimeout" />
    public TimeOutActionEnum GetTimeOutAction => (TimeOutActionEnum)Get();
    /// <inheritdoc cref="_leafchecktimeout" />
    public ConfigResolverAccessor WithTimeOutAction(TimeOutActionEnum? action) => new (Call(action));

    public int? _courtesyFrames { get => (int?)Get(); set => Set(value); }
    public int GetCourtesyFrames => (int)Get();
    public ConfigResolverAccessor WithCourtesyFrames(int? value) => new (Call(value));

    public int GetFileExtensionMaxLength => (int)Get();

    // TODO: Remove outcommented
    // Removing "IsLongTestCategory" feature gets rid of Testing.Core dependency
    //public string _longTestCategory { get => Get<string>(); set => Set(value); }
    //public ConfigResolverAccessor WithLongTestCategory(string category) => new (Call(category));
    //public string GetLongTestCategory => (string)Get();

    // Tooling
    
    public static string NCrunchEnvironmentVariableName         => (string)_staticAccessor.Get();
    public static string NCrunchEnvironmentVariableValue        => (string)_staticAccessor.Get();
    public static string AzurePipelinesEnvironmentVariableValue => (string)_staticAccessor.Get();
    public static string AzurePipelinesEnvironmentVariableName  => (string)_staticAccessor.Get();

    public bool IsUnderNCrunch                   => (bool) Get();
    public bool? GetNCrunchImpersonationMode     => (bool?)Get();
    public bool? _ncrunchImpersonationMode { get => (bool?)Get(); set => Set(value); }
    
    public bool  IsUnderAzurePipelines                  => (bool) Get();
    public bool? GetAzurePipelinesImpersonationMode     => (bool?)Get();
    public bool? _azurePipelinesImpersonationMode { get => (bool?)Get(); set => Set(value); }

    // Persistence

    public static PersistenceConfiguration PersistenceConfigurationOrDefault => (PersistenceConfiguration)_staticAccessor.Get();
    public static PersistenceConfiguration GetDefaultInMemoryConfiguration() => (PersistenceConfiguration)_staticAccessor.Call();

    // Warnings

    public IList<string> GetWarnings(string fileExtension = null) => Call(() => GetWarnings(fileExtension));
}
