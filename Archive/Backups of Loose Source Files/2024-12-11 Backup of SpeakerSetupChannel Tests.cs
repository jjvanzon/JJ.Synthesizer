// SpeakerSetupChannel Tests: SpeakerSetupChannel is Deprecated.

// AudioFileOutputChannel Extensions for Missing Model Properties
{
    // Arrange
    AudioFileOutput audioFileOutputMono = WithChannels(1).Sine().Save().Data;
    IsNotNull(() => audioFileOutputMono);
    IsNotNull(() => audioFileOutputMono.AudioFileOutputChannels);
    AreEqual(1, () => audioFileOutputMono.AudioFileOutputChannels.Count);
    IsNotNull(() => audioFileOutputMono.AudioFileOutputChannels[0]);
    AudioFileOutputChannel audioFileOutputChannel = audioFileOutputMono.AudioFileOutputChannels[0];

    // Act
    SpeakerSetupChannel speakerSetupChannel = audioFileOutputChannel.GetSpeakerSetupChannel();

    // Assert
    IsNotNull(() => speakerSetupChannel);
    AreEqual(0, () => speakerSetupChannel.Index);

    IsNotNull(() => speakerSetupChannel.SpeakerSetup);
    AreEqual((int)SpeakerSetupEnum.Mono, () => speakerSetupChannel.SpeakerSetup.ID);
    AreEqual(nameof(SpeakerSetupEnum.Mono), () => speakerSetupChannel.SpeakerSetup.Name);
    IsNotNull(() => speakerSetupChannel.SpeakerSetup.SpeakerSetupChannels);
    AreEqual(1, () => speakerSetupChannel.SpeakerSetup.SpeakerSetupChannels.Count);
    AreEqual(speakerSetupChannel, () => speakerSetupChannel.SpeakerSetup.SpeakerSetupChannels[0]);

    IsNotNull(() => speakerSetupChannel.Channel);
    AreEqual((int)ChannelEnum.Single, () => speakerSetupChannel.Channel.ID);
    AreEqual(0, () => speakerSetupChannel.Channel.Index);
    AreEqual(nameof(ChannelEnum.Single), () => speakerSetupChannel.Channel.Name);
    IsNotNull(() => speakerSetupChannel.Channel.SpeakerSetupChannels);
    AreEqual(1, () => speakerSetupChannel.Channel.SpeakerSetupChannels.Count);
    AreEqual(speakerSetupChannel, () => speakerSetupChannel.Channel.SpeakerSetupChannels[0]);

    AreEqual(speakerSetupChannel, () => speakerSetupChannel.Channel.SpeakerSetupChannels[0]);
}
{
    // Arrange
    AudioFileOutput audioFileOutputStereo = Save(() => Sine(), speakerSetupEnum: Stereo).Data;
    IsNotNull(() => audioFileOutputStereo);
    IsNotNull(() => audioFileOutputStereo.AudioFileOutputChannels);
    AreEqual(2, () => audioFileOutputStereo.AudioFileOutputChannels.Count);
    IsNotNull(() => audioFileOutputStereo.AudioFileOutputChannels[0]);
    IsNotNull(() => audioFileOutputStereo.AudioFileOutputChannels[1]);

    AudioFileOutputChannel audioFileOutputChannel1 = audioFileOutputStereo.AudioFileOutputChannels[0];
    AudioFileOutputChannel audioFileOutputChannel2 = audioFileOutputStereo.AudioFileOutputChannels[1];

    // Act
    SpeakerSetupChannel speakerSetupChannel1 = audioFileOutputChannel1.GetSpeakerSetupChannel();
    SpeakerSetupChannel speakerSetupChannel2 = audioFileOutputChannel2.GetSpeakerSetupChannel();

    // Assert
    IsNotNull(() => speakerSetupChannel1);
    AreEqual(0, () => speakerSetupChannel1.Index);
    IsNotNull(() => speakerSetupChannel2);
    AreEqual(1, () => speakerSetupChannel2.Index);

    IsNotNull(() => speakerSetupChannel1.SpeakerSetup);
    AreEqual((int)Stereo, () => speakerSetupChannel1.SpeakerSetup.ID);
    AreEqual(nameof(Stereo), () => speakerSetupChannel1.SpeakerSetup.Name);
    IsNotNull(() => speakerSetupChannel1.SpeakerSetup.SpeakerSetupChannels);
    AreEqual(2, () => speakerSetupChannel1.SpeakerSetup.SpeakerSetupChannels.Count);
    AreEqual(speakerSetupChannel1, () => speakerSetupChannel1.SpeakerSetup.SpeakerSetupChannels[0]);
    AreEqual(speakerSetupChannel2, () => speakerSetupChannel1.SpeakerSetup.SpeakerSetupChannels[1]);

    AreEqual(speakerSetupChannel1.SpeakerSetup, () => speakerSetupChannel2.SpeakerSetup);

    IsNotNull(() => speakerSetupChannel1.Channel);
    AreEqual((int)Left, () => speakerSetupChannel1.Channel.ID);
    AreEqual(0, () => speakerSetupChannel1.Channel.Index);
    AreEqual(nameof(Left), () => speakerSetupChannel1.Channel.Name);
    IsNotNull(() => speakerSetupChannel1.Channel.SpeakerSetupChannels);
    AreEqual(2, () => speakerSetupChannel1.Channel.SpeakerSetupChannels.Count);
    AreEqual(speakerSetupChannel1, () => speakerSetupChannel1.Channel.SpeakerSetupChannels[0]);
    AreEqual(speakerSetupChannel2, () => speakerSetupChannel1.Channel.SpeakerSetupChannels[1]);

    IsNotNull(() => speakerSetupChannel2.Channel);
    AreEqual((int)Right, () => speakerSetupChannel2.Channel.ID);
    AreEqual(1, () => speakerSetupChannel2.Channel.Index);
    AreEqual(nameof(Right), () => speakerSetupChannel2.Channel.Name);
    IsNotNull(() => speakerSetupChannel2.Channel.SpeakerSetupChannels);
    AreEqual(2, () => speakerSetupChannel2.Channel.SpeakerSetupChannels.Count);
    AreEqual(speakerSetupChannel1, () => speakerSetupChannel2.Channel.SpeakerSetupChannels[0]);
    AreEqual(speakerSetupChannel2, () => speakerSetupChannel2.Channel.SpeakerSetupChannels[1]);
}