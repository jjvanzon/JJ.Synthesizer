

// -----

// SetEnumWishes.cs:
    
//public static class MissingChannelEnumExtensionWishes
//{
//    public static ChannelEnum GetChannelEnum(this SpeakerSetupChannel entity)
//    {
//        if (entity == null) throw new ArgumentNullException(nameof(entity));
//        if (entity.Channel == null) return ChannelEnum.Undefined;
//        return (ChannelEnum)entity.Channel.ID;
//    }

//    public static void SetChannelEnum(
//        this SpeakerSetupChannel entity, ChannelEnum channelEnum, IChannelRepository channelRepository)
//    {
//        if (channelRepository == null) throw new NullException(() => channelRepository);
//        entity.Channel = channelRepository.Get((int)channelEnum);
//    }

//    public static SpeakerSetupEnum GetSpeakerSetupEnum(this SpeakerSetupChannel entity)
//    {
//        if (entity == null) throw new ArgumentNullException(nameof(entity));
//        if (entity.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;
//        return (SpeakerSetupEnum)entity.SpeakerSetup.ID;
//    }

//    public static SpeakerSetupEnum SetSpeakerSetupEnum(
//        this SpeakerSetupChannel entity, 
//        SpeakerSetupEnum speakerSetupEnum, 
//        ISpeakerSetupRepository speakerSetupRepository)
//    {
//        if (entity == null) throw new ArgumentNullException(nameof(entity));
//        entity.SpeakerSetup = speakerSetupRepository.GetWithRelatedEntities((int)speakerSetupEnum);
//        return (SpeakerSetupEnum)entity.SpeakerSetup.ID;
//    }
//}

//// SpeakerSetupChannel

//public static void SetChannelEnum(
//    this SpeakerSetupChannel entity, ChannelEnum enumValue, IContext context = null)
//{
//    var repository = CreateRepository<IChannelRepository>(context);
//    entity.SetChannelEnum(enumValue, repository);
//}

//public static void SetSpeakerSetupEnum(
//    this SpeakerSetupChannel entity, SpeakerSetupEnum enumValue, IContext context = null)
//{
//    var repository = CreateRepository<ISpeakerSetupRepository>(context);
//    entity.SetSpeakerSetupEnum(enumValue, repository);
//}

// -----
    
// AudioFileWishes.Extensions.cs:

//// Setters with Side-Effects

//// AudioFileOutputChannel.GetSpeakerSetup

//public static SpeakerSetup GetSpeakerSetup(this AudioFileOutputChannel entity)
//{
//    if (entity == null) throw new ArgumentNullException(nameof(entity));
//    if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
//    return entity.AudioFileOutput.SpeakerSetup;
//}

//public static SpeakerSetupEnum GetSpeakerSetupEnum(this AudioFileOutputChannel entity)
//{
//    if (entity == null) throw new ArgumentNullException(nameof(entity));
//    if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
//    return entity.AudioFileOutput.GetSpeakerSetupEnum();
//}

// AudioFileOutput.SetSpeakerSetup_WithSideEffects

///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
//public static void SetSpeakerSetup_WithSideEffects(this AudioFileOutput audioFileOutput, SpeakerSetup speakerSetup, IContext context = null)
//{
//    CreateAudioFileOutputManager(context ?? CreateContext()).SetSpeakerSetup(audioFileOutput, speakerSetup);
//}

///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
//public static void SetSpeakerSetup_WithSideEffects(this AudioFileOutput audioFileOutput, SpeakerSetupEnum speakerSetupEnum, IContext context = null)
//{
//    CreateAudioFileOutputManager(context ?? CreateContext()).SetSpeakerSetup(audioFileOutput, speakerSetupEnum);
//}

///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
//public static void SetSpeakerSetup_WithSideEffects(this AudioFileOutput audioFileOutput, int channelCount, IContext context = null)
//    => SetSpeakerSetup_WithSideEffects(audioFileOutput, channelCount.ToSpeakerSetupEnum(), context);

