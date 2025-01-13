using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Configuration;
using static JJ.Framework.Wishes.Common.FilledInWishes;

// ReSharper disable RedundantNameQualifier

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    // ConfigWishes
    
    public partial class ConfigWishes
    {
        // Environment Variables

        internal const string NCrunchEnvironmentVariableName         = "NCrunch";
        internal const string AzurePipelinesEnvironmentVariableValue = "True";
        internal const string AzurePipelinesEnvironmentVariableName  = "TF_BUILD";
        internal const string NCrunchEnvironmentVariableValue        = "1";
                        
        // Constants
        
        public const int ChannelsEmpty = 0;
        public const int MonoChannels = 1;
        public const int StereoChannels = 2;
        public const int CenterChannel = 0;
        public const int LeftChannel = 0;
        public const int RightChannel = 1;
        public static readonly int? ChannelEmpty = null;
        public static readonly int? EveryChannel = null;

        // Coalesce Defaults
        
        public static int CoalesceBits(int? bits)
        {
            if (!Has(bits)) return DefaultBits;
            return bits.Value;
        }

        public static int CoalesceChannels(int? channels)
        {
            if (!Has(channels)) return DefaultChannels;
            return channels.Value;
        }
    }
    
    // ConfigWishes
}

namespace JJ.Business.Synthesizer.Wishes
{
    // SynthWishes ConfigWishes
    
    public partial class SynthWishes
    {
        // Audio Quality
        
        public int GetBits => Config.GetBits;
        public SynthWishes WithBits(int? bits) { Config.WithBits(bits); return this; }
        public bool Is32Bit => Config.Is32Bit;
        public SynthWishes With32Bit() { Config.With32Bit(); return this; }
        public bool Is16Bit => Config.Is16Bit;
        public SynthWishes With16Bit() { Config.With16Bit(); return this; }
        public bool Is8Bit => Config.Is8Bit;
        public SynthWishes With8Bit() { Config.With8Bit(); return this; }

        public int ChannelsEmpty => ConfigWishes.ChannelsEmpty;
        public int MonoChannels => ConfigWishes.MonoChannels;
        public int StereoChannels => ConfigWishes.StereoChannels;
        public int GetChannels => Config.GetChannels;
        public SynthWishes WithChannels(int? channels) { Config.WithChannels(channels); return this; }
        public bool IsMono => Config.IsMono;
        public SynthWishes WithMono() { Config.WithMono(); return this; }
        public bool IsStereo => Config.IsStereo;
        public SynthWishes WithStereo() { Config.WithStereo(); return this; }

        public int CenterChannel => ConfigWishes.CenterChannel;
        public int LeftChannel => ConfigWishes.LeftChannel;
        public int RightChannel => ConfigWishes.RightChannel;
        public int? EveryChannel => ConfigWishes.EveryChannel;
        public int? ChannelEmpty => ConfigWishes.ChannelEmpty;
        public int? GetChannel => Config.GetChannel;
        public SynthWishes WithChannel(int? channel) { Config.WithChannel(channel); return this; }
        public bool IsLeft => Config.IsLeft;
        public SynthWishes WithLeft() { Config.WithLeft(); return this; }
        public bool IsRight => Config.IsRight;
        public SynthWishes WithRight()  { Config.WithRight(); return this; }
        public bool IsCenter => Config.IsCenter;
        public SynthWishes WithCenter()  { Config.WithCenter(); return this; }
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int GetSamplingRate => Config.GetSamplingRate;
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public SynthWishes WithSamplingRate(int? value) { Config.WithSamplingRate(value); return this; }
        
        public AudioFileFormatEnum GetAudioFormat => Config.GetAudioFormat;
        public SynthWishes WithAudioFormat(AudioFileFormatEnum? audioFormat) { Config.WithAudioFormat(audioFormat); return this; }
        public bool IsWav => Config.IsWav;
        public SynthWishes AsWav() { Config.AsWav(); return this; }
        public bool IsRaw => Config.IsRaw;
        public SynthWishes AsRaw() { Config.AsRaw(); return this; }
        
