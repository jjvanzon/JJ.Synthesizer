using System;
using System.IO;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Calculation.Samples.SampleCalculatorFactory;
using static JJ.Business.Synthesizer.Tests.Helpers.CopiedFromFramework;
using static JJ.Business.Synthesizer.Tests.Helpers.TestHelper;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Tests.docs;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable once NotAccessedField.Local
#pragma warning disable CS0414 // Field is assigned but its value is never used

namespace JJ.Business.Synthesizer.Tests.Technical
{
    /// <inheritdoc cref="docs._throwtests"/>
    [TestClass]
    [TestCategory("Technical")]
    public class ThrowTests : SynthWishes
    {
        private int _channelIndex;
        private ChannelEnum _invalidChannelEnum;
        private Stream _emptyStream = new MemoryStream(new byte[] { });
        private SampleDataType _invalidSampleDataType = new SampleDataType();
        private AudioFileFormat _invalidAudioFileFormat = new AudioFileFormat();
        private InterpolationType _invalidInterpolationType = new InterpolationType();
        private NodeType _invalidNodeType = new NodeType();

        [TestMethod]
        public void Test_Exceptions_InWishes() => new ThrowTests().ExceptionsInWishes();

        void ExceptionsInWishes()
        {
            Channel = _invalidChannelEnum = 0;

            // OperatorWishes.SynthesizerSugar.ChannelIndex InvalidChannelEnum
            ThrowsException(() => { _channelIndex = ChannelIndex; });

            // OperatorWishes.SynthesizerSugar.Panning WithConst_InvalidChannelEnum 
            ThrowsException(() => Panning(Sine(), _[0.25]));

            // OperatorWishes.SynthesizerSugar.Panning Dynamic_InvalidChannelEnum
            ThrowsException(() => Panning(Sine(), Curve((0, 0), (0, 1))));

            // AudioFileWishes.Extensions.GetChannelCount SpeakerSetupNotSupported
            ThrowsException(() => SpeakerSetupEnum.Undefined.GetChannelCount());

            // AudioFileWishes.Extensions.GetSpeakerSetupEnum ChannelCountNotSupported
            ThrowsException(() => 0.ToSpeakerSetup());

            // AudioFileWishes.Extensions.GetSampleDataTypeEnum SampleDataTypeNotSupported
            ThrowsException(() => EnumSpecialWishes.GetSampleDataTypeEnum<long>());

            // AudioFileWishes.Extensions.GetFileExtension AudioFileFormatNotSupported
            ThrowsException(() => AudioFileFormatEnum.Undefined.GetFileExtension());

            // AudioFileWishes.Extensions.GetNominalMax SampleDataTypeNotSupported
            ThrowsException(() => SampleDataTypeEnum.Undefined.GetNominalMax());

            // AudioFileWishes.Extensions.GetHeaderLength AudioFileFormatNotSupported
            ThrowsException(() => AudioFileFormatEnum.Undefined.GetHeaderLength());
        }

        [TestMethod]
        public void Test_Exceptions_InTests() => new ThrowTests().ExceptionsInTests();

        void ExceptionsInTests()
        {
            // ModulationTests.DeepEcho ChannelEnumNotSupported
            ThrowsException(() =>
            {
                var modulationTests = new ModulationTests();
                modulationTests.WithChannel(ChannelEnum.Undefined);
                modulationTests.DeepEcho(Sine());
            });
            
            // AudioFormatTests.GetValueTolerance CombinationOfValuesNotSupported
            ThrowsException(() => new AudioFormatTests().GetValueTolerance(true, InterpolationTypeEnum.Undefined, SampleDataTypeEnum.Undefined));
        }

        [TestMethod]
        public void Test_Exceptions_InBackEnd() => new ThrowTests().ExceptionsInBackEnd();

