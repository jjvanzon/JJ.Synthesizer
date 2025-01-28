ChannelsWishesTests Code Scribbles

        void Test_Channels_Channel_Combo_Change(int from, int to, int channel)
        {
            var x = CreateTestEntities(from, channel);

            // Attempt 2
            var channelEnums = new List<ChannelEnum>();
            {
                AssertProp(() => x.ChannelEnum.Channels(to));
                
                AssertProp(() =>
                {
                    if (to == 1) return x.ChannelEnum.Mono();
                    if (to == 2) return x.ChannelEnum.Stereo();
                    return default; // ncrunch: no coverage
                });

                void AssertProp(Func<ChannelEnum> setter)
                {
                    x = CreateTestEntities(from, channel);
                    
                    x.ChannelEnum.Assert_Channels_Getters(from);
                    
                    var channelEnum2 = setter();

                    x.ChannelEnum.Assert_Channels_Getters(from);
                    channelEnum2.Assert_Channels_Getters(to);
                    
                    channelEnums.Add(channelEnum2);
                }
            }

            // Attempt 1
            {
                AssertProp(() => x.ChannelEnum.Channels(to));
                AssertProp(() => x.ChannelEntity    .Channels(to, x.Context));
                AssertProp(() =>
                {
                    if (to == 1) x.ChannelEnum.Mono();
                    if (to == 2) x.ChannelEnum.Stereo();
                });
                AssertProp(() =>
                {
                    if (to == 1) x.ChannelEntity.Mono(x.Context);
                    if (to == 2) x.ChannelEntity.Stereo(x.Context);
                });
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(from, channel);
                    
                    x.ChannelEnum.Assert_Channels_Getters(from);
                    x.ChannelEntity    .Assert_Channels_Getters(from);
                    
                    setter();

                    x.ChannelEnum.Assert_Channels_Getters(to);
                    x.ChannelEntity    .Assert_Channels_Getters(to);
                }
            }
        }

// Channels Conversion-Style

[TestMethod] public void Test_Channels_ConversionStyle()
{
    foreach (int channels in new[] { 1, 2 })
    {
        var x = new TestEntities(channels: channels);
        
        AreEqual(x.SpeakerSetup,     () => channels.ChannelsToEntity(x.Context));
        AreEqual(channels, () => x.SpeakerSetup    .EntityToChannels());
    
        AreEqual(x.SpeakerSetupEnum, () => channels.ChannelsToEnum());
        AreEqual(channels, () => x.SpeakerSetupEnum.EnumToChannels());
    }
}
                
// Old

/// <inheritdoc cref="docs._testattributewishesold"/>
[TestMethod]
public void ChannelCountToSpeakerSetup_Test()
{
    AreEqual(SpeakerSetupEnum.Mono,   () => 1.ChannelsToEnum());
    AreEqual(SpeakerSetupEnum.Stereo, () => 2.ChannelsToEnum());
}
