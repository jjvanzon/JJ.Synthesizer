using System;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Visitors;

namespace JJ.Business.Synthesizer.Roslyn
{
    internal class OperatorDtoVisitorBase_AfterCodeGenerationSimplification : OperatorDtoVisitorBase_AfterProgrammerLaziness
    {
        protected sealed override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_NoOriginShifting(Reverse_OperatorDto_ConstFactor_NoOriginShifting dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Reverse_OperatorDto_ConstFactor_WithOriginShifting(Reverse_OperatorDto_ConstFactor_WithOriginShifting dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_NoPhaseTracking(Reverse_OperatorDto_VarFactor_NoPhaseTracking dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override IOperatorDto Visit_Reverse_OperatorDto_VarFactor_WithPhaseTracking(Reverse_OperatorDto_VarFactor_WithPhaseTracking dto)
        {
            throw new NotSupportedException();
        }
    }
}
