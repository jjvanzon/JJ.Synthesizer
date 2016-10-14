using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithInheritance;
using JJ.Demos.Synthesizer.Inlining.Dto;

namespace JJ.Demos.Synthesizer.Inlining.Visitors.WithInheritance
{
    internal class DtoToCalculatorVisitor : OperatorDtoVisitorBase
    {
        private Stack<OperatorCalculatorBase> _stack;

        public OperatorCalculatorBase Execute(OperatorDto operatorDto)
        {
            Visit_OperatorDto_Polymorphic(operatorDto);

            throw new NotImplementedException();
        }

        protected override OperatorDto Visit_Add_OperatorDto_Concrete(Add_OperatorDto add_OperatorDto)
        {
            throw new NotSupportedException();
        }
    }
}
