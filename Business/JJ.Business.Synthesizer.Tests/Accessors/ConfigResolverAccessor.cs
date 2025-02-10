using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using System.Reflection;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Wishes.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using System.Diagnostics;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Persistence;
using JJ.Framework.Wishes.Common;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Tests.Helpers;
using wishdocs = JJ.Business.Synthesizer.Wishes.docs;
// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class ConfigResolverAccessor
    {
        private object _ = null;
        
        private static readonly AccessorEx _staticAccessor = new AccessorEx(GetUnderlyingType());
        private readonly AccessorEx _accessor;
        
        public object Obj { get; }
        
        public ConfigResolverAccessor(object obj)
        {
            _accessor = new AccessorEx(obj, GetUnderlyingType());
            Obj = obj;
        }
        
        private static Type GetUnderlyingType()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.Config.ConfigResolver";
            Type     type     = assembly.GetType(typeName, true);
            return   type;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return Obj == null;
            if (obj is ConfigResolverAccessor other) return Obj == other.Obj;
            return false;
        }
        
        public string DebuggerDisplay => _accessor.GetPropertyValue(() => DebuggerDisplay);
        
        public static ConfigResolverAccessor Static => new ConfigResolverAccessor(_staticAccessor.GetPropertyValue());

        public ConfigSectionAccessor _section
        {
            get => new ConfigSectionAccessor(_accessor.GetFieldValue(MemberName()));
            set => _accessor.SetFieldValue(MemberName(), value.Obj);
        }
        
        // Bits

        public bool Is8Bit => _accessor.GetPropertyValue(() => Is8Bit);
        public bool Is16Bit => _accessor.GetPropertyValue(() => Is16Bit);
        public bool Is32Bit => _accessor.GetPropertyValue(() => Is32Bit);
        
        public int? _bits
        {
            get => _accessor.GetFieldValue(() => _bits);
            set => _accessor.SetFieldValue(() => _bits, value);
        }

        public int GetBits => _accessor.GetPropertyValue(() => GetBits);

        public ConfigResolverAccessor WithBits(int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(), 
                new object[] { value }, 
                new Type[] { typeof(int?) }));
        
        
        // Channels
        
        public int NoChannels => _accessor.GetPropertyValue(() => NoChannels);
        public int MonoChannels => _accessor.GetPropertyValue(() => MonoChannels);
        public int StereoChannels => _accessor.GetPropertyValue(() => StereoChannels);

        public bool IsMono => _accessor.GetPropertyValue(() => IsMono);
        public bool IsStereo => _accessor.GetPropertyValue(() => IsStereo);

        public int? _channels
        {
            get => _accessor.GetFieldValue(() => _channels);
            set => _accessor.SetFieldValue(() => _channels, value);
        }

        public int GetChannels => _accessor.GetPropertyValue(() => GetChannels);
        public ConfigResolverAccessor WithChannels(int? channels) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(), 
                new object[] { channels }, 
                new Type[] { typeof(int?) }));
                
        // Channel

        public int CenterChannel => _accessor.GetPropertyValue(() => CenterChannel);
        public int LeftChannel => _accessor.GetPropertyValue(() => LeftChannel);
        public int RightChannel => _accessor.GetPropertyValue(() => RightChannel);
        public int? EmptyChannel => _accessor.GetPropertyValue(() => EmptyChannel);

        public bool IsCenter => _accessor.GetPropertyValue(() => IsCenter);
        public bool IsLeft => _accessor.GetPropertyValue(() => IsLeft);
        public bool IsRight => _accessor.GetPropertyValue(() => IsRight);
        public bool IsAnyChannel => _accessor.GetPropertyValue(() => IsAnyChannel);
        public bool IsEveryChannel => _accessor.GetPropertyValue(() => IsEveryChannel);
        public bool IsNoChannel => _accessor.GetPropertyValue(() => IsNoChannel);

        public int _channel
        {
            get => _accessor.GetFieldValue(() => _channel);
            set => _accessor.SetFieldValue(() => _channel, value);
        }
        public int? GetChannel => _accessor.GetPropertyValue(() => GetChannel);
        
        public ConfigResolverAccessor WithChannel(int? channels) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(), 
                new object[] { channels }, 
                new Type[] { typeof(int?) }));
        
        public ConfigResolverAccessor WithCenter() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        public ConfigResolverAccessor WithLeft() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        public ConfigResolverAccessor WithRight() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        public ConfigResolverAccessor WithNoChannel() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        
        // SamplingRate
        
        /// <inheritdoc cref="wishdocs._getsamplingrate" />
        public int? _samplingRate
        {
            get => _accessor.GetFieldValue(() => _samplingRate);
            set => _accessor.SetFieldValue(() => _samplingRate, value);
        }
        
        /// <inheritdoc cref="wishdocs._getsamplingrate" />
        public ConfigResolverAccessor WithSamplingRate(int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(), 
                new object[] { value }, 
                new Type[] { typeof(int?) }));
        
        /// <inheritdoc cref="wishdocs._getsamplingrate" />
        public int GetSamplingRate => _accessor.GetPropertyValue(() => GetSamplingRate);
        
        public int ResolveSamplingRate() => _accessor.GetPropertyValue(() => ResolveSamplingRate());

        // AudioFormat

        public bool IsRaw => _accessor.GetPropertyValue(() => IsRaw);
        public bool IsWav => _accessor.GetPropertyValue(() => IsWav);

        public AudioFileFormatEnum? _audioFormat
        {
            get => _accessor.GetFieldValue(() => _audioFormat);
            set => _accessor.SetFieldValue(() => _audioFormat, value);
        }

        public AudioFileFormatEnum GetAudioFormat => _accessor.GetPropertyValue(() => GetAudioFormat);
        
        public ConfigResolverAccessor WithAudioFormat(AudioFileFormatEnum? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(), 
                new object[] { value }, 
                new Type[] { typeof(AudioFileFormatEnum?) }));

        // Interpolation
        
        public bool IsLinear => _accessor.GetPropertyValue(() => IsLinear);
        public bool IsBlocky => _accessor.GetPropertyValue(() => IsBlocky);
        
        public InterpolationTypeEnum? _interpolation
        {
            get => _accessor.GetFieldValue(() => _interpolation);
            set => _accessor.SetFieldValue(() => _interpolation, value);
        }

        public InterpolationTypeEnum GetInterpolation => _accessor.GetPropertyValue(() => GetInterpolation);

        public ConfigResolverAccessor WithLinear() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        public ConfigResolverAccessor WithBlocky() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        
        public ConfigResolverAccessor WithInterpolation(InterpolationTypeEnum? interpolation) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(), 
                new object[] { interpolation }, 
                new Type[] { typeof(InterpolationTypeEnum?) }));
        
        // NoteLength
        
        /// <inheritdoc cref="wishdocs._notelength" />
        private FlowNode _noteLength
        {
            get => _accessor.GetFieldValue(() => _noteLength);
            set => _accessor.SetFieldValue(() => _noteLength, value);
        }

        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes) => _accessor.InvokeMethod(() => GetNoteLength(synthWishes));

        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLength(SynthWishes synthWishes, FlowNode noteLength)
            => _accessor.InvokeMethod(() => GetNoteLength(synthWishes, noteLength));

        /// <inheritdoc cref="wishdocs._notelength" />
        public ConfigResolverAccessor WithNoteLength(FlowNode noteLength)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { noteLength },
                new Type[] { typeof(FlowNode) }));

        /// <inheritdoc cref="wishdocs._notelength" />
        public ConfigResolverAccessor WithNoteLength(double noteLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { noteLength, synthWishes },
                new Type[] { typeof(double), typeof(FlowNode) }));
        
        /// <inheritdoc cref="wishdocs._notelength" />
        public ConfigResolverAccessor ResetNoteLength() => new ConfigResolverAccessor(_accessor.InvokeMethod());

        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time, int channel)
            => _accessor.InvokeMethod(() => GetNoteLengthSnapShot(synthWishes, noteLength, time, channel));

        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes) => _accessor.InvokeMethod(() => GetNoteLengthSnapShot(synthWishes));
        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time) => _accessor.InvokeMethod(() => GetNoteLengthSnapShot(synthWishes, time));
        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, double time, int channel) => _accessor.InvokeMethod(() => GetNoteLengthSnapShot(synthWishes, time, channel));
        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength) => _accessor.InvokeMethod(() => GetNoteLengthSnapShot(synthWishes, noteLength));
        /// <inheritdoc cref="wishdocs._notelength" />
        public FlowNode GetNoteLengthSnapShot(SynthWishes synthWishes, FlowNode noteLength, double time) => _accessor.InvokeMethod(() => GetNoteLengthSnapShot(synthWishes, noteLength, time));

        // BarLength

        private FlowNode _barLength
        {
            get => _accessor.GetFieldValue(() => _barLength);
            set => _accessor.SetFieldValue(() => _barLength, value);
        }

        public FlowNode GetBarLength(SynthWishes synthWishes) => _accessor.InvokeMethod(() => GetBarLength(synthWishes));
        
        public ConfigResolverAccessor WithBarLength(FlowNode barLength)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { barLength },
                new Type[] { typeof(FlowNode) }));
        
        public ConfigResolverAccessor WithBarLength(double barLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { barLength, synthWishes },
                new Type[] { typeof(double), typeof(SynthWishes) }));

        public ConfigResolverAccessor ResetBarLength() => new ConfigResolverAccessor(_accessor.InvokeMethod());

        // BeatLength

        private FlowNode _beatLength
        {
            get => _accessor.GetFieldValue(() => _beatLength);
            set => _accessor.SetFieldValue(() => _beatLength, value);
        }
        
        public FlowNode GetBeatLength(SynthWishes synthWishes) => _accessor.InvokeMethod(() => GetBeatLength(synthWishes));
        
        public ConfigResolverAccessor WithBeatLength(FlowNode beatLength)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { beatLength },
                new Type[] { typeof(FlowNode) }));
        
        public ConfigResolverAccessor WithBeatLength(double beatLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { beatLength, synthWishes },
                new Type[] { typeof(double), typeof(SynthWishes) }));
        
        public ConfigResolverAccessor ResetBeatLength() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        
        // Audio Length
        
        /// <inheritdoc cref="wishdocs._audiolength" />
        private FlowNode _audioLength
        {
            get => _accessor.GetFieldValue(() => _audioLength);
            set => _accessor.SetFieldValue(() => _audioLength, value);
        }
        
        /// <inheritdoc cref="wishdocs._audiolength" />
        public FlowNode GetAudioLength(SynthWishes synthWishes) => _accessor.InvokeMethod(() => GetAudioLength(synthWishes));

        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor WithAudioLength(FlowNode newAudioLength)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { newAudioLength },
                new Type[] { typeof(FlowNode) }));

        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor WithAudioLength(double? newAudioLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { newAudioLength, synthWishes }, 
                new Type[] { typeof(double?), typeof(SynthWishes) }));
        
        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor AddAudioLength(FlowNode additionalLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { additionalLength, synthWishes },
                new Type[] { typeof(FlowNode), typeof(SynthWishes) }));


        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor AddAudioLength(double additionalLength, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { additionalLength, synthWishes },
                new Type[] { typeof(double), typeof(SynthWishes) }));

        public ConfigResolverAccessor AddEchoDuration(int count, FlowNode delay, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new object[] { count, delay, synthWishes },
                new Type[] { typeof(int), typeof(FlowNode), typeof(SynthWishes) }));

        public ConfigResolverAccessor AddEchoDuration(int count, double delay, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(count, delay, synthWishes));

        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor EnsureAudioLength(FlowNode audioLengthNeeded, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(audioLengthNeeded, synthWishes));

        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor EnsureAudioLength(double audioLengthNeeded, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(audioLengthNeeded, synthWishes));
        
        /// <inheritdoc cref="wishdocs._audiolength" />
        public ConfigResolverAccessor ResetAudioLength() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        
        // LeadingSilence
        
        /// <inheritdoc cref="wishdocs._padding"/>
        private FlowNode _leadingSilence
        {
            get => _accessor.GetFieldValue(() => _leadingSilence);
            set => _accessor.SetFieldValue(() => _leadingSilence, value);
        }

        /// <inheritdoc cref="wishdocs._padding"/>
        public FlowNode GetLeadingSilence(SynthWishes synthWishes) => _accessor.InvokeMethod(() => GetLeadingSilence(synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithLeadingSilence(FlowNode seconds)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(seconds));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithLeadingSilence(double seconds, SynthWishes synthWishes)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(seconds, synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor ResetLeadingSilence() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        
        // TrailingSilence
        
        /// <inheritdoc cref="wishdocs._padding"/>
        private FlowNode _trailingSilence
        { 
            get => _accessor.GetFieldValue(() => _trailingSilence);
            set => _accessor.SetFieldValue(() => _trailingSilence, value);
        }

        /// <inheritdoc cref="wishdocs._padding"/>
        public FlowNode GetTrailingSilence(SynthWishes synthWishes) => _accessor.InvokeMethod(() => GetTrailingSilence(synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithTrailingSilence(FlowNode seconds) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(seconds));
        
        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithTrailingSilence(double seconds, SynthWishes synthWishes) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(seconds, synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor ResetTrailingSilence() => new ConfigResolverAccessor(_accessor.InvokeMethod());

        // Padding

        /// <inheritdoc cref="wishdocs._padding"/>
        public FlowNode GetPaddingOrNull(SynthWishes synthWishes) => _accessor.InvokeMethod(() => GetPaddingOrNull(synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithPadding(FlowNode seconds) => new ConfigResolverAccessor(_accessor.InvokeMethod(seconds));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor WithPadding(double seconds, SynthWishes synthWishes) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(seconds, synthWishes));

        /// <inheritdoc cref="wishdocs._padding"/>
        public ConfigResolverAccessor ResetPadding() => new ConfigResolverAccessor(_accessor.InvokeMethod());
        
        // AudioPlayback
        
        /// <inheritdoc cref="wishdocs._audioplayback" />
        private bool? _audioPlayback
        {
            get => _accessor.GetFieldValue(() => _audioPlayback);
            set => _accessor.SetFieldValue(() => _audioPlayback, value);
        }

        /// <inheritdoc cref="wishdocs._audioplayback" />
        public ConfigResolverAccessor WithAudioPlayback(bool? enabled = true) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(enabled));
        
        /// <inheritdoc cref="wishdocs._audioplayback" />
        public bool GetAudioPlayback(string fileExtension = null) => _accessor.InvokeMethod(() => GetAudioPlayback(fileExtension));

        // DiskCache

        /// <inheritdoc cref="wishdocs._diskcache" />
        private bool? _diskCache
        {
            get => _accessor.GetFieldValue(() => _diskCache);
            set => _accessor.SetFieldValue(() => _diskCache, value);
        }

        /// <inheritdoc cref="wishdocs._diskcache" />
        public bool GetDiskCache => _accessor.GetPropertyValue(() => GetDiskCache);
        /// <inheritdoc cref="wishdocs._diskcache" />
        public ConfigResolverAccessor WithDiskCache(bool? enabled = true) => new ConfigResolverAccessor(_accessor.InvokeMethod(enabled));
        
        // MathBoost
        
        private bool? _mathBoost
        {
            get => _accessor.GetFieldValue(() => _mathBoost);
            set => _accessor.SetFieldValue(() => _mathBoost, value);
        }
        
        public bool GetMathBoost => _accessor.GetPropertyValue(() => GetMathBoost); 
        public ConfigResolverAccessor WithMathBoost(bool? enabled = true) => new ConfigResolverAccessor(_accessor.InvokeMethod(enabled));

        // ParallelProcessing

        /// <inheritdoc cref="wishdocs._parallelprocessing" />
        private bool? _parallelProcessing
        {
            get => _accessor.GetFieldValue(() => _parallelProcessing);
            set => _accessor.SetFieldValue(() => _parallelProcessing, value);
        }
        
        /// <inheritdoc cref="wishdocs._parallelprocessing" />
        public bool GetParallelProcessing => _accessor.GetPropertyValue(() => GetParallelProcessing);
        /// <inheritdoc cref="wishdocs._parallelprocessing" />
        public ConfigResolverAccessor WithParallelProcessing(bool? enabled = true) => new ConfigResolverAccessor(_accessor.InvokeMethod(enabled));

        // PlayAllTapes

        /// <inheritdoc cref="wishdocs._playalltapes" />
        private bool? _playAllTapes
        {
            get => _accessor.GetFieldValue(() => _playAllTapes);
            set => _accessor.SetFieldValue(() => _playAllTapes, value);
        }
        /// <inheritdoc cref="wishdocs._playalltapes" />
        public bool GetPlayAllTapes => _accessor.GetPropertyValue(() => GetPlayAllTapes);
        /// <inheritdoc cref="wishdocs._playalltapes" />
        public ConfigResolverAccessor WithPlayAllTapes(bool? enabled = true) => new ConfigResolverAccessor(_accessor.InvokeMethod(enabled));

        // Misc Settings

        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        private double? _leafCheckTimeOut
        {
            get => _accessor.GetFieldValue(() => _leafCheckTimeOut);
            set => _accessor.SetFieldValue(() => _leafCheckTimeOut, value);
        }
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => _accessor.GetPropertyValue(() => GetLeafCheckTimeOut);
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public ConfigResolverAccessor WithLeafCheckTimeOut(double? seconds) => new ConfigResolverAccessor(_accessor.InvokeMethod(seconds));

        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        private TimeOutActionEnum? _timeOutAction
        {
            get => _accessor.GetFieldValue(() => _timeOutAction);
            set => _accessor.SetFieldValue(() => _timeOutAction, value);
        }
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        // ReSharper disable once PossibleInvalidOperationException
        public TimeOutActionEnum GetTimeOutAction => _accessor.GetPropertyValue(() => GetTimeOutAction);
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public ConfigResolverAccessor WithTimeOutAction(TimeOutActionEnum? action) => new ConfigResolverAccessor(_accessor.InvokeMethod(action));

        private int? _courtesyFrames
        {
            get => _accessor.GetFieldValue(() => _courtesyFrames);
            set => _accessor.SetFieldValue(() => _courtesyFrames, value);
        }
        public int GetCourtesyFrames => _accessor.GetPropertyValue(() => GetCourtesyFrames);
        
        public ConfigResolverAccessor WithCourtesyFrames(int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(), 
                new object[] { value }, 
                new Type[] { typeof(int?) }));

        public int GetFileExtensionMaxLength => _accessor.GetPropertyValue(() => GetFileExtensionMaxLength);

        private string _longTestCategory
        {
            get => _accessor.GetFieldValue(() => _longTestCategory);
            set => _accessor.SetFieldValue(() => _longTestCategory, value);
        }
        
        public ConfigResolverAccessor WithLongTestCategory(string category) => new ConfigResolverAccessor(_accessor.InvokeMethod(category));

        public string GetLongTestCategory => _accessor.GetPropertyValue(() => GetLongTestCategory);

        // Tooling

        private static string NCrunchEnvironmentVariableName => _staticAccessor.GetFieldValue(() => NCrunchEnvironmentVariableName);
        private static string NCrunchEnvironmentVariableValue => _staticAccessor.GetFieldValue(() => NCrunchEnvironmentVariableValue);
        private static string AzurePipelinesEnvironmentVariableValue => _staticAccessor.GetFieldValue(() => AzurePipelinesEnvironmentVariableValue);
        private static string AzurePipelinesEnvironmentVariableName => _staticAccessor.GetFieldValue(() => AzurePipelinesEnvironmentVariableName);

        private bool? _ncrunchImpersonationMode
        {
            get => _accessor.GetFieldValue(() => _ncrunchImpersonationMode);
            set => _accessor.SetFieldValue(() => _ncrunchImpersonationMode, value);
        }
        private bool? GetNCrunchImpersonationMode => _accessor.GetPropertyValue(() => GetNCrunchImpersonationMode);

        private bool? _azurePipelinesImpersonationMode
        {
            get => _accessor.GetFieldValue(() => _azurePipelinesImpersonationMode);
            set => _accessor.SetFieldValue(() => _azurePipelinesImpersonationMode, value);
        }
        private bool? GetAzurePipelinesImpersonationMode => _accessor.GetPropertyValue(() => GetAzurePipelinesImpersonationMode);

        public bool IsUnderNCrunch => _accessor.GetPropertyValue(() => IsUnderNCrunch);

        public bool IsUnderAzurePipelines => _accessor.GetPropertyValue(() => IsUnderAzurePipelines);

        // Persistence

        public static PersistenceConfiguration PersistenceConfigurationOrDefault => _staticAccessor.GetPropertyValue(() => PersistenceConfigurationOrDefault);

        private static PersistenceConfiguration GetDefaultInMemoryConfiguration() => _staticAccessor.InvokeMethod(() => GetDefaultInMemoryConfiguration());

        // Warnings

        public IList<string> GetWarnings(string fileExtension = null) => _accessor.InvokeMethod(() => GetWarnings(fileExtension));

    }
}
