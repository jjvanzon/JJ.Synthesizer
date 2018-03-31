using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation
{
	public class MidiMappingElementCalculatorResult
	{
		public MidiMappingElementCalculatorResult(
			DimensionEnum standardDimensionEnum,
			string customDimensionName,
			double? dimensionValue,
			int? position,
			ScaleDto scaleDto,
			int? toneNumber)
		{
			StandardDimensionEnum = standardDimensionEnum;
			CustomDimensionName = customDimensionName;
			DimensionValue = dimensionValue;
			Position = position;
			ScaleDto = scaleDto;
			ToneNumber = toneNumber;
		}

		public DimensionEnum StandardDimensionEnum { get; }
		public string CustomDimensionName { get; }
		public double? DimensionValue { get; }
		public int? Position { get; }
		public ScaleDto ScaleDto { get; }
		public int? ToneNumber { get; }
	}
}