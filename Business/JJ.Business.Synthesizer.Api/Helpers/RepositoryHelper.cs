using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Helpers;
using DefaultRepositories = JJ.Data.Synthesizer.DefaultRepositories;
using MemoryRepositories = JJ.Data.Synthesizer.Memory.Repositories;

namespace JJ.Business.Synthesizer.Api.Helpers
{
    internal static class RepositoryHelper
    {
        // PatchRepositories

        private static PatchRepositories _patchRepositories = CreatePatchRepositories();

        public static PatchRepositories PatchRepositories { get { return _patchRepositories; } }

        private static PatchRepositories CreatePatchRepositories()
        {
            return new PatchRepositories(
                CreateRepository<IPatchRepository>(ContextHelper.MemoryContext),
                CreateRepository<IOperatorRepository>(ContextHelper.MemoryContext),
                CreateRepository<IOperatorTypeRepository>(ContextHelper.MemoryContext),
                CreateRepository<IInletRepository>(ContextHelper.MemoryContext),
                CreateRepository<IOutletRepository>(ContextHelper.MemoryContext),
                CreateRepository<ICurveRepository>(ContextHelper.MemoryContext),
                CreateRepository<ISampleRepository>(ContextHelper.MemoryContext),
                CreateRepository<IDocumentRepository>(ContextHelper.MemoryContext),
                CreateRepository<IEntityPositionRepository>(ContextHelper.MemoryContext),
                CreateRepository<IIDRepository>(ContextHelper.MemoryContext));
        }

        // AudioFileOutputRepositories

        private static AudioFileOutputRepositories _audioFileOutputRepositories = CreateAudioFileOutputRepositories();

        public static AudioFileOutputRepositories AudioFileOutputRepositories { get { return _audioFileOutputRepositories; } }

        private static AudioFileOutputRepositories CreateAudioFileOutputRepositories()
        {
            return new AudioFileOutputRepositories(
                CreateRepository<IDocumentRepository>(ContextHelper.MemoryContext),
                CreateRepository<IAudioFileOutputRepository>(ContextHelper.MemoryContext),
                CreateRepository<IAudioFileFormatRepository>(ContextHelper.MemoryContext),
                CreateRepository<ISampleDataTypeRepository>(ContextHelper.MemoryContext),
                CreateRepository<ISpeakerSetupRepository>(ContextHelper.MemoryContext),
                CreateRepository<IAudioFileOutputChannelRepository>(ContextHelper.MemoryContext),
                CreateRepository<IOutletRepository>(ContextHelper.MemoryContext),
                CreateRepository<ICurveRepository>(ContextHelper.MemoryContext),
                CreateRepository<ISampleRepository>(ContextHelper.MemoryContext),
                CreateRepository<IIDRepository>(ContextHelper.MemoryContext));
        }
        // Helpers

        private static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
        {
            return RepositoryFactory.CreateRepository<TRepositoryInterface>(
                context,
                typeof(MemoryRepositories.IDRepository).Assembly,
                typeof(DefaultRepositories.IDRepository).Assembly);
        }
    }
}