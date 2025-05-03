// ReSharper disable RedundantTypeArgumentsOfMethod
#pragma warning disable IDE0001 // Simplify Names

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class ConfigResolverAccessor(object obj)
    {
        private static readonly AccessorCore _staticAccessor = new ("ConfigResolver");
        private readonly AccessorCore _accessor = new (obj);
        
        public object Obj { get; } = obj;
        
        public override bool Equals(object obj)
        {
            if (obj == null) return Obj == null;
            if (obj is ConfigResolverAccessor other) return Obj == other.Obj;
            return false;
        }
        
        public string DebuggerDisplay => _accessor.Get<string>();
        
        public static ConfigResolverAccessor Static => new (_staticAccessor.Get());

        public ConfigSectionAccessor _section
        {
            get => new (_accessor.Get());
            set => _accessor.Set(value.Obj);
        }
        
        // Bits

        public bool Is8Bit => _accessor.Get<bool>();
        public bool Is16Bit => _accessor.Get<bool>();
        public bool Is32Bit => _accessor.Get<bool>();
        
        public int? _bits
        {
            get => _accessor.Get<int?>();
            set => _accessor.Set(value);
        }

        public int GetBits => _accessor.Get<int>();

        public ConfigResolverAccessor WithBits(int? value) 
            => new (_accessor.Call(value));
        
        // Channels
        
        public int NoChannels => _accessor.Get<int>();
        public int MonoChannels => _accessor.Get<int>();
        public int StereoChannels => _accessor.Get<int>();

        public bool IsMono => _accessor.Get<bool>();
        public bool IsStereo => _accessor.Get<bool>();

        public int? _channels
        {
            get => _accessor.Get<int?>();
            set => _accessor.Set(value);
        }

        public int GetChannels => _accessor.Get<int>();
        
        public ConfigResolverAccessor WithChannels(int? channels) 
            => new (_accessor.Call(channels));
                
        // Channel
        
        public int  CenterChannel  => _accessor.Get<int>();
        public int  LeftChannel    => _accessor.Get<int>();
        public int  RightChannel   => _accessor.Get<int>();
        public int? EmptyChannel   => _accessor.Get<int?>();
        
        public bool IsCenter       => _accessor.Get<bool>();
        public bool IsLeft         => _accessor.Get<bool>();
        public bool IsRight        => _accessor.Get<bool>();
        public bool IsAnyChannel   => _accessor.Get<bool>();
        public bool IsEveryChannel => _accessor.Get<bool>();
        public bool IsNoChannel    => _accessor.Get<bool>();

        public int _channel
        {
            get => _accessor.Get<int>();
            set => _accessor.Set(value);
        }
        
        public int? GetChannel => _accessor.Get<int?>();
        
        public ConfigResolverAccessor WithChannel(int? channels) 
            => new (_accessor.Call(channels));
        
        public ConfigResolverAccessor WithCenter()    => new(_accessor.Call());
        public ConfigResolverAccessor WithLeft()      => new(_accessor.Call());
        public ConfigResolverAccessor WithRight()     => new(_accessor.Call());
        public ConfigResolverAccessor WithNoChannel() => new(_accessor.Call());
        
        // SamplingRate
        
        /// <inheritdoc cref="_getsamplingrate" />
        public int? _samplingRate
        {
            get => _accessor.Get<int?>();
            set => _accessor.Set(value);
        }
        
        /// <inheritdoc cref="_getsamplingrate" />
        public ConfigResolverAccessor WithSamplingRate(int? value) 
            => new (_accessor.Call(value));
        
        /// <inheritdoc cref="_getsamplingrate" />
        public int GetSamplingRate => _accessor.Get<int>();
        
        public int ResolveSamplingRate() => (int)_accessor.Call();

        // AudioFormat

        public bool IsRaw => _accessor.Get<bool>();
        public bool IsWav => _accessor.Get<bool>();

        public AudioFileFormatEnum? _audioFormat
        {
            get => _accessor.Get(() => _audioFormat);
            set => _accessor.Set(() => _audioFormat, value);
        }

        public AudioFileFormatEnum GetAudioFormat 
            => _accessor.Get(() => GetAudioFormat);
        
        public ConfigResolverAccessor WithAudioFormat(AudioFileFormatEnum? value) 
            => new (_accessor.Call(value));

        // Interpolation
        
        public bool IsLinear => _accessor.Get<bool>();
        public bool IsBlocky => _accessor.Get<bool>();
        
        public InterpolationTypeEnum? _interpolation
        {
            get => _accessor.Get(() => _interpolation);
            set => _accessor.Set(() => _interpolation, value);
        }

        public InterpolationTypeEnum GetInterpolation 
            => _accessor.Get(() => GetInterpolation);

        public ConfigResolverAccessor WithLinear() => new(_accessor.Call());
        
        public ConfigResolverAccessor WithBlocky() => new(_accessor.Call());
        
        public ConfigResolverAccessor WithInterpolation(InterpolationTypeEnum? interpolation) 
            => new (_accessor.Call(interpolation));
        
        // NoteLength
        
        /// <inheritdoc cref="_notelength" />
        public FlowNode _noteLength
        {
            get => _accessor.Get<FlowNode>();
            set => _accessor.Set<FlowNode>(value);
        }

        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes) 
            => (FlowNode)_accessor.Call(synthWishes);

        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes, FlowNode noteLength)
            => (FlowNode)_accessor.Call(synthWishes, noteLength);

        /// <inheritdoc cref="_notelength" />
        public ConfigResolverAccessor WithNoteLength(FlowNode noteLength)
            => new (_accessor.Call(noteLength));

        /// <inheritdoc cref="_notelength" />
        public ConfigResolverAccessor WithNoteLength(double noteLength, SynthWishes synthWishes)
            => new (_accessor.Call(noteLength, synthWishes));
        
        /// <inheritdoc cref="_notelength" />
        public ConfigResolverAccessor ResetNoteLength() => new (_accessor.Call());

        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time, int channel)
            => (FlowNode)_accessor.Call(synthWishes, noteLength, time, channel);
        
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes) 
            => (FlowNode)_accessor.Call(synthWishes);
        
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time) 
            => (FlowNode)_accessor.Call(synthWishes, time);
        
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time, int channel)
            => (FlowNode)_accessor.Call(synthWishes, time, channel);
        
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength) 
            => (FlowNode)_accessor.Call(synthWishes, noteLength);
        
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time) 
            => (FlowNode)_accessor.Call(synthWishes, noteLength, time);

        // BarLength

        public FlowNode _barLength
        {
            get => _accessor.Get(() => _barLength);
            set => _accessor.Set(() => _barLength, value);
        }

        public FlowNode GetBarLength(SynthWishes synthWishes) 
            => (FlowNode)_accessor.Call(synthWishes);
        
        public ConfigResolverAccessor WithBarLength(FlowNode barLength)
            => new (_accessor.Call(barLength));
        
        public ConfigResolverAccessor WithBarLength(double barLength, SynthWishes synthWishes)
            => new (_accessor.Call(barLength, synthWishes));

        public ConfigResolverAccessor ResetBarLength()
            => new (_accessor.Call());

        // BeatLength

        public FlowNode _beatLength
        {
            get => _accessor.Get(() => _beatLength);
            set => _accessor.Set(() => _beatLength, value);
        }
        
        public FlowNode GetBeatLength(SynthWishes synthWishes) 
            => (FlowNode)_accessor.Call(synthWishes);
        
        public ConfigResolverAccessor WithBeatLength(FlowNode beatLength)
            => new (_accessor.Call(beatLength));
        
        public ConfigResolverAccessor WithBeatLength(double beatLength, SynthWishes synthWishes)
            => new (_accessor.Call(beatLength, synthWishes));
        
        public ConfigResolverAccessor ResetBeatLength() 
            => new (_accessor.Call());
        
        // Audio Length
        
        /// <inheritdoc cref="_audiolength" />
        public FlowNode _audioLength
        {
            get => _accessor.Get(() => _audioLength);
            set => _accessor.Set(() => _audioLength, value);
        }
        
        /// <inheritdoc cref="_audiolength" />
        public FlowNode GetAudioLength(SynthWishes synthWishes) 
            => (FlowNode)_accessor.Call(synthWishes);

        /// <inheritdoc cref="_audiolength" />
        public ConfigResolverAccessor WithAudioLength(FlowNode newAudioLength)
            => new (_accessor.Call(newAudioLength));

        /// <inheritdoc cref="_audiolength" />
        public ConfigResolverAccessor WithAudioLength(double? newAudioLength, SynthWishes synthWishes)
            => new (_accessor.Call(newAudioLength, synthWishes));
        
        /// <inheritdoc cref="_audiolength" />
        public ConfigResolverAccessor AddAudioLength(FlowNode additionalLength, SynthWishes synthWishes)
            => new (_accessor.Call(additionalLength, synthWishes));

        /// <inheritdoc cref="_audiolength" />
        public ConfigResolverAccessor AddAudioLength(double additionalLength, SynthWishes synthWishes)
            => new (_accessor.Call(additionalLength, synthWishes));

        public ConfigResolverAccessor AddEchoDuration(int count, FlowNode delay, SynthWishes synthWishes)
            => new (_accessor.Call(count, delay, synthWishes));

        public ConfigResolverAccessor AddEchoDuration(int count, double delay, SynthWishes synthWishes)
            => new (_accessor.Call(count, delay, synthWishes));

        /// <inheritdoc cref="_audiolength" />
        public ConfigResolverAccessor EnsureAudioLength(FlowNode audioLengthNeeded, SynthWishes synthWishes)
            => new (_accessor.Call(audioLengthNeeded, synthWishes));

        /// <inheritdoc cref="_audiolength" />
        public ConfigResolverAccessor EnsureAudioLength(double audioLengthNeeded, SynthWishes synthWishes)
            => new (_accessor.Call(audioLengthNeeded, synthWishes));
        
        /// <inheritdoc cref="_audiolength" />
        public ConfigResolverAccessor ResetAudioLength()
            => new (_accessor.Call());
        
        // LeadingSilence
        
        /// <inheritdoc cref="_padding"/>
        public FlowNode _leadingSilence
        {
            get => _accessor.Get(() => _leadingSilence);
            set => _accessor.Set(() => _leadingSilence, value);
        }

        /// <inheritdoc cref="_padding"/>
        public FlowNode GetLeadingSilence(SynthWishes synthWishes) 
            => (FlowNode)_accessor.Call(synthWishes);

        /// <inheritdoc cref="_padding"/>
        public ConfigResolverAccessor WithLeadingSilence(FlowNode seconds)
            => new (_accessor.Call(seconds));

        /// <inheritdoc cref="_padding"/>
        public ConfigResolverAccessor WithLeadingSilence(double seconds, SynthWishes synthWishes)
            => new (_accessor.Call(seconds, synthWishes));

        /// <inheritdoc cref="_padding"/>
        public ConfigResolverAccessor ResetLeadingSilence() 
            => new (_accessor.Call());
        
        // TrailingSilence
        
        /// <inheritdoc cref="_padding"/>
        public FlowNode _trailingSilence
        { 
            get => _accessor.Get<FlowNode>();
            set => _accessor.Set<FlowNode>(value);
        }

        /// <inheritdoc cref="_padding"/>
        public FlowNode GetTrailingSilence(SynthWishes synthWishes) 
            => (FlowNode)_accessor.Call(synthWishes);

        /// <inheritdoc cref="_padding"/>
        public ConfigResolverAccessor WithTrailingSilence(FlowNode seconds) 
            => new (_accessor.Call(seconds));
        
        /// <inheritdoc cref="_padding"/>
        public ConfigResolverAccessor WithTrailingSilence(double seconds, SynthWishes synthWishes) 
            => new (_accessor.Call(seconds, synthWishes));

        /// <inheritdoc cref="_padding"/>
        public ConfigResolverAccessor ResetTrailingSilence() 
            => new (_accessor.Call());

        // Padding

        /// <inheritdoc cref="_padding"/>
        public FlowNode GetPaddingOrNull(SynthWishes synthWishes) 
            => (FlowNode)_accessor.Call(synthWishes);

        /// <inheritdoc cref="_padding"/>
        public ConfigResolverAccessor WithPadding(FlowNode seconds) 
            => new (_accessor.Call(seconds));

        /// <inheritdoc cref="_padding"/>
        public ConfigResolverAccessor WithPadding(double seconds, SynthWishes synthWishes) 
            => new (_accessor.Call(seconds, synthWishes));

        /// <inheritdoc cref="_padding"/>
        public ConfigResolverAccessor ResetPadding() 
            => new (_accessor.Call());
        
        // AudioPlayback
        
        /// <inheritdoc cref="_audioplayback" />
        public bool? _audioPlayback
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }

        /// <inheritdoc cref="_audioplayback" />
        public ConfigResolverAccessor WithAudioPlayback(bool? enabled = true) 
            => new (_accessor.Call(enabled));
        
        /// <inheritdoc cref="_audioplayback" />
        public bool GetAudioPlayback(string fileExtension = null) 
            => (bool)_accessor.Call(fileExtension);

        // DiskCache

        /// <inheritdoc cref="_diskcache" />
        public bool? _diskCache
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }

        /// <inheritdoc cref="_diskcache" />
        public bool GetDiskCache => _accessor.Get<bool>();
        
        /// <inheritdoc cref="_diskcache" />
        public ConfigResolverAccessor WithDiskCache(bool? enabled = true) 
            => new (_accessor.Call(enabled));
        
        // MathBoost
        
        public bool? _mathBoost
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }
        
        public bool GetMathBoost => _accessor.Get<bool>(); 
        
        public ConfigResolverAccessor WithMathBoost(bool? enabled = true) 
            => new (_accessor.Call(enabled));

        // ParallelProcessing

        /// <inheritdoc cref="_parallelprocessing" />
        public bool? _parallelProcessing
        {
            get => _accessor.Get<bool>();
            set => _accessor.Set(value);
        }
        
        /// <inheritdoc cref="_parallelprocessing" />
        public bool GetParallelProcessing 
            => _accessor.Get<bool>();
        
        /// <inheritdoc cref="_parallelprocessing" />
        public ConfigResolverAccessor WithParallelProcessing(bool? enabled = true) 
            => new (_accessor.Call(enabled));

        // PlayAllTapes

        /// <inheritdoc cref="_playalltapes" />
        public bool? _playAllTapes
        {
            get => _accessor.Get<bool>();
            set => _accessor.Set(value);
        }
        /// <inheritdoc cref="_playalltapes" />
        public bool GetPlayAllTapes => _accessor.Get<bool>();
        
        /// <inheritdoc cref="_playalltapes" />
        public ConfigResolverAccessor WithPlayAllTapes(bool? enabled = true) 
            => new (_accessor.Call(enabled));

        // Misc Settings

        /// <inheritdoc cref="_leafchecktimeout" />
        public double? _leafCheckTimeOut
        {
            get => _accessor.Get<double?>();
            set => _accessor.Set(value);
        }
        /// <inheritdoc cref="_leafchecktimeout" />
        public double GetLeafCheckTimeOut => _accessor.Get<double>();
        
        /// <inheritdoc cref="_leafchecktimeout" />
        public ConfigResolverAccessor WithLeafCheckTimeOut(double? seconds) 
            => new (_accessor.Call(seconds));

        /// <inheritdoc cref="_leafchecktimeout" />
        public TimeOutActionEnum? _timeOutAction
        {
            get => _accessor.Get<TimeOutActionEnum?>();
            set => _accessor.Set<TimeOutActionEnum?>(value);
        }
        /// <inheritdoc cref="_leafchecktimeout" />
        public TimeOutActionEnum GetTimeOutAction 
            => _accessor.Get<TimeOutActionEnum>();
        
        /// <inheritdoc cref="_leafchecktimeout" />
        public ConfigResolverAccessor WithTimeOutAction(TimeOutActionEnum? action) 
            => new (_accessor.Call(action));

        public int? _courtesyFrames
        {
            get => _accessor.Get<int?>();
            set => _accessor.Set(value);
        }
        
        public int GetCourtesyFrames => _accessor.Get<int>();
        
        public ConfigResolverAccessor WithCourtesyFrames(int? value) 
            => new (_accessor.Call(value));

        public int GetFileExtensionMaxLength => _accessor.Get<int>();

        public string _longTestCategory
        {
            get => _accessor.Get<string>();
            set => _accessor.Set(value);
        }
        
        public ConfigResolverAccessor WithLongTestCategory(string category) 
            => new (_accessor.Call(category));

        public string GetLongTestCategory => (string)_accessor.Get();

        // Tooling
        
        public static string NCrunchEnvironmentVariableName         => _staticAccessor.Get<string>();
        public static string NCrunchEnvironmentVariableValue        => _staticAccessor.Get<string>();
        public static string AzurePipelinesEnvironmentVariableValue => _staticAccessor.Get<string>();
        public static string AzurePipelinesEnvironmentVariableName  => _staticAccessor.Get<string>();

        public bool? _ncrunchImpersonationMode
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }
        
        public bool? GetNCrunchImpersonationMode => _accessor.Get<bool?>();

        public bool? _azurePipelinesImpersonationMode
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }
        
        public bool? GetAzurePipelinesImpersonationMode => _accessor.Get<bool?>();

        public bool IsUnderNCrunch => _accessor.Get<bool>();

        public bool IsUnderAzurePipelines => _accessor.Get<bool>();

        // Persistence

        public static PersistenceConfiguration PersistenceConfigurationOrDefault 
            => (PersistenceConfiguration)_staticAccessor.Get();

        public static PersistenceConfiguration GetDefaultInMemoryConfiguration() 
            => (PersistenceConfiguration)_staticAccessor.Call();

        // Warnings

        public IList<string> GetWarnings(string fileExtension = null) 
            => _accessor.Call(() => GetWarnings(fileExtension));
    }
}
