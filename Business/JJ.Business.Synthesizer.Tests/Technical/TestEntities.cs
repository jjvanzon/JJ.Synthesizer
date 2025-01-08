using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;

#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    internal class TapeEntities
    {
        public TapeBoundEntities TapeBound { get; set; } = new TapeBoundEntities();
        
        public class TapeBoundEntities
        {
            public Tape              Tape                { get; set; }
            public TapeConfig        TapeConfig          { get; set; }
            public TapeActions       TapeActions         { get; set; }
            public TapeAction        TapeAction          { get; set; }
        }
                                 
        public BuffBoundEntities BuffBound { get; set; } = new BuffBoundEntities();
        
        public class BuffBoundEntities
        {
            public Buff               Buff                { get; set; }
            public AudioFileOutput    AudioFileOutput     { get; set; }
        }
         
        public IndependentEntities Independent { get; set; } = new IndependentEntities(); // Independent after Taping
        
        public class IndependentEntities
        { 
            public Sample             Sample              { get; set; }
            public AudioInfoWish      AudioInfoWish       { get; set; }
            public AudioFileInfo      AudioFileInfo       { get; set; }
        }   
        
        // Immutable
        public ImmutableEntities Immutable { get; set; } = new ImmutableEntities();
        public class ImmutableEntities
        {
            public WavHeaderStruct    WavHeader           { get; set; }
            public SampleDataTypeEnum SampleDataTypeEnum  { get; set; }
            public SampleDataType     SampleDataType      { get; set; }
            public Type               Type                { get; set; }
            public int                Channels            { get; set; }
            public SpeakerSetupEnum   SpeakerSetupEnum    { get; set; }
            public SpeakerSetup       SpeakerSetup        { get; set; }
            public int?               Channel             { get; set; }
            public ChannelEnum        ChannelEnum         { get; set; }
            public Channel            ChannelEntity       { get; set; }
        }
    }
    
    internal class TestEntities : TapeEntities
    {   
        // Global-Bound (read-only)
        public static ConfigSectionAccessor GetConfigSectionAccessor() => new ConfigWishesAccessor(new SynthWishes().Config)._section;
        
        public SynthBoundEntities SynthBound { get; set; } = new SynthBoundEntities();
        
        public class SynthBoundEntities
        {
            public SynthWishes  SynthWishes  { get; set; }
            public IContext     Context      { get; set; }
            public FlowNode     FlowNode     { get; set; }
            public FlowNode     FlowNode2    { get; set; }
            public ConfigWishes ConfigWishes { get; set; }
        }
                                                       
        // Tape-Bound
        public IList<TapeEntities> ChannelEntities { get; private set; }
        
        public TestEntities(Action<SynthWishes> initialize, IContext context = null) => Initialize(initialize, context);
        
        public void Initialize(Action<SynthWishes> initialize, IContext context = null)
        {
            var synthWishes = new SynthWishes(context);
            
            SynthBound = new SynthBoundEntities
            {
                SynthWishes  = synthWishes,
                Context      = synthWishes.Context,
                ConfigWishes = synthWishes.Config,
                FlowNode     = synthWishes.Sine(),
                FlowNode2    = synthWishes.Sine() / 2
            };
            
            // Initialize
            SynthBound.SynthWishes.WithSamplingRate(8);
            initialize?.Invoke(SynthBound.SynthWishes);
            
            Record();
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
            SynthBound.SynthWishes.RunOnThis(
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
                              WavHeader          = t.UnderlyingSample.ToWavHeader(),
                              Channels           = t.Config.Channels,
                              SampleDataTypeEnum = t.UnderlyingSample.GetSampleDataTypeEnum(),
                              SampleDataType     = t.UnderlyingSample.SampleDataType,
                              SpeakerSetupEnum   = t.UnderlyingSample.GetSpeakerSetupEnum(),
                              SpeakerSetup       = t.UnderlyingSample.SpeakerSetup,
                              Channel            = t.Config.Channel,
                              ChannelEnum        = t.Config.Channel.ChannelToEnum(t.Config.Channels),
                              ChannelEntity      = t.Config.Channel.ChannelToEntity(t.Config.Channels, SynthBound.Context)
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
                              WavHeader          = t.UnderlyingSample.ToWavHeader(),
                              Channels           = t.Config.Channels,
                              SampleDataTypeEnum = t.UnderlyingSample.GetSampleDataTypeEnum(),
                              SampleDataType     = t.UnderlyingSample.SampleDataType,
                              SpeakerSetupEnum   = t.UnderlyingSample.GetSpeakerSetupEnum(),
                              SpeakerSetup       = t.UnderlyingSample.SpeakerSetup,
                              Channel            = t.Config.Channel,
                              ChannelEnum        = t.Config.Channel.ChannelToEnum(t.Config.Channels),
                              ChannelEntity      = t.Config.Channel.ChannelToEntity(t.Config.Channels, SynthBound.Context)
                          };
                      }));
            
            //WavHeader                  = SynthBound.SynthWishes.ToWish().ToWavHeader(),
            Immutable.Channels           = SynthBound.SynthWishes.GetChannels;
            Immutable.SampleDataTypeEnum = SynthBound.SynthWishes.GetBits.BitsToEnum();
            Immutable.SampleDataType     = SynthBound.SynthWishes.GetBits.BitsToEntity(SynthBound.Context);
            Immutable.SpeakerSetupEnum   = SynthBound.SynthWishes.GetChannels.ChannelsToEnum();
            Immutable.SpeakerSetup       = SynthBound.SynthWishes.GetChannels.ChannelsToEntity(SynthBound.Context);
            Immutable.Channel            = SynthBound.SynthWishes.GetChannel;
            Immutable.ChannelEnum        = SynthBound.SynthWishes.GetChannel.ChannelToEnum(SynthBound.SynthWishes.GetChannels);
            Immutable.ChannelEntity      = SynthBound.SynthWishes.GetChannel.ChannelToEntity(SynthBound.SynthWishes.GetChannels, SynthBound.SynthWishes.Context);
            Immutable.Type               = TypeFromBits(SynthBound.SynthWishes.GetBits);
        
            IsNotNull(() => TapeBound.Tape);
            // TODO: Assert more nulls
}
        
        Type TypeFromBits(int bits)
        {
            switch (bits)
            {
                case 8:  return typeof(byte);
                case 16: return typeof(short);
                case 32: return typeof(float);
                default: throw new Exception($"{new { bits }} not supported.");
            }
        }
    }
}