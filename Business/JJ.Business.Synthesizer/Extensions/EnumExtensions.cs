using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class EnumExtensions
    {
        // AudioFileOutput

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            if (audioFileOutput.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;

            return (SpeakerSetupEnum)audioFileOutput.SpeakerSetup.ID;
        }

        public static void SetSpeakerSetupEnum(this AudioFileOutput entity, SpeakerSetupEnum enumValue, ISpeakerSetupRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == SpeakerSetupEnum.Undefined)
            {
                entity.UnlinkSpeakerSetup();
            }
            else
            {
                SpeakerSetup speakerSetup = repository.GetWithRelatedEntities((int)enumValue);
                entity.LinkTo(speakerSetup);
            }
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);

            if (entity.SampleDataType == null) return SampleDataTypeEnum.Undefined;

            return (SampleDataTypeEnum)entity.SampleDataType.ID;
        }

        public static void SetSampleDataTypeEnum(this AudioFileOutput entity, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == SampleDataTypeEnum.Undefined)
            {
                entity.UnlinkSampleDataType();
            }
            else
            {
                SampleDataType enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        public static AudioFileFormatEnum GetAudioFileFormatEnum(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            if (audioFileOutput.AudioFileFormat == null) return AudioFileFormatEnum.Undefined;

            return (AudioFileFormatEnum)audioFileOutput.AudioFileFormat.ID;
        }

        public static void SetAudioFileFormatEnum(this AudioFileOutput entity, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == AudioFileFormatEnum.Undefined)
            {
                entity.UnlinkAudioFileFormat();
            }
            else
            {
                AudioFileFormat enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        // AudioOutput

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this AudioOutput audioOutput)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);

            if (audioOutput.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;

            return (SpeakerSetupEnum)audioOutput.SpeakerSetup.ID;
        }

        public static void SetSpeakerSetupEnum(this AudioOutput entity, SpeakerSetupEnum enumValue, ISpeakerSetupRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == SpeakerSetupEnum.Undefined)
            {
                entity.UnlinkSpeakerSetup();
            }
            else
            {
                SpeakerSetup speakerSetup = repository.GetWithRelatedEntities((int)enumValue);
                entity.LinkTo(speakerSetup);
            }
        }

        // Inlet

        public static DimensionEnum GetDimensionEnum(this Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            if (entity.Dimension == null) return DimensionEnum.Undefined;

            return (DimensionEnum)entity.Dimension.ID;
        }

        public static void SetDimensionEnum(this Inlet entity, DimensionEnum enumValue, IDimensionRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == DimensionEnum.Undefined)
            {
                entity.UnlinkDimension();
            }
            else
            {
                Dimension enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        // Node

        public static NodeTypeEnum GetNodeTypeEnum(this Node node)
        {
            if (node == null) throw new NullException(() => node);

            if (node.NodeType == null) return NodeTypeEnum.Undefined;

            return (NodeTypeEnum)node.NodeType.ID;
        }

        public static void SetNodeTypeEnum(this Node entity, NodeTypeEnum enumValue, INodeTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == NodeTypeEnum.Undefined)
            {
                entity.UnlinkNodeType();
            }
            else
            {
                NodeType enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        // Operator

        public static DimensionEnum GetStandardDimensionEnum(this Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);

            if (entity.StandardDimension == null) return DimensionEnum.Undefined;

            return (DimensionEnum)entity.StandardDimension.ID;
        }

        public static void SetStandardDimensionEnum(this Operator entity, DimensionEnum enumValue, IDimensionRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == DimensionEnum.Undefined)
            {
                entity.UnlinkStandardDimension();
            }
            else
            {
                Dimension enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        public static OperatorTypeEnum GetOperatorTypeEnum(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            if (op.OperatorType == null) return OperatorTypeEnum.Undefined;

            return (OperatorTypeEnum)op.OperatorType.ID;
        }

        public static void SetOperatorTypeEnum(this Operator entity, OperatorTypeEnum enumValue, IOperatorTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == OperatorTypeEnum.Undefined)
            {
                entity.UnlinkOperatorType();
            }
            else
            {
                OperatorType enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        // Outlet

        public static DimensionEnum GetDimensionEnum(this Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            if (entity.Dimension == null) return DimensionEnum.Undefined;

            return (DimensionEnum)entity.Dimension.ID;
        }

        public static void SetDimensionEnum(this Outlet entity, DimensionEnum enumValue, IDimensionRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == DimensionEnum.Undefined)
            {
                entity.UnlinkDimension();
            }
            else
            {
                Dimension enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        // Sample

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            if (sample.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;

            return (SpeakerSetupEnum)sample.SpeakerSetup.ID;
        }

        public static void SetSpeakerSetupEnum(this Sample entity, SpeakerSetupEnum enumValue, ISpeakerSetupRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == SpeakerSetupEnum.Undefined)
            {
                entity.UnlinkSpeakerSetup();
            }
            else
            {
                SpeakerSetup speakerSetup = repository.GetWithRelatedEntities((int)enumValue);
                entity.LinkTo(speakerSetup);
            }
        }

        public static InterpolationTypeEnum GetInterpolationTypeEnum(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            if (sample.InterpolationType == null) return InterpolationTypeEnum.Undefined;

            return (InterpolationTypeEnum)sample.InterpolationType.ID;
        }

        public static void SetInterpolationTypeEnum(this Sample entity, InterpolationTypeEnum enumValue, IInterpolationTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == InterpolationTypeEnum.Undefined)
            {
                entity.UnlinkInterpolationType();
            }
            else
            {
                InterpolationType enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            if (sample.SampleDataType == null) return SampleDataTypeEnum.Undefined;

            return (SampleDataTypeEnum)sample.SampleDataType.ID;
        }

        public static void SetSampleDataTypeEnum(this Sample entity, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == SampleDataTypeEnum.Undefined)
            {
                entity.UnlinkSampleDataType();
            }
            else
            {
                SampleDataType enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        public static AudioFileFormatEnum GetAudioFileFormatEnum(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

            if (sample.AudioFileFormat == null) return AudioFileFormatEnum.Undefined;

            return (AudioFileFormatEnum)sample.AudioFileFormat.ID;
        }

        public static void SetAudioFileFormatEnum(this Sample entity, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == AudioFileFormatEnum.Undefined)
            {
                entity.UnlinkAudioFileFormat();
            }
            else
            {
                AudioFileFormat enumEntity = repository.Get((int)enumValue);
                entity.LinkTo(enumEntity);
            }
        }

        // Scale

        public static ScaleTypeEnum GetScaleTypeEnum(this Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            if (scale.ScaleType == null) return ScaleTypeEnum.Undefined;

            return (ScaleTypeEnum)scale.ScaleType.ID;
        }

        public static void SetScaleTypeEnum(this Scale entity, ScaleTypeEnum enumValue, IScaleTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            if (enumValue == ScaleTypeEnum.Undefined)
            {
                entity.UnlinkScaleType();
            }
            else
            {
                ScaleType scaleType = repository.Get((int)enumValue);
                entity.LinkTo(scaleType);
            }
        }
    }
}
