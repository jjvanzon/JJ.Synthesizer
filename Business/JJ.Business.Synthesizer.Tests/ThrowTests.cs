﻿using System;
using System.IO;
using System.IO.Pipes;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Framework.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable NotAccessedVariable
#pragma warning disable CS0414 // Field is assigned but its value is never used
#pragma warning disable CS0169 // Field is never used

namespace JJ.Business.Synthesizer.Tests
{
    /// <summary> Aims to test rare exception for code coverage. </summary>
    [TestCategory("Wip")]
    [TestClass]
    public class ThrowTests : SynthesizerSugar
    {
        private ChannelEnum _invalidChannelEnum;
        private int _channelIndex;
        private Stream _emptyStream = new MemoryStream(new byte[] { });
        private Stream _wavStream = TestHelper.GetViolin16BitMono44100WavStream();

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
            ThrowsException(() => { _channelIndex = ChannelIndex; });
            
            // OperatorWishes.SynthesizerSugar.Panning WithConst_InvalidChannelEnum 
            ThrowsException(() => Panning(Sine(), _[0.25]));
            
            // OperatorWishes.SynthesizerSugar.Panning Dynamic_InvalidChannelEnum
            ThrowsException(() => Panning(Sine(), CurveIn((0, 0), (0, 1))));
            
            // CurveWishes.SynthesizerSugar.GetCurve NotFound
            ThrowsException(() => GetCurve("Curve"));
            
            // CurveWishes.SynthesizerSugar.GetOrCreateCurveIn Internal_CacheKeyUnresolvableFromContext
            ThrowsException(() => GetOrCreateCurveIn(null, () => CurveIn(0)));
            
            // AudioFileWishes.SynthesizerSugar.SaveAudio SpeakerSetupNotSupported
            ThrowsException(() => SaveAudio(() => Sine(), speakerSetupEnum: SpeakerSetupEnum.Undefined));
            
            // AudioFileWishes.Extensions.GetChannelCount SpeakerSetupNotSupported
            ThrowsException(() => SpeakerSetupEnum.Undefined.GetChannelCount());

            // AudioFileWishes.Extensions.GetSpeakerSetupEnum ChannelCountNotSupported
            ThrowsException(() => 0.GetSpeakerSetupEnum());
            
            // AudioFileWishes.Extensions.GetSampleDataTypeEnum SampleDataTypeNotSupported
            ThrowsException(() => AudioConversionExtensionWishes.GetSampleDataTypeEnum<long>());

            // AudioFileWishes.Extensions.GetFileExtension AudioFileFormatNotSupported
            ThrowsException(() => AudioFileFormatEnum.Undefined.GetFileExtension());
            
            // AudioFileWishes.Extensions.GetMaxAmplitude SampleDataTypeNotSupported
            ThrowsException(() => SampleDataTypeEnum.Undefined.GetMaxAmplitude());
            
            // AudioFileWishes.Extensions.GetHeaderLength AudioFileFormatNotSupported
            ThrowsException(() => AudioFileFormatEnum.Undefined.GetHeaderLength());
            
            // ModulationTests.Detunica EnvelopeVariationNotSupported
            ThrowsException(() => new ModulationTests().Detunica(envelopeVariation: -1));
        
            // ModulationTests.DeepEcho ChannelEnumNotSupported
            ThrowsException(() => new ModulationTests().DeepEcho(Sine()));
            
            // AudioFormatTests.GetValueTolerance CombinationOfValuesNotSupported
            ThrowsException(() => new AudioFormatTests().GetValueTolerance(true, InterpolationTypeEnum.Undefined, SampleDataTypeEnum.Undefined));
            
            // SampleManager.CreateSample AudioFileFormatEnumNotSupported
            ThrowsException(() => Samples.CreateSample(TestHelper.GetViolin16BitMono44100WavStream(), AudioFileFormatEnum.Undefined));
            
            // SampleManager.CreateWavSample WavFileAtLeast44Bytes
            ThrowsException(() => Samples.CreateSample(_emptyStream, AudioFileFormatEnum.Wav));
            
            // SampleManager.CreateWavSample ChannelCountNotSupported
            //var wavHeaderStruct = new WavHeaderStruct { ChannelCount = 0 };
            //AssertHelper.ThrowsException(() => Samples.CreateWavSample(wavHeaderStruct),
            //                             "audioFile.ChannelCount value '0' not supported.");

        }
    }
}