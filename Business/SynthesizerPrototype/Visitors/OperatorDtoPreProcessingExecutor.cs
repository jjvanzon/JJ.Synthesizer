using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.Visitors
{
    public class OperatorDtoPreProcessingExecutor
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            if (dto == null) throw new NullException(() => dto);

            dto = new OperatorDtoVisitor_MathSimplification().Execute(dto);
            dto = new OperatorDtoVisitor_MachineOptimization().Execute(dto);
            new OperatorDtoVisitor_StackLevel().Execute(dto);

            return dto;
        }
    }
}
