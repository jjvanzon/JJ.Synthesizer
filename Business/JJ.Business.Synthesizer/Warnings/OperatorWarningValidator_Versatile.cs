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
            { OperatorTypeEnum.Curve, typeof(OperatorWarningValidator_Curve) },
            { OperatorTypeEnum.Divide, typeof(OperatorWarningValidator_Divide) },
            { OperatorTypeEnum.Exponent, typeof(OperatorWarningValidator_Exponent) },
            { OperatorTypeEnum.Multiply, typeof(OperatorWarningValidator_Multiply) },
            { OperatorTypeEnum.PatchOutlet, typeof(OperatorWarningValidator_PatchOutlet) },
            { OperatorTypeEnum.Power, typeof(OperatorWarningValidator_Power) },
            { OperatorTypeEnum.Resample, typeof(OperatorWarningValidator_Resample) },
            { OperatorTypeEnum.SawTooth, typeof(OperatorWarningValidator_SawTooth) },
            { OperatorTypeEnum.Sample, typeof(OperatorWarningValidator_Sample) },
            { OperatorTypeEnum.Select, typeof(OperatorWarningValidator_Select) },
            { OperatorTypeEnum.Sine, typeof(OperatorWarningValidator_Sine) },
            { OperatorTypeEnum.SquareWave, typeof(OperatorWarningValidator_SquareWave) },
            { OperatorTypeEnum.Subtract, typeof(OperatorWarningValidator_Subtract) },
            { OperatorTypeEnum.Delay, typeof(OperatorWarningValidator_Delay) },
            { OperatorTypeEnum.SpeedUp, typeof(OperatorWarningValidator_SpeedUp) },
            { OperatorTypeEnum.SlowDown, typeof(OperatorWarningValidator_SlowDown) },
            { OperatorTypeEnum.TriangleWave, typeof(OperatorWarningValidator_TriangleWave) },
            { OperatorTypeEnum.TimePower, typeof(OperatorWarningValidator_TimePower) },
            { OperatorTypeEnum.Earlier, typeof(OperatorWarningValidator_Earlier) },
            { OperatorTypeEnum.Number, typeof(OperatorWarningValidator_Number) }
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
                Execute(validatorType);
            }
        }
    }
}
