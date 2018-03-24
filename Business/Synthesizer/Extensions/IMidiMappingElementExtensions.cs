using System;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions.Basic;

// ReSharper disable once InconsistentNaming

namespace JJ.Business.Synthesizer.Extensions
{
	public static class IMidiMappingElementExtensions
	{
		public static int GetMidiControllerValueRange(this IMidiMappingElement entity)
		{
			int? midiControllerValueRange = TryGetMidiControllerValueRange(entity);

			if (midiControllerValueRange == null)
			{
				throw new NullException(() => midiControllerValueRange);
			}

			return midiControllerValueRange.Value;
		}

		public static int? TryGetMidiControllerValueRange(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillMidiControllerValue.HasValue) return null;
			if (!entity.FromMidiControllerValue.HasValue) return null;

			return entity.TillMidiControllerValue.Value - entity.FromMidiControllerValue.Value;
		}

		public static int GetMidiNoteNumberRange(this IMidiMappingElement entity)
		{
			int? midiNoteNumberRange = TryGetMidiNoteNumberRange(entity);

			if (midiNoteNumberRange == null)
			{
				throw new NullException(() => midiNoteNumberRange);
			}

			return midiNoteNumberRange.Value;
		}

		public static int? TryGetMidiNoteNumberRange(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillMidiNoteNumber.HasValue) return null;
			if (!entity.FromMidiNoteNumber.HasValue) return null;

			return entity.TillMidiNoteNumber.Value - entity.FromMidiNoteNumber.Value;
		}

		public static int GetMidiVelocityRange(this IMidiMappingElement entity)
		{
			int? midiVelocityRange = TryGetMidiVelocityRange(entity);

			if (midiVelocityRange == null)
			{
				throw new NullException(() => midiVelocityRange);
			}

			return midiVelocityRange.Value;
		}

		public static int? TryGetMidiVelocityRange(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillMidiVelocity.HasValue) return null;
			if (!entity.FromMidiVelocity.HasValue) return null;

			return entity.TillMidiVelocity.Value - entity.FromMidiVelocity.Value;
		}

		public static double GetDimensionValueRange(this IMidiMappingElement entity)
		{
			double? midiVelocityRange = TryGetDimensionValueRange(entity);

			if (midiVelocityRange == null)
			{
				throw new NullException(() => midiVelocityRange);
			}

			return midiVelocityRange.Value;
		}

		public static double? TryGetDimensionValueRange(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillDimensionValue.HasValue) return null;
			if (!entity.FromDimensionValue.HasValue) return null;

			return entity.TillDimensionValue.Value - entity.FromDimensionValue.Value;
		}

		public static int GetPositionRange(this IMidiMappingElement entity)
		{
			int? midiVelocityRange = TryGetPositionRange(entity);

			if (midiVelocityRange == null)
			{
				throw new NullException(() => midiVelocityRange);
			}

			return midiVelocityRange.Value;
		}

		public static int? TryGetPositionRange(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillPosition.HasValue) return null;
			if (!entity.FromPosition.HasValue) return null;

			return entity.TillPosition.Value - entity.FromPosition.Value;
		}

		public static int GetToneNumberRange(this IMidiMappingElement entity)
		{
			int? midiVelocityRange = TryGetToneNumberRange(entity);

			if (midiVelocityRange == null)
			{
				throw new NullException(() => midiVelocityRange);
			}

			return midiVelocityRange.Value;
		}

		public static int? TryGetToneNumberRange(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (!entity.TillToneNumber.HasValue) return null;
			if (!entity.FromToneNumber.HasValue) return null;

			return entity.TillToneNumber.Value - entity.FromToneNumber.Value;
		}

		public static bool HasMidiControllerValues(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasMidiControllerValues = entity.MidiControllerCode.HasValue &&
			                               entity.FromMidiControllerValue.HasValue &&
										   entity.TillMidiControllerValue.HasValue;

			return hasMidiControllerValues;
		}

		public static bool HasMidiNoteNumbers(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasMidiNoteNumbers = entity.FromMidiNoteNumber.HasValue &&
			                          entity.TillMidiNoteNumber.HasValue;

			return hasMidiNoteNumbers;
		}

		public static bool HasMidiVelocities(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasMidiVelocities = entity.FromMidiVelocity.HasValue &&
			                         entity.TillMidiVelocity.HasValue;

			return hasMidiVelocities;
		}

		public static bool HasDimensionValues(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasDimensionValues = entity.FromDimensionValue.HasValue &&
			                          entity.TillDimensionValue.HasValue;

			return hasDimensionValues;
		}

		public static bool HasPositions(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasPositions = entity.FromPosition.HasValue &&
			                    entity.TillPosition.HasValue;

			return hasPositions;
		}

		public static bool HasToneNumbers(this IMidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			bool hasToneNumbers = entity.FromToneNumber.HasValue &&
			                      entity.TillToneNumber.HasValue;

			return hasToneNumbers;
		}
	}
}