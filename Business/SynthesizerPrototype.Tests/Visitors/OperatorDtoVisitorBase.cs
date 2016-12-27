using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.Tests.Dto;

namespace JJ.Business.SynthesizerPrototype.Tests.Visitors
{
    internal abstract class OperatorDtoVisitorBase
    {
        private readonly Dictionary<Type, Func<OperatorDtoBase, OperatorDtoBase>> _delegateDictionary;

        [DebuggerHidden]
        protected virtual OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            Type type = dto.GetType();

            Func<OperatorDtoBase, OperatorDtoBase> func;
            if (!_delegateDictionary.TryGetValue(type, out func))
            {
                throw new Exception($"No Visit method delegate found in the dictionary for {type.Name}.");
            }

            OperatorDtoBase dto2 = func(dto);

            // Revisit as long as different instances keep coming.
            while (dto2 != dto)
            {
                dto = dto2;
                dto2 = Visit_OperatorDto_Polymorphic(dto);
            }

            return dto2;
        }

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual OperatorDtoBase Visit_OperatorDto_Base(OperatorDtoBase dto)
        {
            dto.InputOperatorDtos = VisitInputOperatorDtos(dto.InputOperatorDtos);

            return dto;
        }

        [DebuggerHidden]
        protected virtual IList<OperatorDtoBase> VisitInputOperatorDtos(IList<OperatorDtoBase> operatorDtos)
        {
            // Reverse the order, so calculators pop off a stack in the right order.
            for (int i = operatorDtos.Count - 1; i >= 0; i--)
            {
                OperatorDtoBase operatorDto = operatorDtos[i];
                operatorDtos[i] = Visit_OperatorDto_Polymorphic(operatorDto);
            }

            return operatorDtos;
        }

