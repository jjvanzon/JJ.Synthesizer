using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class OperatorWarningValidator_Versatile : ValidatorBase<Operator>
    {
        private IDictionary<OperatorTypeEnum, Type> _validatorTypeDictionary = new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.Absolute, typeof(OperatorWarningValidator_Absolute) },
            { OperatorTypeEnum.Add, typeof(OperatorWarningValidator_Add) },
            { OperatorTypeEnum.Adder, typeof(OperatorWarningValidator_Adder) },
            { OperatorTypeEnum.And, typeof(OperatorWarningValidator_And) },
            { OperatorTypeEnum.Average, typeof(OperatorWarningValidator_Average) },
            { OperatorTypeEnum.Bundle, typeof(OperatorWarningValidator_Bundle) },
            { OperatorTypeEnum.Cache, typeof(OperatorWarningValidator_Cache) },
            { OperatorTypeEnum.Curve, typeof(OperatorWarningValidator_Curve) },
            { OperatorTypeEnum.CustomOperator, typeof(OperatorWarningValidator_CustomOperator) },
            { OperatorTypeEnum.Delay, typeof(OperatorWarningValidator_Delay) },
            { OperatorTypeEnum.Divide, typeof(OperatorWarningValidator_Divide) },
            { OperatorTypeEnum.Earlier, typeof(OperatorWarningValidator_Earlier) },
            { OperatorTypeEnum.Equal, typeof(OperatorWarningValidator_Equal) },
            { OperatorTypeEnum.Exponent, typeof(OperatorWarningValidator_Exponent) },
            { OperatorTypeEnum.GreaterThan, typeof(OperatorWarningValidator_GreaterThan) },
            { OperatorTypeEnum.GreaterThanOrEqual, typeof(OperatorWarningValidator_GreaterThanOrEqual) },
            { OperatorTypeEnum.HighPassFilter, typeof(OperatorWarningValidator_HighPassFilter) },
            { OperatorTypeEnum.If, typeof(OperatorWarningValidator_If) },
            { OperatorTypeEnum.LessThan, typeof(OperatorWarningValidator_LessThan) },
            { OperatorTypeEnum.LessThanOrEqual, typeof(OperatorWarningValidator_LessThanOrEqual) },
            { OperatorTypeEnum.Loop, typeof(OperatorWarningValidator_Loop) },
            { OperatorTypeEnum.LowPassFilter, typeof(OperatorWarningValidator_LowPassFilter) },
            { OperatorTypeEnum.Maximum, typeof(OperatorWarningValidator_Maximum) },
            { OperatorTypeEnum.Minimum, typeof(OperatorWarningValidator_Minimum) },
            { OperatorTypeEnum.Multiply, typeof(OperatorWarningValidator_Multiply) },
            { OperatorTypeEnum.Narrower, typeof(OperatorWarningValidator_Narrower) },
            { OperatorTypeEnum.Negative, typeof(OperatorWarningValidator_Negative) },
            { OperatorTypeEnum.Noise, null },
            { OperatorTypeEnum.Not, typeof(OperatorWarningValidator_Not) },
            { OperatorTypeEnum.NotEqual, typeof(OperatorWarningValidator_NotEqual) },
            { OperatorTypeEnum.Number, typeof(OperatorWarningValidator_Number) },
            { OperatorTypeEnum.OneOverX, typeof(OperatorWarningValidator_OneOverX) },
            { OperatorTypeEnum.Or, typeof(OperatorWarningValidator_Or) },
            { OperatorTypeEnum.PatchInlet, null },
            { OperatorTypeEnum.PatchOutlet, typeof(OperatorWarningValidator_PatchOutlet) },
            { OperatorTypeEnum.Power, typeof(OperatorWarningValidator_Power) },
            { OperatorTypeEnum.Pulse, typeof(OperatorWarningValidator_Pulse) },
            { OperatorTypeEnum.Random, typeof(OperatorWarningValidator_Random) },
            { OperatorTypeEnum.Resample, typeof(OperatorWarningValidator_Resample) },
            { OperatorTypeEnum.Reverse , typeof(OperatorWarningValidator_Reverse ) },
            { OperatorTypeEnum.Reset, typeof(OperatorWarningValidator_Reset) },
            { OperatorTypeEnum.Sample, typeof(OperatorWarningValidator_Sample) },
            { OperatorTypeEnum.SawDown, typeof(OperatorWarningValidator_SawDown) },
            { OperatorTypeEnum.SawUp, typeof(OperatorWarningValidator_SawUp) },
            { OperatorTypeEnum.Select, typeof(OperatorWarningValidator_Select) },
            { OperatorTypeEnum.Shift, typeof(OperatorWarningValidator_Shift) },
            { OperatorTypeEnum.Sine, typeof(OperatorWarningValidator_Sine) },
            { OperatorTypeEnum.SlowDown, typeof(OperatorWarningValidator_SlowDown) },
            { OperatorTypeEnum.Spectrum, typeof(OperatorWarningValidator_Spectrum) },
            { OperatorTypeEnum.SpeedUp, typeof(OperatorWarningValidator_SpeedUp) },
            { OperatorTypeEnum.Square, typeof(OperatorWarningValidator_Square) },
            { OperatorTypeEnum.Stretch, typeof(OperatorWarningValidator_Stretch) },
            { OperatorTypeEnum.Subtract, typeof(OperatorWarningValidator_Subtract) },
            { OperatorTypeEnum.TimePower, typeof(OperatorWarningValidator_TimePower) },
            { OperatorTypeEnum.Triangle, typeof(OperatorWarningValidator_Triangle) },
            { OperatorTypeEnum.Unbundle, typeof(OperatorWarningValidator_Unbundle) },
        };

        public OperatorWarningValidator_Versatile(Operator obj)
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
