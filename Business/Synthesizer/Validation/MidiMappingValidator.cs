using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class MidiMappingValidator : VersatileValidator
	{
		public MidiMappingValidator(MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			ExecuteValidator(new IDValidator(entity.ID));
			ExecuteValidator(new NameValidator(entity.Name, CommonResourceFormatter.Name, required: false));

			For(entity.EntityPosition, ResourceFormatter.EntityPosition).NotNull();
			if (entity.EntityPosition != null)
			{
				ExecuteValidator(new EntityPositionValidator(entity.EntityPosition), ValidationHelper.GetMessagePrefix(entity.EntityPosition));
			}

			For(entity.MidiMappingGroup, ResourceFormatter.MidiMappingGroup).NotNull();
			For(entity.MidiMappingType, ResourceFormatter.MidiMappingType).NotNull();
			For(entity.FromDimensionValue, ResourceFormatter.FromDimensionValue).NotNaN().NotInfinity();
			For(entity.TillDimensionValue, ResourceFormatter.TillDimensionValue).NotNaN().NotInfinity();
			For(entity.MinDimensionValue, ResourceFormatter.MinDimensionValue).NotNaN().NotInfinity();
			For(entity.MaxDimensionValue, ResourceFormatter.MaxDimensionValue).NotNaN().NotInfinity();

			if (entity.GetMidiMappingTypeEnum() == MidiMappingTypeEnum.MidiController)
			{
				For(entity.MidiControllerCode, ResourceFormatter.MidiControllerCode).NotNull();
			}

			For(entity.MidiControllerCode, ResourceFormatter.MidiControllerCode)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.FromMidiValue, ResourceFormatter.FromMidiValue)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.TillMidiValue, ResourceFormatter.FromMidiValue)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);
		}
	}
}