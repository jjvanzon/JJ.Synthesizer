using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
{
    internal class PreProcessing_OperatorDtoVisitor
    {
        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            if (dto == null) throw new NullException(() => dto);

            dto = new ClassSpecialization_OperatorDtoVisitor().Execute(dto);
            dto = new MathSimplification_OperatorDtoVisitor().Execute(dto);
            dto = new MachineOptimization_OperatorDtoVisitor().Execute(dto);
            new StackLevel_OperatorDtoVisitor_PerDimensionWriter().Execute(dto);

            return dto;
        }
    }
}
