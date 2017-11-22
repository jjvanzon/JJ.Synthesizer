using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.Visitors
{
	public class OperatorDtoPreProcessingExecutor
	{
		public IOperatorDto Execute(IOperatorDto dto)
		{
			if (dto == null) throw new NullException(() => dto);

			dto = new OperatorDtoVisitor_MathSimplification().Execute(dto);
			dto = new OperatorDtoVisitor_MachineOptimization().Execute(dto);
			new OperatorDtoVisitor_DimensionStackLevels().Execute(dto);

			return dto;
		}
	}
}
