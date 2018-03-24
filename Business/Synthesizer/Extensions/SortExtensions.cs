using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class SortExtensions
	{
		public static IEnumerable<T> Sort<T>(this IEnumerable<T> list)
			where T : IInletOrOutlet
		{
			if (list == null) throw new NullException(() => list);

			return list.Sort(
				x => x.Position,
				x => x.IsRepeating,
				x => x.RepetitionPosition,
				x => x.GetDimensionEnum(),
				x => x.Name,
				x => x.IsObsolete);
		}

		internal static IEnumerable<VariableInput_OperatorCalculator> Sort(this IEnumerable<VariableInput_OperatorCalculator> list)
		{
			if (list == null) throw new NullException(() => list);

			return list.Sort(
				x => x.Position,
				null,
				null,
				x => x.DimensionEnum,
				x => x.CanonicalName,
				null);
		}

		internal static IEnumerable<InputVariableInfo> Sort(this IEnumerable<InputVariableInfo> list)
		{
			if (list == null) throw new NullException(() => list);

			return list.Sort(
				x => x.Position,
				null,
				null,
				x => x.DimensionEnum,
				x => x.VariableNameCamelCase,
				null);
		}

		/// <param name="getIsObsoleteDelegate">nullable</param>
		/// <param name="getIsRepeating">nullable</param>
		/// <param name="getRepetitionPosition">nullable</param>
		public static IEnumerable<T> Sort<T>(
			this IEnumerable<T> list,
			Func<T, int> getPositionDelegate,
			Func<T, bool> getIsRepeating,
			Func<T, int?> getRepetitionPosition,
			Func<T, DimensionEnum> getDimensionEnumDelegate,
			Func<T, string> getNameDelegate,
			Func<T, bool> getIsObsoleteDelegate)
		{
			if (list == null) throw new NullException(() => list);
			if (getDimensionEnumDelegate == null) throw new NullException(() => getDimensionEnumDelegate);
			if (getNameDelegate == null) throw new NullException(() => getNameDelegate);

			// Position
			IOrderedEnumerable<T> enumerable = list.OrderBy(getPositionDelegate);

			// IsRepeating
			if (getIsRepeating != null)
			{
				enumerable = enumerable.ThenBy(getIsRepeating);
			}

			// RepetitionPosition
			if (getRepetitionPosition != null)
			{
				enumerable = enumerable.ThenBy(x => getRepetitionPosition(x) == null)
									   .ThenBy(getRepetitionPosition);
			}

			// Dimension, Name
			enumerable = enumerable.ThenBy(x => getDimensionEnumDelegate(x) == DimensionEnum.Undefined)
								   .ThenBy(getDimensionEnumDelegate)
								   .ThenBy(x => string.IsNullOrWhiteSpace(getNameDelegate(x)))
								   .ThenBy(x => getNameDelegate);

			// Obsolete
			if (getIsObsoleteDelegate != null)
			{
				enumerable = enumerable.ThenBy(getIsObsoleteDelegate);
			}

			return enumerable;
		}
	}
}
