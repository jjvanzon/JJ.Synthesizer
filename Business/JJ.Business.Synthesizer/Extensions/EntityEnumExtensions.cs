using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class EntityEnumExtensions
    {
        // Node

        public static NodeTypeEnum GetNodeTypeEnum(this Node node)
        {
            if (node.NodeType == null) return NodeTypeEnum.Undefined;

            return (NodeTypeEnum)node.NodeType.ID;
        }

        public static void SetNodeTypeEnum(this Node node, NodeTypeEnum nodeTypeEnum, INodeTypeRepository nodeTypeRepository)
        {
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            NodeType nodeType = nodeTypeRepository.Get((int)nodeTypeEnum);

            node.LinkTo(nodeType);
        }

        // Sample

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this Sample sample)
        {
            if (sample.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;

            return (SpeakerSetupEnum)sample.SpeakerSetup.ID;
        }

        public static void SetSpeakerSetupEnum(this Sample sample, SpeakerSetupEnum speakerSetupEnum, ISpeakerSetupRepository speakerSetupRepository)
        {
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            SpeakerSetup speakerSetup = speakerSetupRepository.GetWithRelatedEntities((int)speakerSetupEnum);

            sample.LinkTo(speakerSetup);
        }

        public static InterpolationTypeEnum GetInterpolationTypeEnum(this Sample sample)
        {
            if (sample.InterpolationType == null) return InterpolationTypeEnum.Undefined;

            return (InterpolationTypeEnum)sample.InterpolationType.ID;
        }
        
        public static void SetInterpolationTypeEnum(this Sample sample, InterpolationTypeEnum interpolationTypeEnum, IInterpolationTypeRepository interpolationTypeRepository)
        {
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);

            InterpolationType interpolationType = interpolationTypeRepository.Get((int)interpolationTypeEnum);

            sample.LinkTo(interpolationType);
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum(this Sample sample)
        {
            if (sample.SampleDataType == null) return SampleDataTypeEnum.Undefined;

            return (SampleDataTypeEnum)sample.SampleDataType.ID;
        }

        public static void SetSampleDataTypeEnum(this Sample sample, SampleDataTypeEnum sampleDataTypeEnum, ISampleDataTypeRepository sampleDataTypeRepository)
        {
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);

            SampleDataType sampleDataType = sampleDataTypeRepository.Get((int)sampleDataTypeEnum);

            sample.LinkTo(sampleDataType);
        }

        public static AudioFileFormatEnum GetAudioFileFormatEnum(this Sample sample)
        {
            if (sample.AudioFileFormat == null) return AudioFileFormatEnum.Undefined;

            return (AudioFileFormatEnum)sample.AudioFileFormat.ID;
        }

        public static void SetAudioFileFormatEnum(this Sample sample, AudioFileFormatEnum audioFileFormatEnum, IAudioFileFormatRepository audioFileFormatRepository)
        {
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);

            AudioFileFormat audioFileFormat = audioFileFormatRepository.Get((int)audioFileFormatEnum);

            sample.LinkTo(audioFileFormat);
        }

        // AudioFileOutput

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;

            return (SpeakerSetupEnum)audioFileOutput.SpeakerSetup.ID;
        }

        public static void SetSpeakerSetupEnum(this AudioFileOutput audioFileOutput, SpeakerSetupEnum speakerSetupEnum, ISpeakerSetupRepository speakerSetupRepository)
        {
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            SpeakerSetup speakerSetup = speakerSetupRepository.GetWithRelatedEntities((int)speakerSetupEnum);

            audioFileOutput.LinkTo(speakerSetup);
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput.SampleDataType == null) return SampleDataTypeEnum.Undefined;

            return (SampleDataTypeEnum)audioFileOutput.SampleDataType.ID;
        }

        public static void SetSampleDataTypeEnum(this AudioFileOutput audioFileOutput, SampleDataTypeEnum sampleDataTypeEnum, ISampleDataTypeRepository sampleDataTypeRepository)
        {
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);

            SampleDataType sampleDataType = sampleDataTypeRepository.Get((int)sampleDataTypeEnum);

            audioFileOutput.LinkTo(sampleDataType);
        }

        public static AudioFileFormatEnum GetAudioFileFormatEnum(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput.AudioFileFormat == null) return AudioFileFormatEnum.Undefined;

            return (AudioFileFormatEnum)audioFileOutput.AudioFileFormat.ID;
        }

        public static void SetAudioFileFormatEnum(this AudioFileOutput audioFileOutput, AudioFileFormatEnum audioFileFormatEnum, IAudioFileFormatRepository audioFileFormatRepository)
        {
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);

            AudioFileFormat audioFileFormat = audioFileFormatRepository.Get((int)audioFileFormatEnum);;

            audioFileOutput.LinkTo(audioFileFormat);
        }
    }
}
