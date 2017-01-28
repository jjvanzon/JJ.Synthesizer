using System;
using System.Collections.Generic;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.Visitors
{
    internal class OperatorDtoVisitor_DimensionStackLevels : OperatorDtoVisitorBase
    {
        private readonly HashSet<Type> _dimensionWriting_OperatorDto_Types = new HashSet<Type>
        {
            typeof(Shift_OperatorDto),
            typeof(Shift_OperatorDto_ConstSignal_ConstDistance),
            typeof(Shift_OperatorDto_ConstSignal_VarDistance),
            typeof(Shift_OperatorDto_VarSignal_ConstDistance),
            typeof(Shift_OperatorDto_VarSignal_VarDistance)
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
            if (!isDimensionWriter)
            {
                return base.Visit_OperatorDto_Polymorphic(dto);
            }

            var castedOperatorDto = dto as IOperatorDto_VarSignal;
            if (castedOperatorDto == null)
            {
                throw new IsNotTypeException<IOperatorDto_VarSignal>(() => castedOperatorDto);
            }

            foreach (OperatorDtoBase inputOperatorDto in dto.InputOperatorDtos.Except(castedOperatorDto.SignalOperatorDto))
            {
                Visit_OperatorDto_Polymorphic(inputOperatorDto);
            }

            // Only behind the signal inlet the dimension stack level increases.
            if (isDimensionWriter)
            {
                _currentStackLevel++;
            }

            Visit_OperatorDto_Polymorphic(castedOperatorDto.SignalOperatorDto);

            if (isDimensionWriter)
            {
                _currentStackLevel--;
            }

            return dto;
        }
    }
}