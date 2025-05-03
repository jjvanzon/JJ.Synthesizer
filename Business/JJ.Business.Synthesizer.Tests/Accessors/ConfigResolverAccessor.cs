using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection.Core;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using wishdocs = JJ.Business.Synthesizer.Wishes.docs;
// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class ConfigResolverAccessor
    {
        
        private static readonly AccessorCore _staticAccessor = new AccessorCore(GetUnderlyingType());
        private readonly AccessorCore _accessor;
        
        public object Obj { get; }
        
        public ConfigResolverAccessor(object obj)
        {
            //_accessor = new AccessorCore(obj, GetUnderlyingType());
            _accessor = new AccessorCore(obj);
            Obj = obj;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return Obj == null;
            if (obj is ConfigResolverAccessor other) return Obj == other.Obj;
            return false;
        }
        
        private static Type GetUnderlyingType()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.Config.ConfigResolver";
            Type     type     = assembly.GetType(typeName, true);
            return   type;
        }
        
        public string DebuggerDisplay => _accessor.Get(() => DebuggerDisplay);
        
        public static ConfigResolverAccessor Static => new ConfigResolverAccessor(_staticAccessor.Get());

        public ConfigSectionAccessor _section
        {
            get => new ConfigSectionAccessor(_accessor.Get(MemberName()));
            set => _accessor.Set(MemberName(), value.Obj);
        }
        
        // Bits

        public bool Is8Bit => _accessor.Get(() => Is8Bit);
        public bool Is16Bit => _accessor.Get(() => Is16Bit);
        public bool Is32Bit => _accessor.Get(() => Is32Bit);
        
        public int? _bits
        {
            get => _accessor.Get(() => _bits);
            set => _accessor.Set(() => _bits, value);
        }

        public int GetBits => _accessor.Get(() => GetBits);

        public ConfigResolverAccessor WithBits(int? value) 
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(), [ value ], [ typeof(int?) ]));
        
        // Channels
        
        public int NoChannels => _accessor.Get(() => NoChannels);
        public int MonoChannels => _accessor.Get(() => MonoChannels);
        public int StereoChannels => _accessor.Get(() => StereoChannels);

        public bool IsMono => _accessor.Get(() => IsMono);
        public bool IsStereo => _accessor.Get(() => IsStereo);

        public int? _channels
        {
            get => _accessor.Get(() => _channels);
            set => _accessor.Set(() => _channels, value);
        }

        public int GetChannels => _accessor.Get(() => GetChannels);
        public ConfigResolverAccessor WithChannels(int? channels) 
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(), 
                new object[] { channels }, 
                new Type[] { typeof(int?) }));
                
        // Channel

        public int CenterChannel => _accessor.Get(() => CenterChannel);
        public int LeftChannel => _accessor.Get(() => LeftChannel);
        public int RightChannel => _accessor.Get(() => RightChannel);
        public int? EmptyChannel => _accessor.Get(() => EmptyChannel);

        public bool IsCenter => _accessor.Get(() => IsCenter);
        public bool IsLeft => _accessor.Get(() => IsLeft);
        public bool IsRight => _accessor.Get(() => IsRight);
        public bool IsAnyChannel => _accessor.Get(() => IsAnyChannel);
        public bool IsEveryChannel => _accessor.Get(() => IsEveryChannel);
        public bool IsNoChannel => _accessor.Get(() => IsNoChannel);

        public int _channel
        {
            get => _accessor.Get(() => _channel);
            set => _accessor.Set(() => _channel, value);
        }
        public int? GetChannel => _accessor.Get(() => GetChannel);
        
        public ConfigResolverAccessor WithChannel(int? channels) 
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(), 
                new object[] { channels }, 
                new Type[] { typeof(int?) }));
        
        public ConfigResolverAccessor WithCenter() => new ConfigResolverAccessor(_accessor.Call());
        public ConfigResolverAccessor WithLeft() => new ConfigResolverAccessor(_accessor.Call());
        public ConfigResolverAccessor WithRight() => new ConfigResolverAccessor(_accessor.Call());
        public ConfigResolverAccessor WithNoChannel() => new ConfigResolverAccessor(_accessor.Call());
        
        // SamplingRate
        
        /// <inheritdoc cref="wishdocs._getsamplingrate" />
        public int? _samplingRate
        {
            get => _accessor.Get(() => _samplingRate);
            set => _accessor.Set(() => _samplingRate, value);
        }
        
        /// <inheritdoc cref="wishdocs._getsamplingrate" />
        public ConfigResolverAccessor WithSamplingRate(int? value) 
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(), 
                new object[] { value }, 
                new Type[] { typeof(int?) }));
        
        /// <inheritdoc cref="wishdocs._getsamplingrate" />
        public int GetSamplingRate => _accessor.Get(() => GetSamplingRate);
        
        public int ResolveSamplingRate() => _accessor.Get(() => ResolveSamplingRate());

        // AudioFormat

        public bool IsRaw => _accessor.Get(() => IsRaw);
        public bool IsWav => _accessor.Get(() => IsWav);

        public AudioFileFormatEnum? _audioFormat
        {
            get => _accessor.Get(() => _audioFormat);
            set => _accessor.Set(() => _audioFormat, value);
        }

        public AudioFileFormatEnum GetAudioFormat => _accessor.Get(() => GetAudioFormat);
        
        public ConfigResolverAccessor WithAudioFormat(AudioFileFormatEnum? value) 
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(), 
                new object[] { value }, 
                new Type[] { typeof(AudioFileFormatEnum?) }));

        // Interpolation
        
        public bool IsLinear => _accessor.Get(() => IsLinear);
        public bool IsBlocky => _accessor.Get(() => IsBlocky);
        
        public InterpolationTypeEnum? _interpolation
        {
            get => _accessor.Get(() => _interpolation);
            set => _accessor.Set(() => _interpolation, value);
        }

        public InterpolationTypeEnum GetInterpolation => _accessor.Get(() => GetInterpolation);

        public ConfigResolverAccessor WithLinear() => new ConfigResolverAccessor(_accessor.Call());
        public ConfigResolverAccessor WithBlocky() => new ConfigResolverAccessor(_accessor.Call());
        
        public ConfigResolverAccessor WithInterpolation(InterpolationTypeEnum? interpolation) 
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(), 
                new object[] { interpolation }, 
                new Type[] { typeof(InterpolationTypeEnum?) }));
        
        // NoteLength
        
        /// <inheritdoc cref="wishdocs._notelength" />
        private FlowNode _noteLength
        {
            get => _accessor.Get(() => _noteLength);
            set => _accessor.Set(() => _noteLength, value);
        }

        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes) => _accessor.Call(() => GetNoteLength(synthWishes));

        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes, FlowNode noteLength)
            => _accessor.Call(() => GetNoteLength(synthWishes, noteLength));

        /// <inheritdoc cref="wishdocs._notelength" />
        public ConfigResolverAccessor WithNoteLength(FlowNode noteLength)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { noteLength },
                new Type[] { typeof(FlowNode) }));

        /// <inheritdoc cref="wishdocs._notelength" />
        public ConfigResolverAccessor WithNoteLength(double noteLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { noteLength, synthWishes },
                new Type[] { typeof(double), typeof(FlowNode) }));
        
        /// <inheritdoc cref="wishdocs._notelength" />
        public ConfigResolverAccessor ResetNoteLength() => new ConfigResolverAccessor(_accessor.Call());

        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time, int channel)
            => _accessor.Call(() => GetNoteLengthSnapShot(synthWishes, noteLength, time, channel));

        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes) => _accessor.Call(() => GetNoteLengthSnapShot(synthWishes));
        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time) => _accessor.Call(() => GetNoteLengthSnapShot(synthWishes, time));
        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time, int channel) => _accessor.Call(() => GetNoteLengthSnapShot(synthWishes, time, channel));
        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength) => _accessor.Call(() => GetNoteLengthSnapShot(synthWishes, noteLength));
        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time) => _accessor.Call(() => GetNoteLengthSnapShot(synthWishes, noteLength, time));

        // BarLength

        private FlowNode _barLength
        {
            get => _accessor.Get(() => _barLength);
            set => _accessor.Set(() => _barLength, value);
        }

        public FlowNode GetBarLength(SynthWishes synthWishes) => _accessor.Call(() => GetBarLength(synthWishes));
        
        public ConfigResolverAccessor WithBarLength(FlowNode barLength)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { barLength },
                new Type[] { typeof(FlowNode) }));
        
        public ConfigResolverAccessor WithBarLength(double barLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { barLength, synthWishes },
                new Type[] { typeof(double), typeof(SynthWishes) }));

        public ConfigResolverAccessor ResetBarLength() => new ConfigResolverAccessor(_accessor.Call());

        // BeatLength

        private FlowNode _beatLength
        {
            get => _accessor.Get(() => _beatLength);
            set => _accessor.Set(() => _beatLength, value);
        }
        
        public FlowNode GetBeatLength(SynthWishes synthWishes) => _accessor.Call(() => GetBeatLength(synthWishes));
        
        public ConfigResolverAccessor WithBeatLength(FlowNode beatLength)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { beatLength },
                new Type[] { typeof(FlowNode) }));
        
        public ConfigResolverAccessor WithBeatLength(double beatLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { beatLength, synthWishes },
                new Type[] { typeof(double), typeof(SynthWishes) }));
        
        public ConfigResolverAccessor ResetBeatLength() => new ConfigResolverAccessor(_accessor.Call());
        
        // Audio Length
        
        /// <inheritdoc cref="wishdocs._audiolength" />
        private FlowNode _audioLength
        {
            get => _accessor.Get(() => _audioLength);
            set => _accessor.Set(() => _audioLength, value);
        }
        
        /// <inheritdoc cref="wishdocs._audiolength" />
        public FlowNode GetAudioLength(SynthWishes synthWishes) => _accessor.Call(() => GetAudioLength(synthWishes));

        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor WithAudioLength(FlowNode newAudioLength)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { newAudioLength },
                new Type[] { typeof(FlowNode) }));

        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor WithAudioLength(double? newAudioLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { newAudioLength, synthWishes }, 
                new Type[] { typeof(double?), typeof(SynthWishes) }));
        
        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor AddAudioLength(FlowNode additionalLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { additionalLength, synthWishes },
                new Type[] { typeof(FlowNode), typeof(SynthWishes) }));


        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor AddAudioLength(double additionalLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { additionalLength, synthWishes },
                new Type[] { typeof(double), typeof(SynthWishes) }));

        public ConfigResolverAccessor AddEchoDuration(int count, FlowNode delay, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(),
                new object[] { count, delay, synthWishes },
                [ typeof(int), typeof(FlowNode), typeof(SynthWishes) ]));

        public ConfigResolverAccessor AddEchoDuration(int count, double delay, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(count, delay, synthWishes));

        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor EnsureAudioLength(FlowNode audioLengthNeeded, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(audioLengthNeeded, synthWishes));

        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor EnsureAudioLength(double audioLengthNeeded, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(audioLengthNeeded, synthWishes));
        
        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor ResetAudioLength() => new ConfigResolverAccessor(_accessor.Call());
        
        // LeadingSilence
        
        /// <inheritdoc cref="wishdocs._padding"/>
        private FlowNode _leadingSilence
        {
            get => _accessor.Get(() => _leadingSilence);
            set => _accessor.Set(() => _leadingSilence, value);
        }

        /// <inheritdoc cref="wishdocs._padding"/>
        public FlowNode GetLeadingSilence(SynthWishes synthWishes) => _accessor.Call(() => GetLeadingSilence(synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithLeadingSilence(FlowNode seconds)
            => new ConfigResolverAccessor(_accessor.Call(seconds));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithLeadingSilence(double seconds, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.Call(seconds, synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor ResetLeadingSilence() => new ConfigResolverAccessor(_accessor.Call());
        
        // TrailingSilence
        
        /// <inheritdoc cref="wishdocs._padding"/>
        private FlowNode _trailingSilence
        { 
            get => _accessor.Get(() => _trailingSilence);
            set => _accessor.Set(() => _trailingSilence, value);
        }

        /// <inheritdoc cref="wishdocs._padding"/>
        public FlowNode GetTrailingSilence(SynthWishes synthWishes) => _accessor.Call(() => GetTrailingSilence(synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithTrailingSilence(FlowNode seconds) 
            => new ConfigResolverAccessor(_accessor.Call(seconds));
        
        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithTrailingSilence(double seconds, SynthWishes synthWishes) 
            => new ConfigResolverAccessor(_accessor.Call(seconds, synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor ResetTrailingSilence() => new ConfigResolverAccessor(_accessor.Call());

        // Padding

        /// <inheritdoc cref="wishdocs._padding"/>
        public FlowNode GetPaddingOrNull(SynthWishes synthWishes) => _accessor.Call(() => GetPaddingOrNull(synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithPadding(FlowNode seconds) => new ConfigResolverAccessor(_accessor.Call(seconds));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithPadding(double seconds, SynthWishes synthWishes) 
            => new ConfigResolverAccessor(_accessor.Call(seconds, synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor ResetPadding() => new ConfigResolverAccessor(_accessor.Call());
        
        // AudioPlayback
        
        /// <inheritdoc cref="wishdocs._audioplayback" />
        private bool? _audioPlayback
        {
            get => _accessor.Get(() => _audioPlayback);
            set => _accessor.Set(() => _audioPlayback, value);
        }

        /// <inheritdoc cref="wishdocs._audioplayback" />
        public ConfigResolverAccessor WithAudioPlayback(bool? enabled = true) 
            => new ConfigResolverAccessor(_accessor.Call(enabled));
        
        /// <inheritdoc cref="wishdocs._audioplayback" />
        public bool GetAudioPlayback(string fileExtension = null) => _accessor.Call(() => GetAudioPlayback(fileExtension));

        // DiskCache

        /// <inheritdoc cref="wishdocs._diskcache" />
        private bool? _diskCache
        {
            get => _accessor.Get(() => _diskCache);
            set => _accessor.Set(() => _diskCache, value);
        }

        /// <inheritdoc cref="wishdocs._diskcache" />
        public bool GetDiskCache => _accessor.Get(() => GetDiskCache);
        /// <inheritdoc cref="wishdocs._diskcache" />
        public ConfigResolverAccessor WithDiskCache(bool? enabled = true) => new ConfigResolverAccessor(_accessor.Call(enabled));
        
        // MathBoost
        
        private bool? _mathBoost
        {
            get => _accessor.Get(() => _mathBoost);
            set => _accessor.Set(() => _mathBoost, value);
        }
        
        public bool GetMathBoost => _accessor.Get(() => GetMathBoost); 
        public ConfigResolverAccessor WithMathBoost(bool? enabled = true) => new ConfigResolverAccessor(_accessor.Call(enabled));

        // ParallelProcessing

        /// <inheritdoc cref="wishdocs._parallelprocessing" />
        private bool? _parallelProcessing
        {
            get => _accessor.Get(() => _parallelProcessing);
            set => _accessor.Set(() => _parallelProcessing, value);
        }
        
        /// <inheritdoc cref="wishdocs._parallelprocessing" />
        public bool GetParallelProcessing => _accessor.Get(() => GetParallelProcessing);
        /// <inheritdoc cref="wishdocs._parallelprocessing" />
        public ConfigResolverAccessor WithParallelProcessing(bool? enabled = true) => new ConfigResolverAccessor(_accessor.Call(enabled));

        // PlayAllTapes

        /// <inheritdoc cref="wishdocs._playalltapes" />
        private bool? _playAllTapes
        {
            get => _accessor.Get(() => _playAllTapes);
            set => _accessor.Set(() => _playAllTapes, value);
        }
        /// <inheritdoc cref="wishdocs._playalltapes" />
        public bool GetPlayAllTapes => _accessor.Get(() => GetPlayAllTapes);
        /// <inheritdoc cref="wishdocs._playalltapes" />
        public ConfigResolverAccessor WithPlayAllTapes(bool? enabled = true) => new ConfigResolverAccessor(_accessor.Call(enabled));

        // Misc Settings

        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        private double? _leafCheckTimeOut
        {
            get => _accessor.Get(() => _leafCheckTimeOut);
            set => _accessor.Set(() => _leafCheckTimeOut, value);
        }
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => _accessor.Get(() => GetLeafCheckTimeOut);
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public ConfigResolverAccessor WithLeafCheckTimeOut(double? seconds) => new ConfigResolverAccessor(_accessor.Call(seconds));

        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        private TimeOutActionEnum? _timeOutAction
        {
            get => _accessor.Get(() => _timeOutAction);
            set => _accessor.Set(() => _timeOutAction, value);
        }
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        // ReSharper disable once PossibleInvalidOperationException
        public TimeOutActionEnum GetTimeOutAction => _accessor.Get(() => GetTimeOutAction);
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public ConfigResolverAccessor WithTimeOutAction(TimeOutActionEnum? action) => new ConfigResolverAccessor(_accessor.Call(action));

        private int? _courtesyFrames
        {
            get => _accessor.Get(() => _courtesyFrames);
            set => _accessor.Set(() => _courtesyFrames, value);
        }
        public int GetCourtesyFrames => _accessor.Get(() => GetCourtesyFrames);
        
        public ConfigResolverAccessor WithCourtesyFrames(int? value) 
            => new ConfigResolverAccessor(_accessor.Call(
                MemberName(), 
                new object[] { value }, 
                new Type[] { typeof(int?) }));

        public int GetFileExtensionMaxLength => _accessor.Get(() => GetFileExtensionMaxLength);

        private string _longTestCategory
        {
            get => _accessor.Get(() => _longTestCategory);
            set => _accessor.Set(() => _longTestCategory, value);
        }
        
        public ConfigResolverAccessor WithLongTestCategory(string category) => new ConfigResolverAccessor(_accessor.Call(category));

        public string GetLongTestCategory => _accessor.Get(() => GetLongTestCategory);

        // Tooling

        private static string NCrunchEnvironmentVariableName => _staticAccessor.Get(() => NCrunchEnvironmentVariableName);
        private static string NCrunchEnvironmentVariableValue => _staticAccessor.Get(() => NCrunchEnvironmentVariableValue);
        private static string AzurePipelinesEnvironmentVariableValue => _staticAccessor.Get(() => AzurePipelinesEnvironmentVariableValue);
        private static string AzurePipelinesEnvironmentVariableName => _staticAccessor.Get(() => AzurePipelinesEnvironmentVariableName);

        private bool? _ncrunchImpersonationMode
        {
            get => _accessor.Get(() => _ncrunchImpersonationMode);
            set => _accessor.Set(() => _ncrunchImpersonationMode, value);
        }
        private bool? GetNCrunchImpersonationMode => _accessor.Get(() => GetNCrunchImpersonationMode);

        private bool? _azurePipelinesImpersonationMode
        {
            get => _accessor.Get(() => _azurePipelinesImpersonationMode);
            set => _accessor.Set(() => _azurePipelinesImpersonationMode, value);
        }
        private bool? GetAzurePipelinesImpersonationMode => _accessor.Get(() => GetAzurePipelinesImpersonationMode);

        public bool IsUnderNCrunch => _accessor.Get(() => IsUnderNCrunch);

        public bool IsUnderAzurePipelines => _accessor.Get(() => IsUnderAzurePipelines);

        // Persistence

        public static PersistenceConfiguration PersistenceConfigurationOrDefault => _staticAccessor.Get(() => PersistenceConfigurationOrDefault);

        private static PersistenceConfiguration GetDefaultInMemoryConfiguration() => _staticAccessor.Call(() => GetDefaultInMemoryConfiguration());

        // Warnings

        public IList<string> GetWarnings(string fileExtension = null) => _accessor.Call(() => GetWarnings(fileExtension));

        // Derived Properties
        
    }
}
