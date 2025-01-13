using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public static class ServiceFactory
    {
        public static IContext CreateContext() 
            => ContextFactory.CreateContextFromConfiguration(ConfigWishes.PersistenceConfigurationOrDefault);

        /// <inheritdoc cref="_createrepository"/>
        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context = null) 
            => RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(
                context ?? CreateContext(), ConfigWishes.PersistenceConfigurationOrDefault);

        public static OperatorFactory CreateOperatorFactory(IContext context)
            => new OperatorFactory(
                CreateRepository<IOperatorRepository>(context),
                CreateRepository<IInletRepository>(context),
                CreateRepository<IOutletRepository>(context),
                CreateRepository<ICurveInRepository>(context),
                CreateRepository<IValueOperatorRepository>(context),
                CreateRepository<ISampleOperatorRepository>(context));

        public static CurveFactory CreateCurveFactory(IContext context)
            => new CurveFactory(
                CreateRepository<ICurveRepository>(context),
                CreateRepository<INodeRepository>(context),
                CreateRepository<INodeTypeRepository>(context));

        public static SampleManager CreateSampleManager(IContext context)
            => new SampleManager(
                CreateRepository<ISampleRepository>(context),
                CreateRepository<ISampleDataTypeRepository>(context),
                CreateRepository<ISpeakerSetupRepository>(context),
                CreateRepository<IAudioFileFormatRepository>(context),
                CreateRepository<IInterpolationTypeRepository>(context));

        public static AudioFileOutputManager CreateAudioFileOutputManager(IContext context)
            => new AudioFileOutputManager(
                CreateRepository<IAudioFileOutputRepository>(context),
                CreateRepository<IAudioFileOutputChannelRepository>(context),
                CreateRepository<ISampleDataTypeRepository>(context),
                CreateRepository<ISpeakerSetupRepository>(context),
                CreateRepository<IAudioFileFormatRepository>(context));
    }
}