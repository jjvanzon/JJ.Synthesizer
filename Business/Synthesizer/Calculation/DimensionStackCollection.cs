using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation
{
    internal class DimensionStackCollection
    {
        private readonly Dictionary<DimensionEnum, DimensionStack> _standardDimensionEnum_To_DimensionStack_Dictionary =
                     new Dictionary<DimensionEnum, DimensionStack>();

        private readonly Dictionary<string, DimensionStack> _customDimensionName_To_DimensionStack_Dictionary =
                     new Dictionary<string, DimensionStack>();

        public DimensionStack GetDimensionStack(IOperatorDto_WithDimension dto)
        {
            if (dto == null) throw new NullException(() => dto);

            if (dto.StandardDimensionEnum != DimensionEnum.Undefined)
            {
                return GetDimensionStack(dto.StandardDimensionEnum);
            }
            else
            {
                return GetDimensionStack(dto.CustomDimensionName);
            }
        }

        public DimensionStack GetDimensionStack(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            DimensionEnum standardDimensionEnum = op.GetStandardDimensionEnum();
            if (standardDimensionEnum != DimensionEnum.Undefined)
            {
                return GetDimensionStack(standardDimensionEnum);
            }
            else
            {
                return GetDimensionStack(op.CustomDimensionName);
            }
        }

        public DimensionStack GetDimensionStack(DimensionEnum standardDimensionEnum)
        {
            DimensionStack dimensionStack;
            if (!_standardDimensionEnum_To_DimensionStack_Dictionary.TryGetValue(standardDimensionEnum, out dimensionStack))
            {
                dimensionStack = CreateDimensionStack(standardDimensionEnum);
                _standardDimensionEnum_To_DimensionStack_Dictionary[standardDimensionEnum] = dimensionStack;
            }

            return dimensionStack;
        }

        public DimensionStack GetDimensionStack(string customDimensionName)
        {
            string formattedCustomDimensionName = NameHelper.ToCanonical(customDimensionName);

            DimensionStack dimensionStack;
            if (!_customDimensionName_To_DimensionStack_Dictionary.TryGetValue(formattedCustomDimensionName, out dimensionStack))
            {
                dimensionStack = CreateDimensionStack(customDimensionName);
                _customDimensionName_To_DimensionStack_Dictionary[formattedCustomDimensionName] = dimensionStack;
            }

            return dimensionStack;
        }

        public IList<DimensionStack> GetDimensionStacks()
        {
            IList<DimensionStack> list = Enumerable.Union(
                _standardDimensionEnum_To_DimensionStack_Dictionary.Values,
                _customDimensionName_To_DimensionStack_Dictionary.Values).ToArray();

            return list;
        }

        private static DimensionStack CreateDimensionStack(DimensionEnum standardDimensionEnum)
        {
            var stack = new DimensionStack(standardDimensionEnum);

            // Initialize stack with one item in it, so Peek immediately works to return 0.0.
            stack.Push(0.0);

            return stack;
        }

        private static DimensionStack CreateDimensionStack(string customDimensionName)
        {
            var stack = new DimensionStack(customDimensionName);

            // Initialize stack with one item in it, so Peek immediately works to return 0.0.
            stack.Push(0.0);

            return stack;
        }
    }
}
