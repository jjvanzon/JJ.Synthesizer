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
        // TODO: I am not sure basic other CreateRepositories method on _repositories is valid,
        // because there seems to be nothing that tells that CreateRepositories must run first.

        // RepositoryWrapper

        private static RepositoryWrapper _repositories = CreateRepositories();

        public static RepositoryWrapper Repositories { get { return _repositories; } }

        private static RepositoryWrapper CreateRepositories()
        {
            return new RepositoryWrapper(
                CreateRepository<IDocumentRepository>(ContextHelper.MemoryContext),
                CreateRepository<ICurveRepository>(ContextHelper.MemoryContext),
                CreateRepository<IPatchRepository>(ContextHelper.MemoryContext),
                CreateRepository<ISampleRepository>(ContextHelper.MemoryContext),
                CreateRepository<IAudioFileOutputRepository>(ContextHelper.MemoryContext),
                CreateRepository<IAudioOutputRepository>(ContextHelper.MemoryContext),
                CreateRepository<IDocumentReferenceRepository>(ContextHelper.MemoryContext),
                CreateRepository<INodeRepository>(ContextHelper.MemoryContext),
                CreateRepository<IOperatorRepository>(ContextHelper.MemoryContext),
                CreateRepository<IOperatorTypeRepository>(ContextHelper.MemoryContext),
                CreateRepository<IInletRepository>(ContextHelper.MemoryContext),
                CreateRepository<IOutletRepository>(ContextHelper.MemoryContext),
                CreateRepository<IScaleRepository>(ContextHelper.MemoryContext),
                CreateRepository<IToneRepository>(ContextHelper.MemoryContext),
                
                CreateRepository<IEntityPositionRepository>(ContextHelper.MemoryContext),

                CreateRepository<IAudioFileFormatRepository>(ContextHelper.MemoryContext),
                CreateRepository<IInterpolationTypeRepository>(ContextHelper.MemoryContext),
                CreateRepository<INodeTypeRepository>(ContextHelper.MemoryContext),
                CreateRepository<ISampleDataTypeRepository>(ContextHelper.MemoryContext),
                CreateRepository<ISpeakerSetupRepository>(ContextHelper.MemoryContext),
                CreateRepository<IScaleTypeRepository>(ContextHelper.MemoryContext),
                CreateRepository<IDimensionRepository>(ContextHelper.MemoryContext),

                CreateRepository<IIDRepository>(ContextHelper.MemoryContext));
        }

        // AudioFileOutputRepositories

        private static AudioFileOutputRepositories _audioFileOutputRepositories = CreateAudioFileOutputRepositories();

        public static AudioFileOutputRepositories AudioFileOutputRepositories { get { return _audioFileOutputRepositories; } }

        private static AudioFileOutputRepositories CreateAudioFileOutputRepositories()
        {
            return new AudioFileOutputRepositories(_repositories);
        }

        // CurveRepositories

        private static CurveRepositories _curveRepositories = CreateCurveRepositories();

        public static CurveRepositories CurveRepositories { get { return _curveRepositories; } }

        private static CurveRepositories CreateCurveRepositories()
        {
            return new CurveRepositories(_repositories);
        }

        // PatchRepositories

        private static PatchRepositories _patchRepositories = CreatePatchRepositories();

        public static PatchRepositories PatchRepositories { get { return _patchRepositories; } }

        private static PatchRepositories CreatePatchRepositories()
        {
            return new PatchRepositories(_repositories);
        }

        // SampleRepositories

        private static SampleRepositories _sampleRepositories = CreateSampleRepositories();

        public static SampleRepositories SampleRepositories { get { return _sampleRepositories; } }

        private static SampleRepositories CreateSampleRepositories()
        {
            return new SampleRepositories(_repositories);
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