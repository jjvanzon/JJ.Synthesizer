using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Visitors
{
	/// <summary>
	/// Will insert DoubleToBoolean_OperatorDto and BooleanToDouble_OperatorDto 
	/// nodes into the graph where appropriate.
	/// </summary>
	internal class OperatorDtoVisitor_BooleanBoundaries : OperatorDtoVisitorBase_AfterMathSimplification
	{
		public IOperatorDto Execute(IOperatorDto dto)
		{
			IOperatorDto dto2 = Visit_OperatorDto_Polymorphic(dto);

			if (OutputIsAlwaysBoolean(dto2))
			{
				return InsertBooleanToDouble(dto2);
			}

			return dto2;
		}

		// Visit

		protected override IOperatorDto Visit_And_OperatorDto(And_OperatorDto dto)
			=> ProcessAndOrNot(dto);

		protected override IOperatorDto Visit_Or_OperatorDto(Or_OperatorDto dto)
			=> ProcessAndOrNot(dto);

		protected override IOperatorDto Visit_Not_OperatorDto(Not_OperatorDto dto) 
			=> ProcessAndOrNot(dto);

		protected override IOperatorDto Visit_If_OperatorDto(If_OperatorDto dto)
			=> ProcessIf(dto);

		protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
		{
			return WithAlreadyProcessedCheck(dto, () =>
			{
				dto = base.Visit_OperatorDto_Polymorphic(dto);

				// ReSharper disable once InvertIf
				if (dto.OperatorTypeEnum != OperatorTypeEnum.And &&
					dto.OperatorTypeEnum != OperatorTypeEnum.Or &&
					dto.OperatorTypeEnum != OperatorTypeEnum.If &&
					dto.OperatorTypeEnum != OperatorTypeEnum.Not &&
					dto.OperatorTypeEnum != OperatorTypeEnum.BooleanToDouble &&
					dto.OperatorTypeEnum != OperatorTypeEnum.DoubleToBoolean)
				{
					dto.Inputs = dto.Inputs.Select(TryInsertBooleanToDouble).ToList();
				}

				return dto;
			});
		}

		// Process

		private IOperatorDto ProcessIf(If_OperatorDto dto)
		{
			Visit_OperatorDto_Base(dto);

			dto.Condition = TryInsertDoubleToBoolean(dto.Condition);

			return dto;
		}

		private IOperatorDto ProcessAndOrNot(IOperatorDto dto)
		{
			dto = Visit_OperatorDto_Base(dto);

			dto.Inputs = dto.Inputs.Select(TryInsertDoubleToBoolean).ToList();

			return dto;
		}

		private InputDto TryInsertDoubleToBoolean(InputDto inputDto)
		{
			bool mustConvert = !OutputIsAlwaysBoolean(inputDto);
			if (mustConvert)
			{
				return InsertDoubleToBoolean(inputDto);
			}

			return inputDto;
		}

		private static InputDto InsertDoubleToBoolean(InputDto inputDto)
		{
			return new DoubleToBoolean_OperatorDto { Number = inputDto };
		}

		private InputDto TryInsertBooleanToDouble(InputDto inputDto)
		{
			bool mustConvert = OutputIsAlwaysBoolean(inputDto);
			if (mustConvert)
			{
				return InsertBooleanToDouble(inputDto);
			}

			return inputDto;
		}

		private static IOperatorDto InsertBooleanToDouble(IOperatorDto inputOperatorDto)
		{
			return new BooleanToDouble_OperatorDto { Input = InputDtoFactory.CreateInputDto(inputOperatorDto) };
		}

		private static InputDto InsertBooleanToDouble(InputDto inputDto)
		{
			return new BooleanToDouble_OperatorDto { Input = inputDto };
		}

		private static bool OutputIsAlwaysBoolean(InputDto inputDto)
		{
			if (!inputDto.IsVar)
			{
				return false;
			}

			IOperatorDto operatorDto = inputDto.Var;

			return OutputIsAlwaysBoolean(operatorDto);
		}

		private static bool OutputIsAlwaysBoolean(IOperatorDto operatorDto)
		{
			switch (operatorDto.OperatorTypeEnum)
			{
				case OperatorTypeEnum.And:
				case OperatorTypeEnum.Equal:
				case OperatorTypeEnum.GreaterThan:
				case OperatorTypeEnum.GreaterThanOrEqual:
				case OperatorTypeEnum.NotEqual:
				case OperatorTypeEnum.LessThan:
				case OperatorTypeEnum.LessThanOrEqual:
				case OperatorTypeEnum.Not:
				case OperatorTypeEnum.Or:
				case OperatorTypeEnum.DoubleToBoolean:
					return true;

				default:
					return false;
			}
		}
	}
}
