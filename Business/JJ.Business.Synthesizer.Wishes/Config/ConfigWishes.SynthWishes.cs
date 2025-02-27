using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.docs;
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
        
        public    int         NoChannels                  => Config.NoChannels;
        public    int         MonoChannels                => Config.MonoChannels;
        public    int         StereoChannels              => Config.StereoChannels;
        public    bool        IsMono                      => Config.IsMono;
        public    bool        IsStereo                    => Config.IsStereo;
        protected int         Channels()                  => ConfigWishes.Channels(this);
        public    int         GetChannels                 => Config.GetChannels;
        protected SynthWishes Mono()                      => ConfigWishes.Mono(this);
        protected SynthWishes Stereo()                    => ConfigWishes.Stereo(this);
        protected SynthWishes Channels(int? channels)     => ConfigWishes.Channels(this, channels);
        protected SynthWishes WithMono()                  => ConfigWishes.WithMono(this);
        protected SynthWishes WithStereo()                => ConfigWishes.WithStereo(this);
        public    SynthWishes WithChannels(int? channels) {  Config.WithChannels(channels); return this; }
        protected SynthWishes AsMono()                    => ConfigWishes.AsMono(this);
        protected SynthWishes AsStereo()                  => ConfigWishes.AsStereo(this);
        protected SynthWishes SetMono()                   => ConfigWishes.SetMono(this);
        protected SynthWishes SetStereo()                 => ConfigWishes.SetStereo(this);
        protected SynthWishes SetChannels(int? channels)  => ConfigWishes.SetChannels(this, channels);
        
        public    int         CenterChannel             => Config.CenterChannel;
        public    int         LeftChannel               => Config.LeftChannel;
        public    int         RightChannel              => Config.RightChannel;
        public    int?        EmptyChannel              => Config.EmptyChannel;
        public    bool        IsCenter                  => Config.IsCenter;
        public    bool        IsLeft                    => Config.IsLeft;
        public    bool        IsRight                   => Config.IsRight;
        public    bool        IsAnyChannel              => Config.IsAnyChannel;
        public    bool        IsEveryChannel            => Config.IsEveryChannel;
        public    bool        IsNoChannel               => Config.IsNoChannel;
        protected int?        Channel()                 => ConfigWishes.Channel(this);
        public    int?        GetChannel                => Config.GetChannel;
        protected SynthWishes Center()                  => ConfigWishes.Center(this);
        public    SynthWishes WithCenter()              {  Config.WithCenter(); return this; }
        protected SynthWishes AsCenter()                => ConfigWishes.AsCenter(this);
        protected SynthWishes Left()                    => ConfigWishes.Left(this);
        public    SynthWishes WithLeft()                {  Config.WithLeft(); return this; }
        protected SynthWishes AsLeft()                  => ConfigWishes.AsLeft(this);
        protected SynthWishes Right()                   => ConfigWishes.Right(this);
        public    SynthWishes WithRight()               {  Config.WithRight(); return this; }
        protected SynthWishes AsRight()                 => ConfigWishes.AsRight(this);
        protected SynthWishes NoChannel()               => ConfigWishes.NoChannel(this);
        protected SynthWishes WithNoChannel()           => ConfigWishes.WithNoChannel(this);
        protected SynthWishes AsNoChannel()             => ConfigWishes.AsNoChannel(this);
        protected SynthWishes Channel(int?     channel) => ConfigWishes.Channel(this, channel);
        public    SynthWishes WithChannel(int? channel) {  Config.WithChannel(channel); return this; }
        protected SynthWishes AsChannel(int?   channel) => ConfigWishes.AsChannel(this, channel);
        protected SynthWishes SetCenter()               => ConfigWishes.SetCenter(this);
        protected SynthWishes SetLeft()                 => ConfigWishes.SetLeft(this);
        protected SynthWishes SetRight()                => ConfigWishes.SetRight(this);
        protected SynthWishes SetNoChannel()            => ConfigWishes.SetNoChannel(this);
        protected SynthWishes SetChannel(int? channel)  => ConfigWishes.SetChannel(this, channel);

        /// <inheritdoc cref="_getsamplingrate" />
        protected int SamplingRate() => ConfigWishes.SamplingRate(this);
        /// <inheritdoc cref="_getsamplingrate" />
        public int GetSamplingRate => Config.GetSamplingRate;
        /// <inheritdoc cref="_withsamplingrate"/>
        protected SynthWishes SamplingRate(int? value) => ConfigWishes.SamplingRate(this, value);
        /// <inheritdoc cref="_withsamplingrate"/>
        public SynthWishes WithSamplingRate(int? value) { Config.WithSamplingRate(value); return this; }
        /// <inheritdoc cref="_withsamplingrate"/>
        protected SynthWishes SetSamplingRate(int? value) => ConfigWishes.SetSamplingRate(this, value);

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

        public bool IsLinear => Config.IsLinear;
        public bool IsBlocky => Config.IsBlocky;
        protected InterpolationTypeEnum Interpolation() => ConfigWishes.Interpolation(this);
        public InterpolationTypeEnum GetInterpolation => Config.GetInterpolation;
        protected SynthWishes Linear() => ConfigWishes.Linear(this);
        protected SynthWishes Blocky() => ConfigWishes.Blocky(this);
        protected SynthWishes WithLinear() => ConfigWishes.WithLinear(this);
        protected SynthWishes WithBlocky() => ConfigWishes.WithBlocky(this);
        protected SynthWishes AsLinear() => ConfigWishes.AsLinear(this);
        protected SynthWishes AsBlocky() => ConfigWishes.AsBlocky(this);
        protected SynthWishes SetLinear() => ConfigWishes.SetLinear(this);
        protected SynthWishes SetBlocky() => ConfigWishes.SetBlocky(this);
        protected SynthWishes Interpolation(InterpolationTypeEnum? interpolation) => ConfigWishes.Interpolation(this, interpolation);
        public SynthWishes WithInterpolation(InterpolationTypeEnum? interpolation) { Config.WithInterpolation(interpolation); return this; }
        protected SynthWishes AsInterpolation(InterpolationTypeEnum? interpolation) => ConfigWishes.AsInterpolation(this, interpolation);
        protected SynthWishes SetInterpolation(InterpolationTypeEnum? interpolation) => ConfigWishes.SetInterpolation(this, interpolation);

        // Durations

        /// <inheritdoc cref="_notelength" />
        public FlowNode NoteLength() => GetNoteLength();
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLength() => Config.GetNoteLength(this);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLength(FlowNode noteLength) => Config.GetNoteLength(this, noteLength);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot() => Config.GetNoteLengthSnapShot(this);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(double time) => Config.GetNoteLengthSnapShot(this, time);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(double time, int channel) => Config.GetNoteLengthSnapShot(this, time, channel);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength) => Config.GetNoteLengthSnapShot(this, noteLength);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time) => Config.GetNoteLengthSnapShot(this, noteLength, time);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time, int channel) => Config.GetNoteLengthSnapShot(this, noteLength, time, channel);
        /// <inheritdoc cref="_notelength" />
        public SynthWishes WithNoteLength(FlowNode seconds) { Config.WithNoteLength(seconds); return this; }
        /// <inheritdoc cref="_notelength" />
        public SynthWishes SetNoteLength(FlowNode seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="_notelength" />
        public SynthWishes NoteLength(FlowNode seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="_notelength" />
        public SynthWishes WithNoteLength(double seconds) { Config.WithNoteLength(seconds, this); return this; }
        /// <inheritdoc cref="_notelength" />
        public SynthWishes SetNoteLength(double seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="_notelength" />
        public SynthWishes NoteLength(double seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="_notelength" />
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

        /// <inheritdoc cref="_audiolength" />
        protected FlowNode AudioLength() => ConfigWishes.AudioLength(this);
        /// <inheritdoc cref="_audiolength" />
        public FlowNode GetAudioLength => Config.GetAudioLength(this);
        /// <inheritdoc cref="_audiolength" />
        protected SynthWishes AudioLength(double? newLength) => ConfigWishes.AudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        protected SynthWishes AudioLength(FlowNode newLength) => ConfigWishes.AudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes WithAudioLength(double? newLength) { Config.WithAudioLength(newLength, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes WithAudioLength(FlowNode newLength) { Config.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="_audiolength" />
        protected SynthWishes SetAudioLength(double? newLength) => ConfigWishes.WithAudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        protected SynthWishes SetAudioLength(FlowNode newLength) => ConfigWishes.WithAudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AddAudioLength(double additionalLength) { Config.AddAudioLength(additionalLength, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AddAudioLength(FlowNode additionalLength) { Config.AddAudioLength(additionalLength, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AddEchoDuration(int count = 4, FlowNode delay = default) { Config.AddEchoDuration(count, delay, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AddEchoDuration(int count, double delay) { Config.AddEchoDuration(count, delay, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes EnsureAudioLength(double audioLengthNeeded) { Config.EnsureAudioLength(audioLengthNeeded, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes EnsureAudioLength(FlowNode audioLengthNeeded) { Config.EnsureAudioLength(audioLengthNeeded, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes ResetAudioLength() { Config.ResetAudioLength(); return this; }

        /// <inheritdoc cref="_padding"/>
        public FlowNode LeadingSilence() => GetLeadingSilence;
        /// <inheritdoc cref="_padding"/>
        public FlowNode GetLeadingSilence => Config.GetLeadingSilence(this);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithLeadingSilence(double seconds) { Config.WithLeadingSilence(seconds, this); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetLeadingSilence(double seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes LeadingSilence(double seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithLeadingSilence(FlowNode seconds) { Config.WithLeadingSilence(seconds); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetLeadingSilence(FlowNode seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes LeadingSilence(FlowNode seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes ResetLeadingSilence() { Config.ResetLeadingSilence(); return this; }
        
        /// <inheritdoc cref="_padding"/>
        public FlowNode TrailingSilence() => GetTrailingSilence;
        /// <inheritdoc cref="_padding"/>
        public FlowNode GetTrailingSilence => Config.GetTrailingSilence(this);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithTrailingSilence(double seconds) { Config.WithTrailingSilence(seconds, this); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetTrailingSilence(double seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes TrailingSilence(double seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithTrailingSilence(FlowNode seconds) { Config.WithTrailingSilence(seconds); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetTrailingSilence(FlowNode seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes TrailingSilence(FlowNode seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes ResetTrailingSilence() { Config.ResetTrailingSilence(); return this; }

        /// <inheritdoc cref="_padding"/>
        public FlowNode PaddingOrNull() => GetPaddingOrNull;
        /// <inheritdoc cref="_padding"/>
        public FlowNode GetPaddingOrNull => Config.GetPaddingOrNull(this);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithPadding(double seconds) { Config.WithPadding(seconds, this); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetPadding(double seconds) => WithPadding(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes Padding(double seconds) => WithPadding(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithPadding(FlowNode seconds) { Config.WithPadding(seconds); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetPadding(FlowNode seconds) => WithPadding(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes Padding(FlowNode seconds) => WithPadding(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes ResetPadding() { Config.ResetPadding(); return this; }
        
        // Feature Toggles
        
        /// <inheritdoc cref="_audioplayback" />
        public bool AudioPlayback(string fileExtension = null) => Config.GetAudioPlayback(fileExtension);
        /// <inheritdoc cref="_audioplayback" />
        public bool GetAudioPlayback(string fileExtension = null) => Config.GetAudioPlayback(fileExtension);
        /// <inheritdoc cref="_audioplayback" />
        public SynthWishes WithAudioPlayback(bool? enabled = true) { Config.WithAudioPlayback(enabled); return this; }
        /// <inheritdoc cref="_audioplayback" />
        public SynthWishes SetAudioPlayback(bool? enabled = true) => WithAudioPlayback(enabled);
        /// <inheritdoc cref="_audioplayback" />
        public SynthWishes AudioPlayback(bool? enabled = true) => WithAudioPlayback(enabled);

        /// <inheritdoc cref="_diskcache" />
        public bool DiskCache() => Config.GetDiskCache;
        /// <inheritdoc cref="_diskcache" />
        public bool GetDiskCache => Config.GetDiskCache;
        /// <inheritdoc cref="_diskcache" />
        public SynthWishes WithDiskCache(bool? enabled = true) { Config.WithDiskCache(enabled); return this; }
        /// <inheritdoc cref="_diskcache" />
        public SynthWishes SetDiskCache(bool? enabled = true) => WithDiskCache(enabled);
        /// <inheritdoc cref="_diskcache" />
        public SynthWishes DiskCache(bool? enabled) => WithDiskCache(enabled);

        public bool MathBoost() => GetMathBoost;
        public bool GetMathBoost => Config.GetMathBoost;
        public SynthWishes WithMathBoost(bool? enabled = true) { Config.WithMathBoost(enabled); return this; }
        public SynthWishes SetMathBoost(bool? enabled = true) => WithMathBoost(enabled);
        public SynthWishes MathBoost(bool? enabled) => WithMathBoost(enabled);

        /// <inheritdoc cref="_parallelprocessing" />
        public bool ParallelProcessing() => Config.GetParallelProcessing;
        /// <inheritdoc cref="_parallelprocessing" />
        public bool GetParallelProcessing => Config.GetParallelProcessing;
        /// <inheritdoc cref="_parallelprocessing" />
        public SynthWishes WithParallelProcessing(bool? enabled = true) { Config.WithParallelProcessing(enabled); return this; }
        /// <inheritdoc cref="_parallelprocessing" />
        public SynthWishes SetParallelProcessing(bool? enabled = true) => WithParallelProcessing(enabled);
        /// <inheritdoc cref="_parallelprocessing" />
        public SynthWishes ParallelProcessing(bool? enabled) => WithParallelProcessing(enabled);

        /// <inheritdoc cref="_playalltapes" />
        public bool PlayAllTapes() => Config.GetPlayAllTapes;
        /// <inheritdoc cref="_playalltapes" />
        public bool GetPlayAllTapes => Config.GetPlayAllTapes;
        /// <inheritdoc cref="_playalltapes" />
        public SynthWishes WithPlayAllTapes(bool? enabled = true) { Config.WithPlayAllTapes(enabled); return this; }
        /// <inheritdoc cref="_playalltapes" />
        public SynthWishes SetPlayAllTapes(bool? enabled = true) => WithPlayAllTapes(enabled);
        /// <inheritdoc cref="_playalltapes" />
        public SynthWishes PlayAllTapes(bool? enabled) => WithPlayAllTapes(enabled);
        
        // Derived Audio Properties
        
        protected int ByteCount() => ConfigWishes.ByteCount(this);
        protected int GetByteCount() => ConfigWishes.GetByteCount(this);
        protected SynthWishes ByteCount(int? value) => ConfigWishes.ByteCount(this, value);
        protected SynthWishes WithByteCount(int? value) => ConfigWishes.WithByteCount(this, value);
        protected SynthWishes SetByteCount(int? value) => ConfigWishes.SetByteCount(this, value);
        
        protected int CourtesyBytes() => ConfigWishes.CourtesyBytes(this);
        protected int GetCourtesyBytes() => ConfigWishes.GetCourtesyBytes(this);
        protected SynthWishes CourtesyBytes(int? value) => ConfigWishes.CourtesyBytes(this, value);
        protected SynthWishes WithCourtesyBytes(int? value) => ConfigWishes.WithCourtesyBytes(this, value);
        protected SynthWishes SetCourtesyBytes(int? value) => ConfigWishes.SetCourtesyBytes(this, value);
        
        /// <inheritdoc cref="_fileextension" />
        protected string FileExtension() => ConfigWishes.FileExtension(this);
        /// <inheritdoc cref="_fileextension" />
        protected string GetFileExtension() => ConfigWishes.GetFileExtension(this);
        /// <inheritdoc cref="_fileextension" />
        protected SynthWishes FileExtension(string value) => ConfigWishes.FileExtension(this, value);
        /// <inheritdoc cref="_fileextension" />
        protected SynthWishes WithFileExtension(string value) => ConfigWishes.WithFileExtension(this, value);
        /// <inheritdoc cref="_fileextension" />
        protected SynthWishes AsFileExtension(string value) => ConfigWishes.AsFileExtension(this, value);
        /// <inheritdoc cref="_fileextension" />
        protected SynthWishes SetFileExtension(string value) => ConfigWishes.SetFileExtension(this, value);
                
        protected int FrameCount() => ConfigWishes.FrameCount(this);
        protected int GetFrameCount() => ConfigWishes.GetFrameCount(this);
        protected SynthWishes FrameCount(int? value) => ConfigWishes.FrameCount(this, value);
        protected SynthWishes WithFrameCount(int? value) => ConfigWishes.WithFrameCount(this, value);
        protected SynthWishes SetFrameCount(int? value) => ConfigWishes.SetFrameCount(this, value);
        
        protected int FrameSize() => ConfigWishes.FrameSize(this);
        protected int GetFrameSize() => ConfigWishes.GetFrameSize(this);

        /// <inheritdoc cref="_headerlength"/>
        protected int HeaderLength() => ConfigWishes.HeaderLength(this);
        /// <inheritdoc cref="_headerlength"/>
        protected int GetHeaderLength() => ConfigWishes.GetHeaderLength(this);

        /// <inheritdoc cref="_headerlength"/>
        protected SynthWishes HeaderLength(int? headerLength) => ConfigWishes.HeaderLength(this, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        protected SynthWishes WithHeaderLength(int? headerLength) => ConfigWishes.WithHeaderLength(this, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        protected SynthWishes SetHeaderLength(int? headerLength) => ConfigWishes.SetHeaderLength(this, headerLength);

        protected double MaxAmplitude() => ConfigWishes.MaxAmplitude(this);
        protected double GetMaxAmplitude() => ConfigWishes.GetMaxAmplitude(this);
        
        protected int SizeOfBitDepth() => ConfigWishes.SizeOfBitDepth(this);
        protected int GetSizeOfBitDepth() => ConfigWishes.GetSizeOfBitDepth(this);
        protected SynthWishes SizeOfBitDepth(int? sizeOfBitDepth) => ConfigWishes.SizeOfBitDepth(this, sizeOfBitDepth);
        protected SynthWishes WithSizeOfBitDepth(int? sizeOfBitDepth) => ConfigWishes.WithSizeOfBitDepth(this, sizeOfBitDepth);
        protected SynthWishes SetSizeOfBitDepth(int? sizeOfBitDepth) => ConfigWishes.SetSizeOfBitDepth(this, sizeOfBitDepth);

        // Misc Settings

        /// <inheritdoc cref="_leafchecktimeout" />
        public double LeafCheckTimeOut() => Config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="_leafchecktimeout" />
        public double GetLeafCheckTimeOut => Config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes WithLeafCheckTimeOut(double? seconds) { Config.WithLeafCheckTimeOut(seconds); return this; }
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes SetLeafCheckTimeOut(double? seconds) => WithLeafCheckTimeOut(seconds);
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes LeafCheckTimeOut(double? seconds) => WithLeafCheckTimeOut(seconds);

        /// <inheritdoc cref="_leafchecktimeout" />
        public TimeOutActionEnum TimeOutAction() => Config.GetTimeOutAction;
        /// <inheritdoc cref="_leafchecktimeout" />
        public TimeOutActionEnum GetTimeOutAction => Config.GetTimeOutAction;
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes WithTimeOutAction(TimeOutActionEnum? action) { Config.WithTimeOutAction(action); return this; }
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes SetTimeOutAction(TimeOutActionEnum? action) => WithTimeOutAction(action);
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes TimeOutAction(TimeOutActionEnum? action) => WithTimeOutAction(action);
        
        protected int         CourtesyFrames()               => ConfigWishes.CourtesyFrames(this);
        public    int         GetCourtesyFrames              => Config.GetCourtesyFrames;
        protected SynthWishes CourtesyFrames    (int? value) => ConfigWishes.CourtesyFrames(this, value);
        public    SynthWishes WithCourtesyFrames(int? value) {  Config.WithCourtesyFrames(value); return this; }
        protected SynthWishes SetCourtesyFrames (int? value) => ConfigWishes.SetCourtesyFrames(this, value);

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