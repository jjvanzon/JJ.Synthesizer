using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Versatile_OperatorWarningValidator : ValidatorBase<Operator>
    {
        private IDictionary<OperatorTypeEnum, Type> _validatorTypeDictionary = new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.Absolute, typeof(Absolute_OperatorWarningValidator) },
            { OperatorTypeEnum.Add, typeof(Add_OperatorWarningValidator) },
            { OperatorTypeEnum.Adder, typeof(Adder_OperatorWarningValidator) },
            { OperatorTypeEnum.And, typeof(And_OperatorWarningValidator) },
            { OperatorTypeEnum.Average, typeof(Average_OperatorWarningValidator) },
            { OperatorTypeEnum.AverageContinuous, typeof(AverageContinuous_OperatorWarningValidator) },
            { OperatorTypeEnum.AverageDiscrete, typeof(AverageDiscrete_OperatorWarningValidator) },
            { OperatorTypeEnum.Bundle, typeof(Bundle_OperatorWarningValidator) },
            { OperatorTypeEnum.Cache, typeof(Cache_OperatorWarningValidator) },
            { OperatorTypeEnum.ChangeTrigger, typeof(ChangeTrigger_OperatorWarningValidator) },
            { OperatorTypeEnum.Curve, typeof(Curve_OperatorWarningValidator) },
            { OperatorTypeEnum.CustomOperator, typeof(CustomOperator_OperatorWarningValidator) },
            { OperatorTypeEnum.Delay, typeof(Delay_OperatorWarningValidator) },
            { OperatorTypeEnum.Divide, typeof(Divide_OperatorWarningValidator) },
            { OperatorTypeEnum.Earlier, typeof(Earlier_OperatorWarningValidator) },
            { OperatorTypeEnum.Equal, typeof(Equal_OperatorWarningValidator) },
            { OperatorTypeEnum.Exponent, typeof(Exponent_OperatorWarningValidator) },
            { OperatorTypeEnum.GetDimension, typeof(GetDimension_OperatorWarningValidator) },
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
            { OperatorTypeEnum.MaxContinuous, typeof(MaxContinuous_OperatorWarningValidator) },
            { OperatorTypeEnum.MaxDiscrete, typeof(MaxDiscrete_OperatorWarningValidator) },
            { OperatorTypeEnum.Maximum, typeof(Maximum_OperatorWarningValidator) },
            { OperatorTypeEnum.MinContinuous, typeof(MinContinuous_OperatorWarningValidator) },
            { OperatorTypeEnum.MinDiscrete, typeof(MinDiscrete_OperatorWarningValidator) },
            { OperatorTypeEnum.Minimum, typeof(Minimum_OperatorWarningValidator) },
            { OperatorTypeEnum.Multiply, typeof(Multiply_OperatorWarningValidator) },
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
            { OperatorTypeEnum.Random, typeof(Random_OperatorWarningValidator) },
            { OperatorTypeEnum.Range, typeof(Range_OperatorWarningValidator) },
            { OperatorTypeEnum.Resample, typeof(Resample_OperatorWarningValidator) },
            { OperatorTypeEnum.Reverse , typeof(Reverse_OperatorWarningValidator ) },
            { OperatorTypeEnum.Reset, typeof(Reset_OperatorWarningValidator) },
            { OperatorTypeEnum.Sample, typeof(Sample_OperatorWarningValidator) },
            { OperatorTypeEnum.SawDown, typeof(SawDown_OperatorWarningValidator) },
            { OperatorTypeEnum.SawUp, typeof(SawUp_OperatorWarningValidator) },
            { OperatorTypeEnum.Select, typeof(Select_OperatorWarningValidator) },
            { OperatorTypeEnum.SetDimension, typeof(SetDimension_OperatorWarningValidator) },
            { OperatorTypeEnum.Shift, typeof(Shift_OperatorWarningValidator) },
            { OperatorTypeEnum.Sine, typeof(Sine_OperatorWarningValidator) },
            { OperatorTypeEnum.SlowDown, typeof(SlowDown_OperatorWarningValidator) },
            { OperatorTypeEnum.Spectrum, typeof(Spectrum_OperatorWarningValidator) },
            { OperatorTypeEnum.SpeedUp, typeof(SpeedUp_OperatorWarningValidator) },
            { OperatorTypeEnum.Square, typeof(Square_OperatorWarningValidator) },
            { OperatorTypeEnum.Stretch, typeof(Stretch_OperatorWarningValidator) },
            { OperatorTypeEnum.Subtract, typeof(Subtract_OperatorWarningValidator) },
            { OperatorTypeEnum.TimePower, typeof(TimePower_OperatorWarningValidator) },
            { OperatorTypeEnum.ToggleTrigger, typeof(ToggleTrigger_OperatorWarningValidator) },
            { OperatorTypeEnum.Triangle, typeof(Triangle_OperatorWarningValidator) },
            { OperatorTypeEnum.PulseTrigger, typeof(PulseTrigger_OperatorWarningValidator) },
            { OperatorTypeEnum.Unbundle, typeof(Unbundle_OperatorWarningValidator) },
        };

        public Versatile_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Type validatorType;
            if (!_validatorTypeDictionary.TryGetValue(Object.GetOperatorTypeEnum(), out validatorType))
            {
                ValidationMessages.Add(() => Object.GetOperatorTypeEnum(), MessageFormatter.UnsupportedOperatorTypeEnumValue(Object.GetOperatorTypeEnum()));
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
