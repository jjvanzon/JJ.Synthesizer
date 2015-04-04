using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    public class VersatileOperatorValidator : ValidatorBase<Operator>
    {
        private IDictionary<string, Type> _validatorTypeDictionary = new Dictionary<string, Type>
        {
            { PropertyNames.Adder, typeof(AdderValidator) },
            { PropertyNames.Add, typeof(AddValidator) },
            { PropertyNames.CurveIn, typeof(CurveInValidator) },
            { PropertyNames.Divide, typeof(DivideValidator) },
            { PropertyNames.Multiply, typeof(MultiplyValidator) },
            { PropertyNames.PatchInlet, typeof(PatchInletValidator) },
            { PropertyNames.PatchOutlet, typeof(PatchOutletValidator) },
            { PropertyNames.Power, typeof(PowerValidator) },
            { PropertyNames.SampleOperator, typeof(SampleOperatorValidator) },
            { PropertyNames.Sine, typeof(SineValidator) },
            { PropertyNames.Substract, typeof(SubstractValidator) },
            { PropertyNames.TimeAdd, typeof(TimeAddValidator) },
            { PropertyNames.TimeDivide, typeof(TimeDivideValidator) },
            { PropertyNames.TimeMultiply, typeof(TimeMultiplyValidator) },
            { PropertyNames.TimePower, typeof(TimePowerValidator) },
            { PropertyNames.TimeSubstract, typeof(TimeSubstractValidator) },
            { PropertyNames.ValueOperator, typeof(ValueOperatorValidator) }
        };

        public VersatileOperatorValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Execute<BasicOperatorValidator>();

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
