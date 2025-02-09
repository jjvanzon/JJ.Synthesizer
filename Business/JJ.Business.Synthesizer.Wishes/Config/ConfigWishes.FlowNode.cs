using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Config;

// ReSharper disable once CheckNamespace

namespace JJ.Business.Synthesizer.Wishes
{
    // FlowNode ConfigWishes

    public partial class FlowNode
    {
        // Audio Quality
        
        public bool Is8Bit => _synthWishes.Is8Bit;
        public bool Is16Bit => _synthWishes.Is16Bit;
        public bool Is32Bit => _synthWishes.Is32Bit;
        public int GetBits => _synthWishes.GetBits;
        public FlowNode WithBits(int? bits) { _synthWishes.WithBits(bits); return this; }

        public int NoChannels => ConfigWishes.NoChannels;
        public int MonoChannels => ConfigWishes.MonoChannels;
        public int StereoChannels => ConfigWishes.StereoChannels;
        public int GetChannels => _synthWishes.GetChannels;
        public bool IsMono => _synthWishes.IsMono;
        public bool IsStereo => _synthWishes.IsStereo;
        public FlowNode WithChannels(int? channels) { _synthWishes.WithChannels(channels); return this; }

        public int CenterChannel => ConfigWishes.CenterChannel;
        public int LeftChannel => ConfigWishes.LeftChannel;
        public int RightChannel => ConfigWishes.RightChannel;
        public int? AnyChannel => ConfigWishes.AnyChannel;
        public int? EveryChannel => ConfigWishes.EveryChannel;
        public int? ChannelEmpty => ConfigWishes.ChannelEmpty;
        public bool IsCenter => _synthWishes.IsCenter;
        public bool IsLeft => _synthWishes.IsLeft;
        public bool IsRight => _synthWishes.IsRight;
        public bool IsChannelEmpty => _synthWishes.IsChannelEmpty;
        public bool IsAnyChannel => _synthWishes.IsAnyChannel;
        public bool IsEveryChannel => _synthWishes.IsEveryChannel;
        public bool IsNoChannel => _synthWishes.IsNoChannel;
        public int? GetChannel => _synthWishes.GetChannel;
        public FlowNode WithChannel(int? channel) { _synthWishes.WithChannel(channel); return this; }
        public FlowNode WithLeft()  { _synthWishes.WithLeft(); return this; }
        public FlowNode WithRight() { _synthWishes.WithRight(); return this; }
        public FlowNode WithCenter() { _synthWishes.WithCenter(); return this; }
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int GetSamplingRate => _synthWishes.GetSamplingRate;
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public FlowNode WithSamplingRate(int? value) { _synthWishes.WithSamplingRate(value); return this; }

        public AudioFileFormatEnum GetAudioFormat => _synthWishes.GetAudioFormat;
        public FlowNode WithAudioFormat(AudioFileFormatEnum? audioFormat) { _synthWishes.WithAudioFormat(audioFormat); return this; }
        public bool IsWav => _synthWishes.IsWav;
        public bool IsRaw => _synthWishes.IsRaw;

        public bool IsLinear => _synthWishes.IsLinear;
        public bool IsBlocky => _synthWishes.IsBlocky;
        public InterpolationTypeEnum GetInterpolation => _synthWishes.GetInterpolation;
        public FlowNode WithLinear() { _synthWishes.WithLinear(); return this; }
        public FlowNode WithBlocky() { _synthWishes.WithBlocky(); return this; }
        public FlowNode WithInterpolation(InterpolationTypeEnum? interpolation) { _synthWishes.WithInterpolation(interpolation); return this; }
        
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
        
        public int GetCourtesyFrames => _synthWishes.GetCourtesyFrames;
        public FlowNode WithCourtesyFrames(int? value) { _synthWishes.WithCourtesyFrames(value); return this; }
        
        /// <inheritdoc cref="docs._fileextensionmaxlength" />
        public int GetFileExtensionMaxLength => _synthWishes.GetFileExtensionMaxLength;
        
        public bool IsUnderNCrunch 
        {
            get => SynthWishes.IsUnderNCrunch;
            set => SynthWishes.IsUnderNCrunch = value;
        }
        
        public bool IsUnderAzurePipelines
        {
            get => SynthWishes.IsUnderAzurePipelines;
            set => SynthWishes.IsUnderAzurePipelines = value;
        }
    }
}