// Alternative Entry-Point AudioFileOutputChannel

///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
//public static void SetSpeakerSetup_WithSideEffects(this AudioFileOutputChannel entity, SpeakerSetup speakerSetup, IContext context = null)
//{
//    if (entity == null) throw new ArgumentNullException(nameof(entity));
//    if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
//    entity.AudioFileOutput.SetSpeakerSetup_WithSideEffects(speakerSetup, context);
//}

///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
//public static void SetSpeakerSetupEnum_WithSideEffects(this AudioFileOutputChannel entity, SpeakerSetupEnum speakerSetupEnum, IContext context = null)
//{
//    if (entity == null) throw new ArgumentNullException(nameof(entity));
//    if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
//    entity.AudioFileOutput.SetSpeakerSetup_WithSideEffects(speakerSetupEnum, context);
//}

///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
//public static void SetSpeakerSetupEnum_WithSideEffects(this AudioFileOutputChannel entity, int channelCount, IContext context = null)
//    => SetSpeakerSetupEnum_WithSideEffects(entity, channelCount.ToSpeakerSetupEnum(), context);

// -----

// AudioFileOutputChannel.GetSpeakerSetupChannel

//public static SpeakerSetupChannel GetSpeakerSetupChannel(this AudioFileOutputChannel audioFileOutputChannel)
//{
//    IList<SpeakerSetupChannel> speakerSetupChannels =
//        audioFileOutputChannel.GetSpeakerSetup()
//                              .SpeakerSetupChannels;
    
//    SpeakerSetupChannel speakerSetupChannel =
//        speakerSetupChannels.Single(x => x.Index == audioFileOutputChannel.Index);

//    speakerSetupChannel.Channel.SpeakerSetupChannels = speakerSetupChannels;

//    return speakerSetupChannel;
//}

//[TestClass]
//[TestCategory("Technical")]
//public class EnumWishesTests_MissingChannelExtensions : SynthWishes
//{
//    [TestMethod]
//    public void Test_SpeakerSetupChannel_GetChannelEnum()
//    {
//        var repository = PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(Context);

//        {
//            // Arrange
//            int          speakerSetupID = SpeakerSetupEnum.Mono.ToID();
//            SpeakerSetup speakerSetup   = repository.GetWithRelatedEntities(speakerSetupID);
//            IsNotNull(() => speakerSetup);
//            var singleSpeakerSetupChannel = speakerSetup.SpeakerSetupChannels[0];
//            IsNotNull(() => singleSpeakerSetupChannel);

//            // Act
//            ChannelEnum singleChannelEnum = singleSpeakerSetupChannel.GetChannelEnum();

//            // Assert
//            AreEqual(ChannelEnum.Single, () => singleChannelEnum);
//        }

//        {
//            // Arrange
//            int          speakerSetupID = SpeakerSetupEnum.Stereo.ToID();
//            SpeakerSetup speakerSetup   = repository.GetWithRelatedEntities(speakerSetupID);
//            IsNotNull(() => speakerSetup);
//            var leftSpeakerSetupChannel  = speakerSetup.SpeakerSetupChannels[0];
//            var rightSpeakerSetupChannel = speakerSetup.SpeakerSetupChannels[1];
//            IsNotNull(() => leftSpeakerSetupChannel);
//            IsNotNull(() => rightSpeakerSetupChannel);

//            // Act
//            ChannelEnum leftChannelEnum  = leftSpeakerSetupChannel.GetChannelEnum();
//            ChannelEnum rightChannelEnum = rightSpeakerSetupChannel.GetChannelEnum();

//            // Assert
//            AreEqual(ChannelEnum.Left,  () => leftChannelEnum);
//            AreEqual(ChannelEnum.Right, () => rightChannelEnum);
//        }
//    }
//}
