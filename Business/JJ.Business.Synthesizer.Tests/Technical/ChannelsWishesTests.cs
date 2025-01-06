using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Helpers.PersistenceHelper;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0611 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class ChannelsWishesTests
    {
        [TestMethod] public void Test_Channels_InTandem()
        {
            Test_Channels_InTandem(1, 2);
            Test_Channels_InTandem(2, 1);
        }
        
        void Test_Channels_InTandem(int init, int value)
        {
            // Check Before Change
            { 
                var x = new TestEntities(channels: init);
                x.Assert_All_Channels_Getters(init);
            }

            // Synth-Bound Changes
            {
                AssertProp(x => AreEqual(x.SynthWishes,  () => x.SynthWishes.Channels(value)));
                AssertProp(x => AreEqual(x.SynthWishes,        x.SynthWishes.WithChannels(value)));
                AssertProp(x => AreEqual(x.FlowNode,     () => x.FlowNode.Channels(value)));
                AssertProp(x => AreEqual(x.FlowNode,           x.FlowNode.WithChannels(value)));
                AssertProp(x => AreEqual(x.ConfigWishes, () => x.ConfigWishes.Channels(value)));
                AssertProp(x => AreEqual(x.ConfigWishes,       x.ConfigWishes.WithChannels(value)));
                
                AssertProp(x =>
                {
                    if (value == 1) AreEqual(x.SynthWishes, () => x.SynthWishes.WithMono());
                    if (value == 2) AreEqual(x.SynthWishes, () => x.SynthWishes.WithStereo());
                });
                
                AssertProp(x =>
                {
                    if (value == 1) AreEqual(x.FlowNode, () => x.FlowNode.WithMono());
                    if (value == 2) AreEqual(x.FlowNode, () => x.FlowNode.WithStereo());
                });
                
                AssertProp(x =>
                {
                    if (value == 1) AreEqual(x.ConfigWishes, () => x.ConfigWishes.WithMono());
                    if (value == 2) AreEqual(x.ConfigWishes, () => x.ConfigWishes.WithStereo());
                });
                
                void AssertProp(Action<TestEntities> setter)
                {
                    var x = new TestEntities(channels: init);
                    x.Assert_All_Channels_Getters(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Channels_Getters(value);
                    x.Assert_TapeBound_Channels_Getters(init);
                    x.Assert_BuffBound_Channels_Getters(init);
                    x.Assert_Independent_Channels_Getters(init);
                    x.Assert_Immutable_Channels_Getters(init);
                    
                    x.Record();
                    
                    x.Assert_All_Channels_Getters(value);
                }
            }

            // Tape-Bound Changes
            {
                AssertProp(x => AreEqual(x.Tape,        () => x.Tape.Channels(value)));
                AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig.Channels(value)));
                AssertProp(x =>                               x.TapeConfig.Channels = value);
                AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.Channels(value)));
                AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction.Channels(value)));
                
                AssertProp(x =>
                {
                    if (value == 1) AreEqual(x.Tape, () => x.Tape.Mono());
                    if (value == 2) AreEqual(x.Tape, () => x.Tape.Stereo());
                });
                
                AssertProp(x =>
                {
                    if (value == 1) AreEqual(x.TapeConfig, () => x.TapeConfig.Mono());
                    if (value == 2) AreEqual(x.TapeConfig, () => x.TapeConfig.Stereo());
                });
                
                AssertProp(x =>
                {
                    if (value == 1) AreEqual(x.TapeActions, () => x.TapeActions.Mono());
                    if (value == 2) AreEqual(x.TapeActions, () => x.TapeActions.Stereo());
                });
                
                AssertProp(x =>
                {
                    if (value == 1) AreEqual(x.TapeAction, () => x.TapeAction.Mono());
                    if (value == 2) AreEqual(x.TapeAction, () => x.TapeAction.Stereo());
                });
                
                void AssertProp(Action<TestEntities> setter)
                {
                    var x = new TestEntities(channels: init);
                    x.Assert_All_Channels_Getters(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Channels_Getters(init);
                    x.Assert_TapeBound_Channels_Getters(value);
                    x.Assert_BuffBound_Channels_Getters(init);
                    x.Assert_Independent_Channels_Getters(init);
                    x.Assert_Immutable_Channels_Getters(init);
                    
                    x.Record();
                    
                    x.Assert_All_Channels_Getters(init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
                }
            }

            // Buff-Bound Changes
            {
                AssertProp(x => AreEqual(x.Buff,            () => x.Buff.Channels(value, x.Context)));
                AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Channels(value, x.Context)));
                
                AssertProp(x =>
                {
                    if (value == 1) AreEqual(x.Buff, () => x.Buff.Mono(x.Context));
                    if (value == 2) AreEqual(x.Buff, () => x.Buff.Stereo(x.Context));
                });
                
                AssertProp(x =>
                {
                    if (value == 1) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Mono(x.Context));
                    if (value == 2) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Stereo(x.Context));
                });

                void AssertProp(Action<TestEntities> setter)
                {    
                    var x = new TestEntities(channels: init);
                    x.Assert_All_Channels_Getters(init);
                    
                    setter(x);

                    x.Assert_SynthBound_Channels_Getters(init);
                    x.Assert_TapeBound_Channels_Getters(init);
                    x.Assert_BuffBound_Channels_Getters(value);
                    x.Assert_Independent_Channels_Getters(init);
                    x.Assert_Immutable_Channels_Getters(init);
                    
                    x.Record();
                    x.Assert_All_Channels_Getters(init);
                }
            }
        }
        
        // Channels for Independently Changeable
        
        [TestMethod] public void Test_Channels_IndependentAfterTaping()
        {
            Test_Channels_IndependentAfterTaping(init: 1, value: 2);
            Test_Channels_IndependentAfterTaping(init: 2, value: 1);
        }
        
        void Test_Channels_IndependentAfterTaping(int init, int value)
        {
            // Independent after Taping
            var x = new TestEntities(channels: init);

            // Sample
            {
                AssertProp(() => AreEqual(x.Sample, () => x.Sample.Channels(value, x.Context)));
                
                AssertProp(() =>
                {
                    if (value == 1) AreEqual(x.Sample, () => x.Sample.Mono(x.Context));
                    if (value == 2) AreEqual(x.Sample, () => x.Sample.Stereo(x.Context));
                });
                
                void AssertProp(Action setter)
                {
                    x.Initialize(channels: init);
                    x.Assert_All_Channels_Getters(init);
                    
                    setter();
                    
                    x.Sample.Assert_Channels_Getters(value);
                    
                    x.AudioInfoWish.Assert_Channels_Getters(init);
                    x.AudioFileInfo.Assert_Channels_Getters(init);
                    x.Assert_Immutable_Channels_Getters(init);
                    x.Assert_Bound_Channels_Getters(init);

                    x.Record();
                    x.Assert_All_Channels_Getters(init);
                }
            }
            
            // AudioInfoWish
            {
                AssertProp(() => AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.Channels(value)));
                AssertProp(() =>                                 x.AudioInfoWish.Channels = value);
                
                AssertProp(() =>
                {
                    if (value == 1) AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.Mono());
                    if (value == 2) AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.Stereo());
                });
                
                void AssertProp(Action setter)
                {
                    x.Initialize(channels: init);
                    x.Assert_All_Channels_Getters(init);
                    
                    setter();
                    
                    x.AudioInfoWish.Assert_Channels_Getters(value);
                    
                    x.AudioFileInfo.Assert_Channels_Getters(init);
                    x.Sample.Assert_Channels_Getters(init);
                    x.Assert_Immutable_Channels_Getters(init);
                    x.Assert_Bound_Channels_Getters(init);

                    x.Record();
                    x.Assert_All_Channels_Getters(init);
                }
            }
                        
            // AudioFileInfo
            {
                AssertProp(() => AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.Channels(value)));
                
                AssertProp(() =>
                {
                    if (value == 1) AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.Mono());
                    if (value == 2) AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.Stereo());
                });
                
                void AssertProp(Action setter)
                {
                    x.Initialize(channels: init);
                    x.Assert_All_Channels_Getters(init);
                    
                    setter();
                    
                    x.AudioFileInfo.Assert_Channels_Getters(value);
                    
                    x.AudioInfoWish.Assert_Channels_Getters(init);
                    x.Sample.Assert_Channels_Getters(init);
                    x.Assert_Bound_Channels_Getters(init);
                    x.Assert_Immutable_Channels_Getters(init);

                    x.Record();
                    x.Assert_All_Channels_Getters(init);
                }
            }
        }
        
        // Channels for Immutables

        [TestMethod] public void Test_Channels_Immutable()
        {
            Test_Channels_Immutable(init: 1, value: 2);
            Test_Channels_Immutable(init: 2, value: 1);
        }
        
        void Test_Channels_Immutable(int init, int value)
        {
            var x = new TestEntities(channels: init);

            var wavHeaders = new List<WavHeaderStruct>();
            {
                AssertProp(() => x.WavHeader.Channels(value));
                
                AssertProp(() => 
                {
                    if (value == 1) return x.WavHeader.Mono();
                    if (value == 2) return x.WavHeader.Stereo();
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    x.WavHeader.Assert_Channels_Getters(init);
                    
                    var wavHeader2 = setter();
                    
                    x.WavHeader.Assert_Channels_Getters(init);
                    wavHeader2.Assert_Channels_Getters(value);
                    
                    wavHeaders.Add(wavHeader2);
                }
            }
            
            var speakerSetupEnums = new List<SpeakerSetupEnum>();
            {
                AssertProp(() => x.SpeakerSetupEnum.Channels(value));
                
                AssertProp(() => 
                {
                    if (value == 1) return x.SpeakerSetupEnum.Mono();
                    if (value == 2) return x.SpeakerSetupEnum.Stereo();
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<SpeakerSetupEnum> setter)
                {
                    x.SpeakerSetupEnum.Assert_Channels_Getters(init);
                    
                    var speakerSetupEnum2 = setter();
                    
                    x.SpeakerSetupEnum.Assert_Channels_Getters(init);
                    speakerSetupEnum2.Assert_Channels_Getters(value);
                    
                    speakerSetupEnums.Add(speakerSetupEnum2);
                }
            }
                        
            var speakerSetups = new List<SpeakerSetup>();
            {
                AssertProp(() => x.SpeakerSetup.Channels(value, x.Context));
                
                AssertProp(() => 
                {
                    if (value == 1) return x.SpeakerSetup.Mono(x.Context);
                    if (value == 2) return x.SpeakerSetup.Stereo(x.Context);
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<SpeakerSetup> setter)
                {
                    x.SpeakerSetup.Assert_Channels_Getters(init);

                    var speakerSetup2 = setter();
                    
                    x.SpeakerSetup.Assert_Channels_Getters(init);
                    speakerSetup2.Assert_Channels_Getters(value);
                    
                    speakerSetups.Add(speakerSetup2);
                }
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            x.Assert_All_Channels_Getters(init);
            
            // Except for our variables
            wavHeaders       .ForEach(w => w.Assert_Channels_Getters(value));
            speakerSetupEnums.ForEach(e => e.Assert_Channels_Getters(value));
            speakerSetups    .ForEach(s => s.Assert_Channels_Getters(value));
        }

        // Channels in ConfigSection
        
        [TestMethod] public void Test_Channels_ConfigSection()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = TestEntities.GetConfigSectionAccessor();
            
            AreEqual(DefaultChannels,      () => configSection.Channels);
            AreEqual(DefaultChannels,      () => configSection.Channels());
            AreEqual(DefaultChannels == 1, () => configSection.IsMono());
            AreEqual(DefaultChannels == 2, () => configSection.IsStereo());
        }
                
        // Channels Conversion-Style
        
        [TestMethod] public void Test_Channels_ConversionStyle()
        {
            foreach (int channels in new[] { 1, 2 })
            {
                var x = new TestEntities(channels: channels);
                
                AreEqual(x.SpeakerSetupEnum, () => channels.ChannelsToEnum());
                AreEqual(x.SpeakerSetup,     () => channels.ChannelsToEntity(x.Context));
            
                AreEqual(channels, () => x.SpeakerSetupEnum.EnumToChannels());
                AreEqual(channels, () => x.SpeakerSetup    .EntityToChannels());
            }
        }
        
        // Channels/Channel Combos
        [TestMethod] public void Test_Channels_Channel_Combo_Getters()
        {
            Test_Channels_Channel_Combo_Getter(1, 0); // Mono/Center
            Test_Channels_Channel_Combo_Getter(2, 0); // Stereo/Left
            Test_Channels_Channel_Combo_Getter(2, 1); // Stereo/Right
        }

        void Test_Channels_Channel_Combo_Getter(int channels, int channel)
        {
            var x = CreateTestEntities(channels, channel);
            
            x.ChannelEnum  .Assert_Channels_Getters(channels);
            x.ChannelEntity.Assert_Channels_Getters(channels);
        }
        
        [TestMethod] public void Test_Channels_Channel_Combo_Changes()
        {
            int channelsMono = 1;
            int channelsStereo = 2;

            AreEqual(SpeakerSetupEnum.Undefined, () => 0.ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Mono,      () => 1.ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Stereo,    () => 2.ChannelsToEnum());
            ThrowsException(                     () => 3.ChannelsToEnum());

            AreEqual(0,  () => SpeakerSetupEnum.Undefined. EnumToChannels());
            AreEqual(1,  () => SpeakerSetupEnum.Mono      .EnumToChannels());
            AreEqual(2,  () => SpeakerSetupEnum.Stereo    .EnumToChannels());
            ThrowsException(() => ((SpeakerSetupEnum)(-1)).EnumToChannels());
            
            // TODO: Converting 2 things to 1 with a this argument and a normal parameter is very confusing.
            // Use tuples? Move these conversion to separate code file?
            AreEqual(ChannelEnum.Undefined, 1.ChannelsToChannelEnum(channel: null));
            AreEqual(ChannelEnum.Single,    1.ChannelsToChannelEnum(channel: 0));
            AreEqual(ChannelEnum.Undefined, 2.ChannelsToChannelEnum(channel: null));
            AreEqual(ChannelEnum.Left,      2.ChannelsToChannelEnum(channel: 0));
            AreEqual(ChannelEnum.Right,     2.ChannelsToChannelEnum(channel: 1));
            ThrowsException(() =>           1.ChannelsToChannelEnum(channel: 1));
            
            AreEqual(0, () => ChannelEnum.Undefined  .ChannelEnumToChannels());
            AreEqual(1, () => ChannelEnum.Single     .ChannelEnumToChannels());
            AreEqual(2, () => ChannelEnum.Right      .ChannelEnumToChannels());
            AreEqual(2, () => ChannelEnum.Left       .ChannelEnumToChannels());
            ThrowsException(() => ((ChannelEnum)(-1)).ChannelEnumToChannels());
            
            var monoEntities   = CreateTestEntities(channels: 1);
            var stereoEntities = CreateTestEntities(channels: 2);
            
            AreEqual(1, () => monoEntities  .SpeakerSetup.EntityToChannels());
            AreEqual(2, () => stereoEntities.SpeakerSetup.EntityToChannels());
            
            AreSame(monoEntities  .SpeakerSetup, () =>   1 .ChannelsToEntity(monoEntities.Context));
            AreSame(stereoEntities.SpeakerSetup, () =>   2 .ChannelsToEntity(stereoEntities.Context));
            IsNull(                              () =>   0 .ChannelsToEntity(monoEntities.Context));
            ThrowsException(                     () => (-1).ChannelsToEntity(monoEntities.Context));

            var centerEntities = CreateTestEntities(channels: 1, channel: 0);
            var leftEntities   = CreateTestEntities(channels: 2, channel: 0);
            var rightEntities  = CreateTestEntities(channels: 2, channel: 1);
            
            AreSame(centerEntities.ChannelEntity, 1.ChannelsToChannelEntity(0, centerEntities.Context));
            AreSame(leftEntities  .ChannelEntity, 2.ChannelsToChannelEntity(0, leftEntities  .Context));
            AreSame(rightEntities .ChannelEntity, 2.ChannelsToChannelEntity(1, rightEntities .Context));
            ThrowsException(() => (-1).ChannelsToChannelEntity(0, centerEntities.Context));

            AreEqual(1, () => centerEntities.ChannelEntity.ChannelEntityToChannels());
            AreEqual(2, () => leftEntities  .ChannelEntity.ChannelEntityToChannels());
            AreEqual(2, () => rightEntities .ChannelEntity.ChannelEntityToChannels());
            
            //AreEqual(ChannelEnum.Left,      () => ChannelEnum.Single.Channels(channelsStereo));
            //AreEqual(ChannelEnum.Single,    () => ChannelEnum.Left.Channels(channelsMono));
            //AreEqual(ChannelEnum.Undefined, () => ChannelEnum.Right.Channels(channelsMono));
        }
        
        // Helper
        
        private TestEntities CreateTestEntities(int channels, int? channel = null) => new TestEntities(x => x.WithChannels(channels).WithChannel(channel));
        
        // Old
 
        /// <inheritdoc cref="docs._testattributewishesold"/>
        [TestMethod]
        public void ChannelCountToSpeakerSetup_Test()
        {
            AreEqual(SpeakerSetupEnum.Mono,   () => 1.ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Stereo, () => 2.ChannelsToEnum());
        }
    }
    
    // Helpers
    
    internal static class ChannelsWishesTestExtensions
    {
        public static void Assert_All_Channels_Getters(this TestEntities x, int channels)
        {
            x.Assert_SynthBound_Channels_Getters(channels);
            x.Assert_TapeBound_Channels_Getters(channels);
            x.Assert_BuffBound_Channels_Getters(channels);
            x.Assert_Independent_Channels_Getters(channels);
            x.Assert_Immutable_Channels_Getters(channels);
        }

        public static void Assert_Bound_Channels_Getters(this TestEntities x, int channels)
        {
            x.Assert_SynthBound_Channels_Getters(channels);
            x.Assert_TapeBound_Channels_Getters(channels);
            x.Assert_BuffBound_Channels_Getters(channels);
        }

        public static void Assert_SynthBound_Channels_Getters(this TestEntities x, int channels)
        {
            AreEqual(channels, () => x.SynthWishes.Channels());
            AreEqual(channels, () => x.SynthWishes.GetChannels);
            AreEqual(channels, () => x.FlowNode.Channels());
            AreEqual(channels, () => x.FlowNode.GetChannels);
            AreEqual(channels, () => x.ConfigWishes.Channels());
            AreEqual(channels, () => x.ConfigWishes.GetChannels);
            
            AreEqual(channels == 1, () => x.SynthWishes.IsMono());
            AreEqual(channels == 1, () => x.SynthWishes.IsMono);
            AreEqual(channels == 1, () => x.FlowNode.IsMono());
            AreEqual(channels == 1, () => x.FlowNode.IsMono);
            AreEqual(channels == 1, () => x.ConfigWishes.IsMono());
            AreEqual(channels == 1, () => x.ConfigWishes.IsMono);
            
            AreEqual(channels == 2, () => x.SynthWishes.IsStereo());
            AreEqual(channels == 2, () => x.SynthWishes.IsStereo);
            AreEqual(channels == 2, () => x.FlowNode.IsStereo());
            AreEqual(channels == 2, () => x.FlowNode.IsStereo);
            AreEqual(channels == 2, () => x.ConfigWishes.IsStereo());
            AreEqual(channels == 2, () => x.ConfigWishes.IsStereo);
        }
        
        public static void Assert_TapeBound_Channels_Getters(this TestEntities x, int channels)
        {
            AreEqual(channels, () => x.Tape.Channels());
            AreEqual(channels, () => x.TapeConfig.Channels());
            AreEqual(channels, () => x.TapeConfig.Channels);
            AreEqual(channels, () => x.TapeActions.Channels());
            AreEqual(channels, () => x.TapeAction.Channels());
            
            AreEqual(channels == 1, () => x.Tape.IsMono());
            AreEqual(channels == 1, () => x.TapeConfig.IsMono());
            AreEqual(channels == 1, () => x.TapeActions.IsMono());
            AreEqual(channels == 1, () => x.TapeAction.IsMono());
        
            AreEqual(channels == 2, () => x.Tape.IsStereo());
            AreEqual(channels == 2, () => x.TapeConfig.IsStereo());
            AreEqual(channels == 2, () => x.TapeActions.IsStereo());
            AreEqual(channels == 2, () => x.TapeAction.IsStereo());
        }
        
        public static void Assert_BuffBound_Channels_Getters(this TestEntities x, int channels)
        {
            AreEqual(channels, () => x.Buff.Channels());
            AreEqual(channels, () => x.AudioFileOutput.Channels());
            
            AreEqual(channels == 1, () => x.Buff.IsMono());
            AreEqual(channels == 1, () => x.AudioFileOutput.IsMono());
            
            AreEqual(channels == 2, () => x.Buff.IsStereo());
            AreEqual(channels == 2, () => x.AudioFileOutput.IsStereo());
        }

        public static void Assert_Independent_Channels_Getters(this TestEntities x, int channels)
        {
            // Independent after Taping
            x.Sample.Assert_Channels_Getters(channels);
            x.AudioInfoWish.Assert_Channels_Getters(channels);
            x.AudioFileInfo.Assert_Channels_Getters(channels);
        }

        public static void Assert_Immutable_Channels_Getters(this TestEntities x, int channels)
        {
            x.WavHeader.Assert_Channels_Getters(channels);
            x.SpeakerSetupEnum.Assert_Channels_Getters(channels);
            x.SpeakerSetup.Assert_Channels_Getters(channels);
        }

        public static void Assert_Channels_Getters(this AudioFileInfo audioFileInfo, int channels)
        {
            AreEqual(channels,      () => audioFileInfo.Channels());
            AreEqual(channels == 1, () => audioFileInfo.IsMono());
            AreEqual(channels == 2, () => audioFileInfo.IsStereo());
        }
        
        public static void Assert_Channels_Getters(this Sample sample, int channels)
        {
            AreEqual(channels,      () => sample.Channels());
            AreEqual(channels == 1, () => sample.IsMono());
            AreEqual(channels == 2, () => sample.IsStereo());
        }
        
        public static void Assert_Channels_Getters(this AudioInfoWish audioInfoWish, int channels)
        {
            AreEqual(channels,      () => audioInfoWish.Channels());
            AreEqual(channels,      () => audioInfoWish.Channels);
            AreEqual(channels == 1, () => audioInfoWish.IsMono());
            AreEqual(channels == 2, () => audioInfoWish.IsStereo());
        }

        public static void Assert_Channels_Getters(this WavHeaderStruct wavHeader, int channels)
        {
            AreEqual(channels,      () => wavHeader.Channels());
            AreEqual(channels,      () => wavHeader.ChannelCount);
            AreEqual(channels == 1, () => wavHeader.IsMono());
            AreEqual(channels == 2, () => wavHeader.IsStereo());
        }
        
        public static void Assert_Channels_Getters(this SpeakerSetupEnum speakerSetupEnum, int channels)
        {
            AreEqual(channels,      () => speakerSetupEnum.Channels());
            AreEqual(channels == 1, () => speakerSetupEnum.IsMono());
            AreEqual(channels == 2, () => speakerSetupEnum.IsStereo());
        }
                
        public static void Assert_Channels_Getters(this SpeakerSetup speakerSetup, int channels)
        {
            if (speakerSetup == null) throw new NullException(() => speakerSetup);
            AreEqual(channels,      () => speakerSetup.Channels());
            AreEqual(channels == 1, () => speakerSetup.IsMono());
            AreEqual(channels == 2, () => speakerSetup.IsStereo());
        }

        public static void Assert_Channels_Getters(this ChannelEnum channelEnum, int? channels)
        {
            AreEqual(channels,            channelEnum.Channels());
            AreEqual(channels == 1, () => channelEnum.IsMono());
            AreEqual(channels == 2, () => channelEnum.IsStereo());
        }
        
        public static void Assert_Channels_Getters(this Channel channel, int? channels)
        {
            if (channel == null) throw new NullException(() => channel);
            AreEqual(channels,            channel.Channels());
            AreEqual(channels == 1, () => channel.IsMono());
            AreEqual(channels == 2, () => channel.IsStereo());
        }
    } 
}