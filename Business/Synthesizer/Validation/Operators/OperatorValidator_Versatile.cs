using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Versatile : ValidatorBase
    {
        public OperatorValidator_Versatile(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            ExecuteValidator(new OperatorValidator_Basic(op));

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Undefined:
                    // Handle Undefined
                    return;

                case OperatorTypeEnum.CustomOperator:
                    // Handle CustomOperator
                    ExecuteValidator(new CustomOperator_OperatorValidator(op));
                    return;

                default:
                    // Handle ValidatorTypes in dictionary
                    if (_specializedValidatorTypeDictionary.TryGetValue(operatorTypeEnum, out Type validatorType))
                    {
                        var validator = (IValidator)Activator.CreateInstance(validatorType, op);
                        ExecuteValidator(validator);
                        return;
                    }

                    // Otherwise assume from System Document.
                    ExecuteValidator(new OperatorValidator_WithUnderlyingPatch(op));

                    break;
            }
        }

        private readonly Dictionary<OperatorTypeEnum, Type> _specializedValidatorTypeDictionary = new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.AverageOverDimension, typeof(OperatorValidator_AggregateOverDimension) },
            { OperatorTypeEnum.Cache, typeof(Cache_OperatorValidator) },
            { OperatorTypeEnum.ClosestOverDimension, typeof(OperatorValidator_AggregateOverDimension) },
            { OperatorTypeEnum.ClosestOverDimensionExp, typeof(OperatorValidator_AggregateOverDimension) },
            { OperatorTypeEnum.Curve, typeof(Curve_OperatorValidator) },
            { OperatorTypeEnum.InletsToDimension, typeof(InletsToDimension_OperatorValidator) },
            { OperatorTypeEnum.Interpolate, typeof(Interpolate_OperatorValidator) },
            { OperatorTypeEnum.MaxOverDimension, typeof(OperatorValidator_AggregateOverDimension) },
            { OperatorTypeEnum.MinOverDimension, typeof(OperatorValidator_AggregateOverDimension) },
            { OperatorTypeEnum.Number, typeof(Number_OperatorValidator) },
            { OperatorTypeEnum.Random, typeof(Random_OperatorValidator) },
            { OperatorTypeEnum.Reset, typeof(Reset_OperatorValidator) },
            { OperatorTypeEnum.Sample, typeof(Sample_OperatorValidator) },
            { OperatorTypeEnum.SortOverDimension, typeof(OperatorValidator_AggregateOverDimension) },
            { OperatorTypeEnum.SumOverDimension, typeof(OperatorValidator_AggregateOverDimension) }
        };
    }
}