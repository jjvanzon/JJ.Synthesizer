using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;

namespace JJ.Business.Synthesizer.Tests.Technical.AttributeWishes
{
    [TestClass]
    [TestCategory("Technical")]
    public class ChannelChannelsComboWishesTests
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
            AreEqual(SpeakerSetupEnum.Undefined, () => ChannelsEmpty    .ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Mono,      () => MonoChannels  .ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Stereo,    () => StereoChannels.ChannelsToEnum());
            ThrowsException(                     () => (-1)          .ChannelsToEnum());

            AreEqual(ChannelsEmpty,     () => SpeakerSetupEnum.Undefined. EnumToChannels());
            AreEqual(MonoChannels,   () => SpeakerSetupEnum.Mono      .EnumToChannels());
            AreEqual(StereoChannels, () => SpeakerSetupEnum.Stereo    .EnumToChannels());
            ThrowsException(() => ((SpeakerSetupEnum)(-1)).EnumToChannels());
            
            // TODO: Converting 2 things to 1 with a this argument and a normal parameter is very confusing.
            // Use tuples? Move these conversion to separate code file?
            AreEqual(ChannelEnum.Single,    1 .ChannelsToChannelEnum(ChannelEmpty)); 
            AreEqual(ChannelEnum.Single,    1 .ChannelsToChannelEnum(CenterChannel));
            AreEqual(ChannelEnum.Single,    1 .ChannelsToChannelEnum(RightChannel)); // Tolerate inconsistent state for smooth switch to mono.
            AreEqual(ChannelEnum.Undefined, 2 .ChannelsToChannelEnum(ChannelEmpty));
            AreEqual(ChannelEnum.Left,      2 .ChannelsToChannelEnum(LeftChannel));
            AreEqual(ChannelEnum.Right,     2 .ChannelsToChannelEnum(RightChannel));
            ThrowsException(() =>         (-1).ChannelsToChannelEnum(CenterChannel));
            
            AreEqual(ChannelsEmpty,     () => ChannelEnum.Undefined  .ChannelEnumToChannels());
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
            IsNull(                                        () => ChannelsEmpty    .ChannelsToEntity(context));
            ThrowsException(                               () => (-1)          .ChannelsToEntity(context));
            
            var centerEntities = CreateTestEntities(MonoChannels,   CenterChannel, context);
            var leftEntities   = CreateTestEntities(StereoChannels, LeftChannel,   context);
            var rightEntities  = CreateTestEntities(StereoChannels, RightChannel,  context);
            
            AreSame(centerEntities.Immutable.ChannelEntity,   MonoChannels.ChannelsToChannelEntity(CenterChannel, context));
            AreSame(  leftEntities.Immutable.ChannelEntity, StereoChannels.ChannelsToChannelEntity(LeftChannel,   context));
            AreSame( rightEntities.Immutable.ChannelEntity, StereoChannels.ChannelsToChannelEntity(RightChannel,  context));
            ThrowsException(() => (-1).ChannelsToChannelEntity(CenterChannel, context));

            AreEqual(MonoChannels, () => centerEntities.Immutable.ChannelEntity.ChannelEntityToChannels());
            AreEqual(StereoChannels, () => leftEntities.Immutable.ChannelEntity.ChannelEntityToChannels());
            AreEqual(StereoChannels, () => rightEntities.Immutable.ChannelEntity.ChannelEntityToChannels());
            
            //AreEqual(ChannelEnum.Left,      () => ChannelEnum.Single.Channels(channelsStereo));
            //AreEqual(ChannelEnum.Single,    () => ChannelEnum.Left.Channels(channelsMono));
            //AreEqual(ChannelEnum.Undefined, () => ChannelEnum.Right.Channels(channelsMono));
        }
                
        private TestEntities CreateTestEntities(int channels) 
            => new TestEntities(x => x.WithChannels(channels));
        
        private TestEntities CreateTestEntities(int channels, IContext context) 
            => new TestEntities(x => x.WithChannels(channels), context);
        
        private TestEntities CreateTestEntities(int channels, int? channel) 
            => new TestEntities(x => x.WithChannels(channels).WithChannel(channel));
        
        private TestEntities CreateTestEntities(int channels, int? channel, IContext context) 
            => new TestEntities(x => x.WithChannels(channels).WithChannel(channel), context);

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
