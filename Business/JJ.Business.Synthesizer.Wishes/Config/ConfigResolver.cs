using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Common;
using JJ.Framework.Wishes.Reflection;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Wishes.Common.EnvironmentHelperWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Configuration.ConfigurationManagerWishes;
using static JJ.Framework.Wishes.Testing.TestWishes;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class ConfigResolver
    {
        string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
        
        /// <summary> For static contexts use this. </summary>
        internal static ConfigResolver Static { get; } = new ConfigResolver();
        
        private readonly ConfigSection _section = TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        // Audio Attributes
        
        // Bits
        
        private int? _bits;
        public int GetBits => CoalesceBits(_bits, _section.Bits);
        public bool Is8Bit => GetBits == 8;
        public bool Is16Bit => GetBits == 16;
        public bool Is32Bit => GetBits == 32;
        public ConfigResolver WithBits(int? bits) { _bits = bits.AssertBits(); return this; }
        
        // Channels
        
        public int NoChannels     => ConfigWishes.NoChannels;
        public int MonoChannels   => ConfigWishes.MonoChannels;
        public int StereoChannels => ConfigWishes.StereoChannels;

        private int? _channels;
        public int GetChannels => CoalesceChannels(_channels, _section.Channels);
        public bool IsMono => GetChannels == 1;
        public bool IsStereo => GetChannels == 2;
        public ConfigResolver WithChannels(int? channels) { _channels = AssertChannels(channels); return this; }
        
        // Channel
        
        public int  CenterChannel => ConfigWishes.CenterChannel;
        public int  LeftChannel   => ConfigWishes.LeftChannel;
        public int  RightChannel  => ConfigWishes.RightChannel;
        public int? AnyChannel    => ConfigWishes.AnyChannel;
        public int? EveryChannel  => ConfigWishes.EveryChannel;
        public int? ChannelEmpty  => ConfigWishes.ChannelEmpty;
        
        private int? _channel;
        public int? GetChannel => CoalesceChannelsChannelCombo(GetChannels, _channel).channel;
        public ConfigResolver WithChannel(int? channel)
        {
            //if (channel == EveryChannel || channel == RightChannel) this.WithStereo(); // Sneaky switch breaks tests.
            _channel = AssertChannel(channel); return this; 
        }
        public bool           IsCenter  =>       IsMono  ? GetChannel == CenterChannel : default;
        public ConfigResolver WithCenter() {   this.WithMono(); WithChannel  (CenterChannel); return this; }
        public bool           IsLeft    =>     IsStereo  ? GetChannel == LeftChannel   : default;
        public ConfigResolver WithLeft  () { this.WithStereo(); WithChannel  (LeftChannel)  ; return this; }
        public bool           IsRight   =>     IsStereo  ? GetChannel == RightChannel  : default;
        public ConfigResolver WithRight () { this.WithStereo(); WithChannel  (RightChannel) ; return this; }
        
        // SamplingRate
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        internal int? _samplingRate;
        /// <inheritdoc cref="docs._getsamplingrate" />
        public ConfigResolver WithSamplingRate(int? value) { _samplingRate = AssertSamplingRate(value); return this; }
        
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public int GetSamplingRate => ResolveSamplingRate().AssertSamplingRate();
        
        private int ResolveSamplingRate()
        {
            if (Has(_samplingRate))
            {
                return _samplingRate.Value;
            }
            
            if (IsUnderNCrunch)
            {
                bool testIsLong = CurrentTestIsInCategory(GetLongTestCategory);
                
                if (testIsLong)
                {
                    return Coalesce(_section.NCrunch.SamplingRateLongRunning, DefaultNCrunchSamplingRateLongRunning);
                }
                else
                {
                    return Coalesce(_section.NCrunch.SamplingRate, DefaultNCrunchSamplingRate);
                }
            }
            
            if (IsUnderAzurePipelines)
            {
                bool testIsLong = CurrentTestIsInCategory(GetLongTestCategory);
                
                if (testIsLong)
                {
                    return Coalesce(_section.AzurePipelines.SamplingRateLongRunning, DefaultAzurePipelinesSamplingRateLongRunning);
                }
                else
                {
                    return Coalesce(_section.AzurePipelines.SamplingRate, DefaultAzurePipelinesSamplingRate);
                }
            }
            
            return CoalesceSamplingRate(_section.SamplingRate);
        }
        
        // AudioFormat
        
        private AudioFileFormatEnum? _audioFormat;
        public AudioFileFormatEnum GetAudioFormat => CoalesceAudioFormat(_audioFormat, _section.AudioFormat);
        public ConfigResolver WithAudioFormat(AudioFileFormatEnum? audioFormat) { _audioFormat = AssertAudioFormat(audioFormat); return this; }
        public bool IsWav => GetAudioFormat == Wav;
        public ConfigResolver AsWav() => WithAudioFormat(Wav);
        public bool IsRaw => GetAudioFormat == Raw;
        public ConfigResolver AsRaw() => WithAudioFormat(Raw);
        
        // Interpolation
        
        private InterpolationTypeEnum? _interpolation;
        public InterpolationTypeEnum GetInterpolation => CoalesceInterpolation(_interpolation, _section.Interpolation);
        public ConfigResolver WithInterpolation(InterpolationTypeEnum? interpolation) { _interpolation = AssertInterpolation(interpolation); return this; }
        public bool IsLinear => GetInterpolation == Line;
        public ConfigResolver WithLinear() => WithInterpolation(Line);
        public bool IsBlocky => GetInterpolation == Block;
        public ConfigResolver WithBlocky() => WithInterpolation(Block);
        
        // Durations
        
        // NoteLength
        
        /// <inheritdoc cref="docs._notelength" />
        private FlowNode _noteLength;
        
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes) => GetNoteLength(synthWishes, null);
        
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes, FlowNode noteLength)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return noteLength ?? _noteLength ?? _beatLength ?? synthWishes[Coalesce(_section.NoteLength, DefaultNoteLength)];
        }
        
        /// <inheritdoc cref="docs._notelength" />
        public ConfigResolver WithNoteLength(FlowNode noteLength) { _noteLength = noteLength; return this; }
        
        /// <inheritdoc cref="docs._notelength" />
        public ConfigResolver WithNoteLength(double noteLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return WithNoteLength(synthWishes[noteLength]);
        }
        
        /// <inheritdoc cref="docs._notelength" />
        public ConfigResolver ResetNoteLength() { _noteLength = null; return this; }
        
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time, int channel)
        {
            noteLength = GetNoteLength(synthWishes, noteLength);
            double noteLengthValue = noteLength.Calculate(time, channel);
            return synthWishes.Value(noteLengthValue);
        }
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes)
            => GetNoteLengthSnapShot(synthWishes, null, 0, 0);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time)
            => GetNoteLengthSnapShot(synthWishes, null, time, 0);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time, int channel)
            => GetNoteLengthSnapShot(synthWishes, null, time, channel);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength)
            => GetNoteLengthSnapShot(synthWishes, noteLength, 0, 0);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time)
            => GetNoteLengthSnapShot(synthWishes, noteLength, time, 0);
        
        // BarLength
        
        private FlowNode _barLength;
        
        public FlowNode GetBarLength(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            
            if (_barLength != null)
            {
                return _barLength;
            }
            
            if (_beatLength != null)
            {
                return _beatLength * 4;
            }
            
            return synthWishes[Coalesce(_section.BarLength, DefaultBarLength)];
        }
        
        public ConfigResolver WithBarLength(FlowNode barLength)
        {
            _barLength = barLength ?? throw new NullException(() => barLength);
            return this;
        }
        
        public ConfigResolver WithBarLength(double barLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return WithBarLength(synthWishes[barLength]);
        }
        
        public ConfigResolver ResetBarLength() { _barLength = null; return this; }
        
        // BeatLength
        
        private FlowNode _beatLength;
        
        public FlowNode GetBeatLength(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            
            if (_beatLength != null)
            {
                return _beatLength;
            }
            
            if (_barLength != null)
            {
                return _barLength * 0.25;
            }
            
            return synthWishes[Coalesce(_section.BeatLength, DefaultBeatLength)];
        }
        
        public ConfigResolver WithBeatLength(FlowNode beatLength)
        {
            _beatLength = beatLength ?? throw new NullException(() => beatLength);
            return this;
        }
        
        public ConfigResolver WithBeatLength(double beatLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return WithBeatLength(synthWishes[beatLength]);
        }
        
        public ConfigResolver ResetBeatLength() { _beatLength = null; return this; }
        
        // Audio Length
        
        /// <inheritdoc cref="docs._audiolength" />
        private FlowNode _audioLength;
        
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            
            if (_audioLength != null) // && _audioLength.Calculate(time: 0) != 0
            {
                return _audioLength;
            }
            
            return synthWishes[Coalesce(_section.AudioLength, DefaultAudioLength)];
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public ConfigResolver WithAudioLength(FlowNode newAudioLength) { _audioLength = newAudioLength; return this; }
        
        /// <inheritdoc cref="docs._audiolength" />
        public ConfigResolver WithAudioLength(double? newAudioLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            if (newAudioLength == default) return WithAudioLength(default);
            return WithAudioLength(synthWishes[newAudioLength.Value]);
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public ConfigResolver AddAudioLength(FlowNode additionalLength, SynthWishes synthWishes)
        {
            double value = additionalLength?.Value ?? 0;
            return WithAudioLength(GetAudioLength(synthWishes) + value);
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public ConfigResolver AddAudioLength(double additionalLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return AddAudioLength(synthWishes[additionalLength], synthWishes);
        }
        
        public ConfigResolver AddEchoDuration(int count, FlowNode delay, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return AddAudioLength(synthWishes.EchoDuration(count, delay), synthWishes);
        }
        
        public ConfigResolver AddEchoDuration(int count, double delay, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return AddAudioLength(synthWishes.EchoDuration(count, delay), synthWishes);
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public ConfigResolver EnsureAudioLength(FlowNode audioLengthNeeded, SynthWishes synthWishes)
        {
            double value = audioLengthNeeded?.Value ?? 0;
            return EnsureAudioLength(value, synthWishes);
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public ConfigResolver EnsureAudioLength(double audioLengthNeeded, SynthWishes synthWishes)
        {
            if (GetAudioLength(synthWishes).Value < audioLengthNeeded)
            {
                WithAudioLength(audioLengthNeeded, synthWishes);
            }
            
            return this;
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public ConfigResolver ResetAudioLength() { _audioLength = null; return this; }
        
        // LeadingSilence
        
        /// <inheritdoc cref="docs._padding"/>
        private FlowNode _leadingSilence;
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetLeadingSilence(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            
            if (_leadingSilence != null)
            {
                return _leadingSilence;
            }
            
            return synthWishes[Coalesce(_section.LeadingSilence, DefaultLeadingSilence)];
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver WithLeadingSilence(FlowNode seconds)
        {
            _leadingSilence = seconds ?? throw new NullException(() => seconds);
            return this;
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver WithLeadingSilence(double seconds, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return WithLeadingSilence(synthWishes[seconds]);
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver ResetLeadingSilence()
        {
            _leadingSilence = null;
            return this;
        }
        
        // TrailingSilence
        
        /// <inheritdoc cref="docs._padding"/>
        private FlowNode _trailingSilence;
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetTrailingSilence(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            
            if (_trailingSilence != null)
            {
                return _trailingSilence;
            }
            
            return synthWishes[Coalesce(_section.TrailingSilence, DefaultTrailingSilence)];
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver WithTrailingSilence(FlowNode seconds)
        {
            _trailingSilence = seconds ?? throw new NullException(() => seconds);
            return this;
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver WithTrailingSilence(double seconds, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return WithTrailingSilence(synthWishes[seconds]);
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver ResetTrailingSilence() { _trailingSilence = null; return this; }
        
        // Padding
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetPaddingOrNull(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            
            FlowNode leadingSilence = GetLeadingSilence(synthWishes);
            FlowNode trailingSilence = GetTrailingSilence(synthWishes);
            if (leadingSilence == trailingSilence) return leadingSilence;
            
            double leadingSilenceValue = leadingSilence.Value;
            double trailingSilenceValue = trailingSilence.Value;
            if (leadingSilenceValue == trailingSilenceValue) return synthWishes[leadingSilenceValue];
            
            return null;
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver WithPadding(FlowNode seconds)
        {
            WithLeadingSilence(seconds);
            WithTrailingSilence(seconds);
            return this;
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver WithPadding(double seconds, SynthWishes synthWishes)
        {
            WithLeadingSilence(seconds, synthWishes);
            WithTrailingSilence(seconds, synthWishes);
            return this;
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver ResetPadding()
        {
            ResetLeadingSilence();
            ResetTrailingSilence();
            return this;
        }
        
        // Feature Toggles
        
        // AudioPlayback
        
        /// <inheritdoc cref="docs._audioplayback" />
        private bool? _audioPlayback;
        /// <inheritdoc cref="docs._audioplayback" />
        public ConfigResolver WithAudioPlayback(bool? enabled = true) { _audioPlayback = enabled; return this; }
        /// <inheritdoc cref="docs._audioplayback" />
        public bool GetAudioPlayback(string fileExtension = null)
        {
            bool audioPlayback = _audioPlayback ?? _section.AudioPlayback ?? DefaultAudioPlayback;
            if (!audioPlayback)
            {
                return false;
            }
            
            if (IsUnderNCrunch)
            {
                return _section.NCrunch.AudioPlayback ?? DefaultToolingAudioPlayback;
            }
            
            if (IsUnderAzurePipelines)
            {
                return _section.AzurePipelines.AudioPlayback ?? DefaultToolingAudioPlayback;
            }
            
            if (Has(fileExtension))
            {
                if (!fileExtension.Is(".wav"))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        // DiskCache
        
        /// <inheritdoc cref="docs._diskcache" />
        private bool? _diskCache;
        /// <inheritdoc cref="docs._diskcache" />
        public bool GetDiskCache => _diskCache ?? _section.DiskCache ?? DefaultDiskCache;
        /// <inheritdoc cref="docs._diskcache" />
        public ConfigResolver WithDiskCache(bool? enabled = true) { _diskCache = enabled; return this; }
        
        // MathBoost
        
        private bool? _mathBoost;
        public bool GetMathBoost => _mathBoost ?? _section.MathBoost ?? DefaultMathBoost;
        public ConfigResolver WithMathBoost(bool? enabled = true) { _mathBoost = enabled; return this; }
        
        // ParallelProcessing
        
        /// <inheritdoc cref="docs._parallelprocessing" />
        private bool? _parallelProcessing;
        /// <inheritdoc cref="docs._parallelprocessing" />
        public bool GetParallelProcessing => _parallelProcessing ?? _section.ParallelProcessing ?? DefaultParallelProcessing;
        /// <inheritdoc cref="docs._parallelprocessing" />
        public ConfigResolver WithParallelProcessing(bool? enabled = true) { _parallelProcessing = enabled; return this; }
        
        // PlayAllTapes
        
        /// <inheritdoc cref="docs._playalltapes" />
        private bool? _playAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => _playAllTapes ?? _section.PlayAllTapes ?? DefaultPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public ConfigResolver WithPlayAllTapes(bool? enabled = true) { _playAllTapes = enabled; return this; }
        
        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        private double? _leafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => _leafCheckTimeOut ?? _section.LeafCheckTimeOut ?? DefaultLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public ConfigResolver WithLeafCheckTimeOut(double? seconds) { _leafCheckTimeOut = seconds; return this; }
        
        /// <inheritdoc cref="docs._timeoutaction" />
        private TimeOutActionEnum? _timeOutAction;
        /// <inheritdoc cref="docs._timeoutaction" />
        // ReSharper disable once PossibleInvalidOperationException
        public TimeOutActionEnum GetTimeOutAction => Coalesce(_timeOutAction, _section.TimeOutAction, DefaultTimeOutAction);
        /// <inheritdoc cref="docs._timeoutaction" />
        public ConfigResolver WithTimeOutAction(TimeOutActionEnum? action) { _timeOutAction = action; return this; }
        
        /// <inheritdoc cref="docs._courtesyframes" />
        private int? _courtesyFrames;
        /// <inheritdoc cref="docs._courtesyframes" />
        public int GetCourtesyFrames => _courtesyFrames ?? _section.CourtesyFrames ?? DefaultCourtesyFrames;
        /// <inheritdoc cref="docs._courtesyframes" />
        public ConfigResolver WithCourtesyFrames(int? value) { _courtesyFrames = value; return this; }
        
        /// <inheritdoc cref="docs._fileextensionmaxlength" />
        public int GetFileExtensionMaxLength => Coalesce(_section.FileExtensionMaxLength, DefaultFileExtensionMaxLength);
        
        private string _longTestCategory;
        public ConfigResolver WithLongTestCategory(string category) { _longTestCategory = category; return this; }
        
        public string GetLongTestCategory => Coalesce(_longTestCategory, _section.LongTestCategory, DefaultLongTestCategory);
        
        // Tooling
        
        private const string NCrunchEnvironmentVariableName         = "NCrunch";
        private const string AzurePipelinesEnvironmentVariableValue = "True";
        private const string AzurePipelinesEnvironmentVariableName  = "TF_BUILD";
        private const string NCrunchEnvironmentVariableValue        = "1";

        private bool? _ncrunchImpersonationMode;
        private bool? GetNCrunchImpersonationMode => _ncrunchImpersonationMode ?? _section.NCrunch.ImpersonationMode ?? DefaultToolingImpersonationMode;
        
        private bool? _azurePipelinesImpersonationMode;
        private bool? GetAzurePipelinesImpersonationMode => _azurePipelinesImpersonationMode ?? _section.AzurePipelines.ImpersonationMode ?? DefaultToolingImpersonationMode;
        
        public bool IsUnderNCrunch
        {
            get
            {
                if (GetNCrunchImpersonationMode != null) return GetNCrunchImpersonationMode.Value;
                bool isUnderNCrunch = EnvironmentVariableIsDefined(NCrunchEnvironmentVariableName, NCrunchEnvironmentVariableValue);
                return isUnderNCrunch;
            }
            set => _ncrunchImpersonationMode = value;
        }
        
        public bool IsUnderAzurePipelines
        {
            get
            {
                if (GetAzurePipelinesImpersonationMode != null) return GetAzurePipelinesImpersonationMode.Value;
                bool isUnderAzurePipelines = EnvironmentVariableIsDefined(AzurePipelinesEnvironmentVariableName, AzurePipelinesEnvironmentVariableValue);
                return isUnderAzurePipelines;
            }
            set => _azurePipelinesImpersonationMode = value;
        }
        
        // Persistence
        
        public static PersistenceConfiguration PersistenceConfigurationOrDefault { get; }
            = TryGetSection<PersistenceConfiguration>() ?? GetDefaultInMemoryConfiguration();
        
        private static PersistenceConfiguration GetDefaultInMemoryConfiguration()
            => new PersistenceConfiguration
            {
                ContextType = "Memory",
                ModelAssembly = ReflectionWishes.GetAssemblyName<Persistence.Synthesizer.Operator>(),
                MappingAssembly = ReflectionWishes.GetAssemblyName<Persistence.Synthesizer.Memory.Mappings.OperatorMapping>(),
                RepositoryAssemblies = new[]
                {
                    ReflectionWishes.GetAssemblyName<Persistence.Synthesizer.Memory.Repositories.NodeTypeRepository>(), ReflectionWishes.GetAssemblyName<Persistence.Synthesizer.DefaultRepositories.OperatorRepository>()
                }
            };
        
        // Warnings
        
        public IList<string> GetWarnings(string fileExtension = null)
        {
            var list = new List<string>();
            
            // Sampling Rate Override
            
            if (_samplingRate != 0)
            {
                list.Add($"Sampling rate override = {_samplingRate}");
            }
            
            // Running Under Tooling
            
            if (GetNCrunchImpersonationMode != null)
            {
                if (GetNCrunchImpersonationMode == true)
                {
                    list.Add("Pretending to be NCrunch.");
                }
                if (GetNCrunchImpersonationMode == false)
                {
                    list.Add("Pretending NOT to be NCrunch.");
                }
            }
            
            if (IsUnderNCrunch)
            {
                list.Add($"Environment variable {NCrunchEnvironmentVariableName} = {NCrunchEnvironmentVariableValue}");
            }
            
            if (GetAzurePipelinesImpersonationMode != null)
            {
                if (GetAzurePipelinesImpersonationMode == true)
                {
                    list.Add("Pretending to be Azure Pipelines.");
                }
                if (GetAzurePipelinesImpersonationMode == false)
                {
                    list.Add("Pretending NOT to be Azure Pipelines.");
                }
            }
            
            if (IsUnderAzurePipelines)
            {
                list.Add($"Environment variable {AzurePipelinesEnvironmentVariableName} = {AzurePipelinesEnvironmentVariableValue} (Azure Pipelines)");
            }
            
            // Long Running
            
            bool isLong = CurrentTestIsInCategory(GetLongTestCategory);
            if (isLong)
            {
                list.Add($"Test has category '{GetLongTestCategory}'");
            }
            
            // Audio Disabled
            
            if (!GetAudioPlayback(fileExtension))
            {
                list.Add("Audio disabled");
            }
            
            return list;
        }
    }
}