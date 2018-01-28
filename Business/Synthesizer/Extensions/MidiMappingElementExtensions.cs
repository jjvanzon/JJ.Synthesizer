using System;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class MidiMappingElementExtensions
	{
		public static int? TryGetMidiControllerValueRange(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillMidiControllerValue.HasValue) return null;
			if (!entity.FromMidiControllerValue.HasValue) return null;

			return entity.TillMidiControllerValue.Value - entity.FromMidiControllerValue.Value;
		}

		public static int? TryGetMidiNoteNumberRange(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillMidiNoteNumber.HasValue) return null;
			if (!entity.FromMidiNoteNumber.HasValue) return null;

			return entity.TillMidiNoteNumber.Value - entity.FromMidiNoteNumber.Value;
		}

		public static int? TryGetMidiVelocityRange(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillMidiVelocity.HasValue) return null;
			if (!entity.FromMidiVelocity.HasValue) return null;

			return entity.TillMidiVelocity.Value - entity.FromMidiVelocity.Value;
		}

		public static double? TryGetDimensionValueRange(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillDimensionValue.HasValue) return null;
			if (!entity.FromDimensionValue.HasValue) return null;

			return entity.TillDimensionValue.Value - entity.FromDimensionValue.Value;
		}

		public static double? TryGetPositionRange(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillPosition.HasValue) return null;
			if (!entity.FromPosition.HasValue) return null;

			return entity.TillPosition.Value - entity.FromPosition.Value;
		}

		public static int? TryGetToneNumberRange(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillToneNumber.HasValue) return null;
			if (!entity.FromToneNumber.HasValue) return null;

			return entity.TillToneNumber.Value - entity.FromToneNumber.Value;
		}

		public static bool HasMidiControllerValues(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasMidiControllerValues = entity.MidiControllerCode.HasValue &&
			                               entity.FromMidiControllerValue.HasValue &&
										   entity.TillMidiControllerValue.HasValue;

			return hasMidiControllerValues;
		}

		public static bool HasMidiNoteNumbers(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasMidiNoteNumbers = entity.FromMidiNoteNumber.HasValue &&
			                          entity.TillMidiNoteNumber.HasValue;

			return hasMidiNoteNumbers;
		}

		public static bool HasMidiVelocities(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasMidiVelocities = entity.FromMidiVelocity.HasValue &&
			                         entity.TillMidiVelocity.HasValue;

			return hasMidiVelocities;
		}

		public static bool HasDimensionValues(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasDimensionValues = entity.FromDimensionValue.HasValue &&
			                          entity.TillDimensionValue.HasValue;

			return hasDimensionValues;
		}

		public static bool HasPositions(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasPositions = entity.FromPosition.HasValue &&
			                    entity.TillPosition.HasValue;

			return hasPositions;
		}

		public static bool HasToneNumbers(this MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasToneNumbers = entity.Scale != null && 
			                      entity.FromToneNumber.HasValue &&
			                      entity.TillToneNumber.HasValue;

			return hasToneNumbers;
		}
	}
}