using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Dto;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal class SpecializationDtoVisitor : OperatorDtoVisitorBase
    {
        public void Execute(OperatorDto operatorDto)
        {
            throw new NotImplementedException();
        }

        protected override void Visit_Add_OperatorDto_Concrete(Add_OperatorDto add_OperatorDto)
        {
            // Depth first: We need the parent and child to replace the node?
            base.Visit_Add_OperatorDto_Concrete(add_OperatorDto);

            if (add_OperatorDto.AInletDto.IsConstSpecialValue)
            {
                // TODO: Return an object, so what can replace it in-place?
                var newDto = new Number_OperatorDto(Double.NaN);

                // This would make VisitInletDto in the base visitor do:
                // inletDto.InputOperatorDto = VisitInputOperatorDto(inletDto.InputOperatorDto); 
                // That would work, but require a lof of returning values.

                // An alternative is to make the DTO's have parent references.
                // That way you can go depth first here and then replace something.
                // Would that bother calls higher up the stack?
                // Would it all of a sudden have a different object where it does not expect it?

                // It is advised in situations like this that the visit method can return a different object.
                // (of a similar base type).
                // This works for in-place translations of structures.
                // Not translations from one system of types to another system of types.

                // It also would prevent maintaining a lot of stacks, to account for the fact
                // that you cannot have two call stacks running at the same time. You'd need object stacks.
                // instead, which seems asymmetrical between the source and dest structure: source uses call stack,
                // dest uses separate stacks of objects.
            }
        }
    }
}