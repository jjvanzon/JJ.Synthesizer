using System;
using System.Collections.Generic;
using System.Diagnostics;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Common;
using JJ.Framework.Wishes.Configuration;
using JJ.Framework.Wishes.Reflection;
using JJ.Framework.Wishes.Testing;
using static JJ.Framework.Wishes.Common.EnvironmentHelperWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class ConfigResolver
    {
        string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
        
        /// <summary> For static contexts use this. </summary>
        internal static ConfigResolver Static { get; } = new ConfigResolver();
        
        private static readonly ConfigSection _section = ConfigurationManagerWishes.TryGetSection<ConfigSection>() ?? new ConfigSection();
        
        // Audio Attributes
        
        // Bits
        
        private int? _bits;
        public int GetBits => ConfigWishes.AssertBits(FilledInWishes.Has(_bits) ? _bits.Value : _section.Bits ?? ConfigWishes.DefaultBits);
        public ConfigResolver WithBits(int? bits) { _bits = ConfigWishes.AssertBits(bits); return this; }
        public bool Is32Bit => GetBits == 32;
        public ConfigResolver With32Bit() => WithBits(32);
        public bool Is16Bit => GetBits == 16;
        public ConfigResolver With16Bit() => WithBits(16);
        public bool Is8Bit => GetBits == 8;
        public ConfigResolver With8Bit() => WithBits(8);
        
        // Channels
        
        private int? _channels;
        public int GetChannels => ConfigWishes.AssertChannels(FilledInWishes.Has(_channels) ? _channels.Value : _section.Channels ?? ConfigWishes.DefaultChannels);
        public ConfigResolver WithChannels(int? channels) { _channels = ConfigWishes.AssertChannels(channels); return this; }
        public bool IsMono => GetChannels == 1;
        public ConfigResolver WithMono() => WithChannels(1);
        public bool IsStereo => GetChannels == 2;
        public ConfigResolver WithStereo() => WithChannels(2);
        
        // Channel
        
        private int? _channel;
        public int? GetChannel => ConfigWishes.AssertChannel(_channel ?? ConfigWishes.DefaultChannel);
        public ConfigResolver WithChannel(int? channel) { _channel = ConfigWishes.AssertChannel(channel); return this; }
        public bool           IsCenter  =>       IsMono  ? GetChannel == ConfigWishes.CenterChannel : default;
        public ConfigResolver WithCenter() {   WithMono(); WithChannel  (ConfigWishes.CenterChannel); return this; }
        public bool           IsLeft    =>     IsStereo  ? GetChannel == ConfigWishes.LeftChannel   : default;
        public ConfigResolver WithLeft  () { WithStereo(); WithChannel  (ConfigWishes.LeftChannel)  ; return this; }
        public bool           IsRight   =>     IsStereo  ? GetChannel == ConfigWishes.RightChannel  : default;
        public ConfigResolver WithRight () { WithStereo(); WithChannel  (ConfigWishes.RightChannel) ; return this; }
        
        // SamplingRate
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        internal int? _samplingRate;
        /// <inheritdoc cref="docs._getsamplingrate" />
        public ConfigResolver WithSamplingRate(int? value) { _samplingRate = ConfigWishes.AssertSamplingRate(value); return this; }
        
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public int GetSamplingRate
        {
            get
            {
                if (FilledInWishes.Has(_samplingRate))
                {
                    return ConfigWishes.AssertSamplingRate(_samplingRate.Value);
                }
                
                if (IsUnderNCrunch)
                {
                    bool testIsLong = TestWishes.CurrentTestIsInCategory(GetLongTestCategory);
                    
                    if (testIsLong)
                    {
                        return ConfigWishes.AssertSamplingRate(_section.NCrunch.SamplingRateLongRunning ?? ConfigWishes.DefaultNCrunchSamplingRateLongRunning);
                    }
                    else
                    {
                        return ConfigWishes.AssertSamplingRate(_section.NCrunch.SamplingRate ?? ConfigWishes.DefaultNCrunchSamplingRate);
                    }
                }
                
                if (IsUnderAzurePipelines)
                {
                    bool testIsLong = TestWishes.CurrentTestIsInCategory(GetLongTestCategory);
                    
                    if (testIsLong)
                    {
                        return ConfigWishes.AssertSamplingRate(_section.AzurePipelines.SamplingRateLongRunning ?? ConfigWishes.DefaultAzurePipelinesSamplingRateLongRunning);
                    }
                    else
                    {
                        return ConfigWishes.AssertSamplingRate(_section.AzurePipelines.SamplingRate ?? ConfigWishes.DefaultAzurePipelinesSamplingRate);
                    }
                }
                
                return ConfigWishes.AssertSamplingRate(_section.SamplingRate ?? ConfigWishes.DefaultSamplingRate);
            }
        }
        
        // AudioFormat
        
        private AudioFileFormatEnum? _audioFormat;
        public AudioFileFormatEnum GetAudioFormat => ConfigWishes.Assert(FilledInWishes.Has(_audioFormat) ? _audioFormat.Value : _section.AudioFormat ?? ConfigWishes.DefaultAudioFormat);
        public ConfigResolver WithAudioFormat(AudioFileFormatEnum? audioFormat) { _audioFormat = ConfigWishes.Assert(audioFormat); return this; }
        public bool IsWav => GetAudioFormat == AudioFileFormatEnum.Wav;
        public ConfigResolver AsWav() => WithAudioFormat(AudioFileFormatEnum.Wav);
        public bool IsRaw => GetAudioFormat == AudioFileFormatEnum.Raw;
        public ConfigResolver AsRaw() => WithAudioFormat(AudioFileFormatEnum.Raw);
        
        // Interpolation
        
        private InterpolationTypeEnum? _interpolation;
        public InterpolationTypeEnum GetInterpolation => ConfigWishes.Assert(FilledInWishes.Has(_interpolation) ? _interpolation.Value : _section.Interpolation ?? ConfigWishes.DefaultInterpolation);
        public ConfigResolver WithInterpolation(InterpolationTypeEnum? interpolation) { _interpolation = ConfigWishes.Assert(interpolation); return this; }
        public bool IsLinear => GetInterpolation == InterpolationTypeEnum.Line;
        public ConfigResolver WithLinear() => WithInterpolation(InterpolationTypeEnum.Line);
        public bool IsBlocky => GetInterpolation == InterpolationTypeEnum.Block;
        public ConfigResolver WithBlocky() => WithInterpolation(InterpolationTypeEnum.Block);
        
        // Durations
        
        // NoteLength
        
        /// <inheritdoc cref="docs._notelength" />
        private FlowNode _noteLength;
        
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes) => GetNoteLength(synthWishes, null);
        
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes, FlowNode noteLength)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            noteLength = noteLength ?? _noteLength ?? _beatLength;
            noteLength = noteLength ?? synthWishes[_section.NoteLength ?? ConfigWishes.DefaultNoteLength];
            return noteLength;
        }
        
        /// <inheritdoc cref="docs._notelength" />
        public ConfigResolver WithNoteLength(FlowNode noteLength) { _noteLength = noteLength; return this; }
        
        /// <inheritdoc cref="docs._notelength" />
        public ConfigResolver WithNoteLength(double noteLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
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
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_barLength != null)
            {
                return _barLength;
            }
            
            if (_beatLength != null)
            {
                return _beatLength * 4;
            }
            
            return synthWishes[_section.BarLength ?? ConfigWishes.DefaultBarLength];
        }
        
        public ConfigResolver WithBarLength(FlowNode barLength)
        {
            _barLength = barLength ?? throw new NullException(() => barLength);
            return this;
        }
        
        public ConfigResolver WithBarLength(double barLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return WithBarLength(synthWishes[barLength]);
        }
        
        public ConfigResolver ResetBarLength() { _barLength = null; return this; }
        
        // BeatLength
        
        private FlowNode _beatLength;
        
        public FlowNode GetBeatLength(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_beatLength != null)
            {
                return _beatLength;
            }
            
            if (_barLength != null)
            {
                return _barLength * 0.25;
            }
            
            return synthWishes[_section.BeatLength ?? ConfigWishes.DefaultBeatLength];
        }
        
        public ConfigResolver WithBeatLength(FlowNode beatLength)
        {
            _beatLength = beatLength ?? throw new NullException(() => beatLength);
            return this;
        }
        
        public ConfigResolver WithBeatLength(double beatLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return WithBeatLength(synthWishes[beatLength]);
        }
        
        public ConfigResolver ResetBeatLength() { _beatLength = null; return this; }
        
        // Audio Length
        
        /// <inheritdoc cref="docs._audiolength" />
        private FlowNode _audioLength;
        
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_audioLength != null &&
                _audioLength.Calculate(time: 0) != 0)
            {
                return _audioLength;
            }
            
            return synthWishes[_section.AudioLength ?? ConfigWishes.DefaultAudioLength];
        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public ConfigResolver WithAudioLength(FlowNode newAudioLength) { _audioLength = newAudioLength; return this; }
        
        /// <inheritdoc cref="docs._audiolength" />
        public ConfigResolver WithAudioLength(double? newAudioLength, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
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
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return AddAudioLength(synthWishes[additionalLength], synthWishes);
        }
        
        public ConfigResolver AddEchoDuration(int count, FlowNode delay, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return AddAudioLength(synthWishes.EchoDuration(count, delay), synthWishes);
        }
        
        public ConfigResolver AddEchoDuration(int count, double delay, SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
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
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_leadingSilence != null)
            {
                return _leadingSilence;
            }
            
            return synthWishes[_section.LeadingSilence ?? ConfigWishes.DefaultLeadingSilence];
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
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
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
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            if (_trailingSilence != null)
            {
                return _trailingSilence;
            }
            
            return synthWishes[_section.TrailingSilence ?? ConfigWishes.DefaultTrailingSilence];
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
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return WithTrailingSilence(synthWishes[seconds]);
        }
        
        /// <inheritdoc cref="docs._padding"/>
        public ConfigResolver ResetTrailingSilence() { _trailingSilence = null; return this; }
        
        // Padding
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetPaddingOrNull(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
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
            bool audioPlayback = _audioPlayback ?? _section.AudioPlayback ?? ConfigWishes.DefaultAudioPlayback;
            if (!audioPlayback)
            {
                return false;
            }
            
            if (IsUnderNCrunch)
            {
                return _section.NCrunch.AudioPlayback ?? ConfigWishes.DefaultToolingAudioPlayback;
            }
            
            if (IsUnderAzurePipelines)
            {
                return _section.AzurePipelines.AudioPlayback ?? ConfigWishes.DefaultToolingAudioPlayback;
            }
            
            if (!string.IsNullOrWhiteSpace(fileExtension))
            {
                if (!string.Equals(fileExtension, ".wav", StringComparison.OrdinalIgnoreCase))
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
        public bool GetDiskCache => _diskCache ?? _section.DiskCache ?? ConfigWishes.DefaultDiskCache;
        /// <inheritdoc cref="docs._diskcache" />
        public ConfigResolver WithDiskCache(bool? enabled = true) { _diskCache = enabled; return this; }
        
        // MathBoost
        
        private bool? _mathBoost;
        public bool GetMathBoost => _mathBoost ?? _section.MathBoost ?? ConfigWishes.DefaultMathBoost;
        public ConfigResolver WithMathBoost(bool? enabled = true) { _mathBoost = enabled; return this; }
        
        // ParallelProcessing
        
        /// <inheritdoc cref="docs._parallelprocessing" />
        private bool? _parallelProcessing;
        /// <inheritdoc cref="docs._parallelprocessing" />
        public bool GetParallelProcessing => _parallelProcessing ?? _section.ParallelProcessing ?? ConfigWishes.DefaultParallelProcessing;
        /// <inheritdoc cref="docs._parallelprocessing" />
        public ConfigResolver WithParallelProcessing(bool? enabled = true) { _parallelProcessing = enabled; return this; }
        
        // PlayAllTapes
        
        /// <inheritdoc cref="docs._playalltapes" />
        private bool? _playAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => _playAllTapes ?? _section.PlayAllTapes ?? ConfigWishes.DefaultPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public ConfigResolver WithPlayAllTapes(bool? enabled = true) { _playAllTapes = enabled; return this; }
        
        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        private double? _leafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => _leafCheckTimeOut ?? _section.LeafCheckTimeOut ?? ConfigWishes.DefaultLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public ConfigResolver WithLeafCheckTimeOut(double? seconds) { _leafCheckTimeOut = seconds; return this; }
        
        /// <inheritdoc cref="docs._timeoutaction" />
        private TimeOutActionEnum? _timeOutAction;
        /// <inheritdoc cref="docs._timeoutaction" />
        // ReSharper disable once PossibleInvalidOperationException
        public TimeOutActionEnum GetTimeOutAction => FilledInWishes.Has(_timeOutAction) ? _timeOutAction.Value : _section.TimeOutAction ?? ConfigWishes.DefaultTimeOutAction;
        /// <inheritdoc cref="docs._timeoutaction" />
        public ConfigResolver WithTimeOutAction(TimeOutActionEnum? action) { _timeOutAction = action; return this; }
        
        /// <inheritdoc cref="docs._courtesyframes" />
        private int? _courtesyFrames;
        /// <inheritdoc cref="docs._courtesyframes" />
        public int GetCourtesyFrames => _courtesyFrames ?? _section.CourtesyFrames ?? ConfigWishes.DefaultCourtesyFrames;
        /// <inheritdoc cref="docs._courtesyframes" />
        public ConfigResolver WithCourtesyFrames(int? value) { _courtesyFrames = value; return this; }
        
        /// <inheritdoc cref="docs._fileextensionmaxlength" />
        public int GetFileExtensionMaxLength => _section.FileExtensionMaxLength ?? ConfigWishes.DefaultFileExtensionMaxLength;
        
        private string _longTestCategory;
        public ConfigResolver WithLongTestCategory(string category) { _longTestCategory = category; return this; }
        public string GetLongTestCategory
        {
            get
            {
                if (FilledInWishes.Has(_longTestCategory)) return _longTestCategory;
                if (FilledInWishes.Has(_section.LongTestCategory)) return _section.LongTestCategory;
                return ConfigWishes.DefaultLongTestCategory;
            }
        }
        
        // Tooling
        
        private bool? _ncrunchImpersonationMode;
        private bool? GetNCrunchImpersonationMode => _ncrunchImpersonationMode ?? _section.NCrunch.ImpersonationMode ?? ConfigWishes.DefaultToolingImpersonationMode;
        
        private bool? _azurePipelinesImpersonationMode;
        private bool? GetAzurePipelinesImpersonationMode => _azurePipelinesImpersonationMode ?? _section.AzurePipelines.ImpersonationMode ?? ConfigWishes.DefaultToolingImpersonationMode;
        
        public bool IsUnderNCrunch
        {
            get
            {
                if (GetNCrunchImpersonationMode != null) return GetNCrunchImpersonationMode.Value;
                bool isUnderNCrunch = EnvironmentVariableIsDefined(ConfigWishes.NCrunchEnvironmentVariableName, ConfigWishes.NCrunchEnvironmentVariableValue);
                return isUnderNCrunch;
            }
            set => _ncrunchImpersonationMode = value;
        }
        
        public bool IsUnderAzurePipelines
        {
            get
            {
                if (GetAzurePipelinesImpersonationMode != null) return GetAzurePipelinesImpersonationMode.Value;
                bool isUnderAzurePipelines = EnvironmentVariableIsDefined(ConfigWishes.AzurePipelinesEnvironmentVariableName, ConfigWishes.AzurePipelinesEnvironmentVariableValue);
                return isUnderAzurePipelines;
            }
            set => _azurePipelinesImpersonationMode = value;
        }
        
        // Persistence
        
        public static PersistenceConfiguration PersistenceConfigurationOrDefault { get; }
            = ConfigurationManagerWishes.TryGetSection<PersistenceConfiguration>() ?? GetDefaultInMemoryConfiguration();
        
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
                list.Add($"Environment variable {ConfigWishes.NCrunchEnvironmentVariableName} = {ConfigWishes.NCrunchEnvironmentVariableValue}");
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
                list.Add($"Environment variable {ConfigWishes.AzurePipelinesEnvironmentVariableName} = {ConfigWishes.AzurePipelinesEnvironmentVariableValue} (Azure Pipelines)");
            }
            
            // Long Running
            
            bool isLong = TestWishes.CurrentTestIsInCategory(GetLongTestCategory);
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