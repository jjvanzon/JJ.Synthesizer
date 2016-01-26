using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Versatile : ValidatorBase<Operator>
    {
        private IPatchRepository _patchRepository;

        private Dictionary<OperatorTypeEnum, Type> _validatorTypeDictionary = new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.Add, typeof(OperatorValidator_Add) },
            { OperatorTypeEnum.Adder, typeof(OperatorValidator_Adder) },
            { OperatorTypeEnum.Bundle, typeof(OperatorValidator_Bundle) },
            { OperatorTypeEnum.Curve, typeof(OperatorValidator_Curve) },
            { OperatorTypeEnum.Delay, typeof(OperatorValidator_Delay) },
            { OperatorTypeEnum.Divide, typeof(OperatorValidator_Divide) },
            { OperatorTypeEnum.Earlier, typeof(OperatorValidator_Earlier) },
            { OperatorTypeEnum.Exponent, typeof(OperatorValidator_Exponent) },
            { OperatorTypeEnum.Loop, typeof(OperatorValidator_Loop) },
            { OperatorTypeEnum.LowPass, typeof(OperatorValidator_LowPass) },
            { OperatorTypeEnum.Multiply, typeof(OperatorValidator_Multiply) },
            { OperatorTypeEnum.Narrower, typeof(OperatorValidator_Narrower) },
            { OperatorTypeEnum.Number, typeof(OperatorValidator_Number) },
            { OperatorTypeEnum.PatchInlet, typeof(OperatorValidator_PatchInlet) },
            { OperatorTypeEnum.PatchOutlet, typeof(OperatorValidator_PatchOutlet) },
            { OperatorTypeEnum.Power, typeof(OperatorValidator_Power) },
            { OperatorTypeEnum.Resample, typeof(OperatorValidator_Resample) },
            { OperatorTypeEnum.Reset, typeof(OperatorValidator_Reset) },
            { OperatorTypeEnum.Sample, typeof(OperatorValidator_Sample) },
            { OperatorTypeEnum.SawTooth, typeof(OperatorValidator_SawTooth) },
            { OperatorTypeEnum.Select, typeof(OperatorValidator_Select) },
            { OperatorTypeEnum.Shift, typeof(OperatorValidator_Shift) },
            { OperatorTypeEnum.Sine, typeof(OperatorValidator_Sine) },
            { OperatorTypeEnum.SlowDown, typeof(OperatorValidator_SlowDown) },
            { OperatorTypeEnum.SpeedUp, typeof(OperatorValidator_SpeedUp) },
            { OperatorTypeEnum.SquareWave, typeof(OperatorValidator_SquareWave) },
            { OperatorTypeEnum.Stretch, typeof(OperatorValidator_Stretch) },
            { OperatorTypeEnum.Subtract, typeof(OperatorValidator_Subtract) },
            { OperatorTypeEnum.TimePower, typeof(OperatorValidator_TimePower) },
            { OperatorTypeEnum.TriangleWave, typeof(OperatorValidator_TriangleWave) },
            { OperatorTypeEnum.Unbundle, typeof(OperatorValidator_Unbundle) },
            { OperatorTypeEnum.WhiteNoise, typeof(OperatorValidator_WhiteNoise) }
        };

        public OperatorValidator_Versatile(Operator obj, IPatchRepository patchRepository)
            : base(obj, postponeExecute: true)
        {
            _patchRepository = patchRepository;

            Execute();
        }

        protected override void Execute()
        {
            Execute<OperatorValidator_Basic>();

            if (Object.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
            {
                Execute(new OperatorValidator_CustomOperator(Object, _patchRepository));
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
