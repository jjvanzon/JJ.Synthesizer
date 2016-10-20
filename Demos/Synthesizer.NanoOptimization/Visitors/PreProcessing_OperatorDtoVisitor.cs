using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal class PreProcessing_OperatorDtoVisitor
    {
        public OperatorDto Execute(OperatorDto dto)
        {
            if (dto == null) throw new NullException(() => dto);

            dto = new ClassSpecialization_OperatorDtoVisitor().Execute(dto);
            dto = new MathSimplification_OperatorDtoVisitor().Execute(dto);
            dto = new MachineOptimization_OperatorDtoVisitor().Execute(dto);

            return dto;
        }
    }
}
