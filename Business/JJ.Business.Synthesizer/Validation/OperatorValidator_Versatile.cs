using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Versatile : ValidatorBase<Operator>
    {
        private IDictionary<string, Type> _validatorTypeDictionary = new Dictionary<string, Type>
        {
            { PropertyNames.Adder, typeof(OperatorValidator_Adder) },
            { PropertyNames.Add, typeof(OperatorValidator_Add) },
            { PropertyNames.CurveIn, typeof(OperatorValidator_CurveIn) },
            { PropertyNames.Divide, typeof(OperatorValidator_Divide) },
            { PropertyNames.Multiply, typeof(OperatorValidator_Multiply) },
            { PropertyNames.PatchInlet, typeof(OperatorValidator_PatchInlet) },
            { PropertyNames.PatchOutlet, typeof(OperatorValidator_PatchOutlet) },
            { PropertyNames.Power, typeof(OperatorValidator_Power) },
            { PropertyNames.SampleOperator, typeof(OperatorValidator_SampleOperator) },
            { PropertyNames.Sine, typeof(OperatorValidator_Sine) },
            { PropertyNames.Substract, typeof(OperatorValidator_Substract) },
            { PropertyNames.TimeAdd, typeof(OperatorValidator_TimeAdd) },
            { PropertyNames.TimeDivide, typeof(OperatorValidator_TimeDivide) },
            { PropertyNames.TimeMultiply, typeof(OperatorValidator_TimeMultiply) },
            { PropertyNames.TimePower, typeof(OperatorValidator_TimePower) },
            { PropertyNames.TimeSubstract, typeof(OperatorValidator_TimeSubstract) },
            { PropertyNames.ValueOperator, typeof(OperatorValidator_ValueOperator) }
        };

        public OperatorValidator_Versatile(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Execute<OperatorValidator_Basic>();

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
