using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_StackLevel : OperatorDtoVisitorBase
    {
        private readonly HashSet<Type> _dimensionWriting_OperatorDto_Types = new HashSet<Type>
        {
            // Even though this collection contains entries that will never be in the input DTO structure,
            // because previous preprocessings will already have replaced them by others,
            // it does not cost us any significant processing power to leave them in.
            // It is safer to just keep all the DTO types of certain OperatorTypes in here.
            // You could only forget ones, and if you would use this visitor in a different way,
            // at least it will give a result that makes sense.
            typeof(Loop_OperatorDto),
            typeof(Loop_OperatorDto_ConstSignal),
            typeof(Loop_OperatorDto_NoSkipOrRelease_ManyConstants),
            typeof(Loop_OperatorDto_ManyConstants),
            typeof(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration),
            typeof(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration),
            typeof(Loop_OperatorDto_NoSkipOrRelease),
            typeof(Loop_OperatorDto_AllVars),
            typeof(Reverse_OperatorDto),
            typeof(Reverse_OperatorDto_ConstSignal),
            typeof(Reverse_OperatorDto_VarSpeed_WithPhaseTracking),
            typeof(Reverse_OperatorDto_VarSpeed_NoPhaseTracking),
            typeof(Reverse_OperatorDtoBase_VarSpeed),
            typeof(Reverse_OperatorDto_ConstSpeed_WithOriginShifting),
            typeof(Reverse_OperatorDto_ConstSpeed_NoOriginShifting),
            typeof(Select_OperatorDto),
            typeof(Select_OperatorDto_VarSignal_VarPosition),
            typeof(Select_OperatorDto_VarSignal_ConstPosition),
            typeof(Select_OperatorDto_ConstSignal_VarPosition),
            typeof(Select_OperatorDto_ConstSignal_ConstPosition),
            typeof(SetDimension_OperatorDto),
            typeof(SetDimension_OperatorDto_VarValue),
            typeof(SetDimension_OperatorDto_ConstValue),
            typeof(Shift_OperatorDto),
            typeof(Shift_OperatorDto_ConstSignal_ConstDistance),
            typeof(Shift_OperatorDto_ConstSignal_VarDistance),
            typeof(Shift_OperatorDto_VarSignal_ConstDistance),
            typeof(Shift_OperatorDto_VarSignal_VarDistance),
            typeof(Squash_OperatorDto),
            typeof(Squash_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin),
            typeof(Squash_OperatorDto_ConstSignal_VarFactor_ZeroOrigin),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin),
            typeof(Squash_OperatorDto_ConstSignal_ConstFactor_ConstOrigin),
            typeof(Squash_OperatorDto_ConstSignal_ConstFactor_VarOrigin),
            typeof(Squash_OperatorDto_ConstSignal_VarFactor_ConstOrigin),
            typeof(Squash_OperatorDto_ConstSignal_VarFactor_VarOrigin),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin),
            typeof(Squash_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting),
            typeof(Squash_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking),
            typeof(Stretch_OperatorDto),
            typeof(Stretch_OperatorDto_ConstSignal_ConstFactor_ZeroOrigin),
            typeof(Stretch_OperatorDto_ConstSignal_VarFactor_ZeroOrigin),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin),
            typeof(Stretch_OperatorDto_ConstSignal_ConstFactor_ConstOrigin),
            typeof(Stretch_OperatorDto_ConstSignal_ConstFactor_VarOrigin),
            typeof(Stretch_OperatorDto_ConstSignal_VarFactor_ConstOrigin),
            typeof(Stretch_OperatorDto_ConstSignal_VarFactor_VarOrigin),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin),
            typeof(Stretch_OperatorDto_ConstSignal_ConstFactor_WithOriginShifting),
            typeof(Stretch_OperatorDto_ConstSignal_VarFactor_WithPhaseTracking),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking),
            typeof(TimePower_OperatorDto),
            typeof(TimePower_OperatorDto_ConstSignal),
            typeof(TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin),
            typeof(TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin)
        };

        private int _currentStackLevel;

        public void Execute(OperatorDtoBase dto)
        {
            _currentStackLevel = 0;

            Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            dto.DimensionStackLevel = _currentStackLevel;

            Type dtoType = dto.GetType();

            bool isDimensionWriter = _dimensionWriting_OperatorDto_Types.Contains(dtoType);

            if (isDimensionWriter)
            {
                _currentStackLevel++;
            }

            base.Visit_OperatorDto_Polymorphic(dto);

            if (isDimensionWriter)
            {
                _currentStackLevel--;
            }

            return dto;
        }
    }
}
