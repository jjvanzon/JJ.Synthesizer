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
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestHelper
    {
        private const string VIOLIN_16BIT_MONO_RAW_FILE_NAME = "violin_16bit_mono.raw";
        private const string VIOLIN_16BIT_MONO_44100_WAV_FILE_NAME = "violin_16bit_mono_44100.wav";

        public static OperatorFactory CreateOperatorFactory(RepositoryWrapper repositoryWraper)
        {
            if (repositoryWraper == null) throw new NullException(() => repositoryWraper);

            var factory = new OperatorFactory(
                repositoryWraper.OperatorRepository,
                repositoryWraper.OperatorTypeRepository,
                repositoryWraper.InletRepository,
                repositoryWraper.OutletRepository,
                repositoryWraper.CurveRepository,
                repositoryWraper.SampleRepository,
                repositoryWraper.IdentityRepository);

            return factory;
        }

        public static OperatorFactory CreateOperatorFactory(IContext context)
        {
            RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);
            return CreateOperatorFactory(context);
        }

        public static CurveFactory CreateCurveFactory(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            var factory = new CurveFactory(repositoryWrapper.CurveRepository, repositoryWrapper.NodeRepository, repositoryWrapper.NodeTypeRepository, repositoryWrapper.IdentityRepository);
            return factory;
        }

        public static SampleManager CreateSampleManager(IContext context)
        {
            RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);
            SampleManager manager = CreateSampleManager(repositoryWrapper);
            return manager;
        }


        public static SampleManager CreateSampleManager(RepositoryWrapper repositoryWrapper)
        {
            var sampleRepositories = new SampleRepositories(repositoryWrapper);
            var manager = new SampleManager(sampleRepositories);
            return manager;
        }

        public static AudioFileOutputManager CreateAudioFileOutputManager(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NotImplementedException();

            var audioFileOutputManager = new AudioFileOutputManager(
                repositoryWrapper.AudioFileOutputRepository,
                repositoryWrapper.AudioFileOutputChannelRepository,
                repositoryWrapper.SampleDataTypeRepository,
                repositoryWrapper.SpeakerSetupRepository,
                repositoryWrapper.AudioFileFormatRepository,
                repositoryWrapper.CurveRepository,
                repositoryWrapper.SampleRepository,
                repositoryWrapper.IdentityRepository);

            return audioFileOutputManager;
        }

        public static AudioFileOutputManager CreateAudioFileOutputManager(IContext context)
        {
            RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);
            return CreateAudioFileOutputManager(repositoryWrapper);
        }

        public static PatchManager CreatePatchManager(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            var patchManager = new PatchManager(
                repositoryWrapper.PatchRepository,
                repositoryWrapper.OperatorRepository,
                repositoryWrapper.InletRepository,
                repositoryWrapper.OutletRepository,
                repositoryWrapper.CurveRepository,
                repositoryWrapper.SampleRepository,
                repositoryWrapper.EntityPositionRepository);

            return patchManager;
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
