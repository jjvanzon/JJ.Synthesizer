using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal abstract class OperatorDtoVisitorBase
    {
        // Polymorphic

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            {
                var castedDto = dto as OperatorDtoBase_VarA_VarB;
                if (castedDto != null)
                {
                    return Visit_OperatorDtoBase_VarA_VarB_Polymorphic(castedDto);
                }
            }

            {
                var castedDto = dto as OperatorDtoBase_VarA_ConstB;
                if (castedDto != null)
                {
                    return Visit_OperatorDtoBase_VarA_ConstB_Polymorphic(castedDto);
                }
            }

            {
                var castedDto = dto as OperatorDtoBase_ConstA_VarB;
                if (castedDto != null)
                {
                    return Visit_OperatorDtoBase_ConstA_VarB_Polymorphic(castedDto);
                }
            }

            {
                var castedDto = dto as OperatorDtoBase_ConstA_ConstB;
                if (castedDto != null)
                {
                    return Visit_OperatorDtoBase_ConstA_ConstB_Polymorphic(castedDto);
                }
            }

            {
                var castedDto = dto as OperatorDtoBase_Vars;
                if (castedDto != null)
                {
                    return Visit_OperatorDtoBase_OperatorDto_Vars_Polymorphic(castedDto);
                }
            }

            {
                var castedDto = dto as OperatorDtoBase_Vars_1Const;
                if (castedDto != null)
                {
                    return Visit_OperatorDtoBase_OperatorDto_Vars_1Const_Polymorphic(castedDto);
                }
            }

            {
                var castedDto = dto as OperatorDtoBase_VarFrequency;
                if (castedDto != null)
                {
                    return Visit_OperatorDtoBase_VarFrequency_Polymorphic(castedDto);
                }
            }

            {
                var castedDto = dto as OperatorDtoBase_ConstFrequency;
                if (castedDto != null)
                {
                    return Visit_OperatorDtoBase_ConstFrequency_Polymorphic(castedDto);
                }
            }

            {
                var castedDto = dto as Number_OperatorDto;
                if (castedDto != null)
                {
                    return Visit_Number_OperatorDto_ConcreteOrPolymorphic(castedDto);
                }
            }

            {
                var castedDto = dto as VariableInput_OperatorDto;
                if (castedDto != null)
                {
                    return Visit_VariableInput_OperatorDto(castedDto);
                }
            }

            {
                var castedDto = dto as Shift_OperatorDto;
                if (castedDto != null)
                {
                    return Visit_Shift_OperatorDto(castedDto);
                }
            }

            {
                var castedDto = dto as Shift_OperatorDto_ConstSignal_ConstDistance;
                if (castedDto != null)
                {
                    return Visit_Shift_OperatorDto_ConstSignal_ConstDistance(castedDto);
                }
            }

            {
                var castedDto = dto as Shift_OperatorDto_ConstSignal_VarDistance;
                if (castedDto != null)
                {
                    return Visit_Shift_OperatorDto_ConstSignal_VarDistance(castedDto);
                }
            }

            {
                var castedDto = dto as Shift_OperatorDto_VarSignal_ConstDistance;
                if (castedDto != null)
                {
                    return Visit_Shift_OperatorDto_VarSignal_ConstDistance(castedDto);
                }
            }

            {
                var castedDto = dto as Shift_OperatorDto_VarSignal_VarDistance;
                if (castedDto != null)
                {
                    return Visit_Shift_OperatorDto_VarSignal_VarDistance(castedDto);
                }
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_VarA_VarB_Polymorphic(OperatorDtoBase_VarA_VarB dto)
        {
            {
                var castedDto = dto as Multiply_OperatorDto;
                if (castedDto != null)
                {
                    return Visit_Multiply_OperatorDto(castedDto);
                }
            }

            {
                var castedDto = dto as Multiply_OperatorDto_VarA_VarB;
                if (castedDto != null)
                {
                    return Visit_Multiply_OperatorDto_VarA_VarB(castedDto);
                }
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_VarA_VarB_Base(OperatorDtoBase_VarA_VarB dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_VarA_ConstB_Polymorphic(OperatorDtoBase_VarA_ConstB dto)
        {
            {
                var castedDto = dto as Multiply_OperatorDto_VarA_ConstB;
                if (castedDto != null)
                {
                    return Visit_Multiply_OperatorDto_VarA_ConstB(castedDto);
                }
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_VarA_ConstB_Base(OperatorDtoBase_VarA_ConstB dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_ConstA_VarB_Polymorphic(OperatorDtoBase_ConstA_VarB dto)
        {
            {
                var castedDto = dto as Multiply_OperatorDto_ConstA_VarB;
                if (castedDto != null)
                {
                    return Visit_Multiply_OperatorDto_ConstA_VarB(castedDto);
                }
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_ConstA_VarB_Base(OperatorDtoBase_ConstA_VarB dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_ConstA_ConstB_Polymorphic(OperatorDtoBase_ConstA_ConstB dto)
        {
            {
                var castedDto = dto as Multiply_OperatorDto_ConstA_ConstB;
                if (castedDto != null)
                {
                    return Visit_Multiply_OperatorDto_ConstA_ConstB(castedDto);
                }
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_ConstA_ConstB_Base(OperatorDtoBase_ConstA_ConstB dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_OperatorDto_Vars_Polymorphic(OperatorDtoBase_Vars dto)
        {
            {
                var castedDto = dto as Add_OperatorDto;
                if (castedDto != null)
                {
                    return Visit_Add_OperatorDto(castedDto);
                }
            }

            {
                var castedDto = dto as Add_OperatorDto_Vars;
                if (castedDto != null)
                {
                    return Visit_Add_OperatorDto_Vars(castedDto);
                }
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_Vars_Base(OperatorDtoBase_Vars dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_OperatorDto_Vars_1Const_Polymorphic(OperatorDtoBase_Vars_1Const dto)
        {
            {
                var castedDto = dto as Add_OperatorDto_Vars_1Const;
                if (castedDto != null)
                {
                    return Visit_Add_OperatorDto_Vars_1Const(castedDto);
                }
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_Vars_1Const_Base(OperatorDtoBase_Vars_1Const dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_VarFrequency_Polymorphic(OperatorDtoBase_VarFrequency dto)
        {
            {
                var castedDto = dto as Sine_OperatorDto;
                if (castedDto != null)
                {
                    return Visit_Sine_OperatorDto(castedDto);
                }
            }

            {
                var castedDto = dto as Sine_OperatorDto_VarFrequency_WithPhaseTracking;
                if (castedDto != null)
                {
                    return Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(castedDto);
                }
            }
            {
                var castedDto = dto as Sine_OperatorDto_VarFrequency_NoPhaseTracking;
                if (castedDto != null)
                {
                    return Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(castedDto);
                }
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_VarFrequency_Base(OperatorDtoBase_VarFrequency dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_ConstFrequency_Polymorphic(OperatorDtoBase_ConstFrequency dto)
        {
            {
                var castedDto = dto as Sine_OperatorDto_ConstFrequency_NoOriginShifting;
                if (castedDto != null)
                {
                    return Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(castedDto);
                }
            }
            {
                var castedDto = dto as Sine_OperatorDto_ConstFrequency_WithOriginShifting;
                if (castedDto != null)
                {
                    return Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(castedDto);
                }
            }

            throw new UnexpectedTypeException(() => dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDtoBase_ConstFrequency_Base(OperatorDtoBase_ConstFrequency dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // Add

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto)
        {
            return Visit_OperatorDtoBase_Vars_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Add_OperatorDto_Vars(Add_OperatorDto_Vars dto)
        {
            return Visit_OperatorDtoBase_Vars_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            return Visit_OperatorDtoBase_Vars_1Const_Base(dto);
        }

        // Multiply

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Multiply_OperatorDto(Multiply_OperatorDto dto)
        {
            return Visit_OperatorDtoBase_VarA_VarB_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            return Visit_OperatorDtoBase_VarA_VarB_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            return Visit_OperatorDtoBase_VarA_ConstB_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto)
        {
            return Visit_OperatorDtoBase_ConstA_VarB_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto)
        {
            return Visit_OperatorDtoBase_ConstA_ConstB_Base(dto);
        }

        // Number

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Number_OperatorDto_ConcreteOrPolymorphic(Number_OperatorDto dto)
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

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Number_OperatorDto_Concrete(Number_OperatorDto dto)
        {
            return Visit_Number_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Number_OperatorDto_Polymorphic(Number_OperatorDto dto)
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

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            return Visit_Number_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            return Visit_Number_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            return Visit_Number_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Number_OperatorDto_Base(Number_OperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // Shift

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Shift_OperatorDto(Shift_OperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // Sine

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Sine_OperatorDto(Sine_OperatorDto dto)
        {
            return Visit_OperatorDtoBase_VarFrequency_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            return Visit_OperatorDtoBase_VarFrequency_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            return Visit_OperatorDtoBase_VarFrequency_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            return Visit_OperatorDtoBase_ConstFrequency_Base(dto);
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto)
        {
            return Visit_OperatorDtoBase_ConstFrequency_Base(dto);
        }

        // VariableInput

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            return Visit_OperatorDto_Base(dto);
        }

        // Base

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDto_Base(OperatorDtoBase dto)
        {
            dto.InputOperatorDtos = VisitInputOperatorDtos(dto.InputOperatorDtos);

            return dto;
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual IList<OperatorDtoBase> VisitInputOperatorDtos(IList<OperatorDtoBase> operatorDtos)
        {
            for (int i = 0; i < operatorDtos.Count; i++)
            {
                OperatorDtoBase operatorDto = operatorDtos[i];
                operatorDtos[i] = VisitOperatorDto(operatorDto);
            }

            return operatorDtos;
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase VisitOperatorDto(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }
    }
}
