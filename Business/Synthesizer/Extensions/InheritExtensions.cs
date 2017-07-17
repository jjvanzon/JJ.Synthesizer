using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;

namespace JJ.Business.Synthesizer.Extensions
{
    internal static class InheritExtensions
    {
        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
        public static string GetCustomDimensionNameWithFallback(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            if (op.GetStandardDimensionEnum() == DimensionEnum.Inherit)
            {
                return op.Patch.DefaultCustomDimensionName;
            }

            return op.CustomDimensionName;
        }

        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
        public static Dimension GetStandardDimensionWithFallback(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            Dimension dimension = op.StandardDimension;
            if (dimension.ToEnum() == DimensionEnum.Inherit)
            {
                return op.Patch.DefaultStandardDimension;
            }

            return dimension;
        }

        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
        public static DimensionEnum GetStandardDimensionEnumWithFallback(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            Dimension dimension = op.GetStandardDimensionWithFallback();
            return dimension.ToEnum();
        }

        /// <summary>In case dimension is 'Inherit', the name is resolved from context.</summary>
        public static string GetNameWithFallback(this IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new ArgumentNullException(nameof(inletOrOutlet));

            // TODO: Low priority: It is a little strange that if dimension is inherit, then name is inherited.

            if (inletOrOutlet.GetDimensionEnum() == DimensionEnum.Inherit)
            {
                return inletOrOutlet.Operator.GetCustomDimensionNameWithFallback();
            }

            return inletOrOutlet.Name;
        }

        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
        public static Dimension GetDimensionWithFallback(this IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new ArgumentNullException(nameof(inletOrOutlet));

            Dimension dimension = inletOrOutlet.Dimension;
            if (dimension.ToEnum() == DimensionEnum.Inherit)
            {
                return inletOrOutlet.Operator.GetStandardDimensionWithFallback();
            }

            return dimension;
        }

        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
        public static DimensionEnum GetDimensionEnumWithFallback(this IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new ArgumentNullException(nameof(inletOrOutlet));

            Dimension dimension = inletOrOutlet.GetDimensionWithFallback();

            return dimension.ToEnum();
        }
    }
}
