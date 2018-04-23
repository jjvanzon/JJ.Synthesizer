using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Visitors
{
	internal class OperatorDtoVisitor_Rewriting : OperatorDtoVisitorBase_AfterMathSimplification
	{
		// General

		public IOperatorDto Execute(IOperatorDto dto) => Visit_OperatorDto_Polymorphic(dto);

		protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
			=> WithAlreadyProcessedCheck(dto, () => base.Visit_OperatorDto_Polymorphic(dto));

		// InletsToDimension

		protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Cubic_LagBehind(InletsToDimension_OperatorDto_Cubic_LagBehind dto)
			=> Process_InletsToDimension_OperatorDto(dto, new Interpolate_OperatorDto_Cubic_LagBehind());

		protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Hermite_LagBehind(InletsToDimension_OperatorDto_Hermite_LagBehind dto)
			=> Process_InletsToDimension_OperatorDto(dto, new Interpolate_OperatorDto_Hermite_LagBehind());

		protected override IOperatorDto Visit_InletsToDimension_OperatorDto_Line(InletsToDimension_OperatorDto_Line dto)
			=> Process_InletsToDimension_OperatorDto(dto, new Interpolate_OperatorDto_Line_LagBehind());

		private static IOperatorDto Process_InletsToDimension_OperatorDto(InletsToDimension_OperatorDto sourceDto, Interpolate_OperatorDto destDto)
		{
			var intermediateDto = new InletsToDimension_OperatorDto_Stripe_LagBehind();
			DtoCloner.CloneProperties(sourceDto, intermediateDto);

			DtoCloner.CloneProperties(sourceDto, destDto);
			destDto.Signal = intermediateDto;
			destDto.SamplingRate = 1.0;

			return destDto;
		}
	}
}