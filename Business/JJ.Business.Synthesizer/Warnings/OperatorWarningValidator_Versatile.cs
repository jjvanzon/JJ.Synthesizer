using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Versatile : ValidatorBase<Operator>
    {
        private IDictionary<OperatorTypeEnum, Type> _validatorTypeDictionary = new Dictionary<OperatorTypeEnum, Type>
        {
            { OperatorTypeEnum.Add, typeof(OperatorWarningValidator_Add) },
            { OperatorTypeEnum.Adder, typeof(OperatorWarningValidator_Adder) },
            { OperatorTypeEnum.CurveIn, typeof(OperatorWarningValidator_CurveIn) },
            { OperatorTypeEnum.Divide, typeof(OperatorWarningValidator_Divide) },
            { OperatorTypeEnum.Multiply, typeof(OperatorWarningValidator_Multiply) },
            { OperatorTypeEnum.PatchOutlet, typeof(OperatorWarningValidator_PatchOutlet) },
            { OperatorTypeEnum.Power, typeof(OperatorWarningValidator_Power) },
            { OperatorTypeEnum.Resample, typeof(OperatorWarningValidator_Resample) },
            { OperatorTypeEnum.Sample, typeof(OperatorWarningValidator_Sample) },
            { OperatorTypeEnum.Sine, typeof(OperatorWarningValidator_Sine) },
            { OperatorTypeEnum.Substract, typeof(OperatorWarningValidator_Substract) },
            { OperatorTypeEnum.TimeAdd, typeof(OperatorWarningValidator_TimeAdd) },
            { OperatorTypeEnum.TimeDivide, typeof(OperatorWarningValidator_TimeDivide) },
            { OperatorTypeEnum.TimeMultiply, typeof(OperatorWarningValidator_TimeMultiply) },
            { OperatorTypeEnum.TimePower, typeof(OperatorWarningValidator_TimePower) },
            { OperatorTypeEnum.TimeSubstract, typeof(OperatorWarningValidator_TimeSubstract) },
            { OperatorTypeEnum.Value, typeof(OperatorWarningValidator_Value) }
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
