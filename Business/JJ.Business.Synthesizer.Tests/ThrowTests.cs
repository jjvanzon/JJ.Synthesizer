using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Framework.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable NotAccessedVariable

namespace JJ.Business.Synthesizer.Tests
{
    /// <summary>
    /// Aims to tests rare exception for code coverage of throw statements.
    /// </summary>
    [TestCategory("Wip")]
    [TestClass]
    public class ThrowTests : SynthesizerSugar
    {
        [UsedImplicitly]
        public ThrowTests()
        {
            var invalidChannelEnum = (ChannelEnum)0;
            Channel = invalidChannelEnum;
        }

        private ThrowTests(IContext context)
            : base(context)
        { }

        [TestMethod]
        public void Test_Throw_OperatorWishes_SynthesizerSugar_ChannelIndex_InvalidChannelEnum()
        {
            int channelIndex;

            AssertHelper.ThrowsException(
                () => { channelIndex = ChannelIndex; });
        }

        [TestMethod]
        public void Test_Throw_OperatorWishes_Extensions_Panning_WithConst_InvalidChannelEnum()
        {
            AssertHelper.ThrowsException(
                () => Panning(Sine(), _[0.25]));
        }
        
        [TestMethod]
        public void Test_Throw_OperatorWishes_Extensions_Panning_Dynamic_InvalidChannelEnum()
        {
            AssertHelper.ThrowsException(
                () => Panning(Sine(), CurveIn((0, 0), (0, 1))));
        }

        [TestMethod]
        public void Test_Throw_CurveWishes_SynthesizerSugar_GetCurve_NotFound()
        {             
            using (IContext context = PersistenceHelper.CreateContext())
                new ThrowTests(context).Throw_CurveWishes_SynthesizerSugar_GetCurve_NotFound_RunTest();
        }

        public void Throw_CurveWishes_SynthesizerSugar_GetCurve_NotFound_RunTest()
        {
            AssertHelper.ThrowsException(
                () => GetCurve("Curve"));
        }

        [TestMethod]
        public void Throw_CurveWishes_SynthesizerSugar_GetOrCreateCurveIn_CacheKeyUnresolvableFromContext()
        {
            AssertHelper.ThrowsException(
                () => GetOrCreateCurveIn("", () => CurveIn(0)));
        }

        [TestMethod]
        public void Throw_AudioFileWishes_SynthesizerSugar_SaveAudio_SpeakerSetupNotSupported()
        {
            AssertHelper.ThrowsException(
                () => SaveAudio(() => Sine(), speakerSetupEnum: SpeakerSetupEnum.Undefined));
        }

    }
}
