﻿using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class ServiceFactory
    {
        //public static OperatorFactory CreateOperatorFactory(IContext context)
        //{
        //    IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
        //    IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
        //    IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);
        //    ICurveInRepository curveInRepository = PersistenceHelper.CreateRepository<ICurveInRepository>(context);
        //    IValueOperatorRepository valueOperatorRepository = PersistenceHelper.CreateRepository<IValueOperatorRepository>(context);
        //    ISampleOperatorRepository sampleOperatorRepository = PersistenceHelper.CreateRepository<ISampleOperatorRepository>(context);
        //    var factory = new OperatorFactory(operatorRepository, inletRepository, outletRepository, curveInRepository, valueOperatorRepository, sampleOperatorRepository);
        //    return factory;
        //}

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
    }
}