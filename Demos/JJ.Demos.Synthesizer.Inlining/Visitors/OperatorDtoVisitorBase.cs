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
        protected virtual void Visit_OperatorDto_Polymorphic(OperatorDto operatorDto)
        {
            var add_OperatorDto = operatorDto as Add_OperatorDto;
            if (add_OperatorDto != null)
            {
                Visit_Add_OperatorDto_ConcreteOrPolymorphic(add_OperatorDto);
            }

            var multiply_OperatorDto = operatorDto as Multiply_OperatorDto;
            if (multiply_OperatorDto != null)
            {
                Visit_Multiply_OperatorDto_ConcreteOrPolymorphic(multiply_OperatorDto);
            }

            var number_OperatorDto = operatorDto as Number_OperatorDto;
            if (number_OperatorDto != null)
            {
                Visit_Number_OperatorDto(number_OperatorDto);
            }

            var shift_OperatorDto = operatorDto as Shift_OperatorDto;
            if (shift_OperatorDto != null)
            {
                Visit_Shift_OperatorDto_ConcreteOrPolymorphic(shift_OperatorDto);
            }
        }

        // Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_ConcreteOrPolymorphic(Add_OperatorDto add_OperatorDto)
        {
            bool isConcrete = add_OperatorDto.GetType() == typeof(Add_OperatorDto);

            if (isConcrete)
            {
                Visit_Add_OperatorDto_Concrete(add_OperatorDto);
            }
            else
            {
                Visit_Add_OperatorDto_Polymorphic(add_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_Concrete(Add_OperatorDto add_OperatorDto)
        {
            Visit_Add_OperatorDto_Base(add_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_Polymorphic(Add_OperatorDto add_OperatorDto)
        {
            var add_OperatorDto_VarA_VarB = add_OperatorDto as Add_OperatorDto_VarA_VarB;
            if (add_OperatorDto_VarA_VarB != null)
            {
                Visit_Add_OperatorDto_VarA_VarB(add_OperatorDto_VarA_VarB);
            }

            var add_OperatorDto_VarA_ConstB = add_OperatorDto as Add_OperatorDto_VarA_ConstB;
            if (add_OperatorDto_VarA_ConstB != null)
            {
                Visit_Add_OperatorDto_VarA_ConstB(add_OperatorDto_VarA_ConstB);
            }

            throw new UnexpectedTypeException(() => add_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_VarA_VarB(Add_OperatorDto_VarA_VarB add_OperatorDto_VarA_VarB)
        {
            Visit_Add_OperatorDto_Base(add_OperatorDto_VarA_VarB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_VarA_ConstB(Add_OperatorDto_VarA_ConstB add_OperatorDto_VarA_ConstB)
        {
            Visit_Add_OperatorDto_Base(add_OperatorDto_VarA_ConstB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Add_OperatorDto_Base(Add_OperatorDto add_OperatorDto)
        {
            Visit_OperatorDto_Base(add_OperatorDto);
        }

        // Multiply

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_ConcreteOrPolymorphic(Multiply_OperatorDto multiply_OperatorDto)
        {
            bool isConcrete = multiply_OperatorDto.GetType() == typeof(Multiply_OperatorDto);

            if (isConcrete)
            {
                Visit_Multiply_OperatorDto_Concrete(multiply_OperatorDto);
            }
            else
            {
                Visit_Multiply_OperatorDto_Polymorphic(multiply_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_Concrete(Multiply_OperatorDto multiply_OperatorDto)
        {
            Visit_Multiply_OperatorDto_Base(multiply_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_Polymorphic(Multiply_OperatorDto multiply_OperatorDto)
        {
            var multiply_OperatorDto_VarA_VarB = multiply_OperatorDto as Multiply_OperatorDto_VarA_VarB;
            if (multiply_OperatorDto_VarA_VarB != null)
            {
                Visit_Multiply_OperatorDto_VarA_VarB(multiply_OperatorDto_VarA_VarB);
            }

            var multiply_OperatorDto_VarA_ConstB = multiply_OperatorDto as Multiply_OperatorDto_VarA_ConstB;
            if (multiply_OperatorDto_VarA_ConstB != null)
            {
                Visit_Multiply_OperatorDto_VarA_ConstB(multiply_OperatorDto_VarA_ConstB);
            }

            throw new UnexpectedTypeException(() => multiply_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB multiply_OperatorDto_VarA_VarB)
        {
            Visit_Multiply_OperatorDto_Base(multiply_OperatorDto_VarA_VarB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB multiply_OperatorDto_VarA_ConstB)
        {
            Visit_Multiply_OperatorDto_Base(multiply_OperatorDto_VarA_ConstB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Multiply_OperatorDto_Base(Multiply_OperatorDto multiply_OperatorDto)
        {
            Visit_OperatorDto_Base(multiply_OperatorDto);
        }

        // Number

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Visit_Number_OperatorDto(Number_OperatorDto number_OperatorDto)
        {
            Visit_OperatorDto_Base(number_OperatorDto);
        }

        // Shift

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Shift_OperatorDto_ConcreteOrPolymorphic(Shift_OperatorDto shift_OperatorDto)
        {
            bool isConcrete = shift_OperatorDto.GetType() == typeof(Shift_OperatorDto);

            if (isConcrete)
            {
                Visit_Shift_OperatorDto_Concrete(shift_OperatorDto);
            }
            else
            {
                Visit_Shift_OperatorDto_Polymorphic(shift_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Shift_OperatorDto_Concrete(Shift_OperatorDto shift_OperatorDto)
        {
            Visit_Shift_OperatorDto_Base(shift_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Shift_OperatorDto_Polymorphic(Shift_OperatorDto shift_OperatorDto)
        {
            var shift_OperatorDto_VarSignal_VarDifference = shift_OperatorDto as Shift_OperatorDto_VarSignal_VarDifference;
            if (shift_OperatorDto_VarSignal_VarDifference != null)
            {
                Visit_Shift_OperatorDto_VarSignal_VarDifference(shift_OperatorDto_VarSignal_VarDifference);
            }

            var shift_OperatorDto_VarSignal_ConstDifference = shift_OperatorDto as Shift_OperatorDto_VarSignal_ConstDifference;
            if (shift_OperatorDto_VarSignal_ConstDifference != null)
            {
                Visit_Shift_OperatorDto_VarSignal_ConstDifference(shift_OperatorDto_VarSignal_ConstDifference);
            }

            throw new UnexpectedTypeException(() => shift_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Shift_OperatorDto_VarSignal_VarDifference(Shift_OperatorDto_VarSignal_VarDifference shift_OperatorDto_VarSignal_VarDifference)
        {
            Visit_Shift_OperatorDto_Base(shift_OperatorDto_VarSignal_VarDifference);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Shift_OperatorDto_VarSignal_ConstDifference(Shift_OperatorDto_VarSignal_ConstDifference shift_OperatorDto_VarSignal_ConstDifference)
        {
            Visit_Shift_OperatorDto_Base(shift_OperatorDto_VarSignal_ConstDifference);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Shift_OperatorDto_Base(Shift_OperatorDto shift_OperatorDto)
        {
            Visit_OperatorDto_Base(shift_OperatorDto);
        }

        // Sine

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Sine_OperatorDto_ConcreteOrPolymorphic(Sine_OperatorDto sine_OperatorDto)
        {
            bool isConcrete = sine_OperatorDto.GetType() == typeof(Sine_OperatorDto);

            if (isConcrete)
            {
                Visit_Sine_OperatorDto_Concrete(sine_OperatorDto);
            }
            else
            {
                Visit_Sine_OperatorDto_Polymorphic(sine_OperatorDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Sine_OperatorDto_Concrete(Sine_OperatorDto sine_OperatorDto)
        {
            Visit_Sine_OperatorDto_Base(sine_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Sine_OperatorDto_Polymorphic(Sine_OperatorDto sine_OperatorDto)
        {
            var sine_OperatorDto_ConstFrequency_NoOriginShifting = sine_OperatorDto as Sine_OperatorDto_ConstFrequency_NoOriginShifting;
            if (sine_OperatorDto_ConstFrequency_NoOriginShifting != null)
            {
                Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(sine_OperatorDto_ConstFrequency_NoOriginShifting);
            }

            var sine_OperatorDto_VarFrequency_NoPhaseTracking = sine_OperatorDto as Sine_OperatorDto_VarFrequency_NoPhaseTracking;
            if (sine_OperatorDto_VarFrequency_NoPhaseTracking != null)
            {
                Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(sine_OperatorDto_VarFrequency_NoPhaseTracking);
            }

            var sine_OperatorDto_VarFrequency_WithPhaseTracking = sine_OperatorDto as Sine_OperatorDto_VarFrequency_WithPhaseTracking;
            if (sine_OperatorDto_VarFrequency_WithPhaseTracking != null)
            {
                Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(sine_OperatorDto_VarFrequency_WithPhaseTracking);
            }

            throw new UnexpectedTypeException(() => sine_OperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting sine_OperatorDto_ConstFrequency_NoOriginShifting)
        {
            Visit_Sine_OperatorDto_Base(sine_OperatorDto_ConstFrequency_NoOriginShifting);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking sine_OperatorDto_VarFrequency_NoPhaseTracking)
        {
            Visit_Sine_OperatorDto_Base(sine_OperatorDto_VarFrequency_NoPhaseTracking);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking sine_OperatorDto_VarFrequency_WithPhaseTracking)
        {
            Visit_Sine_OperatorDto_Base(sine_OperatorDto_VarFrequency_WithPhaseTracking);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_Sine_OperatorDto_Base(Sine_OperatorDto sine_OperatorDto)
        {
            Visit_OperatorDto_Base(sine_OperatorDto);
        }

        // Base

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void Visit_OperatorDto_Base(OperatorDto operatorDto)
        {
            VisitInletDtos(operatorDto.InletDtos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void VisitInletDtos(IList<InletDto> inletDtos)
        {
            foreach (InletDto inletDto in inletDtos)
            {
                VisitInletDto(inletDto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void VisitInletDto(InletDto inletDto)
        {
            VisitInputOperatorDto(inletDto.InputOperatorDto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void VisitInputOperatorDto(OperatorDto operatorDto)
        {
            Visit_OperatorDto_Polymorphic(operatorDto);
        }
    }
}
