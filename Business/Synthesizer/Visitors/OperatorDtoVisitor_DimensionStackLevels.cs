using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class OperatorDtoVisitor_DimensionStackLevels : OperatorDtoVisitorBase
    {
        private readonly HashSet<Type> _dimensionWriting_OperatorDto_Types = new HashSet<Type>
        {
            typeof(Loop_OperatorDto),
            typeof(Loop_OperatorDto_NoSkipOrRelease_ManyConstants),
            typeof(Loop_OperatorDto_ManyConstants),
            typeof(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_ConstLoopEndMarker_NoNoteDuration),
            typeof(Loop_OperatorDto_ConstSkip_WhichEqualsLoopStartMarker_VarLoopEndMarker_NoNoteDuration),
            typeof(Loop_OperatorDto_NoSkipOrRelease),
            typeof(Loop_OperatorDto_AllVars),
            typeof(Reverse_OperatorDto),
            typeof(Reverse_OperatorDto_VarSpeed_WithPhaseTracking),
            typeof(Reverse_OperatorDto_VarSpeed_NoPhaseTracking),
            typeof(Reverse_OperatorDtoBase_VarSpeed),
            typeof(Reverse_OperatorDto_ConstSpeed_WithOriginShifting),
            typeof(Reverse_OperatorDto_ConstSpeed_NoOriginShifting),
            typeof(SetDimension_OperatorDto),
            typeof(SetDimension_OperatorDto_VarPassThrough_VarValue),
            typeof(SetDimension_OperatorDto_VarPassThrough_ConstValue),
            typeof(Shift_OperatorDto),
            typeof(Shift_OperatorDto_VarSignal_ConstDistance),
            typeof(Shift_OperatorDto_VarSignal_VarDistance),
            typeof(Squash_OperatorDto),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_ZeroOrigin),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_ZeroOrigin),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_ConstOrigin),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_VarOrigin),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_ConstOrigin),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_VarOrigin),
            typeof(Squash_OperatorDto_VarSignal_ConstFactor_WithOriginShifting),
            typeof(Squash_OperatorDto_VarSignal_VarFactor_WithPhaseTracking),
            typeof(Stretch_OperatorDto),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_ZeroOrigin),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_ZeroOrigin),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_ConstOrigin),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_VarOrigin),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_ConstOrigin),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_VarOrigin),
            typeof(Stretch_OperatorDto_VarSignal_ConstFactor_WithOriginShifting),
            typeof(Stretch_OperatorDto_VarSignal_VarFactor_WithPhaseTracking),
            typeof(TimePower_OperatorDto),
            typeof(TimePower_OperatorDto_VarSignal_VarExponent_VarOrigin),
            typeof(TimePower_OperatorDto_VarSignal_VarExponent_ZeroOrigin)
        };

        private Dictionary<Tuple<DimensionEnum, string>, int> _dimensionToCurrentStackLevelDictionary;

        public void Execute(OperatorDtoBase dto)
        {
            _dimensionToCurrentStackLevelDictionary = new Dictionary<Tuple<DimensionEnum, string>, int>();

            Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_OperatorDto_Polymorphic(OperatorDtoBase dto)
        {
            // Set DimensionStackLevel for both dimension readers and dimension writers.
            var operatorDto_WithDimension = dto as IOperatorDto_WithDimension;
            if (operatorDto_WithDimension != null)
            {
                dto.DimensionStackLevel = GetCurrentStackLevel(operatorDto_WithDimension.StandardDimensionEnum, operatorDto_WithDimension.CanonicalCustomDimensionName);
            }

            // Determine whether dto is a dimension writer. If not visit, normally.
            Type dtoType = dto.GetType();
            bool isDimensionWriter = _dimensionWriting_OperatorDto_Types.Contains(dtoType);
            if (!isDimensionWriter)
            {
                return base.Visit_OperatorDto_Polymorphic(dto);
            }

            // Do some casts
            var operatorDto_VarSignal = dto as IOperatorDto_VarSignal;
            if (operatorDto_VarSignal == null)
            {
                throw new IsNotTypeException<IOperatorDto_VarSignal>(() => operatorDto_VarSignal);
            }

            operatorDto_WithDimension = dto as IOperatorDto_WithDimension;
            if (operatorDto_WithDimension == null)
            {
                throw new IsNotTypeException<IOperatorDto_WithDimension>(() => operatorDto_WithDimension);
            }

            // Visit non-signal inlets normally, because for those the dimension stack level does not increase.
            foreach (OperatorDtoBase inputOperatorDto in dto.InputOperatorDtos.Except(operatorDto_VarSignal.SignalOperatorDto))
            {
                Visit_OperatorDto_Polymorphic(inputOperatorDto);
            }

            // Only behind the signal inlet the dimension stack level increases.
            int currentStackLevel = GetCurrentStackLevel(operatorDto_WithDimension.StandardDimensionEnum, operatorDto_WithDimension.CanonicalCustomDimensionName);

            currentStackLevel++;
            SetCurrentStackLevel(operatorDto_WithDimension.StandardDimensionEnum, operatorDto_WithDimension.CanonicalCustomDimensionName, currentStackLevel);

            Visit_OperatorDto_Polymorphic(operatorDto_VarSignal.SignalOperatorDto);

            currentStackLevel--;
            SetCurrentStackLevel(operatorDto_WithDimension.StandardDimensionEnum, operatorDto_WithDimension.CanonicalCustomDimensionName, currentStackLevel);

            return dto;
        }

        private int GetCurrentStackLevel(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
        {
            Tuple<DimensionEnum, string> key = new Tuple<DimensionEnum, string>(standardDimensionEnum, canonicalCustomDimensionName);

            int stackLevel;
            if (!_dimensionToCurrentStackLevelDictionary.TryGetValue(key, out stackLevel))
            {
                _dimensionToCurrentStackLevelDictionary[key] = 0;
            }

            return stackLevel;
        }

        private void SetCurrentStackLevel(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName, int value)
        {
            Tuple<DimensionEnum, string> key = new Tuple<DimensionEnum, string>(standardDimensionEnum, canonicalCustomDimensionName);

            _dimensionToCurrentStackLevelDictionary[key] = value;
        }
    }
}
