using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Framework.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#pragma warning disable CS0414 // Field is assigned but its value is never used
#pragma warning disable CS0169 // Field is never used
// ReSharper disable NotAccessedVariable

namespace JJ.Business.Synthesizer.Tests
{
    /// <summary> Aims to test rare exception for code coverage. </summary>
    [TestCategory("Wip")]
    [TestClass]
    public class ThrowTests : SynthesizerSugar
    {
        private ChannelEnum _invalidChannelEnum;
        private int _channelIndex;

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
                new ThrowTests(context).ExceptionTests();
        }

        public void ExceptionTests()
        {
            Channel = _invalidChannelEnum = 0;

            // OperatorWishes.SynthesizerSugar.ChannelIndex InvalidChannelEnum
            AssertHelper.ThrowsException(() => { _channelIndex = ChannelIndex; });
            
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
            AssertHelper.ThrowsException(() => SpeakerSetupEnum.Undefined.GetChannelCount());

            // AudioFileWishes.Extensions.GetSpeakerSetupEnum ChannelCountNotSupported
            AssertHelper.ThrowsException(() => 0.GetSpeakerSetupEnum());
            
            // AudioFileWishes.Extensions.GetSampleDataTypeEnum SampleDataTypeNotSupported
            AssertHelper.ThrowsException(() => AudioConversionExtensionWishes.GetSampleDataTypeEnum<long>());

            // AudioFileWishes.Extensions.GetFileExtension AudioFileFormatNotSupported
            AssertHelper.ThrowsException(() => AudioFileFormatEnum.Undefined.GetFileExtension());
            
            // AudioFileWishes.Extensions.GetMaxAmplitude SampleDataTypeNotSupported
            AssertHelper.ThrowsException(() => SampleDataTypeEnum.Undefined.GetMaxAmplitude());
            
            // AudioFileWishes.Extensions.GetHeaderLength AudioFileFormatNotSupported
            AssertHelper.ThrowsException(() => AudioFileFormatEnum.Undefined.GetHeaderLength());
            
            // ModulationTests.Detunica EnvelopeVariationNotSupported
            AssertHelper.ThrowsException(() => new ModulationTests().Detunica(envelopeVariation: -1));
        
            // ModulationTests.DeepEcho ChannelEnumNotSupported
            AssertHelper.ThrowsException(() => new ModulationTests().DeepEcho(Sine()));
            
            // AudioFormatTests.GetValueTolerance CombinationOfValues_NotSupported
            AssertHelper.ThrowsException(() => new AudioFormatTests().GetValueTolerance(true, InterpolationTypeEnum.Undefined, SampleDataTypeEnum.Undefined));
        }
    }
}
