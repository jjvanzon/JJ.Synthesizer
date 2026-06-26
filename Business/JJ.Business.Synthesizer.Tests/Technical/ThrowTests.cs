using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Accessors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Calculation.Samples.SampleCalculatorFactory;
using static JJ.Business.Synthesizer.Tests.Helpers.CopiedFromFramework;
using static JJ.Business.Synthesizer.Tests.Helpers.TestHelper;
using static JJ.Framework.Testing.Core.AssertCore;
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable once NotAccessedField.Local
#pragma warning disable CS0414 // Field is assigned but its value is never used

namespace JJ.Business.Synthesizer.Tests.Technical
{
    /// <inheritdoc cref="_throwtests"/>
    [TestClass]
    [TestCategory("Technical")]
    public class ThrowTests : SynthWishes
    {
        private ChannelEnum _channelEnum;
        private Stream _emptyStream = new MemoryStream(new byte[] { });
        private SampleDataType _invalidSampleDataType = new SampleDataType();
        private AudioFileFormat _invalidAudioFileFormat = new AudioFileFormat();
        private InterpolationType _invalidInterpolationType = new InterpolationType();
        private NodeType _invalidNodeType = new NodeType();

        [TestMethod] public void Test_Exceptions_InWishes() => Run(ExceptionsInWishes);
        void ExceptionsInWishes()
        {
            // Set invalid ChannelEnum
            var configAccessor = new SynthWishesAccessor(this)._config;
            configAccessor._channel = 3;

            // OperatorWishes.SynthWishes.ChannelIndex InvalidChannelEnum
            //Throws(() => { _channelEnum = GetChannel.ToChannelEnum(GetChannels.ToSpeakerSetupEnum()); });

            // OperatorWishes.SynthWishes.Panning WithConst_InvalidChannelEnum 
            //Throws(() => Panning(Sine(), _[0.25]));

            // OperatorWishes.SynthWishes.Panning Dynamic_InvalidChannelEnum
            //Throws(() => Panning(Sine(), Curve((0, 0), (0, 1))));

            // AttributeWishes.Channels SpeakerSetupNotSupported
            //Throws(() => SpeakerSetupEnum.Undefined.Channels());

            // AttributeWishes.ChannelsToEnum ChannelCountNotSupported
            //Throws(() => 0.ChannelsToEnum());

            // AttributeWishes.Extensions.GetFileExtension AudioFileFormatNotSupported
            //Throws(() => AudioFileFormatEnum.Undefined.FileExtension());

            // AttributeWishes.Extensions.GetNominalMax SampleDataTypeNotSupported
            Throws(() => SampleDataTypeEnum.Undefined.MaxAmplitude());

            // AttributeWishes.Extensions.GetHeaderLength AudioFileFormatNotSupported
            Throws(() => AudioFileFormatEnum.Undefined.HeaderLength());
        }

        [TestMethod] public void Test_Exceptions_InTests() => Run(ExceptionsInTests);
        void ExceptionsInTests()
        {
            // ModulationTests.DeepEcho Channel NotSupported
            Throws(() =>
            {
                var modulationTests = new ModulationTests();
                modulationTests.WithChannel(3);
                modulationTests.DeepEcho(Sine());
            });
            
            // AudioFormatTests.GetValueTolerance CombinationOfValuesNotSupported
            Throws(() => new AudioFormatTests().GetValueTolerance(true, InterpolationTypeEnum.Undefined, 0));
        }

        [TestMethod] public void Test_Exceptions_InBackEnd() => Run(ExceptionsInBackEnd);
        void ExceptionsInBackEnd()
        {
            // SampleManager.CreateSample AudioFileFormatEnumNotSupported
            Throws(() => CreateSampleManager(Context).CreateSample(GetViolin16BitMono44100WavStream(), AudioFileFormatEnum.Undefined));
            
            // SampleManager.CreateWavSample WavFileAtLeast44Bytes
            Throws(() => CreateSampleManager(Context).CreateSample(_emptyStream, AudioFileFormatEnum.Wav));
            
            // SampleDataTypeHelper.SizeOf SampleDataTypeInvalid
            Throws(() => SampleDataTypeHelper.SizeOf(SampleDataTypeEnum.Undefined));

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
                
                Throws(() => accessor.CreateWavSample(wavHeaderStruct));
            }

            // SampleManager.CreateWavSample BytesPerValueNotSupported
            {
                WavHeaderStruct wavHeaderStruct = GetValidWavHeaderStruct();
                wavHeaderStruct.BitsPerValue = short.MaxValue;
                
                var accessor = new SampleManagerAccessor(CreateSampleManager(Context));
                
                Throws(() => accessor.CreateWavSample(wavHeaderStruct));
            }

            // SampleCalculatorFactory.CreateSampleCalculator InvalidComboInterpolationAndSampleDataType
            {
                Sample sample = CreateSampleManager(Context).CreateSample(GetViolin16BitMono44100WavStream());
                sample.SampleDataType = _invalidSampleDataType;
                sample.InterpolationType = _invalidInterpolationType;
                Throws(() => CreateSampleCalculator(sample));
            }

            // SampleCalculatorBase.ReadSamples AudioFileFormatNotSupported
            {
                Sample sample = CreateSampleManager(Context).CreateSample(GetViolin16BitMono44100WavStream());
                sample.AudioFileFormat = _invalidAudioFileFormat;
                Throws(() => CreateSampleCalculator(sample));
            }
        
            // CurveCalculator.CalculateValue NodeTypeNotSupported
            {
                Outlet curveOutlet = Curve((0, 1), (1, 0));
                Curve curve = curveOutlet.Operator.AsCurveIn.Curve;
                curve.Nodes[0].NodeType = _invalidNodeType;
                var curveCalculator = new CurveCalculator(curve);
                Throws(() => curveCalculator.CalculateValue(0.5));
            }

            // AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator SampleDataTypeNotSupported
            {
                AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(Context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.SampleDataType = _invalidSampleDataType;
                Throws(() => AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput));
            }

            // AudioFileOutputCalculatorBase.ctor FilePathRequired
            { 
                AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(Context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.FilePath = null;
                Throws(() => AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput));
            }

            // AudioFileOutputCalculatorBase.Execute AudioFileFormatNotSupported
            {
                string fileName = NameWishes.Name() + "_AudioFileOutputCalculatorBase.Execute AudioFileFormatNotSupported.wav";
                Save(Sine(), fileName);
                AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(Context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.FilePath = fileName;
                audioFileOutput.AudioFileFormat = _invalidAudioFileFormat;
                IAudioFileOutputCalculator calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
                Throws(() => calculator.Execute());
            }
        }
}
}