        public OperatorDtoVisitorBase()
        {
            _delegateDictionary = new Dictionary<Type, Func<OperatorDtoBase, OperatorDtoBase>>
            {
                { typeof(Add_OperatorDto), x => Visit_Add_OperatorDto((Add_OperatorDto)x ) },
                { typeof(Add_OperatorDto_Vars_Consts), x => Visit_Add_OperatorDto_Vars_Consts((Add_OperatorDto_Vars_Consts)x ) },
                { typeof(Add_OperatorDto_Vars_NoConsts), x => Visit_Add_OperatorDto_Vars_NoConsts((Add_OperatorDto_Vars_NoConsts)x ) },
                { typeof(Add_OperatorDto_NoVars_Consts), x => Visit_Add_OperatorDto_NoVars_Consts((Add_OperatorDto_NoVars_Consts)x ) },
                { typeof(Add_OperatorDto_NoVars_NoConsts), x => Visit_Add_OperatorDto_NoVars_NoConsts((Add_OperatorDto_NoVars_NoConsts)x ) },
                { typeof(Add_OperatorDto_Vars_1Const), x => Visit_Add_OperatorDto_Vars_1Const((Add_OperatorDto_Vars_1Const)x ) },
                { typeof(Multiply_OperatorDto), x => Visit_Multiply_OperatorDto((Multiply_OperatorDto)x ) },
                { typeof(Multiply_OperatorDto_VarA_VarB), x => Visit_Multiply_OperatorDto_VarA_VarB((Multiply_OperatorDto_VarA_VarB)x ) },
                { typeof(Multiply_OperatorDto_VarA_ConstB), x => Visit_Multiply_OperatorDto_VarA_ConstB((Multiply_OperatorDto_VarA_ConstB)x ) },
                { typeof(Multiply_OperatorDto_ConstA_VarB), x => Visit_Multiply_OperatorDto_ConstA_VarB((Multiply_OperatorDto_ConstA_VarB)x ) },
                { typeof(Multiply_OperatorDto_ConstA_ConstB), x => Visit_Multiply_OperatorDto_ConstA_ConstB((Multiply_OperatorDto_ConstA_ConstB)x ) },
                { typeof(Number_OperatorDto), x => Visit_Number_OperatorDto((Number_OperatorDto)x ) },
                { typeof(Number_OperatorDto_NaN), x => Visit_Number_OperatorDto_NaN((Number_OperatorDto_NaN)x ) },
                { typeof(Number_OperatorDto_One), x => Visit_Number_OperatorDto_One((Number_OperatorDto_One)x ) },
                { typeof(Number_OperatorDto_Zero), x => Visit_Number_OperatorDto_Zero((Number_OperatorDto_Zero)x ) },
                { typeof(Shift_OperatorDto), x => Visit_Shift_OperatorDto((Shift_OperatorDto)x ) },
                { typeof(Shift_OperatorDto_ConstSignal_ConstDistance), x => Visit_Shift_OperatorDto_ConstSignal_ConstDistance((Shift_OperatorDto_ConstSignal_ConstDistance)x ) },
                { typeof(Shift_OperatorDto_ConstSignal_VarDistance), x => Visit_Shift_OperatorDto_ConstSignal_VarDistance((Shift_OperatorDto_ConstSignal_VarDistance)x ) },
                { typeof(Shift_OperatorDto_VarSignal_ConstDistance), x => Visit_Shift_OperatorDto_VarSignal_ConstDistance((Shift_OperatorDto_VarSignal_ConstDistance)x ) },
                { typeof(Shift_OperatorDto_VarSignal_VarDistance), x => Visit_Shift_OperatorDto_VarSignal_VarDistance((Shift_OperatorDto_VarSignal_VarDistance)x ) },
                { typeof(Sine_OperatorDto), x => Visit_Sine_OperatorDto((Sine_OperatorDto)x ) },
                { typeof(Sine_OperatorDto_ZeroFrequency), x => Visit_Sine_OperatorDto_ZeroFrequency((Sine_OperatorDto_ZeroFrequency)x ) },
                { typeof(Sine_OperatorDto_ConstFrequency_NoOriginShifting), x => Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting((Sine_OperatorDto_ConstFrequency_NoOriginShifting)x ) },
                { typeof(Sine_OperatorDto_ConstFrequency_WithOriginShifting), x => Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting((Sine_OperatorDto_ConstFrequency_WithOriginShifting)x ) },
                { typeof(Sine_OperatorDto_VarFrequency_NoPhaseTracking), x => Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking((Sine_OperatorDto_VarFrequency_NoPhaseTracking)x ) },
                { typeof(Sine_OperatorDto_VarFrequency_WithPhaseTracking), x => Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking((Sine_OperatorDto_VarFrequency_WithPhaseTracking)x ) },
                { typeof(VariableInput_OperatorDto), x => Visit_VariableInput_OperatorDto((VariableInput_OperatorDto)x) }
            };
        }

        [DebuggerHidden]
        protected virtual OperatorDtoBase VisitOperatorDto(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Add_OperatorDto(Add_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Add_OperatorDto_Vars_Consts(Add_OperatorDto_Vars_Consts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Add_OperatorDto_NoVars_Consts(Add_OperatorDto_NoVars_Consts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Add_OperatorDto_NoVars_NoConsts(Add_OperatorDto_NoVars_NoConsts dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Multiply_OperatorDto(Multiply_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_VarB(Multiply_OperatorDto_ConstA_VarB dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Multiply_OperatorDto_ConstA_ConstB(Multiply_OperatorDto_ConstA_ConstB dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Number_OperatorDto(Number_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Number_OperatorDto_One(Number_OperatorDto_One dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Shift_OperatorDto(Shift_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_VarDistance(Shift_OperatorDto_ConstSignal_VarDistance dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Shift_OperatorDto_ConstSignal_ConstDistance(Shift_OperatorDto_ConstSignal_ConstDistance dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Sine_OperatorDto(Sine_OperatorDto dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Sine_OperatorDto_ZeroFrequency(Sine_OperatorDto_ZeroFrequency dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_WithOriginShifting(Sine_OperatorDto_ConstFrequency_WithOriginShifting dto) => Visit_OperatorDto_Base(dto);
        [DebuggerHidden] protected virtual OperatorDtoBase Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto) => Visit_OperatorDto_Base(dto);
    }
}
