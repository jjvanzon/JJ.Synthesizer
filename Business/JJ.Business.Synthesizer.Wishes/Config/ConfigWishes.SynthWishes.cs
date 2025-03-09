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

        public bool Is8Bit => _config.Is8Bit;
        public bool Is16Bit => _config.Is16Bit;
        public bool Is32Bit => _config.Is32Bit;
        public int Bits() => ConfigWishes.Bits(this);
        public int GetBits => _config.GetBits;
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
        public SynthWishes WithBits(int? bits) { _config.WithBits(bits); return this; }
        public SynthWishes AsBits(int? bits) => ConfigWishes.AsBits(this, bits);
        public SynthWishes SetBits(int? bits) => ConfigWishes.SetBits(this, bits);
        
        public int         NoChannels                  => _config.NoChannels;
        public int         MonoChannels                =>  _config.MonoChannels;
        public int         StereoChannels              => _config.StereoChannels;
        public bool        IsMono                      => _config.IsMono;
        public bool        IsStereo                    => _config.IsStereo;
        public int         Channels()                  => ConfigWishes.Channels(this);
        public int         GetChannels                 => _config.GetChannels;
        public SynthWishes Mono()                      => ConfigWishes.Mono(this);
        public SynthWishes Stereo()                    => ConfigWishes.Stereo(this);
        public SynthWishes Channels(int? channels)     => ConfigWishes.Channels(this, channels);
        public SynthWishes WithMono()                  => ConfigWishes.WithMono(this);
        public SynthWishes WithStereo()                => ConfigWishes.WithStereo(this);
        public SynthWishes WithChannels(int? channels) {  _config.WithChannels(channels); return this; }
        public SynthWishes AsMono()                    => ConfigWishes.AsMono(this);
        public SynthWishes AsStereo()                  => ConfigWishes.AsStereo(this);
        public SynthWishes SetMono()                   => ConfigWishes.SetMono(this);
        public SynthWishes SetStereo()                 => ConfigWishes.SetStereo(this);
        public SynthWishes SetChannels(int? channels)  => ConfigWishes.SetChannels(this, channels);
        
        public int         CenterChannel             => _config.CenterChannel;
        public int         LeftChannel               => _config.LeftChannel;
        public int         RightChannel              => _config.RightChannel;
        public int?        EmptyChannel              => _config.EmptyChannel;
        public bool        IsCenter                  => _config.IsCenter;
        public bool        IsLeft                    => _config.IsLeft;
        public bool        IsRight                   => _config.IsRight;
        public bool        IsAnyChannel              => _config.IsAnyChannel;
        public bool        IsEveryChannel            => _config.IsEveryChannel;
        public bool        IsNoChannel               => _config.IsNoChannel;
        public int?        Channel()                 => ConfigWishes.Channel(this);
        public int?        GetChannel                => _config.GetChannel;
        public SynthWishes WithCenter()              {  _config.WithCenter(); return this; }
        public SynthWishes WithLeft()                {  _config.WithLeft(); return this; }
        public SynthWishes WithRight()               {  _config.WithRight(); return this; }
        public SynthWishes WithNoChannel()           {  _config.WithNoChannel(); return this; }
        public SynthWishes WithAnyChannel()          {  _config.WithAnyChannel(); return this; }
        public SynthWishes WithEveryChannel()        {  _config.WithEveryChannel(); return this; }
        public SynthWishes WithChannel(int? channel) {  _config.WithChannel(channel); return this; }
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
        public int GetSamplingRate => _config.GetSamplingRate;
        /// <inheritdoc cref="_withsamplingrate"/>
        public SynthWishes SamplingRate(int? value) => ConfigWishes.SamplingRate(this, value);
        /// <inheritdoc cref="_withsamplingrate"/>
        public SynthWishes WithSamplingRate(int? value) { _config.WithSamplingRate(value); return this; }
        /// <inheritdoc cref="_withsamplingrate"/>
        public SynthWishes SetSamplingRate(int? value) => ConfigWishes.SetSamplingRate(this, value);

        public bool IsWav => _config.IsWav;
        public bool IsRaw => _config.IsRaw;
        public AudioFileFormatEnum AudioFormat() => ConfigWishes.AudioFormat(this);
        public AudioFileFormatEnum GetAudioFormat => _config.GetAudioFormat;

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
        public SynthWishes WithAudioFormat(AudioFileFormatEnum? audioFormat) { _config.WithAudioFormat(audioFormat); return this; }
        public SynthWishes AsAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.AsAudioFormat(this, audioFormat);
        public SynthWishes FromAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.FromAudioFormat(this, audioFormat);
        public SynthWishes ToAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.ToAudioFormat(this, audioFormat);
        public SynthWishes SetAudioFormat(AudioFileFormatEnum? audioFormat) => ConfigWishes.SetAudioFormat(this, audioFormat);

        public bool IsLinear => _config.IsLinear;
        public bool IsBlocky => _config.IsBlocky;
        public InterpolationTypeEnum Interpolation() => ConfigWishes.Interpolation(this);
        public InterpolationTypeEnum GetInterpolation => _config.GetInterpolation;
        public SynthWishes Linear() => ConfigWishes.Linear(this);
        public SynthWishes Blocky() => ConfigWishes.Blocky(this);
        public SynthWishes WithLinear() => ConfigWishes.WithLinear(this);
        public SynthWishes WithBlocky() => ConfigWishes.WithBlocky(this);
        public SynthWishes AsLinear() => ConfigWishes.AsLinear(this);
        public SynthWishes AsBlocky() => ConfigWishes.AsBlocky(this);
        public SynthWishes SetLinear() => ConfigWishes.SetLinear(this);
        public SynthWishes SetBlocky() => ConfigWishes.SetBlocky(this);
        public SynthWishes Interpolation(InterpolationTypeEnum? interpolation) => ConfigWishes.Interpolation(this, interpolation);
        public SynthWishes WithInterpolation(InterpolationTypeEnum? interpolation) { _config.WithInterpolation(interpolation); return this; }
        public SynthWishes AsInterpolation(InterpolationTypeEnum? interpolation) => ConfigWishes.AsInterpolation(this, interpolation);
        public SynthWishes SetInterpolation(InterpolationTypeEnum? interpolation) => ConfigWishes.SetInterpolation(this, interpolation);

        // Durations

        /// <inheritdoc cref="_notelength" />
        public FlowNode NoteLength() => GetNoteLength();
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLength() => _config.GetNoteLength(this);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLength(FlowNode noteLength) => _config.GetNoteLength(this, noteLength);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot() => _config.GetNoteLengthSnapShot(this);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(double time) => _config.GetNoteLengthSnapShot(this, time);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(double time, int channel) => _config.GetNoteLengthSnapShot(this, time, channel);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength) => _config.GetNoteLengthSnapShot(this, noteLength);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time) => _config.GetNoteLengthSnapShot(this, noteLength, time);
        /// <inheritdoc cref="_notelength" />
        public FlowNode GetNoteLengthSnapShot(FlowNode noteLength, double time, int channel) => _config.GetNoteLengthSnapShot(this, noteLength, time, channel);
        /// <inheritdoc cref="_notelength" />
        public SynthWishes WithNoteLength(FlowNode seconds) { _config.WithNoteLength(seconds); return this; }
        /// <inheritdoc cref="_notelength" />
        public SynthWishes SetNoteLength(FlowNode seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="_notelength" />
        public SynthWishes NoteLength(FlowNode seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="_notelength" />
        public SynthWishes WithNoteLength(double seconds) { _config.WithNoteLength(seconds, this); return this; }
        /// <inheritdoc cref="_notelength" />
        public SynthWishes SetNoteLength(double seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="_notelength" />
        public SynthWishes NoteLength(double seconds) => WithNoteLength(seconds);
        /// <inheritdoc cref="_notelength" />
        public SynthWishes ResetNoteLength() { _config.ResetNoteLength(); return this; }

        public FlowNode BarLength() => GetBarLength;
        public FlowNode GetBarLength => _config.GetBarLength(this);
        public SynthWishes WithBarLength(FlowNode seconds) { _config.WithBarLength(seconds); return this; }
        public SynthWishes SetBarLength(FlowNode seconds) => WithBarLength(seconds);
        public SynthWishes BarLength(FlowNode seconds) => WithBarLength(seconds);
        public SynthWishes WithBarLength(double seconds) { _config.WithBarLength(seconds, this); return this; }
        public SynthWishes SetBarLength(double seconds) => WithBarLength(seconds);
        public SynthWishes BarLength(double seconds) => WithBarLength(seconds);
        public SynthWishes ResetBarLength() { _config.ResetBarLength(); return this; }
        
        public FlowNode BeatLength() => GetBeatLength;
        public FlowNode GetBeatLength => _config.GetBeatLength(this);
        public SynthWishes WithBeatLength(FlowNode seconds) { _config.WithBeatLength(seconds); return this; }
        public SynthWishes SetBeatLength(FlowNode seconds) => WithBeatLength(seconds);
        public SynthWishes BeatLength(FlowNode seconds) => WithBeatLength(seconds);
        public SynthWishes WithBeatLength(double seconds) { _config.WithBeatLength(seconds, this); return this; }
        public SynthWishes SetBeatLength(double seconds) => WithBeatLength(seconds);
        public SynthWishes BeatLength(double seconds) => WithBeatLength(seconds);
        public SynthWishes ResetBeatLength() { _config.ResetBeatLength(); return this; }

        /// <inheritdoc cref="_audiolength" />
        public FlowNode AudioLength() => ConfigWishes.AudioLength(this);
        /// <inheritdoc cref="_audiolength" />
        public FlowNode GetAudioLength => _config.GetAudioLength(this);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AudioLength(double? newLength) => ConfigWishes.AudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AudioLength(FlowNode newLength) => ConfigWishes.AudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes WithAudioLength(double? newLength) { _config.WithAudioLength(newLength, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes WithAudioLength(FlowNode newLength) { _config.WithAudioLength(newLength); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes SetAudioLength(double? newLength) => ConfigWishes.WithAudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes SetAudioLength(FlowNode newLength) => ConfigWishes.WithAudioLength(this, newLength);
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AddAudioLength(double additionalLength) { _config.AddAudioLength(additionalLength, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AddAudioLength(FlowNode additionalLength) { _config.AddAudioLength(additionalLength, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AddEchoDuration(int count = 4, FlowNode delay = default) { _config.AddEchoDuration(count, delay, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes AddEchoDuration(int count, double delay) { _config.AddEchoDuration(count, delay, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes EnsureAudioLength(double audioLengthNeeded) { _config.EnsureAudioLength(audioLengthNeeded, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes EnsureAudioLength(FlowNode audioLengthNeeded) { _config.EnsureAudioLength(audioLengthNeeded, this); return this; }
        /// <inheritdoc cref="_audiolength" />
        public SynthWishes ResetAudioLength() { _config.ResetAudioLength(); return this; }

        /// <inheritdoc cref="_padding"/>
        public FlowNode LeadingSilence() => GetLeadingSilence;
        /// <inheritdoc cref="_padding"/>
        public FlowNode GetLeadingSilence => _config.GetLeadingSilence(this);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithLeadingSilence(double seconds) { _config.WithLeadingSilence(seconds, this); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetLeadingSilence(double seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes LeadingSilence(double seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithLeadingSilence(FlowNode seconds) { _config.WithLeadingSilence(seconds); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetLeadingSilence(FlowNode seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes LeadingSilence(FlowNode seconds) => WithLeadingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes ResetLeadingSilence() { _config.ResetLeadingSilence(); return this; }
        
        /// <inheritdoc cref="_padding"/>
        public FlowNode TrailingSilence() => GetTrailingSilence;
        /// <inheritdoc cref="_padding"/>
        public FlowNode GetTrailingSilence => _config.GetTrailingSilence(this);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithTrailingSilence(double seconds) { _config.WithTrailingSilence(seconds, this); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetTrailingSilence(double seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes TrailingSilence(double seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithTrailingSilence(FlowNode seconds) { _config.WithTrailingSilence(seconds); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetTrailingSilence(FlowNode seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes TrailingSilence(FlowNode seconds) => WithTrailingSilence(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes ResetTrailingSilence() { _config.ResetTrailingSilence(); return this; }

        /// <inheritdoc cref="_padding"/>
        public FlowNode PaddingOrNull() => GetPaddingOrNull;
        /// <inheritdoc cref="_padding"/>
        public FlowNode GetPaddingOrNull => _config.GetPaddingOrNull(this);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithPadding(double seconds) { _config.WithPadding(seconds, this); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetPadding(double seconds) => WithPadding(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes Padding(double seconds) => WithPadding(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes WithPadding(FlowNode seconds) { _config.WithPadding(seconds); return this; }
        /// <inheritdoc cref="_padding"/>
        public SynthWishes SetPadding(FlowNode seconds) => WithPadding(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes Padding(FlowNode seconds) => WithPadding(seconds);
        /// <inheritdoc cref="_padding"/>
        public SynthWishes ResetPadding() { _config.ResetPadding(); return this; }
        
        // Feature Toggles
        
        /// <inheritdoc cref="_audioplayback" />
        public bool AudioPlayback(string fileExtension = null) => _config.GetAudioPlayback(fileExtension);
        /// <inheritdoc cref="_audioplayback" />
        public bool GetAudioPlayback(string fileExtension = null) => _config.GetAudioPlayback(fileExtension);
        /// <inheritdoc cref="_audioplayback" />
        public SynthWishes WithAudioPlayback(bool? enabled = true) { _config.WithAudioPlayback(enabled); return this; }
        /// <inheritdoc cref="_audioplayback" />
        public SynthWishes SetAudioPlayback(bool? enabled = true) => WithAudioPlayback(enabled);
        /// <inheritdoc cref="_audioplayback" />
        public SynthWishes AudioPlayback(bool? enabled = true) => WithAudioPlayback(enabled);

        /// <inheritdoc cref="_diskcache" />
        public bool DiskCache() => _config.GetDiskCache;
        /// <inheritdoc cref="_diskcache" />
        public bool GetDiskCache => _config.GetDiskCache;
        /// <inheritdoc cref="_diskcache" />
        public SynthWishes WithDiskCache(bool? enabled = true) { _config.WithDiskCache(enabled); return this; }
        /// <inheritdoc cref="_diskcache" />
        public SynthWishes SetDiskCache(bool? enabled = true) => WithDiskCache(enabled);
        /// <inheritdoc cref="_diskcache" />
        public SynthWishes DiskCache(bool? enabled) => WithDiskCache(enabled);

        public bool MathBoost() => GetMathBoost;
        public bool GetMathBoost => _config.GetMathBoost;
        public SynthWishes WithMathBoost(bool? enabled = true) { _config.WithMathBoost(enabled); return this; }
        public SynthWishes SetMathBoost(bool? enabled = true) => WithMathBoost(enabled);
        public SynthWishes MathBoost(bool? enabled) => WithMathBoost(enabled);

        /// <inheritdoc cref="_parallelprocessing" />
        public bool ParallelProcessing() => _config.GetParallelProcessing;
        /// <inheritdoc cref="_parallelprocessing" />
        public bool GetParallelProcessing => _config.GetParallelProcessing;
        /// <inheritdoc cref="_parallelprocessing" />
        public SynthWishes WithParallelProcessing(bool? enabled = true) { _config.WithParallelProcessing(enabled); return this; }
        /// <inheritdoc cref="_parallelprocessing" />
        public SynthWishes SetParallelProcessing(bool? enabled = true) => WithParallelProcessing(enabled);
        /// <inheritdoc cref="_parallelprocessing" />
        public SynthWishes ParallelProcessing(bool? enabled) => WithParallelProcessing(enabled);

        /// <inheritdoc cref="_playalltapes" />
        public bool PlayAllTapes() => _config.GetPlayAllTapes;
        /// <inheritdoc cref="_playalltapes" />
        public bool GetPlayAllTapes => _config.GetPlayAllTapes;
        /// <inheritdoc cref="_playalltapes" />
        public SynthWishes WithPlayAllTapes(bool? enabled = true) { _config.WithPlayAllTapes(enabled); return this; }
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
        public double GetLeafCheckTimeOut => _config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="_leafchecktimeout" />
        public double LeafCheckTimeOut() => _config.GetLeafCheckTimeOut;
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes WithLeafCheckTimeOut(double? seconds) { _config.WithLeafCheckTimeOut(seconds); return this; }
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes SetLeafCheckTimeOut(double? seconds) => WithLeafCheckTimeOut(seconds);
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes LeafCheckTimeOut(double? seconds) => WithLeafCheckTimeOut(seconds);

        /// <inheritdoc cref="_leafchecktimeout" />
        public TimeOutActionEnum GetTimeOutAction => _config.GetTimeOutAction;
        /// <inheritdoc cref="_leafchecktimeout" />
        public TimeOutActionEnum TimeOutAction() => _config.GetTimeOutAction;
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes WithTimeOutAction(TimeOutActionEnum? action) { _config.WithTimeOutAction(action); return this; }
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes SetTimeOutAction(TimeOutActionEnum? action) => WithTimeOutAction(action);
        /// <inheritdoc cref="_leafchecktimeout" />
        public SynthWishes TimeOutAction(TimeOutActionEnum? action) => WithTimeOutAction(action);
        
        public int         GetCourtesyFrames              => _config.GetCourtesyFrames;
        public int         CourtesyFrames()               => ConfigWishes.CourtesyFrames(this);
        public SynthWishes WithCourtesyFrames(int? value) {  _config.WithCourtesyFrames(value); return this; }
        public SynthWishes CourtesyFrames    (int? value) => ConfigWishes.CourtesyFrames(this, value);
        public SynthWishes SetCourtesyFrames (int? value) => ConfigWishes.SetCourtesyFrames(this, value);

        public int GetFileExtensionMaxLength => _config.GetFileExtensionMaxLength;
        
        public bool IsUnderNCrunch 
        {
            get => _config.IsUnderNCrunch;
            set => _config.IsUnderNCrunch = value;
        }
        
        public bool IsUnderAzurePipelines
        {
            get => _config.IsUnderAzurePipelines;
            set => _config.IsUnderAzurePipelines = value;
        }
    }
}