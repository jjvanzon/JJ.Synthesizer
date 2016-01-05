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
            { OperatorTypeEnum.Loop, typeof(OperatorWarningValidator_Loop) },
            { OperatorTypeEnum.Multiply, typeof(OperatorWarningValidator_Multiply) },
            { OperatorTypeEnum.Number, typeof(OperatorWarningValidator_Number) },
            { OperatorTypeEnum.PatchOutlet, typeof(OperatorWarningValidator_PatchOutlet) },
            { OperatorTypeEnum.PatchInlet, null },
            { OperatorTypeEnum.Power, typeof(OperatorWarningValidator_Power) },
            { OperatorTypeEnum.Resample, typeof(OperatorWarningValidator_Resample) },
            { OperatorTypeEnum.Sample, typeof(OperatorWarningValidator_Sample) },
            { OperatorTypeEnum.SawTooth, typeof(OperatorWarningValidator_SawTooth) },
            { OperatorTypeEnum.Select, typeof(OperatorWarningValidator_Select) },
            { OperatorTypeEnum.Sine, typeof(OperatorWarningValidator_Sine) },
            { OperatorTypeEnum.SlowDown, typeof(OperatorWarningValidator_SlowDown) },
            { OperatorTypeEnum.SpeedUp, typeof(OperatorWarningValidator_SpeedUp) },
            { OperatorTypeEnum.SquareWave, typeof(OperatorWarningValidator_SquareWave) },
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
