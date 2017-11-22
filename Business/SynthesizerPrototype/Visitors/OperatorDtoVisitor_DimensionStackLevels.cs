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

		public void Execute(IOperatorDto dto)
		{
			_currentStackLevel = 0;

			Visit_OperatorDto_Polymorphic(dto);
		}

		protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
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

			foreach (IOperatorDto inputOperatorDto in dto.InputOperatorDtos.Except(castedOperatorDto.SignalOperatorDto))
			{
				Visit_OperatorDto_Polymorphic(inputOperatorDto);
			}

			// Only behind the signal inlet the dimension stack level increases.
			_currentStackLevel++;

			Visit_OperatorDto_Polymorphic(castedOperatorDto.SignalOperatorDto);

			_currentStackLevel--;

			return dto;
		}
	}
}