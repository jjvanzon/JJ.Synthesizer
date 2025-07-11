﻿using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.Legacy.AssertHelper;
using static JJ.Framework.Testing.Core.AssertCore;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class ChannelChannelsComboNightmareTests
    {
        // Channels/Channel Combos
        [TestMethod] public void Test_Channels_Channel_Combo_Getters()
        {
            Test_Channels_Channel_Combo_Getter(MonoChannels,   CenterChannel);
            Test_Channels_Channel_Combo_Getter(StereoChannels, LeftChannel);
            Test_Channels_Channel_Combo_Getter(StereoChannels, RightChannel);
        }

        void Test_Channels_Channel_Combo_Getter(int channels, int channel)
        {
            var x = CreateTestEntities(channels, channel);
            
            Assert_Channels_Getters(x.Immutable.ChannelEnum, channels);
            Assert_Channels_Getters(x.Immutable.ChannelEntity, channels);
        }
        
        [TestMethod] public void Test_Channels_Channel_Combo_Changes()
        {
            AreEqual(SpeakerSetupEnum.Undefined, () => NoChannels .ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Mono,      () => MonoChannels  .ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Stereo,    () => StereoChannels.ChannelsToEnum());
            ThrowsException(                     () => (-1)          .ChannelsToEnum());

            AreEqual(NoChannels,  () => SpeakerSetupEnum.Undefined. EnumToChannels());
            AreEqual(MonoChannels,   () => SpeakerSetupEnum.Mono      .EnumToChannels());
            AreEqual(StereoChannels, () => SpeakerSetupEnum.Stereo    .EnumToChannels());
            ThrowsException(() => ((SpeakerSetupEnum)(-1)).EnumToChannels());
            
            // TODO: Converting 2 things to 1 with a this argument and a normal parameter is very confusing.
            // Use tuples? Move these conversion to separate code file?
            AreEqual(ChannelEnum.Single,    1 .ChannelsToChannelEnum(EmptyChannel)); 
            AreEqual(ChannelEnum.Single,    1 .ChannelsToChannelEnum(CenterChannel));
            AreEqual(ChannelEnum.Single,    1 .ChannelsToChannelEnum(RightChannel)); // Tolerate inconsistent state for smooth switch to mono.
            AreEqual(ChannelEnum.Undefined, 2 .ChannelsToChannelEnum(EmptyChannel));
            AreEqual(ChannelEnum.Left,      2 .ChannelsToChannelEnum(LeftChannel));
            AreEqual(ChannelEnum.Right,     2 .ChannelsToChannelEnum(RightChannel));
            ThrowsException(() =>         (-1).ChannelsToChannelEnum(CenterChannel));
            
            AreEqual(StereoChannels, () => ChannelEnum.Undefined  .ChannelEnumToChannels());
            AreEqual(MonoChannels,   () => ChannelEnum.Single     .ChannelEnumToChannels());
            AreEqual(StereoChannels, () => ChannelEnum.Right      .ChannelEnumToChannels());
            AreEqual(StereoChannels, () => ChannelEnum.Left       .ChannelEnumToChannels());
            ThrowsException(() => ((ChannelEnum)(-1)).ChannelEnumToChannels());

            IContext context = CreateContext();
            var monoEntities   = CreateTestEntities(MonoChannels,   context);
            var stereoEntities = CreateTestEntities(StereoChannels, context);
            
            AreEqual(MonoChannels,   () => monoEntities  .Immutable.SpeakerSetup.EntityToChannels());
            AreEqual(StereoChannels, () => stereoEntities.Immutable.SpeakerSetup.EntityToChannels());
            
            AreSame(monoEntities  .Immutable.SpeakerSetup, () => MonoChannels  .ChannelsToEntity(context));
            AreSame(stereoEntities.Immutable.SpeakerSetup, () => StereoChannels.ChannelsToEntity(context));
            IsNull(                                        () => NoChannels .ChannelsToEntity(context));
            ThrowsException(                               () => (-1)          .ChannelsToEntity(context));
            
            var centerEntities = CreateTestEntities(MonoChannels,   CenterChannel, context);
            var leftEntities   = CreateTestEntities(StereoChannels, LeftChannel,   context);
            var rightEntities  = CreateTestEntities(StereoChannels, RightChannel,  context);
            
            AreEqual(centerEntities.Immutable.ChannelEntity, MonoChannels.ChannelsToChannelEntity(CenterChannel, context));
            AreEqual(  leftEntities.Immutable.ChannelEntity, StereoChannels.ChannelsToChannelEntity(LeftChannel,   context));
            AreEqual( rightEntities.Immutable.ChannelEntity, StereoChannels.ChannelsToChannelEntity(RightChannel,  context));
            ThrowsException(() => (-1).ChannelsToChannelEntity(CenterChannel, context));

            AreEqual(MonoChannels,   () => centerEntities.Immutable.ChannelEntity.ChannelEntityToChannels());
            AreEqual(StereoChannels, () => leftEntities.Immutable.ChannelEntity.ChannelEntityToChannels());
            AreEqual(StereoChannels, () => rightEntities.Immutable.ChannelEntity.ChannelEntityToChannels());
            
            //AreEqual(ChannelEnum.Left,      () => ChannelEnum.Single.Channels(channelsStereo));
            //AreEqual(ChannelEnum.Single,    () => ChannelEnum.Left.Channels(channelsMono));
            //AreEqual(ChannelEnum.Undefined, () => ChannelEnum.Right.Channels(channelsMono));
        }
                
        private TestEntities CreateTestEntities(int channels, [CallerMemberName] string name = null) 
            => new TestEntities(x => x.NoLog().WithChannels(channels), name);
        
        private TestEntities CreateTestEntities(int channels, IContext context, [CallerMemberName] string name = null) 
            => new TestEntities(x => x.NoLog().WithChannels(channels), context, name);
        
        private TestEntities CreateTestEntities(int channels, int? channel, [CallerMemberName] string name = null) 
            => new TestEntities(x => x.NoLog().WithChannels(channels).WithChannel(channel), name);
        
        private TestEntities CreateTestEntities(int channels, int? channel, IContext context, [CallerMemberName] string name = null) 
            => new TestEntities(x => x.NoLog().WithChannels(channels).WithChannel(channel).SamplingRate(HighPerfHz), context, name);

        private void Assert_Channels_Getters(ChannelEnum channelEnum, int? channels)
        {
            AreEqual(channels,            channelEnum.Channels());
            AreEqual(channels == 1, () => channelEnum.IsMono());
            AreEqual(channels == 2, () => channelEnum.IsStereo());
        }
        
        private void Assert_Channels_Getters(Channel channel, int? channels)
        {
            if (channel == null) throw new NullException(() => channel);
            AreEqual(channels,            channel.Channels());
            AreEqual(channels == 1, () => channel.IsMono());
            AreEqual(channels == 2, () => channel.IsStereo());
        }
    }
}
