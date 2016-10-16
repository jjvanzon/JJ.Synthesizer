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
        protected virtual OperatorDto Visit_OperatorDto_Polymorphic(OperatorDto dto)
        {
            var add_OperatorDto = dto as Add_OperatorDto;
            if (add_OperatorDto != null)
            {
                return Visit_Add_OperatorDto_ConcreteOrPolymorphic(add_OperatorDto);
            }

            var multiply_OperatorDto = dto as Multiply_OperatorDto;
            if (multiply_OperatorDto != null)
            {
                return Visit_Multiply_OperatorDto_ConcreteOrPolymorphic(multiply_OperatorDto);
            }

            var number_OperatorDto = dto as Number_OperatorDto;
            if (number_OperatorDto != null)
            {
                return Visit_Number_OperatorDto_ConcreteOrPolymorphic(number_OperatorDto);
            }

            var shift_OperatorDto = dto as Shift_OperatorDto;
            if (shift_OperatorDto != null)
            {
                return Visit_Shift_OperatorDto_ConcreteOrPolymorphic(shift_OperatorDto);
            }

            var sine_OperatorDto = dto as Sine_OperatorDto;
            if (sine_OperatorDto != null)
            {
                return Visit_Sine_OperatorDto_ConcreteOrPolymorphic(sine_OperatorDto);
            }

            var variableInput_OperatorDto = dto as VariableInput_OperatorDto;
            if (variableInput_OperatorDto != null)
            {
                return Visit_VariableInput_OperatorDto(variableInput_OperatorDto);
            }

            throw new UnexpectedTypeException(() => dto);
        }

        // Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_ConcreteOrPolymorphic(Add_OperatorDto dto)
        {
            bool isConcrete = dto.GetType() == typeof(Add_OperatorDto);

            if (isConcrete)
            {
                return Visit_Add_OperatorDto_Concrete(dto);
            }
            else
            {
                return Visit_Add_OperatorDto_Polymorphic(dto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_Concrete(Add_OperatorDto dto)
        {
            return Visit_Add_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_Polymorphic(Add_OperatorDto dto)
        {
            var add_OperatorDto_VarA_VarB = dto as Add_OperatorDto_VarA_VarB;
            if (add_OperatorDto_VarA_VarB != null)
            {
                return Visit_Add_OperatorDto_VarA_VarB(add_OperatorDto_VarA_VarB);
            }

            var add_OperatorDto_VarA_ConstB = dto as Add_OperatorDto_VarA_ConstB;
            if (add_OperatorDto_VarA_ConstB != null)
            {
                return Visit_Add_OperatorDto_VarA_ConstB(add_OperatorDto_VarA_ConstB);
            }

            var add_OperatorDto_ConstA_VarB = dto as Add_OperatorDto_ConstA_VarB;
            if (add_OperatorDto_ConstA_VarB != null)
            {
                return Visit_Add_OperatorDto_ConstA_VarB(add_OperatorDto_ConstA_VarB);
            }

            var add_OperatorDto_ConstA_ConstB = dto as Add_OperatorDto_ConstA_ConstB;
            if (add_OperatorDto_ConstA_ConstB != null)
            {
                return Visit_Add_OperatorDto_ConstA_ConstB(add_OperatorDto_ConstA_ConstB);
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_VarA_VarB(Add_OperatorDto_VarA_VarB dto)
        {
            return Visit_Add_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_VarA_ConstB(Add_OperatorDto_VarA_ConstB dto)
        {
            return Visit_Add_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_ConstA_VarB(Add_OperatorDto_ConstA_VarB dto)
        {
            return Visit_Add_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_ConstA_ConstB(Add_OperatorDto_ConstA_ConstB dto)
        {
            return Visit_Add_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Add_OperatorDto_Base(Add_OperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // Multiply

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_ConcreteOrPolymorphic(Multiply_OperatorDto dto)
        {
            bool isConcrete = dto.GetType() == typeof(Multiply_OperatorDto);

            if (isConcrete)
            {
                return Visit_Multiply_OperatorDto_Concrete(dto);
            }
            else
            {
                return Visit_Multiply_OperatorDto_Polymorphic(dto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_Concrete(Multiply_OperatorDto dto)
        {
            return Visit_Multiply_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_Polymorphic(Multiply_OperatorDto dto)
        {
            var multiply_OperatorDto_VarA_VarB = dto as Multiply_OperatorDto_VarA_VarB;
            if (multiply_OperatorDto_VarA_VarB != null)
            {
                return Visit_Multiply_OperatorDto_VarA_VarB(multiply_OperatorDto_VarA_VarB);
            }

            var multiply_OperatorDto_VarA_ConstB = dto as Multiply_OperatorDto_VarA_ConstB;
            if (multiply_OperatorDto_VarA_ConstB != null)
            {
                return Visit_Multiply_OperatorDto_VarA_ConstB(multiply_OperatorDto_VarA_ConstB);
            }

            var multiply_OperatorDto_ConstA_VarB = dto as Multiply_OperatorDto_ConstA_VarB;
            if (multiply_OperatorDto_ConstA_VarB != null)
            {
                return Visit_Multiply_OperatorDto_ConstA_VarB(multiply_OperatorDto_ConstA_VarB);
            }

            var multiply_OperatorDto_ConstA_ConstB = dto as Multiply_OperatorDto_ConstA_ConstB;
            if (multiply_OperatorDto_ConstA_ConstB != null)
            {
                return Visit_Multiply_OperatorDto_ConstA_ConstB(multiply_OperatorDto_ConstA_ConstB);
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            return Visit_Multiply_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            return Visit_Multiply_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto)
        {
            return Visit_Multiply_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto)
        {
            return Visit_Multiply_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Multiply_OperatorDto_Base(Multiply_OperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // Number

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_ConcreteOrPolymorphic(Number_OperatorDto dto)
        {
            bool isConcrete = dto.GetType() == typeof(Number_OperatorDto);

            if (isConcrete)
            {
                return Visit_Number_OperatorDto_Concrete(dto);
            }
            else
            {
                return Visit_Number_OperatorDto_Polymorphic(dto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_Concrete(Number_OperatorDto dto)
        {
            return Visit_Number_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_Polymorphic(Number_OperatorDto dto)
        {
            var number_OperatorDto_NaN = dto as Number_OperatorDto_NaN;
            if (number_OperatorDto_NaN != null)
            {
                return Visit_Number_OperatorDto_NaN(number_OperatorDto_NaN);
            }

            var number_OperatorDto_One = dto as Number_OperatorDto_One;
            if (number_OperatorDto_One != null)
            {
                return Visit_Number_OperatorDto_One(number_OperatorDto_One);
            }

            var number_OperatorDto_Zero = dto as Number_OperatorDto_Zero;
            if (number_OperatorDto_Zero != null)
            {
                return Visit_Number_OperatorDto_Zero(number_OperatorDto_Zero);
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            return Visit_Number_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            return Visit_Number_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            return Visit_Number_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Number_OperatorDto_Base(Number_OperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // Shift

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_ConcreteOrPolymorphic(Shift_OperatorDto dto)
        {
            bool isConcrete = dto.GetType() == typeof(Shift_OperatorDto);

            if (isConcrete)
            {
                return Visit_Shift_OperatorDto_Concrete(dto);
            }
            else
            {
                return Visit_Shift_OperatorDto_Polymorphic(dto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_Concrete(Shift_OperatorDto dto)
        {
            return Visit_Shift_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_Polymorphic(Shift_OperatorDto dto)
        {
            var shift_OperatorDto_VarSignal_VarDistance = dto as Shift_OperatorDto_VarSignal_VarDistance;
            if (shift_OperatorDto_VarSignal_VarDistance != null)
            {
                return Visit_Shift_OperatorDto_VarSignal_VarDistance(shift_OperatorDto_VarSignal_VarDistance);
            }

            var shift_OperatorDto_VarSignal_ConstDistance = dto as Shift_OperatorDto_VarSignal_ConstDistance;
            if (shift_OperatorDto_VarSignal_ConstDistance != null)
            {
                return Visit_Shift_OperatorDto_VarSignal_ConstDistance(shift_OperatorDto_VarSignal_ConstDistance);
            }

            var shift_OperatorDto_ConstSignal_VarDistance = dto as Shift_OperatorDto_ConstSignal_VarDistance;
            if (shift_OperatorDto_ConstSignal_VarDistance != null)
            {
                return Visit_Shift_OperatorDto_ConstSignal_VarDistance(shift_OperatorDto_ConstSignal_VarDistance);
            }

            var shift_OperatorDto_ConstSignal_ConstDistance = dto as Shift_OperatorDto_ConstSignal_ConstDistance;
            if (shift_OperatorDto_ConstSignal_ConstDistance != null)
            {
                return Visit_Shift_OperatorDto_ConstSignal_ConstDistance(shift_OperatorDto_ConstSignal_ConstDistance);
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            return Visit_Shift_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            return Visit_Shift_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            return Visit_Shift_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            return Visit_Shift_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Shift_OperatorDto_Base(Shift_OperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // Sine

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_ConcreteOrPolymorphic(Sine_OperatorDto dto)
        {
            bool isConcrete = dto.GetType() == typeof(Sine_OperatorDto);

            if (isConcrete)
            {
                return Visit_Sine_OperatorDto_Concrete(dto);
            }
            else
            {
                return Visit_Sine_OperatorDto_Polymorphic(dto);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_Concrete(Sine_OperatorDto dto)
        {
            return Visit_Sine_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_Polymorphic(Sine_OperatorDto dto)
        {
            var sine_OperatorDto_ConstFrequency_NoOriginShifting = dto as Sine_OperatorDto_ConstFrequency_NoOriginShifting;
            if (sine_OperatorDto_ConstFrequency_NoOriginShifting != null)
            {
                return Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(sine_OperatorDto_ConstFrequency_NoOriginShifting);
            }

            var sine_OperatorDto_VarFrequency_NoPhaseTracking = dto as Sine_OperatorDto_VarFrequency_NoPhaseTracking;
            if (sine_OperatorDto_VarFrequency_NoPhaseTracking != null)
            {
                return Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(sine_OperatorDto_VarFrequency_NoPhaseTracking);
            }

            var sine_OperatorDto_VarFrequency_WithPhaseTracking = dto as Sine_OperatorDto_VarFrequency_WithPhaseTracking;
            if (sine_OperatorDto_VarFrequency_WithPhaseTracking != null)
            {
                return Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(sine_OperatorDto_VarFrequency_WithPhaseTracking);
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Visit_Sine_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Visit_Sine_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Visit_Sine_OperatorDto_Base(dto);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_Sine_OperatorDto_Base(Sine_OperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // VariableInput

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // Base

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDto Visit_OperatorDto_Base(OperatorDto dto)
        {
            dto.InletDtos = VisitInletDtos(dto.InletDtos);

            return dto;
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
        protected virtual OperatorDto VisitInputOperatorDto(OperatorDto dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }
    }
}
