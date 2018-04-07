using System;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Converters
{
	internal class MidiMappingToDtoConverter
	{
		public MidiMappingDto Convert(MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var dto = new MidiMappingDto
			{
				IsRelative = entity.IsRelative,
				MidiMappingTypeEnum = entity.GetMidiMappingTypeEnum(),
				FromMidiValue = entity.FromMidiValue,
				TillMidiValue = entity.TillMidiValue,
				MidiControllerCode = entity.MidiControllerCode,
				DimensionEnum = entity.GetDimensionEnum(),
				Name = NameHelper.ToCanonical(entity.Name),
				Position = entity.Position,
				FromDimensionValue = entity.FromDimensionValue,
				TillDimensionValue = entity.TillDimensionValue,
				MinDimensionValue = entity.MinDimensionValue,
				MaxDimensionValue = entity.MaxDimensionValue
			};

			return dto;
		}
	}
}
