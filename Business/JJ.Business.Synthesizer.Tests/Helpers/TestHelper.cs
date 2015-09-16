using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Common;
using JJ.Framework.Data;
using System;
using System.IO;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Configuration;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestHelper
    {
        private const string VIOLIN_16BIT_MONO_RAW_FILE_NAME = "violin_16bit_mono.raw";
        private const string VIOLIN_16BIT_MONO_44100_WAV_FILE_NAME = "violin_16bit_mono_44100.wav";

        private static bool _configurationSectionsAreSet;
        public static void SetConfigurationSections()
        {
            if (!_configurationSectionsAreSet)
            {
                _configurationSectionsAreSet = true;

                var config = CustomConfigurationManager.GetSection<JJ.Business.Synthesizer.Configuration.ConfigurationSection>();
                ConfigurationHelper.SetSection(config);
            }
        }

        public static CurveFactory CreateCurveFactory(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            var factory = new CurveFactory(repositories.CurveRepository, repositories.NodeRepository, repositories.NodeTypeRepository, repositories.IDRepository);
            return factory;
        }

        public static SampleManager CreateSampleManager(IContext context)
        {
            RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositories(context);
            SampleManager manager = CreateSampleManager(repositoryWrapper);
            return manager;
        }

        public static SampleManager CreateSampleManager(RepositoryWrapper repositories)
        {
            var sampleRepositories = new SampleRepositories(repositories);
            var manager = new SampleManager(sampleRepositories);
            return manager;
        }

        public static AudioFileOutputManager CreateAudioFileOutputManager(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NotImplementedException();

            var audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));

            return audioFileOutputManager;
        }

        public static AudioFileOutputManager CreateAudioFileOutputManager(IContext context)
        {
            RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositories(context);
            return CreateAudioFileOutputManager(repositoryWrapper);
        }

        public static PatchManager CreatePatchManager(IContext context, Patch patch = null)
        {
            RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositories(context);
            return CreatePatchManager(repositoryWrapper, patch);
        }

        public static PatchManager CreatePatchManager(RepositoryWrapper repositoryWrapper, Patch patch = null)
        {
            var patchManager = new PatchManager(patch, new PatchRepositories(repositoryWrapper));
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

        public static DocumentManager CreateDocumentManager(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            return new DocumentManager(repositoryWrapper);
        }
    }
}
