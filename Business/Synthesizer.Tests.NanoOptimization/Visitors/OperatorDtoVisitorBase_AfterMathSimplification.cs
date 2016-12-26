using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
{
    internal abstract class OperatorDtoVisitorBase_AfterMathSimplification : OperatorDtoVisitorBase
    {
        protected sealed override OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Add_OperatorDto_Vars_Consts(Add_OperatorDto_Vars_Consts dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Sine_OperatorDto_ZeroFrequency(Sine_OperatorDto_ZeroFrequency dto)
        {
            throw new NotSupportedException();
        }

        protected sealed override OperatorDtoBase Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            throw new NotSupportedException();
        }
    }
}
