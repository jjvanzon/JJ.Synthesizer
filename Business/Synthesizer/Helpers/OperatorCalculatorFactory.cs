using System;
using System.Collections.Generic;
using System.Reflection;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Helpers
{
	/// <summary>
	/// For when creating an OperatorCalculator based on criteria is used
	/// in more places than just the OptimizedPatchCalculatorVisitor.
	/// </summary>
	internal static class OperatorCalculatorFactory
	{
		public static OperatorCalculatorBase Create_Cache_OperatorCalculator(
			IList<ICalculatorWithPosition> arrayCalculators,
			VariableInput_OperatorCalculator dimensionCalculator,
			VariableInput_OperatorCalculator channelDimensionCalculator)
		{
			if (arrayCalculators.Count == 1)
			{
				ICalculatorWithPosition arrayCalculator = arrayCalculators[0];
				OperatorCalculatorBase calculator = Create_Cache_OperatorCalculator_SingleChannel(arrayCalculator, dimensionCalculator);
				return calculator;
			}
			else
			{
				OperatorCalculatorBase calculator = Create_Cache_OperatorCalculator_MultiChannel(arrayCalculators, dimensionCalculator, channelDimensionCalculator);
				return calculator;
			}
		}

		public static OperatorCalculatorBase Create_Cache_OperatorCalculator_MultiChannel(
			IList<ICalculatorWithPosition> arrayCalculators,
			VariableInput_OperatorCalculator dimensionCalculator,
			VariableInput_OperatorCalculator channelDimensionCalculator)
		{
			Type arrayCalculatorType = arrayCalculators.GetItemType();
			Type cache_OperatorCalculator_Type = typeof(Cache_OperatorCalculator_MultiChannel<>).MakeGenericType(arrayCalculatorType);

			var calculator = (OperatorCalculatorBase)Activator.CreateInstance(
				cache_OperatorCalculator_Type,
				arrayCalculators,
				dimensionCalculator,
				channelDimensionCalculator);
			return calculator;
		}

		public static OperatorCalculatorBase Create_Cache_OperatorCalculator_SingleChannel(
			ICalculatorWithPosition arrayCalculator,
			VariableInput_OperatorCalculator dimensionCalculator)
		{
			Type arrayCalculatorType = arrayCalculator.GetType();
			Type cache_OperatorCalculator_Type = typeof(Cache_OperatorCalculator_SingleChannel<>).MakeGenericType(arrayCalculatorType);
			var calculator = (OperatorCalculatorBase)Activator.CreateInstance(cache_OperatorCalculator_Type, arrayCalculator, dimensionCalculator);
			return calculator;
		}

		/// <param name="arrayDto">nullable</param>
		public static OperatorCalculatorBase Create_Curve_OperatorCalculator(
			OperatorCalculatorBase positionCalculator,
			ArrayDto arrayDto,
			DimensionEnum standardDimensionEnum)
		{
			if (positionCalculator == null) throw new ArgumentNullException(nameof(positionCalculator));

			if (arrayDto == null)
			{
				return new Number_OperatorCalculator(0);
			}

			ICalculatorWithPosition arrayCalculator = ArrayCalculatorFactory.CreateArrayCalculator(arrayDto);

			if (arrayCalculator is ArrayCalculator_MinPosition_Line arrayCalculator_MinPosition)
			{
				if (standardDimensionEnum == DimensionEnum.Time)
				{
					return new Curve_OperatorCalculator_MinX_WithOriginShifting(positionCalculator, arrayCalculator_MinPosition);
				}
				else
				{
					return new Curve_OperatorCalculator_MinX_NoOriginShifting(positionCalculator, arrayCalculator_MinPosition);
				}
			}

			if (arrayCalculator is ArrayCalculator_MinPositionZero_Line arrayCalculator_MinPositionZero)
			{
				if (standardDimensionEnum == DimensionEnum.Time)
				{
					return new Curve_OperatorCalculator_MinXZero_WithOriginShifting(positionCalculator, arrayCalculator_MinPositionZero);
				}
				else
				{
					return new Curve_OperatorCalculator_MinXZero_NoOriginShifting(positionCalculator, arrayCalculator_MinPositionZero);
				}
			}

			throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
		}
	}
}