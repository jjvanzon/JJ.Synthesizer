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
        private IDocumentRepository _documentRepository;

        private Dictionary<OperatorTypeEnum, Type> _validatorTypeDictionary = new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.Adder, typeof(OperatorValidator_Adder) },
            { OperatorTypeEnum.Add, typeof(OperatorValidator_Add) },
            { OperatorTypeEnum.Bundle, typeof(OperatorValidator_Bundle) },
            { OperatorTypeEnum.Curve, typeof(OperatorValidator_Curve) },
            { OperatorTypeEnum.Divide, typeof(OperatorValidator_Divide) },
            { OperatorTypeEnum.Exponent, typeof(OperatorValidator_Exponent) },
            { OperatorTypeEnum.Loop, typeof(OperatorValidator_Loop) },
            { OperatorTypeEnum.Multiply, typeof(OperatorValidator_Multiply) },
            { OperatorTypeEnum.PatchInlet, typeof(OperatorValidator_PatchInlet) },
            { OperatorTypeEnum.PatchOutlet, typeof(OperatorValidator_PatchOutlet) },
            { OperatorTypeEnum.Power, typeof(OperatorValidator_Power) },
            { OperatorTypeEnum.Resample, typeof(OperatorValidator_Resample) },
            { OperatorTypeEnum.Sample, typeof(OperatorValidator_Sample) },
            { OperatorTypeEnum.Select, typeof(OperatorValidator_Select) },
            { OperatorTypeEnum.Sine, typeof(OperatorValidator_Sine) },
            { OperatorTypeEnum.SquareWave, typeof(OperatorValidator_SquareWave) },
            { OperatorTypeEnum.Subtract, typeof(OperatorValidator_Subtract) },
            { OperatorTypeEnum.Delay, typeof(OperatorValidator_Delay) },
            { OperatorTypeEnum.SawTooth, typeof(OperatorValidator_SawTooth) },
            { OperatorTypeEnum.SpeedUp, typeof(OperatorValidator_SpeedUp) },
            { OperatorTypeEnum.SlowDown, typeof(OperatorValidator_SlowDown) },
            { OperatorTypeEnum.TimePower, typeof(OperatorValidator_TimePower) },
            { OperatorTypeEnum.Earlier, typeof(OperatorValidator_Earlier) },
            { OperatorTypeEnum.TriangleWave, typeof(OperatorValidator_TriangleWave) },
            { OperatorTypeEnum.Number, typeof(OperatorValidator_Number) },
            { OperatorTypeEnum.Unbundle, typeof(OperatorValidator_Unbundle) },
            { OperatorTypeEnum.WhiteNoise, typeof(OperatorValidator_WhiteNoise) },
        };

        public OperatorValidator_Versatile(Operator obj, IDocumentRepository documentRepository)
            : base(obj, postponeExecute: true)
        {
            _documentRepository = documentRepository;

            Execute();
        }

        protected override void Execute()
        {
            Execute<OperatorValidator_Basic>();

            if (Object.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
            {
                Execute(new OperatorValidator_CustomOperator(Object, _documentRepository));
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
