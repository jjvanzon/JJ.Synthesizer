using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Framework.Common;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal class MachineOptimization_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        public OperatorDto Execute(OperatorDto dto)
        {
            //_consecutive_Add_OperatorDtos_VarA_ConstB = new List<Add_OperatorDto_VarA_ConstB>();

            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDto Visit_Number_OperatorDto_Concrete(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto_Concrete(dto);

            double value = dto.Number;

            if (DoubleHelper.IsSpecialValue(value))
            {
                return new Number_OperatorDto_NaN();
            }

            if (value == 1.0)
            {
                return new Number_OperatorDto_One();
            }

            if (value == 0.0)
            {
                return new Number_OperatorDto_Zero();
            }

            return dto;
        }

        // TODO: I want to detect sequences of additions and then replace them with a single adder.
        // There is a chicken and egg problem here. I already replaced add calculators with specialized ones.
        // But I know what specialized ones.

        //private Add_OperatorDto_VarA_ConstB _current_Add_OperatorDto_VarA_ConstB;
        //private IList<Add_OperatorDto_VarA_ConstB> _consecutive_Add_OperatorDtos_VarA_ConstB;

        //protected override OperatorDto Visit_OperatorDto_Polymorphic(OperatorDto dto)
        //{
        //    if (_current_Add_OperatorDto_VarA_ConstB != null)
        //    {
        //        _consecutive_Add_OperatorDtos_VarA_ConstB.Add(_current_Add_OperatorDto_VarA_ConstB);
        //    }

        //    var addDto = dto as Add_OperatorDto_VarA_ConstB;
        //    if (addDto != null)
        //    {
        //        _current_Add_OperatorDto_VarA_ConstB = addDto;
        //    }
        //    else
        //    {
        //        _current_Add_OperatorDto_VarA_ConstB = null;

        //        _consecutive_Add_OperatorDtos_VarA_ConstB = Visit_Consecutive_Add_OperatorDtos_VarA_ConstB(_consecutive_Add_OperatorDtos_VarA_ConstB);

        //        _consecutive_Add_OperatorDtos_VarA_ConstB.Clear();
        //    }

        //    return base.Visit_OperatorDto_Polymorphic(dto);
        //}

        //private IList<Add_OperatorDto_VarA_ConstB> Visit_Consecutive_Add_OperatorDtos_VarA_ConstB(
        //    IList<Add_OperatorDto_VarA_ConstB> consecutive_Add_OperatorDtos_VarA_ConstB)
        //{
        //    throw new NotImplementedException();
        //}

        // The code above forgets to deal with the _VarA_VarB variation.
        // However, it is also flawed a different way.
        // The adds do not have to be visited consecutively,
        // because they could be tied to operand A and B in a mixed way.
        // I think it would be more practical to visit an add operator,
        // see what's going on with its inlets,
        // and in one method rewrite the add consecutive operators to an Add_8Vars DTO.
        // then visit the rewritten operator.

        //protected override OperatorDto Visit_Add_OperatorDto_VarA_VarB(Add_OperatorDto_VarA_VarB dto)
        //{
        //    OperatorDto dto2 = Rewrite_Add_OperatorDto(dto);

        //    base.Visit_OperatorDto_Polymorphic(dto2);

        //    return dto2;
        //}

        //protected override OperatorDto Visit_Add_OperatorDto_VarA_ConstB(Add_OperatorDto_VarA_ConstB dto)
        //{
        //    OperatorDto dto2 = Rewrite_Add_OperatorDto(dto);

        //    base.Visit_OperatorDto_Polymorphic(dto2);

        //    return dto2;
        //}

        //private OperatorDto Rewrite_Add_OperatorDto(OperatorDto dto)
        //{
        //    // I am at the first add operator here.
        //    // The input might be another add operator.

        //    InletDto aInletDto = dto.InletDtos[0];
        //    InletDto bInletDto = dto.InletDtos[1];

        //    OperatorDto aOperatorDto = aInletDto.InputOperatorDto;
        //    OperatorDto bOperatorDto = bInletDto.InputOperatorDto;

        //    bool aIsAdd = aOperatorDto is Add_OperatorDto_VarA_VarB ||
        //                  aOperatorDto is Add_OperatorDto_VarA_ConstB;

        //    bool bIsAdd = bOperatorDto is Add_OperatorDto_VarA_VarB ||
        //                  bOperatorDto is Add_OperatorDto_VarA_ConstB;

        //    if (aIsAdd)
        //    {
        //        // The two operands of Add should merge together with a single multi-operand add.
        //    }

        //    if (bIsAdd)
        //    {
        //        // The two operands of Add should merge together with a single multi-operand add.
        //    }

        //    // I think I need a multi-operand Add DTO. What is the point of the 2-operand DTO's then?
        //    // Also: What is the point of specializing for const before I merged together adds?
        //    // Why do I even have such a thing as ClassSpecialization_OperatoDtoVisitor?
        //    // Because the const thing is machine optimization?
        //    // Math specialization would be independent of consts?
        //    // No, math visitor actually pre-calculates if there are consts, so it needs the const variations.
        //    // I don't know. There are a lot of chicken and egg situations here.
        //    // I think I need to be OK with the 2-operand add DTO's with consts.
        //    // But when I merged together additions, then I would have to try and pre-calculate things again.
        //    // Hmmm... the different pre-processing visitors seem to have a vague subdivision into responsibilities.
        //    // If I have factorized, I have to do the specialization with consts again... Hmmm... you would expect things to be only done once.

        //    return dto;

        //    throw new NotImplementedException();
        //}
    }
}
