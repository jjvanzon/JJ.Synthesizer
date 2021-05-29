using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Versatile : ValidatorBase
    {
        public OperatorValidator_Versatile(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Cache:
                    ExecuteValidator(new Cache_OperatorValidator(op));
                    break;

                case OperatorTypeEnum.Curve:
                    ExecuteValidator(new Curve_OperatorValidator(op));
                    break;

                case OperatorTypeEnum.InletsToDimension:
                case OperatorTypeEnum.Random:
                case OperatorTypeEnum.RandomStripe:
                    ExecuteValidator(new OperatorValidator_WithInterpolation(op));
                    break;

                case OperatorTypeEnum.Interpolate:
                    ExecuteValidator(new OperatorValidator_WithInterpolation_AndFollowingMode(op));
                    break;

                case OperatorTypeEnum.Number:
                    ExecuteValidator(new Number_OperatorValidator(op));
                    break;

                case OperatorTypeEnum.Reset:
                    ExecuteValidator(new Reset_OperatorValidator(op));
                    break;

                case OperatorTypeEnum.Sample:
                    ExecuteValidator(new Sample_OperatorValidator(op));
                    break;

                case OperatorTypeEnum.AverageOverDimension:
                case OperatorTypeEnum.ClosestOverDimension:
                case OperatorTypeEnum.ClosestOverDimensionExp:
                case OperatorTypeEnum.MaxOverDimension:
                case OperatorTypeEnum.MinOverDimension:
                case OperatorTypeEnum.SortOverDimension:
                case OperatorTypeEnum.SumOverDimension:
                    ExecuteValidator(new AggregateOverDimension_OperatorValidator(op));
                    break;

                default:
                    ExecuteValidator(new OperatorValidator_Basic(op));
                    break;
            }
        }
    }
}