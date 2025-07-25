﻿using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.Logging;
using static System.GC;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.Legacy.AssertHelper;
using static JJ.Framework.IO.Core.FileHelperCore;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class SynthBoundEntities
    {
        public override string        ToString() => DebuggerDisplay(this);
        public SynthWishes            SynthWishes          { get; set; }
        /// <inheritdoc cref="_synthwishesderived" />
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
        public string          SourceFilePath      { get; set; }
        public byte[]          SourceBytes         { get; set; }
        public Stream          SourceStream        { get; set; }
        public BinaryReader    BinaryReader        { get; set; }
        public string          DestFilePath        { get; set; }
        public byte[]          DestBytes           { get; set; }
        public Stream          DestStream          { get; set; }
        public BinaryWriter    BinaryWriter        { get; set; }
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
        readonly SynthWishes _synthWishes;
        public ImmutableEntities(SynthWishes synthWishes) => _synthWishes = synthWishes ?? throw new NullException(() => synthWishes);
        
        public override string       ToString() => DebuggerDisplay(this, _synthWishes);
        public WavHeaderStruct       WavHeader           { get; set; }
        public SampleDataTypeEnum    SampleDataTypeEnum  { get; set; }
        public SampleDataType        SampleDataType      { get; set; }
        public int                   Bits                { get; set; }
        public int                   SizeOfBitDepth      { get; set; }
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
        public int                   CourtesyBytes       { get; set; }
        public int                   FrameSize           { get; set; }
        public (int bits,      int channels, int samplingRate, int frameCount) InfoTupleWithInts    { get; set; }
        public (Type bitsType, int channels, int samplingRate, int frameCount) InfoTupleWithType    { get; set; }
        public (               int channels, int samplingRate, int frameCount) InfoTupleWithoutBits { get; set; }
        public (SampleDataTypeEnum bitsEnum,   SpeakerSetupEnum channelsEnum,   int samplingRate, int frameCount) InfoTupleWithEnums    { get; set; }
        public (SampleDataType     bitsEntity, SpeakerSetup     channelsEntity, int samplingRate, int frameCount) InfoTupleWithEntities { get; set; }
    }

    internal class TapeEntities
    {
        public override string     ToString() => DebuggerDisplay(this);
        public TapeBoundEntities   TapeBound   { get; set; } = new TapeBoundEntities();
        public BuffBoundEntities   BuffBound   { get; set; } = new BuffBoundEntities();
        public IndependentEntities Independent { get; set; } = new IndependentEntities(); // Independent after Taping
        public ImmutableEntities   Immutable   { get; set; } 
    }
    
    internal class TestEntities : TapeEntities, IDisposable
    {   
        public const int HighPerfHz = 8;
        
        public override string ToString() => DebuggerDisplay(this);
        public SynthBoundEntities  SynthBound { get; set; } = new SynthBoundEntities();
        public IList<TapeEntities> ChannelEntities { get; private set; } // Tape-Bound

        public bool Disposed { get; private set; }
        
        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            
            BuffBound?.DestStream?.Dispose();
            BuffBound?.BinaryWriter?.Dispose();
        
            if (ChannelEntities != null)
            {
                foreach (var tapeEntities in ChannelEntities)
                {
                    tapeEntities?.BuffBound?.DestStream?.Dispose();
                    tapeEntities?.BuffBound?.BinaryWriter?.Dispose();
                }
            }
            
            SuppressFinalize(this);
        }

        ~TestEntities()
        {
            Dispose();
        }

        public TestEntities([CallerMemberName] string name = null)
            => Initialize(default, default, default, name);
        
        public TestEntities(Action<SynthWishes> initialize, [CallerMemberName] string name = null)
            => Initialize(initialize, default, default, name);
        
        public TestEntities(IContext context, [CallerMemberName] string name = null) 
            => Initialize(default, context, default, name);
        
        public TestEntities(Action<SynthWishes> initialize, IContext context, [CallerMemberName] string name = null)
            => Initialize(initialize, context, default, name);

        // Name in Front
        
        public TestEntities(string name, Action<SynthWishes> initialize)
            => Initialize(initialize, default, default, name);
        
        public TestEntities(string name, IContext context) 
            => Initialize(default, context, default, name);
        
        public TestEntities(string name, Action<SynthWishes> initialize, IContext context)
            => Initialize(initialize, context, default, name);
        
        // WithDisk
        
        public TestEntities(Action<SynthWishes> initialize, bool withDisk, [CallerMemberName] string name = null)
            => Initialize(initialize, default, withDisk, name);
        
        public TestEntities(IContext context, bool withDisk, [CallerMemberName] string name = null) 
            => Initialize(default, context, withDisk, name);
        
        public TestEntities(Action<SynthWishes> initialize, IContext context, bool withDisk, [CallerMemberName] string name = null)
            => Initialize(initialize, context, withDisk, name);

        // WithDisk in Front
        
        public TestEntities(bool withDisk, [CallerMemberName] string name = null)
            => Initialize(default, default, withDisk, name);
        
        public TestEntities(bool withDisk, Action<SynthWishes> initialize, [CallerMemberName] string name = null) 
            => Initialize(initialize, default, withDisk, name);

        public void Initialize(Action<SynthWishes> initialize, IContext context = null, bool withDisk = false, [CallerMemberName] string name = null)
        {
            var synthWishes = new SynthWishes(context);
            var synthWishesInherited = new SynthWishesDerived(synthWishes);
            var synthWishesAccessor = new SynthWishesAccessor(synthWishes);
            synthWishesAccessor._config._section = CreateConfigSectionWithDefaults();

            SynthBound = new SynthBoundEntities
            {
                SynthWishes         = synthWishes,
                SynthWishesAccessor = synthWishesAccessor,
                Derived             = synthWishesInherited,
                Context             = synthWishes.Context,
                ConfigResolver      = synthWishesAccessor._config,
                ConfigSection       = synthWishesAccessor._config._section,
            };
            
            initialize?.Invoke(synthWishes);

            SynthBound.FlowNode  = synthWishes.Sine();
            SynthBound.FlowNode2 = synthWishes.Sine() / 2;

            SynthBound.FlowNode.SetName(name);
            SynthBound.FlowNode2.SetName(name);
            
            Record(withDisk);
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
        
        private static readonly object _lockAroundFileIOJustInCase = new object();
        
        public void Record(bool withDisk = false)
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
                              AudioFileOutput = t.UnderlyingAudioFileOutput,
                          };
                          
                          Independent = new IndependentEntities
                          {
                              Sample        = t.UnderlyingSample,
                              AudioInfoWish = t.ToInfo(),
                              AudioFileInfo = t.ToInfo().ToLegacy()
                          };
                          
                          Immutable = new ImmutableEntities(SynthBound.SynthWishes)
                          {
                              Bits                  = t.Config.Bits,
                              SizeOfBitDepth        = t.Config.SizeOfBitDepth(),
                              SampleDataTypeEnum    = t.UnderlyingSample.GetSampleDataTypeEnum(),
                              SampleDataType        = t.UnderlyingSample.SampleDataType,
                              SamplingRate          = t.Config.SamplingRate,
                              Channels              = t.Config.Channels,
                              SpeakerSetupEnum      = t.UnderlyingSample.GetSpeakerSetupEnum(),
                              SpeakerSetup          = t.UnderlyingSample.SpeakerSetup,
                              Channel               = t.Config.Channel,
                              ChannelEnum           = t.Config.Channel.ChannelToEnum(t.Config.Channels),
                              ChannelEntity         = t.Config.Channel.ChannelToEntity(t.Config.Channels, SynthBound.Context),
                              Type                  = t.Config.Bits.BitsToType(),
                              Interpolation         = t.Config.Interpolation,
                              InterpolationEntity   = t.UnderlyingSample.InterpolationType,
                              AudioFormat           = t.Config.AudioFormat,
                              AudioFormatEntity     = t.UnderlyingSample.AudioFileFormat,
                              AudioLength           = t.Duration,
                              WavHeader             = t.Config.AudioFormat == Wav ? t.UnderlyingSample.ToWavHeader() : default,
                              FileExtension         = ResolveFileExtension(t.Config.AudioFormat),
                              CourtesyFrames        = t.Config.CourtesyFrames,
                              CourtesyBytes         = t.Config.CourtesyBytes(),
                              FrameSize             = t.Config.FrameSize(),
                              InfoTupleWithInts     = (t.Config.Bits,              t.Config.Channels, t.Config.SamplingRate, t.Config.FrameCount()),
                              InfoTupleWithType     = (t.Config.Bits.BitsToType(), t.Config.Channels, t.Config.SamplingRate, t.Config.FrameCount()),
                              InfoTupleWithoutBits  = (                            t.Config.Channels, t.Config.SamplingRate, t.Config.FrameCount()),
                              InfoTupleWithEnums    = (t.UnderlyingSample.GetSampleDataTypeEnum(), t.UnderlyingSample.GetSpeakerSetupEnum(), t.Config.SamplingRate, t.Config.FrameCount()),
                              InfoTupleWithEntities = (t.UnderlyingSample.SampleDataType, t.UnderlyingSample.SpeakerSetup, t.Config.SamplingRate, t.Config.FrameCount())
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
                              Buff            = t.Buff,
                              AudioFileOutput = t.UnderlyingAudioFileOutput,
                          };
                          
                          // Independent after Taping Channel
                          e.Independent = new IndependentEntities
                          {
                              Sample        = t.UnderlyingSample,
                              AudioInfoWish = t.ToInfo(),
                              AudioFileInfo = t.ToInfo().ToLegacy()
                          };
                          
                          // Immutables for Channel
                          e.Immutable = new ImmutableEntities(SynthBound.SynthWishes)
                          {
                              Bits                  = t.Config.Bits,
                              SizeOfBitDepth        = t.Config.SizeOfBitDepth(),
                              SampleDataTypeEnum    = t.UnderlyingSample.GetSampleDataTypeEnum(),
                              SampleDataType        = t.UnderlyingSample.SampleDataType,
                              SamplingRate          = t.Config.SamplingRate,
                              Channels              = t.Config.Channels,
                              SpeakerSetupEnum      = t.UnderlyingSample.GetSpeakerSetupEnum(),
                              SpeakerSetup          = t.UnderlyingSample.SpeakerSetup,
                              Channel               = t.Config.Channel,
                              ChannelEnum           = t.Config.Channel.ChannelToEnum(t.Config.Channels),
                              ChannelEntity         = t.Config.Channel.ChannelToEntity(t.Config.Channels, SynthBound.Context),
                              Type                  = t.Config.Bits.BitsToType(),
                              Interpolation         = t.Config.Interpolation,
                              InterpolationEntity   = t.UnderlyingSample.InterpolationType,
                              AudioFormat           = t.Config.AudioFormat,
                              AudioFormatEntity     = t.UnderlyingSample.AudioFileFormat,
                              AudioLength           = t.Duration,
                              WavHeader             = t.Config.AudioFormat == Wav ? t.UnderlyingSample.ToWavHeader() : default,
                              FileExtension         = ResolveFileExtension(t.Config.AudioFormat),
                              CourtesyFrames        = t.Config.CourtesyFrames,
                              CourtesyBytes         = t.Config.CourtesyBytes(),
                              FrameSize             = t.Config.FrameSize(),
                              InfoTupleWithInts     = (t.Config.Bits,              t.Config.Channels, t.Config.SamplingRate, t.Config.FrameCount()),
                              InfoTupleWithType     = (t.Config.Bits.BitsToType(), t.Config.Channels, t.Config.SamplingRate, t.Config.FrameCount()),
                              InfoTupleWithoutBits  = (                            t.Config.Channels, t.Config.SamplingRate, t.Config.FrameCount()),
                              InfoTupleWithEnums    = (t.UnderlyingSample.GetSampleDataTypeEnum(), t.UnderlyingSample.GetSpeakerSetupEnum(), t.Config.SamplingRate, t.Config.FrameCount()),
                              InfoTupleWithEntities = (t.UnderlyingSample.SampleDataType, t.UnderlyingSample.SpeakerSetup, t.Config.SamplingRate, t.Config.FrameCount())
                          };
                      }));
            
            IsNotNull(() => TapeBound);
            IsNotNull(() => TapeBound.Tape);
            IsNotNull(() => TapeBound.TapeConfig);
            IsNotNull(() => TapeBound.TapeActions);
            IsNotNull(() => TapeBound.TapeAction);
            
            IsNotNull(() => BuffBound);
            IsNotNull(() => BuffBound.Buff);
            IsNotNull(() => BuffBound.Buff.Bytes);
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
            
            Independent.AudioInfoWish = SynthBound.SynthWishes.ToInfo();
            Independent.AudioFileInfo = SynthBound.SynthWishes.ToInfo().ToLegacy();
            
            Immutable.Bits                  = SynthBound.SynthWishes.GetBits;
            Immutable.SizeOfBitDepth        = SynthBound.SynthWishes.GetSizeOfBitDepth();
            Immutable.SampleDataTypeEnum    = SynthBound.SynthWishes.GetBits.BitsToEnum();
            Immutable.SampleDataType        = SynthBound.SynthWishes.GetBits.BitsToEntity(SynthBound.Context);
            Immutable.SamplingRate          = SynthBound.SynthWishes.GetSamplingRate;
            Immutable.Channels              = SynthBound.SynthWishes.GetChannels;
            Immutable.SpeakerSetupEnum      = SynthBound.SynthWishes.GetChannels.ChannelsToEnum();
            Immutable.SpeakerSetup          = SynthBound.SynthWishes.GetChannels.ChannelsToEntity(SynthBound.Context);
            Immutable.Channel               = SynthBound.SynthWishes.GetChannel;
            Immutable.ChannelEnum           = SynthBound.SynthWishes.GetChannel.ChannelToEnum(SynthBound.SynthWishes.GetChannels);
            Immutable.ChannelEntity         = SynthBound.SynthWishes.GetChannel.ChannelToEntity(SynthBound.SynthWishes.GetChannels, SynthBound.SynthWishes.Context);
            Immutable.Type                  = SynthBound.SynthWishes.GetBits.BitsToType();
            Immutable.Interpolation         = SynthBound.SynthWishes.GetInterpolation;
            Immutable.InterpolationEntity   = SynthBound.SynthWishes.GetInterpolation.ToEntity(SynthBound.Context);
            Immutable.AudioFormat           = SynthBound.SynthWishes.GetAudioFormat;
            Immutable.AudioFormatEntity     = SynthBound.SynthWishes.GetAudioFormat.ToEntity(SynthBound.Context);
            Immutable.AudioLength           = SynthBound.SynthWishes.GetAudioLength.Value;
            Immutable.WavHeader             = SynthBound.SynthWishes.GetAudioFormat == Wav ? SynthBound.SynthWishes.ToWavHeader() : default;
            Immutable.FileExtension         = ResolveFileExtension(SynthBound.SynthWishes.GetAudioFormat);
            Immutable.CourtesyFrames        = SynthBound.SynthWishes.GetCourtesyFrames;
            Immutable.CourtesyBytes         = SynthBound.SynthWishes.GetCourtesyBytes;
            Immutable.FrameSize             = SynthBound.SynthWishes.GetFrameSize;
            Immutable.InfoTupleWithInts     = (SynthBound.SynthWishes.GetBits, SynthBound.SynthWishes.GetChannels, SynthBound.SynthWishes.GetSamplingRate, SynthBound.SynthWishes.FrameCount());
            Immutable.InfoTupleWithType     = (SynthBound.SynthWishes.GetBits.BitsToType(), SynthBound.SynthWishes.GetChannels, SynthBound.SynthWishes.GetSamplingRate, SynthBound.SynthWishes.FrameCount());
            Immutable.InfoTupleWithoutBits  = (SynthBound.SynthWishes.GetChannels, SynthBound.SynthWishes.GetSamplingRate, SynthBound.SynthWishes.FrameCount());
            Immutable.InfoTupleWithEnums    = (SynthBound.SynthWishes.GetBits.BitsToEnum(), SynthBound.SynthWishes.GetChannels.ChannelsToEnum(), SynthBound.SynthWishes.GetSamplingRate, SynthBound.SynthWishes.FrameCount());
            Immutable.InfoTupleWithEntities = (SynthBound.SynthWishes.GetBits.BitsToEntity(SynthBound.Context), SynthBound.SynthWishes.GetChannels.ChannelsToEntity(SynthBound.Context), SynthBound.SynthWishes.GetSamplingRate, SynthBound.SynthWishes.FrameCount());

            BuffBound.SourceBytes  = BuffBound.Buff.Bytes;
            BuffBound.SourceStream = new MemoryStream(BuffBound.Buff.Bytes);
            BuffBound.BinaryReader = new BinaryReader(new MemoryStream(BuffBound.Buff.Bytes));

            BuffBound.DestBytes = new byte[BuffBound.Buff.Bytes.Length];
            BuffBound.DestStream = new MemoryStream();
            BuffBound.BinaryWriter = new BinaryWriter(new MemoryStream());
            
            if (withDisk)
            {
                lock (_lockAroundFileIOJustInCase)
                {
                    // SourceFilePath
                    string filePathBase = TapeBound.Tape.GetFilePath(BuffBound.Buff.FilePath);
                    BuffBound.Buff.Save(filePathBase);
                    BuffBound.SourceFilePath = BuffBound.Buff.FilePath;
                    
                    // DestFilePath
                    Stream tempStream = null;
                    try
                    {
                        (BuffBound.DestFilePath, tempStream) = CreateSafeFileStream(filePathBase);
                        BuffBound.DestFilePath.LogFileAction(SynthBound.SynthWishes);
                    }
                    finally
                    {
                        tempStream?.Dispose(); // Just for file creation.
                    }
                }
            }
        }
    }
}