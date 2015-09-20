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
    public class OperatorValidator_Versatile : ValidatorBase<Operator>
    {
        private IDocumentRepository _documentRepository;

        private Dictionary<OperatorTypeEnum, Type> _validatorTypeDictionary = new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.Adder, typeof(OperatorValidator_Adder) },
            { OperatorTypeEnum.Add, typeof(OperatorValidator_Add) },
            { OperatorTypeEnum.Curve, typeof(OperatorValidator_Curve) },
            { OperatorTypeEnum.Divide, typeof(OperatorValidator_Divide) },
            { OperatorTypeEnum.Multiply, typeof(OperatorValidator_Multiply) },
            { OperatorTypeEnum.PatchInlet, typeof(OperatorValidator_PatchInlet) },
            { OperatorTypeEnum.PatchOutlet, typeof(OperatorValidator_PatchOutlet) },
            { OperatorTypeEnum.Power, typeof(OperatorValidator_Power) },
            { OperatorTypeEnum.Resample, typeof(OperatorValidator_Resample) },
            { OperatorTypeEnum.Sample, typeof(OperatorValidator_Sample) },
            { OperatorTypeEnum.Sine, typeof(OperatorValidator_Sine) },
            { OperatorTypeEnum.Substract, typeof(OperatorValidator_Substract) },
            { OperatorTypeEnum.Delay, typeof(OperatorValidator_Delay) },
            { OperatorTypeEnum.TimeDivide, typeof(OperatorValidator_TimeDivide) },
            { OperatorTypeEnum.TimeMultiply, typeof(OperatorValidator_TimeMultiply) },
            { OperatorTypeEnum.TimePower, typeof(OperatorValidator_TimePower) },
            { OperatorTypeEnum.TimeSubstract, typeof(OperatorValidator_TimeSubstract) },
            { OperatorTypeEnum.Value, typeof(OperatorValidator_Value) },
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
