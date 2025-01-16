using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Configuration;

// ReSharper disable once CheckNamespace

namespace JJ.Business.Synthesizer.Wishes
{
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
        
        public int NoChannels  => ConfigWishes.NoChannels;
        public int MonoChannels   => ConfigWishes.MonoChannels;
        public int StereoChannels => ConfigWishes.StereoChannels;
        public int GetChannels => Config.GetChannels;
        public SynthWishes WithChannels(int? channels) { Config.WithChannels(channels); return this; }
        public bool IsMono => Config.IsMono;
        public SynthWishes WithMono() { Config.WithMono(); return this; }
        public bool IsStereo => Config.IsStereo;
        public SynthWishes WithStereo() { Config.WithStereo(); return this; }
        
        public int  CenterChannel => ConfigWishes.CenterChannel;
        public int  LeftChannel   => ConfigWishes.LeftChannel;
        public int  RightChannel  => ConfigWishes.RightChannel;
        public int? AnyChannel    => ConfigWishes.AnyChannel;
        public int? EveryChannel  => ConfigWishes.EveryChannel;
        public int? ChannelEmpty  => ConfigWishes.ChannelEmpty;
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