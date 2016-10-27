using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors
{
    internal class StackLevel_OperatorDtoVisitor_PerDimensionWriter : OperatorDtoVisitorBase
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

        public void Execute(OperatorDto dto)
        {
            _currentStackLevel = 0;

            Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDto Visit_OperatorDto_Polymorphic(OperatorDto dto)
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
