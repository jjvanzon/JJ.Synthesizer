﻿using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_Versatile : ValidatorBase<Operator>
    {
        public OperatorWarningValidator_Versatile(Operator op)
            : base(op)
        {

            ExecuteValidator(new OperatorWarningValidator_Basic(op));

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Add: ExecuteValidator(new Add_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.AllPassFilter: ExecuteValidator(new AllPassFilter_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.And: ExecuteValidator(new And_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.AverageFollower: ExecuteValidator(new AverageFollower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.AverageOverDimension: ExecuteValidator(new AverageOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.AverageOverInlets: ExecuteValidator(new AverageOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.BandPassFilterConstantPeakGain: ExecuteValidator(new BandPassFilterConstantPeakGain_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.BandPassFilterConstantTransitionGain: ExecuteValidator(new BandPassFilterConstantTransitionGain_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Cache: ExecuteValidator(new Cache_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ChangeTrigger: ExecuteValidator(new ChangeTrigger_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ClosestOverDimension: ExecuteValidator(new ClosestOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ClosestOverDimensionExp: ExecuteValidator(new ClosestOverDimensionExp_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ClosestOverInlets: ExecuteValidator(new ClosestOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ClosestOverInletsExp: ExecuteValidator(new ClosestOverInletsExp_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Curve: ExecuteValidator(new Curve_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.CustomOperator: ExecuteValidator(new CustomOperator_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.DimensionToOutlets: ExecuteValidator(new DimensionToOutlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Divide: ExecuteValidator(new Divide_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Equal: ExecuteValidator(new Equal_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Exponent: ExecuteValidator(new Exponent_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.GreaterThan: ExecuteValidator(new GreaterThan_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.GreaterThanOrEqual: ExecuteValidator(new GreaterThanOrEqual_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.HighPassFilter: ExecuteValidator(new HighPassFilter_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.HighShelfFilter: ExecuteValidator(new HighShelfFilter_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Hold: ExecuteValidator(new Hold_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.If: ExecuteValidator(new If_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.InletsToDimension: ExecuteValidator(new InletsToDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Interpolate: ExecuteValidator(new Interpolate_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.LessThan: ExecuteValidator(new LessThan_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.LessThanOrEqual: ExecuteValidator(new LessThanOrEqual_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Loop: ExecuteValidator(new Loop_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.LowPassFilter: ExecuteValidator(new LowPassFilter_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.LowShelfFilter: ExecuteValidator(new LowShelfFilter_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MaxFollower: ExecuteValidator(new MaxFollower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MaxOverDimension: ExecuteValidator(new MaxOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MaxOverInlets: ExecuteValidator(new MaxOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MinFollower: ExecuteValidator(new MinFollower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MinOverDimension: ExecuteValidator(new MinOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MinOverInlets: ExecuteValidator(new MinOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Multiply: ExecuteValidator(new Multiply_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.MultiplyWithOrigin: ExecuteValidator(new MultiplyWithOrigin_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Negative: ExecuteValidator(new Negative_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Not: ExecuteValidator(new Not_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.NotchFilter: ExecuteValidator(new NotchFilter_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.NotEqual: ExecuteValidator(new NotEqual_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Number: ExecuteValidator(new Number_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.OneOverX: ExecuteValidator(new OneOverX_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Or: ExecuteValidator(new Or_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.PatchOutlet: ExecuteValidator(new PatchOutlet_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.PeakingEQFilter: ExecuteValidator(new PeakingEQFilter_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Power: ExecuteValidator(new Power_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Pulse: ExecuteValidator(new Pulse_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.PulseTrigger: ExecuteValidator(new PulseTrigger_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Random: ExecuteValidator(new Random_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.RangeOverDimension: ExecuteValidator(new RangeOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.RangeOverOutlets: ExecuteValidator(new RangeOverOutlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Reset: ExecuteValidator(new Reset_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Reverse: ExecuteValidator(new Reverse_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Round: ExecuteValidator(new Round_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Sample: ExecuteValidator(new Sample_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SawDown: ExecuteValidator(new SawDown_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SawUp: ExecuteValidator(new SawUp_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Scaler: ExecuteValidator(new Scaler_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Shift: ExecuteValidator(new Shift_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Sine: ExecuteValidator(new Sine_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SortOverDimension: ExecuteValidator(new SortOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SortOverInlets: ExecuteValidator(new SortOverInlets_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Spectrum: ExecuteValidator(new Spectrum_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Square: ExecuteValidator(new Square_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Squash: ExecuteValidator(new Squash_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Stretch: ExecuteValidator(new Stretch_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Subtract: ExecuteValidator(new Subtract_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SumFollower: ExecuteValidator(new SumFollower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.SumOverDimension: ExecuteValidator(new SumOverDimension_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.TimePower: ExecuteValidator(new TimePower_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.ToggleTrigger: ExecuteValidator(new ToggleTrigger_OperatorWarningValidator(op)); break;
                case OperatorTypeEnum.Triangle: ExecuteValidator(new Triangle_OperatorWarningValidator(op)); break;
            }
        }
    }
}