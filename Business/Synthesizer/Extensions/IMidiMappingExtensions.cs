using System;
using JJ.Data.Synthesizer.Interfaces;

// ReSharper disable once InconsistentNaming

namespace JJ.Business.Synthesizer.Extensions
{
	public static class IMidiMappingExtensions
	{
		public static int GetMidiValueRange(this IMidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			return entity.TillMidiValue - entity.FromMidiValue;
		}

		public static double GetDimensionValueRange(this IMidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			return entity.TillDimensionValue - entity.FromDimensionValue;
		}
	}
}