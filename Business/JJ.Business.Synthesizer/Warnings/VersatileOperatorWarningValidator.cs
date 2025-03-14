﻿using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class VersatileOperatorWarningValidator : ValidatorBase<Operator>
    {
        private IDictionary<string, Type> _validatorTypeDictionary = new Dictionary<string, Type>
        {
            { PropertyNames.Add, typeof(AddWarningValidator) },
            { PropertyNames.Adder, typeof(AdderWarningValidator) },
            { PropertyNames.CurveIn, typeof(CurveInWarningValidator) },
            { PropertyNames.Divide, typeof(DivideWarningValidator) },
            { PropertyNames.Multiply, typeof(MultiplyWarningValidator) },
            { PropertyNames.PatchOutlet, typeof(PatchOutletWarningValidator) },
            { PropertyNames.Power, typeof(PowerWarningValidator) },
            { PropertyNames.SampleOperator, typeof(SampleOperatorWarningValidator) },
            { PropertyNames.Sine, typeof(SineWarningValidator) },
            { PropertyNames.Substract, typeof(SubstractWarningValidator) },
            { PropertyNames.TimeAdd, typeof(TimeAddWarningValidator) },
            { PropertyNames.TimeDivide, typeof(TimeDivideWarningValidator) },
            { PropertyNames.TimeMultiply, typeof(TimeMultiplyWarningValidator) },
            { PropertyNames.TimePower, typeof(TimePowerWarningValidator) },
            { PropertyNames.TimeSubstract, typeof(TimeSubstractWarningValidator) },
            { PropertyNames.ValueOperator, typeof(ValueOperatorWarningValidator) }
        };

        public VersatileOperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Type validatorType;
            if (!_validatorTypeDictionary.TryGetValue(Object.OperatorTypeName, out validatorType))
            {
                ValidationMessages.Add(() => Object.OperatorTypeName, MessagesFormatter.UnsupportedOperatorTypeName(Object.OperatorTypeName));
            }
            else
            {
                Execute(validatorType);
            }
        }
    }
}
