using System;
using JJ.Business.Synthesizer.Dto.Operators;

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorDtoVisitorBase_AfterRewriting : OperatorDtoVisitorBase_AfterMathSimplification
    {
        protected sealed override IOperatorDto Visit_InletsToDimension_OperatorDto_Cubic_LagBehind(InletsToDimension_OperatorDto_Cubic_LagBehind dto) => throw new NotSupportedException();

        protected sealed override IOperatorDto Visit_InletsToDimension_OperatorDto_Hermite_LagBehind(InletsToDimension_OperatorDto_Hermite_LagBehind dto) => throw new NotSupportedException();

        protected sealed override IOperatorDto Visit_InletsToDimension_OperatorDto_Line(InletsToDimension_OperatorDto_Line sourceInletsToDimensionOperatorDto) => throw new NotSupportedException();
    }
}