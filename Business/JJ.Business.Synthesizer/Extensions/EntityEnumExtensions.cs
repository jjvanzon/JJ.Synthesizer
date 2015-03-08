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
        public static SpeakerSetupEnum GetSpeakerSetupEnum(this Sample sample)
        {
            if (sample.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;

            return (SpeakerSetupEnum)sample.SpeakerSetup.ID;
        }

        public static void SetSpeakerSetupEnum(this Sample sample, SpeakerSetupEnum speakerSetupEnum, ISpeakerSetupRepository speakerSetupRepository)
        {
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            sample.SpeakerSetup = speakerSetupRepository.GetWithRelatedEntities((int)speakerSetupEnum);
        }

        public static InterpolationTypeEnum GetInterpolationTypeEnum(this Sample sample)
        {
            if (sample.InterpolationType == null) return InterpolationTypeEnum.Undefined;

            return (InterpolationTypeEnum)sample.InterpolationType.ID;
        }
        
        public static void SetInterpolationTypeEnum(this Sample sample, InterpolationTypeEnum interpolationTypeEnum, IInterpolationTypeRepository interpolationTypeRepository)
        {
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);

            sample.InterpolationType = interpolationTypeRepository.Get((int)interpolationTypeEnum);
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

        public static void SetSampleDataTypeEnum(this Sample sample, SampleDataTypeEnum sampleDataTypeEnum, ISampleDataTypeRepository sampleDataTypeRepository)
        {
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);

            sample.SampleDataType = sampleDataTypeRepository.Get((int)sampleDataTypeEnum);
        }

        public static AudioFileFormatEnum GetAudioFileFormatEnum(this Sample sample)
        {
            if (sample.AudioFileFormat == null) return AudioFileFormatEnum.Undefined;

            return (AudioFileFormatEnum)sample.AudioFileFormat.ID;
        }

        public static void SetAudioFileFormatEnum(this Sample sample, AudioFileFormatEnum audioFileFormatEnum, IAudioFileFormatRepository audioFileFormatRepository)
        {
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);

            sample.AudioFileFormat = audioFileFormatRepository.Get((int)audioFileFormatEnum);
        }
    }
}
