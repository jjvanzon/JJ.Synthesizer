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
            { OperatorTypeEnum.And, typeof(And_OperatorWarningValidator) },
            { OperatorTypeEnum.AverageOverDimension, typeof(AverageOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.Average, typeof(Average_OperatorWarningValidator) },
            { OperatorTypeEnum.AverageFollower, typeof(AverageFollower_OperatorWarningValidator) },
            { OperatorTypeEnum.Bundle, typeof(Bundle_OperatorWarningValidator) },
            { OperatorTypeEnum.Cache, typeof(Cache_OperatorWarningValidator) },
            { OperatorTypeEnum.ChangeTrigger, typeof(ChangeTrigger_OperatorWarningValidator) },
            { OperatorTypeEnum.Closest, typeof(Closest_OperatorWarningValidator) },
            { OperatorTypeEnum.ClosestOverDimension, typeof(ClosestOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.Curve, typeof(Curve_OperatorWarningValidator) },
            { OperatorTypeEnum.CustomOperator, typeof(CustomOperator_OperatorWarningValidator) },
            { OperatorTypeEnum.Delay, typeof(Delay_OperatorWarningValidator) },
            { OperatorTypeEnum.Divide, typeof(Divide_OperatorWarningValidator) },
            { OperatorTypeEnum.Earlier, typeof(Earlier_OperatorWarningValidator) },
            { OperatorTypeEnum.Equal, typeof(Equal_OperatorWarningValidator) },
            { OperatorTypeEnum.Exponent, typeof(Exponent_OperatorWarningValidator) },
            { OperatorTypeEnum.Filter, typeof(Filter_OperatorWarningValidator) },
            { OperatorTypeEnum.GetDimension, null },
            { OperatorTypeEnum.GreaterThan, typeof(GreaterThan_OperatorWarningValidator) },
            { OperatorTypeEnum.GreaterThanOrEqual, typeof(GreaterThanOrEqual_OperatorWarningValidator) },
            { OperatorTypeEnum.HighPassFilter, typeof(HighPassFilter_OperatorWarningValidator) },
            { OperatorTypeEnum.Hold, typeof(Hold_OperatorWarningValidator) },
            { OperatorTypeEnum.If, typeof(If_OperatorWarningValidator) },
            { OperatorTypeEnum.LessThan, typeof(LessThan_OperatorWarningValidator) },
            { OperatorTypeEnum.LessThanOrEqual, typeof(LessThanOrEqual_OperatorWarningValidator) },
            { OperatorTypeEnum.Loop, typeof(Loop_OperatorWarningValidator) },
            { OperatorTypeEnum.LowPassFilter, typeof(LowPassFilter_OperatorWarningValidator) },
            { OperatorTypeEnum.MakeContinuous, typeof(MakeContinuous_OperatorWarningValidator) },
            { OperatorTypeEnum.MakeDiscrete, typeof(MakeDiscrete_OperatorWarningValidator) },
            { OperatorTypeEnum.MaxOverDimension, typeof(MaxOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.Max, typeof(Max_OperatorWarningValidator) },
            { OperatorTypeEnum.MaxFollower, typeof(MaxFollower_OperatorWarningValidator) },
            { OperatorTypeEnum.MinOverDimension, typeof(MinOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.Min, typeof(Min_OperatorWarningValidator) },
            { OperatorTypeEnum.MinFollower, typeof(MinFollower_OperatorWarningValidator) },
            { OperatorTypeEnum.Multiply, typeof(Multiply_OperatorWarningValidator) },
            { OperatorTypeEnum.MultiplyWithOrigin, typeof(MultiplyWithOrigin_OperatorWarningValidator) },
            { OperatorTypeEnum.Narrower, typeof(Narrower_OperatorWarningValidator) },
            { OperatorTypeEnum.Negative, typeof(Negative_OperatorWarningValidator) },
            { OperatorTypeEnum.Noise, null },
            { OperatorTypeEnum.Not, typeof(Not_OperatorWarningValidator) },
            { OperatorTypeEnum.NotEqual, typeof(NotEqual_OperatorWarningValidator) },
            { OperatorTypeEnum.Number, typeof(Number_OperatorWarningValidator) },
            { OperatorTypeEnum.OneOverX, typeof(OneOverX_OperatorWarningValidator) },
            { OperatorTypeEnum.Or, typeof(Or_OperatorWarningValidator) },
            { OperatorTypeEnum.PatchInlet, null },
            { OperatorTypeEnum.PatchOutlet, typeof(PatchOutlet_OperatorWarningValidator) },
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
            { OperatorTypeEnum.SlowDown, typeof(SlowDown_OperatorWarningValidator) },
            { OperatorTypeEnum.Spectrum, typeof(Spectrum_OperatorWarningValidator) },
            { OperatorTypeEnum.SpeedUp, typeof(SpeedUp_OperatorWarningValidator) },
            { OperatorTypeEnum.Square, typeof(Square_OperatorWarningValidator) },
            { OperatorTypeEnum.Stretch, typeof(Stretch_OperatorWarningValidator) },
            { OperatorTypeEnum.Subtract, typeof(Subtract_OperatorWarningValidator) },
            { OperatorTypeEnum.SumOverDimension, typeof(SumOverDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.SumFollower, typeof(SumFollower_OperatorWarningValidator) },
            { OperatorTypeEnum.TimePower, typeof(TimePower_OperatorWarningValidator) },
            { OperatorTypeEnum.ToggleTrigger, typeof(ToggleTrigger_OperatorWarningValidator) },
            { OperatorTypeEnum.Triangle, typeof(Triangle_OperatorWarningValidator) },
            { OperatorTypeEnum.Unbundle, typeof(Unbundle_OperatorWarningValidator) },
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
                throw new Exception(String.Format("_validatorTypeDictionary does not contain key OperatorTypeEnum '{0}'.", operatorTypeEnum));
            }
            else
            {
                if (validatorType != null)
                {
                    Execute(validatorType);
                }
            }
        }
    }
}
