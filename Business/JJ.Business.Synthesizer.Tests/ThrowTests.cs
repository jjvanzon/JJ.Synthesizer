using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Framework.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#pragma warning disable CS0414 // Field is assigned but its value is never used
#pragma warning disable CS0169 // Field is never used
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
        private ChannelEnum _invalidChannelEnum;
        private int _dummy;

        [UsedImplicitly]
        public ThrowTests()
        {
        }

        private ThrowTests(IContext context)
            : base(context)
        { }
        
        [TestMethod]
        public void Test_Exceptions()
        {             
            using (IContext context = PersistenceHelper.CreateContext())
                new ThrowTests(context).Throw_OperatorWishes_Extensions_RunTest();
        }

        public void Throw_OperatorWishes_Extensions_RunTest()
        {
            Channel = _invalidChannelEnum = 0;

            // OperatorWishes.SynthesizerSugar.ChannelIndex InvalidChannelEnum
            AssertHelper.ThrowsException(() => { _dummy = ChannelIndex; });
            
            // OperatorWishes.SynthesizerSugar.Panning WithConst_InvalidChannelEnum 
            AssertHelper.ThrowsException(() => Panning(Sine(), _[0.25]));
            
            // OperatorWishes.SynthesizerSugar.Panning Dynamic_InvalidChannelEnum
            AssertHelper.ThrowsException(() => Panning(Sine(), CurveIn((0, 0), (0, 1))));
            
            // CurveWishes.SynthesizerSugar.GetCurve NotFound
            AssertHelper.ThrowsException(() => GetCurve("Curve"));
            
            // CurveWishes.SynthesizerSugar.GetOrCreateCurveIn Internal_CacheKeyUnresolvableFromContext
            AssertHelper.ThrowsException(() => GetOrCreateCurveIn(null, () => CurveIn(0)));
            
            // AudioFileWishes.SynthesizerSugar.SaveAudio SpeakerSetupNotSupported
            AssertHelper.ThrowsException(() => SaveAudio(() => Sine(), speakerSetupEnum: SpeakerSetupEnum.Undefined));
            
            // AudioFileWishes.Extensions.GetChannelCount SpeakerSetupNotSupported
            // ...
        }
    }
}
