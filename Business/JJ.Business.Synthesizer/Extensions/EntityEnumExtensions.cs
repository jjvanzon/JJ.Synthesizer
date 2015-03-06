using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class EntityEnumExtensions
    {
        public static ChannelSetupEnum GetChannelSetupEnum(this Sample sample)
        {
            if (sample.ChannelSetup == null) return ChannelSetupEnum.Undefined;

            return (ChannelSetupEnum)sample.ChannelSetup.ID;
        }

        public static void SetChannelSetupEnum(this Sample sample, ChannelSetupEnum ChannelSetupEnum, IChannelSetupRepository ChannelSetupRepository)
        {
            if (ChannelSetupRepository == null) throw new NullException(() => ChannelSetupRepository);

            sample.ChannelSetup = ChannelSetupRepository.Get((int)ChannelSetupEnum);
        }

        public static ChannelTypeEnum GetChannelTypeEnum(this SampleChannel sampleChannel)
        {
            if (sampleChannel.ChannelType == null) return ChannelTypeEnum.Undefined;

            return (ChannelTypeEnum)sampleChannel.ChannelType.ID;
        }

        public static void SetChannelTypeEnum(this SampleChannel sampleChannel, ChannelTypeEnum ChannelTypeEnum, IChannelTypeRepository ChannelTypeRepository)
        {
            if (ChannelTypeRepository == null) throw new NullException(() => ChannelTypeRepository);

            sampleChannel.ChannelType = ChannelTypeRepository.Get((int)ChannelTypeEnum);
        }

        public static InterpolationTypeEnum GetInterpolationTypeEnum(this Sample sample)
        {
            if (sample.InterpolationType == null) return InterpolationTypeEnum.Undefined;

            return (InterpolationTypeEnum)sample.InterpolationType.ID;
        }

        public static void SetInterpolationTypeEnum(this Sample sample, InterpolationTypeEnum InterpolationTypeEnum, IInterpolationTypeRepository InterpolationTypeRepository)
        {
            if (InterpolationTypeRepository == null) throw new NullException(() => InterpolationTypeRepository);

            sample.InterpolationType = InterpolationTypeRepository.Get((int)InterpolationTypeEnum);
        }

        public static NodeTypeEnum GetNodeTypeEnum(this Node node)
        {
            if (node.NodeType == null) return NodeTypeEnum.Undefined;

            return (NodeTypeEnum)node.NodeType.ID;
        }

        public static void SetNodeTypeEnum(this Node node, NodeTypeEnum nodeTypeEnum, INodeTypeRepository nodeTypeRepository)
        {
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            node.NodeType = nodeTypeRepository.Get((int)nodeTypeEnum);
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum(this Sample sample)
        {
            if (sample.SampleDataType == null) return SampleDataTypeEnum.Undefined;

            return (SampleDataTypeEnum)sample.SampleDataType.ID;
        }

        public static void SetSampleDataTypeEnum(this Sample sample, SampleDataTypeEnum SampleDataTypeEnum, ISampleDataTypeRepository SampleDataTypeRepository)
        {
            if (SampleDataTypeRepository == null) throw new NullException(() => SampleDataTypeRepository);

            sample.SampleDataType = SampleDataTypeRepository.Get((int)SampleDataTypeEnum);
        }
    }
}
