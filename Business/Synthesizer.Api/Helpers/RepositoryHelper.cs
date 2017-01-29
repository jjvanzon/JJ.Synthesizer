using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Helpers;
using DefaultRepositories = JJ.Data.Synthesizer.DefaultRepositories;
using MemoryRepositories = JJ.Data.Synthesizer.Memory.Repositories;

namespace JJ.Business.Synthesizer.Api.Helpers
{
    internal static class RepositoryHelper
    {
        // RepositoryWrapper

        public static RepositoryWrapper Repositories { get; } = CreateRepositories();

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

        public static AudioFileOutputRepositories AudioFileOutputRepositories { get; } = CreateAudioFileOutputRepositories();

        private static AudioFileOutputRepositories CreateAudioFileOutputRepositories()
        {
            return new AudioFileOutputRepositories(Repositories);
        }

        // CurveRepositories

        public static CurveRepositories CurveRepositories { get; } = CreateCurveRepositories();

        private static CurveRepositories CreateCurveRepositories()
        {
            return new CurveRepositories(Repositories);
        }

        // PatchRepositories

        public static PatchRepositories PatchRepositories { get; } = CreatePatchRepositories();

        private static PatchRepositories CreatePatchRepositories()
        {
            return new PatchRepositories(Repositories);
        }

        // SampleRepositories

        public static SampleRepositories SampleRepositories { get; } = CreateSampleRepositories();

        private static SampleRepositories CreateSampleRepositories()
        {
            return new SampleRepositories(Repositories);
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