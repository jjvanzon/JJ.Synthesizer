using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Visitors
{
    internal abstract class OperatorDtoVisitorBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_OperatorDto_Polymorphic(OperatorDto operatorDto)
        {
            var add_OperatorDto = operatorDto as Add_OperatorDto;
            if (add_OperatorDto != null)
            {
                return Visit_Add_OperatorDto_ConcreteOrPolymorphic(add_OperatorDto);
            }

            var multiply_OperatorDto = operatorDto as Multiply_OperatorDto;
            if (multiply_OperatorDto != null)
            {
                return Visit_Multiply_OperatorDto_ConcreteOrPolymorphic(multiply_OperatorDto);
            }

            var number_OperatorDto = operatorDto as Number_OperatorDto;
            if (number_OperatorDto != null)
            {
                return Visit_Number_OperatorDto_ConcreteOrPolymorphic(number_OperatorDto);
            }

            var shift_OperatorDto = operatorDto as Shift_OperatorDto;
            if (shift_OperatorDto != null)
            {
                return Visit_Shift_OperatorDto_ConcreteOrPolymorphic(shift_OperatorDto);
            }

            var sine_OperatorDto = operatorDto as Sine_OperatorDto;
            if (sine_OperatorDto != null)
            {
                return Visit_Sine_OperatorDto_ConcreteOrPolymorphic(sine_OperatorDto);
            }

            var variableInput_OperatorDto = operatorDto as VariableInput_OperatorDto;
            if (sine_OperatorDto != null)
            {
                return Visit_VariableInput_OperatorDto(variableInput_OperatorDto);
            }

            throw new UnexpectedTypeException(() => operatorDto);
        }

        // Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_ConcreteOrPolymorphic(Add_OperatorDto add_OperatorDto)
        {
            bool isConcrete = add_OperatorDto.GetType() == typeof(Add_OperatorDto);

            if (isConcrete)
            {
                return Visit_Add_OperatorDto_Concrete(add_OperatorDto);
            }
            else
            {
                return Visit_Add_OperatorDto_Polymorphic(add_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_Concrete(Add_OperatorDto add_OperatorDto)
        {
            return Visit_Add_OperatorDto_Base(add_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_Polymorphic(Add_OperatorDto add_OperatorDto)
        {
            var add_OperatorDto_VarA_VarB = add_OperatorDto as Add_OperatorDto_VarA_VarB;
            if (add_OperatorDto_VarA_VarB != null)
            {
                return Visit_Add_OperatorDto_VarA_VarB(add_OperatorDto_VarA_VarB);
            }

            var add_OperatorDto_VarA_ConstB = add_OperatorDto as Add_OperatorDto_VarA_ConstB;
            if (add_OperatorDto_VarA_ConstB != null)
            {
                return Visit_Add_OperatorDto_VarA_ConstB(add_OperatorDto_VarA_ConstB);
            }

            var add_OperatorDto_ConstA_VarB = add_OperatorDto as Add_OperatorDto_ConstA_VarB;
            if (add_OperatorDto_ConstA_VarB != null)
            {
                return Visit_Add_OperatorDto_ConstA_VarB(add_OperatorDto_ConstA_VarB);
            }

            var add_OperatorDto_ConstA_ConstB = add_OperatorDto as Add_OperatorDto_ConstA_ConstB;
            if (add_OperatorDto_ConstA_ConstB != null)
            {
                return Visit_Add_OperatorDto_ConstA_ConstB(add_OperatorDto_ConstA_ConstB);
            }

            throw new UnexpectedTypeException(() => add_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_VarA_VarB(Add_OperatorDto_VarA_VarB add_OperatorDto_VarA_VarB)
        {
            return Visit_Add_OperatorDto_Base(add_OperatorDto_VarA_VarB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_VarA_ConstB(Add_OperatorDto_VarA_ConstB add_OperatorDto_VarA_ConstB)
        {
            return Visit_Add_OperatorDto_Base(add_OperatorDto_VarA_ConstB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_ConstA_VarB(Add_OperatorDto_ConstA_VarB add_OperatorDto_ConstA_VarB)
        {
            return Visit_Add_OperatorDto_Base(add_OperatorDto_ConstA_VarB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_ConstA_ConstB(Add_OperatorDto_ConstA_ConstB add_OperatorDto_ConstA_ConstB)
        {
            return Visit_Add_OperatorDto_Base(add_OperatorDto_ConstA_ConstB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_Base(Add_OperatorDto add_OperatorDto)
        {
            return Visit_OperatorDto_Base(add_OperatorDto);
        }

        // Multiply

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_ConcreteOrPolymorphic(Multiply_OperatorDto multiply_OperatorDto)
        {
            bool isConcrete = multiply_OperatorDto.GetType() == typeof(Multiply_OperatorDto);

            if (isConcrete)
            {
                return Visit_Multiply_OperatorDto_Concrete(multiply_OperatorDto);
            }
            else
            {
                return Visit_Multiply_OperatorDto_Polymorphic(multiply_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_Concrete(Multiply_OperatorDto multiply_OperatorDto)
        {
            return Visit_Multiply_OperatorDto_Base(multiply_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_Polymorphic(Multiply_OperatorDto multiply_OperatorDto)
        {
            var multiply_OperatorDto_VarA_VarB = multiply_OperatorDto as Multiply_OperatorDto_VarA_VarB;
            if (multiply_OperatorDto_VarA_VarB != null)
            {
                return Visit_Multiply_OperatorDto_VarA_VarB(multiply_OperatorDto_VarA_VarB);
            }

            var multiply_OperatorDto_VarA_ConstB = multiply_OperatorDto as Multiply_OperatorDto_VarA_ConstB;
            if (multiply_OperatorDto_VarA_ConstB != null)
            {
                return Visit_Multiply_OperatorDto_VarA_ConstB(multiply_OperatorDto_VarA_ConstB);
            }

            var multiply_OperatorDto_ConstA_VarB = multiply_OperatorDto as Multiply_OperatorDto_ConstA_VarB;
            if (multiply_OperatorDto_ConstA_VarB != null)
            {
                return Visit_Multiply_OperatorDto_ConstA_VarB(multiply_OperatorDto_ConstA_VarB);
            }

            var multiply_OperatorDto_ConstA_ConstB = multiply_OperatorDto as Multiply_OperatorDto_ConstA_ConstB;
            if (multiply_OperatorDto_ConstA_ConstB != null)
            {
                return Visit_Multiply_OperatorDto_ConstA_ConstB(multiply_OperatorDto_ConstA_ConstB);
            }

            throw new UnexpectedTypeException(() => multiply_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB multiply_OperatorDto_VarA_VarB)
        {
            return Visit_Multiply_OperatorDto_Base(multiply_OperatorDto_VarA_VarB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB multiply_OperatorDto_VarA_ConstB)
        {
            return Visit_Multiply_OperatorDto_Base(multiply_OperatorDto_VarA_ConstB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB multiply_OperatorDto_ConstA_VarB)
        {
            return Visit_Multiply_OperatorDto_Base(multiply_OperatorDto_ConstA_VarB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB multiply_OperatorDto_ConstA_ConstB)
        {
            return Visit_Multiply_OperatorDto_Base(multiply_OperatorDto_ConstA_ConstB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_Base(Multiply_OperatorDto multiply_OperatorDto)
        {
            return Visit_OperatorDto_Base(multiply_OperatorDto);
        }

        // Number

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_ConcreteOrPolymorphic(Number_OperatorDto number_OperatorDto)
        {
            bool isConcrete = number_OperatorDto.GetType() == typeof(Number_OperatorDto);

            if (isConcrete)
            {
                return Visit_Number_OperatorDto_Concrete(number_OperatorDto);
            }
            else
            {
                return Visit_Number_OperatorDto_Polymorphic(number_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_Concrete(Number_OperatorDto number_OperatorDto)
        {
            return Visit_Number_OperatorDto_Base(number_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_Polymorphic(Number_OperatorDto number_OperatorDto)
        {
            var number_OperatorDto_NaN = number_OperatorDto as Number_OperatorDto_NaN;
            if (number_OperatorDto_NaN != null)
            {
                return Visit_Number_OperatorDto_NaN(number_OperatorDto_NaN);
            }

            var number_OperatorDto_One = number_OperatorDto as Number_OperatorDto_One;
            if (number_OperatorDto_One != null)
            {
                return Visit_Number_OperatorDto_One(number_OperatorDto_One);
            }

            var number_OperatorDto_Zero = number_OperatorDto as Number_OperatorDto_Zero;
            if (number_OperatorDto_Zero != null)
            {
                return Visit_Number_OperatorDto_Zero(number_OperatorDto_Zero);
            }

            throw new UnexpectedTypeException(() => number_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN number_OperatorDto_NaN)
        {
            return Visit_Number_OperatorDto_Base(number_OperatorDto_NaN);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One number_OperatorDto_One)
        {
            return Visit_Number_OperatorDto_Base(number_OperatorDto_One);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero number_OperatorDto_Zero)
        {
            return Visit_Number_OperatorDto_Base(number_OperatorDto_Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_Base(Number_OperatorDto number_OperatorDto)
        {
            return Visit_OperatorDto_Base(number_OperatorDto);
        }

        // Shift

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_ConcreteOrPolymorphic(Shift_OperatorDto shift_OperatorDto)
        {
            bool isConcrete = shift_OperatorDto.GetType() == typeof(Shift_OperatorDto);

            if (isConcrete)
            {
                return Visit_Shift_OperatorDto_Concrete(shift_OperatorDto);
            }
            else
            {
                return Visit_Shift_OperatorDto_Polymorphic(shift_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_Concrete(Shift_OperatorDto shift_OperatorDto)
        {
            return Visit_Shift_OperatorDto_Base(shift_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_Polymorphic(Shift_OperatorDto shift_OperatorDto)
        {
            var shift_OperatorDto_VarSignal_VarDistance = shift_OperatorDto as Shift_OperatorDto_VarSignal_VarDistance;
            if (shift_OperatorDto_VarSignal_VarDistance != null)
            {
                return Visit_Shift_OperatorDto_VarSignal_VarDistance(shift_OperatorDto_VarSignal_VarDistance);
            }

            var shift_OperatorDto_VarSignal_ConstDistance = shift_OperatorDto as Shift_OperatorDto_VarSignal_ConstDistance;
            if (shift_OperatorDto_VarSignal_ConstDistance != null)
            {
                return Visit_Shift_OperatorDto_VarSignal_ConstDistance(shift_OperatorDto_VarSignal_ConstDistance);
            }

            var shift_OperatorDto_ConstSignal_VarDistance = shift_OperatorDto as Shift_OperatorDto_ConstSignal_VarDistance;
            if (shift_OperatorDto_ConstSignal_VarDistance != null)
            {
                return Visit_Shift_OperatorDto_ConstSignal_VarDistance(shift_OperatorDto_ConstSignal_VarDistance);
            }

            var shift_OperatorDto_ConstSignal_ConstDistance = shift_OperatorDto as Shift_OperatorDto_ConstSignal_ConstDistance;
            if (shift_OperatorDto_ConstSignal_ConstDistance != null)
            {
                return Visit_Shift_OperatorDto_ConstSignal_ConstDistance(shift_OperatorDto_ConstSignal_ConstDistance);
            }

            throw new UnexpectedTypeException(() => shift_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance shift_OperatorDto_VarSignal_VarDistance)
        {
            return Visit_Shift_OperatorDto_Base(shift_OperatorDto_VarSignal_VarDistance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance shift_OperatorDto_VarSignal_ConstDistance)
        {
            return Visit_Shift_OperatorDto_Base(shift_OperatorDto_VarSignal_ConstDistance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance shift_OperatorDto_ConstSignal_VarDistance)
        {
            return Visit_Shift_OperatorDto_Base(shift_OperatorDto_ConstSignal_VarDistance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance shift_OperatorDto_ConstSignal_ConstDistance)
        {
            return Visit_Shift_OperatorDto_Base(shift_OperatorDto_ConstSignal_ConstDistance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_Base(Shift_OperatorDto shift_OperatorDto)
        {
            return Visit_OperatorDto_Base(shift_OperatorDto);
        }

        // Sine

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_ConcreteOrPolymorphic(Sine_OperatorDto sine_OperatorDto)
        {
            bool isConcrete = sine_OperatorDto.GetType() == typeof(Sine_OperatorDto);

            if (isConcrete)
            {
                return Visit_Sine_OperatorDto_Concrete(sine_OperatorDto);
            }
            else
            {
                return Visit_Sine_OperatorDto_Polymorphic(sine_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_Concrete(Sine_OperatorDto sine_OperatorDto)
        {
            return Visit_Sine_OperatorDto_Base(sine_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_Polymorphic(Sine_OperatorDto sine_OperatorDto)
        {
            var sine_OperatorDto_ConstFrequency_NoOriginShifting = sine_OperatorDto as Sine_OperatorDto_ConstFrequency_NoOriginShifting;
            if (sine_OperatorDto_ConstFrequency_NoOriginShifting != null)
            {
                return Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(sine_OperatorDto_ConstFrequency_NoOriginShifting);
            }

            var sine_OperatorDto_VarFrequency_NoPhaseTracking = sine_OperatorDto as Sine_OperatorDto_VarFrequency_NoPhaseTracking;
            if (sine_OperatorDto_VarFrequency_NoPhaseTracking != null)
            {
                return Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(sine_OperatorDto_VarFrequency_NoPhaseTracking);
            }

            var sine_OperatorDto_VarFrequency_WithPhaseTracking = sine_OperatorDto as Sine_OperatorDto_VarFrequency_WithPhaseTracking;
            if (sine_OperatorDto_VarFrequency_WithPhaseTracking != null)
            {
                return Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(sine_OperatorDto_VarFrequency_WithPhaseTracking);
            }

            throw new UnexpectedTypeException(() => sine_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting sine_OperatorDto_ConstFrequency_NoOriginShifting)
        {
            return Visit_Sine_OperatorDto_Base(sine_OperatorDto_ConstFrequency_NoOriginShifting);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking sine_OperatorDto_VarFrequency_NoPhaseTracking)
        {
            return Visit_Sine_OperatorDto_Base(sine_OperatorDto_VarFrequency_NoPhaseTracking);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking sine_OperatorDto_VarFrequency_WithPhaseTracking)
        {
            return Visit_Sine_OperatorDto_Base(sine_OperatorDto_VarFrequency_WithPhaseTracking);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_Base(Sine_OperatorDto sine_OperatorDto)
        {
            return Visit_OperatorDto_Base(sine_OperatorDto);
        }

        // VariableInput

        private OperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto variableInput_OperatorDto)
        {
            return Visit_OperatorDto_Base(variableInput_OperatorDto);
        }

        // Base

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_OperatorDto_Base(OperatorDto operatorDto)
        {
            operatorDto.InletDtos = VisitInletDtos(operatorDto.InletDtos);

            return operatorDto;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual IList<InletDto> VisitInletDtos(IList<InletDto> inletDtos)
        {
            for (int i = 0; i < inletDtos.Count; i++)
            {
                InletDto inletDto = inletDtos[i];
                inletDtos[i] = VisitInletDto(inletDto);
            }

            return inletDtos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual InletDto VisitInletDto(InletDto inletDto)
        {
            inletDto.InputOperatorDto = VisitInputOperatorDto(inletDto.InputOperatorDto);

            return inletDto;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto VisitInputOperatorDto(OperatorDto operatorDto)
        {
            return Visit_OperatorDto_Polymorphic(operatorDto);
        }
    }
}
