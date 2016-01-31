using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Versatile : ValidatorBase<Operator>
    {
        private IDictionary<OperatorTypeEnum, Type> _validatorTypeDictionary = new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.Add, typeof(OperatorWarningValidator_Add) },
            { OperatorTypeEnum.Adder, typeof(OperatorWarningValidator_Adder) },
            { OperatorTypeEnum.Bundle, typeof(OperatorWarningValidator_Bundle) },
            { OperatorTypeEnum.Curve, typeof(OperatorWarningValidator_Curve) },
            { OperatorTypeEnum.CustomOperator, typeof(OperatorWarningValidator_CustomOperator) },
            { OperatorTypeEnum.Delay, typeof(OperatorWarningValidator_Delay) },
            { OperatorTypeEnum.Divide, typeof(OperatorWarningValidator_Divide) },
            { OperatorTypeEnum.Earlier, typeof(OperatorWarningValidator_Earlier) },
            { OperatorTypeEnum.Exponent, typeof(OperatorWarningValidator_Exponent) },
            { OperatorTypeEnum.HighPassFilter, typeof(OperatorWarningValidator_HighPassFilter) },
            { OperatorTypeEnum.Loop, typeof(OperatorWarningValidator_Loop) },
            { OperatorTypeEnum.LowPassFilter, typeof(OperatorWarningValidator_LowPassFilter) },
            { OperatorTypeEnum.Multiply, typeof(OperatorWarningValidator_Multiply) },
            { OperatorTypeEnum.Narrower, typeof(OperatorWarningValidator_Narrower) },
            { OperatorTypeEnum.Number, typeof(OperatorWarningValidator_Number) },
            { OperatorTypeEnum.PatchInlet, null },
            { OperatorTypeEnum.PatchOutlet, typeof(OperatorWarningValidator_PatchOutlet) },
            { OperatorTypeEnum.Power, typeof(OperatorWarningValidator_Power) },
            { OperatorTypeEnum.Pulse, typeof(OperatorWarningValidator_Pulse) },
            { OperatorTypeEnum.Random, typeof(OperatorWarningValidator_Random) },
            { OperatorTypeEnum.Resample, typeof(OperatorWarningValidator_Resample) },
            { OperatorTypeEnum.Reset, typeof(OperatorWarningValidator_Reset) },
            { OperatorTypeEnum.Sample, typeof(OperatorWarningValidator_Sample) },
            { OperatorTypeEnum.SawTooth, typeof(OperatorWarningValidator_SawTooth) },
            { OperatorTypeEnum.Select, typeof(OperatorWarningValidator_Select) },
            { OperatorTypeEnum.Shift, typeof(OperatorWarningValidator_Shift) },
            { OperatorTypeEnum.Sine, typeof(OperatorWarningValidator_Sine) },
            { OperatorTypeEnum.SlowDown, typeof(OperatorWarningValidator_SlowDown) },
            { OperatorTypeEnum.Spectrum, typeof(OperatorWarningValidator_Spectrum) },
            { OperatorTypeEnum.SpeedUp, typeof(OperatorWarningValidator_SpeedUp) },
            { OperatorTypeEnum.SquareWave, typeof(OperatorWarningValidator_SquareWave) },
            { OperatorTypeEnum.Stretch, typeof(OperatorWarningValidator_Stretch) },
            { OperatorTypeEnum.Subtract, typeof(OperatorWarningValidator_Subtract) },
            { OperatorTypeEnum.TimePower, typeof(OperatorWarningValidator_TimePower) },
            { OperatorTypeEnum.TriangleWave, typeof(OperatorWarningValidator_TriangleWave) },
            { OperatorTypeEnum.Unbundle, typeof(OperatorWarningValidator_Unbundle) },
            { OperatorTypeEnum.WhiteNoise, null }
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
