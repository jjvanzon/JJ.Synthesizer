using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Versatile : ValidatorBase<Operator>
    {
        private IDictionary<string, Type> _validatorTypeDictionary = new Dictionary<string, Type>
        {
            { PropertyNames.Add, typeof(OperatorWarningValidator_Add) },
            { PropertyNames.Adder, typeof(OperatorWarningValidator_Adder) },
            { PropertyNames.CurveIn, typeof(OperatorWarningValidator_CurveIn) },
            { PropertyNames.Divide, typeof(OperatorWarningValidator_Divide) },
            { PropertyNames.Multiply, typeof(OperatorWarningValidator_Multiply) },
            { PropertyNames.PatchOutlet, typeof(OperatorWarningValidator_PatchOutlet) },
            { PropertyNames.Power, typeof(OperatorWarningValidator_Power) },
            { PropertyNames.SampleOperator, typeof(OperatorWarningValidator_SampleOperator) },
            { PropertyNames.Sine, typeof(OperatorWarningValidator_Sine) },
            { PropertyNames.Substract, typeof(OperatorWarningValidator_Substract) },
            { PropertyNames.TimeAdd, typeof(OperatorWarningValidator_TimeAdd) },
            { PropertyNames.TimeDivide, typeof(OperatorWarningValidator_TimeDivide) },
            { PropertyNames.TimeMultiply, typeof(OperatorWarningValidator_TimeMultiply) },
            { PropertyNames.TimePower, typeof(OperatorWarningValidator_TimePower) },
            { PropertyNames.TimeSubstract, typeof(OperatorWarningValidator_TimeSubstract) },
            { PropertyNames.ValueOperator, typeof(OperatorWarningValidator_ValueOperator) }
        };

        public OperatorWarningValidator_Versatile(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Type validatorType;
            if (!_validatorTypeDictionary.TryGetValue(Object.OperatorTypeName, out validatorType))
            {
                ValidationMessages.Add(() => Object.OperatorTypeName, MessageFormatter.UnsupportedOperatorTypeName(Object.OperatorTypeName));
            }
            else
            {
                Execute(validatorType);
            }
        }
    }
}
