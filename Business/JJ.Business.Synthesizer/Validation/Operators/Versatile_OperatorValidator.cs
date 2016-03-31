using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Versatile_OperatorValidator : ValidatorBase<Operator>
    {
        private IPatchRepository _patchRepository;

        private Dictionary<OperatorTypeEnum, Type> _validatorTypeDictionary = new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.Absolute, typeof(Absolute_OperatorValidator) },
            { OperatorTypeEnum.Add, typeof(Add_OperatorValidator) },
            { OperatorTypeEnum.Adder, typeof(Adder_OperatorValidator) },
            { OperatorTypeEnum.And, typeof(And_OperatorValidator) },
            { OperatorTypeEnum.Average, typeof(Average_OperatorValidator) },
            { OperatorTypeEnum.Bundle, typeof(Bundle_OperatorValidator) },
            { OperatorTypeEnum.Cache, typeof(Cache_OperatorValidator) },
            { OperatorTypeEnum.Curve, typeof(Curve_OperatorValidator) },
            { OperatorTypeEnum.Delay, typeof(Delay_OperatorValidator) },
            { OperatorTypeEnum.Divide, typeof(Divide_OperatorValidator) },
            { OperatorTypeEnum.Earlier, typeof(Earlier_OperatorValidator) },
            { OperatorTypeEnum.Equal, typeof(Equal_OperatorValidator) },
            { OperatorTypeEnum.Exponent, typeof(Exponent_OperatorValidator) },
            { OperatorTypeEnum.Filter, typeof(Filter_OperatorValidator) },
            { OperatorTypeEnum.GreaterThan, typeof(GreaterThan_OperatorValidator) },
            { OperatorTypeEnum.GreaterThanOrEqual, typeof(GreaterThanOrEqual_OperatorValidator) },
            { OperatorTypeEnum.HighPassFilter, typeof(HighPassFilter_OperatorValidator) },
            { OperatorTypeEnum.If, typeof(If_OperatorValidator) },
            { OperatorTypeEnum.LessThan, typeof(LessThan_OperatorValidator) },
            { OperatorTypeEnum.LessThanOrEqual, typeof(LessThanOrEqual_OperatorValidator) },
            { OperatorTypeEnum.Loop, typeof(OperatorValidator_Loop) },
            { OperatorTypeEnum.LowPassFilter, typeof(LowPassFilter_OperatorValidator) },
            { OperatorTypeEnum.Maximum, typeof(Maximum_OperatorValidator) },
            { OperatorTypeEnum.Minimum, typeof(Minimum_OperatorValidator) },
            { OperatorTypeEnum.Multiply, typeof(Multiply_OperatorValidator) },
            { OperatorTypeEnum.Narrower, typeof(Narrower_OperatorValidator) },
            { OperatorTypeEnum.Negative, typeof(Negative_OperatorValidator) },
            { OperatorTypeEnum.Noise, typeof(Noise_OperatorValidator) },
            { OperatorTypeEnum.Not, typeof(Not_OperatorValidator) },
            { OperatorTypeEnum.NotEqual, typeof(NotEqual_OperatorValidator) },
            { OperatorTypeEnum.Number, typeof(Number_OperatorValidator) },
            { OperatorTypeEnum.OneOverX, typeof(OneOverX_OperatorValidator) },
            { OperatorTypeEnum.Or, typeof(Or_OperatorValidator) },
            { OperatorTypeEnum.PatchInlet, typeof(PatchInlet_OperatorValidator) },
            { OperatorTypeEnum.PatchOutlet, typeof(PatchOutlet_OperatorValidator) },
            { OperatorTypeEnum.Power, typeof(Power_OperatorValidator) },
            { OperatorTypeEnum.Pulse, typeof(Pulse_OperatorValidator) },
            { OperatorTypeEnum.Random, typeof(Random_OperatorValidator) },
            { OperatorTypeEnum.Resample, typeof(Resample_OperatorValidator) },
            { OperatorTypeEnum.Reset, typeof(Reset_OperatorValidator) },
            { OperatorTypeEnum.Reverse, typeof(Reverse_OperatorValidator) },
            { OperatorTypeEnum.Round, typeof(Round_OperatorValidator) },
            { OperatorTypeEnum.Sample, typeof(Sample_OperatorValidator) },
            { OperatorTypeEnum.SawDown, typeof(SawDown_OperatorValidator) },
            { OperatorTypeEnum.SawUp, typeof(SawUp_OperatorValidator) },
            { OperatorTypeEnum.Scaler, typeof(Scaler_OperatorValidator) },
            { OperatorTypeEnum.Select, typeof(Select_OperatorValidator) },
            { OperatorTypeEnum.Shift, typeof(Shift_OperatorValidator) },
            { OperatorTypeEnum.Sine, typeof(Sine_OperatorValidator) },
            { OperatorTypeEnum.SlowDown, typeof(SlowDown_OperatorValidator) },
            { OperatorTypeEnum.Spectrum, typeof(Spectrum_OperatorValidator) },
            { OperatorTypeEnum.SpeedUp, typeof(SpeedUp_OperatorValidator) },
            { OperatorTypeEnum.Square, typeof(Square_OperatorValidator) },
            { OperatorTypeEnum.Stretch, typeof(Stretch_OperatorValidator) },
            { OperatorTypeEnum.Subtract, typeof(Subtract_OperatorValidator) },
            { OperatorTypeEnum.TimePower, typeof(TimePower_OperatorValidator) },
            { OperatorTypeEnum.Triangle, typeof(Triangle_OperatorValidator) },
            { OperatorTypeEnum.Unbundle, typeof(Unbundle_OperatorValidator) }
        };

        public Versatile_OperatorValidator(Operator obj, IPatchRepository patchRepository)
            : base(obj, postponeExecute: true)
        {
            _patchRepository = patchRepository;

            Execute();
        }

        protected override void Execute()
        {
            Execute<Basic_OperatorValidator>();

            if (Object.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
            {
                Execute(new CustomOperator_OperatorValidator(Object, _patchRepository));
                return;
            }

            Type validatorType;
            if (!_validatorTypeDictionary.TryGetValue(Object.GetOperatorTypeEnum(), out validatorType))
            {
                ValidationMessages.Add(() => Object.GetOperatorTypeEnum(), MessageFormatter.UnsupportedOperatorTypeEnumValue(Object.GetOperatorTypeEnum()));
            }
            else
            {
                Execute(validatorType);
            }
        }
    }
}
