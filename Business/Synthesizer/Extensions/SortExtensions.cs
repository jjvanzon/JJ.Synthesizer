using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class SortExtensions
    {
        public static IEnumerable<Inlet> Sort([NotNull] this IEnumerable<Inlet> list)
        {
            if (list == null) throw new NullException(() => list);

            return list.Sort(
                x => x.Position,
                x => x.GetDimensionEnum(),
                x => x.Name,
                x => x.IsObsolete);
        }

        public static IEnumerable<Outlet> Sort(this IEnumerable<Outlet> list)
        {
            if (list == null) throw new NullException(() => list);

            return list.Sort(
                x => x.Position,
                x => x.GetDimensionEnum(),
                x => x.Name,
                x => x.IsObsolete);
        }

        internal static IEnumerable<VariableInput_OperatorCalculator> Sort([NotNull] this IEnumerable<VariableInput_OperatorCalculator> list)
        {
            if (list == null) throw new NullException(() => list);

            return list.Sort(
                x => x.Position,
                x => x.DimensionEnum,
                x => x.CanonicalName);
        }

        internal static IEnumerable<ExtendedVariableInfo> Sort([NotNull] this IEnumerable<ExtendedVariableInfo> list)
        {
            if (list == null) throw new NullException(() => list);

            return list.Sort(
                x => x.Position,
                x => x.DimensionEnum,
                x => x.VariableNameCamelCase);
        }

        public static IEnumerable<T> Sort<T>(
            this IEnumerable<T> list,
            Func<T, int> getPositionDelegate,
            Func<T, DimensionEnum> getDimensionEnumDelegate,
            Func<T, string> getNameDelegate,
            Func<T, bool> getIsObsoleteDelegate = null)
        {
            if (list == null) throw new NullException(() => list);
            if (getDimensionEnumDelegate == null) throw new NullException(() => getDimensionEnumDelegate);
            if (getNameDelegate == null) throw new NullException(() => getNameDelegate);

            IOrderedEnumerable<T> enumerable = list.OrderBy(getPositionDelegate)
                                                   .ThenBy(x => getDimensionEnumDelegate(x) == DimensionEnum.Undefined)
                                                   .ThenBy(getDimensionEnumDelegate)
                                                   .ThenBy(x => string.IsNullOrWhiteSpace(getNameDelegate(x)))
                                                   .ThenBy(x => getNameDelegate);
            if (getIsObsoleteDelegate != null)
            {
                enumerable = enumerable.ThenBy(getIsObsoleteDelegate);
            }

            return enumerable;
        }
    }
}
