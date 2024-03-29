﻿//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Framework.Collections;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Visitors
//{
//    internal class OperatorDtoVisitor_DimensionStackLevels : OperatorDtoVisitorBase
//    {
//        private Dictionary<(DimensionEnum, string), int> _dimensionToCurrentStackLevelDictionary;

//        public void Execute(IOperatorDto dto)
//        {
//            _dimensionToCurrentStackLevelDictionary = new Dictionary<(DimensionEnum, string), int>();

//            Visit_OperatorDto_Polymorphic(dto);
//        }

//        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
//        {
//            // Set DimensionStackLevel for both dimension readers and dimension writers.
//            var operatorDto_WithDimension = dto as IOperatorDto_WithDimension;
//            if (operatorDto_WithDimension != null)
//            {
//                operatorDto_WithDimension.DimensionStackLevel = GetCurrentStackLevel(operatorDto_WithDimension.StandardDimensionEnum, operatorDto_WithDimension.CanonicalCustomDimensionName);
//            }

//            if (dto is IOperatorDto_WithAdditionalChannelDimension operatorDto_WithAdditionalChannelDimension)
//            {
//                operatorDto_WithAdditionalChannelDimension.ChannelDimensionStackLevel = GetCurrentStackLevel(DimensionEnum.Channel, "");
//            }

//            // Determine whether dto is a dimension writer. If not, continue visiting.
//            bool isDimensionWriter = dto is IOperatorDto_WithPositionOutput;
//            if (!isDimensionWriter)
//            {
//                return base.Visit_OperatorDto_Polymorphic(dto);
//            }

//            // Do some casts and type checks
//            var operatorDto_WithSignal_WithDimension = dto as IOperatorDto_WithSignal_WithDimension;
//            if (operatorDto_WithSignal_WithDimension == null)
//            {
//                throw new IsNotTypeException<IOperatorDto_WithSignal_WithDimension>(() => operatorDto_WithSignal_WithDimension);
//            }

//            // Visit non-signal inlets normally, because for those the dimension stack level does not increase.
//            foreach (IOperatorDto inputOperatorDto in dto.Inputs
//                                                         .Except(operatorDto_WithSignal_WithDimension.Signal)
//                                                         .Where(x => x.IsVar)
//                                                         .Select(x => x.Var))
//            {
//                Visit_OperatorDto_Polymorphic(inputOperatorDto);
//            }

//            // Only behind the signal inlet the dimension stack level increases.
//            if (operatorDto_WithSignal_WithDimension.Signal.IsVar)
//            {
//                int currentStackLevel = GetCurrentStackLevel(operatorDto_WithDimension.StandardDimensionEnum, operatorDto_WithDimension.CanonicalCustomDimensionName);

//                currentStackLevel++;
//                SetCurrentStackLevel(operatorDto_WithDimension.StandardDimensionEnum, operatorDto_WithDimension.CanonicalCustomDimensionName, currentStackLevel);

//                Visit_OperatorDto_Polymorphic(operatorDto_WithSignal_WithDimension.Signal.Var);

//                currentStackLevel--;
//                SetCurrentStackLevel(operatorDto_WithDimension.StandardDimensionEnum, operatorDto_WithDimension.CanonicalCustomDimensionName, currentStackLevel);
//            }

//            return dto;
//        }

//        private int GetCurrentStackLevel(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
//        {
//            var key = (standardDimensionEnum, canonicalCustomDimensionName);

//            if (!_dimensionToCurrentStackLevelDictionary.TryGetValue(key, out int stackLevel))
//            {
//                _dimensionToCurrentStackLevelDictionary[key] = 0;
//            }

//            return stackLevel;
//        }

//        private void SetCurrentStackLevel(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName, int value)
//        {
//            var key = (standardDimensionEnum, canonicalCustomDimensionName);

//            _dimensionToCurrentStackLevelDictionary[key] = value;
//        }
//    }
//}
