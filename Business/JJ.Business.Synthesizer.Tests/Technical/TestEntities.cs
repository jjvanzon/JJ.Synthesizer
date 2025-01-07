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
using static JJ.Framework.Testing.AssertHelper;

#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    internal class TapeEntities
    {
        // Tape-Bound
        public Tape               Tape                { get; set; }
        public TapeConfig         TapeConfig          { get; set; }
        public TapeActions        TapeActions         { get; set; }
        public TapeAction         TapeAction          { get; set; }
    }
    
    internal class TestEntities : TapeEntities
    {   
        // Global-Bound (read-only)
        public static ConfigSectionAccessor GetConfigSectionAccessor() => new ConfigWishesAccessor(new SynthWishes().Config)._section;

        // SynthWishes-Bound
        public SynthWishes         SynthWishes         { get; private set; }
        public IContext            Context             { get; private set; }
        public FlowNode            FlowNode            { get; private set; }
        public FlowNode            FlowNode2           { get; private set; }
        public ConfigWishes        ConfigWishes        { get; private set; }
                                                       
        // Tape-Bound
        public IList<TapeEntities> ChannelEntities { get; private set; }
        
        // Buff-Bound
        public Buff                Buff                { get; private set; }
        public AudioFileOutput     AudioFileOutput     { get; private set; }
                                                      
        // Independent after Taping                   
        public Sample              Sample              { get; private set; }
        public AudioInfoWish       AudioInfoWish       { get; private set; }
        public AudioFileInfo       AudioFileInfo       { get; private set; }
                                                      
        // Immutable                                  
        public WavHeaderStruct     WavHeader           { get; private set; }
        public SampleDataTypeEnum  SampleDataTypeEnum  { get; private set; }
        public SampleDataType      SampleDataType      { get; private set; }
        public Type                Type                { get; private set; }
        public SpeakerSetupEnum    SpeakerSetupEnum    { get; private set; }
        public SpeakerSetup        SpeakerSetup        { get; private set; }
        public ChannelEnum         ChannelEnum         { get; private set; }
        public Channel             ChannelEntity       { get; private set; }
        
        public TestEntities(int? bits = default, int? channels = default, int? channel = default) 
            => Initialize(bits, channels, channel);
        
        public TestEntities(Action<SynthWishes> initialize) => Initialize(initialize);
        
        public void Initialize(int? bits = default, int? channels = default, int? channel = default)
        {
            Initialize(x => x.WithBits(bits)
                             .WithChannels(channels)
                             .WithChannel(channel));
        }
        
        public void Initialize(Action<SynthWishes> initialize)
        {
            // SynthWishes-Bound
            SynthWishes  = new SynthWishes();
            Context      = SynthWishes.Context;
            ConfigWishes = SynthWishes.Config;
            FlowNode     = SynthWishes.Sine();
            FlowNode2    = SynthWishes.Sine() / 2;
            
            // Initialize
            SynthWishes.WithSamplingRate(100);
            initialize?.Invoke(SynthWishes);
            
            ChannelEnum   = SynthWishes.GetChannel.ChannelToEnum(SynthWishes.GetChannels);
            ChannelEntity = SynthWishes.GetChannel.ChannelToEntity(SynthWishes.GetChannels, Context);
            
            Record();
        }
        
        public void Record()
        {
            int channelCount = SynthWishes.GetChannels;
            //if (Tape != null) channelCount = Tape.Config.Channels;
            
            ChannelEntities = new TapeEntities[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                ChannelEntities[i] = new TapeEntities();    
            }
            
            // Record
            SynthWishes.RunOnThis(() => (SynthWishes.GetChannel == 0 ? FlowNode : FlowNode2)
                                        // Tape-Bound
                                        .AfterRecord(x => 
                                        {
                                            Tape = x;
                                            TapeConfig  = x.Config;
                                            TapeActions = x.Actions;
                                            TapeAction  = x.Actions.AfterRecord;
                                        })
                                        .AfterRecordChannel(x =>
                                        {
                                            ChannelEntities[x.i].Tape = x;
                                            ChannelEntities[x.i].TapeConfig = x.Config;
                                            ChannelEntities[x.i].TapeActions = x.Actions;
                                            ChannelEntities[x.i].TapeAction = x.Actions.AfterRecordChannel;
                                        }));
            
            IsNotNull(() => Tape);
            
            // Buff-Bound
            Buff            = Tape.Buff;
            AudioFileOutput = Tape.UnderlyingAudioFileOutput;
            
            // Independent after Taping
            Sample        = Tape.UnderlyingSample;
            AudioInfoWish = Sample.ToWish();
            AudioFileInfo = AudioInfoWish.FromWish();
            
            // Immutable
            WavHeader          = Sample.ToWavHeader();
            SampleDataTypeEnum = Sample.GetSampleDataTypeEnum();
            SampleDataType     = Sample.SampleDataType;
            SpeakerSetupEnum   = Sample.GetSpeakerSetupEnum();
            SpeakerSetup       = Sample.SpeakerSetup;

            int bits = SynthWishes.GetBits;
            switch (bits)
            {
                case 8:  Type = typeof(byte);  break;
                case 16: Type = typeof(short); break;
                case 32: Type = typeof(float); break;
                default: throw new Exception($"{new { bits }} not supported.");
            }
        }
    }
}