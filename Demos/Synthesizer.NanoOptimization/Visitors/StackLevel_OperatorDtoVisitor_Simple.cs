using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal class StackLevel_OperatorDtoVisitor_Simple : OperatorDtoVisitorBase
    {
        private int _currentStackLevel;

        public void Execute(OperatorDto dto)
        {
            _currentStackLevel = 0;

            Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDto Visit_OperatorDto_Polymorphic(OperatorDto dto)
        {
            dto.StackLevel = _currentStackLevel;

            _currentStackLevel++;

            base.Visit_OperatorDto_Polymorphic(dto);

            _currentStackLevel--;

            return dto;
        }
    }
}
