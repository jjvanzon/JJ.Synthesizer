using System;
using System.IO;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable once NotAccessedField.Local
#pragma warning disable CS0414 // Field is assigned but its value is never used

namespace JJ.Business.Synthesizer.Tests
{
    /// <summary> Aims to test rare exception for code coverage. </summary>
    [TestCategory("Wip")]
    [TestClass]
    public class ThrowTests : SynthesizerSugar
    {
        private int _channelIndex;
        private ChannelEnum _invalidChannelEnum;
        private Stream _emptyStream = new MemoryStream(new byte[] { });
        private SampleDataType _invalidSampleDataType = new SampleDataType();
        private AudioFileFormat _invalidAudioFileFormat = new AudioFileFormat();
        private InterpolationType _invalidInterpolationType = new InterpolationType();
        private NodeType _invalidNodeType = new NodeType();

        [UsedImplicitly]
        public ThrowTests()
        {
        }

        private ThrowTests(IContext context)
            : base(context)
        { }
        
        [TestMethod]
        public void Test_Exceptions_InWishes()
        {             
            using (IContext context = PersistenceHelper.CreateContext())
                new ThrowTests(context).ExceptionsInWishes();
        }

        void ExceptionsInWishes()
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
        }

        [TestMethod]
        public void Test_Exceptions_InTests()
        {             
            using (IContext context = PersistenceHelper.CreateContext())
                new ThrowTests(context).ExceptionsInTests();
        }

        void ExceptionsInTests()
        {

            // ModulationTests.Detunica EnvelopeVariationNotSupported
            ThrowsException(() => new ModulationTests().Detunica(envelopeVariation: -1));
        
            // ModulationTests.DeepEcho ChannelEnumNotSupported
            ThrowsException(() => new ModulationTests().DeepEcho(Sine()));
            
            // AudioFormatTests.GetValueTolerance CombinationOfValuesNotSupported
            ThrowsException(() => new AudioFormatTests().GetValueTolerance(true, InterpolationTypeEnum.Undefined, SampleDataTypeEnum.Undefined));
        }
                    
        [TestMethod]
        public void Test_Exceptions_InBackEnd()
        {             
            using (IContext context = PersistenceHelper.CreateContext())
                new ThrowTests(context).ExceptionsInBackEnd();
        }

        void ExceptionsInBackEnd()
        {
            // SampleManager.CreateSample AudioFileFormatEnumNotSupported
            ThrowsException(() => Samples.CreateSample(TestHelper.GetViolin16BitMono44100WavStream(), AudioFileFormatEnum.Undefined));
            
            // SampleManager.CreateWavSample WavFileAtLeast44Bytes
            ThrowsException(() => Samples.CreateSample(_emptyStream, AudioFileFormatEnum.Wav));
            
            // SampleDataTypeHelper.SizeOf SampleDataTypeInvalid
            ThrowsException(() => SampleDataTypeHelper.SizeOf(SampleDataTypeEnum.Undefined));

            // WavHeaderStructToAudioFileInfoConverter ChannelCountCannotBe0
            {
                WavHeaderStruct wavHeaderStruct = TestHelper.GetValidWavHeaderStruct();
                wavHeaderStruct.ChannelCount = 0;
                ThrowsException(
                    () => WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct),
                    "wavHeaderStruct.ChannelCount cannot be 0.");
            }
            
            // WavHeaderStructToAudioFileInfoConverter BitsPerValueCannotBe0
            {
                WavHeaderStruct wavHeaderStruct = TestHelper.GetValidWavHeaderStruct();
                wavHeaderStruct.BitsPerValue = 0;
                ThrowsException(
                    () => WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct),
                    "wavHeaderStruct.BitsPerValue cannot be 0.");
            }

            // SampleManager.CreateWavSample ChannelCountNotSupported
            {
                WavHeaderStruct wavHeaderStruct = TestHelper.GetValidWavHeaderStruct();
                wavHeaderStruct.ChannelCount = short.MaxValue;
                
                var accessor = new SampleManagerAccessor(Samples);
                
                ThrowsException(() => accessor.CreateWavSample(wavHeaderStruct));
            }

            // SampleManager.CreateWavSample BytesPerValueNotSupported
            {
                WavHeaderStruct wavHeaderStruct = TestHelper.GetValidWavHeaderStruct();
                wavHeaderStruct.BitsPerValue = short.MaxValue;
                
                var accessor = new SampleManagerAccessor(Samples);
                
                ThrowsException(() => accessor.CreateWavSample(wavHeaderStruct));
            }

            // SampleCalculatorFactory.CreateSampleCalculator InvalidComboInterpolationAndSampleDataType
            {
                Sample sample = Samples.CreateSample(TestHelper.GetViolin16BitMono44100WavStream());
                sample.SampleDataType = _invalidSampleDataType;
                sample.InterpolationType = _invalidInterpolationType;
                ThrowsException(() => SampleCalculatorFactory.CreateSampleCalculator(sample));
            }

            // SampleCalculatorBase.ReadSamples AudioFileFormatNotSupported
            {
                Sample sample = Samples.CreateSample(TestHelper.GetViolin16BitMono44100WavStream());
                sample.AudioFileFormat = _invalidAudioFileFormat;
                ThrowsException(() => SampleCalculatorFactory.CreateSampleCalculator(sample));
            }
        
            // CurveCalculator.CalculateValue NodeTypeNotSupported
            {
                Curve curve = CurveIn((0, 1), (1, 0)).Curve;
                curve.Nodes[0].NodeType = _invalidNodeType;
                var curveCalculator = new CurveCalculator(curve);
                ThrowsException(() => curveCalculator.CalculateValue(0.5));
            }

            // AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator SampleDataTypeNotSupported
            {
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(Context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.SampleDataType = _invalidSampleDataType;
                ThrowsException(() => AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput));
            }

            // AudioFileOutputCalculatorBase.ctor FilePathRequired
            { 
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(Context);
                AudioFileOutput        audioFileOutput        = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.FilePath = null;
                ThrowsException(() => AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput));
            }

            // AudioFileOutputCalculatorBase.Execute AudioFileFormatNotSupported
            {
                string fileName = NameHelper.Name() + "_AudioFileOutputCalculatorBase.Execute AudioFileFormatNotSupported.wav";
                SaveAudio(() => Sine(), fileName: fileName);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(Context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.FilePath = fileName;
                audioFileOutput.AudioFileFormat = _invalidAudioFileFormat;
                IAudioFileOutputCalculator calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
                ThrowsException(() => calculator.Execute());
            }
        }
}
}
