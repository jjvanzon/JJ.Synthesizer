using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors
{
    internal class OperatorDtoVisitor_StackLevel_PerDimensionWriter : OperatorDtoVisitorBase
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
