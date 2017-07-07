using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_Versatile : ValidatorBase
    {
        public OperatorWarningValidator_Versatile(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            ExecuteValidator(new OperatorWarningValidator_Basic(op));

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                // Only ones with specialized validators are in here. Others are covered with the Basic validator already executed above.
                case OperatorTypeEnum.AverageFollower: ExecuteValidator(new AverageFollower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.AverageOverDimension: ExecuteValidator(new AverageOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.AverageOverInlets: ExecuteValidator(new AverageOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Cache: ExecuteValidator(new Cache_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ChangeTrigger: ExecuteValidator(new ChangeTrigger_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ClosestOverDimension: ExecuteValidator(new ClosestOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ClosestOverDimensionExp: ExecuteValidator(new ClosestOverDimensionExp_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ClosestOverInlets: ExecuteValidator(new ClosestOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ClosestOverInletsExp: ExecuteValidator(new ClosestOverInletsExp_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Curve: ExecuteValidator(new Curve_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.CustomOperator: ExecuteValidator(new CustomOperator_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.DimensionToOutlets: ExecuteValidator(new DimensionToOutlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Exponent: ExecuteValidator(new Exponent_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Hold: ExecuteValidator(new Hold_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.InletsToDimension: ExecuteValidator(new InletsToDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Interpolate: ExecuteValidator(new Interpolate_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Loop: ExecuteValidator(new Loop_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MaxFollower: ExecuteValidator(new MaxFollower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MaxOverDimension: ExecuteValidator(new MaxOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MaxOverInlets: ExecuteValidator(new MaxOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MinFollower: ExecuteValidator(new MinFollower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MinOverDimension: ExecuteValidator(new MinOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MinOverInlets: ExecuteValidator(new MinOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Number: ExecuteValidator(new Number_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.PatchOutlet: ExecuteValidator(new PatchOutlet_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.PulseTrigger: ExecuteValidator(new PulseTrigger_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Random: ExecuteValidator(new Random_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.RangeOverDimension: ExecuteValidator(new RangeOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.RangeOverOutlets: ExecuteValidator(new RangeOverOutlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Reset: ExecuteValidator(new Reset_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Reverse: ExecuteValidator(new Reverse_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Round: ExecuteValidator(new Round_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Sample: ExecuteValidator(new Sample_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Scaler: ExecuteValidator(new Scaler_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Shift: ExecuteValidator(new Shift_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SortOverDimension: ExecuteValidator(new SortOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SortOverInlets: ExecuteValidator(new SortOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Spectrum: ExecuteValidator(new Spectrum_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Squash: ExecuteValidator(new Squash_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Stretch: ExecuteValidator(new Stretch_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SumFollower: ExecuteValidator(new SumFollower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SumOverDimension: ExecuteValidator(new SumOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.TimePower: ExecuteValidator(new TimePower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ToggleTrigger: ExecuteValidator(new ToggleTrigger_OperatorWarningValidator(op)); break;
            }
        }
    }
}
