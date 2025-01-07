using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class ChannelChannelsComboWishesTests
    {
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
                
        private TestEntities CreateTestEntities(int channels, int? channel = null) => new TestEntities(x => x.WithChannels(channels).WithChannel(channel));
    }
}
