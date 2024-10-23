using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class ServiceFactory
    {
        public static OperatorFactory CreateOperatorFactory(IContext context)
            => new OperatorFactory(
                PersistenceHelper.CreateRepository<IOperatorRepository>(context),
                PersistenceHelper.CreateRepository<IInletRepository>(context),
                PersistenceHelper.CreateRepository<IOutletRepository>(context),
                PersistenceHelper.CreateRepository<ICurveInRepository>(context),
                PersistenceHelper.CreateRepository<IValueOperatorRepository>(context),
                PersistenceHelper.CreateRepository<ISampleOperatorRepository>(context));

        public static CurveFactory CreateCurveFactory(IContext context)
            => new CurveFactory(
                PersistenceHelper.CreateRepository<ICurveRepository>(context),
                PersistenceHelper.CreateRepository<INodeRepository>(context),
                PersistenceHelper.CreateRepository<INodeTypeRepository>(context));

        public static SampleManager CreateSampleManager(IContext context)
            => new SampleManager(
                PersistenceHelper.CreateRepository<ISampleRepository>(context),
                PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(context),
                PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(context),
                PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(context),
                PersistenceHelper.CreateRepository<IInterpolationTypeRepository>(context));

        public static AudioFileOutputManager CreateAudioFileOutputManager(IContext context)
            => new AudioFileOutputManager(
                PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(context),
                PersistenceHelper.CreateRepository<IAudioFileOutputChannelRepository>(context),
                PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(context),
                PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(context),
                PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(context));
    }
}