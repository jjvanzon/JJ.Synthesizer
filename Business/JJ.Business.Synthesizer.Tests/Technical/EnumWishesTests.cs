using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class EnumWishesTests_MissingChannelExtensions : SynthWishes
    {
        [TestMethod]
        public void Test_GetChannelEnum()
        {
            var repository = PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(Context);

            {
                int          speakerSetupID = SpeakerSetupEnum.Mono.ToID();
                SpeakerSetup speakerSetup   = repository.GetWithRelatedEntities(speakerSetupID);
                IsNotNull(() => speakerSetup);
                var         singleSpeakerSetupChannel = speakerSetup.SpeakerSetupChannels[0];
                ChannelEnum singleChannelEnum         = singleSpeakerSetupChannel.GetChannelEnum();
                AreEqual(ChannelEnum.Single, () => singleChannelEnum);
            }

            {
                int          speakerSetupID = SpeakerSetupEnum.Stereo.ToID();
                SpeakerSetup speakerSetup   = repository.GetWithRelatedEntities(speakerSetupID);
                IsNotNull(() => speakerSetup);
                var         leftSpeakerSetupChannel  = speakerSetup.SpeakerSetupChannels[0];
                var         rightSpeakerSetupChannel = speakerSetup.SpeakerSetupChannels[1];
                ChannelEnum leftChannelEnum          = leftSpeakerSetupChannel.GetChannelEnum();
                ChannelEnum rightChannelEnum         = rightSpeakerSetupChannel.GetChannelEnum();
                AreEqual(ChannelEnum.Left,  () => leftChannelEnum);
                AreEqual(ChannelEnum.Right, () => rightChannelEnum);
            }
        }
    }

    [TestClass]
    public class EnumWishesTests
    {
        // ToEntity

        [TestMethod]
        public void ToEntity_AudioFileFormat()
        {
            var enumValue = AudioFileFormatEnum.Wav;

            AudioFileFormat enumEntity = enumValue.ToEntity();

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_Channel()
        {
            var enumValue = ChannelEnum.Left;

            Channel enumEntity = enumValue.ToEntity();

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_InterpolationType()
        {
            var enumValue = InterpolationTypeEnum.Line;

            InterpolationType enumEntity = enumValue.ToEntity();

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_NodeType()
        {
            var enumValue = NodeTypeEnum.Line;

            NodeType enumEntity = enumValue.ToEntity();

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_SampleDataType()
        {
            var enumValue = SampleDataTypeEnum.Int16;

            SampleDataType enumEntity = enumValue.ToEntity();

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_SpeakerSetup()
        {
            var enumValue = SpeakerSetupEnum.Stereo;

            SpeakerSetup enumEntity = enumValue.ToEntity();

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }
    }
}