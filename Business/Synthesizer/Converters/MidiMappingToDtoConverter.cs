using System;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Converters
{
	public class MidiMappingToDtoConverter
	{
		public MidiMappingDto Convert(MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var dto = new MidiMappingDto
			{
				IsRelative = entity.IsRelative,
				MidiControllerCode = entity.MidiControllerCode,
				FromMidiControllerValue = entity.FromMidiControllerValue,
				TillMidiControllerValue = entity.TillMidiControllerValue,
				FromMidiNoteNumber = entity.FromMidiNoteNumber,
				TillMidiNoteNumber = entity.TillMidiNoteNumber,
				FromMidiVelocity = entity.FromMidiVelocity,
				TillMidiVelocity = entity.TillMidiVelocity,
				StandardDimensionEnum = entity.GetStandardDimensionEnum(),
				CustomDimensionName = entity.CustomDimensionName,
				FromDimensionValue = entity.FromDimensionValue,
				TillDimensionValue = entity.TillDimensionValue,
				MinDimensionValue = entity.MinDimensionValue,
				MaxDimensionValue = entity.MaxDimensionValue,
				FromPosition = entity.FromPosition,
				TillPosition = entity.TillPosition,
				FromToneNumber = entity.FromToneNumber,
				TillToneNumber = entity.TillToneNumber
			};

			return dto;
		}
	}
}