        public InterpolationTypeEnum GetInterpolation => Config.GetInterpolation;
        public SynthWishes WithInterpolation(InterpolationTypeEnum? interpolation) { Config.WithInterpolation(interpolation); return this; }
        public bool IsLinear => Config.IsLinear;
        public SynthWishes WithLinear() {Config.WithLinear(); return this; }
        public bool IsBlocky => Config.IsBlocky;
        public SynthWishes WithBlocky() { Config.WithBlocky(); return this; }

        // Durations
        
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength() => Config.GetNoteLength(this);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength(FlowNode noteLength) => Config.GetNoteLength(this, noteLength);
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes WithNoteLength(FlowNode seconds) { Config.WithNoteLength(seconds); return this; }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes WithNoteLength(double seconds) { Config.WithNoteLength(seconds, this); return this; }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes ResetNoteLength() { Config.ResetNoteLength(); return this; }
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot() 
            => Config.GetNoteLengthSnapShot(this);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(double time) 
            => Config.GetNoteLengthSnapShot(this, time);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(double time, int channel) 
            => Config.GetNoteLengthSnapShot(this, time, channel);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength) 
            => Config.GetNoteLengthSnapShot(this, noteLength);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time) 
            => Config.GetNoteLengthSnapShot(this, noteLength, time);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time, int channel) 
            => Config.GetNoteLengthSnapShot(this, noteLength, time, channel);

        public FlowNode GetBarLength => Config.GetBarLength(this);
        public SynthWishes WithBarLength(FlowNode seconds) { Config.WithBarLength(seconds); return this; }
        public SynthWishes WithBarLength(double seconds) { Config.WithBarLength(seconds, this); return this; }
        public SynthWishes ResetBarLength() { Config.ResetBarLength(); return this; }
        
        public FlowNode GetBeatLength => Config.GetBeatLength(this);
        public SynthWishes WithBeatLength(FlowNode seconds) { Config.WithBeatLength(seconds); return this; }
        public SynthWishes WithBeatLength(double seconds) { Config.WithBeatLength(seconds, this); return this; }
        public SynthWishes ResetBeatLength() { Config.ResetBeatLength(); return this; }

        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength => Config.GetAudioLength(this);
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength(double? newLength) { Config.WithAudioLength(newLength, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength(FlowNode newLength) { Config.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddAudioLength(double additionalLength) { Config.AddAudioLength(additionalLength, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddAudioLength(FlowNode additionalLength) { Config.AddAudioLength(additionalLength, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddEchoDuration(int count = 4, FlowNode delay = default) { Config.AddEchoDuration(count, delay, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddEchoDuration(int count, double delay) { Config.AddEchoDuration(count, delay, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes EnsureAudioLength(double audioLengthNeeded) { Config.EnsureAudioLength(audioLengthNeeded, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes EnsureAudioLength(FlowNode audioLengthNeeded) { Config.EnsureAudioLength(audioLengthNeeded, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes ResetAudioLength() { Config.ResetAudioLength(); return this; }

        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetLeadingSilence => Config.GetLeadingSilence(this);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithLeadingSilence(double seconds) { Config.WithLeadingSilence(seconds, this); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithLeadingSilence(FlowNode seconds) { Config.WithLeadingSilence(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes ResetLeadingSilence() { Config.ResetLeadingSilence(); return this; }
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetTrailingSilence => Config.GetTrailingSilence(this);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithTrailingSilence(double seconds) { Config.WithTrailingSilence(seconds, this); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithTrailingSilence(FlowNode seconds) { Config.WithTrailingSilence(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes ResetTrailingSilence() { Config.ResetTrailingSilence(); return this; }

        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetPaddingOrNull => Config.GetPaddingOrNull(this);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithPadding(double seconds) { Config.WithPadding(seconds, this); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithPadding(FlowNode seconds) { Config.WithPadding(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes ResetPadding() { Config.ResetPadding(); return this; }

        // Feature Toggles
        
        /// <inheritdoc cref="docs._audioplayback" />
        public bool GetAudioPlayback(string fileExtension = null) => Config.GetAudioPlayback(fileExtension);
        /// <inheritdoc cref="docs._audioplayback" />
        public SynthWishes WithAudioPlayback(bool? enabled = true) { Config.WithAudioPlayback(enabled); return this; }

        /// <inheritdoc cref="docs._diskcache" />
        public bool GetDiskCache => Config.GetDiskCache;
        /// <inheritdoc cref="docs._diskcache" />
        public SynthWishes WithDiskCache(bool? enabled = true) { Config.WithDiskCache(enabled); return this; }
        
        public bool GetMathBoost => Config.GetMathBoost;
        public SynthWishes WithMathBoost(bool? enabled = true) { Config.WithMathBoost(enabled); return this; }
        
        /// <inheritdoc cref="docs._parallelprocessing" />
        public bool GetParallelProcessing => Config.GetParallelProcessing;
        /// <inheritdoc cref="docs._parallelprocessing" />
        public SynthWishes WithParallelProcessing(bool? enabled = true) { Config.WithParallelProcessing(enabled); return this; }

        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => Config.GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes WithPlayAllTapes(bool? enabled = true) { Config.WithPlayAllTapes(enabled); return this; }

        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => Config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes WithLeafCheckTimeOut(double? seconds) { Config.WithLeafCheckTimeOut(seconds); return this; }

        /// <inheritdoc cref="docs._leafchecktimeout" />
        public TimeOutActionEnum GetTimeOutAction => Config.GetTimeOutAction;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes WithTimeOutAction(TimeOutActionEnum? action) { Config.WithTimeOutAction(action); return this; }
        
        /// <inheritdoc cref="docs._courtesyframes" />
        public int GetCourtesyFrames => Config.GetCourtesyFrames;
        /// <inheritdoc cref="docs._courtesyframes" />
        public SynthWishes WithCourtesyFrames(int? value) { Config.WithCourtesyFrames(value); return this; }
        
        /// <inheritdoc cref="docs._fileextensionmaxlength" />
        public int GetFileExtensionMaxLength => Config.GetFileExtensionMaxLength;
    }
    
    // FlowNode ConfigWishes

    public partial class FlowNode
    {
        // Audio Quality
        
        public int GetBits => _synthWishes.GetBits;
        public FlowNode WithBits(int? bits) { _synthWishes.WithBits(bits); return this; }
        public bool Is32Bit => _synthWishes.Is32Bit;
        public FlowNode With32Bit() { _synthWishes.With32Bit(); return this; }
        public bool Is16Bit => _synthWishes.Is16Bit;
        public FlowNode With16Bit() { _synthWishes.With16Bit(); return this; }
        public bool Is8Bit => _synthWishes.Is8Bit;
        public FlowNode With8Bit() { _synthWishes.With8Bit(); return this; }

        public int ChannelsEmpty => ConfigWishes.ChannelsEmpty;
        public int MonoChannels => ConfigWishes.MonoChannels;
        public int StereoChannels => ConfigWishes.StereoChannels;
        public int GetChannels => _synthWishes.GetChannels;
        public FlowNode WithChannels(int? channels) { _synthWishes.WithChannels(channels); return this; }
        public bool IsMono => _synthWishes.IsMono;
        public FlowNode WithMono() { _synthWishes.WithMono(); return this; }
        public bool IsStereo => _synthWishes.IsStereo;
        public FlowNode WithStereo() { _synthWishes.WithStereo(); return this; }

        public int CenterChannel => ConfigWishes.CenterChannel;
        public int LeftChannel => ConfigWishes.LeftChannel;
        public int RightChannel => ConfigWishes.RightChannel;
        public int? EveryChannel => ConfigWishes.EveryChannel;
        public int? ChannelEmpty => ConfigWishes.ChannelEmpty;
        public int? GetChannel => _synthWishes.GetChannel;
        public FlowNode WithChannel(int? channel) { _synthWishes.WithChannel(channel); return this; }
        public bool IsLeft => _synthWishes.IsLeft;
        public FlowNode WithLeft()  { _synthWishes.WithLeft(); return this; }
        public bool IsRight => _synthWishes.IsRight;
        public FlowNode WithRight() { _synthWishes.WithRight(); return this; }
        public bool IsCenter => _synthWishes.IsCenter;
        public FlowNode WithCenter() { _synthWishes.WithCenter(); return this; }
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int GetSamplingRate => _synthWishes.GetSamplingRate;
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public FlowNode WithSamplingRate(int? value) { _synthWishes.WithSamplingRate(value); return this; }

        public AudioFileFormatEnum GetAudioFormat => _synthWishes.GetAudioFormat;
        public FlowNode WithAudioFormat(AudioFileFormatEnum? audioFormat) { _synthWishes.WithAudioFormat(audioFormat); return this; }
        public bool IsWav => _synthWishes.IsWav;
        public FlowNode AsWav() { _synthWishes.AsWav(); return this; }
        public bool IsRaw => _synthWishes.IsRaw;
        public FlowNode AsRaw() { _synthWishes.AsRaw(); return this; }

        public InterpolationTypeEnum GetInterpolation => _synthWishes.GetInterpolation;
        public FlowNode WithInterpolation(InterpolationTypeEnum? interpolation) { _synthWishes.WithInterpolation(interpolation); return this; }
        public bool IsLinear => _synthWishes.IsLinear;
        public FlowNode WithLinear() { _synthWishes.WithLinear(); return this; }
        public bool IsBlocky => _synthWishes.IsBlocky;
        public FlowNode WithBlocky() { _synthWishes.WithBlocky(); return this; }
        
        // Durations
        
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength() => _synthWishes.GetNoteLength();
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength(FlowNode noteLength) => _synthWishes.GetNoteLength(noteLength);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode WithNoteLength(FlowNode newLength) { _synthWishes.WithNoteLength(newLength); return this; }
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode WithNoteLength(double newLength) { _synthWishes.WithNoteLength(newLength); return this; }
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode ResetNoteLength() { _synthWishes.ResetNoteLength(); return this; }
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot() 
            => _synthWishes.GetNoteLengthSnapShot();
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(double time) 
            => _synthWishes.GetNoteLengthSnapShot(time);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(double time, int channel) 
            => _synthWishes.GetNoteLengthSnapShot(time, channel);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength) 
            => _synthWishes.GetNoteLengthSnapShot(noteLength);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time) 
            => _synthWishes.GetNoteLengthSnapShot(noteLength, time);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time, int channel) 
            => _synthWishes.GetNoteLengthSnapShot(noteLength, time, channel);

        public FlowNode GetBarLength => _synthWishes.GetBarLength;
        public FlowNode WithBarLength(FlowNode newLength) { _synthWishes.WithBarLength(newLength); return this; }
        public FlowNode WithBarLength(double newLength) { _synthWishes.WithBarLength(newLength); return this; }
        public FlowNode ResetBarLength() { _synthWishes.ResetBarLength(); return this; }

        public FlowNode GetBeatLength => _synthWishes.GetBeatLength;
        public FlowNode WithBeatLength(FlowNode newLength) { _synthWishes.WithBeatLength(newLength); return this; }
        public FlowNode WithBeatLength(double newLength) { _synthWishes.WithBeatLength(newLength); return this; }
        public FlowNode ResetBeatLength() { _synthWishes.ResetBeatLength(); return this; }

        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength => _synthWishes.GetAudioLength;
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode WithAudioLength(FlowNode newLength) { _synthWishes.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode WithAudioLength(double? newLength) { _synthWishes.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode AddAudioLength(FlowNode additionalLength) { _synthWishes.AddAudioLength(additionalLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode AddAudioLength(double additionalLength) { _synthWishes.AddAudioLength(additionalLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode AddEchoDuration(int count = 4, FlowNode delay = default) { _synthWishes.AddEchoDuration(count, delay); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode AddEchoDuration(int count, double delay) { _synthWishes.AddEchoDuration(count, delay); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode EnsureAudioLength(FlowNode audioLengthNeeded) { _synthWishes.EnsureAudioLength(audioLengthNeeded); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode EnsureAudioLength(double audioLengthNeeded) { _synthWishes.EnsureAudioLength(audioLengthNeeded); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode ResetAudioLength() { _synthWishes.ResetAudioLength(); return this; }
                
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetLeadingSilence => _synthWishes.GetLeadingSilence;
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode WithLeadingSilence(double seconds) { _synthWishes.WithLeadingSilence(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode WithLeadingSilence(FlowNode seconds) { _synthWishes.WithLeadingSilence(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode ResetLeadingSilence() { _synthWishes.ResetLeadingSilence(); return this; }
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetTrailingSilence => _synthWishes.GetTrailingSilence;
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode WithTrailingSilence(double seconds) { _synthWishes.WithTrailingSilence(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode WithTrailingSilence(FlowNode seconds) { _synthWishes.WithTrailingSilence(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode ResetTrailingSilence() { _synthWishes.ResetTrailingSilence(); return this; }
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetPaddingOrNull => _synthWishes.GetPaddingOrNull;
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode WithPadding(double seconds) { _synthWishes.WithPadding(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode WithPadding(FlowNode seconds) { _synthWishes.WithPadding(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode ResetPadding() { _synthWishes.ResetPadding(); return this; }
        
        // Feature Toggles
        
        /// <inheritdoc cref="docs._audioplayback" />
        public bool GetAudioPlayback(string fileExtension = null) => _synthWishes.GetAudioPlayback(fileExtension);
        /// <inheritdoc cref="docs._audioplayback" />
        public FlowNode WithAudioPlayback(bool? enabled = true) { _synthWishes.WithAudioPlayback(enabled); return this; }

        /// <inheritdoc cref="docs._diskcache" />
        public bool GetDiskCache => _synthWishes.GetDiskCache;
        /// <inheritdoc cref="docs._diskcache" />
        public FlowNode WithDiskCache(bool? enabled = true) { _synthWishes.WithDiskCache(enabled); return this; }
        
        public bool GetMathBoost => _synthWishes.GetMathBoost;
        public FlowNode WithMathBoost(bool? enabled = true) { _synthWishes.WithMathBoost(enabled); return this; }
        
        /// <inheritdoc cref="docs._parallelprocessing" />
        public bool GetParallelProcessing => _synthWishes.GetParallelProcessing;
        /// <inheritdoc cref="docs._parallelprocessing" />
        public FlowNode WithParallelProcessing(bool? enabled = true) { _synthWishes.WithParallelProcessing(enabled); return this; }

        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => _synthWishes.GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public FlowNode WithPlayAllTapes(bool? enabled = true) { _synthWishes.WithPlayAllTapes(enabled); return this; }
 
        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => _synthWishes.GetLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public FlowNode WithLeafCheckTimeOut(double? seconds) { _synthWishes.WithLeafCheckTimeOut(seconds); return this; }
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public TimeOutActionEnum GetTimeOutAction => _synthWishes.GetTimeOutAction;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public FlowNode WithTimeOutAction(TimeOutActionEnum? action) { _synthWishes.WithTimeOutAction(action); return this; }
        
        /// <inheritdoc cref="docs._courtesyframes" />
        public int GetCourtesyFrames => _synthWishes.GetCourtesyFrames;
        /// <inheritdoc cref="docs._courtesyframes" />
        public FlowNode WithCourtesyFrames(int? value) { _synthWishes.WithCourtesyFrames(value); return this; }
        
        /// <inheritdoc cref="docs._fileextensionmaxlength" />
        public int GetFileExtensionMaxLength => _synthWishes.GetFileExtensionMaxLength;
    }
}