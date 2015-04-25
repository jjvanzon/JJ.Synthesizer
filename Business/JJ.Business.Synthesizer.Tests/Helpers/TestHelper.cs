using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Common;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestHelper
    {
        private const string VIOLIN_16BIT_MONO_RAW_FILE_NAME = "violin_16bit_mono.raw";
        private const string VIOLIN_16BIT_MONO_44100_WAV_FILE_NAME = "violin_16bit_mono_44100.wav";

        public static OperatorFactory CreateOperatorFactory(IContext context)
        {
            IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
            IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
            IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);
            ICurveRepository curveRepository = PersistenceHelper.CreateRepository<ICurveRepository>(context);
            ISampleRepository sampleRepository = PersistenceHelper.CreateRepository<ISampleRepository>(context);
            var factory = new OperatorFactory(operatorRepository, inletRepository, outletRepository, curveRepository, sampleRepository);
            return factory;
        }

        public static CurveFactory CreateCurveFactory(IContext context)
        {
            ICurveRepository curveRepository = PersistenceHelper.CreateRepository<ICurveRepository>(context);
            INodeRepository nodeRepository = PersistenceHelper.CreateRepository<INodeRepository>(context);
            INodeTypeRepository nodeTypeRepository = PersistenceHelper.CreateRepository<INodeTypeRepository>(context);
            var factory = new CurveFactory(curveRepository, nodeRepository, nodeTypeRepository);
            return factory;
        }

        public static SampleManager CreateSampleManager(IContext context)
        {
            ISampleRepository sampleRepository = PersistenceHelper.CreateRepository<ISampleRepository>(context);
            ISampleDataTypeRepository sampleDataTypeRepository = PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(context);
            ISpeakerSetupRepository speakerSetupRepository = PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(context);
            IAudioFileFormatRepository audioFileFormatRepository = PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(context);
            IInterpolationTypeRepository interpolationTypeRepository = PersistenceHelper.CreateRepository<IInterpolationTypeRepository>(context);

            var manager = new SampleManager(sampleRepository, sampleDataTypeRepository, speakerSetupRepository, audioFileFormatRepository, interpolationTypeRepository);
            return manager;
        }

        public static AudioFileOutputManager CreateAudioFileOutputManager(IContext context)
        {
            IAudioFileOutputRepository audioFileOutputRepository = PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(context);
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository = PersistenceHelper.CreateRepository<IAudioFileOutputChannelRepository>(context);
            ISampleDataTypeRepository sampleDataTypeRepository = PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(context);
            ISpeakerSetupRepository speakerSetupRepository = PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(context);
            IAudioFileFormatRepository audioFileFormatRepository = PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(context);

            var manager = new AudioFileOutputManager(audioFileOutputRepository, audioFileOutputChannelRepository, sampleDataTypeRepository, speakerSetupRepository, audioFileFormatRepository);
            return manager;
        }

        public static Stream GetViolin16BitMonoRawStream()
        {
            Stream stream = EmbeddedResourceHelper.GetEmbeddedResourceStream(typeof(TestHelper).Assembly, "TestResources", VIOLIN_16BIT_MONO_RAW_FILE_NAME);
            return stream;
        }

        public static Stream GetViolin16BitMono44100WavStream()
        {
            Stream stream = EmbeddedResourceHelper.GetEmbeddedResourceStream(typeof(TestHelper).Assembly, "TestResources", VIOLIN_16BIT_MONO_44100_WAV_FILE_NAME);
            return stream;
        }
    }
}
