using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Config;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;

// ReSharper disable once CheckNamespace

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Audio Quality

        public bool Is8Bit => Config.Is8Bit;
        public bool Is16Bit => Config.Is16Bit;
        public bool Is32Bit => Config.Is32Bit;
        protected int Bits() => ConfigWishes.Bits(this);
        public int GetBits => Config.GetBits;

        protected SynthWishes With8Bit() => ConfigWishes.With8Bit(this);
        protected SynthWishes With16Bit() => ConfigWishes.With16Bit(this);
        protected SynthWishes With32Bit() => ConfigWishes.With32Bit(this);
        protected SynthWishes As8Bit() => ConfigWishes.As8Bit(this);
        protected SynthWishes As16Bit() => ConfigWishes.As16Bit(this);
        protected SynthWishes As32Bit() => ConfigWishes.As32Bit(this);
        protected SynthWishes Set8Bit() => ConfigWishes.Set8Bit(this);
        protected SynthWishes Set16Bit() => ConfigWishes.Set16Bit(this);
        protected SynthWishes Set32Bit() => ConfigWishes.Set32Bit(this);
        protected SynthWishes Bits(int? bits) => ConfigWishes.Bits(this, bits);
        public SynthWishes WithBits(int? bits) { Config.WithBits(bits); return this; }
        protected SynthWishes AsBits(int? bits) => ConfigWishes.AsBits(this, bits);
        protected SynthWishes SetBits(int? bits) => ConfigWishes.SetBits(this, bits);

        public int Channels() => Config.GetChannels;
        public int GetChannels => Config.GetChannels;
        public int NoChannels => Config.NoChannels;
        public int MonoChannels => Config.MonoChannels;
        public int StereoChannels => Config.StereoChannels;
        public bool IsMono => Config.IsMono;
        public bool IsStereo => Config.IsStereo;
        public SynthWishes WithChannels(int? channels) { Config.WithChannels(channels); return this; }
        protected SynthWishes SetChannels(int? channels) => WithChannels(channels);
        protected SynthWishes Channels(int? channels) => WithChannels(channels);
        protected SynthWishes WithMono() => WithChannels(MonoChannels);
        protected SynthWishes SetMono() => WithChannels(MonoChannels);
        protected SynthWishes AsMono() => WithChannels(MonoChannels);
        protected SynthWishes Mono() => WithChannels(MonoChannels);
        protected SynthWishes WithStereo() => WithChannels(StereoChannels);
        protected SynthWishes SetStereo() => WithChannels(StereoChannels);
        protected SynthWishes AsStereo() => WithChannels(StereoChannels);
        protected SynthWishes Stereo() => WithChannels(StereoChannels);
        
        public int? Channel() => Config.GetChannel;
        public int? GetChannel => Config.GetChannel;
        public int CenterChannel => Config.CenterChannel;
        public int LeftChannel => Config.LeftChannel;
        public int RightChannel => Config.RightChannel;
        public int? AnyChannel => Config.AnyChannel;
        public int? EveryChannel => Config.EveryChannel;
        public int? ChannelEmpty => Config.ChannelEmpty;
        public bool IsLeft => Config.IsLeft;
        public bool IsRight => Config.IsRight;
        public bool IsCenter => Config.IsCenter;
        public SynthWishes WithChannel(int? channel) { Config.WithChannel(channel); return this; }
        public SynthWishes SetChannel(int? channel) => WithChannel(channel);
        public SynthWishes Channel(int? channel) => WithChannel(channel);
        // WARNING: Route to WithLeft not WithChannel; unexpected behavior differences.
        public SynthWishes WithLeft() { Config.WithLeft(); return this; }
        public SynthWishes SetLeft() => WithLeft();
        public SynthWishes AsLeft() => WithLeft();
        public SynthWishes Left() => WithLeft();
        public SynthWishes WithRight() { Config.WithRight(); return this; }
        public SynthWishes SetRight() => WithRight();
        public SynthWishes AsRight() => WithRight();
        public SynthWishes Right() => WithRight();
        public SynthWishes WithCenter() { Config.WithCenter(); return this; }
        public SynthWishes SetCenter() => WithCenter();
        public SynthWishes AsCenter() => WithCenter();
        public SynthWishes Center() => WithCenter();

        /// <inheritdoc cref="docs._getsamplingrate" />
        public int SamplingRate() => Config.GetSamplingRate;
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int GetSamplingRate => Config.GetSamplingRate;
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public SynthWishes WithSamplingRate(int? value) { Config.WithSamplingRate(value); return this; }
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public SynthWishes SetSamplingRate(int? value) => WithSamplingRate(value);
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public SynthWishes SamplingRate(int? value) => WithSamplingRate(value);
        
        public bool IsWav => Config.IsWav;
        public bool IsRaw => Config.IsRaw;
        protected AudioFileFormatEnum AudioFormat() => ConfigWishes.AudioFormat(this);
        public AudioFileFormatEnum GetAudioFormat => Config.GetAudioFormat;

        protected SynthWishes WithWav() => ConfigWishes.WithWav(this);
        protected SynthWishes AsWav() => ConfigWishes.AsWav(this);
        protected SynthWishes SetWav() => ConfigWishes.SetWav(this);
        protected SynthWishes WithRaw() => ConfigWishes.WithRaw(this);
        protected SynthWishes AsRaw() => ConfigWishes.AsRaw(this);
        protected SynthWishes SetRaw() => ConfigWishes.SetRaw(this);
        protected SynthWishes AudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.AudioFormat(this, audioFormat);
        public SynthWishes WithAudioFormat(AudioFileFormatEnum? audioFormat) { Config.WithAudioFormat(audioFormat); return this; }
        protected SynthWishes AsAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.AsAudioFormat(this, audioFormat);
        protected SynthWishes SetAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.SetAudioFormat(this, audioFormat);

        public InterpolationTypeEnum Interpolation() => Config.GetInterpolation;
        public InterpolationTypeEnum GetInterpolation => Config.GetInterpolation;
        public bool IsLinear => Config.IsLinear;
        public bool IsBlocky => Config.IsBlocky;
        public SynthWishes WithInterpolation(InterpolationTypeEnum? interpolation) { Config.WithInterpolation(interpolation); return this; }
        public SynthWishes SetInterpolation(InterpolationTypeEnum? interpolation) => WithInterpolation(interpolation);
        public SynthWishes AsInterpolation(InterpolationTypeEnum? interpolation) => WithInterpolation(interpolation);
        public SynthWishes Interpolation(InterpolationTypeEnum? interpolation) => WithInterpolation(interpolation);
        public SynthWishes WithLinear() => WithInterpolation(Line);
        public SynthWishes SetLinear() => WithInterpolation(Line);
        public SynthWishes AsLinear() => WithInterpolation(Line);
        public SynthWishes Linear() => WithInterpolation(Line);
        public SynthWishes WithBlocky() => WithInterpolation(Block);
        public SynthWishes SetBlocky() => WithInterpolation(Block);
        public SynthWishes AsBlocky() => WithInterpolation(Block);
        public SynthWishes Blocky() => WithInterpolation(Block);

        // Durations
        
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode NoteLength() => GetNoteLength();
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength() => Config.GetNoteLength(this);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength(FlowNode noteLength) => Config.GetNoteLength(this, noteLength);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot() => Config.GetNoteLengthSnapShot(this);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(double time) => Config.GetNoteLengthSnapShot(this, time);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(double time, int channel) => Config.GetNoteLengthSnapShot(this, time, channel);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength) => Config.GetNoteLengthSnapShot(this, noteLength);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time) => Config.GetNoteLengthSnapShot(this, noteLength, time);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time, int channel) => Config.GetNoteLengthSnapShot(this, noteLength, time, channel);
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes WithNoteLength(FlowNode seconds) { Config.WithNoteLength(seconds); return this; }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes SetNoteLength(FlowNode seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes NoteLength(FlowNode seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes WithNoteLength(double seconds) { Config.WithNoteLength(seconds, this); return this; }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes SetNoteLength(double seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes NoteLength(double seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes ResetNoteLength() { Config.ResetNoteLength(); return this; }

        public FlowNode BarLength() => GetBarLength;
        public FlowNode GetBarLength => Config.GetBarLength(this);
        public SynthWishes WithBarLength(FlowNode seconds) { Config.WithBarLength(seconds); return this; }
        public SynthWishes SetBarLength(FlowNode seconds) => WithBarLength(seconds);
        public SynthWishes BarLength(FlowNode seconds) => WithBarLength(seconds);
        public SynthWishes WithBarLength(double seconds) { Config.WithBarLength(seconds, this); return this; }
        public SynthWishes SetBarLength(double seconds) => WithBarLength(seconds);
        public SynthWishes BarLength(double seconds) => WithBarLength(seconds);
        public SynthWishes ResetBarLength() { Config.ResetBarLength(); return this; }
        
        public FlowNode BeatLength() => GetBeatLength;
        public FlowNode GetBeatLength => Config.GetBeatLength(this);
        public SynthWishes WithBeatLength(FlowNode seconds) { Config.WithBeatLength(seconds); return this; }
        public SynthWishes SetBeatLength(FlowNode seconds) => WithBeatLength(seconds);
        public SynthWishes BeatLength(FlowNode seconds) => WithBeatLength(seconds);
        public SynthWishes WithBeatLength(double seconds) { Config.WithBeatLength(seconds, this); return this; }
        public SynthWishes SetBeatLength(double seconds) => WithBeatLength(seconds);
        public SynthWishes BeatLength(double seconds) => WithBeatLength(seconds);
        public SynthWishes ResetBeatLength() { Config.ResetBeatLength(); return this; }

        /// <inheritdoc cref="docs._audiolength" />
        protected FlowNode AudioLength() => ConfigWishes.AudioLength(this);
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength => Config.GetAudioLength(this);
        /// <inheritdoc cref="docs._audiolength" />
        protected SynthWishes AudioLength(double? newLength) => ConfigWishes.AudioLength(this, newLength);
        /// <inheritdoc cref="docs._audiolength" />
        protected SynthWishes AudioLength(FlowNode newLength) => ConfigWishes.AudioLength(this, newLength);
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength(double? newLength) { Config.WithAudioLength(newLength, this); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength(FlowNode newLength) { Config.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="docs._audiolength" />
        protected SynthWishes SetAudioLength(double? newLength) => ConfigWishes.WithAudioLength(this, newLength);
        /// <inheritdoc cref="docs._audiolength" />
        protected SynthWishes SetAudioLength(FlowNode newLength) => ConfigWishes.WithAudioLength(this, newLength);
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
        public FlowNode LeadingSilence() => GetLeadingSilence;
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetLeadingSilence => Config.GetLeadingSilence(this);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithLeadingSilence(double seconds) { Config.WithLeadingSilence(seconds, this); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetLeadingSilence(double seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes LeadingSilence(double seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithLeadingSilence(FlowNode seconds) { Config.WithLeadingSilence(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetLeadingSilence(FlowNode seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes LeadingSilence(FlowNode seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes ResetLeadingSilence() { Config.ResetLeadingSilence(); return this; }
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode TrailingSilence() => GetTrailingSilence;
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetTrailingSilence => Config.GetTrailingSilence(this);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithTrailingSilence(double seconds) { Config.WithTrailingSilence(seconds, this); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetTrailingSilence(double seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes TrailingSilence(double seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithTrailingSilence(FlowNode seconds) { Config.WithTrailingSilence(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetTrailingSilence(FlowNode seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes TrailingSilence(FlowNode seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes ResetTrailingSilence() { Config.ResetTrailingSilence(); return this; }

        /// <inheritdoc cref="docs._padding"/>
        public FlowNode PaddingOrNull() => GetPaddingOrNull;
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetPaddingOrNull => Config.GetPaddingOrNull(this);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithPadding(double seconds) { Config.WithPadding(seconds, this); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetPadding(double seconds) => WithPadding(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes Padding(double seconds) => WithPadding(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithPadding(FlowNode seconds) { Config.WithPadding(seconds); return this; }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetPadding(FlowNode seconds) => WithPadding(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes Padding(FlowNode seconds) => WithPadding(seconds);
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes ResetPadding() { Config.ResetPadding(); return this; }

        // Feature Toggles
        
        /// <inheritdoc cref="docs._audioplayback" />
        public bool AudioPlayback(string fileExtension = null) => Config.GetAudioPlayback(fileExtension);
        /// <inheritdoc cref="docs._audioplayback" />
        public bool GetAudioPlayback(string fileExtension = null) => Config.GetAudioPlayback(fileExtension);
        /// <inheritdoc cref="docs._audioplayback" />
        public SynthWishes WithAudioPlayback(bool? enabled = true) { Config.WithAudioPlayback(enabled); return this; }
        /// <inheritdoc cref="docs._audioplayback" />
        public SynthWishes SetAudioPlayback(bool? enabled = true) => WithAudioPlayback(enabled);
        /// <inheritdoc cref="docs._audioplayback" />
        public SynthWishes AudioPlayback(bool? enabled = true) => WithAudioPlayback(enabled);

        /// <inheritdoc cref="docs._diskcache" />
        public bool DiskCache() => Config.GetDiskCache;
        /// <inheritdoc cref="docs._diskcache" />
        public bool GetDiskCache => Config.GetDiskCache;
        /// <inheritdoc cref="docs._diskcache" />
        public SynthWishes WithDiskCache(bool? enabled = true) { Config.WithDiskCache(enabled); return this; }
        /// <inheritdoc cref="docs._diskcache" />
        public SynthWishes SetDiskCache(bool? enabled = true) => WithDiskCache(enabled);
        /// <inheritdoc cref="docs._diskcache" />
        public SynthWishes DiskCache(bool? enabled) => WithDiskCache(enabled);

        public bool MathBoost() => GetMathBoost;
        public bool GetMathBoost => Config.GetMathBoost;
        public SynthWishes WithMathBoost(bool? enabled = true) { Config.WithMathBoost(enabled); return this; }
        public SynthWishes SetMathBoost(bool? enabled = true) => WithMathBoost(enabled);
        public SynthWishes MathBoost(bool? enabled) => WithMathBoost(enabled);

        /// <inheritdoc cref="docs._parallelprocessing" />
        public bool ParallelProcessing() => Config.GetParallelProcessing;
        /// <inheritdoc cref="docs._parallelprocessing" />
        public bool GetParallelProcessing => Config.GetParallelProcessing;
        /// <inheritdoc cref="docs._parallelprocessing" />
        public SynthWishes WithParallelProcessing(bool? enabled = true) { Config.WithParallelProcessing(enabled); return this; }
        /// <inheritdoc cref="docs._parallelprocessing" />
        public SynthWishes SetParallelProcessing(bool? enabled = true) => WithParallelProcessing(enabled);
        /// <inheritdoc cref="docs._parallelprocessing" />
        public SynthWishes ParallelProcessing(bool? enabled) => WithParallelProcessing(enabled);

        /// <inheritdoc cref="docs._playalltapes" />
        public bool PlayAllTapes() => Config.GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes => Config.GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes WithPlayAllTapes(bool? enabled = true) { Config.WithPlayAllTapes(enabled); return this; }
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes SetPlayAllTapes(bool? enabled = true) => WithPlayAllTapes(enabled);
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes PlayAllTapes(bool? enabled) => WithPlayAllTapes(enabled);

        // Misc Settings

        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double LeafCheckTimeOut() => Config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double GetLeafCheckTimeOut => Config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes WithLeafCheckTimeOut(double? seconds) { Config.WithLeafCheckTimeOut(seconds); return this; }
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes SetLeafCheckTimeOut(double? seconds) => WithLeafCheckTimeOut(seconds);
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes LeafCheckTimeOut(double? seconds) => WithLeafCheckTimeOut(seconds);

        /// <inheritdoc cref="docs._leafchecktimeout" />
        public TimeOutActionEnum TimeOutAction() => Config.GetTimeOutAction;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public TimeOutActionEnum GetTimeOutAction => Config.GetTimeOutAction;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes WithTimeOutAction(TimeOutActionEnum? action) { Config.WithTimeOutAction(action); return this; }
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes SetTimeOutAction(TimeOutActionEnum? action) => WithTimeOutAction(action);
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes TimeOutAction(TimeOutActionEnum? action) => WithTimeOutAction(action);

        /// <inheritdoc cref="docs._courtesyframes" />
        public int CourtesyFrames() => Config.GetCourtesyFrames;
        /// <inheritdoc cref="docs._courtesyframes" />
        public int GetCourtesyFrames => Config.GetCourtesyFrames;
        /// <inheritdoc cref="docs._courtesyframes" />
        public SynthWishes WithCourtesyFrames(int? value) { Config.WithCourtesyFrames(value); return this; }
        /// <inheritdoc cref="docs._courtesyframes" />
        public SynthWishes SetCourtesyFrames(int? value) => WithCourtesyFrames(value);
        /// <inheritdoc cref="docs._courtesyframes" />
        public SynthWishes CourtesyFrames(int? value) => WithCourtesyFrames(value);
        
        /// <inheritdoc cref="docs._fileextensionmaxlength" />
        public int GetFileExtensionMaxLength => Config.GetFileExtensionMaxLength;
        
        public bool IsUnderNCrunch 
        {
            get => Config.IsUnderNCrunch;
            set => Config.IsUnderNCrunch = value;
        }
        
        public bool IsUnderAzurePipelines
        {
            get => Config.IsUnderAzurePipelines;
            set => Config.IsUnderAzurePipelines = value;
        }
    }
}