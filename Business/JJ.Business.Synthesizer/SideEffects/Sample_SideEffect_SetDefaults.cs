using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Business;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.SideEffects
{
    public class Sample_SideEffect_SetDefaults : ISideEffect
    {
        private Sample _entity;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private IChannelSetupRepository _channelSetupRepository;
        private IInterpolationTypeRepository _interpolationTypeRepository;

        public Sample_SideEffect_SetDefaults(
            Sample entity,
            ISampleDataTypeRepository sampleDataTypeRepository,
            IChannelSetupRepository channelSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (channelSetupRepository == null) throw new NullException(() => channelSetupRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);

            _entity = entity;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _channelSetupRepository = channelSetupRepository;
            _interpolationTypeRepository = interpolationTypeRepository;
        }

        public void Execute()
        {
            _entity.Amplifier = 1;
            _entity.TimeMultiplier = 1;
            _entity.IsActive = true;
            _entity.SamplingRate = 44100;
            _entity.SetSampleDataTypeEnum(SampleDataTypeEnum.Int16, _sampleDataTypeRepository);
            _entity.SetChannelSetupEnum(ChannelSetupEnum.Mono, _channelSetupRepository);
            _entity.SetInterpolationTypeEnum(InterpolationTypeEnum.Line, _interpolationTypeRepository);
        }
    }
}