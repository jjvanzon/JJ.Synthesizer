using System.Collections.Generic;
using JJ.Business.SynthesizerPrototype.Dto;

namespace JJ.Business.SynthesizerPrototype.Tests.Helpers
{
	internal static class OperatorDtoFactory
	{
		public static IOperatorDto CreateOperatorDto_8Partials()
		{
			VariableInput_OperatorDto frequency_OperatorDto = Create_Frequency_VariableInput_OperatorDto();

			const int partialCount = 8;

			IList<IOperatorDto> partialOperatorDtos = new List<IOperatorDto>(partialCount);

			for (var i = 0; i < partialCount; i++)
			{
				IOperatorDto partialOperatorDto = CreateOperatorDto_SinglePartial(frequency_OperatorDto);
				partialOperatorDtos.Add(partialOperatorDto);
			}

			IOperatorDto operatorDto = new Add_OperatorDto { Vars = partialOperatorDtos };
			return operatorDto;
		}

		public static IOperatorDto CreateOperatorDto_SinglePartial()
		{
			VariableInput_OperatorDto frequency_OperatorDto = Create_Frequency_VariableInput_OperatorDto();
			IOperatorDto operatorDto = CreateOperatorDto_SinglePartial(frequency_OperatorDto);
			return operatorDto;
		}

		private static IOperatorDto CreateOperatorDto_SinglePartial(VariableInput_OperatorDto frequency_VariableInput_OperatorDto)
		{
			const double volume = 10.0;
			const double phaseShift = 0.25;

			var dto = new Multiply_OperatorDto
			{
				AOperatorDto = new Shift_OperatorDto
				{
					SignalOperatorDto = new Sine_OperatorDto
					{
						FrequencyOperatorDto = frequency_VariableInput_OperatorDto
					},
					DistanceOperatorDto = new Number_OperatorDto
					{
						Number = phaseShift
					}
				},
				BOperatorDto = new Number_OperatorDto { Number = volume }
			};

			return dto;
		}

		private static VariableInput_OperatorDto Create_Frequency_VariableInput_OperatorDto()
		{
			const double frequency = 440.0;
			return new VariableInput_OperatorDto { DefaultValue = frequency };
		}
	}
}