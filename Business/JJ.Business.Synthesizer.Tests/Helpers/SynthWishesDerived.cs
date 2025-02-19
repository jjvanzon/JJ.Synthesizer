using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    // TODO: Move to its own file.

    /// <inheritdoc cref="docs._synthwishesderived" />
    internal class SynthWishesDerived : SynthWishes
    {
        readonly SynthWishes _other;
        
        /// <inheritdoc cref="docs._synthwishesderived" />
        public SynthWishesDerived(SynthWishes other) => _other = other;
        
        // Primary Audio Properties
        
        // Bits
        
        public bool Is8Bit_Call  => Is8Bit;
        public bool Is16Bit_Call => Is16Bit;
        public bool Is32Bit_Call => Is32Bit;
        public int  Bits_Call()  => Bits();
        public int  GetBits_Call => GetBits;
        
        public SynthWishes With8Bit_Call ()          { _other.With8Bit ();     return With8Bit ();     }
        public SynthWishes With16Bit_Call()          { _other.With16Bit();     return With16Bit();     }
        public SynthWishes With32Bit_Call()          { _other.With32Bit();     return With32Bit();     }
        public SynthWishes As8Bit_Call   ()          { _other.As8Bit   ();     return As8Bit   ();     }
        public SynthWishes As16Bit_Call  ()          { _other.As16Bit  ();     return As16Bit  ();     }
        public SynthWishes As32Bit_Call  ()          { _other.As32Bit  ();     return As32Bit  ();     }
        public SynthWishes Set8Bit_Call  ()          { _other.Set8Bit  ();     return Set8Bit  ();     }
        public SynthWishes Set16Bit_Call ()          { _other.Set16Bit ();     return Set16Bit ();     }
        public SynthWishes Set32Bit_Call ()          { _other.Set32Bit ();     return Set32Bit ();     }
        public SynthWishes Bits_Call     (int? bits) { _other.Bits     (bits); return Bits     (bits); }
        public SynthWishes WithBits_Call (int? bits) { _other.WithBits (bits); return WithBits (bits); }
        public SynthWishes AsBits_Call   (int? bits) { _other.AsBits   (bits); return AsBits   (bits); }
        public SynthWishes SetBits_Call  (int? bits) { _other.SetBits  (bits); return SetBits  (bits); }
        
        // Channels
        
        public int  NoChannels_Call     => NoChannels;
        public int  MonoChannels_Call   => MonoChannels;
        public int  StereoChannels_Call => StereoChannels;
        public bool IsMono_Call         => IsMono;
        public bool IsStereo_Call       => IsStereo;
        public int  Channels_Call()     => Channels();
        public int  GetChannels_Call    => GetChannels;
        
        public   SynthWishes Mono_Call        ()              { _other.Mono        ();         return Mono();                 }
        public   SynthWishes Stereo_Call      ()              { _other.Stereo      ();         return Stereo();               }
        public   SynthWishes Channels_Call    (int? channels) { _other.Channels    (channels); return Channels(channels);     }
        public   SynthWishes WithMono_Call    ()              { _other.WithMono    ();         return WithMono();             }
        public   SynthWishes WithStereo_Call  ()              { _other.WithStereo  ();         return WithStereo();           }
        public   SynthWishes WithChannels_Call(int? channels) { _other.WithChannels(channels); return WithChannels(channels); }
        public   SynthWishes AsMono_Call      ()              { _other.AsMono      ();         return AsMono();               }
        public   SynthWishes AsStereo_Call    ()              { _other.AsStereo    ();         return AsStereo();             }
        //public SynthWishes AsChannels_Call  (int? channels) { _other.AsChannels  (channels); return AsChannels(channels);   } // By Design: does not fit semantically.
        public   SynthWishes SetMono_Call     ()              { _other.SetMono     ();         return SetMono();              }
        public   SynthWishes SetStereo_Call   ()              { _other.SetStereo   ();         return SetStereo();            }
        public   SynthWishes SetChannels_Call (int? channels) { _other.SetChannels (channels); return SetChannels(channels);  }
        
        // Channel
        
        public int  CenterChannel_Call => CenterChannel;
        public int  LeftChannel_Call   => LeftChannel;
        public int  RightChannel_Call  => RightChannel;
        public int? EmptyChannel_Call  => EmptyChannel;
        public bool IsCenter_Call      => IsCenter;
        public bool IsLeft_Call        => IsLeft;
        public bool IsRight_Call       => IsRight;
        public int? Channel_Call()     => Channel();
        public int? GetChannel_Call    => GetChannel;
        
        public SynthWishes Center_Call       ()             { _other.Center       ()       ; return Center       ()       ;}
        public SynthWishes WithCenter_Call   ()             { _other.WithCenter   ()       ; return WithCenter   ()       ;}
        public SynthWishes AsCenter_Call     ()             { _other.AsCenter     ()       ; return AsCenter     ()       ;}
        public SynthWishes Left_Call         ()             { _other.Left         ()       ; return Left         ()       ;}
        public SynthWishes WithLeft_Call     ()             { _other.WithLeft     ()       ; return WithLeft     ()       ;}
        public SynthWishes AsLeft_Call       ()             { _other.AsLeft       ()       ; return AsLeft       ()       ;}
        public SynthWishes Right_Call        ()             { _other.Right        ()       ; return Right        ()       ;}
        public SynthWishes WithRight_Call    ()             { _other.WithRight    ()       ; return WithRight    ()       ;}
        public SynthWishes AsRight_Call      ()             { _other.AsRight      ()       ; return AsRight      ()       ;}
        public SynthWishes NoChannel_Call    ()             { _other.NoChannel    ()       ; return NoChannel    ()       ;}
        public SynthWishes WithNoChannel_Call()             { _other.WithNoChannel()       ; return WithNoChannel()       ;}
        public SynthWishes AsNoChannel_Call  ()             { _other.AsNoChannel  ()       ; return AsNoChannel  ()       ;}
        public SynthWishes Channel_Call      (int? channel) { _other.Channel      (channel); return Channel      (channel);}
        public SynthWishes WithChannel_Call  (int? channel) { _other.WithChannel  (channel); return WithChannel  (channel);}
        public SynthWishes AsChannel_Call    (int? channel) { _other.AsChannel    (channel); return AsChannel    (channel);}
        public SynthWishes SetCenter_Call    ()             { _other.SetCenter    ()       ; return SetCenter    ()       ;}
        public SynthWishes SetLeft_Call      ()             { _other.SetLeft      ()       ; return SetLeft      ()       ;}
        public SynthWishes SetRight_Call     ()             { _other.SetRight     ()       ; return SetRight     ()       ;}
        public SynthWishes SetNoChannel_Call ()             { _other.SetNoChannel ()       ; return SetNoChannel ()       ;}
        public SynthWishes SetChannel_Call   (int? channel) { _other.SetChannel   (channel); return SetChannel   (channel);}
        
        // SamplingRate
        
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int SamplingRate_Call()  => SamplingRate();
        /// <inheritdoc cref="docs._getsamplingrate" />
        public int GetSamplingRate_Call => GetSamplingRate;
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public SynthWishes SamplingRate_Call    (int? value) => SamplingRate    (value);
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public SynthWishes WithSamplingRate_Call(int? value) => WithSamplingRate(value);
        /// <inheritdoc cref="docs._withsamplingrate"/>
        public SynthWishes SetSamplingRate_Call (int? value) => SetSamplingRate (value);
        
        // AudioFormat
        
        public bool                IsWav_Call          => IsWav;
        public bool                IsRaw_Call          => IsRaw;
        public AudioFileFormatEnum AudioFormat_Call()  => AudioFormat();
        public AudioFileFormatEnum GetAudioFormat_Call => GetAudioFormat;
        
        public SynthWishes  WithWav_Call() { _other.WithWav(); return WithWav(); }
        public SynthWishes  AsWav_Call  () { _other.AsWav  (); return AsWav  (); }
        public SynthWishes  SetWav_Call () { _other.SetWav (); return SetWav (); }
        public SynthWishes  WithRaw_Call() { _other.WithRaw(); return WithRaw(); }
        public SynthWishes  AsRaw_Call  () { _other.AsRaw  (); return AsRaw  (); }
        public SynthWishes  SetRaw_Call () { _other.SetRaw (); return SetRaw (); }
        public SynthWishes  AudioFormat_Call    (AudioFileFormatEnum? audioFormat) { _other.AudioFormat    (audioFormat); return AudioFormat    (audioFormat); }
        public SynthWishes  WithAudioFormat_Call(AudioFileFormatEnum? audioFormat) { _other.WithAudioFormat(audioFormat); return WithAudioFormat(audioFormat); }
        public SynthWishes  AsAudioFormat_Call  (AudioFileFormatEnum? audioFormat) { _other.AsAudioFormat  (audioFormat); return AsAudioFormat  (audioFormat); }
        public SynthWishes  SetAudioFormat_Call (AudioFileFormatEnum? audioFormat) { _other.SetAudioFormat (audioFormat); return SetAudioFormat (audioFormat); }
        
        // Interpolation
        
        public bool                  IsLinear_Call         => IsLinear;
        public bool                  IsBlocky_Call         => IsBlocky;
        public InterpolationTypeEnum Interpolation_Call()  => Interpolation();
        public InterpolationTypeEnum GetInterpolation_Call => GetInterpolation;
        
        public SynthWishes Linear_Call           () { _other.Linear    (); return Linear    (); }
        public SynthWishes Blocky_Call           () { _other.Blocky    (); return Blocky    (); }
        public SynthWishes WithLinear_Call       () { _other.WithLinear(); return WithLinear(); }
        public SynthWishes WithBlocky_Call       () { _other.WithBlocky(); return WithBlocky(); }
        public SynthWishes AsLinear_Call         () { _other.AsLinear  (); return AsLinear  (); }
        public SynthWishes AsBlocky_Call         () { _other.AsBlocky  (); return AsBlocky  (); }
        public SynthWishes SetLinear_Call        () { _other.SetLinear (); return SetLinear (); }
        public SynthWishes SetBlocky_Call        () { _other.SetBlocky (); return SetBlocky (); }
        public SynthWishes Interpolation_Call    (InterpolationTypeEnum? interpolation) { _other.Interpolation    (interpolation); return Interpolation    (interpolation); }
        public SynthWishes WithInterpolation_Call(InterpolationTypeEnum? interpolation) { _other.WithInterpolation(interpolation); return WithInterpolation(interpolation); }
        public SynthWishes AsInterpolation_Call  (InterpolationTypeEnum? interpolation) { _other.AsInterpolation  (interpolation); return AsInterpolation  (interpolation); }
        public SynthWishes SetInterpolation_Call (InterpolationTypeEnum? interpolation) { _other.SetInterpolation (interpolation); return SetInterpolation (interpolation); }
        
        // Durations
        
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode NoteLength_Call           ()                                              => NoteLength();
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength_Call        ()                                              => GetNoteLength();
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLength_Call        (FlowNode noteLength)                           => GetNoteLength(noteLength);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot_Call()                                              => GetNoteLengthSnapShot();
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot_Call(double time)                                   => GetNoteLengthSnapShot(time);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot_Call(double time, int channel)                      => GetNoteLengthSnapShot(time, channel);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot_Call(FlowNode noteLength)                           => GetNoteLengthSnapShot(noteLength);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot_Call(FlowNode noteLength, double time)              => GetNoteLengthSnapShot(noteLength, time);
        /// <inheritdoc cref="docs._notelength" />
        public FlowNode GetNoteLengthSnapShot_Call(FlowNode noteLength, double time, int channel) => GetNoteLengthSnapShot(noteLength, time, channel);
        
        
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes WithNoteLength_Call (FlowNode seconds) { _other.WithNoteLength (seconds); return WithNoteLength (seconds); }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes SetNoteLength_Call  (FlowNode seconds) { _other.SetNoteLength  (seconds); return SetNoteLength  (seconds); }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes NoteLength_Call     (FlowNode seconds) { _other.NoteLength     (seconds); return NoteLength     (seconds); }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes WithNoteLength_Call (double   seconds) { _other.WithNoteLength (seconds); return WithNoteLength (seconds); }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes SetNoteLength_Call  (double   seconds) { _other.SetNoteLength  (seconds); return SetNoteLength  (seconds); }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes NoteLength_Call     (double   seconds) { _other.NoteLength     (seconds); return NoteLength     (seconds); }
        /// <inheritdoc cref="docs._notelength" />
        public SynthWishes ResetNoteLength_Call()                 { _other.ResetNoteLength();        return ResetNoteLength();        }
        
        public FlowNode BarLength_Call()  => BarLength();
        public FlowNode GetBarLength_Call => GetBarLength;
        public SynthWishes WithBarLength_Call (FlowNode seconds) { _other.WithBarLength (seconds); return WithBarLength (seconds); }
        public SynthWishes SetBarLength_Call  (FlowNode seconds) { _other.SetBarLength  (seconds); return SetBarLength  (seconds); }
        public SynthWishes BarLength_Call     (FlowNode seconds) { _other.BarLength     (seconds); return BarLength     (seconds); }
        public SynthWishes WithBarLength_Call (double   seconds) { _other.WithBarLength (seconds); return WithBarLength (seconds); }
        public SynthWishes SetBarLength_Call  (double   seconds) { _other.SetBarLength  (seconds); return SetBarLength  (seconds); }
        public SynthWishes BarLength_Call     (double   seconds) { _other.BarLength     (seconds); return BarLength     (seconds); }
        public SynthWishes ResetBarLength_Call()                 { _other.ResetBarLength();        return ResetBarLength();        }
        
        public FlowNode    BeatLength_Call()  => BeatLength();
        public FlowNode    GetBeatLength_Call => GetBeatLength;
        
        public SynthWishes WithBeatLength_Call (FlowNode seconds) { _other.WithBeatLength (seconds); return WithBeatLength (seconds); }
        public SynthWishes SetBeatLength_Call  (FlowNode seconds) { _other.SetBeatLength  (seconds); return SetBeatLength  (seconds); }
        public SynthWishes BeatLength_Call     (FlowNode seconds) { _other.BeatLength     (seconds); return BeatLength     (seconds); }
        public SynthWishes WithBeatLength_Call (double   seconds) { _other.WithBeatLength (seconds); return WithBeatLength (seconds); }
        public SynthWishes SetBeatLength_Call  (double   seconds) { _other.SetBeatLength  (seconds); return SetBeatLength  (seconds); }
        public SynthWishes BeatLength_Call     (double   seconds) { _other.BeatLength     (seconds); return BeatLength     (seconds); }
        public SynthWishes ResetBeatLength_Call()                 { _other.ResetBeatLength();        return ResetBeatLength();        }
        
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode AudioLength_Call()  => AudioLength();
        /// <inheritdoc cref="docs._audiolength" />
        public FlowNode GetAudioLength_Call => GetAudioLength;
        
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AudioLength_Call      (double?  newLength) { _other.AudioLength    (newLength); return AudioLength    (newLength); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AudioLength_Call      (FlowNode newLength) { _other.AudioLength    (newLength); return AudioLength    (newLength); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength_Call  (double?  newLength) { _other.WithAudioLength(newLength); return WithAudioLength(newLength); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes WithAudioLength_Call  (FlowNode newLength) { _other.WithAudioLength(newLength); return WithAudioLength(newLength); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes SetAudioLength_Call   (double?  newLength) { _other.SetAudioLength (newLength); return SetAudioLength (newLength); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes SetAudioLength_Call   (FlowNode newLength) { _other.SetAudioLength (newLength); return SetAudioLength (newLength); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddAudioLength_Call   (double   additionalLength) { _other.AddAudioLength(additionalLength); return AddAudioLength(additionalLength); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddAudioLength_Call   (FlowNode additionalLength) { _other.AddAudioLength(additionalLength); return AddAudioLength(additionalLength); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddEchoDuration_Call  (int count = 4, FlowNode delay = default) { _other.AddEchoDuration(count, delay); return AddEchoDuration(count, delay); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes AddEchoDuration_Call  (int count    , double   delay          ) { _other.AddEchoDuration(count, delay); return AddEchoDuration(count, delay); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes EnsureAudioLength_Call(double   audioLengthNeeded) { _other.EnsureAudioLength(audioLengthNeeded); return EnsureAudioLength(audioLengthNeeded); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes EnsureAudioLength_Call(FlowNode audioLengthNeeded) { _other.EnsureAudioLength(audioLengthNeeded); return EnsureAudioLength(audioLengthNeeded); }
        /// <inheritdoc cref="docs._audiolength" />
        public SynthWishes ResetAudioLength_Call () { _other.ResetAudioLength(); return ResetAudioLength(); }
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode LeadingSilence_Call()  => LeadingSilence();
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetLeadingSilence_Call => GetLeadingSilence;
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithLeadingSilence_Call(double   seconds) { _other.WithLeadingSilence(seconds); return WithLeadingSilence(seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetLeadingSilence_Call (double   seconds) { _other.SetLeadingSilence (seconds); return SetLeadingSilence (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes LeadingSilence_Call    (double   seconds) { _other.LeadingSilence    (seconds); return LeadingSilence    (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithLeadingSilence_Call(FlowNode seconds) { _other.WithLeadingSilence(seconds); return WithLeadingSilence(seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetLeadingSilence_Call (FlowNode seconds) { _other.SetLeadingSilence (seconds); return SetLeadingSilence (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes LeadingSilence_Call    (FlowNode seconds) { _other.LeadingSilence    (seconds); return LeadingSilence    (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes ResetLeadingSilence_Call()                { _other.ResetLeadingSilence();       return ResetLeadingSilence();       }
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode TrailingSilence_Call()  => TrailingSilence();
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetTrailingSilence_Call => GetTrailingSilence;
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithTrailingSilence_Call (double   seconds) { _other.WithTrailingSilence (seconds); return WithTrailingSilence(seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetTrailingSilence_Call  (double   seconds) { _other.SetTrailingSilence  (seconds); return SetTrailingSilence (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes TrailingSilence_Call     (double   seconds) { _other.TrailingSilence     (seconds); return TrailingSilence    (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithTrailingSilence_Call (FlowNode seconds) { _other.WithTrailingSilence (seconds); return WithTrailingSilence(seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetTrailingSilence_Call  (FlowNode seconds) { _other.SetTrailingSilence  (seconds); return SetTrailingSilence (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes TrailingSilence_Call     (FlowNode seconds) { _other.TrailingSilence     (seconds); return TrailingSilence    (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes ResetTrailingSilence_Call()                 { _other.ResetTrailingSilence();        return ResetTrailingSilence();       }
        
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode PaddingOrNull_Call()  => PaddingOrNull();
        /// <inheritdoc cref="docs._padding"/>
        public FlowNode GetPaddingOrNull_Call => GetPaddingOrNull;
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithPadding_Call(double   seconds) { _other.WithPadding (seconds); return WithPadding (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetPadding_Call (double   seconds) { _other.SetPadding  (seconds); return SetPadding  (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes Padding_Call    (double   seconds) { _other.Padding     (seconds); return Padding     (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes WithPadding_Call(FlowNode seconds) { _other.WithPadding (seconds); return WithPadding (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes SetPadding_Call (FlowNode seconds) { _other.SetPadding  (seconds); return SetPadding  (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes Padding_Call    (FlowNode seconds) { _other.Padding     (seconds); return Padding     (seconds); }
        /// <inheritdoc cref="docs._padding"/>
        public SynthWishes ResetPadding_Call()                { _other.ResetPadding(       ); return ResetPadding(       ); }
        
        // Feature Toggles
        
        /// <inheritdoc cref="docs._audioplayback" />
        public bool AudioPlayback_Call    (string fileExtension = null) { _other.AudioPlayback    (fileExtension); return AudioPlayback    (fileExtension); }
        /// <inheritdoc cref="docs._audioplayback" />
        public bool GetAudioPlayback_Call (string fileExtension = null) { _other.GetAudioPlayback (fileExtension); return GetAudioPlayback (fileExtension); }
        /// <inheritdoc cref="docs._audioplayback" />
        public SynthWishes WithAudioPlayback_Call(bool? enabled = true) { _other.WithAudioPlayback(enabled);       return WithAudioPlayback(enabled);       }
        /// <inheritdoc cref="docs._audioplayback" />
        public SynthWishes SetAudioPlayback_Call (bool? enabled = true) { _other.SetAudioPlayback (enabled);       return SetAudioPlayback (enabled);       }
        /// <inheritdoc cref="docs._audioplayback" />
        public SynthWishes AudioPlayback_Call    (bool? enabled = true) { _other.AudioPlayback    (enabled);       return AudioPlayback    (enabled);       }
        
        // TODO: Feature Toggles without a prefix look like they should turn the feature on, not get the Boolean. Consider making them setters.
        
        /// <inheritdoc cref="docs._diskcache" />
        public bool DiskCache_Call()  => DiskCache();
        /// <inheritdoc cref="docs._diskcache" />
        public bool GetDiskCache_Call => GetDiskCache;
        /// <inheritdoc cref="docs._diskcache" />
        public SynthWishes WithDiskCache_Call(bool? enabled = true) { _other.WithDiskCache(enabled); return WithDiskCache(enabled); }
        /// <inheritdoc cref="docs._diskcache" />
        public SynthWishes SetDiskCache_Call (bool? enabled = true) { _other.SetDiskCache (enabled); return SetDiskCache (enabled); }
        /// <inheritdoc cref="docs._diskcache" />
        public SynthWishes DiskCache_Call    (bool? enabled       ) { _other.DiskCache    (enabled); return DiskCache    (enabled); }
        
        public bool MathBoost_Call()  => MathBoost();
        public bool GetMathBoost_Call => GetMathBoost;
        public SynthWishes WithMathBoost_Call(bool? enabled = true) { _other.WithMathBoost(enabled); return WithMathBoost(enabled); }
        public SynthWishes SetMathBoost_Call (bool? enabled = true) { _other.SetMathBoost (enabled); return SetMathBoost (enabled); }
        public SynthWishes MathBoost_Call    (bool? enabled       ) { _other.MathBoost    (enabled); return MathBoost    (enabled); }
        
        /// <inheritdoc cref="docs._parallelprocessing" />
        public bool ParallelProcessing_Call()  => ParallelProcessing();
        /// <inheritdoc cref="docs._parallelprocessing" />
        public bool GetParallelProcessing_Call => GetParallelProcessing;
        /// <inheritdoc cref="docs._parallelprocessing" />
        public SynthWishes WithParallelProcessing_Call(bool? enabled = true) { _other.WithParallelProcessing(enabled); return WithParallelProcessing(enabled); }
        /// <inheritdoc cref="docs._parallelprocessing" />
        public SynthWishes SetParallelProcessing_Call (bool? enabled = true) { _other.SetParallelProcessing (enabled); return SetParallelProcessing (enabled); }
        /// <inheritdoc cref="docs._parallelprocessing" />
        public SynthWishes ParallelProcessing_Call    (bool? enabled       ) { _other.ParallelProcessing    (enabled); return ParallelProcessing    (enabled); }
        
        /// <inheritdoc cref="docs._playalltapes" />
        public bool PlayAllTapes_Call()  => PlayAllTapes();
        /// <inheritdoc cref="docs._playalltapes" />
        public bool GetPlayAllTapes_Call => GetPlayAllTapes;
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes WithPlayAllTapes_Call(bool? enabled = true) { _other.WithPlayAllTapes(enabled); return WithPlayAllTapes(enabled); }
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes SetPlayAllTapes_Call (bool? enabled = true) { _other.SetPlayAllTapes (enabled); return SetPlayAllTapes (enabled); }
        /// <inheritdoc cref="docs._playalltapes" />
        public SynthWishes PlayAllTapes_Call    (bool? enabled       ) { _other.PlayAllTapes    (enabled); return PlayAllTapes    (enabled); }
        
        // Derived Audio Properties
        
        public int ByteCount_Call   () => ByteCount();
        public int GetByteCount_Call() => GetByteCount();
        public SynthWishes ByteCount_Call    (int? value) { _other.ByteCount    (value); return ByteCount    (value); }
        public SynthWishes WithByteCount_Call(int? value) { _other.WithByteCount(value); return WithByteCount(value); }
        public SynthWishes SetByteCount_Call (int? value) { _other.SetByteCount (value); return SetByteCount (value); }
        
        public int CourtesyBytes_Call   () => CourtesyBytes();
        public int GetCourtesyBytes_Call() => GetCourtesyBytes();
        public SynthWishes CourtesyBytes_Call    (int? value) { _other.CourtesyBytes    (value); return CourtesyBytes    (value); }
        public SynthWishes WithCourtesyBytes_Call(int? value) { _other.WithCourtesyBytes(value); return WithCourtesyBytes(value); }
        public SynthWishes SetCourtesyBytes_Call (int? value) { _other.SetCourtesyBytes (value); return SetCourtesyBytes (value); }
        
        /// <inheritdoc cref="docs._fileextension" />
        public string FileExtension_Call   () => FileExtension();
        /// <inheritdoc cref="docs._fileextension" />
        public string GetFileExtension_Call() => GetFileExtension();
        /// <inheritdoc cref="docs._fileextension" />
        public SynthWishes FileExtension_Call    (string value) { _other.FileExtension    (value); return FileExtension    (value); }
        /// <inheritdoc cref="docs._fileextension" />
        public SynthWishes WithFileExtension_Call(string value) { _other.WithFileExtension(value); return WithFileExtension(value); }
        /// <inheritdoc cref="docs._fileextension" />
        public SynthWishes AsFileExtension_Call  (string value) { _other.AsFileExtension  (value); return AsFileExtension  (value); }
        /// <inheritdoc cref="docs._fileextension" />
        public SynthWishes SetFileExtension_Call (string value) { _other.SetFileExtension (value); return SetFileExtension (value); }
        
        public int FrameCount_Call   () => FrameCount();
        public int GetFrameCount_Call() => GetFrameCount();
        public SynthWishes FrameCount_Call    (int? value) { _other.FrameCount    (value); return FrameCount    (value); }
        public SynthWishes WithFrameCount_Call(int? value) { _other.WithFrameCount(value); return WithFrameCount(value); }
        public SynthWishes SetFrameCount_Call (int? value) { _other.SetFrameCount (value); return SetFrameCount (value); }
        
        public int FrameSize_Call   () => FrameSize();
        public int GetFrameSize_Call() => GetFrameSize();
        
        /// <inheritdoc cref="docs._headerlength"/>
        public int HeaderLength_Call   () => HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public int GetHeaderLength_Call() => GetHeaderLength();
        
        /// <inheritdoc cref="docs._headerlength"/>
        public SynthWishes HeaderLength_Call    (int? headerLength) { _other.HeaderLength    (headerLength); return HeaderLength    (headerLength); }
        /// <inheritdoc cref="docs._headerlength"/>
        public SynthWishes WithHeaderLength_Call(int? headerLength) { _other.WithHeaderLength(headerLength); return WithHeaderLength(headerLength); }
        /// <inheritdoc cref="docs._headerlength"/>
        public SynthWishes SetHeaderLength_Call (int? headerLength) { _other.SetHeaderLength (headerLength); return SetHeaderLength (headerLength); }
        
        public double MaxAmplitude_Call   () => MaxAmplitude();
        public double GetMaxAmplitude_Call() => GetMaxAmplitude();
        
        public int SizeOfBitDepth_Call   () => SizeOfBitDepth();
        public int GetSizeOfBitDepth_Call() => GetSizeOfBitDepth();
        public SynthWishes SizeOfBitDepth_Call    (int? sizeOfBitDepth) { _other.SizeOfBitDepth    (sizeOfBitDepth); return SizeOfBitDepth    (sizeOfBitDepth); }
        public SynthWishes WithSizeOfBitDepth_Call(int? sizeOfBitDepth) { _other.WithSizeOfBitDepth(sizeOfBitDepth); return WithSizeOfBitDepth(sizeOfBitDepth); }
        public SynthWishes SetSizeOfBitDepth_Call (int? sizeOfBitDepth) { _other.SetSizeOfBitDepth (sizeOfBitDepth); return SetSizeOfBitDepth (sizeOfBitDepth); }
        
        // Misc Settings
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double LeafCheckTimeOut_Call()  => LeafCheckTimeOut();
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public double GetLeafCheckTimeOut_Call => GetLeafCheckTimeOut;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes WithLeafCheckTimeOut_Call(double? seconds) { _other.WithLeafCheckTimeOut(seconds); return WithLeafCheckTimeOut(seconds); }
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes SetLeafCheckTimeOut_Call (double? seconds) { _other.SetLeafCheckTimeOut (seconds); return SetLeafCheckTimeOut (seconds); }
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes LeafCheckTimeOut_Call    (double? seconds) { _other.LeafCheckTimeOut    (seconds); return LeafCheckTimeOut    (seconds); }
        
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public TimeOutActionEnum TimeOutAction_Call()  => TimeOutAction();
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public TimeOutActionEnum GetTimeOutAction_Call => GetTimeOutAction;
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes WithTimeOutAction_Call(TimeOutActionEnum? action) { _other.WithTimeOutAction(action); return WithTimeOutAction(action); }
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes SetTimeOutAction_Call (TimeOutActionEnum? action) { _other.SetTimeOutAction (action); return SetTimeOutAction (action); }
        /// <inheritdoc cref="docs._leafchecktimeout" />
        public SynthWishes TimeOutAction_Call    (TimeOutActionEnum? action) { _other.TimeOutAction    (action); return TimeOutAction    (action); }
        
        public int CourtesyFrames_Call()  => CourtesyFrames();
        public int GetCourtesyFrames_Call => GetCourtesyFrames;
        public SynthWishes CourtesyFrames_Call    (int? value) { _other.CourtesyFrames    (value); return CourtesyFrames    (value); }
        public SynthWishes WithCourtesyFrames_Call(int? value) { _other.WithCourtesyFrames(value); return WithCourtesyFrames(value); }
        public SynthWishes SetCourtesyFrames_Call (int? value) { _other.SetCourtesyFrames (value); return SetCourtesyFrames (value); }
        
        public int GetFileExtensionMaxLength_Call => GetFileExtensionMaxLength;
        
        public bool IsUnderNCrunch_Call
        {
            get => IsUnderNCrunch;
            set => _other.IsUnderNCrunch = IsUnderNCrunch = value;
        }
        
        public bool IsUnderAzurePipelines_Call
        {
            get => IsUnderAzurePipelines;
            set => _other.IsUnderAzurePipelines = IsUnderAzurePipelines = value;
        }
    }
}