using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.Testing.AssertHelper;

#pragma warning disable CS0618

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    internal class SynthBoundEntities
    {
        public override string        ToString() => DebuggerDisplay(this);
        public SynthWishes            SynthWishes          { get; set; }
        /// <inheritdoc cref="docs._synthwishesderived" />
        public SynthWishesDerived     Derived { get; set; }
        public SynthWishesAccessor    SynthWishesAccessor  { get; set; }
        public IContext               Context              { get; set; }
        public FlowNode               FlowNode             { get; set; }
        public FlowNode               FlowNode2            { get; set; }
        public ConfigResolverAccessor ConfigResolver       { get; set; }
        public ConfigSectionAccessor  ConfigSection        { get; set; }
    }

    internal class TapeBoundEntities
    {
        public override string ToString() => DebuggerDisplay(this);
        public Tape            Tape                { get; set; }
        public TapeConfig      TapeConfig          { get; set; }
        public TapeActions     TapeActions         { get; set; }
        public TapeAction      TapeAction          { get; set; }
    }
    
    internal class BuffBoundEntities
    {
        public override string ToString() => DebuggerDisplay(this);
        public Buff            Buff                { get; set; }
        public AudioFileOutput AudioFileOutput     { get; set; }
    }
            
    internal class IndependentEntities
    { 
        public override string ToString() => DebuggerDisplay(this);
        public Sample          Sample              { get; set; }
        public AudioInfoWish   AudioInfoWish       { get; set; }
        public AudioFileInfo   AudioFileInfo       { get; set; }
    }   

    internal class ImmutableEntities
    {
        public override string       ToString() => DebuggerDisplay(this);
        public WavHeaderStruct       WavHeader           { get; set; }
        public SampleDataTypeEnum    SampleDataTypeEnum  { get; set; }
        public SampleDataType        SampleDataType      { get; set; }
        public int                   Bits                { get; set; }
        public Type                  Type                { get; set; }
        public int                   SamplingRate        { get; set; }
        public int                   Channels            { get; set; }
        public SpeakerSetupEnum      SpeakerSetupEnum    { get; set; }
        public SpeakerSetup          SpeakerSetup        { get; set; }
        public int?                  Channel             { get; set; }
        public ChannelEnum           ChannelEnum         { get; set; }
        public Channel               ChannelEntity       { get; set; }
        public InterpolationTypeEnum Interpolation       { get; set; }
        public InterpolationType     InterpolationEntity { get; set; }
        public AudioFileFormatEnum   AudioFormat         { get; set; }
        public AudioFileFormat       AudioFormatEntity   { get; set; }
        public double                AudioLength         { get; set; }
        public string                FileExtension       { get; set; }
        public int                   CourtesyFrames      { get; set; }
        public int                   FrameSize           { get; set; }
    }

    internal class TapeEntities
    {
        public override string     ToString() => DebuggerDisplay(this);
        public TapeBoundEntities   TapeBound   { get; set; } = new TapeBoundEntities();
        public BuffBoundEntities   BuffBound   { get; set; } = new BuffBoundEntities();
        public IndependentEntities Independent { get; set; } = new IndependentEntities(); // Independent after Taping
        public ImmutableEntities   Immutable   { get; set; } = new ImmutableEntities();
    }
    
    internal class ConfigTestEntities : TapeEntities
    {   
        public override string     ToString() => DebuggerDisplay(this);
        public SynthBoundEntities  SynthBound { get; set; } = new SynthBoundEntities();
        public IList<TapeEntities> ChannelEntities { get; private set; } // Tape-Bound

        public ConfigTestEntities(IContext context = null) => Initialize(null, context);
        
        public ConfigTestEntities(Action<SynthWishes> initialize, IContext context = null) => Initialize(initialize, context);
        
        public void Initialize(Action<SynthWishes> initialize, IContext context = null)
        {
            var synthWishes = new SynthWishes(context);
            var synthWishesInherited = new SynthWishesDerived(synthWishes);
            var synthWishesAccessor = new SynthWishesAccessor(synthWishes);
            synthWishesAccessor.Config._section = CreateConfigSectionWithDefaults();

            SynthBound = new SynthBoundEntities
            {
                SynthWishes          = synthWishes,
                SynthWishesAccessor  = synthWishesAccessor,
                Derived = synthWishesInherited,
                Context              = synthWishes.Context,
                ConfigResolver       = synthWishesAccessor.Config,
                ConfigSection        = synthWishesAccessor.Config._section,
                FlowNode             = synthWishes.Sine(),
                FlowNode2            = synthWishes.Sine() / 2
            };
            
            // Initialize
            initialize?.Invoke(SynthBound.SynthWishes);
            
            Record();
        }
        
        private ConfigSectionAccessor CreateConfigSectionWithDefaults()
        {
            var configSection = new ConfigSectionAccessor
            {
                Bits                   = DefaultBits,
                Channels               = DefaultChannels,
                SamplingRate           = DefaultSamplingRate,
                AudioFormat            = DefaultAudioFormat,
                Interpolation          = DefaultInterpolation,
                CourtesyFrames         = DefaultCourtesyFrames,
                NoteLength             = DefaultNoteLength,
                BarLength              = DefaultBarLength,
                BeatLength             = DefaultBeatLength,
                AudioLength            = DefaultAudioLength,
                LeadingSilence         = DefaultLeadingSilence,
                TrailingSilence        = DefaultTrailingSilence,
                AudioPlayback          = DefaultAudioPlayback,
                DiskCache              = DefaultDiskCache,
                MathBoost              = DefaultMathBoost,
                ParallelProcessing     = DefaultParallelProcessing,
                PlayAllTapes           = DefaultPlayAllTapes,
                LeafCheckTimeOut       = DefaultLeafCheckTimeOut,
                TimeOutAction          = DefaultTimeOutAction,
                FileExtensionMaxLength = DefaultFileExtensionMaxLength,
                LongTestCategory       = DefaultLongTestCategory,
                NCrunch = new ConfigToolingElementAccessor
                {
                    AudioPlayback           = DefaultToolingAudioPlayback,
                    ImpersonationMode       = DefaultToolingImpersonationMode,
                    SamplingRate            = DefaultNCrunchSamplingRate,
                    SamplingRateLongRunning = DefaultNCrunchSamplingRateLongRunning
                },
                AzurePipelines = new ConfigToolingElementAccessor
                {
                    AudioPlayback           = DefaultToolingAudioPlayback,
                    ImpersonationMode       = DefaultToolingImpersonationMode,
                    SamplingRate            = DefaultAzurePipelinesSamplingRate,
                    SamplingRateLongRunning = DefaultAzurePipelinesSamplingRateLongRunning
                }
            };
            
            return configSection;
        }
        
        public void Record()
        {
            int channelCount = SynthBound.SynthWishes.GetChannels;
            //if (TapeBound.Tape != null) channelCount = TapeBound.Tape.Config.Channels;
            
            ChannelEntities = new TapeEntities[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                ChannelEntities[i] = new TapeEntities();    
            }
            
            // Record
            SynthBound.SynthWishes.RunOnThisOne(
                () => (SynthBound.SynthWishes.GetChannel == 0 ? SynthBound.FlowNode : SynthBound.FlowNode2)
                      .AfterRecord(t =>
                      {
                          TapeBound = new TapeBoundEntities
                          {
                              Tape        = t,
                              TapeConfig  = t.Config,
                              TapeActions = t.Actions,
                              TapeAction  = t.Actions.AfterRecord
                          };
                          
                          BuffBound = new BuffBoundEntities
                          {
                              Buff            = t.Buff,
                              AudioFileOutput = t.UnderlyingAudioFileOutput
                          };
                          
                          Independent = new IndependentEntities
                          {
                              Sample        = t.UnderlyingSample,
                              AudioInfoWish = t.UnderlyingSample.ToWish(),
                              AudioFileInfo = t.UnderlyingSample.ToWish().FromWish()
                          };
                          
                          Immutable = new ImmutableEntities
                          {
                              Bits                = t.Config.Bits,
                              SampleDataTypeEnum  = t.UnderlyingSample.GetSampleDataTypeEnum(),
                              SampleDataType      = t.UnderlyingSample.SampleDataType,
                              SamplingRate        = t.Config.SamplingRate,
                              Channels            = t.Config.Channels,
                              SpeakerSetupEnum    = t.UnderlyingSample.GetSpeakerSetupEnum(),
                              SpeakerSetup        = t.UnderlyingSample.SpeakerSetup,
                              Channel             = t.Config.Channel,
                              ChannelEnum         = t.Config.Channel.ChannelToEnum(t.Config.Channels),
                              ChannelEntity       = t.Config.Channel.ChannelToEntity(t.Config.Channels, SynthBound.Context),
                              Type                = t.Config.Bits.BitsToType(),
                              Interpolation       = t.Config.Interpolation,
                              InterpolationEntity = t.UnderlyingSample.InterpolationType,
                              AudioFormat         = t.Config.AudioFormat,
                              AudioFormatEntity   = t.UnderlyingSample.AudioFileFormat,
                              AudioLength         = t.Duration,
                              WavHeader           = t.Config.AudioFormat == Wav ? t.UnderlyingSample.ToWavHeader() : default,
                              FileExtension       = ResolveFileExtension(t.Config.AudioFormat),
                              CourtesyFrames      = t.Config.CourtesyFrames,
                              FrameSize           = t.Config.FrameSize()
                          };
                      })
                      .AfterRecordChannel(t =>
                      {
                          var e = ChannelEntities[t.i];
                          
                          e.TapeBound = new TapeBoundEntities
                          {
                              Tape        = t,
                              TapeConfig  = t.Config,
                              TapeActions = t.Actions,
                              TapeAction  = t.Actions.AfterRecordChannel
                          };
                          
                          e.BuffBound = new BuffBoundEntities
                          {
                              Buff               = t.Buff,
                              AudioFileOutput    = t.UnderlyingAudioFileOutput
                          };
                          
                          // Independent after Taping Channel
                          e.Independent = new IndependentEntities
                          {
                              Sample             = t.UnderlyingSample,
                              AudioInfoWish      = t.UnderlyingSample.ToWish(),
                              AudioFileInfo      = t.UnderlyingSample.ToWish().FromWish()
                          };
                          
                          // Immutables for Channel
                          e.Immutable = new ImmutableEntities
                          {
                              Bits                = t.Config.Bits,
                              SampleDataTypeEnum  = t.UnderlyingSample.GetSampleDataTypeEnum(),
                              SampleDataType      = t.UnderlyingSample.SampleDataType,
                              SamplingRate        = t.Config.SamplingRate,
                              Channels            = t.Config.Channels,
                              SpeakerSetupEnum    = t.UnderlyingSample.GetSpeakerSetupEnum(),
                              SpeakerSetup        = t.UnderlyingSample.SpeakerSetup,
                              Channel             = t.Config.Channel,
                              ChannelEnum         = t.Config.Channel.ChannelToEnum(t.Config.Channels),
                              ChannelEntity       = t.Config.Channel.ChannelToEntity(t.Config.Channels, SynthBound.Context),
                              Type                = t.Config.Bits.BitsToType(),
                              Interpolation       = t.Config.Interpolation,
                              InterpolationEntity = t.UnderlyingSample.InterpolationType,
                              AudioFormat         = t.Config.AudioFormat,
                              AudioFormatEntity   = t.UnderlyingSample.AudioFileFormat,
                              AudioLength         = t.Duration,
                              WavHeader           = t.Config.AudioFormat == Wav ? t.UnderlyingSample.ToWavHeader() : default,
                              FileExtension       = ResolveFileExtension(t.Config.AudioFormat),
                              CourtesyFrames      = t.Config.CourtesyFrames,
                              FrameSize           = t.Config.FrameSize()
                          };
                      }));
            
            IsNotNull(() => TapeBound);
            IsNotNull(() => TapeBound.Tape);
            IsNotNull(() => TapeBound.TapeConfig);
            IsNotNull(() => TapeBound.TapeActions);
            IsNotNull(() => TapeBound.TapeAction);
            
            IsNotNull(() => BuffBound);
            IsNotNull(() => BuffBound.Buff);
            IsNotNull(() => BuffBound.AudioFileOutput);
            
            IsNotNull(() => Independent);
            IsNotNull(() => Independent.Sample);
            IsNotNull(() => Independent.AudioInfoWish);
            IsNotNull(() => Independent.AudioFileInfo);

            IsNotNull(() => Immutable);
            IsNotNull(() => Immutable.SampleDataType);
            IsNotNull(() => Immutable.Type);
            IsNotNull(() => Immutable.SpeakerSetup);
            // Is nullable: Immutable.ChannelEntity
            IsNotNull(() => Immutable.InterpolationEntity);
            IsNotNull(() => Immutable.AudioFormatEntity);
            // TODO: Assert other types of being filled in.
            
            Immutable.Bits                = SynthBound.SynthWishes.GetBits;
            Immutable.SampleDataTypeEnum  = SynthBound.SynthWishes.GetBits.BitsToEnum();
            Immutable.SampleDataType      = SynthBound.SynthWishes.GetBits.BitsToEntity(SynthBound.Context);
            Immutable.SamplingRate        = SynthBound.SynthWishes.GetSamplingRate;
            Immutable.Channels            = SynthBound.SynthWishes.GetChannels;
            Immutable.SpeakerSetupEnum    = SynthBound.SynthWishes.GetChannels.ChannelsToEnum();
            Immutable.SpeakerSetup        = SynthBound.SynthWishes.GetChannels.ChannelsToEntity(SynthBound.Context);
            Immutable.Channel             = SynthBound.SynthWishes.GetChannel;
            Immutable.ChannelEnum         = SynthBound.SynthWishes.GetChannel.ChannelToEnum(SynthBound.SynthWishes.GetChannels);
            Immutable.ChannelEntity       = SynthBound.SynthWishes.GetChannel.ChannelToEntity(SynthBound.SynthWishes.GetChannels, SynthBound.SynthWishes.Context);
            Immutable.Type                = SynthBound.SynthWishes.GetBits.BitsToType();
            Immutable.Interpolation       = SynthBound.SynthWishes.GetInterpolation;
            Immutable.InterpolationEntity = SynthBound.SynthWishes.GetInterpolation.ToEntity(SynthBound.Context);
            Immutable.AudioFormat         = SynthBound.SynthWishes.GetAudioFormat;
            Immutable.AudioFormatEntity   = SynthBound.SynthWishes.GetAudioFormat.ToEntity(SynthBound.Context);
            Immutable.AudioLength         = SynthBound.SynthWishes.GetAudioLength.Value;
            // TODO: Revisit after adding more WavHeaderWishes
            //Immutable.WavHeader          = SynthBound.SynthWishes.GetAudioFormat == Wav ? SynthBound.SynthWishes.ToWavHeader() : default;
            //Immutable.WavHeader          = SynthBound.SynthWishes.GetAudioFormat == Wav ? SynthBound.SynthWishes.ToWish().ToWavHeader() : default;
            Immutable.FileExtension       = ResolveFileExtension(SynthBound.SynthWishes.GetAudioFormat);
            Immutable.CourtesyFrames      = SynthBound.SynthWishes.GetCourtesyFrames;
            Immutable.FrameSize           = SynthBound.SynthWishes.FrameSize();
        }
    }
    
    /// <inheritdoc cref="docs._synthwishesderived" />
    internal class SynthWishesDerived : SynthWishes
    {
        readonly SynthWishes _other;
        
        /// <inheritdoc cref="docs._synthwishesderived" />
        public SynthWishesDerived(SynthWishes other) => _other = other;
        
        // Bits
        
        public SynthWishes Bits_Call(int? bits)
        {
            _other.Bits(bits);
            return Bits(bits);
        }

        public SynthWishes WithBits_Call(int? bits)
        {
            _other.WithBits(bits);
            return WithBits(bits);
        }
        
        public SynthWishes With8Bit_Call()
        {
            _other.With8Bit();
            return With8Bit();
        }
        
        public SynthWishes With16Bit_Call()
        {
            _other.With16Bit();
            return With16Bit();
        }
        
        public SynthWishes With32Bit_Call()
        {
            _other.With32Bit();
            return With32Bit();
        }
        
        // Channels
        
        public SynthWishes Channels_Call(int? channels)
        {
            _other.Channels(channels);
            return Channels(channels);
        }
        
        public SynthWishes SetChannels_Call(int? channels)
        {
            _other.SetChannels(channels);
            return SetChannels(channels);
        }
                
        public SynthWishes WithChannels_Call(int? channels)
        {
            _other.WithChannels(channels);
            return WithChannels(channels);
        }

        public SynthWishes Mono_Call()
        {
            _other.Mono();
            return Mono();
        }
                
        public SynthWishes Stereo_Call()
        {
            _other.Stereo();
            return Stereo();
        }
        
        public SynthWishes SetMono_Call()
        {
            _other.SetMono();
            return SetMono();
        }
                
        public SynthWishes SetStereo_Call()
        {
            _other.SetStereo();
            return SetStereo();
        }
        
        public SynthWishes WithMono_Call()
        {
            _other.WithMono();
            return WithMono();
        }

        public SynthWishes WithStereo_Call()
        {
            _other.WithStereo();
            return WithStereo();
        }
    }
}