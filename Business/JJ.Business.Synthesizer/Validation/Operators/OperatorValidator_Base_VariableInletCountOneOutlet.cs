using System;
using System.Collections;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal abstract class OperatorValidator_Base_VariableInletCountOneOutlet : OperatorValidator_Base
    {
        public OperatorValidator_Base_VariableInletCountOneOutlet(
            Operator obj, 
            OperatorTypeEnum expectedOperatorTypeEnum,
            IList<string> allowedDataKeys)
            : base(
                  obj,
                  expectedOperatorTypeEnum,
                  expectedInletCount: obj.Inlets.Count,
                  expectedOutletCount: 1,
                  allowedDataKeys: allowedDataKeys)
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            For(() => op.Inlets.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets)).GreaterThan(0);
        }
    }
}