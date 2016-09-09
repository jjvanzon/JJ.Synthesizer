using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Versatile_OperatorWarningValidator : ValidatorBase<Operator>
    {
        private Dictionary<OperatorTypeEnum, Type> _warningValidatorTypeDictionary = 
            new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.Absolute, typeof(Absolute_OperatorWarningValidator) },
            { OperatorTypeEnum.Add, typeof(Add_OperatorWarningValidator) },
            { OperatorTypeEnum.AllPassFilter, typeof(AllPassFilter_OperatorWarningValidator) },
            { OperatorTypeEnum.And, typeof(And_OperatorWarningValidator) },
            { OperatorTypeEnum.AverageOverDimension, typeof(AverageOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.AverageOverInlets, typeof(AverageOverInlets_OperatorWarningValidator) },
            { OperatorTypeEnum.AverageFollower, typeof(AverageFollower_OperatorWarningValidator) },
            { OperatorTypeEnum.BandPassFilterConstantPeakGain, typeof(BandPassFilterConstantPeakGain_OperatorWarningValidator) },
            { OperatorTypeEnum.BandPassFilterConstantTransitionGain, typeof(BandPassFilterConstantTransitionGain_OperatorWarningValidator) },
            { OperatorTypeEnum.Bundle, typeof(Bundle_OperatorWarningValidator) },
            { OperatorTypeEnum.Cache, typeof(Cache_OperatorWarningValidator) },
            { OperatorTypeEnum.ChangeTrigger, typeof(ChangeTrigger_OperatorWarningValidator) },
            { OperatorTypeEnum.ClosestOverInlets, typeof(ClosestOverInlets_OperatorWarningValidator) },
            { OperatorTypeEnum.ClosestOverInletsExp, typeof(ClosestOverInletsExp_OperatorWarningValidator) },
            { OperatorTypeEnum.ClosestOverDimension, typeof(ClosestOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.ClosestOverDimensionExp, typeof(ClosestOverDimensionExp_OperatorWarningValidator) },
            { OperatorTypeEnum.Curve, typeof(Curve_OperatorWarningValidator) },
            { OperatorTypeEnum.CustomOperator, typeof(CustomOperator_OperatorWarningValidator) },
            { OperatorTypeEnum.Divide, typeof(Divide_OperatorWarningValidator) },
            { OperatorTypeEnum.Equal, typeof(Equal_OperatorWarningValidator) },
            { OperatorTypeEnum.Exponent, typeof(Exponent_OperatorWarningValidator) },
            { OperatorTypeEnum.GetDimension, null },
            { OperatorTypeEnum.GreaterThan, typeof(GreaterThan_OperatorWarningValidator) },
            { OperatorTypeEnum.GreaterThanOrEqual, typeof(GreaterThanOrEqual_OperatorWarningValidator) },
            { OperatorTypeEnum.HighPassFilter, typeof(HighPassFilter_OperatorWarningValidator) },
            { OperatorTypeEnum.HighShelfFilter, typeof(HighShelfFilter_OperatorWarningValidator) },
            { OperatorTypeEnum.Hold, typeof(Hold_OperatorWarningValidator) },
            { OperatorTypeEnum.If, typeof(If_OperatorWarningValidator) },
            { OperatorTypeEnum.LessThan, typeof(LessThan_OperatorWarningValidator) },
            { OperatorTypeEnum.LessThanOrEqual, typeof(LessThanOrEqual_OperatorWarningValidator) },
            { OperatorTypeEnum.Loop, typeof(Loop_OperatorWarningValidator) },
            { OperatorTypeEnum.LowShelfFilter, typeof(LowShelfFilter_OperatorWarningValidator) },
            { OperatorTypeEnum.LowPassFilter, typeof(LowPassFilter_OperatorWarningValidator) },
            { OperatorTypeEnum.MakeContinuous, typeof(MakeContinuous_OperatorWarningValidator) },
            { OperatorTypeEnum.MakeDiscrete, typeof(MakeDiscrete_OperatorWarningValidator) },
            { OperatorTypeEnum.MaxOverDimension, typeof(MaxOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.MaxOverInlets, typeof(MaxOverInlets_OperatorWarningValidator) },
            { OperatorTypeEnum.MaxFollower, typeof(MaxFollower_OperatorWarningValidator) },
            { OperatorTypeEnum.MinOverDimension, typeof(MinOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.MinOverInlets, typeof(MinOverInlets_OperatorWarningValidator) },
            { OperatorTypeEnum.MinFollower, typeof(MinFollower_OperatorWarningValidator) },
            { OperatorTypeEnum.Multiply, typeof(Multiply_OperatorWarningValidator) },
            { OperatorTypeEnum.MultiplyWithOrigin, typeof(MultiplyWithOrigin_OperatorWarningValidator) },
            { OperatorTypeEnum.Negative, typeof(Negative_OperatorWarningValidator) },
            { OperatorTypeEnum.Noise, null },
            { OperatorTypeEnum.NotchFilter, typeof(NotchFilter_OperatorWarningValidator) },
            { OperatorTypeEnum.Not, typeof(Not_OperatorWarningValidator) },
            { OperatorTypeEnum.NotEqual, typeof(NotEqual_OperatorWarningValidator) },
            { OperatorTypeEnum.Number, typeof(Number_OperatorWarningValidator) },
            { OperatorTypeEnum.OneOverX, typeof(OneOverX_OperatorWarningValidator) },
            { OperatorTypeEnum.Or, typeof(Or_OperatorWarningValidator) },
            { OperatorTypeEnum.PatchInlet, null },
            { OperatorTypeEnum.PatchOutlet, typeof(PatchOutlet_OperatorWarningValidator) },
            { OperatorTypeEnum.PeakingEQFilter, typeof(PeakingEQFilter_OperatorWarningValidator) },
            { OperatorTypeEnum.Power, typeof(Power_OperatorWarningValidator) },
            { OperatorTypeEnum.Pulse, typeof(Pulse_OperatorWarningValidator) },
            { OperatorTypeEnum.PulseTrigger, typeof(PulseTrigger_OperatorWarningValidator) },
            { OperatorTypeEnum.Random, typeof(Random_OperatorWarningValidator) },
            { OperatorTypeEnum.Range, typeof(Range_OperatorWarningValidator) },
            { OperatorTypeEnum.Resample, typeof(Resample_OperatorWarningValidator) },
            { OperatorTypeEnum.Reset, typeof(Reset_OperatorWarningValidator) },
            { OperatorTypeEnum.Reverse , typeof(Reverse_OperatorWarningValidator ) },
            { OperatorTypeEnum.Round , typeof(Round_OperatorWarningValidator ) },
            { OperatorTypeEnum.Sample, typeof(Sample_OperatorWarningValidator) },
            { OperatorTypeEnum.SawDown, typeof(SawDown_OperatorWarningValidator) },
            { OperatorTypeEnum.SawUp, typeof(SawUp_OperatorWarningValidator) },
            { OperatorTypeEnum.Scaler, typeof(Scaler_OperatorWarningValidator) },
            { OperatorTypeEnum.Select, typeof(Select_OperatorWarningValidator) },
            { OperatorTypeEnum.SetDimension, null },
            { OperatorTypeEnum.Shift, typeof(Shift_OperatorWarningValidator) },
            { OperatorTypeEnum.Sine, typeof(Sine_OperatorWarningValidator) },
            { OperatorTypeEnum.SortOverInlets, typeof(SortOverInlets_OperatorWarningValidator) },
            { OperatorTypeEnum.SortOverDimension, typeof(SortOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.Spectrum, typeof(Spectrum_OperatorWarningValidator) },
            { OperatorTypeEnum.Square, typeof(Square_OperatorWarningValidator) },
            { OperatorTypeEnum.Squash, typeof(Squash_OperatorWarningValidator) },
            { OperatorTypeEnum.Stretch, typeof(Stretch_OperatorWarningValidator) },
            { OperatorTypeEnum.Subtract, typeof(Subtract_OperatorWarningValidator) },
            { OperatorTypeEnum.SumOverDimension, typeof(SumOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.SumFollower, typeof(SumFollower_OperatorWarningValidator) },
            { OperatorTypeEnum.TimePower, typeof(TimePower_OperatorWarningValidator) },
            { OperatorTypeEnum.ToggleTrigger, typeof(ToggleTrigger_OperatorWarningValidator) },
            { OperatorTypeEnum.Triangle, typeof(Triangle_OperatorWarningValidator) },
            { OperatorTypeEnum.Unbundle, typeof(Unbundle_OperatorWarningValidator) },
            // Temporary (2016-08-01). Remove code line later.
            { (OperatorTypeEnum)59, null }
        };

        public Versatile_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            OperatorTypeEnum operatorTypeEnum = Object.GetOperatorTypeEnum();
            Type validatorType;
            if (!_warningValidatorTypeDictionary.TryGetValue(operatorTypeEnum, out validatorType))
            {
                throw new Exception(String.Format("_warningValidatorTypeDictionary does not contain key OperatorTypeEnum '{0}'.", operatorTypeEnum));
            }
            else
            {
                if (validatorType != null)
                {
                    ExecuteValidator(validatorType);
                }
            }
        }
    }
}
