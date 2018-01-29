using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Calculation
{
	public struct MidiMappingCalculatorResult
	{
		public MidiMappingCalculatorResult(
			DimensionEnum standardDimensionEnum,
			string customDimensionName,
			double? dimensionValue,
			int? position,
			Scale scale,
			int? toneNumber)
		{
			StandardDimensionEnum = standardDimensionEnum;
			CustomDimensionName = customDimensionName;
			DimensionValue = dimensionValue;
			Position = position;
			Scale = scale;
			ToneNumber = toneNumber;
		}

		public DimensionEnum StandardDimensionEnum { get; }
		public string CustomDimensionName { get; }
		public double? DimensionValue { get; }
		public int? Position { get; }
		public Scale Scale { get; }
		public int? ToneNumber { get; }
	}
}