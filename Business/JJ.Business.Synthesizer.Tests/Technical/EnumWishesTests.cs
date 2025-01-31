using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class EnumWishesTests : SynthWishes
    {
        // ToEntity

        [TestMethod]
        public void ToEntity_AudioFileFormat()
        {
            var enumValue = AudioFileFormatEnum.Wav;

            AudioFileFormat enumEntity = enumValue.ToEntity(Context);

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_Channel()
        {
            var enumValue = ChannelEnum.Left;

            Channel enumEntity = enumValue.ToEntity(Context);

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_InterpolationType()
        {
            var enumValue = InterpolationTypeEnum.Line;

            InterpolationType enumEntity = enumValue.ToEntity(Context);

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_NodeType()
        {
            var enumValue = NodeTypeEnum.Line;

            NodeType enumEntity = enumValue.ToEntity(Context);

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_SampleDataType()
        {
            var enumValue = SampleDataTypeEnum.Int16;

            SampleDataType enumEntity = enumValue.ToEntity(Context);

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }

        [TestMethod]
        public void ToEntity_SpeakerSetup()
        {
            var enumValue = SpeakerSetupEnum.Stereo;

            SpeakerSetup enumEntity = enumValue.ToEntity(Context);

            IsNotNull(() => enumEntity);
            AreEqual((int)enumValue, () => enumEntity.ID);
            AreEqual($"{enumValue}", () => enumEntity.Name);
        }
    }
}