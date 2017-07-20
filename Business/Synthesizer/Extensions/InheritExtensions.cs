using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class InheritExtensions
    {
        // Operator

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

            if (op.GetStandardDimensionEnum() == DimensionEnum.Inherit)
            {
                return op.Patch.DefaultStandardDimension;
            }

            return op.StandardDimension;
        }

        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
        public static DimensionEnum GetStandardDimensionEnumWithFallback(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            Dimension dimension = op.GetStandardDimensionWithFallback();

            return dimension?.ToEnum() ?? DimensionEnum.Undefined;
        }

        public static bool DimensionIsInherited(this Operator op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            return op.GetStandardDimensionEnum() == DimensionEnum.Inherit;
        }

        // Inlet / Outlet

        /// <summary>In case dimension is 'Inherit', the name is resolved from context.</summary>
        public static string GetNameWithFallback(this IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new ArgumentNullException(nameof(inletOrOutlet));

            OperatorTypeEnum operatorTypeEnum = inletOrOutlet.Operator.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.PatchInlet:
                case OperatorTypeEnum.PatchOutlet:
                {
                    if (inletOrOutlet.GetDimensionEnum() == DimensionEnum.Inherit)
                    {
                        // PatchInlet Inlets and Outlets inherit their dimension from the patch, not operator.
                        return inletOrOutlet.Operator.Patch.DefaultCustomDimensionName;
                    }
                    break;
                }

                default:
                {
                    if (inletOrOutlet.GetDimensionEnum() == DimensionEnum.Inherit)
                    {
                        return inletOrOutlet.Operator.GetCustomDimensionNameWithFallback();
                    }
                    break;
                }
            }

            return inletOrOutlet.Name;
        }

        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
        public static Dimension GetDimensionWithFallback(this IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new ArgumentNullException(nameof(inletOrOutlet));

            OperatorTypeEnum operatorTypeEnum = inletOrOutlet.Operator.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.PatchInlet:
                case OperatorTypeEnum.PatchOutlet:
                {
                    if (inletOrOutlet.GetDimensionEnum() == DimensionEnum.Inherit)
                    {
                        // PatchInlet Inlets and Outlets inherit their dimension from the patch, not operator.
                        return inletOrOutlet.Operator.Patch.DefaultStandardDimension;
                    }
                    break;
                }

                default:
                {
                    if (inletOrOutlet.GetDimensionEnum() == DimensionEnum.Inherit)
                    {
                        return inletOrOutlet.Operator.GetStandardDimensionWithFallback();
                    }
                    break;
                }
            }

            return inletOrOutlet.Dimension;
        }

        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
        public static DimensionEnum GetDimensionEnumWithFallback(this IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new ArgumentNullException(nameof(inletOrOutlet));

            Dimension dimension = inletOrOutlet.GetDimensionWithFallback();

            return dimension?.ToEnum() ?? DimensionEnum.Undefined;
        }

        public static bool DimensionIsInherited(this IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new ArgumentNullException(nameof(inletOrOutlet));
            return inletOrOutlet.GetDimensionEnum() == DimensionEnum.Inherit;
        }
    }
}
