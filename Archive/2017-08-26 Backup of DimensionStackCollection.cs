﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation
//{
//    internal class DimensionStackCollection
//    {
//        private readonly Dictionary<DimensionEnum, DimensionStack> _standardDimensionEnum_To_DimensionStack_Dictionary =
//                     new Dictionary<DimensionEnum, DimensionStack>();

//        private readonly Dictionary<string, DimensionStack> _customDimensionName_To_DimensionStack_Dictionary =
//                     new Dictionary<string, DimensionStack>();

//        public DimensionStack GetDimensionStack(IOperatorDto_WithDimension dto)
//        {
//            if (dto == null) throw new NullException(() => dto);

//            return GetDimensionStack(dto.StandardDimensionEnum, dto.CanonicalCustomDimensionName);
//        }

//        public DimensionStack GetDimensionStack(Operator op)
//        {
//            if (op == null) throw new NullException(() => op);

//            DimensionStack dimensionStack = GetDimensionStack(
//                op.GetStandardDimensionEnumWithFallback(),
//                op.GetCustomDimensionNameWithFallback());

//            return dimensionStack;
//        }

//        public DimensionStack GetDimensionStack(DimensionEnum standardDimensionEnum, string customDimensionName)
//        {
//            if (standardDimensionEnum != DimensionEnum.Undefined && NameHelper.IsFilledIn(customDimensionName))
//            {
//                // This might be too harsh an assumption. If this turns out to be true, remove this constraint.
//                throw new Exception($"{standardDimensionEnum} and {customDimensionName} cannot both be filled in.");
//            }

//            if (standardDimensionEnum != DimensionEnum.Undefined)
//            {
//                return GetDimensionStack(standardDimensionEnum);
//            }
//            else
//            {
//                return GetDimensionStack(customDimensionName);
//            }
//        }

//        public DimensionStack GetDimensionStack(DimensionEnum standardDimensionEnum)
//        {
//            // ReSharper disable once InvertIf
//            if (!_standardDimensionEnum_To_DimensionStack_Dictionary.TryGetValue(standardDimensionEnum, out DimensionStack dimensionStack))
//            {
//                dimensionStack = CreateDimensionStack(standardDimensionEnum);
//                _standardDimensionEnum_To_DimensionStack_Dictionary[standardDimensionEnum] = dimensionStack;
//            }

//            return dimensionStack;
//        }

//        public DimensionStack GetDimensionStack(string customDimensionName)
//        {
//            string canonicalCustomDimensionName = NameHelper.ToCanonical(customDimensionName);

//            // ReSharper disable once InvertIf
//            if (!_customDimensionName_To_DimensionStack_Dictionary.TryGetValue(canonicalCustomDimensionName, out DimensionStack dimensionStack))
//            {
//                dimensionStack = CreateDimensionStack(canonicalCustomDimensionName);
//                _customDimensionName_To_DimensionStack_Dictionary[canonicalCustomDimensionName] = dimensionStack;
//            }

//            return dimensionStack;
//        }

//        public IList<DimensionStack> GetDimensionStacks()
//        {
//            // ReSharper disable once InvokeAsExtensionMethod
//            IList<DimensionStack> list = Enumerable.Union(
//                _standardDimensionEnum_To_DimensionStack_Dictionary.Values,
//                _customDimensionName_To_DimensionStack_Dictionary.Values).ToArray();

//            return list;
//        }

//        private static DimensionStack CreateDimensionStack(DimensionEnum standardDimensionEnum)
//        {
//            var stack = new DimensionStack(standardDimensionEnum);

//            // Initialize stack with one item in it, so Peek immediately works to return 0.0.
//            stack.Push(0.0);

//            return stack;
//        }

//        private static DimensionStack CreateDimensionStack(string canonicalCustomDimensionName)
//        {
//            var stack = new DimensionStack(canonicalCustomDimensionName);

//            // Initialize stack with one item in it, so Peek immediately works to return 0.0.
//            stack.Push(0.0);

//            return stack;
//        }
//    }
//}
