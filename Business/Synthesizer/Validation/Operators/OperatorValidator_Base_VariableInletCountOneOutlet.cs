using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal abstract class OperatorValidator_Base_VariableInletCountOneOutlet : OperatorValidator_Base
    {
        public OperatorValidator_Base_VariableInletCountOneOutlet(
            Operator obj, 
            OperatorTypeEnum expectedOperatorTypeEnum,
            IList<string> expectedDataKeys)
            : base(
                obj,
                expectedOperatorTypeEnum,
                Enumerable.Repeat(DimensionEnum.Undefined, obj.Inlets.Count).ToArray(),
                new[] { DimensionEnum.Undefined },
                expectedDataKeys)
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Obj;

            For(() => op.Inlets.Count, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Inlets)).GreaterThan(0);
        }
    }
}