using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Common;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal class SpecializationDtoVisitor : OperatorDtoVisitorBase
    {
        public OperatorDto Execute(OperatorDto operatorDto)
        {
            return Visit_OperatorDto_Polymorphic(operatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override OperatorDto Visit_Add_OperatorDto_Concrete(Add_OperatorDto add_OperatorDto)
        {
            base.Visit_Add_OperatorDto_Concrete(add_OperatorDto);

            InletDto aInletDto = add_OperatorDto.AInletDto;
            InletDto bInletDto = add_OperatorDto.BInletDto;

            if (aInletDto.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            if (bInletDto.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            if (aInletDto.IsConstZero)
            {
                return bInletDto.InputOperatorDto;
            }

            if (bInletDto.IsConstZero)
            {
                return aInletDto.InputOperatorDto;
            }

            if (aInletDto.IsConst && bInletDto.IsConst)
            {
                return new Number_OperatorDto(aInletDto.Value + bInletDto.Value);
            }

            if (aInletDto.IsVar && bInletDto.IsConst)
            {
                return new Add_OperatorDto_VarA_ConstB(aInletDto, bInletDto);
            }

            if (bInletDto.IsVar && aInletDto.IsConst)
            {
                return new Add_OperatorDto_VarA_ConstB(bInletDto, aInletDto);
            }

            return add_OperatorDto;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override OperatorDto Visit_Multiply_OperatorDto_Concrete(Multiply_OperatorDto multiply_OperatorDto)
        {
            base.Visit_Multiply_OperatorDto_Concrete(multiply_OperatorDto);

            InletDto aInletDto = multiply_OperatorDto.AInletDto;
            InletDto bInletDto = multiply_OperatorDto.BInletDto;

            if (aInletDto.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            if (bInletDto.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            if (aInletDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }

            if (bInletDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }

            if (aInletDto.IsConstOne)
            {
                return bInletDto.InputOperatorDto;
            }

            if (bInletDto.IsConstOne)
            {
                return aInletDto.InputOperatorDto;
            }

            if (aInletDto.IsConst && bInletDto.IsConst)
            {
                return new Number_OperatorDto(aInletDto.Value * bInletDto.Value);
            }

            if (aInletDto.IsVar && bInletDto.IsConst)
            {
                return new Multiply_OperatorDto_VarA_ConstB(aInletDto, bInletDto);
            }

            if (bInletDto.IsVar && aInletDto.IsConst)
            {
                return new Multiply_OperatorDto_VarA_ConstB(bInletDto, aInletDto);
            }

            return multiply_OperatorDto;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override OperatorDto Visit_Number_OperatorDto_Concrete(Number_OperatorDto number_OperatorDto)
        {
            base.Visit_Number_OperatorDto_Concrete(number_OperatorDto);

            double value = number_OperatorDto.Value;

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

            return number_OperatorDto;
        }

        // WAS HERE

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override OperatorDto Visit_Shift_OperatorDto_Concrete(Shift_OperatorDto shift_OperatorDto)
        {
            base.Visit_Shift_OperatorDto_Concrete(shift_OperatorDto);

            InletDto signalInletDto = shift_OperatorDto.SignalInletDto;
            InletDto distanceInletDto = shift_OperatorDto.DistanceInletDto;

            if (signalInletDto.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            if (distanceInletDto.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            if (signalInletDto.IsConst)
            {
                return signalInletDto.InputOperatorDto;
            }

            if (distanceInletDto.IsConstZero)
            {
                return signalInletDto.InputOperatorDto;
            }

            if (signalInletDto.IsVar && distanceInletDto.IsConst)
            {
                return new Shift_OperatorDto_VarSignal_ConstDistance(signalInletDto, distanceInletDto);
            }

            if (signalInletDto.IsVar && distanceInletDto.IsVar)
            {
                return new Shift_OperatorDto_VarSignal_ConstDistance(signalInletDto, distanceInletDto);
            }

            return shift_OperatorDto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_Concrete(Sine_OperatorDto sine_OperatorDto)
        {
            base.Visit_Sine_OperatorDto_Concrete(sine_OperatorDto);

            InletDto frequencyInletDto = sine_OperatorDto.FrequencyInletDto;

            if (frequencyInletDto.IsConstSpecialValue)
            {
                return new Number_OperatorDto_NaN();
            }

            if (frequencyInletDto.IsConstZero)
            {
                return new Number_OperatorDto_Zero();
            }

            if (frequencyInletDto.IsConst)
            {
                return new Sine_OperatorDto_ConstFrequency_NoOriginShifting(frequencyInletDto);
            }

            if (frequencyInletDto.IsVar)
            {
                return new Sine_OperatorDto_VarFrequency_WithPhaseTracking(frequencyInletDto);
            }

            return sine_OperatorDto;
        }
    }
}