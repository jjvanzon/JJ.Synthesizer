using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using System;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class EnumExtensions
	{
		// AudioFileOutput

		public static SpeakerSetupEnum GetSpeakerSetupEnum(this AudioFileOutput entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (entity.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;

			return entity.SpeakerSetup.ToEnum();
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

			return entity.SampleDataType.ToEnum();
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

			return audioFileOutput.AudioFileFormat.ToEnum();
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

		// AudioFileFormat

		public static AudioFileFormatEnum ToEnum(this AudioFileFormat entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return (AudioFileFormatEnum)entity.ID;
		}

		// AudioOutput

		public static SpeakerSetupEnum GetSpeakerSetupEnum(this AudioOutput entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (entity.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;

			return entity.SpeakerSetup.ToEnum();
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

		// Channel

		public static ChannelEnum ToEnum(this Channel entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return (ChannelEnum)entity.ID;
		}

		// Dimension

		public static DimensionEnum ToEnum(this Dimension entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return (DimensionEnum)entity.ID;
		}

		// Inlet or Outlet

		public static DimensionEnum GetDimensionEnum(this IInletOrOutlet entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (entity.Dimension == null) return DimensionEnum.Undefined;

			return entity.Dimension.ToEnum();
		}

		public static void SetDimensionEnum(this IInletOrOutlet entity, DimensionEnum enumValue, IDimensionRepository repository)
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

		// InterpolationType

		public static InterpolationTypeEnum ToEnum(this InterpolationType entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return (InterpolationTypeEnum)entity.ID;
		}

		// Node

		public static NodeTypeEnum GetNodeTypeEnum(this Node node)
		{
			if (node == null) throw new NullException(() => node);

			if (node.NodeType == null) return NodeTypeEnum.Undefined;

			return node.NodeType.ToEnum();
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

		// NodeType

		public static NodeTypeEnum ToEnum(this NodeType entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return (NodeTypeEnum)entity.ID;
		}

		// Operator

		public static DimensionEnum GetStandardDimensionEnum(this Operator entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (entity.StandardDimension == null) return DimensionEnum.Undefined;

			return entity.StandardDimension.ToEnum();
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
		
		// Patch

		public static DimensionEnum GetStandardDimensionEnum(this Patch entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (entity.StandardDimension == null) return DimensionEnum.Undefined;

			return entity.StandardDimension.ToEnum();
		}

		public static void SetStandardDimensionEnum(this Patch entity, DimensionEnum enumValue, IDimensionRepository repository)
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

		// Sample

		public static SpeakerSetupEnum GetSpeakerSetupEnum(this Sample sample)
		{
			if (sample == null) throw new NullException(() => sample);

			if (sample.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;

			return sample.SpeakerSetup.ToEnum();
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

			return sample.InterpolationType.ToEnum();
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

			return sample.SampleDataType.ToEnum();
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

			return sample.AudioFileFormat.ToEnum();
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

		// SampleDataType

		public static SampleDataTypeEnum ToEnum(this SampleDataType entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return (SampleDataTypeEnum)entity.ID;
		}

		// Scale

		public static ScaleTypeEnum GetScaleTypeEnum(this Scale scale)
		{
			if (scale == null) throw new NullException(() => scale);

			if (scale.ScaleType == null) return ScaleTypeEnum.Undefined;

			return scale.ScaleType.ToEnum();
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

		// ScaleType

		public static ScaleTypeEnum ToEnum(this ScaleType entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return (ScaleTypeEnum)entity.ID;
		}

		// SpeakerSetup

		public static SpeakerSetupEnum ToEnum(this SpeakerSetup entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return (SpeakerSetupEnum)entity.ID;
		}
	}
}