        void ExceptionsInBackEnd()
        {
            // SampleManager.CreateSample AudioFileFormatEnumNotSupported
            ThrowsException(() => CreateSampleManager(Context).CreateSample(GetViolin16BitMono44100WavStream(), AudioFileFormatEnum.Undefined));
            
            // SampleManager.CreateWavSample WavFileAtLeast44Bytes
            ThrowsException(() => CreateSampleManager(Context).CreateSample(_emptyStream, AudioFileFormatEnum.Wav));
            
            // SampleDataTypeHelper.SizeOf SampleDataTypeInvalid
            ThrowsException(() => SampleDataTypeHelper.SizeOf(SampleDataTypeEnum.Undefined));

            // WavHeaderStructToAudioFileInfoConverter ChannelCountCannotBe0
            {
                WavHeaderStruct wavHeaderStruct = GetValidWavHeaderStruct();
                wavHeaderStruct.ChannelCount = 0;
                ThrowsException_OrInnerException<Exception>(
                    () => WavHeaderStructToAudioFileInfoConverterAccessor.Convert(wavHeaderStruct),
                    "wavHeaderStruct.ChannelCount cannot be 0.");
            }
            
            // WavHeaderStructToAudioFileInfoConverter BitsPerValueCannotBe0
            {
                WavHeaderStruct wavHeaderStruct = GetValidWavHeaderStruct();
                wavHeaderStruct.BitsPerValue = 0;
                ThrowsException_OrInnerException<Exception>(
                    () => WavHeaderStructToAudioFileInfoConverterAccessor.Convert(wavHeaderStruct),
                    "wavHeaderStruct.BitsPerValue cannot be 0.");
            }

            // SampleManager.CreateWavSample ChannelCountNotSupported
            {
                WavHeaderStruct wavHeaderStruct = GetValidWavHeaderStruct();
                wavHeaderStruct.ChannelCount = short.MaxValue;
                
                var accessor = new SampleManagerAccessor(CreateSampleManager(Context));
                
                ThrowsException(() => accessor.CreateWavSample(wavHeaderStruct));
            }

            // SampleManager.CreateWavSample BytesPerValueNotSupported
            {
                WavHeaderStruct wavHeaderStruct = GetValidWavHeaderStruct();
                wavHeaderStruct.BitsPerValue = short.MaxValue;
                
                var accessor = new SampleManagerAccessor(CreateSampleManager(Context));
                
                ThrowsException(() => accessor.CreateWavSample(wavHeaderStruct));
            }

            // SampleCalculatorFactory.CreateSampleCalculator InvalidComboInterpolationAndSampleDataType
            {
                Sample sample = CreateSampleManager(Context).CreateSample(GetViolin16BitMono44100WavStream());
                sample.SampleDataType = _invalidSampleDataType;
                sample.InterpolationType = _invalidInterpolationType;
                ThrowsException(() => CreateSampleCalculator(sample));
            }

            // SampleCalculatorBase.ReadSamples AudioFileFormatNotSupported
            {
                Sample sample = CreateSampleManager(Context).CreateSample(GetViolin16BitMono44100WavStream());
                sample.AudioFileFormat = _invalidAudioFileFormat;
                ThrowsException(() => CreateSampleCalculator(sample));
            }
        
            // CurveCalculator.CalculateValue NodeTypeNotSupported
            {
                Outlet curveOutlet = Curve((0, 1), (1, 0));
                Curve curve = curveOutlet.Operator.AsCurveIn.Curve;
                curve.Nodes[0].NodeType = _invalidNodeType;
                var curveCalculator = new CurveCalculator(curve);
                ThrowsException(() => curveCalculator.CalculateValue(0.5));
            }

            // AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator SampleDataTypeNotSupported
            {
                AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(Context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.SampleDataType = _invalidSampleDataType;
                ThrowsException(() => AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput));
            }

            // AudioFileOutputCalculatorBase.ctor FilePathRequired
            { 
                AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(Context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.FilePath = null;
                ThrowsException(() => AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput));
            }

            // AudioFileOutputCalculatorBase.Execute AudioFileFormatNotSupported
            {
                string fileName = NameHelper.MemberName() + "_AudioFileOutputCalculatorBase.Execute AudioFileFormatNotSupported.wav";
                WithName(fileName).Save(() => Sine());
                AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(Context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.FilePath = fileName;
                audioFileOutput.AudioFileFormat = _invalidAudioFileFormat;
                IAudioFileOutputCalculator calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
                ThrowsException(() => calculator.Execute());
            }
        }
}
}
