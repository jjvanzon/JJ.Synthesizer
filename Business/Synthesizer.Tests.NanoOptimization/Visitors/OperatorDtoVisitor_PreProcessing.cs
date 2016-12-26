using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
{
    internal class OperatorDtoVisitor_PreProcessing
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            if (dto == null) throw new NullException(() => dto);

            dto = new OperatorDtoVisitor_ClassSpecialization().Execute(dto);
            dto = new OperatorDtoVisitor_MathSimplification().Execute(dto);
            dto = new OperatorDtoVisitor_MachineOptimization().Execute(dto);
            new OperatorDtoVisitor_StackLevel_PerDimensionWriter().Execute(dto);

            return dto;
        }
    }
}
