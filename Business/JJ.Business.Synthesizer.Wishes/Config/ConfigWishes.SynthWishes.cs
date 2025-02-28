using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.docs;

// ReSharper disable once CheckNamespace

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // Audio Quality

        public bool Is8Bit => Config.Is8Bit;
        public bool Is16Bit => Config.Is16Bit;
        public bool Is32Bit => Config.Is32Bit;
        public int Bits() => ConfigWishes.Bits(this);
        public int GetBits => Config.GetBits;
        public SynthWishes With8Bit() => ConfigWishes.With8Bit(this);
        public SynthWishes With16Bit() => ConfigWishes.With16Bit(this);
        public SynthWishes With32Bit() => ConfigWishes.With32Bit(this);
        public SynthWishes As8Bit() => ConfigWishes.As8Bit(this);
        public SynthWishes As16Bit() => ConfigWishes.As16Bit(this);
        public SynthWishes As32Bit() => ConfigWishes.As32Bit(this);
        public SynthWishes Set8Bit() => ConfigWishes.Set8Bit(this);
        public SynthWishes Set16Bit() => ConfigWishes.Set16Bit(this);
        public SynthWishes Set32Bit() => ConfigWishes.Set32Bit(this);
        public SynthWishes Bits(int? bits) => ConfigWishes.Bits(this, bits);
        public SynthWishes WithBits(int? bits) { Config.WithBits(bits); return this; }
        public SynthWishes AsBits(int? bits) => ConfigWishes.AsBits(this, bits);
        public SynthWishes SetBits(int? bits) => ConfigWishes.SetBits(this, bits);
        
        public int         NoChannels                  => Config.NoChannels;
        public int         MonoChannels                => Config.MonoChannels;
        public int         StereoChannels              => Config.StereoChannels;
        public bool        IsMono                      => Config.IsMono;
        public bool        IsStereo                    => Config.IsStereo;
        public int         Channels()                  => ConfigWishes.Channels(this);
        public int         GetChannels                 => Config.GetChannels;
        public SynthWishes Mono()                      => ConfigWishes.Mono(this);
        public SynthWishes Stereo()                    => ConfigWishes.Stereo(this);
        public SynthWishes Channels(int? channels)     => ConfigWishes.Channels(this, channels);
        public SynthWishes WithMono()                  => ConfigWishes.WithMono(this);
        public SynthWishes WithStereo()                => ConfigWishes.WithStereo(this);
        public SynthWishes WithChannels(int? channels) {  Config.WithChannels(channels); return this; }
        public SynthWishes AsMono()                    => ConfigWishes.AsMono(this);
        public SynthWishes AsStereo()                  => ConfigWishes.AsStereo(this);
        public SynthWishes SetMono()                   => ConfigWishes.SetMono(this);
        public SynthWishes SetStereo()                 => ConfigWishes.SetStereo(this);
        public SynthWishes SetChannels(int? channels)  => ConfigWishes.SetChannels(this, channels);
        
        public int         CenterChannel             => Config.CenterChannel;
        public int         LeftChannel               => Config.LeftChannel;
        public int         RightChannel              => Config.RightChannel;
        public int?        EmptyChannel              => Config.EmptyChannel;
        public bool        IsCenter                  => Config.IsCenter;
        public bool        IsLeft                    => Config.IsLeft;
        public bool        IsRight                   => Config.IsRight;
        public bool        IsAnyChannel              => Config.IsAnyChannel;
        public bool        IsEveryChannel            => Config.IsEveryChannel;
        public bool        IsNoChannel               => Config.IsNoChannel;
        public int?        Channel()                 => ConfigWishes.Channel(this);
        public int?        GetChannel                => Config.GetChannel;
        public SynthWishes WithCenter()              {  Config.WithCenter(); return this; }
        public SynthWishes WithLeft()                {  Config.WithLeft(); return this; }
        public SynthWishes WithRight()               {  Config.WithRight(); return this; }
        public SynthWishes WithNoChannel()           {  Config.WithNoChannel(); return this; }
        public SynthWishes WithAnyChannel()          {  Config.WithAnyChannel(); return this; }
        public SynthWishes WithEveryChannel()        {  Config.WithEveryChannel(); return this; }
        public SynthWishes WithChannel(int? channel) {  Config.WithChannel(channel); return this; }
        public SynthWishes Center()                  => ConfigWishes.Center(this);
        public SynthWishes Left()                    => ConfigWishes.Left(this);
        public SynthWishes Right()                   => ConfigWishes.Right(this);
        public SynthWishes NoChannel()               => ConfigWishes.NoChannel(this);
        public SynthWishes AnyChannel()              => ConfigWishes.AnyChannel(this);
        public SynthWishes EveryChannel()            => ConfigWishes.EveryChannel(this);
        public SynthWishes Channel(int?     channel) => ConfigWishes.Channel(this, channel);
        public SynthWishes AsCenter()                => ConfigWishes.AsCenter(this);
        public SynthWishes AsLeft()                  => ConfigWishes.AsLeft(this);
        public SynthWishes AsRight()                 => ConfigWishes.AsRight(this);
        public SynthWishes AsNoChannel()             => ConfigWishes.AsNoChannel(this);
        public SynthWishes AsAnyChannel()            => ConfigWishes.AsAnyChannel(this);
        public SynthWishes AsEveryChannel()          => ConfigWishes.AsEveryChannel(this);
        public SynthWishes AsChannel(int?   channel) => ConfigWishes.AsChannel(this, channel);
        public SynthWishes SetCenter()               => ConfigWishes.SetCenter(this);
        public SynthWishes SetLeft()                 => ConfigWishes.SetLeft(this);
        public SynthWishes SetRight()                => ConfigWishes.SetRight(this);
        public SynthWishes SetNoChannel()            => ConfigWishes.SetNoChannel(this);
        public SynthWishes SetAnyChannel()           => ConfigWishes.SetAnyChannel(this);
        public SynthWishes SetEveryChannel()         => ConfigWishes.SetEveryChannel(this);
        public SynthWishes SetChannel(int? channel)  => ConfigWishes.SetChannel(this, channel);

        /// <inheritdoc cref="_getsamplingrate" />
        public int SamplingRate() => ConfigWishes.SamplingRate(this);
        /// <inheritdoc cref="_getsamplingrate" />
        public int GetSamplingRate => Config.GetSamplingRate;
        /// <inheritdoc cref="_withsamplingrate"/>
        public SynthWishes SamplingRate(int? value) => ConfigWishes.SamplingRate(this, value);
        /// <inheritdoc cref="_withsamplingrate"/>
        public SynthWishes WithSamplingRate(int? value) { Config.WithSamplingRate(value); return this; }
        /// <inheritdoc cref="_withsamplingrate"/>
        public SynthWishes SetSamplingRate(int? value) => ConfigWishes.SetSamplingRate(this, value);

        public bool IsWav => Config.IsWav;
        public bool IsRaw => Config.IsRaw;
        public AudioFileFormatEnum AudioFormat() => ConfigWishes.AudioFormat(this);
        public AudioFileFormatEnum GetAudioFormat => Config.GetAudioFormat;

        public SynthWishes WithWav() => ConfigWishes.WithWav(this);
        public SynthWishes AsWav() => ConfigWishes.AsWav(this);
        public SynthWishes FromWav() => ConfigWishes.FromWav(this);
        public SynthWishes ToWav() => ConfigWishes.ToWav(this);
        public SynthWishes SetWav() => ConfigWishes.SetWav(this);
        public SynthWishes WithRaw() => ConfigWishes.WithRaw(this);
        public SynthWishes AsRaw() => ConfigWishes.AsRaw(this);
        public SynthWishes FromRaw() => ConfigWishes.FromRaw(this);
        public SynthWishes ToRaw() => ConfigWishes.ToRaw(this);
        public SynthWishes SetRaw() => ConfigWishes.SetRaw(this);
        public SynthWishes AudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.AudioFormat(this, audioFormat);
        public SynthWishes WithAudioFormat(AudioFileFormatEnum? audioFormat) { Config.WithAudioFormat(audioFormat); return this; }
        public SynthWishes AsAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.AsAudioFormat(this, audioFormat);
        public SynthWishes FromAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.FromAudioFormat(this, audioFormat);
        public SynthWishes ToAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.ToAudioFormat(this, audioFormat);
        public SynthWishes SetAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.SetAudioFormat(this, audioFormat);

        public bool IsLinear => Config.IsLinear;
        public bool IsBlocky => Config.IsBlocky;
        public InterpolationTypeEnum Interpolation() => ConfigWishes.Interpolation(this);
        public InterpolationTypeEnum GetInterpolation => Config.GetInterpolation;
        public SynthWishes Linear() => ConfigWishes.Linear(this);
        public SynthWishes Blocky() => ConfigWishes.Blocky(this);
        public SynthWishes WithLinear() => ConfigWishes.WithLinear(this);
        public SynthWishes WithBlocky() => ConfigWishes.WithBlocky(this);
        public SynthWishes AsLinear() => ConfigWishes.AsLinear(this);
        public SynthWishes AsBlocky() => ConfigWishes.AsBlocky(this);
        public SynthWishes SetLinear() => ConfigWishes.SetLinear(this);
        public SynthWishes SetBlocky() => ConfigWishes.SetBlocky(this);
        public SynthWishes Interpolation(InterpolationTypeEnum? interpolation) => ConfigWishes.Interpolation(this, interpolation);
        public SynthWishes WithInterpolation(InterpolationTypeEnum? interpolation) { Config.WithInterpolation(interpolation); return this; }
        public SynthWishes AsInterpolation(InterpolationTypeEnum? interpolation) => ConfigWishes.AsInterpolation(this, interpolation);
        public SynthWishes SetInterpolation(InterpolationTypeEnum? interpolation) => ConfigWishes.SetInterpolation(this, interpolation);

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
        public FlowNode AudioLength() => ConfigWishes.AudioLength(this);
        /// <inheritdoc cref="_audiolength" />
        public FlowNode GetAudioLength => Config.GetAudioLength(this);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AudioLength(double? newLength) => ConfigWishes.AudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AudioLength(FlowNode newLength) => ConfigWishes.AudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes WithAudioLength(double? newLength) { Config.WithAudioLength(newLength, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes WithAudioLength(FlowNode newLength) { Config.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes SetAudioLength(double? newLength) => ConfigWishes.WithAudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes SetAudioLength(FlowNode newLength) => ConfigWishes.WithAudioLength(this, newLength);
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
        
        public int GetByteCount => ConfigWishes.GetByteCount(this);
        public int ByteCount() => ConfigWishes.ByteCount(this);
        public SynthWishes WithByteCount(int? value) => ConfigWishes.WithByteCount(this, value);
        public SynthWishes SetByteCount(int? value) => ConfigWishes.SetByteCount(this, value);
        public SynthWishes ByteCount(int? value) => ConfigWishes.ByteCount(this, value);
        
        public int GetCourtesyBytes => ConfigWishes.GetCourtesyBytes(this);
        public int CourtesyBytes() => ConfigWishes.CourtesyBytes(this);
        public SynthWishes WithCourtesyBytes(int? value) => ConfigWishes.WithCourtesyBytes(this, value);
        public SynthWishes SetCourtesyBytes(int? value) => ConfigWishes.SetCourtesyBytes(this, value);
        public SynthWishes CourtesyBytes(int? value) => ConfigWishes.CourtesyBytes(this, value);
        
        /// <inheritdoc cref="_fileextension" />
        public string GetFileExtension => ConfigWishes.GetFileExtension(this);
        /// <inheritdoc cref="_fileextension" />
        public string FileExtension() => ConfigWishes.FileExtension(this);
        /// <inheritdoc cref="_fileextension" />
        public SynthWishes WithFileExtension(string value) => ConfigWishes.WithFileExtension(this, value);
        /// <inheritdoc cref="_fileextension" />
        public SynthWishes FileExtension(string value) => ConfigWishes.FileExtension(this, value);
        /// <inheritdoc cref="_fileextension" />
        public SynthWishes AsFileExtension(string value) => ConfigWishes.AsFileExtension(this, value);
        /// <inheritdoc cref="_fileextension" />
        public SynthWishes SetFileExtension(string value) => ConfigWishes.SetFileExtension(this, value);
                
        public int GetFrameCount => ConfigWishes.GetFrameCount(this);
        public int FrameCount() => ConfigWishes.FrameCount(this);
        public SynthWishes WithFrameCount(int? value) => ConfigWishes.WithFrameCount(this, value);
        public SynthWishes FrameCount(int? value) => ConfigWishes.FrameCount(this, value);
        public SynthWishes SetFrameCount(int? value) => ConfigWishes.SetFrameCount(this, value);
        
        public int GetFrameSize => ConfigWishes.GetFrameSize(this);
        public int FrameSize() => ConfigWishes.FrameSize(this);

        /// <inheritdoc cref="_headerlength"/>
        public int GetHeaderLength => ConfigWishes.GetHeaderLength(this);
        /// <inheritdoc cref="_headerlength"/>
        public int HeaderLength() => ConfigWishes.HeaderLength(this);
        /// <inheritdoc cref="_headerlength"/>
        public SynthWishes HeaderLength(int? headerLength) => ConfigWishes.HeaderLength(this, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public SynthWishes WithHeaderLength(int? headerLength) => ConfigWishes.WithHeaderLength(this, headerLength);
        /// <inheritdoc cref="_headerlength"/>
        public SynthWishes SetHeaderLength(int? headerLength) => ConfigWishes.SetHeaderLength(this, headerLength);

        public double GetMaxAmplitude => ConfigWishes.GetMaxAmplitude(this);
        public double MaxAmplitude() => ConfigWishes.MaxAmplitude(this);
        
        public int GetSizeOfBitDepth => ConfigWishes.GetSizeOfBitDepth(this);
        public int SizeOfBitDepth() => ConfigWishes.SizeOfBitDepth(this);
        public SynthWishes WithSizeOfBitDepth(int? sizeOfBitDepth) => ConfigWishes.WithSizeOfBitDepth(this, sizeOfBitDepth);
        public SynthWishes SizeOfBitDepth(int? sizeOfBitDepth) => ConfigWishes.SizeOfBitDepth(this, sizeOfBitDepth);
        public SynthWishes SetSizeOfBitDepth(int? sizeOfBitDepth) => ConfigWishes.SetSizeOfBitDepth(this, sizeOfBitDepth);

        // Misc Settings

        /// <inheritdoc cref="_leafchecktimeout" />
        public double GetLeafCheckTimeOut => Config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="_leafchecktimeout" />
        public double LeafCheckTimeOut() => Config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes WithLeafCheckTimeOut(double? seconds) { Config.WithLeafCheckTimeOut(seconds); return this; }
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes SetLeafCheckTimeOut(double? seconds) => WithLeafCheckTimeOut(seconds);
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes LeafCheckTimeOut(double? seconds) => WithLeafCheckTimeOut(seconds);

        /// <inheritdoc cref="_leafchecktimeout" />
        public TimeOutActionEnum GetTimeOutAction => Config.GetTimeOutAction;
        /// <inheritdoc cref="_leafchecktimeout" />
        public TimeOutActionEnum TimeOutAction() => Config.GetTimeOutAction;
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes WithTimeOutAction(TimeOutActionEnum? action) { Config.WithTimeOutAction(action); return this; }
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes SetTimeOutAction(TimeOutActionEnum? action) => WithTimeOutAction(action);
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes TimeOutAction(TimeOutActionEnum? action) => WithTimeOutAction(action);
        
        public int         GetCourtesyFrames              => Config.GetCourtesyFrames;
        public int         CourtesyFrames()               => ConfigWishes.CourtesyFrames(this);
        public SynthWishes WithCourtesyFrames(int? value) {  Config.WithCourtesyFrames(value); return this; }
        public SynthWishes CourtesyFrames    (int? value) => ConfigWishes.CourtesyFrames(this, value);
        public SynthWishes SetCourtesyFrames (int? value) => ConfigWishes.SetCourtesyFrames(this, value);

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